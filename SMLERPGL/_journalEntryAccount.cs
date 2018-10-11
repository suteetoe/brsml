using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;

namespace SMLERPGL
{
    public partial class _journalEntryAccount : UserControl
    {
        private string _oldDocNo = "";
        private string _oldBookCode = "";
        private DateTime _oldDate = new DateTime(1000, 1, 1);
        private int _getColumnDocDate = 0;
        private int _getColumnBookCode = 0;
        private int _getColumnDocNo = 0;
        private int _getColumnTransFlag = 0;
        public bool _showPrintDialogByCtrl = false;
        private string _fieldTransDirect = "trans_direct";

        private System.Windows.Forms.TabPage _tab_wht_out;
        private System.Windows.Forms.TabPage _tab_wht_in;
        private System.Windows.Forms.TabPage _tab_vat_buy;
        private System.Windows.Forms.TabPage _tab_vat_sale;
        SMLERPGLControl._vatBuy _vatBuy;
        SMLERPGLControl._vatSale _vatSale;
        SMLERPGLControl._withHoldingTax _whtIn;
        SMLERPGLControl._withHoldingTax _whtOut;


        private int _oldTransFlag = 0;
        private string _logDocDateOld;
        private string _logDocNoOld;
        private decimal _logAmountOld;
        private StringBuilder _logDetailOld;

        public _journalEntryAccount()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
            {
                _g.g._checkOpenPeriod();
            }

            //
            this._autoRunningButton._iconNumber = 1;
            this._autoRunningButton.Image = imageList1.Images[this._autoRunningButton._iconNumber];
            this._clearDataAfterSaveButton._iconNumber = 0;
            this._clearDataAfterSaveButton.Image = imageList1.Images[this._clearDataAfterSaveButton._iconNumber];
            //
            _myManageData1._dataListOpen = true;
            _myManageData1._displayMode = 1;
            _myManageData1._dataList._lockRecord = true;
            _myManageData1._selectDisplayMode(this._myManageData1._displayMode);
            _myManageData1._dataList._loadViewAddColumn += (MyLib._myGrid grid) =>
            {
                // เพิ่ม Column ต่อท้าย
                grid._addColumn(_fieldTransDirect, 1, 0, 0, false, true, true, false);
            };
            _myManageData1._dataList._loadViewFormat("screen_gl_journal", MyLib._myGlobal._userSearchScreenGroup, true);
            _myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
            _myManageData1._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            _myManageData1._manageButton = this._myToolBar;
            _myManageData1._manageBackgroundPanel = this._myPanel1;
            _myManageData1._newDataClick += new MyLib.NewDataEvent(_myManageData1__newDataClick);
            _myManageData1._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
            _myManageData1._clearData += new MyLib.ClearDataEvent(_myManageData1__clearData);

            int __columnTransFlagIndex = _myManageData1._dataList._gridData._findColumnByName(_g.d.gl_journal._table + "." + _g.d.gl_journal._trans_flag);
            if (__columnTransFlagIndex == -1)
            {
                _myManageData1._dataList._gridData._addColumn(_g.d.gl_journal._table + "." + _g.d.gl_journal._trans_flag, 2, 10, 10);
            }

            _myManageData1._dataList._referFieldAdd(_g.d.gl_journal._doc_date, 2);
            _myManageData1._dataList._referFieldAdd(_g.d.gl_journal._doc_no, 1);
            _myManageData1._dataList._referFieldAdd(_g.d.gl_journal._book_code, 1);
            _myManageData1._dataList._referFieldAdd(_g.d.gl_journal._trans_flag, 4);

            _myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
            _myManageData1._dataList._gridData._beforeDisplayRendering += new MyLib.BeforeDisplayRenderRowEventHandler(_gridData__beforeDisplayRendering);
            //
            _myManageData1._calcArea();
            _myManageData1._dataList._printRangeButton.Visible = true;
            _myManageData1._dataList._printRangeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
            _myManageData1._dataList._multiPrintEvent += _dataList__multiPrintEvent;

            _glDetail1._glDetailGrid._clear();
            _glDetail1._glDetailGrid._queryForUpdateWhere += new MyLib.QueryForUpdateWhereEventHandler(_glDetailGrid__queryForUpdateWhere);
            _glDetail1._glDetailGrid._queryForUpdateCheck += new MyLib.QueryForUpdateCheckEventHandler(_glDetailGrid__queryForUpdateCheck);
            _glDetail1._glDetailGrid._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_glDetailGrid__queryForInsertCheck);
            _glDetail1._glDetailGrid._queryForRowRemoveCheck += new MyLib.QueryForRowRemoveCheckEventHandler(_glDetailGrid__queryForRowRemoveCheck);
            _glDetail1._glDetailGrid._afterAddRow += _glDetailGrid__afterAddRow;
            _screenTop._reLoad();
            //
            this._screenTop.screenCode = "JV";
            this._screenTop._saveKeyDown += new MyLib.SaveKeyDownHandler(_screenTop__saveKeyDown);
            this._screenTop._checkKeyDownReturn += new MyLib.CheckKeyDownReturnHandler(_screenTop__checkKeyDownReturn);
            this._screenTop._checkKeyDown += new MyLib.CheckKeyDownHandler(_screenTop__checkKeyDown);
            this._screenTop._textBoxChanged += _screenTop__textBoxChanged;
            //
            _recurring1._journalInputScreen = this._screenTop;
            _recurring1._journalInputDetail = this._glDetail1;
            this.Resize += new EventHandler(_journalEntryAccount_Resize);
            this._screenTop._setCheckBox(_g.d.gl_journal._trans_direct, true);
            this._screenTop._enableTransDirect(false);

