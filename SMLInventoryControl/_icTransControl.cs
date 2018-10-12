using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Threading;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using Crom.Controls.Docking;

// Anek : Create 02-06-2009
namespace SMLInventoryControl
{
    public partial class _icTransControl : UserControl
    {
        private MyLib._myPanel _myPanelGl;
        private System.Windows.Forms.TabPage _tab_gl;
        private System.Windows.Forms.TabPage _tab_shipment;
        private _shipmentControl _shipmentControl;

        private SMLERPGLControl._journalScreen _glScreenTop;
        private SMLERPGLControl._glDetail _glDetail;
        private _g.g._transControlTypeEnum _transControlTypeTemp;
        private MyLib._searchDataFull _searchItem;
        private SMLERPGlobal._searchProperties _searchScreenProperties;
        private SMLERPAPARControl._payControl _payControl;
        private SMLERPAPARControl._advanceControl _payAdvance;
        private SMLERPGLControl._withHoldingTax _withHoldingTax;
        private SMLERPGLControl._vatBuy _vatBuy;
        private SMLERPGLControl._vatSale _vatSale;
        private _basket._productOriginalStyleForm _productOriginalStyle;
        private SMLERPControl._icBalanceForm _icBalance;
        private SMLERPControl._getPicture _docPicture;

        private _lotForm _lotScreen;
        SMLERPControl._customer._arStatusForm _arInfoForm;
        private _importHandheld._importHandheldForm _importFromHandHeld;
        private StringBuilder _logDetailOld;
        private string _logDocDateOld;
        private string _logDocNoOld;
        private decimal _logAmountOld;
        private string _lastDocRefNo = "";
        private string _lastItemCodeInfo = "";
        private string __newTime = "";
        private string _icTransTable = _g.d.ic_trans._table;
        private string _icTransDetailTable = _g.d.ic_trans_detail._table;

        public _search_bill _searchPurchase;
        public MyLib._myGrid _myGridTemp;
        public string _oldDocNo = "";
        public string _oldDocRef = "";
        public int _getTransFlag = 0;
        public int _getTransType = 0;
        public int _getTransFlagTemp = 0;
        public int _getTransTypeTemp = 0;
        public int _getTransStatus = 0;
        public string _getTransTemplate = "";
        public int _getSaleStatus = 0;

        public string _menuNameTemp = "";
        public string _menuName
        {
            get
            {
                return this._menuNameTemp;
            }
            set
            {
                this._menuNameTemp = value;
                this._icTransScreenTop._menuName = value;
            }
        }

        public _g.g._vatTypeEnum _vatTypeTemp = _g.g._vatTypeEnum.ว่าง;
        public string _dataListExtraWhere = "";
        public Boolean _transControlDisplayOnly = false;
        //toe
        public bool _showPrintDialogByCtrl = false;
        /// <summary>เป็นการเปิดใบกำกับภาษีใหม่จาก POS หรือไม่</summary>
        private bool _fullInvoiceFromPOS = false;

        public Boolean _isImportFromCart = false;
        public string _importCartNumber = "";

        public Boolean _isImportFromHandHeld = false;
        public string _importHandHeldNumber = "";

        public Boolean _isImportFromInternet = false;
        public string _importInternetNumber = "";

        // toe for edit
        private string _creator_code = "";
        private DateTime _create_datetime = new DateTime();
        //private Boolean _getResource = true; 


        private DateTime _lastSaveDate = new DateTime();
        private String _lastDocFormatCode = "";

        /// <summary>
        /// สำหรับเอาไว้ดูรายวันจาก DataCenter กำหนดคู่กับ Server Active Sync
        /// </summary>
        public Boolean _loadFromDataCenter = false;
        string _requestApproveCode = "";

        private Boolean _isEditData = true;
        public Boolean _isEdit
        {
            get
            {
                return _isEditData;
            }

            set
            {
                this._isEditData = value;
                this._icTransScreenTop.Enabled = _isEditData;

                if (this._icTransItemGrid != null)
                    this._icTransItemGrid._isEdit = _isEditData;

                if (this._icTransScreenBottom != null)
                    this._icTransScreenBottom.Enabled = _isEditData;

                if (this._icTransScreenMore != null)
                    this._icTransScreenMore.Enabled = _isEditData;

                if (this._vatBuy != null)
                    this._vatBuy.Enabled = _isEditData;

                if (this._vatSale != null)
                    this._vatSale.Enabled = _isEditData;

                if (this._withHoldingTax != null)
                    this._withHoldingTax.Enabled = _isEditData;

                if (this._glScreenTop != null)
                    this._glScreenTop.Enabled = _isEditData;

                if (this._glDetail != null)
                    this._glDetail._glDetailGrid._isEdit = _isEditData;

                if (this._shipmentControl != null)
                    this._shipmentControl.Enabled = _isEditData;

                if (this._payAdvance != null)
                    this._payAdvance._dataGrid._isEdit = _isEditData;

            }
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
                this._glScreenTop._setDataDate(_g.d.gl_journal._doc_date, MyLib._myGlobal._convertDate(this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_date)));
                this._glScreenTop._setDataStr(_g.d.gl_journal._doc_no, this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_no));
                _g.g._accountPeriodClass __accountPeriod = _g.g._accountPeriodClassFind(this._icTransScreenTop._getDataDate(_g.d.ic_trans._doc_date));
                if (__accountPeriod != null)
                {
                    this._glScreenTop._setDataStr(_g.d.gl_journal._period_number, __accountPeriod._number.ToString());
                    this._glScreenTop._setDataStr(_g.d.gl_journal._account_year, __accountPeriod._year.ToString());
                }

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
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLIMS ||
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

        public _g.g._vatTypeEnum _vatType
        {
            set
            {
                this._vatTypeTemp = value;
            }
            get
            {
                int __data = (int)MyLib._myGlobal._decimalPhase(this._icTransScreenTop._getDataStr(_g.d.ic_trans._vat_type));
                switch (__data)
                {
                    case 0: this._vatTypeTemp = _g.g._vatTypeEnum.ภาษีแยกนอก; break;
                    case 1: this._vatTypeTemp = _g.g._vatTypeEnum.ภาษีรวมใน; break;
                    case 2: this._vatTypeTemp = _g.g._vatTypeEnum.ยกเว้นภาษี; break;
                    default:
                        MessageBox.Show("_vatType Error");
                        break;
                }
                return this._vatTypeTemp;
            }
        }

        public int _vatTypeNumber()
        {
            switch (this._vatType)
            {
                case _g.g._vatTypeEnum.ภาษีแยกนอก: return 0;
                case _g.g._vatTypeEnum.ภาษีรวมใน: return 1;
                case _g.g._vatTypeEnum.ยกเว้นภาษี: return 2;
                default:
                    MessageBox.Show("_vatTypeNumber Error");
                    break;
            }
            return 0;
        }

        public _g.g._transControlTypeEnum _transControlType
        {
            set
            {
                if (value != _g.g._transControlTypeEnum.ว่าง)
                {
                    //this._icTransScreenTop._getResource =
                    //    this._icTransScreenMore._getResource =
                    //    this._icTransScreenBottom._getResource = this._getResource;


                    this._transControlTypeTemp = value;
                    //
                    this._icTransItemGrid._icTransControlType = value;
                    this._icTransScreenTop._icTransControlType = value;
                    this._icTransScreenBottom._icTransControlType = value;
                    this._icTransRef._icTransControlType = value;
                    this._icTransScreenMore._icTransControlType = value;
                    this._icTransScreenBottom._itemGrid = this._icTransItemGrid;

                    // toe
                    //this._icTransScreenTop._icTransScreenBottom = this._icTransScreenBottom;

                    // Recent
                    this._icTransScreenTop._recentXmlFileName = "ictrans" + value.ToString();
                    //
                    if (this._transControlDisplayOnly == false)
                    {
                        if (MyLib._myGlobal._isDesignMode == false)
                        {
                            this._searchPurchase = new _search_bill();
                            this._searchItem = new MyLib._searchDataFull();
                            this._searchScreenProperties = new SMLERPGlobal._searchProperties();
                            //
                            this._icTransScreenTop._vatType += new VatTypeEventHandler(_icTransScreenTop__vatType);
                            this._icTransScreenTop._managerData = this._myManageTrans;
                            this._icTransScreenTop._textBoxSaved += new MyLib.TextBoxSavedHandler(_icTransScreenTop__textBoxSaved);
                            this._icTransScreenTop._textBoxChanged += new MyLib.TextBoxChangedHandler(_icTransScreenTop__textBoxChanged);
                            this._icTransScreenTop._saveKeyDown += new MyLib.SaveKeyDownHandler(_ictransScreenTop__saveKeyDown);
                            this._icTransScreenTop._radiotButtonCheckedChanged += new RadioButtonCheckedChangedEventHandler(_ictransScreenTop__radiotButtonCheckedChanged);
                            this._icTransScreenTop._comboBoxSelectIndexChanged += new MyLib.ComboBoxSelectIndexChangedHandler(_icTransScreenTop__comboBoxSelectIndexChanged);
                            this._icTransScreenTop._icTransItemGrid = this._icTransItemGrid;
                            this._icTransScreenTop._vatCase += new VatCaseEventHandler(_icTransScreenTop__vatCase);
                            this._icTransScreenTop._getBranchCode += _icTransScreenTop__getBranchCode;
                            //
                            this._saveButton.Click += new EventHandler(_saveButton_Click);
                            //
                            this._icTransRef.Visible = false;
                            //
                            this._icTransItemGrid._vatTypeNumber += new _icTransItemGridControl.VatTypeNumberHandler(_icTransItemGrid__vatTypeNumber);
                            this._icTransItemGrid._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_ictransItemGrid__queryForInsertCheck);
                            this._icTransItemGrid._icTransScreenTop = this._icTransScreenTop;
                            this._icTransItemGrid._vatType += new _icTransItemGridControl.VatTypeEventHandler(_icTransItemGrid__vatType);
                            this._icTransItemGrid._getDocDate += new _icTransItemGridControl.DocDateEventHandler(_icTransItemGrid__getDocDate);
                            this._icTransItemGrid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_ictransItemGrid__alterCellUpdate);
                            this._icTransItemGrid._vatRate += new _icTransItemGridControl.VatRateEventHandler(_icTransItemGrid__vatRate);
                            this._icTransItemGrid._reCalc += new _icTransItemGridControl.RecalcEventHandler(_icTransItemGrid__reCalc);
                            this._icTransItemGrid._afterSelectRow += new MyLib.AfterSelectRowEventHandler(_icTransItemGrid__afterSelectRow);
                            this._icTransItemGrid._afterProcess += new _icTransItemGridControl.AfterProcessEventHandler(_icTransItemGrid__afterProcess);
                            this._icTransItemGrid._changeCreditDay += new ChangeCreditDayEventHandler(_ictransScreenTop__changeCreditDay);
                            this._icTransItemGrid._setRemark += _icTransItemGrid__setRemark;
                            this._icTransItemGrid._docNo += new _icTransItemGridControl.DocNoHandler(_icTransItemGrid__docNo);
                            this._icTransItemGrid._docNoOld += new _icTransItemGridControl.DocNoOldHandler(_icTransItemGrid__docNoOld);
                            this._icTransItemGrid._recheckCount += new _icTransItemGridControl.RecheckCountEventHandler(_icTransItemGrid__recheckCount);
                            this._icTransItemGrid._recheckCountDay += new _icTransItemGridControl.RecheckCountDayEventHandler(_icTransItemGrid__recheckCountDay);
                            this._icTransItemGrid._itemInfo += new _icTransItemGridControl.ItemInfoHandler(_icTransItemGrid__itemInfo);
                            this._icTransItemGrid._itemReplacement += new _icTransItemGridControl.ItemReplacementHandler(_icTransItemGrid__itemReplacement);
                            this._icTransItemGrid._lot += new _icTransItemGridControl.LotHandler(_icTransItemGrid__lot);
                            this._icTransItemGrid._afterRemoveRow += new MyLib.AfterRemoveRowEventHandler(_icTransItemGrid__afterRemoveRow);
                            this._icTransItemGrid._exchangeRateGet += new _icTransItemGridControl.ExchangeRateGetEventHandler(_icTransItemGrid__exchangeRateGet);
                            this._icTransItemGrid._getBranchCode += _icTransItemGrid__getBranchCode;
                            // 
                            this._myToolBar.EnabledChanged += new EventHandler(_myToolBar_EnabledChanged);
                            this._checkPurchasePermiumButton.Enabled = false;
                            this._checkPurchasePermiumButton.Visible = false;
                            this._purchasePointButton.Enabled = false;
                            this._purchasePointButton.Visible = false;
                            //
                            this._myManageTrans._dataList._flowButton.Enabled = true;
                            this._myManageTrans._dataList._gridData._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_gridData__beforeDisplayRow);
                            this._myManageTrans._dataList._flowEvent += new MyLib.FlowEventHandler(_dataList__flowEvent);
                            //
                            this._myManageTrans._dataList._controlKeyEvent += new MyLib.ControlKeyEventHandler(_dataList__controlKeyEvent);
                            this._myManageTrans._checkEditData += new MyLib.CheckEditDataEvent(_myManageTrans__checkEditData);
                            this._icTransScreenTop._changeCreditDay += new ChangeCreditDayEventHandler(_ictransScreenTop__changeCreditDay);
                            //
                            this._load();
                            // ปุ่มเพิ่ม
                            switch (value)
                            {
                                case _g.g._transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ:
                                    {
                                        MyLib.ToolStripMyButton __getItem = new MyLib.ToolStripMyButton();
                                        __getItem.ResourceName = "เรียกรายการสินค้าจากเอกสารอ้างอิง";
                                        __getItem.Text = "เรียกรายการสินค้าจากเอกสารอ้างอิง";
                                        this._myToolBar.Items.Add(__getItem);
                                        __getItem.Click += (s1, e1) =>
                                        {
                                            // ค้นหา PO
                                            string __poNumber = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_ref).ToUpper();
                                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                                            StringBuilder __myquery = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans._doc_date + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans._doc_no + "=\'" + __poNumber + "\'"));
                                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + this._icTransDetailTable + " where " + _g.d.ic_trans_detail._last_status + "=0 and " + _g.d.ic_trans_detail._doc_no + "=\'" + __poNumber + "\' order by " + _g.d.ic_trans_detail._line_number));
                                            __myquery.Append("</node>");
                                            //
                                            ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                                            DataTable __dt = ((DataSet)__getData[0]).Tables[0];
                                            DataTable __details = ((DataSet)__getData[1]).Tables[0];
                                            if (__dt.Rows.Count > 0)
                                            {
                                                this._icTransItemGrid._clear();
                                                for (int __row = 0; __row < __details.Rows.Count; __row++)
                                                {
                                                    DataRow __detail = __details.Rows[__row];
                                                    int __addr = this._icTransItemGrid._addRow();
                                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._barcode, __detail[_g.d.ic_trans_detail._barcode].ToString(), false);
                                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __detail[_g.d.ic_trans_detail._barcode].ToString(), false);
                                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __detail[_g.d.ic_trans_detail._barcode].ToString(), false);
                                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __detail[_g.d.ic_trans_detail._unit_code].ToString(), false);
                                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._qty, MyLib._myGlobal._decimalPhase(__detail[_g.d.ic_trans_detail._qty].ToString()), false);
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show(MyLib._myGlobal._resource("ไม่พบเอกสารอ้างอิง"));
                                            }
                                        };
                                    };
                                    break;

                                case _g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก:
                                    {
                                        MyLib.ToolStripMyButton __getItem = new MyLib.ToolStripMyButton();
                                        __getItem.ResourceName = "เรียกรายการสินค้าจากบิลขาย";
                                        __getItem.Text = "เรียกรายการสินค้าจากบิลขาย";
                                        this._myToolBar.Items.Add(__getItem);
                                        __getItem.Click += (s1, e1) =>
                                        {
                                            this._icTransScreenTop._saveLastControl();
                                            // ค้นหา PO
                                            string __poNumber = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_ref).ToUpper();
                                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                                            StringBuilder __myquery = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans._doc_date + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._trans_flag + "=44 and " + _g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans._doc_no + "=\'" + __poNumber + "\'"));
                                            //__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._trans_flag + "=44 and " + _g.d.ic_trans_detail._last_status + "=0 and " + _g.d.ic_trans_detail._doc_no + "=\'" + __poNumber + "\' order by " + _g.d.ic_trans_detail._line_number));
                                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select item_code, (select name_1 from ic_inventory where ic_inventory.code = item_code) as item_name, unit_code, wh_code, shelf_code, sum(qty) as qty " +
                                                ", array_to_string(array(select item_name from ic_trans_detail as n where n.doc_no = \'" + __poNumber + "\' and n.item_code = ic_trans_detail.item_code and n.wh_code = ic_trans_detail.wh_code and n.shelf_code = ic_trans_detail.shelf_code order by line_number), ' ') as group_item_name " +
                                                " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._trans_flag + "=44 and " + _g.d.ic_trans_detail._last_status + "=0 and " + _g.d.ic_trans_detail._doc_no + "=\'" + __poNumber + "\' group by item_code, unit_code, wh_code, shelf_code order by item_code "));


                                            __myquery.Append("</node>");
                                            //
                                            ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                                            DataTable __dt = ((DataSet)__getData[0]).Tables[0];
                                            DataTable __details = ((DataSet)__getData[1]).Tables[0];
                                            if (__dt.Rows.Count > 0)
                                            {
                                                this._icTransItemGrid._clear();
                                                for (int __row = 0; __row < __details.Rows.Count; __row++)
                                                {
                                                    DataRow __detail = __details.Rows[__row];
                                                    int __addr = this._icTransItemGrid._addRow();
                                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __detail[_g.d.ic_trans_detail._item_code].ToString(), false);
                                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __detail["group_item_name"].ToString(), false);
                                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __detail[_g.d.ic_trans_detail._unit_code].ToString(), false);

                                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code, __detail[_g.d.ic_trans_detail._wh_code].ToString(), false);
                                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code, __detail[_g.d.ic_trans_detail._shelf_code].ToString(), false);

                                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._qty, MyLib._myGlobal._decimalPhase(__detail[_g.d.ic_trans_detail._qty].ToString()), false);
                                                }
                                                this._icTransItemGrid._searchUnitNameWareHouseNameShelfNameAll();

                                                this._icTransScreenTop._enabedControl(_g.d.ic_trans._cust_code, false);
                                            }
                                            else
                                            {
                                                MessageBox.Show(MyLib._myGlobal._resource("ไม่พบเอกสารอ้างอิง"));
                                            }
                                        };

                                        // ใบเบิก
                                        MyLib.ToolStripMyButton __getIssueItem = new MyLib.ToolStripMyButton();
                                        __getIssueItem.ResourceName = "เรียกรายการสินค้าจากใบเบิก";
                                        __getIssueItem.Text = "เรียกรายการสินค้าจากใบเบิก";
                                        this._myToolBar.Items.Add(__getIssueItem);
                                        __getIssueItem.Click += (s1, e1) =>
                                        {
                                            this._icTransScreenTop._saveLastControl();
                                            // ค้นหา PO
                                            string __poNumber = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_ref).ToUpper();
                                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                                            StringBuilder __myquery = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans._doc_date + " from " + _g.d.ic_wms_trans_detail._table + " where " + _g.d.ic_trans._trans_flag + "=522 and " + _g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans._doc_no + "=\'" + __poNumber + "\'"));
                                            //__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._trans_flag + "=44 and " + _g.d.ic_trans_detail._last_status + "=0 and " + _g.d.ic_trans_detail._doc_no + "=\'" + __poNumber + "\' order by " + _g.d.ic_trans_detail._line_number));
                                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select item_code, (select name_1 from ic_inventory where ic_inventory.code = item_code) as item_name, unit_code, wh_code, shelf_code, sum(qty) as qty " +
                                                ",item_name as group_item_name " +
                                                " from " + _g.d.ic_wms_trans_detail._table + " where " + _g.d.ic_trans_detail._trans_flag + "=522 and " + _g.d.ic_trans_detail._last_status + "=0 and " + _g.d.ic_trans_detail._doc_no + "=\'" + __poNumber + "\' group by item_code, unit_code, wh_code, shelf_code,item_name order by item_code "));


                                            __myquery.Append("</node>");
                                            //
                                            ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                                            DataTable __dt = ((DataSet)__getData[0]).Tables[0];
                                            DataTable __details = ((DataSet)__getData[1]).Tables[0];
                                            if (__dt.Rows.Count > 0)
                                            {
                                                this._icTransItemGrid._clear();
                                                for (int __row = 0; __row < __details.Rows.Count; __row++)
                                                {
                                                    DataRow __detail = __details.Rows[__row];
                                                    int __addr = this._icTransItemGrid._addRow();
                                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __detail[_g.d.ic_trans_detail._item_code].ToString(), false);
                                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __detail["group_item_name"].ToString(), false);
                                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __detail[_g.d.ic_trans_detail._unit_code].ToString(), false);

                                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code, __detail[_g.d.ic_trans_detail._wh_code].ToString(), false);
                                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code, __detail[_g.d.ic_trans_detail._shelf_code].ToString(), false);

                                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._qty, MyLib._myGlobal._decimalPhase(__detail[_g.d.ic_trans_detail._qty].ToString()), false);
                                                }
                                                this._icTransItemGrid._searchUnitNameWareHouseNameShelfNameAll();
                                                this._icTransScreenTop._enabedControl(_g.d.ic_trans._cust_code, false);
                                            }
                                            else
                                            {
                                                MessageBox.Show(MyLib._myGlobal._resource("ไม่พบเอกสารอ้างอิง"));
                                            }
                                        };
                                    };
                                    break;

                                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                                    {
                                        if (_g.g._companyProfile._request_ar_credit)
                                        {
                                            MyLib.ToolStripMyButton __getItem = new MyLib.ToolStripMyButton();
                                            __getItem.ResourceName = "เรียกรายการรออนุมัติ";
                                            __getItem.Text = "เรียกรายการรออนุมัติ";
                                            this._myToolBar.Items.Add(__getItem);
                                            __getItem.Click += (s1, e1) =>
                                            {
                                                // show doc for approve
                                                this._getSaleOrderForApprove();
                                            };
                                        }
                                    }
                                    break;

                            }

                        }
                    }
                    this._transControlDisplayOnly = false;
                    //Debug.Print(Environment.StackTrace.ToString());
                    this._build();
                    this._myManageTrans__newDataClick();
                    this.Invalidate();
                }
            }
            get
            {
                return this._transControlTypeTemp;
            }
        }
        private string _icTransScreenTop__getBranchCode() {
            return this._icTransScreenTop__getBranchCode(0);
        }


        private string _icTransScreenTop__getBranchCode(int mode)
        {
            if (this._icTransScreenBottom != null)
            {
                switch (this._transControlType)
                {
                    case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                        if (mode == 1)
                        {
                            return this._icTransScreenMore._getDataStr(_g.d.ic_trans._branch_code_to);
                        }
                        else {
                            return this._icTransScreenMore._getDataStr(_g.d.ic_trans._branch_code);
                        }
                       
                    default:
                        return this._icTransScreenMore._getDataStr(_g.d.ic_trans._branch_code);
                }
            }
            return "";
        }

        private void _getSaleOrderForApprove()
        {
            Form __formRequestSaleOrderCredit = new Form();
            __formRequestSaleOrderCredit.StartPosition = FormStartPosition.CenterScreen;
            __formRequestSaleOrderCredit.Size = new Size(1024, 768);


            _approveOrderControl __approveControl = new _approveOrderControl();
            __approveControl._processButton.Click += (approveControlSender, approveEventargs) =>
            {
                __formRequestSaleOrderCredit.Close();
                string __getRefNo = __approveControl._dataList._gridData._cellGet(__approveControl._dataList._gridData._selectRow, _g.d.ic_trans_draft._table + "." + _g.d.ic_trans_draft._doc_no).ToString();

                // ย้าย password ไปเช็คก่อน Save
                /*
                _pricePasswordForm __passForm = new _pricePasswordForm(5);
                __passForm.label1.Text = "Ref No :";
                __passForm._userTextBox.Text = __getRefNo;
                __passForm._processButton.Click += (processButtonSender, processButtonEventArgs) =>
                {
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    string __getPassword = __passForm._passwordTextBox.Text;
                    // check user and pass from otp

                    bool __pass = false;

                    string __getQuery = "select doc_no, approve_code, req_datetime from erp_request_order where doc_no = \'" + __getRefNo + "\'";
                    DataTable __approveDataTable = __myFrameWork._queryShort(__getQuery).Tables[0];

                    if (__approveDataTable.Rows.Count > 0)
                    {
                        string __approvePassword = __approveDataTable.Rows[0][_g.d.erp_request_order._approve_code].ToString();
                        if (__getPassword.Equals(__approvePassword))
                        {
                            __pass = true;
                        }
                        else
                        {
                            MessageBox.Show("รหัสอนุมัติวงเงินไม่ถูกต้อง", "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }


                    // if has expire resent new otp
                    if (false)
                    {

                        if (MessageBox.Show("รหัสผ่านหมดอายุ คุณต้องการส่งคำขออนุมัติใหม่ หรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            __passForm.Close();

                            MyLib.RandomStringGenerator __gen = new MyLib.RandomStringGenerator();
                            string __approveCode = __gen.NextString(6, false, false, true, false, false);

                            string __genNewOtpResult = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update " + _g.d.erp_request_order._table + " set " + _g.d.erp_request_order._req_datetime + "=\'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\'," + _g.d.erp_request_order._approve_code + "=\'" + __approveCode + "\'  where " + _g.d.erp_request_order._doc_no + "=\'" + __getRefNo + "\'");
                            if (__genNewOtpResult.Length ==0)
                            {
                                MyLib._myGlobal._displayWarning(1, MyLib._myGlobal._resource("ส่งคำขออนุมัติใหม่สำเร็จ"));
                            }
                            else
                            {
                                MessageBox.Show(MyLib._myGlobal._errorText(__genNewOtpResult), "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }

                    // pass get order

                    if (__pass)
                    {
                        // get order load to screen
                        __passForm.Close();
                        // 
                        StringBuilder __queryGetOrder = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

                        __queryGetOrder.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_trans_draft._table +" where " + _g.d.ic_trans_draft._doc_no + "=\'" + __getRefNo + "\'"));
                        __queryGetOrder.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_trans_detail_draft._table + " where " + _g.d.ic_trans_detail_draft._doc_no + "=\'" + __getRefNo + "\' order by line_number "));
                        __queryGetOrder.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ap_ar_trans_detail_draft._table + " where " + _g.d.ap_ar_trans_detail_draft._doc_no + "=\'" + __getRefNo + "\'"));
                        __queryGetOrder.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.cb_trans_detail_draft._table + " where " + _g.d.cb_trans_detail_draft._doc_no + "=\'" + __getRefNo + "\'"));


                        __queryGetOrder.Append("</node>");

                        ArrayList __getOrderResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryGetOrder.ToString());
                        if (__getOrderResult.Count > 0)
                        {
                            DataTable __transTable = ((DataSet)__getOrderResult[0]).Tables[0];
                            DataTable __transDetaiilTable = ((DataSet)__getOrderResult[1]).Tables[0];
                            DataTable __aparTransTable = ((DataSet)__getOrderResult[2]).Tables[0];
                            DataTable __cbTransTable = ((DataSet)__getOrderResult[3]).Tables[0];

                            this._clearScreen();
                            this._icTransScreenTop._loadData(__transTable);
                            this._icTransScreenBottom._loadData(__transTable);
                            this._icTransScreenMore._loadData(__transTable);

                            this._icTransRef._transGrid._loadFromDataTable(__aparTransTable);
                            this._icTransItemGrid._loadFromDataTable(__transDetaiilTable);
                            this._payAdvance._dataGrid._loadFromDataTable(__cbTransTable);

                            this._icTransScreenTop._search(false);
                            this._icTransScreenMore._search();
                            this._icTransScreenBottom._search();
                            this._icTransItemGrid._searchUnitNameWareHouseNameShelfNameAll();

                            // run new doc
                            this._icTransScreenTop._setDataStr(_g.d.ic_trans._doc_no, this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_format_code));
                        }
                    }

                };

                __passForm.ShowDialog();
                */

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                StringBuilder __queryGetOrder = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

                __queryGetOrder.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_trans_draft._table + " where " + _g.d.ic_trans_draft._doc_no + "=\'" + __getRefNo + "\'"));
                __queryGetOrder.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_trans_detail_draft._table + " where " + _g.d.ic_trans_detail_draft._doc_no + "=\'" + __getRefNo + "\' order by line_number "));
                __queryGetOrder.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ap_ar_trans_detail_draft._table + " where " + _g.d.ap_ar_trans_detail_draft._doc_no + "=\'" + __getRefNo + "\'"));
                __queryGetOrder.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.cb_trans_detail_draft._table + " where " + _g.d.cb_trans_detail_draft._doc_no + "=\'" + __getRefNo + "\'"));


                __queryGetOrder.Append("</node>");

                ArrayList __getOrderResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryGetOrder.ToString());
                if (__getOrderResult.Count > 0)
                {
                    DataTable __transTable = ((DataSet)__getOrderResult[0]).Tables[0];
                    DataTable __transDetaiilTable = ((DataSet)__getOrderResult[1]).Tables[0];
                    DataTable __aparTransTable = ((DataSet)__getOrderResult[2]).Tables[0];
                    DataTable __cbTransTable = ((DataSet)__getOrderResult[3]).Tables[0];

                    this._clearScreen();
                    this._icTransScreenTop._loadData(__transTable);
                    this._icTransScreenBottom._loadData(__transTable);
                    this._icTransScreenMore._loadData(__transTable);

                    this._icTransRef._transGrid._loadFromDataTable(__aparTransTable);
                    this._icTransItemGrid._loadFromDataTable(__transDetaiilTable);
                    this._payAdvance._dataGrid._loadFromDataTable(__cbTransTable);

                    this._icTransScreenTop._search(false);
                    this._icTransScreenMore._search(false);
                    this._icTransScreenBottom._search();
                    this._icTransItemGrid._searchUnitNameWareHouseNameShelfNameAll();

                    // run new doc
                    this._icTransScreenTop._setDataStr(_g.d.ic_trans._doc_no, this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_format_code));
                    this._requestApproveCode = __getRefNo;

                    this._isEdit = false;
                }

            };



            __approveControl.Dock = DockStyle.Fill;
            __formRequestSaleOrderCredit.Controls.Add(__approveControl);
            __formRequestSaleOrderCredit.ShowDialog();
        }


        private string _icTransItemGrid__getBranchCode(int mode)
        {
            return this._icTransScreenTop__getBranchCode(mode);
        }

        private decimal _icTransItemGrid__exchangeRateGet()
        {
            return (this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._exchange_rate) == 0) ? 1 : this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._exchange_rate);
        }

        void _icTransItemGrid__setRemark(string fieldName, string remark)
        {
            this._icTransScreenBottom._setDataStr(fieldName, remark);

            if (this._icTransScreenMore != null)
            {
                this._icTransScreenMore._setDataStr(fieldName, remark);
            }
        }

        void _icTransItemGrid__afterRemoveRow(object sender)
        {
            this._calc(sender);
        }

        void _icTransItemGrid__lot(bool loadLotControl)
        {

            if (this._lotScreen == null && loadLotControl)
            {

                this._lotScreen = new _lotForm(this._transControlType);
                DockableFormInfo __form0 = this._myManageTrans._dock.Add(this._lotScreen, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
                __form0.ShowCloseButton = true;
                __form0.ShowContextMenuButton = false;
                __form0.Disposed += (s1, e1) =>
                {
                    try
                    {
                        this._lotScreen.Dispose();
                    }
                    catch
                    {
                    }
                    this._lotScreen = null;
                };
            }
            int __itemCodeColumnNumber = this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._item_code);
            if (this._lotScreen != null && __itemCodeColumnNumber != -1)
            {
                this._lotScreen._load(this._icTransItemGrid, this._icTransItemGrid._selectRow, DateTime.Now);
            }
        }

        private void _clearMemory()
        {
            if (this._searchPurchase != null) this._searchPurchase.Dispose();
            if (this._searchItem != null) this._searchItem.Dispose();
            if (this._myGridTemp != null) this._myGridTemp.Dispose();
            if (this._payControl != null) this._payControl.Dispose();
            if (this._payAdvance != null) this._payAdvance.Dispose();
            if (this._withHoldingTax != null) this._withHoldingTax.Dispose();
            if (this._vatBuy != null) this._vatBuy.Dispose();
            if (this._vatSale != null) this._vatSale.Dispose();
            if (this._productOriginalStyle != null) this._productOriginalStyle.Dispose();
            if (this._icBalance != null) this._icBalance.Dispose();
            if (this._importFromHandHeld != null) this._importFromHandHeld.Dispose();
            if (this._icTransItemGrid != null) this._icTransItemGrid.Dispose();
            if (this._icTransScreenTop != null) this._icTransScreenTop.Dispose();
            if (this._icTransScreenBottom != null) this._icTransScreenBottom.Dispose();
            if (this._icTransRef != null) this._icTransRef.Dispose();
            if (this._icTransScreenMore != null) this._icTransScreenMore.Dispose();
            //
            this._searchPurchase = null;
            this._searchItem = null;
            this._myGridTemp = null;
            this._payControl = null;
            this._payAdvance = null;
            this._withHoldingTax = null;
            this._vatBuy = null;
            this._vatSale = null;
            this._productOriginalStyle = null;
            this._icBalance = null;
            this._icTransItemGrid = null;
            this._icTransScreenTop = null;
            this._icTransScreenBottom = null;
            this._icTransRef = null;
            this._icTransScreenMore = null;
            GC.SuppressFinalize(this);
            GC.Collect();
        }

        private void _basket()
        {
            // Basket
            switch (this._transControlType)
            {
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                case _g.g._transControlTypeEnum.คลัง_ยอดคงเหลือสินค้ายกมา:
                case _g.g._transControlTypeEnum.คลัง_ตรวจนับสินค้า:
                case _g.g._transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ:
                case _g.g._transControlTypeEnum.คลัง_จ่ายสินค้าออก_จากการขาย:
                case _g.g._transControlTypeEnum.คลัง_ปรับปรุงเพิ่มลดสินค้า:
                case _g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า:
                case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก:
                case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา:
                case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                case _g.g._transControlTypeEnum.สินค้า_โอนเข้า:
                case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                    if (this._productOriginalStyle == null)
                    {
                        this._productOriginalStyle = new _basket._productOriginalStyleForm();
                        DockableFormInfo __form = this._myManageTrans._dock.Add(this._productOriginalStyle, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
                        __form.ShowCloseButton = true;
                        __form.ShowContextMenuButton = false;
                        this._productOriginalStyle.Disposed += (s1, e1) =>
                            {
                                try
                                {
                                    this._productOriginalStyle.Dispose();
                                }
                                catch
                                {
                                }
                                this._productOriginalStyle = null;
                            };
                        this._productOriginalStyle._moveItemList += (object sender) =>
                        {
                            for (int __row = 0; __row < this._productOriginalStyle._basketGrid._rowData.Count; __row++)
                            {
                                string __itemCode = this._productOriginalStyle._basketGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString().Trim();
                                if (__itemCode.Length > 0)
                                {
                                    string __itemName = this._productOriginalStyle._basketGrid._cellGet(__row, _g.d.ic_trans_detail._item_name).ToString();
                                    string __unitCode = this._productOriginalStyle._basketGrid._cellGet(__row, _g.d.ic_trans_detail._unit_code).ToString();
                                    decimal __qty = MyLib._myGlobal._decimalPhase(this._productOriginalStyle._basketGrid._cellGet(__row, _g.d.ic_trans_detail._qty).ToString());
                                    int __addr = this._icTransItemGrid._addRow();
                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, true);
                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __itemName, false);
                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, true);
                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._qty, __qty, true);
                                }
                            }
                        };
                    }
                    break;
            }
        }

        SMLInventoryControl._icmainGridReplacementControl _replacementGrid;
        void _icTransItemGrid__itemReplacement(string itemCode)
        {
            switch (this._transControlType)
            {
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                case _g.g._transControlTypeEnum.คลัง_ยอดคงเหลือสินค้ายกมา:
                case _g.g._transControlTypeEnum.คลัง_ตรวจนับสินค้า:
                case _g.g._transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ:
                case _g.g._transControlTypeEnum.คลัง_จ่ายสินค้าออก_จากการขาย:
                case _g.g._transControlTypeEnum.คลัง_ปรับปรุงเพิ่มลดสินค้า:
                case _g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า:
                case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก:
                case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา:
                case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                case _g.g._transControlTypeEnum.สินค้า_โอนเข้า:
                case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                    {
                        if (this._replacementGrid == null)
                        {
                            this._replacementGrid = new _icmainGridReplacementControl();
                            this._replacementGrid._load(itemCode);
                            Form __replacementForm = new Form();
                            __replacementForm.Size = new System.Drawing.Size(800, 600);
                            this._replacementGrid.Dock = DockStyle.Fill;

                            __replacementForm.Controls.Add(this._replacementGrid);
                            __replacementForm.Text = MyLib._myGlobal._resource("สินค้าทดแทน") + " " + itemCode;

                            DockableFormInfo __form0 = this._myManageTrans._dock.Add(__replacementForm, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());

                            __form0.ShowCloseButton = true;
                            __form0.ShowContextMenuButton = false;
                            __form0.Disposed += (s1, e1) =>
                            {
                                try
                                {
                                    this._replacementGrid.Dispose();
                                }
                                catch
                                {
                                }
                                this._icBalance = null;
                            };
                        }

                        //MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        //string __query = "select * ,(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._code) + "=" + MyLib._myGlobal._addUpper(_g.d.ic_inventory_replacement._table + "." + _g.d.ic_inventory_replacement._ic_replace_code) + ") as " + _g.d.ic_inventory_replacement._ic_name + " from " + _g.d.ic_inventory_replacement._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_replacement._ic_replace_code) + "=\'" + itemCode.ToString().ToUpper() + "\'";

                        //DataSet __getData = __myFrameWork._queryShort(__query);
                        //this._replacementGrid._loadFromDataTable(__getData.Tables[0]);


                        //this._replacementForm.Dock = DockStyle.Fill;


                        //this._icBalance._load(itemCode);
                    }
                    break;
            }
        }

        void _icTransItemGrid__itemInfo(string itemCode)
        {
            switch (this._transControlType)
            {
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                case _g.g._transControlTypeEnum.คลัง_ยอดคงเหลือสินค้ายกมา:
                case _g.g._transControlTypeEnum.คลัง_ตรวจนับสินค้า:
                case _g.g._transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ:
                case _g.g._transControlTypeEnum.คลัง_จ่ายสินค้าออก_จากการขาย:
                case _g.g._transControlTypeEnum.คลัง_ปรับปรุงเพิ่มลดสินค้า:
                case _g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า:
                case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก:
                case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา:
                case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                case _g.g._transControlTypeEnum.สินค้า_โอนเข้า:
                case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                    if (this._icBalance == null)
                    {
                        this._icBalance = new SMLERPControl._icBalanceForm();
                        if (_g.g._companyProfile._hidden_price_formula)
                        {
                            this._icBalance.splitContainer4.Panel2Collapsed = true;
                            this._icBalance.splitContainer4.Panel2.Hide();
                        }
                        this._icBalance._getWarehouseLocation += _icBalance__getWarehouseLocation;
                        DockableFormInfo __form0 = this._myManageTrans._dock.Add(this._icBalance, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
                        __form0.ShowCloseButton = true;
                        __form0.ShowContextMenuButton = false;
                        __form0.Disposed += (s1, e1) =>
                        {
                            try
                            {
                                this._icBalance.Dispose();
                            }
                            catch
                            {
                            }
                            this._icBalance = null;
                        };
                        this._icBalance._load(itemCode);
                    }
                    break;
            }
        }

        private void _icBalance__getWarehouseLocation(string whCode, string locationCode)
        {
            Boolean __pass = true;
            if (_g.g._companyProfile._perm_wh_shelf)
            {
                __pass = false;
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                StringBuilder __extraWhere = new StringBuilder();

                string __screen_type = "";
                switch (this._transControlType)
                {
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                        __screen_type = " and " + _g.d.erp_user_group_wh_shelf._screen_code + "=\'" + "PU" + "\' ";
                        break;
                    case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                    case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                        {
                            if (MyLib._myGlobal._programName.Equals("SML CM"))
                            {
                                __screen_type = " and " + _g.d.erp_user_group_wh_shelf._screen_code + "=\'" + "SI" + "\' ";
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                        __screen_type = " and " + _g.d.erp_user_group_wh_shelf._screen_code + "=\'" + "SI" + "\' ";
                        break;
                    case _g.g._transControlTypeEnum.สินค้า_โอนเข้า:
                    case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                    case _g.g._transControlTypeEnum.สินค้า_โอนเข้า_ยกเลิก:
                    case _g.g._transControlTypeEnum.สินค้า_โอนออก_ยกเลิก:
                        __screen_type = " and " + _g.d.erp_user_group_wh_shelf._screen_code + "=\'" + "IM" + "\' ";
                        break;
                    case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                    case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก:
                        __screen_type = " and " + _g.d.erp_user_group_wh_shelf._screen_code + "=\'" + "ST" + "\' ";
                        break;
                }

                __extraWhere.Append("select " + _g.d.ic_wh_shelf._wh_code + "," + _g.d.ic_wh_shelf._shelf_code + " from " + _g.d.ic_wh_shelf._table + " where " + _g.d.ic_wh_shelf._ic_code + "=\'" + this._icTransItemGrid._cellGet(this._icTransItemGrid._selectRow, _g.d.ic_trans_detail._item_code).ToString() + "\'");
                __extraWhere.Append(" and " + _g.d.ic_wh_shelf._wh_code + "=\'" + whCode + "\'");
                __extraWhere.Append(" and " + _g.d.ic_wh_shelf._shelf_code + "=\'" + locationCode + "\'");
                if (__screen_type.Length > 0)
                {
                    // __queryWareHouseAndShelf = "select * from (" + __queryWareHouseAndShelf + ") as temp1 where " +
                    __extraWhere.Append(" and " +
                    _g.d.ic_wh_shelf._wh_code + " in (select " + _g.d.erp_user_group_wh_shelf._wh_code + " from " + _g.d.erp_user_group_wh_shelf._table + " where " + _g.d.erp_user_group_wh_shelf._group_code + " in (select " + _g.d.erp_user_group_detail._group_code + " from " + _g.d.erp_user_group_detail._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_user_group_detail._user_code) + " = '" + MyLib._myGlobal._userCode.ToUpper() + "') " + __screen_type + ") and " +
                    _g.d.ic_wh_shelf._shelf_code + " in (select " + _g.d.erp_user_group_wh_shelf._shelf_code + " from " + _g.d.erp_user_group_wh_shelf._table + " where " + _g.d.erp_user_group_wh_shelf._group_code + " in (select " + _g.d.erp_user_group_detail._group_code + " from " + _g.d.erp_user_group_detail._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_user_group_detail._user_code) + " = '" + MyLib._myGlobal._userCode.ToUpper() + "') " + __screen_type + ") "
                    );
                }

                DataTable __result = __myFrameWork._queryShort(__extraWhere.ToString()).Tables[0];
                if (__result.Rows.Count > 0)
                {
                    __pass = true;
                }
            }

            if (__pass)
            {
                this._icTransItemGrid._cellUpdate(this._icTransItemGrid._selectRow, (this._icTransItemGrid._selectWareHouseAndShelfMode == 0) ? _g.d.ic_trans_detail._wh_code : _g.d.ic_trans_detail._wh_code_2, whCode, false);
                this._icTransItemGrid._cellUpdate(this._icTransItemGrid._selectRow, (this._icTransItemGrid._selectWareHouseAndShelfMode == 0) ? _g.d.ic_trans_detail._shelf_code : _g.d.ic_trans_detail._shelf_code_2, locationCode, true);
                this._icTransItemGrid._searchUnitNameWareHouseNameShelfNameAll();
            }
        }

        decimal _icTransItemGrid__recheckCountDay()
        {
            decimal __result = MyLib._myGlobal._decimalPhase(this._icTransScreenTop._getDataNumber(_g.d.ic_trans._recheck_count_day).ToString());
            return __result;
        }

        bool _icTransItemGrid__recheckCount()
        {
            int __result = (int)MyLib._myGlobal._decimalPhase(this._icTransScreenTop._getDataStr(_g.d.ic_trans._recheck_count).ToString());
            return (__result == 0) ? false : true;
        }

        public _icTransControl()
        {
            InitializeComponent();

            //if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            //{
            //   this._getResource = false;
            //}

            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this._mySelectBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this._toolStripExtra.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._logDocNoOld = "\'\'";
            this._logDocDateOld = "\'\'";
            this._logAmountOld = 0M;
            this._logDetailOld = new StringBuilder();

            this.Disposed += (s1, e1) =>
            {
                this._clearMemory();
            };

            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
            {
                _g.g._checkOpenPeriod();
            }

            this._printButton.Visible = false;
            /*if (_g.g._transControlDisplayOnly == false)
            {
                if (MyLib._myGlobal._isDesignMode  == false)
                {
                    this._searchPurchase = new _search_bill();
                    this._searchItem = new MyLib._searchDataFull();
                    this._searchScreenProperties = new _searchProperties();
                    //
                    this._icTransScreenTop._vatType += new VatTypeEventHandler(_icTransScreenTop__vatType);
                    this._icTransScreenTop._managerData = this._myManageTrans;
                    this._icTransScreenTop._textBoxSaved += new MyLib.TextBoxSavedHandler(_icTransScreenTop__textBoxSaved);
                    this._icTransScreenTop._textBoxChanged += new MyLib.TextBoxChangedHandler(_icTransScreenTop__textBoxChanged);
                    this._icTransScreenTop._saveKeyDown += new MyLib.SaveKeyDownHandler(_ictransScreenTop__saveKeyDown);
                    this._icTransScreenTop._radiotButtonCheckedChanged += new RadioButtonCheckedChangedEventHandler(_ictransScreenTop__radiotButtonCheckedChanged);
                    this._icTransScreenTop._comboBoxSelectIndexChanged += new MyLib.ComboBoxSelectIndexChangedHandler(_icTransScreenTop__comboBoxSelectIndexChanged);
                    this._icTransScreenTop._icTransItemGrid = this._icTransItemGrid;
                    this._icTransScreenTop._vatCase += new VatCaseEventHandler(_icTransScreenTop__vatCase);
                    this._saveButton.Click += new EventHandler(_saveButton_Click);
                    this._icTransRef.Visible = false;
                    this._icTransItemGrid._vatTypeNumber += new _icTransItemGridControl.VatTypeNumberHandler(_icTransItemGrid__vatTypeNumber);
                    this._icTransItemGrid._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_ictransItemGrid__queryForInsertCheck);
                    this._icTransItemGrid._icTransScreenTop = this._icTransScreenTop;
                    this._icTransItemGrid._vatType += new _icTransItemGridControl.VatTypeEventHandler(_icTransItemGrid__vatType);
                    this._icTransItemGrid._getDocDate += new _icTransItemGridControl.DocDateEventHandler(_icTransItemGrid__getDocDate);
                    this._icTransItemGrid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_ictransItemGrid__alterCellUpdate);
                    this._icTransItemGrid._vatRate += new _icTransItemGridControl.VatRateEventHandler(_icTransItemGrid__vatRate);
                    this._icTransItemGrid._reCalc += new _icTransItemGridControl.RecalcEventHandler(_icTransItemGrid__reCalc);
                    this._icTransItemGrid._afterSelectRow += new MyLib.AfterSelectRowEventHandler(_icTransItemGrid__afterSelectRow);
                    this._icTransItemGrid._afterProcess += new _icTransItemGridControl.AfterProcessEventHandler(_icTransItemGrid__afterProcess);
                    this._icTransItemGrid._docNo += new _icTransItemGridControl.DocNoHandler(_icTransItemGrid__docNo);
                    this._icTransItemGrid._docNoOld += new _icTransItemGridControl.DocNoOldHandler(_icTransItemGrid__docNoOld);
                    // 
                    this._myToolBar.EnabledChanged += new EventHandler(_myToolBar_EnabledChanged);
                    this._checkPurchasePermiumButton.Enabled = false;
                    this._checkPurchasePermiumButton.Visible = false;
                    this._purchasePointButton.Enabled = false;
                    this._purchasePointButton.Visible = false;
                    //
                    this._myManageTrans._dataList._flowButton.Enabled = true;
                    this._myManageTrans._dataList._gridData._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_gridData__beforeDisplayRow);
                    this._myManageTrans._dataList._flowEvent += new MyLib.FlowEventHandler(_dataList__flowEvent);
                    //
                    this._myManageTrans._dataList._controlKeyEvent += new MyLib.ControlKeyEventHandler(_dataList__controlKeyEvent);
                    this._myManageTrans._checkEditData += new MyLib.CheckEditDataEvent(_myManageTrans__checkEditData);
                    this._icTransScreenTop._changeCreditDay += new ChangeCreditDayEventHandler(_ictransScreenTop__changeCreditDay);
                    //
                    this.Load += new EventHandler(_ictransControl_Load);
                }
            }
            _g.g._transControlDisplayOnly = false;*/
        }

        string _icTransItemGrid__docNoOld()
        {
            return this._oldDocNo;
        }

        string _icTransItemGrid__docNo()
        {
            return this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_no);
        }

        int _icTransItemGrid__vatTypeNumber()
        {
            return this._vatTypeNumber();
        }

        _g.g._vatTypeEnum _icTransScreenTop__vatType()
        {
            return this._vatType;
        }

        void _icTransItemGrid__afterProcess(string discountWord, decimal discountAmount)
        {
            if (discountAmount != 0 && discountWord.Length == 0)
            {
                discountWord = discountAmount.ToString();
            }
            this._icTransScreenBottom._setDataStr(_g.d.ic_trans._discount_word, discountWord);

            // 
            if (this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ)
            {
                string __docNoPack = this._icTransRef._getDocRefPackForQuery(-1);
                // check singha online

                if (__docNoPack.Length > 0)
                {
                    string __queryCheckDocFromSinghaOnline = "select doc_no, " + _g.d.ic_trans._ref_doc_type + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans._ref_doc_type + "=\'SOS\' ";
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                    DataTable __singhaDoc = __myFrameWork._queryShort(__queryCheckDocFromSinghaOnline).Tables[0];
                    if (__singhaDoc.Rows.Count > 0)
                    {
                        for (int __row = 0; __row < __singhaDoc.Rows.Count; __row++)
                        {
                            string __refDocNo = __singhaDoc.Rows[__row]["doc_no"].ToString();
                            string __getShipmentRef = "select * from ic_trans_shipment where doc_no = '" + __refDocNo + "' ";

                            DataTable __shipmentRef = __myFrameWork._queryShort(__getShipmentRef).Tables[0];
                            if (this._shipmentControl != null && __shipmentRef.Rows.Count > 0)
                            {
                                this._shipmentControl._shipmentScreen._loadData(__shipmentRef);
                                this._shipmentControl._search(false);
                            }
                        }

                    }
                }

            }
        }

        void _gridFindItemInfo()
        {
            Boolean __display = false;
            StringBuilder __html = new StringBuilder(@"
    <html><head><style>body {
	font-family: tahoma, verdana, geneva, arial, helvetica, sans-serif;
	font-size: 9pt;
    color='#585858';
	margin: 2pt;
	margin-bottom: 0pt;
	margin-left: 2pt;
	margin-right: 0pt;
	margin-top: 2pt;	
    }</style></head><body bgcolor='#E0F8F7'>");
            int __itemCodeColumnNumber = this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._item_code);
            if (__itemCodeColumnNumber != -1)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __itemCode = this._icTransItemGrid._cellGet(this._icTransItemGrid._selectRow, __itemCodeColumnNumber).ToString().Trim().ToUpper();
                if (__itemCode.Length > 0)
                {
                    if (__itemCode.Equals(this._lastItemCodeInfo) == false)
                    {
                        // แสดงรายละเอียดสินค้า
                        if (this._icBalance != null)
                        {
                            this._icBalance._load(__itemCode);
                        }
                        //

                        // แสดง รายการสินค้าทดแทน
                        if (this._replacementGrid != null)
                        {
                            this._replacementGrid._load(__itemCode);
                        }
                        this._lastItemCodeInfo = __itemCode;
                        __display = true;
                        string __fieldBalanceQtyByCost = "balance_qty_cost";
                        string __calcPack = "/(select stand_value/divide_value from ic_unit_use where ic_unit_use.ic_code=ic_inventory.code and ic_unit_use.code=ic_inventory.unit_standard)";
                        string __queryUnit = "(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + "{0})";
                        string __query = "select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_inventory._name_1, String.Format(__queryUnit + "||\'(\'||{0}||\')\'", _g.d.ic_inventory._table + "." + _g.d.ic_inventory._unit_standard) + " as " + _g.d.ic_inventory._unit_standard, _g.d.ic_inventory._balance_qty + " as " + __fieldBalanceQtyByCost, _g.d.ic_inventory._balance_qty + __calcPack + " as " + _g.d.ic_inventory._balance_qty, _g.d.ic_inventory._book_out_qty + __calcPack + " as " + _g.d.ic_inventory._book_out_qty, _g.d.ic_inventory._accrued_in_qty + __calcPack + " as " + _g.d.ic_inventory._accrued_in_qty, _g.d.ic_inventory._accrued_out_qty + __calcPack + " as " + _g.d.ic_inventory._accrued_out_qty, _g.d.ic_inventory._unit_type) + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + __itemCode + "\'";
                        string __queryPacking = "select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_unit_use._code, String.Format(__queryUnit, _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code) + " as " + _g.d.ic_unit_use._name_1, _g.d.ic_unit_use._row_order, _g.d.ic_unit_use._stand_value, _g.d.ic_unit_use._divide_value) + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._status + "=1 and " + _g.d.ic_unit_use._ic_code + "=\'" + __itemCode + "\' order by " + _g.d.ic_unit_use._ratio;
                        //DataTable __dt = __myFrameWork._queryShort(__query).Tables[0];
                        StringBuilder __myquery = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryPacking));
                        __myquery.Append("</node>");
                        //
                        ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                        DataTable __dt = ((DataSet)__getData[0]).Tables[0];
                        if (__dt.Rows.Count > 0)
                        {
                            decimal __balanceQtyByCost = MyLib._myGlobal._decimalPhase(__dt.Rows[0][__fieldBalanceQtyByCost].ToString());
                            decimal __balanceQty = MyLib._myGlobal._decimalPhase(__dt.Rows[0][_g.d.ic_inventory._balance_qty].ToString());
                            decimal __bookOutQty = MyLib._myGlobal._decimalPhase(__dt.Rows[0][_g.d.ic_inventory._book_out_qty].ToString());
                            decimal __accruedInQty = MyLib._myGlobal._decimalPhase(__dt.Rows[0][_g.d.ic_inventory._accrued_in_qty].ToString());
                            decimal __accruedOutQty = MyLib._myGlobal._decimalPhase(__dt.Rows[0][_g.d.ic_inventory._accrued_out_qty].ToString());
                            decimal __availableQty = __balanceQty - __accruedOutQty;
                            int __unitType = (int)MyLib._myGlobal._decimalPhase(__dt.Rows[0][_g.d.ic_inventory._unit_type].ToString());
                            __html.Append("<b><font color='black'>" + __dt.Rows[0][_g.d.ic_inventory._name_1].ToString() + "</font></b>&nbsp;");
                            __html.Append(MyLib._myGlobal._resource("คงเหลือ") + ":&nbsp;<b><b><font color='green'>" + MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __balanceQty) + "</font></b>&nbsp;<b><font color='black'>" + __dt.Rows[0][_g.d.ic_inventory._unit_standard].ToString() + "</font></b>&nbsp;");
                            if (__accruedInQty != 0.0M) __html.Append(MyLib._myGlobal._resource("ค้างรับ") + ":&nbsp;<b><font color='green'>" + MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __accruedInQty) + "</font></b>&nbsp;");
                            if (__bookOutQty != 0.0M) __html.Append(MyLib._myGlobal._resource("ค้างจอง") + ":&nbsp;<b><font color='red'>" + MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __bookOutQty) + "</font></b>&nbsp;");
                            if (__accruedOutQty != 0.0M) __html.Append(MyLib._myGlobal._resource("ค้างส่ง") + ":&nbsp;<b><font color='red'>" + MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __accruedOutQty) + "</font></b>&nbsp;");
                            if (__availableQty != __balanceQty) __html.Append(MyLib._myGlobal._resource("ยอดที่ขายได้") + ":&nbsp;<b><font color='blue'>" + MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __availableQty) + "</font></b>&nbsp;");
                            if (__unitType == 1)
                            {
                                // หลายหน่วยนับ
                                ///string __queryPacking = "select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_unit_use._code, String.Format(__queryUnit, _g.d.ic_unit_use._table + "." + _g.d.ic_unit_usหหหหหหหฟe._code) + " as " + _g.d.ic_unit_use._name_1, _g.d.ic_unit_use._row_order, _g.d.ic_unit_use._stand_value, _g.d.ic_unit_use._divide_value) + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._status + "=1 and " + _g.d.ic_unit_use._ic_code + "=\'" + __itemCode + "\' order by " + _g.d.ic_unit_use._ratio;
                                DataTable __dtPacking = ((DataSet)__getData[1]).Tables[0]; //__myFrameWork._queryShort(__queryPacking).Tables[0];
                                if (__dtPacking.Rows.Count > 0)
                                {
                                    StringBuilder __packingStr = new StringBuilder();
                                    for (int __loop = 0; __loop < __dtPacking.Rows.Count; __loop++)
                                    {
                                        string __unitCode = __dtPacking.Rows[__loop][_g.d.ic_unit_use._code].ToString();
                                        string __unitName = __dtPacking.Rows[__loop][_g.d.ic_unit_use._name_1].ToString();
                                        if (__packingStr.Length != 0)
                                        {
                                            __packingStr.Append(",");
                                        }
                                        if (__unitCode.Equals(__unitName))
                                        {
                                            __packingStr.Append(__unitCode);
                                        }
                                        else
                                        {
                                            __packingStr.Append(__unitName + "(" + __unitCode + ")");
                                        }
                                    }
                                    __html.Append(MyLib._myGlobal._resource("หน่วยนับ") + ":&nbsp;<b><font color='black'>" + __packingStr.ToString() + "</font></b>&nbsp;");
                                    //
                                    try
                                    {
                                        List<_g.g._convertPackingWordClass> __packing = new List<_g.g._convertPackingWordClass>();
                                        DataRow[] __selectPacking = __dtPacking.Select(_g.d.ic_unit_use._row_order + " > 0", _g.d.ic_unit_use._row_order + " desc");
                                        for (int __loop = 0; __loop < __selectPacking.Length; __loop++)
                                        {
                                            _g.g._convertPackingWordClass __newData = new _g.g._convertPackingWordClass();
                                            __newData._unitCode = __selectPacking[__loop][_g.d.ic_unit_use._code].ToString();
                                            __newData._unitName = __selectPacking[__loop][_g.d.ic_unit_use._name_1].ToString();
                                            __newData._standValue = MyLib._myGlobal._decimalPhase(__selectPacking[__loop][_g.d.ic_unit_use._stand_value].ToString());
                                            __newData._divideValue = MyLib._myGlobal._decimalPhase(__selectPacking[__loop][_g.d.ic_unit_use._divide_value].ToString());
                                            __packing.Add(__newData);
                                        }
                                        string __packingWord = _g.g._convertPackingWord(__packing, __balanceQtyByCost, true);
                                        if (__packingWord.Length > 0)
                                        {
                                            __html.Append(MyLib._myGlobal._resource("คงเหลือ") + ":&nbsp;<b><font color='black'>" + __packingWord + "</font></b>&nbsp;");
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    this._lastItemCodeInfo = "";
                    // clear
                    // แสดงรายละเอียดสินค้า
                    if (this._icBalance != null)
                    {
                        this._icBalance._clear();
                    }
                }
            }
            __html.Append("</body></html>");
            if (__display)
            {
                this._webBrowser.DocumentText = __html.ToString();
            }
        }

        void _icTransItemGrid__afterSelectRow(object sender, int row)

        {
            this._gridFindItemInfo();
            if (this._lotScreen != null)
            {
                int __itemCodeColumnNumber = this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._item_code);
                int __columnCostType = this._icTransItemGrid._findColumnByName(this._icTransItemGrid._columnCostType);
                if (__itemCodeColumnNumber != -1)
                {
                    int __costType = MyLib._myGlobal._intPhase(((_icTransItemGridControl)sender)._cellGet(row, __columnCostType).ToString());
                    if ((__costType == 1 && (
                        MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLAccount &&
                        MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLAccountProfessional &&
                        MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLAccountPOS && MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLAccountPOSProfessional)) ||

                    ((MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional) && __costType == 2))
                    {
                        this._lotScreen._load((_icTransItemGridControl)sender, row, DateTime.Now);
                    }
                }
            }
        }


        int _icTransScreenTop__vatCase(object sender)
        {
            // 0=ภาษีซื้อ,1=ภาษีขาย
            return (this._vatBuy != null) ? 0 : 1;
        }

        void _icTransItemGrid__reCalc()
        {
            this._calc(this._icTransItemGrid);
        }

        void _icTransScreenTop__textBoxChanged(object sender, string name)

        {
            switch (this._transControlType)
            {
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ:
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก:
                    if (name.Equals(_g.d.ic_trans._doc_ref))
                    {
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        string __getDocRef = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_ref).Trim().ToUpper();
                        if (this._lastDocRefNo.Equals(__getDocRef) == false)
                        {
                            this._lastDocRefNo = __getDocRef;
                            // ดึงรายละเอียดสินค้าจากใบเสนอซื้อ
                            int __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เสนอราคา);
                            string __queryHead = "select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans._doc_date, _g.d.ic_trans._cust_code, _g.d.ic_trans._department_code, _g.d.ic_trans._user_request, _g.d.ic_trans._vat_type, _g.d.ic_trans._inquiry_type) + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __getDocRef.ToUpper() + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag.ToString();
                            string __queryDetail = "select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._barcode, "coalesce((select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),\'\') as " + _g.d.ic_trans_detail._item_name, _g.d.ic_trans_detail._unit_code, "coalesce((select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code + "),'') as " + _g.d.ic_trans_detail._unit_name, _g.d.ic_trans_detail._qty, _g.d.ic_trans_detail._price, _g.d.ic_trans_detail._discount, _g.d.ic_trans_detail._sum_amount, _g.d.ic_trans_detail._due_date) + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __getDocRef.ToUpper() + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag.ToString() + " order by " + _g.d.ic_trans_detail._line_number;
                            //
                            StringBuilder __myquery = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryHead));
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryDetail));
                            __myquery.Append("</node>");
                            //
                            ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                            DataTable __header = ((DataSet)__getData[0]).Tables[0];
                            DataTable __details = ((DataSet)__getData[1]).Tables[0];
                            //
                            if (__header.Rows.Count > 0)
                            {
                                DataRow __dataRow = __header.Rows[0];
                                //
                                DateTime __docDate = MyLib._myGlobal._convertDateFromQuery(__dataRow[_g.d.ic_trans._doc_date].ToString());
                                string __custCode = __dataRow[_g.d.ic_trans._cust_code].ToString();
                                string __departmentCode = __dataRow[_g.d.ic_trans._department_code].ToString();
                                string __userRequest = __dataRow[_g.d.ic_trans._user_request].ToString();
                                int __vatType = MyLib._myGlobal._intPhase(__dataRow[_g.d.ic_trans._vat_type].ToString());
                                int __inquiryType = MyLib._myGlobal._intPhase(__dataRow[_g.d.ic_trans._inquiry_type].ToString());
                                //
                                this._icTransScreenTop._setDataDate(_g.d.ic_trans._doc_ref_date, __docDate);
                                this._icTransScreenTop._setDataStr(_g.d.ic_trans._cust_code, __custCode);
                                this._icTransScreenTop._setDataStr(_g.d.ic_trans._department_code, __departmentCode);
                                this._icTransScreenTop._setDataStr(_g.d.ic_trans._user_request, __userRequest);
                                this._icTransScreenTop._setComboBox(_g.d.ic_trans._vat_type, __vatType);
                                this._icTransScreenTop._setComboBox(_g.d.ic_trans._inquiry_type, __inquiryType);
                            }
                            //
                            this._icTransItemGrid._clear();
                            if (__details.Rows.Count > 0)
                            {
                                for (int __row = 0; __row < __details.Rows.Count; __row++)
                                {
                                    DataRow __dataRow = __details.Rows[__row];
                                    string __itemCode = __dataRow[_g.d.ic_trans_detail._item_code].ToString();
                                    string __barCode = __dataRow[_g.d.ic_trans_detail._barcode].ToString();
                                    string __itemName = __dataRow[_g.d.ic_trans_detail._item_name].ToString();
                                    string __unitCode = __dataRow[_g.d.ic_trans_detail._unit_code].ToString();
                                    decimal __qty = MyLib._myGlobal._decimalPhase(__dataRow[_g.d.ic_trans_detail._qty].ToString());
                                    decimal __price = MyLib._myGlobal._decimalPhase(__dataRow[_g.d.ic_trans_detail._price].ToString());
                                    string __discount = __dataRow[_g.d.ic_trans_detail._discount].ToString();
                                    decimal __amount = MyLib._myGlobal._decimalPhase(__dataRow[_g.d.ic_trans_detail._sum_amount].ToString());
                                    DateTime __dueDate = MyLib._myGlobal._convertDateFromQuery(__dataRow[_g.d.ic_trans_detail._due_date].ToString());

                                    //
                                    int __addr = this._icTransItemGrid._addRow();
                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, false);
                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._barcode, __barCode, false);
                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __itemName, false);
                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, false);
                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._due_date, __dueDate, false);
                                    //
                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._qty, __qty, false);
                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._price, __price, false);
                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._discount, __discount, false);
                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._sum_amount, __amount, false);
                                    // อ้างอิง
                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._doc_ref, __getDocRef, false);
                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._ref_line_number, __row, false);
                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._ref_row, __row, false);
                                }
                                this._calc(this._icTransItemGrid);
                            }
                        }
                    }
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ:
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                    if (name.Equals(_g.d.ic_trans._doc_ref))
                    {
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        string __getDocRef = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_ref).Trim().ToUpper();
                        if (this._lastDocRefNo.Equals(__getDocRef) == false)
                        {
                            this._lastDocRefNo = __getDocRef;
                            // ดึงรายละเอียดสินค้าจากใบเสนอซื้อ
                            int __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ);

                            string __extraWhereApprove = "";
                            if (this._transControlType == _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ)
                                __extraWhereApprove = " and " + _g.d.ic_trans._approve_status + "=0 ";


                            string __queryHead = "select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans._doc_date, _g.d.ic_trans._cust_code, _g.d.ic_trans._department_code, _g.d.ic_trans._user_request, _g.d.ic_trans._vat_type, _g.d.ic_trans._inquiry_type) + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __getDocRef.ToUpper() + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag.ToString() + __extraWhereApprove;
                            string __queryDetail = "select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._barcode, "coalesce((select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),\'\') as " + _g.d.ic_trans_detail._item_name, _g.d.ic_trans_detail._unit_code, "coalesce((select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code + "),'') as " + _g.d.ic_trans_detail._unit_name, _g.d.ic_trans_detail._qty, _g.d.ic_trans_detail._price, _g.d.ic_trans_detail._discount, _g.d.ic_trans_detail._sum_amount, _g.d.ic_trans_detail._due_date, _g.d.ic_trans_detail._vat_type, _g.d.ic_trans_detail._item_type, _g.d.ic_trans_detail._tax_type) + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __getDocRef.ToUpper() + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag.ToString() + " order by " + _g.d.ic_trans_detail._line_number;
                            //
                            StringBuilder __myquery = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryHead));
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryDetail));
                            __myquery.Append("</node>");
                            //
                            ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                            DataTable __header = ((DataSet)__getData[0]).Tables[0];
                            DataTable __details = ((DataSet)__getData[1]).Tables[0];
                            //
                            if (__header.Rows.Count > 0)
                            {
                                DataRow __dataRow = __header.Rows[0];
                                //
                                DateTime __docDate = MyLib._myGlobal._convertDateFromQuery(__dataRow[_g.d.ic_trans._doc_date].ToString());
                                string __custCode = __dataRow[_g.d.ic_trans._cust_code].ToString();
                                string __departmentCode = __dataRow[_g.d.ic_trans._department_code].ToString();
                                string __userRequest = __dataRow[_g.d.ic_trans._user_request].ToString();
                                int __vatType = MyLib._myGlobal._intPhase(__dataRow[_g.d.ic_trans._vat_type].ToString());
                                int __inquiryType = MyLib._myGlobal._intPhase(__dataRow[_g.d.ic_trans._inquiry_type].ToString());
                                //
                                this._icTransScreenTop._setDataDate(_g.d.ic_trans._doc_ref_date, __docDate);
                                this._icTransScreenTop._setDataStr(_g.d.ic_trans._cust_code, __custCode);
                                this._icTransScreenTop._setDataStr(_g.d.ic_trans._department_code, __departmentCode);
                                this._icTransScreenTop._setDataStr(_g.d.ic_trans._user_request, __userRequest);
                                this._icTransScreenTop._setComboBox(_g.d.ic_trans._vat_type, __vatType);
                                this._icTransScreenTop._setComboBox(_g.d.ic_trans._inquiry_type, __inquiryType);
                                //
                                this._icTransItemGrid._clear();
                                if (this._transControlType == _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ)
                                {
                                    if (__details.Rows.Count > 0)
                                    {
                                        for (int __row = 0; __row < __details.Rows.Count; __row++)
                                        {
                                            DataRow __dataDetailRow = __details.Rows[__row];
                                            string __itemCode = __dataDetailRow[_g.d.ic_trans_detail._item_code].ToString();
                                            string __barCode = __dataDetailRow[_g.d.ic_trans_detail._barcode].ToString();
                                            string __itemName = __dataDetailRow[_g.d.ic_trans_detail._item_name].ToString();
                                            string __unitCode = __dataDetailRow[_g.d.ic_trans_detail._unit_code].ToString();
                                            decimal __qty = MyLib._myGlobal._decimalPhase(__dataDetailRow[_g.d.ic_trans_detail._qty].ToString());
                                            decimal __price = MyLib._myGlobal._decimalPhase(__dataDetailRow[_g.d.ic_trans_detail._price].ToString());
                                            string __discount = __dataDetailRow[_g.d.ic_trans_detail._discount].ToString();
                                            decimal __amount = MyLib._myGlobal._decimalPhase(__dataDetailRow[_g.d.ic_trans_detail._sum_amount].ToString());
                                            DateTime __dueDate = MyLib._myGlobal._convertDateFromQuery(__dataDetailRow[_g.d.ic_trans_detail._due_date].ToString());

                                            int __vatItemType = MyLib._myGlobal._intPhase(__dataDetailRow[_g.d.ic_trans_detail._vat_type].ToString());
                                            int __taxType = MyLib._myGlobal._intPhase(__dataDetailRow[_g.d.ic_trans_detail._tax_type].ToString());
                                            int __itemType = MyLib._myGlobal._intPhase(__dataDetailRow[_g.d.ic_trans_detail._tax_type].ToString());

                                            //
                                            int __addr = this._icTransItemGrid._addRow();
                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, false);
                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._barcode, __barCode, false);
                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __itemName, false);
                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, false);
                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._due_date, __dueDate, false);
                                            //
                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._qty, __qty, false);
                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._price, __price, false);

                                            int __priceColumnNumber2 = this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._price_2);

                                            if (__priceColumnNumber2 != -1)
                                            {
                                                this._icTransItemGrid._cellUpdate(__addr, __priceColumnNumber2, __price, false);
                                            }

                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._discount, __discount, false);
                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._sum_amount, __amount, false);

                                            int __totalColumnNumber2 = this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._sum_amount_2);
                                            if (__totalColumnNumber2 != -1)
                                            {
                                                this._icTransItemGrid._cellUpdate(__addr, __totalColumnNumber2, __amount, false);
                                            }


                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_type, __itemType, false);
                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._vat_type, __vatItemType, false);
                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._tax_type, __taxType, false);
                                            // อ้างอิง
                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._doc_ref, __getDocRef, false);
                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._ref_line_number, __row, false);
                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._ref_row, __row, false);
                                        }
                                        this._calc(this._icTransItemGrid);
                                    }
                                }
                            }
                        }
                    }
                    break;
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                    if (name.Equals(_g.d.ic_trans._doc_ref))
                    {
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        string __getDocRef = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_ref).Trim().ToUpper();
                        if (this._lastDocRefNo.Equals(__getDocRef) == false)
                        {
                            this._lastDocRefNo = __getDocRef;
                            // ดึงรายละเอียดสินค้าจากใบเสนอซื้อ
                            int __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า);
                            string __queryHead = "select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans._doc_date, _g.d.ic_trans._cust_code, _g.d.ic_trans._department_code, _g.d.ic_trans._user_request, _g.d.ic_trans._vat_type, _g.d.ic_trans._inquiry_type) + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __getDocRef.ToUpper() + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag.ToString();
                            string __queryDetail = "select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._barcode, "coalesce((select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),\'\') as " + _g.d.ic_trans_detail._item_name, _g.d.ic_trans_detail._unit_code, "coalesce((select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code + "),'') as " + _g.d.ic_trans_detail._unit_name, _g.d.ic_trans_detail._qty, _g.d.ic_trans_detail._price, _g.d.ic_trans_detail._discount, _g.d.ic_trans_detail._sum_amount, _g.d.ic_trans_detail._due_date) + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __getDocRef.ToUpper() + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag.ToString() + " order by " + _g.d.ic_trans_detail._line_number;
                            //
                            StringBuilder __myquery = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryHead));
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryDetail));
                            __myquery.Append("</node>");
                            //
                            ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                            DataTable __header = ((DataSet)__getData[0]).Tables[0];
                            DataTable __details = ((DataSet)__getData[1]).Tables[0];
                            //
                            if (__header.Rows.Count > 0)
                            {
                                DataRow __dataRow = __header.Rows[0];
                                //
                                DateTime __docDate = MyLib._myGlobal._convertDateFromQuery(__dataRow[_g.d.ic_trans._doc_date].ToString());
                                string __custCode = __dataRow[_g.d.ic_trans._cust_code].ToString();
                                string __departmentCode = __dataRow[_g.d.ic_trans._department_code].ToString();
                                string __userRequest = __dataRow[_g.d.ic_trans._user_request].ToString();
                                int __vatType = MyLib._myGlobal._intPhase(__dataRow[_g.d.ic_trans._vat_type].ToString());
                                int __inquiryType = MyLib._myGlobal._intPhase(__dataRow[_g.d.ic_trans._inquiry_type].ToString());
                                //
                                this._icTransScreenTop._setDataDate(_g.d.ic_trans._doc_ref_date, __docDate);
                                this._icTransScreenTop._setDataStr(_g.d.ic_trans._cust_code, __custCode);
                                this._icTransScreenTop._setDataStr(_g.d.ic_trans._department_code, __departmentCode);
                                this._icTransScreenTop._setDataStr(_g.d.ic_trans._user_request, __userRequest);
                                this._icTransScreenTop._setComboBox(_g.d.ic_trans._vat_type, __vatType);
                                this._icTransScreenTop._setComboBox(_g.d.ic_trans._inquiry_type, __inquiryType);
                            }
                            //
                            this._icTransItemGrid._clear();
                            if (this._transControlType == _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ)
                            {
                                if (__details.Rows.Count > 0)
                                {
                                    for (int __row = 0; __row < __details.Rows.Count; __row++)
                                    {
                                        DataRow __dataRow = __details.Rows[__row];
                                        string __itemCode = __dataRow[_g.d.ic_trans_detail._item_code].ToString();
                                        string __barCode = __dataRow[_g.d.ic_trans_detail._barcode].ToString();
                                        string __itemName = __dataRow[_g.d.ic_trans_detail._item_name].ToString();
                                        string __unitCode = __dataRow[_g.d.ic_trans_detail._unit_code].ToString();
                                        decimal __qty = MyLib._myGlobal._decimalPhase(__dataRow[_g.d.ic_trans_detail._qty].ToString());
                                        decimal __price = MyLib._myGlobal._decimalPhase(__dataRow[_g.d.ic_trans_detail._price].ToString());
                                        string __discount = __dataRow[_g.d.ic_trans_detail._discount].ToString();
                                        decimal __amount = MyLib._myGlobal._decimalPhase(__dataRow[_g.d.ic_trans_detail._sum_amount].ToString());
                                        DateTime __dueDate = MyLib._myGlobal._convertDateFromQuery(__dataRow[_g.d.ic_trans_detail._due_date].ToString());
                                        //
                                        int __addr = this._icTransItemGrid._addRow();
                                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, false);
                                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._barcode, __barCode, false);
                                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __itemName, false);
                                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, false);
                                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._due_date, __dueDate, false);
                                        //
                                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._qty, __qty, false);
                                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._price, __price, false);
                                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._discount, __discount, false);
                                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._sum_amount, __amount, false);
                                        // อ้างอิง
                                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._doc_ref, __getDocRef, false);
                                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._ref_line_number, __row, false);
                                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._ref_row, __row, false);
                                    }
                                    this._calc(this._icTransItemGrid);
                                }
                            }
                        }
                    }
                    break;
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ:
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                    if (name.Equals(_g.d.ic_trans._doc_ref))
                    {
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        string __getDocRef = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_ref).Trim().ToUpper();
                        if (this._lastDocRefNo.Equals(__getDocRef) == false)
                        {
                            this._lastDocRefNo = __getDocRef;
                            // ดึงรายละเอียดสินค้าจากใบเสนอซื้อ
                            int __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย);
                            string __queryHead = "select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans._doc_date, _g.d.ic_trans._cust_code, _g.d.ic_trans._department_code, _g.d.ic_trans._user_request, _g.d.ic_trans._vat_type, _g.d.ic_trans._inquiry_type) + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __getDocRef.ToUpper() + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag.ToString();
                            string __queryDetail = "select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._barcode, "coalesce((select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),\'\') as " + _g.d.ic_trans_detail._item_name, _g.d.ic_trans_detail._unit_code, "coalesce((select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code + "),'') as " + _g.d.ic_trans_detail._unit_name, _g.d.ic_trans_detail._qty, _g.d.ic_trans_detail._price, _g.d.ic_trans_detail._discount, _g.d.ic_trans_detail._sum_amount, _g.d.ic_trans_detail._due_date) + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __getDocRef.ToUpper() + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag.ToString() + " order by " + _g.d.ic_trans_detail._line_number;
                            //
                            StringBuilder __myquery = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryHead));
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryDetail));
                            __myquery.Append("</node>");
                            //
                            ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                            DataTable __header = ((DataSet)__getData[0]).Tables[0];
                            DataTable __details = ((DataSet)__getData[1]).Tables[0];
                            //
                            if (__header.Rows.Count > 0)
                            {
                                DataRow __dataRow = __header.Rows[0];
                                //
                                DateTime __docDate = MyLib._myGlobal._convertDateFromQuery(__dataRow[_g.d.ic_trans._doc_date].ToString());
                                string __custCode = __dataRow[_g.d.ic_trans._cust_code].ToString();
                                string __departmentCode = __dataRow[_g.d.ic_trans._department_code].ToString();
                                string __userRequest = __dataRow[_g.d.ic_trans._user_request].ToString();
                                int __vatType = MyLib._myGlobal._intPhase(__dataRow[_g.d.ic_trans._vat_type].ToString());
                                int __inquiryType = MyLib._myGlobal._intPhase(__dataRow[_g.d.ic_trans._inquiry_type].ToString());
                                //
                                this._icTransScreenTop._setDataDate(_g.d.ic_trans._doc_ref_date, __docDate);
                                this._icTransScreenTop._setDataStr(_g.d.ic_trans._cust_code, __custCode);
                                this._icTransScreenTop._setDataStr(_g.d.ic_trans._department_code, __departmentCode);
                                this._icTransScreenTop._setDataStr(_g.d.ic_trans._user_request, __userRequest);
                                this._icTransScreenTop._setComboBox(_g.d.ic_trans._vat_type, __vatType);
                                this._icTransScreenTop._setComboBox(_g.d.ic_trans._inquiry_type, __inquiryType);
                            }
                            //
                            this._icTransItemGrid._clear();
                            if (this._transControlType == _g.g._transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ)
                            {
                                if (__details.Rows.Count > 0)
                                {
                                    for (int __row = 0; __row < __details.Rows.Count; __row++)
                                    {
                                        DataRow __dataRow = __details.Rows[__row];
                                        string __itemCode = __dataRow[_g.d.ic_trans_detail._item_code].ToString();
                                        string __barCode = __dataRow[_g.d.ic_trans_detail._barcode].ToString();
                                        string __itemName = __dataRow[_g.d.ic_trans_detail._item_name].ToString();
                                        string __unitCode = __dataRow[_g.d.ic_trans_detail._unit_code].ToString();
                                        decimal __qty = MyLib._myGlobal._decimalPhase(__dataRow[_g.d.ic_trans_detail._qty].ToString());
                                        decimal __price = MyLib._myGlobal._decimalPhase(__dataRow[_g.d.ic_trans_detail._price].ToString());
                                        string __discount = __dataRow[_g.d.ic_trans_detail._discount].ToString();
                                        decimal __amount = MyLib._myGlobal._decimalPhase(__dataRow[_g.d.ic_trans_detail._sum_amount].ToString());
                                        DateTime __dueDate = MyLib._myGlobal._convertDateFromQuery(__dataRow[_g.d.ic_trans_detail._due_date].ToString());
                                        //
                                        int __addr = this._icTransItemGrid._addRow();
                                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, false);
                                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._barcode, __barCode, false);
                                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __itemName, false);
                                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, false);
                                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._due_date, __dueDate, false);
                                        //
                                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._qty, __qty, false);
                                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._price, __price, false);
                                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._discount, __discount, false);
                                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._sum_amount, __amount, false);
                                        // อ้างอิง
                                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._doc_ref, __getDocRef, false);
                                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._ref_line_number, __row, false);
                                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._ref_row, __row, false);
                                    }
                                    this._calc(this._icTransItemGrid);
                                }
                            }
                        }
                    }
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ:
                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก:
                    if (name.Equals(_g.d.ic_trans._doc_ref))
                    {
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        string __getDocRef = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_ref).Trim().ToUpper();
                        if (this._lastDocRefNo.Equals(__getDocRef) == false)
                        {
                            this._lastDocRefNo = __getDocRef;
                            // ดึงรายละเอียดสินค้าจากใบเสนอซื้อ
                            int __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ);
                            string __queryHead = "select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans._cust_code, _g.d.ic_trans._department_code, _g.d.ic_trans._user_request, _g.d.ic_trans._on_hold, _g.d.ic_trans._vat_type, _g.d.ic_trans._inquiry_type) + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __getDocRef.ToUpper() + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag.ToString();
                            string __queryDetail = "select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._barcode, "coalesce((select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),\'\') as " + _g.d.ic_trans_detail._item_name, _g.d.ic_trans_detail._unit_code, "coalesce((select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code + "),'') as " + _g.d.ic_trans_detail._unit_name, _g.d.ic_trans_detail._qty, _g.d.ic_trans_detail._price, _g.d.ic_trans_detail._discount, _g.d.ic_trans_detail._sum_amount, _g.d.ic_trans_detail._due_date) + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __getDocRef.ToUpper() + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag.ToString() + " order by " + _g.d.ic_trans_detail._line_number;
                            //
                            StringBuilder __myquery = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryHead));
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryDetail));
                            __myquery.Append("</node>");
                            //
                            ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                            DataTable __header = ((DataSet)__getData[0]).Tables[0];
                            DataTable __details = ((DataSet)__getData[1]).Tables[0];
                            //
                            if (__header.Rows.Count > 0)
                            {
                                DataRow __dataRow = __header.Rows[0];
                                //
                                string __custCode = __dataRow[_g.d.ic_trans._cust_code].ToString();
                                string __departmentCode = __dataRow[_g.d.ic_trans._department_code].ToString();
                                string __userRequest = __dataRow[_g.d.ic_trans._user_request].ToString();
                                int __vatType = MyLib._myGlobal._intPhase(__dataRow[_g.d.ic_trans._vat_type].ToString());
                                int __inquiryType = MyLib._myGlobal._intPhase(__dataRow[_g.d.ic_trans._inquiry_type].ToString());
                                int __onHold = MyLib._myGlobal._intPhase(__dataRow[_g.d.ic_trans._on_hold].ToString());
                                if (__onHold == 1)
                                {
                                    MessageBox.Show(MyLib._myGlobal._resource("เอกสารระงับไว้ ยังไม่สามารถใช้ได้"));
                                    this._icTransScreenTop._setDataStr(_g.d.ic_trans._doc_ref, "");
                                }
                                else
                                {
                                    this._icTransScreenTop._setDataStr(_g.d.ic_trans._cust_code, __custCode);
                                    this._icTransScreenTop._setDataStr(_g.d.ic_trans._department_code, __departmentCode);
                                    this._icTransScreenTop._setDataStr(_g.d.ic_trans._user_request, __userRequest);
                                    this._icTransScreenTop._setComboBox(_g.d.ic_trans._vat_type, __vatType);
                                    this._icTransScreenTop._setComboBox(_g.d.ic_trans._inquiry_type, __inquiryType);
                                    //
                                    this._icTransItemGrid._clear();
                                    if (__details.Rows.Count > 0)
                                    {
                                        for (int __row = 0; __row < __details.Rows.Count; __row++)
                                        {
                                            DataRow __dataDetailRow = __details.Rows[__row];
                                            string __itemCode = __dataDetailRow[_g.d.ic_trans_detail._item_code].ToString();
                                            string __barCode = __dataDetailRow[_g.d.ic_trans_detail._barcode].ToString();
                                            string __itemName = __dataDetailRow[_g.d.ic_trans_detail._item_name].ToString();
                                            string __unitCode = __dataDetailRow[_g.d.ic_trans_detail._unit_code].ToString();
                                            decimal __qty = MyLib._myGlobal._decimalPhase(__dataDetailRow[_g.d.ic_trans_detail._qty].ToString());
                                            decimal __price = MyLib._myGlobal._decimalPhase(__dataDetailRow[_g.d.ic_trans_detail._price].ToString());
                                            string __discount = __dataDetailRow[_g.d.ic_trans_detail._discount].ToString();
                                            decimal __amount = MyLib._myGlobal._decimalPhase(__dataDetailRow[_g.d.ic_trans_detail._sum_amount].ToString());
                                            DateTime __dueDate = MyLib._myGlobal._convertDateFromQuery(__dataDetailRow[_g.d.ic_trans_detail._due_date].ToString());
                                            //
                                            int __addr = this._icTransItemGrid._addRow();
                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, true);
                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._barcode, __barCode, false);
                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __itemName, false);
                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, false);
                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._temp_float_1, __qty, false);
                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._temp_float_2, __price, false);
                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._temp_string_1, __discount, false);
                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._due_date, __dueDate, false);
                                            //
                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._qty, __qty, false);
                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._price, __price, false);
                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._discount, __discount, false);
                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._sum_amount, __amount, false);
                                            // อ้างอิง
                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._ref_doc_no, __getDocRef, false);
                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._ref_line_number, __row, false);
                                            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._ref_row, __row, false);
                                        }
                                        this._calc(this._icTransItemGrid);
                                    }
                                }
                            }
                        }
                    }
                    break;

            }

            //toe ส่วนลดและภาษี ตามลูกค้า
            if (name.Equals(_g.d.ic_trans._ar_code) || name.Equals(_g.d.ic_trans._ap_code) || name.Equals(_g.d.ic_trans._cust_code))
            {
                // toe update vatbuy && vatsale
                /*if (this._vatBuy != null)
                {
                    this._vatBuy._checkApDetail(this._myManageTrans._mode);
                }

                if (this._vatSale != null)
                {
                    this._vatSale._checkArDetail(this._myManageTrans._mode);

                }*/
                if ( name.Equals(_g.d.ic_trans._ar_code))
                {
                    switch (this._transControlType)
                    {
                        case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                            {
                                if (_g.g._companyProfile._change_customer_not_clear_detail)
                                {
                                    this._icTransRef._transGrid._clear();
                                }
                                else
                                {
                                    this._icTransRef._transGrid._clear();
                                    this._icTransItemGrid._clear();
                                }
                            }
                            break;
                    }
                }

                switch (this._transControlType)
                {
                    case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                    case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                    case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                    case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                    case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                        {
                            //toe 
                            if (name.Equals(_g.d.ic_trans._ar_code) || name.Equals(_g.d.ic_trans._cust_code))
                            {

                                // check default sale 
                                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                                string __cust_code = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
                                if (__cust_code.Length > 0)
                                {
                                    DataTable __getDefaultSale = __myFrameWork._queryShort("select " + _g.d.ar_customer_detail._set_tax_type + "," + _g.d.ar_customer_detail._tax_type + "," + _g.d.ar_customer_detail._tax_rate + "," + _g.d.ar_customer_detail._sale_code + "," + _g.d.ar_customer_detail._discount_bill + "," + _g.d.ar_customer_detail._discount_item + ", (select " + _g.d.ar_customer._remark + " from ar_customer where ar_customer.code = ar_customer_detail.ar_code ) as " + _g.d.ar_customer._remark + ", (select " + _g.d.ar_customer._status + " from ar_customer where ar_customer.code = ar_customer_detail.ar_code ) as " + _g.d.ar_customer._status + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._ar_code + "=\'" + __cust_code + "\'").Tables[0];
                                    if (__getDefaultSale.Rows.Count > 0)
                                    {
                                        // พนักงานขาย
                                        string __saleCode = __getDefaultSale.Rows[0][_g.d.ar_customer_detail._sale_code].ToString();
                                        if (__saleCode.Length > 0)
                                        {
                                            this._icTransScreenTop._setDataStr(_g.d.ic_trans._sale_code, __saleCode, "", true);
                                            this._icTransScreenTop._search(true);
                                        }

                                        // ส่วนลดท้ายบิล 
                                        if (MyLib._myGlobal._OEMVersion.Equals("SINGHA") == false || (this._transControlType != _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้ && this._transControlType != _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้))
                                        {
                                            string __discountBill = __getDefaultSale.Rows[0][_g.d.ar_customer_detail._discount_bill].ToString();
                                            if (__discountBill.Length > 0)
                                                this._icTransScreenBottom._setDataStr(_g.d.ic_trans._discount_word, __discountBill);

                                        }
                                        // กรณีระบุภาษี
                                        int __setTaxType = MyLib._myGlobal._intPhase(__getDefaultSale.Rows[0][_g.d.ar_customer_detail._set_tax_type].ToString());
                                        if (__setTaxType == 1)
                                        {
                                            int __taxType = MyLib._myGlobal._intPhase(__getDefaultSale.Rows[0][_g.d.ar_customer_detail._tax_type].ToString());
                                            this._icTransScreenTop._setComboBox(_g.d.ic_trans._vat_type, __taxType);

                                            // อัตราภาษี
                                            decimal __taxRate = MyLib._myGlobal._decimalPhase(__getDefaultSale.Rows[0][_g.d.ar_customer_detail._tax_rate].ToString());
                                            if (__taxRate != 0)
                                            {
                                                this._icTransScreenBottom._setDataNumber(_g.d.ic_trans._vat_rate, __taxRate);
                                            }
                                        }

                                        if (MyLib._myGlobal._OEMVersion == "imex" || MyLib._myGlobal._OEMVersion == "ims")
                                        {
                                            this._icTransScreenBottom._setDataStr(_g.d.ic_trans._remark, __getDefaultSale.Rows[0][_g.d.ar_customer._remark].ToString());
                                        }



                                    }

                                    if ((this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ || this._transControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้) && this._shipmentControl != null)
                                    {
                                        DataTable __getShipmentData = __myFrameWork._queryShort("select " + _g.d.ar_customer._address + "," + _g.d.ar_customer._telephone + "," + _g.d.ar_customer._fax + "," + _g.d.ar_customer._name_1 +
                                            ", (select " + _g.d.ap_ar_transport_label._name_1 + " from " + _g.d.ap_ar_transport_label._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.ap_ar_transport_label._table + "." + _g.d.ap_ar_transport_label._cust_code + "  order by " + _g.d.ap_ar_transport_label._name_1 + " limit 1) as shipment_name " +
                                            ", (select " + _g.d.ap_ar_transport_label._ship_code + " from " + _g.d.ap_ar_transport_label._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.ap_ar_transport_label._table + "." + _g.d.ap_ar_transport_label._cust_code + "  order by " + _g.d.ap_ar_transport_label._name_1 + " limit 1) as " + _g.d.ap_ar_transport_label._ship_code +
                                             ", (select " + _g.d.ap_ar_transport_label._address + " from " + _g.d.ap_ar_transport_label._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.ap_ar_transport_label._table + "." + _g.d.ap_ar_transport_label._cust_code + "  order by " + _g.d.ap_ar_transport_label._name_1 + " limit 1) as address2 " +
                                             ", (select " + _g.d.ap_ar_transport_label._telephone + " from " + _g.d.ap_ar_transport_label._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.ap_ar_transport_label._table + "." + _g.d.ap_ar_transport_label._cust_code + "  order by " + _g.d.ap_ar_transport_label._name_1 + " limit 1) as telephone2 " +
                                             ", (select " + _g.d.ar_customer_detail._logistic_area + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "  order by " + _g.d.ap_ar_transport_label._name_1 + " ) as " + _g.d.ar_customer_detail._logistic_area +
                                             ", (select " + _g.d.ap_ar_transport_label._logistic_area + " from " + _g.d.ap_ar_transport_label._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.ap_ar_transport_label._table + "." + _g.d.ap_ar_transport_label._cust_code + "  order by " + _g.d.ap_ar_transport_label._name_1 + " limit 1) as logistic_area2 " +
                                             " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + " =  \'" + __cust_code + "\' ").Tables[0];
                                        if (__getShipmentData.Rows.Count > 0)
                                        {
                                            // update shipment detail
                                            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                                            {
                                                this._shipmentControl._shipmentScreen._setDataStr(_g.d.ic_trans_shipment._transport_name, ((__getShipmentData.Rows[0]["shipment_name"].ToString().Length > 0) ? __getShipmentData.Rows[0]["shipment_name"].ToString() : __getShipmentData.Rows[0][_g.d.ar_customer._name_1].ToString()));
                                                this._shipmentControl._shipmentScreen._setDataStr(_g.d.ic_trans_shipment._transport_telephone, __getShipmentData.Rows[0]["telephone2"].ToString());

                                            }
                                            else
                                            {
                                                this._shipmentControl._shipmentScreen._setDataStr(_g.d.ic_trans_shipment._transport_name, __getShipmentData.Rows[0][_g.d.ar_customer._name_1].ToString());
                                                this._shipmentControl._shipmentScreen._setDataStr(_g.d.ic_trans_shipment._transport_telephone, __getShipmentData.Rows[0][_g.d.ar_customer._telephone].ToString());
                                            }

                                            if (MyLib._myGlobal._OEMVersion == "imex" || MyLib._myGlobal._OEMVersion == "ims")
                                            {
                                                this._shipmentControl._shipmentScreen._setDataStr(_g.d.ic_trans_shipment._transport_address, __getShipmentData.Rows[0][_g.d.ar_customer._address].ToString().Replace("\n", "\r\n"));
                                            }
                                            else
                                            {
                                                this._shipmentControl._shipmentScreen._setDataStr(_g.d.ic_trans_shipment._transport_address, ((__getShipmentData.Rows[0]["address2"].ToString().Length > 0) ? __getShipmentData.Rows[0]["address2"].ToString().Replace("\n", "\r\n") : __getShipmentData.Rows[0][_g.d.ar_customer._address].ToString().Replace("\n", "\r\n")));
                                            }
                                            this._shipmentControl._shipmentScreen._setDataStr(_g.d.ic_trans_shipment._transport_fax, __getShipmentData.Rows[0][_g.d.ar_customer._fax].ToString());
                                            this._shipmentControl._shipmentScreen._setDataStr(_g.d.ic_trans_shipment._ship_code, __getShipmentData.Rows[0][_g.d.ap_ar_transport_label._ship_code].ToString());
                                            this._shipmentControl._shipmentScreen._setDataStr(_g.d.ic_trans_shipment._logistic_area, ((__getShipmentData.Rows[0]["logistic_area2"].ToString().Length > 0) ? __getShipmentData.Rows[0]["logistic_area2"].ToString() : __getShipmentData.Rows[0][_g.d.ar_customer_detail._logistic_area].ToString().Replace("\n", "\r\n")));
                                            //this._shipmentControl._shipmentScreen._setDataStr(_g.d.ap_ar_transport_label._fax, __getShipmentData.Rows[0][_g.d.ar_customer._fax].ToString());
                                        }

                                    }
                                }

                                if (this._arInfoForm != null)
                                    _arStatusInfo();
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                    case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                        {
                            if (name.Equals(_g.d.ic_trans._ap_code) || name.Equals(_g.d.ic_trans._cust_code))
                            {
                                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                                string __cust_code = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
                                if (__cust_code.Length > 0)
                                {
                                    DataTable __getDefaultSale = __myFrameWork._queryShort("select " + _g.d.ap_supplier_detail._set_tax_type + "," + _g.d.ap_supplier_detail._tax_type + "," + _g.d.ap_supplier_detail._tax_rate + "," + _g.d.ap_supplier_detail._discount_bill + "," + _g.d.ap_supplier_detail._discount_item + ", (select " + _g.d.ap_supplier._remark + " from ap_supplier where ap_supplier.code = ap_supplier_detail.ap_code ) as " + _g.d.ap_supplier._remark + " from " + _g.d.ap_supplier_detail._table + " where " + _g.d.ap_supplier_detail._ap_code + "=\'" + __cust_code + "\'").Tables[0];

                                    // ส่วนลดท้ายบิล
                                    if (__getDefaultSale.Rows.Count > 0)
                                    {
                                        string __discountBill = __getDefaultSale.Rows[0][_g.d.ap_supplier_detail._discount_bill].ToString();
                                        if (__discountBill.Length > 0)
                                            this._icTransScreenBottom._setDataStr(_g.d.ic_trans._discount_word, __discountBill);

                                        // กรณีระบุภาษี
                                        int __setTaxType = MyLib._myGlobal._intPhase(__getDefaultSale.Rows[0][_g.d.ap_supplier_detail._set_tax_type].ToString());
                                        if (__setTaxType == 1)
                                        {
                                            int __taxType = MyLib._myGlobal._intPhase(__getDefaultSale.Rows[0][_g.d.ap_supplier_detail._tax_type].ToString());
                                            this._icTransScreenTop._setComboBox(_g.d.ic_trans._vat_type, __taxType);

                                            // อัตราภาษี
                                            decimal __taxRate = MyLib._myGlobal._decimalPhase(__getDefaultSale.Rows[0][_g.d.ap_supplier_detail._tax_rate].ToString());
                                            if (__taxRate != 0)
                                            {
                                                this._icTransScreenBottom._setDataNumber(_g.d.ic_trans._vat_rate, __taxRate);
                                            }
                                        }

                                        if (MyLib._myGlobal._OEMVersion == "imex" || MyLib._myGlobal._OEMVersion == "ims")
                                        {
                                            this._icTransScreenBottom._setDataStr(_g.d.ic_trans._remark, __getDefaultSale.Rows[0][_g.d.ap_supplier._remark].ToString());
                                        }
                                    }
                                }
                            }
                        }
                        break;
                }


                // singha check status ar_customer
                if (MyLib._myGlobal._OEMVersion == ("SINGHA") && (name.Equals(_g.d.ic_trans._ar_code) || name.Equals(_g.d.ic_trans._cust_code))
                        && (
                        this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ ||
                        this._transControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้ ||
                        this._transControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้ ||
                        this._transControlType == _g.g._transControlTypeEnum.ขาย_เสนอราคา ||
                        this._transControlType == _g.g._transControlTypeEnum.ขาย_สั่งขาย ||
                        this._transControlType == _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า))
                {
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    string __cust_code = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
                    DataTable __custStatusDataTable = __myFrameWork._queryShort("select " + _g.d.ar_customer._status + "," + _g.d.ar_customer._ar_branch_code + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + __cust_code + "\'").Tables[0];
                    if (__custStatusDataTable.Rows.Count > 0)
                    {
                        int __arStatus = MyLib._myGlobal._intPhase(__custStatusDataTable.Rows[0][_g.d.ar_customer._status].ToString());

                        if (__arStatus == 1)
                        {
                            MessageBox.Show("สถานะ ลูกค้าปิดการใช้งาน");
                            this._icTransScreenTop._setDataStr(name, "", "", true);
                        }

                        if (_g.g._companyProfile._customer_by_branch && _g.g._companyProfile._change_branch_code == false)
                        {
                            string __arBranch = __custStatusDataTable.Rows[0][_g.d.ar_customer._ar_branch_code].ToString();
                            if (_g.g._companyProfile._branch_code.Equals(__arBranch) == false)
                            {
                                MessageBox.Show("ไม่อนุญาติลูกค้าต่างสาขา");
                                this._icTransScreenTop._setDataStr(name, "", "", true);
                            }

                        }
                    }

                }
            }

            this._glScreenCheck();
            if (this._lotScreen != null)
            {
                try
                {
                    int __itemCodeColumnNumber = this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._item_code);
                    if (__itemCodeColumnNumber != -1)
                    {
                        this._lotScreen._load((_icTransItemGridControl)sender, ((_icTransItemGridControl)sender)._selectRow, DateTime.Now);
                    }
                }
                catch
                {
                }
            }
        }


        decimal _icTransItemGrid__vatRate()
        {
            return this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._vat_rate);
        }

        string _icTransItemGrid__getDocDate(object sender)
        {
            return this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_date);
        }

        _g.g._vatTypeEnum _icTransItemGrid__vatType(object sender)
        {
            return this._vatType;
        }

        void _checkEnable()
        {
            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore && this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ && this._myManageTrans._mode == 1)
            {
                Control __docDate = this._icTransScreenTop._getControl(_g.d.ic_trans._doc_date);
                if (__docDate != null)
                {
                    __docDate.Enabled = true;
                }
                //
                Control __custCode = this._icTransScreenTop._getControl(_g.d.ic_trans._cust_code);
                if (__custCode != null)
                {
                    __custCode.Enabled = true;
                }
            }

            //
            if (this._icTransScreenTop._getControl(_g.d.ic_trans._inquiry_type) != null)
            {
                try
                {
                    int __getData = MyLib._myGlobal._intPhase(this._icTransScreenTop._getDataStr(_g.d.ic_trans._inquiry_type));
                    if (this._payControl != null)
                    {
                        switch (this._transControlType)
                        {
                            case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                            case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                                this._payControl.Enabled = (__getData == 2 || __getData == 3) ? true : false;
                                break;
                            default:
                                this._payControl.Enabled = (__getData == 1 || __getData == 3) ? true : false;
                                break;
                        }
                    }
                    if (this._withHoldingTax != null)
                    {
                        switch (this._transControlType)
                        {
                            case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                            case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                                this._withHoldingTax.Enabled = (__getData == 2 || __getData == 3) ? true : false;
                                break;
                            default:
                                this._withHoldingTax.Enabled = (__getData == 1 || __getData == 3) ? true : false;
                                break;
                        }
                    }
                }
                catch
                {
                }
            }

            if (_g.g._companyProfile._auto_insert_time == true)
            {
                switch (this._transControlType)
                {
                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                    case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                    case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                    case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:

                        if (this._myManageTrans._mode == 1)
                        {
                            this._icTransScreenTop._enabedControl(_g.d.ic_trans._doc_time, false);
                        }
                        else
                        {
                            this._icTransScreenTop._enabedControl(_g.d.ic_trans._doc_time, true);
                        }
                        break;
                }
            }
            if ((this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ || this._transControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้ || this._transControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้)
                && _g.g._companyProfile._doc_sale_tax_number_fixed)
            {
                if (this._myManageTrans._mode == 1)
                {
                    this._icTransScreenTop._enabedControl(_g.d.ic_trans._tax_doc_date, false);
                    this._icTransScreenTop._enabedControl(_g.d.ic_trans._tax_doc_no, false);
                }
                else
                {
                    this._icTransScreenTop._enabedControl(_g.d.ic_trans._tax_doc_date, true);
                    this._icTransScreenTop._enabedControl(_g.d.ic_trans._tax_doc_no, true);
                }
            }

            if ((this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ ||
                this._transControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้ ||
                this._transControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้) && _g.g._companyProfile._lock_change_customer && this._myManageTrans._mode == 2)
            {
                this._icTransScreenTop._enabedControl(_g.d.ic_trans._cust_code, false);
            }

            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA") && MyLib._myGlobal._isUserResetPrintLog)
            {
                this._myManageTrans._dataList._clearLogPrintButton.Enabled = false;
            }
        }

        void _icTransScreenTop__comboBoxSelectIndexChanged(object sender, string name)

        {
            if (name.Equals(_g.d.ic_trans._inquiry_type))
            {
                this._checkEnable();
            }
        }

        void _icTransScreenTop__textBoxSaved(object sender, string name)
        {
            if (this._withHoldingTax != null)
            {
                this._withHoldingTax._custCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
                this._withHoldingTax._docDate = MyLib._myGlobal._convertDate(this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_date));
            }
            this._icTransItemGrid._custCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
            if (this._icTransRef != null)
            {
                this._icTransRef._docNo = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_no);
                this._icTransRef._custCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
            }
            this._checkEnable();
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

            if (name.Equals(_g.d.ic_trans._doc_date))
            {
                string __docDate = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_date).ToString();
                this._setDefaultDate();
            }

            if ((MyLib._myGlobal._OEMVersion.Equals("SINGHA") || _g.g._companyProfile._branchStatus == 1) && this._transControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก)
            {

                if (name.Equals(_g.d.ic_trans._wh_to))
                {
                    string __getWHTo = this._icTransScreenTop._getDataStr(_g.d.ic_trans._wh_to);
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                    DataTable __getBranchTable = __myFrameWork._queryShort("select branch_code from ic_warehouse where code = \'" + __getWHTo + "\' ").Tables[0];
                    if (__getBranchTable.Rows.Count > 0 && __getBranchTable.Rows[0][0].ToString().Length > 0)
                    {
                        this._icTransScreenMore._setDataStr(_g.d.ic_trans._branch_code_to, __getBranchTable.Rows[0][0].ToString());
                    }

                }
                else if (name.Equals(_g.d.ic_trans._wh_from))
                {
                    string __getWHTo = this._icTransScreenTop._getDataStr(_g.d.ic_trans._wh_from);
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                    DataTable __getBranchTable = __myFrameWork._queryShort("select branch_code from ic_warehouse where code = \'" + __getWHTo + "\' ").Tables[0];
                    if (__getBranchTable.Rows.Count > 0 && __getBranchTable.Rows[0][0].ToString().Length > 0)
                    {
                        this._icTransScreenMore._setDataStr(_g.d.ic_trans._branch_code, __getBranchTable.Rows[0][0].ToString());
                    }

                }

            }

            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA") && _g.g._companyProfile._tax_from_invoice &&
               (this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ ||
               this._transControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้ ||
               this._transControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้
               ) &&
               (name.Equals(_g.d.ic_trans._doc_no) || name.Equals(_g.d.ic_trans._doc_date)))
            {
                DateTime __docDate = this._icTransScreenTop._getDataDate(_g.d.ic_trans._doc_date);
                string __docNo = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_no);

                if (__docNo.Length > 0)
                {
                    this._icTransScreenTop._setDataStr(_g.d.ic_trans._tax_doc_no, __docNo, "", true);
                    this._icTransScreenTop._setDataDate(_g.d.ic_trans._tax_doc_date, __docDate);
                }

            }
        }

        void _setDefaultDate()
        {
            DateTime __getDate = MyLib._myGlobal._convertDate(this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_date));
            if (this._payControl != null)
            {
                this._payControl._defaultDate = __getDate;
            }
        }

        void _ictransItemGrid__alterCellUpdate(object sender, int row, int column)

        {
            this._calc(sender);
            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore &&
                this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ &&
                this._myManageTrans._mode == 1)
            {
                Boolean __enable = true;
                if (this._icTransItemGrid._rowData.Count > 0)
                {
                    __enable = false;
                }
                //
                Control __docDate = this._icTransScreenTop._getControl(_g.d.ic_trans._doc_date);
                if (__docDate != null)
                {
                    __docDate.Enabled = __enable;
                }
                //
                Control __custCode = this._icTransScreenTop._getControl(_g.d.ic_trans._cust_code);
                if (__custCode != null)
                {
                    __custCode.Enabled = __enable;
                }
            }
            this._gridFindItemInfo();
        }

        void _ictransScreenTop__changeCreditDay(int creditDay)
        {
            this._icTransScreenBottom._setDataNumber(_g.d.ic_trans._credit_day, (decimal)creditDay);
            this._icTransScreenBottom._ictransScreenBottomControl__textBoxChanged(null, _g.d.ic_trans._credit_day);
        }

        bool _myManageTrans__checkEditData(int row, MyLib._myGrid sender)
        {
            return _myManageTrans__checkEditData(row, sender, true);
        }
        bool _myManageTrans__checkEditData(int row, MyLib._myGrid sender, bool checkCancel)
        {
            if (MyLib._myGlobal._isUserTest)
            {
                // return true;
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
            int __usedStatusColumn = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._used_status);
            int __usedStatusColumn2 = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._used_status_2);
            int __docSuccessColumn = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_success);
            int __lastStatusColumn = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._last_status);

            if (this._transControlType == _g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก || this._transControlType == _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก || this._transControlType == _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก)
            {
                __lastStatusColumn = sender._findColumnByName(_g.d.ic_wms_trans._table + "." + _g.d.ic_wms_trans._last_status);
                __usedStatusColumn = sender._findColumnByName(_g.d.ic_wms_trans._table + "." + _g.d.ic_wms_trans._used_status);
                __docSuccessColumn = sender._findColumnByName(_g.d.ic_wms_trans._table + "." + _g.d.ic_wms_trans._doc_success);
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

            if (__result == false && this._transControlType == _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ
                && (MyLib._myGlobal._OEMVersion.Equals("ims") || MyLib._myGlobal._OEMVersion.Equals("imex")))
            {
                __result = true;//  = (MessageBox.Show("รายการนี้มีการอ้างอิงไปแล้ว ต้องการแก้ไขหรือไม่", "อ้างอิงไปแล้ว", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes) ? true : false;
            }

            // check is lock record

            if (__result == true)
            {
                int __columnDocNo = sender._findColumnByName(this._icTransTable + "." + _g.d.ic_trans._doc_no);
                if (__columnDocNo != -1)
                {
                    string __docNo = sender._cellGet(row, __columnDocNo).ToString();

                    string __query = "select is_lock_record from " + this._icTransTable + " where doc_no = \'" + __docNo + "\' and trans_flag =" + this._getTransFlag;
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

        void _dataList__flowEvent(MyLib._myGrid grid)
        {
            _flowDisplay(grid);
        }

        void _flowDisplay(MyLib._myGrid grid)
        {
            try
            {
                int __docColumn = grid._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);

                if (this._transControlType == _g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก ||
                    this._transControlType == _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก ||
                    this._transControlType == _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก)
                {
                    __docColumn = grid._findColumnByName(_g.d.ic_wms_trans._table + "." + _g.d.ic_wms_trans._doc_no);
                }
                if (__docColumn != -1 && grid._selectRow != -1)
                {
                    string __docNo = grid._cellGet(grid._selectRow, __docColumn).ToString();
                    /*_docFlowForm __docFlow = new _docFlowForm(this._transControlType, _g.d.ic_trans._doc_no, "", __docNo);
                    __docFlow.Show();*/
                    SMLProcess._docFlowForm __docFlow = new SMLProcess._docFlowForm(this._transControlType, _g.d.ic_trans._doc_no, "", __docNo);
                    __docFlow.Show();

                }
                else
                {
                    MessageBox.Show(MyLib._myGlobal._resource("ไม่พบเอกสาร"));
                }
            }
            catch
            {
            }
        }

        void _dataList__controlKeyEvent(MyLib._myGrid grid, Keys key)

        {
            if (key == Keys.F)
            {
                _flowDisplay(grid);
            }
        }


        MyLib.BeforeDisplayRowReturn _gridData__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData)
        {
            MyLib.BeforeDisplayRowReturn __result = senderRow;
            try
            {
                string __getLockRecord = this._myManageTrans._dataList._gridData._cellGet(row, this._myManageTrans._dataList._isLockRecord).ToString();
                if (__getLockRecord.Equals("1") && this._myManageTrans._dataList._showIsLockColumn == false)
                {
                    return __result;
                }

                int __usedStatusColumn = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._used_status);
                int __usedStatusColumn2 = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._used_status_2);
                int __docSuccessColumn = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_success);
                int __lastStatusColumn = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._last_status);

                int __approveStatusColumnNumber = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._approve_status);

                if (this._transControlType == _g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก || this._transControlType == _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก || this._transControlType == _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก)
                {
                    __lastStatusColumn = sender._findColumnByName(_g.d.ic_wms_trans._table + "." + _g.d.ic_wms_trans._last_status);
                    __usedStatusColumn = sender._findColumnByName(_g.d.ic_wms_trans._table + "." + _g.d.ic_wms_trans._used_status);
                    __docSuccessColumn = sender._findColumnByName(_g.d.ic_wms_trans._table + "." + _g.d.ic_wms_trans._doc_success);
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
                if (__usedStatusColumn2 != -1)
                {
                    // มีการนำไปใช้แล้ว (จ่ายชำระ)
                    int __status = MyLib._myGlobal._intPhase(sender._cellGet(row, __usedStatusColumn2).ToString());
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

                if (__approveStatusColumnNumber != -1)
                {
                    int approveStatus = MyLib._myGlobal._intPhase(sender._cellGet(row, __approveStatusColumnNumber).ToString());
                    if (approveStatus == 1)
                    {
                        __result.newColor = Color.Green;
                    }
                    else if (approveStatus == 2)
                    {
                        __result.newColor = Color.SaddleBrown;
                    }
                }

                if (columnName.Equals(this._myManageTrans._dataList._isLockRecordShowColumn))
                {
                    if (__getLockRecord.Equals("1") && this._myManageTrans._dataList._showIsLockColumn == true)
                    {
                        ((ArrayList)__result.newData)[columnNumber] = "L";
                    }
                }


            }
            catch
            {
            }
            return (__result);
        }

        void _ictransScreenTop__radiotButtonCheckedChanged(object sender)
        {
            switch (this._transControlType)
            {
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                    MyLib._myRadioButton __radio = (MyLib._myRadioButton)sender;
                    string __name = __radio.__resource_name;
                    if (__name.Equals(_g.d.ic_trans._table + "." + _g.d.ic_trans._cancel_type_1))
                    {
                        if (__radio.Checked)
                        {
                            this._icTransItemGrid.Enabled = false;
                            this._itemApprovalSelectButton.Enabled = false;
                        }
                    }
                    else
                        if (__name.Equals(_g.d.ic_trans._table + "." + _g.d.ic_trans._cancel_type_2))
                    {
                        if (__radio.Checked)
                        {
                            this._icTransItemGrid.Enabled = true;
                            this._itemApprovalSelectButton.Enabled = true;
                        }
                    }
                    break;
            }
        }

        void _myToolBar_EnabledChanged(object sender, EventArgs e)

        {
            //this._icTransItemGrid.Enabled = ((ToolStrip)sender).Enabled;
            //this._icTransRef.Enabled = ((ToolStrip)sender).Enabled;
            switch (this._transControlType)
            {
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                    break;
            }
        }

        void _ictransScreenTop__vatTypeSelected(object sender, _g.g._vatTypeEnum vatType, int selectIndex)
        {
            decimal __vatRate = (vatType == _g.g._vatTypeEnum.ยกเว้นภาษี) ? 0.0M : _g.g._companyProfile._vat_rate;
            this._icTransScreenBottom._setDataNumber(_g.d.ic_trans._vat_rate, __vatRate);
            this._icTransItemGrid._findPriceAll();
        }

        bool _ictransItemGrid__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            string __itemCode = sender._cellGet(row, _g.d.ic_trans_detail._item_code).ToString().Trim();
            string __itemName = "";
            try
            {
                __itemName = sender._cellGet(row, _g.d.ic_trans_detail._item_name).ToString().Trim();
            }
            catch
            {
            }
            return (__itemCode.Length == 0 && __itemName.Length == 0) ? false : true;
        }

        public void _getConfig()
        {
            this._getTransFlag = _g.g._transFlagGlobal._transFlag(this._transControlType);
            this._getTransTemplate = _g.g._transGlobalTemplate._transTemplate(this._transControlType);
            this._getTransType = _g.g._transTypeGlobal._transType(this._transControlType);

            //  toe fix 
            if (this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ && this._loadFromDataCenter == true)
            {
                this._getTransTemplate = "screen_sale_center";
                // toe 
                MyLib._myFrameWork __datacenterFrameWork = new MyLib._myFrameWork(_g.g._companyProfile._activeSyncServer, "SMLConfig" + _g.g._companyProfile._activeSyncProvider + ".xml", MyLib._myGlobal._databaseType.PostgreSql);
                MyLib._myGlobal._databaseType __centerDatabaseType = __datacenterFrameWork._setDataBaseCode();

                this._myManageTrans._dataList._webServiceURL = _g.g._companyProfile._activeSyncServer;
                this._myManageTrans._dataList._ProviderName = "SMLConfig" + _g.g._companyProfile._activeSyncProvider.ToUpper() + ".xml";
                this._myManageTrans._dataList._databaseName = _g.g._companyProfile._activeSyncDatabase;
                this._myManageTrans._dataList._databaseType = __centerDatabaseType;
                this._myManageTrans._dataList._userOhterConnection = true;
            }
        }

        void _load()
        {
            // เปลี่ยน Table ทำงาน
            switch (this._transControlType)
            {
                case _g.g._transControlTypeEnum.คลัง_ยอดคงเหลือสินค้ายกมา:
                case _g.g._transControlTypeEnum.คลัง_ตรวจนับสินค้า:
                case _g.g._transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ:
                case _g.g._transControlTypeEnum.คลัง_จ่ายสินค้าออก_จากการขาย:
                case _g.g._transControlTypeEnum.คลัง_ปรับปรุงเพิ่มลดสินค้า:

                case _g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก:
                case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:
                case _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:
                    this._icTransTable = _g.d.ic_wms_trans._table;
                    this._icTransDetailTable = _g.d.ic_wms_trans_detail._table;
                    this._icTransScreenTop._icTransTable = this._icTransTable;
                    break;
                default:
                    this._icTransTable = _g.d.ic_trans._table;
                    this._icTransDetailTable = _g.d.ic_trans_detail._table;
                    break;
            }
            this._myManageTrans._dataList._lockRecord = true;
            this._getConfig();
            switch (this._transControlType)
            {
                default:
                    this._myManageTrans._dataList._loadViewAddColumn += new MyLib.LoadViewAddColumn(_dataList__loadViewAddColumn);
                    this._myManageTrans._dataList._autoUpper = false;
                    this._myManageTrans._dataListOpen = true;
                    this._myManageTrans._dataList._lockRecord = true;
                    this._myManageTrans._dataList._isLockDoc = true;

                    this._myManageTrans._dataList._showIsLockColumn = true;

                    this._myManageTrans._manageButton = this._myToolBar;
                    this._myManageTrans._manageBackgroundPanel = this._myPanel1;
                    //this._myManageTrans._dataList._extraWhere = _g.d.ic_trans._trans_flag + "=" + _getTransFlag.ToString() + " and " + _g.d.ic_trans._trans_type + "=" + _getTransType + this._dataListExtraWhere;
                    this._myManageTrans._dataList._extraWhereEvent += new MyLib.ExtraWhereEventHandler(_dataList__extraWhereEvent);
                    this._myManageTrans._dataList._loadViewFormat(_getTransTemplate, MyLib._myGlobal._userSearchScreenGroup, true);
                    this._myManageTrans._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageTrans__loadDataToScreen);
                    this._myManageTrans._dataList._docPictureEvent += new MyLib.DocumentPictureEventHandler(_dataList__docPictureEvent);
                    this._myManageTrans._dataList._referFieldAdd(_g.d.ic_trans._doc_no, 1);
                    this._myManageTrans._dataList._referFieldAdd(_g.d.ic_trans._trans_flag, 4);
                    this._myManageTrans._newDataClick += new MyLib.NewDataEvent(_myManageTrans__newDataClick);
                    this._myManageTrans._discardData += new MyLib.DiscardDataEvent(_myManageTrans__discardData);
                    this._myManageTrans._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
                    this._myManageTrans._dataList._multiPrintEvent += _dataList__multiPrintEvent;
                    this._myManageTrans._dataList._clearLogPrintButton.Click += _clearLogPrintButton_Click;
                    this._myManageTrans._clearData += new MyLib.ClearDataEvent(_myManageTrans__clearData);
                    this._myManageTrans._closeScreen += new MyLib.CloseScreenEvent(_myManageTrans__closeScreen);

                    this._myManageTrans._clearDataRecurring += new MyLib.ClearDataRecurringEvent(_myManageTrans__clearDataRecurring);

                    /*this._myManageTrans._dataList._refreshData();
                    this._myManageTrans.Invalidate();*/
                    this._myManageTrans._calcArea();
                    //this._myManageTrans._autoSize = true;
                    ////Control __codeControl = this._ictransScreenTop._getControl(_g.d.ic_trans._doc_no);
                    ////this._ictransScreenTop._getAutoRun();
                    //this._ictransScreenTop._setDataStr(_g.d.ic_trans._doc_no, MyLib._myGlobal._getAutoRun(_g.d.ic_trans._table, _g.d.ic_trans._doc_no, _g.d.ic_trans._trans_flag, _g.g._ictransFlagGlobal._ictransFlag(this.icTransControlType).ToString(), _g.d.ap_ar_trans._trans_type, _g.g._ictransTypeGlobal._ictransType(this.icTransControlType).ToString()).ToString(), "", true);
                    ////__codeControl.Focus();
                    break;
            }
        }

        private void _clearLogPrintButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการล้างประวัติการพิมพ์ หรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __queryResetLog = "delete from " + _g.d.erp_print_logs._table + " where doc_no = \'" + this._oldDocNo + "\' and trans_flag =" + this._getTransFlag.ToString();
                string __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __queryResetLog);
                if (__result.Length == 0)
                {
                    MessageBox.Show("ล้างประวัติการพิมพ์ สำเร็จ", "สำเร็จ");
                }
                else
                {
                    MessageBox.Show(__result);
                }
            }
        }

        // toe print range
        void _dataList__multiPrintEvent(ArrayList selectedRow)
        {
            // call print range
            ArrayList __printList = new ArrayList();
            StringBuilder __packDocNoList = new StringBuilder();
            int __docNoColumnIndex = this._myManageTrans._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
            int __isLockRecordColumnIndex = this._myManageTrans._dataList._gridData._findColumnByName(this._myManageTrans._dataList._isLockRecord);
            if (this._transControlType == _g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก || this._transControlType == _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก || this._transControlType == _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก)
            {
                __docNoColumnIndex = this._myManageTrans._dataList._gridData._findColumnByName(_g.d.ic_wms_trans._table + "." + _g.d.ic_wms_trans._doc_no);
            }
            for (int __row = 0; __row < selectedRow.Count; __row++)
            {
                int __getRow = (int)selectedRow[__row];

                // check islock
                bool __pass = true;
                if (__isLockRecordColumnIndex != -1)
                {
                    string __getIsLock = this._myManageTrans._dataList._gridData._cellGet(__getRow, __isLockRecordColumnIndex).ToString();
                    if (__getIsLock.Equals("1"))
                    {
                        __pass = false;
                    }

                }

                if ((this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ || this._transControlType == _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ) && _g.g._companyProfile._print_invoice_one_time == false) {
                    __pass = true;
                }
                if (__pass)
                {
                    List<SMLERPReportTool._ReportToolCondition> __condition = new List<SMLERPReportTool._ReportToolCondition>();
                    string __getDocNo = this._myManageTrans._dataList._gridData._cellGet(__getRow, __docNoColumnIndex).ToString();
                    __condition.Add(new SMLERPReportTool._ReportToolCondition(_g.d.gl_journal._doc_no, __getDocNo));
                    __condition.Add(new SMLERPReportTool._ReportToolCondition(_g.d.ic_trans._trans_flag, this._getTransFlag.ToString()));
                    __printList.Add(__condition.ToArray());

                    // pack doc_no List 

                    if (__packDocNoList.Length > 0)
                    {
                        __packDocNoList.Append(",");
                    }
                    __packDocNoList.Append("\'" + __getDocNo + "\'");
                }
            }

            if (__printList.Count > 0)
            {
                // ใส่ค่าว่างไปก่อน ค่อย query ดึงจากรหัสหน้าจอเอา
                string __queryGetDocFormat = "select " + _g.d.erp_doc_format._form_code + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._code + " in ( select distinct " + _g.d.ic_trans._doc_format_code + " from " + this._icTransTable + " where " + _g.d.ic_trans._doc_no + " in (" + __packDocNoList.ToString() + ") " + " and " + _g.d.ic_trans._trans_flag + "=" + this._getTransFlag + " ) ";
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
        }

        // toe doc picture form
        void _dataList__docPictureEvent(MyLib._myGrid myGrid)
        {
            if (this._docPicture == null)
            {
                this._docPicture = new SMLERPControl._getPicture();
                this._docPicture._DisplayPictureAmount = 8;
                this._docPicture._Tablename = _g.d.sml_doc_images._table;
                this._docPicture.Dock = DockStyle.Fill;

                // toe set full mode
                this._docPicture.panel1.AutoScroll = true;
                this._docPicture._pictureZoom.SizeMode = PictureBoxSizeMode.AutoSize;


                Form __formGetPicture = new Form();
                __formGetPicture.TopMost = true;
                __formGetPicture.Controls.Add(this._docPicture);

                /*DockableFormInfo __form0 = this._myManageTrans._dock.Add(__formGetPicture, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
                __form0.ShowCloseButton = true;
                __form0.ShowContextMenuButton = false;
                __form0.Disposed += (s1, e1) =>
                {
                    try
                    {
                        this._docPicture.Dispose();
                    }
                    catch
                    {
                    }
                    this._docPicture = null;
                }; */
                this._docPicture._loadImage(this._oldDocNo);
                __formGetPicture.Disposed += (s1, e1) =>
                {
                    try
                    {
                        this._docPicture.Dispose();
                    }
                    catch
                    {
                    }
                    this._docPicture = null;
                };
                __formGetPicture.Show();
            }

        }

        void _myManageTrans__clearDataRecurring()
        {
            this._logDocNoOld = "\'\'";
            this._logDocDateOld = "\'\'";
            this._logAmountOld = 0M;
            this._logDetailOld = new StringBuilder();
            this._lastItemCodeInfo = "";
            this._lastDocRefNo = "";

            this._icTransScreenTop._setDataStr(_g.d.ic_trans._doc_no, "");
            this._icTransScreenTop._setDataDate(_g.d.ic_trans._doc_date, "");
            this._icTransScreenTop._setDataStr(_g.d.ic_trans._doc_time, "");
            this._icTransScreenTop._setDataStr(_g.d.ic_trans._doc_format_code, "");
            this._icTransScreenTop._setDataStr(_g.d.ic_trans._remark, "");
            this._checkEnable();

            switch (this._transControlType)
            {
                case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                    this._icTransScreenTop._setDataStr(_g.d.ic_trans._doc_ref, "");
                    this._icTransScreenTop._setDataDate(_g.d.ic_trans._doc_ref_date, "");
                    break;
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                    //this._icTransScreenTop._setDataStr(_g.d.ic_trans._cust_code, "");
                    //this._icTransScreenTop._setDataStr(_g.d.ic_trans._contactor, "");
                    this._icTransScreenTop._setDataStr(_g.d.ic_trans._tax_doc_no, "");
                    this._icTransScreenTop._setDataDate(_g.d.ic_trans._tax_doc_date, "");
                    this._icTransScreenTop._setDataStr(_g.d.ic_trans._doc_ref, "");
                    this._icTransScreenTop._setDataDate(_g.d.ic_trans._doc_ref_date, "");
                    break;
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                    this._icTransScreenTop._setDataStr(_g.d.ic_trans._doc_ref, "");
                    this._icTransScreenTop._setDataDate(_g.d.ic_trans._doc_ref_date, "");
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                    this._icTransScreenTop._setDataStr(_g.d.ic_trans._tax_doc_no, "");
                    this._icTransScreenTop._setDataDate(_g.d.ic_trans._tax_doc_date, "");
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                    this._icTransScreenTop._setDataStr(_g.d.ic_trans._tax_doc_no, "");
                    this._icTransScreenTop._setDataDate(_g.d.ic_trans._tax_doc_date, "");
                    break;
                case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                    this._icTransScreenTop._setDataStr(_g.d.ic_trans._tax_doc_no, "");
                    this._icTransScreenTop._setDataDate(_g.d.ic_trans._tax_doc_date, "");
                    break;
                default:
                    this._myManageTrans__clearData();
                    break;
            }
        }

        string _dataList__extraWhereEvent()
        {
            string __result = _g.d.ic_trans._trans_flag + "=" + _getTransFlag.ToString() + " and " + _g.d.ic_trans._trans_type + "=" + _getTransType;

            if (this._transControlType == _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน)
            {
                __result = " ((" + _g.d.ic_trans._trans_flag + "=" + _getTransFlag.ToString() + " and " + _g.d.ic_trans._trans_type + "=" + _getTransType + ") or (" + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + " and " + _g.d.ic_trans._trans_type + "= " + _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + " ))";
            }

            if (this._dataListExtraWhere.Length > 0)
            {

                __result = __result + " and " + this._dataListExtraWhere;
            }

            // toe ระบบสาขา
            if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only == true && _g.g._companyProfile._change_branch_code == false)
            {
                __result = __result + " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\' ";
            }

            return __result;
        }

        void _dataList__loadViewAddColumn(MyLib._myGrid myGrid)
        {
            myGrid._addColumn(this._icTransTable + "." + _g.d.ic_trans._trans_flag, 2, 1, 1, false, true, true);
        }

        void _myManageTrans__closeScreen()
        {
            _g.g._updateDateTimeForCalc(this._transControlType, "");
            this.Dispose();
        }

        bool _myManageTrans__discardData()
        {
            bool result = true;
            if (this._icTransScreenTop._isChange || this._icTransScreenBottom._isChange || this._icTransItemGrid._isChange)
            {
                if (DialogResult.No == MyLib._myGlobal._displayWarning(3, null))
                {
                    result = false;
                }
                else
                {
                    this._icTransScreenTop._isChange = false;
                }
            }

            return (result);
        }

        //_ar_ap_trans._ap_trans_details _apTransDetails = null;
        // _itemApprovalSelectButton : รายการขออนุมัติซื้อ
        // _selectAllButton : เลือกทั้งหมด
        // _itemApprovalButton : อนุมัติรายการ
        // _printButton : พิมพ์ใบเสร็จรับเงิน

        private int _buildCount = 0;

        void _build()
        {
            if (this._transControlType == _g.g._transControlTypeEnum.ว่าง)
            {
                return;
            }
            this._toolStripExtra.Visible = false;
            this._buildCount++;
            if (this._buildCount > 1)
            {
                MessageBox.Show("TransControl : มีการสร้างจอสองครั้ง");
            }
            this._icTransRef.Visible = false;
            this._mySelectBar.Visible = false;

            switch (this._transControlType)
            {
                #region สินค้า
                case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก:
                    this._icTransScreenTop._screen_code = "IA";
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสารปรับปรุงสต๊อคสินค้า";
                    this._printButton.Click += new EventHandler(_printButton_Click);

                    break;
                case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด:
                    this._icTransScreenTop._screen_code = "IS";
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์";
                    this._printButton.Click += new EventHandler(_printButton_Click);

                    break;
                case _g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า:
                    this._icTransScreenTop._screen_code = "CO";
                    break;
                case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา:
                    this._icTransScreenTop._screen_code = "IB";
                    break;
                case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                    // Print
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._icTransRef.Visible = true;
                    }
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสารเบิกสินค้าและวัตถุดิบ";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    this._icTransScreenTop._screen_code = "IO";
                    this._icTransScreenTop._selectInvoceFromPos += new SelectInvoiceFromPosEventHandler(_icTransScreenTop__selectInvoceFromPos);

                    break;
                case _g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ:
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    this._icTransScreenTop._screen_code = "RIO";
                    break;

                case _g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ_ยกเลิก:
                    this._tab.Visible = false;
                    this._toolStripExtra.Visible = false;
                    this._icTransScreenTop._screen_code = "CRO";

                    break;

                case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ_ยกเลิก:
                    this._tab.Visible = false;
                    this._toolStripExtra.Visible = false;
                    this._icTransScreenTop._screen_code = "IOC";
                    break;
                case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                    this._icTransRef.Visible = true;
                    // Print
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสารรับคืนสินค้า";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    this._icTransScreenTop._screen_code = "IR";
                    break;
                case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก_ยกเลิก:
                    this._tab.Visible = false;
                    this._toolStripExtra.Visible = false;
                    this._icTransScreenTop._screen_code = "IRC";
                    break;
                case _g.g._transControlTypeEnum.สินค้า_โอนออก_ยกเลิก:
                    this._tab.Visible = false;
                    this._toolStripExtra.Visible = false;
                    this._icTransScreenTop._screen_code = "IMC";
                    break;
                case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก_ยกเลิก:
                    this._tab.Visible = false;
                    this._toolStripExtra.Visible = false;
                    this._icTransScreenTop._screen_code = "IAC";
                    break;
                case _g.g._transControlTypeEnum.StockCheck:
                    this._icTransRef.Visible = true;
                    this._icTransItemGrid._extraWordShow = false;
                    this._icTransItemGrid.Enabled = false;
                    break;
                case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                    // Print
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสารรับสินค้าสำเร็จรูป";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    //
                    this._icTransScreenTop._screen_code = "IF";
                    break;
                case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป_ยกเลิก:
                    this._tab.Visible = false;
                    this._toolStripExtra.Visible = false;
                    this._icTransScreenTop._screen_code = "IFC";
                    break;
                case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {


                        //this._icTransRef.Visible = true;
                        this._mySelectBar.Visible = true;
                        this._itemApprovalSelectButton.Visible = true;
                        this._selectAllButton.Visible = false;
                        this._itemApprovalButton.Visible = true;
                        this._addButton.Visible = false;
                        this._itemApprovalSelectButton.ResourceName = "อ้างอิงใบขอโอนสินค้า";
                        this._itemApprovalSelectButton.Enabled = true;
                        this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                        this._itemApprovalButton.ResourceName = "อ้างอิงรายการซื้อสินค้า";
                        this._itemApprovalButton.Enabled = true;
                        this._itemApprovalButton.Click += _itemApprovalButton_Click;
                        //this._itemApprovalButton.Visible = true;
                    }
                    // Print ------------------------------------------
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสารโอนสินค้าออก";
                    this._printButton.Click += new EventHandler(_printButton_Click); // 21-12-2552  anek
                    this._icTransScreenTop._screen_code = "IM";
                    this._icTransScreenTop._selectInvoceFromPos += new SelectInvoiceFromPosEventHandler(_icTransScreenTop__selectInvoceFromPos);

                    break;
                case _g.g._transControlTypeEnum.สินค้า_ขอโอน:
                    // Print ------------------------------------------
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสารขอโอนสินค้าออก";
                    this._printButton.Click += new EventHandler(_printButton_Click); // 21-12-2552  anek
                    this._icTransScreenTop._screen_code = "RIM";
                    // this._icTransScreenTop._selectInvoceFromPos += new SelectInvoiceFromPosEventHandler(_icTransScreenTop__selectInvoceFromPos);

                    break;
                #endregion
                #region คลังสินค้า
                case _g.g._transControlTypeEnum.คลัง_ยอดคงเหลือสินค้ายกมา:
                    this._icTransScreenTop._screen_code = "WIB";
                    break;
                case _g.g._transControlTypeEnum.คลัง_ตรวจนับสินค้า:
                    this._icTransScreenTop._screen_code = "WCO";
                    break;
                case _g.g._transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ:
                    this._icTransScreenTop._screen_code = "WI";
                    break;
                case _g.g._transControlTypeEnum.คลัง_จ่ายสินค้าออก_จากการขาย:
                    this._icTransScreenTop._screen_code = "WO";
                    break;
                case _g.g._transControlTypeEnum.คลัง_ปรับปรุงเพิ่มลดสินค้า:
                    this._icTransScreenTop._screen_code = "WIA";
                    break;


                // singha
                case _g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก:
                    this._icTransScreenTop._screen_code = "WSDD";
                    this._printButton.Visible = true;
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    break;
                case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:
                    this._icTransScreenTop._screen_code = "WSDW";
                    this._icTransRef.Visible = true;
                    this._printButton.Visible = true;
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    break;
                case _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:
                    this._icTransScreenTop._screen_code = "WSDR";
                    this._icTransRef.Visible = true;
                    this._printButton.Visible = true;
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    break;

                #endregion
                #region ซื้อ
                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                    this._icTransScreenTop._screen_code = "PR";
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสารเสนอซื้อ";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "PCC";
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "PRT";
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "PDC";
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "PDRC";
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "PAC";
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "PUC";
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "PTC";
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                    this._mySelectBar.Visible = true;
                    this._itemApprovalSelectButton.Visible = true;
                    this._itemApprovalSelectButton.ResourceName = "เลือกเอกสารซื้อสินค้า/ค่าบริการ";
                    this._itemApprovalButton.Visible = false;
                    this._selectAllButton.Visible = false;
                    this._addButton.Visible = false;
                    this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                    this._addButton.Click += new EventHandler(_addButton_Click);
                    //
                    //
                    this._vatBuy = new SMLERPGLControl._vatBuy();
                    //this._withHoldingTax = new SMLERPGLControl._withHoldingTax(SMLERPGLControl._withHoldingTaxType.ซื้อ);
                    //
                    //Print 
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสารซื้อสินค้าเพิ่มหนี้";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    this._icTransScreenTop._screen_code = "PA";
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                    this._mySelectBar.Visible = true;
                    this._itemApprovalSelectButton.Visible = true;
                    this._itemApprovalSelectButton.ResourceName = "เลือกเอกสารตั้งหนี้";
                    this._itemApprovalButton.Visible = false;
                    this._selectAllButton.Visible = false;
                    this._addButton.Visible = false;
                    this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                    this._addButton.Click += new EventHandler(_addButton_Click);
                    //
                    //
                    this._vatBuy = new SMLERPGLControl._vatBuy();
                    //this._withHoldingTax = new SMLERPGLControl._withHoldingTax(SMLERPGLControl._withHoldingTaxType.ซื้อ);
                    //
                    //Print 
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสารเพิ่มหนี้";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    this._icTransScreenTop._screen_code = "PID";
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                    this._mySelectBar.Visible = true;
                    this._itemApprovalSelectButton.Visible = true;
                    this._itemApprovalSelectButton.ResourceName = "เลือกเอกสารรับสินค้า";
                    this._itemApprovalButton.Visible = false;
                    this._selectAllButton.Visible = false;
                    this._addButton.Visible = false;
                    this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                    this._addButton.Click += new EventHandler(_addButton_Click);
                    //
                    //Print 
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสารราคาผิด";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    this._icTransScreenTop._screen_code = "PIA";
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า:
                    /*this._ictransItemGrid.Visible = false;
                    this._ictransScreenBottom.Visible = false;
                    this._apTransDetails = new SMLERPAPARControl._ap_trans_details();
                    //this._apTransDetails.Dock = DockStyle.Fill;
                    this._myManageTrans.Panel2.Controls.Add(this._apTransDetails);*/
                    // Print
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสารจ่ายเงินล่วงหน้า";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก:
                    this._icTransScreenTop._screen_code = "PRC";
                    break;
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ:
                    this._icTransScreenTop._screen_code = "SSA";
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ:
                    this._icTransScreenTop._screen_code = "PRA";
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                    this._toolStripExtra.Visible = true;
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLIMS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._mySelectBar.Visible = true;
                        this._itemApprovalSelectButton.Visible = true;
                        this._itemApprovalSelectButton.ResourceName = "เลือกเอกสารเสนอซื้อ"; //"เลือกเอกสารอนุมัติเสนอซื้อ";
                        this._itemApprovalButton.Visible = false;
                        this._selectAllButton.Visible = false;
                        this._addButton.Visible = false;
                        this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);

                        // po shipment
                        {
                            _shipmentControl = new _shipmentControl(1);
                            _shipmentControl.Dock = DockStyle.Fill;
                            //_shipmentControl._getCustCode += _shipmentControl__getCustCode;

                            this._tab_shipment = new System.Windows.Forms.TabPage();

                            this._tab_shipment.SuspendLayout();
                            this._tab.Controls.Add(this._tab_shipment);
                            // 
                            // tab_gl
                            // 
                            this._tab_shipment.Controls.Add(_shipmentControl);
                            this._tab_shipment.ImageKey = "document_add.png";
                            this._tab_shipment.Location = new System.Drawing.Point(4, 23);
                            this._tab_shipment.Name = "tab_shipment";
                            this._tab_shipment.Size = new System.Drawing.Size(66, 0);
                            this._tab_shipment.TabIndex = 1;
                            this._tab_shipment.Text = "tab_shipment";
                            this._tab_shipment.UseVisualStyleBackColor = true;
                            //
                            this._tab_shipment.ResumeLayout(false);
                        }
                    }

                    if (MyLib._myGlobal._programName.Equals("SML CM"))
                    {
                        MyLib.ToolStripMyButton __buttonSearchSR = new MyLib.ToolStripMyButton();
                        __buttonSearchSR.Image = global::SMLInventoryControl.Properties.Resources.contract;
                        __buttonSearchSR.ImageTransparentColor = System.Drawing.Color.Magenta;
                        __buttonSearchSR.Name = "__buttonSearchSR";
                        __buttonSearchSR.Padding = new System.Windows.Forms.Padding(1);
                        __buttonSearchSR.ResourceName = "เลือกเอกสารสั่งซื้อ/สั่งจอง";
                        __buttonSearchSR.Size = new System.Drawing.Size(90, 22);
                        __buttonSearchSR.Text = "เลือกเอกสารสั่งซื้อ/สั่งจอง";
                        __buttonSearchSR.Click += __buttonSearchSR_Click;

                        this._mySelectBar.Items.Add(__buttonSearchSR);
                    }
                    // Print
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสารสั่งซื้อ";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    // End Print
                    this._checkPurchasePermiumButton.Visible = true;
                    this._checkPurchasePermiumButton.Enabled = true;
                    this._purchasePointButton.Enabled = true;
                    this._purchasePointButton.Visible = true;
                    this._icTransScreenTop._screen_code = "PO";
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ:
                    this._icTransScreenTop._screen_code = "POA";
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                    this._icTransItemGrid.Enabled = false;
                    this._mySelectBar.Visible = true;
                    this._itemApprovalSelectButton.Visible = true;
                    this._itemApprovalSelectButton.ResourceName = "ดึงรายการสินค้า";
                    this._itemApprovalSelectButton.Enabled = true;
                    this._itemApprovalButton.Visible = false;
                    this._selectAllButton.Visible = false;
                    this._addButton.Visible = false;
                    this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                    this._icTransScreenTop._screen_code = "POC";
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                    this._mySelectBar.Visible = true;
                    this._itemApprovalSelectButton.Visible = true;
                    this._itemApprovalSelectButton.ResourceName = "เลือกเอกสารสั่งซื้อสินค้า";
                    this._itemApprovalButton.Visible = false;
                    this._selectAllButton.Visible = false;
                    this._addButton.Visible = false;
                    this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                    this._addButton.Click += new EventHandler(_addButton_Click);
                    //
                    this._payControl = new SMLERPAPARControl._payControl();
                    this._withHoldingTax = new SMLERPGLControl._withHoldingTax(SMLERPGLControl._withHoldingTaxType.ซื้อ);
                    this._vatBuy = new SMLERPGLControl._vatBuy();
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        this._payAdvance = new SMLERPAPARControl._advanceControl();
                    }
                    // Print
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสารซื้อสินค้าและค่าบริการ";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    //
                    this._icTransScreenTop._screen_code = "PU";
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                    this._mySelectBar.Visible = true;
                    this._itemApprovalSelectButton.Visible = true;
                    this._itemApprovalSelectButton.ResourceName = "เลือกเอกสารรับสินค้า";
                    this._itemApprovalButton.Visible = false;
                    this._selectAllButton.Visible = false;
                    this._addButton.Visible = false;
                    this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                    this._addButton.Click += new EventHandler(_addButton_Click);
                    //
                    this._payControl = new SMLERPAPARControl._payControl();
                    this._withHoldingTax = new SMLERPGLControl._withHoldingTax(SMLERPGLControl._withHoldingTaxType.ซื้อ);
                    this._vatBuy = new SMLERPGLControl._vatBuy();
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        this._payAdvance = new SMLERPAPARControl._advanceControl();
                    }
                    // Print
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสารซื้อสินค้าและค่าบริการ";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    //
                    this._icTransScreenTop._screen_code = "PIU";
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                    this._mySelectBar.Visible = true;
                    this._itemApprovalSelectButton.Visible = true;
                    this._itemApprovalSelectButton.ResourceName = "เลือกเอกสารสั่งซื้อสินค้า";
                    this._itemApprovalButton.Visible = false;
                    this._selectAllButton.Visible = false;
                    this._addButton.Visible = false;
                    this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                    this._addButton.Click += new EventHandler(_addButton_Click);
                    //
                    // Print
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสารรับสินค้า";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    //
                    this._icTransScreenTop._screen_code = "PI";
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                    this._mySelectBar.Visible = true;
                    this._itemApprovalSelectButton.Visible = true;
                    this._itemApprovalSelectButton.ResourceName = "เลือกเอกสารซื้อสินค้า/ค่าบริการ";
                    this._itemApprovalButton.Visible = false;
                    this._selectAllButton.Visible = false;
                    this._addButton.Visible = false;
                    this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                    this._addButton.Click += new EventHandler(_addButton_Click);
                    // Print ------------------------------------------
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสารส่งคืนสินค้าลดหนี้";
                    this._printButton.Click += new EventHandler(_printButton_Click); // 21-12-2552  anek
                    // ------------------------------------------------
                    this._withHoldingTax = new SMLERPGLControl._withHoldingTax(SMLERPGLControl._withHoldingTaxType.ซื้อ);
                    //
                    this._payControl = new SMLERPAPARControl._payControl();
                    this._vatBuy = new SMLERPGLControl._vatBuy();
                    this._icTransScreenTop._screen_code = "PT";
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                    this._mySelectBar.Visible = true;
                    this._itemApprovalSelectButton.Visible = true;
                    this._itemApprovalSelectButton.ResourceName = "เลือกเอกสารตั้งหนี้";
                    this._itemApprovalButton.Visible = false;
                    this._selectAllButton.Visible = false;
                    this._addButton.Visible = false;
                    this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                    this._addButton.Click += new EventHandler(_addButton_Click);
                    // Print ------------------------------------------
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสารลดหนี้";
                    this._printButton.Click += new EventHandler(_printButton_Click); // 21-12-2552  anek
                    // ------------------------------------------------
                    //this._withHoldingTax = new SMLERPGLControl._withHoldingTax(SMLERPGLControl._withHoldingTaxType.ซื้อ);
                    //
                    this._payControl = new SMLERPAPARControl._payControl();
                    this._vatBuy = new SMLERPGLControl._vatBuy();
                    this._icTransScreenTop._screen_code = "PIC";
                    break;
                #endregion
                #region ขาย
                case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "SDC";
                    break;
                case _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "SDRC";
                    break;
                case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "SCR";
                    break;
                case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "SCT";
                    break;
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "SIC";
                    break;
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "STC";
                    break;
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "SAC";
                    break;
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก:
                    this._icTransScreenTop._screen_code = "SOC";
                    break;
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                    this._icTransItemGrid.Enabled = false;
                    this._mySelectBar.Visible = true;
                    this._itemApprovalSelectButton.Visible = true;
                    this._itemApprovalSelectButton.ResourceName = "ดึงรายการสินค้า";
                    this._itemApprovalSelectButton.Enabled = true;
                    this._itemApprovalButton.Visible = false;
                    this._selectAllButton.Visible = false;
                    this._addButton.Visible = false;
                    this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                    this._icTransScreenTop._screen_code = "SSC";
                    break;
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                    this._mySelectBar.Visible = true;
                    this._itemApprovalSelectButton.Visible = true;
                    this._itemApprovalSelectButton.ResourceName = "เลือกเอกสารขายสินค้า/ค่าบริการ";
                    this._itemApprovalButton.Visible = false;
                    this._selectAllButton.Visible = false;
                    this._addButton.Visible = false;
                    this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                    this._addButton.Click += new EventHandler(_addButton_Click);
                    //
                    this._vatSale = new SMLERPGLControl._vatSale();
                    //
                    //this._withHoldingTax = new SMLERPGLControl._withHoldingTax(SMLERPGLControl._withHoldingTaxType.ขาย);
                    //
                    // Print
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสารขายสินค้า (เพิ่มหนี้)";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    this._icTransScreenTop._screen_code = "SA";
                    //
                    break;
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                    this._mySelectBar.Visible = true;
                    this._itemApprovalSelectButton.Visible = true;
                    this._itemApprovalSelectButton.ResourceName = "เลือกเอกสารขายสินค้า/ค่าบริการ";
                    this._itemApprovalButton.Visible = false;
                    this._selectAllButton.Visible = false;
                    this._addButton.Visible = false;
                    this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                    this._addButton.Click += new EventHandler(_addButton_Click);
                    //
                    this._payControl = new SMLERPAPARControl._payControl();
                    this._withHoldingTax = new SMLERPGLControl._withHoldingTax(SMLERPGLControl._withHoldingTaxType.ขาย);
                    this._vatSale = new SMLERPGLControl._vatSale();
                    //
                    // Print
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสารขายสินค้า (ลดหนี้)";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    this._icTransScreenTop._screen_code = "ST";

                    // shipment
                    if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                    {
                        _shipmentControl = new _shipmentControl(0);
                        _shipmentControl.Dock = DockStyle.Fill;
                        _shipmentControl._getCustCode += _shipmentControl__getCustCode;

                        if (MyLib._myGlobal._OEMVersion == "SINGHA")
                        {
                            _shipmentControl._afterSelectAddress += _shipmentControl__afterSelectAddress;
                        }

                        this._tab_shipment = new System.Windows.Forms.TabPage();

                        this._tab_shipment.SuspendLayout();
                        this._tab.Controls.Add(this._tab_shipment);
                        // 
                        // tab_gl
                        // 
                        this._tab_shipment.Controls.Add(_shipmentControl);
                        this._tab_shipment.ImageKey = "document_add.png";
                        this._tab_shipment.Location = new System.Drawing.Point(4, 23);
                        this._tab_shipment.Name = "tab_shipment";
                        this._tab_shipment.Size = new System.Drawing.Size(66, 0);
                        this._tab_shipment.TabIndex = 1;
                        this._tab_shipment.Text = "tab_shipment";
                        this._tab_shipment.UseVisualStyleBackColor = true;
                        //
                        this._tab_shipment.ResumeLayout(false);
                    }
                    break;
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:

                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLBIllFree)
                    {
                        this._mySelectBar.Visible = true;
                        this._itemApprovalSelectButton.Visible = true;
                        this._itemApprovalSelectButton.ResourceName = "เลือกเอกสารเสนอราคา/ใบสั่งซื้อ/ใบสั่งจอง/ใบสั่งขาย";
                        this._itemApprovalButton.Visible = false;
                        this._selectAllButton.Visible = false;
                        this._addButton.Visible = false;
                        this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                        this._addButton.Click += new EventHandler(_addButton_Click);

                        if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOS ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                        {
                            _shipmentControl = new _shipmentControl(0);
                            _shipmentControl.Dock = DockStyle.Fill;
                            _shipmentControl._getCustCode += _shipmentControl__getCustCode;

                            if (MyLib._myGlobal._OEMVersion == "SINGHA")
                            {
                                _shipmentControl._afterSelectAddress += _shipmentControl__afterSelectAddress;
                            }

                            this._tab_shipment = new System.Windows.Forms.TabPage();

                            this._tab_shipment.SuspendLayout();
                            this._tab.Controls.Add(this._tab_shipment);
                            // 
                            // tab_gl
                            // 
                            this._tab_shipment.Controls.Add(_shipmentControl);
                            this._tab_shipment.ImageKey = "document_add.png";
                            this._tab_shipment.Location = new System.Drawing.Point(4, 23);
                            this._tab_shipment.Name = "tab_shipment";
                            this._tab_shipment.Size = new System.Drawing.Size(66, 0);
                            this._tab_shipment.TabIndex = 1;
                            this._tab_shipment.Text = "tab_shipment";
                            this._tab_shipment.UseVisualStyleBackColor = true;
                            //
                            this._tab_shipment.ResumeLayout(false);
                        }


                    }
                    else
                    {
                        this._tab.TabPages[this._tab_more.Name].Dispose();
                    }

                    // Print
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสาร";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    //
                    this._payControl = new SMLERPAPARControl._payControl();
                    this._withHoldingTax = new SMLERPGLControl._withHoldingTax(SMLERPGLControl._withHoldingTaxType.ขาย);
                    this._vatSale = new SMLERPGLControl._vatSale();
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore && MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLBIllFree)
                    {
                        this._payAdvance = new SMLERPAPARControl._advanceControl();
                    }
                    //
                    this._icTransScreenTop._screen_code = "SI";
                    this._icTransScreenTop._selectInvoceFromPos += new SelectInvoiceFromPosEventHandler(_icTransScreenTop__selectInvoceFromPos);
                    break;
                // toe
                case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                    this._mySelectBar.Visible = true;
                    this._itemApprovalSelectButton.Visible = true;
                    this._itemApprovalSelectButton.ResourceName = "เลือกเอกสารเสนอราคา/ใบสั่งซื้อ/ใบสั่งจอง/ใบสั่งขาย";
                    this._itemApprovalButton.Visible = false;
                    this._selectAllButton.Visible = false;
                    this._addButton.Visible = false;
                    this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                    this._addButton.Click += new EventHandler(_addButton_Click);
                    // Print
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสาร";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    //
                    this._payControl = new SMLERPAPARControl._payControl();
                    this._withHoldingTax = new SMLERPGLControl._withHoldingTax(SMLERPGLControl._withHoldingTaxType.ขาย);
                    this._vatSale = new SMLERPGLControl._vatSale();
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        this._payAdvance = new SMLERPAPARControl._advanceControl();
                    }
                    //
                    this._icTransScreenTop._screen_code = "SI";
                    this._icTransScreenTop._selectInvoceFromPos += new SelectInvoiceFromPosEventHandler(_icTransScreenTop__selectInvoceFromPos);
                    break;
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                    this._mySelectBar.Visible = true;
                    this._itemApprovalSelectButton.Visible = true;
                    this._itemApprovalSelectButton.ResourceName = "เลือกเอกสารเสนอราคา/ใบสั่งซื้อ/ใบสั่งจอง";
                    this._itemApprovalButton.Visible = false;
                    this._selectAllButton.Visible = false;
                    this._addButton.Visible = false;
                    this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                    this._addButton.Click += new EventHandler(_addButton_Click);
                    // Print
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสารสั่งขาย";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    this._icTransScreenTop._screen_code = "SS";

                    // shipment
                    {
                        _shipmentControl = new _shipmentControl(0);
                        _shipmentControl.Dock = DockStyle.Fill;
                        _shipmentControl._getCustCode += _shipmentControl__getCustCode;

                        if (MyLib._myGlobal._OEMVersion == "SINGHA")
                        {
                            _shipmentControl._afterSelectAddress += _shipmentControl__afterSelectAddress;
                        }

                        this._tab_shipment = new System.Windows.Forms.TabPage();

                        this._tab_shipment.SuspendLayout();
                        this._tab.Controls.Add(this._tab_shipment);
                        // 
                        // tab_gl
                        // 
                        this._tab_shipment.Controls.Add(_shipmentControl);
                        this._tab_shipment.ImageKey = "document_add.png";
                        this._tab_shipment.Location = new System.Drawing.Point(4, 23);
                        this._tab_shipment.Name = "tab_shipment";
                        this._tab_shipment.Size = new System.Drawing.Size(66, 0);
                        this._tab_shipment.TabIndex = 1;
                        this._tab_shipment.Text = "tab_shipment";
                        this._tab_shipment.UseVisualStyleBackColor = true;
                        //
                        this._tab_shipment.ResumeLayout(false);
                    }

                    break;
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                    this._printButton.Visible = true;
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    this._icTransScreenTop._screen_code = "SO";
                    break;
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                    this._mySelectBar.Visible = true;
                    this._itemApprovalSelectButton.Visible = true;
                    this._itemApprovalSelectButton.ResourceName = "เลือกเอกสารเสนอราคา";
                    this._itemApprovalButton.Visible = false;
                    this._selectAllButton.Visible = false;
                    this._addButton.Visible = false;
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสารสั่งจองสั่งซื้อสินค้า";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                    this._addButton.Click += new EventHandler(_addButton_Click);
                    //
                    this._icTransScreenTop._screen_code = "SR";
                    break;
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ:
                    this._icTransScreenTop._screen_code = "SOA";
                    break;
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ:
                    this._icTransScreenTop._screen_code = "SRA";
                    break;
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                    this._icTransItemGrid.Enabled = false;
                    this._mySelectBar.Visible = true;
                    this._itemApprovalSelectButton.Visible = true;
                    this._itemApprovalSelectButton.ResourceName = "ดึงรายการสินค้า";
                    this._itemApprovalSelectButton.Enabled = true;
                    this._itemApprovalButton.Visible = false;
                    this._selectAllButton.Visible = false;
                    this._addButton.Visible = false;
                    this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                    this._icTransScreenTop._screen_code = "SRC";
                    break;
                #endregion
                #region เจ้าหนี้
                case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                    this._mySelectBar.Visible = true;
                    this._itemApprovalSelectButton.Visible = true;
                    this._itemApprovalSelectButton.ResourceName = "เลือกเอกสารสั่งซื้อสินค้า";
                    this._itemApprovalButton.Visible = false;
                    this._selectAllButton.Visible = false;
                    this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                    this._icTransRef._toolBarButtom.Visible = false;
                    this._addButton.Visible = false;
                    this._addButton.Click += new EventHandler(_addButton_Click);
                    //
                    this._vatBuy = new SMLERPGLControl._vatBuy();
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        this._payAdvance = new SMLERPAPARControl._advanceControl();
                    }
                    //
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสารตั้งหนี้อื่น ๆ";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    this._icTransScreenTop._screen_code = "COB";
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "COC";
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "CNO";
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "CIC";
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                    this._mySelectBar.Visible = true;
                    this._itemApprovalSelectButton.Visible = true;
                    this._itemApprovalSelectButton.ResourceName = "เลือกเอกสารตั้งหนี้อื่นๆ";
                    this._itemApprovalButton.Visible = false;
                    this._selectAllButton.Visible = false;
                    this._addButton.Visible = false;
                    this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                    this._addButton.Click += new EventHandler(_addButton_Click);
                    this._vatBuy = new SMLERPGLControl._vatBuy();
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสารลดหนี้อื่น ๆ";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    this._icTransScreenTop._screen_code = "CCO";
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                    this._mySelectBar.Visible = true;
                    this._itemApprovalSelectButton.Visible = true;
                    this._itemApprovalSelectButton.ResourceName = "เลือกเอกสารตั้งหนี้อื่นๆ";
                    this._itemApprovalButton.Visible = false;
                    this._selectAllButton.Visible = false;
                    this._addButton.Visible = false;
                    this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                    this._addButton.Click += new EventHandler(_addButton_Click);
                    this._vatBuy = new SMLERPGLControl._vatBuy();
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสารเพิ่มหนี้อื่น ๆ";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    this._icTransScreenTop._screen_code = "CDO";
                    break;
                #endregion
                #region ลูกหนี้
                case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                    this._mySelectBar.Visible = true;
                    this._itemApprovalSelectButton.Visible = true;
                    this._itemApprovalSelectButton.ResourceName = "เลือกเอกสารเสนอราคา";
                    this._itemApprovalButton.Visible = false;
                    this._selectAllButton.Visible = false;
                    this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                    this._icTransRef._toolBarButtom.Visible = false;
                    this._addButton.Visible = false;
                    this._addButton.Click += new EventHandler(_addButton_Click);
                    //
                    this._vatSale = new SMLERPGLControl._vatSale();
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        this._payAdvance = new SMLERPAPARControl._advanceControl();
                    }
                    //
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสารตั้งหนี้อื่น ๆ";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    this._icTransScreenTop._screen_code = "AOB";
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                    this._mySelectBar.Visible = true;
                    this._itemApprovalSelectButton.Visible = true;
                    this._itemApprovalSelectButton.ResourceName = "เลือกเอกสารตั้งหนี้อื่นๆ";
                    this._itemApprovalButton.Visible = false;
                    this._selectAllButton.Visible = false;
                    this._addButton.Visible = false;
                    this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                    this._addButton.Click += new EventHandler(_addButton_Click);
                    this._vatSale = new SMLERPGLControl._vatSale();
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสารลดหนี้อื่น ๆ";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    this._icTransScreenTop._screen_code = "ACO";
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                    this._mySelectBar.Visible = true;
                    this._itemApprovalSelectButton.Visible = true;
                    this._itemApprovalSelectButton.ResourceName = "เลือกเอกสารตั้งหนี้อื่นๆ";
                    this._itemApprovalButton.Visible = false;
                    this._selectAllButton.Visible = false;
                    this._addButton.Visible = false;
                    this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                    this._addButton.Click += new EventHandler(_addButton_Click);
                    this._vatSale = new SMLERPGLControl._vatSale();
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสารเพิ่มหนี้อื่น ๆ";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    this._icTransScreenTop._screen_code = "ADO";
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "AOC";
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "ADC";
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "AIC";
                    break;
                #endregion
                #region ค่าใช้จ่ายอื่น ๆ
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                    this._mySelectBar.Visible = true;
                    this._itemApprovalSelectButton.Visible = true;
                    this._itemApprovalSelectButton.ResourceName = "เลือกเอกสารสั่งซื้อสินค้า";
                    this._itemApprovalButton.Visible = false;
                    this._selectAllButton.Visible = false;
                    this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                    this._icTransRef._toolBarButtom.Visible = false;
                    this._addButton.Visible = false;
                    this._addButton.Click += new EventHandler(_addButton_Click);
                    //
                    this._payControl = new SMLERPAPARControl._payControl();
                    this._withHoldingTax = new SMLERPGLControl._withHoldingTax(SMLERPGLControl._withHoldingTaxType.ซื้อ);

                    // toe print multi withHoldingTax
                    this._withHoldingTax.printSeparator.Visible = true;
                    this._withHoldingTax._printWithHoldingTagButton.Visible = true;
                    this._withHoldingTax._printWithHoldingTagButton.Click += _printWithHoldingTagButton_Click;
                    this._withHoldingTax._printSelectRecordButton.Visible = true;
                    this._withHoldingTax._printSelectRecordButton.Click += _printSelectRecordButton_Click;

                    this._vatBuy = new SMLERPGLControl._vatBuy();
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        this._payAdvance = new SMLERPAPARControl._advanceControl();
                    }
                    // Print
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์";
                    this._printButton.Click += new EventHandler(_printButton_Click);

                    //
                    this._icTransScreenTop._screen_code = "EPO";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                    this._mySelectBar.Visible = true;
                    this._itemApprovalSelectButton.Visible = true;
                    this._itemApprovalSelectButton.ResourceName = "เลือกเอกสารตั้งหนี้อื่นๆ";
                    this._itemApprovalButton.Visible = false;
                    this._selectAllButton.Visible = false;
                    this._addButton.Visible = false;
                    this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                    this._addButton.Click += new EventHandler(_addButton_Click);
                    this._vatBuy = new SMLERPGLControl._vatBuy();
                    this._payControl = new SMLERPAPARControl._payControl();
                    this._withHoldingTax = new SMLERPGLControl._withHoldingTax(SMLERPGLControl._withHoldingTaxType.ซื้อ);
                    this._icTransScreenTop._screen_code = "EPC";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                    this._mySelectBar.Visible = true;
                    this._itemApprovalSelectButton.Visible = true;
                    this._itemApprovalSelectButton.ResourceName = "เลือกเอกสารตั้งหนี้อื่นๆ";
                    this._itemApprovalButton.Visible = false;
                    this._selectAllButton.Visible = false;
                    this._addButton.Visible = false;
                    this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                    this._addButton.Click += new EventHandler(_addButton_Click);
                    this._vatBuy = new SMLERPGLControl._vatBuy();
                    this._payControl = new SMLERPAPARControl._payControl();
                    this._withHoldingTax = new SMLERPGLControl._withHoldingTax(SMLERPGLControl._withHoldingTaxType.ซื้อ);
                    this._icTransScreenTop._screen_code = "EPD";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "EOC";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "EDC";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "ECC";
                    break;
                #endregion
                #region รายได้อื่น ๆ
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                    this._mySelectBar.Visible = true;
                    this._itemApprovalSelectButton.Visible = true;
                    this._itemApprovalSelectButton.ResourceName = "เลือกเอกสารเสนอราคา";
                    this._itemApprovalButton.Visible = false;
                    this._selectAllButton.Visible = false;
                    this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                    this._icTransRef._toolBarButtom.Visible = false;
                    this._addButton.Visible = false;
                    this._addButton.Click += new EventHandler(_addButton_Click);
                    //
                    // toe 
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์";
                    this._printButton.Click += new EventHandler(_printButton_Click);

                    this._payControl = new SMLERPAPARControl._payControl();
                    this._withHoldingTax = new SMLERPGLControl._withHoldingTax(SMLERPGLControl._withHoldingTaxType.ขาย);
                    this._vatSale = new SMLERPGLControl._vatSale();
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        this._payAdvance = new SMLERPAPARControl._advanceControl();
                    }
                    //
                    this._icTransScreenTop._screen_code = "OI";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                    this._mySelectBar.Visible = true;
                    this._itemApprovalSelectButton.Visible = true;
                    this._itemApprovalSelectButton.ResourceName = "เลือกเอกสารตั้งหนี้อื่นๆ";
                    this._itemApprovalButton.Visible = false;
                    this._selectAllButton.Visible = false;
                    this._addButton.Visible = false;
                    this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                    this._addButton.Click += new EventHandler(_addButton_Click);
                    this._payControl = new SMLERPAPARControl._payControl();
                    this._vatSale = new SMLERPGLControl._vatSale();
                    this._withHoldingTax = new SMLERPGLControl._withHoldingTax(SMLERPGLControl._withHoldingTaxType.ขาย);
                    this._icTransScreenTop._screen_code = "OCN";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                    this._mySelectBar.Visible = true;
                    this._itemApprovalSelectButton.Visible = true;
                    this._itemApprovalSelectButton.ResourceName = "เลือกเอกสารตั้งหนี้อื่นๆ";
                    this._itemApprovalButton.Visible = false;
                    this._selectAllButton.Visible = false;
                    this._addButton.Visible = false;
                    this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                    this._addButton.Click += new EventHandler(_addButton_Click);
                    this._payControl = new SMLERPAPARControl._payControl();
                    this._vatSale = new SMLERPGLControl._vatSale();
                    this._icTransScreenTop._screen_code = "ODN";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "OIC";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "ODC";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "OCC";
                    break;
                #endregion
                #region เช็ครับ
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา:
                    this._icTransScreenTop._screen_code = "CHB";
                    this._printButton.Visible = false;
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                    this._icTransScreenTop._screen_code = "CHP";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:
                    this._icTransScreenTop._screen_code = "CRT";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก:
                    this._icTransScreenTop._screen_code = "CRC";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่:
                    this._icTransScreenTop._screen_code = "CHN";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                    this._icTransScreenTop._screen_code = "CHD";
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสาร";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                    this._icTransScreenTop._screen_code = "CDE";
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสาร";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    this._payControl = new SMLERPAPARControl._payControl();
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "CDC";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "CCP";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "CTC";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "DCC";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "CNC";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "CED";
                    break;
                #endregion
                #region เช็คจ่าย
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา:
                    this._icTransScreenTop._screen_code = "CPB";
                    this._printButton.Visible = false;
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                    this._icTransScreenTop._screen_code = "CPP";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:
                    this._icTransScreenTop._screen_code = "CHC";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:
                    this._icTransScreenTop._screen_code = "CPR";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:
                    this._icTransScreenTop._screen_code = "CPE";
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสาร";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    this._payControl = new SMLERPAPARControl._payControl();
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "DPC";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "CEC";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "CPC";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "CEP";
                    break;
                #endregion
                #region บัตรเครดิต
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน:
                    this._icTransScreenTop._screen_code = "CRD";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก:
                    this._icTransScreenTop._screen_code = "CRN";
                    break;
                #endregion
                #region ฝาก-ถอน-โอน
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                    this._payControl = new SMLERPAPARControl._payControl();
                    this._icTransScreenTop._screen_code = "WM";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                    this._payControl = new SMLERPAPARControl._payControl();
                    this._icTransScreenTop._screen_code = "DM";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร:
                    this._icTransScreenTop._screen_code = "TM";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร_ยกเลิก:
                    this._icTransScreenTop._screen_code = "TMC";
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "DMC";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "WMC";
                    break;
                #endregion
                #region เงินสดย่อย
                case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย:
                    this._payControl = new SMLERPAPARControl._payControl();
                    this._icTransScreenTop._screen_code = "PRM";
                    // Print
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสาร";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    break;
                case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย:
                    this._payControl = new SMLERPAPARControl._payControl();
                    this._icTransScreenTop._screen_code = "PCD";
                    // Print
                    this._printButton.Visible = true;
                    this._printButton.ResourceName = "พิมพ์เอกสาร";
                    this._printButton.Click += new EventHandler(_printButton_Click);
                    break;
                case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "PEC";
                    break;
                case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย_ยกเลิก:
                    this._tab.Visible = false;
                    this._icTransItemGrid.Visible = false;
                    this._icTransScreenTop._screen_code = "PMC";
                    break;

                #endregion
                case _g.g._transControlTypeEnum.สินค้า_บันทึกการจัดส่ง:
                    this._icTransScreenTop._screen_code = "SHM";
                    this._printButton.Visible = true;

                    // new print label button
                    MyLib.ToolStripMyButton _printLabelButton = new MyLib.ToolStripMyButton();
                    _printLabelButton.ResourceName = "พิมพ์ใบปะหน้ากล่อง";
                    _printLabelButton.Text = "พิมพ์ใบปะหน้ากล่อง";
                    _printLabelButton.Click -= _printLabelButton_Click;
                    _printLabelButton.Click += _printLabelButton_Click;
                    _printLabelButton.Image = global::SMLInventoryControl.Properties.Resources.printer;
                    _printLabelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
                    _printLabelButton.Name = "_printLabelButton";

                    this._myToolBar.Items.Add(_printLabelButton);

                    break;

                default:
                    this._mySelectBar.Visible = true;
                    this._itemApprovalSelectButton.Visible = false;
                    this._selectAllButton.Visible = false;
                    this._itemApprovalButton.Visible = false;
                    this._addButton.Visible = false;
                    this._itemApprovalSelectButton.Click += new EventHandler(_itemApprovalSelectButton_Click);
                    this._addButton.Click += new EventHandler(_addButton_Click);
                    break;
            }
            /*else
            {
                this.splitContainer1.Panel2Collapsed = true;
            }*/
            // Event
            this._icTransItemGrid._afterCalcTotal += new MyLib.AfterCalcTotalEventHandler(_ictransItemGrid__afterCalcTotal);
            this._icTransItemGrid._icTransRef = this._icTransRef;
            this._icTransScreenBottom._screenTopRef = this._icTransScreenTop;
            this._icTransScreenTop._comboBoxSelectIndexChanged += new MyLib.ComboBoxSelectIndexChangedHandler(_ictransScreenTop__comboBoxSelectIndexChanged);
            this._icTransScreenBottom._afterCalc += new _icTransScreenBottomControl._afterCalcEvent(_icTransScreenBottom__afterCalc);
            this._icTransScreenBottom._advanceAmount += new _icTransScreenBottomControl._advanceAmountEvent(_icTransScreenBottom__advanceAmount);
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
                    this._vatBuy._getCustCode += new SMLERPGLControl._vatBuy._getCustCodeEventHandler(_vat__getCustCode);
                }
                else
                    if (this._vatSale != null)
                {
                    this._vatSale.Dock = DockStyle.Fill;
                    this._tab.TabPages[__tabVatCode].Controls.Add(this._vatSale);
                    this._vatSale._vatRequest += new SMLERPGLControl._vatSale.VatRequestDataEventHandler(_vatSale__vatRequest);
                    this._vatSale._getCustCode += new SMLERPGLControl._vatSale._getCustCodeEventHandler(_vat__getCustCode);
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
                this._payControl._getTotalAmount += new SMLERPAPARControl._payControl._getTotalAmountEvent(_payScreen__getTotalAmount);
                this._payControl._getTotalTaxAmount += new SMLERPAPARControl._payControl._getTotalTaxAmountEvent(_payControl__getTotalTaxAmount);
                this._payControl._payDepositGrid._getCustCode += new SMLERPAPARControl._payDepositAdvanceGridControl._getCustCodeEvent(_payDepositGrid__getCustCode);
                this._payControl._payDepositGrid._getProcessDate += new SMLERPAPARControl._payDepositAdvanceGridControl._getProcessDateEvent(_payDepositGrid__getProcessDate);
                this._payControl._getCustCode += new SMLERPAPARControl._payControl._getCustCodeEvent(_payControl___getCustCode);
                this._tab.TabPages[__tabPayCode].Controls.Add(this._payControl);
                string __tabName = "";
                switch (this._transControlType)
                {
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                    case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                    case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:
                        __tabName = _g.d.ic_trans._tab_pay_out;
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                    case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                    case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                    case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                        __tabName = _g.d.ic_trans._tab_pay_in;
                        break;
                }
                this._tab.TabPages[__tabPayCode].Tag = __tabName;
            }
            // เงินมัดจำ
            string __tabAdvanceCode = "tab_advance";
            if (this._payAdvance == null)
            {
                this._tab.TabPages[__tabAdvanceCode].Dispose();
            }
            else
            {
                this._payAdvance._icTransControlType = this._transControlType;
                switch (this._transControlType)
                {
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                        this._payAdvance._dataGrid._build(_g.g._depositAdvanceEnum.จ่ายเงินมัดจำ);
                        break;
                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                    case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                    case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                        this._payAdvance._dataGrid._build(_g.g._depositAdvanceEnum.รับเงินมัดจำ);
                        break;
                }
                this._payAdvance.Dock = DockStyle.Fill;
                //this._payAdvanceGrid._payAdvanceGrid._getTotalAmount += new SMLERPAPARControl._payControl._getTotalAmountEvent(_payScreen__getTotalAmount);
                //this._payAdvanceGrid._payAdvanceGrid._getTotalTaxAmount += new SMLERPAPARControl._payControl._getTotalTaxAmountEvent(_payControl__getTotalTaxAmount);
                this._payAdvance._dataGrid._getCustCode += new SMLERPAPARControl._payDepositAdvanceGridControl._getCustCodeEvent(_payDepositGrid__getCustCode);
                this._payAdvance._dataGrid._getProcessDate += new SMLERPAPARControl._payDepositAdvanceGridControl._getProcessDateEvent(_payDepositGrid__getProcessDate);
                this._payAdvance._dataGrid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_dataGrid__alterCellUpdate);
                this._payAdvance._dataGrid._refreshData += _dataGrid__refreshData;
                this._tab.TabPages[__tabAdvanceCode].Controls.Add(this._payAdvance);
                this._tab.TabPages[__tabAdvanceCode].Tag = _g.d.ic_trans._tab_advance;
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
                switch (this._transControlType)
                {
                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                    case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                    case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                        __tabName = _g.d.ic_trans._tab_wht_out;
                        break;
                    case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                    case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                        __tabName = _g.d.ic_trans._tab_wht_in;
                        break;
                }
                this._tab.TabPages[__tabWHTCode].Tag = __tabName;
                this._withHoldingTax._mainGrid._afterCalcTotal += new MyLib.AfterCalcTotalEventHandler(_mainGrid__afterCalcTotal);
                this._withHoldingTax._getDueDateEventArgs += _withHoldingTax__getDueDateEventArgs;
            }
            switch (this._transControlType)
            {
                case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก:
                case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด:
                case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา:
                case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน:
                case _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน:
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ:
                case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า:
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ:
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน:
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า:
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน:
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                case _g.g._transControlTypeEnum.คลัง_ปรับปรุงเพิ่มลดสินค้า:
                case _g.g._transControlTypeEnum.คลัง_ยอดคงเหลือสินค้ายกมา:
                case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                case _g.g._transControlTypeEnum.เจ้าหนี้_ตัดหนี้สูญ:
                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_ตัดหนี้สูญ:
                case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย:
                case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินเข้าธนาคาร:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:

                case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                    this._glCreate();
                    break;

            }
            this._tab._getResource();

            if (MyLib._myGlobal._isUserLockDocument == true)
            {

                this._myManageTrans._dataList._buttonUnlockDoc.Visible = true;
                this._myManageTrans._dataList._buttonLockDoc.Visible = true;
                this._myManageTrans._dataList._separatorLockDoc.Visible = true;
            }

            // toe print doc range button
            this._myManageTrans._dataList._printRangeButton.Visible = this._printButton.Visible;
            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA") &&
                (this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ || this._transControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้ || this._transControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้)
                )
            {


                if (_g.g._companyProfile._print_invoice_one_time)
                {
                    this._myManageTrans._dataList._printRangeButton.Visible = false;

                    if (MyLib._myGlobal._isUserResetPrintLog == true)
                        this._myManageTrans._dataList._clearLogPrintButton.Visible = true;
                }
            }
        }

        private void _itemApprovalButton_Click(object sender, EventArgs e)
        {
            if (this._transControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก)
            {
                this._icTransItemGrid._clear();
                this._icTransRef._transGrid._clear();
                this._icTransRef.Visible = (this._icTransRef.Visible) ? false : true;
                this._icTransRef._transGrid._addRow();
                this._icTransRef._transGrid._cellUpdate(0, _g.d.ap_ar_trans_detail._bill_type, 1, true);
                this._icTransRef._transGrid.AddRow = false;
            }

        }

        private void __buttonSearchSR_Click(object sender, EventArgs e)
        {
            // search sr
            MyLib._searchDataFull __searchSRBill = new MyLib._searchDataFull();
            __searchSRBill._name = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า);
            __searchSRBill._dataList._loadViewFormat(__searchSRBill._name, MyLib._myGlobal._userSearchScreenGroup, true);
            __searchSRBill.StartPosition = FormStartPosition.CenterScreen;
            __searchSRBill._dataList._extraWhere = _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า) + " and last_status = 0 and " + _g.d.ic_trans._approve_status + "=1 and " + _g.d.ic_trans._doc_success + "=0 and (select sum(qty*(stand_value/divide_value)) from ic_trans_detail where ic_trans_detail.doc_no = ic_trans.doc_no and ic_trans_detail.trans_flag = ic_trans.trans_flag) >  coalesce((select sum(qty*(stand_value/divide_value)) from ic_trans_detail as refer where refer.trans_flag=6 and refer.ref_doc_no = ic_trans.doc_no and refer.doc_ref_type = 99 and exists (select item_code from ic_trans_detail as detail where detail.doc_no = ic_trans.doc_no and detail.trans_flag = ic_trans.trans_flag and refer.item_code = detail.item_code) ), 0) ";
            __searchSRBill._dataList._refreshData();

            MyLib.ToolStripMyButton __buttonProcessSelected = new MyLib.ToolStripMyButton();
            __buttonProcessSelected.DisplayStyle = ToolStripItemDisplayStyle.Image;
            //SMLInventoryControl.Properties.Resources.flash

            __buttonProcessSelected.Image = (Image)global::SMLInventoryControl.Properties.Resources.flash;
            __buttonProcessSelected.ImageTransparentColor = System.Drawing.Color.Magenta;
            __buttonProcessSelected.Name = "__buttonProcessSelected";
            __buttonProcessSelected.Padding = new System.Windows.Forms.Padding(1);
            __buttonProcessSelected.ResourceName = "";
            __buttonProcessSelected.Size = new System.Drawing.Size(30, 22);
            __buttonProcessSelected.Text = "Process Selected";
            __buttonProcessSelected.ToolTipText = "Process Selected";
            __buttonProcessSelected.Click += (s1, e1) =>
            {
                bool __first = true;
                for (int __row = 0; __row < __searchSRBill._dataList._gridData._rowData.Count; __row++)
                {
                    if (__searchSRBill._dataList._gridData._cellGet(__row, "check").ToString().Equals("1"))
                    {
                        this._icTransItemGrid._clear();
                        MyLib._myGrid __grid = __searchSRBill._dataList._gridData;

                        string __docNo = __grid._cellGet(__row, _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no).ToString();
                        DateTime __docDate = MyLib._myGlobal._convertDateFromQuery(__grid._cellGet(__row, _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date).ToString());

                        int __searchResult = this._icTransRef._transGrid._findData(this._icTransRef._transGrid._findColumnByName(_g.d.ap_ar_trans_detail._billing_no), __docNo);

                        if (__searchResult == -1)
                        {

                            this._icTransRef._transGrid._selectRow = this._icTransRef._transGrid._addRow();

                            this._icTransRef._transGrid._cellUpdate(this._icTransRef._transGrid._selectRow, _g.d.ap_ar_trans_detail._bill_type, 99, true);
                            this._icTransRef._transGrid._cellUpdate(this._icTransRef._transGrid._selectRow, _g.d.ap_ar_trans_detail._billing_no, __docNo, true);
                            this._icTransRef._transGrid._cellUpdate(this._icTransRef._transGrid._selectRow, _g.d.ap_ar_trans_detail._billing_date, __docDate, true);
                            // this._icTransRef._transGrid._cellUpdate(this._icTransRef._transGrid._selectRow, _g.d.ap_ar_trans_detail._remark, __docDate, true);

                            string __qtyQuery = "(qty*(stand_value/divide_value) - (coalesce((select sum(qty * (stand_value / divide_value)) from ic_trans_detail as refer where refer.trans_flag = 6 and refer.ref_doc_no = ic_trans_detail.doc_no and refer.doc_ref_type = 99 and refer.item_code = ic_trans_detail.item_code  ), 0)) )";
                            // add detail grid
                            StringBuilder __query = new StringBuilder();
                            __query.Append("select doc_no, item_code, item_name, ((" + __qtyQuery + ")/(stand_value/divide_value )) as qty, unit_code, wh_code, shelf_code, line_number from ic_trans_detail where doc_no = \'" + __docNo + "\' and trans_flag = 34 order by line_number ");

                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            DataTable __result = __myFrameWork._queryShort(__query.ToString()).Tables[0];

                            for (int __rowData = 0; __rowData < __result.Rows.Count; __rowData++)
                            {
                                string __itemCode = __result.Rows[__rowData][_g.d.ic_trans_detail._item_code].ToString();
                                string __itemName = __result.Rows[__rowData][_g.d.ic_trans_detail._item_name].ToString();
                                string __refDocNo = __result.Rows[__rowData][_g.d.ic_trans_detail._doc_no].ToString();
                                string __unitCode = __result.Rows[__rowData][_g.d.ic_trans_detail._unit_code].ToString();
                                string __wareHouseCode = __result.Rows[__rowData][_g.d.ic_trans_detail._wh_code].ToString();
                                string __shelfCode = __result.Rows[__rowData][_g.d.ic_trans_detail._shelf_code].ToString();
                                decimal __qty = MyLib._myGlobal._decimalPhase(__result.Rows[__rowData][_g.d.ic_trans_detail._qty].ToString());
                                int __line_number = MyLib._myGlobal._intPhase(__result.Rows[__rowData]["line_number"].ToString());

                                if (__qty != 0)
                                {
                                    int __addr = this._icTransItemGrid._addRow();
                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._doc_ref_type, 99, false);
                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._ref_doc_no, __refDocNo, false);
                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, false);
                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __itemName, false);

                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, false);
                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code, __wareHouseCode, false);
                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code, __shelfCode, false);
                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._qty, __qty, true);

                                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._ref_row, __line_number, true);

                                    //this._searchUnitNameWareHouseNameShelfName(__addr);

                                    //this._calcItemPrice(__addr, __addr, this._findColumnByName(_g.d.ic_trans_detail._discount));
                                }
                            }
                            this._icTransItemGrid._searchUnitNameWareHouseNameShelfNameAll();
                        }

                    }
                }
                __searchSRBill.Close();
                SendKeys.Send("{TAB}");

            };
            __searchSRBill._dataList._button.Items.Add(__buttonProcessSelected);
            __searchSRBill.ShowDialog();
        }

        private void _dataGrid__refreshData(SMLERPAPARControl._payDepositAdvanceGridControl sender)
        {
            this._icTransScreenBottom._calc(this._icTransItemGrid);
        }

        private void _printSelectRecordButton_Click(object sender, EventArgs e)
        {
            if (this._myManageTrans._mode == 2)
            {
                int __docNoColumnIndex = this._withHoldingTax._mainGrid._findColumnByName(_g.d.gl_wht_list._tax_doc_no);
                string __getDocNo = this._withHoldingTax._mainGrid._cellGet(this._withHoldingTax._mainGrid._selectRow, __docNoColumnIndex).ToString();
                bool __printResult = SMLERPReportTool._global._printForm(this._icTransScreenTop._docFormatCode, __getDocNo, this._getTransFlag.ToString(), false);

            }
            else
            {
                MessageBox.Show(MyLib._myGlobal._resource("ยังไม่มีการบันทึกเอกสาร"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DateTime _withHoldingTax__getDueDateEventArgs()
        {
            return this._icTransScreenTop._getDataDate(_g.d.ic_trans._doc_date);
        }

        private void _shipmentControl__afterSelectAddress(object sender, string roworder, DataRow rowdata)
        {
            if (this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ && MyLib._myGlobal._OEMVersion == "SINGHA")
            {
                // update tax id , branch_type for singha
                int __count = 0;
                for (int __row = 0; __row < this._vatSale._vatGrid._rowData.Count; __row++)
                {
                    if (this._vatSale._vatGrid._cellGet(__row, _g.d.gl_journal_vat_sale._vat_number).ToString().Trim().Length > 0)
                    {
                        __count++;
                    }
                }
                if (__count == 1)
                {
                    // กรณีมีบรรทัดเดียว ให้ดึงค่าต่างๆ ลงมาใส่โดยอัตโนมัติ
                    if (rowdata[_g.d.ap_ar_transport_label._tax_id].ToString().Length > 0)
                    {
                        this._vatSale._manualTaxID = true;
                        this._vatSale._vatGrid._cellUpdate(0, _g.d.gl_journal_vat_sale._ar_name, rowdata[_g.d.ap_ar_transport_label._name_1].ToString(), false);
                        this._vatSale._vatGrid._cellUpdate(0, _g.d.gl_journal_vat_sale._branch_type, MyLib._myGlobal._intPhase(rowdata[_g.d.ap_ar_transport_label._branch_type].ToString()), false);
                        this._vatSale._vatGrid._cellUpdate(0, _g.d.gl_journal_vat_sale._branch_code, rowdata[_g.d.ap_ar_transport_label._branch_code].ToString(), false);
                        this._vatSale._vatGrid._cellUpdate(0, _g.d.gl_journal_vat_sale._tax_no, rowdata[_g.d.ap_ar_transport_label._tax_id].ToString(), false);
                    }

                    /*this._vatGrid._cellUpdate(__row, _g.d.gl_journal_vat_sale._ar_name, __data.Rows[0][_g.d.ar_customer._name_1].ToString(), true);
                    this._vatGrid._cellUpdate(__row, _g.d.gl_journal_vat_sale._branch_type, MyLib._myGlobal._intPhase(__data.Rows[0][_g.d.ar_customer_detail._branch_type].ToString()), true);
                    this._vatGrid._cellUpdate(__row, _g.d.gl_journal_vat_sale._branch_code, __data.Rows[0][_g.d.ar_customer_detail._branch_code].ToString(), true);
                    this._vatGrid._cellUpdate(__row, _g.d.gl_journal_vat_sale._tax_no, __data.Rows[0][_g.d.ar_customer_detail._tax_id].ToString(), true);*;
                    */

                    this._vatSale._vatGrid.Invalidate();
                }
            }
        }

        void _printWithHoldingTagButton_Click(object sender, EventArgs e)
        {
            if (this._myManageTrans._mode == 2)
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
                    __condition.Add(new SMLERPReportTool._ReportToolCondition(_g.d.ic_trans._trans_flag, this._getTransFlag.ToString()));
                    __printList.Add(__condition.ToArray());

                    // pack doc_no List 

                    if (__packDocNoList.Length > 0)
                    {
                        __packDocNoList.Append(",");
                    }
                    __packDocNoList.Append("\'" + __getDocNo + "\'");
                }
                // ใส่ค่าว่างไปก่อน ค่อย query ดึงจากรหัสหน้าจอเอา
                string __queryGetDocFormat = "select " + _g.d.erp_doc_format._form_code + " from " + _g.d.erp_doc_format._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_doc_format._code) + "=\'" + this._icTransScreenTop._docFormatCode + "\'";
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

        string _shipmentControl__getCustCode()
        {
            string __getCustCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
            if (__getCustCode.Length == 0)
            {
                MessageBox.Show(MyLib._myGlobal._resource("ยังไม่ได้ใส่รหัสเจ้าหนี้"));
            }

            return __getCustCode;
        }


        _labelPrintForm _labelPrintForm;
        void _printLabelButton_Click(object sender, EventArgs e)
        {
            if (_labelPrintForm == null)
            {
                int __formType = 0;
                if (this._getTransType == 2)
                {
                    __formType = 1;
                }

                _labelPrintForm = new _labelPrintForm(__formType);
            }
            _labelPrintForm._load(this._oldDocNo, this._getTransFlag);
            _labelPrintForm.StartPosition = FormStartPosition.CenterScreen;
            _labelPrintForm.ShowDialog();
        }

        void _icTransScreenTop__selectInvoceFromPos()
        {
            // เลือก invoice จากระบบ pos
            _selectBillFromPosForm __selectBillFromPos = new _selectBillFromPosForm();
            if (this._transControlType == _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ)
            {
                __selectBillFromPos.label1.Text = "เลขที่เอกสาร";
                __selectBillFromPos.Text = "เรียกรายการโอนสินค้า";
                __selectBillFromPos._extraWhere = " and trans_flag = 70 ";
                __selectBillFromPos._selectInvoce += new _selectBillFromPosForm.SelectInvoiceEventHandler(__selectBillFromtranferBill);

            }
            else if (this._transControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก)
            {
                __selectBillFromPos.label1.Text = "เลขที่เอกสาร";
                __selectBillFromPos.Text = "เรียกรายการขอเบิกสินค้า";
                __selectBillFromPos._extraWhere = " and trans_flag = 122 ";
                __selectBillFromPos._selectInvoce += new _selectBillFromPosForm.SelectInvoiceEventHandler(__selectBillFromtranferBill);
            }
            else
            {
                __selectBillFromPos._selectInvoce += new _selectBillFromPosForm.SelectInvoiceEventHandler(__selectBillFromPos__selectInvoce);
            }
            __selectBillFromPos.ShowDialog();
        }

        void __selectBillFromtranferBill(string docNo)
        {
            string __transFlag = "70";
            string __fromWH = this._icTransScreenTop._getDataStr(_g.d.ic_trans._wh_from);
            string __fromLC = this._icTransScreenTop._getDataStr(_g.d.ic_trans._location_from);
            string __toWH = this._icTransScreenTop._getDataStr(_g.d.ic_trans._wh_to);
            string __toLC = this._icTransScreenTop._getDataStr(_g.d.ic_trans._location_to);

            string __getWHQuery = _g.d.ic_trans_detail._wh_code + "," +
                "(select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code + "=" + _g.d.ic_trans_detail._wh_code + ") as " + _g.d.ic_trans_detail._wh_name;


            string __GetShelfQuery = _g.d.ic_trans_detail._shelf_code + "," +
                "(select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._whcode + "=" + _g.d.ic_trans_detail._wh_code + " and " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code + "=" + _g.d.ic_trans_detail._shelf_code + ") as " + _g.d.ic_trans_detail._shelf_name;

            if (this._transControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก)
            {
                __transFlag = "122";

                if (__fromWH.Length > 0)
                {
                    __getWHQuery = "\'" + __fromWH + "\' as " + _g.d.ic_trans_detail._wh_code + "," +
                    "(select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code + "=\'" + __fromWH + "\') as " + _g.d.ic_trans_detail._wh_name + "," +
                    "\'" + __toWH + "\' as " + _g.d.ic_trans_detail._wh_code_2 + "," +
                    "(select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code + "=\'" + __toWH + "\') as " + _g.d.ic_trans_detail._wh_name_2;

                    __GetShelfQuery = "\'" + __fromLC + "\' as " + _g.d.ic_trans_detail._shelf_code + "," +
                    "(select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._whcode + "=\'" + __fromWH + "\' and " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code + "=\'" + __fromLC + "\' ) as " + _g.d.ic_trans_detail._shelf_name + "," +
                    "\'" + __toLC + "\' as " + _g.d.ic_trans_detail._shelf_code_2 + "," +
                    "(select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._whcode + "=\'" + __toWH + "\' and " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code + "=\'" + __toLC + "\' ) as " + _g.d.ic_trans_detail._shelf_name_2;
                }
            }

            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans_detail._average_cost + "," +
                    "(select " + _g.d.ic_unit_use._stand_value + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=" + this._icTransDetailTable + "." + _g.d.ic_trans_detail._item_code + " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=(select " + _g.d.ic_inventory._unit_cost + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + this._icTransDetailTable + "." + _g.d.ic_trans_detail._item_code + ")" + ") as " + this._icTransItemGrid._columnAverageCostUnitStand + "," +
                    "(select " + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=" + this._icTransDetailTable + "." + _g.d.ic_trans_detail._item_code + " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=(select " + _g.d.ic_inventory._unit_cost + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + this._icTransDetailTable + "." + _g.d.ic_trans_detail._item_code + ")" + ") as " + this._icTransItemGrid._columnAverageCostUnitDiv + "," +
                    _g.d.ic_trans_detail._trans_type + "," +
                    _g.d.ic_trans_detail._trans_flag + "," +
                    _g.d.ic_trans_detail._doc_group + "," +
                    _g.d.ic_trans_detail._doc_date + "," +
                    _g.d.ic_trans_detail._doc_date_calc + "," +
                    _g.d.ic_trans_detail._doc_time_calc + "," +
                    _g.d.ic_trans_detail._doc_no + "," +
                    _g.d.ic_trans_detail._doc_ref + "," +
                    _g.d.ic_trans_detail._cust_code + "," +
                    _g.d.ic_trans_detail._inquiry_type + "," +
                    _g.d.ic_trans_detail._item_code_main + "," +
                    _g.d.ic_trans_detail._chq_number + "," +
                    _g.d.ic_trans_detail._bank_branch + "," +
                    _g.d.ic_trans_detail._bank_name + "," +
                    _g.d.ic_trans_detail._item_code_2 + "," +
                    _g.d.ic_trans_detail._bank_name_2 + "," +
                    _g.d.ic_trans_detail._bank_branch_2 + "," +
                    _g.d.ic_trans_detail._tax_type + "," +
                    _g.d.ic_trans_detail._item_code + "," +
                    _g.d.ic_trans_detail._barcode + "," +
                    _g.d.ic_trans_detail._item_name + "," +
                    _g.d.ic_trans_detail._date_expire + "," +
                    _g.d.ic_trans_detail._unit_code + "," +
                    "(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_trans_detail._unit_code + ") as " + _g.d.ic_trans_detail._unit_name + "," +
                    _g.d.ic_trans_detail._qty + "," +
                    _g.d.ic_trans_detail._price + "," +
                    _g.d.ic_trans_detail._discount + "," +
                    _g.d.ic_trans_detail._sum_of_cost + "," +
                    _g.d.ic_trans_detail._is_permium + "," +
                    _g.d.ic_trans_detail._sum_amount + "," +
                    _g.d.ic_trans_detail._due_date + "," +
                    _g.d.ic_trans_detail._remark + "," +
                    _g.d.ic_trans_detail._user_approve + "," +
                    _g.d.ic_trans_detail._price_type + "," +
                    _g.d.ic_trans_detail._price_mode + "," +
                    _g.d.ic_trans_detail._status + "," +
                    _g.d.ic_trans_detail._is_get_price + "," +
                    _g.d.ic_trans_detail._line_number + "," +
                    _g.d.ic_trans_detail._date_due + "," +
                    _g.d.ic_trans_detail._ref_doc_no + "," +
                    _g.d.ic_trans_detail._ref_doc_date + "," +
                    _g.d.ic_trans_detail._ref_line_number + "," +
                    _g.d.ic_trans_detail._ref_cust_code + "," +
                    _g.d.ic_trans_detail._branch_code + "," +
                    __getWHQuery + "," +
                    __GetShelfQuery + "," +
                    _g.d.ic_trans_detail._department_code + "," +
                    _g.d.ic_trans_detail._total_vat_value + "," +
                    _g.d.ic_trans_detail._cancel_qty + "," +
                    _g.d.ic_trans_detail._total_qty + "," +
                    _g.d.ic_trans_detail._stand_value + "," +
                    _g.d.ic_trans_detail._divide_value + "," +
                    _g.d.ic_trans_detail._ratio + "," +
                    _g.d.ic_trans_detail._ic_pattern + "," +
                    _g.d.ic_trans_detail._ic_color + "," +
                    _g.d.ic_trans_detail._ic_size + "," +
                    _g.d.ic_trans_detail._is_serial_number + "," +
                    _g.d.ic_trans_detail._item_type + "," +
                    "(select " + _g.d.ic_inventory._unit_type + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + this._icTransDetailTable + "." + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_trans_detail._unit_type + "," +
                    "(select " + _g.d.ic_inventory._cost_type + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + this._icTransDetailTable + "." + _g.d.ic_trans_detail._item_code + ") as " + this._icTransItemGrid._columnCostType + "," +
                    _g.d.ic_trans_detail._set_ref_line + "," +
                    _g.d.ic_trans_detail._set_ref_price + "," +
                    _g.d.ic_trans_detail._set_ref_qty + "," +
                    _g.d.ic_trans_detail._ref_row + "," +
                    _g.d.ic_trans_detail._hidden_cost_1 + "," +
                    _g.d.ic_trans_detail._sum_amount_exclude_vat + "," +
                    _g.d.ic_trans_detail._hidden_cost_1_exclude_vat + "," +
                    _g.d.ic_trans_detail._price_exclude_vat + "," +
                    _g.d.ic_trans_detail._discount_amount + "," +
                    _g.d.ic_trans_detail._temp_float_1 + "," +
                    _g.d.ic_trans_detail._temp_float_2 + "," +
                    _g.d.ic_trans_detail._temp_string_1 + "," +
                    _g.d.ic_trans_detail._doc_ref_type + "," +
                    _g.d.ic_trans_detail._ref_guid + "," +
                    _g.d.ic_trans_detail._lot_number_1 + "," +
                    _g.d.ic_trans_detail._mfd_date + "," +
                    _g.d.ic_trans_detail._mfn_name + "," +
                    _g.d.ic_trans_detail._priority_level +
                    " from " + this._icTransDetailTable + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + docNo + "\'  and " + _g.d.ic_trans_detail._trans_flag + "= " + __transFlag +
                    " order by " + _g.d.ic_trans_detail._line_number));
            __query.Append("</node>");

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
            if (__result.Count > 0)
            {
                //this._icTransScreenTop._loadData(((DataSet)__getData[0]).Tables[0]);
                this._icTransItemGrid._loadFromDataTable(((DataSet)__result[0]).Tables[0]);


            }
            else
            {
                MessageBox.Show("ไม่พบเอกสารเลขที่" + " : " + docNo);
            }

        }

        void __selectBillFromPos__selectInvoce(string docNo)
        {
            this._loadDataToScreen(docNo, " where " + _g.d.ic_trans._doc_no + "=\'" + docNo.ToUpper() + "\'");
            this._icTransScreenTop._setDataStr(_g.d.ic_trans._doc_no, "");
            this._icTransScreenTop._setDataStr(_g.d.ic_trans._tax_doc_no, "");
            this._icTransScreenTop._setDataStr(_g.d.ic_trans._doc_ref, docNo.ToUpper());
            DateTime __refDate = MyLib._myGlobal._convertDate(this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_date));
            this._icTransScreenTop._setDataStr(_g.d.ic_trans._doc_ref, docNo.ToUpper());
            this._icTransScreenTop._setDataDate(_g.d.ic_trans._doc_ref_date, __refDate);
            this._icTransScreenBottom._setCheckBox(_g.d.ic_trans._total_manual, "1");
            /*MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __query = new StringBuilder();
            __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + docNo.ToUpper() + "\'"));
            //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_trans_detail._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans_detail._doc_no) + "=\'" + docNo.ToUpper() + "\' order by " + _g.d.ic_trans_detail._line_number));
            __query.Append("</node>");
            ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
            if (__getData.Count > 0)
            {
                //this._icTransScreenTop._loadData(((DataSet)__getData[0]).Tables[0]);
                //this._icTransItemGrid._loadFromDataTable(((DataSet)__getData[1]).Tables[0]);
            }
            else
            {
                MessageBox.Show("ไม่พบเอกสารเลขที่" + " : " + docNo);
            }*/
            this._fullInvoiceFromPOS = true;
        }

        decimal _icTransScreenBottom__advanceAmount(_g.g._vatTypeEnum vatType)
        {
            decimal __totalAdvance = 0M;
            if (this._payAdvance != null)
            {
                decimal __vatRate = this._icTransItemGrid__vatRate();
                this._payAdvance._dataGrid._calcTotal(false);
                __totalAdvance = ((MyLib._myGrid._columnType)this._payAdvance._dataGrid._columnList[this._payAdvance._dataGrid._findColumnByName(_g.d.cb_trans_detail._amount)])._total;
                switch (vatType)
                {
                    case _g.g._vatTypeEnum.ภาษีแยกนอก:
                        __totalAdvance = MyLib._myGlobal._round(__totalAdvance * 100.0M / (__vatRate + 100.0M), _g.g._companyProfile._item_amount_decimal);
                        break;
                }
            }
            return __totalAdvance;
        }

        void _dataGrid__alterCellUpdate(object sender, int row, int column)
        {
            this._icTransScreenBottom._calc(this._icTransItemGrid);
        }

        SMLERPGLControl._vatRequestData _vatSale__vatRequest()
        {
            SMLERPGLControl._vatRequestData __result = new SMLERPGLControl._vatRequestData();
            __result._vatDocNo = this._icTransScreenTop._getDataStr(_g.d.ic_trans._tax_doc_no);
            string __taxDocDate = this._icTransScreenTop._getDataStr(_g.d.ic_trans._tax_doc_date).ToString();
            __result._vatDate = MyLib._myGlobal._convertDate(__taxDocDate);
            __result._vatRate = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._vat_rate);
            __result._vatBaseAmount = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_before_vat);
            __result._vatAmount = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_vat_value);
            __result._vatTotalAmount = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_after_vat);
            __result._vatExceptAmount = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_except_vat);
            return __result;
        }

        string _vat__getCustCode()
        {
            return this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
        }

        SMLERPGLControl._vatRequestData _vatBuy__vatRequest()
        {
            SMLERPGLControl._vatRequestData __result = new SMLERPGLControl._vatRequestData();
            __result._vatDocNo = this._icTransScreenTop._getDataStr(_g.d.ic_trans._tax_doc_no);
            string __taxDocDate = this._icTransScreenTop._getDataStr(_g.d.ic_trans._tax_doc_date).ToString();
            __result._vatDate = MyLib._myGlobal._convertDate(__taxDocDate);
            __result._vatRate = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._vat_rate);
            __result._vatBaseAmount = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_before_vat);
            __result._vatAmount = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_vat_value);
            __result._vatTotalAmount = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_after_vat);
            __result._vatExceptAmount = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_except_vat);
            return __result;
        }

        string _payControl___getCustCode()
        {
            return this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
        }

        DateTime _payDepositGrid__getProcessDate()
        {
            return MyLib._myGlobal._convertDate(this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_date));
        }

        string _payDepositGrid__getCustCode()
        {
            return this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
        }

        void _mainGrid__afterCalcTotal(object sender)
        {
            if (this._payControl != null)
            {
                this._withHoldingTax._mainGrid._calcTotal(false);
                this._payControl._reCalc();
            }
        }

        void _icTransScreenBottom__afterCalc()
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

        decimal _payScreen__getTotalAmount()
        {
            // ดึงยอดรวมไปหน้าจอจ่ายเงิน
            return this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_amount);
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

        void _printButton_Click(object sender, EventArgs e)
        {
            if (this._myManageTrans._mode == 2)
            {


                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    _showPrintDialogByCtrl = true;
                }

                try
                {
                    _printFormData(this._icTransScreenTop._docFormatCode);
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                MessageBox.Show(MyLib._myGlobal._resource("ยังไม่มีการบันทึกเอกสาร"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _showPrintDialogByCtrl = false;
        }

        void _ictransScreenTop__comboBoxSelectIndexChanged(object sender, string name)
        {
            switch (this._transControlType)
            {
                case _g.g._transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ:
                    break;
                default:
                    this._icTransScreenBottom._vatType = this._vatType;
                    break;
            }
        }

        void _ictransScreenTop__saveKeyDown(object sender)
        {
            this._saveData();
        }

        void _calc(object sender)
        {
            // คำนวณยอดรวม
            MyLib._myGrid __sender = (MyLib._myGrid)sender;
            // คำนวณยอดรวมก่อน แล้วค่อยคำนวณอันอื่นต่อ
            __sender._calcTotal(false);
            //
            int __columnTotalSumAmount = __sender._findColumnByName(_g.d.ic_trans_detail._sum_amount);

            // multicurrency 
            if (_g.g._companyProfile._multi_currency == true)
            {
                int __columnTotalSumAmount2 = __sender._findColumnByName(_g.d.ic_trans_detail._sum_amount_2);
                if (__columnTotalSumAmount2 != -1)
                {
                    __columnTotalSumAmount = __columnTotalSumAmount2;
                }
            }


            if (__columnTotalSumAmount != -1)
            {
                switch (this._transControlType)
                {
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                        {
                            MyLib._myGrid._columnType __getColumn = (MyLib._myGrid._columnType)__sender._columnList[__columnTotalSumAmount];
                            decimal __getValue = __getColumn._total;
                            decimal __getOldValue = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_amount);
                            if (__getValue != __getOldValue)
                            {
                                this._icTransScreenBottom._setDataNumber(_g.d.ic_trans._total_amount, __getValue);
                                this._icTransScreenBottom.Invalidate();
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                        {
                            MyLib._myGrid._columnType __getColumn = (MyLib._myGrid._columnType)__sender._columnList[__columnTotalSumAmount];
                            decimal __getValue = __getColumn._total;
                            decimal __getOldValue = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_amount);
                            if (__getValue != __getOldValue)
                            {
                                this._icTransScreenBottom._setDataNumber(_g.d.ic_trans._total_amount, __getValue);
                                this._icTransScreenBottom.Invalidate();
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                        {
                            MyLib._myGrid._columnType __getColumn = (MyLib._myGrid._columnType)__sender._columnList[__columnTotalSumAmount];
                            decimal __getValue = __getColumn._total;
                            decimal __getOldValue = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_amount);
                            if (__getValue != __getOldValue)
                            {
                                this._icTransScreenBottom._setDataNumber(_g.d.ic_trans._total_amount, __getValue);
                                this._icTransScreenBottom.Invalidate();
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย:
                    case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย:
                        {
                            MyLib._myGrid._columnType __getColumn = (MyLib._myGrid._columnType)__sender._columnList[__columnTotalSumAmount];
                            decimal __getValue = __getColumn._total;
                            decimal __getOldValue = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_amount);
                            if (__getValue != __getOldValue)
                            {
                                this._icTransScreenBottom._setDataNumber(_g.d.ic_trans._total_amount, __getValue);
                                this._icTransScreenBottom.Invalidate();
                            }
                        }
                        break;
                    default:
                        {
                            decimal __vatRate = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._vat_rate);
                            // ปรับยอดรวมถอด vat
                            int __columnSumAmountExcludeVat = __sender._findColumnByName(_g.d.ic_trans_detail._sum_amount_exclude_vat);

                            for (int __row = 0; __row < __sender._rowData.Count; __row++)
                            {
                                decimal __sumAmount = MyLib._myGlobal._decimalPhase(__sender._cellGet(__row, __columnTotalSumAmount).ToString());
                                int __taxType = MyLib._myGlobal._intPhase(__sender._cellGet(__row, _g.d.ic_trans_detail._tax_type).ToString());

                                if (_g.g._companyProfile._multi_currency == true)
                                {
                                    __sumAmount = MyLib._myGlobal._decimalPhase(__sender._cellGet(__row, _g.d.ic_trans_detail._sum_amount).ToString());
                                }

                                decimal __sumAmountExcludeVat = __sumAmount;
                                if (this._vatType == _g.g._vatTypeEnum.ภาษีรวมใน && __taxType == 0)
                                {
                                    __sumAmountExcludeVat = Math.Round((__sumAmount * 100M) / (100M + __vatRate), _g.g._companyProfile._item_amount_decimal);
                                }
                                __sender._cellUpdate(__row, __columnSumAmountExcludeVat, __sumAmountExcludeVat, false);
                            }
                            // ปรับยอดรวมถอด vat (ต้นทุนแฝง)
                            int __columnHiddenCost = __sender._findColumnByName(_g.d.ic_trans_detail._hidden_cost_1);
                            if (__columnHiddenCost != -1)
                            {
                                int __columnHiddenExcludeVat = __sender._findColumnByName(_g.d.ic_trans_detail._hidden_cost_1_exclude_vat);
                                for (int __row = 0; __row < __sender._rowData.Count; __row++)
                                {
                                    decimal __sumAmount = MyLib._myGlobal._decimalPhase(__sender._cellGet(__row, __columnHiddenCost).ToString());
                                    decimal __sumAmountExcludeVat = __sumAmount;
                                    if (this._vatType == _g.g._vatTypeEnum.ภาษีรวมใน)
                                    {
                                        __sumAmountExcludeVat = Math.Round((__sumAmount * 100M) / (100M + __vatRate), _g.g._companyProfile._item_amount_decimal);
                                    }
                                    __sender._cellUpdate(__row, __columnHiddenExcludeVat, __sumAmountExcludeVat, false);
                                }
                            }
                            //
                            MyLib._myGrid._columnType __getColumn = (MyLib._myGrid._columnType)__sender._columnList[__columnTotalSumAmount];
                            decimal __getValue = __getColumn._total;
                            if (__getValue != this._icTransScreenTop._getDataNumber(_g.d.ic_trans._total_amount))
                            {
                                this._icTransScreenTop._setDataNumber(_g.d.ic_trans._total_amount, __getValue);
                                this._icTransScreenTop.Invalidate();
                            }
                            // รวมใหม่ตามเงื่อนไข
                            if (this._icTransScreenBottom.Visible == true)
                            {
                                /*decimal _totalAmountForVat = 0; // ยอดสินค้ามีภาษี
                                decimal _totalAmountForExceptVat = 0; // ยอดสินค้ายกเว้นภาษี
                                int __columnItemType = __sender._findColumnByName(_g.d.ic_trans_detail._item_type);
                                for (int __row = 0; __row < __sender._rowData.Count; __row++)
                                {
                                    if ((int)__sender._cellGet(__row, __columnItemType) != 3 && (int)__sender._cellGet(__row, __columnItemType) != 5)
                                    {
                                        decimal __getValueFromGrid = (decimal)__sender._cellGet(__row, __columnTotalSumAmount);
                                        // ต้องกลับมาแก้ flag -- jead
                                        _totalAmountForVat += __getValueFromGrid;
                                    }
                                }
                                this._icTransScreenBottom._setDataNumber(_g.d.ic_trans._total_value, _totalAmountForVat);
                                this._icTransScreenBottom._setDataNumber(_g.d.ic_trans._total_except_vat, _totalAmountForExceptVat);*/
                                __sender._calcTotal(false);
                                this._icTransScreenBottom._calc(this._icTransItemGrid);
                                this._icTransScreenBottom.Invalidate();
                            }
                            // คำนวณกรณีอ้างบืล
                            switch (this._transControlType)
                            {
                                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                                case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                                case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                                    {
                                        for (int __row = 0; __row < this._icTransRef._transGrid._rowData.Count; __row++)
                                        {
                                            string __refDocNo = this._icTransRef._transGrid._cellGet(__row, _g.d.ap_ar_trans_detail._billing_no).ToString().Trim();
                                            decimal __balance = MyLib._myGlobal._decimalPhase(this._icTransRef._transGrid._cellGet(__row, _g.d.ap_ar_trans_detail._sum_debt_balance).ToString());
                                            int __item_typeColumnNumber = this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._item_type);
                                            int __taxTypeColumnNumber = this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._tax_type);
                                            decimal __sumRefAmount = 0.0M;
                                            for (int __loop = 0; __loop < this._icTransItemGrid._rowData.Count; __loop++)
                                            {
                                                string __docNo = this._icTransItemGrid._cellGet(__loop, _g.d.ic_trans_detail._ref_doc_no).ToString().Trim();
                                                int __item_type = (__item_typeColumnNumber == -1) ? 0 : (int)this._icTransItemGrid._cellGet(__loop, __item_typeColumnNumber);
                                                int __tax_type = (__taxTypeColumnNumber == -1) ? 0 : (int)this._icTransItemGrid._cellGet(__loop, __taxTypeColumnNumber);
                                                if (__refDocNo.Equals(__docNo) && (__item_type != 3 && __item_type != 5))
                                                {
                                                    //decimal __sumAmount = MyLib._myGlobal._decimalPhase(this._icTransItemGrid._cellGet(__loop, _g.d.ic_trans_detail._sum_amount).ToString());
                                                    switch (this._vatTypeNumber())
                                                    {
                                                        case 0:
                                                            {
                                                                // แยกนอก
                                                                decimal __sumAmount = MyLib._myGlobal._decimalPhase(this._icTransItemGrid._cellGet(__loop, _g.d.ic_trans_detail._sum_amount_exclude_vat).ToString());
                                                                if (__tax_type == 0)
                                                                {
                                                                    __sumAmount += (__sumAmount * (__vatRate / 100M));
                                                                }
                                                                __sumRefAmount += __sumAmount;
                                                            }
                                                            break;
                                                        case 1:
                                                        case 2: // toe
                                                            {
                                                                // รวมใน
                                                                decimal __sumAmount = MyLib._myGlobal._decimalPhase(this._icTransItemGrid._cellGet(__loop, _g.d.ic_trans_detail._sum_amount).ToString());
                                                                __sumRefAmount += __sumAmount;
                                                            }
                                                            break;
                                                    }
                                                }
                                            }

                                            __sumRefAmount = MyLib._myGlobal._round(__sumRefAmount, _g.g._companyProfile._item_amount_decimal);

                                            this._icTransRef._transGrid._cellUpdate(__row, _g.d.ap_ar_trans_detail._sum_debt_amount, __sumRefAmount, false);
                                            //this._icTransRef._transGrid._cellUpdate(__row, _g.d.ap_ar_trans_detail._final_amount, __sumRefAmount, false);
                                            switch (this._transControlType)
                                            {
                                                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                                                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                                                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                                                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                                                case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                                                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                                                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                                                    __sumRefAmount = __sumRefAmount * -1;
                                                    break;
                                            }
                                            this._icTransRef._transGrid._cellUpdate(__row, _g.d.ap_ar_trans_detail._final_amount, __balance + __sumRefAmount, false);
                                            //this._icTransRef._transGrid._cellUpdate(__row, _g.d.ap_ar_trans_detail._sum_debt_amount, __balance + __sumRefAmount, false);
                                            this._icTransRef._transGrid.Invalidate();
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                }
            }
            // กรณีรับคืน/ส่งคืนอ้างบิล
            switch (this._transControlType)
            {
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                    {

                        decimal __vatRate = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._vat_rate);
                        decimal __refAmount = 0M;// มูลค่าเอกสารเดิม
                        decimal __refRealAmount = 0M; // มูลค่าที่ถูกต้อง
                        decimal __refDiff = 0M; // ผลต่าง
                        for (int __row = 0; __row < this._icTransRef._transGrid._rowData.Count; __row++)
                        {
                            string __docNo = this._icTransRef._transGrid._cellGet(__row, _g.d.ap_ar_trans_detail._billing_no).ToString().Trim();
                            if (__docNo.Length > 0)
                            {
                                decimal __refAmountValue = MyLib._myGlobal._decimalPhase(this._icTransRef._transGrid._cellGet(__row, _g.d.ap_ar_trans_detail._sum_debt_balance).ToString());
                                decimal __refRealAmountValue = MyLib._myGlobal._decimalPhase(this._icTransRef._transGrid._cellGet(__row, _g.d.ap_ar_trans_detail._final_amount).ToString());
                                decimal __refAmountBeforeVat = MyLib._myGlobal._decimalPhase(this._icTransRef._transGrid._cellGet(__row, _g.d.ap_ar_trans_detail._sum_before_vat).ToString());
                                __refAmount += __refAmountValue;
                                __refRealAmount += __refRealAmountValue;
                            }
                        }
                        if (this._vatTypeNumber() == 0 || this._vatTypeNumber() == 1)
                        {
                            // ภาษีแยกนอก
                            //__refAmount = __refAmount * (100M / (100M + __vatRate));// __refAmount - MyLib._myGlobal._round(__refAmount * (__vatRate / 100.0M), _g.g._companyProfile._item_amount_decimal); // 
                            //__refRealAmount =  __refRealAmount * (100M / (100M + __vatRate)); //  __refRealAmount - MyLib._myGlobal._round(__refRealAmount * (__vatRate / 100M), _g.g._companyProfile._item_amount_decimal); // 

                            decimal __refAmountTemp = __refAmount * (100M / (100M + __vatRate));// __refAmount - MyLib._myGlobal._round(__refAmount * (__vatRate / 100.0M), _g.g._companyProfile._item_amount_decimal); // 
                            decimal __refRealAmountTemp = __refRealAmount * (100M / (100M + __vatRate)); //  __refRealAmount - MyLib._myGlobal._round(__refRealAmount * (__vatRate / 100M), _g.g._companyProfile._item_amount_decimal); // 

                            decimal __vatrefAmount = __refAmount - __refAmountTemp;// MyLib._myGlobal._round(__refAmount - __refAmountTemp, _g.g._companyProfile._item_amount_decimal);
                            decimal __vatRealAmount = __refRealAmount - __refRealAmountTemp;// MyLib._myGlobal._round(__refRealAmount - __refRealAmountTemp, _g.g._companyProfile._item_amount_decimal);

                            __refAmount = __refAmount - __vatrefAmount;
                            __refRealAmount = __refRealAmount - __vatRealAmount;
                        }

                        __refDiff = __refAmount - __refRealAmount;
                        if (__refDiff < 0)
                        {
                            __refDiff = __refDiff * -1;
                        }



                        if ((this._transControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้ || this._transControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้)
                            && this._icTransScreenBottom._getDataStr(_g.d.ic_trans._total_manual).ToString().Equals("1")
                            && MyLib._myGlobal._OEMVersion.Equals("imex"))
                        {
                            __refAmount = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._ref_amount);
                            __refDiff = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_before_vat);

                            if (this._transControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้)
                                __refRealAmount = __refAmount - __refDiff;
                            else if (this._transControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้)
                                __refRealAmount = __refAmount + __refDiff;
                            //this._icTransScreenBottom._setDataNumber(_g.d.ic_trans._ref_amount, __refAmount);
                            this._icTransScreenBottom._setDataNumber(_g.d.ic_trans._ref_new_amount, __refRealAmount);
                            this._icTransScreenBottom._setDataNumber(_g.d.ic_trans._ref_diff, __refDiff);
                            this._icTransScreenBottom.Invalidate();
                        }
                        else if ((this._transControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้ || this._transControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้) && this._icTransScreenBottom._getDataStr(_g.d.ic_trans._total_manual).ToString().Equals("1"))
                        {

                        }
                        else
                        {
                            this._icTransScreenBottom._setDataNumber(_g.d.ic_trans._ref_amount, __refAmount);
                            this._icTransScreenBottom._setDataNumber(_g.d.ic_trans._ref_new_amount, __refRealAmount);
                            this._icTransScreenBottom._setDataNumber(_g.d.ic_trans._ref_diff, __refDiff);
                            this._icTransScreenBottom.Invalidate();
                        }

                        // toe กรณี ผลต่างไม่ตรงกับ ยอดก่อน vat ให้ปรับปรุง
                        //decimal __refDiffAmountCheck = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._ref_diff);
                        //decimal __totalBeforeVat = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_before_vat);
                        //decimal __refNewAmountCheck = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._ref_new_amount);

                        //if (__refDiffAmountCheck.CompareTo(__totalBeforeVat) != 0)
                        //{
                        //    decimal __diff = __refDiffAmountCheck - __totalBeforeVat;
                        //    __refNewAmountCheck += __diff;
                        //    this._icTransScreenBottom._setDataNumber(_g.d.ic_trans._ref_new_amount, __refNewAmountCheck);
                        //    this._icTransScreenBottom.Invalidate();
                        //}

                        // toe เช็คความถูกต้องของตัวเลข diff
                        decimal __refAmountCheck = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._ref_amount);
                        decimal __refNewAmountCheck = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._ref_new_amount);
                        decimal __refDiffAmountCheck = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._ref_diff);

                        if (this._transControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้)
                        {
                            if ((__refAmountCheck - __refDiffAmountCheck).CompareTo(__refNewAmountCheck) != 0)
                            {
                                decimal __newAmount = __refAmountCheck - __refDiffAmountCheck;
                                this._icTransScreenBottom._setDataNumber(_g.d.ic_trans._ref_new_amount, __newAmount);
                            }
                        }
                        else if (this._transControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้)
                        {

                            if ((__refAmountCheck + __refDiffAmountCheck).CompareTo(__refNewAmountCheck) != 0)
                            {
                                decimal __newAmount = __refAmountCheck + __refDiffAmountCheck;
                                this._icTransScreenBottom._setDataNumber(_g.d.ic_trans._ref_new_amount, __newAmount);
                            }
                        }

                    }
                    break;
            }
            if (this._payControl != null)
            {
                this._payControl._reCalc();
            }
        }

        void _ictransItemGrid__afterCalcTotal(object sender)
        {
            this._calc(sender);
        }

        void _rePrintForm()
        {
            // ใช้ this._oldDocNo อ้างอิง
            if (this._myToolBar.Enabled == false && this._oldDocNo.Length > 0 && this._myManageTrans._readOnly == false)
            {
                MyLib._myFrameWork __myFramework = new MyLib._myFrameWork();
                // check 
                bool __printForm = false;

                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                {
                    string __query = "select " + _g.d.erp_doc_format._form_code + " from " + _g.d.erp_doc_format._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_doc_format._code) + "=\'" + this._icTransScreenTop._docFormatCode + "\'";
                    DataTable __result = __myFramework._queryShort(__query).Tables[0];
                    if (__result.Rows.Count > 0)
                    {
                        if (__result.Rows[0][_g.d.erp_doc_format._form_code].ToString().Length > 0)
                        {
                            __printForm = true;
                        }
                    }
                }




                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore && this._transControlType != _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด && this._transControlType != _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก && __printForm == false)
                {
                    this._printDataWork();
                }
                else
                {
                    string __docNo = this._oldDocNo;

                    bool __isPrint = true;
                    // singha
                    if (MyLib._myGlobal._OEMVersion.Equals("SINGHA") &&
                        (this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ || this._transControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้ || this._transControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้) &&
                        _g.g._companyProfile._print_invoice_one_time == true)
                    {
                        // check print invoice
                        DataTable __printCountData = __myFramework._queryShort("select count(*) as xcount from " + _g.d.erp_print_logs._table + " where " + _g.d.erp_print_logs._doc_no + "=\'" + __docNo + "\' and " + _g.d.erp_print_logs._trans_flag + " = " + this._getTransFlag.ToString()).Tables[0];
                        if (__printCountData.Rows.Count > 0 && MyLib._myGlobal._decimalPhase(__printCountData.Rows[0][0].ToString()) > 0)
                        {
                            MessageBox.Show("ไม่อนุญาติให้พิมพ์ซ้ำ", "พิมพ์ไปแล้ว", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            __isPrint = false;
                        }
                    }

                    // check lock record
                    {
                        string __query = "select is_lock_record from " + this._icTransTable + " where doc_no=\'" + _oldDocNo + "\' and trans_flag = " + this._getTransFlag;
                        DataTable __result = __myFramework._queryShort(__query).Tables[0];
                        if (__result.Rows.Count > 0)
                        {
                            if (MyLib._myGlobal._intPhase(__result.Rows[0]["is_lock_record"].ToString()) == 1)
                            {
                                __isPrint = false;
                            }
                        }
                    }

                    if ((this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ || this._transControlType == _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ) && _g.g._companyProfile._print_invoice_one_time == false)
                    {
                        __isPrint = true;
                    }

                    if (__isPrint)
                    {
                        //string __docNo = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_no).ToString().Trim();
                        bool __printResult = SMLERPReportTool._global._printForm(this._icTransScreenTop._docFormatCode, __docNo, this._getTransFlag.ToString(), false);
                        if (__printResult == true)
                        {
                            // update print count
                            //MyLib._myFrameWork __myFramework = new MyLib._myFrameWork();
                            __myFramework._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "insert into " + _g.d.erp_print_logs._table + "(" + _g.d.erp_print_logs._doc_no + "," + _g.d.erp_print_logs._trans_flag + "," + _g.d.erp_print_logs._user_code + "," + _g.d.erp_print_logs._print_datetime + ") values (\'" + __docNo + "\'," + this._getTransFlag.ToString() + ",\'" + MyLib._myGlobal._userCode + "\', \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\')");
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

        void _docFlowRecheck()
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

            MessageBox.Show(this._oldDocNo + " : Recheck Doc Flow Success");


            this._myManageTrans._dataList._refreshData();
            // this._myManageTrans._dataList._gridData.Invalidate();
        }

        void _arStatusInfo()
        {
            if (this._arInfoForm != null)
            {
                this._arInfoForm._clear();
                string __custCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);

                if (__custCode.Length > 0)
                    this._arInfoForm._load(__custCode);
            }
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
                        case Keys.L:
                            {
                                switch (this._transControlType)
                                {
                                    case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                                    case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                                    case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                                    case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                                    case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                                        {
                                            string __custCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
                                            if (__custCode.Length > 0)
                                            {
                                                if (this._arInfoForm == null)
                                                {
                                                    this._arInfoForm = new SMLERPControl._customer._arStatusForm();
                                                    DockableFormInfo __form0 = this._myManageTrans._dock.Add(this._arInfoForm, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
                                                    __form0.ShowCloseButton = true;
                                                    __form0.ShowContextMenuButton = false;
                                                    __form0.Disposed += (s1, e1) =>
                                                    {
                                                        try
                                                        {
                                                            this._arInfoForm.Dispose();
                                                        }
                                                        catch
                                                        {
                                                        }
                                                        this._arInfoForm = null;
                                                    };
                                                }
                                                _arStatusInfo();
                                                return true;
                                            }
                                        }
                                        break;
                                }
                                return false;
                            }
                        case Keys.M:
                            {
                                // ดึงจาก รถเข็น Android
                                _importCartForm __cart = new _importCartForm();
                                __cart._processButton.Click += (s1, e1) =>
                                {
                                    this._icTransScreenTop._setDataStr(_g.d.ic_trans._cust_code, __cart._custCode);
                                    this._icTransScreenTop._setComboBox(_g.d.ic_trans._sale_type, __cart._billType);
                                    this._icTransScreenTop._setComboBox(_g.d.ic_trans._vat_type, __cart._taxType);
                                    this._icTransScreenTop._setDataStr(_g.d.ic_trans._sale_code, __cart._saleCode.Text);
                                    //
                                    this._icTransItemGrid._clear();
                                    for (int __row = 0; __row < __cart._itemGrid._rowData.Count; __row++)
                                    {
                                        string __barCode = __cart._itemGrid._cellGet(__row, _g.d.ic_trans_detail._barcode).ToString();
                                        string __itemCode = __cart._itemGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString();
                                        string __itemName = __cart._itemGrid._cellGet(__row, _g.d.ic_trans_detail._item_name).ToString();
                                        string __unitCode = __cart._itemGrid._cellGet(__row, _g.d.ic_trans_detail._unit_code).ToString();
                                        decimal __qty = MyLib._myGlobal._decimalPhase(__cart._itemGrid._cellGet(__row, _g.d.ic_trans_detail._qty).ToString());
                                        decimal __price = MyLib._myGlobal._decimalPhase(__cart._itemGrid._cellGet(__row, _g.d.ic_trans_detail._price).ToString());
                                        string __discountWord = __cart._itemGrid._cellGet(__row, _g.d.ic_trans_detail._discount).ToString();

                                        string __wh_code = __cart._itemGrid._cellGet(__row, _g.d.ic_trans_detail._wh_code).ToString();
                                        string __shelf_code = __cart._itemGrid._cellGet(__row, _g.d.ic_trans_detail._shelf_code).ToString();
                                        //
                                        int __line = this._icTransItemGrid._addRow();
                                        if (__barCode.Length > 0)
                                        {
                                            this._icTransItemGrid._cellUpdate(__line, _g.d.ic_trans_detail._barcode, __barCode, false);
                                        }

                                        this._icTransItemGrid._cellUpdate(__line, _g.d.ic_trans_detail._item_code, __itemCode, false);
                                        this._icTransItemGrid._cellUpdate(__line, _g.d.ic_trans_detail._item_name, __itemName, false);
                                        this._icTransItemGrid._cellUpdate(__line, _g.d.ic_trans_detail._unit_code, __unitCode, false);

                                        if (__wh_code != "")
                                        {
                                            this._icTransItemGrid._cellUpdate(__line, _g.d.ic_trans_detail._wh_code, __wh_code, false);
                                            this._icTransItemGrid._cellUpdate(__line, _g.d.ic_trans_detail._shelf_code, __shelf_code, false);

                                        }

                                        // กรณี ไม่ได้ set ราคา ให้หาราคา auto เลย
                                        if (__price == 0)
                                        {
                                            this._icTransItemGrid._cellUpdate(__line, _g.d.ic_trans_detail._qty, __qty, true);
                                        }
                                        else
                                        {
                                            this._icTransItemGrid._cellUpdate(__line, _g.d.ic_trans_detail._qty, __qty, false);
                                            this._icTransItemGrid._cellUpdate(__line, _g.d.ic_trans_detail._price, __price, false);
                                        }


                                        this._icTransItemGrid._cellUpdate(__line, _g.d.ic_trans_detail._discount, __discountWord, false);
                                        this._icTransItemGrid._calcItemPrice(__line, __line, this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._discount));

                                        this._isImportFromCart = true;
                                        this._importCartNumber = __cart._selectCartNumber;
                                    }
                                    this._icTransItemGrid.Invalidate();
                                    this._icTransItemGrid._searchUnitNameWareHouseNameShelfNameAll();
                                    this._icTransItemGrid__reCalc();
                                    __cart.Dispose();
                                };
                                __cart.ShowDialog();
                            }
                            break;
                        case Keys.O:
                            {
                                if (_g.g._companyProfile._internetSync)
                                {
                                    // ดึงรายการจาก Internet
                                    _importOrderFromInternet _importInternetOrder = new _importOrderFromInternet();
                                    _importInternetOrder.StartPosition = FormStartPosition.CenterScreen;
                                    _importInternetOrder._importButton.Click += (sender, e) =>
                                    {
                                        if (_importInternetOrder._orderNumberTextbox.Text.Length == 0)
                                        {
                                            MessageBox.Show("กรุณาป้อนหมายเลข Order");
                                        }
                                        else
                                        {
                                            string __getOrderQuery = "select * from take_order_temp where id = \'" + _importInternetOrder._orderNumberTextbox.Text + "\' and product_code = \'" + MyLib._myGlobal._productCode + "\' and database_code = \'" + MyLib._myGlobal._databaseName.ToUpper() + "\' ";

                                            MyLib._myFrameWork __ws = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
                                            DataSet __result = __ws._query(MyLib._myGlobal._internetSyncName, __getOrderQuery);

                                            if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
                                            {
                                                this._icTransItemGrid._clear();
                                                DataTable __internetOrder = __result.Tables[0];
                                                for (int __row = 0; __row < __internetOrder.Rows.Count; __row++)
                                                {
                                                    string __barCode = __internetOrder.Rows[__row]["barcode"].ToString();
                                                    decimal __qty = MyLib._myGlobal._decimalPhase(__internetOrder.Rows[__row]["qty"].ToString());
                                                    //decimal __price = MyLib._myGlobal._decimalPhase(__internetOrder.Rows[__row]["price"].ToString());


                                                    int __line = this._icTransItemGrid._addRow();
                                                    this._icTransItemGrid._cellUpdate(__line, _g.d.ic_trans_detail._item_code, __barCode, true);
                                                    this._icTransItemGrid._cellUpdate(__line, _g.d.ic_trans_detail._qty, __qty, true);

                                                    string __selectInternetDocNo = _importInternetOrder._orderNumberTextbox.Text;
                                                    this._isImportFromInternet = true;
                                                    this._importInternetNumber = this._docNoAdd(this._importInternetNumber, __selectInternetDocNo);

                                                }
                                            }
                                            _importInternetOrder.Close();
                                        }



                                    };
                                    _importInternetOrder.ShowDialog();
                                    return true;
                                }
                                return false;
                            }
                        case Keys.D7:
                            // ดึงข้อมูลจาก handheld
                            {
                                if (this._importFromHandHeld == null)
                                {
                                    this._importFromHandHeld = new _importHandheld._importHandheldForm();
                                    this._importFromHandHeld._sendButton.Click += (s1, e1) =>
                                    {
                                        if (this._importFromHandHeld._sendButton.Text.Trim().Length == 0)
                                        {
                                            MessageBox.Show(MyLib._myGlobal._resource("กรุณาเลือกเอกสารก่อน"));
                                        }
                                        else
                                        {
                                            this._icTransItemGrid._clear();
                                            for (int __row = 0; __row < this._importFromHandHeld._itemGrid._rowData.Count; __row++)
                                            {
                                                string __barCode = this._importFromHandHeld._itemGrid._cellGet(__row, _g.d.barcode_import_list_detail._barcode).ToString();
                                                string __unitCode = this._importFromHandHeld._itemGrid._cellGet(__row, _g.d.barcode_import_list_detail._unit_code).ToString();

                                                string __lot_number = "";
                                                string __expire_date = "";
                                                string __mfd_date = "";
                                                string __mfn_name = "";
                                                if (MyLib._myGlobal._OEMVersion.Equals("ims") || MyLib._myGlobal._OEMVersion.Equals("imex"))
                                                {
                                                    __lot_number = this._importFromHandHeld._itemGrid._cellGet(__row, _g.d.barcode_import_list_detail._lot_number).ToString();
                                                    __expire_date = this._importFromHandHeld._itemGrid._cellGet(__row, _g.d.barcode_import_list_detail._expire_date).ToString();
                                                    __mfd_date = this._importFromHandHeld._itemGrid._cellGet(__row, _g.d.barcode_import_list_detail._mfd_date).ToString();
                                                    __mfn_name = this._importFromHandHeld._itemGrid._cellGet(__row, _g.d.barcode_import_list_detail._mfn_name).ToString();
                                                }
                                                decimal __qty = MyLib._myGlobal._decimalPhase(this._importFromHandHeld._itemGrid._cellGet(__row, _g.d.barcode_import_list_detail._qty).ToString());
                                                //
                                                int __line = this._icTransItemGrid._addRow();
                                                this._icTransItemGrid._cellUpdate(__line, _g.d.ic_trans_detail._item_code, __barCode, true);
                                                this._icTransItemGrid._cellUpdate(__line, _g.d.ic_trans_detail._unit_code, __unitCode, true);
                                                this._icTransItemGrid._cellUpdate(__line, _g.d.ic_trans_detail._qty, __qty, true);
                                                if (MyLib._myGlobal._OEMVersion.Equals("ims") || MyLib._myGlobal._OEMVersion.Equals("imex"))
                                                {
                                                    int __costType = (int)this._icTransItemGrid._cellGet(__line, this._icTransItemGrid._columnCostType);
                                                    if (__costType == 1 || ((MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                                                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                                                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                                                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional) && __costType == 2))
                                                    {
                                                        // update lot detail
                                                        this._icTransItemGrid._cellUpdate(__line, _g.d.ic_trans_detail._lot_number_1, __lot_number, true);
                                                        this._icTransItemGrid._cellUpdate(__line, _g.d.ic_trans_detail._mfn_name, __mfn_name, true);

                                                        if (__expire_date.Length > 0)
                                                        {
                                                            DateTime __expire_date_update = MyLib._myGlobal._convertDateFromQuery(__expire_date);
                                                            this._icTransItemGrid._cellUpdate(__line, _g.d.ic_trans_detail._date_expire, __expire_date_update, true);
                                                        }

                                                        if (__mfd_date.Length > 0)
                                                        {
                                                            DateTime __mdf_date_update = MyLib._myGlobal._convertDateFromQuery(__mfd_date);
                                                            this._icTransItemGrid._cellUpdate(__line, _g.d.ic_trans_detail._mfd_date, __mdf_date_update, true);
                                                        }


                                                    }
                                                }

                                                string __selectHandHeldDocNo = this._importFromHandHeld._dataList._gridData._cellGet(this._importFromHandHeld._dataList._gridData._selectRow, _g.d.barcode_import_list._table + "." + _g.d.barcode_import_list._doc_no).ToString();
                                                this._isImportFromHandHeld = true;
                                                this._importHandHeldNumber = this._docNoAdd(this._importHandHeldNumber, __selectHandHeldDocNo);

                                            }
                                        }
                                    };
                                    this._importFromHandHeld._appendButton.Click += (s2, e2) =>
                                    {
                                        if (this._importFromHandHeld._sendButton.Text.Trim().Length == 0)
                                        {
                                            MessageBox.Show(MyLib._myGlobal._resource("กรุณาเลือกเอกสารก่อน"));
                                        }
                                        else
                                        {
                                            //this._icTransItemGrid._clear();
                                            for (int __row = 0; __row < this._importFromHandHeld._itemGrid._rowData.Count; __row++)
                                            {
                                                string __barCode = this._importFromHandHeld._itemGrid._cellGet(__row, _g.d.barcode_import_list_detail._barcode).ToString();
                                                string __unitCode = this._importFromHandHeld._itemGrid._cellGet(__row, _g.d.barcode_import_list_detail._unit_code).ToString();
                                                decimal __qty = MyLib._myGlobal._decimalPhase(this._importFromHandHeld._itemGrid._cellGet(__row, _g.d.barcode_import_list_detail._qty).ToString());
                                                //
                                                int __line = this._icTransItemGrid._addRow();
                                                this._icTransItemGrid._cellUpdate(__line, _g.d.ic_trans_detail._item_code, __barCode, true);
                                                this._icTransItemGrid._cellUpdate(__line, _g.d.ic_trans_detail._unit_code, __unitCode, true);
                                                this._icTransItemGrid._cellUpdate(__line, _g.d.ic_trans_detail._qty, __qty, true);
                                            }

                                            string __selectHandHeldDocNo = this._importFromHandHeld._dataList._gridData._cellGet(this._importFromHandHeld._dataList._gridData._selectRow, _g.d.barcode_import_list._table + "." + _g.d.barcode_import_list._doc_no).ToString();
                                            this._isImportFromHandHeld = true;
                                            this._importHandHeldNumber = this._docNoAdd(this._importHandHeldNumber, __selectHandHeldDocNo);

                                        }
                                    };
                                    DockableFormInfo __form0 = this._myManageTrans._dock.Add(this._importFromHandHeld, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
                                    __form0.ShowCloseButton = true;
                                    __form0.ShowContextMenuButton = false;
                                    __form0.Disposed += (s1, e1) =>
                                    {
                                        try
                                        {
                                            this._importFromHandHeld.Dispose();
                                        }
                                        catch
                                        {
                                        }
                                        this._importFromHandHeld = null;
                                    };
                                }
                            }
                            return true;
                        case Keys.P:
                            {
                                this._rePrintForm();
                                return true;
                            }
                        case Keys.Home:
                        case Keys.PageUp:
                            {
                                this._icTransScreenTop._focusFirst();
                                return true;
                            }
                        case Keys.PageDown:
                            {
                                if (this._icTransItemGrid._selectRow == -1) this._icTransItemGrid._selectRow = 0;
                                if (this._icTransItemGrid._selectColumn == -1) this._icTransItemGrid._selectColumn = this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._item_code);
                                this._icTransItemGrid._inputCell(this._icTransItemGrid._selectRow, this._icTransItemGrid._selectColumn);
                                return true;
                            }
                        case Keys.B:
                            {
                                this._basket();
                                return true;
                            }
                        case Keys.R:
                            {
                                _docFlowRecheck();
                            }
                            break;
                        case Keys.W:
                            {
                                switch (this._transControlType)
                                {
                                    case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา:
                                    case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                                    case _g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ:
                                    case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                                    case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                                    case _g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า:
                                    case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก:
                                    case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด:
                                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:

                                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:

                                    case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                                        {
                                            if (MessageBox.Show("ต้องการแก้ไขคลังและที่เก็บทั้งหมด หรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                            {
                                                if (this._transControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก)
                                                {
                                                    string __whCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._wh_from);
                                                    string __shelfCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._location_from);
                                                    string __whCode2 = this._icTransScreenTop._getDataStr(_g.d.ic_trans._wh_to);
                                                    string __shelfCode2 = this._icTransScreenTop._getDataStr(_g.d.ic_trans._location_to);

                                                    for (int __row = 0; __row < this._icTransItemGrid._rowData.Count; __row++)
                                                    {
                                                        this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._wh_code, __whCode, false);
                                                        this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._shelf_code, __shelfCode, false);
                                                        this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._wh_code_2, __whCode2, false);
                                                        this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._shelf_code_2, __shelfCode2, false);
                                                    }
                                                    this._icTransItemGrid._searchUnitNameWareHouseNameShelfNameAll();

                                                }
                                                else
                                                {
                                                    string __whCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._wh_from);
                                                    string __shelfCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._location_from);

                                                    for (int __row = 0; __row < this._icTransItemGrid._rowData.Count; __row++)
                                                    {
                                                        this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._wh_code, __whCode, false);
                                                        this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._shelf_code, __shelfCode, false);
                                                    }
                                                    this._icTransItemGrid._searchUnitNameWareHouseNameShelfNameAll();
                                                }

                                            }
                                        }
                                        break;
                                }
                                return true;
                            }
                        case Keys.I:
                            {
                                if (this._transControlType == _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น && MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                                {
                                    _icTransImportDataControl __importDataControl = new _icTransImportDataControl();
                                    __importDataControl.StartPosition = FormStartPosition.CenterScreen;
                                    __importDataControl.ShowDialog();
                                    this._myManageTrans._dataList._refreshData();
                                    return true;
                                }
                                return false;
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
                                    if (_myManageTrans__checkEditData(this._myManageTrans._dataList._gridData._selectRow, this._myManageTrans._dataList._gridData))
                                    {
                                        // ยกเลิก เอกสาร
                                        _g._docCancelForm __docCancelForm = new _g._docCancelForm(this._oldDocNo);

                                        // check open period
                                        bool __openPeriod = _g.g._checkOpenPeriod(this._icTransScreenTop._getDataDate(_g.d.ic_trans._doc_date));

                                        if (__openPeriod)
                                        {
                                            if (__docCancelForm.ShowDialog() == DialogResult.Yes)
                                            {
                                                if (__docCancelForm._comboCancelConfirm.SelectedIndex == 1)
                                                {
                                                    // update cancel 
                                                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                                                    StringBuilder __queryListUpdate = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                                                    __queryListUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + this._icTransTable + " set is_cancel = 1, " + _g.d.ic_trans._cancel_code + "=\'" + MyLib._myGlobal._userCode + "\', " + _g.d.ic_trans._cancel_datetime + "=\'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\' where doc_no = \'" + this._oldDocNo + "\' and trans_flag =" + this._getTransFlag));
                                                    __queryListUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal._table + " where " + _g.d.gl_journal._doc_no + " = \'" + this._oldDocNo + "\' and " + _g.d.gl_journal._trans_flag + " =" + this._getTransFlag));
                                                    __queryListUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._doc_no + " = \'" + this._oldDocNo + "\' and " + _g.d.gl_journal_detail._trans_flag + " =" + this._getTransFlag));

                                                    if (this._vatBuy != null)
                                                    {
                                                        __queryListUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_vat_buy._table + " where " + _g.d.gl_journal_vat_buy._doc_no + " = \'" + this._oldDocNo + "\' and " + _g.d.gl_journal_vat_buy._trans_flag + " =" + this._getTransFlag));
                                                    }

                                                    __queryListUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.erp_cancel_logs._table +
                                                        " (" + _g.d.erp_cancel_logs._doc_no + "," + _g.d.erp_cancel_logs._trans_flag + "," + _g.d.erp_cancel_logs._user_code + "," + _g.d.erp_cancel_logs._cancel_flag + "," + _g.d.erp_cancel_logs._cancel_datetime + "," + _g.d.erp_cancel_logs._cancel_reason + ") " +
                                                        " values " +
                                                        " (\'" + this._oldDocNo + "\', " + this._getTransFlag + ", \'" + MyLib._myGlobal._userCode + "\', 0, \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\', \'" + __docCancelForm._cancelReasonTextbox.Text + "\') "));

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

                                                        // send arm
                                                        if (_g.g._companyProfile._arm_send_cancel_doc)
                                                        {
                                                            StringBuilder __message = new StringBuilder();
                                                            //__message.Append("ยกเลิกเอกสาร " + this._oldDocNo + " โดย " + MyLib._myGlobal._userName + "(" + MyLib._myGlobal._userCode + ") เหตุผล : " + __docCancelForm._cancelReasonTextbox.Text);
                                                            __message.Append(string.Format("ยกเลิกเอกสาร {0} ประเภทเอกสาร{1}  เหตุผล : {3} User : {2} {4}",
                                                                this._oldDocNo,
                                                                _g.g._transFlagGlobal._transName(this._getTransFlag),
                                                                MyLib._myGlobal._userName,
                                                                __docCancelForm._cancelReasonTextbox.Text,
                                                                DateTime.Now.ToString("yyyyMMddHHmmss", new CultureInfo("en-US"))));

                                                            string __sendTo = _g.g._companyProfile._arm_send_cancel_doc_to;

                                                            DataTable __sendCancelBranch = __myFrameWork._queryShort("select " + _g.d.erp_branch_list._arm_send_cancel_doc_to + " from " + _g.d.erp_branch_list._table + " where " + _g.d.erp_branch_list._code + " = \'" + this._icTransScreenTop__getBranchCode() + "\' ").Tables[0];
                                                            if (__sendCancelBranch.Rows.Count > 0 && __sendCancelBranch.Rows[0][0].ToString().Length > 0)
                                                            {
                                                                __sendTo = __sendCancelBranch.Rows[0][0].ToString();
                                                            }

                                                            SMLERPMailMessage._sendMessage._sendMessageSaleHub(__sendTo, __message.ToString(), "");
                                                        }

                                                        MessageBox.Show("ยกเลิกเอกสารสำเร็จ");
                                                        this._myManageTrans._dataList._refreshData();

                                                    }
                                                    this._myManageTrans._dataList._refreshData();
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

                if (keyData == (Keys.Shift | Keys.F12))
                {
                    if (_myManageTrans__checkEditData(this._myManageTrans._dataList._gridData._selectRow, this._myManageTrans._dataList._gridData, false))
                    {
                        // un cancel  doc
                        if (MyLib._myGlobal._OEMVersion.Equals("SINGHA")
                                    /*&& (
                                    this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ ||
                                            this._transControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้ ||
                                            this._transControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้)*/
                                    )
                        {
                            // check open period
                            bool __openPeriod = _g.g._checkOpenPeriod(this._icTransScreenTop._getDataDate(_g.d.ic_trans._doc_date));
                            if (__openPeriod)
                            {
                                if (MessageBox.Show("ต้องการเรียกเอกสารกลับมาใชังานได้ปรกติ หรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                {
                                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                                    StringBuilder __queryListUpdate = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                                    __queryListUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + this._icTransTable + " set is_cancel = 0 where doc_no = \'" + this._oldDocNo + "\' and trans_flag in (" + this._getTransFlag + (this._transControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก ? ",72" : "") + ")"));
                                    __queryListUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.erp_cancel_logs._table +
                                        " (" + _g.d.erp_cancel_logs._doc_no + "," + _g.d.erp_cancel_logs._trans_flag + "," + _g.d.erp_cancel_logs._user_code + "," + _g.d.erp_cancel_logs._cancel_flag + "," + _g.d.erp_cancel_logs._cancel_datetime + ") " +
                                        " values " +
                                        " (\'" + this._oldDocNo + "\', " + this._getTransFlag + ", \'" + MyLib._myGlobal._userCode + "\', 1, \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\') "));

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

                                        // send arm
                                        if (_g.g._companyProfile._arm_send_cancel_doc)
                                        {
                                            StringBuilder __message = new StringBuilder();
                                            //__message.Append("ยกเลิกเอกสาร " + this._oldDocNo + " โดย " + MyLib._myGlobal._userName + "(" + MyLib._myGlobal._userCode + ") เหตุผล : " + __docCancelForm._cancelReasonTextbox.Text);
                                            __message.Append(string.Format("คืนสถานะเอกสาร {0} ประเภทเอกสาร{1} User : {2} {3}",
                                                this._oldDocNo,
                                                _g.g._transFlagGlobal._transName(this._getTransFlag),
                                                MyLib._myGlobal._userName,
                                                DateTime.Now.ToString("yyyyMMddHHmmss", new CultureInfo("en-US"))));

                                            string __sendTo = _g.g._companyProfile._arm_send_cancel_doc_to;

                                            DataTable __sendCancelBranch = __myFrameWork._queryShort("select " + _g.d.erp_branch_list._arm_send_cancel_doc_to + " from " + _g.d.erp_branch_list._table + " where " + _g.d.erp_branch_list._code + " = \'" + this._icTransScreenTop__getBranchCode() + "\' ").Tables[0];
                                            if (__sendCancelBranch.Rows.Count > 0 && __sendCancelBranch.Rows[0][0].ToString().Length > 0)
                                            {
                                                __sendTo = __sendCancelBranch.Rows[0][0].ToString();
                                            }

                                            SMLERPMailMessage._sendMessage._sendMessageSaleHub(__sendTo, __message.ToString(), "");
                                        }

                                        MessageBox.Show("เรียกคืนเอกสารสำเร็จ");
                                        this._myManageTrans._dataList._refreshData();

                                        // ประมวลผล GL ด้วย อย่าลืม
                                    }
                                    this._myManageTrans._dataList._refreshData();
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
                /*if (keyData == Keys.F7)
                {
                    this._icTransItemGrid._clear();
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    DataTable __getItem = __myFrameWork._queryShort("select * from ic_inventory order by code").Tables[0];
                    for (int __row = 0; __row < __getItem.Rows.Count; __row++)
                    {
                        string __itemCode = __getItem.Rows[__row][_g.d.ic_inventory._code].ToString();
                        int __addr = this._icTransItemGrid._addRow();
                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, false);
                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code, "001", false);
                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code, "001", false);
                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._qty, 100.0M, false);
                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._price, 50.0M, false);
                        __addr = this._icTransItemGrid._addRow();
                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, false);
                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code, "001", false);
                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code, "002", false);
                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._qty, 100.0M, false);
                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._price, 50.0M, false);
                        __addr = this._icTransItemGrid._addRow();
                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, false);
                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code, "001", false);
                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code, "003", false);
                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._qty, 100.0M, false);
                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._price, 50.0M, false);
                        __addr = this._icTransItemGrid._addRow();
                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, false);
                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code, "001", false);
                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code, "004", false);
                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._qty, 100.0M, false);
                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._price, 50.0M, false);
                        __addr = this._icTransItemGrid._addRow();
                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, false);
                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code, "001", false);
                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code, "005", false);
                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._qty, 100.0M, false);
                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._price, 50.0M, false);
                    }
                }*/

                if (keyData == Keys.F12)
                {
                    this._saveData();
                    return true;
                }
                if (SystemInformation.ComputerName.ToLower().IndexOf("jead8") != -1 || SystemInformation.ComputerName.ToLower().IndexOf("toe-pc") != -1)
                {
                    if (keyData == Keys.F8)
                    {
                        _jeadTestForm __test = new _jeadTestForm();
                        __test.Show();
                        return true;
                    }
                    if (keyData == Keys.F9)
                    {
                        /* if (MessageBox.Show(MyLib._myGlobal._resource("สร้างข้อมูลซื้อขาย AUTO"), "warnning", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                         {
                             Random __random = new Random();
                             MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                             //__myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "truncate table ic_trans");
                             //__myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "truncate table ic_trans_detail");
                             DataTable __item = __myFrameWork._queryShort("select code,unit_standard from ic_inventory order by code").Tables[0];
                             DataTable __cust = __myFrameWork._queryShort("select code from ar_customer order by code").Tables[0];
                             DataTable __sale = __myFrameWork._queryShort("select code from erp_user order by code").Tables[0];
                             DataTable __shelf = __myFrameWork._queryShort("select code,whcode from ic_shelf order by whcode,code").Tables[0];
                             // รายวัน
                             DateTime __docDateBegin = new DateTime(2011, 12, 17);
                             //
                             DateTime __docDate = new DateTime(__docDateBegin.Year, __docDateBegin.Month, __docDateBegin.Day).AddDays(1);
                             string __docTime = "09:00";
                             for (int __dayLoop = 0; __dayLoop < 453; __dayLoop++)
                             {
                                 StringBuilder __myQuery = new StringBuilder();
                                 __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                 for (int __bill = 0; __bill < __random.Next(1000) + 100; __bill++)
                                 {
                                     int __totalItem = __random.Next(50) + 10;
                                     int __shelfAddr = __random.Next(__shelf.Rows.Count);
                                     string __whCode = __shelf.Rows[__shelfAddr]["whcode"].ToString();
                                     string __shelfCode = __shelf.Rows[__shelfAddr]["code"].ToString(); ;
                                     string __docNo = "S-2-" + __dayLoop.ToString() + "-" + __bill.ToString();
                                     string __custCode = __cust.Rows[__random.Next(__cust.Rows.Count)]["code"].ToString();
                                     string __saleCode = __sale.Rows[__random.Next(__sale.Rows.Count)]["code"].ToString();
                                     int __inquiryType = __random.Next(2);
                                     decimal __totalValue = 0M;
                                     decimal __totalVatValue = 0M;
                                     decimal __sumAmountExcludeVat = 0M;
                                     decimal __totalAmount = 0M;
                                     for (int __loop = 0; __loop < __totalItem; __loop++)
                                     {
                                         string __itemCode = "'";
                                         while (__itemCode.IndexOf("\'") != -1)
                                         {
                                             __itemCode = __item.Rows[__random.Next(__item.Rows.Count)]["code"].ToString();
                                         }
                                         string __unit = __item.Rows[__random.Next(__item.Rows.Count)]["unit_standard"].ToString();
                                         decimal __qty = __random.Next(1000) + 2;
                                         decimal __price = __random.Next(100) + 5;
                                         decimal __amount = __qty * __price;
                                         decimal __vat = MyLib._myGlobal._round((__amount * 100) / 107, 2);
                                         decimal __amountExcludeVat = __amount - __vat;
                                         decimal __sumofCost = __amountExcludeVat - (__amountExcludeVat * ((decimal)__random.Next(10) / 100M));
                                         __totalAmount += __amount;
                                         __totalValue += __amountExcludeVat;
                                         __totalVatValue += __vat;
                                         __sumAmountExcludeVat += __amountExcludeVat;
                                         __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into ic_trans_detail (doc_no,cust_code,doc_date,doc_time,doc_date_calc,doc_time_calc,item_code,unit_code,qty,price,sum_amount, trans_type,trans_flag,inquiry_type,stand_value,divide_value, ratio,calc_flag, last_status,vat_type,sale_code,wh_code,shelf_code,total_vat_value,sum_amount_exclude_vat,sum_of_cost,sum_of_cost_1) values (\'" + __docNo + "\',\'" + __custCode + "\',\'" + MyLib._myGlobal._convertDateToQuery(__docDate) + "\',\'" + __docTime + "\',\'" + __docDate + "\',\'" + __docTime + "\',\'" + __itemCode + "\',\'" + __unit + "\'," + __qty.ToString() + "," + __price.ToString() + "," + __amount.ToString() + ",2,44," + __inquiryType.ToString() + ",1,1,1,-1,0,1,\'" + __saleCode + "\',\'" + __whCode + "\',\'" + __shelfCode + "\'," + __vat.ToString() + "," + __amountExcludeVat.ToString() + "," + __sumofCost.ToString() + "," + __sumofCost.ToString() + ")"));
                                     }
                                     // เพิ่มหัว
                                     __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into ic_trans (doc_no,sale_code, cust_code,doc_date,doc_time,inquiry_type,vat_type,trans_type,trans_flag, vat_rate,last_status,total_value,total_vat_value,total_after_vat,total_amount) values (\'" + __docNo + "\',\'" + __saleCode + "\',\'" + __custCode + "\',\'" + MyLib._myGlobal._convertDateToQuery(__docDate) + "\',\'" + __docTime + "\'," + __inquiryType.ToString() + ",0,2,44,7.0,0," + __totalValue.ToString() + "," + __totalVatValue.ToString() + "," + __totalAmount.ToString() + "," + __totalAmount.ToString() + ")"));
                                 }
                                 __myQuery.Append("</node>");
                                 string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                                 if (__result.Length != 0)
                                 {
                                     MessageBox.Show(__result);
                                     break;
                                 }
                                 __docDate = __docDate.AddDays(1);
                             }
                             __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update ic_inventory set balance_qty=coalesce((select sum((qty*(stand_value/divide_value))*calc_flag) from ic_trans_detail where code=item_code and last_status=0),0)");
                             MessageBox.Show("success");
                         }*/
                        return true;
                    }
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        string _extraWhere(int row)
        {
            string __result = "";
            string __getItem = "";
            try
            {
                switch (this._transControlType)
                {
                    case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                        __getItem = this._icTransItemGrid._cellGet(row, _g.d.ic_trans_detail._item_code).ToString();
                        __result = _g.d.ic_unit_use._ic_code + "=\'" + __getItem + "\'";
                        break;
                }
            }
            catch (Exception ex)
            {
            }
            return __result;
        }

        void _saveButton_Click(object sender, EventArgs e)
        {
            // The control has been CTRL-Clicked
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                _showPrintDialogByCtrl = true;
            }
            this._saveData();
        }

        void _addButton_Click(object sender, EventArgs e)
        {
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

        //อนุมัติใบเสนอขายสินค้า
        void _itemApprovalSelectButton_Click(object sender, EventArgs e)
        {
            MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
            //_ictransSearch _icconditionSearch = new _ictransSearch();
            switch (this._transControlType)
            {
                case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                    this._icTransRef.Visible = (this._icTransRef.Visible) ? false : true;
                    this._icTransItemGrid._clear();
                    /*if (this._icTransRef._transGrid._selectRow == -1)
                    {
                        if (this._saveButton.Enabled == true)
                        {
                            this._icTransRef._transGrid._selectRow = 0;
                            this._icTransRef._transGrid._selectColumn = 0;
                            this._icTransRef._transGrid._inputCell(this._icTransRef._transGrid._selectRow, this._icTransRef._transGrid._selectColumn);
                        }
                        //
                        this._icTransItemGrid._visableRefColumn((this._icTransRef._transGrid._rowCount(this._icTransRef._transGrid._findColumnByName(_g.d.ap_ar_trans_detail._billing_no)) > 0) ? true : false);
                    }*/
                    break;

                case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                    this._icTransItemGrid._icTransRefProcessButton(this, this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_ref));
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                    this._icTransRef.Visible = (this._icTransRef.Visible) ? false : true;
                    if (this._icTransRef._transGrid._selectRow == -1)
                    {
                        if (this._saveButton.Enabled == true)
                        {
                            this._icTransRef._transGrid._selectRow = 0;
                            this._icTransRef._transGrid._selectColumn = 0;
                            this._icTransRef._transGrid._inputCell(this._icTransRef._transGrid._selectRow, this._icTransRef._transGrid._selectColumn);
                        }
                        //
                        this._icTransItemGrid._visableRefColumn((this._icTransRef._transGrid._rowCount(this._icTransRef._transGrid._findColumnByName(_g.d.ap_ar_trans_detail._billing_no)) > 0) ? true : false);
                    }
                    break;
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                    try
                    {
                        string __getCustCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
                        if (__getCustCode.Length > 0)
                        {
                            this._icTransRef.Visible = (this._icTransRef.Visible) ? false : true;
                            if (this._icTransRef._transGrid._selectRow == -1)
                            {
                                if (this._saveButton.Enabled == true)
                                {
                                    this._icTransRef._transGrid._selectRow = 0;
                                    this._icTransRef._transGrid._selectColumn = 0;
                                    this._icTransRef._transGrid._inputCell(this._icTransRef._transGrid._selectRow, this._icTransRef._transGrid._selectColumn);
                                }
                                //
                                this._icTransItemGrid._visableRefColumn((this._icTransRef._transGrid._rowCount(this._icTransRef._transGrid._findColumnByName(_g.d.ap_ar_trans_detail._billing_no)) > 0) ? true : false);
                            }
                        }
                        else
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("ยังไม่ได้ใส่รหัสลูกค้า"));
                            Control __getControl = this._icTransScreenTop._getControl(_g.d.ic_trans._cust_code);
                            __getControl.Focus();
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                    try
                    {
                        string __getCustCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
                        if (__getCustCode.Length > 0)
                        {
                            this._icTransRef.Visible = (this._icTransRef.Visible) ? false : true;
                            if (this._icTransRef._transGrid._selectRow == -1)
                            {
                                if (this._saveButton.Enabled == true)
                                {
                                    this._icTransRef._transGrid._selectRow = 0;
                                    this._icTransRef._transGrid._selectColumn = 0;
                                    this._icTransRef._transGrid._inputCell(this._icTransRef._transGrid._selectRow, this._icTransRef._transGrid._selectColumn);
                                }
                                //
                                this._icTransItemGrid._visableRefColumn((this._icTransRef._transGrid._rowCount(this._icTransRef._transGrid._findColumnByName(_g.d.ap_ar_trans_detail._billing_no)) > 0) ? true : false);
                            }
                        }
                        else
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("ยังไม่ได้ใส่รหัสเจ้าหนี้"));
                            Control __getControl = this._icTransScreenTop._getControl(_g.d.ic_trans._cust_code);
                            __getControl.Focus();
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    break;
            } //ปิด Save Button

            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA") && this._transControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้)
            {
                this._icTransRef._transGrid._alterCellUpdate += (gridSender, row, column) =>
                {
                    if (column == this._icTransRef._transGrid._findColumnByName(_g.d.ap_ar_trans_detail._billing_no))
                    {
                        string __refDocNo = this._icTransRef._transGrid._cellGet(row, _g.d.ap_ar_trans_detail._billing_no).ToString();

                        // ดึงที่อยู่จัดส่ง
                        if (__refDocNo.Length > 0)
                        {
                            string __getShipmentRef = "select * from ic_trans_shipment where doc_no = '" + __refDocNo + "' ";
                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            DataTable __shipmentRef = __myFrameWork._queryShort(__getShipmentRef).Tables[0];
                            if (this._shipmentControl != null)
                            {
                                this._shipmentControl._shipmentScreen._loadData(__shipmentRef);
                            }

                            // update doc_ref screen top
                            DateTime __docDate = (DateTime)this._icTransRef._transGrid._cellGet(row, _g.d.ap_ar_trans_detail._billing_date);

                            this._icTransScreenTop._setDataStr(_g.d.ic_trans._doc_ref, __refDocNo);
                            this._icTransScreenTop._setDataDate(_g.d.ic_trans._doc_ref_date, __docDate);


                        }
                    }
                };
            }
        }

        public void _loadDataToScreen(string docNo, string whereString)
        {
            this._myManageTrans__clearData();

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            if (this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ && this._loadFromDataCenter)
            {
                __myFrameWork = new MyLib._myFrameWork(_g.g._companyProfile._activeSyncServer, "SMLConfig" + _g.g._companyProfile._activeSyncProvider + ".xml", MyLib._myGlobal._databaseType.PostgreSql);
                __myFrameWork._findDatabaseType();
            }
            StringBuilder __query = new StringBuilder();
            __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
            int __tableCount = 0;
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + this._icTransTable + " " + whereString));
            string __extraQuery = "";
            if (this._transControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก || this._transControlType == _g.g._transControlTypeEnum.สินค้า_ขอโอน)
            {
                __extraQuery = "," + _g.d.ic_trans_detail._wh_code_2 + "," +
                    "(select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code + "=" + _g.d.ic_trans_detail._wh_code_2 + ") as " + _g.d.ic_trans_detail._wh_name_2 + "," +
                    _g.d.ic_trans_detail._shelf_code_2 + ",(select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._whcode + "=" + _g.d.ic_trans_detail._wh_code_2 + " and " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code + "=" + _g.d.ic_trans_detail._shelf_code_2 + ") as " + _g.d.ic_trans_detail._shelf_name_2;
            }
            else if (this._transControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร)
            {
                __extraQuery = "," + _g.d.ic_trans_detail._transfer_amount + "," + _g.d.ic_trans_detail._fee_amount;
            }
            else if (this._transControlType == _g.g._transControlTypeEnum.สินค้า_บันทึกการจัดส่ง)
            {
                __extraQuery = ",(select name_1 from ar_customer where ar_customer.code = ic_trans_detail.item_name ) as " + _g.d.ic_trans._cust_name;
            }

            if (_g.g._companyProfile._multi_currency)
            {
                __extraQuery += ",(case when coalesce((select currency_code from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag ), '') = '' then price else price_2 end ) as  " + _g.d.ic_trans_detail._price_2;
                __extraQuery += ",(case when coalesce((select currency_code from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag ), '') = '' then sum_amount else sum_amount_2 end ) as  " + _g.d.ic_trans_detail._sum_amount_2;
            }

            // toe เพิ่ม flag รายการใบกำกับภาษีอย่างเต็มออกแทน flag เก่า
            string __oldFullInvoiceFlagQuery = " and ( (" + _g.d.ic_trans_detail._trans_type + "=" + _getTransType + " and " + _g.d.ic_trans_detail._trans_flag + "=" + _getTransFlag + ") ) "; // or (" + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + " and " + _g.d.ic_trans_detail._trans_type + "= " + _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + " ) )";

            if (this._transControlType == _g.g._transControlTypeEnum.คลัง_ตรวจนับสินค้า ||
                                this._transControlType == _g.g._transControlTypeEnum.คลัง_ปรับปรุงเพิ่มลดสินค้า ||
                                this._transControlType == _g.g._transControlTypeEnum.คลัง_ยอดคงเหลือสินค้ายกมา ||
                                this._transControlType == _g.g._transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ ||
                                this._transControlType == _g.g._transControlTypeEnum.คลัง_จ่ายสินค้าออก_จากการขาย ||
                                this._transControlType == _g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก ||
                                this._transControlType == _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก ||
                                this._transControlType == _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก)
            {
                __tableCount++;
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans_detail._average_cost + "," +
                    "(select " + _g.d.ic_unit_use._stand_value + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=" + this._icTransDetailTable + "." + _g.d.ic_trans_detail._item_code + " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=(select " + _g.d.ic_inventory._unit_cost + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + this._icTransDetailTable + "." + _g.d.ic_trans_detail._item_code + ")" + ") as " + this._icTransItemGrid._columnAverageCostUnitStand + "," +
                    "(select " + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=" + this._icTransDetailTable + "." + _g.d.ic_trans_detail._item_code + " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=(select " + _g.d.ic_inventory._unit_cost + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + this._icTransDetailTable + "." + _g.d.ic_trans_detail._item_code + ")" + ") as " + this._icTransItemGrid._columnAverageCostUnitDiv + "," +
                    _g.d.ic_trans_detail._trans_type + "," +
                    _g.d.ic_trans_detail._trans_flag + "," +
                    _g.d.ic_trans_detail._doc_group + "," +
                    _g.d.ic_trans_detail._doc_date + "," +
                    _g.d.ic_trans_detail._doc_date_calc + "," +
                    _g.d.ic_trans_detail._doc_time_calc + "," +
                    _g.d.ic_trans_detail._doc_no + "," +
                    _g.d.ic_trans_detail._doc_ref + "," +
                    _g.d.ic_trans_detail._cust_code + "," +
                    _g.d.ic_trans_detail._inquiry_type + "," +
                    _g.d.ic_trans_detail._item_code_main + "," +
                    _g.d.ic_trans_detail._chq_number + "," +
                    _g.d.ic_trans_detail._item_code_2 + "," +
                    _g.d.ic_trans_detail._bank_name_2 + "," +
                    _g.d.ic_trans_detail._bank_branch_2 + "," +
                    _g.d.ic_trans_detail._tax_type + "," +
                    _g.d.ic_trans_detail._item_code + "," +
                    _g.d.ic_trans_detail._barcode + "," +
                    _g.d.ic_trans_detail._item_name + "," +
                    _g.d.ic_trans_detail._date_expire + "," +
                    _g.d.ic_trans_detail._unit_code + "," +
                    "(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_trans_detail._unit_code + ") as " + _g.d.ic_trans_detail._unit_name + "," +
                    _g.d.ic_trans_detail._qty + "," +
                    _g.d.ic_trans_detail._price + "," +
                    _g.d.ic_trans_detail._discount + "," +
                    _g.d.ic_trans_detail._sum_of_cost + "," +
                    _g.d.ic_trans_detail._is_permium + "," +
                    _g.d.ic_trans_detail._sum_amount + "," +
                    _g.d.ic_trans_detail._due_date + "," +
                    _g.d.ic_trans_detail._remark + "," +
                    _g.d.ic_trans_detail._user_approve + "," +
                    _g.d.ic_trans_detail._price_type + "," +
                    _g.d.ic_trans_detail._price_mode + "," +
                    _g.d.ic_trans_detail._status + "," +
                    _g.d.ic_trans_detail._is_get_price + "," +
                    _g.d.ic_trans_detail._line_number + "," +
                    _g.d.ic_trans_detail._date_due + "," +
                    _g.d.ic_trans_detail._ref_doc_no + "," +
                    _g.d.ic_trans_detail._ref_doc_date + "," +
                    _g.d.ic_trans_detail._ref_line_number + "," +
                    _g.d.ic_trans_detail._ref_cust_code + "," +
                    _g.d.ic_trans_detail._branch_code + "," +
                    _g.d.ic_trans_detail._wh_code + "," +
                    "(select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code + "=" + _g.d.ic_trans_detail._wh_code + ") as " + _g.d.ic_trans_detail._wh_name + "," +
                    _g.d.ic_trans_detail._shelf_code + "," +
                    "(select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._whcode + "=" + _g.d.ic_trans_detail._wh_code + " and " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code + "=" + _g.d.ic_trans_detail._shelf_code + ") as " + _g.d.ic_trans_detail._shelf_name + "," +
                    _g.d.ic_trans_detail._department_code + "," +
                    _g.d.ic_trans_detail._total_vat_value + "," +
                    _g.d.ic_trans_detail._cancel_qty + "," +
                    _g.d.ic_trans_detail._total_qty + "," +
                    _g.d.ic_trans_detail._stand_value + "," +
                    _g.d.ic_trans_detail._divide_value + "," +
                    _g.d.ic_trans_detail._ratio + "," +
                    _g.d.ic_trans_detail._ic_pattern + "," +
                    _g.d.ic_trans_detail._ic_color + "," +
                    _g.d.ic_trans_detail._ic_size + "," +
                    _g.d.ic_trans_detail._is_serial_number + "," +
                    _g.d.ic_trans_detail._item_type + "," +
                    "(select " + _g.d.ic_inventory._unit_type + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + this._icTransDetailTable + "." + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_trans_detail._unit_type + "," +
                    "(select " + _g.d.ic_inventory._cost_type + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + this._icTransDetailTable + "." + _g.d.ic_trans_detail._item_code + ") as " + this._icTransItemGrid._columnCostType + "," +
                    _g.d.ic_trans_detail._set_ref_line + "," +
                    _g.d.ic_trans_detail._set_ref_price + "," +
                    _g.d.ic_trans_detail._set_ref_qty + "," +
                    _g.d.ic_trans_detail._ref_row + "," +
                    _g.d.ic_trans_detail._hidden_cost_1 + "," +
                    _g.d.ic_trans_detail._sum_amount_exclude_vat + "," +
                    _g.d.ic_trans_detail._hidden_cost_1_exclude_vat + "," +
                    _g.d.ic_trans_detail._price_exclude_vat + "," +
                    _g.d.ic_trans_detail._discount_amount + "," +
                    _g.d.ic_trans_detail._temp_float_1 + "," +
                    _g.d.ic_trans_detail._temp_float_2 + "," +
                    _g.d.ic_trans_detail._temp_string_1 + "," +
                    _g.d.ic_trans_detail._doc_ref_type + "," +
                    _g.d.ic_trans_detail._ref_guid + "," +
                    _g.d.ic_trans_detail._lot_number_1 + "," +
                    _g.d.ic_trans_detail._mfd_date + "," +
                    _g.d.ic_trans_detail._mfn_name + "," +
                    _g.d.ic_trans_detail._priority_level + __extraQuery +
                    " from " + this._icTransDetailTable + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + docNo + "\' " + ((this._transControlType == _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน) ? __oldFullInvoiceFlagQuery : " and " + _g.d.ic_trans_detail._trans_type + "=" + _getTransType + " and " + _g.d.ic_trans_detail._trans_flag + "=" + _getTransFlag) +
                    " order by " + _g.d.ic_trans_detail._line_number));
            }
            else
            {
                __tableCount++;
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans_detail._average_cost + "," +
                    "(select " + _g.d.ic_unit_use._stand_value + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=" + this._icTransDetailTable + "." + _g.d.ic_trans_detail._item_code + " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=(select " + _g.d.ic_inventory._unit_cost + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + this._icTransDetailTable + "." + _g.d.ic_trans_detail._item_code + ")" + ") as " + this._icTransItemGrid._columnAverageCostUnitStand + "," +
                    "(select " + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=" + this._icTransDetailTable + "." + _g.d.ic_trans_detail._item_code + " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=(select " + _g.d.ic_inventory._unit_cost + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + this._icTransDetailTable + "." + _g.d.ic_trans_detail._item_code + ")" + ") as " + this._icTransItemGrid._columnAverageCostUnitDiv + "," +
                    _g.d.ic_trans_detail._trans_type + "," +
                    _g.d.ic_trans_detail._trans_flag + "," +
                    _g.d.ic_trans_detail._doc_group + "," +
                    _g.d.ic_trans_detail._doc_date + "," +
                    _g.d.ic_trans_detail._doc_date_calc + "," +
                    _g.d.ic_trans_detail._doc_time_calc + "," +
                    _g.d.ic_trans_detail._doc_no + "," +
                    _g.d.ic_trans_detail._doc_ref + "," +
                    _g.d.ic_trans_detail._cust_code + "," +
                    _g.d.ic_trans_detail._inquiry_type + "," +
                    _g.d.ic_trans_detail._item_code_main + "," +
                    _g.d.ic_trans_detail._chq_number + "," +
                    _g.d.ic_trans_detail._bank_branch + "," +
                    _g.d.ic_trans_detail._bank_name + "," +
                    _g.d.ic_trans_detail._item_code_2 + "," +
                    _g.d.ic_trans_detail._bank_name_2 + "," +
                    _g.d.ic_trans_detail._bank_branch_2 + "," +
                    _g.d.ic_trans_detail._tax_type + "," +
                    _g.d.ic_trans_detail._item_code + "," +
                    _g.d.ic_trans_detail._barcode + "," +
                    _g.d.ic_trans_detail._item_name + "," +
                    _g.d.ic_trans_detail._date_expire + "," +
                    _g.d.ic_trans_detail._unit_code + "," +
                    "(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_trans_detail._unit_code + ") as " + _g.d.ic_trans_detail._unit_name + "," +
                    _g.d.ic_trans_detail._qty + "," +
                    _g.d.ic_trans_detail._price + "," +
                    _g.d.ic_trans_detail._discount + "," +
                    _g.d.ic_trans_detail._sum_of_cost + "," +
                    _g.d.ic_trans_detail._is_permium + "," +
                    _g.d.ic_trans_detail._sum_amount + "," +
                    _g.d.ic_trans_detail._due_date + "," +
                    _g.d.ic_trans_detail._remark + "," +
                    _g.d.ic_trans_detail._user_approve + "," +
                    _g.d.ic_trans_detail._price_type + "," +
                    _g.d.ic_trans_detail._price_mode + "," +
                    _g.d.ic_trans_detail._status + "," +
                    _g.d.ic_trans_detail._is_get_price + "," +
                    _g.d.ic_trans_detail._line_number + "," +
                    _g.d.ic_trans_detail._date_due + "," +
                    _g.d.ic_trans_detail._ref_doc_no + "," +
                    _g.d.ic_trans_detail._ref_doc_date + "," +
                    _g.d.ic_trans_detail._ref_line_number + "," +
                    _g.d.ic_trans_detail._ref_cust_code + "," +
                    _g.d.ic_trans_detail._branch_code + "," +
                    _g.d.ic_trans_detail._wh_code + "," +
                    "(select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code + "=" + _g.d.ic_trans_detail._wh_code + ") as " + _g.d.ic_trans_detail._wh_name + "," +
                    _g.d.ic_trans_detail._shelf_code + "," +
                    "(select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._whcode + "=" + _g.d.ic_trans_detail._wh_code + " and " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code + "=" + _g.d.ic_trans_detail._shelf_code + ") as " + _g.d.ic_trans_detail._shelf_name + "," +
                    _g.d.ic_trans_detail._department_code + "," +
                    _g.d.ic_trans_detail._total_vat_value + "," +
                    _g.d.ic_trans_detail._cancel_qty + "," +
                    _g.d.ic_trans_detail._total_qty + "," +
                    _g.d.ic_trans_detail._stand_value + "," +
                    _g.d.ic_trans_detail._divide_value + "," +
                    _g.d.ic_trans_detail._ratio + "," +
                    _g.d.ic_trans_detail._ic_pattern + "," +
                    _g.d.ic_trans_detail._ic_color + "," +
                    _g.d.ic_trans_detail._ic_size + "," +
                    _g.d.ic_trans_detail._is_serial_number + "," +
                    _g.d.ic_trans_detail._item_type + "," +
                    "(select " + _g.d.ic_inventory._unit_type + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + this._icTransDetailTable + "." + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_trans_detail._unit_type + "," +
                    "(select " + _g.d.ic_inventory._cost_type + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + this._icTransDetailTable + "." + _g.d.ic_trans_detail._item_code + ") as " + this._icTransItemGrid._columnCostType + "," +
                    _g.d.ic_trans_detail._set_ref_line + "," +
                    _g.d.ic_trans_detail._set_ref_price + "," +
                    _g.d.ic_trans_detail._set_ref_qty + "," +
                    _g.d.ic_trans_detail._ref_row + "," +
                    _g.d.ic_trans_detail._hidden_cost_1 + "," +
                    _g.d.ic_trans_detail._sum_amount_exclude_vat + "," +
                    _g.d.ic_trans_detail._hidden_cost_1_exclude_vat + "," +
                    _g.d.ic_trans_detail._price_exclude_vat + "," +
                    _g.d.ic_trans_detail._discount_amount + "," +
                    _g.d.ic_trans_detail._temp_float_1 + "," +
                    _g.d.ic_trans_detail._temp_float_2 + "," +
                    _g.d.ic_trans_detail._temp_string_1 + "," +
                    _g.d.ic_trans_detail._doc_ref_type + "," +
                    _g.d.ic_trans_detail._ref_guid + "," +
                    _g.d.ic_trans_detail._lot_number_1 + "," +
                    _g.d.ic_trans_detail._mfd_date + "," +
                    _g.d.ic_trans_detail._mfn_name + "," +
                    _g.d.ic_trans_detail._priority_level + __extraQuery +
                    " from " + this._icTransDetailTable + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + docNo + "\' " + ((this._transControlType == _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน) ? __oldFullInvoiceFlagQuery : " and " + _g.d.ic_trans_detail._trans_type + "=" + _getTransType + " and " + _g.d.ic_trans_detail._trans_flag + "=" + _getTransFlag) +
                    " order by " + _g.d.ic_trans_detail._line_number));
            }
            // เอกสารอ้างอิง
            string __queryFieldRef = "select " + MyLib._myGlobal._fieldAndComma(_g.d.ap_ar_trans_detail._bill_type, _g.d.ap_ar_trans_detail._billing_no, _g.d.ap_ar_trans_detail._billing_date, _g.d.ap_ar_trans_detail._sum_debt_balance, _g.d.ap_ar_trans_detail._sum_debt_value, _g.d.ap_ar_trans_detail._final_amount, _g.d.ap_ar_trans_detail._sum_debt_amount, _g.d.ap_ar_trans_detail._remark, _g.d.ap_ar_trans_detail._ref_doc_no, _g.d.ap_ar_trans_detail._ref_doc_date) + " from " + _g.d.ap_ar_trans_detail._table;
            string __queryFieldRefFull = __queryFieldRef + " where " + _g.d.ap_ar_trans_detail._doc_no + "=\'" + docNo + "\' " + ((this._transControlType == _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน) ? __oldFullInvoiceFlagQuery : " and " + _g.d.ap_ar_trans_detail._trans_type + "=" + _getTransType + " and " + _g.d.ap_ar_trans_detail._trans_flag + "=" + _getTransFlag) + " order by " + _g.d.ap_ar_trans_detail._line_number;
            switch (this._transControlType)
            {
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:

                case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:
                case _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:

                case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                    __tableCount++;
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryFieldRefFull));
                    break;
            }
            // จ่ายเงิน
            int __payTableNumber = -1;
            int __whtTableNumber = -1;
            if (this._payControl != null)
            {
                __payTableNumber = __tableCount + 1;
                __query.Append(this._payControl._queryLoad(docNo));
                __tableCount += this._payControl._tableLoadCount; // มี 9 query ใน _queryLoad
                //
                if (this._withHoldingTax != null)
                {
                    __whtTableNumber = __tableCount + 1;
                    __tableCount += 2;
                    __query.Append(this._withHoldingTax._queryLoad(docNo, this._transControlType));
                }
            }
            int __vatBuyTableNumber = -1;
            if (this._vatBuy != null)
            {
                __tableCount++;
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.gl_journal_vat_buy._table + " where " + _g.d.gl_journal_vat_buy._doc_no + "=\'" + docNo + "\' and " + _g.d.gl_journal_vat_buy._trans_flag + "=" + _getTransFlag.ToString() + " order by line_number"));
                __vatBuyTableNumber = __tableCount;
            }
            int __vatSaleTableNumber = -1;
            if (this._vatSale != null)
            {
                // toe ชั่วคราว
                string __fullInvoiceVatSaleFilter = " and ((" + _g.d.gl_journal_vat_sale._trans_flag + "=" + _getTransFlag.ToString() + ") or (" + _g.d.gl_journal_vat_sale._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + "))";

                __tableCount++;
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.gl_journal_vat_sale._table + " where " + _g.d.gl_journal_vat_sale._doc_no + "=\'" + docNo + "\' " + ((this._transControlType == _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน) ? __fullInvoiceVatSaleFilter : " and " + _g.d.gl_journal_vat_sale._trans_flag + "=" + _getTransFlag.ToString()) + " order by line_number"));
                __vatSaleTableNumber = __tableCount;
            }
            // มัดจำ
            int __advanceTableNumber = -1;
            if (this._payAdvance != null)
            {
                __tableCount++;
                __query.Append(this._payAdvance._queryLoad(docNo));
                __advanceTableNumber = __tableCount;
            }
            // Serial Number
            int __serialNumberTableNumber = ++__tableCount;
            string __serialNumberFullInvoideTransflag = " and ((" + _g.d.ic_trans_serial_number._trans_flag + "=" + _getTransFlag.ToString() + ") or (" + _g.d.ic_trans_serial_number._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + "))"; // toe
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_trans_serial_number._table + " where " + _g.d.ic_trans_serial_number._doc_no + "=\'" + docNo + "\' " + ((this._transControlType == _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน) ? __serialNumberFullInvoideTransflag : " and " + _g.d.ic_trans_serial_number._trans_flag + "=" + _getTransFlag.ToString()) + " and " + _g.d.ic_trans_serial_number._line_number + "<>-1 order by " + _g.d.ic_trans_serial_number._line_number));
            // Load Gl
            int __glTableNumber = -1;
            if (this._glScreenTop != null)
            {
                string __extraWhereGL = "";
                if (this._transControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก)
                {
                    __extraWhereGL = " AND trans_flag =" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนออก) + " ";
                }

                __glTableNumber = __tableCount + 1;
                __query.Append(this._glDetail._loadDataQuery(docNo, __extraWhereGL));
                __tableCount += 7;
            }


            int __docPictureTableNumber = -1;
            __docPictureTableNumber = ++__tableCount;
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select count(*) as image_count from " + _g.d.sml_doc_images._table + " where " + _g.d.sml_doc_images._image_id + "=\'" + docNo + "\'"));


            // department
            int __departmentTableNumber = ++__tableCount;
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select *,(select " + _g.d.erp_department_list._name_1 + " from " + _g.d.erp_department_list._table + " where " + _g.d.erp_department_list._table + "." + _g.d.erp_department_list._code + " = " + _g.d.ic_trans_detail_department._table + "." + _g.d.ic_trans_detail_department._department_code + ") as " + _g.d.ic_trans_detail_department._department_name + " from " + _g.d.ic_trans_detail_department._table + " where " + _g.d.ic_trans_detail_department._doc_no + "=\'" + docNo + "\' " + ((this._transControlType == _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน) ? __serialNumberFullInvoideTransflag : " and " + _g.d.ic_trans_detail_department._trans_flag + "=" + _getTransFlag.ToString()) + " and " + _g.d.ic_trans_detail_department._line_number + "<>-1 order by " + _g.d.ic_trans_detail_department._line_number));

            // project 
            int __projectTableNumber = ++__tableCount;
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select *,(select " + _g.d.erp_project_list._name_1 + " from " + _g.d.erp_project_list._table + " where " + _g.d.erp_project_list._table + "." + _g.d.erp_project_list._code + " = " + _g.d.ic_trans_detail_project._table + "." + _g.d.ic_trans_detail_project._project_code + ") as " + _g.d.ic_trans_detail_project._project_name + " from " + _g.d.ic_trans_detail_project._table + " where " + _g.d.ic_trans_detail_project._doc_no + "=\'" + docNo + "\' " + ((this._transControlType == _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน) ? __serialNumberFullInvoideTransflag : " and " + _g.d.ic_trans_detail_project._trans_flag + "=" + _getTransFlag.ToString()) + " and " + _g.d.ic_trans_detail_project._line_number + "<>-1 order by " + _g.d.ic_trans_detail_project._line_number));

            // allocate
            int __allocateTableNumber = ++__tableCount;
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select *,(select " + _g.d.erp_allocate_list._name_1 + " from " + _g.d.erp_allocate_list._table + " where " + _g.d.erp_allocate_list._table + "." + _g.d.erp_allocate_list._code + " = " + _g.d.ic_trans_detail_allocate._table + "." + _g.d.ic_trans_detail_allocate._allocate_code + ") as " + _g.d.ic_trans_detail_allocate._allocate_name + " from " + _g.d.ic_trans_detail_allocate._table + " where " + _g.d.ic_trans_detail_allocate._doc_no + "=\'" + docNo + "\' " + ((this._transControlType == _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน) ? __serialNumberFullInvoideTransflag : " and " + _g.d.ic_trans_detail_allocate._trans_flag + "=" + _getTransFlag.ToString()) + " and " + _g.d.ic_trans_detail_allocate._line_number + "<>-1 order by " + _g.d.ic_trans_detail_allocate._line_number));

            // site
            int __siteTableNumber = ++__tableCount;
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select *,(select " + _g.d.erp_side_list._name_1 + " from " + _g.d.erp_side_list._table + " where " + _g.d.erp_side_list._table + "." + _g.d.erp_side_list._code + " = " + _g.d.ic_trans_detail_site._table + "." + _g.d.ic_trans_detail_site._site_code + ") as " + _g.d.ic_trans_detail_site._site_name + " from " + _g.d.ic_trans_detail_site._table + " where " + _g.d.ic_trans_detail_site._doc_no + "=\'" + docNo + "\' " + ((this._transControlType == _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน) ? __serialNumberFullInvoideTransflag : " and " + _g.d.ic_trans_detail_site._trans_flag + "=" + _getTransFlag.ToString()) + " and " + _g.d.ic_trans_detail_site._line_number + "<>-1 order by " + _g.d.ic_trans_detail_site._line_number));

            // job
            int __jobTableNumber = ++__tableCount;
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select *,(select " + _g.d.erp_job_list._name_1 + " from " + _g.d.erp_job_list._table + " where " + _g.d.erp_job_list._table + "." + _g.d.erp_job_list._code + " = " + _g.d.ic_trans_detail_jobs._table + "." + _g.d.ic_trans_detail_jobs._job_code + ") as " + _g.d.ic_trans_detail_jobs._job_name + " from " + _g.d.ic_trans_detail_jobs._table + " where " + _g.d.ic_trans_detail_jobs._doc_no + "=\'" + docNo + "\' " + ((this._transControlType == _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน) ? __serialNumberFullInvoideTransflag : " and " + _g.d.ic_trans_detail_jobs._trans_flag + "=" + _getTransFlag.ToString()) + " and " + _g.d.ic_trans_detail_jobs._line_number + "<>-1 order by " + _g.d.ic_trans_detail_jobs._line_number));

            int __shipmentTableNumber = 0;
            if (this._shipmentControl != null)
            {
                __shipmentTableNumber = ++__tableCount;
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_trans_shipment._table + " where " + _g.d.ic_trans_shipment._doc_no + "=\'" + docNo + "\' and " + _g.d.ic_trans_shipment._trans_flag + '=' + _getTransFlag));
            }
            __query.Append("</node>");

            string __debugStr = __query.ToString();

            ArrayList __getData = __myFrameWork._queryListGetData((this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ && this._loadFromDataCenter) ? _g.g._companyProfile._activeSyncDatabase : MyLib._myGlobal._databaseName, __query.ToString());
            // หัวกับรายการ
            DataTable __dt1 = ((DataSet)__getData[0]).Tables[0];
            this._icTransScreenTop._docFormatCode = "";
            try
            {
                this._icTransScreenTop._docFormatCode = __dt1.Rows[0][_g.d.ic_trans._doc_format_code].ToString();
            }
            catch
            {
            }
            this._icTransScreenTop._loadData(__dt1);
            this._icTransScreenTop._oldTaxDocNo = this._icTransScreenTop._getDataStr(_g.d.ic_trans._tax_doc_no);
            this._icTransScreenTop._last_cust_code = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
            this._icTransScreenTop._last_check_cust_code = this._icTransScreenTop._last_cust_code;
            this._icTransScreenTop._last_check_doc_date = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_date);
            // ท้ายรายการ
            this._icTransScreenBottom._loadData(((DataSet)__getData[0]).Tables[0]);
            // อื่นๆ
            this._icTransScreenMore._loadData(((DataSet)__getData[0]).Tables[0]);
            //
            this._icTransItemGrid._loadFromDataTable(((DataSet)__getData[1]).Tables[0]);
            // เอา Serial Number เข้าใน Grid
            DataTable __serialNumberTable = ((DataSet)__getData[__serialNumberTableNumber]).Tables[0];
            for (int __loop1 = 0; __loop1 < this._icTransItemGrid._rowData.Count; __loop1++)
            {
                // โต๋ แก้ไขกรณี เช็คจาก ลำดับบรรทัดใน grid เปลี่ยนไปเช็คจากลำดับบรรทัดใน DataTable

                int __lineCheck = __loop1;
                int __lineNumberFromDB = MyLib._myGlobal._intPhase(((DataSet)__getData[1]).Tables[0].Rows[__loop1][_g.d.ic_trans_detail._line_number].ToString());
                if (__lineCheck != __lineNumberFromDB)
                {
                    __lineCheck = __lineNumberFromDB;
                }

                _icTransItemGridControl._serialNumberStruct __serial = new _icTransItemGridControl._serialNumberStruct();
                Boolean __update = false;
                Decimal __qtySum = 0M;
                for (int __loop2 = 0; __loop2 < __serialNumberTable.Rows.Count; __loop2++)
                {
                    DataRow __data = (DataRow)__serialNumberTable.Rows[__loop2];
                    if (((int)MyLib._myGlobal._decimalPhase(__data[_g.d.ic_trans_serial_number._doc_line_number].ToString())) == __lineCheck)
                    {
                        _icTransItemGridControl._serialNumberDetailStruct __detail = new _icTransItemGridControl._serialNumberDetailStruct();
                        __detail._serialNumber = __data[_g.d.ic_trans_serial_number._serial_number].ToString();
                        __detail._description = __data[_g.d.ic_trans_serial_number._description].ToString();
                        __detail._price = MyLib._myGlobal._decimalPhase(__data[_g.d.ic_trans_serial_number._price].ToString());
                        __detail._voidDate = MyLib._myGlobal._convertDateFromQuery(__data[_g.d.ic_trans_serial_number._void_date].ToString());
                        __serial.__details.Add(__detail);
                        __update = true;
                        __qtySum++;
                    }
                }
                if (__update)
                {
                    this._icTransItemGrid._cellUpdate(__loop1, this._icTransItemGrid._columnSerialNumber, __serial, false);
                    this._icTransItemGrid._cellUpdate(__loop1, this._icTransItemGrid._columnSerialNumberCount, __qtySum, false);
                }
            }

            // เอา deparement เข้า grid
            DataTable __departmentTable = ((DataSet)__getData[__departmentTableNumber]).Tables[0];
            for (int __loop1 = 0; __loop1 < this._icTransItemGrid._rowData.Count; __loop1++)
            {
                // โต๋ แก้ไขกรณี เช็คจาก ลำดับบรรทัดใน grid เปลี่ยนไปเช็คจากลำดับบรรทัดใน DataTable

                int __lineCheck = __loop1;
                int __lineNumberFromDB = MyLib._myGlobal._intPhase(((DataSet)__getData[1]).Tables[0].Rows[__loop1][_g.d.ic_trans_detail._line_number].ToString());
                if (__lineCheck != __lineNumberFromDB)
                {
                    __lineCheck = __lineNumberFromDB;
                }

                _icTransItemGridControl.icTransWeightStruct __detail = new _icTransItemGridControl.icTransWeightStruct();
                Boolean __update = false;
                for (int __loop2 = 0; __loop2 < __departmentTable.Rows.Count; __loop2++)
                {
                    DataRow __data = (DataRow)__departmentTable.Rows[__loop2];
                    if (((int)MyLib._myGlobal._decimalPhase(__data[_g.d.ic_trans_detail_department._doc_line_number].ToString())) == __lineCheck)
                    {
                        _icTransItemGridControl.icTransWeightDetailStruct __department = new _icTransItemGridControl.icTransWeightDetailStruct();
                        __department._code = __data[_g.d.ic_trans_detail_department._department_code].ToString();
                        __department._name = __data[_g.d.ic_trans_detail_department._department_name].ToString();
                        __department._ratio = MyLib._myGlobal._decimalPhase(__data[_g.d.ic_trans_detail_department._ratio].ToString());
                        __department._amount = MyLib._myGlobal._decimalPhase(__data[_g.d.ic_trans_detail_department._amount].ToString());
                        __detail.__details.Add(__department);
                        __update = true;
                    }
                }
                if (__update)
                {
                    this._icTransItemGrid._cellUpdate(__loop1, this._icTransItemGrid._columnDepartment, __detail, false);
                }
            }

            // เอา project เข้า grid
            DataTable __projectTable = ((DataSet)__getData[__projectTableNumber]).Tables[0];
            for (int __loop1 = 0; __loop1 < this._icTransItemGrid._rowData.Count; __loop1++)
            {
                // โต๋ แก้ไขกรณี เช็คจาก ลำดับบรรทัดใน grid เปลี่ยนไปเช็คจากลำดับบรรทัดใน DataTable

                int __lineCheck = __loop1;
                int __lineNumberFromDB = MyLib._myGlobal._intPhase(((DataSet)__getData[1]).Tables[0].Rows[__loop1][_g.d.ic_trans_detail._line_number].ToString());
                if (__lineCheck != __lineNumberFromDB)
                {
                    __lineCheck = __lineNumberFromDB;
                }

                _icTransItemGridControl.icTransWeightStruct __detail = new _icTransItemGridControl.icTransWeightStruct();
                Boolean __update = false;
                for (int __loop2 = 0; __loop2 < __projectTable.Rows.Count; __loop2++)
                {
                    DataRow __data = (DataRow)__projectTable.Rows[__loop2];
                    if (((int)MyLib._myGlobal._decimalPhase(__data[_g.d.ic_trans_detail_project._doc_line_number].ToString())) == __lineCheck)
                    {
                        _icTransItemGridControl.icTransWeightDetailStruct __project = new _icTransItemGridControl.icTransWeightDetailStruct();
                        __project._code = __data[_g.d.ic_trans_detail_project._project_code].ToString();
                        __project._name = __data[_g.d.ic_trans_detail_project._project_name].ToString();
                        __project._ratio = MyLib._myGlobal._decimalPhase(__data[_g.d.ic_trans_detail_project._ratio].ToString());
                        __project._amount = MyLib._myGlobal._decimalPhase(__data[_g.d.ic_trans_detail_project._amount].ToString());
                        __detail.__details.Add(__project);
                        __update = true;
                    }
                }
                if (__update)
                {
                    this._icTransItemGrid._cellUpdate(__loop1, this._icTransItemGrid._columnProject, __detail, false);
                }
            }

            // เอา allocate เข้า grid
            DataTable __allocateTable = ((DataSet)__getData[__allocateTableNumber]).Tables[0];
            for (int __loop1 = 0; __loop1 < this._icTransItemGrid._rowData.Count; __loop1++)
            {
                // โต๋ แก้ไขกรณี เช็คจาก ลำดับบรรทัดใน grid เปลี่ยนไปเช็คจากลำดับบรรทัดใน DataTable

                int __lineCheck = __loop1;
                int __lineNumberFromDB = MyLib._myGlobal._intPhase(((DataSet)__getData[1]).Tables[0].Rows[__loop1][_g.d.ic_trans_detail._line_number].ToString());
                if (__lineCheck != __lineNumberFromDB)
                {
                    __lineCheck = __lineNumberFromDB;
                }

                _icTransItemGridControl.icTransWeightStruct __detail = new _icTransItemGridControl.icTransWeightStruct();
                Boolean __update = false;
                for (int __loop2 = 0; __loop2 < __allocateTable.Rows.Count; __loop2++)
                {
                    DataRow __data = (DataRow)__allocateTable.Rows[__loop2];
                    if (((int)MyLib._myGlobal._decimalPhase(__data[_g.d.ic_trans_detail_allocate._doc_line_number].ToString())) == __lineCheck)
                    {
                        _icTransItemGridControl.icTransWeightDetailStruct __allocate = new _icTransItemGridControl.icTransWeightDetailStruct();
                        __allocate._code = __data[_g.d.ic_trans_detail_allocate._allocate_code].ToString();
                        __allocate._name = __data[_g.d.ic_trans_detail_allocate._allocate_name].ToString();
                        __allocate._ratio = MyLib._myGlobal._decimalPhase(__data[_g.d.ic_trans_detail_allocate._ratio].ToString());
                        __allocate._amount = MyLib._myGlobal._decimalPhase(__data[_g.d.ic_trans_detail_allocate._amount].ToString());
                        __detail.__details.Add(__allocate);
                        __update = true;
                    }
                }
                if (__update)
                {
                    this._icTransItemGrid._cellUpdate(__loop1, this._icTransItemGrid._columnAlloCate, __detail, false);
                }
            }

            // เอา side เข้า grid
            DataTable __sideTable = ((DataSet)__getData[__siteTableNumber]).Tables[0];
            for (int __loop1 = 0; __loop1 < this._icTransItemGrid._rowData.Count; __loop1++)
            {
                // โต๋ แก้ไขกรณี เช็คจาก ลำดับบรรทัดใน grid เปลี่ยนไปเช็คจากลำดับบรรทัดใน DataTable

                int __lineCheck = __loop1;
                int __lineNumberFromDB = MyLib._myGlobal._intPhase(((DataSet)__getData[1]).Tables[0].Rows[__loop1][_g.d.ic_trans_detail._line_number].ToString());
                if (__lineCheck != __lineNumberFromDB)
                {
                    __lineCheck = __lineNumberFromDB;
                }

                _icTransItemGridControl.icTransWeightStruct __detail = new _icTransItemGridControl.icTransWeightStruct();
                Boolean __update = false;
                for (int __loop2 = 0; __loop2 < __sideTable.Rows.Count; __loop2++)
                {
                    DataRow __data = (DataRow)__sideTable.Rows[__loop2];
                    if (((int)MyLib._myGlobal._decimalPhase(__data[_g.d.ic_trans_detail_site._doc_line_number].ToString())) == __lineCheck)
                    {
                        _icTransItemGridControl.icTransWeightDetailStruct __allocate = new _icTransItemGridControl.icTransWeightDetailStruct();
                        __allocate._code = __data[_g.d.ic_trans_detail_site._site_code].ToString();
                        __allocate._name = __data[_g.d.ic_trans_detail_site._site_name].ToString();
                        __allocate._ratio = MyLib._myGlobal._decimalPhase(__data[_g.d.ic_trans_detail_site._ratio].ToString());
                        __allocate._amount = MyLib._myGlobal._decimalPhase(__data[_g.d.ic_trans_detail_site._amount].ToString());
                        __detail.__details.Add(__allocate);
                        __update = true;
                    }
                }
                if (__update)
                {
                    this._icTransItemGrid._cellUpdate(__loop1, this._icTransItemGrid._columnSideList, __detail, false);
                }
            }

            // เอา job เข้า grid
            DataTable __jobTable = ((DataSet)__getData[__jobTableNumber]).Tables[0];
            for (int __loop1 = 0; __loop1 < this._icTransItemGrid._rowData.Count; __loop1++)
            {
                // โต๋ แก้ไขกรณี เช็คจาก ลำดับบรรทัดใน grid เปลี่ยนไปเช็คจากลำดับบรรทัดใน DataTable

                int __lineCheck = __loop1;
                int __lineNumberFromDB = MyLib._myGlobal._intPhase(((DataSet)__getData[1]).Tables[0].Rows[__loop1][_g.d.ic_trans_detail._line_number].ToString());
                if (__lineCheck != __lineNumberFromDB)
                {
                    __lineCheck = __lineNumberFromDB;
                }

                _icTransItemGridControl.icTransWeightStruct __detail = new _icTransItemGridControl.icTransWeightStruct();
                Boolean __update = false;
                for (int __loop2 = 0; __loop2 < __jobTable.Rows.Count; __loop2++)
                {
                    DataRow __data = (DataRow)__jobTable.Rows[__loop2];
                    if (((int)MyLib._myGlobal._decimalPhase(__data[_g.d.ic_trans_detail_jobs._doc_line_number].ToString())) == __lineCheck)
                    {
                        _icTransItemGridControl.icTransWeightDetailStruct __allocate = new _icTransItemGridControl.icTransWeightDetailStruct();
                        __allocate._code = __data[_g.d.ic_trans_detail_jobs._job_code].ToString();
                        __allocate._name = __data[_g.d.ic_trans_detail_jobs._job_name].ToString();
                        __allocate._ratio = MyLib._myGlobal._decimalPhase(__data[_g.d.ic_trans_detail_jobs._ratio].ToString());
                        __allocate._amount = MyLib._myGlobal._decimalPhase(__data[_g.d.ic_trans_detail_jobs._amount].ToString());
                        __detail.__details.Add(__allocate);
                        __update = true;
                    }
                }
                if (__update)
                {
                    this._icTransItemGrid._cellUpdate(__loop1, this._icTransItemGrid._columnJobsList, __detail, false);
                }
            }

            // ท้าย
            switch (this._transControlType)
            {
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย:
                case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                    this._icTransScreenBottom._loadData(((DataSet)__getData[0]).Tables[0]);
                    break;
            }
            this._icTransScreenTop._search(true);
            this._icTransScreenMore._search(false);
            if (this._icTransScreenBottom != null)
                this._icTransScreenBottom._search();

            // อ้างอืง
            switch (this._transControlType)
            {
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:

                case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:
                case _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:

                case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                    this._icTransRef._transGrid._loadFromDataTable(((DataSet)__getData[2]).Tables[0]);

                    // toe doc_ref_old
                    for (int __loop2 = 0; __loop2 < this._icTransRef._transGrid._rowData.Count; __loop2++)
                    {
                        string __getDocNo = this._icTransRef._transGrid._cellGet(__loop2, _g.d.ap_ar_trans_detail._billing_no).ToString().Trim();
                        if (__getDocNo.Length > 0)
                        {
                            this._oldDocRef = this._docNoAdd(this._oldDocRef, __getDocNo);
                        }
                    }
                    break;
            }
            if (this._payControl != null)
            {
                this._payControl._loadToScreen(__getData, __payTableNumber);
                if (this._withHoldingTax != null)
                {
                    this._withHoldingTax._loadToScreen(__getData, __whtTableNumber);
                }
                this._payControl._reCalc();
            }
            //
            if (__advanceTableNumber != -1)
            {
                this._payAdvance._dataGrid._loadFromDataTable(((DataSet)__getData[__advanceTableNumber]).Tables[0]);
            }
            if (__vatBuyTableNumber != -1)
            {
                if (__dt1.Rows[0][_g.d.ic_trans._is_manual_vat].ToString().Equals("1"))
                    this._vatBuy._manualVatCheckbox.Checked = true;

                this._vatBuy._vatGrid._loadFromDataTable(((DataSet)__getData[__vatBuyTableNumber]).Tables[0]);
            }
            if (__vatSaleTableNumber != -1)
            {
                this._vatSale._vatGrid._loadFromDataTable(((DataSet)__getData[__vatSaleTableNumber]).Tables[0]);
            }
            int __refCount = this._icTransRef._transGrid._rowCount(this._icTransRef._transGrid._findColumnByName(_g.d.ap_ar_trans_detail._billing_no));
            this._icTransItemGrid._visableRefColumn((__refCount > 0) ? true : false);
            this._icTransRef.Visible = (__refCount > 0 || this._transControlType == _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก || this._transControlType == _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก) ? true : false;
            this._icTransRef._refCheckStatus = (__refCount > 0) ? true : false;
            // Log
            this._logDetailOld = new StringBuilder();
            this._logDetailOld.Append(this._icTransScreenTop._logCreate("top"));
            this._logDocDateOld = this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_date);
            this._logDocNoOld = this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_no);
            this._logAmountOld = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_amount);
            //
            this._checkEnable();
            // ดึงตัวแปร
            switch (this._transControlType)
            {
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                    {
                        for (int __loop = 0; __loop < this._icTransItemGrid._rowData.Count; __loop++)
                        {
                            this._icTransItemGrid._ictransItemGridControl__alterCellUpdate(null, __loop, 0);
                        }
                    }
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร:
                    {
                        for (int __loop = 0; __loop < this._icTransItemGrid._rowData.Count; __loop++)
                        {
                            this._icTransItemGrid._ictransItemGridControl__alterCellUpdate(null, __loop, 0);
                            this._icTransItemGrid._ictransItemGridControl__alterCellUpdate(null, __loop, this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._item_code_2));
                        }
                    }
                    break;

            }
            // GL
            if (__glTableNumber != -1)
            {
                this._glScreenTop._loadData((((DataSet)__getData[__glTableNumber]).Tables[0]));
                this._glDetail._glDetailGrid._loadFromDataTable(((DataSet)__getData[__glTableNumber + 1]).Tables[0]);
                this._glDetail._loadDataExtra(this._glDetail._glDetailGrid, __getData, __glTableNumber + 2);
            }

            // docPicture

            if (__docPictureTableNumber != -1)
            {
                Boolean __havePictureDoc = MyLib._myGlobal._decimalPhase(((DataSet)__getData[__docPictureTableNumber]).Tables[0].Rows[0][0].ToString()) > 0 ? true : false;
                this._myManageTrans._dataList._docPictureButton.Enabled = __havePictureDoc;

                // check form docpicture is on and loadpicture
                if (this._docPicture != null)
                {
                    this._docPicture._clearpic();
                    if (__havePictureDoc == true)
                    {
                        this._docPicture._loadImage(docNo);
                    }

                }
            }

            // toe creator
            if (__dt1.Rows.Count > 0)
            {
                this._creator_code = __dt1.Rows[0][_g.d.ic_trans._creator_code].ToString();
                this._create_datetime = MyLib._myGlobal._convertDateFromQuery(__dt1.Rows[0][_g.d.ic_trans._create_datetime].ToString());
            }

            if (this._shipmentControl != null)
            {
                DataTable __getShipmentData = ((DataSet)__getData[__shipmentTableNumber]).Tables[0];
                if (__getShipmentData.Rows.Count > 0)
                {
                    /*//this._shipmentControl._search(__getShipmentData.Rows[0][_g.d.ic_trans_shipment._cust_code].ToString(), __getShipmentData.Rows[0][_g.d.ic_trans_shipment._transport_name].ToString());
                    this._shipmentControl._shipmentScreen._setDataStr(_g.d.ap_ar_transport_label._name_1, __getShipmentData.Rows[0][_g.d.ic_trans_shipment._transport_name].ToString());
                    this._shipmentControl._shipmentScreen._setDataStr(_g.d.ap_ar_transport_label._address, __getShipmentData.Rows[0][_g.d.ic_trans_shipment._transport_address].ToString());
                    this._shipmentControl._shipmentScreen._setDataStr(_g.d.ap_ar_transport_label._telephone, __getShipmentData.Rows[0][_g.d.ic_trans_shipment._transport_telephone].ToString());
                    this._shipmentControl._shipmentScreen._setDataStr(_g.d.ap_ar_transport_label._fax, __getShipmentData.Rows[0][_g.d.ic_trans_shipment._transport_fax].ToString());*/
                    this._shipmentControl._shipmentScreen._loadData(__getShipmentData);
                }
            }

            if (_g.g._companyProfile._disabled_edit_doc_no_doc_date == true || _g.g._companyProfile._disable_edit_doc_no_doc_date_user == true)
            {
                this._icTransScreenTop._enabedControl(_g.d.ic_trans._doc_no, false);
            }

            if (_g.g._companyProfile._multi_currency == true)
            {
                this._calc(this._icTransItemGrid);
            }
            this._icTransScreenTop._isChange = false;

            // arm
            if (this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ)
            {
                if (((DataSet)__getData[0]).Tables[0].Rows[0][_g.d.ic_trans._ref_doc_type].ToString().Equals("ARM"))
                {
                    // checkbox arm
                    this._icTransScreenBottom._setCheckBox(_g.d.ic_trans._is_arm, true);
                }
            }

        }

        bool _myManageTrans__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                ArrayList __rowDataArray = (ArrayList)rowData;
                this._oldDocRef = "";
                this._icTransScreenTop._oldDocRef = "";
                this._oldDocNo = __rowDataArray[this._getColumnNumberDocNo()].ToString().ToUpper();
                this._loadDataToScreen(this._oldDocNo, whereString);
                this._setDefaultDate();

                // this._myPanel1.Enabled = this._myToolBar.Enabled;
                if (MyLib._myGlobal._OEMVersion.Equals("SINGHA") &&
                    (this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ ||
                this._transControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้ ||
                this._transControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้)


                    && MyLib._myGlobal._isUserResetPrintLog)
                {
                    this._myManageTrans._dataList._clearLogPrintButton.Enabled = true;
                }
                _arStatusInfo();
                return (true);
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
            return (false);
        }

        int _getColumnNumberDocNo()
        {
            return this._myManageTrans._dataList._gridData._findColumnByName(this._icTransTable + "." + _g.d.ic_trans._doc_no);
        }

        void _myManageTrans__newDataClick()
        {
            this._lastItemCodeInfo = "";
            this._lastDocRefNo = "";
            this._fullInvoiceFromPOS = false;

            this._isImportFromCart = false;
            this._importCartNumber = "";

            this._isImportFromHandHeld = false;
            this._importHandHeldNumber = "";

            this._isImportFromInternet = false;
            this._importInternetNumber = "";

            if (MyLib._myGlobal._isDesignMode == false)
            {
                try
                {
                    this._oldDocNo = "";
                    this._oldDocRef = "";
                    this._icTransScreenTop._oldDocRef = "";

                    this._myManageTrans._mode = 1;
                    if (this._glScreenTop != null)
                    {
                        this._glScreenTop._clear();
                        this._glDetail._glDetailGrid._clear();
                    }
                    //this._myManageTrans__clearData(); // toe เอาออก รับกับ recurring
                    this._icTransScreenBottom._newData();
                    this._icTransScreenTop._newData();
                    this._icTransScreenMore._newData();
                    this._icTransScreenTop._focusFirst();
                    this._icTransItemGrid._visableRefColumn(false);
                    this._icTransItemGrid._serialNumberForm = null;
                    this._icTransItemGrid._searchItemRenew();
                    this._checkEnable();
                    switch (this._transControlType)
                    {
                        case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                            this._itemApprovalSelectButton.Enabled = false;
                            break;
                        case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                        case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                            this._icTransRef.Visible = true;
                            break;

                    }

                    // toe กำหนดค่าเริ่มต้นการขาย
                    if (this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ && _g.g._companyProfile._default_sale_type != 0)
                    {
                        if (_g.g._companyProfile._default_sale_type == 1)
                        {
                            this._icTransScreenTop._setComboBox(_g.d.ic_trans._inquiry_type, 0);

                        }
                        else if (_g.g._companyProfile._default_sale_type == 2)
                        {
                            this._icTransScreenTop._setComboBox(_g.d.ic_trans._inquiry_type, 1);
                        }
                    }

                    if (MyLib._myGlobal._programName.Equals("SML CM"))
                    {
                        switch (this._transControlType)
                        {
                            case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                                this._icTransScreenTop._setDataStr(_g.d.ic_trans._approve_code, MyLib._myGlobal._approve_po_group);
                                break;
                            case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                                this._icTransScreenMore._setDataStr(_g.d.ic_trans._approve_code, MyLib._myGlobal._approve_sr_group);
                                break;
                            case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                                this._icTransScreenMore._setDataStr(_g.d.ic_trans._approve_code, MyLib._myGlobal._approve_ss_group);
                                break;

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                this._icTransScreenTop._isChange = false;
            }
        }

        void _myManageTrans__clearData()
        {
            this._clearScreen();
        }

        void _clearScreen()
        {
            // recurring
            this._requestApproveCode = "";
            this._isEdit = true;

            this._fullInvoiceFromPOS = false;
            this._lastItemCodeInfo = "";
            this._lastDocRefNo = "";
            this._icTransScreenTop._clear();
            this._icTransItemGrid._clear();
            this._icTransItemGrid.AddRow = true;
            //  this._icTransItemGrid
            this._icTransItemGrid._lastApprovePrice = "";

            this._icTransScreenBottom._clear();
            this._icTransScreenMore._clear();
            //
            this._icTransScreenTop._isChange = false;
            this._icTransScreenTop._oldTaxDocNo = "";
            this._icTransScreenMore._isChange = false;
            //
            this._icTransRef._transGrid._clear();
            this._icTransItemGrid._visableRefColumn(false);
            if (this._payAdvance != null) this._payAdvance._dataGrid._clear();
            if (this._payControl != null) this._payControl._clear();
            this._icTransRef._refCheckStatus = false;
            this._icTransRef._refCheck.Enabled = true;
            // Log
            this._logDocNoOld = "\'\'";
            this._logDocDateOld = "\'\'";
            this._logAmountOld = 0M;
            this._logDetailOld = new StringBuilder();
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
                this._vatSale._manualTaxID = false;
            }
            this._icTransScreenTop._docFormatCode = "";

            this._isImportFromCart = false;
            this._importCartNumber = "";

            this._isImportFromHandHeld = false;
            this._importHandHeldNumber = "";

            this._isImportFromInternet = false;
            this._importInternetNumber = "";

            this._creator_code = "";
            this._create_datetime = new DateTime();

            if (this._glScreenTop != null)
            {
                this._glScreenTop._clear();
                this._glDetail._glDetailGrid._clear();
                //this._glDetail._loadDataExtra(this._glDetail._glDetailGrid, __getData, __glTableNumber + 2);
            }

            if (this._shipmentControl != null)
            {
                this._shipmentControl._shipmentScreen._clear();
            }

            this._checkEnable();
        }

        string _dataList__deleteData_getRefDocNo(string docNo)
        {
            switch (this._transControlType)
            {
                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก:
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก:
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก:
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก:
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก:
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก:
                case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ_ยกเลิก:
                case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก_ยกเลิก:
                case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา_ยกเลิก:
                case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก_ยกเลิก:
                case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป_ยกเลิก:
                case _g.g._transControlTypeEnum.สินค้า_โอนเข้า_ยกเลิก:
                case _g.g._transControlTypeEnum.สินค้า_โอนออก_ยกเลิก:
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    string __query = "select " + _g.d.ic_trans._doc_ref + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + docNo + "\'";
                    DataTable __data = __myFrameWork._queryShort(__query).Tables[0];
                    if (__data.Rows.Count > 0)
                    {
                        return __data.Rows[0][_g.d.ic_trans._doc_ref].ToString();
                    }
                    break;
            }
            return "";
        }

        void _dataList__deleteData(ArrayList selectRowOrder)
        {
            //if (__isdeleteData)
            {
                SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
                MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
                ArrayList __itemList = new ArrayList();
                StringBuilder __myQuery = new StringBuilder();
                int __columnDocDate = this._myManageTrans._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date);
                string __result = "";
                string __getDocNo = "";
                string __docNoList = "";

                // กรณียกเลิก ให้ไปเอารหัสเก่าไปประมวล (จากเลขที่อ้างอืง)
                switch (this._transControlType)
                {
                    case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                        {
                            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                            for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
                            {
                                MyLib._deleteDataType __getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                                int __columnDocNo = this._getColumnNumberDocNo();
                                __getDocNo = this._myManageTrans._dataList._gridData._cellGet(__getData.row, __columnDocNo).ToString();

                                DateTime __getDocDate = (DateTime)this._myManageTrans._dataList._gridData._cellGet(__getData.row, __columnDocDate);
                                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                                {
                                    if (_g.g._checkOpenPeriod(__getDocDate) == false)
                                    {
                                        break;
                                    }
                                }

                                if (__getDocNo.Trim().Length > 0)
                                {
                                    //if (__docNoList.Length > 0)
                                    {
                                        //   __docNoList.Append(",");
                                    }
                                    __docNoList = _docNoAdd(__docNoList, __getDocNo);

                                    DataTable __docRefTable = myFrameWork._queryShort("select " + _g.d.ap_ar_trans_detail._billing_no + " from " + _g.d.ap_ar_trans_detail._table + " where " + _g.d.ap_ar_trans_detail._doc_no + "=\'" + __getDocNo + "\' and " + _g.d.ap_ar_trans_detail._trans_flag + "=" + this._getTransFlag).Tables[0];
                                    if (__docRefTable.Rows.Count > 0)
                                    {
                                        for (int refrow = 0; refrow < __docRefTable.Rows.Count; refrow++)
                                        {
                                            string __refDocNo = __docRefTable.Rows[refrow][0].ToString();
                                            __docNoList = _docNoAdd(__docNoList, __refDocNo);
                                        }
                                    }
                                }
                                __itemList = this._getOldItemCode(__itemList, _g.d.ic_trans_detail._doc_no + "=\'" + this._dataList__deleteData_getRefDocNo(__getDocNo) + "\'");
                                //
                                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), this._icTransTable,
                                    " where " + _g.d.ic_trans._doc_no + "=\'" + __getDocNo + "\' "
                                    + " and (" + _g.d.ic_trans._trans_flag + "=" + this._getTransFlag.ToString() + " or " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ")"));
                                //
                                string __where = _g.d.ic_trans_detail._doc_no + "=\'" + __getDocNo + "\' and (" + _g.d.ic_trans_detail._trans_flag + "=" + this._getTransFlag.ToString() + " or " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ")";
                                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} where " + __where), this._icTransDetailTable));
                                // ลบรายวัน (Serial Number)
                                string __whereTransDetailSerialNumber = _g.d.ic_trans_serial_number._doc_no + "=\'" + __getDocNo + "\' and (" + _g.d.ic_trans_detail._trans_flag + "=" + this._getTransFlag.ToString() + " or " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ")";
                                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} where " + __whereTransDetailSerialNumber), _g.d.ic_trans_serial_number._table));
                                //
                                // Process
                                // เอารหัสเก่าไปประมวลผล
                                __itemList = this._getOldItemCode(__itemList, __where);
                                // log
                                this._logDocNoOld = "\'" + __getDocNo + "\'";
                                this._logDocDateOld = "\'\'";
                                if (__columnDocDate != -1) this._logDocDateOld = "\'" + MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDate(this._myManageTrans._dataList._gridData._cellGet(__getData.row, __columnDocDate).ToString())) + "\'";
                                this._logAmountOld = 0M;
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(this._createLog(3)));
                            } // for
                            __myQuery.Append("</node>");
                            __result = myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                            if (__result.Length == 0)
                            {
                                MyLib._myGlobal._displayWarning(0, null);
                                this._myManageTrans._dataList._refreshData();
                            }
                            else
                            {
                                MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร:
                        {
                            //
                            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                            for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
                            {
                                MyLib._deleteDataType __getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                                int __columnDocNo = this._getColumnNumberDocNo();
                                __getDocNo = this._myManageTrans._dataList._gridData._cellGet(__getData.row, __columnDocNo).ToString();

                                DateTime __getDocDate = (DateTime)this._myManageTrans._dataList._gridData._cellGet(__getData.row, __columnDocDate);
                                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                                {
                                    if (_g.g._checkOpenPeriod(__getDocDate) == false)
                                    {
                                        break;
                                    }
                                }

                                if (__getDocNo.Trim().Length > 0)
                                {
                                    //if (__docNoList.Length > 0)
                                    {
                                        //    __docNoList.Append(",");
                                    }
                                    // __docNoList.Append("\'" + __getDocNo + "\'");
                                    __docNoList = _docNoAdd(__docNoList, __getDocNo);

                                    DataTable __docRefTable = myFrameWork._queryShort("select " + _g.d.ap_ar_trans_detail._billing_no + " from " + _g.d.ap_ar_trans_detail._table + " where " + _g.d.ap_ar_trans_detail._doc_no + "=\'" + __getDocNo + "\' and " + _g.d.ap_ar_trans_detail._trans_flag + "=" + this._getTransFlag).Tables[0];
                                    if (__docRefTable.Rows.Count > 0)
                                    {
                                        for (int refrow = 0; refrow < __docRefTable.Rows.Count; refrow++)
                                        {
                                            string __refDocNo = __docRefTable.Rows[refrow][0].ToString();
                                            __docNoList = _docNoAdd(__docNoList, __refDocNo);
                                        }
                                    }
                                }
                                __itemList = this._getOldItemCode(__itemList, _g.d.ic_trans_detail._doc_no + "=\'" + this._dataList__deleteData_getRefDocNo(__getDocNo) + "\'");
                                //
                                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), this._icTransTable,
                                    " where " + _g.d.ic_trans._doc_no + "=\'" + __getDocNo + "\' "
                                    + " and (" + _g.d.ic_trans._trans_flag + "=" + this._getTransFlag.ToString() + " or " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินเข้าธนาคาร).ToString() + ")"));
                                //
                                string __where = _g.d.ic_trans_detail._doc_no + "=\'" + __getDocNo + "\' and (" + _g.d.ic_trans_detail._trans_flag + "=" + this._getTransFlag.ToString() + " or " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินเข้าธนาคาร).ToString() + ")";
                                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} where " + __where), this._icTransDetailTable));
                                // ลบรายวัน (Serial Number)
                                string __whereTransDetailSerialNumber = _g.d.ic_trans_serial_number._doc_no + "=\'" + __getDocNo + "\' and (" + _g.d.ic_trans_detail._trans_flag + "=" + this._getTransFlag.ToString() + " or " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินเข้าธนาคาร).ToString() + ")";
                                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} where " + __whereTransDetailSerialNumber), _g.d.ic_trans_serial_number._table));
                                //
                                // Process
                                // เอารหัสเก่าไปประมวลผล
                                __itemList = this._getOldItemCode(__itemList, __where);
                                // ลบ GL
                                __myQuery.Append(this._glDeleteQuery(__getDocNo));

                                // log
                                this._logDocNoOld = "\'" + __getDocNo + "\'";
                                this._logDocDateOld = "\'\'";
                                if (__columnDocDate != -1) this._logDocDateOld = "\'" + MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDate(this._myManageTrans._dataList._gridData._cellGet(__getData.row, __columnDocDate).ToString())) + "\'";
                                this._logAmountOld = 0M;
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(this._createLog(3)));
                            } // for
                            __myQuery.Append("</node>");
                            __result = myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                            if (__result.Length == 0)
                            {
                                MyLib._myGlobal._displayWarning(0, null);
                                this._myManageTrans._dataList._refreshData();
                            }
                            else
                            {
                                MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        break;


                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน:

                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:

                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค_ยกเลิก:

                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:

                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค_ยกเลิก:

                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:

                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน_ยกเลิก:

                    case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย:
                    case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย:

                    case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย_ยกเลิก:

                    case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก:

                    case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก:
                    case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก:
                    case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก:
                    case _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน_ยกเลิก:

                    case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                    case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก:
                    case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                    case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก:
                    case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                    case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                    case _g.g._transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ:
                    case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                    case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                    case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ:
                    case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                    case _g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก:
                    case _g.g._transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ:

                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก:

                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก:

                    case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น_ยกเลิก:

                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก:
                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก:
                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก:
                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก:
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก:
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก:
                    case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                    case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                    case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ:
                    case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                    case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก:
                    case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                    case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก:
                    case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ:

                    case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                    case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น_ยกเลิก:
                    case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                    case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น_ยกเลิก:
                    case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                    case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น_ยกเลิก:


                    case _g.g._transControlTypeEnum.คลัง_ยอดคงเหลือสินค้ายกมา:
                    case _g.g._transControlTypeEnum.คลัง_ตรวจนับสินค้า:
                    case _g.g._transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ:
                    case _g.g._transControlTypeEnum.คลัง_จ่ายสินค้าออก_จากการขาย:
                    case _g.g._transControlTypeEnum.คลัง_ปรับปรุงเพิ่มลดสินค้า:

                    case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                    case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ_ยกเลิก:
                    case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก:
                    case _g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า:
                    case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก_ยกเลิก:
                    case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา:
                    case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                    case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก_ยกเลิก:
                    case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                    case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป_ยกเลิก:
                    case _g.g._transControlTypeEnum.สินค้า_โอนออก_ยกเลิก:
                    case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด: // toe
                    case _g.g._transControlTypeEnum.สินค้า_บันทึกการจัดส่ง:
                    case _g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ:
                    case _g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ_ยกเลิก:

                    case _g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก:
                    case _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:
                    case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:

                    case _g.g._transControlTypeEnum.สินค้า_ขอโอน:
                        {
                            if (this._transControlType == _g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก || this._transControlType == _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก || this._transControlType == _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก)
                            {
                                __columnDocDate = this._myManageTrans._dataList._gridData._findColumnByName(_g.d.ic_wms_trans._table + "." + _g.d.ic_wms_trans._doc_date);
                            }
                            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                            for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
                            {
                                MyLib._deleteDataType __getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                                __getDocNo = this._myManageTrans._dataList._gridData._cellGet(__getData.row, this._getColumnNumberDocNo()).ToString();

                                DateTime __getDocDate = (DateTime)this._myManageTrans._dataList._gridData._cellGet(__getData.row, __columnDocDate);
                                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                                {
                                    if (_g.g._checkOpenPeriod(__getDocDate) == false)
                                    {
                                        this._myManageTrans._dataList._refreshData();
                                        return;
                                    }
                                }

                                if (__getDocNo.Trim().Length > 0)
                                {
                                    //if (__docNoList.Length > 0)
                                    {
                                        //__docNoList.Append(",");
                                    }
                                    // __docNoList.Append("\'" + __getDocNo + "\'");
                                    __docNoList = _docNoAdd(__docNoList, __getDocNo);

                                    DataTable __docRefTable = myFrameWork._queryShort("select " + _g.d.ap_ar_trans_detail._billing_no + " from " + _g.d.ap_ar_trans_detail._table + " where " + _g.d.ap_ar_trans_detail._doc_no + "=\'" + __getDocNo + "\' and " + _g.d.ap_ar_trans_detail._trans_flag + "=" + this._getTransFlag).Tables[0];
                                    if (__docRefTable.Rows.Count > 0)
                                    {
                                        for (int refrow = 0; refrow < __docRefTable.Rows.Count; refrow++)
                                        {
                                            string __refDocNo = __docRefTable.Rows[refrow][0].ToString();
                                            __docNoList = _docNoAdd(__docNoList, __refDocNo);
                                        }
                                    }

                                    DataTable __docRefTransTable = myFrameWork._queryShort("select " + _g.d.ic_trans._doc_ref + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __getDocNo + "\' and " + _g.d.ic_trans._trans_flag + "=" + this._getTransFlag).Tables[0];
                                    if (__docRefTransTable.Rows.Count > 0)
                                    {
                                        for (int refrow = 0; refrow < __docRefTransTable.Rows.Count; refrow++)
                                        {
                                            string __refDocNo = __docRefTransTable.Rows[refrow][0].ToString();
                                            __docNoList = _docNoAdd(__docNoList, __refDocNo);
                                        }

                                    }
                                }

                                // doc_ref

                                __itemList = this._getOldItemCode(__itemList, _g.d.ic_trans_detail._doc_no + "=\'" + this._dataList__deleteData_getRefDocNo(__getDocNo) + "\'");
                                //
                                if (this._vatBuy != null) __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_vat_buy._table + " where " + _g.d.gl_journal_vat_buy._doc_no + "=\'" + __getDocNo + "\' and " + _g.d.gl_journal_vat_buy._trans_flag + "=" + this._getTransFlag.ToString()));
                                if (this._vatSale != null) __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_vat_sale._table + " where " + _g.d.gl_journal_vat_sale._doc_no + "=\'" + __getDocNo + "\' and " + _g.d.gl_journal_vat_sale._trans_flag + "=" + this._getTransFlag.ToString()));
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_serial_number._table + " where " + _g.d.ic_trans_serial_number._doc_no + "=\'" + __getDocNo + "\' and " + _g.d.ic_trans_serial_number._trans_flag + "=" + this._getTransFlag.ToString()));
                                //
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format("delete from {0} {1}", this._icTransTable, __getData.whereString + "and " + _g.d.ic_trans._trans_flag + "=" + this._getTransFlag.ToString())));
                                // ลบรายวันย่อย
                                string __whereTransDetail = _g.d.ic_trans_detail._doc_no + "=\'" + __getDocNo + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + this._getTransFlag.ToString();
                                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} where " + __whereTransDetail), this._icTransDetailTable));
                                // ลบรายวัน (Serial Number)
                                string __whereTransDetailSerialNumber = _g.d.ic_trans_serial_number._doc_no + "=\'" + __getDocNo + "\' and " + _g.d.ic_trans_serial_number._trans_flag + "=" + this._getTransFlag.ToString();
                                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} where " + __whereTransDetailSerialNumber), _g.d.ic_trans_serial_number._table));
                                // ลบข้อมูล การจ่ายเงิน
                                // ลบของเก่าก่อน
                                if (this._payControl != null)
                                {
                                    // ลบการจ่ายเงิน
                                    SMLERPAPARControl._payControl __payControl = new SMLERPAPARControl._payControl();
                                    __payControl._icTransControlType = this._transControlType;
                                    __myQuery.Append(__payControl._queryDelete(__getDocNo));
                                }
                                if (this._payAdvance != null)
                                {
                                    // เงินมัดจำ
                                    SMLERPAPARControl._advanceControl __payAdvance = new SMLERPAPARControl._advanceControl();
                                    __payAdvance._icTransControlType = this._transControlType; ;
                                    __myQuery.Append(__payAdvance._queryDelete(__getDocNo));
                                }
                                if (this._withHoldingTax != null)
                                {
                                    // ลบภาษีหัก ณ. ที่จ่าย
                                    SMLERPGLControl._withHoldingTax __withHoldingTax = new SMLERPGLControl._withHoldingTax(SMLERPGLControl._withHoldingTaxType.ว่าง);
                                    __myQuery.Append(__withHoldingTax._queryDelete(__getDocNo, this._transControlType));
                                }
                                //
                                switch (this._transControlType)
                                {
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา:
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา:
                                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                                    case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.cb_trans_detail._table + " where doc_no = \'" + __getDocNo + "\'"));
                                        break;
                                }
                                //
                                switch (this._transControlType)
                                {
                                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                                    case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                                    case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                                    case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                                    case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                                    case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                                    case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                                    case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                                    case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                                    case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                                    case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                                    case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:

                                    case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:
                                    case _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:

                                        __whereTransDetail = _g.d.ap_ar_trans_detail._doc_no + "=\'" + __getDocNo + "\' and " + _g.d.ap_ar_trans_detail._trans_flag + "=" + this._getTransFlag.ToString();
                                        __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} where " + __whereTransDetail), _g.d.ap_ar_trans_detail._table));
                                        break;
                                }
                                //
                                if (this._vatBuy != null)
                                {
                                    __whereTransDetail = _g.d.gl_journal_vat_buy._doc_no + "=\'" + __getDocNo + "\' and " + _g.d.gl_journal_vat_buy._trans_flag + "=" + this._getTransFlag.ToString();
                                    __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} where " + __whereTransDetail), _g.d.gl_journal_vat_buy._table));
                                }
                                if (this._vatSale != null)
                                {
                                    __whereTransDetail = _g.d.gl_journal_vat_sale._doc_no + "=\'" + __getDocNo + "\' and " + _g.d.gl_journal_vat_sale._trans_flag + "=" + this._getTransFlag.ToString();
                                    __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} where " + __whereTransDetail), _g.d.gl_journal_vat_sale._table));
                                }
                                // Process
                                // เอารหัสเก่าไปประมวลผล
                                __itemList = this._getOldItemCode(__itemList, __whereTransDetail);
                                // log
                                this._logDocNoOld = "\'" + __getDocNo + "\'";
                                this._logDocDateOld = "\'\'";
                                if (__columnDocDate != -1) this._logDocDateOld = "\'" + MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDate(this._myManageTrans._dataList._gridData._cellGet(__getData.row, __columnDocDate).ToString())) + "\'";
                                this._logAmountOld = 0M;
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(this._createLog(3)));
                                // ลบ GL
                                __myQuery.Append(this._glDeleteQuery(__getDocNo));

                                // เช็ครับยกมา,เช็คจ่ายยกมา ให้ลบไปใส่ cb 
                                switch (this._transControlType)
                                {
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา:
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา:
                                        {
                                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.cb_trans_detail._table + " where " + _g.d.cb_trans_detail._doc_no + "=\'" + __getDocNo + "\' and trans_flag = " + this._getTransFlag.ToString()));
                                        }
                                        break;
                                }

                                if (this._shipmentControl != null)
                                {
                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_shipment._table + " where " + _g.d.ic_trans_shipment._doc_no + "=\'" + __getDocNo + "\' and trans_flag = " + this._getTransFlag.ToString()));

                                }

                                switch (this._transControlType)
                                {
                                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail_promotion._table + " where doc_no = \'" + __getDocNo + "\'"));
                                        break;
                                }
                            } // for
                            //

                            __myQuery.Append("</node>");
                            __result = myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                            if (__result.Length == 0)
                            {
                                //
                                MyLib._myGlobal._displayWarning(0, null);
                                this._myManageTrans._dataList._refreshData();
                            }
                            else
                            {
                                MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        break;
                }
                //
                string __itemListForProcess = _g.g._getItemRepack(__itemList);
                SMLProcess._docFlow __process = new SMLProcess._docFlow();
                __process._processAll(this._transControlType, __itemListForProcess, __docNoList.ToString());
                // Process Stock
                // กรณียกเลิก ให้ไปเอารหัสเก่าไปประมวล (จากเลขที่อ้างอืง)
                string __resultStr = __smlFrameWork._process_stock_balance(MyLib._myGlobal._databaseName, __itemListForProcess, _g.g._transFlagGlobal._transFlag(this._transControlType).ToString());
                if (__resultStr.Length > 0)
                {
                    MessageBox.Show(__resultStr);
                }
            }
        }

        /// <summary>
        /// ดึงรหัสสินค้าเก่า เพื่อประกอบนำไปประมวลผล
        /// </summary>
        /// <param name="source"></param>
        /// <param name="whereStr"></param>
        /// <returns></returns>
        ArrayList _getOldItemCode(ArrayList source, string whereStr)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __getItem = __myFrameWork._query(MyLib._myGlobal._databaseName, "select " + _g.d.ic_trans_detail._item_code + " from " + this._icTransDetailTable + " where " + whereStr);
            for (int __row = 0; __row < __getItem.Tables[0].Rows.Count; __row++)
            {
                source.Add(__getItem.Tables[0].Rows[__row][0].ToString());
            }
            return source;
        }

        /// <summary>
        /// ดึงรหัสสินค้าจาก DataGrid เพื่อประกอบไปประมวลผล
        /// </summary>
        /// <param name="source"></param>
        /// <param name="grid"></param>
        /// <returns></returns>
        ArrayList _getItemCodeFromGrid(ArrayList source, object grid)
        {
            MyLib._myGrid __data = (MyLib._myGrid)grid;
            for (int __row = 0; __row < __data._rowData.Count; __row++)
            {
                source.Add(__data._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString());
            }
            return source;
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

        private string _glDeleteQuery(string docNo)
        {
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal._table + " where " + _g.d.gl_journal._doc_no + "=\'" + docNo + "\'"));
            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._doc_no + "=\'" + docNo + "\'"));
            if (this._glDetail != null)
            {
                __myQuery.Append(this._glDetail._deleteGlExtraList(docNo));
            }
            return __myQuery.ToString();
        }

        /// <summary>
        /// ตรวจสอบวงเงิน
        /// </summary>
        /// <param name="mode">ประเภทรายการ 1=สั่งจอง 2=สั่งขาย</param>
        /// <returns></returns>
        private string _creditMoneyCheck(_g.g._transControlTypeEnum type)
        {
            // bill hold return query request
            string __inquiry_type = this._icTransScreenTop._getDataStr(_g.d.ic_trans._inquiry_type);

            string __cust_code = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
            SMLERPARAPInfo._process __arapProcess = new SMLERPARAPInfo._process();

            //DataTable __creditTable = __arapProcess._arCreditMoneyBalance(SMLERPARAPInfo._apArConditionEnum.ลูกหนี้_สถานะลูกหนี้, __cust_code, this._oldDocNo);
            string __queryCreditMoneyCheck = __arapProcess._arCreditMoneyBalanceQuery(SMLERPARAPInfo._apArConditionEnum.ลูกหนี้_สถานะลูกหนี้, __cust_code, this._oldDocNo);

            StringBuilder __queryCredit = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            __queryCredit.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryCreditMoneyCheck));

            // current deposit
            __queryCredit.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans._total_amount + " from " + _g.d.ic_trans._table + "  where " + _g.d.ic_trans._doc_no + "=\'" + this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_ref_trans) + "\' and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า) + " and " + _g.d.ic_trans._last_status + "=0"));

            __queryCredit.Append("</node>");

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryCredit.ToString());

            if (__result.Count > 0)
            {
                DataTable __creditTable = ((DataSet)__result[0]).Tables[0];
                DataTable __advanceAmount = ((DataSet)__result[1]).Tables[0];

                // เช็คเฉพาะ doc_success = 0 and approve_status = 2
                if (__creditTable.Rows.Count > 0)
                {
                    string __cust_name = "";
                    decimal __credit_money = 0;
                    decimal __credit_money_max = 0;
                    decimal __credit_balance = 0;
                    int __credit_status = 0;
                    string __close_reason = "";
                    decimal __total_amout = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_amount);

                    __cust_name = __creditTable.Rows[0][_g.d.ar_customer._name_1].ToString();
                    __credit_money = MyLib._myGlobal._decimalPhase(__creditTable.Rows[0][_g.d.ap_ar_resource._credit_money].ToString());
                    __credit_money_max = MyLib._myGlobal._decimalPhase(__creditTable.Rows[0][_g.d.ap_ar_resource._credit_money_max].ToString());
                    __credit_balance = MyLib._myGlobal._decimalPhase(__creditTable.Rows[0][_g.d.ap_ar_resource._balance_end].ToString());
                    __credit_status = MyLib._myGlobal._intPhase(__creditTable.Rows[0][_g.d.ar_customer_detail._credit_status].ToString());
                    __close_reason = __creditTable.Rows[0][_g.d.ar_customer_detail._close_reason].ToString();


                    decimal __chqOnHand = MyLib._myGlobal._intPhase(__creditTable.Rows[0]["chq_outstanding"].ToString());
                    decimal __sr_remain = MyLib._myGlobal._intPhase(__creditTable.Rows[0]["sr_remain"].ToString());
                    decimal __ss_remain = MyLib._myGlobal._intPhase(__creditTable.Rows[0]["ss_remain"].ToString());
                    decimal __advance_amount = MyLib._myGlobal._intPhase(__creditTable.Rows[0]["advance_amount"].ToString());


                    // check credit moneyy > 0
                    if (__credit_money <= 1 || __inquiry_type.Equals("1") || __inquiry_type.Equals("3"))
                    {
                        if (__credit_money <= 1 && __credit_status > 0 && (__inquiry_type.Equals("0") || __inquiry_type.Equals("2")))
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("ลูกค้า") + " " + __cust_code + " : " + __cust_name + " " + MyLib._myGlobal._resource("ถูกปิดสถานะเครดิต"), "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return "false";
                        }


                        {
                            __advance_amount = (__advanceAmount.Rows.Count > 0) ? MyLib._myGlobal._intPhase(__advanceAmount.Rows[0][_g.d.ic_trans._total_amount].ToString()) : 0M;

                            decimal __overAmount = __total_amout - __advance_amount;
                            decimal __overAmountPercent = MyLib._myGlobal._round((__advance_amount / __total_amout) * 100, 2);

                            // new flow
                            StringBuilder __query = new StringBuilder();
                            __query.Append("insert into " + _g.d.erp_credit_request._table + "(" + _g.d.erp_credit_request._doc_no + "," + _g.d.erp_credit_request._trans_flag + "," + _g.d.erp_credit_request._credit_money + "," + _g.d.erp_credit_request._credit_money_max + "," + _g.d.erp_credit_request._chq_amount + "," + _g.d.erp_credit_request._advance_amount + "," + _g.d.erp_credit_request._ar_balance_amount + "," + _g.d.erp_credit_request._sr_amount + "," + _g.d.erp_credit_request._ss_amount + "," + _g.d.erp_credit_request._bill_amount + "," + _g.d.erp_credit_request._over_amount + "," + _g.d.erp_credit_request._over_percent + ") " +
                                " values (\'{0}\', " + this._getTransFlag + "," + __credit_money + "," + __credit_money_max + "," + __chqOnHand + "," + __advance_amount + "," + __credit_balance + "," + __sr_remain + "," + __ss_remain + "," + __total_amout + "," + __overAmount + "," + __overAmountPercent + ")");
                            return __query.ToString();
                        }

                    }
                    else
                    {
                        if (__credit_status == 0)
                        {
                            decimal __creditMoneyRemain = (__credit_balance + __sr_remain + __ss_remain + __chqOnHand);
                            if (__credit_money < (__total_amout + __creditMoneyRemain))
                            {
                                decimal __overAmount = (__total_amout + __creditMoneyRemain) - __credit_money;
                                decimal __overAmountPercent = MyLib._myGlobal._round((__overAmount / __credit_money) * 100, 2);
                                __advance_amount = 0;

                                StringBuilder __query = new StringBuilder();
                                __query.Append("insert into " + _g.d.erp_credit_request._table + "(" + _g.d.erp_credit_request._doc_no + "," + _g.d.erp_credit_request._trans_flag + "," + _g.d.erp_credit_request._credit_money + "," + _g.d.erp_credit_request._credit_money_max + "," + _g.d.erp_credit_request._chq_amount + "," + _g.d.erp_credit_request._advance_amount + "," + _g.d.erp_credit_request._ar_balance_amount + "," + _g.d.erp_credit_request._sr_amount + "," + _g.d.erp_credit_request._ss_amount + "," + _g.d.erp_credit_request._bill_amount + "," + _g.d.erp_credit_request._over_amount + "," + _g.d.erp_credit_request._over_percent + ") " +
                                    " values (\'{0}\', " + this._getTransFlag + "," + __credit_money + "," + __credit_money_max + "," + __chqOnHand + "," + __advance_amount + "," + __credit_balance + "," + __sr_remain + "," + __ss_remain + "," + __total_amout + "," + __overAmount + "," + __overAmountPercent + ")");
                                return __query.ToString();
                            }
                        }
                        else
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("ลูกค้า") + " " + __cust_code + " : " + __cust_name + " " + MyLib._myGlobal._resource("ถูกปิดสถานะเครดิต"), "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return "false";
                        }
                    }
                }
            }
            return "";
        }

        private bool _creditMoneySaleCheck()
        {
            bool __isSaveData = true;

            string __cust_code = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);

            // เชื่อมวงเงินเครดิต โดย dblink
            decimal __total_amout = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_amount);

            string __cust_name = "";
            decimal __credit_money = 0;
            decimal __credit_money_max = 0;
            decimal __credit_balance = 0;
            int __credit_status = 0;
            string __close_reason = "";

            if (_g.g._companyProfile._join_money_credit == true && _g.g._companyProfile._join_money_credit_list.Length > 0)
            {
                #region เชื่อมวงเงิน 2 database
                try
                {
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    if (__myFrameWork._databaseSelectType == MyLib._myGlobal._databaseType.PostgreSql)
                    {
                        // postgres dblink extension concept select union all and sum
                        string __dbHost = "";
                        string __dbUser = "";
                        string __dbPass = "";
                        string[] __databaseList = _g.g._companyProfile._join_money_credit_list.Split(',');

                        string __getXmlServerConfig = __myFrameWork._loadXmlFile(MyLib._myGlobal._databaseConfig);
                        System.Xml.XmlDocument xml = new System.Xml.XmlDocument();
                        xml.LoadXml(__getXmlServerConfig);
                        System.Xml.XmlNode node = xml.SelectNodes("/node")[0];

                        __dbHost = node["server"].InnerText.ToString();
                        __dbUser = node["user"].InnerText.ToString();
                        __dbPass = node["password"].InnerText.ToString();

                        // make dblink query 
                        StringBuilder __queryStr = new StringBuilder();
                        SMLERPARAPInfo._process __arapProcess = new SMLERPARAPInfo._process();
                        //DataTable __creditTable = __arapProcess._arCreditMoneyBalance(SMLERPARAPInfo._apArConditionEnum.ลูกหนี้_สถานะลูกหนี้, __cust_code);

                        foreach (string database in __databaseList)
                        {
                            if (__queryStr.Length > 0)
                            {
                                __queryStr.Append(" union all ");
                            }

                            string __conectionStr = string.Format("dbname={3} port=5432 host={0} user={1} password={2}", __dbHost, __dbUser, __dbPass, database.Trim());
                            string __queryCreditCheck = __arapProcess._arCreditMoneyBalanceQuery(SMLERPARAPInfo._apArConditionEnum.ลูกหนี้_สถานะลูกหนี้, __cust_code);
                            string __fieldAs = "code varchar, name_1 varchar, credit_money float, credit_money_max float, credit_status int  , balance_end float";

                            __queryStr.Append(" select code, name_1,  credit_money, credit_money_max, credit_status, balance_end from dblink ('" + __conectionStr + "','" + MyLib._myGlobal._convertStrToQuery(__queryCreditCheck) + "') as p (" + __fieldAs + ") ");
                        }

                        string __query = "select code, name_1,  credit_money, credit_money_max, credit_status, sum(balance_end) as balance_end from ( " + __queryStr.ToString() + " ) as temp1 group by code, name_1, credit_money, credit_money_max, credit_status ";

                        DataSet __queryResult = __myFrameWork._query(MyLib._myGlobal._databaseName, __query.ToString());

                        if (__queryResult.Tables.Count > 0 && __queryResult.Tables[0].Rows.Count > 0)
                        {
                            DataTable __creditTable = __queryResult.Tables[0];
                            // ดึง query แล้ว check วงเงิน
                            __cust_name = __creditTable.Rows[0][_g.d.ar_customer._name_1].ToString();
                            __credit_money = MyLib._myGlobal._decimalPhase(__creditTable.Rows[0][_g.d.ap_ar_resource._credit_money].ToString());
                            __credit_money_max = MyLib._myGlobal._decimalPhase(__creditTable.Rows[0][_g.d.ap_ar_resource._credit_money_max].ToString());
                            __credit_balance = MyLib._myGlobal._decimalPhase(__creditTable.Rows[0][_g.d.ap_ar_resource._balance_end].ToString());
                            __credit_status = MyLib._myGlobal._intPhase(__creditTable.Rows[0][_g.d.ar_customer_detail._credit_status].ToString());
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                #endregion
            }
            else
            {

                SMLERPARAPInfo._process __arapProcess = new SMLERPARAPInfo._process();
                DataTable __creditTable = __arapProcess._arCreditMoneyBalance(SMLERPARAPInfo._apArConditionEnum.ลูกหนี้_สถานะลูกหนี้, __cust_code);

                if (__creditTable.Rows.Count > 0)
                {
                    //ตรวจสอบ และ warning
                    __cust_name = __creditTable.Rows[0][_g.d.ar_customer._name_1].ToString();
                    __credit_money = MyLib._myGlobal._decimalPhase(__creditTable.Rows[0][_g.d.ap_ar_resource._credit_money].ToString());
                    __credit_money_max = MyLib._myGlobal._decimalPhase(__creditTable.Rows[0][_g.d.ap_ar_resource._credit_money_max].ToString());
                    __credit_balance = MyLib._myGlobal._decimalPhase(__creditTable.Rows[0][_g.d.ap_ar_resource._balance_end].ToString());
                    __credit_status = MyLib._myGlobal._intPhase(__creditTable.Rows[0][_g.d.ar_customer_detail._credit_status].ToString());
                    __close_reason = __creditTable.Rows[0][_g.d.ar_customer_detail._close_reason].ToString();

                    if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                    {
                        __close_reason = "";

                        if (__creditTable.Rows[0][_g.d.ar_customer_detail._close_reason_1].ToString().Equals("1"))
                        {
                            __close_reason += "ชำระเช็คเกินกำหนด";
                        }

                        if (__creditTable.Rows[0][_g.d.ar_customer_detail._close_reason_2].ToString().Equals("1"))
                        {
                            if (__close_reason.Length > 0)
                            {
                                __close_reason += ",";
                            }
                            __close_reason += "ใหม่ยอดเกินวงเงินเครดิต";
                        }

                        if (__creditTable.Rows[0][_g.d.ar_customer_detail._close_reason_3].ToString().Equals("1"))
                        {
                            if (__close_reason.Length > 0)
                            {
                                __close_reason += ",";
                            }
                            __close_reason += "ผิดเงื่อนไขการชำระ";

                        }

                        if (__creditTable.Rows[0][_g.d.ar_customer_detail._close_reason_4].ToString().Equals("1"))
                        {
                            if (__close_reason.Length > 0)
                            {
                                __close_reason += ",";
                            }
                            __close_reason += __creditTable.Rows[0][_g.d.ar_customer_detail._close_reason].ToString();
                        }
                    }

                    if (_g.g._companyProfile._ar_credit_chq_outstanding)
                    {
                        decimal __chqOnHand = MyLib._myGlobal._decimalPhase(__creditTable.Rows[0]["chq_outstanding"].ToString());
                        __credit_balance += __chqOnHand;
                    }
                }
            }

            if (__credit_status == 0)
            {

                if ((_g.g._companyProfile._warning_credit_money == true || _g.g._companyProfile._lock_credit_money == true || (__credit_status == 2 && _g.g._companyProfile._request_ar_credit)))
                {
                    if (__credit_money != 0) // หากมีการกำหนดวงเงินเครดิต || (__credit_status == 2 && _g.g._companyProfile._request_ar_credit)
                    {

                        if ((__total_amout > (__credit_money - __credit_balance))) // || (__credit_status == 2 && _g.g._companyProfile._request_ar_credit)
                        {
                            decimal __requestCredit = __total_amout - (__credit_money - __credit_balance);
                            if (_g.g._companyProfile._request_ar_credit)
                            {
                                if (this._requestApproveCode.Length > 0)
                                {
                                    #region ขออนุมัติวงเงินไว้แล้ว

                                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                                    // ขออนุมัติไว้แล้ว เอา รหัสผ่านไปใส่
                                    if (_g.g._companyProfile._request_credit_type == 0)
                                    {
                                        // ของ siri เช็คสถานะการอนุมัติ
                                        string __queryCheckApproveStatus = "select approve_status from " + _g.d.ic_trans_draft._table + " where " + _g.d.ic_trans_draft._doc_no + "=\'" + this._requestApproveCode + "\'";
                                        DataTable __checkApproveStatusDatatable = __myFrameWork._queryShort(__queryCheckApproveStatus).Tables[0];
                                        if (__checkApproveStatusDatatable.Rows[0]["approve_status"].ToString().Equals("1"))
                                        {
                                            return true;
                                        }
                                        else
                                        {
                                            MessageBox.Show("ยังไม่มีการอนุมัติรายการเอกสาร : " + this._requestApproveCode);
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        // ใส่รหัสผ่าน
                                        _pricePasswordForm __passForm = new _pricePasswordForm(5);
                                        __passForm.label1.Text = "Ref No :";
                                        __passForm._userTextBox.Text = this._requestApproveCode;
                                        __passForm._processButton.Click += (processButtonSender, processButtonEventArgs) =>
                                        {
                                            __passForm.DialogResult = DialogResult.OK;
                                        };
                                        if (__passForm.ShowDialog() == DialogResult.OK)
                                        {
                                            // check password
                                            string __getPassword = __passForm._passwordTextBox.Text;
                                            bool __pass = false;

                                            string __getQuery = "select doc_no, approve_code, req_datetime from erp_request_order where doc_no = \'" + this._requestApproveCode + "\'";
                                            DataTable __approveDataTable = __myFrameWork._queryShort(__getQuery).Tables[0];
                                            if (__approveDataTable.Rows.Count > 0)
                                            {
                                                // if has expire resent new otp


                                                if (false)
                                                {
                                                    if (MessageBox.Show("รหัสผ่านหมดอายุ คุณต้องการส่งคำขออนุมัติใหม่ หรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                                    {
                                                        MyLib.RandomStringGenerator __gen = new MyLib.RandomStringGenerator();
                                                        int __passLength = (MyLib._myGlobal._OEMVersion.Equals("SINGHA")) ? 4 : 6;
                                                        string __approveCode = __gen.NextString(__passLength, false, false, true, false, false);

                                                        string __genNewOtpResult = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update " + _g.d.erp_request_order._table + " set " + _g.d.erp_request_order._req_datetime + "=\'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\'," + _g.d.erp_request_order._approve_code + "=\'" + __approveCode + "\'  where " + _g.d.erp_request_order._doc_no + "=\'" + this._requestApproveCode + "\'");
                                                        if (__genNewOtpResult.Length == 0)
                                                        {
                                                            MyLib._myGlobal._displayWarning(1, MyLib._myGlobal._resource("ส่งคำขออนุมัติใหม่สำเร็จ"));
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show(MyLib._myGlobal._errorText(__genNewOtpResult), "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        }
                                                    }
                                                    return false;
                                                }

                                                string __approvePassword = __approveDataTable.Rows[0][_g.d.erp_request_order._approve_code].ToString();
                                                if (__getPassword.Equals(__approvePassword))
                                                {
                                                    __isSaveData = true;
                                                }
                                                else
                                                {
                                                    MessageBox.Show("รหัสอนุมัติวงเงินไม่ถูกต้อง", "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    __isSaveData = false;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }
                                    #endregion

                                }
                                else
                                {
                                    #region ขออนุมัติวงเงิน

                                    string __noticeMessage = "ยอดวงเงินเครดิตของลุกค้า เกินที่กำหนดไว้ ต้องการขออนุมัติวงเงินเครดิตหรือไม่";

                                    if (MessageBox.Show(MyLib._myGlobal._resource(__noticeMessage), "warnning", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                    {
                                        // save temp and gen otp 4 digit for approve
                                        MyLib.RandomStringGenerator __gen = new MyLib.RandomStringGenerator();
                                        // gen ref_no
                                        // gen otp
                                        int __passLength = (MyLib._myGlobal._OEMVersion.Equals("SINGHA")) ? 4 : 6;
                                        string __refNo = __gen.NextString(8, false, true, true, false, false);
                                        string __approveCode = __gen.NextString(__passLength, false, false, true, false, false);

                                        // check duplicate refNo
                                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                                        while (true)
                                        {
                                            string __checkRefDuplicateQueryStr = "select " + _g.d.erp_request_order._doc_no + " from " + _g.d.erp_request_order._table + " where " + _g.d.erp_request_order._doc_no + "=\'" + __refNo + "\'";
                                            DataTable __check = __myFrameWork._queryShort(__checkRefDuplicateQueryStr).Tables[0];

                                            if (__check.Rows.Count == 0)
                                            {
                                                break;
                                            }
                                            __refNo = __gen.NextString(8, false, true, true, false, false);
                                        }


                                        StringBuilder __draftQuery = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                                        // save draft table
                                        string __field = "";
                                        string __value = "";
                                        ArrayList __screenBottom = this._icTransScreenBottom._createQueryForDatabase();
                                        ArrayList __screenMore = this._icTransScreenMore._createQueryForDatabase();

                                        // ic_trans_draft
                                        __field = MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_draft._trans_flag, _g.d.ic_trans_draft._trans_type, _g.d.ic_trans_draft._doc_no, _g.d.ic_trans_draft._doc_date, _g.d.ic_trans_draft._creator_code, _g.d.ic_trans_draft._create_datetime,
                                            _g.d.ic_trans_draft._cust_code, _g.d.ic_trans_draft._doc_time, _g.d.ic_trans_draft._doc_format_code, _g.d.ic_trans_draft._contactor,
                                            _g.d.ic_trans_draft._inquiry_type, _g.d.ic_trans_draft._vat_type, _g.d.ic_trans_draft._sale_code, _g.d.ic_trans_draft._sale_group, _g.d.ic_trans_draft._doc_ref, _g.d.ic_trans_draft._doc_ref_date);

                                        __value = MyLib._myGlobal._fieldAndComma(
                                            this._getTransFlag.ToString(),
                                            this._getTransType.ToString(),

                                            "\'" + __refNo + "\'",
                                            this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_date),
                                             "\'" + MyLib._myGlobal._userCode + "\'",
                                             "\'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\'",
                                            this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._cust_code),
                                            this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_time),
                                            this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_format_code),
                                            this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._contactor),
                                            this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._inquiry_type),
                                            this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._vat_type),
                                            this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._sale_code),
                                            this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._sale_group),
                                            this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_ref),
                                            this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_ref_date)
                                            );

                                        __draftQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans_draft._table + "(" + __field + "," + __screenBottom[0] + "," + __screenMore[0] + ") values(" + __value + "," + __screenBottom[1] + "," + __screenMore[1] + ")"));

                                        // ic_trans_detail_draft
                                        #region ic_trans_detail_draft

                                        __field = MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail_draft._trans_flag, _g.d.ic_trans_detail_draft._trans_type, _g.d.ic_trans_detail_draft._doc_no, _g.d.ic_trans_detail_draft._doc_date);
                                        __value = MyLib._myGlobal._fieldAndComma(this._getTransFlag.ToString(),
                                            this._getTransType.ToString(),

                                            "\'" + __refNo + "\'",
                                            this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_date));

                                        this._icTransItemGrid._updateRowIsChangeAll(true);
                                        __draftQuery.Append(this._icTransItemGrid._createQueryForInsert(_g.d.ic_trans_detail_draft._table, __field + ",", __value + ",", false, true));


                                        #endregion


                                        // cb_trans_detail_draft
                                        #region cb_trans_detail_draft

                                        __field = MyLib._myGlobal._fieldAndComma(_g.d.cb_trans_detail_draft._trans_flag, _g.d.cb_trans_detail_draft._trans_type, _g.d.cb_trans_detail_draft._doc_no, _g.d.cb_trans_detail_draft._doc_date,
                                            _g.d.cb_trans_detail_draft._doc_type, _g.d.cb_trans_detail_draft._ap_ar_code);

                                        __value = MyLib._myGlobal._fieldAndComma(this._getTransFlag.ToString(),
                                            this._getTransType.ToString(),

                                            "\'" + __refNo + "\'",
                                            this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_date),
                                            "6",
                                            this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._cust_code));

                                        this._payAdvance._dataGrid._updateRowIsChangeAll(true);
                                        __draftQuery.Append(this._payAdvance._dataGrid._createQueryForInsert(_g.d.cb_trans_detail_draft._table, __field + ",", __value + ","));

                                        #endregion

                                        // ap_ar_trans_detail_draft
                                        __field = MyLib._myGlobal._fieldAndComma(_g.d.ap_ar_trans_detail_draft._trans_flag, _g.d.ap_ar_trans_detail_draft._trans_type, _g.d.ap_ar_trans_detail_draft._doc_no, _g.d.ap_ar_trans_detail_draft._doc_date);
                                        __value = MyLib._myGlobal._fieldAndComma(this._getTransFlag.ToString(),
                                            this._getTransType.ToString(),

                                            "\'" + __refNo + "\'",
                                            this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_date));
                                        this._icTransRef._transGrid._updateRowIsChangeAll(true);
                                        __draftQuery.Append(this._icTransRef._transGrid._createQueryForInsert(_g.d.ap_ar_trans_detail_draft._table, __field + ",", __value + ",", false, true));

                                        // save otp erp_request_order
                                        __field = MyLib._myGlobal._fieldAndComma(_g.d.erp_request_order._trans_flag, _g.d.erp_request_order._trans_type, _g.d.erp_request_order._doc_no, _g.d.erp_request_order._req_datetime, _g.d.erp_request_order._approve_code, _g.d.erp_request_order._cust_code);
                                        __value = MyLib._myGlobal._fieldAndComma(this._getTransFlag.ToString(),
                                            this._getTransType.ToString(),

                                            "\'" + __refNo + "\'",
                                            "\'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\'",
                                            "\'" + __approveCode + "\'",
                                            "" + this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._cust_code) + "");

                                        __draftQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.erp_request_order._table + "(" + __field + ") values(" + __value + ")"));

                                        __draftQuery.Append("</node>");

                                        string __saveDraftResult = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __draftQuery.ToString());
                                        if (__saveDraftResult.Length > 0)
                                        {
                                            MessageBox.Show(MyLib._myGlobal._errorText(__saveDraftResult), "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return false;
                                        }
                                        else
                                        {

                                            // send message 
                                            #region Send message ย้ายไป _sendMessageRequestCreditMoney

                                            /*
                                            StringBuilder __queryGetTelephoneSMS = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                                            // sms by branch
                                            __queryGetTelephoneSMS.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_branch_list._phone_number_approve + "," + _g.d.erp_branch_list._sale_hub_approve + " from " + _g.d.erp_branch_list._table + " where " + _g.d.erp_branch_list._code + "=" + this._icTransScreenMore._getDataStrQuery(_g.d.ic_trans._branch_code)));
                                            // by amount
                                            __queryGetTelephoneSMS.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_credit_approve_level._phone_number_approve + "," + _g.d.erp_credit_approve_level._sale_hub_auth + " from " + _g.d.erp_credit_approve_level._table + " where " + __credit_balance.ToString() + " between " + _g.d.erp_credit_approve_level._from_amount + " and " + _g.d.erp_credit_approve_level._to_amount));
                                            // last amount
                                            __queryGetTelephoneSMS.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_credit_approve_level._phone_number_approve + "," + _g.d.erp_credit_approve_level._sale_hub_auth + " from " + _g.d.erp_credit_approve_level._table + " order by " + _g.d.erp_credit_approve_level._to_amount + " desc limit 1 "));
                                            __queryGetTelephoneSMS.Append("</node>");

                                            ArrayList __getSMSToNumber = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryGetTelephoneSMS.ToString());

                                            string __sendTo = "";
                                            string __sendMail = "";

                                            if (__getSMSToNumber.Count > 0)
                                            {
                                                DataTable __approveBranchNumberTable = ((DataSet)__getSMSToNumber[0]).Tables[0];
                                                DataTable __approveAmountLevelTable = ((DataSet)__getSMSToNumber[1]).Tables[0];
                                                DataTable __approveAmountHightLevelTable = ((DataSet)__getSMSToNumber[1]).Tables[0];

                                                // 1. เอา เบอร์จากสาขาเอกสาร
                                                if (__approveBranchNumberTable.Rows.Count > 0)
                                                {
                                                    __sendTo = __approveBranchNumberTable.Rows[0][_g.d.erp_branch_list._phone_number_approve].ToString();
                                                    __sendMail = __approveBranchNumberTable.Rows[0][_g.d.erp_branch_list._sale_hub_approve].ToString();
                                                }

                                                // 2. ตามตารางกำหนดช่วงวงเงินอนุมัติ
                                                if (__approveAmountLevelTable.Rows.Count > 0)
                                                {
                                                    StringBuilder __smsByLevel = new StringBuilder();
                                                    StringBuilder __emailByLevel = new StringBuilder();

                                                    for (int __count = 0; __count < __approveAmountLevelTable.Rows.Count; __count++)
                                                    {

                                                        if (__smsByLevel.Length > 0)
                                                            __smsByLevel.Append(",");

                                                        if (__emailByLevel.Length > 0)
                                                            __emailByLevel.Append(",");

                                                        __smsByLevel.Append(__approveAmountLevelTable.Rows[__count][_g.d.erp_credit_approve_level._phone_number_approve].ToString());
                                                        __emailByLevel.Append(__approveAmountLevelTable.Rows[__count][_g.d.erp_credit_approve_level._sale_hub_auth].ToString());
                                                    }

                                                    __sendTo = __smsByLevel.ToString();
                                                    __sendMail = __emailByLevel.ToString();

                                                }
                                                else if (__approveAmountHightLevelTable.Rows.Count > 0)
                                                {

                                                    __sendTo = __approveAmountHightLevelTable.Rows[0][_g.d.erp_credit_approve_level._phone_number_approve].ToString();
                                                    __sendMail = __approveAmountHightLevelTable.Rows[0][_g.d.erp_credit_approve_level._sale_hub_auth].ToString();

                                                }
                                            }

                                            if (_g.g._companyProfile._request_credit_type == 1 || _g.g._companyProfile._request_credit_type == 3)
                                            {
                                                string __prefixMessage = "CUS";
                                                if (__credit_status == 2)
                                                    __prefixMessage = "Close Credit";
                                                // sms
                                                string __message = string.Format(__prefixMessage + " {2} TC {4} Debt {5} Buy {3} Doc {0} UC {1}", __refNo, __approveCode, __cust_name, __total_amout.ToString(), __credit_money.ToString(), __credit_balance.ToString());

                                                string __phoneNumber = __sendTo;


                                                // 3. เอาจาก 1.1.2
                                                if (__phoneNumber.Length == 0)
                                                {
                                                    __phoneNumber = _g.g._companyProfile._phone_number_approve;
                                                }

                                                if (__phoneNumber.Length > 0)
                                                {
                                                    // 
                                                    string __sendSMSresult = MyLib.SendSMS._sendSMS._send(__phoneNumber, System.Uri.EscapeUriString(__message));
                                                    if (__sendSMSresult.Length > 0)
                                                    {
                                                        // หยุดการส่งข้อความทั้งหมด และ ออกจาก thread
                                                        MessageBox.Show(__sendSMSresult);
                                                    }
                                                }

                                            }

                                            if (_g.g._companyProfile._request_credit_type == 2 || _g.g._companyProfile._request_credit_type == 3)
                                            {
                                                string __phoneNumber = __sendMail; // "supachai_pi@boonrawd.co.th";
                                                                                   // 3. เอาจาก 1.1.2
                                                if (__phoneNumber.Length == 0)
                                                {
                                                    __phoneNumber = _g.g._companyProfile._sale_hub_approve;
                                                }

                                                string __prefixMessage = "เกินวงเงิน";
                                                if (__credit_status == 2)
                                                    __prefixMessage = "ปิดสถานะการขาย " + __close_reason;

                                                string __message = string.Format(__prefixMessage + " {2} วงเงินเครดิต {4} บาท ยอดหนี้ปัจจุบัน {5} บาท ยอดที่สั่งซื้อ {3} บาท เลขที่ {0} รหัสอนุมัติ {1}", __refNo, __approveCode, __cust_name, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __total_amout), MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __credit_money), MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __credit_balance));

                                                // sale hub
                                                string __sendSMSresult = MyLib.SendSMS._sendSMS._sendSaleHubSINGHA(__phoneNumber, System.Uri.EscapeUriString(__message));
                                                if (__sendSMSresult.Length > 0)
                                                {
                                                    // หยุดการส่งข้อความทั้งหมด และ ออกจาก thread
                                                    MessageBox.Show(__sendSMSresult);
                                                }

                                            }
                                            */

                                            string __messageSMS = "";
                                            string __messgaeSaleHub = "";
                                            if (_g.g._companyProfile._request_credit_type == 1 || _g.g._companyProfile._request_credit_type == 3)
                                            {
                                                __messageSMS = string.Format("CUS {2} TC {4} Debt {5} Buy {3} Doc {0} UC {1}", __refNo, __approveCode, __cust_name, __total_amout.ToString(), __credit_money.ToString(), __credit_balance.ToString());
                                            }

                                            if (_g.g._companyProfile._request_credit_type == 2 || _g.g._companyProfile._request_credit_type == 3)
                                            {
                                                __messgaeSaleHub = string.Format("เกินวงเงิน {2} วงเงินเครดิต {4} บาท ยอดหนี้ปัจจุบัน {5} บาท ยอดที่สั่งซื้อ {3} บาท เลขที่ {0} รหัสอนุมัติ {1}"
                                                    , __refNo
                                                    , __approveCode
                                                    , __cust_name
                                                    , MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __total_amout)
                                                    , (__credit_money == 0 ? "0" : MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __credit_money))
                                                    , (__credit_balance == 0 ? "0" : MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __credit_balance)));

                                            }

                                            string __sendResult = _sendMessageRequestCreditMoney(__messageSMS, __messgaeSaleHub, __requestCredit);
                                            if (__sendResult.Length > 0)
                                            {
                                                // หยุดการส่งข้อความทั้งหมด และ ออกจาก thread
                                                MessageBox.Show(__sendResult);
                                            }

                                            #endregion

                                            // clear screen
                                            // message ref code
                                            MyLib._myGlobal._displayWarning(1, MyLib._myGlobal._resource("บันทึกข้อมูลสำเร็จ หมายเลขขออนุมัติเอกสารของคุณคือ : ") + __refNo);

                                            this._clearScreen();
                                            this._myManageTrans__newDataClick();
                                            return false;
                                        }

                                    }
                                    else
                                    {
                                        return false;
                                    }
                                    #endregion
                                }
                            }
                            else
                            {
                                if (_g.g._companyProfile._lock_credit_money == true)
                                {
                                    // ห้ามขายเกินวงเงิน
                                    #region ห้ามขายเกินวงเงิน
                                    MessageBox.Show(MyLib._myGlobal._resource("ยอดวงเงินเครดิตของลุกค้า เกินที่กำหนดไว้ "), "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                                    bool __pass = false;
                                    if (_g.g._companyProfile._password_ar_credit == true)
                                    {
                                        _pricePasswordForm __pricePassword = new _pricePasswordForm(3);
                                        __pricePassword.ShowDialog();
                                        if (__pricePassword._passwordPass)
                                        {
                                            //this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._user_approve, __pricePassword._userCode, false);
                                            this._icTransScreenMore._setDataStr(_g.d.ic_trans._user_approve, __pricePassword._userCode);
                                            __pass = true;
                                        }
                                        __pricePassword.Dispose();
                                    }
                                    else
                                    {
                                        __pass = false;
                                    }

                                    if (__pass == false)
                                    {
                                        __isSaveData = false;
                                    }
                                    #endregion
                                }
                                else
                                {
                                    // เตือนเกินวงเงิน
                                    if (MessageBox.Show(MyLib._myGlobal._resource("ยอดวงเงินเครดิตของลุกค้า เกินที่กำหนดไว้ ต้องการดำเนินการต่อหรือไม่"), "warnning", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                    {
                                        __isSaveData = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (__credit_status == 2 && _g.g._companyProfile._request_ar_credit)
            {
                // ปิดสถานะเครดิต
                // request open credit
                string __noticeMessage = "ปิดสถานะการขาย : " + __close_reason + "\n" + "ต้องการขออนุมัติเปิดวงเงินเครดิตหรือไม่";
                if (MessageBox.Show(MyLib._myGlobal._resource(__noticeMessage), "warnning", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    string __messageSMS = "";
                    string __messgaeSaleHub = "";
                    if (_g.g._companyProfile._request_credit_type == 1 || _g.g._companyProfile._request_credit_type == 3)
                    {
                        __messageSMS = string.Format("Close Credit {0} TC {2} Debt {3} Buy {1}", __cust_name, __total_amout.ToString(), __credit_money.ToString(), __credit_balance.ToString());
                    }

                    if (_g.g._companyProfile._request_credit_type == 2 || _g.g._companyProfile._request_credit_type == 3)
                    {

                        __messgaeSaleHub = string.Format("ปิดสถานะการขาย " + __close_reason + " {0} วงเงินเครดิต {2} บาท ยอดหนี้ปัจจุบัน {3} บาท ยอดที่สั่งซื้อ {1} บาท กรุณาติดต่อแผนกการเงิน", __cust_name, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __total_amout), MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __credit_money), MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __credit_balance));

                    }

                    string __sendResult = _sendMessageRequestCreditMoney(__messageSMS, __messgaeSaleHub, __credit_balance);
                    if (__sendResult.Length > 0)
                    {
                        // หยุดการส่งข้อความทั้งหมด และ ออกจาก thread
                        MessageBox.Show(__sendResult);
                    }

                    MyLib._myGlobal._displayWarning(1, MyLib._myGlobal._resource("ส่งคำขออนุมัติเปิดวงเงินสำเร็จ"));

                    __isSaveData = false;
                }

            }
            else
            {
                MessageBox.Show(MyLib._myGlobal._resource("ลูกค้า") + " " + __cust_code + " : " + __cust_name + " " + MyLib._myGlobal._resource("ถูกปิดสถานะเครดิต"), "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                __isSaveData = false;
            }

            return __isSaveData;
        }

        string _sendMessageRequestCreditMoney(string messageSMS, string messageSaleHub, decimal creditBalance)
        {
            StringBuilder __result = new StringBuilder();

            #region Send message

            // branch setup
            string __branch_filter = "";

            if (_g.g._companyProfile._branchStatus == 1)
            {
                __branch_filter = " (( '**' || replace(" + _g.d.erp_credit_approve_level._branch_code + ", ',', '**,**') || '**' like '%**" + this._icTransScreenTop__getBranchCode() + "**%')) ";
            }


            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __queryGetTelephoneSMS = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            // sms by branch
            __queryGetTelephoneSMS.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_branch_list._phone_number_approve + "," + _g.d.erp_branch_list._sale_hub_approve + " from " + _g.d.erp_branch_list._table + " where " + _g.d.erp_branch_list._code + "=" + this._icTransScreenMore._getDataStrQuery(_g.d.ic_trans._branch_code)));
            // by amount
            __queryGetTelephoneSMS.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_credit_approve_level._phone_number_approve + "," + _g.d.erp_credit_approve_level._sale_hub_auth + " from " + _g.d.erp_credit_approve_level._table + " where " + __branch_filter + (__branch_filter.Length > 0 ? " and " : "") + creditBalance.ToString() + " between " + _g.d.erp_credit_approve_level._from_amount + " and " + _g.d.erp_credit_approve_level._to_amount));
            // last amount
            __queryGetTelephoneSMS.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_credit_approve_level._phone_number_approve + "," + _g.d.erp_credit_approve_level._sale_hub_auth + " from " + _g.d.erp_credit_approve_level._table + (__branch_filter.Length > 0 ? " where " + __branch_filter : "") + " order by " + _g.d.erp_credit_approve_level._to_amount + " desc limit 1 "));
            __queryGetTelephoneSMS.Append("</node>");

            ArrayList __getSMSToNumber = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryGetTelephoneSMS.ToString());

            string __sendTo = "";
            string __sendMail = "";

            if (__getSMSToNumber.Count > 0)
            {
                DataTable __approveBranchNumberTable = ((DataSet)__getSMSToNumber[0]).Tables[0];
                DataTable __approveAmountLevelTable = ((DataSet)__getSMSToNumber[1]).Tables[0];
                DataTable __approveAmountHightLevelTable = ((DataSet)__getSMSToNumber[1]).Tables[0];

                // 1. เอา เบอร์จากสาขาเอกสาร
                if (__approveBranchNumberTable.Rows.Count > 0)
                {
                    __sendTo = __approveBranchNumberTable.Rows[0][_g.d.erp_branch_list._phone_number_approve].ToString();
                    __sendMail = __approveBranchNumberTable.Rows[0][_g.d.erp_branch_list._sale_hub_approve].ToString();
                }

                // 2. ตามตารางกำหนดช่วงวงเงินอนุมัติ
                if (__approveAmountLevelTable.Rows.Count > 0)
                {
                    StringBuilder __smsByLevel = new StringBuilder();
                    StringBuilder __emailByLevel = new StringBuilder();

                    for (int __count = 0; __count < __approveAmountLevelTable.Rows.Count; __count++)
                    {

                        if (__smsByLevel.Length > 0)
                            __smsByLevel.Append(",");

                        if (__emailByLevel.Length > 0)
                            __emailByLevel.Append(",");

                        __smsByLevel.Append(__approveAmountLevelTable.Rows[__count][_g.d.erp_credit_approve_level._phone_number_approve].ToString());
                        __emailByLevel.Append(__approveAmountLevelTable.Rows[__count][_g.d.erp_credit_approve_level._sale_hub_auth].ToString());
                    }

                    __sendTo = __smsByLevel.ToString();
                    __sendMail = __emailByLevel.ToString();

                }
                else if (__approveAmountHightLevelTable.Rows.Count > 0)
                {

                    __sendTo = __approveAmountHightLevelTable.Rows[0][_g.d.erp_credit_approve_level._phone_number_approve].ToString();
                    __sendMail = __approveAmountHightLevelTable.Rows[0][_g.d.erp_credit_approve_level._sale_hub_auth].ToString();

                }
            }

            if (_g.g._companyProfile._request_credit_type == 1 || _g.g._companyProfile._request_credit_type == 3)
            {
                // sms
                string __message = messageSMS; // string.Format(__prefixMessage + " {2} TC {4} Debt {5} Buy {3} Doc {0} UC {1}", __refNo, __approveCode, __cust_name, __total_amout.ToString(), __credit_money.ToString(), __credit_balance.ToString());

                string __phoneNumber = __sendTo;


                // 3. เอาจาก 1.1.2
                if (__phoneNumber.Length == 0)
                {
                    __phoneNumber = _g.g._companyProfile._phone_number_approve;
                }

                if (__phoneNumber.Length > 0)
                {
                    // 
                    SMLERPMailMessage._sendMessage._sendMessageSMS(__phoneNumber, (__message));
                    /* ย้ายไปส่งโดย thread
                    string __sendSMSresult = MyLib.SendSMS._sendSMS._send(__phoneNumber, System.Uri.EscapeUriString(__message));
                    if (__sendSMSresult.Length > 0)
                    {
                        // หยุดการส่งข้อความทั้งหมด และ ออกจาก thread
                        // MessageBox.Show(__sendSMSresult);
                        __result.Append("SMS Send Error : " + __sendSMSresult);

                    }*/
                }

            }

            if (_g.g._companyProfile._request_credit_type == 2 || _g.g._companyProfile._request_credit_type == 3)
            {
                string __phoneNumber = __sendMail; // "supachai_pi@boonrawd.co.th";
                                                   // 3. เอาจาก 1.1.2
                if (__phoneNumber.Length == 0)
                {
                    __phoneNumber = _g.g._companyProfile._sale_hub_approve;
                }


                string __message = messageSaleHub;  //string.Format(__prefixMessage + " {2} วงเงินเครดิต {4} บาท ยอดหนี้ปัจจุบัน {5} บาท ยอดที่สั่งซื้อ {3} บาท เลขที่ {0} รหัสอนุมัติ {1}", __refNo, __approveCode, __cust_name, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __total_amout), MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __credit_money), MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __credit_balance));

                // sale hub
                SMLERPMailMessage._sendMessage._sendMessageSaleHub(__phoneNumber, (__message), "");
                /*
                string __sendSMSresult = MyLib.SendSMS._sendSMS._sendSaleHubSINGHA(__phoneNumber, System.Uri.EscapeUriString(__message));
                if (__sendSMSresult.Length > 0)
                {
                    // หยุดการส่งข้อความทั้งหมด และ ออกจาก thread
                    //MessageBox.Show(__sendSMSresult);
                    if (__result.Length > 0)
                    {
                        __result.Append("\n");
                    }
                    __result.Append("Sale Hub Send Error : " + __sendSMSresult);
                }
                */

            }
            #endregion

            return __result.ToString();
        }

        private void _saveData()
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            bool __isSaveData = true;
            string __docNoList = "";
            if (this._myToolBar.Enabled)
            {
                if (this._payControl != null && this._payControl.Enabled == true)
                {
                    if (this._payControl._checker() == false)
                    {
                        __isSaveData = false;
                    }
                }

                if (this._myManageTrans._mode == 1 && _g.g._companyProfile._auto_insert_time == true)
                {
                    switch (this._transControlType)
                    {
                        case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                        case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                        case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                        case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                            this._icTransScreenTop._setDataStr(_g.d.ic_trans._doc_time, DateTime.Now.Hour.ToString("D2") + ":" + DateTime.Now.Minute.ToString("D2"));
                            break;
                    }
                }

                // 3.กรณีที>เครื>องลูกเวลาไม่ตรงกับ server  ระบบตอ้งไม่ใหบ้นัทึก ท
                DateTime __now = DateTime.Now;

                string __queryCheckDate = "select date(now()) as now";
                DataTable __dateCheckResult = __myFrameWork._queryShort(__queryCheckDate).Tables[0];
                if (__dateCheckResult.Rows.Count > 0)
                {
                    DateTime __dateServer = MyLib._myGlobal._convertDateFromQuery(__dateCheckResult.Rows[0][0].ToString());
                    if (__now.Year.CompareTo(__dateServer.Year) != 0 ||
                        __now.Month.CompareTo(__dateServer.Month) != 0 ||
                        __now.Day.CompareTo(__dateServer.Day) != 0)
                    {
                        MessageBox.Show("เวลาเครื่องไม่ตรงกับเวลาในระบบ");
                        return;
                    }
                }

                if (this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ && _g.g._companyProfile._lock_sale_day_interval && _g.g._companyProfile._sale_day_interval > 0)
                {
                    // check 
                    DateTime __saleDocDate = this._icTransScreenTop._getDataDate(_g.d.ic_trans._doc_date);
                    TimeSpan __saleSpan = __now.Subtract(__saleDocDate);

                    int __totalSaleDay = (int)Math.Floor(__saleSpan.TotalDays);
                    if (__totalSaleDay > _g.g._companyProfile._sale_day_interval)
                    {
                        MessageBox.Show(string.Format("ห้ามขายย้อนหลังเกิน {0} วัน", _g.g._companyProfile._sale_day_interval));
                        return;
                    }
                }

                #region ตรวจสอบข้อมูล

                // singha 
                #region ตรวจสอบสถานะลูกค้า (SINGHA)

                if (MyLib._myGlobal._OEMVersion == ("SINGHA")
                    && (
                    this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ ||
                    this._transControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้ ||
                    this._transControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้ ||
                    this._transControlType == _g.g._transControlTypeEnum.ขาย_เสนอราคา ||
                    this._transControlType == _g.g._transControlTypeEnum.ขาย_สั่งขาย ||
                    this._transControlType == _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า))
                {
                    string __cust_code = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
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

                #endregion

                // เช็คเลือกประเภทเอกสาร
                #region เช็คเลือกประเภทเอกสาร
                string __docFormatCodeSelect = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_format_code);
                if (__docFormatCodeSelect.Equals(""))
                {
                    __isSaveData = false;
                    MessageBox.Show(MyLib._myGlobal._resource("กรุณาเลือก") + " " + MyLib._myGlobal._resource("เลขที่เอกสาร"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                #endregion

                // check screen top
                #region ตรวจสอบการป้อนข้อมูล

                this._icTransScreenTop._saveLastControl();
                this._icTransScreenBottom._saveLastControl();
                this._icTransScreenMore._saveLastControl();
                this._icTransRef._transGrid._removeLastControl();

                string __screentopCheck = this._icTransScreenTop._checkEmtryField();
                if (__screentopCheck.Length > 0)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อน") + " " + __screentopCheck, MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    __isSaveData = false;

                }

                #endregion

                #region เช็คป้อนสาขา

                if (__isSaveData == true && _g.g._companyProfile._warning_branch_input)
                {
                    if (this._icTransScreenMore._getControl(_g.d.ic_trans._branch_code) != null)
                    {
                        if (this._icTransScreenMore._getDataStr(_g.d.ic_trans._branch_code).Trim().Length == 0 && this._tab.Visible == true)
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อน สาขา"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            __isSaveData = false;
                        }
                    }
                }

                #endregion

                #region เช็คป้อนแผนก

                if (__isSaveData == true && _g.g._companyProfile._use_department == 1 && _g.g._companyProfile._warning_department)
                {
                    if (this._icTransScreenMore._getControl(_g.d.ic_trans._department_code) != null)
                    {
                        if (this._icTransScreenMore._getDataStr(_g.d.ic_trans._department_code).Trim().Length == 0 && this._tab.Visible == true)
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อน แผนก"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            __isSaveData = false;
                        }
                    }
                }

                if (MyLib._myGlobal._programName.Equals("SML CM") && this._transControlType == _g.g._transControlTypeEnum.ขาย_เสนอราคา && _g.g._companyProfile._quotation_expire)
                {
                    decimal __creditDay = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._expire_day);
                    if (__creditDay <= 0)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อน จำนวนวันหมดอายุ"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        __isSaveData = false;

                    }
                }

                #endregion

                #region ตรวจสอบใบกำกับภาษี

                if (_g.g._companyProfile._check_input_vat &&
                    (this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ
                    || this._transControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้
                    || this._transControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้
                    || this._transControlType == _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ
                    || this._transControlType == _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้
                    || this._transControlType == _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด
                    || this._transControlType == _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้
                    || this._transControlType == _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด
                    || this._transControlType == _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้
                    || this._transControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น
                    || this._transControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้
                    || this._transControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้
                    || this._transControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น
                    || this._transControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้
                    || this._transControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้))
                {
                    if (this._icTransScreenTop._getControl(_g.d.ic_trans._vat_type) != null)
                    {
                        string __vatType = this._icTransScreenTop._getDataStr(_g.d.ic_trans._vat_type, false);
                        if (__vatType == "0" || __vatType == "1")
                        {
                            if (this._icTransScreenTop._getDataStr(_g.d.ic_trans._tax_doc_no).ToString().Length == 0)
                            {
                                MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อนเลขที่ใบกำกับภาษี"), MyLib._myGlobal._resource("ผิดพลาด"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                __isSaveData = false;
                            }

                            string __tempCheck = this._icTransScreenTop._getDataStr(_g.d.ic_trans._tax_doc_date);
                            if (this._icTransScreenTop._getDataStr(_g.d.ic_trans._tax_doc_date).Length == 0)
                            {
                                MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อนวันที่ใบกำกับภาษี"), MyLib._myGlobal._resource("ผิดพลาด"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                __isSaveData = false;
                            }
                        }
                    }
                }

                #endregion

                // toe check permisssion
                #region เช็คสิทธิ์
                if (this._myManageTrans._mode == 1 && this._myManageTrans._isAdd == false)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("warning44"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    __isSaveData = false;
                }
                #endregion

                #region Check งวดบัญชี
                if (__isSaveData && (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional))
                {
                    if (this._transControlType != _g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก && this._transControlType != _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก && this._transControlType != _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก)
                    {
                        __isSaveData = _g.g._checkOpenPeriod(this._icTransScreenTop._getDataDate(_g.d.ic_trans._doc_date));
                    }
                }
                #endregion

                // ตรวจสอบ Lot กำหนดเอง ต้องป้อนหมายเลข LOT ด้วย
                #region ตรวจสอบ Lot กำหนดเอง
                if (__isSaveData)
                {
                    string __inquiryType = this._icTransScreenTop._getDataStr(_g.d.ic_trans._inquiry_type);

                    // บอล กรณีตั้งหนี้จากการรับสินค้า, ลดหนี้พาเชียล ไม่ต้องเช็ค Lot
                    if (
                        // ขาเข้า ต้องป้อนเท่านั้น
                        (this._transControlType == _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา)
                        || (this._transControlType == _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป)
                        || (this._transControlType == _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก)
                        || (this._transControlType == _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด)
                        || (this._transControlType == _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ)
                        || (this._transControlType == _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า)
                        || (this._transControlType == _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด)
                        || (this._transControlType == _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้)
                        || (this._transControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้)


                        // ขาออก auto ได้
                        || (this._transControlType == _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ)
                        || (this._transControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก)
                        || (this._transControlType == _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก)
                        || ((this._transControlType == _g.g._transControlTypeEnum.ขาย_สั่งขาย) && (MyLib._myGlobal._OEMVersion.Equals("ims") || MyLib._myGlobal._OEMVersion.Equals("imex")) == false)
                        || ((this._transControlType == _g.g._transControlTypeEnum.ขาย_สั่งขาย) && ((MyLib._myGlobal._OEMVersion.Equals("ims") || MyLib._myGlobal._OEMVersion.Equals("imex")) && _g.g._companyProfile._sale_order_banalce_control == false))
                        || ((this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ))
                        || (this._transControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้)
                        || (this._transControlType == _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด)
                        || (this._transControlType == _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด)

                        )
                    /*&& (
                       (this._transControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้ && (__inquiryType != "1" && __inquiryType != "2"))  && (MyLib._myGlobal._OEMVersion.Equals("ims") || MyLib._myGlobal._OEMVersion.Equals("imex"))
                        ))*/
                    {

                        if (this._transControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้ && (__inquiryType == "1" || __inquiryType == "2"))
                        {
                            // ข้าม เพิ่มหนี้ ไม่กระทบ stock
                        }
                        else
                        {
                            int __columnNumberCost = this._icTransItemGrid._findColumnByName(this._icTransItemGrid._columnCostType);
                            int __columnNumberLot1 = this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._lot_number_1);
                            for (int __row = 0; __row < this._icTransItemGrid._rowData.Count; __row++)
                            {
                                if ((int)MyLib._myGlobal._decimalPhase(this._icTransItemGrid._cellGet(__row, __columnNumberCost).ToString()) == 2)
                                {
                                    if (this._icTransItemGrid._cellGet(__row, __columnNumberLot1).ToString().Trim().Length == 0)
                                    {
                                        if (MyLib._myGlobal._OEMVersion.Equals("ims") || MyLib._myGlobal._OEMVersion.Equals("imex")) // imex
                                        {
                                            if (_g.g._companyProfile._find_lot_auto == true && this._myManageTrans._mode == 1 &&
                                                (this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ || this._transControlType == _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ)

                                                )
                                            {
                                                // lot auto
                                                decimal __findLotQty = (decimal)this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._qty);
                                                string __itemCode = this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString();
                                                string __getWh = this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._wh_code).ToString();
                                                string __getShelf = this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._shelf_code).ToString();

                                                SMLERPControl._icInfoProcess __process = new SMLERPControl._icInfoProcess();

                                                //string __wh_shelf_where = _g.d.ic_trans_detail_lot._wh_code + "=\'" + __getWh + "\' and  " + _g.d.ic_trans_detail_lot._shelf_code + " =\'" + __getShelf + "\'" +
                                                //    " and coalesce((select " + _g.d.ic_lot_manage._lot_select + " from " + _g.d.ic_lot_manage._table + " where " + _g.d.ic_lot_manage._table + "." + _g.d.ic_lot_manage._ic_code + " = temp2." + _g.d.ic_resource._ic_code + " and " + _g.d.ic_lot_manage._table + "." + _g.d.ic_lot_manage._lot_number + " = temp2." + _g.d.ic_resource._lot_number + " and " + _g.d.ic_lot_manage._table + "." + _g.d.ic_lot_manage._wh_code + " = temp2." + _g.d.ic_trans_detail_lot._wh_code + " and " + _g.d.ic_lot_manage._table + "." + _g.d.ic_lot_manage._shelf_code + " = temp2." + _g.d.ic_trans_detail_lot._shelf_code + "), 0 ) = 0 ";

                                                //DataTable __lotBalance = __process._stkStockInfoAndBalanceByLotLocation(_g.g._productCostType.ปรกติ, null, __itemCode, __itemCode, "", true, SMLERPControl._icInfoProcess._stockBalanceType.ยอดคงเหลือตามLOT_ที่เก็บ_เรียงตามเอกสาร_IMEX, __wh_shelf_where);

                                                // toe เปลี่ยน ใหม่ ดึงจาก trans_detail เลย

                                                DataTable __lotBalance = __process._stkLotInfoAndBalance(null, __itemCode, __itemCode, "\'" + __getWh + "\'", "\'" + __getShelf + "\'", true);

                                                Boolean __foundLot = false;

                                                // ตัดจำนวนก่อนหน้าใน lot นั้นออกไปก่อนทำการคำณวนใด ๆ กันเลือกสินค้าไว้หลาย LOT
                                                for (int __rowTemp = 0; __rowTemp < this._icTransItemGrid._rowData.Count; __rowTemp++)
                                                {
                                                    if (this._icTransItemGrid._cellGet(__rowTemp, _g.d.ic_trans_detail._item_code).ToString().Equals(__itemCode) &&
                                                        this._icTransItemGrid._cellGet(__rowTemp, _g.d.ic_trans_detail._wh_code).ToString().Equals(__getWh) &&
                                                        this._icTransItemGrid._cellGet(__rowTemp, _g.d.ic_trans_detail._shelf_code).ToString().Equals(__getShelf))
                                                    {

                                                        string __getLotNumberTemp = this._icTransItemGrid._cellGet(__rowTemp, _g.d.ic_trans_detail._lot_number_1).ToString();
                                                        decimal __getLineQty = MyLib._myGlobal._decimalPhase(this._icTransItemGrid._cellGet(__rowTemp, _g.d.ic_trans_detail._qty).ToString());

                                                        for (int __lotRow = 0; __lotRow < __lotBalance.Rows.Count; __lotRow++)
                                                        {
                                                            // check lot
                                                            if (__lotBalance.Rows[__lotRow][_g.d.ic_resource._lot_number].ToString().Equals(__getLotNumberTemp))
                                                            {
                                                                __lotBalance.Rows[__lotRow][_g.d.ic_resource._balance_qty] = (decimal)(MyLib._myGlobal._decimalPhase(__lotBalance.Rows[__lotRow][_g.d.ic_resource._balance_qty].ToString()) - __getLineQty);
                                                            }
                                                        }
                                                        // ปรับยอด lot ลง

                                                    }
                                                }

                                                // ทำจนกว่า จะไม่พอ
                                                for (int __lotRow = 0; __lotRow < __lotBalance.Rows.Count; __lotRow++)
                                                {
                                                    decimal __lotBalanceQty = MyLib._myGlobal._decimalPhase(__lotBalance.Rows[__lotRow][_g.d.ic_resource._balance_qty].ToString());
                                                    if (__lotBalanceQty > 0)
                                                    {
                                                        if (__findLotQty <= __lotBalanceQty)
                                                        {
                                                            // ถ้าพอตัด ทั้ง Lot
                                                            __foundLot = true;
                                                            string __getLotNumber = __lotBalance.Rows[__lotRow][_g.d.ic_resource._lot_number].ToString();
                                                            string __mfn = __lotBalance.Rows[__lotRow][_g.d.ic_trans_detail_lot._mfn_name].ToString();
                                                            string __expireDate = __lotBalance.Rows[__lotRow][_g.d.ic_resource._date_expire].ToString();
                                                            string __mfd = __lotBalance.Rows[__lotRow][_g.d.ic_trans_detail_lot._mfd_date].ToString();

                                                            // update lot

                                                            this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._lot_number_1, __getLotNumber, true);
                                                            this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._mfn_name, __mfn, true);
                                                            if (__expireDate.Length > 0)
                                                                this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._date_expire, MyLib._myGlobal._convertDateFromQuery(__expireDate), true);

                                                            if (__mfd.Length > 0)
                                                                this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._mfd_date, MyLib._myGlobal._convertDateFromQuery(__mfd), true);
                                                            break;
                                                        }
                                                        else
                                                        {

                                                            // ยกยอดไป บรรทัดต่อไป

                                                            string __getLotNumber = __lotBalance.Rows[__lotRow][_g.d.ic_resource._lot_number].ToString();
                                                            string __mfn = __lotBalance.Rows[__lotRow][_g.d.ic_trans_detail_lot._mfn_name].ToString();
                                                            string __expireDate = __lotBalance.Rows[__lotRow][_g.d.ic_resource._date_expire].ToString();
                                                            string __mfd = __lotBalance.Rows[__lotRow][_g.d.ic_trans_detail_lot._mfd_date].ToString();

                                                            // update lot
                                                            this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._lot_number_1, __getLotNumber, true);
                                                            this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._mfn_name, __mfn, true);
                                                            if (__expireDate.Length > 0)
                                                                this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._date_expire, MyLib._myGlobal._convertDateFromQuery(__expireDate), true);

                                                            if (__mfd.Length > 0)
                                                                this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._mfd_date, MyLib._myGlobal._convertDateFromQuery(__mfd), true);

                                                            this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._qty, __lotBalanceQty, false);
                                                            this._icTransItemGrid._calcItemPrice(__row, __row, this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._discount));

                                                            // add new row
                                                            decimal __newRowQty = __findLotQty - __lotBalanceQty;
                                                            int __newRow = __row + 1;
                                                            this._icTransItemGrid._addRow(__row + 1);

                                                            //                             this._addColumn(_g.d.ic_trans_detail._doc_ref_type, 10, 25, 25, true, true, true);
                                                            // this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, true, true, true, true, "", "", "", _g.d.ic_trans_detail._doc_ref);
                                                            this._icTransItemGrid._cellUpdate(__newRow, _g.d.ic_trans_detail._doc_ref_type, MyLib._myGlobal._intPhase(this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._doc_ref_type).ToString()), true);
                                                            this._icTransItemGrid._cellUpdate(__newRow, _g.d.ic_trans_detail._ref_doc_no, this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._ref_doc_no).ToString(), true);


                                                            this._icTransItemGrid._cellUpdate(__newRow, _g.d.ic_trans_detail._item_code, this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString(), true);
                                                            this._icTransItemGrid._cellUpdate(__newRow, _g.d.ic_trans_detail._unit_code, this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._unit_code).ToString(), false);
                                                            this._icTransItemGrid._cellUpdate(__newRow, _g.d.ic_trans_detail._wh_code, this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._wh_code).ToString(), false);
                                                            this._icTransItemGrid._cellUpdate(__newRow, _g.d.ic_trans_detail._shelf_code, this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._shelf_code).ToString(), false);
                                                            this._icTransItemGrid._cellUpdate(__newRow, _g.d.ic_trans_detail._qty, __newRowQty, false);
                                                            this._icTransItemGrid._cellUpdate(__newRow, _g.d.ic_trans_detail._price, MyLib._myGlobal._decimalPhase(this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._price).ToString()), false);

                                                            string __discount = this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._discount).ToString();

                                                            this._icTransItemGrid._cellUpdate(__newRow, _g.d.ic_trans_detail._discount, __discount, false);
                                                            this._icTransItemGrid._cellUpdate(__newRow, _g.d.ic_trans_detail._sum_amount, __newRowQty, false);

                                                            this._icTransItemGrid._searchUnitNameWareHouseNameShelfName(__newRow);

                                                            // toe เพิ่ม สั่งคำณวนราคาใหม่
                                                            this._icTransItemGrid._calcItemPrice(__newRow, __newRow, this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._discount));
                                                            __foundLot = true;
                                                            break;
                                                        }
                                                    }
                                                }

                                                if (__foundLot == false)
                                                {
                                                    if (_g.g._companyProfile._check_lot_auto == true)
                                                    {
                                                        MessageBox.Show(MyLib._myGlobal._resource("รหัสสินค้า") + " " + this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString() + " " + MyLib._myGlobal._resource("ยังไม่ได้กำหนดหมายเลข LOT แบบกำหนดเอง"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                        __isSaveData = false;
                                                        return;
                                                    }
                                                    else
                                                    {
                                                        if (MessageBox.Show(MyLib._myGlobal._resource("รหัสสินค้า") + " " + this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString() + " " + MyLib._myGlobal._resource("ยังไม่บันทึกหมายเลข LOT แบบกำหนดเอง คุณต้องการที่จะบันทึกหรือไม่"), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                                                        {
                                                            __isSaveData = false;
                                                            return;
                                                        }
                                                    }
                                                }

                                            }
                                            else
                                            {
                                                if (_g.g._companyProfile._check_lot_auto == true)
                                                {

                                                    MessageBox.Show(MyLib._myGlobal._resource("รหัสสินค้า") + " " + this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString() + " " + MyLib._myGlobal._resource("ยังไม่ได้กำหนดหมายเลข LOT แบบกำหนดเอง"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                    __isSaveData = false;
                                                    return;

                                                }
                                                else
                                                {
                                                    if (MessageBox.Show(MyLib._myGlobal._resource("รหัสสินค้า") + " " + this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString() + " " + MyLib._myGlobal._resource("ยังไม่บันทึกหมายเลข LOT แบบกำหนดเอง คุณต้องการที่จะบันทึกหรือไม่"), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                                                    {
                                                        __isSaveData = false;
                                                        return;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            //if (MessageBox.Show(MyLib._myGlobal._resource("รหัสสินค้า") + " " + this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString() + " " + MyLib._myGlobal._resource("ยังไม่บันทึกหมายเลข LOT แบบกำหนดเอง \nคุณต้ัองการค้นหา Lot อัตโนมัติหรือไม่"), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)

                                            if (_g.g._companyProfile._find_lot_auto == true && this._myManageTrans._mode == 1 && (this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ))
                                            {
                                                // find lot
                                                decimal __findLotQty = (decimal)this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._qty);
                                                string __itemCode = this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString();
                                                string __getWh = this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._wh_code).ToString();
                                                string __getShelf = this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._shelf_code).ToString();

                                                SMLERPControl._icInfoProcess __process = new SMLERPControl._icInfoProcess();

                                                string __wh_shelf_where = _g.d.ic_trans_detail_lot._wh_code + "=\'" + __getWh + "\' and  " + _g.d.ic_trans_detail_lot._shelf_code + " =\'" + __getShelf + "\'";


                                                DataTable __lotBalance = __process._stkStockInfoAndBalanceByLotLocation(_g.g._productCostType.ปรกติ, null, __itemCode, __itemCode, DateTime.Now, true, SMLERPControl._icInfoProcess._stockBalanceType.ยอดคงเหลือตามLOT_ที่เก็บ, __wh_shelf_where);
                                                Boolean __foundLot = false;

                                                // ตัดจำนวนก่อนหน้าใน lot นั้นออกไปก่อนทำการคำณวนใด ๆ กันเลือกสินค้าไว้หลาย LOT
                                                for (int __rowTemp = 0; __rowTemp < this._icTransItemGrid._rowData.Count; __rowTemp++)
                                                {
                                                    if (this._icTransItemGrid._cellGet(__rowTemp, _g.d.ic_trans_detail._item_code).ToString().Equals(__itemCode) &&
                                                        this._icTransItemGrid._cellGet(__rowTemp, _g.d.ic_trans_detail._wh_code).ToString().Equals(__getWh) &&
                                                        this._icTransItemGrid._cellGet(__rowTemp, _g.d.ic_trans_detail._shelf_code).ToString().Equals(__getShelf))
                                                    {

                                                        string __getLotNumberTemp = this._icTransItemGrid._cellGet(__rowTemp, _g.d.ic_trans_detail._lot_number_1).ToString();
                                                        decimal __getLineQty = MyLib._myGlobal._decimalPhase(this._icTransItemGrid._cellGet(__rowTemp, _g.d.ic_trans_detail._qty).ToString());

                                                        for (int __lotRow = 0; __lotRow < __lotBalance.Rows.Count; __lotRow++)
                                                        {
                                                            // check lot
                                                            if (__lotBalance.Rows[__lotRow][_g.d.ic_resource._lot_number].ToString().Equals(__getLotNumberTemp))
                                                            {
                                                                __lotBalance.Rows[__lotRow][_g.d.ic_resource._balance_qty] = (decimal)(MyLib._myGlobal._decimalPhase(__lotBalance.Rows[__lotRow][_g.d.ic_resource._balance_qty].ToString()) - __getLineQty);
                                                            }
                                                        }
                                                        // ปรับยอด lot ลง

                                                    }
                                                }

                                                // เช็คความจำนวนก่อน หากพอในการตัด lot เดียวให้ update lot_number อย่างดียว

                                                for (int __lotRow = 0; __lotRow < __lotBalance.Rows.Count; __lotRow++)
                                                {
                                                    decimal __lotBalanceQty = MyLib._myGlobal._decimalPhase(__lotBalance.Rows[__lotRow][_g.d.ic_resource._balance_qty].ToString());
                                                    if (__lotBalanceQty >= __findLotQty)
                                                    {
                                                        __foundLot = true;
                                                        string __getLotNumber = __lotBalance.Rows[__lotRow][_g.d.ic_resource._lot_number].ToString();
                                                        string __mfn = __lotBalance.Rows[__lotRow][_g.d.ic_trans_detail_lot._mfn_name].ToString();
                                                        string __expireDate = __lotBalance.Rows[__lotRow][_g.d.ic_resource._date_expire].ToString();
                                                        string __mfd = __lotBalance.Rows[__lotRow][_g.d.ic_trans_detail_lot._mfd_date].ToString();

                                                        // update lot

                                                        this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._lot_number_1, __getLotNumber, true);
                                                        this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._mfn_name, __mfn, true);
                                                        if (__expireDate.Length > 0)
                                                            this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._date_expire, MyLib._myGlobal._convertDateFromQuery(__expireDate), true);

                                                        if (__mfd.Length > 0)
                                                            this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._mfd_date, MyLib._myGlobal._convertDateFromQuery(__mfd), true);
                                                        break;
                                                    }
                                                }

                                                // กระจาย 
                                                // 1. ปรับ qty ให้ตรงกับ lot คงเหลือตัวแรก
                                                // 2. จำนวนทีเหลือ add row เพื่อให้ next line process

                                                if (__foundLot == false)
                                                {
                                                    for (int __lotRow = 0; __lotRow < __lotBalance.Rows.Count; __lotRow++)
                                                    {
                                                        decimal __lotBalanceQty = MyLib._myGlobal._decimalPhase(__lotBalance.Rows[__lotRow][_g.d.ic_resource._balance_qty].ToString());
                                                        if (__lotBalanceQty > 0)
                                                        {

                                                            string __getLotNumber = __lotBalance.Rows[__lotRow][_g.d.ic_resource._lot_number].ToString();
                                                            string __mfn = __lotBalance.Rows[__lotRow][_g.d.ic_trans_detail_lot._mfn_name].ToString();
                                                            string __expireDate = __lotBalance.Rows[__lotRow][_g.d.ic_resource._date_expire].ToString();
                                                            string __mfd = __lotBalance.Rows[__lotRow][_g.d.ic_trans_detail_lot._mfd_date].ToString();

                                                            // update lot
                                                            this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._lot_number_1, __getLotNumber, true);
                                                            this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._mfn_name, __mfn, true);
                                                            if (__expireDate.Length > 0)
                                                                this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._date_expire, MyLib._myGlobal._convertDateFromQuery(__expireDate), true);

                                                            if (__mfd.Length > 0)
                                                                this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._mfd_date, MyLib._myGlobal._convertDateFromQuery(__mfd), true);

                                                            this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._qty, __lotBalanceQty, false);
                                                            this._icTransItemGrid._calcItemPrice(__row, __row, this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._discount));

                                                            // add new row
                                                            decimal __newRowQty = __findLotQty - __lotBalanceQty;
                                                            int __newRow = this._icTransItemGrid._addRow();
                                                            this._icTransItemGrid._cellUpdate(__newRow, _g.d.ic_trans_detail._item_code, this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString(), true);
                                                            this._icTransItemGrid._cellUpdate(__newRow, _g.d.ic_trans_detail._unit_code, this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._unit_code).ToString(), false);
                                                            this._icTransItemGrid._cellUpdate(__newRow, _g.d.ic_trans_detail._wh_code, this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._wh_code).ToString(), false);
                                                            this._icTransItemGrid._cellUpdate(__newRow, _g.d.ic_trans_detail._shelf_code, this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._shelf_code).ToString(), false);
                                                            this._icTransItemGrid._cellUpdate(__newRow, _g.d.ic_trans_detail._qty, __newRowQty, false);
                                                            this._icTransItemGrid._cellUpdate(__newRow, _g.d.ic_trans_detail._price, MyLib._myGlobal._decimalPhase(this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._price).ToString()), false);
                                                            this._icTransItemGrid._cellUpdate(__newRow, _g.d.ic_trans_detail._discount, MyLib._myGlobal._decimalPhase(this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._discount).ToString()), false);
                                                            this._icTransItemGrid._cellUpdate(__newRow, _g.d.ic_trans_detail._sum_amount, __newRowQty, false);

                                                            this._icTransItemGrid._searchUnitNameWareHouseNameShelfName(__newRow);

                                                            // toe เพิ่ม สั่งคำณวนราคาใหม่
                                                            this._icTransItemGrid._calcItemPrice(__newRow, __newRow, this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._discount));
                                                            __foundLot = true;
                                                            break;

                                                        }
                                                    }
                                                }

                                                if (__foundLot == false)
                                                {
                                                    //
                                                    MessageBox.Show(MyLib._myGlobal._resource("รหัสสินค้า") + " " + this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString() + " " + MyLib._myGlobal._resource("ยังไม่บันทึกหมายเลข LOT แบบกำหนดเอง"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    __isSaveData = false;
                                                    return;
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show(MyLib._myGlobal._resource("รหัสสินค้า") + " " + this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString() + " " + MyLib._myGlobal._resource("ยังไม่บันทึกหมายเลข LOT แบบกำหนดเอง"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                __isSaveData = false;
                                                return;
                                            }

                                        }
                                    }
                                }

                            }
                        } // end check ไม่กระทบ stock

                    }


                    // imex 
                }

                #endregion

                // เช็คยอดรวม
                #region mark  ตรวจสอบยอดรวม กันไว้
                /*{
                    // ตรวจสอบยอดรวม กันไว้
                    switch (this._transControlType)
                    {
                        case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                            decimal __vatRate = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._vat_rate);
                            decimal __totalValue = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_value);
                            decimal __totalDiscount = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_discount);
                            decimal __advanceAmount = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._advance_amount);
                            decimal __totalBeforeVat = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_before_vat);
                            decimal __totalVatValue = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_vat_value);
                            decimal __totalAfterVat = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_after_vat);
                            decimal __totalExceptVat = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_except_vat);
                            decimal __totalAmount = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_amount);
                            //
                            decimal __beforeVat = 0M;
                            decimal __totalAdvance = __advanceAmount;
                            decimal __vatValue = 0M;
                            decimal __afterVat = 0M;
                            decimal __totalExcptVat = __totalExceptVat;
                            decimal __totalAmountCalc = 0M;
                            switch (this._vatType)
                            {
                                case _g.g._vatTypeEnum.ภาษีแยกนอก:
                                    {
                                        __beforeVat = (__totalValue - __totalDiscount) - __totalAdvance;
                                        __vatValue = MyLib._myGlobal._round(__beforeVat * (__vatRate / 100.0M), _g.g._companyProfile._item_amount_decimal);
                                        __afterVat = __beforeVat + __vatValue;
                                        __totalAmountCalc = __afterVat + __totalExcptVat;
                                    }
                                    break;
                                case _g.g._vatTypeEnum.ภาษีรวมใน:
                                    {
                                        __totalAmountCalc = (__totalValue - __totalDiscount) - __totalAdvance;
                                        __beforeVat = MyLib._myGlobal._round((__totalAmountCalc * 100.0M) / (100.0M + __vatRate), _g.g._companyProfile._item_amount_decimal);
                                        __vatValue = MyLib._myGlobal._round(__totalAmountCalc - __beforeVat, _g.g._companyProfile._item_amount_decimal);
                                        __afterVat = __beforeVat + __vatValue;
                                    }
                                    break;
                                case _g.g._vatTypeEnum.ยกเว้นภาษี:
                                    __vatValue = 0;
                                    __totalAmountCalc = (__totalValue - __totalDiscount) - __totalAdvance;
                                    break;
                            }
                            break;
                    }
                }*/
                #endregion

                // ตรวจสอบ Serial Number
                #region Serial Number Check
                if (__isSaveData && this._icTransItemGrid._isSerialNumber)
                {

                    int __columnSerialNumberCount = this._icTransItemGrid._findColumnByName(this._icTransItemGrid._columnSerialNumberCount);
                    if (__columnSerialNumberCount != -1)
                    {
                        for (int __row = 0; __row < this._icTransItemGrid._rowData.Count; __row++)
                        {
                            int __isSerialNumber = (int)MyLib._myGlobal._decimalPhase(this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._is_serial_number).ToString());
                            if (__isSerialNumber == 1)
                            {
                                decimal __qtySum = MyLib._myGlobal._decimalPhase(this._icTransItemGrid._cellGet(__row, this._icTransItemGrid._columnSerialNumberCount).ToString());
                                decimal __qty = MyLib._myGlobal._decimalPhase(this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._qty).ToString());
                                if (__qtySum != __qty)
                                {
                                    MessageBox.Show(MyLib._myGlobal._resource("หมายเลขเครื่องไม่สัมพันธ์กับจำนวน") + " : " + this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString());
                                    __isSaveData = false;
                                    break;
                                }
                            }
                        }
                    }
                }
                #endregion

                // check expire date
                #region check expire date
                if (__isSaveData)
                {
                    int __columnExpireDate = this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._date_expire);
                    if (__columnExpireDate != -1)
                    {
                        for (int __row = 0; __row < this._icTransItemGrid._rowData.Count; __row++)
                        {
                            int __iSExpireItem = (int)this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._use_expire);
                            int __costType = (int)this._icTransItemGrid._cellGet(__row, this._icTransItemGrid._columnCostType);

                            if (__iSExpireItem == 1 && __costType == 3)
                            {
                                DateTime __getDate = (DateTime)this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._date_expire);
                                string __cellData = (__getDate.Year < 1900) ? "" : MyLib._myGlobal._convertDateToString(__getDate, false);
                                if (__cellData == "")
                                {
                                    MessageBox.Show(MyLib._myGlobal._resource("กรุณาระบุวันหมดอายุ") + " : " + this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString(), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    __isSaveData = false;
                                    break;
                                }
                            }
                        }
                    }
                }
                #endregion

                // เช็คการเฉลี่ยโครงการ การจัดสรร
                #region เช็คการเฉลี่ยโครงการ การจัดสรร
                if (__isSaveData)
                {
                    switch (this._transControlType)
                    {
                        case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                        case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                        case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                        case _g.g._transControlTypeEnum.สินค้า_โอนออก:

                        case _g.g._transControlTypeEnum.คลัง_ยอดคงเหลือสินค้ายกมา:
                            break;
                        default:
                            int __getColumnSumAmount = this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._sum_amount);
                            if (__getColumnSumAmount != -1)
                            {
                                for (int row = 0; row < this._icTransItemGrid._rowData.Count; row++)
                                {
                                    decimal __getTotal = (decimal)this._icTransItemGrid._cellGet(row, __getColumnSumAmount);

                                    SMLInventoryControl._icTransItemGridControl.icTransWeightStruct __departmentWeight = new _icTransItemGridControl.icTransWeightStruct();
                                    if (_g.g._companyProfile._use_department == 1)
                                        __departmentWeight = (SMLInventoryControl._icTransItemGridControl.icTransWeightStruct)this._icTransItemGrid._cellGet(row, this._icTransItemGrid._columnDepartment);

                                    SMLInventoryControl._icTransItemGridControl.icTransWeightStruct __projectWeight = new _icTransItemGridControl.icTransWeightStruct();
                                    if (_g.g._companyProfile._use_project == 1)
                                        __projectWeight = (SMLInventoryControl._icTransItemGridControl.icTransWeightStruct)this._icTransItemGrid._cellGet(row, this._icTransItemGrid._columnProject);

                                    SMLInventoryControl._icTransItemGridControl.icTransWeightStruct __allocateWeight = new _icTransItemGridControl.icTransWeightStruct();
                                    if (_g.g._companyProfile._use_allocate == 1)
                                        __allocateWeight = (SMLInventoryControl._icTransItemGridControl.icTransWeightStruct)this._icTransItemGrid._cellGet(row, this._icTransItemGrid._columnAlloCate);

                                    SMLInventoryControl._icTransItemGridControl.icTransWeightStruct __sideWeight = new _icTransItemGridControl.icTransWeightStruct();
                                    if (_g.g._companyProfile._use_unit == 1)
                                        __sideWeight = (SMLInventoryControl._icTransItemGridControl.icTransWeightStruct)this._icTransItemGrid._cellGet(row, this._icTransItemGrid._columnSideList);

                                    SMLInventoryControl._icTransItemGridControl.icTransWeightStruct __jobWeight = new _icTransItemGridControl.icTransWeightStruct();
                                    if (_g.g._companyProfile._use_job == 1)
                                        __jobWeight = (SMLInventoryControl._icTransItemGridControl.icTransWeightStruct)this._icTransItemGrid._cellGet(row, this._icTransItemGrid._columnJobsList);

                                    if (__departmentWeight != null && __departmentWeight.__details.Count > 0 && __getTotal != __departmentWeight._getTotal ||
                                        __projectWeight != null && __projectWeight.__details.Count > 0 && __getTotal != __projectWeight._getTotal ||
                                        __allocateWeight != null && __allocateWeight.__details.Count > 0 && __getTotal != __allocateWeight._getTotal ||
                                        __sideWeight != null && __sideWeight.__details.Count > 0 && __getTotal != __sideWeight._getTotal ||
                                        __jobWeight != null && __jobWeight.__details.Count > 0 && __getTotal != __jobWeight._getTotal)
                                    {

                                        StringBuilder __optionList = new StringBuilder();

                                        if (__departmentWeight != null && __departmentWeight.__details.Count > 0 && __getTotal != __departmentWeight._getTotal)
                                            __optionList.Append(((__optionList.Length > 0) ? ", " : "") + "แผนก");

                                        if (__projectWeight != null && __projectWeight.__details.Count > 0 && __getTotal != __projectWeight._getTotal)
                                            __optionList.Append(((__optionList.Length > 0) ? ", " : "") + "โครงการ");

                                        if (__allocateWeight != null && __allocateWeight.__details.Count > 0 && __getTotal != __allocateWeight._getTotal)
                                            __optionList.Append(((__optionList.Length > 0) ? ", " : "") + "การจัดสรร");

                                        if (__sideWeight != null && __sideWeight.__details.Count > 0 && __getTotal != __sideWeight._getTotal)
                                            __optionList.Append(((__optionList.Length > 0) ? ", " : "") + "หน่วยงาน");

                                        if (__jobWeight != null && __jobWeight.__details.Count > 0 && __getTotal != __jobWeight._getTotal)
                                            __optionList.Append(((__optionList.Length > 0) ? ", " : "") + "งาน");

                                        string __getItemName = this._icTransItemGrid._cellGet(row, _g.d.ic_trans_detail._item_code).ToString();
                                        MessageBox.Show("กรุณาเฉลี่ยรายการ " + __getItemName + " [" + __optionList.ToString() + "] " + "ให้ครบ", "error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        __isSaveData = false;
                                        break;
                                    }
                                }
                            }
                            break;
                    }
                }
                #endregion

                // เช็คคลังและที่เก็บ
                #region เช็คคลังและที่เก็บ
                if (__isSaveData)
                {
                    int __whColumnNumber = this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._wh_code);
                    int __locationColumnNumber = this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._shelf_code);
                    int __whColumnNumber2 = this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._wh_code_2);
                    int __locationColumnNumber2 = this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._shelf_code_2);

                    if (__whColumnNumber != -1 || __locationColumnNumber != -1 || __whColumnNumber2 != -1 || __locationColumnNumber2 != -1)
                    {
                        for (int __row = 0; __row < this._icTransItemGrid._rowData.Count; __row++)
                        {
                            string __getItemCode = this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString();
                            if (__getItemCode.Length > 0 && __getItemCode.Equals(".") == false)
                            {

                                if (__whColumnNumber != -1 || __whColumnNumber2 != -1)
                                {
                                    string __getwhCode = this._icTransItemGrid._cellGet(__row, __whColumnNumber).ToString();
                                    string __getWHCode2 = (__whColumnNumber2 == -1) ? "NONE" : this._icTransItemGrid._cellGet(__row, __whColumnNumber2).ToString();
                                    if (__getwhCode.Length == 0 || __getWHCode2.Length == 0)
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource("ป้อนข้อมูลคลังสินค้าไม่ครบ"), MyLib._myGlobal._resource("ผิดพลาด"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        __isSaveData = false;
                                        break;
                                    }
                                }

                                if (__locationColumnNumber != -1 || __locationColumnNumber2 != -1)
                                {
                                    string __getShelfCode = this._icTransItemGrid._cellGet(__row, __locationColumnNumber).ToString();
                                    string __getShelfCode2 = (__locationColumnNumber2 == -1) ? "NONE" : this._icTransItemGrid._cellGet(__row, __locationColumnNumber2).ToString();
                                    if (__getShelfCode.Length == 0 || __getShelfCode2.Length == 0)
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource("ป้อนข้อมูลที่เก็บสินค้าไม่ครบ"), MyLib._myGlobal._resource("ผิดพลาด"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        __isSaveData = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                // เช็คหัก ณ ที่จ่าย
                #region เช็คหัก ณ ที่จ่าย
                if (__isSaveData && this._withHoldingTax != null && this._withHoldingTax.Enabled == true)
                {
                    string __checkWht = this._withHoldingTax._checkInputData();

                    if (__checkWht.Length > 0)
                    {
                        __isSaveData = false;
                        MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อน") + " " + __checkWht, MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                #endregion

                // เฉพาะเพิ่มข้อมูลใหม่ ตรวจสอบยอดห้ามติดลบ 
                #region เฉพาะเพิ่มข้อมูลใหม่ ตรวจสอบยอดห้ามติดลบ
                if (__isSaveData)
                {
                    if (this._myManageTrans._mode == 1 ||
                        (this._transControlType == _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก || this._transControlType == _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก))
                    {
                        // เฉพาะเพิ่มข้อมูลใหม่ ตรวจสอบยอดห้ามติดลบ
                        string __checkBalanceStr = this._icTransItemGrid._checkBalanceAll();
                        if (__checkBalanceStr.Length > 0)
                        {
                            MessageBox.Show(__checkBalanceStr.ToString());
                            return;
                        }
                    }
                }
                #endregion

                int __isHold = 0;
                bool __autoApprove = false;
                string __creditStatusRequestQuery = "";
                // ตรวจสอบวงเงินเครดิต
                #region ตรวจสอบวงเงินเครดิต
                if (__isSaveData)
                {

                    if (this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ && this._myManageTrans._mode == 1)
                    {
                        // toe disable ไว้ก่อน ยังไม่เสร็จ ขาด ตรวจสอบเช็คค้าง
                        string __cust_code = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
                        if (this._icTransScreenTop._getDataStr(_g.d.ic_trans._inquiry_type).Equals("0") && __cust_code.Length > 0) // ขายเชื่อเท่านั้น
                        {
                            //string __creditTable = "";
                            __isSaveData = this._creditMoneySaleCheck();
                        }
                    }

                    if (MyLib._myGlobal._programName.Equals("SML CM") && (this._transControlType == _g.g._transControlTypeEnum.ขาย_สั่งขาย || this._transControlType == _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า))
                    {
                        string __inquiry_type = this._icTransScreenTop._getDataStr(_g.d.ic_trans._inquiry_type);
                        if (__inquiry_type.Equals("0") || __inquiry_type.Equals("2")) // ขายเชื่อเท่านั้น
                        {
                            // เช็ควงเงินก่อน
                            __creditStatusRequestQuery = this._creditMoneyCheck(this._transControlType);

                            if (__creditStatusRequestQuery.Length > 0)
                                __isHold = 1;
                            else
                                __autoApprove = true;

                            if (__creditStatusRequestQuery == "false")
                            {
                                __isSaveData = false;
                                __isHold = 0;
                            }

                            if (__isHold == 1)
                            {
                                if (MessageBox.Show("เกินวงเงินเครดิตลูกค้า ต้องการบันทึกเพื่อขออนุมัติหรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                {
                                    __isSaveData = false;
                                }
                            }
                        }
                        // จองเงินสด hold ไว้เพื่อไปเช็ค % เงินล่วงหน้า
                        else if (this._transControlType == _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า && (__inquiry_type.Equals("1") || __inquiry_type.Equals("3")))
                        {
                            __creditStatusRequestQuery = this._creditMoneyCheck(this._transControlType);
                            __isHold = 1;
                        }
                        else if (this._transControlType == _g.g._transControlTypeEnum.ขาย_สั่งขาย && (__inquiry_type.Equals("1") || __inquiry_type.Equals("3")))
                        {
                            __autoApprove = true;
                        }
                        //__isSaveData = this._creditMoneySaleCheck();
                    }
                }
                #endregion

                #endregion

                #region ปรับข้อมูลก่อนบันทึก

                // ปรับสกุลเงิน
                #region ปรับสกุลเงิน
                if (__isSaveData && _g.g._companyProfile._multi_currency
               // &&  MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional
               //  && this._icTransScreenBottom._getDataStr(_g.d.ic_trans._currency_code).Length > 0 &&
               //this._icTransScreenBottom._getDataStr(_g.d.ic_trans._currency_code) != _g.g._companyProfile._home_currency
               //&&  this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._exchange_rate) != 0
               )
                {
                    decimal __exchangeRate = (this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._exchange_rate) == 0) ? 1 : this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._exchange_rate);
                    int __discountColumnNumber = this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._discount);
                    int __columnPrice2 = this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._price_2);

                    //
                    if (__columnPrice2 != -1)
                    {
                        for (int __row = 0; __row < this._icTransItemGrid._rowData.Count; __row++)
                        {
                            string __discountWord = (__discountColumnNumber == -1) ? "0.0" : this._icTransItemGrid._cellGet(__row, __discountColumnNumber).ToString();
                            decimal __qty = (decimal)this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._qty);

                            decimal __homePrice = MyLib._myGlobal._decimalPhase(this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._price_2).ToString()) * __exchangeRate;
                            decimal __homeAmount = MyLib._myGlobal._calcAfterDiscount(__discountWord, MyLib._myGlobal._round(__qty * __homePrice, _g.g._companyProfile._item_amount_decimal), _g.g._companyProfile._item_amount_decimal, __qty);

                            this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._price, __homePrice, false);
                            this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._sum_amount, __homeAmount, false);
                        }
                    }
                }
                #endregion

                // option running เอกสารใหม่ ขายสินค้าและบริการ เท่านั้น
                #region running เอกสารใหม่ ขายสินค้าและบริการ เท่านั้น
                if (__isSaveData && this._myManageTrans._mode == 1 && _g.g._companyProfile._auto_run_docno_sale == true)
                {
                    // กรณีมีเอกสารอยู่แล้ว ให้ Autorun อีกรอบ
                    switch (this._transControlType)
                    {
                        case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                            {
                                string __docFormatCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_format_code).Trim();
                                if (__docFormatCode.Length > 0)
                                {
                                    for (int __loop = 0; __loop < 2; __loop++)
                                    {
                                        string __fieldName = "";
                                        string __fieldNameFormat = "";
                                        switch (__loop)
                                        {
                                            case 0:
                                                __fieldName = _g.d.ic_trans._doc_no;
                                                __fieldNameFormat = _g.d.erp_doc_format._format;
                                                break;
                                            case 1:
                                                __fieldName = _g.d.ic_trans._tax_doc_no;
                                                __fieldNameFormat = _g.d.erp_doc_format._tax_format;
                                                break;
                                        }
                                        // ตรวจสอบเอกสารซ้ำ กรณีเพิ่ม
                                        string __docNo = this._icTransScreenTop._getDataStr(__fieldName).Trim();
                                        string __query1 = "select " + __fieldName + " from " + _g.d.ic_trans._table + " where " + __fieldName + "=\'" + __docNo + "\'";
                                        DataTable __getData = __myFrameWork._queryShort(__query1).Tables[0];
                                        if (__getData.Rows.Count > 0)
                                        {
                                            string __query2 = "select " + _g.d.erp_doc_format._format + "," + _g.d.erp_doc_format._tax_format + "," + _g.d.erp_doc_format._doc_running + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._code + "=\'" + __docFormatCode + "\'";
                                            DataTable __getFormat = __myFrameWork._queryShort(__query2).Tables[0];
                                            if (__getFormat.Rows.Count > 0)
                                            {
                                                string __format = __getFormat.Rows[0][__fieldNameFormat].ToString();
                                                string __startRunningNumber = __getFormat.Rows[0][_g.d.erp_doc_format._doc_running].ToString();
                                                string __newDoc = "";
                                                // กรณีมีเอกสารอยู่แล้ว ให้ Autorun อีกรอบ
                                                switch (__loop)
                                                {
                                                    case 0: // เลขที่เอกสาร
                                                        __newDoc = _g.g._getAutoRun(_g.g._autoRunType.ข้อมูลรายวัน, __docFormatCode, this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_date).ToString(), __format, this._icTransScreenTop._icTransControlType, _g.g._transControlTypeEnum.ว่าง, this._icTransTable, __startRunningNumber);
                                                        break;
                                                    case 1: // เลขที่ใบกำกับภาษี
                                                        __newDoc = _g.g._getAutoRun(_g.g._autoRunType.ใบกำกับภาษี, __docFormatCode, this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_date).ToString(), __format, this._icTransScreenTop._icTransControlType, _g.g._transControlTypeEnum.ว่าง, this._icTransTable);
                                                        break;
                                                }
                                                this._icTransScreenTop._setDataStr(__fieldName, __newDoc);
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }
                #endregion

                #endregion


                if (__isSaveData)
                {
                    Boolean __glManual = false;
                    if (this._glScreenTop != null)
                    {
                        __glManual = (((int)MyLib._myGlobal._decimalPhase(this._glScreenTop._getDataStr(_g.d.gl_journal._trans_direct))) == 1) ? true : false;
                    }

                    // กำหนดให้ดึงทุกบรรทัด
                    this._icTransItemGrid._updateRowIsChangeAll(true);
                    this._icTransRef._transGrid._updateRowIsChangeAll(true);

                    switch (this._transControlType)
                    {
                        case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                        case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                        case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                            // เอาเลขที่อ้างอืงใส่
                            string __docRef = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_ref);
                            for (int __row = 0; __row < this._icTransItemGrid._rowData.Count; __row++)
                            {
                                this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._ref_doc_no, __docRef, false);
                            }
                            break;
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                            for (int __row = 0; __row < this._icTransItemGrid._rowData.Count; __row++)
                            {
                                string __chqNumber = this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString();
                                this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._chq_number, __chqNumber, false);
                            }
                            break;
                    }
                    //
                    this._icTransItemGrid.Focus();
                    this._calc(this._icTransItemGrid);
                    // ดึงรายละเอียดที่เกี่ยวข้อง เพื่อ Update Field ที่จำเป็น
                    StringBuilder __myquery = new StringBuilder();
                    /*ArrayList __itemListGetData = new ArrayList();
                    __itemListGetData = _getItemCodeFromGrid(__itemListGetData, this._icTransItemGrid);
                    __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    if (__itemListGetData.Count > 0)
                    {
                        StringBuilder __itemListStr = new StringBuilder();
                        for (int __loop = 0; __loop < __itemListGetData.Count; __loop++)
                        {
                            if (__itemListStr.Length > 0)
                            {
                                __itemListStr.Append(",");
                            }
                            __itemListStr.Append("\'" + __itemListGetData[__loop].ToString() + "\'");
                        }
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_inventory._code, _g.d.ic_inventory._item_type) + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + " in (" + __itemListStr.ToString() + ") order by " + _g.d.ic_inventory._code));

                    }
                    __myquery.Append("</node>");*/
                    //ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                    //DataTable __itemListData = ((DataSet)__getData[0]).Tables[0];
                    // เพิ่มข้อมูลใหม่
                    StringBuilder __myQuery = new StringBuilder();
                    __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    // Trans
                    ArrayList __getTopQuery = this._icTransScreenTop._createQueryForDatabase();
                    //
                    ArrayList __getBottomQuery = this._icTransScreenBottom._createQueryForDatabase();
                    ArrayList __getMoreQuery = this._icTransScreenMore._createQueryForDatabase();
                    ArrayList __itemList = new ArrayList();
                    //
                    string __docNo = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_no);
                    __docNoList = this._docNoAdd(__docNoList, this._oldDocNo);

                    // toe add doc_ref
                    string __mainDocRef = "";
                    switch (this._transControlType)
                    {

                        case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป_ยกเลิก:
                        case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ_ยกเลิก:
                        case _g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ_ยกเลิก:
                        case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก_ยกเลิก:
                        case _g.g._transControlTypeEnum.สินค้า_โอนออก_ยกเลิก:
                        case _g.g._transControlTypeEnum.สินค้า_โอนเข้า_ยกเลิก:
                        case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก_ยกเลิก:
                        case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด_ยกเลิก:

                        case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก:
                        case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ:

                        case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                        case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ:

                        case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก:
                        case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก:

                        case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก:
                        case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก:
                        case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก:

                        case _g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก:
                        case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                        case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:

                        case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก:
                        case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก:

                        case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก:
                        case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก:
                        case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก:

                        case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก:
                        case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก:
                        case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น_ยกเลิก:
                        case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล_ยกเลิก:
                        case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก:

                        case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น_ยกเลิก:
                        case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น_ยกเลิก:
                        case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น_ยกเลิก:
                        case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล_ยกเลิก:
                        case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก:

                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ยกเลิก:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก:

                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก:

                            __mainDocRef = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_ref);
                            break;
                    }

                    if (__mainDocRef.Length > 0)
                        __docNoList = this._docNoAdd(__docNoList, __mainDocRef);

                    if (this._oldDocRef.Length > 0)
                    {
                        __docNoList = __docNoList + "," + this._oldDocRef;
                    }

                    switch (this._transControlType)
                    {
                        case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก:
                        case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก:
                        case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก:
                            string __getDocRefCancel = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_ref);
                            __docNoList = this._docNoAdd(__docNoList, __getDocRefCancel);

                            break;

                    }

                    __docNoList = this._docNoAdd(__docNoList, __docNo);
                    string __docDate = this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_date);
                    string __docTime = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_time);
                    // ลบข้อมูลเก่าออกก่อน ในกรณีแก้ไข
                    if (this._myManageTrans._mode == 2)
                    {
                        if (this._vatBuy != null)
                        {
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_vat_buy._table + " where " + _g.d.gl_journal_vat_buy._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.gl_journal_vat_buy._trans_flag + "=" + this._getTransFlag.ToString()));
                        }
                        if (this._vatSale != null)
                        {
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_vat_sale._table + " where " + _g.d.gl_journal_vat_sale._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.gl_journal_vat_sale._trans_flag + "=" + this._getTransFlag.ToString()));
                        }
                        if (this._glScreenTop != null)
                        {
                            // ลบ GL เก่าออก
                            //
                            // if (_g.g._companyProfile._gl_process_realtime == false || __glManual == true)
                            {
                                __myQuery.Append(this._glDeleteQuery(this._oldDocNo));
                            }
                        }
                    }

                    // toe เพิ่ม creator และ editor
                    string __create_doc_field = "";
                    string __create_doc_value = "";

                    if (this._myManageTrans._mode == 2)
                    {
                        __create_doc_field = "," + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans._creator_code, _g.d.ic_trans._create_datetime, _g.d.ic_trans._last_editor_code, _g.d.ic_trans._lastedit_datetime);
                        __create_doc_value = "," + MyLib._myGlobal._fieldAndComma("\'" + this._creator_code + "\'", "\'" + MyLib._myGlobal._convertDateTimeToQuery(this._create_datetime) + "\'", "\'" + MyLib._myGlobal._userCode + "\'", "\'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\'");
                    }
                    else
                    {
                        __create_doc_field = "," + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans._creator_code, _g.d.ic_trans._create_datetime);
                        __create_doc_value = "," + MyLib._myGlobal._fieldAndComma("\'" + MyLib._myGlobal._userCode + "\'", "\'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\'");

                    }

                    //
                    //Top Screen and Bottom Screen Check
                    if (__getTopQuery[0].ToString().Length > 0)
                    {
                        switch (this._transControlType)
                        {
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:

                            case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย:
                            case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย:
                            case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย_ยกเลิก:

                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                            case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_โอนออก_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ_ยกเลิก:

                            case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก_ยกเลิก:
                            case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก:
                            case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น_ยกเลิก:
                            case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก:
                            case _g.g._transControlTypeEnum.SoInquiry:
                            case _g.g._transControlTypeEnum.SoEstimate:
                            case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                            case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ:
                            case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก:
                            case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                            case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                            case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ:
                            case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                            case _g.g._transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ:
                            case _g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก:
                            case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                            case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ:
                            case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                            case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                            case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                            case _g.g._transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ:
                            case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                            case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                            case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                            case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                            case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                            case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                            case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                            case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                            case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                            case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                            case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                            case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                            case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                            case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                            case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                            case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                            case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:

                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก:

                            case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก:
                            case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก:
                            case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก:
                            case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก:
                            case _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน_ยกเลิก:
                            case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก:
                            case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก:
                            case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก:
                            case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก:
                            case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก:
                            case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า:
                            case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก:
                            case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก:
                            case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                            case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา:
                            case _g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ:
                            case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                            case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                            case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก:
                            case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด:
                            // toe
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค_ยกเลิก:

                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค_ยกเลิก:

                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร_ยกเลิก:

                            case _g.g._transControlTypeEnum.สินค้า_บันทึกการจัดส่ง:

                            case _g.g._transControlTypeEnum.คลัง_ยอดคงเหลือสินค้ายกมา:
                            case _g.g._transControlTypeEnum.คลัง_ตรวจนับสินค้า:
                            case _g.g._transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ:
                            case _g.g._transControlTypeEnum.คลัง_จ่ายสินค้าออก_จากการขาย:
                            case _g.g._transControlTypeEnum.คลัง_ปรับปรุงเพิ่มลดสินค้า:

                            case _g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก:
                            case _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:
                            case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:

                            case _g.g._transControlTypeEnum.สินค้า_ขอโอน:

                                this._getTransStatus = 0;
                                if (this._myManageTrans._mode == 2)
                                {
                                    // ลบของเก่าก่อน
                                    // ลบหัว
                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + this._icTransTable + " where " + _g.d.ic_trans._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.ic_trans._trans_flag + "=" + this._getTransFlag.ToString()));
                                }
                                //
                                string __bottomField = "";
                                string __bottomData = "";
                                if (__getBottomQuery[0].ToString().Trim().Length != 0)
                                {
                                    __bottomField = "," + __getBottomQuery[0].ToString();
                                    __bottomData = "," + __getBottomQuery[1].ToString();
                                }
                                string __moreField = "";
                                string __moreData = "";
                                if (__getMoreQuery[0].ToString().Trim().Length != 0)
                                {
                                    __moreField = "," + __getMoreQuery[0].ToString();
                                    __moreData = "," + __getMoreQuery[1].ToString();
                                }

                                if (this._vatBuy != null)
                                {
                                    __moreField += "," + _g.d.ic_trans._is_manual_vat;
                                    __moreData += "," + ((this._vatBuy._manualVatCheckbox.Checked) ? "1" : "0");

                                }


                                if (__isHold == 1)
                                {
                                    __moreField += "," + _g.d.ic_trans._is_hold;
                                    __moreData += ",1";

                                }

                                if (__autoApprove == true)
                                {
                                    __moreField += "," + _g.d.ic_trans._auto_approved;
                                    __moreData += ",1";
                                }

                                if (this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ)
                                {
                                    if (this._icTransScreenBottom._getDataStr(_g.d.ic_trans._is_arm).Equals("1"))
                                    {
                                        __moreField += "," + _g.d.ic_trans._ref_doc_type;
                                        __moreData += ",\'ARM\'";
                                    }
                                }


                                /* __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans._table
                                     + " (" + _g.d.ic_trans._doc_format_code + "," + _g.d.ic_trans._trans_type.ToString() + "," + _g.d.ic_trans._trans_flag.ToString() + ","
                                     + _g.d.ic_trans._status + "," + __getTopQuery[0].ToString() + __bottomField + __moreField
                                     + ") values (\'" + this._icTransScreenTop._docFormatCode + "\'," + this._getTransType.ToString() + "," + this._getTransFlag.ToString() + "," + this._getTransStatus.ToString() + "," + __getTopQuery[1].ToString() + __bottomData + __moreData + ")"));
                                 */
                                //somruk

                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + this._icTransTable
                                    + " (" + _g.d.ic_trans._trans_type.ToString() + "," + _g.d.ic_trans._trans_flag.ToString() + ","
                                    + _g.d.ic_trans._status + "," + __getTopQuery[0].ToString() + __bottomField + __moreField + "," + _g.d.ic_trans._is_pos + __create_doc_field
                                    + ") values (" + this._getTransType.ToString() + "," + this._getTransFlag.ToString() + "," + this._getTransStatus.ToString() + "," + __getTopQuery[1].ToString() + __bottomData + __moreData + ",0" + __create_doc_value + ")"));
                                break;
                            case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                                {
                                    this._getTransStatus = 0;
                                    if (this._myManageTrans._mode == 2)
                                    {
                                        // ลบของเก่าก่อน
                                        // ลบหัว
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + this._icTransTable
                                            + " where " + _g.d.ic_trans._doc_no + "=\'" + this._oldDocNo + "\' and ("
                                            + _g.d.ic_trans._trans_flag + "=" + this._getTransFlag.ToString() + " or " + _g.d.ic_trans._trans_flag
                                            + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ")"));
                                    }
                                    // โอนออก
                                    /*  __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans._table
                                          + " (" + _g.d.ic_trans._doc_format_code + "," + _g.d.ic_trans._trans_type.ToString() + "," + _g.d.ic_trans._trans_flag.ToString() + ","
                                          + _g.d.ic_trans._status + "," + __getTopQuery[0].ToString()
                                          + ") values (\'" + this._icTransScreenTop._docFormatCode + "\'," + this._getTransType.ToString() + "," + this._getTransFlag.ToString() + "," + this._getTransStatus.ToString() + "," + __getTopQuery[1].ToString() + ")"));
                                      */
                                    //somruk
                                    __moreField = "";
                                    __moreData = "";
                                    if (__getMoreQuery[0].ToString().Trim().Length != 0)
                                    {
                                        __moreField = "," + __getMoreQuery[0].ToString();
                                        __moreData = "," + __getMoreQuery[1].ToString();
                                    }

                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + this._icTransTable
                                        + " (" + _g.d.ic_trans._trans_type.ToString() + "," + _g.d.ic_trans._trans_flag.ToString() + ","
                                        + _g.d.ic_trans._status + "," + __getTopQuery[0].ToString() + "," + _g.d.ic_trans._is_pos + __moreField + __create_doc_field
                                        + ") values (" + this._getTransType.ToString() + "," + this._getTransFlag.ToString() + "," + this._getTransStatus.ToString() + "," + __getTopQuery[1].ToString() + ",0" + __moreData + __create_doc_value + ")"));
                                    // โอนเข้า (สร้าง Auto)
                                    // ขยับเวลาไปอีก 1 นาที ต้นทุนจะได้ไม่ผิด
                                    string __lastTime = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_time);
                                    string[] __lastTimeSplit = __lastTime.Split(':');
                                    int __hour = MyLib._myGlobal._intPhase(__lastTimeSplit[0]);
                                    int __minute = MyLib._myGlobal._intPhase(__lastTimeSplit[1]) + 1;
                                    if (__minute > 59)
                                    {
                                        __minute = 0;
                                        __hour++;
                                        if (__hour > 24)
                                        {
                                            __hour = 0;
                                        }
                                    }
                                    this.__newTime = String.Format("{0}:{1}", __hour.ToString("0#"), __minute.ToString("0#"));
                                    this._icTransScreenTop._setDataStr(_g.d.ic_trans._doc_time, __newTime);
                                    ArrayList __getTopQuery2 = this._icTransScreenTop._createQueryForDatabase();
                                    /* __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans._table
                                         + " (" + _g.d.ic_trans._doc_format_code + "," + _g.d.ic_trans._trans_type.ToString() + "," + _g.d.ic_trans._trans_flag.ToString() + ","
                                         + _g.d.ic_trans._status + "," + __getTopQuery[0].ToString()
                                         + ") values (\'" + this._icTransScreenTop._docFormatCode + "\'," + this._getTransType.ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + "," + this._getTransStatus.ToString() + "," + __getTopQuery[1].ToString() + ")"));
                                     */

                                    //somruk
                                    // toe more field 2
                                    string __moreFieldTransferIn = __moreField.Replace("," + _g.d.ic_trans._branch_code_to + ",", ",tmp_" + _g.d.ic_trans._branch_code_to + ",")
                                        .Replace("," + _g.d.ic_trans._side_code_to + ",", ",tmp_" + _g.d.ic_trans._side_code_to + ",")
                                        .Replace("," + _g.d.ic_trans._department_code_to + ",", ",tmp_" + _g.d.ic_trans._department_code_to + ",")
                                        .Replace("," + _g.d.ic_trans._project_code_to + ",", ",tmp_" + _g.d.ic_trans._project_code_to + ",")
                                        .Replace("," + _g.d.ic_trans._allocate_code_to + ",", ",tmp_" + _g.d.ic_trans._allocate_code_to + ",")
                                        .Replace("," + _g.d.ic_trans._job_code_to + ",", ",tmp_" + _g.d.ic_trans._job_code_to + ",");

                                    __moreFieldTransferIn = __moreFieldTransferIn.Replace("," + _g.d.ic_trans._branch_code + ",", "," + _g.d.ic_trans._branch_code_to + ",")
                                        .Replace("," + _g.d.ic_trans._side_code + ",", "," + _g.d.ic_trans._side_code_to + ",")
                                        .Replace("," + _g.d.ic_trans._department_code + ",", "," + _g.d.ic_trans._department_code_to + ",")
                                        .Replace("," + _g.d.ic_trans._project_code + ",", "," + _g.d.ic_trans._project_code_to + ",")
                                        .Replace("," + _g.d.ic_trans._allocate_code + ",", "," + _g.d.ic_trans._allocate_code_to + ",")
                                        .Replace("," + _g.d.ic_trans._job_code + ",", "," + _g.d.ic_trans._job_code_to + ",");

                                    __moreFieldTransferIn = __moreFieldTransferIn.Replace(",tmp_" + _g.d.ic_trans._branch_code_to + ",", "," + _g.d.ic_trans._branch_code + ",")
                                        .Replace(",tmp_" + _g.d.ic_trans._side_code_to + ",", "," + _g.d.ic_trans._side_code + ",")
                                        .Replace(",tmp_" + _g.d.ic_trans._department_code_to + ",", "," + _g.d.ic_trans._department_code + ",")
                                        .Replace(",tmp_" + _g.d.ic_trans._project_code_to + ",", "," + _g.d.ic_trans._project_code + ",")
                                        .Replace(",tmp_" + _g.d.ic_trans._allocate_code_to + ",", "," + _g.d.ic_trans._allocate_code + ",")
                                        .Replace(",tmp_" + _g.d.ic_trans._job_code_to + ",", "," + _g.d.ic_trans._job_code + ",");

                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + this._icTransTable
                                      + " (" + _g.d.ic_trans._trans_type.ToString() + "," + _g.d.ic_trans._trans_flag.ToString() + ","
                                      + _g.d.ic_trans._status + "," + __getTopQuery[0].ToString() + "," + _g.d.ic_trans._is_pos + __moreFieldTransferIn + __create_doc_field
                                      + ") values (" + this._getTransType.ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + "," + this._getTransStatus.ToString() + "," + __getTopQuery[1].ToString() + ",0" + __moreData + __create_doc_value + ")"));
                                    // เอาค่าเดิมมา
                                    this._icTransScreenTop._setDataStr(_g.d.ic_trans._doc_time, __lastTime);
                                }
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร:
                                {
                                    this._getTransStatus = 0;
                                    if (this._myManageTrans._mode == 2)
                                    {
                                        // ลบของเก่า
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + this._icTransTable + " where " + _g.d.ic_trans._doc_no + "=\'" + this._oldDocNo + "\' and (" + _g.d.ic_trans._trans_flag + "=" + this._getTransFlag.ToString() + " or " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินเข้าธนาคาร) + ")"));
                                    }

                                    __moreField = "";
                                    __moreData = "";
                                    if (__getMoreQuery[0].ToString().Trim().Length != 0)
                                    {
                                        __moreField = "," + __getMoreQuery[0].ToString();
                                        __moreData = "," + __getMoreQuery[1].ToString();
                                    }
                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + this._icTransTable
                                        + "(" + _g.d.ic_trans._trans_type + "," + _g.d.ic_trans._trans_flag + ","
                                        + _g.d.ic_trans._status + "," + __getTopQuery[0].ToString() + "," + __getBottomQuery[0].ToString() + __moreField + __create_doc_field
                                        + ") values (" + this._getTransType.ToString() + "," + this._getTransFlag.ToString() + "," + this._getTransStatus.ToString() + "," + __getTopQuery[1].ToString() + "," + __getBottomQuery[1].ToString() + __moreData + __create_doc_value
                                        + ")"));

                                    // ขยับเวลาไปอีก 1 นาที 
                                    string __lastTime = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_time);
                                    string[] __lastTimeSplit = __lastTime.Split(':');
                                    int __hour = MyLib._myGlobal._intPhase(__lastTimeSplit[0]);
                                    int __minute = MyLib._myGlobal._intPhase(__lastTimeSplit[1]) + 1;
                                    if (__minute > 59)
                                    {
                                        __minute = 0;
                                        __hour++;
                                        if (__hour > 24)
                                        {
                                            __hour = 0;
                                        }
                                    }
                                    this.__newTime = String.Format("{0}:{1}", __hour.ToString("0#"), __minute.ToString("0#"));
                                    this._icTransScreenTop._setDataStr(_g.d.ic_trans._doc_time, __newTime);
                                    ArrayList __getTopQuery2 = this._icTransScreenTop._createQueryForDatabase();

                                    string __moreFieldTransferIn = __moreField.Replace("," + _g.d.ic_trans._branch_code_to + ",", ",tmp_" + _g.d.ic_trans._branch_code_to + ",")
    .Replace("," + _g.d.ic_trans._side_code_to + ",", ",tmp_" + _g.d.ic_trans._side_code_to + ",")
    .Replace("," + _g.d.ic_trans._department_code_to + ",", ",tmp_" + _g.d.ic_trans._department_code_to + ",")
    .Replace("," + _g.d.ic_trans._project_code_to + ",", ",tmp_" + _g.d.ic_trans._project_code_to + ",")
    .Replace("," + _g.d.ic_trans._allocate_code_to + ",", ",tmp_" + _g.d.ic_trans._allocate_code_to + ",")
    .Replace("," + _g.d.ic_trans._job_code_to + ",", ",tmp_" + _g.d.ic_trans._job_code_to + ",");

                                    __moreFieldTransferIn = __moreFieldTransferIn.Replace("," + _g.d.ic_trans._branch_code + ",", "," + _g.d.ic_trans._branch_code_to + ",")
                                        .Replace("," + _g.d.ic_trans._side_code + ",", "," + _g.d.ic_trans._side_code_to + ",")
                                        .Replace("," + _g.d.ic_trans._department_code + ",", "," + _g.d.ic_trans._department_code_to + ",")
                                        .Replace("," + _g.d.ic_trans._project_code + ",", "," + _g.d.ic_trans._project_code_to + ",")
                                        .Replace("," + _g.d.ic_trans._allocate_code + ",", "," + _g.d.ic_trans._allocate_code_to + ",")
                                        .Replace("," + _g.d.ic_trans._job_code + ",", "," + _g.d.ic_trans._job_code_to + ",");

                                    __moreFieldTransferIn = __moreFieldTransferIn.Replace(",tmp_" + _g.d.ic_trans._branch_code_to + ",", "," + _g.d.ic_trans._branch_code + ",")
                                        .Replace(",tmp_" + _g.d.ic_trans._side_code_to + ",", "," + _g.d.ic_trans._side_code + ",")
                                        .Replace(",tmp_" + _g.d.ic_trans._department_code_to + ",", "," + _g.d.ic_trans._department_code + ",")
                                        .Replace(",tmp_" + _g.d.ic_trans._project_code_to + ",", "," + _g.d.ic_trans._project_code + ",")
                                        .Replace(",tmp_" + _g.d.ic_trans._allocate_code_to + ",", "," + _g.d.ic_trans._allocate_code + ",")
                                        .Replace(",tmp_" + _g.d.ic_trans._job_code_to + ",", "," + _g.d.ic_trans._job_code + ",");


                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + this._icTransTable
                                        + "(" + _g.d.ic_trans._trans_type + "," + _g.d.ic_trans._trans_flag + ","
                                        + _g.d.ic_trans._status + "," + __getTopQuery2[0].ToString() + "," + __getBottomQuery[0].ToString() + __moreFieldTransferIn + __create_doc_field
                                        + ") values (" + this._getTransType.ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินเข้าธนาคาร).ToString() + "," + this._getTransStatus.ToString() + "," + __getTopQuery2[1].ToString() + "," + __getBottomQuery[1].ToString() + __moreData + __create_doc_value
                                        + ")"));

                                    // คืนค่าเวลาเดิม
                                    this._icTransScreenTop._setDataStr(_g.d.ic_trans._doc_time, __lastTime);
                                }
                                break;
                            default:
                                if (SystemInformation.ComputerName.ToLower().IndexOf("jead8") != -1 || SystemInformation.ComputerName.ToLower().IndexOf("toe-pc") != -1)
                                {
                                    MessageBox.Show("_saveData case not found : " + this._transControlType.ToString());
                                }
                                break;
                        }
                    }
                    // แปลสภาพ field ใน grid
                    decimal __vatRate = (this._vatType == _g.g._vatTypeEnum.ยกเว้นภาษี) ? 0.0M : this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._vat_rate);
                    int _inquiryType = 0;
                    if (this._icTransScreenTop._getControl(_g.d.ic_trans._inquiry_type) != null)
                    {
                        _inquiryType = MyLib._myGlobal._intPhase(this._icTransScreenTop._getDataStr(_g.d.ic_trans._inquiry_type));
                    }
                    for (int __row = 0; __row < this._icTransItemGrid._rowData.Count; __row++)
                    {
                        decimal __price = 0M;
                        decimal __priceExcludeVat = 0M;
                        decimal __discountAmount = 0M;
                        // หาเอกสารอ้างอิง
                        string __docRef = this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._ref_doc_no).ToString().Trim();
                        // แปรสภาพราคา
                        int __columnPrice = this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._price);
                        if (__columnPrice != -1)
                        {
                            __price = (decimal)this._icTransItemGrid._cellGet(__row, __columnPrice);
                            __priceExcludeVat = __price;
                            // กรณีเป็นรวมใน
                            if (this._vatType == _g.g._vatTypeEnum.ภาษีรวมใน)
                            {
                                __priceExcludeVat = (__priceExcludeVat * 100M) / (100M + __vatRate);
                            }
                        }
                        // แปลสภาพส่วนลด
                        int __columnDiscount = this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._discount);
                        int __columnQty = this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._qty);
                        if (__columnDiscount != -1 && __columnQty != -1)
                        {
                            decimal __qty = (decimal)this._icTransItemGrid._cellGet(__row, __columnQty);
                            string __discountWord = this._icTransItemGrid._cellGet(__row, __columnDiscount).ToString();
                            MyLib._calcDiscountResultStruct __resultDiscount = MyLib._myGlobal._calcDiscountOnly(__price, __discountWord, MyLib._myGlobal._round(__qty * __price, _g.g._companyProfile._item_amount_decimal), _g.g._companyProfile._item_amount_decimal);
                            __discountAmount = __resultDiscount._discountAmount;
                            // กรณีราคาเปลี่ยนจากสูตร
                            if (__price != __resultDiscount._newPrice)
                            {
                                __price = __resultDiscount._newPrice;
                            }
                            // กรณีเป็นรวมใน
                            if (this._vatType == _g.g._vatTypeEnum.ภาษีรวมใน)
                            {
                                __discountAmount = (__discountAmount * 100M) / (100M + __vatRate);
                            }
                        }
                        // ภาษีมูลค่าเพิ่ม
                        int __columnVat = this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._total_vat_value);
                        decimal __vatAmount = 0M;
                        if (__columnQty != -1)
                        {
                            decimal __qty = (decimal)this._icTransItemGrid._cellGet(__row, __columnQty);
                            __vatAmount = ((__priceExcludeVat * __qty) - __discountAmount) * __vatRate / 100M;
                        }
                        //
                        this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._total_vat_value, __vatAmount, false);
                        this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._price_exclude_vat, __priceExcludeVat, false);
                        this._icTransItemGrid._cellUpdate(__row, _g.d.ic_trans_detail._discount_amount, __discountAmount, false);
                    }
                    // TransDetail  DataGrid Check
                    string __docDateQuery = this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_date);
                    string __docTimeQuery = this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_time);
                    string __fieldTransDetailList = _g.d.ic_trans_detail._cust_code + "," + _g.d.ic_trans_detail._trans_type + "," + _g.d.ic_trans_detail._trans_flag + "," + _g.d.ic_trans_detail._doc_date + "," + _g.d.ic_trans_detail._doc_time + "," + _g.d.ic_trans_detail._doc_date_calc + "," + _g.d.ic_trans_detail._doc_time_calc + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._is_pos + "," + _g.d.ic_trans_detail._inquiry_type + ",";
                    string __dataTransDetailList = "\'" + this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code) + "\'," + _getTransType + "," + _getTransFlag + "," + __docDateQuery + "," + __docTimeQuery + "," + __docDateQuery + "," + __docTimeQuery + "," + this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_no) + ",0," + _inquiryType.ToString() + ",";
                    SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();

                    Boolean __branchCodeTransDetail = false;
                    string __getBranchcode = MyLib._myGlobal._branchCode;
                    if (this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._branch_code) != -1)
                    {
                        __branchCodeTransDetail = true;
                    }
                    if (this._icTransScreenMore._getControl(_g.d.ic_trans._branch_code) != null && this._icTransScreenMore._getDataStr(_g.d.ic_trans._branch_code).Length > 0)
                    {
                        __getBranchcode = this._icTransScreenMore._getDataStr(_g.d.ic_trans._branch_code);
                    }

                    #region Save ic_trans_detail, ic_trans_serial_number

                    switch (this._transControlType)
                    {
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                        case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย:
                        case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                        case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                        case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ:
                        case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก:
                        case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                        case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                        case _g.g._transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ:
                        case _g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก:
                        case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                        case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                        case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ:
                        case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                        case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                        case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                        case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                        case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                        case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                        case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                        case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                        case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                        case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                        case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                        case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                        case _g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า:
                        case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                        case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ:
                        case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                        case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                        case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                        case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา:

                        case _g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ:
                        case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                        case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                        case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก:
                        case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด:

                        case _g.g._transControlTypeEnum.สินค้า_บันทึกการจัดส่ง:

                        case _g.g._transControlTypeEnum.คลัง_ยอดคงเหลือสินค้ายกมา:
                        case _g.g._transControlTypeEnum.คลัง_ตรวจนับสินค้า:
                        case _g.g._transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ:
                        case _g.g._transControlTypeEnum.คลัง_จ่ายสินค้าออก_จากการขาย:
                        case _g.g._transControlTypeEnum.คลัง_ปรับปรุงเพิ่มลดสินค้า:

                        case _g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก:
                        case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:
                        case _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:

                        case _g.g._transControlTypeEnum.สินค้า_ขอโอน:
                            {
                                if (this._myManageTrans._mode == 2)
                                {
                                    // เอารหัสเก่าไปประมวลผล
                                    string __where1 = _g.d.ic_trans_detail._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + this._getTransFlag.ToString();
                                    __itemList = this._getOldItemCode(__itemList, __where1);
                                    // หาง
                                    // ลบของเก่าก่อน save
                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + this._icTransDetailTable + " where " + __where1));
                                    // ลบ serial number
                                    string __whereSerialNumber = _g.d.ic_trans_serial_number._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.ic_trans_serial_number._trans_flag + "=" + this._getTransFlag.ToString();
                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_serial_number._table + " where " + __whereSerialNumber));
                                    //
                                    switch (this._transControlType)
                                    {
                                        case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย:
                                        case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย:
                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                                        case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                                        case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                                        case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                                        case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                                        case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                                        case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                                        case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                                        case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                                        case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                                        case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                                        case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                                        case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                                        case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                                        case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                                        case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                                        case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                                        case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                                        case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                                        case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                                        case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:

                                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                                        case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:
                                        case _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:

                                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ap_ar_trans_detail._table + " where " + _g.d.ap_ar_trans_detail._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.ap_ar_trans_detail._trans_flag + "=" + this._getTransFlag.ToString()));
                                            this._icTransRef._transGrid._updateRowIsChangeAll(true);
                                            break;
                                    }

                                    // ลบ Trans Detail Department
                                    if (_g.g._companyProfile._use_department == 1)
                                    {
                                        string __whereICTransDepartment = _g.d.ic_trans_detail_department._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.ic_trans_detail_department._trans_flag + "=" + this._getTransFlag.ToString();
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail_department._table + " where " + __whereICTransDepartment));
                                    }

                                    // ลบ Trans Detail Project
                                    if (_g.g._companyProfile._use_project == 1)
                                    {
                                        string __whereICTransProject = _g.d.ic_trans_detail_project._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.ic_trans_detail_project._trans_flag + "=" + this._getTransFlag.ToString();
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail_project._table + " where " + __whereICTransProject));
                                    }

                                    // ลบ Trans Detail Allocate
                                    if (_g.g._companyProfile._use_allocate == 1)
                                    {
                                        string __whereICTransAllocate = _g.d.ic_trans_detail_allocate._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.ic_trans_detail_allocate._trans_flag + "=" + this._getTransFlag.ToString();
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail_allocate._table + " where " + __whereICTransAllocate));
                                    }

                                    if (_g.g._companyProfile._use_job == 1)
                                    {
                                        string __whereICTransAllocate = _g.d.ic_trans_detail_jobs._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.ic_trans_detail_jobs._trans_flag + "=" + this._getTransFlag.ToString();
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail_jobs._table + " where " + __whereICTransAllocate));
                                    }

                                    if (_g.g._companyProfile._use_unit == 1)
                                    {
                                        string __whereICTransAllocate = _g.d.ic_trans_detail_site._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.ic_trans_detail_site._trans_flag + "=" + this._getTransFlag.ToString();
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail_site._table + " where " + __whereICTransAllocate));
                                    }

                                }
                                // รหัสสินค้าใน Grid เอาไป Process
                                __itemList = _getItemCodeFromGrid(__itemList, this._icTransItemGrid);
                                //
                                _getTransStatus = 0;
                                // ปรับสถานะ 1=เป็นด้านบวก,-1=ด้านลบออก
                                if (this._transControlType == _g.g._transControlTypeEnum.คลัง_ตรวจนับสินค้า ||
                                    this._transControlType == _g.g._transControlTypeEnum.คลัง_ปรับปรุงเพิ่มลดสินค้า ||
                                    this._transControlType == _g.g._transControlTypeEnum.คลัง_ยอดคงเหลือสินค้ายกมา ||
                                    this._transControlType == _g.g._transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ ||
                                    this._transControlType == _g.g._transControlTypeEnum.คลัง_จ่ายสินค้าออก_จากการขาย)
                                {
                                    __fieldTransDetailList = __fieldTransDetailList + _g.d.ic_trans_detail._status + "," + _g.d.ic_trans_detail._calc_flag + "," + _g.d.ic_trans_detail._vat_type + "," + _g.d.ic_trans_detail._last_status + "," + _g.d.ic_trans_detail._auto_create + "," + ((__branchCodeTransDetail == false) ? _g.d.ic_trans_detail._branch_code + "," : "");
                                    __dataTransDetailList = __dataTransDetailList + this._getTransStatus + "," + _g.g._transCalcTypeGlobal._transStockCalcType(this._transControlType) + "," + this._vatTypeNumber().ToString() + ",0,0," + ((__branchCodeTransDetail == false) ? "\'" + __getBranchcode + "\'," : "");

                                }
                                else
                                {
                                    __fieldTransDetailList = __fieldTransDetailList + _g.d.ic_trans_detail._sale_code + "," + _g.d.ic_trans_detail._sale_group + "," + _g.d.ic_trans_detail._status + "," + _g.d.ic_trans_detail._calc_flag + "," + _g.d.ic_trans_detail._vat_type + "," + _g.d.ic_trans_detail._last_status + "," + _g.d.ic_trans_detail._auto_create + "," + ((__branchCodeTransDetail == false) ? _g.d.ic_trans_detail._branch_code + "," : "");
                                    __dataTransDetailList = __dataTransDetailList + "\'" + this._icTransScreenTop._getDataStr(_g.d.ic_trans._sale_code) + "\',\'" + this._icTransScreenTop._getDataStr(_g.d.ic_trans._sale_group) + "\'," + this._getTransStatus + "," + _g.g._transCalcTypeGlobal._transStockCalcType(this._transControlType) + "," + this._vatTypeNumber().ToString() + ",0,0," + ((__branchCodeTransDetail == false) ? "\'" + __getBranchcode + "\'," : "");
                                }

                                // toe branch trans_detail insert 
                                //if (_g.g._companyProfile._branchStatus == 1)
                                //{
                                //    int __itemCodeColumnCheck = this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._branch_code);

                                //    if (this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._branch_code) == -1)
                                //    {
                                //        __fieldTransDetailList += _g.d.ic_trans_detail._branch_code + ",";
                                //        __dataTransDetailList += "\'" + __getBranchcode + "\',";
                                //    }
                                //}

                                //
                                __myQuery.Append(this._icTransItemGrid._createQueryForInsert(this._icTransDetailTable, __fieldTransDetailList, __dataTransDetailList, false, true));


                                // กรณีอ้างบิล 
                                string __fieldList2 = "";
                                string __dataList2 = "";
                                switch (this._transControlType)
                                {
                                    case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย:
                                    case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย:
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                                    case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                                    case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                                    case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                                    case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                                    case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                                    case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                                    case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                                    case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                                    case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                                    case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                                    case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                                    case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                                    case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                                    case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                                    case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                                    case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                                    case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:

                                    case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:
                                    case _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:
                                        __fieldList2 = _g.d.ap_ar_trans_detail._calc_flag + "," + _g.d.ap_ar_trans_detail._trans_type + "," + _g.d.ap_ar_trans_detail._trans_flag + "," + _g.d.ap_ar_trans_detail._doc_date + "," + _g.d.ap_ar_trans_detail._doc_no + ",";
                                        __dataList2 = _g.g._transCalcTypeGlobal._apArTransCalcType(this._transControlType).ToString() + "," + this._getTransType.ToString() + "," + this._getTransFlag.ToString() + "," + this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_date) + "," + this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_no) + ",";

                                        __myQuery.Append(this._icTransRef._transGrid._createQueryForInsert(_g.d.ap_ar_trans_detail._table, __fieldList2, __dataList2, false, true));
                                        // เอกสารอ้างอิงเอาไปประมวลผลด้วย
                                        for (int __loop2 = 0; __loop2 < this._icTransRef._transGrid._rowData.Count; __loop2++)
                                        {
                                            string __getDocNo = this._icTransRef._transGrid._cellGet(__loop2, _g.d.ap_ar_trans_detail._billing_no).ToString().Trim();
                                            if (__getDocNo.Length > 0)
                                            {
                                                __docNoList = this._docNoAdd(__docNoList, __getDocNo);
                                            }
                                        }
                                        break;
                                }
                            }
                            break;
                        case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                            {
                                if (this._myManageTrans._mode == 2)
                                {
                                    // เอารหัสเก่าไปประมวลผล
                                    string __where1 = _g.d.ic_trans_detail._doc_no + "=\'" + this._oldDocNo + "\' and (" + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนออก).ToString()
                                        + " or " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ")";
                                    __itemList = this._getOldItemCode(__itemList, __where1);
                                    // หาง
                                    // ลบของเก่าก่อน save
                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + this._icTransDetailTable + " where " + __where1));
                                    this._icTransItemGrid._updateRowIsChangeAll(true);
                                    // ลบ serial
                                    string __whereSerialNumber = _g.d.ic_trans_serial_number._doc_no + "=\'" + this._oldDocNo + "\' and (" + _g.d.ic_trans_serial_number._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนออก).ToString()
                                        + " or " + _g.d.ic_trans_serial_number._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ")";
                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_serial_number._table + " where " + __whereSerialNumber));

                                    // ลบ Trans Detail Department
                                    if (_g.g._companyProfile._use_department == 1)
                                    {
                                        string __whereICTransDepartment = _g.d.ic_trans_detail_department._doc_no + "=\'" + this._oldDocNo + "\' and (" + _g.d.ic_trans_detail_department._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนออก).ToString()
                                        + " or " + _g.d.ic_trans_detail_department._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ")";
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail_department._table + " where " + __whereICTransDepartment));
                                    }

                                    // ลบ Trans Detail Project
                                    if (_g.g._companyProfile._use_project == 1)
                                    {
                                        string __whereICTransProject = _g.d.ic_trans_detail_project._doc_no + "=\'" + this._oldDocNo + "\' and (" + _g.d.ic_trans_detail_project._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนออก).ToString()
                                        + " or " + _g.d.ic_trans_detail_project._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ")";
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail_project._table + " where " + __whereICTransProject));
                                    }

                                    // ลบ Trans Detail Allocate
                                    if (_g.g._companyProfile._use_allocate == 1)
                                    {
                                        string __whereICTransAllocate = _g.d.ic_trans_detail_allocate._doc_no + "=\'" + this._oldDocNo + "\' and (" + _g.d.ic_trans_detail_allocate._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนออก).ToString()
                                        + " or " + _g.d.ic_trans_detail_allocate._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ")";
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail_allocate._table + " where " + __whereICTransAllocate));
                                    }

                                    // ลบ Trans Detail Allocate
                                    if (_g.g._companyProfile._use_job == 1)
                                    {
                                        string __whereICTransAllocate = _g.d.ic_trans_detail_jobs._doc_no + "=\'" + this._oldDocNo + "\' and (" + _g.d.ic_trans_detail_jobs._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนออก).ToString()
                                        + " or " + _g.d.ic_trans_detail_jobs._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ")";
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail_jobs._table + " where " + __whereICTransAllocate));
                                    }

                                    // ลบ Trans Detail Allocate
                                    if (_g.g._companyProfile._use_unit == 1)
                                    {
                                        string __whereICTransAllocate = _g.d.ic_trans_detail_site._doc_no + "=\'" + this._oldDocNo + "\' and (" + _g.d.ic_trans_detail_site._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนออก).ToString()
                                        + " or " + _g.d.ic_trans_detail_site._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ")";
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail_site._table + " where " + __whereICTransAllocate));
                                    }

                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ap_ar_trans_detail._table + " where " + _g.d.ap_ar_trans_detail._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.ap_ar_trans_detail._trans_flag + "=" + this._getTransFlag.ToString()));
                                    this._icTransRef._transGrid._updateRowIsChangeAll(true);

                                }
                                // รหัสสินค้าใน Grid เอาไป Process
                                __itemList = _getItemCodeFromGrid(__itemList, this._icTransItemGrid);
                                //
                                _getTransStatus = 0;
                                // ปรับสถานะ -1=ด้านลบออก
                                string __fieldList1 = __fieldTransDetailList + _g.d.ic_trans_detail._branch_code + "," + _g.d.ic_trans_detail._status + "," + _g.d.ic_trans_detail._last_status + "," + _g.d.ic_trans_detail._auto_create + "," + _g.d.ic_trans_detail._calc_flag + ",";
                                string __dataList1 = __dataTransDetailList + "\'" + MyLib._myGlobal._branchCode + "\'," + this._getTransStatus + ",0,0," + _g.g._transCalcTypeGlobal._transStockCalcType(_g.g._transControlTypeEnum.สินค้า_โอนออก) + ",";
                                for (int __guidLoop = 0; __guidLoop < this._icTransItemGrid._rowData.Count; __guidLoop++)
                                {
                                    this._icTransItemGrid._cellUpdate(__guidLoop, _g.d.ic_trans_detail._ref_guid, Guid.NewGuid().ToString("N"), false);
                                }
                                __myQuery.Append(this._icTransItemGrid._createQueryForInsert(this._icTransDetailTable, __fieldList1, __dataList1, false, true));
                                // สลับช่องโอนออก กับ โอนเข้า
                                for (int __row2 = 0; __row2 < this._icTransItemGrid._rowData.Count; __row2++)
                                {
                                    string __getWh1 = this._icTransItemGrid._cellGet(__row2, _g.d.ic_trans_detail._wh_code).ToString();
                                    string __getWh2 = this._icTransItemGrid._cellGet(__row2, _g.d.ic_trans_detail._wh_code_2).ToString();
                                    string __getShelf1 = this._icTransItemGrid._cellGet(__row2, _g.d.ic_trans_detail._shelf_code).ToString();
                                    string __getShelf2 = this._icTransItemGrid._cellGet(__row2, _g.d.ic_trans_detail._shelf_code_2).ToString();
                                    this._icTransItemGrid._cellUpdate(__row2, _g.d.ic_trans_detail._wh_code, __getWh2, false);
                                    this._icTransItemGrid._cellUpdate(__row2, _g.d.ic_trans_detail._wh_code_2, __getWh1, false);
                                    this._icTransItemGrid._cellUpdate(__row2, _g.d.ic_trans_detail._shelf_code, __getShelf2, false);
                                    this._icTransItemGrid._cellUpdate(__row2, _g.d.ic_trans_detail._shelf_code_2, __getShelf1, false);
                                }
                                // ปรับสถานะ 1=เป็นด้านบวก
                                string __fieldList3 = __fieldTransDetailList + _g.d.ic_trans_detail._branch_code + "," + _g.d.ic_trans_detail._status + "," + _g.d.ic_trans_detail._last_status + "," + _g.d.ic_trans_detail._auto_create + "," + _g.d.ic_trans_detail._calc_flag + ",";
                                __dataTransDetailList = "\'\'," + _getTransType + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + "," + this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_date) + ",\'" + this.__newTime + "\'," + this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_date) + ",\'" + this.__newTime + "\'," + this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_no) + ",0," + _inquiryType.ToString() + ",";
                                string __dataList3 = __dataTransDetailList + "\'" + MyLib._myGlobal._branchCode + "\'," + this._getTransStatus + ",0,0," + _g.g._transCalcTypeGlobal._transStockCalcType(_g.g._transControlTypeEnum.สินค้า_โอนเข้า) + ",";
                                __myQuery.Append(this._icTransItemGrid._createQueryForInsert(this._icTransDetailTable, __fieldList3, __dataList3, false, true));
                                // สลับช่องโอนออก กับ โอนเข้า (กลับให้เหมือนเดิม)
                                for (int __row2 = 0; __row2 < this._icTransItemGrid._rowData.Count; __row2++)
                                {
                                    string __getWh1 = this._icTransItemGrid._cellGet(__row2, _g.d.ic_trans_detail._wh_code).ToString();
                                    string __getWh2 = this._icTransItemGrid._cellGet(__row2, _g.d.ic_trans_detail._wh_code_2).ToString();
                                    string __getShelf1 = this._icTransItemGrid._cellGet(__row2, _g.d.ic_trans_detail._shelf_code).ToString();
                                    string __getShelf2 = this._icTransItemGrid._cellGet(__row2, _g.d.ic_trans_detail._shelf_code_2).ToString();
                                    this._icTransItemGrid._cellUpdate(__row2, _g.d.ic_trans_detail._wh_code, __getWh2, false);
                                    this._icTransItemGrid._cellUpdate(__row2, _g.d.ic_trans_detail._wh_code_2, __getWh1, false);
                                    this._icTransItemGrid._cellUpdate(__row2, _g.d.ic_trans_detail._shelf_code, __getShelf2, false);
                                    this._icTransItemGrid._cellUpdate(__row2, _g.d.ic_trans_detail._shelf_code_2, __getShelf1, false);
                                }

                                string __fieldList2 = "";
                                string __dataList2 = "";
                                __fieldList2 = _g.d.ap_ar_trans_detail._calc_flag + "," + _g.d.ap_ar_trans_detail._trans_type + "," + _g.d.ap_ar_trans_detail._trans_flag + "," + _g.d.ap_ar_trans_detail._doc_date + "," + _g.d.ap_ar_trans_detail._doc_no + ",";
                                __dataList2 = _g.g._transCalcTypeGlobal._apArTransCalcType(this._transControlType).ToString() + "," + this._getTransType.ToString() + "," + this._getTransFlag.ToString() + "," + this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_date) + "," + this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_no) + ",";

                                __myQuery.Append(this._icTransRef._transGrid._createQueryForInsert(_g.d.ap_ar_trans_detail._table, __fieldList2, __dataList2, false, true));
                                // เอกสารอ้างอิงเอาไปประมวลผลด้วย
                                for (int __loop2 = 0; __loop2 < this._icTransRef._transGrid._rowData.Count; __loop2++)
                                {
                                    string __getDocNo = this._icTransRef._transGrid._cellGet(__loop2, _g.d.ap_ar_trans_detail._billing_no).ToString().Trim();
                                    if (__getDocNo.Length > 0)
                                    {
                                        __docNoList = this._docNoAdd(__docNoList, __getDocNo);
                                    }
                                }
                            }
                            break;
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร:
                            {
                                if (this._myManageTrans._mode == 2)
                                {
                                    string __where1 = _g.d.ic_trans_detail._doc_no + "=\'" + this._oldDocNo + "\' and (" + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร).ToString()
                                        + " or " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินเข้าธนาคาร).ToString() + ")";

                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + this._icTransDetailTable + " where " + __where1));

                                    // ลบ Trans Detail Department
                                    if (_g.g._companyProfile._use_department == 1)
                                    {
                                        string __whereICTransDepartment = _g.d.ic_trans_detail_department._doc_no + "=\'" + this._oldDocNo + "\' and (" + _g.d.ic_trans_detail_department._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนออก).ToString()
                                        + " or " + _g.d.ic_trans_detail_department._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ")";
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail_department._table + " where " + __whereICTransDepartment));
                                    }

                                    // ลบ Trans Detail Project
                                    if (_g.g._companyProfile._use_department == 1)
                                    {
                                        string __whereICTransProject = _g.d.ic_trans_detail_project._doc_no + "=\'" + this._oldDocNo + "\' and (" + _g.d.ic_trans_detail_project._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนออก).ToString()
                                        + " or " + _g.d.ic_trans_detail_department._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ")";
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail_project._table + " where " + __whereICTransProject));
                                    }

                                    // ลบ Trans Detail Allocate
                                    if (_g.g._companyProfile._use_department == 1)
                                    {
                                        string __whereICTransAllocate = _g.d.ic_trans_detail_allocate._doc_no + "=\'" + this._oldDocNo + "\' and (" + _g.d.ic_trans_detail_allocate._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนออก).ToString()
                                        + " or " + _g.d.ic_trans_detail_department._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ")";
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail_allocate._table + " where " + __whereICTransAllocate));
                                    }


                                }
                                _getTransStatus = 0;

                                string __fieldList1 = __fieldTransDetailList + _g.d.ic_trans_detail._branch_code + "," + _g.d.ic_trans_detail._status + "," + _g.d.ic_trans_detail._last_status + "," + _g.d.ic_trans_detail._auto_create + "," + _g.d.ic_trans_detail._calc_flag + ",";
                                string __dataList1 = __dataTransDetailList + "\'" + MyLib._myGlobal._branchCode + "\'," + this._getTransStatus + ",0,0," + _g.g._transCalcTypeGlobal._transStockCalcType(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร) + ",";
                                __myQuery.Append(this._icTransItemGrid._createQueryForInsert(this._icTransDetailTable, __fieldList1, __dataList1, false, true));

                                // สลับช่องโอนออก กับ โอนเข้า
                                for (int __row2 = 0; __row2 < this._icTransItemGrid._rowData.Count; __row2++)
                                {
                                    string __getBook1 = this._icTransItemGrid._cellGet(__row2, _g.d.ic_trans_detail._item_code).ToString();
                                    string __getBook2 = this._icTransItemGrid._cellGet(__row2, _g.d.ic_trans_detail._item_code_2).ToString();
                                    string __getBank1 = this._icTransItemGrid._cellGet(__row2, _g.d.ic_trans_detail._bank_name).ToString();
                                    string __getBank2 = this._icTransItemGrid._cellGet(__row2, _g.d.ic_trans_detail._bank_name_2).ToString();
                                    string __getBranch1 = this._icTransItemGrid._cellGet(__row2, _g.d.ic_trans_detail._bank_branch).ToString();
                                    string __getBranch2 = this._icTransItemGrid._cellGet(__row2, _g.d.ic_trans_detail._bank_branch_2).ToString();

                                    this._icTransItemGrid._cellUpdate(__row2, _g.d.ic_trans_detail._item_code, __getBook2, false);
                                    this._icTransItemGrid._cellUpdate(__row2, _g.d.ic_trans_detail._item_code_2, __getBook1, false);
                                    this._icTransItemGrid._cellUpdate(__row2, _g.d.ic_trans_detail._bank_name, __getBank2, false);
                                    this._icTransItemGrid._cellUpdate(__row2, _g.d.ic_trans_detail._bank_name_2, __getBank1, false);
                                    this._icTransItemGrid._cellUpdate(__row2, _g.d.ic_trans_detail._bank_branch, __getBranch2, false);
                                    this._icTransItemGrid._cellUpdate(__row2, _g.d.ic_trans_detail._bank_branch_2, __getBranch1, false);

                                }

                                string __fieldList3 = __fieldTransDetailList + _g.d.ic_trans_detail._branch_code + "," + _g.d.ic_trans_detail._status + "," + _g.d.ic_trans_detail._last_status + "," + _g.d.ic_trans_detail._auto_create + "," + _g.d.ic_trans_detail._calc_flag + ",";
                                __dataTransDetailList = "\'\'," + _getTransType + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินเข้าธนาคาร).ToString() + "," + this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_date) + ",\'" + this.__newTime + "\'," + this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_date) + ",\'" + this.__newTime + "\'," + this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_no) + ",0," + _inquiryType.ToString() + ",";
                                string __dataList3 = __dataTransDetailList + "\'" + MyLib._myGlobal._branchCode + "\'," + this._getTransStatus + ",0,0," + _g.g._transCalcTypeGlobal._transStockCalcType(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินเข้าธนาคาร) + ",";
                                __myQuery.Append(this._icTransItemGrid._createQueryForInsert(this._icTransDetailTable, __fieldList3, __dataList3, false, true));

                                // สลับช่องกลับ
                                for (int __row2 = 0; __row2 < this._icTransItemGrid._rowData.Count; __row2++)
                                {
                                    string __getBook1 = this._icTransItemGrid._cellGet(__row2, _g.d.ic_trans_detail._item_code).ToString();
                                    string __getBook2 = this._icTransItemGrid._cellGet(__row2, _g.d.ic_trans_detail._item_code_2).ToString();
                                    string __getBank1 = this._icTransItemGrid._cellGet(__row2, _g.d.ic_trans_detail._bank_name).ToString();
                                    string __getBank2 = this._icTransItemGrid._cellGet(__row2, _g.d.ic_trans_detail._bank_name_2).ToString();
                                    string __getBranch1 = this._icTransItemGrid._cellGet(__row2, _g.d.ic_trans_detail._bank_branch).ToString();
                                    string __getBranch2 = this._icTransItemGrid._cellGet(__row2, _g.d.ic_trans_detail._bank_branch_2).ToString();

                                    this._icTransItemGrid._cellUpdate(__row2, _g.d.ic_trans_detail._item_code, __getBook2, false);
                                    this._icTransItemGrid._cellUpdate(__row2, _g.d.ic_trans_detail._item_code_2, __getBook1, false);
                                    this._icTransItemGrid._cellUpdate(__row2, _g.d.ic_trans_detail._bank_name, __getBank2, false);
                                    this._icTransItemGrid._cellUpdate(__row2, _g.d.ic_trans_detail._bank_name_2, __getBank1, false);
                                    this._icTransItemGrid._cellUpdate(__row2, _g.d.ic_trans_detail._bank_branch, __getBranch2, false);
                                    this._icTransItemGrid._cellUpdate(__row2, _g.d.ic_trans_detail._bank_branch_2, __getBranch1, false);

                                }
                            }
                            break;
                    }
                    #endregion

                    if (this._myManageTrans._mode == 2)
                    {
                        // ลบของเก่าก่อน
                        if (this._payControl != null)
                        {
                            // ลบการจ่ายเงิน
                            __myQuery.Append(this._payControl._queryDelete(this._oldDocNo));
                        }
                        if (this._payAdvance != null)
                        {
                            // เงินมัดจำ
                            __myQuery.Append(this._payAdvance._queryDelete(this._oldDocNo));
                        }
                        if (this._withHoldingTax != null)
                        {
                            // ลบภาษีหัก ณ. ที่จ่าย
                            __myQuery.Append(this._withHoldingTax._queryDelete(this._oldDocNo, this._transControlType));
                        }
                    }
                    if (this._payControl != null && this._payControl.Enabled == true)
                    {
                        // การจ่ายเงิน
                        __myQuery.Append(this._payControl._queryInsert(__docNo, __docDate, __docTime, this._icTransScreenTop._docFormatCode, ""));
                    }
                    if (this._payAdvance != null)
                    {
                        // เงินมัดจำ
                        __myQuery.Append(this._payAdvance._queryInsert(this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code), __docNo, __docDate, __docTime));
                    }
                    if (this._withHoldingTax != null && this._withHoldingTax.Enabled == true)
                    {
                        // ภาษีหัก ณ. ที่จ่าย
                        __myQuery.Append(this._withHoldingTax._queryInsert(__docNo, __docDate, this._transControlType));
                    }
                    if (this._vatBuy != null)
                    {
                        // ภาษีซื้อ
                        this._vatBuy._deleteRowIfBlank();
                        this._vatBuy._vatGrid._updateRowIsChangeAll(true);
                        this._vatBuy._checkApDetail(this._myManageTrans._mode);
                        string __vatFieldList = _g.d.gl_journal_vat_buy._book_code + "," + _g.d.gl_journal_vat_buy._vat_calc + "," + _g.d.gl_journal_vat_buy._trans_type + "," + _g.d.gl_journal_vat_buy._trans_flag + "," + _g.d.gl_journal_vat_buy._doc_date + "," + _g.d.gl_journal_vat_buy._doc_no + "," + _g.d.gl_journal_vat_buy._ap_code + ",";
                        string __vatDataList = "\'\'," + _g.g._transTypeGlobal._vatBuyType(this._transControlType).ToString() + "," + _getTransType + "," + _getTransFlag + "," + this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_date) + "," + this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_no) + "," + this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._cust_code) + ",";
                        __myQuery.Append(this._vatBuy._vatGrid._createQueryForInsert(_g.d.gl_journal_vat_buy._table, __vatFieldList, __vatDataList, false, true));
                    }
                    if (this._vatSale != null)
                    {
                        // ภาษีขาย
                        this._vatSale._vatGrid._updateRowIsChangeAll(true);
                        this._vatSale._deleteRowIfBlank();
                        if (this._vatSale._manualTaxID == false)
                        {
                            this._vatSale._checkArDetail(this._myManageTrans._mode);
                        }
                        string __vatFieldList = _g.d.gl_journal_vat_sale._book_code + "," + _g.d.gl_journal_vat_sale._vat_calc + "," + _g.d.gl_journal_vat_sale._trans_type + "," + _g.d.gl_journal_vat_sale._trans_flag + "," + _g.d.gl_journal_vat_sale._doc_date + "," + _g.d.gl_journal_vat_sale._doc_no + "," + _g.d.gl_journal_vat_sale._ar_code + ",";
                        string __vatDataList = "\'\'," + _g.g._transTypeGlobal._vatSaleType(this._transControlType).ToString() + "," + _getTransType + "," + _getTransFlag + "," + this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_date) + "," + this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_no) + "," + this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._cust_code) + ",";
                        __myQuery.Append(this._vatSale._vatGrid._createQueryForInsert(_g.d.gl_journal_vat_sale._table, __vatFieldList, __vatDataList, false, true));
                    }
                    // Serial Number
                    for (int __row = 0; __row < this._icTransItemGrid._rowData.Count; __row++)
                    {
                        if ((int)MyLib._myGlobal._decimalPhase(this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._is_serial_number).ToString()) == 1)
                        {
                            string __itemCode = this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString();
                            string __unitCode = (this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._unit_code) == -1) ? "" : this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._unit_code).ToString().Split('~')[0].ToString();
                            string __whCode = (this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._wh_code) == -1) ? "" : this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._wh_code).ToString().Split('~')[0].ToString();
                            string __shelfCode = (this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._shelf_code) == -1) ? "" : this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._shelf_code).ToString().Split('~')[0].ToString();
                            string __whCode_2 = (this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._wh_code_2) == -1) ? "" : this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._wh_code_2).ToString().Split('~')[0].ToString();
                            string __shelfCode_2 = (this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._shelf_code_2) == -1) ? "" : this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._shelf_code_2).ToString().Split('~')[0].ToString();
                            _icTransItemGridControl._serialNumberStruct __serial = (_icTransItemGridControl._serialNumberStruct)this._icTransItemGrid._cellGet(__row, this._icTransItemGrid._columnSerialNumber);
                            string __field = MyLib._myGlobal._fieldAndComma(
                                _g.d.ic_trans_serial_number._doc_no,
                                _g.d.ic_trans_serial_number._doc_date,
                                _g.d.ic_trans_serial_number._doc_time,
                                _g.d.ic_trans_serial_number._line_number,
                                _g.d.ic_trans_serial_number._doc_line_number,
                                _g.d.ic_trans_serial_number._trans_flag,
                                _g.d.ic_trans_serial_number._ic_code,
                                _g.d.ic_trans_serial_number._unit_code,
                                _g.d.ic_trans_serial_number._wh_code,
                                _g.d.ic_trans_serial_number._shelf_code,
                                _g.d.ic_trans_serial_number._wh_code_2,
                                _g.d.ic_trans_serial_number._shelf_code_2,
                                _g.d.ic_trans_serial_number._calc_flag,
                                _g.d.ic_trans_serial_number._description,
                                _g.d.ic_trans_serial_number._serial_number,
                                _g.d.ic_trans_serial_number._price,
                                _g.d.ic_trans_serial_number._inquiry_type,
                                _g.d.ic_trans_serial_number._void_date);

                            // toe
                            if (__serial != null)
                            {
                                for (int __loop = 0; __loop < __serial.__details.Count; __loop++)
                                {
                                    if (this._transControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก)
                                    {
                                        // ทำโอนเข้า auto
                                        {
                                            int __lineNumber = __loop + 1;
                                            StringBuilder __value = new StringBuilder();
                                            __value.Append("\'" + __docNo + "\',");
                                            __value.Append(__docDate + ",");
                                            __value.Append("\'" + __docTime + "\',");
                                            __value.Append(__lineNumber.ToString() + ",");
                                            __value.Append(__row.ToString().ToString() + ",");
                                            __value.Append(this._getTransFlag.ToString() + ",");
                                            __value.Append("\'" + __itemCode + "\',");
                                            __value.Append("\'" + __unitCode + "\',");
                                            __value.Append("\'" + __whCode + "\',");
                                            __value.Append("\'" + __shelfCode + "\',");
                                            __value.Append("\'" + __whCode_2 + "\',");
                                            __value.Append("\'" + __shelfCode_2 + "\',");
                                            __value.Append(_g.g._transCalcTypeGlobal._transStockCalcType(this._transControlType) + ",");
                                            __value.Append("\'" + __serial.__details[__loop]._description + "\',");
                                            __value.Append("\'" + __serial.__details[__loop]._serialNumber + "\',");
                                            __value.Append(__serial.__details[__loop]._price.ToString() + ",");
                                            __value.Append(_inquiryType.ToString() + ",");
                                            __value.Append("\'" + MyLib._myGlobal._convertDateToQuery(__serial.__details[__loop]._voidDate) + "\'");
                                            string __query = "insert into " + _g.d.ic_trans_serial_number._table + " (" + __field + ") values (" + __value.ToString() + ")";
                                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));
                                        }
                                        {
                                            // line_number=-1 ไว้ แล้วไม่ต้อง load
                                            StringBuilder __value = new StringBuilder();
                                            __value.Append("\'" + __docNo + "\',");
                                            __value.Append(__docDate + ",");
                                            __value.Append("\'" + this.__newTime + "\',");
                                            __value.Append("-1,");
                                            __value.Append(__row.ToString().ToString() + ",");
                                            __value.Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ",");
                                            __value.Append("\'" + __itemCode + "\',");
                                            __value.Append("\'" + __unitCode + "\',");
                                            __value.Append("\'" + __whCode_2 + "\',");
                                            __value.Append("\'" + __shelfCode_2 + "\',");
                                            __value.Append("\'" + __whCode + "\',");
                                            __value.Append("\'" + __shelfCode + "\',");
                                            __value.Append("1,");
                                            __value.Append("\'" + __serial.__details[__loop]._description + "\',");
                                            __value.Append("\'" + __serial.__details[__loop]._serialNumber + "\',");
                                            __value.Append(__serial.__details[__loop]._price.ToString() + ",");
                                            __value.Append(_inquiryType.ToString() + ",");
                                            __value.Append("\'" + MyLib._myGlobal._convertDateToQuery(__serial.__details[__loop]._voidDate) + "\'");
                                            string __query = "insert into " + _g.d.ic_trans_serial_number._table + " (" + __field + ") values (" + __value.ToString() + ")";
                                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));
                                        }
                                    }
                                    else
                                    {
                                        int __lineNumber = __loop + 1;
                                        StringBuilder __value = new StringBuilder();
                                        __value.Append("\'" + __docNo + "\',");
                                        __value.Append(__docDate + ",");
                                        __value.Append("\'" + __docTime + "\',");
                                        __value.Append(__lineNumber.ToString() + ",");
                                        __value.Append(__row.ToString().ToString() + ",");
                                        __value.Append(this._getTransFlag.ToString() + ",");
                                        __value.Append("\'" + __itemCode + "\',");
                                        __value.Append("\'" + __unitCode + "\',");
                                        __value.Append("\'" + __whCode + "\',");
                                        __value.Append("\'" + __shelfCode + "\',");
                                        __value.Append("\'" + __whCode_2 + "\',");
                                        __value.Append("\'" + __shelfCode_2 + "\',");
                                        __value.Append(_g.g._transCalcTypeGlobal._transStockCalcType(this._transControlType) + ",");
                                        __value.Append("\'" + __serial.__details[__loop]._description + "\',");
                                        __value.Append("\'" + __serial.__details[__loop]._serialNumber + "\',");
                                        __value.Append(__serial.__details[__loop]._price.ToString() + ",");
                                        __value.Append(_inquiryType.ToString() + ",");
                                        __value.Append("\'" + MyLib._myGlobal._convertDateToQuery(__serial.__details[__loop]._voidDate) + "\'");
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans_serial_number._table + " (" + __field + ") values (" + __value.ToString() + ")"));
                                    }
                                }
                            }
                        }
                    }
                    // เช็ครับยกมา,เช็คจ่ายยกมา ให้เอาไปใส่ cb ด้วย เพื่อประมวลผลเช็ค
                    switch (this._transControlType)
                    {
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา:
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.cb_trans_detail._table + " where " + _g.d.cb_trans_detail._doc_no + "=\'" + __docNo + "\'"));
                            int __lineNumber = 0;
                            for (int __row = 0; __row < this._icTransItemGrid._rowData.Count; __row++)
                            {
                                string __chqNumber = (this._transControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา) ? this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString().Trim() : this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._chq_number).ToString().Trim();
                                if (__chqNumber.Length > 0)
                                {
                                    __lineNumber++;
                                    string __passBookCode = (this._transControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา) ? "" : this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString().Trim();
                                    string __bankCode = this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._bank_name).ToString().Trim();
                                    string __branch = this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._bank_branch).ToString().Trim();
                                    string __dateDue = this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._date_due).ToString().Trim();
                                    string __remark = this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._remark).ToString().Trim();
                                    decimal __amount = MyLib._myGlobal._decimalPhase(this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._sum_amount).ToString().Trim());
                                    string __query = "insert into " + _g.d.cb_trans_detail._table + " (" +
                                        MyLib._myGlobal._fieldAndComma(
                                        _g.d.cb_trans_detail._doc_no,
                                        _g.d.cb_trans_detail._doc_date,
                                        _g.d.cb_trans_detail._doc_time,
                                        _g.d.cb_trans_detail._trans_type,
                                        _g.d.cb_trans_detail._trans_flag,
                                        _g.d.cb_trans_detail._pass_book_code,
                                        _g.d.cb_trans_detail._status,
                                        _g.d.cb_trans_detail._line_number,
                                        _g.d.cb_trans_detail._doc_type,
                                        _g.d.cb_trans_detail._trans_number_type,
                                        _g.d.cb_trans_detail._ap_ar_type,
                                        _g.d.cb_trans_detail._ap_ar_code,
                                        _g.d.cb_trans_detail._trans_number,
                                        _g.d.cb_trans_detail._bank_code,
                                        _g.d.cb_trans_detail._bank_branch,
                                        _g.d.cb_trans_detail._chq_date,
                                        _g.d.cb_trans_detail._chq_due_date,
                                        _g.d.cb_trans_detail._remark,
                                        _g.d.cb_trans_detail._amount) +
                                        ") values (" +
                                        MyLib._myGlobal._fieldAndComma(
                                        "\'" + __docNo + "\'",
                                        __docDate,
                                        "\'" + __docTime + "\'",
                                        this._getTransType.ToString(),
                                        this._getTransFlag.ToString(),
                                        "\'" + __passBookCode + "\'",
                                        "0",
                                        __lineNumber.ToString(),
                                        "2",
                                        (this._transControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา) ? "1" : "2",
                                        (this._transControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา) ? "1" : "2",
                                        "\'" + this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code) + "\'",
                                        "\'" + __chqNumber + "\'",
                                        "\'" + __bankCode + "\'",
                                        "\'" + __branch + "\'",
                                        __docDate,
                                        "\'" + __dateDue + "\'",
                                        "\'" + __remark + "\'",
                                        __amount.ToString()) + ")";
                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));
                                }
                            }
                            break;
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
                            ((MyLib._myGrid._columnType)this._glDetail._glDetailGrid._columnList[__getColumnNumberCredit])._total.ToString() + "," + this._getTransFlag.ToString(); // +"," + __periodNumber.ToString();
                        string __extField = _g.d.gl_journal._debit + "," + _g.d.gl_journal._credit + "," + _g.d.gl_journal._trans_flag;// +"," + _g.d.gl_journal._period_number;
                        // head
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.gl_journal._table + " (" + __getData[0].ToString() + "," + __extField + ") values (" + __getData[1].ToString() + "," + __extData + ")"));
                        // detail
                        string __fieldListGl = _g.d.gl_journal._doc_date + "," + _g.d.gl_journal._book_code + "," + _g.d.gl_journal._doc_no + "," + _g.d.gl_journal._period_number + "," + _g.d.gl_journal._journal_type + "," + _g.d.gl_journal._trans_flag + ",";
                        string __dataListGl = this._glScreenTop._getDataStrQuery(_g.d.gl_journal._doc_date) + "," + this._glScreenTop._getDataStrQuery(_g.d.gl_journal._book_code) + "," + this._glScreenTop._getDataStrQuery(_g.d.gl_journal._doc_no) + "," + __periodNumber.ToString() + "," + this._glScreenTop._getDataStr(_g.d.gl_journal._journal_type) + "," + this._getTransFlag.ToString() + ",";
                        this._glDetail._glDetailGrid._updateRowIsChangeAll(true);
                        __myQuery.Append(this._glDetail._glDetailGrid._createQueryForInsert(_g.d.gl_journal_detail._table, __fieldListGl, __dataListGl));
                        // Gl Extra
                        __myQuery.Append(this._glDetail._saveGlExtraListQuery(this._glDetail._glDetailGrid, __fieldListGl, __dataListGl));

                        if (this._transControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก)
                        {
                            for (int __rowDetail = 0; __rowDetail < this._glDetail._glDetailGrid._rowData.Count; __rowDetail++)
                            {
                                // กลับข้าง  
                                decimal __debitAmount = (decimal)this._glDetail._glDetailGrid._cellGet(__rowDetail, _g.d.gl_journal._debit);
                                decimal __creditAmount = (decimal)this._glDetail._glDetailGrid._cellGet(__rowDetail, _g.d.gl_journal._credit);

                                this._glDetail._glDetailGrid._cellUpdate(__rowDetail, _g.d.gl_journal._debit, __creditAmount, true);
                                this._glDetail._glDetailGrid._cellUpdate(__rowDetail, _g.d.gl_journal._credit, __debitAmount, true);
                            }

                            // head
                            __extData = ((MyLib._myGrid._columnType)this._glDetail._glDetailGrid._columnList[__getColumnNumberDebit])._total.ToString() + "," +
                           ((MyLib._myGrid._columnType)this._glDetail._glDetailGrid._columnList[__getColumnNumberCredit])._total.ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า);
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.gl_journal._table + " (" + __getData[0].ToString() + "," + __extField + ") values (" + __getData[1].ToString() + "," + __extData + ")"));
                            // detail
                            __fieldListGl = _g.d.gl_journal._doc_date + "," + _g.d.gl_journal._book_code + "," + _g.d.gl_journal._doc_no + "," + _g.d.gl_journal._period_number + "," + _g.d.gl_journal._journal_type + "," + _g.d.gl_journal._trans_flag + ",";
                            __dataListGl = this._glScreenTop._getDataStrQuery(_g.d.gl_journal._doc_date) + "," + this._glScreenTop._getDataStrQuery(_g.d.gl_journal._book_code) + "," + this._glScreenTop._getDataStrQuery(_g.d.gl_journal._doc_no) + "," + __periodNumber.ToString() + "," + this._glScreenTop._getDataStr(_g.d.gl_journal._journal_type) + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า) + ",";
                            this._glDetail._glDetailGrid._updateRowIsChangeAll(true);
                            __myQuery.Append(this._glDetail._glDetailGrid._createQueryForInsert(_g.d.gl_journal_detail._table, __fieldListGl, __dataListGl));
                            // Gl Extra
                            __myQuery.Append(this._glDetail._saveGlExtraListQuery(this._glDetail._glDetailGrid, __fieldListGl, __dataListGl));
                        }
                    }
                    // update ค่าอื่นๆ เผื่อหลุด
                    __myQuery.Append(_g.g._queryUpdateTrans());
                    //

                    // โต๋ กรณี ใบกำกับภาษีอย่างเต็ม จาก POS
                    if (this._fullInvoiceFromPOS == true)
                    {
                        // เพิ่ม query ยกเลิก รายการขาย
                    }

                    // เฉลี่ยแผนก
                    #region share department, project, job, allocate, side
                    for (int __row = 0; __row < this._icTransItemGrid._rowData.Count; __row++)
                    {
                        _icTransItemGridControl.icTransWeightStruct __detail = (_icTransItemGridControl.icTransWeightStruct)this._icTransItemGrid._cellGet(__row, this._icTransItemGrid._columnDepartment);

                        string __itemCode = this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString();

                        string __field = MyLib._myGlobal._fieldAndComma(
                                _g.d.ic_trans_detail_department._doc_no,
                                _g.d.ic_trans_detail_department._doc_date,
                                _g.d.ic_trans_detail_department._line_number,
                                _g.d.ic_trans_detail_department._doc_line_number,
                                _g.d.ic_trans_detail_department._trans_flag,
                                _g.d.ic_trans_detail_department._ic_code,
                                _g.d.ic_trans_detail_department._department_code,
                                _g.d.ic_trans_detail_department._ratio,
                                _g.d.ic_trans_detail_department._amount);

                        // toe
                        if (__detail != null)
                        {
                            for (int __loop = 0; __loop < __detail.__details.Count; __loop++)
                            {
                                if (this._transControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก)
                                {
                                    // ทำโอนเข้า auto
                                    {
                                        int __lineNumber = __loop + 1;
                                        StringBuilder __value = new StringBuilder();
                                        __value.Append("\'" + __docNo + "\',");
                                        __value.Append(__docDate + ",");
                                        __value.Append(__lineNumber.ToString() + ",");
                                        __value.Append(__row.ToString().ToString() + ",");
                                        __value.Append(this._getTransFlag.ToString() + ",");
                                        __value.Append("\'" + __itemCode + "\',");
                                        __value.Append("\'" + __detail.__details[__loop]._code + "\',");
                                        __value.Append(__detail.__details[__loop]._ratio.ToString() + ",");
                                        __value.Append(__detail.__details[__loop]._amount.ToString());
                                        string __query = "insert into " + _g.d.ic_trans_detail_department._table + " (" + __field + ") values (" + __value.ToString() + ")";
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));
                                    }
                                    {
                                        // line_number=-1 ไว้ แล้วไม่ต้อง load
                                        StringBuilder __value = new StringBuilder();
                                        __value.Append("\'" + __docNo + "\',");
                                        __value.Append(__docDate + ",");
                                        __value.Append("-1,");
                                        __value.Append(__row.ToString().ToString() + ",");
                                        __value.Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ",");
                                        __value.Append("\'" + __itemCode + "\',");
                                        __value.Append("\'" + __detail.__details[__loop]._code + "\',");
                                        __value.Append(__detail.__details[__loop]._ratio.ToString() + ",");
                                        __value.Append(__detail.__details[__loop]._amount.ToString());
                                        string __query = "insert into " + _g.d.ic_trans_detail_department._table + " (" + __field + ") values (" + __value.ToString() + ")";
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));
                                    }
                                }
                                else
                                {
                                    int __lineNumber = __loop + 1;
                                    StringBuilder __value = new StringBuilder();
                                    __value.Append("\'" + __docNo + "\',");
                                    __value.Append(__docDate + ",");
                                    __value.Append(__lineNumber.ToString() + ",");
                                    __value.Append(__row.ToString().ToString() + ",");
                                    __value.Append(this._getTransFlag.ToString() + ",");
                                    __value.Append("\'" + __itemCode + "\',");
                                    __value.Append("\'" + __detail.__details[__loop]._code + "\',");
                                    __value.Append(__detail.__details[__loop]._ratio.ToString() + ",");
                                    __value.Append(__detail.__details[__loop]._amount.ToString());
                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans_detail_department._table + " (" + __field + ") values (" + __value.ToString() + ")"));
                                }
                            }
                        }
                    }

                    // โครงการ
                    for (int __row = 0; __row < this._icTransItemGrid._rowData.Count; __row++)
                    {
                        _icTransItemGridControl.icTransWeightStruct __detail = (_icTransItemGridControl.icTransWeightStruct)this._icTransItemGrid._cellGet(__row, this._icTransItemGrid._columnProject);

                        string __itemCode = this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString();

                        string __field = MyLib._myGlobal._fieldAndComma(
                                _g.d.ic_trans_detail_project._doc_no,
                                _g.d.ic_trans_detail_project._doc_date,
                                _g.d.ic_trans_detail_project._line_number,
                                _g.d.ic_trans_detail_project._doc_line_number,
                                _g.d.ic_trans_detail_project._trans_flag,
                                _g.d.ic_trans_detail_project._ic_code,
                                _g.d.ic_trans_detail_project._project_code,
                                _g.d.ic_trans_detail_project._ratio,
                                _g.d.ic_trans_detail_project._amount);

                        // toe
                        if (__detail != null)
                        {
                            for (int __loop = 0; __loop < __detail.__details.Count; __loop++)
                            {
                                if (this._transControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก)
                                {
                                    // ทำโอนเข้า auto
                                    {
                                        int __lineNumber = __loop + 1;
                                        StringBuilder __value = new StringBuilder();
                                        __value.Append("\'" + __docNo + "\',");
                                        __value.Append(__docDate + ",");
                                        __value.Append(__lineNumber.ToString() + ",");
                                        __value.Append(__row.ToString().ToString() + ",");
                                        __value.Append(this._getTransFlag.ToString() + ",");
                                        __value.Append("\'" + __itemCode + "\',");
                                        __value.Append("\'" + __detail.__details[__loop]._code + "\',");
                                        __value.Append(__detail.__details[__loop]._ratio.ToString() + ",");
                                        __value.Append(__detail.__details[__loop]._amount.ToString());
                                        string __query = "insert into " + _g.d.ic_trans_detail_project._table + " (" + __field + ") values (" + __value.ToString() + ")";
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));
                                    }
                                    {
                                        // line_number=-1 ไว้ แล้วไม่ต้อง load
                                        StringBuilder __value = new StringBuilder();
                                        __value.Append("\'" + __docNo + "\',");
                                        __value.Append(__docDate + ",");
                                        __value.Append("-1,");
                                        __value.Append(__row.ToString().ToString() + ",");
                                        __value.Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ",");
                                        __value.Append("\'" + __itemCode + "\',");
                                        __value.Append("\'" + __detail.__details[__loop]._code + "\',");
                                        __value.Append(__detail.__details[__loop]._ratio.ToString() + ",");
                                        __value.Append(__detail.__details[__loop]._amount.ToString());
                                        string __query = "insert into " + _g.d.ic_trans_detail_project._table + " (" + __field + ") values (" + __value.ToString() + ")";
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));
                                    }
                                }
                                else
                                {
                                    int __lineNumber = __loop + 1;
                                    StringBuilder __value = new StringBuilder();
                                    __value.Append("\'" + __docNo + "\',");
                                    __value.Append(__docDate + ",");
                                    __value.Append(__lineNumber.ToString() + ",");
                                    __value.Append(__row.ToString().ToString() + ",");
                                    __value.Append(this._getTransFlag.ToString() + ",");
                                    __value.Append("\'" + __itemCode + "\',");
                                    __value.Append("\'" + __detail.__details[__loop]._code + "\',");
                                    __value.Append(__detail.__details[__loop]._ratio.ToString() + ",");
                                    __value.Append(__detail.__details[__loop]._amount.ToString());
                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans_detail_project._table + " (" + __field + ") values (" + __value.ToString() + ")"));
                                }
                            }
                        }
                    }

                    // การจัดสรร
                    for (int __row = 0; __row < this._icTransItemGrid._rowData.Count; __row++)
                    {
                        _icTransItemGridControl.icTransWeightStruct __detail = (_icTransItemGridControl.icTransWeightStruct)this._icTransItemGrid._cellGet(__row, this._icTransItemGrid._columnAlloCate);

                        string __itemCode = this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString();

                        string __field = MyLib._myGlobal._fieldAndComma(
                                _g.d.ic_trans_detail_allocate._doc_no,
                                _g.d.ic_trans_detail_allocate._doc_date,
                                _g.d.ic_trans_detail_allocate._line_number,
                                _g.d.ic_trans_detail_allocate._doc_line_number,
                                _g.d.ic_trans_detail_allocate._trans_flag,
                                _g.d.ic_trans_detail_allocate._ic_code,
                                _g.d.ic_trans_detail_allocate._allocate_code,
                                _g.d.ic_trans_detail_allocate._ratio,
                                _g.d.ic_trans_detail_allocate._amount);

                        // toe
                        if (__detail != null)
                        {
                            for (int __loop = 0; __loop < __detail.__details.Count; __loop++)
                            {
                                if (this._transControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก)
                                {
                                    // ทำโอนเข้า auto
                                    {
                                        int __lineNumber = __loop + 1;
                                        StringBuilder __value = new StringBuilder();
                                        __value.Append("\'" + __docNo + "\',");
                                        __value.Append(__docDate + ",");
                                        __value.Append(__lineNumber.ToString() + ",");
                                        __value.Append(__row.ToString().ToString() + ",");
                                        __value.Append(this._getTransFlag.ToString() + ",");
                                        __value.Append("\'" + __itemCode + "\',");
                                        __value.Append("\'" + __detail.__details[__loop]._code + "\',");
                                        __value.Append(__detail.__details[__loop]._ratio.ToString() + ",");
                                        __value.Append(__detail.__details[__loop]._amount.ToString());
                                        string __query = "insert into " + _g.d.ic_trans_detail_allocate._table + " (" + __field + ") values (" + __value.ToString() + ")";
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));
                                    }
                                    {
                                        // line_number=-1 ไว้ แล้วไม่ต้อง load
                                        StringBuilder __value = new StringBuilder();
                                        __value.Append("\'" + __docNo + "\',");
                                        __value.Append(__docDate + ",");
                                        __value.Append("-1,");
                                        __value.Append(__row.ToString().ToString() + ",");
                                        __value.Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ",");
                                        __value.Append("\'" + __itemCode + "\',");
                                        __value.Append("\'" + __detail.__details[__loop]._code + "\',");
                                        __value.Append(__detail.__details[__loop]._ratio.ToString() + ",");
                                        __value.Append(__detail.__details[__loop]._amount.ToString());
                                        string __query = "insert into " + _g.d.ic_trans_detail_allocate._table + " (" + __field + ") values (" + __value.ToString() + ")";
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));
                                    }
                                }
                                else
                                {
                                    int __lineNumber = __loop + 1;
                                    StringBuilder __value = new StringBuilder();
                                    __value.Append("\'" + __docNo + "\',");
                                    __value.Append(__docDate + ",");
                                    __value.Append(__lineNumber.ToString() + ",");
                                    __value.Append(__row.ToString().ToString() + ",");
                                    __value.Append(this._getTransFlag.ToString() + ",");
                                    __value.Append("\'" + __itemCode + "\',");
                                    __value.Append("\'" + __detail.__details[__loop]._code + "\',");
                                    __value.Append(__detail.__details[__loop]._ratio.ToString() + ",");
                                    __value.Append(__detail.__details[__loop]._amount.ToString());
                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans_detail_allocate._table + " (" + __field + ") values (" + __value.ToString() + ")"));
                                }
                            }
                        }
                    }

                    // หน่วยงาน
                    for (int __row = 0; __row < this._icTransItemGrid._rowData.Count; __row++)
                    {
                        _icTransItemGridControl.icTransWeightStruct __detail = (_icTransItemGridControl.icTransWeightStruct)this._icTransItemGrid._cellGet(__row, this._icTransItemGrid._columnSideList);

                        string __itemCode = this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString();

                        string __field = MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail_site._doc_no, _g.d.ic_trans_detail_site._doc_date, _g.d.ic_trans_detail_site._line_number, _g.d.ic_trans_detail_site._doc_line_number, _g.d.ic_trans_detail_site._trans_flag, _g.d.ic_trans_detail_site._ic_code, _g.d.ic_trans_detail_site._site_code, _g.d.ic_trans_detail_site._ratio, _g.d.ic_trans_detail_site._amount);

                        // toe
                        if (__detail != null)
                        {
                            for (int __loop = 0; __loop < __detail.__details.Count; __loop++)
                            {
                                if (this._transControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก)
                                {
                                    // ทำโอนเข้า auto
                                    {
                                        int __lineNumber = __loop + 1;
                                        StringBuilder __value = new StringBuilder();
                                        __value.Append("\'" + __docNo + "\',");
                                        __value.Append(__docDate + ",");
                                        __value.Append(__lineNumber.ToString() + ",");
                                        __value.Append(__row.ToString().ToString() + ",");
                                        __value.Append(this._getTransFlag.ToString() + ",");
                                        __value.Append("\'" + __itemCode + "\',");
                                        __value.Append("\'" + __detail.__details[__loop]._code + "\',");
                                        __value.Append(__detail.__details[__loop]._ratio.ToString() + ",");
                                        __value.Append(__detail.__details[__loop]._amount.ToString());
                                        string __query = "insert into " + _g.d.ic_trans_detail_site._table + " (" + __field + ") values (" + __value.ToString() + ")";
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));
                                    }
                                    {
                                        // line_number=-1 ไว้ แล้วไม่ต้อง load
                                        StringBuilder __value = new StringBuilder();
                                        __value.Append("\'" + __docNo + "\',");
                                        __value.Append(__docDate + ",");
                                        __value.Append("-1,");
                                        __value.Append(__row.ToString().ToString() + ",");
                                        __value.Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ",");
                                        __value.Append("\'" + __itemCode + "\',");
                                        __value.Append("\'" + __detail.__details[__loop]._code + "\',");
                                        __value.Append(__detail.__details[__loop]._ratio.ToString() + ",");
                                        __value.Append(__detail.__details[__loop]._amount.ToString());
                                        string __query = "insert into " + _g.d.ic_trans_detail_site._table + " (" + __field + ") values (" + __value.ToString() + ")";
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));
                                    }
                                }
                                else
                                {
                                    int __lineNumber = __loop + 1;
                                    StringBuilder __value = new StringBuilder();
                                    __value.Append("\'" + __docNo + "\',");
                                    __value.Append(__docDate + ",");
                                    __value.Append(__lineNumber.ToString() + ",");
                                    __value.Append(__row.ToString().ToString() + ",");
                                    __value.Append(this._getTransFlag.ToString() + ",");
                                    __value.Append("\'" + __itemCode + "\',");
                                    __value.Append("\'" + __detail.__details[__loop]._code + "\',");
                                    __value.Append(__detail.__details[__loop]._ratio.ToString() + ",");
                                    __value.Append(__detail.__details[__loop]._amount.ToString());
                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans_detail_site._table + " (" + __field + ") values (" + __value.ToString() + ")"));
                                }
                            }
                        }
                    }

                    // งาน
                    for (int __row = 0; __row < this._icTransItemGrid._rowData.Count; __row++)
                    {
                        _icTransItemGridControl.icTransWeightStruct __detail = (_icTransItemGridControl.icTransWeightStruct)this._icTransItemGrid._cellGet(__row, this._icTransItemGrid._columnJobsList);

                        string __itemCode = this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString();

                        string __field = MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail_jobs._doc_no, _g.d.ic_trans_detail_jobs._doc_date, _g.d.ic_trans_detail_jobs._line_number, _g.d.ic_trans_detail_jobs._doc_line_number, _g.d.ic_trans_detail_jobs._trans_flag, _g.d.ic_trans_detail_jobs._ic_code, _g.d.ic_trans_detail_jobs._job_code, _g.d.ic_trans_detail_jobs._ratio, _g.d.ic_trans_detail_jobs._amount);

                        // toe
                        if (__detail != null)
                        {
                            for (int __loop = 0; __loop < __detail.__details.Count; __loop++)
                            {
                                if (this._transControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก)
                                {
                                    // ทำโอนเข้า auto
                                    {
                                        int __lineNumber = __loop + 1;
                                        StringBuilder __value = new StringBuilder();
                                        __value.Append("\'" + __docNo + "\',");
                                        __value.Append(__docDate + ",");
                                        __value.Append(__lineNumber.ToString() + ",");
                                        __value.Append(__row.ToString().ToString() + ",");
                                        __value.Append(this._getTransFlag.ToString() + ",");
                                        __value.Append("\'" + __itemCode + "\',");
                                        __value.Append("\'" + __detail.__details[__loop]._code + "\',");
                                        __value.Append(__detail.__details[__loop]._ratio.ToString() + ",");
                                        __value.Append(__detail.__details[__loop]._amount.ToString());
                                        string __query = "insert into " + _g.d.ic_trans_detail_jobs._table + " (" + __field + ") values (" + __value.ToString() + ")";
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));
                                    }
                                    {
                                        // line_number=-1 ไว้ แล้วไม่ต้อง load
                                        StringBuilder __value = new StringBuilder();
                                        __value.Append("\'" + __docNo + "\',");
                                        __value.Append(__docDate + ",");
                                        __value.Append("-1,");
                                        __value.Append(__row.ToString().ToString() + ",");
                                        __value.Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ",");
                                        __value.Append("\'" + __itemCode + "\',");
                                        __value.Append("\'" + __detail.__details[__loop]._code + "\',");
                                        __value.Append(__detail.__details[__loop]._ratio.ToString() + ",");
                                        __value.Append(__detail.__details[__loop]._amount.ToString());
                                        string __query = "insert into " + _g.d.ic_trans_detail_jobs._table + " (" + __field + ") values (" + __value.ToString() + ")";
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));
                                    }
                                }
                                else
                                {
                                    int __lineNumber = __loop + 1;
                                    StringBuilder __value = new StringBuilder();
                                    __value.Append("\'" + __docNo + "\',");
                                    __value.Append(__docDate + ",");
                                    __value.Append(__lineNumber.ToString() + ",");
                                    __value.Append(__row.ToString().ToString() + ",");
                                    __value.Append(this._getTransFlag.ToString() + ",");
                                    __value.Append("\'" + __itemCode + "\',");
                                    __value.Append("\'" + __detail.__details[__loop]._code + "\',");
                                    __value.Append(__detail.__details[__loop]._ratio.ToString() + ",");
                                    __value.Append(__detail.__details[__loop]._amount.ToString());
                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans_detail_jobs._table + " (" + __field + ") values (" + __value.ToString() + ")"));
                                }
                            }
                        }
                    }

                    #endregion

                    if (this._shipmentControl != null)
                    {
                        string __getReceiveName = this._shipmentControl._shipmentScreen._getDataStr(_g.d.ic_trans_shipment._transport_name);
                        string __getReceiveAddress = this._shipmentControl._shipmentScreen._getDataStr(_g.d.ic_trans_shipment._transport_address);
                        string __getReceiveTelephone = this._shipmentControl._shipmentScreen._getDataStr(_g.d.ic_trans_shipment._transport_telephone);
                        //string __getReceiveFax = ""; this._shipmentControl._shipmentScreen._getDataStr(_g.d.ic_trans_shipment._transport_fax);
                        if (this._myManageTrans._mode == 2)
                        {
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_shipment._table + " where " + _g.d.ic_trans_shipment._doc_no + "=\'" + _oldDocNo + "\' and " + _g.d.ic_trans_shipment._trans_flag + "  =" + _getTransFlag.ToString()));
                        }
                        // save shipm,ent
                        if (__getReceiveName.Length > 0)
                        {
                            ArrayList __getShipmentQuery = this._shipmentControl._shipmentScreen._createQueryForDatabase();

                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans_shipment._table + "(" + _g.d.ic_trans_shipment._doc_no + "," + _g.d.ic_trans_shipment._doc_date + "," + _g.d.ic_trans_shipment._trans_flag + "," + _g.d.ic_trans_shipment._cust_code + "," + __getShipmentQuery[0] + ") values (\'" + __docNo + "\', " + __docDate + ", " + _getTransFlag + ",\'" + this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code) + "\'," + __getShipmentQuery[1] + ") "));
                        }
                    }

                    // toe กรณียกเลิก เก็บเลขที่เอกสารอ้างอิงด้วย
                    switch (this._transControlType)
                    {
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก:
                            {
                                string __getDocNo = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_ref);
                                if (__getDocNo.Length > 0)
                                {
                                    __docNoList = this._docNoAdd(__docNoList, __getDocNo);
                                }
                            }
                            break;
                    }

                    #region ลบรายการขออนุมัติ
                    if (this._requestApproveCode.Length > 0)
                    {
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_draft._table + " where " + _g.d.ic_trans_draft._doc_no + "=\'" + this._requestApproveCode + "\'"));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ap_ar_trans_detail_draft._table + " where " + _g.d.ap_ar_trans_detail_draft._doc_no + "=\'" + this._requestApproveCode + "\'"));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.cb_trans_detail_draft._table + " where " + _g.d.cb_trans_detail_draft._doc_no + "=\'" + this._requestApproveCode + "\'"));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail_draft._table + " where " + _g.d.ic_trans_detail_draft._doc_no + "=\'" + this._requestApproveCode + "\'"));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.erp_request_order._table + " where " + _g.d.erp_request_order._doc_no + "=\'" + this._requestApproveCode + "\'"));

                        // erp_credit_approved_logs
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.erp_credit_approved_logs._table + "(" +
                            _g.d.erp_credit_approved_logs._trans_flag + "," + _g.d.erp_credit_approved_logs._trans_type + "," + _g.d.erp_credit_approved_logs._doc_no + "," + _g.d.erp_credit_approved_logs._doc_date + "," + _g.d.erp_credit_approved_logs._ref_no + "," + _g.d.erp_credit_approved_logs._creator_code + "," + _g.d.erp_credit_approved_logs._create_datetime +
                            ")  values( " +
                            this._getTransFlag.ToString() + "," + this._getTransType.ToString() + "," + this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_no) + "," + this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_date) + "," + "\'" + this._requestApproveCode + "\' " + "," + "\'" + MyLib._myGlobal._userCode + "\' " + "," + "\'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\' " +
                            " )"));

                    }
                    #endregion


                    // รายการขออนุมัติ
                    if (__creditStatusRequestQuery.Length > 0)
                    {
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.erp_credit_request._table + " where doc_no = \'" + this._oldDocNo + "\' and trans_flag = " + this._getTransFlag));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.erp_credit_approved_logs._table + " where doc_no = \'" + this._oldDocNo + "\' and trans_flag = " + this._getTransFlag));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format(__creditStatusRequestQuery, __docNo)));
                    }

                    __myQuery.Append("</node>");
                    string __queryStr = __myQuery.ToString();

                    SMLERPGL._transProcessUserControl __processControl = null;

                    #region GL Auto
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        _g.g._companyProfileLoad();

                        // toe กรณีแก้ไข และ ลง GL Auto


                        if (this._glScreenTop != null && _g.g._companyProfile._gl_process_realtime && __glManual == false)
                        {
                            // จำลอง datatable ที่ query ออกมาได้

                            decimal __service_amount = 0M;
                            int __vat_type = MyLib._myGlobal._intPhase(this._icTransScreenTop._getDataNumber(_g.d.ic_trans._vat_type).ToString());
                            decimal __total_discoutnt = (__vat_type == 1) ? (this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_discount) * 100 / (100 + _g.g._companyProfile._vat_rate)) : this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_discount);
                            decimal __credit_card_amount = 0M;
                            decimal __credit_card_charge = 0M;
                            string __departmentCode = "";
                            string __projectCode = "";
                            string __allocateCode = "";
                            decimal __vatNext = 0M;
                            string __sideCode = "";
                            string __jobCode = "";
                            decimal __total_cost = 0M;

                            string __incheck = this._icTransScreenBottom._getDataStr(_g.d.ic_trans._remark);

                            __processControl = new SMLERPGL._transProcessUserControl();

                            //SMLERPGL._transProcessUserControl._transDataObject __transData = new SMLERPGL._transProcessUserControl._transDataObject("IC", (this._oldDocNo.Length == 0) ? __docNo : this._oldDocNo, this._getTransFlag, this._getTransType, this._icTransScreenTop._docFormatCode)
                            SMLERPGL._transProcessUserControl._transDataObject __transData = new SMLERPGL._transProcessUserControl._transDataObject("IC", __docNo, this._getTransFlag, this._getTransType, this._icTransScreenTop._docFormatCode)
                            {
                                cust_code = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code),
                                cust_name = (this._icTransScreenTop._getControl(_g.d.ic_trans._cust_code) != null) ? ((MyLib._myTextBox)this._icTransScreenTop._getControl(_g.d.ic_trans._cust_code))._textSecond : "",
                                vat_type = __vat_type,
                                tax_doc_no = this._icTransScreenTop._getDataStr(_g.d.ic_trans._tax_doc_no),
                                remark = this._icTransScreenBottom._getDataStr(_g.d.ic_trans._remark),
                                total_amount = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_amount),
                                service_amount = __service_amount,
                                total_value = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_value),
                                total_before_vat = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_before_vat),
                                total_except_vat = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_except_vat),
                                total_vat_value = this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_vat_value),
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
                                doc_ref = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_ref),
                                doc_date = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_date, true).Replace("\'", ""),
                                doc_time = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_time),
                                inquiry_type = MyLib._myGlobal._intPhase(this._icTransScreenTop._getDataNumber(_g.d.ic_trans._inquiry_type).ToString()),
                                advance_amount = this._icTransScreenTop._getDataNumber(_g.d.ic_trans._advance_amount),
                                deposit_amount = (this._payControl != null) ? this._payControl._payDetailScreen._getDataNumber(_g.d.cb_trans._deposit_amount) : 0M,
                                credit_card_amount = __credit_card_amount,
                                credit_card_charge = __credit_card_charge,
                                tax_doc_date = this._icTransScreenTop._getDataStr(_g.d.ic_trans._tax_doc_date, true).Replace("\'", ""),
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

                            // detail

                            for (int __row = 0; __row < this._icTransItemGrid._rowData.Count; __row++)
                            {
                                string __itemCode = this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString();

                                if (__itemCode != "")
                                {
                                    decimal __amount = (this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._sum_amount) != -1) ? (decimal)this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._sum_amount) : 0M;
                                    decimal __sum_amount_exclude_vat = (this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._sum_amount_exclude_vat) != -1) ? (decimal)this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._sum_amount_exclude_vat) : 0M;

                                    SMLERPGL._transProcessUserControl._transDetailDataObject __detailDataGL = new SMLERPGL._transProcessUserControl._transDetailDataObject((this._oldDocNo.Length == 0) ? __docNo : this._oldDocNo, this._getTransFlag, this._getTransType)
                                    {
                                        item_type = (this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._item_type) != -1) ? MyLib._myGlobal._intPhase(this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._item_type).ToString()) : 0,
                                        item_code = __itemCode,
                                        price = (this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._price) != -1) ? (decimal)this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._price) : 0M,
                                        amount = __amount,
                                        sum_amount_exclude_vat = __sum_amount_exclude_vat,
                                        remark = this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._remark) != -1 ? this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._remark).ToString() : "",
                                        line_number = __row,
                                        pass_book_code = __itemCode, //  ((this._transControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน || this._transControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน) ? __itemCode : ""),
                                        sum_amount = (this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._sum_amount) != -1) ? (decimal)this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._sum_amount) : 0M
                                    };

                                    if (this._transControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค)
                                    {
                                        // case when trans_flag in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค) + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก) + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค) + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก) + ") then coalesce((select " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + " trans_flag from ic_trans_detail as x where x.chq_number = ic_trans_detail.item_code and x.doc_date <= ic_trans_detail.doc_date and x.doc_time < ic_trans_detail.doc_time order by doc_date desc, doc_time desc " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + " ), 0) else 0 end
                                        // หา last flag
                                        DataTable __chqStatusTable = __myFrameWork._queryShort("select " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + " trans_flag from ic_trans_detail as x where x.chq_number = \'" + __itemCode + "\'  order by doc_date desc, doc_time desc " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + " ").Tables[0];
                                        if (__chqStatusTable.Rows.Count > 0)
                                        {
                                            __detailDataGL.last_flag = MyLib._myGlobal._intPhase(__chqStatusTable.Rows[0][0].ToString());
                                        }
                                    }


                                    __processControl._addTransDetailData(__detailDataGL, ((__row == 0) ? true : false));

                                }

                                // กลับข้าง
                                if (this._transControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร)
                                {
                                    SMLERPGL._transProcessUserControl._transDetailDataObject __detailDataGL = new SMLERPGL._transProcessUserControl._transDetailDataObject((this._oldDocNo.Length == 0) ? __docNo : this._oldDocNo, _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินเข้าธนาคาร), this._getTransType)
                                    {
                                        item_type = (this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._item_type) != -1) ? MyLib._myGlobal._intPhase(this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._item_type).ToString()) : 0,
                                        item_code = this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._item_code_2).ToString(),
                                        price = (this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._price) != -1) ? (decimal)this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._price) : 0M,
                                        amount = (this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._sum_amount) != -1) ? (decimal)this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._sum_amount) : 0M,
                                        sum_amount_exclude_vat = (this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._sum_amount_exclude_vat) != -1) ? (decimal)this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._sum_amount_exclude_vat) : 0M,
                                        remark = this._icTransItemGrid._findColumnByName(_g.d.ic_trans_detail._remark) != -1 ? this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._remark).ToString() : "",
                                        pass_book_code = __itemCode,
                                        line_number = __row
                                    };


                                    __processControl._addTransDetailData(__detailDataGL, false);
                                }
                            }

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

                                        SMLERPGL._transProcessUserControl._transDetailDataObject __detailDataGL = new SMLERPGL._transProcessUserControl._transDetailDataObject((this._oldDocNo.Length == 0) ? __docNo : this._oldDocNo, this._getTransFlag, this._getTransType)
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

                                        SMLERPGL._transProcessUserControl._transDetailDataObject __detailDataGL = new SMLERPGL._transProcessUserControl._transDetailDataObject((this._oldDocNo.Length == 0) ? __docNo : this._oldDocNo, this._getTransFlag, this._getTransType)
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

                            __processControl._procesGLByTemp(this._icTransScreenTop._docFormatCode);
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
                    #endregion

                    string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __queryStr);
                    //
                    if (__result.Length == 0)
                    {
                        // Process 
                        /*string __itemListForProcess = _g.g._getItemRepack(__itemList);
                        SMLProcess._docFlow __process = new SMLProcess._docFlow();
                        __process._processAll(this._transControlType,__itemListForProcess, __docNoList);
                        //
                        _g.g._updateDateTimeForCalc(this._transControlType, __itemListForProcess);
                        // Process Stock
                        // กรณียกเลิก ให้ไปเอารหัสเก่าไปประมวล (จากเลขที่อ้างอืง)
                        switch (this._transControlType)
                        {
                            case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก:
                            case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                            case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก:
                            case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก:
                            case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก:
                            case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                            case _g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก:
                            case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก:
                            case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก:
                            case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_โอนเข้า_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_โอนออก_ยกเลิก:
                            case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                                __itemList = this._getOldItemCode(__itemList, _g.d.ic_trans_detail._doc_no + "=\'" + this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_ref) + "\'");
                                break;
                        }
                        __resultStr = __smlFrameWork._process_stock_balance(MyLib._myGlobal._databaseName, __itemListForProcess, _g.g._transFlagGlobal._transFlag(this._transControlType).ToString());
                        if (__resultStr.Length > 0)
                        {
                            MessageBox.Show(__resultStr);
                        }*/
                        // กรณียกเลิก ให้ไปเอารหัสเก่าไปประมวล (จากเลขที่อ้างอืง)                        
                        if (MyLib._myGlobal._OEMVersion.Equals("SINGHA") && this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ)
                        {
                            this._lastDocFormatCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_format_code);
                            this._lastSaveDate = this._icTransScreenTop._getDataDate(_g.d.ic_trans._doc_date);
                        }

                        switch (this._transControlType)
                        {
                            case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก:
                            case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                            case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก:
                            case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก:
                            case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก:
                            case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                            case _g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก:
                            case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก:
                            case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก:
                            case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ_ยกเลิก:

                            case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_โอนเข้า_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_โอนออก_ยกเลิก:
                            case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                            // toe
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค_ยกเลิก:

                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค_ยกเลิก:

                                __itemList = this._getOldItemCode(__itemList, _g.d.ic_trans_detail._doc_no + "=\'" + this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_ref) + "\'");
                                break;
                        }
                        /*StringBuilder __test1 = new StringBuilder();
                        for (int __loop = 0; __loop < __itemList.Count; __loop++)
                        {
                            if (__test1.Length > 0)
                            {
                                __test1.Append(",");
                            }
                            __test1.Append("\'" + __itemList[__loop].ToString() + "\'");
                        }*/

                        // โต๋ กรณี ต้องคำณวน cost ก่อนพิมพ์
                        switch (this._transControlType)
                        {
                            case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                            case _g.g._transControlTypeEnum.สินค้า_โอนเข้า:
                            case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                            case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                                {
                                    _processThread __process = new _processThread(this._transControlType, __itemList, __docNoList);
                                    //__process._run();
                                    Thread __thread = new Thread(new ThreadStart(__process._run));
                                    __thread.IsBackground = true;
                                    __thread.Start();

                                    // toe coupon process
                                    if (this._payControl != null && this._payControl.Enabled == true)
                                    {
                                        StringBuilder __couponNumberList = new StringBuilder();
                                        for (int __loop = 0; __loop < this._payControl._payCouponGrid._rowData.Count; __loop++)
                                        {
                                            string __number = this._payControl._payCouponGrid._cellGet(__loop, _g.d.cb_trans_detail._trans_number).ToString().Trim();
                                            if (__number.Length > 0)
                                            {
                                                if (__couponNumberList.Length > 0)
                                                {
                                                    __couponNumberList.Append(",");
                                                }
                                                __couponNumberList.Append(__number);
                                            }
                                        }

                                        if (__couponNumberList.ToString().Length > 0)
                                        {
                                            SMLProcess._posProcess __processPos = new SMLProcess._posProcess();
                                            __processPos._numberList = __couponNumberList.ToString();
                                            Thread __posThread = new Thread(__processPos._processCoupon);
                                            __posThread.Start();
                                        }
                                    }
                                }
                                break;
                        }

                        // change doc picture code
                        if (this._myManageTrans._mode == 2)
                        {
                            if (__docNo.Equals(this._oldDocNo) == false)
                            {
                                // check have doc_picture
                                if (this._myManageTrans._dataList._docPictureButton.Enabled == true)
                                {
                                    // load control ไว้
                                    string _codepic_ = __docNo.Replace("/", "").Trim();
                                    //if (this._docPicture != null)
                                    //{
                                    //    // check doc_no equa last_docno

                                    //    // save picture
                                    //    string __UpdatePicureResult = this._docPicture._updateImage(_codepic_);

                                    //}
                                    //else
                                    //{
                                    //    // ไม่โหลด control
                                    //}
                                    __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update  " + _g.d.sml_doc_images._table + " set " + _g.d.sml_doc_images._image_id + " = \'" + _codepic_ + "\' where " + _g.d.sml_doc_images._image_id + " = \'" + this._oldDocNo + "\'");
                                }
                            }
                        }

                        // gl process realtime
                        if (__processControl != null && __glManual == false && _g.g._companyProfile._gl_process_realtime == true) // && this._myManageTrans._mode == 1 
                        {
                            // call process 
                            //SMLERPGL._transProcessUserControl __processControl = new SMLERPGL._transProcessUserControl();
                            __processControl._processGLByTrans(this._icTransScreenTop._getDataDate(_g.d.ic_trans._doc_date), this._oldDocNo, this._icTransScreenTop._docFormatCode);
                            List<SMLERPGL._transProcessUserControl._glStruct> __glStruct = __processControl.getGLTrans;

                            // check balance 

                            // if not balance delete last insert

                            if (__glStruct != null && __glStruct.Count > 0 && __glStruct[0]._debit == __glStruct[0]._credit)
                            {
                                // save gl
                                __processControl._saveToDatabase(__glStruct, 1, 1);
                            }

                            /*  โต๋ ย้ายไปเช็คก่อน Save แล้ว balance ค่อยให้ Save
                            else if (__glStruct != null && __glStruct.Count > 0 && __glStruct[0]._debit != __glStruct[0]._credit)
                            {
                                StringBuilder __rollbackSaveQuery = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

                                if (this._transControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก)
                                {
                                    __rollbackSaveQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + this._icTransTable
        + " where " + _g.d.ic_trans._doc_no + "=\'" + this._oldDocNo + "\' and ("
        + _g.d.ic_trans._trans_flag + "=" + this._getTransFlag.ToString() + " or " + _g.d.ic_trans._trans_flag
        + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ")"));
                                }
                                else if (this._transControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร)
                                {
                                    __rollbackSaveQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + this._icTransTable + " where " + _g.d.ic_trans._doc_no + "=\'" + this._oldDocNo + "\' and (" + _g.d.ic_trans._trans_flag + "=" + this._getTransFlag.ToString() + " or " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินเข้าธนาคาร) + ")"));

                                }
                                else
                                {
                                    __rollbackSaveQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + this._icTransTable + " where " + _g.d.ic_trans._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.ic_trans._trans_flag + "=" + this._getTransFlag.ToString()));
                                }


                                // หาง


                                if (this._transControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก)
                                {
                                    string __where1 = _g.d.ic_trans_detail._doc_no + "=\'" + this._oldDocNo + "\' and (" + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนออก).ToString()
                                        + " or " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ")";
                                    // หาง
                                    // ลบของเก่าก่อน save
                                    __rollbackSaveQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + this._icTransDetailTable + " where " + __where1));
                                    this._icTransItemGrid._updateRowIsChangeAll(true);
                                    // ลบ serial
                                    string __whereSerialNumber = _g.d.ic_trans_serial_number._doc_no + "=\'" + this._oldDocNo + "\' and (" + _g.d.ic_trans_serial_number._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนออก).ToString()
                                        + " or " + _g.d.ic_trans_serial_number._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ")";
                                    __rollbackSaveQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_serial_number._table + " where " + __whereSerialNumber));

                                    // ลบ Trans Detail Department
                                    if (_g.g._companyProfile._use_department == 1)
                                    {
                                        string __whereICTransDepartment = _g.d.ic_trans_detail_department._doc_no + "=\'" + this._oldDocNo + "\' and (" + _g.d.ic_trans_detail_department._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนออก).ToString()
                                        + " or " + _g.d.ic_trans_detail_department._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ")";
                                        __rollbackSaveQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail_department._table + " where " + __whereICTransDepartment));
                                    }

                                    // ลบ Trans Detail Project
                                    if (_g.g._companyProfile._use_project == 1)
                                    {
                                        string __whereICTransProject = _g.d.ic_trans_detail_project._doc_no + "=\'" + this._oldDocNo + "\' and (" + _g.d.ic_trans_detail_project._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนออก).ToString()
                                        + " or " + _g.d.ic_trans_detail_project._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ")";
                                        __rollbackSaveQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail_project._table + " where " + __whereICTransProject));
                                    }

                                    // ลบ Trans Detail Allocate
                                    if (_g.g._companyProfile._use_allocate == 1)
                                    {
                                        string __whereICTransAllocate = _g.d.ic_trans_detail_allocate._doc_no + "=\'" + this._oldDocNo + "\' and (" + _g.d.ic_trans_detail_allocate._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนออก).ToString()
                                        + " or " + _g.d.ic_trans_detail_allocate._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ")";
                                        __rollbackSaveQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail_allocate._table + " where " + __whereICTransAllocate));
                                    }

                                    // ลบ Trans Detail Allocate
                                    if (_g.g._companyProfile._use_job == 1)
                                    {
                                        string __whereICTransAllocate = _g.d.ic_trans_detail_jobs._doc_no + "=\'" + this._oldDocNo + "\' and (" + _g.d.ic_trans_detail_jobs._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนออก).ToString()
                                        + " or " + _g.d.ic_trans_detail_jobs._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ")";
                                        __rollbackSaveQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail_jobs._table + " where " + __whereICTransAllocate));
                                    }

                                    // ลบ Trans Detail Allocate
                                    if (_g.g._companyProfile._use_unit == 1)
                                    {
                                        string __whereICTransAllocate = _g.d.ic_trans_detail_site._doc_no + "=\'" + this._oldDocNo + "\' and (" + _g.d.ic_trans_detail_site._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนออก).ToString()
                                        + " or " + _g.d.ic_trans_detail_site._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ")";
                                        __rollbackSaveQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail_site._table + " where " + __whereICTransAllocate));
                                    }

                                }
                                else if (this._transControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร)
                                {
                                    string __where1 = _g.d.ic_trans_detail._doc_no + "=\'" + this._oldDocNo + "\' and (" + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร).ToString()
                                       + " or " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินเข้าธนาคาร).ToString() + ")";

                                    __rollbackSaveQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + this._icTransDetailTable + " where " + __where1));

                                    // ลบ Trans Detail Department
                                    if (_g.g._companyProfile._use_department == 1)
                                    {
                                        string __whereICTransDepartment = _g.d.ic_trans_detail_department._doc_no + "=\'" + this._oldDocNo + "\' and (" + _g.d.ic_trans_detail_department._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนออก).ToString()
                                        + " or " + _g.d.ic_trans_detail_department._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ")";
                                        __rollbackSaveQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail_department._table + " where " + __whereICTransDepartment));
                                    }

                                    // ลบ Trans Detail Project
                                    if (_g.g._companyProfile._use_department == 1)
                                    {
                                        string __whereICTransProject = _g.d.ic_trans_detail_project._doc_no + "=\'" + this._oldDocNo + "\' and (" + _g.d.ic_trans_detail_project._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนออก).ToString()
                                        + " or " + _g.d.ic_trans_detail_department._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ")";
                                        __rollbackSaveQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail_project._table + " where " + __whereICTransProject));
                                    }

                                    // ลบ Trans Detail Allocate
                                    if (_g.g._companyProfile._use_department == 1)
                                    {
                                        string __whereICTransAllocate = _g.d.ic_trans_detail_allocate._doc_no + "=\'" + this._oldDocNo + "\' and (" + _g.d.ic_trans_detail_allocate._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนออก).ToString()
                                        + " or " + _g.d.ic_trans_detail_department._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString() + ")";
                                        __rollbackSaveQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail_allocate._table + " where " + __whereICTransAllocate));
                                    }

                                }
                                else
                                {

                                    // ลบของเก่าก่อน save
                                    string __where1 = _g.d.ic_trans_detail._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + this._getTransFlag.ToString();
                                    __rollbackSaveQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + this._icTransDetailTable + " where " + __where1));

                                    // ic_tarns_serial_number
                                    string __whereSerialNumber = _g.d.ic_trans_serial_number._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.ic_trans_serial_number._trans_flag + "=" + this._getTransFlag.ToString();
                                    __rollbackSaveQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_serial_number._table + " where " + __whereSerialNumber));

                                    switch (this._transControlType)
                                    {
                                        case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย:
                                        case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย:
                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                                        case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                                        case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                                        case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                                        case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                                        case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                                        case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                                        case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                                        case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                                        case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                                        case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                                        case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                                        case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                                        case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                                        case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                                        case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                                        case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                                        case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                                        case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                                        case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                                        case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                                            __rollbackSaveQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ap_ar_trans_detail._table + " where " + _g.d.ap_ar_trans_detail._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.ap_ar_trans_detail._trans_flag + "=" + this._getTransFlag.ToString()));
                                            break;
                                    }

                                    if (_g.g._companyProfile._use_department == 1)
                                    {
                                        string __whereICTransDepartment = _g.d.ic_trans_detail_department._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.ic_trans_detail_department._trans_flag + "=" + this._getTransFlag.ToString();
                                        __rollbackSaveQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail_department._table + " where " + __whereICTransDepartment));
                                    }

                                    // ลบ Trans Detail Project
                                    if (_g.g._companyProfile._use_project == 1)
                                    {
                                        string __whereICTransProject = _g.d.ic_trans_detail_project._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.ic_trans_detail_project._trans_flag + "=" + this._getTransFlag.ToString();
                                        __rollbackSaveQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail_project._table + " where " + __whereICTransProject));
                                    }

                                    // ลบ Trans Detail Allocate
                                    if (_g.g._companyProfile._use_allocate == 1)
                                    {
                                        string __whereICTransAllocate = _g.d.ic_trans_detail_allocate._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.ic_trans_detail_allocate._trans_flag + "=" + this._getTransFlag.ToString();
                                        __rollbackSaveQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail_allocate._table + " where " + __whereICTransAllocate));
                                    }

                                    if (_g.g._companyProfile._use_job == 1)
                                    {
                                        string __whereICTransAllocate = _g.d.ic_trans_detail_jobs._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.ic_trans_detail_jobs._trans_flag + "=" + this._getTransFlag.ToString();
                                        __rollbackSaveQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail_jobs._table + " where " + __whereICTransAllocate));
                                    }

                                    if (_g.g._companyProfile._use_unit == 1)
                                    {
                                        string __whereICTransAllocate = _g.d.ic_trans_detail_site._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.ic_trans_detail_site._trans_flag + "=" + this._getTransFlag.ToString();
                                        __rollbackSaveQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail_site._table + " where " + __whereICTransAllocate));
                                    }

                                }

                                if (this._payControl != null)
                                {
                                    // ลบการจ่ายเงิน
                                    __rollbackSaveQuery.Append(this._payControl._queryDelete(this._oldDocNo));
                                }
                                if (this._payAdvance != null)
                                {
                                    // เงินมัดจำ
                                    __rollbackSaveQuery.Append(this._payAdvance._queryDelete(this._oldDocNo));
                                }
                                if (this._withHoldingTax != null)
                                {
                                    // ลบภาษีหัก ณ. ที่จ่าย
                                    __rollbackSaveQuery.Append(this._withHoldingTax._queryDelete(this._oldDocNo, this._transControlType));
                                }
                                if (this._vatBuy != null)
                                {
                                    __rollbackSaveQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_vat_buy._table + " where " + _g.d.gl_journal_vat_buy._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.gl_journal_vat_buy._trans_flag + "=" + this._getTransFlag.ToString()));
                                }
                                if (this._vatSale != null)
                                {
                                    __rollbackSaveQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_vat_sale._table + " where " + _g.d.gl_journal_vat_sale._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.gl_journal_vat_sale._trans_flag + "=" + this._getTransFlag.ToString()));
                                }

                                __rollbackSaveQuery.Append(this._glDeleteQuery(this._oldDocNo));
                                __rollbackSaveQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.cb_trans_detail._table + " where " + _g.d.cb_trans_detail._doc_no + "=\'" + __docNo + "\'"));


                                __rollbackSaveQuery.Append("</node>");

                                __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __rollbackSaveQuery.ToString());

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

                        // Save Log
                        this._saveLog(this._myManageTrans._mode);
                        //
                        MyLib._myGlobal._displayWarning(1, null);

                        if (_g.g._companyProfile._arm_send_cn &&
                            (this._transControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้ || this._transControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้))
                        {
                            StringBuilder __armMessageCN = new StringBuilder();
                            string __sendCNTo = _g.g._companyProfile._arm_send_cn_to;

                            // get granch
                            //__armMessageCN.Append((this._myManageTrans._mode == 2 ? "แก้ไข" : "") + "ลดหนี้ " + ((MyLib._myTextBox)this._icTransScreenTop._getControl(_g.d.ic_trans._cust_code))._textSecond + "(" + ((MyLib._myTextBox)this._icTransScreenTop._getControl(_g.d.ic_trans._cust_code))._textFirst + ") โดย " + MyLib._myGlobal._userName + "(" + MyLib._myGlobal._userCode + ") มูลค่า : " + this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_amount));
                            MyLib._myComboBox __combo = (MyLib._myComboBox)this._icTransScreenTop._getControl(_g.d.ic_trans._inquiry_type);
                            string __cnType = __combo.Text;


                            string[] __spCNText = __cnType.Split('.');
                            if (__spCNText.Length > 1)
                            {
                                __cnType = __spCNText[1];
                            }

                            __armMessageCN.Append(
                             string.Format("{0}ลดหนี้ {1} {2} {3} User : {4} {5}",

                             (this._myManageTrans._mode == 2 ? "แก้ไข" : "")
                             , __docNo
                             , ((MyLib._myTextBox)this._icTransScreenTop._getControl(_g.d.ic_trans._cust_code))._textFirst + " " + ((MyLib._myTextBox)this._icTransScreenTop._getControl(_g.d.ic_trans._cust_code))._textSecond
                             , __cnType
                             , MyLib._myGlobal._userCode
                             , DateTime.Now.ToString("yyyyMMddHHmmss", new CultureInfo("en-US"))
                             ));

                            DataTable __sendCancelBranch = __myFrameWork._queryShort("select " + _g.d.erp_branch_list._arm_send_cn_to + " from " + _g.d.erp_branch_list._table + " where " + _g.d.erp_branch_list._code + " = \'" + this._icTransScreenTop__getBranchCode() + "\' ").Tables[0];
                            if (__sendCancelBranch.Rows.Count > 0 && __sendCancelBranch.Rows[0][0].ToString().Length > 0)
                            {
                                __sendCNTo = __sendCancelBranch.Rows[0][0].ToString();
                            }

                            SMLERPMailMessage._sendMessage._sendMessageSaleHub(__sendCNTo, __armMessageCN.ToString(), "");
                        }

                        //
                        this._myManageTrans._dataList._refreshData();
                        if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                        {
                            switch (this._transControlType)
                            {
                                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                                case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                                case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า:
                                case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                                case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                                case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                                case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                                    {
                                        _printFormData("");
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            if ((this._myManageTrans._mode == 2 &&

                                (this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ || this._transControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้ || this._transControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้)
                                && MyLib._myGlobal._OEMVersion.Equals("SINGHA") && _g.g._companyProfile._print_invoice_one_time) == false)
                            {
                                _printFormData(this._icTransScreenTop._docFormatCode);
                            }
                        }

                        // delete from cart
                        if (this._isImportFromCart == true)
                        {
                            StringBuilder __queryDeleteCart = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                            __queryDeleteCart.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.order_item._table + " where " + _g.d.order_item._cart_number + " = \'" + this._importCartNumber + "\'"));
                            __queryDeleteCart.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.order_cart._table + " set " + _g.d.order_cart._amount + "=coalesce((select sum(" + _g.d.order_item._amount + ") from " + _g.d.order_item._table + " where " + _g.d.order_item._table + "." + _g.d.order_item._cart_number + "=" + _g.d.order_cart._table + "." + _g.d.order_cart._cart_number + "),0), " + _g.d.order_cart._cust_code + "=\'\'  where " + _g.d.order_cart._cart_number + "=\'" + this._importCartNumber + "\'"));
                            __queryDeleteCart.Append("</node>");

                            string __resultDeleteCart = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __queryDeleteCart.ToString());
                        }

                        if (this._isImportFromHandHeld == true)
                        {
                            StringBuilder __queryDeleteCart = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                            __queryDeleteCart.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.barcode_import_list._table + " where " + _g.d.barcode_import_list._doc_no + " in (" + this._importHandHeldNumber + ") "));
                            __queryDeleteCart.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.barcode_import_list_detail._table + " where " + _g.d.barcode_import_list_detail._doc_no + " in (" + this._importHandHeldNumber + ") "));
                            __queryDeleteCart.Append("</node>");

                            string __resultDeleteCart = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __queryDeleteCart.ToString());
                            if (__resultDeleteCart.Length == 0)
                            {
                                if (this._importFromHandHeld != null)
                                {
                                    this._importFromHandHeld._dataList._refreshData();
                                    this._importFromHandHeld._itemGrid._clear();
                                }
                            }
                        }

                        if (this._isImportFromInternet == true)
                        {
                            StringBuilder __queryDeleteCart = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                            __queryDeleteCart.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from take_order_temp where  id in (" + this._importInternetNumber + ") and product_code = \'" + MyLib._myGlobal._productCode + "\' and database_code = \'" + MyLib._myGlobal._databaseName.ToUpper() + "\'   "));
                            __queryDeleteCart.Append("</node>");

                            MyLib._myFrameWork __ws = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
                            //DataSet __result = __ws._query(MyLib._myGlobal._internetSyncName, __getOrderQuery);
                            string __resultDeleteCart = __ws._queryList(MyLib._myGlobal._internetSyncName, __queryDeleteCart.ToString());
                            if (__resultDeleteCart.Length > 0)
                            {
                                // Message
                                MessageBox.Show(__resultDeleteCart);
                            }

                        }

                        //  ย้ายมาจากก่อนพิมพ์
                        switch (this._transControlType)
                        {
                            case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                            case _g.g._transControlTypeEnum.สินค้า_โอนเข้า:
                            case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                            case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                                break;
                            default:
                                {

                                    //if (MyLib._myGlobal._OEMVersion.Equals("imex") == false)
                                    {

                                        _processThread __process = new _processThread(this._transControlType, __itemList, __docNoList);
                                        //__process._run();
                                        Thread __thread = new Thread(new ThreadStart(__process._run));
                                        __thread.IsBackground = true;
                                        __thread.Start();

                                        // toe coupon process
                                        if (this._payControl != null && this._payControl.Enabled == true)
                                        {
                                            StringBuilder __couponNumberList = new StringBuilder();
                                            for (int __loop = 0; __loop < this._payControl._payCouponGrid._rowData.Count; __loop++)
                                            {
                                                string __number = this._payControl._payCouponGrid._cellGet(__loop, _g.d.cb_trans_detail._trans_number).ToString().Trim();
                                                if (__number.Length > 0)
                                                {
                                                    if (__couponNumberList.Length > 0)
                                                    {
                                                        __couponNumberList.Append(",");
                                                    }
                                                    __couponNumberList.Append(__number);
                                                }
                                            }

                                            if (__couponNumberList.ToString().Length > 0)
                                            {
                                                SMLProcess._posProcess __processPos = new SMLProcess._posProcess();
                                                __processPos._numberList = __couponNumberList.ToString();
                                                Thread __posThread = new Thread(__processPos._processCoupon);
                                                __posThread.IsBackground = true;
                                                __posThread.Start();
                                            }
                                        }
                                    }
                                }
                                break;
                        }

                        this._clearScreen();
                        this._myManageTrans__newDataClick();
                        //this._icTransItemGrid._searchItem = new MyLib._searchDataFull();

                        if (MyLib._myGlobal._OEMVersion.Equals("SINGHA") && this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ)
                        {
                            this._icTransScreenTop._setDataDate(_g.d.ic_trans._doc_date, this._lastSaveDate);
                            this._icTransScreenTop._setDataStr(_g.d.ic_trans._doc_no, this._lastDocFormatCode);
                        }
                    }
                    else
                    {
                        MessageBox.Show(MyLib._myGlobal._errorText(__result), "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show(MyLib._myGlobal._resource("ไม่สามารถบันทึกข้อมูลได้"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            _showPrintDialogByCtrl = false;
        }

        private string _createLog(int mode)
        {
            string __docDate = (mode == 3) ? "\'\'" : this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_date);
            string __docNo = (mode == 3) ? "\'\'" : this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_no);
            decimal __amount = (mode == 3) ? 0M : this._icTransScreenBottom._getDataNumber(_g.d.ic_trans._total_amount);
            StringBuilder __logDetail = new StringBuilder();
            __logDetail.Append(this._icTransScreenTop._logCreate("top"));
            string __query = MyLib._myUtil._convertTextToXml("insert into " + _g.d.logs._table + " (" + _g.d.logs._function_type + "," + _g.d.logs._computer_name + "," + _g.d.logs._guid + "," +
                _g.d.logs._doc_date + "," + _g.d.logs._doc_no + "," + _g.d.logs._doc_amount + "," +
                _g.d.logs._doc_date_old + "," + _g.d.logs._doc_no_old + "," + _g.d.logs._doc_amount_old + "," +
                _g.d.logs._menu_name + "," + _g.d.logs._screen_code + "," + _g.d.logs._function_code + "," + _g.d.logs._user_code + "," +
                _g.d.logs._date_time + "," + _g.d.logs._data1 + "," + _g.d.logs._data2 + ") values (2," +
                "\'" + SystemInformation.ComputerName + "\'," + "\'" + Guid.NewGuid().ToString("N") + "\'," +
                __docDate + "," + __docNo + "," + __amount.ToString() + "," +
                this._logDocDateOld + "," + this._logDocNoOld + "," + this._logAmountOld.ToString() + "," +
                "\'" + this._menuName + "\'," + _g.g._transFlagGlobal._transFlag(this._transControlType).ToString() + "," + mode.ToString() + ",\'" +
                MyLib._myGlobal._userCode + "\',\'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US")) + "\',\'" + __logDetail.ToString() + "\',\'" + this._logDetailOld + "\')");
            return __query;
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

        public string _createDataQuery(MyLib._myGrid _myGrid, string __tableName, string __fieldList, string __dataList)
        {
            StringBuilder __myQuery = new StringBuilder();
            try
            {
                if ((__fieldList.Length > 0) && (__dataList.Length > 0))
                {
                    for (int __row = 0; __row < _myGrid._rowData.Count; __row++)
                    {
                        StringBuilder __fieldName = new StringBuilder();
                        StringBuilder __data = new StringBuilder();
                        for (int __loop = 0; __loop < _myGrid._columnList.Count; __loop++)
                        {
                            MyLib._myGrid._columnType __getColumn = (MyLib._myGrid._columnType)_myGrid._columnList[__loop];
                            if (__loop != 0)
                            {
                                __fieldName.Append(",");
                                __data.Append(",");
                            }
                            __fieldName.Append(((MyLib._myGrid._columnType)_myGrid._columnList[__loop])._originalName);
                            if (__getColumn._type == 1)
                            {
                                __data.Append(("\'" + _myGrid._cellGet(__row, __loop).ToString() + "\'"));
                            }
                            if (__getColumn._type == 2 || __getColumn._type == 3)
                            {
                                __data.Append(_myGrid._cellGet(__row, __loop).ToString());
                            }
                        }
                        if (__fieldName.Length > 0)
                        {
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + __tableName + " (" + __fieldList + __fieldName.ToString() + ") values (" + __dataList + __data.ToString() + ")"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return __myQuery.ToString();
        }

        private void _printFormData(string docTypeCode)
        {
            MyLib._myFrameWork __myFramework = new MyLib._myFrameWork();
            // check 
            bool __printForm = false;
            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
            {
                string __query = "select " + _g.d.erp_doc_format._form_code + " from " + _g.d.erp_doc_format._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_doc_format._code) + "=\'" + this._icTransScreenTop._docFormatCode + "\'";
                DataTable __result = __myFramework._queryShort(__query).Tables[0];
                if (__result.Rows.Count > 0)
                {
                    if (__result.Rows[0][_g.d.erp_doc_format._form_code].ToString().Length > 0)
                    {
                        __printForm = true;
                        docTypeCode = this._icTransScreenTop._docFormatCode;
                    }
                }
            }

            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore && this._transControlType != _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด && this._transControlType != _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก && __printForm == false)
            {
                this._printDataWork();
            }
            else
            {
                string __docNo = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_no).ToString().Trim();
                bool __isPrint = true;
                // singha
                if (MyLib._myGlobal._OEMVersion.Equals("SINGHA") &&
                    (this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ || this._transControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้ || this._transControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้)
                    && _g.g._companyProfile._print_invoice_one_time == true)
                {
                    // check print invoice
                    DataTable __printCountData = __myFramework._queryShort("select count(*) as xcount from " + _g.d.erp_print_logs._table + " where " + _g.d.erp_print_logs._doc_no + "=\'" + __docNo + "\' and " + _g.d.erp_print_logs._trans_flag + " = " + this._getTransFlag.ToString()).Tables[0];
                    if (__printCountData.Rows.Count > 0 && MyLib._myGlobal._decimalPhase(__printCountData.Rows[0][0].ToString()) > 0)
                    {
                        MessageBox.Show("ไม่อนุญาติให้พิมพ์ซ้ำ", "พิมพ์ไปแล้ว", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        __isPrint = false;
                    }
                    __printCountData.Dispose();
                }


                if (__isPrint)
                {

                    // เจอ error ตอนพิมพ์สิงห์ แล้วไป save log print
                    bool __printResult = SMLERPReportTool._global._printForm(docTypeCode, __docNo, this._getTransFlag.ToString(), _showPrintDialogByCtrl);
                    if (__printResult == true)
                    {
                        // update print count
                        // MyLib._myFrameWork __myFramework = new MyLib._myFrameWork();
                        __myFramework._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "insert into " + _g.d.erp_print_logs._table + "(" + _g.d.erp_print_logs._doc_no + "," + _g.d.erp_print_logs._trans_flag + "," + _g.d.erp_print_logs._user_code + "," + _g.d.erp_print_logs._print_datetime + ") values (\'" + __docNo + "\'," + this._getTransFlag.ToString() + ",\'" + MyLib._myGlobal._userCode + "\', \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\')");
                    }
                }
            }
        }

        private void _printDataWork()
        {
            try
            {
                string __getDocNo = "";
                string __getDocDate = "";
                string __getArCode = "";
                DateTime __dateDocDate;
                __getDocNo = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_no);
                __dateDocDate = MyLib._myGlobal._convertDate(this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_date));
                __getDocDate = MyLib._myGlobal._convertDateToQuery(__dateDocDate);
                switch (this._transControlType)
                {
                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                    case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                        __getArCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
                        if (__getDocNo.Length > 0 && __getArCode.Length > 0)
                        {
                            _formSaleOrderPreview __showPrintDialog = new _formSaleOrderPreview(_gForm._formEnum.ขาย_ขายสินค้าและบริการ_ici.ToString(), __getDocNo, __getDocDate, __getArCode);
                            __showPrintDialog.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อนเลขที่เอกสาร และลูกหนี้"));
                        }
                        break;
                    case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                        __getArCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
                        if (__getDocNo.Length > 0 && __getArCode.Length > 0)
                        {
                            _formSaleOrderPreview __showPrintDialog = new _formSaleOrderPreview(_gForm._formEnum.ใบเสนอราคา.ToString(), __getDocNo, __getDocDate, __getArCode);
                            __showPrintDialog.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อนเลขที่เอกสาร และลูกค้า"));
                        }
                        break;
                    case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                        __getArCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
                        if (__getDocNo.Length > 0 && __getArCode.Length > 0)
                        {
                            _formSaleOrderPreview __showPrintDialog = new _formSaleOrderPreview(_gForm._formEnum.ขาย_สั่งจองสินค้าและสั่งซื้อสินค้า.ToString(), __getDocNo, __getDocDate, __getArCode);
                            __showPrintDialog.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อนเลขที่เอกสาร และลูกค้า"));
                        }
                        break;
                    case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                        __getArCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
                        if (__getDocNo.Length > 0 && __getArCode.Length > 0)
                        {
                            _formSaleOrderPreview __showPrintDialog = new _formSaleOrderPreview(_gForm._formEnum.ขาย_ใบสั่งขาย.ToString(), __getDocNo, __getDocDate, __getArCode);
                            __showPrintDialog.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อนเลขที่เอกสาร และลูกค้า"));
                        }
                        break;
                    case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                        __getArCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
                        if (__getDocNo.Length > 0 && __getArCode.Length > 0)
                        {
                            _formSaleOrderPreview __showPrintDialog = new _formSaleOrderPreview(_gForm._formEnum.ขาย_เพิ่มหนี้.ToString(), __getDocNo, __getDocDate, __getArCode);
                            __showPrintDialog.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อนเลขที่เอกสาร และลูกค้า"));
                        }
                        break;
                    case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                        __getArCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
                        if (__getDocNo.Length > 0 && __getArCode.Length > 0)
                        {
                            _formSaleOrderPreview __showPrintDialog = new _formSaleOrderPreview(_gForm._formEnum.ขาย_ลดหนี้.ToString(), __getDocNo, __getDocDate, __getArCode);
                            __showPrintDialog.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อนเลขที่เอกสาร และลูกค้า"));
                        }
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                        __getArCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
                        if (__getDocNo.Length > 0 && __getArCode.Length > 0)
                        {
                            _formSaleOrderPreview __showPrintDialog = new _formSaleOrderPreview(_gForm._formEnum.ซื้อ_ซื้อสินค้าและค่าบริการ.ToString(), __getDocNo, __getDocDate, __getArCode);
                            __showPrintDialog.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อนเลขที่เอกสาร และลูกค้า"));
                        }
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                        __getArCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
                        if (__getDocNo.Length > 0 && __getArCode.Length > 0)
                        {
                            _formSaleOrderPreview __showPrintDialog = new _formSaleOrderPreview(_gForm._formEnum.ซื้อ_พาเชียล_ตั้งหนี้.ToString(), __getDocNo, __getDocDate, __getArCode);
                            __showPrintDialog.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อนเลขที่เอกสาร และลูกค้า"));
                        }
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                        __getArCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
                        if (__getDocNo.Length > 0 && __getArCode.Length > 0)
                        {
                            _formSaleOrderPreview __showPrintDialog = new _formSaleOrderPreview(_gForm._formEnum.ซื้อ_พาเชียล_รับสินค้า.ToString(), __getDocNo, __getDocDate, __getArCode);
                            __showPrintDialog.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อนเลขที่เอกสาร และลูกค้า"));
                        }
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                        __getArCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
                        if (__getDocNo.Length > 0 && __getArCode.Length > 0)
                        {
                            _formSaleOrderPreview __showPrintDialog = new _formSaleOrderPreview(_gForm._formEnum.ซื้อ_ใบสั่งซื้อ.ToString(), __getDocNo, __getDocDate, __getArCode);
                            __showPrintDialog.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อนเลขที่เอกสาร และลูกค้า"));
                        }
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                        __getArCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
                        if (__getDocNo.Length > 0 && __getArCode.Length > 0)
                        {
                            _formSaleOrderPreview __showPrintDialog = new _formSaleOrderPreview(_gForm._formEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด.ToString(), __getDocNo, __getDocDate, __getArCode);
                            __showPrintDialog.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อนเลขที่เอกสาร และลูกค้า"));
                        }
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                        __getArCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
                        if (__getDocNo.Length > 0 && __getArCode.Length > 0)
                        {
                            _formSaleOrderPreview __showPrintDialog = new _formSaleOrderPreview(_gForm._formEnum.ซื้อ_พาเชียล_เพิ่มหนี้.ToString(), __getDocNo, __getDocDate, __getArCode);
                            __showPrintDialog.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อนเลขที่เอกสาร และลูกค้า"));
                        }
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                        __getArCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
                        if (__getDocNo.Length > 0 && __getArCode.Length > 0)
                        {
                            _formSaleOrderPreview __showPrintDialog = new _formSaleOrderPreview(_gForm._formEnum.ซื้อ_พาเชียล_ราคาผิด.ToString(), __getDocNo, __getDocDate, __getArCode);
                            __showPrintDialog.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อนเลขที่เอกสาร และลูกค้า"));
                        }
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                        __getArCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
                        if (__getDocNo.Length > 0 && __getArCode.Length > 0)
                        {
                            _formSaleOrderPreview __showPrintDialog = new _formSaleOrderPreview(_gForm._formEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด.ToString(), __getDocNo, __getDocDate, __getArCode);
                            __showPrintDialog.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อนเลขที่เอกสาร และลูกค้า"));
                        }
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                        __getArCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
                        if (__getDocNo.Length > 0 && __getArCode.Length > 0)
                        {
                            _formSaleOrderPreview __showPrintDialog = new _formSaleOrderPreview(_gForm._formEnum.ซื้อ_พาเชียล_ลดหนี้.ToString(), __getDocNo, __getDocDate, __getArCode);
                            __showPrintDialog.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อนเลขที่เอกสาร และลูกค้า"));
                        }
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า:
                        __getArCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code);
                        if (__getDocNo.Length > 0 && __getArCode.Length > 0)
                        {
                            _formSaleOrderPreview __showPrintDialog = new _formSaleOrderPreview(_gForm._formEnum.ซื้อ_จ่ายเงินมัดจำหรือเงินล่วงหน้า.ToString(), __getDocNo, __getDocDate, __getArCode);
                            __showPrintDialog.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อนเลขที่เอกสาร และลูกค้า"));
                        }
                        break;
                    case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                        if (__getDocNo.Length > 0)
                        {
                            _formSaleOrderPreview __showPrintDialog = new _formSaleOrderPreview(_gForm._formEnum.สินค้า_รับสินค้าสำเร็จรูป.ToString(), __getDocNo, __getDocDate, __getArCode);
                            __showPrintDialog.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อนเลขที่เอกสาร"));
                        }
                        break;
                    case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                        if (__getDocNo.Length > 0)
                        {
                            _formSaleOrderPreview __showPrintDialog = new _formSaleOrderPreview(_gForm._formEnum.สินค้า_เบิกสินค้าวัตถุดิบ.ToString(), __getDocNo, __getDocDate, __getArCode);
                            __showPrintDialog.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อนเลขที่เอกสาร"));
                        }
                        break;
                    case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                        if (__getDocNo.Length > 0)
                        {
                            _formSaleOrderPreview __showPrintDialog = new _formSaleOrderPreview(_gForm._formEnum.สินค้า_รับคืนสินค้าจากการเบิก.ToString(), __getDocNo, __getDocDate, __getArCode);
                            __showPrintDialog.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อนเลขที่เอกสาร"));
                        }
                        break;
                    case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                        if (__getDocNo.Length > 0)
                        {
                            _formSaleOrderPreview __showPrintDialog = new _formSaleOrderPreview(_gForm._formEnum.สินค้า_โอนออก.ToString(), __getDocNo, __getDocDate, __getArCode);
                            __showPrintDialog.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อนเลขที่เอกสาร"));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _checkPurchasePermiumButton_Click(object sender, EventArgs e)
        {
            // ลบของแถมออกจาก Grid
            int __rowDelete = 0;
            while (__rowDelete < this._icTransItemGrid._rowData.Count)
            {
                string __getIsPermium = this._icTransItemGrid._cellGet(__rowDelete, _g.d.ic_trans_detail._is_permium).ToString();
                if (__getIsPermium.Equals("1"))
                {
                    this._icTransItemGrid._rowData.RemoveAt(__rowDelete);
                }
                else
                {
                    __rowDelete++;
                }
            }
            //
            SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
            StringBuilder __source = new StringBuilder();
            __source.Append(MyLib._myGlobal._xmlHeader + "<node>");
            for (int __row = 0; __row < this._icTransItemGrid._rowData.Count; __row++)
            {
                string __itemCode = this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString();
                if (__itemCode.Length > 0)
                {
                    decimal __qty = MyLib._myGlobal._decimalPhase(this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._qty).ToString());
                    decimal __standValue = MyLib._myGlobal._decimalPhase(this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._stand_value).ToString());
                    decimal __divideValue = MyLib._myGlobal._decimalPhase(this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._divide_value).ToString());
                    __source.Append(String.Format("<row itemcode=\'{0}\' qty=\'{1}\' standvalue=\'{2}\' dividevalue=\'{3}\'/>", __itemCode, __qty, __standValue, __divideValue));
                }
            }
            __source.Append("</node>");
            string __result = __smlFrameWork._purchase_permium_process(MyLib._myGlobal._databaseName, "", __source.ToString());
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string[] __permiumList = __result.Split(',');
                for (int __row = 0; __row < __permiumList.Length; __row++)
                {
                    string[] __permiumDetail = __permiumList[__row].Split(':');
                    string __permiumCode = __permiumDetail[0].ToString();
                    decimal __permiumCount = MyLib._myGlobal._decimalPhase(__permiumDetail[1].ToString());
                    // ดึงของแถม
                    DataTable __getPermium = __myFrameWork._queryShort("select " + _g.d.ic_purchase_permium_list._ic_code + "," + _g.d.ic_purchase_permium_list._unit_code + "," + _g.d.ic_purchase_permium_list._qty +
                        " from " + _g.d.ic_purchase_permium_list._table + " where " + _g.d.ic_purchase_permium_list._permium_code + "=\'" + __permiumCode + "\'").Tables[0];
                    for (int __rowPermium = 0; __rowPermium < __getPermium.Rows.Count; __rowPermium++)
                    {
                        int __addr = this._icTransItemGrid._addRow();
                        string __itemCode = __getPermium.Rows[__rowPermium][_g.d.ic_purchase_permium_list._ic_code].ToString();
                        string __unitCode = __getPermium.Rows[__rowPermium][_g.d.ic_purchase_permium_list._unit_code].ToString();
                        decimal __qty = MyLib._myGlobal._decimalPhase(__getPermium.Rows[__rowPermium][_g.d.ic_purchase_permium_list._qty].ToString()) * __permiumCount;
                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._is_permium, 1, false);
                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, true);
                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, true);
                        this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._qty, __qty, true);
                    }
                }
            }
            catch
            {
            }
        }

        SMLInventoryControl._stkReorder _stkReorder;
        Form _stkReorderForm;
        private void _purchasePointButton_Click(object sender, EventArgs e)
        {
            if (this._stkReorderForm == null)
            {
                this._stkReorder = new SMLInventoryControl._stkReorder(1);
                this._stkReorder.Dock = DockStyle.Fill;
                this._stkReorder._save += new SMLInventoryControl.SaveEventHandler(_stkReorder__save);
                this._stkReorderForm = new Form();
                this._stkReorderForm.WindowState = FormWindowState.Maximized;
                this._stkReorderForm.Controls.Add(this._stkReorder);
            }
            else
            {
                for (int __row = 0; __row < this._stkReorder._resultGrid._rowData.Count; __row++)
                {
                    this._stkReorder._resultGrid._cellUpdate(__row, "Select", 0, false);
                }
            }
            this._stkReorderForm.ShowDialog();
        }

        void _stkReorder__save(object sender)
        {
            this._stkReorderForm.Close();
            for (int __row = 0; __row < this._stkReorder._resultGrid._rowData.Count; __row++)
            {
                int __selected = MyLib._myGlobal._intPhase(this._stkReorder._resultGrid._cellGet(__row, "Select").ToString());
                if (__selected == 1)
                {
                    int __addr = this._icTransItemGrid._addRow();
                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, this._stkReorder._resultGrid._cellGet(__row, _g.d.ic_resource._ic_code).ToString(), true);
                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, this._stkReorder._resultGrid._cellGet(__row, _g.d.ic_resource._ic_unit_code).ToString(), true);
                    this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._qty, this._stkReorder._resultGrid._cellGet(__row, _g.d.ic_resource._purchase_qty), true);
                }
            }
        }
    }

    public class _ictransItemGridButtomControl : MyLib._myGrid
    {
        //#grid2
        private _g.g._transControlTypeEnum _ictransControlTemp;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        SMLERPGlobal._searchProperties __searchProperties = new SMLERPGlobal._searchProperties();
        ArrayList __searchMasterList = new ArrayList();
        string _columnUnitName = "unit_name";
        string _columnUnitType = "unit_type";
        string _columnCustName = "cust_name";

        public _g.g._transControlTypeEnum _icTransControlType
        {
            set
            {
                this._ictransControlTemp = value;
                this._build();
                this.Invalidate();
            }
            get
            {
                return this._ictransControlTemp;
            }
        }

        public _ictransItemGridButtomControl()
        {
            this._build();
        }

        void _build()
        {
            this._columnList.Clear();
            this._table_name = _g.d.ic_trans_detail._table;
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            string __formatNumberNone = MyLib._myGlobal._getFormatNumber("m00");
            this._width_by_persent = true;
        }
    }

    public class _processThread
    {
        private ArrayList _itemList = new ArrayList();
        private string _docNoList = "";
        private _g.g._transControlTypeEnum _transControlType = _g.g._transControlTypeEnum.ว่าง;

        public _processThread(_g.g._transControlTypeEnum transControlType, ArrayList itemList, string docNoList)
        {
            this._transControlType = transControlType;
            this._itemList = itemList;
            this._docNoList = docNoList;
        }

        public void _run()
        {
            try
            {
                SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
                string __itemListForProcess = _g.g._getItemRepack(_itemList);
                SMLProcess._docFlow __process = new SMLProcess._docFlow();
                __process._processAll(this._transControlType, __itemListForProcess, _docNoList);
                //
                _g.g._updateDateTimeForCalc(this._transControlType, __itemListForProcess);
                // Process Stock
                //if (_g.g._companyProfile._disable_auto_stock_process == false)
                //{
                string __resultStr = __smlFrameWork._process_stock_balance(MyLib._myGlobal._databaseName, __itemListForProcess, _g.g._transFlagGlobal._transFlag(this._transControlType).ToString());
                if (__resultStr.Length > 0)
                {
                    Console.WriteLine("_run : " + __resultStr);
                }
                //}

            }
            catch
            {
            }
        }
    }
}