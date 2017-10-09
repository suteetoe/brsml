using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGLControl
{
    public partial class _withHoldingTax : UserControl
    {
        public delegate DateTime _getDueDate();
        public event _getDueDate _getDueDateEventArgs;

        public SMLERPGLControl._withHoldingTaxType _whType;
        public string _custCode = "";
        public DateTime _docDate = DateTime.Now;
        private string _formatNumberAmount = _g.g._getFormatNumberStr(3);
        private string _objectField = "object";

        private MyLib._searchDataFull _searchCust;
        private string _searchFormatName;
        private string _searchColumnName;
        bool _onLoadData = false;

        bool _whtDocRunning = false;
        public string _screenCode = "RWHT";
        public _withHoldingTax(SMLERPGLControl._withHoldingTaxType mode)
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolbar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            this._whType = mode;
            string __custField = "";
            string __custNameField = "";

            switch (this._whType)
            {
                case _withHoldingTaxType.เจ้าหนี้_ยกเลิกจ่ายชำระหนี้:
                case _withHoldingTaxType.เจ้าหนี้_จ่ายชำระหนี้:
                case _withHoldingTaxType.ซื้อ:
                    __custField = _g.d.gl_wht_list._ap_code;
                    __custNameField = _g.d.gl_wht_list._ap_name;
                    this._searchFormatName = _g.g._screen_ap_supplier_search;
                    this._searchColumnName = _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code;
                    _whtDocRunning = true;
                    break;
                default:
                    __custField = _g.d.gl_wht_list._ar_code;
                    __custNameField = _g.d.gl_wht_list._ar_name;
                    this._searchFormatName = _g.g._search_screen_ar;
                    this._searchColumnName = _g.d.ar_customer._table + "." + _g.d.ar_customer._code;
                    break;
            }
            this._mainGrid._table_name = _g.d.gl_wht_list._table;
            this._mainGrid._addColumn(_g.d.gl_wht_list._tax_doc_no, 1, 20, 20, false, false);

            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLGeneralLedger)
            {
                this._mainGrid._addColumn(_g.d.gl_wht_list._cust_name, 1, 40, 15, false, false, true, false, "", "", "", __custNameField);
                this._mainGrid._addColumn(_g.d.gl_wht_list._cust_address, 1, 40, 25, false, false, true, false, "", "", "");
            }
            else
            {
                this._mainGrid._addColumn(_g.d.gl_wht_list._cust_code, 1, 40, 15, false, false, true, false, "", "", "", __custField);
                this._mainGrid._addColumn(_g.d.gl_wht_list._cust_name, 1, 40, 25, false, false, true, false, "", "", "", __custNameField);
                this._mainGrid._addColumn(_g.d.gl_wht_list._cust_address, 1, 40, 25, false, true, true, false, "", "", "");
            }

            this._mainGrid._addColumn(_g.d.gl_wht_list._due_date, 4, 20, 20, false, false);
            this._mainGrid._addColumn(_g.d.gl_wht_list._amount, 3, 1, 10, false, false, true, false, this._formatNumberAmount);
            this._mainGrid._addColumn(_g.d.gl_wht_list._tax_value, 3, 1, 10, false, false, true, false, this._formatNumberAmount);

            this._mainGrid._addColumn(this._objectField, 12, 1, 1, false, true);

            this._mainGrid._total_show = true;
            this._mainGrid._afterAddRow += new MyLib.AfterAddRowEventHandler(_mainGrid__afterAddRow);
            this._mainGrid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_mainGrid__alterCellUpdate);
            this._mainGrid._afterSelectRow += new MyLib.AfterSelectRowEventHandler(_mainGrid__afterSelectRow);
            this._mainGrid._afterCalcTotal += new MyLib.AfterCalcTotalEventHandler(_mainGrid__afterCalcTotal);
            this._mainGrid.Invalidated += new InvalidateEventHandler(_mainGrid_Invalidated);
            //
            this._detailScreen._maxColumn = 2;
            int __row = 0;

            this._detailScreen._table_name = _g.d.gl_wht_list._table;
            this._detailScreen._addDateBox(__row, 0, 1, 0, _g.d.gl_wht_list._due_date, 1, true);

            this._detailScreen._addTextBox(__row++, 1, 1, 0, _g.d.gl_wht_list._tax_doc_no, 1, 0, (_whtDocRunning) ? 1 : 0, true, false);

            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLGeneralLedger)
            {
                this._detailScreen._addTextBox(__row, 0, 1, 0, _g.d.gl_wht_list._cust_name, 1, 1, 0, true, false, false, true, true, __custNameField);
                this._detailScreen._addComboBox(__row++, 1, _g.d.gl_wht_list._cust_tax_type, true, new string[] { _g.d.ar_customer._personality, _g.d.ar_customer._juristic_person }, false);
                this._detailScreen._addTextBox(__row++, 0, 1, 0, _g.d.gl_wht_list._cust_address, 2, 1, 0, true, false, true, true, true);
            }
            else
            {
                this._detailScreen._addTextBox(__row, 0, 1, 0, _g.d.gl_wht_list._cust_code, 1, 1, 1, true, false, false, true, true, __custField);
                this._detailScreen._addComboBox(__row++, 1, _g.d.gl_wht_list._cust_tax_type, true, new string[] { _g.d.ar_customer._personality, _g.d.ar_customer._juristic_person }, false);
            }

            this._detailScreen._addTextBox(__row, 0, _g.d.gl_wht_list._tax_number, 1);
            this._detailScreen._addTextBox(__row++, 1, _g.d.gl_wht_list._card_number, 1);

            if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLGeneralLedger)
            {
                this._detailScreen._enabedControl(_g.d.gl_wht_list._tax_number, false);
                this._detailScreen._enabedControl(_g.d.gl_wht_list._card_number, false);
                this._detailScreen._enabedControl(_g.d.gl_wht_list._cust_tax_type, false);
            }

            this._detailScreen._textBoxChanged += new MyLib.TextBoxChangedHandler(_detailScreen__textBoxChanged);
            this._detailScreen._textBoxSearch += new MyLib.TextBoxSearchHandler(_detailScreen__textBoxSearch);
            this._detailScreen._checkKeyDownReturn += new MyLib.CheckKeyDownReturnHandler(_detailScreen__checkKeyDownReturn);
            this._detailScreen._comboBoxSelectIndexChanged += _detailScreen__comboBoxSelectIndexChanged;
            //
            this._detailGrid._table_name = _g.d.gl_wht_list_detail._table;
            this._detailGrid._addColumn(_g.d.gl_wht_list_detail._income_type, 1, 1, 50);
            this._detailGrid._addColumn(_g.d.gl_wht_list_detail._amount, 3, 1, 20, true, false, true, false, this._formatNumberAmount);
            this._detailGrid._addColumn(_g.d.gl_wht_list_detail._tax_rate, 3, 1, 10, true, false, true, false, this._formatNumberAmount);
            this._detailGrid._addColumn(_g.d.gl_wht_list_detail._tax_value, 3, 1, 20, true, false, true, false, this._formatNumberAmount);
            this._detailGrid._total_show = true;
            this._detailGrid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_detailGrid__alterCellUpdate);
            this._detailGrid._afterCalcTotal += new MyLib.AfterCalcTotalEventHandler(_detailGrid__afterCalcTotal);
            this._detailGrid._afterAddRow += _detailGrid__afterAddRow;
            //
            this._checkDetailEnable();
        }

        void _detailScreen__comboBoxSelectIndexChanged(object sender, string name)
        {
            this._saveDetail();
            this._refreshData();
        }

        public string _checkInputData()
        {
            StringBuilder __result = new StringBuilder();

            for (int __rowMain = 0; __rowMain < this._mainGrid._rowData.Count; __rowMain++)
            {
                _WHTObjectClass __objectMain = (_WHTObjectClass)this._mainGrid._cellGet(__rowMain, this._objectField);
                decimal __amount = MyLib._myGlobal._decimalPhase(this._mainGrid._cellGet(__rowMain, _g.d.gl_wht_list._amount).ToString());

                if (__amount > 0M && __objectMain._taxDocNo.Trim().Length == 0)
                {
                    __result.Append("เลขที่หักภาษี ณ ที่จ่าย มูลค่า : " + __amount.ToString("#,###,###,###,###.00"));
                }
            }
            return __result.ToString();
        }

        void _detailGrid__afterAddRow(object sender, int row)
        {
            this._detailGrid._cellUpdate(row, _g.d.gl_wht_list_detail._tax_rate, _g.g._companyProfile._wht_rate, false);
        }

        void _detailScreen__textBoxChanged(object sender, string name)
        {
            if (this._whtDocRunning && name.Equals(_g.d.gl_wht_list._tax_doc_no))
            {
                // running
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                DataTable __getFormat = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._format + "," + _g.d.erp_doc_format._code + "," + _g.d.erp_doc_format._tax_format + "," + _g.d.erp_doc_format._doc_running + "," + _g.d.erp_doc_format._vat_type + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._code + "=\'" + this._detailScreen._getDataStr(_g.d.gl_wht_list._tax_doc_no) + "\'").Tables[0];
                if (__getFormat.Rows.Count > 0)
                {
                    string __format = __getFormat.Rows[0][0].ToString();
                    string __docFormatCode = __getFormat.Rows[0][1].ToString();

                    DateTime __docDate = DateTime.Now;
                    if (_getDueDateEventArgs != null)
                    {
                        __docDate = _getDueDateEventArgs();
                    }

                    string __startRunningNumber = __getFormat.Rows[0][_g.d.erp_doc_format._doc_running].ToString();
                    string __newDoc = _g.g._getAutoRun(_g.g._autoRunType.ว่าง, __docFormatCode, this._detailScreen._getDataStr(_g.d.gl_wht_list._due_date).ToString(), __format, _g.g._transControlTypeEnum.ว่าง, _g.g._transControlTypeEnum.ว่าง, _g.d.gl_wht_list._table, __startRunningNumber, _g.d.gl_wht_list._tax_doc_no, "");
                    this._detailScreen._setDataStr(_g.d.gl_wht_list._tax_doc_no, __newDoc);
                    SendKeys.Send("{TAB}");
                }
            }

            this._saveDetail();
            this._refreshData();
        }

        public int _loadToScreen(ArrayList source, int tableNumber)
        {
            try
            {
                DataTable __main = ((DataSet)source[tableNumber]).Tables[0];
                DataTable __detail = ((DataSet)source[tableNumber + 1]).Tables[0];
                for (int __rowMain = 0; __rowMain < __main.Rows.Count; __rowMain++)
                {
                    DataRow __dataRowMain = __main.Rows[__rowMain];
                    _WHTObjectClass __objectMain = new _WHTObjectClass();
                    __objectMain._taxDocNo = __dataRowMain[_g.d.gl_wht_list._tax_doc_no].ToString();
                    __objectMain._custCode = __dataRowMain[_g.d.gl_wht_list._cust_code].ToString();
                    __objectMain._dueDate = MyLib._myGlobal._convertDateFromQuery(__dataRowMain[_g.d.gl_wht_list._due_date].ToString());
                    decimal __amount = MyLib._myGlobal._decimalPhase(__dataRowMain[_g.d.gl_wht_list._amount].ToString());
                    decimal __taxAmount = MyLib._myGlobal._decimalPhase(__dataRowMain[_g.d.gl_wht_list._tax_value].ToString());

                    __objectMain._custName = __dataRowMain[_g.d.gl_wht_list._cust_name].ToString();
                    __objectMain._custAddress = __dataRowMain[_g.d.gl_wht_list._cust_address].ToString();
                    __objectMain._tax_number = __dataRowMain[_g.d.gl_wht_list._tax_number].ToString();
                    __objectMain._card_number = __dataRowMain[_g.d.gl_wht_list._card_number].ToString();
                    __objectMain._cust_type = __dataRowMain[_g.d.gl_wht_list._cust_tax_type].ToString();

                    this._onLoadData = true;
                    __objectMain._detailList = new List<_WHTDetailObjectClass>();
                    //
                    int __addr = this._mainGrid._addRow();
                    this._mainGrid._cellUpdate(__addr, _g.d.gl_wht_list._tax_doc_no, __objectMain._taxDocNo, false);
                    this._mainGrid._cellUpdate(__addr, _g.d.gl_wht_list._cust_code, __objectMain._custCode, true);
                    this._mainGrid._cellUpdate(__addr, _g.d.gl_wht_list._cust_name, __objectMain._custName, true);
                    this._mainGrid._cellUpdate(__addr, _g.d.gl_wht_list._cust_address, __objectMain._custAddress, true);
                    this._mainGrid._cellUpdate(__addr, _g.d.gl_wht_list._due_date, __objectMain._dueDate, false);
                    //

                    if (__detail.Rows.Count > 0)
                    {

                        DataRow[] __detailSelect = __detail.Select("tax_doc_no=\'" + __objectMain._taxDocNo + "\'");
                        for (int __row = 0; __row < __detailSelect.Length; __row++)
                        {
                            DataRow __dataRowDetail = __detailSelect[__row];
                            _WHTDetailObjectClass __objectDetail = new _WHTDetailObjectClass();
                            __objectDetail._incomeType = __dataRowDetail[_g.d.gl_wht_list_detail._income_type].ToString();
                            __objectDetail._taxRate = MyLib._myGlobal._decimalPhase(__dataRowDetail[_g.d.gl_wht_list_detail._tax_rate].ToString());
                            __objectDetail._amount = MyLib._myGlobal._decimalPhase(__dataRowDetail[_g.d.gl_wht_list_detail._amount].ToString());
                            __objectDetail._taxValue = MyLib._myGlobal._decimalPhase(__dataRowDetail[_g.d.gl_wht_list_detail._tax_value].ToString());
                            //
                            __objectMain._detailList.Add(__objectDetail);
                        }
                    }
                    //
                    this._mainGrid._cellUpdate(__addr, this._objectField, __objectMain, false);
                    //
                    this._mainGrid._cellUpdate(__addr, _g.d.gl_wht_list._amount, __amount, false);
                    this._mainGrid._cellUpdate(__addr, _g.d.gl_wht_list._tax_value, __taxAmount, false);
                    this._onLoadData = false;

                }
                //this._mainGrid._selectRow = 0;
                this._mainGrid.Invalidate();
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
            return tableNumber + 2;
            /*this._payCashScreen._loadData(((DataSet)source[tableNumber]).Tables[0]);
            this._payTransferGrid._loadFromDataTable(((DataSet)source[tableNumber + 1]).Tables[0]);
            this._payChequeGrid._loadFromDataTable(((DataSet)source[tableNumber + 2]).Tables[0]);
            this._payCreditCardGrid._loadFromDataTable(((DataSet)source[tableNumber + 3]).Tables[0]);*/
        }

        public string _queryLoad(string docNo, _g.g._transControlTypeEnum transFlag)
        {
            return _queryLoad(docNo, transFlag, "");
        }

        public string _queryLoad(string docNo, _g.g._transControlTypeEnum transFlag, string extraWhere)
        {
            StringBuilder __query = new StringBuilder();
            string __transFlag = _g.g._transFlagGlobal._transFlag(transFlag).ToString();
            // หัว
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.gl_wht_list._table + " where " + _g.d.gl_wht_list._doc_no + "=\'" + docNo + "\' and " + _g.d.gl_wht_list._trans_flag + "=" + __transFlag.ToString() + extraWhere));
            // รายละเอียด
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.gl_wht_list_detail._table + " where " + _g.d.gl_wht_list_detail._doc_no + "=\'" + docNo + "\' and " + _g.d.gl_wht_list_detail._trans_flag + "=" + __transFlag.ToString() + extraWhere + " order by " + _g.d.gl_wht_list_detail._tax_doc_no + "," + _g.d.gl_wht_list_detail._line_number));
            return __query.ToString();
        }

        public string _queryInsert(string docNo, string docDate, _g.g._transControlTypeEnum transFlag)
        {
            string __transFlag = _g.g._transFlagGlobal._transFlag(transFlag).ToString();
            StringBuilder __query = new StringBuilder();
            //
            for (int __rowMain = 0; __rowMain < this._mainGrid._rowData.Count; __rowMain++)
            {
                _WHTObjectClass __objectMain = (_WHTObjectClass)this._mainGrid._cellGet(__rowMain, this._objectField);
                if (__objectMain._taxDocNo.Trim().Length > 0)
                {
                    StringBuilder __queryMain = new StringBuilder();
                    __queryMain.Append("insert into " + _g.d.gl_wht_list._table + " ");
                    // Field
                    // toe เพิ่ม docDate ลงไปใน gl_wht_list
                    __queryMain.Append("(" + _g.d.gl_wht_list._doc_no + "," + _g.d.gl_wht_list._doc_date + "," + _g.d.gl_wht_list._tax_doc_no + "," + _g.d.gl_wht_list._due_date + "," + _g.d.gl_wht_list._amount + ",");
                    __queryMain.Append(_g.d.gl_wht_list._tax_value + "," + _g.d.gl_wht_list._cust_code + "," + _g.d.gl_wht_list._trans_flag + ",");
                    __queryMain.Append(_g.d.gl_wht_list._cust_name + "," + _g.d.gl_wht_list._cust_address + "," + _g.d.gl_wht_list._cust_tax_type + "," + _g.d.gl_wht_list._card_number + "," + _g.d.gl_wht_list._tax_number);
                    __queryMain.Append(" ) values ");
                    // Value
                    // toe เพิ่ม docDate ลงไปใน gl_wht_list
                    decimal __amount = MyLib._myGlobal._decimalPhase(this._mainGrid._cellGet(__rowMain, _g.d.gl_wht_list._amount).ToString());
                    decimal __taxValue = MyLib._myGlobal._decimalPhase(this._mainGrid._cellGet(__rowMain, _g.d.gl_wht_list._tax_value).ToString());
                    __queryMain.Append("(\'" + docNo + "\'," + docDate + ",\'" + __objectMain._taxDocNo + "\',\'" + MyLib._myGlobal._convertDateToQuery(__objectMain._dueDate) + "\'," + __amount.ToString() + ",");
                    __queryMain.Append(__taxValue.ToString() + ",\'" + __objectMain._custCode + "\'," + __transFlag.ToString() + ",");

                    __queryMain.Append("\'" + this._mainGrid._cellGet(__rowMain, _g.d.gl_wht_list._cust_name).ToString() + "\', \'" + this._mainGrid._cellGet(__rowMain, _g.d.gl_wht_list._cust_address).ToString() + "\', " + MyLib._myGlobal._intPhase(__objectMain._cust_type).ToString() + " ,\'" + __objectMain._card_number + "\', \'" + __objectMain._tax_number + "\' " + ")");
                    //
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryMain.ToString()));
                    // WHT Detail
                    for (int __row = 0; __row < __objectMain._detailList.Count; __row++)
                    {
                        _WHTDetailObjectClass __objectDetail = (_WHTDetailObjectClass)__objectMain._detailList[__row];
                        if (__objectMain._taxDocNo.Trim().Length > 0)
                        {
                            StringBuilder __queryDetail = new StringBuilder();
                            __queryDetail.Append("insert into " + _g.d.gl_wht_list_detail._table + " ");
                            // Field
                            __queryDetail.Append("(" + _g.d.gl_wht_list_detail._line_number + "," + _g.d.gl_wht_list_detail._doc_no + "," + _g.d.gl_wht_list_detail._tax_doc_no + "," + _g.d.gl_wht_list_detail._trans_flag + ",");
                            __queryDetail.Append(_g.d.gl_wht_list_detail._income_type + "," + _g.d.gl_wht_list_detail._amount + "," + _g.d.gl_wht_list_detail._tax_rate + "," + _g.d.gl_wht_list_detail._tax_value + "," + _g.d.gl_wht_list_detail._due_date + "," + _g.d.gl_wht_list_detail._cust_code + ") values ");
                            // Value
                            int __lineNumber = __row + 1;
                            __queryDetail.Append("(" + __lineNumber.ToString() + ",\'" + docNo + "\',\'" + __objectMain._taxDocNo + "\'," + __transFlag + ",\'");
                            __queryDetail.Append(__objectDetail._incomeType + "\'," + __objectDetail._amount.ToString() + "," + __objectDetail._taxRate.ToString() + "," + __objectDetail._taxValue.ToString() + ",\'" + MyLib._myGlobal._convertDateToQuery(__objectMain._dueDate) + "\',\'" + __objectMain._custCode + "\')");
                            //                         
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryDetail.ToString()));
                        }
                    }
                }
            }
            return __query.ToString();
        }

        public string _queryDelete(string docNo, _g.g._transControlTypeEnum transFlag)
        {
            StringBuilder __query = new StringBuilder();
            string __transFlag = _g.g._transFlagGlobal._transFlag(transFlag).ToString();
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_wht_list._table + " where " + _g.d.gl_wht_list._doc_no + "=\'" + docNo + "\' and " + _g.d.gl_wht_list._trans_flag + "=" + __transFlag));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_wht_list_detail._table + " where " + _g.d.gl_wht_list_detail._doc_no + "=\'" + docNo + "\' and " + _g.d.gl_wht_list_detail._trans_flag + "=" + __transFlag));
            return __query.ToString();
        }

        void _detailGrid__afterCalcTotal(object sender)
        {
            if (this._mainGrid._selectRow != -1 && this._mainGrid._selectRow < this._mainGrid._rowData.Count)
            {
                if (this._detailScreen.Enabled)
                {
                    decimal __amount = ((MyLib._myGrid._columnType)this._detailGrid._columnList[this._detailGrid._findColumnByName(_g.d.gl_wht_list_detail._amount)])._total;
                    decimal __taxValue = ((MyLib._myGrid._columnType)this._detailGrid._columnList[this._detailGrid._findColumnByName(_g.d.gl_wht_list_detail._tax_value)])._total;
                    this._mainGrid._cellUpdate(this._mainGrid._selectRow, _g.d.gl_wht_list._amount, __amount, false);
                    this._mainGrid._cellUpdate(this._mainGrid._selectRow, _g.d.gl_wht_list._tax_value, __taxValue, false);
                    this._mainGrid._calcTotal(true);
                    this._mainGrid.Invalidate();
                }
            }
        }

        bool _detailScreen__checkKeyDownReturn(object sender, Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Tab)
            {
                if (sender != null)
                {
                    if (sender.GetType() == typeof(MyLib._myTextBox))
                    {
                        MyLib._myTextBox __getTextBox = (MyLib._myTextBox)sender;
                        if (__getTextBox._name.Equals(_g.d.gl_wht_list._cust_code))
                        {
                            if (this._detailGrid._rowData.Count == 0)
                            {
                                this._detailGrid._addRow();
                                this._detailGrid._selectRow = 0;
                                this._detailGrid._selectColumn = 0;
                            }
                            this._detailGrid._gotoCell(this._detailGrid._selectRow, this._detailGrid._selectColumn);
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        void _detailScreen__textBoxSearch(object sender)
        {
            MyLib._myTextBox __textbox = (MyLib._myTextBox)sender;

            if (__textbox._name == _g.d.gl_wht_list._cust_code)
            {
                if (this._searchCust == null)
                {
                    this._searchCust = new MyLib._searchDataFull();
                    this._searchCust._dataList._loadViewFormat(this._searchFormatName, MyLib._myGlobal._userSearchScreenGroup, false);
                    this._searchCust._dataList._refreshData();
                    this._searchCust._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._searchCust._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchCust__searchEnterKeyPress);
                    this._searchCust.WindowState = FormWindowState.Maximized;
                }
                this._searchCust.ShowDialog();
            }
            else if (__textbox._name == _g.d.gl_wht_list._tax_doc_no)
            {
                MyLib._searchDataFull __searchDoc = new MyLib._searchDataFull();
                __searchDoc._dataList._loadViewFormat(_g.g._search_screen_erp_doc_format, MyLib._myGlobal._userSearchScreenGroup, false);
                __searchDoc._dataList._extraWhere2 = MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code, false) + "=\'" + this._screenCode + "\'";
                __searchDoc._dataList._refreshData();
                __searchDoc._dataList._gridData._mouseClick += (s1, e1) =>
                {
                    if (e1._row != -1)
                    {
                        string __docFormatCode = __searchDoc._dataList._gridData._cellGet(e1._row, _g.d.erp_doc_format._table + "." + _g.d.erp_doc_format._code).ToString();
                        /*
                        string __docFormat = __searchDoc._dataList._gridData._cellGet(e1._row, _g.d.erp_doc_format._table + "." + _g.d.erp_doc_format._format).ToString();
                        this._conditionScreen._setDataStr(_g.d.ic_resource._doc_format_code, __docFormatCode);
                        this._conditionScreen._setDataStr(_g.d.ic_resource._doc_format, __docFormat);
                        */
                        this._detailScreen._setDataStr(_g.d.gl_wht_list._tax_doc_no, __docFormatCode);
                        __searchDoc.Close();
                    }

                };
                __searchDoc._searchEnterKeyPress += (s1, e1) =>
                {
                    if (e1 != -1)
                    {
                        string __docFormatCode = __searchDoc._dataList._gridData._cellGet(e1, _g.d.erp_doc_format._table + "." + _g.d.erp_doc_format._code).ToString();
                        /*
                        string __docFormat = __searchDoc._dataList._gridData._cellGet(e1, _g.d.erp_doc_format._table + "." + _g.d.erp_doc_format._format).ToString();
                        this._conditionScreen._setDataStr(_g.d.ic_resource._doc_format_code, __docFormatCode);
                        this._conditionScreen._setDataStr(_g.d.ic_resource._doc_format, __docFormat);/
                        */
                        this._detailScreen._setDataStr(_g.d.gl_wht_list._tax_doc_no, __docFormatCode);
                        __searchDoc.Close();
                    }

                };
                MyLib._myGlobal._startSearchBox(__textbox, "รูปแบบเลขที่เอกสาร", __searchDoc);
                // doc_no
            }
        }

        void _searchCust__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }

        void _searchByParent(object sender, int row)
        {
            this._searchCust.Close();
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._setDataFromSearch(sender, row);
        }

        void _setDataFromSearch(object sender, int row)
        {
            this._searchCust.Close();
            string __code = ((MyLib._myGrid)sender)._cellGet(row, this._searchColumnName).ToString();
            this._detailScreen._setDataStr(_g.d.gl_wht_list._cust_code, __code);
            SendKeys.Send("{TAB}");
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            this._setDataFromSearch(sender, e._row);
        }

        void _detailGrid__alterCellUpdate(object sender, int row, int column)
        {

            decimal __amount = MyLib._myGlobal._decimalPhase(this._detailGrid._cellGet(row, _g.d.gl_wht_list_detail._amount).ToString());
            decimal __taxRate = MyLib._myGlobal._decimalPhase(this._detailGrid._cellGet(row, _g.d.gl_wht_list_detail._tax_rate).ToString());
            decimal __taxValue = MyLib._myGlobal._decimalPhase(this._detailGrid._cellGet(row, _g.d.gl_wht_list_detail._tax_value).ToString());
            // กรณี อยู่ช่อง tax_value
            if (column == this._detailGrid._findColumnByName(_g.d.gl_wht_list_detail._tax_value) && __taxRate == 0)
            {
                decimal __taxRateNew = (__amount == 0) ? 0M : ((__taxValue * 100M) / __amount);
                this._detailGrid._cellUpdate(row, _g.d.gl_wht_list_detail._tax_rate, __taxRateNew, false);
            }
            // กรณีอยู่ช่อง จำนวนเงิน หรือ อัตราภาษี
            if (column == this._detailGrid._findColumnByName(_g.d.gl_wht_list_detail._amount) || column == this._detailGrid._findColumnByName(_g.d.gl_wht_list_detail._tax_rate))
            {
                __taxValue = __amount * (__taxRate / 100M);
                this._detailGrid._cellUpdate(row, _g.d.gl_wht_list_detail._tax_value, __taxValue, false);
            }
            this._saveDetail();
        }

        void _mainGrid_Invalidated(object sender, InvalidateEventArgs e)
        {
            this._checkDetailEnable();
        }

        void _mainGrid__afterCalcTotal(object sender)
        {
            this._checkDetailEnable();
        }

        void _checkDetailEnable()
        {
            this._detailScreen.Enabled = (this._mainGrid._selectRow == -1 || this._mainGrid._selectRow >= this._mainGrid._rowData.Count) ? false : true;
            this._detailGrid.Enabled = (this._mainGrid._selectRow == -1 || this._mainGrid._selectRow >= this._mainGrid._rowData.Count) ? false : true;

            this._printSelectRecordButton.Enabled = this._detailScreen.Enabled;
        }

        void _mainGrid__afterSelectRow(object sender, int row)
        {
            this._checkDetailEnable();
            this._loadDetail(row);
        }

        void _loadDetail(int row)
        {
            this._checkDetailEnable();
            if (this._detailScreen.Enabled)
            {
                _WHTObjectClass __object = (_WHTObjectClass)this._mainGrid._cellGet(row, this._objectField);
                this._onLoadData = true;
                this._detailScreen._clear();
                this._detailGrid._clear();
                this._detailScreen._setDataStr(_g.d.gl_wht_list._tax_doc_no, __object._taxDocNo, "", true);
                this._detailScreen._setDataDate(_g.d.gl_wht_list._due_date, __object._dueDate);
                this._detailScreen._setDataStr(_g.d.gl_wht_list._cust_code, __object._custCode, "", true);

                this._detailScreen._setDataStr(_g.d.gl_wht_list._cust_name, __object._custName, "", true);
                this._detailScreen._setDataStr(_g.d.gl_wht_list._cust_address, __object._custAddress, "", true);
                this._detailScreen._setDataStr(_g.d.gl_wht_list._card_number, __object._card_number, "", true);
                this._detailScreen._setDataStr(_g.d.gl_wht_list._tax_number, __object._tax_number, "", true);
                this._detailScreen._setComboBox(_g.d.gl_wht_list._cust_tax_type, MyLib._myGlobal._intPhase(__object._cust_type));

                for (int __row = 0; __row < __object._detailList.Count; __row++)
                {
                    _WHTDetailObjectClass __objectDetail = (_WHTDetailObjectClass)__object._detailList[__row];
                    int __addr = this._detailGrid._addRow();
                    this._detailGrid._cellUpdate(__addr, _g.d.gl_wht_list_detail._income_type, __objectDetail._incomeType, false);
                    this._detailGrid._cellUpdate(__addr, _g.d.gl_wht_list_detail._amount, __objectDetail._amount, false);
                    this._detailGrid._cellUpdate(__addr, _g.d.gl_wht_list_detail._tax_rate, __objectDetail._taxRate, false);
                    this._detailGrid._cellUpdate(__addr, _g.d.gl_wht_list_detail._tax_value, __objectDetail._taxValue, false);
                }
                this._refreshData();
                this._onLoadData = false;
                this._detailGrid.Invalidate();
            }
        }

        void _refreshData()
        {
            if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLGeneralLedger)
            {

                if (this._onLoadData == false)
                {

                    //
                    string __custCode = this._detailScreen._getDataStr(_g.d.gl_wht_list._cust_code);
                    string __custName = "";
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    switch (this._whType)
                    {
                        case _withHoldingTaxType.เจ้าหนี้_ยกเลิกจ่ายชำระหนี้:
                        case _withHoldingTaxType.เจ้าหนี้_จ่ายชำระหนี้:
                        case _withHoldingTaxType.ซื้อ:
                            {
                                int __apStatus = 0;
                                string __taxID = "";
                                string __cardID = "";

                                //
                                StringBuilder __query = new StringBuilder();
                                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ap_supplier._ap_status + "," + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._code + "=\'" + __custCode + "\'"));
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ap_supplier_detail._tax_id + "," + _g.d.ap_supplier_detail._card_id + " from " + _g.d.ap_supplier_detail._table + " where " + _g.d.ap_supplier_detail._ap_code + "=\'" + __custCode + "\'"));
                                __query.Append("</node>");
                                ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
                                DataSet __getData1 = (DataSet)__getData[0];
                                if (__getData1.Tables[0].Rows.Count > 0)
                                {
                                    //int __status = MyLib._myGlobal._intPhase(__getData1.Tables[0].Rows[0][_g.d.ap_supplier._ap_status].ToString());
                                    __apStatus = MyLib._myGlobal._intPhase(__getData1.Tables[0].Rows[0][_g.d.ap_supplier._ap_status].ToString());
                                    //__apStatus = MyLib._myResource._findResource(_g.d.ap_supplier._table + "." + ((__status == 0) ? _g.d.ap_supplier._personality : _g.d.ap_supplier._juristic_person))._str;
                                    __custName = __getData1.Tables[0].Rows[0][_g.d.ap_supplier._name_1].ToString();
                                }
                                DataSet __getData2 = (DataSet)__getData[1];
                                if (__getData2.Tables[0].Rows.Count > 0)
                                {
                                    __taxID = __getData2.Tables[0].Rows[0][_g.d.ap_supplier_detail._tax_id].ToString();
                                    __cardID = __getData2.Tables[0].Rows[0][_g.d.ap_supplier_detail._card_id].ToString();
                                }
                                this._detailScreen._setDataStr(_g.d.gl_wht_list._cust_code, __custCode, __custName, true);
                                this._detailScreen._setComboBox(_g.d.gl_wht_list._cust_tax_type, __apStatus);
                                this._detailScreen._setDataStr(_g.d.gl_wht_list._tax_number, __taxID, "", true);
                                this._detailScreen._setDataStr(_g.d.gl_wht_list._card_number, __cardID, "", true);
                            }
                            break;
                        default:
                            {
                                int __arStatus = 0;
                                string __taxID = "";
                                string __cardID = "";
                                //
                                StringBuilder __query = new StringBuilder();
                                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_customer._ar_status + "," + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + __custCode + "\'"));
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_customer_detail._tax_id + "," + _g.d.ar_customer_detail._card_id + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._ar_code + "=\'" + __custCode + "\'"));
                                __query.Append("</node>");
                                ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
                                DataSet __getData1 = (DataSet)__getData[0];
                                if (__getData1.Tables[0].Rows.Count > 0)
                                {
                                    __arStatus = MyLib._myGlobal._intPhase(__getData1.Tables[0].Rows[0][_g.d.ar_customer._ar_status].ToString());
                                    //__arStatus = MyLib._myResource._findResource(_g.d.ar_customer._table + "." + ((__status == 0) ? _g.d.ar_customer._personality : _g.d.ar_customer._juristic_person))._str;
                                    __custName = __getData1.Tables[0].Rows[0][_g.d.ar_customer._name_1].ToString();
                                }
                                DataSet __getData2 = (DataSet)__getData[1];
                                if (__getData2.Tables[0].Rows.Count > 0)
                                {
                                    __taxID = __getData2.Tables[0].Rows[0][_g.d.ar_customer_detail._tax_id].ToString();
                                    __cardID = __getData2.Tables[0].Rows[0][_g.d.ar_customer_detail._card_id].ToString();
                                }
                                this._detailScreen._setDataStr(_g.d.gl_wht_list._cust_code, __custCode, __custName, true);
                                this._detailScreen._setComboBox(_g.d.gl_wht_list._cust_tax_type, __arStatus);
                                this._detailScreen._setDataStr(_g.d.gl_wht_list._tax_number, __taxID, "", true);
                                this._detailScreen._setDataStr(_g.d.gl_wht_list._card_number, __cardID, "", true);
                            }
                            break;
                    }
                    //
                }
            }
        }

        void _saveDetail()
        {
            if (this._mainGrid._selectRow != -1 && this._mainGrid._selectRow < this._mainGrid._rowData.Count)
            {
                if (this._onLoadData == false)
                {
                    if (this._detailScreen.Enabled)
                    {
                        string __docNo = this._detailScreen._getDataStr(_g.d.gl_wht_list._tax_doc_no);
                        DateTime __docDate = MyLib._myGlobal._convertDate(this._detailScreen._getDataStr(_g.d.gl_wht_list._due_date));
                        string __custCode = this._detailScreen._getDataStr(_g.d.gl_wht_list._cust_code);

                        string __custName = this._detailScreen._getDataStr(_g.d.gl_wht_list._cust_name);
                        string __custAddress = this._detailScreen._getDataStr(_g.d.gl_wht_list._cust_address);
                        string __cust_type = this._detailScreen._getDataStr(_g.d.gl_wht_list._cust_tax_type);
                        string __card_id = this._detailScreen._getDataStr(_g.d.gl_wht_list._card_number);
                        string __tax_id = this._detailScreen._getDataStr(_g.d.gl_wht_list._tax_number);
                        string __tax_type = this._detailScreen._getDataStr(_g.d.gl_wht_list._cust_tax_type);

                        _WHTObjectClass __object = (_WHTObjectClass)this._mainGrid._cellGet(this._mainGrid._selectRow, this._objectField);
                        //
                        this._mainGrid._cellUpdate(this._mainGrid._selectRow, _g.d.gl_wht_list._tax_doc_no, __docNo, false);
                        this._mainGrid._cellUpdate(this._mainGrid._selectRow, _g.d.gl_wht_list._due_date, __docDate, false);

                        if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLGeneralLedger)
                        {
                            this._mainGrid._cellUpdate(this._mainGrid._selectRow, _g.d.gl_wht_list._cust_name, __custName, false);
                            this._mainGrid._cellUpdate(this._mainGrid._selectRow, _g.d.gl_wht_list._cust_address, __custAddress, false);

                        }

                        //if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLGeneralLedger)
                        this._mainGrid._cellUpdate(this._mainGrid._selectRow, _g.d.gl_wht_list._cust_code, __custCode, true);
                        this._mainGrid.Invalidate();
                        //
                        __object._taxDocNo = __docNo;
                        __object._dueDate = __docDate;
                        __object._custCode = __custCode;
                        __object._custName = __custName;
                        __object._custAddress = __custAddress;
                        __object._card_number = __card_id;
                        __object._tax_number = __tax_id;
                        __object._cust_type = __tax_type;

                        //
                        __object._detailList.Clear();
                        //
                        for (int __row = 0; __row < this._detailGrid._rowData.Count; __row++)
                        {
                            if (this._detailGrid._cellGet(__row, _g.d.gl_wht_list_detail._income_type).ToString().Length > 0)
                            {
                                _WHTDetailObjectClass __newObject = new _WHTDetailObjectClass();
                                __newObject._incomeType = this._detailGrid._cellGet(__row, _g.d.gl_wht_list_detail._income_type).ToString();
                                __newObject._amount = MyLib._myGlobal._decimalPhase(this._detailGrid._cellGet(__row, _g.d.gl_wht_list_detail._amount).ToString());
                                __newObject._taxRate = MyLib._myGlobal._decimalPhase(this._detailGrid._cellGet(__row, _g.d.gl_wht_list_detail._tax_rate).ToString());
                                __newObject._taxValue = MyLib._myGlobal._decimalPhase(this._detailGrid._cellGet(__row, _g.d.gl_wht_list_detail._tax_value).ToString());
                                __object._detailList.Add(__newObject);
                            }
                        }
                        this._mainGrid._cellUpdate(this._mainGrid._selectRow, this._objectField, __object, true);
                        this._mainGrid.Validate();
                    }
                }
            }
        }

        void _mainGrid__alterCellUpdate(object sender, int row, int column)
        {
            int __columnCustCode = this._mainGrid._findColumnByName(_g.d.gl_wht_list._cust_code);
            if (__columnCustCode != -1)
            {
                //this._mainGrid._cellUpdate(row, _g.d.gl_wht_list._cust_name, this._searchCustName(this._mainGrid._cellGet(row, _g.d.gl_wht_list._cust_code).ToString()), false);
                //this._mainGrid.Invalidate();
                _searchCustName(row);
            }
        }

        string _searchCustName(string custCode)
        {
            string __custName = "";
            string __query = "";
            switch (this._whType)
            {
                case _withHoldingTaxType.เจ้าหนี้_ยกเลิกจ่ายชำระหนี้:
                case _withHoldingTaxType.เจ้าหนี้_จ่ายชำระหนี้:
                case _withHoldingTaxType.ซื้อ:
                    __query = "select " + _g.d.ap_supplier._name_1 + "," + _g.d.ap_supplier._address + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._code + "=\'" + custCode + "\'";
                    break;
                default:
                    __query = "select " + _g.d.ar_customer._name_1 + "," + _g.d.ar_customer._address + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + custCode + "\'";
                    break;
            }
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __getData = __myFrameWork._queryShort(__query).Tables[0];
            if (__getData.Rows.Count > 0)
            {
                __custName = __getData.Rows[0][0].ToString();
            }
            return __custName;
        }

        void _searchCustName(int row)
        {
            string custCode = this._mainGrid._cellGet(row, _g.d.gl_wht_list._cust_code).ToString();
            string __query = "";
            switch (this._whType)
            {
                case _withHoldingTaxType.เจ้าหนี้_ยกเลิกจ่ายชำระหนี้:
                case _withHoldingTaxType.เจ้าหนี้_จ่ายชำระหนี้:
                case _withHoldingTaxType.ซื้อ:
                    __query = "select " + _g.d.ap_supplier._name_1 + "," + _g.d.ap_supplier._address + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._code + "=\'" + custCode + "\'";
                    break;
                default:
                    __query = "select " + _g.d.ar_customer._name_1 + "," + _g.d.ar_customer._address + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + custCode + "\'";
                    break;
            }
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __getData = __myFrameWork._queryShort(__query).Tables[0];
            if (__getData.Rows.Count > 0)
            {
                //__custName = __getData.Rows[0][0].ToString();
                this._mainGrid._cellUpdate(row, _g.d.gl_wht_list._cust_name, __getData.Rows[0][0].ToString(), false);
                this._mainGrid._cellUpdate(row, _g.d.gl_wht_list._cust_address, __getData.Rows[0][1].ToString(), false);
                this._mainGrid.Invalidate();
            }
        }


        void _mainGrid__afterAddRow(object sender, int row)
        {
            _WHTObjectClass __newObject = new _WHTObjectClass();
            __newObject._custCode = this._custCode;
            __newObject._dueDate = this._docDate;
            this._mainGrid._cellUpdate(row, this._objectField, __newObject, false);
            //
            this._mainGrid._cellUpdate(row, _g.d.gl_wht_list._due_date, __newObject._dueDate, false);
            this._mainGrid._cellUpdate(row, _g.d.gl_wht_list._cust_code, __newObject._custCode, true);
        }

        public void _clear()
        {
            this._mainGrid._clear();
            this._detailScreen._clear();
            this._detailGrid._clear();
            this._checkDetailEnable();

            this._detailScreen._setComboBox(_g.d.gl_wht_list._cust_tax_type, 0);
        }

        private void _addButton_Click(object sender, EventArgs e)
        {
            int __addr = this._mainGrid._addRow();
            this._mainGrid.Invalidate();
            this._mainGrid._selectRow = __addr;
            this._refreshData();

            if (_getDueDateEventArgs != null)
            {
                DateTime __docDate = _getDueDateEventArgs();
                this._detailScreen._setDataDate(_g.d.gl_wht_list._due_date, __docDate);
            }
            /*
            // set default object row
            this._saveDetail();

            switch (this._whType)
            {
                case _withHoldingTaxType.ซื้อ:
                case _withHoldingTaxType.เจ้าหนี้_จ่ายชำระหนี้:
                case _withHoldingTaxType.เจ้าหนี้_ยกเลิกจ่ายชำระหนี้:
                    {
                        // run doc no
                        string __runDocNo = _screenCode;

                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                        DataTable __getFormat = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._format + "," + _g.d.erp_doc_format._code + "," + _g.d.erp_doc_format._tax_format + "," + _g.d.erp_doc_format._doc_running + "," + _g.d.erp_doc_format._vat_type + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._code + "=\'" + this._screenCode + "\'").Tables[0];
                        if (__getFormat.Rows.Count > 0)
                        {
                            string __format = __getFormat.Rows[0][0].ToString();
                            string __docFormatCode = __getFormat.Rows[0][1].ToString();

                            DateTime __docDate = DateTime.Now;
                            if (_getDueDateEventArgs != null)
                            {
                                __docDate = _getDueDateEventArgs();
                            }

                            string __startRunningNumber = __getFormat.Rows[0][_g.d.erp_doc_format._doc_running].ToString();
                            string __newDoc = _g.g._getAutoRun(_g.g._autoRunType.ว่าง, __docFormatCode, this._detailScreen._getDataStr(_g.d.gl_wht_list._due_date).ToString(), __format, _g.g._transControlTypeEnum.ว่าง, _g.g._transControlTypeEnum.ว่าง, _g.d.gl_wht_list._table, __startRunningNumber, _g.d.gl_wht_list._tax_doc_no, "");
                            this._detailScreen._setDataStr(_g.d.gl_wht_list._tax_doc_no, __newDoc);
                        }
                    }
                    break;
            }*/
        }

        private void _buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._mainGrid._selectRow != -1 && this._mainGrid._selectRow < this._mainGrid._rowData.Count)
                {
                    this._mainGrid._rowData.RemoveAt(this._mainGrid._selectRow);
                }
                this._mainGrid.Invalidate();
            }
            catch (Exception __ex)
            {
                MessageBox.Show("Fail ." + __ex.Message.ToString());
            }
        }

        private void _mainGrid_Load(object sender, EventArgs e)
        {

        }
    }

    public class _WHTObjectClass
    {
        public string _taxDocNo = "";
        public string _custCode = "";
        public DateTime _dueDate = new DateTime();
        public List<_WHTDetailObjectClass> _detailList = new List<_WHTDetailObjectClass>();
        public string _custName = "";
        public string _custAddress = "";
        public string _card_number = "";
        public string _tax_number = "";
        public string _cust_type = "";
    }

    public class _WHTDetailObjectClass
    {
        public string _incomeType = "";
        public decimal _amount = 0M;
        public decimal _taxRate = 0M;
        public decimal _taxValue = 0M;
    }
}