            //if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLGeneralLedger)
            {
                this._tab_wht_out = new System.Windows.Forms.TabPage();
                this._tab_wht_in = new System.Windows.Forms.TabPage();
                this._tab_vat_buy = new System.Windows.Forms.TabPage();
                this._tab_vat_sale = new System.Windows.Forms.TabPage();

                this._vatSale = new SMLERPGLControl._vatSale();
                this._vatBuy = new SMLERPGLControl._vatBuy();
                this._whtIn = new SMLERPGLControl._withHoldingTax(SMLERPGLControl._withHoldingTaxType.ซื้อ);
                this._whtOut = new SMLERPGLControl._withHoldingTax(SMLERPGLControl._withHoldingTaxType.ขาย);

                // add vat wht
                this.SuspendLayout();
                this._tab.SuspendLayout();

                this._tab.TabPages.Add(this._tab_wht_out);
                this._tab.TabPages.Add(this._tab_wht_in);
                this._tab.TabPages.Add(this._tab_vat_buy);
                this._tab.TabPages.Add(this._tab_vat_sale);

                // set tabl
                this._tab_wht_out.Name = "tab_wht_out";
                this._tab_wht_out.Controls.Add(this._whtOut);
                this._tab_wht_out.Text = "tab_wht_out";
                this._whtOut.Dock = DockStyle.Fill;

                this._tab_wht_in.Name = "tab_wht_in";
                this._tab_wht_in.Controls.Add(this._whtIn);
                this._tab_wht_in.Text = "tab_wht_in";
                this._whtIn.Dock = DockStyle.Fill;

                this._tab_vat_buy.Name = "tab_vat_buy";
                this._tab_vat_buy.Controls.Add(this._vatBuy);
                this._tab_vat_buy.Text = "tab_vat_buy";
                this._vatBuy.Dock = DockStyle.Fill;

                this._tab_vat_sale.Name = "tab_vat_sale";
                this._tab_vat_sale.Controls.Add(this._vatSale);
                this._tab_vat_sale.Text = "tab_vat_sale";
                this._vatSale.Dock = DockStyle.Fill;


                this._tab.ResumeLayout(false);
                this.ResumeLayout(false);
            }

