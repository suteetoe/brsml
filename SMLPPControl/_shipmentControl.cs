using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLPPControl
{
    public partial class _shipmentControl : UserControl
    {
        public bool _showPrintDialogByCtrl = false;

        public string _oldDocNo = "";
        private SMLPPGlobal.g._ppControlTypeEnum _transControlType = SMLPPGlobal.g._ppControlTypeEnum.ว่าง;

        public TabPage tab_shipment_address;
        public TabPage tab_more;
        public _icTransShipmentControl _shipmentAddress;
        public _shipmentScreenMore _screenMore;

        int _getTransFlag = 0;
        public SMLPPGlobal.g._ppControlTypeEnum transControlType
        {
            get
            {
                return this._transControlType;
            }
            set
            {
                this._transControlType = value;
                this.build();
            }
        }
        public _shipmentControl()
        {
            InitializeComponent();
            this._myManageData._form2.Controls.Add(this._shipmentDetailControl1);
        }


        void build()
        {
            if (this.DesignMode == false)
            {
                if (this.transControlType != SMLPPGlobal.g._ppControlTypeEnum.ว่าง)
                {
                    this._shipmentDetailControl1.transControlType = this._transControlType;
                    string __screenTemplate = "";

                    this._myManageData._dataList._lockRecord = true;

                    switch (this.transControlType)
                    {
                        case SMLPPGlobal.g._ppControlTypeEnum.Shipment:
                            {
                                __screenTemplate = "screen_shipment";
                                this._shipmentDetailControl1._screenTop._screen_code = "PSHM";
                                this._shipmentDetailControl1._shipmentGrid._getCustCode += _shipmentGrid__getCustCode;
                                this._myManageData._dataList._referFieldAdd(_g.d.pp_shipment._doc_no, 1);
                                this._shipmentDetailControl1._printButton.Click += _printButton_Click;
                                this._getTransFlag = SMLPPGlobal.g._transFlagPP(this.transControlType);

                            }
                            break;
                        case SMLPPGlobal.g._ppControlTypeEnum.SaleShipment:
                            {
                                this._myManageData._dataList._buttonNew.Visible =
                                this._myManageData._dataList._buttonNewFromTemp.Visible =
                                this._myManageData._dataList._buttonSelectAll.Visible =
                                this._myManageData._dataList._buttonDelete.Visible = false;

                                __screenTemplate = _g.g._search_screen_sale;
                                this._shipmentDetailControl1._screenTop._screen_code = "SI"; // sale shipment
                                this._shipmentDetailControl1._screenTop._docFormatCode = "SSM";
                                this._shipmentDetailControl1._shipmentGrid._getCustCode += _shipmentGrid__getCustCode;
                                this._shipmentDetailControl1._printButton.Click += _printButton_Click;
                                this._myManageData._dataList._referFieldAdd(_g.d.ic_trans._doc_no, 1);
                                this._myManageData._dataList._referFieldAdd(_g.d.ic_trans._trans_flag, 4);

                                this._getTransFlag = 44;

                                // add tab shipment
                                this.tab_shipment_address = new TabPage();
                                this.tab_shipment_address.Name = "tab_shipment_address";
                                this.tab_shipment_address.Text = "2.tab_shipment_address";

                                this.tab_more = new TabPage();
                                this.tab_more.Name = "tab_more";
                                this.tab_more.Text = "3.tab_more";

                                _shipmentAddress = new _icTransShipmentControl();
                                _shipmentAddress._getCustCode += _shipmentGrid__getCustCode;
                                _shipmentAddress.Dock = DockStyle.Fill;

                                _screenMore = new _shipmentScreenMore();
                                _screenMore.transControlType = SMLPPGlobal.g._ppControlTypeEnum.SaleShipment;
                                _screenMore.Dock = DockStyle.Fill;


                                this._shipmentDetailControl1._myTab.TabPages.Add(this.tab_more);
                                this._shipmentDetailControl1._myTab.TabPages.Add(this.tab_shipment_address);

                                this.tab_shipment_address.Controls.Add(this._shipmentAddress);
                                this.tab_more.Controls.Add(this._screenMore);

                            }
                            break;
                    }

                    this._myManageData._dataList._loadViewAddColumn += _dataList__loadViewAddColumn;
                    this._myManageData._dataList._extraWhereEvent += _dataList__extraWhereEvent;
                    this._myManageData._dataList._loadViewFormat(__screenTemplate, MyLib._myGlobal._userSearchScreenGroup, true);
                    this._myManageData._dataList._deleteData += _dataList__deleteData;
                    this._myManageData._manageButton = this._shipmentDetailControl1._toolbar;

                    this._shipmentDetailControl1._closeButton.Click += _closeButton_Click;
                    this._myManageData._newDataClick += _myManageData__newDataClick;
                    this._myManageData._clearData += _myManageData__clearData;
                    this._myManageData._closeScreen += _myManageData__closeScreen;
                    this._shipmentDetailControl1._saveButton.Click += _saveButton_Click;
                    this._myManageData._loadDataToScreen += _myManageData__loadDataToScreen;
                    this._myManageData._dataList._gridData._beforeDisplayRow += _gridData__beforeDisplayRow;

                    this._myManageData._calcArea();
                    this._myManageData__newDataClick();
                }
            }

        }

        private MyLib.BeforeDisplayRowReturn _gridData__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData)
        {
            MyLib.BeforeDisplayRowReturn __result = senderRow;
            int __lastStatusColumn = sender._findColumnByName(_g.d.pp_shipment._table + "." + _g.d.pp_shipment._last_status);

            if (__lastStatusColumn != -1)
            {
                // เอกสารมีการยกเลิก
                int __status = MyLib._myGlobal._intPhase(sender._cellGet(row, __lastStatusColumn).ToString());
                if (__status == 1)
                {
                    __result.newColor = Color.Red;
                }
            }

            return (__result);
        }

        private void _dataList__deleteData(ArrayList selectRowOrder)
        {
            MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();

            StringBuilder __myQuery = new StringBuilder();

            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            int __columnDocNo = this._myManageData._dataList._gridData._findColumnByName(_g.d.pp_shipment._table + "." + _g.d.pp_shipment._doc_no);

            for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
            {
                MyLib._deleteDataType __getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                string __getDocNo = this._myManageData._dataList._gridData._cellGet(__getData.row, __columnDocNo).ToString();

                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.pp_shipment._table + " where doc_no = \'" + __getDocNo + "\' and trans_flag = 1901 "));
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.pp_shipment_detail._table + " where doc_no = \'" + __getDocNo + "\' and trans_flag = 1901 "));

            }
            __myQuery.Append("</node>");

            string __result = myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());

            if (__result.Length == 0)
            {
                //
                MyLib._myGlobal._displayWarning(0, null);
                this._myManageData._dataList._refreshData();
            }
            else
            {
                MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void _printFormData(string docTypeCode)
        {
            MyLib._myFrameWork __myFramework = new MyLib._myFrameWork();
            // check 
            bool __printForm = false;
            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
            {
                string __query = "select " + _g.d.erp_doc_format._form_code + " from " + _g.d.erp_doc_format._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_doc_format._code) + "=\'" + this._shipmentDetailControl1._screenTop._docFormatCode + "\'";
                DataTable __result = __myFramework._queryShort(__query).Tables[0];
                if (__result.Rows.Count > 0)
                {
                    if (__result.Rows[0][_g.d.erp_doc_format._form_code].ToString().Length > 0)
                    {
                        __printForm = true;
                        docTypeCode = this._shipmentDetailControl1._screenTop._docFormatCode;
                    }
                }
            }


            string __docNo = this._shipmentDetailControl1._screenTop._getDataStr(_g.d.ic_trans._doc_no).ToString().Trim();

            bool __printResult = SMLERPReportTool._global._printForm(docTypeCode, __docNo, this._getTransFlag.ToString(), _showPrintDialogByCtrl);
            if (__printResult == true)
            {
                // update print count
                // MyLib._myFrameWork __myFramework = new MyLib._myFrameWork();
                __myFramework._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "insert into " + _g.d.erp_print_logs._table + "(" + _g.d.erp_print_logs._doc_no + "," + _g.d.erp_print_logs._trans_flag + "," + _g.d.erp_print_logs._user_code + "," + _g.d.erp_print_logs._print_datetime + ") values (\'" + __docNo + "\'," + this._getTransFlag.ToString() + ",\'" + MyLib._myGlobal._userCode + "\', \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\')");
            }

        }


        private void _printButton_Click(object sender, EventArgs e)
        {
            // print data
            _printFormData(this._shipmentDetailControl1._screenTop._docFormatCode);

        }

        string _dataList__extraWhereEvent()
        {
            string __result = "";
            if (this.transControlType == SMLPPGlobal.g._ppControlTypeEnum.SaleShipment)
            {
                __result = _g.d.ic_trans._trans_flag + "=44 ";
            }

            return __result;
        }

        void _dataList__loadViewAddColumn(MyLib._myGrid myGrid)
        {
            switch (this.transControlType)
            {
                case SMLPPGlobal.g._ppControlTypeEnum.SaleShipment:

                    myGrid._addColumn(_g.d.ic_trans._table + "." + _g.d.ic_trans._trans_flag, 2, 1, 1, false, true, true);
                    break;
            }
        }

        bool _myManageData__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            ArrayList __rowDataArray = (ArrayList)rowData;
            this._oldDocNo = __rowDataArray[this._getColumnNumberDocNo()].ToString().ToUpper();

            this._myManageData__clearData();

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            StringBuilder __query = new StringBuilder();
            __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
            switch (this._transControlType)
            {
                case SMLPPGlobal.g._ppControlTypeEnum.Shipment:
                    {
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.pp_shipment._table + " " + whereString));
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.pp_shipment_detail._table + " where " + _g.d.pp_shipment_detail._doc_no + "=\'" + this._oldDocNo + "\'"));

                    }
                    break;
                case SMLPPGlobal.g._ppControlTypeEnum.SaleShipment:
                    {
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_trans._table + " " + whereString));
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_trans_detail._table + " where " + _g.d.pp_shipment_detail._doc_no + "=\'" + this._oldDocNo + "\' and trans_flag = 44 "));
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_trans_shipment._table + " where " + _g.d.ic_trans_shipment._doc_no + "=\'" + this._oldDocNo + "\' and trans_flag = 44 "));
                    }
                    break;

            }

            __query.Append("</node>");

            ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

            try
            {
                DataTable __dt1 = ((DataSet)__getData[0]).Tables[0];
                DataTable __dt2 = ((DataSet)__getData[1]).Tables[0];

                this._shipmentDetailControl1._screenTop._loadData(__dt1);
                this._shipmentDetailControl1._shipmentGrid._loadFromDataTable(__dt2);

                if (this._screenMore != null)
                {
                    this._screenMore._loadData(__dt1);
                }

                switch (this.transControlType)
                {
                    case SMLPPGlobal.g._ppControlTypeEnum.SaleShipment:
                        {
                            DataTable __shipment = ((DataSet)__getData[2]).Tables[0];


                            this._shipmentDetailControl1._screenButtom._loadData(__dt1);
                            this._shipmentAddress._shipmentScreen._loadData(__shipment);
                        }
                        break;
                }

                this._shipmentDetailControl1._screenTop._search(false);

                this._shipmentDetailControl1._screenTop._docFormatCode = "";
                try
                {
                    this._shipmentDetailControl1._screenTop._docFormatCode = __dt1.Rows[0][_g.d.ic_trans._doc_format_code].ToString();
                }
                catch
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }


            return true;
        }
        int _getColumnNumberDocNo()
        {
            if (this.transControlType == SMLPPGlobal.g._ppControlTypeEnum.SaleShipment)
                return this._myManageData._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
            return this._myManageData._dataList._gridData._findColumnByName(_g.d.pp_shipment._table + "." + _g.d.pp_shipment._doc_no);
        }

        string _shipmentGrid__getCustCode()
        {
            return this._shipmentDetailControl1._screenTop._getDataStr(_g.d.pp_shipment._cust_code);
        }

        void _saveButton_Click(object sender, EventArgs e)
        {
            _saveData();
        }

        void _saveData()
        {

            System.Collections.ArrayList __dataScreenTop = this._shipmentDetailControl1._screenTop._createQueryForDatabase();

            string __docNo = this._shipmentDetailControl1._screenTop._getDataStr(_g.d.pp_shipment._doc_no);
            int __transFlag = SMLPPGlobal.g._transFlagPP(this.transControlType);

            StringBuilder __queryList = new StringBuilder();
            __queryList.Append(MyLib._myGlobal._xmlHeader);
            __queryList.Append("<node>");


            switch (this.transControlType)
            {
                case SMLPPGlobal.g._ppControlTypeEnum.SaleShipment:
                    {
                        ArrayList __moreData = this._screenMore._createQueryForDatabase();
                        // update trans
                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._remark + "=\'" + this._shipmentDetailControl1._screenButtom._getDataStr(_g.d.ic_trans._remark) + "\', " + __moreData[2].ToString() + " where " + _g.d.ic_trans._doc_no + "=\'" + __docNo + "\' and trans_flag = 44 "));

                        // update ic_trans_shipment
                        ArrayList __shipmentData = this._shipmentAddress._shipmentScreen._createQueryForDatabase();

                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans_shipment._table + " set " + __shipmentData[2] + " where " + _g.d.ic_trans._doc_no + "=\'" + __docNo + "\' and trans_flag = 44 "));

                    }
                    break;
                default:
                    {
                        // for edit

                        if (this._myManageData._mode == 2)
                        {
                            //
                            __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.pp_shipment._table + " where " + _g.d.pp_shipment._doc_no + "=\'" + __docNo + "\'"));
                            __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.pp_shipment_detail._table + " where " + _g.d.pp_shipment_detail._doc_no + "=\'" + __docNo + "\'"));
                        }

                        // header
                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.pp_shipment._table + "(" + _g.d.pp_shipment._trans_flag + "," + __dataScreenTop[0] + ") values (" + __transFlag.ToString() + "," + __dataScreenTop[1] + " )"));

                        // detail
                        string __detailField = MyLib._myGlobal._fieldAndComma(_g.d.pp_shipment_detail._doc_no, _g.d.pp_shipment_detail._doc_date, _g.d.pp_shipment_detail._trans_flag);
                        string __detailValue = MyLib._myGlobal._fieldAndComma(
                            this._shipmentDetailControl1._screenTop._getDataStrQuery(_g.d.pp_shipment._doc_no),
                            this._shipmentDetailControl1._screenTop._getDataStrQuery(_g.d.pp_shipment._doc_date),
                            __transFlag.ToString()
                            );

                        this._shipmentDetailControl1._shipmentGrid._updateRowIsChangeAll(true);

                        __queryList.Append(this._shipmentDetailControl1._shipmentGrid._createQueryForInsert(_g.d.pp_shipment_detail._table, __detailField + ",", __detailValue + ",", false, true));
                    }
                    break;

            }
            __queryList.Append("</node>");

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __queryList.ToString());
            if (__result.Length > 0)
            {
                MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MyLib._myGlobal._displayWarning(1, null);
                _printFormData(this._shipmentDetailControl1._screenTop._docFormatCode);

                this._myManageData._dataList._refreshData();

                _myManageData__clearData();
                this._myManageData__newDataClick();

            }
        }

        void _myManageData__clearData()
        {
            this._shipmentDetailControl1._screenTop._clear();
            this._shipmentDetailControl1._shipmentGrid._clear();
            this._shipmentDetailControl1._screenButtom._clear();

        }

        void _myManageData__newDataClick()
        {
            this._shipmentDetailControl1._screenTop._newData();
        }

        void _myManageData__closeScreen()
        {
            this.Dispose();
        }

        void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        void _rePrintForm()
        {
            // ใช้ this._oldDocNo อ้างอิง
            if (this._shipmentDetailControl1._toolbar.Enabled == false && this._oldDocNo.Length > 0)
            {
                MyLib._myFrameWork __myFramework = new MyLib._myFrameWork();
                // check 
                bool __printForm = false;
                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                {
                    string __query = "select " + _g.d.erp_doc_format._form_code + " from " + _g.d.erp_doc_format._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_doc_format._code) + "=\'" + this._shipmentDetailControl1._screenTop._docFormatCode + "\'";
                    DataTable __result = __myFramework._queryShort(__query).Tables[0];
                    if (__result.Rows.Count > 0)
                    {
                        if (__result.Rows[0][_g.d.erp_doc_format._form_code].ToString().Length > 0)
                        {
                            __printForm = true;
                        }
                    }
                }

                {
                    string __docNo = this._oldDocNo;

                    bool __isPrint = true;


                    if (__isPrint)
                    {
                        //string __docNo = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_no).ToString().Trim();
                        bool __printResult = SMLERPReportTool._global._printForm(this._shipmentDetailControl1._screenTop._docFormatCode, __docNo, this._getTransFlag.ToString(), false);
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
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

                    case Keys.F12:
                        {
                            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                            {
                                //if (_my _myManageTrans__checkEditData(this._myManageTrans._dataList._gridData._selectRow, this._myManageTrans._dataList._gridData))
                                {
                                    // ยกเลิก เอกสาร
                                    _g._docCancelForm __docCancelForm = new _g._docCancelForm(this._oldDocNo);

                                    if (__docCancelForm.ShowDialog() == DialogResult.Yes)
                                    {
                                        if (__docCancelForm._comboCancelConfirm.SelectedIndex == 1)
                                        {
                                            // update cancel 
                                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                                            StringBuilder __queryListUpdate = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                                            __queryListUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("update pp_shipment set is_cancel = 1, " + _g.d.pp_shipment._cancel_code + "=\'" + MyLib._myGlobal._userCode + "\', " + _g.d.pp_shipment._cancel_datetime + "=\'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\' where doc_no = \'" + this._oldDocNo + "\' and trans_flag =" + this._getTransFlag));
                                            //__queryListUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal._table + " where " + _g.d.gl_journal._doc_no + " = \'" + this._oldDocNo + "\' and " + _g.d.gl_journal._trans_flag + " =" + this._getTransFlag));
                                            //__queryListUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._doc_no + " = \'" + this._oldDocNo + "\' and " + _g.d.gl_journal_detail._trans_flag + " =" + this._getTransFlag));

                                            //if (this._vatBuy != null)
                                            //{
                                            //    __queryListUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_vat_buy._table + " where " + _g.d.gl_journal_vat_buy._doc_no + " = \'" + this._oldDocNo + "\' and " + _g.d.gl_journal_vat_buy._trans_flag + " =" + this._getTransFlag));
                                            //}

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
                                                /*
                                                string __docNoList = "";

                                                __docNoList = this._docNoAdd(__docNoList, this._oldDocNo);

                                                // get ref doc and add to __docNoList
                                                if (this._oldDocRef.Length > 0)
                                                {
                                                    __docNoList = __docNoList + "," + this._oldDocRef;
                                                }
                                                */
                                                SMLProcess._docFlow __process = new SMLProcess._docFlow();
                                                __process._processAll(_g.g._transFlagGlobal._transFlagByNumber(this._getTransFlag), "", "\'" + this._oldDocNo + "\'");

                                                MessageBox.Show("ยกเลิกเอกสารสำเร็จ");
                                                this._myManageData._dataList._refreshData();

                                            }
                                            this._myManageData._dataList._refreshData();
                                        }
                                        return true;
                                    }
                                }
                                //else
                                //{
                                //    MessageBox.Show("เอกสารอ้างอิงไปแล้ว");
                                //}
                            }
                            return false;

                        }
                    case Keys.R:
                        {
                            _docFlowRecheck();
                        }
                        break;

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

                            StringBuilder __queryListUpdate = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                            __queryListUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("update pp_shipment set is_cancel = 0 where doc_no = \'" + this._oldDocNo + "\' and trans_flag =" + this._getTransFlag));
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

                                SMLProcess._docFlow __process = new SMLProcess._docFlow();
                                __process._processAll(_g.g._transFlagGlobal._transFlagByNumber(this._getTransFlag), "", "\'" + this._oldDocNo + "\'");
                                MessageBox.Show("เรียกคืนเอกสารสำเร็จ");
                                this._myManageData._dataList._refreshData();

                                // ประมวลผล GL ด้วย อย่าลืม
                            }
                            this._myManageData._dataList._refreshData();
                        }
                        return true;
                    }
                    return false;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _docFlowRecheck()
        {
            SMLProcess._docFlow __process = new SMLProcess._docFlow();
            __process._processAll(_g.g._transFlagGlobal._transFlagByNumber(this._getTransFlag), "", "\'" + this._oldDocNo + "\'");

            MessageBox.Show(this._oldDocNo + " : Recheck Doc Flow Success");

            this._myManageData._dataList._refreshData();
            // this._myManageTrans._dataList._gridData.Invalidate();
        }

    }
}
