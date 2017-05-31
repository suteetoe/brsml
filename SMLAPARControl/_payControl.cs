using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPAPARControl
{
    public partial class _payControl : UserControl
    {
        public delegate decimal _getTotalAmountEvent();
        public delegate decimal _getTotalTaxAmountEvent();
        public delegate string _getCustCodeEvent();
        //
        public event _getTotalAmountEvent _getTotalAmount;
        public event _getTotalTaxAmountEvent _getTotalTaxAmount;
        public event _getCustCodeEvent _getCustCode;

        public _searchChqForm _chqSearchForm;

        //
        /// <summary>
        /// ผลต่าง
        /// </summary>
        public decimal _sum_diff = 0M;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        /// <summary>
        /// ให้ส่งค่าจำนวนเงินมากจากหน้าจอ 
        /// </summary>
        private decimal __sum_amount = 0M;
        public decimal _sum_amount
        {
            get { return __sum_amount; }
            set { __sum_amount = value; }
        }

        /// <summary>
        /// ส่งค่ามาจะได้รุ้ว่ามาจากระบบใหนจะได้ส่ง Query กลับได้
        /// </summary>
        private string __from_screen = "";
        public string _from_screen
        {
            get { return __from_screen; }
            set { __from_screen = value; }
        }

        /// <summary>
        /// เลขที่เอกสาร
        /// </summary>
        private string __doc_no = "";
        public string _doc_no
        {
            get { return __doc_no; }
            set { __doc_no = value; }
        }

        /// <summary>
        /// วันที่เอกสาร
        /// </summary>
        private string __doc_date = "";
        public string _doc_date
        {
            get { return __doc_date; }
            set { __doc_date = value; }
        }

        /// <summary>
        /// Query
        /// </summary>
        private string __result = "";
        public string _result
        {
            get { return __result; }
            set { __result = value; }
        }

        /// <summary>
        /// ตรวจสอบว่ามาจากหน้าใหน
        /// </summary>
        private bool __is_page;

        public bool _is_page
        {
            get { return __is_page; }
            set { __is_page = value; }
        }

        /// <summary>
        /// หัก ณ ที่จ่าย 
        /// </summary>
        private decimal __vat_amount;

        public decimal _vat_amount
        {
            get { return __vat_amount; }
            set { __vat_amount = value; }
        }

        private bool __is_msg;

        public bool _is_msg
        {
            get { return __is_msg; }
            set { __is_msg = value; }
        }


        /// <summary>
        /// Query
        /// </summary>
        private string __result_intsert_pay_money = "";

        public string _result_intsert_pay_money
        {
            get { return __result_intsert_pay_money; }
            set { __result_intsert_pay_money = value; }
        }

        /// <summary>
        /// Query เงินสด
        /// </summary>
        private string __query_pay_money_cash = "";
        public string _query_pay_money_cash
        {
            get { return __query_pay_money_cash; }
            set { __query_pay_money_cash = value; }
        }

        /// <summary>
        /// Query เงินโอน
        /// </summary>
        private string __query_pay_money_transfer = "";
        public string _query_pay_money_transfer
        {
            get { return __query_pay_money_transfer; }
            set { __query_pay_money_transfer = value; }
        }

        /// <summary>
        /// Query เครดิต
        /// </summary>
        private string __query_pay_credit_card = "";
        public string _query_pay_credit_card
        {
            get { return __query_pay_credit_card; }
            set { __query_pay_credit_card = value; }
        }
        /// <summary>
        /// Query เช็ค
        /// </summary>
        private string __query_pay_chq_list = "";
        public string _query_pay_chq_list
        {
            get { return __query_pay_chq_list; }
            set { __query_pay_chq_list = value; }
        }

        /// <summary>
        /// แสดงปุ่ม ข้างล้างหรือไม่
        /// </summary>
        private bool __show_bottom = false;
        public bool _show_bottom
        {
            get { return __show_bottom; }
            set { __show_bottom = value; }
        }

        /// <summary>
        /// ค้นหาข้อมูล
        /// </summary>
        private string __query_where = "";
        public string _query_where
        {
            get { return __query_where; }
            set { __query_where = value; }
        }

        private bool __is_save = false;
        public bool _is_save
        {
            get { return __is_save; }
            set { __is_save = value; }
        }

        private bool __recieve_or_pay = false;
        private _g.g._transControlTypeEnum _transControlTypeTemp;
        private DateTime _defaultDateTemp;

        public _expenseGridControl _gridExpense;
        public _incomeGridControl _gridIncome;
        private System.Windows.Forms.TabPage tab_expense;
        private System.Windows.Forms.TabPage tab_income;


        private System.Windows.Forms.TabPage tab_currency_other;
        private _payOtherCurrencyGridControl _payOtherCurrencyGrid;

        public _payControl()
        {
            InitializeComponent();
            //this._chq_toolbar.Visible = false;
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolStripExtra.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._payDetailScreen._refreshData += new _payDetailScreen._refreshDataEvent(_payCashScreen__refreshData);
            this._payChequeGrid._refreshData += new _payChequeGridControl._refreshDataEvent(_payChequeGrid__refreshData);
            this._payCreditCardGrid._refreshData += new _payCreditCardGridControl._refreshDataEvent(_payCreditCardGrid__refreshData);
            this._payTransferGrid._refreshData += new _payTransferGridControl._refreshDataEvent(_payTransferGrid__refreshData);
            this._payDepositGrid._refreshData += new _payDepositAdvanceGridControl._refreshDataEvent(_payDeposit__refreshData);
            this._payPettyCashGrid._refreshData += new _payPettyCashGridControl._refreshDataEvent(_payPettyCashGrid__refreshData);

            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSLite)
            {
                this._chq_toolbar.Visible = false;
            }

            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
            {
                // add income expense
                this.SuspendLayout();

                // add tab income
                this._gridIncome = new _incomeGridControl();
                this._gridIncome._refreshData += new _incomeGridControl._refreshDataEvent(_gridIncome__refreshData);

                this.tab_income = new System.Windows.Forms.TabPage();
                this.tab_income.SuspendLayout();

                this._tab.Controls.Add(this.tab_income);

                this.tab_income.BackColor = System.Drawing.Color.Transparent;
                this.tab_income.Controls.Add(this._gridIncome);
                this.tab_income.Location = new System.Drawing.Point(4, 23);
                this.tab_income.Name = "tab_income";
                this.tab_coupon.Size = new System.Drawing.Size(768, 457);
                this.tab_income.TabIndex = 0;
                this.tab_income.Text = "8.tab_income";


                // add tab expense
                this._gridExpense = new _expenseGridControl();
                this._gridExpense._refreshData += new _expenseGridControl._refreshDataEvent(_gridExpense__refreshData);

                this.tab_expense = new System.Windows.Forms.TabPage();
                this.tab_expense.SuspendLayout();

                this._tab.Controls.Add(this.tab_expense);

                this.tab_expense.BackColor = System.Drawing.Color.Transparent;
                this.tab_expense.Controls.Add(this._gridExpense);
                this.tab_expense.Location = new System.Drawing.Point(4, 23);
                this.tab_expense.Name = "tab_expense";
                this.tab_expense.Size = new System.Drawing.Size(768, 457);
                this.tab_expense.TabIndex = 0;
                this.tab_expense.Text = "9.tab_expense";

                if (_g.g._companyProfile._multi_currency)
                {
                    // add tab other currency
                    this._payOtherCurrencyGrid = new _payOtherCurrencyGridControl();
                    this._payOtherCurrencyGrid.Dock = DockStyle.Fill;
                    this._payOtherCurrencyGrid._refreshData += (payOtherCurrencyGridSender) =>
                    {
                        this._reCalc();
                    };
                    this.tab_currency_other = new TabPage();
                    this.tab_currency_other.SuspendLayout();

                    this._tab.Controls.Add(this.tab_currency_other);

                    this.tab_currency_other.BackColor = System.Drawing.Color.Transparent;
                    this.tab_currency_other.Controls.Add(this._payOtherCurrencyGrid);
                    this.tab_currency_other.Location = new System.Drawing.Point(4, 23);
                    this.tab_currency_other.Name = "tab_currency_other";
                    this.tab_currency_other.Size = new System.Drawing.Size(768, 457);
                    this.tab_currency_other.TabIndex = 0;
                    this.tab_currency_other.Text = "10.tab_currency_other";


                    this.tab_currency_other.ResumeLayout(false);
                }

                this.tab_expense.ResumeLayout(false);
                this.tab_expense.PerformLayout();
                this.tab_income.ResumeLayout(false);
                this.tab_income.PerformLayout();
                this.ResumeLayout(false);
            }
        }

        void _gridExpense__refreshData(_expenseGridControl sender)
        {
            this._reCalc();
        }

        void _gridIncome__refreshData(_incomeGridControl sender)
        {
            this._reCalc();
        }

        public Boolean _checker()
        {
            this._payDetailScreen._saveLastControl();
            Boolean __result = true;
            decimal __amountDebit = MyLib._myGlobal._decimalPhase(this._payDetailScreen._getDataStr(_g.d.cb_trans._total_net_amount));
            decimal __amountCredit = MyLib._myGlobal._decimalPhase(this._payDetailScreen._getDataStr(_g.d.cb_trans._total_amount_pay));
            if (__amountCredit != __amountDebit)
            {
                MessageBox.Show(MyLib._myGlobal._resource("ยอดการจ่ายเงินไม่สมบูรณ์"));
                __result = false;
            }
            return __result;
        }

        void _payPettyCashGrid__refreshData(_payPettyCashGridControl sender)
        {
            this._reCalc();
        }

        void _payDeposit__refreshData(_payDepositAdvanceGridControl sender)
        {
            this._reCalc();
        }

        public DateTime _defaultDate
        {
            set
            {
                this._defaultDateTemp = value;
                this._payChequeGrid._defaultDate = value;
                this._payTransferGrid._defaultDate = value;
            }
            get
            {
                return this._defaultDateTemp;
            }
        }

        void _payTransferGrid__refreshData(_payTransferGridControl sender)
        {
            this._reCalc();
        }

        void _payCreditCardGrid__refreshData(_payCreditCardGridControl sender)
        {
            this._reCalc();
        }

        void _payChequeGrid__refreshData(_payChequeGridControl sender)
        {
            this._reCalc();
        }

        void _payCashScreen__refreshData(_payDetailScreen sender)
        {
            this._reCalc();
        }

        public string _queryDelete(string docNo)
        {
            StringBuilder __query = new StringBuilder();
            string __transFlag = _g.g._transFlagGlobal._transFlag(this._icTransControlType).ToString();
            string __transType = _g.g._transTypeGlobal._transType(this._icTransControlType).ToString();
            // เงินสด
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._doc_no + "=\'" + docNo + "\' and " + _g.d.cb_trans._trans_flag + "=" + __transFlag.ToString() + " and " + _g.d.cb_trans._trans_type + "=" + __transType));
            // เงินโอน,เช็ค,บัตรเครดิต ฯลฯ (ยกเว้นเงินมัดจำ)
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.cb_trans_detail._table + " where " + _g.d.cb_trans_detail._doc_no + "=\'" + docNo + "\' and " + _g.d.cb_trans_detail._trans_flag + "=" + __transFlag.ToString() + " and " + _g.d.cb_trans_detail._trans_type + "=" + __transType + " and " + _g.d.cb_trans_detail._doc_type + "<>6"));
            return __query.ToString();
        }

        public int _tableLoadCount = 0;
        /// <summary>
        /// สร้าง query สำหรับ load
        /// </summary>
        /// <param name="docNo"></param>
        /// <returns></returns>
        public string _queryLoad(string docNo)
        {
            _tableLoadCount = 0;

            StringBuilder __query = new StringBuilder();
            string __transFlag = _g.g._transFlagGlobal._transFlag(this._icTransControlType).ToString();
            string __transType = _g.g._transTypeGlobal._transType(this._icTransControlType).ToString();
            // เงินสด
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._doc_no + "=\'" + docNo + "\' and " + _g.d.cb_trans._trans_flag + "=" + __transFlag.ToString() + " and " + _g.d.cb_trans._trans_type + "=" + __transType));
            _tableLoadCount++;
            //
            string __queryDetail = "select *{0} from " + _g.d.cb_trans_detail._table + " where " + _g.d.cb_trans_detail._doc_no + "=\'" + docNo + "\' and " + _g.d.cb_trans_detail._trans_flag + "=" + __transFlag.ToString() + " and " + _g.d.cb_trans_detail._trans_type + "=" + __transType + " and " + _g.d.cb_trans_detail._doc_type + "={1} order by " + _g.d.cb_trans_detail._line_number;

            // เงินโอน
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format(__queryDetail, "", "1")));
            _tableLoadCount++;

            // เช็ค
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format(__queryDetail, "", "2")));
            _tableLoadCount++;

            // บัตรเครดิต
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format(__queryDetail, "", "3")));
            _tableLoadCount++;

            // เงินสดย่อย
            string __getName1 = ",(select " + _g.d.cb_petty_cash._name_1 + " from " + _g.d.cb_petty_cash._table + " where " + _g.d.cb_petty_cash._table + "." + _g.d.cb_petty_cash._code + "=" + _g.d.cb_trans_detail._trans_number + ") as " + _g.d.cb_trans_detail._description;
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format(__queryDetail, __getName1, "4")));
            _tableLoadCount++;

            // เงินล่วงหน้า
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format(__queryDetail, "", "5")));
            _tableLoadCount++;

            // คูปอง
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format(__queryDetail, "", "9")));
            _tableLoadCount++;

            // รายจ่ายอื่น 11
            __getName1 = ",(select " + _g.d.erp_expenses_list._name_1 + " from " + _g.d.erp_expenses_list._table + " where " + _g.d.erp_expenses_list._table + "." + _g.d.erp_expenses_list._code + "=" + _g.d.cb_trans_detail._trans_number + ") as " + _g.d.cb_trans_detail._description;
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format(__queryDetail, __getName1, "11")));
            _tableLoadCount++;

            // รายได้อ่น 12
            __getName1 = ",(select " + _g.d.erp_income_list._name_1 + " from " + _g.d.erp_income_list._table + " where " + _g.d.erp_income_list._table + "." + _g.d.erp_income_list._code + "=" + _g.d.cb_trans_detail._trans_number + ") as " + _g.d.cb_trans_detail._description;
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format(__queryDetail, __getName1, "12")));
            _tableLoadCount++;

            // จ่ายสกุลอื่นๆ  19
            __getName1 = ",(select " + _g.d.erp_currency._name_1 + " from " + _g.d.erp_currency._table + " where " + _g.d.erp_currency._table + "." + _g.d.erp_currency._code + "=" + _g.d.cb_trans_detail._trans_number + ") as " + _g.d.cb_trans_detail._description;
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format(__queryDetail, __getName1, "19")));
            _tableLoadCount++;

            return __query.ToString();
        }

        /// <summary>
        /// load datatable ขึ้นจอ
        /// </summary>
        /// <param name="tableNumber"></param>
        public int _loadToScreen(ArrayList source, int tableNumber)
        {
            int __oldTableNumber = tableNumber;

            this._payDetailScreen._loadData(((DataSet)source[tableNumber++]).Tables[0]);
            this._payTransferGrid._loadFromDataTable(((DataSet)source[tableNumber++]).Tables[0]);
            this._payChequeGrid._loadFromDataTable(((DataSet)source[tableNumber++]).Tables[0]);
            this._payCreditCardGrid._loadFromDataTable(((DataSet)source[tableNumber++]).Tables[0]);
            this._payPettyCashGrid._loadFromDataTable(((DataSet)source[tableNumber++]).Tables[0]);
            this._payDepositGrid._loadFromDataTable(((DataSet)source[tableNumber++]).Tables[0]);
            this._payCouponGrid._loadFromDataTable(((DataSet)source[tableNumber++]).Tables[0]);

            if (this._gridIncome != null && this._gridExpense != null)
            {
                this._gridExpense._loadFromDataTable(((DataSet)source[tableNumber++]).Tables[0]);
                this._gridIncome._loadFromDataTable(((DataSet)source[tableNumber++]).Tables[0]);
            }

            if (this._payOtherCurrencyGrid != null)
            {
                this._payOtherCurrencyGrid._loadFromDataTable(((DataSet)source[__oldTableNumber + 9]).Tables[0]);
            }
            tableNumber++;

            return tableNumber;
        }

        /// <summary>
        /// สร้าง query สำหรับ insert
        /// </summary>
        /// <param name="docNo"></param>
        /// <param name="docDate"></param>
        /// <returns></returns>
        public string _queryInsert(string docNo, string docDate, string docTime, string docFormatCode, string remark)
        {
            StringBuilder __query = new StringBuilder();
            string __transFlag = _g.g._transFlagGlobal._transFlag(this._icTransControlType).ToString();
            string __transType = _g.g._transTypeGlobal._transType(this._icTransControlType).ToString();
            string __payType = _g.g._transTypeGlobal._payType(this._icTransControlType).ToString();
            if (this._getCustCode == null)
            {
                return "";
            }
            string __custCode = this._getCustCode();
            ArrayList __dataQuery = this._payDetailScreen._createQueryForDatabase();
            // รายละเอียด
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.cb_trans._table + " (" + _g.d.cb_trans._doc_format_code + "," +
                _g.d.cb_trans._doc_no + "," + _g.d.cb_trans._doc_date + "," + _g.d.cb_trans._doc_time + "," + _g.d.cb_trans._ap_ar_code + "," + _g.d.cb_trans._pay_type + "," + _g.d.cb_trans._trans_type + "," + _g.d.cb_trans._trans_flag + "," + _g.d.cb_trans._remark + "," + __dataQuery[0].ToString() + ") values (\'" + docFormatCode + "\'," +
                "\'" + docNo + "\'," + docDate + ",\'" + docTime + "\',\'" + __custCode + "\'," + __payType + "," + __transType + "," + __transFlag + ",\'" + remark + "\'," + __dataQuery[1].ToString() + ")"));
            //
            string __fieldList = _g.d.cb_trans_detail._doc_no + "," + _g.d.cb_trans_detail._doc_date + "," + _g.d.cb_trans_detail._doc_time + "," + _g.d.cb_trans_detail._trans_type + "," + _g.d.cb_trans_detail._trans_flag + ",";
            string __dataList = "\'" + docNo + "\'," + docDate + ",\'" + docTime + "\'," + __transType + "," + __transFlag + ",";
            // เงินโอน
            this._payTransferGrid._updateRowIsChangeAll(true);
            __query.Append(this._payTransferGrid._createQueryForInsert(_g.d.cb_trans_detail._table, __fieldList + _g.d.cb_trans_detail._doc_type + ",", __dataList + "1,", false, true));
            // เช็ค
            int __ap_ar_type = 0;
            switch (_g.g._transType(this._icTransControlType))
            {
                case _g.g._transTypeEnum.ขาย_ลูกหนี้:
                case _g.g._transTypeEnum.ลูกหนี้:
                    __ap_ar_type = 1;
                    break;
                default:
                    __ap_ar_type = 2;
                    break;
            }
            this._payChequeGrid._updateRowIsChangeAll(true);
            __query.Append(this._payChequeGrid._createQueryForInsert(_g.d.cb_trans_detail._table,
                __fieldList + _g.d.cb_trans_detail._doc_type + "," + _g.d.cb_trans_detail._trans_number_type + "," + _g.d.cb_trans_detail._ap_ar_type + "," + _g.d.cb_trans_detail._ap_ar_code + ",",
                __dataList + "2," + this._payChequeGrid._chqType.ToString() + "," + __ap_ar_type + ",\'" + __custCode + "\',", false, true));
            // บัตรเครดิต
            this._payCreditCardGrid._updateRowIsChangeAll(true);
            __query.Append(this._payCreditCardGrid._createQueryForInsert(_g.d.cb_trans_detail._table,
                __fieldList + _g.d.cb_trans_detail._doc_type + "," + _g.d.cb_trans_detail._trans_number_type + "," + _g.d.cb_trans_detail._ap_ar_type + "," + _g.d.cb_trans_detail._ap_ar_code + ",",
                __dataList + "3," + this._payChequeGrid._chqType.ToString() + "," + __ap_ar_type + ",\'" + __custCode + "\',", false, true));
            // เงินสดย่อย
            this._payPettyCashGrid._updateRowIsChangeAll(true);
            __query.Append(this._payPettyCashGrid._createQueryForInsert(_g.d.cb_trans_detail._table, __fieldList + _g.d.cb_trans_detail._doc_type + ",", __dataList + "4,", false, true));
            // เงินล่วงหน้า
            this._payDepositGrid._updateRowIsChangeAll(true);
            __query.Append(this._payDepositGrid._createQueryForInsert(_g.d.cb_trans_detail._table, __fieldList + _g.d.cb_trans_detail._doc_type + ",", __dataList + "5,", false, true));

            // โต๋ คูปอง
            this._payCouponGrid._updateRowIsChangeAll(true);
            __query.Append(this._payCouponGrid._createQueryForInsert(_g.d.cb_trans_detail._table, __fieldList + _g.d.cb_trans_detail._doc_type + ",", __dataList + "9,", false, true));

            if (this._gridExpense != null && this._gridIncome != null)
            {
                // รายจ่ายอื่น 11
                this._gridExpense._updateRowIsChangeAll(true);
                __query.Append(this._gridExpense._createQueryForInsert(_g.d.cb_trans_detail._table, __fieldList + _g.d.cb_trans_detail._doc_type + ",", __dataList + "11,", false, true));

                // รายได้อื่น 12
                this._gridIncome._updateRowIsChangeAll(true);
                __query.Append(this._gridIncome._createQueryForInsert(_g.d.cb_trans_detail._table, __fieldList + _g.d.cb_trans_detail._doc_type + ",", __dataList + "12,", false, true));

            }

            // ลบค่าว่าง
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.cb_trans_detail._table + " where " + _g.d.cb_trans_detail._trans_number + " is null or " + _g.d.cb_trans_detail._trans_number + "=\'\'"));
            //
            return __query.ToString();
        }

        public void _reCalc()
        {
            //StringBuilder __getText = new StringBuilder();
            try
            {
                if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLBIllFree)
                {
                    this._payTransferGrid._calcTotal(false);
                    this._payCreditCardGrid._calcTotal(false);
                    this._payChequeGrid._calcTotal(false);
                    this._payDepositGrid._calcTotal(false);
                    this._payPettyCashGrid._calcTotal(false);
                    this._payCouponGrid._calcTotal(false);
                    if (_gridExpense != null && this._gridIncome != null)
                    {
                        this._gridExpense._calcTotal(false);
                        this._gridIncome._calcTotal(false);
                    }

                    if (this._payOtherCurrencyGrid != null)
                    {
                        this._payOtherCurrencyGrid._calcTotal(false);
                    }
                    //
                    decimal __totalAmount = (this._getTotalAmount == null) ? 0M : this._getTotalAmount();
                    decimal __totalTaxAmount = (this._getTotalTaxAmount == null) ? 0M : this._getTotalTaxAmount();
                    decimal __depositAmount = (this._payDepositGrid._findColumnByName(_g.d.cb_trans_detail._amount) == -1) ? 0M : ((MyLib._myGrid._columnType)this._payDepositGrid._columnList[this._payDepositGrid._findColumnByName(_g.d.cb_trans_detail._amount)])._total;
                    decimal __pettyCashAmount = (this._payPettyCashGrid._findColumnByName(_g.d.cb_trans_detail._amount) == -1) ? 0M : ((MyLib._myGrid._columnType)this._payPettyCashGrid._columnList[this._payPettyCashGrid._findColumnByName(_g.d.cb_trans_detail._amount)])._total;
                    decimal __totalCouponAmount = (this._payCouponGrid._findColumnByName(_g.d.cb_trans_detail._amount) == -1) ? 0M : ((MyLib._myGrid._columnType)this._payCouponGrid._columnList[this._payCouponGrid._findColumnByName(_g.d.cb_trans_detail._amount)])._total;

                    decimal __totalExpenseAmount = 0M;
                    decimal __totalIncomeAmount = 0M;

                    if (_gridExpense != null && this._gridIncome != null)
                    {
                        switch (this._icTransControlType)
                        {
                            case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                            case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                            case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                            case _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน:
                            case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน:
                            case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                            case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                            case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า:
                            case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:
                                __totalExpenseAmount = (this._gridExpense._findColumnByName(_g.d.cb_trans_detail._amount) == -1) ? 0M : ((MyLib._myGrid._columnType)this._gridExpense._columnList[this._gridExpense._findColumnByName(_g.d.cb_trans_detail._amount)])._total;
                                __totalIncomeAmount = (this._gridIncome._findColumnByName(_g.d.cb_trans_detail._amount) == -1) ? 0M : ((MyLib._myGrid._columnType)this._gridIncome._columnList[this._gridIncome._findColumnByName(_g.d.cb_trans_detail._amount)])._total;
                                break;
                            case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                            case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                            case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                            case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                            case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                            case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า:
                            case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ:
                            case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน:
                            case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                                __totalExpenseAmount = (this._gridIncome._findColumnByName(_g.d.cb_trans_detail._amount) == -1) ? 0M : ((MyLib._myGrid._columnType)this._gridIncome._columnList[this._gridIncome._findColumnByName(_g.d.cb_trans_detail._amount)])._total;
                                __totalIncomeAmount = (this._gridExpense._findColumnByName(_g.d.cb_trans_detail._amount) == -1) ? 0M : ((MyLib._myGrid._columnType)this._gridExpense._columnList[this._gridExpense._findColumnByName(_g.d.cb_trans_detail._amount)])._total;
                                break;
                        }
                    }

                    decimal __totalPayOtherCurrencyAmount = 0M;

                    if (this._payOtherCurrencyGrid != null)
                    {
                        __totalPayOtherCurrencyAmount = (this._payOtherCurrencyGrid._findColumnByName(_g.d.cb_trans_detail._sum_amount) == -1) ? 0M : ((MyLib._myGrid._columnType)this._payOtherCurrencyGrid._columnList[this._payOtherCurrencyGrid._findColumnByName(_g.d.cb_trans_detail._sum_amount)])._total;
                    }

                    //
                    this._payDetailScreen._setDataNumber(_g.d.cb_trans._total_amount, __totalAmount);
                    this._payDetailScreen._setDataNumber(_g.d.cb_trans._petty_cash_amount, __pettyCashAmount);
                    this._payDetailScreen._setDataNumber(_g.d.cb_trans._chq_amount, (this._payChequeGrid._findColumnByName(_g.d.cb_trans_detail._amount) == -1) ? 0M : ((MyLib._myGrid._columnType)this._payChequeGrid._columnList[this._payChequeGrid._findColumnByName(_g.d.cb_trans_detail._amount)])._total);
                    this._payDetailScreen._setDataNumber(_g.d.cb_trans._tranfer_amount, (this._payTransferGrid._findColumnByName(_g.d.cb_trans_detail._amount) == -1) ? 0M : ((MyLib._myGrid._columnType)this._payTransferGrid._columnList[this._payTransferGrid._findColumnByName(_g.d.cb_trans_detail._amount)])._total);
                    this._payDetailScreen._setDataNumber(_g.d.cb_trans._card_amount, (this._payCreditCardGrid._findColumnByName(_g.d.cb_trans_detail._amount) == -1) ? 0M : ((MyLib._myGrid._columnType)this._payCreditCardGrid._columnList[this._payCreditCardGrid._findColumnByName(_g.d.cb_trans_detail._sum_amount)])._total);
                    this._payDetailScreen._setDataNumber(_g.d.cb_trans._coupon_amount, (this._payCouponGrid._findColumnByName(_g.d.cb_trans_detail._amount) == -1) ? 0M : ((MyLib._myGrid._columnType)this._payCouponGrid._columnList[this._payCouponGrid._findColumnByName(_g.d.cb_trans_detail._amount)])._total);
                    this._payDetailScreen._setDataNumber(_g.d.cb_trans._total_tax_at_pay, __totalTaxAmount);
                    this._payDetailScreen._setDataNumber(_g.d.cb_trans._deposit_amount, __depositAmount);

                    this._payDetailScreen._setDataNumber(_g.d.cb_trans._total_expense_other, __totalExpenseAmount);
                    this._payDetailScreen._setDataNumber(_g.d.cb_trans._total_income_other, __totalIncomeAmount);

                    this._payDetailScreen._setDataNumber(_g.d.cb_trans._total_other_currency, __totalPayOtherCurrencyAmount);
                    // แต้ม

                    decimal __pointRate = this._payDetailScreen._getDataNumber(_g.d.cb_trans._point_rate);
                    if (__pointRate != 0)
                    {
                        decimal __pointAmount = this._payDetailScreen._getDataNumber(_g.d.cb_trans._point_qty) * this._payDetailScreen._getDataNumber(_g.d.cb_trans._point_rate);
                        this._payDetailScreen._setDataNumber(_g.d.cb_trans._point_amount, __pointAmount);
                    }
                    // ค่า Charge บัตรเครดิต
                    this._payDetailScreen._setDataNumber(_g.d.cb_trans._total_credit_charge, (this._payCreditCardGrid._findColumnByName(_g.d.cb_trans_detail._charge) == -1) ? 0M : ((MyLib._myGrid._columnType)this._payCreditCardGrid._columnList[this._payCreditCardGrid._findColumnByName(_g.d.cb_trans_detail._charge)])._total);
                    //
                }
                else
                {
                    decimal __totalAmount = (this._getTotalAmount == null) ? 0M : this._getTotalAmount();
                    decimal __totalTaxAmount = (this._getTotalTaxAmount == null) ? 0M : this._getTotalTaxAmount();

                    this._payDetailScreen._setDataNumber(_g.d.cb_trans._total_amount, __totalAmount);
                    this._payDetailScreen._setDataNumber(_g.d.cb_trans._total_tax_at_pay, __totalTaxAmount);
                }
                this._payDetailScreen._recalc();
            }
            catch (Exception _ex)
            {
                MessageBox.Show(_ex.Message.ToString());
            }
        }

        public _g.g._transControlTypeEnum _icTransControlType
        {
            set
            {
                this._transControlTypeTemp = value;
                this._payDetailScreen._icTransControlType = value;
                //
                string __tabCredit = "tab_credit";
                string __tabDeposit = "tab_deposit";
                string __taCoupon = "tab_coupon";
                string __tabCheque = "tab_cheque";
                //
                switch (value)
                {
                    case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                        this._payChequeGrid._build(this._icTransControlType);
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabDeposit));
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__taCoupon));
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                        this._payChequeGrid._build(this._icTransControlType);
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabCredit));
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabDeposit));
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__taCoupon));
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabCheque));
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                        this._payChequeGrid._build(this._icTransControlType);
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabCredit));
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabDeposit));
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__taCoupon));
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabCheque));

                        if (tab_expense != null)
                        {
                            this.tab_expense.Name = "tab_payother";
                            this.tab_income.Name = "tab_subtract";
                        }

                        break;
                    case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย:
                        this._payChequeGrid._build(this._icTransControlType);
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabCredit));
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabDeposit));
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__taCoupon));

                        if (tab_expense != null)
                        {
                            this.tab_expense.Name = "tab_payother";
                            this.tab_income.Name = "tab_subtract";
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย:
                        this._payChequeGrid._build(this._icTransControlType);
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabCredit));
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabDeposit));
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__taCoupon));
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                        this._payChequeGrid._build(this._icTransControlType);
                        this._payDepositGrid._build(_g.g._depositAdvanceEnum.จ่ายเงินล่วงหน้า);
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__taCoupon));

                        if (tab_expense != null)
                        {
                            this.tab_expense.Name = "tab_payother";
                            this.tab_income.Name = "tab_subtract";
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                        this._payChequeGrid._build(this._icTransControlType);
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabCredit));
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabDeposit));
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__taCoupon));
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                        this._payChequeGrid._build(this._icTransControlType);
                        this._payDepositGrid._build(_g.g._depositAdvanceEnum.จ่ายเงินล่วงหน้า);
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__taCoupon));

                        if (tab_expense != null)
                        {
                            this.tab_expense.Name = "tab_payother";
                            this.tab_income.Name = "tab_subtract";
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                        this._payChequeGrid._build(this._icTransControlType);
                        //
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabCredit));
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabDeposit));
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__taCoupon));
                        if (tab_expense != null)
                        {
                            this.tab_expense.Name = "tab_payother";
                            this.tab_income.Name = "tab_subtract";
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                        this._payChequeGrid._build(this._icTransControlType);
                        // เปลี่ยน Resource และ Mode การทำงานของ grid 
                        this._payDepositGrid._build(_g.g._depositAdvanceEnum.รับเงินล่วงหน้า);
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__taCoupon));
                        break;
                    case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                        this._payChequeGrid._build(this._icTransControlType);
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__taCoupon));
                        //
                        // เปลี่ยน Resource และ Mode การทำงานของ grid 
                        this._payDepositGrid._build(_g.g._depositAdvanceEnum.รับเงินล่วงหน้า);
                        // toe
                        //this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabCredit));
                        break;
                    case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                        this._payChequeGrid._build(this._icTransControlType);
                        //
                        // เปลี่ยน Resource และ Mode การทำงานของ grid 
                        this._payDepositGrid._build(_g.g._depositAdvanceEnum.จ่ายเงินล่วงหน้า);
                        //
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabCredit));
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__taCoupon));
                        if (tab_expense != null)
                        {
                            this.tab_expense.Name = "tab_payother";
                            this.tab_income.Name = "tab_subtract";
                        }
                        break;
                    case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                        this._payChequeGrid._build(this._icTransControlType);
                        //
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabCredit));
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabDeposit));
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__taCoupon));
                        if (tab_expense != null)
                        {
                            this.tab_expense.Name = "tab_payother";
                            this.tab_income.Name = "tab_subtract";
                        }
                        break;
                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:

                        if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLBIllFree)
                        {
                            this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey("tab_petty_cash"));
                            this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey("tab_transfer"));
                            this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabCredit));
                            this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabCheque));
                            this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabDeposit));
                            this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__taCoupon));
                        }
                        else
                        {
                            this._payChequeGrid._build(this._icTransControlType);
                            // เปลี่ยน Resource และ Mode การทำงานของ grid 
                            this._payDepositGrid._build(_g.g._depositAdvanceEnum.รับเงินล่วงหน้า);
                        }

                        //if (MyLib._myGlobal._programName.Equals("SML CM"))
                        //{
                        //    this._calcForCash.Visible = false;
                        //}
                        break;
                    case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                        this._payChequeGrid._build(this._icTransControlType);
                        // เปลี่ยน Resource และ Mode การทำงานของ grid 
                        this._payDepositGrid._build(_g.g._depositAdvanceEnum.รับเงินล่วงหน้า);
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__taCoupon));
                        break;
                    case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า:
                    case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ:
                        // ลบ Tab
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabDeposit));
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__taCoupon));
                        //
                        this._payChequeGrid._build(this._icTransControlType);
                        break;
                    case _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน:
                    case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน:
                        // ลบ Tab
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabCredit));
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabDeposit));
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__taCoupon));
                        //
                        this._payChequeGrid._build(this._icTransControlType);
                        if (tab_expense != null)
                        {
                            this.tab_expense.Name = "tab_payother";
                            this.tab_income.Name = "tab_subtract";
                        }
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน:
                        // ลบ Tab
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabDeposit));
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__taCoupon));
                        //
                        this._payChequeGrid._build(this._icTransControlType);
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน:
                        // ลบ Tab
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabDeposit));
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__taCoupon));
                        //
                        this._payChequeGrid._build(this._icTransControlType);
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                        // ลบ Tab
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabCredit));
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__taCoupon));
                        //
                        this._payChequeGrid._build(this._icTransControlType);
                        if (tab_expense != null)
                        {
                            this.tab_expense.Name = "tab_payother";
                            this.tab_income.Name = "tab_subtract";
                        }
                        // เปลี่ยน Resource และ Mode การทำงานของ grid 
                        this._payDepositGrid._build(_g.g._depositAdvanceEnum.จ่ายเงินล่วงหน้า);
                        if (tab_expense != null)
                        {
                            this.tab_expense.Name = "tab_payother";
                            this.tab_income.Name = "tab_subtract";
                        }
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                        // ลบ Tab
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabCredit));
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__taCoupon));
                        //
                        this._payChequeGrid._build(this._icTransControlType);
                        if (tab_expense != null)
                        {
                            this.tab_expense.Name = "tab_payother";
                            this.tab_income.Name = "tab_subtract";
                        }
                        // เปลี่ยน Resource และ Mode การทำงานของ grid 
                        this._payDepositGrid._build(_g.g._depositAdvanceEnum.จ่ายเงินล่วงหน้า);
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า:
                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ:
                        // ลบ Tab
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabCredit));
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabDeposit));
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__taCoupon));
                        //
                        this._payChequeGrid._build(this._icTransControlType);
                        if (tab_expense != null)
                        {
                            this.tab_expense.Name = "tab_payother";
                            this.tab_income.Name = "tab_subtract";
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                        this._payChequeGrid._build(this._icTransControlType);
                        this._payDepositGrid._build(_g.g._depositAdvanceEnum.รับเงินล่วงหน้า);
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__taCoupon));
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:
                        this._payChequeGrid._build(this._icTransControlType);
                        this._payDepositGrid._build(_g.g._depositAdvanceEnum.จ่ายเงินล่วงหน้า);
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__tabCredit));
                        this._tab.TabPages.RemoveAt(this._tab.TabPages.IndexOfKey(__taCoupon));
                        if (tab_expense != null)
                        {
                            this.tab_expense.Name = "tab_payother";
                            this.tab_income.Name = "tab_subtract";
                        }
                        break;

                }
                this.Invalidate();
            }
            get
            {
                return this._transControlTypeTemp;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys keyCode = (Keys)(int)keyData & Keys.KeyCode;

            if (keyData == Keys.F12)
            {
                _onSummit();
                return true;
            }

            // ไม่รู้ดัก event เพื่ออะไร
            /*if (keyData == Keys.Escape)
            {
                this.Dispose();
                return true;
            }*/
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc_no">เลขที่เอกสาร</param>
        /// <param name="sum_amount">จำนวนเงิน</param>
        /// <param name="query_where">รายละเอียดในการคนหา</param>
        /// <param name="load_data">ให้โหลดข้อมูล หรือ ไม่</param>
        /// <param name="show_bottom">แสดงปุ่ม หรือไม่</param>
        public void _onload(string doc_no, double sum_amount, string query_where, bool load_data, bool show_bottom)
        {
            //this._clear();
            __is_save = true;
            _is_page = true;
            _result = "";
            __query_where = (query_where.Length == 0) ? "" : " and " + query_where;
            this.__query_pay_money_cash = "0.00";
            //เงินโอน
            this.__query_pay_money_transfer = "0.00";
            // เช็ค
            this.__query_pay_money_cash = "0.00";
            this.__query_pay_credit_card = "0.00";
            this.__query_pay_chq_list = "0.00";

            if (show_bottom) _myPanel2.Visible = true;
            __sum_amount = Decimal.Parse(sum_amount.ToString());
            this._doc_no = doc_no;
            if (load_data)
            {
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from cb_trans_detail where doc_ref ='" + doc_no + "'" + __query_where));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from cb_chq_list where doc_ref ='" + doc_no + "'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from cb_credit_card where doc_ref ='" + doc_no + "'"));
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                this._payChequeGrid._loadFromDataTable(((DataSet)_getData[1]).Tables[0]);
                this._payCreditCardGrid._loadFromDataTable(((DataSet)_getData[2]).Tables[0]);
            }
            this._reCalc();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc_no">เลขที่เอกสาร</param>
        /// <param name="sum_amount">จำนวนเงิน</param>
        /// <param name="query_where">รายละเอียดในการคนหา</param>
        /// <param name="load_data">ให้โหลดข้อมูล หรือ ไม่</param>
        /// <param name="show_bottom">แสดงปุ่ม หรือไม่</param>
        /// <param name="recieve_or_pay">true = รับเงิน false = จ่ายเงิน</param>
        public void _onload(string doc_no, double sum_amount, string query_where, bool load, bool show_bottom, bool recieve_or_pay, string doc_date, bool is_page)
        {
            //this._clear();
            _is_page = is_page;

            /////////this._pay_cash1._setDataNumber(_g.d.cb_trans_detail._sum_amount, _pay_money_cash);
            __is_save = true;
            __recieve_or_pay = recieve_or_pay;
            __query_where = (query_where.Length == 0) ? "" : " and " + query_where;
            _result = "";
            this.__query_pay_money_cash = "0.00";
            //เงินโอน
            this.__query_pay_money_transfer = "0.00";
            // เช็ค
            this.__query_pay_money_cash = "0.00";
            this.__query_pay_credit_card = "0.00";
            this.__query_pay_chq_list = "0.00";

            //  1=เงินสด , 2=เช็ค , 3=บัตรเครดิต 4=เงินสดย่อย
            //  13=เงินโอนเข้า  41=รับเช็ค 33=ขึ้นเงินบัตรเครดิต 35=รับเข้าเงินสดย่อย
            //  15=เงินโอนออก 43=จ่ายเช็ค  99=จ่ายเครดิต 37=เบิกเงินสดย่อย
            string _flag_transfer = "13";
            string _flag_credit = "33";
            string _flag_chq = "41";
            string _flag_cash = "35";
            string _credit_trans_type = "1";
            string _chq_trans_type = "1";
            if (!__recieve_or_pay)
            {
                _flag_transfer = "15";
                _flag_credit = "99";
                _flag_chq = "43";
                _flag_cash = "37";
                _credit_trans_type = "2";
                _chq_trans_type = "2";
            }

            if (show_bottom) _myPanel2.Visible = true;
            __sum_amount = Decimal.Parse(sum_amount.ToString());
            this._doc_no = doc_no;
            this._doc_date = doc_date;
            if (load)
            {
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from cb_trans_detail where doc_no ='" + doc_no + "' and trans_flag =" + _flag_transfer + " and trans_type =1" + __query_where));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from cb_chq_list where doc_ref ='" + doc_no + "' and chq_type = " + _chq_trans_type));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from cb_credit_card where doc_ref ='" + doc_no + "' and trans_type = " + _credit_trans_type));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from cb_trans_detail where doc_no ='" + doc_no + "' and trans_flag =" + _flag_cash + " and trans_type =4" + __query_where));
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());

                this._payChequeGrid._loadFromDataTable(((DataSet)_getData[1]).Tables[0]);
                this._payCreditCardGrid._loadFromDataTable(((DataSet)_getData[2]).Tables[0]);
                this._payDetailScreen._loadData(((DataSet)_getData[3]).Tables[0]);
            }
            _reCalc();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc_no">เลขที่เอกสาร</param>
        /// <param name="sum_amount">จำนวนเงิน</param>
        /// <param name="query_where">รายละเอียดในการคนหา</param>
        /// <param name="load_data">ให้โหลดข้อมูล หรือ ไม่</param>
        /// <param name="show_bottom">แสดงปุ่ม หรือไม่</param>
        /// <param name="recieve_or_pay">true = รับเงิน false = จ่ายเงิน</param>
        public void _onload(string doc_no, string sum_amount, string query_where, string doc_date, bool recieve_or_pay, bool is_page)
        {
            //this._clear();
            try
            {
                _is_page = is_page;
                this.__show_bottom = true;
                __is_save = true;
                __recieve_or_pay = recieve_or_pay;
                __query_where = (query_where.Length == 0) ? "" : " and " + query_where;
                _result = "";
                this.__query_pay_money_cash = "0.00";
                //เงินโอน
                this.__query_pay_money_transfer = "0.00";
                // เช็ค
                this.__query_pay_money_cash = "0.00";
                this.__query_pay_credit_card = "0.00";
                this.__query_pay_chq_list = "0.00";

                //  1=เงินสด , 2=เช็ค , 3=บัตรเครดิต 4=เงินสดย่อย
                //  13=เงินโอนเข้า  41=รับเช็ค 33=ขึ้นเงินบัตรเครดิต 35=รับเข้าเงินสดย่อย
                //  15=เงินโอนออก 43=จ่ายเช็ค  99=จ่ายเครดิต 37=เบิกเงินสดย่อย

                _myPanel2.Visible = false;
                __sum_amount = MyLib._myGlobal._decimalPhase(sum_amount.ToString());
                this._doc_no = doc_no;
                this._doc_date = doc_date;

                string _flag_transfer = "13";
                string _flag_credit = "33";
                string _flag_chq = "41";
                string _flag_cash = "35";
                string _credit_trans_type = "1";
                string _chq_trans_type = "1";
                if (!__recieve_or_pay)
                {
                    _flag_transfer = "15";
                    _flag_credit = "99";
                    _flag_chq = "43";
                    _flag_cash = "37";
                    _credit_trans_type = "2";
                    _chq_trans_type = "2";
                }
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from cb_trans_detail where doc_no ='" + doc_no + "' and trans_flag =" + _flag_transfer + " and trans_type =1" + __query_where));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from cb_chq_list where doc_ref ='" + doc_no + "' and chq_type = " + _chq_trans_type));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from cb_credit_card where doc_ref ='" + doc_no + "' and trans_type = " + _credit_trans_type));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from cb_trans_detail where doc_no ='" + doc_no + "' and trans_flag =" + _flag_cash + " and trans_type =4" + __query_where));
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());

                this._payChequeGrid._loadFromDataTable(((DataSet)_getData[1]).Tables[0]);
                this._payCreditCardGrid._loadFromDataTable(((DataSet)_getData[2]).Tables[0]);
                this._payDetailScreen._loadData(((DataSet)_getData[3]).Tables[0]);
                _reCalc();
            }
            catch
            {
            }

        }

        public void _clear()
        {
            this._payDetailScreen._clear();
            this._payCreditCardGrid._clear();
            this._payChequeGrid._clear();
            this._payTransferGrid._clear();
            this._payDepositGrid._clear();
            this._payPettyCashGrid._clear();

            if (this._gridExpense != null && this._gridIncome != null)
            {
                this._gridExpense._clear();
                this._gridIncome._clear();
            }

            __sum_amount = 0;
            this._sum_diff = 0M;
        }

        private void _myButton2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _myButtonSave_Click(object sender, EventArgs e)
        {
            _onSummit();
        }

        public string _getAutoRun(string source, string _qw)
        {
            return MyLib._myGlobal._getAutoRun(_g.d.ap_ar_trans._table, _g.d.ap_ar_trans._doc_no, _qw, "sp", true).ToString();
        }

        public string _onSummit()
        {
            __result_intsert_pay_money = "";
            _result = "";
            try
            {
                if (_chk_sum())
                {
                    //  1=เงินสด , 2=เช็ค , 3=บัตรเครดิต 4=เงินสดย่อย
                    //  13=เงินโอนเข้า  41=รับเช็ค 33=ขึ้นเงินบัตรเครดิต 35=รับเข้าเงินสดย่อย
                    //  15=เงินโอนออก 43=จ่ายเช็ค  99=จ่ายเครดิต 37=เบิกเงินสดย่อย
                    string _flag_transfer = "13";
                    string _flag_credit = "33";
                    string _flag_chq = "41";
                    string _flag_cash = "35";
                    string _credit_trans_type = "1";
                    string _chq_trans_type = "1";
                    if (!__recieve_or_pay)
                    {
                        _flag_transfer = "15";
                        _flag_credit = "99";
                        _flag_chq = "43";
                        _flag_cash = "37";
                        _credit_trans_type = "2";
                        _chq_trans_type = "2";
                    }
                    // เงินสด
                    this.__query_pay_money_cash = this._payDetailScreen._getDataNumber(_g.d.cb_trans_detail._sum_amount).ToString();
                    //เงินโอน
                    // เช็ค
                    decimal __gride_value_1 = ((MyLib._myGrid._columnType)this._payCreditCardGrid._columnList[this._payCreditCardGrid._findColumnByName(_g.d.cb_credit_card._amount)])._total;
                    this._query_pay_credit_card = __gride_value_1.ToString();
                    // เครดิต
                    decimal __gride_value_2 = ((MyLib._myGrid._columnType)this._payChequeGrid._columnList[this._payChequeGrid._findColumnByName(_g.d.cb_chq_list._amount)])._total;
                    this._query_pay_chq_list = __gride_value_2.ToString();

                    /// Grid  Credit 
                    _result = "";
                    this._payCreditCardGrid._updateRowIsChangeAll(true);
                    this._payChequeGrid._updateRowIsChangeAll(true);
                    _result += MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.cb_trans_detail._table + " where doc_no = '" + __doc_no + "' and trans_flag =" + _flag_transfer + " and trans_type = 1 " + this.__query_where);
                    _result += MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.cb_trans_detail._table + " where doc_no = '" + __doc_no + "' and trans_flag =" + _flag_credit + " and trans_type = 3 " + this.__query_where);
                    _result += MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.cb_trans_detail._table + " where doc_no = '" + __doc_no + "' and trans_flag =" + _flag_chq + " and trans_type = 2 " + this.__query_where);
                    _result += MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.cb_trans_detail._table + " where doc_no = '" + __doc_no + "' and trans_flag =" + _flag_cash + " and trans_type = 4 " + this.__query_where);
                    _result += MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.cb_credit_card._table + " where doc_ref = '" + __doc_no + "' and trans_type = " + _credit_trans_type);
                    _result += MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.cb_chq_list._table + " where doc_ref = '" + __doc_no + "' and chq_type = " + _chq_trans_type);
                    _result += this._payCreditCardGrid._createQueryRowRemove(_g.d.cb_credit_card._table);
                    _result += this._payChequeGrid._createQueryRowRemove(_g.d.cb_chq_list._table);

                    for (int _row = 0; _row < this._payCreditCardGrid._rowData.Count; _row++)
                    {
                        if (this._payCreditCardGrid._cellGet(_row, 0) == null)
                        {
                            this._payCreditCardGrid._rowData.RemoveAt(_row);
                            _row--;
                        }
                    }

                    for (int __row = 0; __row < this._payChequeGrid._rowData.Count; __row++)
                    {
                        if (this._payChequeGrid._cellGet(__row, 0) == null)
                        {
                            this._payChequeGrid._rowData.RemoveAt(__row);
                            __row--;
                        }
                    }



                    if (this._payDetailScreen._getDataNumber(_g.d.cb_trans_detail._sum_amount) > 0)
                    {
                        string _fildTransDetail = _g.d.cb_trans_detail._doc_no + ","
                            + _g.d.cb_trans_detail._doc_date + ","
                            + _g.d.cb_trans_detail._trans_flag + ","
                            + _g.d.cb_trans_detail._trans_type + ","
                            + _g.d.cb_trans_detail._sum_amount + ","
                            + _g.d.cb_trans_detail._petty_cash_code;
                        string _DataTransDetail = "'" + __doc_no + "','"
                            + MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDate(__doc_date)) + "',"
                            + _flag_cash + ",4,"
                            + this._query_pay_money_cash
                            + ",'CB-99'";
                        _result += MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.cb_trans_detail._table + " (" + _fildTransDetail + ") values (" + _DataTransDetail + ")");

                    }
                    if (this._payCreditCardGrid._rowData.Count != 0)
                    {
                        string _fildCredit = "";
                        string _DataCredit = "";
                        string __queryCredit = "";
                        string _fildTableCredit = "";
                        string _DataTableCredit = "";
                        string __queryTableCredit = "";

                        for (int __row = 0; __row < this._payCreditCardGrid._rowData.Count; __row++)
                        {
                            if (this._payCreditCardGrid._cellGet(__row, _g.d.cb_credit_card._credit_card_no).ToString().Length != 0)
                            {
                                _fildCredit = _g.d.cb_trans_detail._doc_no + ","
                                   + _g.d.cb_trans_detail._doc_date + ","
                                   + _g.d.cb_trans_detail._trans_flag + ","
                                   + _g.d.cb_trans_detail._trans_type + ","
                                   + _g.d.cb_trans_detail._credit_card_no + ","
                                   + _g.d.cb_trans_detail._sum_amount;
                                //
                                _fildTableCredit = _g.d.cb_credit_card._credit_card_no + ","
                                   //+ _g.d.cb_credit_card._credit_card_code + ","
                                   //+ _g.d.cb_credit_card._credit_get_date + ","
                                   //+ _g.d.cb_credit_card._credit_card_expire + ","
                                   + _g.d.cb_credit_card._doc_ref + ","
                                   + _g.d.cb_credit_card._amount + ","
                                   + _g.d.cb_credit_card._sum_amount + ","
                                   + _g.d.cb_credit_card._date_cut + ","
                                   + _g.d.cb_credit_card._no_approved + ","
                                   + _g.d.cb_credit_card._charge + ","
                                   //+ _g.d.cb_credit_card._bank_code + ","
                                   //+ _g.d.cb_credit_card._bank_branch + ","
                                   + _g.d.cb_credit_card._trans_type + ","
                                   + _g.d.cb_credit_card._credit_card_type;
                                //
                                _DataCredit = "'" + __doc_no + "','"
                               + MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDate(__doc_date)) + "',"
                               + _flag_credit + ",3,'"
                               + this._payCreditCardGrid._cellGet(__row, _g.d.cb_credit_card._credit_card_no) + "',"
                                   + this._payCreditCardGrid._cellGet(__row, _g.d.cb_credit_card._sum_amount);
                                //
                                _DataTableCredit = "'" + this._payCreditCardGrid._cellGet(__row, _g.d.cb_credit_card._credit_card_no) + "','"
                                   //+ this._pay_credit1._cellGet(__row, _g.d.cb_credit_card._credit_card_code) + "','"
                                   //+ MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDate(this._pay_credit1._cellGet(__row, _g.d.cb_credit_card._credit_get_date).ToString())) + "','"
                                   //+ MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDate(this._pay_credit1._cellGet(__row, _g.d.cb_credit_card._credit_card_expire).ToString())) + "','"
                                   + __doc_no + "',"
                                   + this._payCreditCardGrid._cellGet(__row, _g.d.cb_credit_card._amount) + ","
                                   + this._payCreditCardGrid._cellGet(__row, _g.d.cb_credit_card._sum_amount) + ",'"
                                   + MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDate(this._payCreditCardGrid._cellGet(__row, _g.d.cb_credit_card._date_cut).ToString())) + "','"
                                   + this._payCreditCardGrid._cellGet(__row, _g.d.cb_credit_card._no_approved) + "',"
                                   + this._payCreditCardGrid._cellGet(__row, _g.d.cb_credit_card._charge) + ","
                                   //+ this._pay_credit1._cellGet(__row, _g.d.cb_credit_card._bank_code) + "','"
                                   //+ this._pay_credit1._cellGet(__row, _g.d.cb_credit_card._bank_branch) + "',"
                                   + _credit_trans_type + ",'"
                                   + this._payCreditCardGrid._cellGet(__row, _g.d.cb_credit_card._credit_card_type) + "'";

                                //
                                __queryCredit = "insert into " + _g.d.cb_trans_detail._table + " (" + _fildCredit + ") values (" + _DataCredit + ")";
                                __queryTableCredit = "insert into " + _g.d.cb_credit_card._table + " (" + _fildTableCredit + ") values (" + _DataTableCredit + ")";

                                _result += MyLib._myUtil._convertTextToXmlForQuery(__queryCredit);
                                _result += MyLib._myUtil._convertTextToXmlForQuery(__queryTableCredit);
                            }
                        }
                        //_result += this._pay_credit1._createQueryForInsert(_g.d.cb_credit_card._table, _g.d.cb_credit_card._doc_ref + "," + _g.d.cb_credit_card._trans_type + ",", "'" + __doc_no + "'," + _credit_trans_type + ",");
                    }
                    /// Grid Chque
                    if (this._payChequeGrid._rowData.Count != 0)
                    {
                        string _fildChq = "";
                        string _DataChq = "";
                        string __queryChq = "";
                        string _fildTableChq = "";
                        string _DataTableChq = "";
                        string __queryTableChq = "";
                        for (int __row = 0; __row < this._payChequeGrid._rowData.Count; __row++)
                        {
                            string ct = this._payChequeGrid._cellGet(__row, _g.d.cb_chq_list._chq_number).ToString();
                            if (this._payChequeGrid._cellGet(__row, _g.d.cb_chq_list._chq_number).ToString().Length != 0)
                            {
                                _fildChq = _g.d.cb_trans_detail._doc_no + ","
                               + _g.d.cb_trans_detail._doc_date + ","
                               + _g.d.cb_trans_detail._trans_flag + ","
                               + _g.d.cb_trans_detail._trans_type + ","
                               + _g.d.cb_trans_detail._trans_number + ","
                               + _g.d.cb_trans_detail._sum_amount + ","
                               + _g.d.cb_trans_detail._bank_code + ","
                               + _g.d.cb_trans_detail._bank_branch;
                                //
                                _fildTableChq = _g.d.cb_chq_list._doc_ref + ","
                               + _g.d.cb_chq_list._chq_get_date + ","
                               + _g.d.cb_chq_list._chq_due_date + ","
                               + _g.d.cb_chq_list._chq_number + ","
                               + _g.d.cb_chq_list._owner_name + ","
                               + _g.d.cb_chq_list._amount + ","
                               + _g.d.cb_chq_list._bank_code + ","
                               + _g.d.cb_chq_list._bank_branch + ","
                               + _g.d.cb_chq_list._chq_type;
                                //
                                _DataChq = "'" + __doc_no + "','"
                                + MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDate(__doc_date)) + "',"
                                + _flag_chq + ",2,'"
                                + this._payChequeGrid._cellGet(__row, _g.d.cb_chq_list._chq_number) + "',"
                                + this._payChequeGrid._cellGet(__row, _g.d.cb_chq_list._amount) + ",'"
                                + this._payChequeGrid._cellGet(__row, _g.d.cb_chq_list._bank_code) + "','"
                                + this._payChequeGrid._cellGet(__row, _g.d.cb_chq_list._bank_branch) + "'";

                                string c1 = MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDate(this._payChequeGrid._cellGet(__row, _g.d.cb_chq_list._chq_get_date).ToString())).ToString();
                                string c2 = MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDate(this._payChequeGrid._cellGet(__row, _g.d.cb_chq_list._chq_due_date).ToString())).ToString();
                                string c3 = this._payChequeGrid._cellGet(__row, _g.d.cb_chq_list._chq_number).ToString();
                                string c4 = this._payChequeGrid._cellGet(__row, _g.d.cb_chq_list._owner_name).ToString();
                                string c5 = this._payChequeGrid._cellGet(__row, _g.d.cb_chq_list._amount).ToString();
                                string c6 = this._payChequeGrid._cellGet(__row, _g.d.cb_chq_list._bank_code).ToString();
                                string c7 = this._payChequeGrid._cellGet(__row, _g.d.cb_chq_list._bank_branch).ToString();
                                string c8 = _chq_trans_type.ToString();
                                string _setDataCHQNumber = (this._payChequeGrid._cellGet(__row, _g.d.cb_chq_list._chq_number).ToString().Length == 0) ? "" : "'" + this._payChequeGrid._cellGet(__row, _g.d.cb_chq_list._chq_number).ToString() + "'";
                                //
                                _DataTableChq = "'" + __doc_no + "','"
                                + MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDate(this._payChequeGrid._cellGet(__row, _g.d.cb_chq_list._chq_get_date).ToString())) + "','"
                                + MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDate(this._payChequeGrid._cellGet(__row, _g.d.cb_chq_list._chq_due_date).ToString())) + "','"
                                + this._payChequeGrid._cellGet(__row, _g.d.cb_chq_list._chq_number) + "','"
                                + this._payChequeGrid._cellGet(__row, _g.d.cb_chq_list._owner_name) + "',"
                                + this._payChequeGrid._cellGet(__row, _g.d.cb_chq_list._amount) + ",'"
                                + this._payChequeGrid._cellGet(__row, _g.d.cb_chq_list._bank_code) + "','"
                                + this._payChequeGrid._cellGet(__row, _g.d.cb_chq_list._bank_branch) + "',"
                                + _chq_trans_type;
                                //

                                __queryChq = "insert into " + _g.d.cb_trans_detail._table + " (" + _fildChq + ") values (" + _DataChq + ")";
                                __queryTableChq = "insert into " + _g.d.cb_chq_list._table + " (" + _fildTableChq + ") values (" + _DataTableChq + ")";

                                _result += MyLib._myUtil._convertTextToXmlForQuery(__queryChq);
                                _result += MyLib._myUtil._convertTextToXmlForQuery(__queryTableChq);
                            }
                        }
                        //_result += this._pay_cheque1._createQueryForInsert(_g.d.cb_chq_list._table, _g.d.cb_chq_list._doc_ref + "," + _g.d.cb_chq_list._chq_type + ",", "'" + __doc_no + "'," + _chq_trans_type + ",");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
                __is_save = false;

            }
            return _result.ToString();
        }

        /// <summary>
        /// ยอดเงินสด
        /// </summary>
        public decimal _payCashAmount()
        {
            return this._payDetailScreen._getDataNumber(_g.d.cb_trans_detail._sum_amount);
        }

        /// <summary>
        /// ยอดเงินโอน
        /// </summary>
        /// <returns></returns>
        public decimal _payTransferAmount()
        {
            return 0; //  this._pay_transfer1._getDataNumber(_g.d.cb_trans_detail._sum_amount);
        }

        /// <summary>
        /// ยอดบัตรเครดิต
        /// </summary>
        /// <returns></returns>
        public decimal _payCreditCardAmount()
        {
            return ((MyLib._myGrid._columnType)this._payCreditCardGrid._columnList[this._payCreditCardGrid._findColumnByName(_g.d.cb_credit_card._amount)])._total;
        }

        /// <summary>
        /// ยอดเช็ค
        /// </summary>
        /// <returns></returns>
        public decimal _payChqAmount()
        {
            return ((MyLib._myGrid._columnType)this._payChequeGrid._columnList[this._payChequeGrid._findColumnByName(_g.d.cb_chq_list._amount)])._total;
        }

        /// <summary>
        /// ตรวจสอบว่าเงินที่จ่าย กับจำนวนที่ต้องจ่ายเท่ากันหรือไม่
        /// </summary>
        /// <returns></returns>
        bool _chk_sum()
        {
            string __message = "กรุณาตรวจสอบ จำนวนเงินชำระเงินไม่ครบตามจำนวน \n\r ต้องการทำขั้นตอนต่อไป กด YES \n\r ต้องการกลับไปตรวจสอบใหม่ กด NO";
            bool _bool = false;
            //if (this._myShadowLabel.Text
            if (this.__sum_amount != 0)
            {
                decimal __sum_value_2 = this._payCashAmount();// this._pay_cash1._getDataNumber(_g.d.cb_trans_detail._sum_amount);
                decimal __sum_value_3 = this._payTransferAmount(); //  this._pay_transfer1._getDataNumber(_g.d.cb_trans_detail._sum_amount);
                decimal __gride_value_1 = this._payCreditCardAmount(); //  ((MyLib._myGrid._columnType)this._pay_credit1._columnList[this._pay_credit1._findColumnByName(_g.d.cb_credit_card._amount)])._total;
                decimal __gride_value_2 = this._payChqAmount(); // ((MyLib._myGrid._columnType)this._pay_cheque1._columnList[this._pay_cheque1._findColumnByName(_g.d.cb_chq_list._amount)])._total;
                //
                //
                if (this.__sum_amount == (__sum_value_2 + __sum_value_3 + __gride_value_1 + __gride_value_2 + __vat_amount))
                {
                    /*if (__sum_value_3 > 0 && this._pay_transfer1._getDataStr(_g.d.cb_trans_detail._pass_book_code).Length < 1)
                    {
                        __is_save = false;
                        MessageBox.Show(MyLib._myGlobal._resource("กรุณากรอกข้อมูล สมุดธนาคาร"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (__sum_value_3 > 0 && this._pay_transfer1._getDataStr(_g.d.cb_trans_detail._bank_code).Length < 1)
                    {
                        __is_save = false;
                        MessageBox.Show(MyLib._myGlobal._resource("กรุณากรอกข้อมูล สมุดธนาคาร"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        __is_save = true;
                        _bool = true;
                    }*/
                }
                else
                {
                    if (_is_msg)
                    {
                        __is_save = false;
                        DialogResult dlgResult = MessageBox.Show(MyLib._myGlobal._resource(__message), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dlgResult == DialogResult.Yes)
                        {
                            __is_save = true;
                            _bool = true;
                        }
                        else if (dlgResult == DialogResult.No)
                        {
                            __is_save = false;
                        }
                    }
                    else
                    {
                        __is_save = true;
                        _bool = true;
                    }
                }
            }
            else
            {
                if (_is_msg)
                {
                    DialogResult dlgResult = MessageBox.Show(MyLib._myGlobal._resource(__message), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        __is_save = true;
                        _bool = true;
                    }
                    else if (dlgResult == DialogResult.No)
                    {
                        __is_save = false;
                    }
                }
                else
                {
                    __is_save = true;
                    _bool = true;
                }
            }
            return _bool;
        }

        private string _convetToDecimal(string str)
        {
            if (str != "")
            {
                try
                {
                    //str = str.Replace("฿", "");
                    return Convert.ToDecimal(str).ToString("n");
                }
                catch
                {
                }
            }
            return null;
        }

        private void _ap_pay_Load(object sender, EventArgs e)
        {
            _is_msg = true;
        }

        private void _pay_cash1_Load(object sender, EventArgs e)
        {

        }

        private void _calcForCash_Click(object sender, EventArgs e)
        {
            this._reCalc();
            decimal __totalPay = this._payDetailScreen._getDataNumber(_g.d.cb_trans._total_amount_pay);
            decimal __getCash = this._payDetailScreen._getDataNumber(_g.d.cb_trans._cash_amount);
            decimal __calc = this._payDetailScreen._getDataNumber(_g.d.cb_trans._total_net_amount) - (__totalPay - __getCash);
            this._payDetailScreen._setDataNumber(_g.d.cb_trans._cash_amount, __calc);
            this._reCalc();
        }

        private void _findChqButton_Click(object sender, EventArgs e)
        {
            string __getCustCode = this._getCustCode();
            if (__getCustCode.Length == 0)
            {
                MessageBox.Show(MyLib._myGlobal._resource("กรุณาเลือกลูกค้า"));
                return;
            }

            if (_chqSearchForm == null)
            {
                _chqSearchForm = new _searchChqForm(this._icTransControlType);
                _chqSearchForm._processButton.Click += _chqSearchForm_processButton_Click;
            }

            this._chqSearchForm.Text = MyLib._myGlobal._resource("ค้นหาเช็ค") + " : " + __getCustCode;
            this._chqSearchForm._process(__getCustCode);
            // ลบรายการที่เลือกไปแล้ว
            for (int __row = 0; __row < this._payChequeGrid._rowData.Count; __row++)
            {
                string __docNo = this._payChequeGrid._cellGet(__row, _g.d.cb_trans_detail._trans_number).ToString();
                int __addr = this._chqSearchForm._resultGrid._findData(this._chqSearchForm._resultGrid._findColumnByName(_g.d.cb_trans_detail._chq_number), __docNo);
                if (__addr != -1)
                {
                    this._chqSearchForm._resultGrid._rowData.RemoveAt(__addr);
                }
            }
            //
            this._chqSearchForm.ShowDialog();
        }

        void _chqSearchForm_processButton_Click(object sender, EventArgs e)
        {
            this._chqSearchForm.Close();
            // เพิ่มบรรทัดใหม่
            for (int __row = 0; __row < this._chqSearchForm._resultGrid._rowData.Count; __row++)
            {
                // ลบบรรทัดที่ว่าง
                int __rowDelete = 0;
                while (__rowDelete < this._payChequeGrid._rowData.Count)
                {
                    if (this._payChequeGrid._cellGet(__rowDelete, _g.d.cb_trans_detail._trans_number).ToString().Trim().Length == 0)
                    {
                        this._payChequeGrid._rowData.RemoveAt(__rowDelete);
                    }
                    else
                    {
                        __rowDelete++;
                    }
                }
                if ((int)this._chqSearchForm._resultGrid._cellGet(__row, _g.d.ap_ar_resource._select) == 1)
                {
                    int __rowAddr = this._payChequeGrid._addRow();
                    this._payChequeGrid._cellUpdate(__rowAddr, _g.d.cb_trans_detail._chq_on_hand, 1, true);

                    this._payChequeGrid._cellUpdate(__rowAddr, _g.d.cb_trans_detail._pass_book_code, this._chqSearchForm._resultGrid._cellGet(__row, _g.d.cb_trans_detail._pass_book_code, true).ToString(), true);
                    this._payChequeGrid._cellUpdate(__rowAddr, _g.d.cb_trans_detail._bank_code, this._chqSearchForm._resultGrid._cellGet(__row, _g.d.cb_trans_detail._bank_code, true).ToString(), true);
                    this._payChequeGrid._cellUpdate(__rowAddr, _g.d.cb_trans_detail._bank_branch, this._chqSearchForm._resultGrid._cellGet(__row, _g.d.cb_trans_detail._bank_branch, true).ToString(), true);
                    this._payChequeGrid._cellUpdate(__rowAddr, _g.d.cb_trans_detail._trans_number, this._chqSearchForm._resultGrid._cellGet(__row, _g.d.cb_trans_detail._chq_number).ToString(), true);

                    this._payChequeGrid._cellUpdate(__rowAddr, _g.d.cb_trans_detail._balance_amount, MyLib._myGlobal._decimalPhase(this._chqSearchForm._resultGrid._cellGet(__row, _g.d.cb_trans_detail._balance_amount).ToString()), true);
                    this._payChequeGrid._cellUpdate(__rowAddr, _g.d.cb_trans_detail._sum_amount, MyLib._myGlobal._decimalPhase(this._chqSearchForm._resultGrid._cellGet(__row, _g.d.cb_trans_detail._chq_amount).ToString()), false);
                    this._payChequeGrid._cellUpdate(__rowAddr, _g.d.cb_trans_detail._amount, MyLib._myGlobal._decimalPhase(this._chqSearchForm._resultGrid._cellGet(__row, _g.d.cb_trans_detail._balance_amount).ToString()), true);
                    this._payChequeGrid._cellUpdate(__rowAddr, _g.d.cb_trans_detail._chq_due_date, MyLib._myGlobal._convertDateFromQuery(this._chqSearchForm._resultGrid._cellGet(__row, _g.d.cb_trans_detail._chq_due_date).ToString()), true);

                    this._payChequeGrid._cellUpdate(__rowAddr, _g.d.cb_trans_detail._doc_ref, this._chqSearchForm._resultGrid._cellGet(__row, _g.d.cb_trans_detail._doc_no).ToString(), true);

                    // update สถานะ เช็คในมือ

                    // bank & branch
                    // amount
                }
            }
            this._payChequeGrid._gotoCell(this._payChequeGrid._rowData.Count, this._payChequeGrid._findColumnByName(_g.d.ap_ar_trans_detail._billing_no));
        }
    }

    public class _payChequeGridControl : MyLib._myGrid
    {
        public delegate void _refreshDataEvent(_payChequeGridControl sender);
        public event _refreshDataEvent _refreshData;
        //
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        ArrayList _search_data_full_buffer = new ArrayList();
        ArrayList _search_data_full_buffer_name = new ArrayList();
        int _search_data_full_buffer_addr = -1;
        MyLib._searchDataFull _search_data_full_pointer;
        string _search_data_full_name = "";
        //string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
        string __formatNumber = MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal.ToString());
        private _g.g._transControlTypeEnum _mode;
        public DateTime _defaultDate = new DateTime();
        public int _chqType = 0;

        public _payChequeGridControl()
        {
            this._table_name = _g.d.cb_trans_detail._table;
            this._width_by_persent = true;
            this.AllowDrop = true;
            this._isEdit = true;

            this.ColumnBackgroundAuto = false;
            this.ColumnBackgroundEnd = Color.FromArgb(198, 220, 249);
            this.RowOddBackground = Color.AliceBlue;
            this.AutoSize = true;
            this.Dock = DockStyle.Fill;

            this._clickSearchButton += new MyLib.SearchEventHandler(_pay_credit__clickSearchButton);
            this._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_pay_credit__alterCellUpdate);
            //this._beforeDisplayRendering += new MyLib.BeforeDisplayRenderRowEventHandler(_payChequeGridControl__beforeDisplayRendering);

            this._beforeInputCell += _payChequeGridControl__beforeInputCell;
            this.Invalidate();
        }

        private bool _payChequeGridControl__beforeInputCell(MyLib._myGrid sender, int row, int column)
        {
            if (row > -1 && row < this._rowData.Count)
            {
                if (column == this._findColumnByName(_g.d.cb_trans_detail._chq_due_date))
                {
                    if (this._cellGet(row, _g.d.cb_trans_detail._chq_on_hand).ToString().Equals("1"))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        MyLib.BeforeDisplayRowReturn _payChequeGridControl__beforeDisplayRendering(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData, Graphics e)
        {
            MyLib.BeforeDisplayRowReturn __result = senderRow;
            if (row < sender._rowData.Count && columnName.Equals(_g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._chq_on_hand))
            {
                if (sender._cellGet(row, columnName) == null)
                {
                    ((ArrayList)senderRow.newData)[columnNumber] = MyLib._myResource._findResource(_g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._new_chq)._str;
                }
                else
                {
                    int __mode = (int)MyLib._myGlobal._decimalPhase(sender._cellGet(row, sender._findColumnByName(columnName)).ToString());
                    //int __select = (int)MyLib._myGlobal._decimalPhase(sender._cellGet(row, sender._findColumnByName(this._gridSelect)).ToString());
                    ((ArrayList)senderRow.newData)[columnNumber] = "";
                    //if (__select == 1)
                    //{
                    switch (__mode)
                    {
                        case 1:
                            ((ArrayList)senderRow.newData)[columnNumber] = MyLib._myResource._findResource(_g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._old_chq)._str;
                            break;
                        default:
                            ((ArrayList)senderRow.newData)[columnNumber] = MyLib._myResource._findResource(_g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._new_chq)._str;
                            break;
                    }
                }
                //}
            }
            return __result;
        }

        public void _build(_g.g._transControlTypeEnum mode)
        {
            this._mode = mode;
            this._columnList.Clear();
            this._addColumn(_g.d.cb_trans_detail._chq_on_hand, 10, 0, 10, false, false, true);

            switch (mode)
            {
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 1, 10, true, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 20, true, false, true, false, "", "", "", _g.d.cb_trans_detail._chq_number);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumber, "", "", _g.d.cb_trans_detail._chq_amount);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 10, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 1, 15, true, false, true);
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 25, true, false, true);
                    this._chqType = 1;
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 20, true, false, true, false, "", "", "", _g.d.cb_trans_detail._chq_number);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumber, "", "", _g.d.cb_trans_detail._chq_amount);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 10, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 1, 15, true, false, true);
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 25, true, false, true);
                    this._chqType = 1;
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 20, true, false, true, false, "", "", "", _g.d.cb_trans_detail._chq_number);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumber, "", "", _g.d.cb_trans_detail._chq_amount);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 10, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 1, 15, true, false, true);
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 25, true, false, true);
                    this._chqType = 2;
                    break;
                case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 20, true, false, true, false, "", "", "", _g.d.cb_trans_detail._chq_number);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumber, "", "", _g.d.cb_trans_detail._chq_amount);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 10, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 1, 15, true, false, true);
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 25, true, false, true);
                    this._chqType = 2;
                    break;
                case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 20, true, false, true, false, "", "", "", _g.d.cb_trans_detail._chq_number);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumber, "", "", _g.d.cb_trans_detail._chq_amount);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 10, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 1, 15, true, false, true);
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 25, true, false, true);
                    this._chqType = 1;
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 20, true, false, true, false, "", "", "", _g.d.cb_trans_detail._chq_number);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumber, "", "", _g.d.cb_trans_detail._chq_amount);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 10, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 1, 15, true, false, true);
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 25, true, false, true);
                    this._chqType = 2;
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 20, true, false, true, false, "", "", "", _g.d.cb_trans_detail._chq_number);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumber, "", "", _g.d.cb_trans_detail._chq_amount);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 10, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 1, 15, true, false, true);
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 25, true, false, true);
                    this._chqType = 1;
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 20, true, false, true, false, "", "", "", _g.d.cb_trans_detail._chq_number);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumber, "", "", _g.d.cb_trans_detail._chq_amount);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 10, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 1, 15, true, false, true);
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 25, true, false, true);
                    this._chqType = 2;
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 20, true, false, true, false, "", "", "", _g.d.cb_trans_detail._chq_number);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumber, "", "", _g.d.cb_trans_detail._chq_amount);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 10, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 1, 15, true, false, true);
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 25, true, false, true);
                    this._chqType = 2;
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 1, 10, true, false, true, true);
                    if (MyLib._myGlobal._OEMVersion == "imex" || MyLib._myGlobal._OEMVersion == "ims")
                    {
                        this._addColumn(_g.d.cb_trans_detail._external_chq, 10, 10, 10, true, false);

                    }
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 20, true, false, true, false, "", "", "", _g.d.cb_trans_detail._chq_number);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumber, "", "", _g.d.cb_trans_detail._chq_amount);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 10, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 1, 15, true, false, true);
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 25, true, false, true);
                    this._chqType = 1;
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 1, 10, true, false, true, true);
                    if (MyLib._myGlobal._OEMVersion == "imex" || MyLib._myGlobal._OEMVersion == "ims")
                    {
                        this._addColumn(_g.d.cb_trans_detail._external_chq, 10, 10, 10, true, false);
                    }
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 20, true, false, true, false, "", "", "", _g.d.cb_trans_detail._chq_number);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumber, "", "", _g.d.cb_trans_detail._chq_amount);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 10, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 1, 15, true, false, true);
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 25, true, false, true);
                    this._chqType = 1;
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 20, true, false, true, false, "", "", "", _g.d.cb_trans_detail._chq_number);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumber, "", "", _g.d.cb_trans_detail._chq_amount);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 10, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 1, 15, true, false, true);
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 25, true, false, true);
                    this._chqType = 2;
                    break;
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 20, true, false, true, false, "", "", "", _g.d.cb_trans_detail._chq_number);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumber, "", "", _g.d.cb_trans_detail._chq_amount);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 10, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 1, 15, true, false, true);
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 25, true, false, true);
                    this._chqType = 2;
                    break;
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 1, 10, true, false, true, true);
                    if (MyLib._myGlobal._OEMVersion == "imex" || MyLib._myGlobal._OEMVersion == "ims")
                    {
                        this._addColumn(_g.d.cb_trans_detail._external_chq, 10, 10, 10, true, false);
                    }
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 20, true, false, true, false, "", "", "", _g.d.cb_trans_detail._chq_number);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumber, "", "", _g.d.cb_trans_detail._chq_amount);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 10, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 1, 15, true, false, true);
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 25, true, false, true);
                    this._chqType = 1;
                    break;
                case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า:
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 1, 10, true, false, true, true);
                    if (MyLib._myGlobal._OEMVersion == "imex" || MyLib._myGlobal._OEMVersion == "ims")
                    {
                        this._addColumn(_g.d.cb_trans_detail._external_chq, 10, 10, 10, true, false);
                    }
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 20, true, false, true, false, "", "", "", _g.d.cb_trans_detail._chq_number);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumber, "", "", _g.d.cb_trans_detail._chq_amount);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 10, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 1, 15, true, false, true);
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 25, true, false, true);
                    this._chqType = 1;
                    break;
                case _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 20, true, false, true, false, "", "", "", _g.d.cb_trans_detail._chq_number);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumber, "", "", _g.d.cb_trans_detail._chq_amount);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 10, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 1, 15, true, false, true);
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 25, true, false, true);
                    this._chqType = 2;
                    break;
                case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ:
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 1, 10, true, false, true, true);
                    if (MyLib._myGlobal._OEMVersion == "imex" || MyLib._myGlobal._OEMVersion == "ims")
                    {
                        this._addColumn(_g.d.cb_trans_detail._external_chq, 10, 10, 10, true, false);
                    }
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 20, true, false, true, false, "", "", "", _g.d.cb_trans_detail._chq_number);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumber, "", "", _g.d.cb_trans_detail._chq_amount);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 10, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 1, 15, true, false, true);
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 25, true, false, true);
                    this._chqType = 1;
                    break;
                case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 20, true, false, true, false, "", "", "", _g.d.cb_trans_detail._chq_number);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumber, "", "", _g.d.cb_trans_detail._chq_amount);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 10, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 1, 15, true, false, true);
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 25, true, false, true);
                    this._chqType = 2;
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 20, true, false, true, false, "", "", "", _g.d.cb_trans_detail._chq_number);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumber, "", "", _g.d.cb_trans_detail._chq_amount);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 10, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 1, 15, true, false, true);
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 25, true, false, true);
                    this._chqType = 2;
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 20, true, false, true, false, "", "", "", _g.d.cb_trans_detail._chq_number);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumber, "", "", _g.d.cb_trans_detail._chq_amount);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 10, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 1, 15, true, false, true);
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 25, true, false, true);
                    this._chqType = 2;
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 20, true, false, true, false, "", "", "", _g.d.cb_trans_detail._chq_number);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumber, "", "", _g.d.cb_trans_detail._chq_amount);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 10, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 1, 15, true, false, true);
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 25, true, false, true);
                    this._chqType = 2;
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน:
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 20, true, false, true, false, "", "", "", _g.d.cb_trans_detail._chq_number);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumber, "", "", _g.d.cb_trans_detail._chq_amount);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 10, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 1, 15, true, false, true);
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 25, true, false, true);
                    this._chqType = 1;
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 20, true, false, true, false, "", "", "", _g.d.cb_trans_detail._chq_number);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumber, "", "", _g.d.cb_trans_detail._chq_amount);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 10, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 1, 15, true, false, true);
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 25, true, false, true);
                    this._chqType = 2;
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน:
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 20, true, false, true, false, "", "", "", _g.d.cb_trans_detail._chq_number);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumber, "", "", _g.d.cb_trans_detail._chq_amount);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 10, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 1, 15, true, false, true);
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 25, true, false, true);
                    this._chqType = 1;
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 1, 10, true, false, true, true);
                    if (MyLib._myGlobal._OEMVersion == "imex" || MyLib._myGlobal._OEMVersion == "ims")
                    {
                        this._addColumn(_g.d.cb_trans_detail._external_chq, 10, 10, 10, true, false);
                    }
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 20, true, false, true, false, "", "", "", _g.d.cb_trans_detail._chq_number);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumber, "", "", _g.d.cb_trans_detail._chq_amount);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 10, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 1, 15, true, false, true);
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 25, true, false, true);
                    this._chqType = 1;
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 20, true, false, true, false, "", "", "", _g.d.cb_trans_detail._chq_number);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumber, "", "", _g.d.cb_trans_detail._chq_amount);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 10, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 1, 15, true, false, true);
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 25, true, false, true);
                    this._chqType = 2;
                    break;
                default:
                    if (SystemInformation.ComputerName.ToLower().IndexOf("jead8") != -1 || SystemInformation.ComputerName.ToLower().IndexOf("toe-pc") != -1)
                    {
                        MessageBox.Show("Case not found : " + mode.ToString());
                    }
                    break;
            }
            this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
            this._addColumn(_g.d.cb_trans_detail._balance_amount, 3, 0, 0, false, true, true);
            this._addColumn(_g.d.cb_trans_detail._doc_ref, 1, 0, 0, false, true, true);


            this._cellComboBoxGet += _payChequeGridControl__cellComboBoxGet;
            this._cellComboBoxItem += _payChequeGridControl__cellComboBoxItem;

            this._width_by_persent = true;
            this._calcPersentWidthToScatter();
        }

        string[] _chq_onhand_type = { MyLib._myResource._findResource(_g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._new_chq)._str, MyLib._myResource._findResource(_g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._old_chq)._str };

        string[] _external_cqh = { "เช็คในเขต", "เช็คนอกเขต" };
        object[] _payChequeGridControl__cellComboBoxItem(object sender, int row, int column)
        {
            if (column == this._findColumnByName(_g.d.cb_trans_detail._external_chq))
            {
                return _external_cqh;
            }

            if (column == this._findColumnByName(_g.d.cb_trans_detail._chq_on_hand))
            {
                return _chq_onhand_type;
            }

            return null;
        }

        string _payChequeGridControl__cellComboBoxGet(object sender, int row, int column, string columnName, int select)
        {
            if (column == this._findColumnByName(_g.d.cb_trans_detail._external_chq))
            {
                return _external_cqh[(select == -1) ? 0 : select].ToString();
            }

            if (column == this._findColumnByName(_g.d.cb_trans_detail._chq_on_hand))
            {
                return _chq_onhand_type[(select == -1) ? 0 : select].ToString(); ;
            }

            return "0";

        }

        void _pay_credit__alterCellUpdate(object sender, int row, int column)
        {
            this._search_screen_book(row);
            this._search_screen_bank(row);
            if (this._findColumnByName(_g.d.cb_trans_detail._chq_due_date) != -1)
            {
                DateTime __getDate = (DateTime)this._cellGet(row, _g.d.cb_trans_detail._chq_due_date); //MyLib._myGlobal._convertDate(((DateTime)this._cellGet(row, _g.d.cb_trans_detail._chq_due_date)).ToString(MyLib._myGlobal._cultureInfo())); toe
                if (__getDate.Year < 1900)
                {
                    this._cellUpdate(row, _g.d.cb_trans_detail._chq_due_date, this._defaultDate, false);
                }
            }

            int __columnSumAmount = this._findColumnByName(_g.d.cb_trans_detail._sum_amount); // มูลค่าเช็ค
            int __columnAmount = this._findColumnByName(_g.d.cb_trans_detail._amount);

            if (__columnSumAmount != -1 && column == __columnSumAmount)
            {
                decimal __amount = MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.cb_trans_detail._amount).ToString());
                decimal __chqAmount = MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.cb_trans_detail._sum_amount).ToString());
                if (__amount == 0 && __chqAmount > 0)
                {
                    this._cellUpdate(row, _g.d.cb_trans_detail._amount, __chqAmount, false);
                }
            }

            // เช็คป้อนต่ำกว่า มูลค่าเช็ค
            if (column == __columnSumAmount || column == __columnAmount)
            {
                decimal __amount = MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.cb_trans_detail._amount).ToString());
                decimal __chqAmount = MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.cb_trans_detail._sum_amount).ToString());
                decimal __balanceAmount = MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.cb_trans_detail._balance_amount).ToString());
                int __chq_on_hand = MyLib._myGlobal._intPhase(this._cellGet(row, _g.d.cb_trans_detail._chq_on_hand).ToString());

                if (__chq_on_hand == 0)
                {
                    if (__amount > __chqAmount)
                    {
                        // nofify and update
                        MessageBox.Show(MyLib._myGlobal._resource("จำนวนเงินไม่สัมพันธ์กับมูลค่าเช็ค"), MyLib._myGlobal._resource("warning"));
                        this._cellUpdate(row, _g.d.cb_trans_detail._amount, __chqAmount, false);
                    }
                }
                else
                {
                    // เช็คจากมูลค่าคลเหลือเช็ค
                    if (__amount > __balanceAmount)
                    {
                        // nofify and update
                        MessageBox.Show(MyLib._myGlobal._resource("จำนวนเงินไม่สัมพันธ์กับมูลค่าคงเหลือเช็ค"), MyLib._myGlobal._resource("warning"));
                        this._cellUpdate(row, _g.d.cb_trans_detail._amount, __balanceAmount, false);
                    }
                }
            }

            int __columnChqNumber = this._findColumnByName(_g.d.cb_trans_detail._trans_number);

            if (column == __columnChqNumber)
            {
                string __chqNumber = this._cellGet(row, column).ToString();

                int __payType = _g.g._transTypeGlobal._payType(this._mode);

                string __queryCheckChqNumber = "select count(*) as xcount from cb_chq_list where " + _g.d.cb_chq_list._chq_type + "=" + __payType.ToString() + " and " + _g.d.cb_chq_list._chq_number + "=\'" + __chqNumber + "\' ";
                DataTable __result = _myFrameWork._queryShort(__queryCheckChqNumber).Tables[0];

                if (__result.Rows.Count > 0 && MyLib._myGlobal._intPhase(__result.Rows[0][0].ToString()) > 0)
                {
                    MessageBox.Show("หมายเลขเช็ค " + __chqNumber + " ซ้ำ", "เตือน");
                }
            }


            //
            if (this._refreshData != null)
            {
                this._refreshData(this);
            }
        }

        void _pay_credit__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            string __queryWhere = "";
            if (e._columnName.Equals(_g.d.cb_trans_detail._pass_book_code))
            {
                _search_data_full_name = _g.g._search_screen_สมุดเงินฝาก;
                __queryWhere = "";
            }
            else
                if (e._columnName.Equals(_g.d.cb_trans_detail._bank_code))
            {
                _search_data_full_name = _g.g._search_screen_bank;
                __queryWhere = "";
            }
            else
                    if (e._columnName.Equals(_g.d.cb_trans_detail._bank_branch))
            {
                _search_data_full_name = _g.g._search_screen_bank_branch;
                __queryWhere = _g.d.erp_bank_branch._bank_code + " = '" + this._cellGet(e._row, _g.d.cb_trans_detail._bank_code) + "'";
            }

            Boolean __found = false;
            int __addr = 0;
            for (int __loop = 0; __loop < this._search_data_full_buffer_name.Count; __loop++)
            {
                if (this._search_data_full_buffer_name[__loop].ToString().Equals(_search_data_full_name.ToLower()))
                {
                    __addr = __loop;
                    __found = true;
                    break;
                }
            }
            if (__found == false)
            {
                __addr = this._search_data_full_buffer_name.Add(_search_data_full_name.ToLower());
                this._search_data_full_buffer.Add((MyLib._searchDataFull)new MyLib._searchDataFull());
            }
            this._search_data_full_pointer = (MyLib._searchDataFull)this._search_data_full_buffer[__addr];
            this._search_data_full_buffer_addr = __addr;

            if (!this._search_data_full_pointer._name.Equals(_search_data_full_name.ToLower()))
            {
                this._search_data_full_pointer.Text = e._columnName;
                this._search_data_full_pointer._name = _search_data_full_name;
                this._search_data_full_pointer._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_search_data_full_pointer__searchEnterKeyPress);
                this._search_data_full_pointer._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full_pointer__searchEnterKeyPress);
                this._search_data_full_pointer._dataList._loadViewFormat(_search_data_full_pointer._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._search_data_full_pointer._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._search_data_full_pointer._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            }
            MyLib._myGlobal._startSearchBox(this._inputTextBox, e._columnName, this._search_data_full_pointer, false, true, __queryWhere);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            string __result = "";
            this._search_data_full_pointer.Close();
            if (this._search_data_full_pointer._name.Equals(_g.g._search_screen_สมุดเงินฝาก))
            {
                __result = this._search_data_full_pointer._dataList._gridData._cellGet(e._row, _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._code).ToString();
                this._cellUpdate(this._selectRow, _g.d.cb_trans_detail._pass_book_code, __result, true);
            }
            else
                if (this._search_data_full_pointer._name.Equals(_g.g._search_screen_bank))
            {
                __result = this._search_data_full_pointer._dataList._gridData._cellGet(e._row, _g.d.erp_bank._table + "." + _g.d.erp_bank._code).ToString();
                this._cellUpdate(this._selectRow, _g.d.cb_trans_detail._bank_code, __result, true);
            }
            else
                    if (this._search_data_full_pointer._name.Equals(_g.g._search_screen_bank_branch))
            {
                __result = this._search_data_full_pointer._dataList._gridData._cellGet(e._row, _g.d.erp_bank_branch._table + "." + _g.d.erp_bank_branch._code).ToString();
                this._cellUpdate(this._selectRow, _g.d.cb_trans_detail._bank_branch, __result, true);
            }
            SendKeys.Send("{ENTER}");
        }

        void _search_data_full_pointer__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
            SendKeys.Send("{ENTER}");
        }

        private void _searchByParent(MyLib._myGrid sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, row);
        }

        private void _searchAll(string name, int row)
        {
            if (name.Equals(_g.g._search_screen_สมุดเงินฝาก))
            {
                string result = (string)this._search_data_full_pointer._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    this._search_data_full_pointer.Close();
                    this._cellUpdate(this._selectRow, _g.d.cb_trans_detail._pass_book_code, result, true);
                    this._search_screen_book(row);
                }
            }
            else
                if (name.Equals(_g.g._search_screen_bank))
            {
                string result = (string)this._search_data_full_pointer._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    this._search_data_full_pointer.Close();
                    this._cellUpdate(this._selectRow, _g.d.cb_trans_detail._bank_code, result, true);
                    this._search_screen_bank(row);
                }
            }
            else
                    if (name.Equals(_g.g._search_screen_bank_branch))
            {
                string result = (string)this._search_data_full_pointer._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    this._search_data_full_pointer.Close();
                    this._cellUpdate(this._selectRow, _g.d.cb_trans_detail._bank_branch, result, true);
                }
            }
        }

        void _search_screen_book(int row)
        {
            if (this._findColumnByName(_g.d.cb_trans_detail._pass_book_code) != -1)
            {
                string __code = this._cellGet(row, _g.d.cb_trans_detail._pass_book_code).ToString();
                string __query = "select bank_code,bank_branch from " + _g.d.erp_pass_book._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_pass_book._code) + "=\'" + __code.ToUpper() + "\' ";
                DataSet __getData = _myFrameWork._query(MyLib._myGlobal._databaseName, __query.ToString());
                if (__getData.Tables[0].Rows.Count == 0)
                {
                    this._cellUpdate(row, _g.d.cb_trans_detail._pass_book_code, "", false);
                    this._cellUpdate(row, _g.d.cb_trans_detail._bank_code, "", false);
                    this._cellUpdate(row, _g.d.cb_trans_detail._bank_branch, "", false);
                }
                else
                {
                    this._cellUpdate(row, _g.d.cb_trans_detail._bank_code, __getData.Tables[0].Rows[0][_g.d.erp_pass_book._bank_code].ToString(), false);
                    this._cellUpdate(row, _g.d.cb_trans_detail._bank_branch, __getData.Tables[0].Rows[0][_g.d.erp_pass_book._bank_branch].ToString(), false);
                }
            }
        }

        void _search_screen_bank(int row)
        {
            string __bank_code = this._cellGet(row, _g.d.cb_trans_detail._bank_code).ToString();
            string __query = "select name_1 from " + _g.d.erp_bank._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_bank._code) + "=\'" + __bank_code.ToUpper() + "\' ";
            DataSet __getData = _myFrameWork._query(MyLib._myGlobal._databaseName, __query.ToString());
            if (__getData.Tables[0].Rows.Count == 0)
            {
                this._cellUpdate(row, _g.d.cb_trans_detail._bank_code, "", false);
                this._cellUpdate(row, _g.d.cb_trans_detail._bank_branch, "", false);
            }
        }
    }

    public class _payTransferGridControl : MyLib._myGrid
    {
        public delegate void _refreshDataEvent(_payTransferGridControl sender);
        public event _refreshDataEvent _refreshData;
        //
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        ArrayList _search_data_full_buffer = new ArrayList();
        ArrayList _search_data_full_buffer_name = new ArrayList();
        int _search_data_full_buffer_addr = -1;
        MyLib._searchDataFull _search_data_full_pointer;
        string _searchName = "";
        string _search_data_full_name = "";
        //string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
        string __formatNumber = MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal.ToString());
        public DateTime _defaultDate = DateTime.Now;

        public _payTransferGridControl()
        {
            this._table_name = _g.d.cb_trans_detail._table;
            this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 1, 15, true, false, true, false, "", "", "", _g.d.cb_trans_detail._transfer_date);
            this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 25, true, false, true, true, "", "", "", _g.d.cb_trans_detail._pass_book_code);
            this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 1, 10, false, false, true, true);
            this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 1, 15, false, false, true, true);
            this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 10, true, false, true, false, __formatNumber);
            this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 40, true, false, true);
            this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
            this._width_by_persent = true;
            this.AllowDrop = true;
            this._isEdit = true;
            this._total_show = true;

            this.ColumnBackgroundAuto = false;
            this.ColumnBackgroundEnd = Color.FromArgb(198, 220, 249);
            this.RowOddBackground = Color.AliceBlue;
            this.AutoSize = true;
            this.Dock = DockStyle.Fill;

            this._clickSearchButton += new MyLib.SearchEventHandler(_pay_credit__clickSearchButton);
            this._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_pay_credit__alterCellUpdate);
            this._afterAddRow += _payTransferGridControl__afterAddRow;

            this.Invalidate();
        }

        void _payTransferGridControl__afterAddRow(object sender, int row)
        {
            this._cellUpdate(row, _g.d.cb_trans_detail._chq_due_date, _defaultDate, true);
        }

        void _pay_credit__alterCellUpdate(object sender, int row, int column)
        {
            _search_screen_book(row);
            if (this._refreshData != null)
            {
                this._refreshData(this);
            }
        }

        void _pay_credit__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            string __queryWhere = "";
            if (e._columnName.Equals(_g.d.cb_trans_detail._trans_number))
            {
                _search_data_full_name = _g.g._search_screen_สมุดเงินฝาก;
                __queryWhere = "";
                //
                Boolean __found = false;
                int __addr = 0;
                for (int __loop = 0; __loop < this._search_data_full_buffer_name.Count; __loop++)
                {
                    if (this._search_data_full_buffer_name[__loop].ToString().Equals(_search_data_full_name.ToLower()))
                    {
                        __addr = __loop;
                        __found = true;
                        break;
                    }
                }
                if (__found == false)
                {
                    __addr = this._search_data_full_buffer_name.Add(_search_data_full_name.ToLower());
                    this._search_data_full_buffer.Add((MyLib._searchDataFull)new MyLib._searchDataFull());
                }
                this._search_data_full_pointer = (MyLib._searchDataFull)this._search_data_full_buffer[__addr];
                this._search_data_full_buffer_addr = __addr;

                if (!this._search_data_full_pointer._name.Equals(_search_data_full_name.ToLower()))
                {
                    this._search_data_full_pointer.Text = e._columnName;
                    this._search_data_full_pointer._name = _search_data_full_name;
                    this._search_data_full_pointer._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_search_data_full_pointer__searchEnterKeyPress);
                    this._search_data_full_pointer._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full_pointer__searchEnterKeyPress);
                    this._search_data_full_pointer._dataList._loadViewFormat(_search_data_full_pointer._name, MyLib._myGlobal._userSearchScreenGroup, false);
                    this._search_data_full_pointer._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._search_data_full_pointer._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                }
                MyLib._myGlobal._startSearchBox(this._inputTextBox, e._columnName, this._search_data_full_pointer, false, true, __queryWhere);
            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            string __result = "";
            this._search_data_full_pointer.Close();
            if (this._search_data_full_pointer._name.Equals(_g.g._search_screen_สมุดเงินฝาก))
            {
                __result = this._search_data_full_pointer._dataList._gridData._cellGet(e._row, _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._code).ToString();
                this._cellUpdate(this._selectRow, _g.d.cb_trans_detail._trans_number, __result, true);
                SendKeys.Send("{ENTER}");
            }
        }

        void _search_data_full_pointer__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
            SendKeys.Send("{ENTER}");
        }

        private void _searchByParent(MyLib._myGrid sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, row);
        }

        private void _searchAll(string name, int row)
        {
            if (name.Equals(_g.g._search_screen_สมุดเงินฝาก))
            {
                string result = (string)this._search_data_full_pointer._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    this._search_data_full_pointer.Close();
                    this._cellUpdate(this._selectRow, _g.d.cb_trans_detail._trans_number, result, true);
                    _search_screen_book(this._selectRow);
                }
            }
        }

        void _search_screen_book(int row)
        {
            string __book_code = this._cellGet(row, _g.d.cb_trans_detail._trans_number).ToString();
            string __query = "select bank_code,bank_branch from " + _g.d.erp_pass_book._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_pass_book._code) + "=\'" + __book_code.ToUpper() + "\' ";
            DataSet __getData = _myFrameWork._query(MyLib._myGlobal._databaseName, __query.ToString());
            if (__getData.Tables[0].Rows.Count == 0)
            {
                this._cellUpdate(row, _g.d.cb_trans_detail._trans_number, "", false);
                this._cellUpdate(row, _g.d.cb_trans_detail._bank_code, "", false);
                this._cellUpdate(row, _g.d.cb_trans_detail._bank_branch, "", false);
            }
            else
            {
                this._cellUpdate(row, _g.d.cb_trans_detail._bank_code, __getData.Tables[0].Rows[0][_g.d.erp_pass_book._bank_code].ToString(), false);
                this._cellUpdate(row, _g.d.cb_trans_detail._bank_branch, __getData.Tables[0].Rows[0][_g.d.erp_pass_book._bank_branch].ToString(), false);
            }
        }
    }

    public class _payPettyCashGridControl : MyLib._myGrid
    {
        public delegate void _refreshDataEvent(_payPettyCashGridControl sender);
        public event _refreshDataEvent _refreshData;
        //
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        ArrayList _search_data_full_buffer = new ArrayList();
        ArrayList _search_data_full_buffer_name = new ArrayList();
        int _search_data_full_buffer_addr = -1;
        MyLib._searchDataFull _search_data_full_pointer;
        string _searchName = "";
        string _search_data_full_name = "";
        //string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
        string __formatNumber = MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal.ToString());

        public _payPettyCashGridControl()
        {
            this._table_name = _g.d.cb_trans_detail._table;
            this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 20, true, false, true, true, "", "", "", _g.d.cb_trans_detail._petty_cash_code);
            this._addColumn(_g.d.cb_trans_detail._description, 1, 1, 40, false, false, false, true);
            this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 10, true, false, true, false, __formatNumber);
            this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 30, true, false, true);
            this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
            this._width_by_persent = true;
            this.AllowDrop = true;
            this._isEdit = true;
            this._total_show = true;

            this.ColumnBackgroundAuto = false;
            this.ColumnBackgroundEnd = Color.FromArgb(198, 220, 249);
            this.RowOddBackground = Color.AliceBlue;
            this.AutoSize = true;
            this.Dock = DockStyle.Fill;

            this._clickSearchButton += new MyLib.SearchEventHandler(_pay_credit__clickSearchButton);
            this._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_pay_credit__alterCellUpdate);

            this.Invalidate();
        }

        void _pay_credit__alterCellUpdate(object sender, int row, int column)
        {
            _search_screen_book(row);
            if (this._refreshData != null)
            {
                this._refreshData(this);
            }
        }

        void _pay_credit__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            string __queryWhere = "";
            if (e._columnName.Equals(_g.d.cb_trans_detail._trans_number))
            {
                _search_data_full_name = _g.g._search_screen_cb_petty_cash;
                __queryWhere = "";
                //
                Boolean __found = false;
                int __addr = 0;
                for (int __loop = 0; __loop < this._search_data_full_buffer_name.Count; __loop++)
                {
                    if (this._search_data_full_buffer_name[__loop].ToString().Equals(_search_data_full_name.ToLower()))
                    {
                        __addr = __loop;
                        __found = true;
                        break;
                    }
                }
                if (__found == false)
                {
                    __addr = this._search_data_full_buffer_name.Add(_search_data_full_name.ToLower());
                    this._search_data_full_buffer.Add((MyLib._searchDataFull)new MyLib._searchDataFull());
                }
                this._search_data_full_pointer = (MyLib._searchDataFull)this._search_data_full_buffer[__addr];
                this._search_data_full_buffer_addr = __addr;

                if (!this._search_data_full_pointer._name.Equals(_search_data_full_name.ToLower()))
                {
                    this._search_data_full_pointer.Text = e._columnName;
                    this._search_data_full_pointer._name = _search_data_full_name;
                    this._search_data_full_pointer._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_search_data_full_pointer__searchEnterKeyPress);
                    this._search_data_full_pointer._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full_pointer__searchEnterKeyPress);
                    this._search_data_full_pointer._dataList._loadViewFormat(_search_data_full_pointer._name, MyLib._myGlobal._userSearchScreenGroup, false);
                    this._search_data_full_pointer._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._search_data_full_pointer._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                }
                MyLib._myGlobal._startSearchBox(this._inputTextBox, e._columnName, this._search_data_full_pointer, false, true, __queryWhere);
            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            string __result = "";
            this._search_data_full_pointer.Close();
            if (this._search_data_full_pointer._name.Equals(_g.g._search_screen_cb_petty_cash))
            {
                __result = this._search_data_full_pointer._dataList._gridData._cellGet(e._row, _g.d.cb_petty_cash._table + "." + _g.d.cb_petty_cash._code).ToString();
                this._cellUpdate(this._selectRow, _g.d.cb_trans_detail._trans_number, __result, true);
                SendKeys.Send("{ENTER}");
            }
        }

        void _search_data_full_pointer__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
            SendKeys.Send("{ENTER}");
        }

        private void _searchByParent(MyLib._myGrid sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, row);
        }

        private void _searchAll(string name, int row)
        {
            if (name.Equals(_g.g._search_screen_cb_petty_cash))
            {
                string result = (string)this._search_data_full_pointer._dataList._gridData._cellGet(row, _g.d.cb_petty_cash._table + "." + _g.d.cb_petty_cash._code);
                if (result.Length > 0)
                {
                    this._search_data_full_pointer.Close();
                    this._cellUpdate(this._selectRow, _g.d.cb_trans_detail._trans_number, result, true);
                    _search_screen_book(this._selectRow);
                }
            }
        }

        void _search_screen_book(int row)
        {
            string __book_code = this._cellGet(row, _g.d.cb_trans_detail._trans_number).ToString();
            string __query = "select " + _g.d.cb_petty_cash._name_1 + " from " + _g.d.cb_petty_cash._table + " where " + MyLib._myGlobal._addUpper(_g.d.cb_petty_cash._code) + "=\'" + __book_code.ToUpper() + "\' ";
            DataSet __getData = _myFrameWork._query(MyLib._myGlobal._databaseName, __query.ToString());
            if (__getData.Tables[0].Rows.Count == 0)
            {
                this._cellUpdate(row, _g.d.cb_trans_detail._trans_number, "", false);
                this._cellUpdate(row, _g.d.cb_trans_detail._description, "", false);
            }
            else
            {
                this._cellUpdate(row, _g.d.cb_trans_detail._description, __getData.Tables[0].Rows[0][_g.d.cb_petty_cash._name_1].ToString(), false);
            }
        }
    }
}