            this._logDetailOld = new StringBuilder();
            this._logDocNoOld = "";
            this._logDocDateOld = "";
        }

        void _glDetailGrid__afterAddRow(object sender, int row)
        {
            string __branchCode = this._screenTop._getDataStr(_g.d.gl_journal._branch_code);
            if (__branchCode.Length > 0)
            {
                this._glDetail1._glDetailGrid._cellUpdate(row, _g.d.gl_journal_detail._branch_code, __branchCode, true);
            }
        }

        void _screenTop__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.gl_journal._doc_date))
            {
                _g.g._accountPeriodClass __accountPeriod = _g.g._accountPeriodClassFind(this._screenTop._getDataDate(_g.d.gl_journal._doc_date));
                if (__accountPeriod != null)
                {
                    this._screenTop._setDataStr(_g.d.gl_journal._period_number, __accountPeriod._number.ToString());
                    this._screenTop._setDataStr(_g.d.gl_journal._account_year, __accountPeriod._year.ToString());
                }
                this._screenTop.Invalidate();
            }

        }

        void _dataList__multiPrintEvent(ArrayList selectedRow)
        {
            ArrayList __printList = new ArrayList();
            int __docNoColumnIndex = _myManageData1._dataList._gridData._findColumnByName(_g.d.gl_journal._table + "." + _g.d.gl_journal._doc_no);
            for (int __row = 0; __row < selectedRow.Count; __row++)
            {
                int __getRow = (int)selectedRow[__row];
                List<SMLERPReportTool._ReportToolCondition> __condition = new List<SMLERPReportTool._ReportToolCondition>();
                string __getDocNo = this._myManageData1._dataList._gridData._cellGet(__getRow, __docNoColumnIndex).ToString();
                __condition.Add(new SMLERPReportTool._ReportToolCondition(_g.d.gl_journal._doc_no, __getDocNo));
                __printList.Add(__condition.ToArray());
            }
            // ใส่ค่าว่างไปก่อน ค่อย query ดึงจากรหัสหน้าจอเอา
            string __doc_format_code = ""; // this._screenTop._getDataStr(_g.d.gl_journal._doc_format_code); 

            // send key CTRL เพื่อดึง ไม่ต้องถามอีกให้ปล่อยไปก่อน
            bool __printResult = SMLERPReportTool._global._printRangeForm(__doc_format_code, __printList, true, _g.g._companyProfile._voucher_form_code);
        }

        MyLib.BeforeDisplayRowReturn _gridData__beforeDisplayRendering(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData, Graphics e)
        {
            decimal __debit = MyLib._myGlobal._decimalPhase(sender._cellGet(row, _g.d.gl_journal._table + "." + _g.d.gl_journal._debit).ToString());
            decimal __credit = MyLib._myGlobal._decimalPhase(sender._cellGet(row, _g.d.gl_journal._table + "." + _g.d.gl_journal._credit).ToString());
            if (__debit != __credit)
            {
                senderRow.newColor = Color.MediumVioletRed;
            }
            else
            {
                if ((int)MyLib._myGlobal._decimalPhase(sender._cellGet(row, _fieldTransDirect).ToString()) == 1)
                {
                    // ป้อนตรง
                    senderRow.newColor = Color.Blue;
                }
            }
            return senderRow;
        }

        void _journalEntryAccount_Resize(object sender, EventArgs e)
        {
            //if (_myManageData1._dataList._loadViewDataSuccess == false)
            //{
            //    _myManageData1._dataList._loadViewData(0);
            //}
        }

        void _myManageData1__closeScreen()
        {
            this.Dispose();
        }

        bool _glDetailGrid__queryForRowRemoveCheck(MyLib._myGrid sender, int row)
        {
            if (sender._cellGet(row, 0) == null)
            {
                return true;
            }
            // เป็นค่าว่างหรือไม่ (ให้ลบออก)
            if (((string)sender._cellGet(row, 0)).ToString().Length == 0)
            {
                return true;
            }
            return false;
        }

        bool _glDetailGrid__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            if (sender._cellGet(row, 0) == null)
            {
                return false;
            }
            // เป็นค่าว่างหรือไม่ (ไม่บันทึก)
            if (((string)sender._cellGet(row, 0)).ToString().Length == 0)
            {
                return false;
            }
            return true;
        }

        bool _glDetailGrid__queryForUpdateCheck(MyLib._myGrid sender, int row)
        {
            if (sender._cellGet(row, 0) == null)
            {
                return false;
            }
            // เป็นค่าว่างหรือไม่ (ไม่บันทึก)
            if (((string)sender._cellGet(row, 0)).ToString().Length == 0)
            {
                return false;
            }
            // ดูว่าเป็น row_number เป็น 0 หรือไม่ ถ้าเป็น 0 คือเป็นรายการใหม่ (ห้ามใช้คำสั่ง Update เดี๋ยวมันจะไป Insert ให้ต่อไป)
            if (((int)sender._cellGet(row, sender._rowNumberName)) == 0)
            {
                return false;
            }
            return true;
        }

        string _glDetailGrid__queryForUpdateWhere(MyLib._myGrid sender, int row)
        {
            int __getInt = (int)sender._cellGet(row, sender._rowNumberName);
            return (string.Concat(sender._rowNumberName, "=", __getInt.ToString()));
        }


        private string _createLog(int mode)
        {
            int __columnDebit = this._glDetail1._glDetailGrid._findColumnByName(_g.d.gl_journal_detail._debit);

            string __docDate = (mode == 3) ? "\'\'" : this._screenTop._getDataStrQuery(_g.d.gl_journal._doc_date);
            string __docNo = (mode == 3) ? "\'\'" : this._screenTop._getDataStrQuery(_g.d.gl_journal._doc_no);
            decimal __amount = (mode == 3) ? 0M : this._screenTop._getDataNumber(_g.d.gl_journal._debit);
            if (__columnDebit != -1)
                __amount = ((MyLib._myGrid._columnType)this._glDetail1._glDetailGrid._columnList[__columnDebit])._total; // this._screenTop._getDataNumber(_g.d.gl_journal._total_amount);

            string __docRef = this._screenTop._getDataStr(_g.d.gl_journal._ref_no);
            if (mode == 1 && __docRef.Length > 0)
            {
                // ดึงรายการจาก doc_ref มาให้
                String __docRefQuery = "select doc_no, " + _g.d.gl_journal._doc_date + "," + _g.d.gl_journal._debit +
                    " from " + _g.d.gl_journal._table + " where " + _g.d.gl_journal._doc_no + "=\'" + __docRef.ToUpper() + "\'";
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __ref = __myFrameWork._queryShort(__docRefQuery).Tables[0];
                if (__ref.Rows.Count > 0)
                {
                    this._logDocNoOld = "\'" + __ref.Rows[0][_g.d.gl_journal._doc_no].ToString() + "\'";
                    this._logDocDateOld = "\'" + __ref.Rows[0][_g.d.gl_journal._doc_date].ToString() + "\'";
                    this._logAmountOld = MyLib._myGlobal._decimalPhase(__ref.Rows[0][_g.d.gl_journal._debit].ToString());
                }
            }

            if (this._logDocNoOld.Length == 0)
            {
                this._logDocNoOld = "\'\'";
            }
            if (this._logDocDateOld.Length == 0)
            {
                this._logDocDateOld = "null";
            }

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
                "\'menu_gl_journal\',99," + mode.ToString() + ",\'" +
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


        void _screenTop__saveKeyDown(object sender)
        {
            _save_data();
        }

        void _save_data()
        {
            // กำหนด Focus ใหม่ ไม่งั้นข้อมูลจะไม่สมบูรณ์
            this._glDetail1._glDetailGrid.Focus();
            this._screenTop._saveLastControl();

            if (this._vatBuy != null)
            {
                this._vatBuy._vatGrid._removeLastControl();
            }

            if (this._vatSale != null)
            {
                this._vatSale._vatGrid._removeLastControl();
            }

            if (_myManageData1._manageButton.Enabled)
            {
                string __docFormatCodeSelect = this._screenTop._getDataStr(_g.d.gl_journal._doc_format_code);
                if (__docFormatCodeSelect.Equals("") && _oldTransFlag == 0)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("กรุณาเลือก") + " " + MyLib._myGlobal._resource("เลขที่เอกสาร"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                decimal __getTotal = MyLib._myGlobal._decimalPhase(_glDetail1._total_amount.textBox.Text);

                if (__getTotal != 0)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("ยอด Debit, Credit ไม่เท่ากัน\nไม่สามารถบันทึกรายการได้"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string __getEmtry = this._screenTop._checkEmtryField();
                    if (__getEmtry.Length > 0)
                    {
                        MyLib._myGlobal._displayWarning(2, __getEmtry);
                    }
                    else
                    {
                        if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                        {
                            if (_g.g._checkOpenPeriod(this._screenTop._getDataDate(_g.d.gl_journal._doc_date)) == false)
                            {
                                return;
                            }
                        }
                        ArrayList __getData = this._screenTop._createQueryForDatabase();
                        StringBuilder __myQuery = new StringBuilder();
                        __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        Control __getControl = this._screenTop._getControl(_g.d.gl_journal._doc_date);
                        DateTime __getDate = ((MyLib._myDateBox)__getControl)._dateTime;
                        int __periodNumber = _g.g._accountPeriodFind(__getDate);
                        string __docNo = this._screenTop._getDataStrQuery(_g.d.gl_journal._doc_no);
                        string __docDate = this._screenTop._getDataStrQuery(_g.d.gl_journal._doc_date);

                        // grid
                        string __fieldList = _g.d.gl_journal._doc_date + "," + _g.d.gl_journal._book_code + "," + _g.d.gl_journal._doc_no + "," + _g.d.gl_journal._period_number + "," + _g.d.gl_journal._journal_type + ",";
                        string __dataList = this._screenTop._getDataStrQuery(_g.d.gl_journal._doc_date) + "," + this._screenTop._getDataStrQuery(_g.d.gl_journal._book_code) + "," + this._screenTop._getDataStrQuery(_g.d.gl_journal._doc_no) + "," + __periodNumber.ToString() + "," + this._screenTop._getDataStr(_g.d.gl_journal._journal_type) + ",";
                        //
                        int __getColumnNumberDebit = this._glDetail1._glDetailGrid._findColumnByName(_g.d.gl_journal_detail._debit);
                        int __getColumnNumberCredit = this._glDetail1._glDetailGrid._findColumnByName(_g.d.gl_journal_detail._credit);
                        //
                        //if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLGeneralLedger)
                        {
                            // toe fix save vat wht
                            if (_myManageData1._mode == 2)
                            {
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_vat_buy._table + " where " + _g.d.gl_journal_vat_buy._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.gl_journal_vat_buy._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.บัญชี_ข้อมูลรายวัน)));
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_vat_sale._table + " where " + _g.d.gl_journal_vat_sale._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.gl_journal_vat_sale._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.บัญชี_ข้อมูลรายวัน)));
                                __myQuery.Append(this._whtIn._queryDelete(this._oldDocNo, ((_oldTransFlag == 0) ? _g.g._transControlTypeEnum.GL_ภาษีหักณที่จ่าย : _g.g._transFlagGlobal._transFlagByNumber(_oldTransFlag))));
                                __myQuery.Append(this._whtOut._queryDelete(this._oldDocNo, ((_oldTransFlag == 0) ? _g.g._transControlTypeEnum.GL_ภาษีถูกหักณที่จ่าย : _g.g._transFlagGlobal._transFlagByNumber(_oldTransFlag))));

                            }

                        }

                        if (_myManageData1._mode == 1)
                        {
                            string __extData = ((MyLib._myGrid._columnType)this._glDetail1._glDetailGrid._columnList[__getColumnNumberDebit])._total.ToString() + "," +
                                ((MyLib._myGrid._columnType)this._glDetail1._glDetailGrid._columnList[__getColumnNumberCredit])._total.ToString();// +"," + __periodNumber.ToString();
                            string __extField = _g.d.gl_journal._debit + "," + _g.d.gl_journal._credit;// +"," + _g.d.gl_journal._period_number;
                            __myQuery.Append("<query>insert into " + _g.d.gl_journal._table + " (" + __getData[0].ToString() + "," + __extField + ") values (" + __getData[1].ToString() + "," + __extData + ")</query>");
                            // TransSub
                            __myQuery.Append(this._glDetail1._glDetailGrid._createQueryForInsert(_g.d.gl_journal_detail._table, __fieldList, __dataList));
                        }
                        else
                        {
                            string __extStr = _g.d.gl_journal._debit + "=" + ((MyLib._myGrid._columnType)this._glDetail1._glDetailGrid._columnList[__getColumnNumberDebit])._total.ToString() + "," +
                                _g.d.gl_journal._credit + "=" + ((MyLib._myGrid._columnType)this._glDetail1._glDetailGrid._columnList[__getColumnNumberCredit])._total.ToString();
                            //+ "," +                                _g.d.gl_journal._period_number + "=" + __periodNumber.ToString();
                            __myQuery.Append("<query>update " + _g.d.gl_journal._table + " set " + __extStr + "," + __getData[2].ToString() + _myManageData1._dataList._whereString + "</query>");
                            // ตรวจสอบว่าสาระสำคัญมีการเปลี่ยนหรือไม่ (วันที่,เลขที่,รหัสสมุด)
                            this._glDetail1._glDetailGrid._updateRowIsChangeAll(true);
                            _myManageData1._isLockWhereString = " where guid_code=\'" + MyLib._myGlobal._guid + "\'";
                            __myQuery.Append(this._glDetail1._glDetailGrid._createQueryRowRemove(_g.d.gl_journal_detail._table));
                            // อย่าลืม Event _queryForUpdateWhere ไม่งั้นมันไม่ทำงานนะ
                            string __fieldUpdate = _g.d.gl_journal._doc_date + "=" + this._screenTop._getDataStrQuery(_g.d.gl_journal._doc_date) + "," + _g.d.gl_journal._book_code + "=" +
                                this._screenTop._getDataStrQuery(_g.d.gl_journal._book_code) + "," + _g.d.gl_journal._doc_no + "=" + this._screenTop._getDataStrQuery(_g.d.gl_journal._doc_no) + "," +
                                _g.d.gl_journal._period_number + "=" + __periodNumber.ToString() + "," + _g.d.gl_journal._journal_type + "=" + this._screenTop._getDataStr(_g.d.gl_journal._journal_type);
                            // Trans
                            __myQuery.Append(this._glDetail1._glDetailGrid._createQueryForUpdate(_g.d.gl_journal_detail._table, __fieldUpdate));
                            // ต่อท้ายด้วย Insert บรรทัดใหม่ (TransSub)
                            __myQuery.Append(this._glDetail1._glDetailGrid._createQueryForInsert(_g.d.gl_journal_detail._table, __fieldList, __dataList, true));
                            // Delete xxx List first
                            __myQuery.Append(_glDetail1._deleteGlExtraList(this._oldDocNo));
                        }
                        // Extra
                        __myQuery.Append(_glDetail1._saveGlExtraListQuery(this._glDetail1._glDetailGrid, __fieldList, __dataList));
                        // Process (ชั่วคราว)
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal._table + " set " + _g.d.gl_journal._is_pass + "=0 where " + _g.d.gl_journal._is_pass + " is null"));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal_detail._table + " set " + _g.d.gl_journal._is_pass + "=0 where " + _g.d.gl_journal_detail._is_pass + " is null"));

                        // if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLGeneralLedger)
                        {

                            string __getDocNo = this._screenTop._getDataStr(_g.d.gl_journal._doc_no);
                            string __getDocDate = this._screenTop._getDataStr(_g.d.gl_journal._doc_date);
                            // toe fix save vat wht
                            __myQuery.Append(this._whtIn._queryInsert(__getDocNo, __docDate, ((_oldTransFlag == 0) ? _g.g._transControlTypeEnum.GL_ภาษีหักณที่จ่าย : _g.g._transFlagGlobal._transFlagByNumber(_oldTransFlag))));
                            __myQuery.Append(this._whtOut._queryInsert(__getDocNo, __docDate, ((_oldTransFlag == 0) ? _g.g._transControlTypeEnum.GL_ภาษีถูกหักณที่จ่าย : _g.g._transFlagGlobal._transFlagByNumber(_oldTransFlag))));

                            // ภาษีซื้อ
                            this._vatBuy._deleteRowIfBlank();
                            this._vatBuy._vatGrid._updateRowIsChangeAll(true);
                            //this._vatBuy._checkApDetail(this._myManageTrans._mode);
                            string __vatFieldList = _g.d.gl_journal_vat_buy._book_code + "," + _g.d.gl_journal_vat_buy._vat_calc + "," + _g.d.gl_journal_vat_buy._trans_type + "," + _g.d.gl_journal_vat_buy._trans_flag + "," + _g.d.gl_journal_vat_buy._doc_date + "," + _g.d.gl_journal_vat_buy._doc_no + ",";
                            string __vatDataList = "\'\',1," + "1" + "," + ((_oldTransFlag == 0) ? _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.บัญชี_ข้อมูลรายวัน) : _oldTransFlag) + "," + __docDate + "," + __docNo + ",";
                            __myQuery.Append(this._vatBuy._vatGrid._createQueryForInsert(_g.d.gl_journal_vat_buy._table, __vatFieldList, __vatDataList, false, true));

                            // ภาษีขาย
                            this._vatSale._vatGrid._updateRowIsChangeAll(true);
                            this._vatSale._deleteRowIfBlank();
                            //this._vatSale._checkArDetail(this._myManageTrans._mode);
                            __vatFieldList = _g.d.gl_journal_vat_sale._book_code + "," + _g.d.gl_journal_vat_sale._vat_calc + "," + _g.d.gl_journal_vat_sale._trans_type + "," + _g.d.gl_journal_vat_sale._trans_flag + "," + _g.d.gl_journal_vat_sale._doc_date + "," + _g.d.gl_journal_vat_sale._doc_no + ",";
                            __vatDataList = "\'\',1," + "2" + "," + ((_oldTransFlag == 0) ? _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.บัญชี_ข้อมูลรายวัน) : _oldTransFlag) + "," + __docDate + "," + __docNo + ",";
                            __myQuery.Append(this._vatSale._vatGrid._createQueryForInsert(_g.d.gl_journal_vat_sale._table, __vatFieldList, __vatDataList, false, true));

                            /*
                            string __selectWhtDueDate = "(select " + _g.d.gl_wht_list._due_date + " from " + _g.d.gl_wht_list._table + " where " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._doc_no + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._doc_no + " and " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._tax_doc_no + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._tax_doc_no + ")";
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_wht_list_detail._table + " set " + _g.d.gl_wht_list_detail._due_date + "=" + __selectWhtDueDate + " where " + _g.d.gl_wht_list_detail._due_date + " is null or " + _g.d.gl_wht_list_detail._due_date + "<>" + __selectWhtDueDate));
                            //
                            string __selectWhtCustCode = "(select " + _g.d.gl_wht_list._cust_code + " from " + _g.d.gl_wht_list._table + " where " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._doc_no + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._doc_no + " and " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._tax_doc_no + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._tax_doc_no + ")";
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_wht_list_detail._table + " set " + _g.d.gl_wht_list_detail._cust_code + "=" + __selectWhtCustCode + " where " + _g.d.gl_wht_list_detail._cust_code + " is null or " + _g.d.gl_wht_list_detail._cust_code + "<>" + __selectWhtCustCode));
                            */
                        }
                        __myQuery.Append("</node>");
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                        if (__result.Length == 0)
                        {
                            MyLib._myGlobal._displayWarning(1, null);

                            // Save Log
                            this._saveLog(this._myManageData1._mode);

                            // print
                            _printFormData(this._screenTop.screenCode);

                            this._screenTop._isChange = false;
                            if (_myManageData1._mode == 1)
                            {
                                _myManageData1._afterInsertData();
                                string __getOldNumber = this._screenTop._getDataStr(_g.d.gl_journal._doc_no);
                                string __getOldNumberRefer = this._screenTop._getDataStr(_g.d.gl_journal._ref_no);
                                if (this._clearDataAfterSaveButton._iconNumber == 0)
                                {
                                    this._glDetail1._glDetailGrid._clear();
                                    this._screenTop._clear();
                                }
                                if (this._autoRunningButton._iconNumber == 0)
                                {
                                    this._screenTop._setDataStr(_g.d.gl_journal._doc_no, MyLib._myGlobal._autoRunningNumberStyleRightToLeft(__getOldNumber));
                                    this._screenTop._setDataStr(_g.d.gl_journal._ref_no, MyLib._myGlobal._autoRunningNumberStyleRightToLeft(__getOldNumberRefer));
                                }
                                this._screenTop._focusFirst();
                            }
                            else
                            {
                                _myManageData1._afterUpdateData();
                            }
                        }
                        else
                        {
                            MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        void _printFormData(string docTypeCode)
        {
            string __docNo = this._screenTop._getDataStr(_g.d.gl_journal._doc_no).Trim();

            List<SMLERPReportTool._ReportToolCondition> __condition = new List<SMLERPReportTool._ReportToolCondition>();
            __condition.Add(new SMLERPReportTool._ReportToolCondition(_g.d.gl_journal._doc_no, __docNo));

            SMLERPReportTool._global._printForm(docTypeCode, __condition.ToArray(), _showPrintDialogByCtrl, _g.g._companyProfile._voucher_form_code);
        }

        void _rePrintForm()
        {
            if (this._myToolBar.Enabled == false && this._oldDocNo.Length > 0)
            {
                string __doc_format_code = this._screenTop._getDataStr(_g.d.gl_journal._doc_format_code);

                List<SMLERPReportTool._ReportToolCondition> __condition = new List<SMLERPReportTool._ReportToolCondition>();
                __condition.Add(new SMLERPReportTool._ReportToolCondition(_g.d.gl_journal._doc_no, this._oldDocNo));

                bool __printResult = SMLERPReportTool._global._printForm(__doc_format_code, __condition.ToArray(), false, _g.g._companyProfile._voucher_form_code);

            }
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys keyCode = (Keys)(int)keyData & Keys.KeyCode;
            if ((keyData & Keys.Control) == Keys.Control)
            {
                switch (keyCode)
                {
                    case Keys.Home:
                        {
                            this._screenTop._focusFirst();
                            return true;
                        }
                    case Keys.I:
                        _recurring1._recurringActive();
                        return true;
                    case Keys.K:
                        _recurring1._deleteData();
                        return true;
                    case Keys.G:
                        _glDetail1._chartOfAccountScreenShow();
                        return true;
                    case Keys.P:
                        this._rePrintForm();
                        return true;
                }
            }
            if (keyData == Keys.F9)
            {
                _glDetail1._chartOfAccountScreenShow();
                return true;
            }
            else
                if (keyData == Keys.F12)
            {
                _save_data();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        Boolean _screenTop__checkKeyDown(object sender, Keys keyData)
        {
            if (_myManageData1._manageButton.Enabled == false)
            {
                MyLib._myGlobal._displayWarning(4, null);
            }
            if (keyData == Keys.PageDown)
            {
                _glDetailFocus();
            }
            return true;
        }

        bool _screenTop__checkKeyDownReturn(object sender, Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Tab)
            {
                if (sender != null)
                {
                    if (sender.GetType() == typeof(MyLib._myTextBox))
                    {
                        MyLib._myTextBox __getTextBox = (MyLib._myTextBox)sender;
                        if (__getTextBox._name.Equals(_g.d.gl_journal._description))
                        {
                            _glDetailFocus();
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        void _glDetailFocus()
        {
            if (_glDetail1._glDetailGrid._selectRow == -1)
            {
                _glDetail1._glDetailGrid._selectRow = 0;
                _glDetail1._glDetailGrid._selectColumn = 0;
            }
            _glDetail1._glDetailGrid._inputCell(_glDetail1._glDetailGrid._selectRow, _glDetail1._glDetailGrid._selectColumn);
        }

        void _myManageData1__clearData()
        {
            this._screenTop._clear();
            this._screenTop._setCheckBox(_g.d.gl_journal._trans_direct, true);
            _glDetail1._glDetailGrid._clear();
            _oldTransFlag = 0;


            // Log
            this._logDocNoOld = "\'\'";
            this._logDocDateOld = "\'\'";
            this._logAmountOld = 0M;
            this._logDetailOld = new StringBuilder();

            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLGeneralLedger)
            {
                this._vatSale._vatGrid._clear();
                this._vatBuy._vatGrid._clear();
                this._whtIn._clear();
                this._whtOut._clear();

            }
        }

        bool _myManageData1__discardData()
        {
            bool result = true;
            if (_screenTop._isChange)
            {
                if (DialogResult.No == MyLib._myGlobal._displayWarning(3, null))
                {
                    result = false;
                }
                else
                {
                    _screenTop._isChange = false;
                }
            }
            return (result);
        }

        void _myManageData1__newDataClick()
        {
            _oldTransFlag = 0;
            Control __codeControl = _screenTop._getControl(_g.d.gl_journal._book_code);
            __codeControl.Enabled = true;
            for (int __row = 0; __row < _glDetail1._glDetailGrid._rowData.Count; __row++)
            {
                _glDetail1._glDetailGrid._cellUpdate(__row, _g.d.gl_journal_detail._credit, 0.0M, false);
                _glDetail1._glDetailGrid._cellUpdate(__row, _g.d.gl_journal_detail._debit, 0.0M, false);
            }
            _glDetail1._glDetailGrid.Invalidate();
            _screenTop._focusFirst();
            this._screenTop._isChange = false;
            this._screenTop._setCheckBox(_g.d.gl_journal._trans_direct, true);
        }

        void _dataList__deleteData(ArrayList selectRowOrder)
        {
            _get_column_number();

            int __columnDocAmount = this._myManageData1._dataList._gridData._findColumnByName(_g.d.gl_journal._table + "." + _g.d.gl_journal._debit);
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
            {
                MyLib._deleteDataType __getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                string __getBookCode = this._myManageData1._dataList._gridData._cellGet(__getData.row, _getColumnBookCode).ToString();
                DateTime __getDate = (DateTime)this._myManageData1._dataList._gridData._cellGet(__getData.row, _getColumnDocDate);

                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                {
                    if (_g.g._checkOpenPeriod(__getDate) == false)
                    {
                        return;
                    }
                }
                string __getDocNo = this._myManageData1._dataList._gridData._cellGet(__getData.row, _getColumnDocNo).ToString();
                __myQuery.Append(string.Format("<query>delete from {0} {1}</query>", this._screenTop._table_name, __getData.whereString));
                string myFormat = MyLib._myUtil._convertTextToXmlForQuery("delete from {0} where " + _g.d.gl_journal._doc_date + "=\'" + MyLib._myGlobal._convertDateToQuery(__getDate) + "\'" +
                    " and  " + _g.d.gl_journal._doc_no + "=\'" + __getDocNo + "\' and " + _g.d.gl_journal._book_code + "=\'" + __getBookCode + "\'");
                // Delete xxx List
                this._glDetail1._deleteGlExtraList(__getDocNo);
                //
                __myQuery.Append(string.Format(myFormat, _g.d.gl_journal_detail._table));

                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLGeneralLedger)
                {
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_vat_buy._table + " where " + _g.d.gl_journal_vat_buy._doc_no + "=\'" + __getDocNo + "\'"));
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_vat_sale._table + " where " + _g.d.gl_journal_vat_sale._doc_no + "=\'" + __getDocNo + "\'"));

                    __myQuery.Append(this._whtIn._queryDelete(__getDocNo, _g.g._transControlTypeEnum.GL_ภาษีหักณที่จ่าย));
                    __myQuery.Append(this._whtOut._queryDelete(__getDocNo, _g.g._transControlTypeEnum.GL_ภาษีถูกหักณที่จ่าย));
                }

                // log
                this._logDocNoOld = "\'" + __getDocNo + "\'";
                this._logDocDateOld = "\'\'";
                if (_getColumnDocDate != -1) this._logDocDateOld = "\'" + MyLib._myGlobal._convertDateToQuery((DateTime)(this._myManageData1._dataList._gridData._cellGet(__getData.row, _getColumnDocDate))) + "\'";
                this._logAmountOld = 0M;
                if (__columnDocAmount != -1) this._logAmountOld = MyLib._myGlobal._decimalPhase(this._myManageData1._dataList._gridData._cellGet(__getData.row, __columnDocAmount).ToString());

                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(this._createLog(3)));

            } // for
            __myQuery.Append("</node>");
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
            if (__result.Length == 0)
            {
                MyLib._myGlobal._displayWarning(0, null);
                _myManageData1._dataList._refreshData();
            }
            else
            {
                MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void _get_column_number()
        {
            _getColumnDocDate = _myManageData1._dataList._gridData._findColumnByName(_g.d.gl_journal._table + "." + _g.d.gl_journal._doc_date);
            _getColumnBookCode = _myManageData1._dataList._gridData._findColumnByName(_g.d.gl_journal._table + "." + _g.d.gl_journal._book_code);
            _getColumnDocNo = _myManageData1._dataList._gridData._findColumnByName(_g.d.gl_journal._table + "." + _g.d.gl_journal._doc_no);
            _getColumnTransFlag = _myManageData1._dataList._gridData._findColumnByName(_g.d.gl_journal._table + "." + _g.d.gl_journal._trans_flag);
        }

        bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                ArrayList __rowDataArray = (ArrayList)rowData;
                _get_column_number();
                // get old
                _oldBookCode = __rowDataArray[_getColumnBookCode].ToString();
                //_oldDate = MyLib._myGlobal._convertDate(__rowDataArray[_getColumnDocDate].ToString());
                _oldDate = (DateTime)__rowDataArray[_getColumnDocDate];
                _oldDocNo = __rowDataArray[_getColumnDocNo].ToString();
                _oldTransFlag = (_getColumnTransFlag != -1) ? MyLib._myGlobal._intPhase(__rowDataArray[_getColumnTransFlag].ToString()) : 0;
                //
                this._glDetail1._loadData(this._screenTop, forEdit, _oldDocNo, _oldTransFlag);

                //if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLGeneralLedger)
                {
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    StringBuilder __myquery = new StringBuilder();
                    __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.gl_journal_vat_buy._table + " where " + _g.d.gl_journal_vat_buy._doc_no + "=\'" + _oldDocNo + "\'"));
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.gl_journal_vat_sale._table + " where " + _g.d.gl_journal_vat_sale._doc_no + "=\'" + _oldDocNo + "\'"));

                    string __extraWhereWhtIn = "";
                    string __extraWhereWhtOut = "";

                    if (_oldTransFlag != 0)
                    {
                        string __transFlagSale = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + "," +
                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้).ToString() + "," +
                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้).ToString() + "," +
                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้).ToString() + "," +
                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน).ToString() + "," +
                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ).ToString() + "," +
                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น).ToString() + "," +
                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.GL_ภาษีถูกหักณที่จ่าย).ToString();
                        string __transFlagBuy = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ).ToString() + "," +
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด).ToString() + "," +
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด).ToString() + "," +
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้).ToString() + "," +
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น).ToString() + "," +
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.GL_ภาษีหักณที่จ่าย).ToString();

                        __extraWhereWhtIn = " and trans_flag in (" + __transFlagBuy + ") ";
                        __extraWhereWhtOut = " and trans_flag in (" + __transFlagSale + ")";
                    }

                    __myquery.Append(this._whtIn._queryLoad(_oldDocNo, ((_oldTransFlag == 0) ? _g.g._transControlTypeEnum.GL_ภาษีหักณที่จ่าย : _g.g._transFlagGlobal._transFlagByNumber(_oldTransFlag)), __extraWhereWhtIn));
                    __myquery.Append(this._whtOut._queryLoad(_oldDocNo, ((_oldTransFlag == 0) ? _g.g._transControlTypeEnum.GL_ภาษีถูกหักณที่จ่าย : _g.g._transFlagGlobal._transFlagByNumber(_oldTransFlag)), __extraWhereWhtOut));

                    /* 
                     __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.gl_wht_list._table + " where " + _g.d.gl_wht_list._doc_no + "=\'" + _oldDocNo + "\' and " + _g.d.gl_wht_list._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.GL_ภาษีหักณที่จ่าย)));
                     __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.gl_wht_list_detail._table + " where " + _g.d.gl_wht_list_detail._doc_no + "=\'" + _oldDocNo + "\' and " + _g.d.gl_wht_list_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.GL_ภาษีหักณที่จ่าย)));

                     __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.gl_wht_list._table + " where " + _g.d.gl_wht_list._doc_no + "=\'" + _oldDocNo + "\' and " + _g.d.gl_wht_list._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.GL_ภาษีถูกหักณที่จ่าย)));
                     __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.gl_wht_list_detail._table + " where " + _g.d.gl_wht_list_detail._doc_no + "=\'" + _oldDocNo + "\' and " + _g.d.gl_wht_list_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.GL_ภาษีถูกหักณที่จ่าย)));
                    
                     */
                    __myquery.Append("</node>");
                    ArrayList __getDataMain = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                    if (__getDataMain.Count > 0)
                    {
                        this._vatSale._vatGrid._clear();
                        this._vatBuy._vatGrid._clear();
                        this._whtIn._clear();
                        this._whtOut._clear();

                        // load data
                        this._vatBuy._vatGrid._loadFromDataTable(((DataSet)__getDataMain[0]).Tables[0]);
                        this._vatSale._vatGrid._loadFromDataTable(((DataSet)__getDataMain[1]).Tables[0]);

                        this._whtIn._loadToScreen(__getDataMain, 2);
                        this._whtOut._loadToScreen(__getDataMain, 4);

                    }

                    // Log
                    this._logDetailOld = new StringBuilder();
                    this._logDetailOld.Append(this._screenTop._logCreate("top"));
                    this._logDocDateOld = this._screenTop._getDataStrQuery(_g.d.gl_journal._doc_date);
                    this._logDocNoOld = this._screenTop._getDataStrQuery(_g.d.gl_journal._doc_no);

                    int __columnDebit = this._glDetail1._glDetailGrid._findColumnByName(_g.d.gl_journal_detail._debit);
                    this._logAmountOld = 0M;
                    if (__columnDebit != -1)
                        this._logAmountOld = ((MyLib._myGrid._columnType)this._glDetail1._glDetailGrid._columnList[__columnDebit])._total; // this._screenTop._getDataNumber(_g.d.gl_journal._total_amount);
                    //
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return (true);
        }

        private void _buttonSave_Click(object sender, EventArgs e)
        {
            _save_data();
        }

        private void _buttonPrint_Click(object sender, EventArgs e)
        {
            if (this._myManageData1._mode == 2)
            {


                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    _showPrintDialogByCtrl = true;
                }

                try
                {
                    string __doc_format_code = this._screenTop._getDataStr(_g.d.gl_journal._doc_format_code);
                    _printFormData(__doc_format_code);
                }
                catch
                {
                    if (MyLib._myGlobal._isUserTest)
                        System.Diagnostics.Debugger.Break();
                }
            }
            else
            {
                MessageBox.Show(MyLib._myGlobal._resource("ยังไม่มีการบันทึกเอกสาร"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _showPrintDialogByCtrl = false;
        }

        private void _buttonChartOfAccountPopUp_Click(object sender, EventArgs e)
        {
            _glDetail1._chartOfAccountScreenShow();
        }

        private void _clearDataAfterSaveButton_Click(object sender, EventArgs e)
        {
            this._clearDataAfterSaveButton._iconNumber = (this._clearDataAfterSaveButton._iconNumber == 0) ? 1 : 0;
            this._clearDataAfterSaveButton.Image = imageList1.Images[this._clearDataAfterSaveButton._iconNumber];
            this._clearDataAfterSaveButton.Invalidate();
        }

        private void _autoRunningButton_Click(object sender, EventArgs e)
        {
            this._autoRunningButton._iconNumber = (this._autoRunningButton._iconNumber == 0) ? 1 : 0;
            this._autoRunningButton.Image = imageList1.Images[this._autoRunningButton._iconNumber];
            this._autoRunningButton.Invalidate();
        }
    }
}
