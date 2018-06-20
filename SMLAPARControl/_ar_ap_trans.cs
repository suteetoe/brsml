using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.Threading;

namespace SMLERPAPARControl
{
    public partial class _ar_ap_trans : UserControl
    {
        public delegate void _afterSaveEventHandler();
        public event _afterSaveEventHandler _afterSave;

        private MyLib._myPanel _myPanelGl;
        private System.Windows.Forms.TabPage _tab_gl;
        private SMLERPGLControl._journalScreen _glScreenTop;
        private SMLERPAPARControl.ap_ar_trans_screen_more _screenMore;
        private SMLERPGLControl._glDetail _glDetail;

        private int _transFlag = 0;
        private int _transType = 0;
        // เอาไว้สั่งประมวลผล
        private string _processCode = "";
        private string _proceeeOldCode = "";
        //
        private string _oldDocNo = "";
        public string _oldDocRef = "";

        private _ap_ar_gl _gl = new _ap_ar_gl();
        private SMLERPAPARControl._payControl _payControl;
        private SMLERPGLControl._withHoldingTax _withHoldingTax;
        private SMLERPGLControl._vatBuy _vatBuy;
        private SMLERPGLControl._vatSale _vatSale;
        private _g.g._transControlTypeEnum _controlTypeTemp;
        private bool _showPrintDialogByCtrl = false;

        public bool _printFormAfterSave = false;
        private string _logDocDateOld;
        private string _logDocNoOld;
        private decimal _logAmountOld;
        private StringBuilder _logDetailOld;

        public string _menuName = "";
        string _tableName = "";


        // toe
        private string _creator_code = "";
        private DateTime _create_datetime = new DateTime();

        public delegate Boolean _customPrintEventArgs(string docType, string docNo, string transFlag, bool showOption);

        /// <summary>
        /// Event สำหรับแก้ไข Process พิมพ์ แบบ manual
        /// </summary>
        public event _customPrintEventArgs _customPrint;

        private string _glDeleteQuery(string docNo)
        {
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal._table + " where " + _g.d.gl_journal._doc_no + "=\'" + docNo + "\'"));
            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._doc_no + "=\'" + docNo + "\'"));
            return __myQuery.ToString();
        }

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

        public string _docTypeCode
        {
            get
            {
                return "";
            }
        }

        public _g.g._transControlTypeEnum _transControlType
        {
            set
            {
                if (value != _g.g._transControlTypeEnum.ว่าง)
                {
                    this._controlTypeTemp = value;
                    this._screenTop._transControlType = value;
                    this._detailGrid._transControlType = value;
                    this._screenBottom._transControlType = value;
                    this._refDoc._transControlType = value;
                    this._withHoldingTax = null;
                    this._load();
                    this._detailGrid._keyDown -= new MyLib.KeyDownEventHandler(_screenGrid__keyDown);
                    this._detailGrid._keyDown += new MyLib.KeyDownEventHandler(_screenGrid__keyDown);
                    this._detailGrid._alterCellUpdate -= new MyLib.AfterCellUpdateEventHandler(_screenGrid__alterCellUpdate);
                    this._detailGrid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_screenGrid__alterCellUpdate);
                    this._detailGrid._clickSearchButton -= new MyLib.SearchEventHandler(_screenGrid__clickSearchButton);
                    this._detailGrid._clickSearchButton += new MyLib.SearchEventHandler(_screenGrid__clickSearchButton);
                    this._tmbReceiveMoney.Click -= new EventHandler(_tmbReceiveMoney_Click);
                    this._tmbReceiveMoney.Click += new EventHandler(_tmbReceiveMoney_Click);
                    this._refDoc._tsbProcess.Click -= new EventHandler(_tsbProcess_Click);
                    this._refDoc._tsbProcess.Click += new EventHandler(_tsbProcess_Click);
                    this._screenTop._textBoxChanged -= new MyLib.TextBoxChangedHandler(_screenTop__textBoxChanged);
                    this._screenTop._textBoxChanged += new MyLib.TextBoxChangedHandler(_screenTop__textBoxChanged);
                    this._screenTop._textBoxSaved += _screenTop__textBoxSaved;
                    this._detailGrid._screen = this;
                    this._detailGrid._queryForInsertCheck -= new MyLib.QueryForInsertCheckEventHandler(_screenGrid__queryForInsertCheck);
                    this._detailGrid._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_screenGrid__queryForInsertCheck);
                    this._detailGrid._afterCalcTotal -= new MyLib.AfterCalcTotalEventHandler(_screenGrid__afterCalcTotal);
                    this._detailGrid._afterCalcTotal += new MyLib.AfterCalcTotalEventHandler(_screenGrid__afterCalcTotal);
                    this._myManageData1._dataList._gridData._beforeDisplayRow -= new MyLib.BeforeDisplayRowEventHandler(_gridData__beforeDisplayRow);
                    this._myManageData1._dataList._gridData._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_gridData__beforeDisplayRow);
                    this._screenTop._managerData = this._myManageData1;
                    this._detailGrid._getCustCode -= new _ar_ap_trans_grid._getCustCodeEventHandler(_screenGrid__getCustCode);
                    this._detailGrid._getCustCode += new _ar_ap_trans_grid._getCustCodeEventHandler(_screenGrid__getCustCode);
                    this._detailGrid._getProcessDate -= new _ar_ap_trans_grid._getProcessDateEventHandler(_screenGrid__getProcessDate);
                    this._detailGrid._getProcessDate += new _ar_ap_trans_grid._getProcessDateEventHandler(_screenGrid__getProcessDate);


                    this._screenBottom._textBoxChanged -= new MyLib.TextBoxChangedHandler(_screenBottom__textBoxChanged);
                    this._screenBottom._textBoxChanged += new MyLib.TextBoxChangedHandler(_screenBottom__textBoxChanged);

                    this._build();
                    this.Invalidate();
                }
            }
            get
            {
                return this._controlTypeTemp;
            }
        }

        private void _screenTop__textBoxSaved(object sender, string name)
        {
            if (this._withHoldingTax != null)
            {
                this._withHoldingTax._custCode = this._screenTop._getDataStr(_g.d.ic_trans._cust_code);
                this._withHoldingTax._docDate = MyLib._myGlobal._convertDate(this._screenTop._getDataStr(_g.d.ic_trans._doc_date));
            }
        }

        void _screenBottom__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ap_ar_trans._total_pay_tax))
            {
                this._reCalc();
            }
        }

        void _build()
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                switch (this._transControlType)
                {
                    //(เจ้าหนี้)   
                    //----------------------------------------------------------------------------------
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้ยกมา:
                        {
                            this._screenTop._screen_code = "DC";
                        }
                        break;
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้ยกมา:
                        {
                            this._screenTop._screen_code = "DA";
                        }
                        break;
                    case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้ยกมา:
                        {
                            this._screenTop._screen_code = "DB";
                        }
                        break;
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น_ยกเลิก:
                    //(ลูกหนี้)   
                    //----------------------------------------------------------------------------------
                    case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้ยกมา:
                    case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้ยกมา:
                    case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้ยกมา:
                        this._grouper1.Visible = true;
                        break;
                    case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น_ยกเลิก:
                    case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น_ยกเลิก:
                    case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น_ยกเลิก:
                        this._grouper1.Visible = false;
                        break;
                    case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                        {
                            this._screenTop._screen_code = "DE";
                            _toolBar.Visible = true;
                            _tmbReceiveMoney.Text = "เลือก ใบรับวางบิล (เจ้าหนี้)";
                            _print.Visible = true; //anek print
                            //
                            this._withHoldingTax = new SMLERPGLControl._withHoldingTax(SMLERPGLControl._withHoldingTaxType.เจ้าหนี้_จ่ายชำระหนี้);
                            this._payControl = new _payControl();
                            this._vatBuy = new SMLERPGLControl._vatBuy();
                            this._vatBuy._searchVatBuyButton.Visible = true;
                            this._vatBuy._getCustCode += _vatBuy__getCustCode;

                            this._withHoldingTax.printSeparator.Visible = true;
                            this._withHoldingTax._printWithHoldingTagButton.Visible = true;
                            this._withHoldingTax._printWithHoldingTagButton.Click += _printWithHoldingTagButton_Click;
                            this._withHoldingTax._printSelectRecordButton.Visible = true;
                            this._withHoldingTax._printSelectRecordButton.Click += _printSelectRecordButton_Click;


                        }
                        break;
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล:
                        {
                            this._screenTop._screen_code = "DD";
                            _print.Visible = true; //anek print
                        }
                        break;
                    case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                        {
                            _toolBar.Visible = true;
                            _tmbReceiveMoney.Text = "เลือก ใบวางบิล (ลูกหนี้)";
                            _print.Visible = true; //anek print
                            this._withHoldingTax = new SMLERPGLControl._withHoldingTax(SMLERPGLControl._withHoldingTaxType.ลูกหนี้_รับชำระหนี้);
                            this._payControl = new _payControl();
                            this._vatSale = new SMLERPGLControl._vatSale();
                        }
                        break;
                    case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล:
                        {
                            _print.Visible = true; //anek print
                        }
                        break;
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล_ยกเลิก:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก:
                    case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล_ยกเลิก:
                    case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก:
                        {
                            this._grouper1.Visible = false;
                        }
                        break;
                    case _g.g._transControlTypeEnum.IMEX_Bill_Collector:
                        {
                            _print.Visible = true; //anek print
                            this._screenTop._screen_code = "BLC";

                        }
                        break;
                }
            }
            // จ่ายเงิน
            string __tabPayCode = "tab_pay";
            if (this._payControl == null)
            {
                this._tab.TabPages[__tabPayCode].Dispose();
            }
            else
            {
                this._payControl._icTransControlType = this._transControlType;
                this._payControl.Dock = DockStyle.Fill;
                this._payControl._getTotalAmount -= new SMLERPAPARControl._payControl._getTotalAmountEvent(_payControl__getTotalAmount);
                this._payControl._getTotalAmount += new SMLERPAPARControl._payControl._getTotalAmountEvent(_payControl__getTotalAmount);
                this._payControl._getTotalTaxAmount -= new SMLERPAPARControl._payControl._getTotalTaxAmountEvent(_payControl__getTotalTaxAmount);
                this._payControl._getTotalTaxAmount += new SMLERPAPARControl._payControl._getTotalTaxAmountEvent(_payControl__getTotalTaxAmount);
                this._payControl._getCustCode -= new SMLERPAPARControl._payControl._getCustCodeEvent(_payControl__getCustCode);
                this._payControl._getCustCode += new SMLERPAPARControl._payControl._getCustCodeEvent(_payControl__getCustCode);
                this._payControl._payDepositGrid._getCustCode -= new _payDepositAdvanceGridControl._getCustCodeEvent(_payDepositGrid__getCustCode);
                this._payControl._payDepositGrid._getCustCode += new _payDepositAdvanceGridControl._getCustCodeEvent(_payDepositGrid__getCustCode);
                this._payControl._payDepositGrid._getProcessDate -= new _payDepositAdvanceGridControl._getProcessDateEvent(_payDepositGrid__getProcessDate);
                this._payControl._payDepositGrid._getProcessDate += new _payDepositAdvanceGridControl._getProcessDateEvent(_payDepositGrid__getProcessDate);
                this._tab.TabPages[__tabPayCode].Controls.Add(this._payControl);
                string __tabName = "";
                switch (this._transControlType)
                {
                    case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                        __tabName = _g.d.ic_trans._tab_pay_out;
                        break;
                    case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                        __tabName = _g.d.ic_trans._tab_pay_in;
                        break;
                }
                this._tab.TabPages[__tabPayCode].Tag = __tabName;
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
                }
                else
                    if (this._vatSale != null)
                {
                    this._vatSale.Dock = DockStyle.Fill;
                    this._tab.TabPages[__tabVatCode].Controls.Add(this._vatSale);
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
                this._withHoldingTax._mainGrid._afterCalcTotal -= new MyLib.AfterCalcTotalEventHandler(_mainGrid__afterCalcTotal);
                this._withHoldingTax._mainGrid._afterCalcTotal += new MyLib.AfterCalcTotalEventHandler(_mainGrid__afterCalcTotal);
                this._tab.TabPages[__tabWHTCode].Controls.Add(this._withHoldingTax);
                string __tabName = "";
                switch (this._transControlType)
                {
                    case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                        __tabName = _g.d.ic_trans._tab_wht_out;
                        break;
                    case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                        __tabName = _g.d.ic_trans._tab_wht_in;
                        break;
                }
                this._tab.TabPages[__tabWHTCode].Tag = __tabName;
                //this._withHoldingTax._mainGrid._afterCalcTotal += new MyLib.AfterCalcTotalEventHandler(_mainGrid__afterCalcTotal);
            }

            string __tabMore = "tab_more";
            if (_g.g._companyProfile._branchStatus == 1)
            {
                switch (this._transControlType)
                {
                    case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล_ยกเลิก:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น_ยกเลิก:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก:

                    case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก:
                    case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล_ยกเลิก:
                    case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น_ยกเลิก:
                    case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น_ยกเลิก:
                    case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น_ยกเลิก:
                        break;
                    default:
                        {
                            _screenMore = new ap_ar_trans_screen_more();
                            _screenMore._icTransControlType = this._transControlType;
                        }
                        break;
                }

            }

            if (_screenMore == null)
            {
                this._tab.TabPages[__tabMore].Dispose();
            }
            else
            {
                this._tab.TabPages[__tabMore].Controls.Add(this._screenMore);
                this._screenMore.Dock = DockStyle.Fill;
                this._tab.TabPages[__tabMore].Tag = _g.d.ap_ar_trans._tab_more;

            }

            switch (this._transControlType)
            {
                case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้ยกมา:
                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้ยกมา:
                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้ยกมา:
                case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้ยกมา:
                case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้ยกมา:
                case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้ยกมา:
                    this._tab.TabPages.Clear();
                    this._glCreate();

                    if (_screenMore != null)
                        this._tab.Controls.Add(this.tab_more);
                    break;
                default:

                    break;
            }
            switch (this._transControlType)
            {
                case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                case _g.g._transControlTypeEnum.เจ้าหนี้_ตัดหนี้สูญ:
                case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                case _g.g._transControlTypeEnum.ลูกหนี้_ตัดหนี้สูญ:
                    this._glCreate();
                    break;
            }

            // tab more
            //
            this._tab._getResource();
            //
            this._myManageData1__newDataClick();
            this._screenTop._setDataDate(_g.d.ap_ar_trans._doc_date, MyLib._myGlobal._workingDate);


            if (this._withHoldingTax != null)
            {
                this._withHoldingTax._getDueDateEventArgs += _withHoldingTax__getDueDateEventArgs;
            }

            this._tableName = _g.d.ap_ar_trans._table;
            if (_transFlag > 80 && _transFlag < 200)
            {
                // กรณีพวกตั้งหนี้
                _tableName = _g.d.ic_trans._table;
            }
        }

        private void _printSelectRecordButton_Click(object sender, EventArgs e)
        {
            if (this._myManageData1._mode == 2)
            {
                int __docNoColumnIndex = this._withHoldingTax._mainGrid._findColumnByName(_g.d.gl_wht_list._tax_doc_no);
                string __getDocNo = this._withHoldingTax._mainGrid._cellGet(this._withHoldingTax._mainGrid._selectRow, __docNoColumnIndex).ToString();
                bool __printResult = SMLERPReportTool._global._printForm(this._screenTop._docFormatCode, __getDocNo, this._transFlag.ToString(), false);

            }
            else
            {
                MessageBox.Show(MyLib._myGlobal._resource("ยังไม่มีการบันทึกเอกสาร"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DateTime _withHoldingTax__getDueDateEventArgs()
        {
            return this._screenTop._getDataDate(_g.d.ic_trans._doc_date);
        }

        void _printWithHoldingTagButton_Click(object sender, EventArgs e)
        {
            if (this._myManageData1._mode == 2)
            {
                // call print range
                ArrayList __printList = new ArrayList();
                StringBuilder __packDocNoList = new StringBuilder();
                int __docNoColumnIndex = this._withHoldingTax._mainGrid._findColumnByName(_g.d.gl_wht_list._tax_doc_no);

                for (int __row = 0; __row < this._withHoldingTax._mainGrid._rowData.Count; __row++)
                {
                    int __getRow = __row;
                    List<SMLERPReportTool._ReportToolCondition> __condition = new List<SMLERPReportTool._ReportToolCondition>();
                    string __getDocNo = this._withHoldingTax._mainGrid._cellGet(__getRow, __docNoColumnIndex).ToString();
                    __condition.Add(new SMLERPReportTool._ReportToolCondition(_g.d.gl_journal._doc_no, __getDocNo));

                    __condition.Add(new SMLERPReportTool._ReportToolCondition(_g.d.ic_trans._trans_flag, this._transFlag.ToString()));
                    __printList.Add(__condition.ToArray());

                    // pack doc_no List 

                    if (__packDocNoList.Length > 0)
                    {
                        __packDocNoList.Append(",");
                    }
                    __packDocNoList.Append("\'" + __getDocNo + "\'");
                }
                // ใส่ค่าว่างไปก่อน ค่อย query ดึงจากรหัสหน้าจอเอา
                string __queryGetDocFormat = "select " + _g.d.erp_doc_format._form_code + " from " + _g.d.erp_doc_format._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_doc_format._code) + "=\'" + this._screenTop._docFormatCode + "\'";
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __result = __myFrameWork._queryShort(__queryGetDocFormat).Tables[0];

                StringBuilder __doc_format_code = new StringBuilder();
                if (__result.Rows.Count > 0)
                {
                    for (int __loop = 0; __loop < __result.Rows.Count; __loop++)
                    {
                        if (__doc_format_code.Length > 0)
                        {
                            __doc_format_code.Append(",");
                        }
                        __doc_format_code.Append("" + __result.Rows[__loop][_g.d.erp_doc_format._form_code].ToString() + ""); // ไปหาเอา // this._screenTop._getDataStr(_g.d.gl_journal._doc_format_code); 
                    }
                }
                // string __doc_format_code 
                // send key CTRL เพื่อดึง ไม่ต้องถามอีกให้ปล่อยไปก่อน
                bool __printResult = SMLERPReportTool._global._printRangeForm("", __printList, true, __doc_format_code.ToString());
            }
            else
            {
                MessageBox.Show(MyLib._myGlobal._resource("ยังไม่มีการบันทึกเอกสาร"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        string _vatBuy__getCustCode()
        {
            string __getCustCode = this._screenTop._getDataStr(_g.d.ap_ar_trans._cust_code);
            if (__getCustCode.Length > 0)
            {
                return __getCustCode;
            }
            else
            {
                MessageBox.Show(MyLib._myGlobal._resource(MyLib._myGlobal._resource("ยังไม่ได้ใส่รหัสลูกค้า")));
                Control __getControl = this._screenTop._getControl(_g.d.ic_trans._cust_code);
                __getControl.Focus();
            }
            return "";
        }

        void _mainGrid__afterCalcTotal(object sender)
        {
            if (this._payControl != null)
            {
                this._withHoldingTax._mainGrid._calcTotal(false);
                this._payControl._reCalc();
            }
        }

        string _payControl__getCustCode()
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

        decimal _payControl__getTotalTaxAmount()
        {
            // ดึง ภาษี หัก ณ. ที่จ่าย
            if (this._withHoldingTax == null)
            {
                return 0M;
            }
            this._withHoldingTax._mainGrid._calcTotal(false);
            decimal __result = ((MyLib._myGrid._columnType)this._withHoldingTax._mainGrid._columnList[this._withHoldingTax._mainGrid._findColumnByName(_g.d.gl_wht_list._tax_value)])._total;
            //Console.WriteLine(__result.ToString());
            return __result;
        }

        decimal _payControl__getTotalAmount()
        {
            // ดึงยอดรวมไปหน้าจอจ่ายเงิน
            return this._screenBottom._getDataNumber(_g.d.ap_ar_trans._total_net_value);
        }

        public _ar_ap_trans()
        {
            InitializeComponent();

            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolbar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this._toolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
            {
                _g.g._checkOpenPeriod();
            }

            this._logDetailOld = new StringBuilder();

            //
            //this._myManageData1._dataList._loadViewData(0);
            //
        }

        /// <summary>
        /// grid ขอวันที่
        /// </summary>
        /// <returns></returns>
        DateTime _screenGrid__getProcessDate()
        {
            return MyLib._myGlobal._convertDate(this._screenTop._getDataStr(_g.d.ap_ar_trans._doc_date));
        }

        /// <summary>
        /// grid ขอรหัสลูกค้า/เจ้าหนี้
        /// </summary>
        /// <returns></returns>
        string _screenGrid__getCustCode()
        {
            return this._screenTop._getDataStr(_g.d.ap_ar_trans._cust_code);
        }

        MyLib.BeforeDisplayRowReturn _gridData__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData)
        {
            MyLib.BeforeDisplayRowReturn __result = senderRow;
            try
            {
                int __usedStatusColumn = sender._findColumnByName(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._used_status);
                int __docSuccessColumn = sender._findColumnByName(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_success);
                int __lastStatusColumn = sender._findColumnByName(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._last_status);

                int __usedStatusColumn2 = -1;
                switch (this._transControlType)
                {
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้ยกมา:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้ยกมา:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้ยกมา:

                    case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:

                    case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้ยกมา:
                    case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้ยกมา:
                    case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้ยกมา:

                    case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                    case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                    case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                        __usedStatusColumn = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._used_status);
                        __docSuccessColumn = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_success);
                        __lastStatusColumn = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._last_status);
                        __usedStatusColumn2 = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._used_status_2);
                        break;

                }



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

                if (__usedStatusColumn2 != -1)
                {
                    // มีการนำไปใช้แล้ว
                    int __status = MyLib._myGlobal._intPhase(sender._cellGet(row, __usedStatusColumn2).ToString());
                    if (__status == 1)
                    {
                        __result.newColor = Color.LightSeaGreen;
                    }
                }
            }
            catch
            {
            }
            return (__result);
        }

        void _screenGrid__afterCalcTotal(object sender)
        {
            _reCalc();
        }

        bool _screenGrid__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            // ตรวจว่าบรรทัดไหนจะ Save
            int __billingNoColumnNumber = this._detailGrid._findColumnByName(_g.d.ap_ar_trans_detail._billing_no);
            if (__billingNoColumnNumber != -1)
            {
                string __docNo = this._detailGrid._cellGet(row, __billingNoColumnNumber).ToString().Trim();
                if (__docNo.Length == 0)
                {
                    return false;
                }
            }
            return true;
        }

        void _screenTop__textBoxChanged(object sender, string name)
        {
            this._refDoc._custCode = this._screenTop._getDataStr(_g.d.ap_ar_trans._cust_code);
            // toe
            /*
            try
            {
                if (name.Equals(_g.d.ap_ar_trans._doc_no))
                {
                    string __docNo = this._screenTop._getDataStr(_g.d.ic_trans._doc_no);
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    DataTable __getFormat = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._format + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._code + "=\'" + __docNo + "\'").Tables[0];
                    if (__getFormat.Rows.Count > 0)
                    {
                        string __format = __getFormat.Rows[0][0].ToString();
                        this._screenTop._docFormatCode = __getFormat.Rows[0][0].ToString();

                        string __newDoc = "";
                        if (_transControlType.Equals(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้) ||
                            _transControlType.Equals(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก) ||
                            _transControlType.Equals(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้) ||
                            _transControlType.Equals(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก) ||
                            _transControlType.Equals(_g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล) ||
                            _transControlType.Equals(_g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล))
                        {
                            __newDoc = _g.g._getAutoRun(_g.g._autoRunType.เจ้าหนี้_ลูกหนี้รายวัน, __docNo, this._screenTop._getDataStr(_g.d.ap_ar_trans._doc_date).ToString(), __format, _g.g._transControlTypeEnum.ว่าง, this._transControlType);
                        }
                        else
                        {
                            __newDoc = _g.g._getAutoRun(_g.g._autoRunType.ข้อมูลรายวัน, __docNo, this._screenTop._getDataStr(_g.d.ic_trans._doc_date).ToString(), __format, _g.g._transControlTypeEnum.ว่าง, this._transControlType);
                        }
                        this._screenTop._setDataStr(_g.d.ic_trans._doc_no, __newDoc, "", true);
                    }
                }
                else
                {
                    this._refDoc._custCode = this._screenTop._getDataStr(_g.d.ap_ar_trans._cust_code);
                    if (this._refDoc != null)
                    {
                        this._refDoc._custCode = this._screenTop._getDataStr(_g.d.ic_trans._cust_code);
                        this._refDoc._transGrid._clear();
                    }
                }
            }
            catch
            {
            }
            */
            this._glScreenCheck();
        }

        void _tsbProcess_Click(object sender, EventArgs e)
        {
            try
            {
                MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
                string __cust_code = this._screenTop._getDataStr(_g.d.ap_ar_trans._cust_code).ToString();
                string __in_data = "";
                for (int _row = 0; _row < this._refDoc._transGrid._rowData.Count; _row++)
                {
                    string _doc_no = this._refDoc._transGrid._cellGet(_row, _g.d.ap_ar_trans_detail._billing_no).ToString();
                    if (_doc_no.Length > 0)
                    {
                        string __c = (__in_data.Length == 0) ? "" : ",";
                        __in_data += __c + "'" + _doc_no + "'";
                    }
                }

                // 13=รับวางบิล  , 35=วางบิล
                int ___transFlag = 0;
                switch (this._transControlType)
                {
                    case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้: ___transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล); break;
                    case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้: ___transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล); break;
                }
                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append("select " + _g.d.ap_ar_trans_detail._billing_no + "," + _g.d.ap_ar_trans_detail._doc_no + "," + _g.d.ap_ar_trans_detail._sum_pay_money);
                __myQuery.Append(" from " + _g.d.ap_ar_trans_detail._table);
                __myQuery.Append(" where (" + _g.d.ap_ar_trans_detail._trans_flag + "=" + ___transFlag + ") and (" + _g.d.ap_ar_trans._doc_no + " in (" + __in_data + "))");

                //if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only == true && (_g.g._companyProfile._change_branch_code == false))
                //{              
                //    __myQuery.Append(" and ( " + _g.d.ap_ar_resource._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\') ");
                //}

                __myQuery.Append(" order by " + _g.d.ap_ar_trans_detail._line_number);
                DataSet __getData = _myFrameWork._query(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (__getData.Tables[0].Rows.Count > 0)
                {
                    this._detailGrid._clear();
                    for (int __loop = 0; __loop < __getData.Tables[0].Rows.Count; __loop++)
                    {
                        string __billingNo = __getData.Tables[0].Rows[__loop][_g.d.ap_ar_trans_detail._billing_no].ToString();
                        string __docNo = __getData.Tables[0].Rows[__loop][_g.d.ap_ar_trans_detail._doc_no].ToString();
                        decimal _sumPayAmount = MyLib._myGlobal._decimalPhase(__getData.Tables[0].Rows[__loop][_g.d.ap_ar_trans_detail._sum_pay_money].ToString());

                        int __rowNum = this._detailGrid._addRow();
                        this._detailGrid._cellUpdate(__rowNum, _g.d.ap_ar_trans_detail._billing_no, __billingNo, true);



                        decimal __balance_ref = (decimal)this._detailGrid._cellGet(__rowNum, _g.d.ap_ar_trans_detail._balance_ref);
                        if (__balance_ref != 0M)
                        {
                            this._detailGrid._cellUpdate(__rowNum, _g.d.ap_ar_trans_detail._doc_ref, __docNo, false);
                            this._detailGrid._cellUpdate(__rowNum, _g.d.ap_ar_trans_detail._sum_pay_money, __balance_ref, false);
                        }

                    }
                }
                this._detailGrid.Invalidate();
            }
            catch
            {
            }
        }

        public decimal _convertToNumber(string _str)
        {
            decimal _result = 0.00M;
            try
            {
                if (!_str.Equals(""))
                {
                    string __format = MyLib._myGlobal._getFormatNumber("m02");
                    _result = Convert.ToDecimal(string.Format(__format, MyLib._myGlobal._decimalPhase(_str)));
                }
            }
            catch
            {
                return 0.00M;
            }

            return _result;
        }

        void _tmbReceiveMoney_Click(object sender, EventArgs e)
        {
            switch (this._controlTypeTemp)
            {
                case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                    try
                    {
                        string __getCustCode = this._screenTop._getDataStr(_g.d.ap_ar_trans._cust_code);
                        if (__getCustCode.Length > 0)
                        {
                            this._refDoc.Visible = (this._refDoc.Visible) ? false : true;
                            this._refDoc._transGrid.Visible = true;
                            if (this._refDoc._transGrid._selectRow == 0)
                            {
                                if (this._saveButton.Enabled == true)
                                {
                                    this._refDoc._transGrid._selectRow = 0;
                                    this._refDoc._transGrid._selectColumn = 0;
                                    this._refDoc._transGrid._inputCell(this._refDoc._transGrid._selectRow, this._refDoc._transGrid._selectColumn);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show(MyLib._myGlobal._resource(MyLib._myGlobal._resource("ยังไม่ได้ใส่รหัสลูกค้า")));
                            Control __getControl = this._screenTop._getControl(_g.d.ic_trans._cust_code);
                            __getControl.Focus();
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                    try
                    {
                        string __getCustCode = this._screenTop._getDataStr(_g.d.ap_ar_trans._cust_code);
                        if (__getCustCode.Length > 0)
                        {
                            this._refDoc.Visible = (this._refDoc.Visible) ? false : true;
                            this._refDoc._transGrid.Visible = true;
                            if (this._refDoc._transGrid._selectRow == 0)
                            {
                                if (this._saveButton.Enabled == true)
                                {
                                    this._refDoc._transGrid._selectRow = 0;
                                    this._refDoc._transGrid._selectColumn = 0;
                                    this._refDoc._transGrid._inputCell(this._refDoc._transGrid._selectRow, this._refDoc._transGrid._selectColumn);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("ยังไม่ได้ใส่รหัสลูกค้า");
                            Control __getControl = this._screenTop._getControl(_g.d.ic_trans._cust_code);
                            __getControl.Focus();
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    break;

            }
        }

        void _load()
        {
            try
            {
                _myManageData1._dataListOpen = true;
                this._myManageData1._displayMode = 0;
                _myManageData1._selectDisplayMode(this._myManageData1._displayMode);
                _transFlag = _g.g._transFlagGlobal._transFlag(this._controlTypeTemp);
                _transType = _g.g._arapTransTypeGlobal._transType(this._controlTypeTemp);
                // this._myManageData1._dataList._extraWhere = " (" + _g.d.ap_ar_trans._trans_flag + " = " + _transFlag + ") and (" + _g.d.ap_ar_trans._trans_type + " = " + _transType + ")";
                this._myManageData1._dataList._extraWhereEvent += _dataList__extraWhereEvent;
                this._myManageData1._dataList._lockRecord = true; // ใช้แบบ Lock Record ต้องมี guid_code ด้วยนะ อย่าลืม
                this._myManageData1._dataList._isLockDoc = true;
                this._myManageData1._dataList._loadViewFormat(_g.g._arapLoadViewGlobal._loadViewName(this._controlTypeTemp, 0), MyLib._myGlobal._userSearchScreenGroup, true);
                //this._myManageData1._dataList._loadViewFormat(_g.g._screen_ap_ar_trans, MyLib._myGlobal._userSearchScreenGroup, true);
                //this._myManageData1._dataList._referFieldAdd(_arapReferFieldGlobal._referField(this._controlTypeTemp), 1);
                if (_transFlag < 80 || _transFlag > 200) this._myManageData1._dataList._referFieldAdd(_g.d.ap_ar_trans._doc_no, 1);
                if (_transFlag > 80 && _transFlag < 200) this._myManageData1._dataList._referFieldAdd(_g.d.ic_trans._doc_no, 1);
                this._myManageData1._manageButton = this._myToolbar;
                this._myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
                this._myManageData1._checkEditData += new MyLib.CheckEditDataEvent(_myManageData1__checkEditData);
                this._myManageData1._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
                this._myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
                this._myManageData1._newDataClick += new MyLib.NewDataEvent(_myManageData1__newDataClick);
                this._myManageData1._clearData += new MyLib.ClearDataEvent(_myManageData1__clearData);
                this._myManageData1._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);

                this._myManageData1._dataList._autoUpper = false;

                this._screenTop._saveKeyDown += new MyLib.SaveKeyDownHandler(_screenTop__saveKeyDown);
                this._myManageData1._calcArea();
                this._myManageData1._autoSize = true;

                if (MyLib._myGlobal._isUserLockDocument == true)
                {
                    this._myManageData1._dataList._buttonUnlockDoc.Visible = true;
                    this._myManageData1._dataList._buttonLockDoc.Visible = true;
                    this._myManageData1._dataList._separatorLockDoc.Visible = true;
                }
            }
            catch
            {
            }
        }

        bool _myManageData1__checkEditData(int row, MyLib._myGrid sender)
        {
            return _myManageData1__checkEditData(row, sender, true);
        }

        bool _myManageData1__checkEditData(int row, MyLib._myGrid sender, bool checkCancel)
        {
            if (MyLib._myGlobal._isUserTest)
            {
                //return true;
            }

            if (MyLib._myGlobal._OEMVersion == "imex" &&
                (this._transControlType == _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น || this._transControlType == _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้))
            {
                return true;
            }

            int __usedStatus = 0;
            int __usedStatus2 = 0;
            int __docSuccess = 0;
            int __lastStatus = 0;
            int __usedStatusColumn = sender._findColumnByName(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._used_status);
            int __usedStatusColumn2 = -1; // sender._findColumnByName(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._used_status_2);
            int __docSuccessColumn = sender._findColumnByName(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_success);
            int __lastStatusColumn = sender._findColumnByName(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._last_status);

            switch (this._transControlType)
            {
                case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้ยกมา:
                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้ยกมา:
                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้ยกมา:

                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:

                case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้ยกมา:
                case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้ยกมา:
                case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้ยกมา:

                case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                    __usedStatusColumn = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._used_status);
                    __docSuccessColumn = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_success);
                    __lastStatusColumn = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._last_status);
                    __usedStatusColumn2 = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._used_status_2);
                    break;

            }

            if (__usedStatusColumn != -1) __usedStatus = MyLib._myGlobal._intPhase(sender._cellGet(row, __usedStatusColumn).ToString());
            if (__usedStatusColumn2 != -1) __usedStatus2 = MyLib._myGlobal._intPhase(sender._cellGet(row, __usedStatusColumn2).ToString());
            if (__docSuccessColumn != -1) __docSuccess = MyLib._myGlobal._intPhase(sender._cellGet(row, __docSuccessColumn).ToString());
            if (__lastStatusColumn != -1) __lastStatus = MyLib._myGlobal._intPhase(sender._cellGet(row, __lastStatusColumn).ToString());
            Boolean __result = (__usedStatus == 1 || __usedStatus2 == 1 || __docSuccess == 1 || (__lastStatus == 1 && checkCancel == true)) ? false : true;
            switch (this._transControlType)
            {
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                    if (_g.g._companyProfile._sale_order_edit)
                    {
                        __result = true;
                    }
                    break;
            }

            if (__result == true)
            {
                int __columnDocNo = sender._findColumnByName(this._tableName + "." + _g.d.ic_trans._doc_no);
                if (__columnDocNo != -1)
                {
                    string __docNo = sender._cellGet(row, __columnDocNo).ToString();

                    string __query = "select is_lock_record from " + this._tableName + " where doc_no = \'" + __docNo + "\' and trans_flag =" + this._transFlag;
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    DataSet __lockRecordResult = __myFrameWork._queryShort(__query);
                    if (__lockRecordResult.Tables.Count > 0 && __lockRecordResult.Tables[0].Rows.Count > 0 &&
                        MyLib._myGlobal._intPhase(__lockRecordResult.Tables[0].Rows[0][0].ToString()) > 0)
                    {
                        return false;
                    }
                }

            }

            return __result;
        }

        string _dataList__extraWhereEvent()
        {
            string __result = "";

            __result = " (" + _g.d.ap_ar_trans._trans_flag + " = " + _transFlag + ") and (" + _g.d.ap_ar_trans._trans_type + " = " + _transType + ")"; ;
            // toe ใส่สาขาเข้าไป filter
            if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only == true && _g.g._companyProfile._change_branch_code == false)
            {
                __result = __result + " and " + _g.d.ap_ar_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\' ";
            }

            return __result;
        }




        private string _extraWhere(int row)
        {
            /*
        1=สั่งซื้อ (PO) , 2=สั่งขาย (SO) , 3=คลัง(IC), 4=เจ้าหนี้ (AP) , 5=ลูกหนี้ (AR)

        ซื้อสินค้า   	12/1
        ซื้อเพิ่ม/เพิ่มหนี้	14/1	
        ส่งคืนสินค้า/ลดหนี้	16/1
        ตั้งหนี้ยกมา	81/4
        เพิ่มหนี้ยกมา	83/4
        ลดหนี้ยกมา	85/4


        รับชำระหนี้ อ้างเอกสารได้คือ
        ขายสินค้า		44/2
        ขายเพิ่ม/เพิ่มหนี้	46/2	
        รับคืน/ลดหนี้	48/2
        ตั้งหนี้ยกมา	93/5
        เพิ่มหนี้ยกมา	95/5
        ลดหนี้ยกมา	97/5
*/
            string _c = "";
            string _cust_code = this._screenTop._getDataStr(_g.d.ap_ar_trans._cust_code);
            string _result = "";
            string _trans_type = "";
            string _trans_flag = "";
            string _trans_flag_not_in = "";
            string _inquiry_type = "";
            string _trans_pay = "";
            string ___trans_flag = "";

            string _table_name = _g.d.ic_trans._table;
            try
            {
                string __resilt = this._detailGrid._cellGet(row, _g.d.ap_ar_trans_detail._bill_type).ToString();
                //if (TransControlType.Equals(_g.g._controlTypeEnum.APPayBill)) __resilt = this._screenGrid._cellGet(row, _g.d.ap_ar_trans_detail._billing_no).ToString();
                //if (TransControlType.Equals(_g.g._controlTypeEnum.ARPayBill)) __resilt = this._screenGrid._cellGet(row, 0).ToString();
                //if (TransControlType.Equals(_g.g._controlTypeEnum.APDebtBilling)) __resilt = this._screenGrid._cellGet(row, 1).ToString();
                //if (TransControlType.Equals(_g.g._controlTypeEnum.ARDebtBilling)) __resilt = this._screenGrid._cellGet(row, 1).ToString();

                //if (TransControlType.Equals(_g.g._controlTypeEnum.APCancelPayBill)) __resilt = this._screenGrid._cellGet(row, 0).ToString();
                //if (TransControlType.Equals(_g.g._controlTypeEnum.ARCancelPayBill)) __resilt = this._screenGrid._cellGet(row, 0).ToString();
                //if (TransControlType.Equals(_g.g._controlTypeEnum.APCancelDebtBilling)) __resilt = this._screenGrid._cellGet(row, 1).ToString();
                //if (TransControlType.Equals(_g.g._controlTypeEnum.ARCancelDebtBilling)) __resilt = this._screenGrid._cellGet(row, 1).ToString();

                if (_transType == 1)
                {
                    _trans_pay = "13,19";
                    // ซื้อสินค้า   	12/1
                    if (__resilt.Equals("0"))
                    {
                        _c = (_trans_flag.Equals("")) ? "" : ",";
                        _trans_type += _c + "1";

                        _inquiry_type = _c + "0,2";
                        _trans_flag_not_in += _c + "13";
                    }
                    //ซื้อเพิ่ม/เพิ่มหนี้	14/1	
                    if (__resilt.Equals("1"))
                    {
                        _c = (_trans_flag.Equals("")) ? "" : ",";
                        _trans_type += _c + "1";
                        _trans_flag += _c + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด);
                        _trans_flag_not_in += _c + "15";
                    }
                    //ส่งคืนสินค้า/ลดหนี้	16/1
                    if (__resilt.Equals("2"))
                    {
                        _c = (_trans_flag.Equals("")) ? "" : ",";
                        _trans_type += _c + "1";
                        _trans_flag += _c + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด);
                        _trans_flag_not_in += _c + "17";
                    }
                    //ตั้งหนี้ยกมา	81/4
                    if (__resilt.Equals("3"))
                    {
                        _c = (_trans_flag.Equals("")) ? "" : ",";
                        _trans_type += _c + "4";
                        _trans_flag += _c + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้ยกมา);
                        _trans_flag_not_in += _c + "82";
                    }
                    //เพิ่มหนี้ยกมา	83/4
                    if (__resilt.Equals("4"))
                    {
                        _c = (_trans_flag.Equals("")) ? "" : ",";
                        _trans_type += _c + "4";
                        _trans_flag += _c + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้ยกมา);
                        _trans_flag_not_in += _c + "84";
                    }
                    //ลดหนี้ยกมา	85/4
                    if (__resilt.Equals("5"))
                    {
                        _c = (_trans_flag.Equals("")) ? "" : ",";
                        _trans_type += _c + "4";
                        _trans_flag += _c + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้ยกมา);
                        _trans_flag_not_in += _c + "86";
                    }
                }
                else
                {
                    _trans_pay = "35,39," + _transFlag;
                    //----------------------------------------------------------------------------------------------------------------------
                    //ขายสินค้า		44/2
                    if (__resilt.Equals("0"))
                    {
                        _c = (_trans_flag.Equals("")) ? "" : ",";
                        _trans_type += _c + "2";
                        _trans_flag += _c + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ);
                        _inquiry_type = _c + "0,2";
                        _trans_flag_not_in += _c + "45";
                    }
                    //ขายเพิ่ม/เพิ่มหนี้	46/2	
                    if (__resilt.Equals("1"))
                    {
                        _c = (_trans_flag.Equals("")) ? "" : ",";
                        _trans_type += _c + "2";
                        _trans_flag += _c + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้);
                        _trans_flag_not_in += _c + "47";
                    }
                    //รับคืน/ลดหนี้	48/2
                    if (__resilt.Equals("2"))
                    {
                        _c = (_trans_flag.Equals("")) ? "" : ",";
                        _trans_type += _c + "2";
                        _trans_flag += _c + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้);
                        _trans_flag_not_in += _c + "49";
                    }
                    //ตั้งหนี้ยกมา	93/5
                    if (__resilt.Equals("3"))
                    {
                        _c = (_trans_flag.Equals("")) ? "" : ",";
                        _trans_type += _c + "5";
                        _trans_flag += _c + "93";
                        _trans_flag_not_in += _c + "94";
                    }
                    //เพิ่มหนี้ยกมา	95/5
                    if (__resilt.Equals("4"))
                    {
                        _c = (_trans_flag.Equals("")) ? "" : ",";
                        _trans_type += _c + "5";
                        _trans_flag += _c + "95";
                        _trans_flag_not_in += _c + "96";
                    }
                    //ลดหนี้ยกมา	97/5
                    if (__resilt.Equals("5"))
                    {
                        _c = (_trans_flag.Equals("")) ? "" : ",";
                        _trans_type += _c + "5";
                        _trans_flag += _c + "97";
                        _trans_flag_not_in += _c + "98";
                    }
                }


                string _trans_pay_money = "";// " and ( doc_no not in  ((select doc_no from ap_ar_trans where (trans_flag  in (" + _trans_pay + ")))))";
                string trans_inquiry_type = (_inquiry_type.Equals("")) ? "" : " and (" + _g.d.ic_trans._inquiry_type + " in (" + _inquiry_type + "))";
                string __trans = "and  (doc_no not in  ((select distinct billing_no from ap_ar_trans_detail where (trans_flag  in (" + _trans_pay + ")))))";
                _result += " (" + _g.d.ic_trans._trans_flag + "  in (" + _trans_flag + ")) and (" + _g.d.ic_trans._cust_code + " = '" + _cust_code + "') and ( " + _g.d.ic_trans._doc_no + " not in  ((select doc_ref from " + _g.d.ic_trans._table + " where (" + _g.d.ic_trans._trans_flag + "  in (" + _trans_flag_not_in + ")) and (" + _g.d.ic_trans._cust_code + " = '" + _cust_code + "')))) " + __trans + " " + _trans_pay_money + " " + trans_inquiry_type;
                //_result += " (" + _g.d.ic_trans._trans_flag + "  in (" + _trans_flag + ")) and (" + _g.d.ic_trans._trans_type + " in(" + _trans_type + ")) and (" + _g.d.ic_trans._cust_code + " = '" + _cust_code + "')";
            }
            catch
            {
            }

            return _result;
        }



        void _screenGrid__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            if (
                this._controlTypeTemp.Equals(_g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล) ||
                this._controlTypeTemp.Equals(_g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล) ||
                this._controlTypeTemp.Equals(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้) ||
                this._controlTypeTemp.Equals(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้))
            {
                string _cust_code = this._screenTop._getDataStr(_g.d.ap_ar_trans._cust_code);
                string _doc_no = this._screenTop._getDataStr(_g.d.ap_ar_trans._doc_no);
                if (_cust_code.Length > 0)
                {
                    this._detailGrid.__clickSearchButton(sender, e, _cust_code, _extraWhere(e._row));
                }
                else
                {
                    if (_g.g._arapTransTypeGlobal._transType(this._controlTypeTemp).Equals(1))
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("รหัสเจ้าหนี้ ห้ามว่าง"), "Fail", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("รหัสลูกหนี้ ห้ามว่าง"), "Fail", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
            }
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

                string __docNo = this._oldDocNo;

                if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore || this._printFormAfterSave == true)
                {
                    if (_customPrint != null)
                    {
                        bool __printResult = _customPrint(this._screenTop._docFormatCode, __docNo, this._transFlag.ToString(), false);
                        if (__printResult == true)
                        {
                            // update print count
                            MyLib._myFrameWork __myFramework = new MyLib._myFrameWork();
                            __myFramework._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "insert into " + _g.d.erp_print_logs._table + "(" + _g.d.erp_print_logs._doc_no + "," + _g.d.erp_print_logs._trans_flag + "," + _g.d.erp_print_logs._user_code + "," + _g.d.erp_print_logs._print_datetime + ") values (\'" + __docNo + "\'," + this._transFlag.ToString() + ",\'" + MyLib._myGlobal._userCode + "\', \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\')");
                        }
                    }
                    else
                    {

                        //string __docNo = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_no).ToString().Trim();
                        bool __printResult = SMLERPReportTool._global._printForm(this._screenTop._docFormatCode, __docNo, this._transFlag.ToString(), false);
                        if (__printResult == true)
                        {
                            // update print count
                            MyLib._myFrameWork __myFramework = new MyLib._myFrameWork();
                            __myFramework._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "insert into " + _g.d.erp_print_logs._table + "(" + _g.d.erp_print_logs._doc_no + "," + _g.d.erp_print_logs._trans_flag + "," + _g.d.erp_print_logs._user_code + "," + _g.d.erp_print_logs._print_datetime + ") values (\'" + __docNo + "\'," + this._transFlag.ToString() + ",\'" + MyLib._myGlobal._userCode + "\', \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\')");
                        }
                    }
                }
            }

            //// reprint 
            //if (MyLib._myGlobal._OEMVersion == "tvdirect" && this._oldDocNo.Length > 0)
            //{
            //    string __docNo = this._oldDocNo;
            //    SMLERPReportTool._global._printForm(this._icTransScreenTop._docFormatCode, __docNo, this._getTransFlag.ToString(), false);
            //}
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
            if (this._oldDocRef.Length > 0)
            {
                __docNoList = __docNoList + "," + this._oldDocRef;
            }


            switch (this._transControlType)
            {
                case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล_ยกเลิก:
                case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก:
                case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล_ยกเลิก:
                case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก:
                    __docNoList = this._docNoAdd(__docNoList, this._screenTop._getDataStr(_g.d.ap_ar_trans._doc_ref));

                    break;
            }

            //SMLProcess._docFlow __process = new SMLProcess._docFlow();
            //__process._processAll(this._transControlType, "", __docNoList);

            SMLProcess._docFlowThread __proces = new SMLProcess._docFlowThread(this._transControlType, "", __docNoList);
            __proces._processAll();

            MessageBox.Show(this._oldDocNo + " : Recheck Doc Flow Success");
            this._myManageData1._dataList._refreshData();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys __keyCode = (Keys)(int)keyData & Keys.KeyCode;
            if ((keyData & Keys.Control) == Keys.Control)
            {
                switch (__keyCode)
                {
                    case Keys.Home:
                        {
                            this._screenTop._focusFirst();
                            return true;
                        }
                    case Keys.P:
                        {
                            this._rePrintForm();
                            return true;
                        }
                    case Keys.R:
                        {
                            _docFlowRecheck();
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

                                    bool __openPeriod = _g.g._checkOpenPeriod(this._screenTop._getDataDate(_g.d.ap_ar_trans._doc_date));

                                    if (__openPeriod)
                                    {
                                        if (__docCancelForm.ShowDialog() == DialogResult.Yes)
                                        {
                                            if (__docCancelForm._comboCancelConfirm.SelectedIndex == 1)
                                            {
                                                string __table_name = _g.d.ap_ar_trans._table;
                                                if (_transFlag > 80 && _transFlag < 200)
                                                {
                                                    // กรณีพวกตั้งหนี้
                                                    __table_name = _g.d.ic_trans._table;
                                                }
                                                // update cancel 
                                                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                                                StringBuilder __queryListUpdate = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                                                __queryListUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + __table_name + " set is_cancel = 1, " + _g.d.ic_trans._cancel_code + "=\'" + MyLib._myGlobal._userCode + "\', " + _g.d.ic_trans._cancel_datetime + "=\'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\' where doc_no = \'" + this._oldDocNo + "\' and trans_flag =" + this._transFlag));
                                                __queryListUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal._table + " where " + _g.d.gl_journal._doc_no + " = \'" + this._oldDocNo + "\' and " + _g.d.gl_journal._trans_flag + " =" + this._transFlag));
                                                __queryListUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._doc_no + " = \'" + this._oldDocNo + "\' and " + _g.d.gl_journal_detail._trans_flag + " =" + this._transFlag));

                                                if (this._vatBuy != null)
                                                {
                                                    __queryListUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_vat_buy._table + " where " + _g.d.gl_journal_vat_buy._doc_no + " = \'" + this._oldDocNo + "\' and " + _g.d.gl_journal_vat_buy._trans_flag + " =" + this._transFlag));
                                                }

                                                __queryListUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.erp_cancel_logs._table +
                                                    " (" + _g.d.erp_cancel_logs._doc_no + "," + _g.d.erp_cancel_logs._trans_flag + "," + _g.d.erp_cancel_logs._user_code + "," + _g.d.erp_cancel_logs._cancel_flag + "," + _g.d.erp_cancel_logs._cancel_datetime + "," + _g.d.erp_cancel_logs._cancel_reason + ") " +
                                                    " values " +
                                                    " (\'" + this._oldDocNo + "\', " + this._transFlag + ", \'" + MyLib._myGlobal._userCode + "\', 0, \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\', \'" + __docCancelForm._cancelReasonTextbox.Text + "\') "));

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
                                                    if (this._oldDocRef.Length > 0)
                                                    {
                                                        __docNoList = __docNoList + "," + this._oldDocRef;
                                                    }
                                                    SMLProcess._docFlow __process = new SMLProcess._docFlow();
                                                    __process._processAll(this._transControlType, "", __docNoList);

                                                    MessageBox.Show("ยกเลิกเอกสารสำเร็จ");
                                                    this._myManageData1._dataList._refreshData();

                                                }
                                                this._myManageData1._dataList._refreshData();
                                            }
                                            return true;
                                        }
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
            if (keyData == Keys.Escape)
            {
                this.Dispose();
                return true;
            }

            if (keyData == (Keys.Shift | Keys.F12))
            {
                if (_myManageData1__checkEditData(this._myManageData1._dataList._gridData._selectRow, this._myManageData1._dataList._gridData, false))
                {
                    // un cancel  doc
                    if (MyLib._myGlobal._OEMVersion.Equals("SINGHA")
                                /*&& (
                                this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ ||
                                        this._transControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้ ||
                                        this._transControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้)*/
                                )
                    {
                        bool __openPeriod = _g.g._checkOpenPeriod(this._screenTop._getDataDate(_g.d.ap_ar_trans._doc_date));

                        if (__openPeriod)
                        {
                            if (MessageBox.Show("ต้องการเรียกเอกสารกลับมาใชังานได้ปรกติ หรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                                string __table_name = _g.d.ap_ar_trans._table;
                                if (_transFlag > 80 && _transFlag < 200)
                                {
                                    // กรณีพวกตั้งหนี้
                                    __table_name = _g.d.ic_trans._table;
                                }

                                StringBuilder __queryListUpdate = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                                __queryListUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + __table_name + " set is_cancel = 0 where doc_no = \'" + this._oldDocNo + "\' and trans_flag =" + this._transFlag));
                                __queryListUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.erp_cancel_logs._table +
                                    " (" + _g.d.erp_cancel_logs._doc_no + "," + _g.d.erp_cancel_logs._trans_flag + "," + _g.d.erp_cancel_logs._user_code + "," + _g.d.erp_cancel_logs._cancel_flag + "," + _g.d.erp_cancel_logs._cancel_datetime + ") " +
                                    " values " +
                                    " (\'" + this._oldDocNo + "\', " + this._transFlag + ", \'" + MyLib._myGlobal._userCode + "\', 1, \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\') "));

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
                                    if (this._oldDocRef.Length > 0)
                                    {
                                        __docNoList = __docNoList + "," + this._oldDocRef;
                                    }
                                    SMLProcess._docFlow __process = new SMLProcess._docFlow();
                                    __process._processAll(this._transControlType, "", __docNoList);
                                    MessageBox.Show("เรียกคืนเอกสารสำเร็จ");
                                    this._myManageData1._dataList._refreshData();

                                    // ประมวลผล GL ด้วย อย่าลืม
                                }
                                this._myManageData1._dataList._refreshData();
                            }
                            return true;
                        }
                    }
                    return false;
                }
                else
                {
                    MessageBox.Show("เอกสารอ้างอิงไปแล้ว");
                }
            }

            if (keyData == Keys.F12)
            {
                this._saveData();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _screenTop__saveKeyDown(object sender)
        {
            this._saveData();
        }

        private void _saveLog(int mode)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __resultLog = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, _createLog(mode));
            if (__resultLog.Length != 0)
            {
                MessageBox.Show(__resultLog, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string _createLog(int mode)
        {
            string __docDate = (mode == 3) ? "\'\'" : this._screenTop._getDataStrQuery(_g.d.ap_ar_trans._doc_date);
            string __docNo = (mode == 3) ? "\'\'" : this._screenTop._getDataStrQuery(_g.d.ap_ar_trans._doc_no);
            decimal __amount = (mode == 3) ? 0M : this._screenBottom._getDataNumber(_g.d.ap_ar_trans._total_net_value);
            StringBuilder __logDetail = new StringBuilder();
            __logDetail.Append(this._screenTop._logCreate("top"));
            string __query = MyLib._myUtil._convertTextToXml("insert into " + _g.d.logs._table + " (" + _g.d.logs._function_type + "," + _g.d.logs._computer_name + "," + _g.d.logs._guid + "," +
                _g.d.logs._doc_date + "," + _g.d.logs._doc_no + "," + _g.d.logs._doc_amount + "," +
                _g.d.logs._doc_date_old + "," + _g.d.logs._doc_no_old + "," + _g.d.logs._doc_amount_old + "," +
                _g.d.logs._menu_name + "," + _g.d.logs._screen_code + "," + _g.d.logs._function_code + "," + _g.d.logs._user_code + "," +
                _g.d.logs._date_time + "," + _g.d.logs._data1 + "," + _g.d.logs._data2 + ") values (2," +
                "\'" + SystemInformation.ComputerName + "\'," + "\'" + Guid.NewGuid().ToString("N") + "\'," +
                __docDate + "," + __docNo + "," + __amount.ToString() + "," +
                this._logDocDateOld + "," + this._logDocNoOld + "," + this._logAmountOld.ToString() + "," +
                "\'" + this._menuName + "\'," + _g.g._transFlagGlobal._transFlag(this._transControlType).ToString() + "," + mode.ToString() + ",\'" +
                MyLib._myGlobal._userCode + "\',\'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", new System.Globalization.CultureInfo("en-US")) + "\',\'" + __logDetail.ToString() + "\',\'" + this._logDetailOld + "\')");
            return __query;
        }


        private void _saveData()
        {
            bool _is_save_data = true;
            string _getApPay = "";
            StringBuilder __docNoList = new StringBuilder();

            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                  MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                  MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                  MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
            {
                _is_save_data = _g.g._checkOpenPeriod(this._screenTop._getDataDate(_g.d.ap_ar_trans._doc_date));
            }

            if (_is_save_data)
            {
                // toe เพิ่ม creator และ editor
                string __create_doc_field = "";
                string __create_doc_value = "";

                if (this._myManageData1._mode == 2)
                {
                    __create_doc_field = "," + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans._creator_code, _g.d.ic_trans._create_datetime, _g.d.ic_trans._last_editor_code, _g.d.ic_trans._lastedit_datetime);
                    __create_doc_value = "," + MyLib._myGlobal._fieldAndComma("\'" + this._creator_code + "\'", "\'" + MyLib._myGlobal._convertDateTimeToQuery(this._create_datetime) + "\'", "\'" + MyLib._myGlobal._userCode + "\'", "\'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\'");
                }
                else
                {
                    __create_doc_field = "," + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans._creator_code, _g.d.ic_trans._create_datetime);
                    __create_doc_value = "," + MyLib._myGlobal._fieldAndComma("\'" + MyLib._myGlobal._userCode + "\'", "\'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\'");

                }

                string __table_name = _g.d.ap_ar_trans._table;
                string __table_detail_name = _g.d.ap_ar_trans_detail._table;
                if (_transFlag > 80 && _transFlag < 200)
                {
                    // กรณีพวกตั้งหนี้
                    __table_name = _g.d.ic_trans._table;
                    __table_detail_name = _g.d.ic_trans_detail._table;
                }
                _screenTop._saveLastControl();
                string getEmtry = _screenTop._checkEmtryField();
                if (getEmtry.Length > 0)
                {
                    MyLib._myGlobal._displayWarning(2, getEmtry);
                }
                else
                {
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                    if (this._payControl != null)
                    {
                        if (this._payControl._checker() == false)
                        {
                            _is_save_data = false;
                        }
                    }

                    if (_is_save_data == true && _g.g._companyProfile._warning_branch_input)
                    {
                        if (this._screenMore != null && this._screenMore._getControl(_g.d.ap_ar_trans._branch_code) != null)
                        {
                            if (this._screenMore._getDataStr(_g.d.ap_ar_trans._branch_code).Trim().Length == 0)
                            {
                                MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อน สาขา"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                _is_save_data = false;
                            }
                        }
                    }

                    // toe check permisssion
                    if (_is_save_data == true && this._myManageData1._mode == 1 && this._myManageData1._isAdd == false)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("warning44"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        _is_save_data = false;
                    }

                    //if (this._detailGrid._rowData.Count == 0)
                    //{
                    //    _is_save_data = false;
                    //    MessageBox.Show("");
                    //}

                    if (_is_save_data && MyLib._myGlobal._OEMVersion == ("SINGHA")
                        && (
                        this._transControlType == _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้ยกมา ||
                        this._transControlType == _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้ยกมา ||
                        this._transControlType == _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้ยกมา ||

                        this._transControlType == _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น_ยกเลิก ||
                        this._transControlType == _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น_ยกเลิก ||
                        this._transControlType == _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น_ยกเลิก ||

                        this._transControlType == _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้ ||
                        this._transControlType == _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล ||
                        this._transControlType == _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล_ยกเลิก ||
                        this._transControlType == _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก))
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

                    if (_is_save_data)
                    {
                        this._detailGrid._calcTotal(true);

                        string __docNo = this._screenTop._getDataStr(_g.d.ap_ar_trans._doc_no);
                        __docNoList.Append("\'" + __docNo + "\'");

                        // doc_ref

                        switch (this._transControlType)
                        {
                            case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล_ยกเลิก:
                            case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก:
                            case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล_ยกเลิก:
                            case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก:
                                __docNoList.Append(",\'" + this._screenTop._getDataStr(_g.d.ap_ar_trans._doc_ref) + "\'");

                                break;
                        }


                        if (this._oldDocRef.Length > 0)
                        {
                            __docNoList.Append("," + _oldDocRef);
                        }

                        for (int __row = 0; __row < this._refDoc._transGrid._rowData.Count; __row++)
                        {
                            string __getDocNo = this._refDoc._transGrid._cellGet(__row, _g.d.ap_ar_trans_detail._billing_no).ToString();
                            if (__getDocNo.Length > 0)
                            {
                                __docNoList.Append("," + "\'" + __getDocNo + "\'");
                            }
                        }

                        for (int __row = 0; __row < this._detailGrid._rowData.Count; __row++)
                        {
                            string __getDocNo = this._detailGrid._cellGet(__row, _g.d.ap_ar_trans_detail._billing_no).ToString();
                            if (__getDocNo.Length > 0)
                            {
                                __docNoList.Append("," + "\'" + __getDocNo + "\'");
                            }


                        }

                        string __docDate = this._screenTop._getDataStrQuery(_g.d.ap_ar_trans._doc_date);
                        string __docTime = this._screenTop._getDataStr(_g.d.ap_ar_trans._doc_time);
                        // ลบข้อมูลเก่าออกก่อน ในกรณีแก้ไข
                        StringBuilder __myQuery = new StringBuilder();
                        __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        __myQuery.Append(this._deleteFirst(this._oldDocNo));
                        // ลบข้อมูลเก่าออกก่อน ในกรณีแก้ไข
                        if (this._oldDocNo.Length > 0)
                        {
                            if (_transFlag > 80 && _transFlag < 200)
                            {
                                // กรณีพวกตั้งหนี้
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + this._oldDocNo + "\'" + " and " + _g.d.ic_trans._trans_flag + "=" + this._transFlag.ToString()));
                            }
                            else
                            {
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ap_ar_trans._table + " where " + _g.d.ap_ar_trans._doc_no + "=\'" + this._oldDocNo + "\'" + " and " + _g.d.ap_ar_trans._trans_flag + "=" + this._transFlag.ToString()));
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ap_ar_trans_detail._table + " where " + _g.d.ap_ar_trans_detail._doc_no + "=\'" + this._oldDocNo + "\'" + " and " + _g.d.ap_ar_trans_detail._trans_flag + "=" + this._transFlag.ToString()));
                                // ลบอ้างอิง

                                // toe แก้ลบ อ้างอิง
                                string __where = _g.d.ap_ar_trans_detail._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.ap_ar_trans_detail._trans_flag + "=99";
                                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} where " + __where), _g.d.ap_ar_trans_detail._table));
                            }

                            // ลบของเก่าก่อน
                            if (this._payControl != null)
                            {
                                // ลบการจ่ายเงิน
                                __myQuery.Append(this._payControl._queryDelete(this._oldDocNo));
                            }

                            // toe 
                            if (this._withHoldingTax != null)
                            {
                                // ลบหักณ ที่จ่าย
                                __myQuery.Append(this._withHoldingTax._queryDelete(this._oldDocNo, this._transControlType));
                            }

                        }
                        // เพิ่มข้อมูลใหม่
                        // Trans
                        ArrayList __getTopQuery = this._screenTop._createQueryForDatabase();
                        ArrayList __getBottomQuery = this._screenBottom._createQueryForDatabase();

                        string __moreQueryField = "";
                        string __moreQueryValue = "";

                        if (this._screenMore != null)
                        {
                            ArrayList __getMoreQuery = this._screenMore._createQueryForDatabase();
                            __moreQueryField = "," + __getMoreQuery[0];
                            __moreQueryValue = "," + __getMoreQuery[1];
                        }

                        if (this._vatBuy != null)
                        {
                            __moreQueryField += "," + _g.d.ic_trans._is_manual_vat;
                            __moreQueryValue += "," + ((this._vatBuy._manualVatCheckbox.Checked) ? "1" : "0");
                        }

                        string __fieldList =
                               _g.d.ap_ar_trans._trans_type + "," +
                               _g.d.ap_ar_trans._trans_flag + "," +
                               _g.d.ap_ar_trans._doc_date + "," +
                               _g.d.ap_ar_trans._doc_no + ",";

                        string __dataList = _g.g._arapTransTypeGlobal._transType(this._controlTypeTemp).ToString() + "," +
                            _g.g._transFlagGlobal._transFlag(this._controlTypeTemp).ToString() + "," +
                            this._screenTop._getDataStrQuery(_g.d.ap_ar_trans._doc_date) + "," +
                            this._screenTop._getDataStrQuery(_g.d.ap_ar_trans._doc_no) + ",";

                        //-----------------------------------------------------------------------------------------------------
                        string _flied_head = "";
                        string _data_head = "";
                        string _update_head = "";
                        string _getDataQurey = "";
                        //-----------------------------------------------------------------------------------------------------

                        _flied_head = (_flied_head.Equals("")) ? "" : "," + _flied_head;
                        _data_head = (_data_head.Equals("")) ? "" : "," + _data_head;
                        _update_head = (_update_head.Equals("")) ? "" : "," + _update_head;
                        // กำหนดให้ดึงทุกบรรทัด
                        this._detailGrid._updateRowIsChangeAll(true);
                        //
                        string _inset_fild_bottom = (!__getBottomQuery[0].ToString().Equals("")) ? "," + __getBottomQuery[0].ToString() : "";
                        string _inset_date_bottom = (!__getBottomQuery[1].ToString().Equals("")) ? "," + __getBottomQuery[1].ToString() : "";
                        //somruk
                        //__myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + __table_name + " ("
                        //    + _g.d.ap_ar_trans._doc_format_code + "," + _g.d.ap_ar_trans._trans_type + "," + _g.d.ap_ar_trans._trans_flag + "," + __getTopQuery[0].ToString()
                        //    + _inset_fild_bottom + _flied_head
                        //    + ") values (\'" + this._screenTop._docFormatCode + "\',"
                        //    + _g.g._arapTransTypeGlobal._transType(this._controlTypeTemp).ToString() + "," + _g.g._transFlagGlobal._transFlag(this._controlTypeTemp).ToString()
                        //    + "," + __getTopQuery[1].ToString() + _inset_date_bottom + _data_head
                        //    + ")"));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + __table_name + " ("
                             + _g.d.ap_ar_trans._trans_type + "," + _g.d.ap_ar_trans._trans_flag +
                             "," + __getTopQuery[0].ToString() + __moreQueryField
                            + _inset_fild_bottom + _flied_head + __create_doc_field
                            + ") values ("
                            + _g.g._arapTransTypeGlobal._transType(this._controlTypeTemp).ToString() + "," + _g.g._transFlagGlobal._transFlag(this._controlTypeTemp).ToString()
                            + "," + __getTopQuery[1].ToString() + __moreQueryValue
                            + _inset_date_bottom + _data_head + __create_doc_value
                            + ")"));
                        __myQuery.Append(this._detailGrid._createQueryForInsert(__table_detail_name, __fieldList, __dataList, false, true));
                        __myQuery.Append(_getApPay);
                        if (_getDataQurey.Length > 0) __myQuery.Append(_getDataQurey);

                        if (this._vatBuy != null)
                        {
                            // ภาษีซื้อ
                            this._vatBuy._deleteRowIfBlank();
                            this._vatBuy._vatGrid._updateRowIsChangeAll(true);
                            string __vatFieldList = _g.d.gl_journal_vat_buy._book_code + "," + _g.d.gl_journal_vat_buy._vat_calc + "," + _g.d.gl_journal_vat_buy._trans_type + "," + _g.d.gl_journal_vat_buy._trans_flag + "," + _g.d.gl_journal_vat_buy._doc_date + "," + _g.d.gl_journal_vat_buy._doc_no + "," + _g.d.gl_journal_vat_buy._ap_code + ",";
                            string __vatDataList = "\'\'," + _g.g._transTypeGlobal._vatBuyType(this._transControlType).ToString() + "," + this._transType.ToString() + "," + this._transFlag.ToString() + "," + this._screenTop._getDataStrQuery(_g.d.ap_ar_trans._doc_date) + "," + this._screenTop._getDataStrQuery(_g.d.ap_ar_trans._doc_no) + "," + this._screenTop._getDataStrQuery(_g.d.ap_ar_trans._cust_code) + ",";
                            __myQuery.Append(this._vatBuy._vatGrid._createQueryForInsert(_g.d.gl_journal_vat_buy._table, __vatFieldList, __vatDataList, false, true));
                        }
                        if (this._vatSale != null)
                        {
                            // ภาษีขาย
                            this._vatSale._deleteRowIfBlank();
                            this._vatSale._vatGrid._updateRowIsChangeAll(true);
                            string __vatFieldList = _g.d.gl_journal_vat_sale._book_code + "," + _g.d.gl_journal_vat_buy._vat_calc + "," + _g.d.gl_journal_vat_sale._trans_type + "," + _g.d.gl_journal_vat_sale._trans_flag + "," + _g.d.gl_journal_vat_sale._doc_date + "," + _g.d.gl_journal_vat_sale._doc_no + "," + _g.d.gl_journal_vat_sale._ar_code + ",";
                            string __vatDataList = "\'\'," + _g.g._transTypeGlobal._vatSaleType(this._transControlType).ToString() + "," + this._transType.ToString() + "," + this._transFlag.ToString() + "," + this._screenTop._getDataStrQuery(_g.d.ap_ar_trans._doc_date) + "," + this._screenTop._getDataStrQuery(_g.d.ap_ar_trans._doc_no) + "," + this._screenTop._getDataStrQuery(_g.d.ap_ar_trans._cust_code) + ",";
                            __myQuery.Append(this._vatSale._vatGrid._createQueryForInsert(_g.d.gl_journal_vat_sale._table, __vatFieldList, __vatDataList, false, true));
                        }
                        // ภาษีหัก ณ. ที่จ่าย
                        if (this._withHoldingTax != null)
                        {
                            __myQuery.Append(this._withHoldingTax._queryInsert(__docNo, __docDate, this._transControlType));
                        }
                        if (this._payControl != null)
                        {
                            // การจ่ายเงิน
                            __myQuery.Append(this._payControl._queryInsert(__docNo, __docDate, __docTime, this._screenTop._docFormatCode, ""));
                        }
                        // เพิ่มอ้างอิง
                        this._refDoc._transGrid._updateRowIsChangeAll(true);

                        string __fieldList2 =
                               _g.d.ap_ar_trans._trans_type + "," +
                               _g.d.ap_ar_trans._trans_flag + "," +
                               _g.d.ap_ar_trans._doc_date + "," +
                               _g.d.ap_ar_trans._doc_no + ",";

                        string __dataList2 = _g.g._arapTransTypeGlobal._transType(this._controlTypeTemp).ToString() + "," +
                            //_g.g._icTransFlagGlobal._ictransFlag(this._controlTypeTemp).ToString() + "," +
                            "99," +
                            this._screenTop._getDataStrQuery(_g.d.ap_ar_trans._doc_date) + "," +
                            this._screenTop._getDataStrQuery(_g.d.ap_ar_trans._doc_no) + ",";

                        __myQuery.Append(this._refDoc._transGrid._createQueryForInsert(_g.d.ap_ar_trans_detail._table, __fieldList2, __dataList2, false, true));
                        // GL
                        Boolean __glManual = false;
                        if (this._glScreenTop != null)
                        {
                            __glManual = (((int)MyLib._myGlobal._decimalPhase(this._glScreenTop._getDataStr(_g.d.gl_journal._trans_direct))) == 1) ? true : false;
                            // ลบ GL เก่าออก
                            __myQuery.Append(this._glDeleteQuery(this._oldDocNo));
                        }
                        if (__glManual)
                        {
                            Control __getControl = this._glScreenTop._getControl(_g.d.gl_journal._doc_date);
                            DateTime __getDate = ((MyLib._myDateBox)__getControl)._dateTime;
                            int __periodNumber = _g.g._accountPeriodFind(__getDate);
                            int __getColumnNumberDebit = this._glDetail._glDetailGrid._findColumnByName(_g.d.gl_journal_detail._debit);
                            int __getColumnNumberCredit = this._glDetail._glDetailGrid._findColumnByName(_g.d.gl_journal_detail._credit);
                            ArrayList __getData = this._glScreenTop._createQueryForDatabase();
                            string __extData = ((MyLib._myGrid._columnType)this._glDetail._glDetailGrid._columnList[__getColumnNumberDebit])._total.ToString() + "," +
                                ((MyLib._myGrid._columnType)this._glDetail._glDetailGrid._columnList[__getColumnNumberCredit])._total.ToString() + "," + this._transFlag.ToString();
                            string __extField = _g.d.gl_journal._debit + "," + _g.d.gl_journal._credit + "," + _g.d.gl_journal._trans_flag;
                            // head
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.gl_journal._table + " (" + __getData[0].ToString() + "," + __extField + ") values (" + __getData[1].ToString() + "," + __extData + ")"));
                            // detail
                            string __fieldListGl = _g.d.gl_journal._doc_date + "," + _g.d.gl_journal._book_code + "," + _g.d.gl_journal._doc_no + "," + _g.d.gl_journal._trans_flag + "," + _g.d.gl_journal._period_number + "," + _g.d.gl_journal._journal_type + ",";
                            string __dataListGl = this._glScreenTop._getDataStrQuery(_g.d.gl_journal._doc_date) + "," + this._glScreenTop._getDataStrQuery(_g.d.gl_journal._book_code) + "," + this._glScreenTop._getDataStrQuery(_g.d.gl_journal._doc_no) + "," + this._transFlag.ToString() + "," + __periodNumber.ToString() + "," + this._glScreenTop._getDataStr(_g.d.gl_journal._journal_type) + ",";
                            this._glDetail._glDetailGrid._updateRowIsChangeAll(true);
                            __myQuery.Append(this._glDetail._glDetailGrid._createQueryForInsert(_g.d.gl_journal_detail._table, __fieldListGl, __dataListGl));
                        }

                        // update ค่าอื่นๆ เผื่อหลุด
                        __myQuery.Append(_g.g._queryUpdateTrans());
                        //
                        __myQuery.Append("</node>");


                        // toe gl auto
                        SMLERPGL._transProcessUserControl __processControl = null;

                        if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                        {
                            _g.g._companyProfileLoad();

                            // toe กรณีแก้ไข และ ลง GL Auto
                            if (this._glScreenTop != null && _g.g._companyProfile._gl_process_realtime)
                            {
                                // จำลอง datatable ที่ query ออกมาได้

                                decimal __service_amount = 0M;
                                int __vat_type = MyLib._myGlobal._intPhase(this._screenTop._getDataNumber(_g.d.ic_trans._vat_type).ToString());
                                decimal __total_discoutnt = (__vat_type == 1) ? (this._screenBottom._getDataNumber(_g.d.ic_trans._total_discount) * 100 / (100 + _g.g._companyProfile._vat_rate)) : this._screenBottom._getDataNumber(_g.d.ic_trans._total_discount);
                                decimal __credit_card_amount = 0M;
                                decimal __credit_card_charge = 0M;
                                string __departmentCode = "";
                                string __projectCode = "";
                                string __allocateCode = "";
                                decimal __vatNext = 0M;
                                string __sideCode = "";
                                string __jobCode = "";
                                decimal __total_cost = 0M;

                                decimal __total_vat_value = 0M;
                                if (_transFlag > 80 && _transFlag < 200)
                                {
                                    __total_vat_value = this._screenBottom._getDataNumber(_g.d.ic_trans._total_vat_value);
                                }
                                else
                                {
                                    if (this._vatBuy != null)
                                    {
                                        this._vatBuy._vatGrid._calcTotal(false);
                                        MyLib._myGrid._columnType __getColumn = (MyLib._myGrid._columnType)this._vatBuy._vatGrid._columnList[this._vatBuy._vatGrid._findColumnByName(_g.d.gl_journal_vat_buy._vat_amount)];
                                        __total_vat_value = __getColumn._total;
                                    }

                                    if (this._vatSale != null)
                                    {
                                        this._vatSale._vatGrid._calcTotal(false);
                                        MyLib._myGrid._columnType __getColumn = (MyLib._myGrid._columnType)this._vatSale._vatGrid._columnList[this._vatSale._vatGrid._findColumnByName(_g.d.gl_journal_vat_sale._amount)];
                                        __total_vat_value = __getColumn._total;
                                    }

                                }

                                if (this._transFlag == 19)
                                {
                                    int __columnVatAmount = this._vatBuy._vatGrid._findColumnByName(_g.d.gl_journal_vat_buy._vat_amount);
                                    __vatNext = ((MyLib._myGrid._columnType)this._vatBuy._vatGrid._columnList[__columnVatAmount])._total;
                                }

                                //string __incheck = this._icTransScreenBottom._getDataStr(_g.d.ic_trans._remark);

                                __processControl = new SMLERPGL._transProcessUserControl();

                                SMLERPGL._transProcessUserControl._transDataObject __transData = new SMLERPGL._transProcessUserControl._transDataObject(((_transFlag > 80 && _transFlag < 200) ? "IC" : "CB"), this._oldDocNo, this._transFlag, this._transType, this._screenTop._docFormatCode)
                                {
                                    cust_code = this._screenTop._getDataStr(_g.d.ap_ar_trans._cust_code),
                                    cust_name = (this._screenTop._getControl(_g.d.ic_trans._cust_code) != null) ? ((MyLib._myTextBox)this._screenTop._getControl(_g.d.ic_trans._cust_code))._textSecond : "",
                                    vat_type = __vat_type,
                                    tax_doc_no = this._screenTop._getDataStr(_g.d.ic_trans._tax_doc_no),
                                    remark = this._screenBottom._getDataStr(_g.d.ic_trans._remark),
                                    total_amount = (_transFlag > 80 && _transFlag < 200) ? this._screenBottom._getDataNumber(_g.d.ic_trans._total_amount) : this._screenBottom._getDataNumber(_g.d.ap_ar_trans._total_net_value),
                                    service_amount = __service_amount,
                                    total_value = this._screenBottom._getDataNumber(_g.d.ic_trans._total_value),
                                    total_before_vat = this._screenBottom._getDataNumber(_g.d.ic_trans._total_before_vat),
                                    total_except_vat = this._screenBottom._getDataNumber(_g.d.ic_trans._total_except_vat),
                                    total_vat_value = __total_vat_value,
                                    total_discount = __total_discoutnt,
                                    cash_amount = (this._payControl != null) ? this._payControl._payDetailScreen._getDataNumber(_g.d.cb_trans._cash_amount) : 0M,
                                    tax_at_pay = (this._payControl != null) ? this._payControl._payDetailScreen._getDataNumber(_g.d.cb_trans._total_tax_at_pay) : 0M,
                                    point_amount = (this._payControl != null) ? this._payControl._payDetailScreen._getDataNumber(_g.d.cb_trans._point_amount) : 0M,
                                    coupon_amount = (this._payControl != null) ? this._payControl._payDetailScreen._getDataNumber(_g.d.cb_trans._coupon_amount) : 0M,
                                    card_amount = (this._payControl != null) ? this._payControl._payDetailScreen._getDataNumber(_g.d.cb_trans._card_amount) : 0M,
                                    chq_amount = (this._payControl != null) ? this._payControl._payDetailScreen._getDataNumber(_g.d.cb_trans._chq_amount) : 0M,
                                    tranfer_amount = (this._payControl != null) ? this._payControl._payDetailScreen._getDataNumber(_g.d.cb_trans._tranfer_amount) : 0M,
                                    total_income_amount = (this._payControl != null) ? this._payControl._payDetailScreen._getDataNumber(_g.d.cb_trans._total_income_amount) : 0M,
                                    petty_cash_amount = (this._payControl != null) ? this._payControl._payDetailScreen._getDataNumber(_g.d.cb_trans._petty_cash_amount) : 0M,
                                    discount_amount = (this._payControl != null) ? this._payControl._payDetailScreen._getDataNumber(_g.d.cb_trans._discount_amount) : 0M,
                                    doc_ref = this._screenTop._getDataStr(_g.d.ic_trans._doc_ref),
                                    doc_date = this._screenTop._getDataStr(_g.d.ic_trans._doc_date, true).Replace("\'", ""),
                                    doc_time = this._screenTop._getDataStr(_g.d.ic_trans._doc_time),
                                    inquiry_type = MyLib._myGlobal._intPhase(this._screenTop._getDataNumber(_g.d.ic_trans._inquiry_type).ToString()),
                                    advance_amount = this._screenBottom._getDataNumber(_g.d.ic_trans._advance_amount),
                                    deposit_amount = (this._payControl != null) ? this._payControl._payDetailScreen._getDataNumber(_g.d.cb_trans._deposit_amount) : 0M,
                                    credit_card_amount = __credit_card_amount,
                                    credit_card_charge = __credit_card_charge,
                                    tax_doc_date = this._screenTop._getDataStr(_g.d.ic_trans._tax_doc_date, true).Replace("\'", ""),
                                    branch_code = _g.g._companyProfile._branch_code,
                                    department_code = __departmentCode,
                                    project_code = __projectCode,
                                    allocate_code = __allocateCode,
                                    vat_next = __vatNext,
                                    side_code = __sideCode,
                                    job_code = __jobCode,
                                    total_cost = __total_cost,
                                    total_income_other = (this._payControl != null) ? this._payControl._payDetailScreen._getDataNumber(_g.d.cb_trans._total_income_other) : 0M,
                                    total_expense_other = (this._payControl != null) ? this._payControl._payDetailScreen._getDataNumber(_g.d.cb_trans._total_expense_other) : 0M
                                };

                                __processControl._addTransData(__transData, true);

                                __processControl._addTransDetailData(null, true);
                                // detail

                                // pay detail

                                if (this._payControl != null)
                                {
                                    // chq
                                    for (int __row = 0; __row < this._payControl._payChequeGrid._rowData.Count; __row++)
                                    {
                                        string __getChqNumber = this._payControl._payChequeGrid._cellGet(__row, _g.d.cb_trans_detail._trans_number).ToString();
                                        if (__getChqNumber.Length > 0)
                                        {
                                            decimal __amount = (decimal)this._payControl._payChequeGrid._cellGet(__row, _g.d.cb_trans_detail._amount);
                                            int __pass_book_code_column = this._payControl._payChequeGrid._findColumnByName(_g.d.cb_trans_detail._pass_book_code);

                                            SMLERPGL._transProcessUserControl._transDetailDataObject __detailDataGL = new SMLERPGL._transProcessUserControl._transDetailDataObject((this._oldDocNo.Length == 0) ? __docNo : this._oldDocNo, this._transFlag, this._transType)
                                            {
                                                doc_type = 2,
                                                item_type = 9999,
                                                trans_number = __getChqNumber,
                                                price = (decimal)this._payControl._payChequeGrid._cellGet(__row, _g.d.cb_trans_detail._sum_amount),
                                                amount = __amount,
                                                sum_amount_exclude_vat = 0,
                                                remark = "",
                                                line_number = __row,
                                                sum_amount = (decimal)this._payControl._payChequeGrid._cellGet(__row, _g.d.cb_trans_detail._sum_amount),
                                                pass_book_code = (this._payControl._payChequeGrid._findColumnByName(_g.d.cb_trans_detail._pass_book_code) == -1) ? "" : this._payControl._payChequeGrid._cellGet(__row, _g.d.cb_trans_detail._pass_book_code).ToString()
                                            };
                                            __processControl._addTransDetailData(__detailDataGL, false);
                                        }
                                    }

                                    // tranfer
                                    for (int __row = 0; __row < this._payControl._payTransferGrid._rowData.Count; __row++)
                                    {
                                        string __getBookCode = this._payControl._payTransferGrid._cellGet(__row, _g.d.cb_trans_detail._trans_number).ToString();
                                        if (__getBookCode.Length > 0)
                                        {
                                            decimal __amount = (decimal)this._payControl._payTransferGrid._cellGet(__row, _g.d.cb_trans_detail._amount);

                                            SMLERPGL._transProcessUserControl._transDetailDataObject __detailDataGL = new SMLERPGL._transProcessUserControl._transDetailDataObject((this._oldDocNo.Length == 0) ? __docNo : this._oldDocNo, this._transFlag, this._transType)
                                            {
                                                doc_type = 1,
                                                item_type = 9999,
                                                trans_number = __getBookCode,
                                                price = __amount,
                                                amount = __amount,
                                                sum_amount_exclude_vat = 0,
                                                remark = "",
                                                line_number = __row,
                                                pass_book_code = __getBookCode,

                                            };
                                            __processControl._addTransDetailData(__detailDataGL, false);
                                        }
                                    }

                                    // credit
                                }

                                __processControl._procesGLByTemp(this._screenTop._docFormatCode);
                                List<SMLERPGL._transProcessUserControl._glStruct> __glStruct = __processControl.getGLTrans;

                                // check balance 
                                if (__glStruct != null && __glStruct.Count > 0 && __glStruct[0]._debit != __glStruct[0]._credit)
                                {
                                    //MessageBox.Show("ยอดเดบิต : " + __glStruct[0]._debit + " เครดิต : " + __glStruct[0]._credit + " ไม่ลงตัว ", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    string __textHeader = "ยอดเดบิต : " + __glStruct[0]._debit + " เครดิต : " + __glStruct[0]._credit + " ไม่ลงตัว ";

                                    SMLERPGLControl._glDetail __detail = new SMLERPGLControl._glDetail();
                                    Form __alertGLDetailForm = new Form();
                                    __alertGLDetailForm.Text = __textHeader;
                                    __alertGLDetailForm.Size = new Size(600, 400);
                                    __alertGLDetailForm.Controls.Add(__detail);
                                    __alertGLDetailForm.StartPosition = FormStartPosition.CenterScreen;

                                    __detail.Dock = DockStyle.Fill;
                                    __detail._glDetailGrid.IsEdit = false;

                                    // add data togrid
                                    for (int glRow = 0; glRow < __glStruct[0]._glDetail.Count; glRow++)
                                    {
                                        string __accCode = __glStruct[0]._glDetail[glRow]._accountCode;
                                        string __accName = __glStruct[0]._glDetail[glRow]._accountName;
                                        string __accDesc = __glStruct[0]._glDetail[glRow]._accountDescription;
                                        string __accBranch = ""; // __glStruct[0]._glDetail[0].bra
                                        decimal __debit = __glStruct[0]._glDetail[glRow]._debit;
                                        decimal __credit = __glStruct[0]._glDetail[glRow]._credit;

                                        int __addRow = __detail._glDetailGrid._addRow();

                                        __detail._glDetailGrid._cellUpdate(__addRow, _g.d.gl_journal_detail._account_code, __accCode, true);
                                        __detail._glDetailGrid._cellUpdate(__addRow, _g.d.gl_journal_detail._account_name, __accName, true);
                                        __detail._glDetailGrid._cellUpdate(__addRow, _g.d.gl_journal_detail._description, __accDesc, true);
                                        __detail._glDetailGrid._cellUpdate(__addRow, _g.d.gl_journal_detail._branch_code, __accBranch, true);
                                        __detail._glDetailGrid._cellUpdate(__addRow, _g.d.gl_journal_detail._debit, __debit, true);
                                        __detail._glDetailGrid._cellUpdate(__addRow, _g.d.gl_journal_detail._credit, __credit, true);

                                    }

                                    __detail._glDetailGrid.Invalidate();

                                    __alertGLDetailForm.ShowDialog();

                                    return;
                                }
                            }
                        }

                        //
                        string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                        //
                        if (__result.Length == 0)
                        {
                            _g.g._companyProfileLoad();
                            if (__processControl != null && __glManual == false && _g.g._companyProfile._gl_process_realtime == true) // (this._myManageData1._mode == 1 && _g.g._companyProfile._gl_process_realtime)
                            {

                                // call process 
                                //SMLERPGL._transProcessUserControl __processControl = new SMLERPGL._transProcessUserControl();
                                __processControl._processGLByTrans(this._screenTop._getDataDate(_g.d.ic_trans._doc_date), __docNo, this._screenTop._docFormatCode);
                                List<SMLERPGL._transProcessUserControl._glStruct> __glStruct = __processControl.getGLTrans;

                                // check balance 

                                // if not balance delete last insert

                                if (__glStruct != null && __glStruct.Count > 0 && __glStruct[0]._debit == __glStruct[0]._credit)
                                {
                                    // save gl
                                    __processControl._saveToDatabase(__glStruct, 1, 1);
                                }
                                /*else if (__glStruct != null && __glStruct.Count > 0 && __glStruct[0]._debit != __glStruct[0]._credit)
                                {
                                    // roll  back data
                                    //StringBuilder __rollBackQuery = new StringBuilder();
                                    StringBuilder __whereIn = new StringBuilder();
                                    // __rollBackQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                    int _getColumnCode = _myManageData1._dataList._gridData._findColumnByName(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_no);
                                    int __columnDocDate = _myManageData1._dataList._gridData._findColumnByName(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_date);

                                    //int _getColumnCustCode = _myManageData1._dataList._gridData._findColumnByName(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._cust_code);
                                    if (_transFlag > 80 && _transFlag < 200)
                                    {
                                        _getColumnCode = _myManageData1._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                                        __columnDocDate = _myManageData1._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date);

                                        //_getColumnCustCode = _myManageData1._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code);
                                    }

                                    string __getDocNo = __docNo;


                                    //string __getCustCode = this._myManageData1._dataList._gridData._cellGet(getData.row, _getColumnCustCode).ToString();
                                    //if (__getCustCode.IndexOf(' ') != -1)
                                    //{
                                    //    __getCustCode = __getCustCode.Split(' ')[0].ToString();
                                    //}
                                    if (__whereIn.Length > 0)
                                    {
                                        __whereIn.Append(",");
                                    }
                                    //__whereIn.Append("''" + __getCustCode + "''");
                                    
                                    __rollBackQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), _myManageData1._dataList._tableName, " where doc_no = \'" + __getDocNo + "\' " + " and trans_flag =" + this._transFlag));
                                    string myFormat = MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ap_ar_trans_detail._table + " where " + _g.d.ap_ar_trans_detail._doc_no + "=\'" + __getDocNo + "\'  and trans_flag =" + this._transFlag);
                                    if (_transFlag > 80 && _transFlag < 200)
                                    {
                                        myFormat = MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __getDocNo + "\'  and trans_flag =" + this._transFlag);
                                    }
                                    __rollBackQuery.Append(string.Format(myFormat, _g.d.ap_ar_trans._table));

                                    // toe ลบเอกสารอ้างอิง (99)
                                    __rollBackQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ap_ar_trans_detail._table + " where " + _g.d.ap_ar_trans_detail._doc_no + "=\'" + __getDocNo + "\' and " + _g.d.ap_ar_trans_detail._trans_flag + "=99"));

                                    __rollBackQuery.Append(this._deleteFirst(__getDocNo));
                                    // ลบ GL
                                    __rollBackQuery.Append(this._glDeleteQuery(__getDocNo));

                                    if (this._withHoldingTax != null)
                                    {
                                        __rollBackQuery.Append(this._withHoldingTax._queryDelete(__getDocNo, this._transControlType));
                                    }

                                    //
                                    __rollBackQuery.Append("</node>");

                                    __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __rollBackQuery.ToString());

                                    if (__result.Length == 0)
                                    {
                                        MessageBox.Show("ยอดเดบิต " + " เครดิต " + " ไม่ลงตัว ", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else
                                    {
                                        MessageBox.Show("error" + __result);
                                    }

                                    return;

                                }*/
                            }

                            this._processCode = this._screenTop._getDataStr(_g.d.ap_ar_trans._cust_code);
                            // Save Log
                            this._saveLog(this._myManageData1._mode);

                            MyLib._myGlobal._displayWarning(1, null);



                            this._myManageData1._dataList._refreshData();
                            // สั่งประมวลผล
                            /*string _whereIn = "";
                            if (this._proceeeOldCode.Length > 0)
                            {
                                _whereIn = "''" + this._proceeeOldCode + "''";
                            }
                            if (this._processCode.Length > 0)
                            {
                                if (_whereIn.Length > 0)
                                {
                                    _whereIn = _whereIn + ",";
                                }
                                _whereIn = _whereIn + "''" + this._processCode + "''";
                            }
                            // Process เช็ค,บัตรเครดิต
                            SMLERPGlobal._smlFrameWork __process = new SMLERPGlobal._smlFrameWork();
                            string __resultProcessChqAndCreditCard = __process._processChqCreditCard(MyLib._myGlobal._databaseName);
                            // Process 
                            SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
                            __smlFrameWork._process_ap_ar_flow(MyLib._myGlobal._databaseName);
                            SMLProcess._sendProcess._process(SMLProcess._sendProcessType.ap_supplier, _whereIn);
                            SMLProcess._sendProcess._procesNow();
                             * */
                            if (this._afterSave != null)
                            {
                                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                                {
                                    this._printFormAfterSave = _checkFormPrint();
                                }
                                this._afterSave();
                            }

                            // toe
                            if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore || _printFormAfterSave == true)
                            {
                                // call print form
                                _printFormData(this._screenTop._docFormatCode);
                            }

                            //if (MyLib._myGlobal._OEMVersion.Equals("imex") == false)
                            {
                                _processThread __process = new _processThread(this._transControlType, "", __docNoList.ToString());

                                //SMLProcess._docFlow __process = new SMLProcess._docFlow();
                                //__process._processAll(_g.g._transControlTypeEnum.ว่าง, "", __docNoList.ToString());

                                Thread __thread = new Thread(new ThreadStart(__process._run));
                                __thread.IsBackground = true;
                                __thread.Start();
                            }

                            //
                            this._screenTop._clear();
                            this._detailGrid._clear();
                            this._screenBottom._clear();
                            this._myManageData1._newData(true);
                            if (this._withHoldingTax != null)
                            {
                                this._withHoldingTax._clear();
                            }
                            // Clear Search Control (cust_code)
                            MyLib._myTextBox __custCode = (MyLib._myTextBox)this._screenTop._getControl(_g.d.ap_ar_trans._cust_code);
                            if (__custCode != null)
                            {
                                this._screenTop._deleteSearchList(__custCode);
                            }
                        }
                        else
                        {
                            MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            this._showPrintDialogByCtrl = false;
        }

        private void _printFormData(string docTypeCode)
        {
            string __docNo = this._screenTop._getDataStr(_g.d.ap_ar_trans._doc_no);

            if (_customPrint != null)
            {
                bool __printResult = _customPrint(this._screenTop._docFormatCode, __docNo, this._transFlag.ToString(), false);
                if (__printResult == true)
                {
                    // update print count
                    MyLib._myFrameWork __myFramework = new MyLib._myFrameWork();
                    __myFramework._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "insert into " + _g.d.erp_print_logs._table + "(" + _g.d.erp_print_logs._doc_no + "," + _g.d.erp_print_logs._trans_flag + "," + _g.d.erp_print_logs._user_code + "," + _g.d.erp_print_logs._print_datetime + ") values (\'" + __docNo + "\'," + this._transFlag.ToString() + ",\'" + MyLib._myGlobal._userCode + "\', \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\')");
                }

            }
            else
            {
                bool __printResult = SMLERPReportTool._global._printForm(docTypeCode, __docNo, _g.g._transFlagGlobal._transFlag(this._transControlType).ToString(), this._showPrintDialogByCtrl);
                if (__printResult == true)
                {
                    // update print count
                    MyLib._myFrameWork __myFramework = new MyLib._myFrameWork();
                    __myFramework._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "insert into " + _g.d.erp_print_logs._table + "(" + _g.d.erp_print_logs._doc_no + "," + _g.d.erp_print_logs._trans_flag + "," + _g.d.erp_print_logs._user_code + "," + _g.d.erp_print_logs._print_datetime + ") values (\'" + __docNo + "\'," + this._transFlag.ToString() + ",\'" + MyLib._myGlobal._userCode + "\', \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\')");
                }
            }
        }

        /*public void _clearScreen()
        {
            try
            {
                this._screenTop._clear();
                this._detailGrid._clear();
                this._screenBottom._clear();
                this._myManageData1._newData(true);
            }
            catch (Exception ex)
            {
            }
        }*/

        string _deleteFirst(string getDocNo)
        {
            StringBuilder __myQuery = new StringBuilder();
            if (this._payControl != null)
            {
                // ลบการจ่ายเงิน


                //SMLERPAPARControl._payControl __payControl = new SMLERPAPARControl._payControl();
                //__payControl._icTransControlType = this._transControlType;
                __myQuery.Append(this._payControl._queryDelete(getDocNo));

                // เงินมัดจำ
                //SMLERPAPARControl._advanceControl __payAdvance = new SMLERPAPARControl._advanceControl();
                //__payAdvance._icTransControlType = this._transControlType;
                //__myQuery.Append(__payAdvance._queryDelete(getDocNo));

                // ลบภาษีหัก ณ. ที่จ่าย
                //SMLERPGLControl._withHoldingTax __withHoldingTax = new SMLERPGLControl._withHoldingTax(SMLERPGLControl._withHoldingTaxType.ว่าง);
                if (this._withHoldingTax != null)
                {
                    __myQuery.Append(this._withHoldingTax._queryDelete(getDocNo, this._transControlType));
                }
                //
                if (this._vatBuy != null)
                {
                    string __whereTransDetail = _g.d.gl_journal_vat_buy._doc_no + "=\'" + getDocNo + "\' and " + _g.d.gl_journal_vat_buy._trans_flag + "=" + this._transFlag.ToString();
                    __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} where " + __whereTransDetail), _g.d.gl_journal_vat_buy._table));
                }
                if (this._vatSale != null)
                {
                    string __whereTransDetail = _g.d.gl_journal_vat_sale._doc_no + "=\'" + getDocNo + "\' and " + _g.d.gl_journal_vat_sale._trans_flag + "=" + this._transFlag.ToString();
                    __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} where " + __whereTransDetail), _g.d.gl_journal_vat_sale._table));
                }
            }
            return __myQuery.ToString();
        }

        void _dataList__deleteData(ArrayList selectRowOrder)
        {
            //Boolean __isdeleteData = true;

            MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myQuery = new StringBuilder();
            StringBuilder __whereIn = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            int _getColumnCode = _myManageData1._dataList._gridData._findColumnByName(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_no);
            int __columnDocDate = _myManageData1._dataList._gridData._findColumnByName(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_date);

            //int _getColumnCustCode = _myManageData1._dataList._gridData._findColumnByName(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._cust_code);
            if (_transFlag > 80 && _transFlag < 200)
            {
                _getColumnCode = _myManageData1._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                __columnDocDate = _myManageData1._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date);

                //_getColumnCustCode = _myManageData1._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code);
            }
            StringBuilder __getDocNoList = new StringBuilder();
            for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
            {
                MyLib._deleteDataType __getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                string __getDocNo = this._myManageData1._dataList._gridData._cellGet(__getData.row, _getColumnCode).ToString();

                if (__columnDocDate != -1)
                {
                    DateTime __getDocDate = (DateTime)_myManageData1._dataList._gridData._cellGet(__getData.row, __columnDocDate);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        if (_g.g._checkOpenPeriod(__getDocDate) == false)
                        {
                            _myManageData1._dataList._refreshData();
                            return;
                        }
                    }
                }

                if (__getDocNo.Trim().Length > 0)
                {
                    if (__getDocNoList.Length > 0)
                    {
                        __getDocNoList.Append(",");
                    }
                    __getDocNoList.Append("\'" + __getDocNo + "\'");
                }
                //string __getCustCode = this._myManageData1._dataList._gridData._cellGet(getData.row, _getColumnCustCode).ToString();
                //if (__getCustCode.IndexOf(' ') != -1)
                //{
                //    __getCustCode = __getCustCode.Split(' ')[0].ToString();
                //}
                if (__whereIn.Length > 0)
                {
                    __whereIn.Append(",");
                }
                //__whereIn.Append("''" + __getCustCode + "''");
                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), _myManageData1._dataList._tableName, __getData.whereString + " and trans_flag =" + this._transFlag));
                string myFormat = MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ap_ar_trans_detail._table + " where " + _g.d.ap_ar_trans_detail._doc_no + "=\'" + __getDocNo + "\'  and trans_flag =" + this._transFlag);
                if (_transFlag > 80 && _transFlag < 200)
                {
                    myFormat = MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __getDocNo + "\'  and trans_flag =" + this._transFlag);
                }
                __myQuery.Append(string.Format(myFormat, _g.d.ap_ar_trans._table));

                // toe ลบเอกสารอ้างอิง (99)
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ap_ar_trans_detail._table + " where " + _g.d.ap_ar_trans_detail._doc_no + "=\'" + __getDocNo + "\' and " + _g.d.ap_ar_trans_detail._trans_flag + "=99"));

                __myQuery.Append(this._deleteFirst(__getDocNo));
                // ลบ GL
                __myQuery.Append(this._glDeleteQuery(__getDocNo));

                if (this._withHoldingTax != null)
                {
                    __myQuery.Append(this._withHoldingTax._queryDelete(__getDocNo, this._transControlType));
                }

                // log
                this._logDocNoOld = "\'" + __getDocNo + "\'";
                this._logDocDateOld = "\'\'";
                if (__columnDocDate != -1) this._logDocDateOld = "\'" + MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDate(_myManageData1._dataList._gridData._cellGet(__getData.row, __columnDocDate).ToString())) + "\'";
                this._logAmountOld = 0M;
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(this._createLog(3)));

            } // for
            //
            __myQuery.Append("</node>");


            // toe get doc_ref
            switch (this._transControlType)
            {

                case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล_ยกเลิก:
                case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก:
                case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล_ยกเลิก:
                case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก:
                    {
                        string __getDocRefQuery = "select " + _g.d.ap_ar_trans_detail._doc_ref + " from ap_ar_trans where doc_no in (" + __getDocNoList + ")";
                        DataTable __refTable = _myFrameWork._queryShort(__getDocRefQuery).Tables[0];
                        for (int __row = 0; __row < __refTable.Rows.Count; __row++)
                        {
                            string __getDocNo = __refTable.Rows[__row][_g.d.ap_ar_trans_detail._doc_ref].ToString();
                            if (__getDocNo.Length > 0)
                            {
                                __getDocNoList.Append("," + "\'" + __getDocNo + "\'");
                            }

                        }

                    }
                    break;
                default:
                    {
                        string __getDocRefQuery = "select " + _g.d.ap_ar_trans_detail._billing_no + " from ap_ar_trans_detail where doc_no in (" + __getDocNoList + ")";
                        DataTable __refTable = _myFrameWork._queryShort(__getDocRefQuery).Tables[0];
                        for (int __row = 0; __row < __refTable.Rows.Count; __row++)
                        {
                            string __getDocNo = __refTable.Rows[__row][_g.d.ap_ar_trans_detail._billing_no].ToString();
                            if (__getDocNo.Length > 0)
                            {
                                __getDocNoList.Append("," + "\'" + __getDocNo + "\'");
                            }

                        }
                    }
                    break;
            }

            /*string __getDocRefQuery = "select " + _g.d.ap_ar_trans_detail._billing_no + " from ap_ar_trans_detail where doc_no in (" + __getDocNoList + ")";
            DataTable __refTable = _myFrameWork._queryShort(__getDocRefQuery).Tables[0];
            for (int __row = 0; __row < __refTable.Rows.Count; __row++)
            {
                string __getDocNo = __refTable.Rows[__row][_g.d.ap_ar_trans_detail._billing_no].ToString();
                if (__getDocNo.Length > 0)
                {
                    __getDocNoList.Append("," + "\'" + __getDocNo + "\'");
                }

            }*/

            string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
            if (result.Length == 0)
            {
                SMLProcess._docFlow __process = new SMLProcess._docFlow();

                // __process._processAll(_g.g._transControlTypeEnum.ว่าง, "", __getDocNoList.ToString());

                __process._processAll(this._transControlType, "", __getDocNoList.ToString());

                //
                MyLib._myGlobal._displayWarning(1, MyLib._myGlobal._resource("ทำงานสำเร็จ"));
                _myManageData1._dataList._refreshData();
                _myManageData1._newData(true);
                // Clear search (cust_code)
                MyLib._myTextBox __custCode = (MyLib._myTextBox)this._screenTop._getControl(_g.d.ap_ar_trans._cust_code);
                if (__custCode != null)
                {
                    this._screenTop._deleteSearchList(__custCode);
                }
            }
            else
            {
                MessageBox.Show(result, MyLib._myGlobal._resource("มีข้อผิดพลาด"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        void _clearData()
        {
            this._screenTop._clear();
            this._detailGrid._clear();
            this._screenBottom._clear();
            this._logDetailOld = new StringBuilder();
            this._logDocNoOld = "\'\'";
            this._logDocDateOld = "\'\'";
            this._logAmountOld = 0M;

            if (this._glScreenTop != null)
            {
                this._glScreenTop._clear();
                this._glDetail._glDetailGrid._clear();
            }
            this._detailGrid._selectBill = null;
            //
            this._screenTop._setDataDate(_g.d.ap_ar_trans._doc_date, MyLib._myGlobal._workingDate);
            this._screenTop._focusFirst();
            this._screenTop._isChange = false;
            this._refDoc._transGrid._clear();
            if (this._payControl != null)
            {
                this._payControl._clear();
            }
            if (this._withHoldingTax != null)
            {
                this._withHoldingTax._clear();
            }
            if (this._vatBuy != null)
            {
                this._vatBuy._vatGrid._clear();
                this._vatBuy._clear();
            }
            if (this._vatSale != null)
            {
                this._vatSale._vatGrid._clear();
            }

            if (this._screenMore != null)
                this._screenMore._clear();
            this._oldDocNo = "";
            this._oldDocRef = "";

            // toe
            this._creator_code = "";
            this._create_datetime = new DateTime();

        }

        void _myManageData1__clearData()
        {
            this._clearData();
        }

        void _myManageData1__newDataClick()
        {
            this._clearData();
            if (this._screenMore != null)
                this._screenMore._newData();
            this._screenTop._setDataDate(_g.d.ap_ar_trans._doc_date, MyLib._myGlobal._workingDate);
            this._screenTop._setDataStr(_g.d.ap_ar_trans._branch_code, MyLib._myGlobal._branchCode);

            if (_g.g._companyProfile._disabled_edit_doc_no_doc_date == true || _g.g._companyProfile._disable_edit_doc_no_doc_date_user == true)
            {
                //this._enabedControl(_g.d.ic_trans._doc_no, false);
                this._screenTop._enabedControl(_g.d.ap_ar_trans._doc_date, false);
                ((MyLib._myTextBox)this._screenTop._getControl(_g.d.ap_ar_trans._doc_no)).textBox.Enabled = false;
            }

        }

        void _myManageData1__closeScreen()
        {
            this.Dispose();
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

        private int _get_ap_ar_column_number()
        {
            return _myManageData1._dataList._gridData._findColumnByName(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_no);
        }

        private int _get_ic_column_number()
        {
            return _myManageData1._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
        }

        bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                this._clearData();
                MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
                ArrayList __rowDataArray = (ArrayList)rowData;
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                int __tableCount = -1;
                if (_transFlag < 80 || _transFlag > 200)
                {
                    this._oldDocNo = __rowDataArray[this._get_ap_ar_column_number()].ToString();
                    __tableCount++;
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ap_ar_trans._table + whereString + " and " + _g.d.ap_ar_trans._trans_flag + "=" + this._transFlag));
                    //
                    __tableCount++;
                    string __extraField = "";
                    if (_transFlag == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.IMEX_Bill_Collector))
                        __extraField = ",(select name_1 from ar_customer where ar_customer.code = ap_ar_trans_detail.cust_code ) as " + _g.d.ap_ar_trans_detail._cust_name;

                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * " + __extraField + " from " + _g.d.ap_ar_trans_detail._table + " where " + _g.d.ap_ar_trans_detail._doc_no + " = '" + this._oldDocNo + "'" + " and " + _g.d.ap_ar_trans._trans_flag + "=" + this._transFlag + " order by " + _g.d.ap_ar_trans_detail._line_number));
                    //
                    __tableCount++;
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ap_ar_trans_detail._table + " where " + _g.d.ap_ar_trans_detail._doc_no + " = '" + this._oldDocNo + "'" + " and " + _g.d.ap_ar_trans._trans_flag + "=99"/* + this._transFlag*/));
                }

                if (_transFlag > 80 && _transFlag < 200)
                {
                    this._oldDocNo = __rowDataArray[_get_ic_column_number()].ToString();
                    __tableCount++;
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_trans._table + whereString + " and " + _g.d.ap_ar_trans._trans_flag + "=" + this._transFlag));
                    //
                    __tableCount++;
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_trans_detail._table + " where upper(" + _g.d.ic_trans_detail._doc_no + ") = '" + this._oldDocNo + "'" + "  and " + _g.d.ap_ar_trans._trans_flag + "=" + this._transFlag));
                }
                int __payControlTableNumber = -1;
                if (this._payControl != null)
                {
                    __payControlTableNumber = __tableCount + 1;
                    __myquery.Append(this._payControl._queryLoad(this._oldDocNo));
                    __tableCount += this._payControl._tableLoadCount;
                    //
                    __tableCount += 2;
                    __myquery.Append(this._withHoldingTax._queryLoad(this._oldDocNo, this._transControlType));
                }
                int __vatBuyTableNumber = -1;
                if (this._vatBuy != null)
                {
                    __tableCount++;
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.gl_journal_vat_buy._table + " where " + _g.d.gl_journal_vat_buy._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.gl_journal_vat_buy._trans_flag + "=" + this._transFlag.ToString()));
                    __vatBuyTableNumber = __tableCount;
                }
                int __vatSaleTableNumber = -1;
                if (this._vatSale != null)
                {
                    __tableCount++;
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.gl_journal_vat_sale._table + " where " + _g.d.gl_journal_vat_sale._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.gl_journal_vat_sale._trans_flag + "=" + this._transFlag.ToString()));
                    __vatSaleTableNumber = __tableCount;
                }
                // Load Gl
                int __glTableNumber = -1;
                int __glDetailTableNumber = -1;
                if (this._glScreenTop != null)
                {
                    /*
                    __tableCount++;
                    __glTableNumber = __tableCount;
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.gl_journal._table + " where " + _g.d.gl_journal._doc_no + "=\'" + this._oldDocNo + "\' "));
                    __tableCount++;
                    __glDetailTableNumber = __tableCount;
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._doc_no + "=\'" + this._oldDocNo + "\' order by " + _g.d.gl_journal_detail._debit_or_credit + "," + _g.d.gl_journal_detail._account_code));
                    */
                    __glTableNumber = __tableCount + 1;
                    __glDetailTableNumber = __glTableNumber + 1;

                    __myquery.Append(this._glDetail._loadDataQuery(this._oldDocNo));
                    __tableCount += 7;

                }
                //
                __myquery.Append("</node>");
                ArrayList __getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                this._screenTop._loadData(((DataSet)__getData[0]).Tables[0]);
                if (this._screenMore != null)
                    this._screenMore._loadData(((DataSet)__getData[0]).Tables[0]);

                DataTable __dt1 = ((DataSet)__getData[0]).Tables[0];
                this._screenTop._docFormatCode = "";
                try
                {
                    this._screenTop._docFormatCode = __dt1.Rows[0][_g.d.ap_ar_trans._doc_format_code].ToString();
                }
                catch
                {
                }
                this._screenBottom._loadData(((DataSet)__getData[0]).Tables[0]);

                this._detailGrid._loadFromDataTable(((DataSet)__getData[1]).Tables[0]);

                if (_transFlag < 80 || _transFlag > 200) this._refDoc._transGrid._loadFromDataTable(((DataSet)__getData[2]).Tables[0]);

                if (this._payControl != null)
                {
                    int __lastNumber = this._payControl._loadToScreen(__getData, __payControlTableNumber);
                    this._withHoldingTax._loadToScreen(__getData, __lastNumber);
                    this._payControl._reCalc();

                }
                if (__vatBuyTableNumber != -1)
                {
                    if (__dt1.Rows[0][_g.d.ic_trans._is_manual_vat].ToString().Equals("1"))
                        this._vatBuy._manualVatCheckbox.Checked = true;


                    this._vatBuy._vatGrid._loadFromDataTable(((DataSet)__getData[__vatBuyTableNumber]).Tables[0]);
                    if (this._transControlType == _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้)
                    {
                        this._vatBuy._checkHideGridVatRefColumn();
                    }
                }
                if (__vatSaleTableNumber != -1)
                {
                    this._vatSale._vatGrid._loadFromDataTable(((DataSet)__getData[__vatSaleTableNumber]).Tables[0]);
                }
                // GL
                if (__glTableNumber != -1)
                {
                    /*
                    this._glScreenTop._loadData((((DataSet)__getData[__glTableNumber]).Tables[0]));
                    this._glDetail._glDetailGrid._loadFromDataTable(((DataSet)__getData[__glDetailTableNumber]).Tables[0]);
                    */

                    this._glScreenTop._loadData((((DataSet)__getData[__glTableNumber]).Tables[0]));
                    this._glDetail._glDetailGrid._loadFromDataTable(((DataSet)__getData[__glTableNumber + 1]).Tables[0]);
                    this._glDetail._loadDataExtra(this._glDetail._glDetailGrid, __getData, __glTableNumber + 2);

                }
                //
                this._screenTop._search(true);
                this._proceeeOldCode = this._screenTop._getDataStr(_g.d.ap_ar_trans._cust_code);
                this._refDoc._custCode = this._screenTop._getDataStr(_g.d.ap_ar_trans._cust_code);

                // toe creator
                if (__dt1.Rows.Count > 0)
                {
                    this._creator_code = __dt1.Rows[0][_g.d.ic_trans._creator_code].ToString();
                    this._create_datetime = MyLib._myGlobal._convertDateFromQuery(__dt1.Rows[0][_g.d.ic_trans._create_datetime].ToString());
                }

                this._reCalc();

                this._logDetailOld = new StringBuilder();
                this._logDetailOld.Append(this._screenTop._logCreate("top"));
                this._logDocDateOld = this._screenTop._getDataStrQuery(_g.d.ap_ar_trans._doc_date);
                this._logDocNoOld = this._screenTop._getDataStrQuery(_g.d.ap_ar_trans._doc_no);
                this._logAmountOld = this._screenBottom._getDataNumber(_g.d.ap_ar_trans._total_net_value);

                // toe olddocref
                for (int __row = 0; __row < this._refDoc._transGrid._rowData.Count; __row++)
                {
                    string __docNo = this._refDoc._transGrid._cellGet(__row, _g.d.ap_ar_trans_detail._billing_no).ToString();
                    this._oldDocRef = this._docNoAdd(this._oldDocRef, __docNo);
                }

                for (int __row = 0; __row < this._detailGrid._rowData.Count; __row++)
                {
                    string __docNo = this._detailGrid._cellGet(__row, _g.d.ap_ar_trans_detail._billing_no).ToString();
                    this._oldDocRef = this._docNoAdd(this._oldDocRef, __docNo);

                }

                return (true);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
            }
            return (false);
        }

        void _screenGrid__alterCellUpdate(object sender, int row, int column)
        {
            /*try
            {
                string _doc_no = this._screenTop._getDataStr(_g.d.ap_ar_trans._doc_no);
                string _doc_date = this._screenTop._getDataStr(_g.d.ap_ar_trans._doc_date);
                this._screenGrid._calcTotal();
                this._screenGrid.Invalidate();

                switch (this._controlTypeTemp)
                {
                    case _g.g._controlTypeEnum.เจ้าหนี้_ใบรับวางบิล:
                        this._screenBottom._setDataNumber(_g.d.ap_ar_trans._total_value, ((MyLib._myGrid._columnType)this._screenGrid._columnList[this._screenGrid._findColumnByName(_g.d.ap_ar_trans_detail._sum_value)])._total);
                        this._screenBottom._setDataNumber(_g.d.ap_ar_trans._total_before_vat, ((MyLib._myGrid._columnType)this._screenGrid._columnList[this._screenGrid._findColumnByName(_g.d.ap_ar_trans_detail._sum_before_vat)])._total);
                        this._screenBottom._setDataNumber(_g.d.ap_ar_trans._total_vat_value, ((MyLib._myGrid._columnType)this._screenGrid._columnList[this._screenGrid._findColumnByName(_g.d.ap_ar_trans_detail._sum_tax_value)])._total);
                        this._screenBottom._setDataNumber(_g.d.ap_ar_trans._total_discount, ((MyLib._myGrid._columnType)this._screenGrid._columnList[this._screenGrid._findColumnByName(_g.d.ap_ar_trans_detail._sum_discount)])._total);
                        this._screenBottom._setDataNumber(_g.d.ap_ar_trans._total_net_value, ((MyLib._myGrid._columnType)this._screenGrid._columnList[this._screenGrid._findColumnByName(_g.d.ap_ar_trans_detail._sum_debt_value)])._total);
                        break;
                    case _g.g._controlTypeEnum.ลูกหนี้_ใบวางบิล:
                    case _g.g._controlTypeEnum.APCancelPayBill:
                    case _g.g._controlTypeEnum.ARCancelPayBill:
                        this._screenGrid._calcTotal();
                        this._screenGrid.Invalidate();
                        this._screenBottom._setDataNumber(_g.d.ap_ar_trans._total_value, ((MyLib._myGrid._columnType)this._screenGrid._columnList[this._screenGrid._findColumnByName(_g.d.ap_ar_trans_detail._sum_value)])._total);
                        this._screenBottom._setDataNumber(_g.d.ap_ar_trans._total_before_vat, ((MyLib._myGrid._columnType)this._screenGrid._columnList[this._screenGrid._findColumnByName(_g.d.ap_ar_trans_detail._sum_before_vat)])._total);
                        this._screenBottom._setDataNumber(_g.d.ap_ar_trans._total_vat_value, ((MyLib._myGrid._columnType)this._screenGrid._columnList[this._screenGrid._findColumnByName(_g.d.ap_ar_trans_detail._sum_tax_value)])._total);
                        this._screenBottom._setDataNumber(_g.d.ap_ar_trans._total_discount, ((MyLib._myGrid._columnType)this._screenGrid._columnList[this._screenGrid._findColumnByName(_g.d.ap_ar_trans_detail._sum_discount)])._total);
                        this._screenBottom._setDataNumber(_g.d.ap_ar_trans._total_net_value, ((MyLib._myGrid._columnType)this._screenGrid._columnList[this._screenGrid._findColumnByName(_g.d.ap_ar_trans_detail._sum_debt_value)])._total);




                        string _total_debt_balance = this._screenBottom._getDataNumber(_g.d.ap_ar_trans._total_debt_balance).ToString();
                        string _query_where = _g.d.ap_ar_trans._trans_flag + " = " + this._transFlag;
                        if ((_doc_no.Length > 0) && (_total_debt_balance.Length > 0))
                        {
                            _pay_bill._pay._onload(_doc_no, _total_debt_balance, _query_where, _doc_date, false);
                        }
                        break;
                    case _g.g._controlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                    case _g.g._controlTypeEnum.ลูกหนี้_รับชำระหนี้:
                    case _g.g._controlTypeEnum.APCancelDebtBilling:
                    case _g.g._controlTypeEnum.ARCancelDebtBilling:

                        this._screenGrid._calcTotal();
                        this._screenGrid.Invalidate();

                        this._screenBottom._setDataNumber(_g.d.ap_ar_trans._total_value, ((MyLib._myGrid._columnType)this._screenGrid._columnList[this._screenGrid._findColumnByName(_g.d.ap_ar_trans_detail._sum_value)])._total);
                        this._screenBottom._setDataNumber(_g.d.ap_ar_trans._total_before_vat, ((MyLib._myGrid._columnType)this._screenGrid._columnList[this._screenGrid._findColumnByName(_g.d.ap_ar_trans_detail._sum_before_vat)])._total);
                        this._screenBottom._setDataNumber(_g.d.ap_ar_trans._total_vat_value, ((MyLib._myGrid._columnType)this._screenGrid._columnList[this._screenGrid._findColumnByName(_g.d.ap_ar_trans_detail._sum_tax_value)])._total);
                        this._screenBottom._setDataNumber(_g.d.ap_ar_trans._total_discount, ((MyLib._myGrid._columnType)this._screenGrid._columnList[this._screenGrid._findColumnByName(_g.d.ap_ar_trans_detail._sum_discount)])._total);
                        this._screenBottom._setDataNumber(_g.d.ap_ar_trans._total_net_value, ((MyLib._myGrid._columnType)this._screenGrid._columnList[this._screenGrid._findColumnByName(_g.d.ap_ar_trans_detail._sum_debt_value)])._total);
                        this._screenBottom._setDataNumber(_g.d.ap_ar_trans._total_pay_money, ((MyLib._myGrid._columnType)this._screenGrid._columnList[this._screenGrid._findColumnByName(_g.d.ap_ar_trans_detail._sum_pay_money)])._total);
                        decimal __getDeptBalance = ((MyLib._myGrid._columnType)this._screenGrid._columnList[this._screenGrid._findColumnByName(_g.d.ap_ar_trans_detail._sum_debt_balance)])._total;
                        this._screenBottom._setDataNumber(_g.d.ap_ar_trans._total_debt_balance, ((MyLib._myGrid._columnType)this._screenGrid._columnList[this._screenGrid._findColumnByName(_g.d.ap_ar_trans_detail._sum_debt_balance)])._total);

                        //decimal _total_debt_amount = ((MyLib._myGrid._columnType)this._screenGrid._columnList[this._screenGrid._findColumnByName(_g.d.ap_ar_trans_detail._sum_debt_value)])._total;
                        //decimal _sum_pay_money = ((MyLib._myGrid._columnType)this._screenGrid._columnList[this._screenGrid._findColumnByName(_g.d.ap_ar_trans_detail._sum_pay_money)])._total;
                        //this._screenBottom._setDataNumber(_g.d.ap_ar_trans._total_debt_balance, (_total_debt_amount - _sum_pay_money));

                        string _total_debt_balance2 = this._screenBottom._getDataNumber(_g.d.ap_ar_trans._total_debt_balance).ToString();
                        string _query_where2 = _g.d.ap_ar_trans._trans_flag + " = " + this._transFlag;
                        if ((_doc_no.Length > 0) && (_total_debt_balance2.Length > 0))
                        {
                            _pay_bill._pay._onload(_doc_no, _total_debt_balance2, _query_where2, _doc_date, false);
                        }
                        break;
                }
            }
            catch (Exception)
            {
            }*/
        }

        bool _screenGrid__keyDown(object sender, int row, int column, Keys keyCode)
        {
            if (row == 0 && (keyCode == Keys.PageUp || keyCode == Keys.Up))
            {
                this._screenTop.Focus();
                return true;
            }
            return false;
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                this._showPrintDialogByCtrl = true;
            }
            this._saveData();
        }

        private void _glButton_Click(object sender, EventArgs e)
        {
            DateTime __dt = MyLib._myGlobal._convertDate(this._screenTop._getDataStr(_g.d.ap_ar_trans._doc_date));
            if ((__dt.Year > 1979) ||
                this._screenTop._getDataStr(_g.d.ap_ar_trans._ar_code).Length > 0)
            {
                _gl._setDate(_g.d.gl_journal._doc_date, this._screenTop._getDataStr(_g.d.ap_ar_trans._doc_date));
                _gl._setText(_g.d.gl_journal._ap_ar_code, this._screenTop._getDataStr(_g.d.ap_ar_trans._ar_code));
                _gl._setText(_g.d.gl_journal._ap_ar_originate_from, this._controlTypeTemp.ToString());
                _gl.ShowDialog();
            }
            else
            {
                MessageBox.Show(MyLib._myGlobal._resource("วันที่เอกสารห้ามว่าง"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void _print_Click(object sender, EventArgs e)
        {
            // _print_from();
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                this._showPrintDialogByCtrl = true;
            }

            // toe call print  other version
            this._printFormAfterSave = _checkFormPrint();

            if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore || this._printFormAfterSave == true)
            {
                this._printFormData(this._screenTop._docFormatCode);
            }
            this._showPrintDialogByCtrl = false;

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

        void _print_from()
        {
            DateTime __dt = MyLib._myGlobal._convertDate(this._screenTop._getDataStr(_g.d.ap_ar_trans._doc_date));
            if ((__dt.Year > 1979) ||
                this._screenTop._getDataStr(_g.d.ap_ar_trans._ar_code).Length > 0)
            {
                _report_ar_ap._view _view = new _report_ar_ap._view();
                _view.scree_name = _g.g._transFlagGlobal._transFlag(this._controlTypeTemp).ToString();
                _view.__loadDefaul(this._controlTypeTemp.ToString());
                _view.ShowDialog();
            }
            else
            {
                MessageBox.Show(MyLib._myGlobal._resource("วันที่เอกสารห้ามว่าง"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

        }

        /// <summary>
        /// คำนวณยอดรวม
        /// </summary>
        public void _reCalc()
        {
            try
            {
                if (this._controlTypeTemp.Equals(_g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล) || this._controlTypeTemp.Equals(_g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล))
                {
                    this._detailGrid.Invalidate();
                    decimal __taxAtPayAmount = this._screenBottom._getDataNumber(_g.d.ap_ar_trans._total_pay_tax);
                    decimal __totalValue = ((MyLib._myGrid._columnType)this._detailGrid._columnList[this._detailGrid._findColumnByName(_g.d.ap_ar_trans_detail._sum_pay_money)])._total;
                    decimal __totalNetValue = __totalValue - __taxAtPayAmount;
                    this._screenBottom._setDataNumber(_g.d.ap_ar_trans._total_net_value, __totalNetValue);
                }
                else if (this._controlTypeTemp.Equals(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้) ||
                     this._controlTypeTemp.Equals(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้))
                {
                    /*
                      ||
                     this._controlTypeTemp.Equals(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก) ||
                     this._controlTypeTemp.Equals(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก)
                     */
                    this._detailGrid._calcTotal(false);
                    decimal __totalPayMoney = ((MyLib._myGrid._columnType)this._detailGrid._columnList[this._detailGrid._findColumnByName(_g.d.ap_ar_trans_detail._sum_pay_money)])._total;
                    this._screenBottom._setDataNumber(_g.d.ap_ar_trans._total_net_value, __totalPayMoney);
                    this._payControl._reCalc();
                }
                else if (this._controlTypeTemp.Equals(_g.g._transControlTypeEnum.IMEX_Bill_Collector))
                {
                    this._detailGrid._calcTotal(false);
                    decimal __totalPayMoney = ((MyLib._myGrid._columnType)this._detailGrid._columnList[this._detailGrid._findColumnByName(_g.d.ap_ar_trans_detail._sum_debt_amount)])._total;
                    this._screenBottom._setDataNumber(_g.d.ap_ar_trans._total_net_value, __totalPayMoney);

                }


            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }
    }

    public class _processThread
    {
        private string _docNoList = "";
        private _g.g._transControlTypeEnum _transControlType = _g.g._transControlTypeEnum.ว่าง;

        public _processThread(_g.g._transControlTypeEnum transControlType, string itemList, string docNoList)
        {
            this._transControlType = transControlType;
            this._docNoList = docNoList;
        }

        public void _run()
        {
            try
            {
                SMLProcess._docFlow __process = new SMLProcess._docFlow();
                __process._processAll(this._transControlType, "", _docNoList);

                /*SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();

                //
                _g.g._updateDateTimeForCalc(this._transControlType, "");
                // Process Stock
                //if (_g.g._companyProfile._disable_auto_stock_process == false)
                //{
                string __resultStr = __smlFrameWork._process_stock_balance(MyLib._myGlobal._databaseName, "", _g.g._transFlagGlobal._transFlag(this._transControlType).ToString());
                if (__resultStr.Length > 0)
                {
                    Console.WriteLine("_run : " + __resultStr);
                }
                //}*/

            }
            catch
            {
            }
        }
    }

}