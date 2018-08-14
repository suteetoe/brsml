using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;

namespace SMLERPGL
{
    public partial class _transProcessUserControl : UserControl
    {
        /*
         * การโอน
         * ล้างข้อมูลใหม่ คือ ล้างตามรหัสเอกสาร แล้วค่อยโอนเข้าไปใหม่ แบบนี้ในการณี มีขยะ จากการลบเอกสารต้นทาง ถ้าเราไม่ล้าง มันก็จะกลายเป็นมีขยะต่อไป
         * เมื่อทำการโอน ให้ลบ ของเก่าก่อน โอน where เลขที่เอกสาร และรหัสเอกสาร
        */

        string _checkFieldName = "Check";
        string _resultFieldName = "Process";
        DataTable _docFormatGL = null;
        DataTable _docFormat = null;
        DataTable _icTrans = null;
        DataTable _icTransDetail = null;
        DataTable _icInventory = null;
        DataTable _chatOfAccount = null;
        DataTable _passBook = null;
        DataTable _pettyCash = null;
        DataTable _apSupplier = null;
        DataTable _arCustomer = null;
        DataTable _otherExpense = null; // ค่าใช้จ่ายอื่น
        DataTable _otherIncome = null; // รายได้อื่น
        DataTable _bankIncomeAccount = null; // เช็ครับล่วงหน้า
        DataTable _creditCardAccount = null; // บัตรเครดิต
        DataTable _departmentTable = null;
        DataTable _projectTable = null;
        DataTable _allocateTable = null;
        DataTable _sideTable = null;
        DataTable _jobsTable = null;


        Thread _threadWorking = null;
        Boolean _success = false;
        string _queryDocFormat = "";
        int _lineNumber;
        int _lineNumberDepartment;
        decimal _sumDebit = 0.0M;
        decimal _sumCredit = 0.0M;
        string _transRemark = "";
        bool _autoSaveData = true;
        string _docFormatTempProcess = "";

        public _transProcessUserControl()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this.toolStrip2.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            this._conditionScreen._table_name = _g.d.gl_resource._table;
            this._conditionScreen._maxColumn = 2;
            this._conditionScreen._addDateBox(0, 0, 1, 0, _g.d.gl_resource._date_begin, 1, true);
            this._conditionScreen._addDateBox(0, 1, 1, 0, _g.d.gl_resource._date_end, 1, true);
            this._conditionScreen._addTextBox(1, 0, 1, 0, _g.d.gl_resource._doc_begin, 1, 100, 0, true, false);
            this._conditionScreen._addTextBox(1, 1, 1, 0, _g.d.gl_resource._doc_end, 1, 100, 0, true, false);
            try
            {
                int __period = _g.g._accountPeriodFind(MyLib._myGlobal._workingDate) - 1;
                this._conditionScreen._setDataDate(_g.d.gl_resource._date_begin, _g.g._accountPeriodDateBegin[__period]);
                this._conditionScreen._setDataDate(_g.d.gl_resource._date_end, _g.g._accountPeriodDateEnd[__period]);
            }
            catch
            {
            }
            //
            this._transTypeGrid._table_name = _g.d.erp_doc_format._table;
            this._transTypeGrid._isEdit = false;
            this._transTypeGrid._addColumn(this._checkFieldName, 11, 10, 10);
            this._transTypeGrid._addColumn(_g.d.erp_doc_format._code, 1, 100, 20);
            this._transTypeGrid._addColumn(_g.d.erp_doc_format._name_1, 1, 100, 40);
            this._transTypeGrid._addColumn(_g.d.erp_doc_format._screen_code, 1, 100, 30);
            //
            this._resultGrid._addColumn(_resultFieldName, 1, 100, 100);
            //
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            this._queryDocFormat = "select " + MyLib._myGlobal._fieldAndComma(_g.d.erp_doc_format._code, _g.d.erp_doc_format._name_1, _g.d.erp_doc_format._screen_code, _g.d.erp_doc_format._gl_book, _g.d.erp_doc_format._gl_description) + " from " + _g.d.erp_doc_format._table + " where coalesce((select count(*) from " + _g.d.erp_doc_format_gl._table + " where " + _g.d.erp_doc_format_gl._table + "." + _g.d.erp_doc_format_gl._doc_code + "=" + _g.d.erp_doc_format._table + "." + _g.d.erp_doc_format._code + "),0) > 0 or " + _g.d.erp_doc_format._gl_book + "<>\'\' order by " + _g.d.erp_doc_format._code;
            DataTable __dt = __myFrameWork._queryShort(this._queryDocFormat).Tables[0];
            for (int __row = 0; __row < __dt.Rows.Count; __row++)
            {
                int __addr = this._transTypeGrid._addRow();
                this._transTypeGrid._cellUpdate(__addr, this._checkFieldName, 0, false);
                this._transTypeGrid._cellUpdate(__addr, _g.d.erp_doc_format._code, __dt.Rows[__row][_g.d.erp_doc_format._code].ToString(), false);
                this._transTypeGrid._cellUpdate(__addr, _g.d.erp_doc_format._name_1, __dt.Rows[__row][_g.d.erp_doc_format._name_1].ToString(), false);
                this._transTypeGrid._cellUpdate(__addr, _g.d.erp_doc_format._screen_code, __dt.Rows[__row][_g.d.erp_doc_format._screen_code].ToString(), false);
            }
            this._transTypeGrid.Invalidate();
            //
            this._stopButton.Enabled = false;
            this._timer.Stop();
            this._timer.Enabled = false;
            string __computerName = SystemInformation.ComputerName.ToLower();
            if (__computerName.IndexOf("jead8") != -1)
            {
                this._conditionScreen._setDataDate(_g.d.gl_resource._date_begin, new DateTime(2008, 1, 1));
                this._conditionScreen._setDataDate(_g.d.gl_resource._date_end, new DateTime(2013, 12, 31));
                //this._conditionScreen._setDataStr(_g.d.gl_resource._doc_begin, "TM560526001");
                //this._conditionScreen._setDataStr(_g.d.gl_resource._doc_end, "TM560526001");
            }
        }

        private void _updateCheck(Boolean status)
        {
            for (int __row = 0; __row < this._transTypeGrid._rowData.Count; __row++)
            {
                this._transTypeGrid._cellUpdate(__row, this._checkFieldName, (status) ? 1 : 0, false);
            }
            this._transTypeGrid.Invalidate();
        }

        private void _selectAllButton_Click(object sender, EventArgs e)
        {
            this._updateCheck(true);
        }

        private void _removeSelectAllButton_Click(object sender, EventArgs e)
        {
            this._updateCheck(false);
        }

        private enum _accountDetailType
        {
            สินค้า_ต้นทุน,
            สินค้า_ราคา,
            ธนาคาร,
            ธนาคาร_เช็ครับล่วงหน้า,
            ธนาคาร_เช็คจ่ายล่วงหน้า,
            ธนาคาร_เช็ครับคืน,
            ธนาคาร_บัตรเครดิต,
            ธนาคาร_โอนเงินระหว่างธนาคาร_ออก,
            ธนาคาร_โอนเงินระหว่างธนาคาร_เข้า,
            เช็คจ่าย,
            เช็ครับ,
            เงินสดย่อย,
            รายวันเงินสดย่อย,
            รายได้อื่น,
            ค่าใช้จ่ายอื่น,
            รับจ่าย_รายได้อื่น,
            รับจ่าย_ค่าใช้จ่ายอื่น,
            เงินมัดจำรับ,
            เงินล่่วงหน้ารับ
        }

        /// <summary>
        /// ประเภทการเฉลี่ย
        /// </summary>
        public enum _accountDistType
        {
            None,
            แผนก,
            โครงการ,
            การจัดสรร,
            หน่วยงาน,
            งาน
        }

        public int _findDocFormatRow(string docFormatCode)
        {
            return this._transTypeGrid._findData(this._transTypeGrid._findColumnByName(_g.d.erp_doc_format._code), docFormatCode);
        }

        public void _processGLByTrans(DateTime docDate, string docNo, string docFormatCode)
        {
            // check docformat

            // start search
            int __rowIndex = _findDocFormatRow(docFormatCode);

            if (__rowIndex != -1)
            {
                this._transTypeGrid._cellUpdate(__rowIndex, this._checkFieldName, 1, true);
                this._transTypeGrid.Invalidate();

                this._conditionScreen._setDataDate(_g.d.gl_resource._date_begin, docDate);
                this._conditionScreen._setDataDate(_g.d.gl_resource._date_end, docDate);

                this._conditionScreen._setDataStr(_g.d.gl_resource._doc_begin, docNo);
                this._conditionScreen._setDataStr(_g.d.gl_resource._doc_end, docNo);

                this._autoSaveData = false;
                this._process();
            }
        }

        public void _procesGLByTemp(string docFormatCode)
        {
            this._autoSaveData = false;
            this._docFormatTempProcess = docFormatCode;

            this._queryDocFormat = "select " + MyLib._myGlobal._fieldAndComma(_g.d.erp_doc_format._code, _g.d.erp_doc_format._name_1, _g.d.erp_doc_format._screen_code, _g.d.erp_doc_format._gl_book, _g.d.erp_doc_format._gl_description) + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._code + "=\'" + this._docFormatTempProcess + "\' and coalesce((select count(*) from " + _g.d.erp_doc_format_gl._table + " where " + _g.d.erp_doc_format_gl._table + "." + _g.d.erp_doc_format_gl._doc_code + "=" + _g.d.erp_doc_format._table + "." + _g.d.erp_doc_format._code + "),0) > 0 or " + _g.d.erp_doc_format._gl_book + "<>\'\' order by " + _g.d.erp_doc_format._code;

            this._loadBeforeForProcess();
            processWithData(this._autoSaveData);
        }

        public void _addTransData(_transDataObject trans, bool isCreateNew)
        {
            if (isCreateNew == true)
            {
                DataTable __result = new DataTable("Result");
                __result.Columns.Add("trans", typeof(String));
                __result.Columns.Add(_g.d.ic_trans._vat_type, typeof(int));
                __result.Columns.Add(_g.d.ic_trans._tax_doc_no, typeof(String));
                __result.Columns.Add(_g.d.ic_trans._cust_name, typeof(String));
                __result.Columns.Add(_g.d.ic_trans._cust_code, typeof(String));
                __result.Columns.Add(_g.d.ic_trans._remark, typeof(String));
                __result.Columns.Add(_g.d.ic_trans._total_amount, typeof(Decimal));
                __result.Columns.Add("service_amount", typeof(Decimal));
                __result.Columns.Add(_g.d.ic_trans._total_value, typeof(Decimal));
                __result.Columns.Add(_g.d.ic_trans._total_before_vat, typeof(Decimal));
                __result.Columns.Add(_g.d.ic_trans._total_except_vat, typeof(Decimal));
                __result.Columns.Add(_g.d.ic_trans._total_vat_value, typeof(Decimal));
                __result.Columns.Add(_g.d.ic_trans._total_discount, typeof(Decimal));
                __result.Columns.Add(_g.d.cb_trans._cash_amount, typeof(Decimal));
                __result.Columns.Add("tax_at_pay", typeof(Decimal));
                __result.Columns.Add(_g.d.cb_trans._point_amount, typeof(Decimal));
                __result.Columns.Add(_g.d.cb_trans._coupon_amount, typeof(Decimal));
                __result.Columns.Add(_g.d.cb_trans._card_amount, typeof(Decimal));
                __result.Columns.Add(_g.d.cb_trans._chq_amount, typeof(Decimal));
                __result.Columns.Add(_g.d.cb_trans._tranfer_amount, typeof(Decimal));
                __result.Columns.Add(_g.d.cb_trans._total_income_amount, typeof(Decimal));
                __result.Columns.Add(_g.d.cb_trans._petty_cash_amount, typeof(Decimal));
                __result.Columns.Add(_g.d.cb_trans._discount_amount, typeof(Decimal));
                __result.Columns.Add(_g.d.ic_trans._doc_no, typeof(String));
                __result.Columns.Add(_g.d.ic_trans._doc_ref, typeof(String));
                __result.Columns.Add(_g.d.ic_trans._doc_date, typeof(String));
                __result.Columns.Add(_g.d.ic_trans._doc_time, typeof(String));
                __result.Columns.Add(_g.d.ic_trans._doc_format_code, typeof(String));
                __result.Columns.Add(_g.d.ic_trans._trans_type, typeof(int));
                __result.Columns.Add(_g.d.ic_trans._inquiry_type, typeof(int));
                __result.Columns.Add(_g.d.ic_trans._trans_flag, typeof(int));
                __result.Columns.Add(_g.d.ic_trans._advance_amount, typeof(Decimal));
                __result.Columns.Add(_g.d.cb_trans._deposit_amount, typeof(Decimal));
                __result.Columns.Add("credit_card_amount", typeof(Decimal));
                __result.Columns.Add("credit_card_charge", typeof(Decimal));
                __result.Columns.Add(_g.d.ic_trans._tax_doc_date, typeof(String));
                __result.Columns.Add(_g.d.ic_trans._branch_code, typeof(String));
                __result.Columns.Add(_g.d.ic_trans._department_code, typeof(String));
                __result.Columns.Add(_g.d.ic_trans._project_code, typeof(String));
                __result.Columns.Add(_g.d.ic_trans._allocate_code, typeof(String));
                __result.Columns.Add(_g.d.ic_trans._side_code, typeof(String));
                __result.Columns.Add(_g.d.ic_trans._job_code, typeof(String));
                __result.Columns.Add("vat_next", typeof(Decimal));
                __result.Columns.Add("total_cost", typeof(Decimal));
                __result.Columns.Add("total_income_other", typeof(Decimal));
                __result.Columns.Add("total_expense_other", typeof(Decimal));

                __result.Columns.Add("vat_no_return", typeof(Decimal));

                this._icTrans = __result;
            }

            this._icTrans.Rows.Add(
                trans.trans,
                trans.vat_type,
                trans.tax_doc_no,
                trans.cust_name,
                trans.cust_code,
                trans.remark,
                trans.total_amount,
                trans.service_amount,
                trans.total_value,
                trans.total_before_vat,
                trans.total_except_vat,
                trans.total_vat_value,
                trans.total_discount,
                trans.cash_amount,
                trans.tax_at_pay,
                trans.point_amount,
                trans.coupon_amount,
                trans.card_amount,
                trans.chq_amount,
                trans.tranfer_amount,
                trans.total_income_amount,
                trans.petty_cash_amount,
                trans.discount_amount,
                trans.doc_no,
                trans.doc_ref,
                trans.doc_date,
                trans.doc_time,
                trans.doc_format_code,
                trans.trans_type,
                trans.inquiry_type,
                trans.trans_flag,
                trans.advance_amount,
                trans.deposit_amount,
                trans.credit_card_amount,
                trans.credit_card_charge,
                trans.tax_doc_date,
                trans.branch_code,
                trans.department_code,
                trans.project_code,
                trans.allocate_code,
                trans.side_code,
                trans.job_code,
                trans.vat_next,
                trans.total_cost,
                trans.total_income_other,
                trans.total_expense_other,
                trans.vatBuyNoReturn
                );
        }

        public void _addTransDetailData(_transDetailDataObject transDetail, bool isCreateNew)
        {
            if (isCreateNew == true)
            {
                DataTable __result = new DataTable("Result");
                __result.Columns.Add(_g.d.ic_trans_detail._item_type, typeof(int));
                __result.Columns.Add(_g.d.cb_trans_detail._charge, typeof(Decimal));
                __result.Columns.Add(_g.d.cb_trans_detail._trans_number, typeof(String));
                __result.Columns.Add(_g.d.ic_trans_detail._item_code, typeof(String));
                __result.Columns.Add(_g.d.cb_trans_detail._pass_book_code, typeof(String));
                __result.Columns.Add(_g.d.ic_trans_detail._doc_no, typeof(String));
                __result.Columns.Add(_g.d.ic_trans_detail._price, typeof(Decimal));
                __result.Columns.Add(_g.d.cb_trans_detail._amount, typeof(Decimal));
                __result.Columns.Add(_g.d.ic_trans_detail._sum_of_cost, typeof(Decimal));
                __result.Columns.Add(_g.d.ic_trans_detail._sum_amount, typeof(Decimal));
                __result.Columns.Add(_g.d.ic_trans_detail._sum_amount_exclude_vat, typeof(Decimal));
                __result.Columns.Add(_g.d.ic_trans_detail._trans_type, typeof(int));
                __result.Columns.Add(_g.d.ic_trans_detail._trans_flag, typeof(int));
                __result.Columns.Add("doc_type", typeof(int));
                __result.Columns.Add("chq_bank_ref", typeof(String));
                __result.Columns.Add("credit_card_ref", typeof(String));
                __result.Columns.Add(_g.d.ic_trans_detail._remark, typeof(String));
                __result.Columns.Add("transfer_amount", typeof(Decimal));
                __result.Columns.Add("fee_amount", typeof(Decimal));
                __result.Columns.Add("last_flag", typeof(int));
                __result.Columns.Add(_g.d.ic_trans_detail._line_number, typeof(int));
                __result.Columns.Add(_g.d.ic_trans_detail._branch_code, typeof(String));

                this._icTransDetail = __result;
            }

            if (transDetail != null)
            {
                this._icTransDetail.Rows.Add(
                    transDetail.item_type,
                    transDetail.charge,
                    transDetail.trans_number,
                    transDetail.item_code,
                    transDetail.pass_book_code,
                    transDetail.doc_no,
                    transDetail.price,
                    transDetail.amount,
                    transDetail.sum_of_cost,
                    transDetail.sum_amount,
                    transDetail.sum_amount_exclude_vat,
                    transDetail.trans_type,
                    transDetail.trans_flag,
                    transDetail.doc_type,
                    transDetail.chq_bank_ref,
                    transDetail.credit_card_ref,
                    transDetail.remark,
                    transDetail.transfer_amount,
                    transDetail.fee_amount,
                    transDetail.last_flag,
                    transDetail.line_number,
                    transDetail.branch_code
                    );
            }

        }


        private String[] _processQuery(string dateStart, string dateStop)
        {
            return _processQuery(dateStart, dateStop, "", "");
        }

        private String[] _processQuery(string dateStart, string dateStop, string doc_format_code, String doc_no)
        {
            List<string> __return = new List<string>();

            //String[] __return = { "", "", "", "", "" };
            string __docNoBegin = this._conditionScreen._getDataStr(_g.d.gl_resource._doc_begin);
            string __docNoEnd = this._conditionScreen._getDataStr(_g.d.gl_resource._doc_end);
            string __docNoWare = "";

            StringBuilder __transFlagStr = new StringBuilder();
            StringBuilder __transDetailFlagStr = new StringBuilder();
            StringBuilder __transFlagSelected = new StringBuilder();

            if (doc_format_code.Length > 0)
            {
                int __addr = this._transTypeGrid._findData(this._transTypeGrid._findColumnByName(_g.d.erp_doc_format._code), doc_format_code);
                if (__addr != -1)
                {
                    string __docFomatCode = this._transTypeGrid._cellGet(__addr, _g.d.erp_doc_format._code).ToString().ToUpper();
                    string __screenCode = this._transTypeGrid._cellGet(__addr, _g.d.erp_doc_format._screen_code).ToString().ToUpper();
                    _g.g._transControlTypeEnum __transFlag = _g.g._transFlagGlobal._transFlagByScreenCode(__screenCode);
                    if (__transFlag != _g.g._transControlTypeEnum.ว่าง)
                    {
                        if (__transFlagStr.Length > 0)
                        {
                            __transFlagStr.Append(",");
                        }
                        __transFlagStr.Append(_g.g._transFlagGlobal._transFlag(__transFlag).ToString());
                        if (__transDetailFlagStr.Length > 0)
                        {
                            __transDetailFlagStr.Append(",");
                        }
                        __transDetailFlagStr.Append(_g.g._transFlagGlobal._transFlag(__transFlag).ToString());
                        if (__transFlag == _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร)
                        {
                            __transDetailFlagStr.Append(",").Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินเข้าธนาคาร).ToString());
                        }
                        if (__transFlag == _g.g._transControlTypeEnum.สินค้า_โอนออก)
                        {
                            __transDetailFlagStr.Append(",").Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString());
                        }
                        if (__transFlagSelected.Length > 0)
                        {
                            __transFlagSelected.Append(",");
                        }
                        __transFlagSelected.Append("\'" + __docFomatCode + "\'");
                    }
                }
            }
            else
            {
                for (int __row = 0; __row < this._transTypeGrid._rowData.Count; __row++)
                {
                    int __check = (int)MyLib._myGlobal._decimalPhase(this._transTypeGrid._cellGet(__row, this._checkFieldName).ToString());
                    if (__check == 1)
                    {
                        string __docFomatCode = this._transTypeGrid._cellGet(__row, _g.d.erp_doc_format._code).ToString().ToUpper();
                        string __screenCode = this._transTypeGrid._cellGet(__row, _g.d.erp_doc_format._screen_code).ToString().ToUpper();
                        _g.g._transControlTypeEnum __transFlag = _g.g._transFlagGlobal._transFlagByScreenCode(__screenCode);
                        if (__transFlag != _g.g._transControlTypeEnum.ว่าง)
                        {
                            if (__transFlagStr.Length > 0)
                            {
                                __transFlagStr.Append(",");
                            }
                            __transFlagStr.Append(_g.g._transFlagGlobal._transFlag(__transFlag).ToString());


                            if (__transDetailFlagStr.Length > 0)
                            {
                                __transDetailFlagStr.Append(",");
                            }
                            __transDetailFlagStr.Append(_g.g._transFlagGlobal._transFlag(__transFlag).ToString());
                            if (__transFlag == _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร)
                            {
                                __transDetailFlagStr.Append(",").Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินเข้าธนาคาร).ToString());
                            }
                            if (__transFlag == _g.g._transControlTypeEnum.สินค้า_โอนออก)
                            {
                                __transFlagStr.Append(",").Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString());
                                __transDetailFlagStr.Append(",").Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า).ToString());
                            }
                            if (__transFlagSelected.Length > 0)
                            {
                                __transFlagSelected.Append(",");
                            }
                            __transFlagSelected.Append("\'" + __docFomatCode + "\'");
                        }
                    }
                }
            }

            // หัวเอกสารของ ic_trans,cb_transcb_trans
            // __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_date + " between " + __dateBegin + " and " + __dateEnd + " and " + _g.d.ic_trans._trans_flag + " in (" + __transFlagStr.ToString() + ") order by " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_time));

            if (doc_format_code.Length > 0)
            {
                __docNoWare = " and doc_no=\'" + doc_no + "\' ";
            }
            else
            {
                if (__docNoBegin.Length != 0 || __docNoEnd.Length != 0)
                {
                    if (__docNoBegin.Length != 0 && __docNoEnd.Length == 0)
                    {
                        // ป้อนเฉพาะเอกสารเริ่มต้น
                        __docNoWare = " and doc_no=\'" + __docNoBegin + "\'";
                    }
                    else
                        if (__docNoBegin.Length == 0 && __docNoEnd.Length != 0)
                    {
                        // ป้อนเฉพาะเอกสารสุดท้าย
                        __docNoWare = " and doc_no=\'" + __docNoEnd + "\'";
                    }
                    else
                            if (__docNoBegin.Length != 0 && __docNoEnd.Length != 0)
                    {
                        // ป้อนเอกสารเริ่มต้นและสิ้นสุด
                        __docNoWare = " and doc_no between \'" + __docNoBegin + "\' and \'" + __docNoEnd + "\'";
                    }
                }
            }

            // Table 0=รายการรายวัน
            // Flag 239=ลูกหนี้_รับชำระหนี้
            // Flag 19=เจ้าหนี้_จ่ายชำระหนี้
            string __cb_total_vat_amount = " case when  trans_flag = 19 then (select sum(vat_amount) from gl_journal_vat_buy where gl_journal_vat_buy.doc_no = cb_trans.doc_no and gl_journal_vat_buy.trans_flag = cb_trans.trans_flag and gl_journal_vat_buy.vat_type = 0) else 0 end ";
            string __cb_vat_next_amount = " case when  trans_flag = 19 then (select sum(vat_amount) from gl_journal_vat_buy as vatbuy where vatbuy.doc_no = cb_trans.doc_no and vatbuy.trans_flag = cb_trans.trans_flag and coalesce((select vat_type from gl_journal_vat_buy where gl_journal_vat_buy.vat_doc_no = vatbuy.ref_vat_no and vatbuy.ref_doc_no = gl_journal_vat_buy.doc_no ), 0)=2) else 0 end  ";

            string __cb_vatbuy_noreturn_amount = " case when  trans_flag = 19 then (select sum(vat_amount) from gl_journal_vat_buy as vatbuy where vatbuy.doc_no = cb_trans.doc_no and vatbuy.trans_flag = cb_trans.trans_flag and vatbuy.vat_type=1) else 0 end  ";

            string __cbTransFlag = "239,19";
            //
            StringBuilder __subQuery1 = new StringBuilder();
            __subQuery1.Append("select 'cb' as trans,0 as vat_type,'' as tax_doc_no" +
                ",(select  case when  trans_type in ( 1,4) or trans_flag in (260) then (select name_1 from ap_supplier where code = ap_ar_code ) else (case when trans_type in (2,5) then (select name_1 from ar_customer where code = ap_ar_code ) end) end ) as cust_name" +
                ",ap_ar_code as cust_code" +
                ",(select remark from ap_ar_trans where ap_ar_trans.doc_no = cb_trans.doc_no and ap_ar_trans.trans_flag = cb_trans.trans_flag) as remark,total_amount, 0 as service_amount,0  as total_value,0 as total_before_vat,0 as total_except_vat, " + __cb_total_vat_amount + " as total_vat_value" +
                ",0 as total_discount, cash_amount,total_tax_at_pay as tax_at_pay,0 as point_amount,0 as coupon_amount,card_amount,chq_amount,tranfer_amount,total_income_amount" +
                ",petty_cash_amount, discount_amount, doc_no,doc_ref,doc_date,doc_time,doc_format_code ,trans_type,0 as inquiry_type,trans_flag,advance_amount,deposit_amount,card_amount as credit_card_amount" +
                ",total_credit_charge as credit_card_charge, null::date as tax_doc_date,(select branch_code from ap_ar_trans where ap_ar_trans.doc_no = cb_trans.doc_no and ap_ar_trans.trans_flag = cb_trans.trans_flag) as " + _g.d.cb_trans._branch_code + ",'' as " + _g.d.ic_trans._department_code + ", '' as " + _g.d.ic_trans._project_code + ", '' as " + _g.d.ic_trans._allocate_code +
                "," + __cb_vat_next_amount + " as vat_next, '' as " + _g.d.ic_trans._side_code + " , '' as " + _g.d.ic_trans._job_code +
                ", 0 as " + _g.d.ic_trans._total_cost +
                "," + _g.d.cb_trans._total_income_other + "," + _g.d.cb_trans._total_expense_other +
                "," + __cb_vatbuy_noreturn_amount + " as vat_no_return" +
                " from " + _g.d.cb_trans._table +
                "  where " + _g.d.cb_trans._doc_date + " between " + dateStart + " and " + dateStop + " " +
                __docNoWare + " and " + _g.d.cb_trans._trans_flag + " in (" + __transFlagStr.ToString() + ") " +
                " and " + _g.d.cb_trans._trans_flag + " in (" + __cbTransFlag + ") and " + _g.d.cb_trans._doc_format_code + " in (" + __transFlagSelected.ToString() + ") " +
                " and " + _g.d.cb_trans._doc_no + " not in (select " + _g.d.gl_journal._doc_no + " from " + _g.d.gl_journal._table + " where " + _g.d.gl_journal._table + "." + _g.d.gl_journal._trans_direct + "=1)" +
                " and (select coalesce(is_cancel, 0) from ap_ar_trans where ap_ar_trans.doc_no = cb_trans.doc_no and ap_ar_trans.trans_flag=cb_trans.trans_flag)=0 ");

            __subQuery1.Append(" union all ");

            ///23, 240 ยกเลิกจ่ายชำระ ยกเลิกรับชำระ
            StringBuilder __cancelSubqyery = new StringBuilder();
            __cancelSubqyery.Append("select 'ic' as trans,0 as vat_type,'' as tax_doc_no,'' as cust_name, cust_code,'' as remark, 0 as total_amount, 0 as service_amount,0  as total_value,0 as total_before_vat,0 as total_except_vat, 0  as total_vat_value,0 as total_discount ");
            __cancelSubqyery.Append(" , 0 as cash_amount,0 as tax_at_pay,0 as point_amount,0 as coupon_amount, 0 as card_amount, 0 as chq_amount, 0 as tranfer_amount, 0 as total_income_amount, 0 as petty_cash_amount ");
            __cancelSubqyery.Append(" , 0 as discount_amount, doc_no,doc_ref,doc_date,doc_time,doc_format_code ,trans_type,0 as inquiry_type,trans_flag ");
            __cancelSubqyery.Append(" , 0 as advance_amount, 0 as deposit_amount,  0 as  credit_card_amount,0 as credit_card_charge, null::date as tax_doc_date ");
            __cancelSubqyery.Append(" , branch_code,'' as department_code, '' as project_code, '' as allocate_code ");
            __cancelSubqyery.Append(" , 0 as vat_next, '' as side_code , '' as job_code, 0 as total_cost ");
            __cancelSubqyery.Append(" , 0 as total_income_other, 0 as total_expense_other,  0 as  vat_no_return  ");
            //__cancelSubqyery.Append("");

            __cancelSubqyery.Append(" from " + _g.d.ap_ar_trans._table);

            __cancelSubqyery.Append(" where ");
            __cancelSubqyery.Append(_g.d.ap_ar_trans._doc_date + " between " + dateStart + " and " + dateStop + " " + __docNoWare);
            __cancelSubqyery.Append(" and " + _g.d.ap_ar_trans._trans_flag + " in (" + __transFlagStr.ToString() + ") " + " and " + _g.d.ap_ar_trans._trans_flag + " in (23,240) ");
            __cancelSubqyery.Append(" and " + _g.d.ap_ar_trans._doc_format_code + " in (" + __transFlagSelected.ToString() + ") ");
            __cancelSubqyery.Append(" and " + _g.d.ap_ar_trans._doc_no + " not in (select " + _g.d.gl_journal._doc_no + " from " + _g.d.gl_journal._table + " where " + _g.d.gl_journal._table + "." + _g.d.gl_journal._trans_direct + "=1)");
            __cancelSubqyery.Append(" and  coalesce(is_cancel, 0) = 0 ");

            __cancelSubqyery.Append(" union all ");

            string __payFormat = "coalesce((select {0} from cb_trans where cb_trans.trans_flag=ic_trans.trans_flag and cb_trans.doc_no=ic_trans.doc_no),0)";
            string __creditCardFormat = "coalesce((select coalesce(sum({0}),0) from cb_trans_detail where cb_trans_detail.trans_flag=ic_trans.trans_flag and cb_trans_detail.doc_no=ic_trans.doc_no and cb_trans_detail.doc_type=3),0)";
            string __cashAmountQuery = String.Format(__payFormat, "cash_amount");
            string __taxAtPayQuery = String.Format(__payFormat, "total_tax_at_pay");
            string __totalIncomeAmountQuery = String.Format(__payFormat, "total_income_amount");
            string __pettyCashAmountQuery = String.Format(__payFormat, "petty_cash_amount");
            string __depositAmountQuery = String.Format(__payFormat, "deposit_amount");
            string __chqAmountQuery = String.Format(__payFormat, "chq_amount");
            string __transferAmountQuery = String.Format(__payFormat, "tranfer_amount");
            string __discountPaymentQuery = string.Format(__payFormat, _g.d.cb_trans._discount_amount);

            string __sumOfCostByDoc = "(select sum(sum_of_cost) from ic_trans_detail where ic_trans_detail.doc_no = ic_trans.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag) as " + _g.d.ic_trans._total_cost;
            //
            string __cardAmountQuery = String.Format(__creditCardFormat, "amount");
            string __cardChargeAmountQuery = String.Format(__creditCardFormat, "charge");

            string __totalIncomeOther = string.Format(__payFormat, _g.d.cb_trans._total_income_other);
            string __totalExpenseOhter = string.Format(__payFormat, _g.d.cb_trans._total_expense_other);

            string __vatBuyNextQuery = " (select sum(coalesce(vat_amount, 0)) from gl_journal_vat_buy where gl_journal_vat_buy.doc_no = ic_trans.doc_no and gl_journal_vat_buy.trans_flag = ic_trans.trans_flag and vat_type = 2 ) as vat_next ";

            string __vatBuyNoReturnQuery = " (select sum(coalesce(vat_amount, 0)) from gl_journal_vat_buy where gl_journal_vat_buy.doc_no = ic_trans.doc_no and gl_journal_vat_buy.trans_flag = ic_trans.trans_flag and vat_type = 1 ) as vat_no_return ";

            // ไม่เอารายการที่ป้อนตรงๆ trans_direct=1
            StringBuilder __subQuery2 = new StringBuilder();
            __subQuery2.Append(" select 'ic' as trans,vat_type,tax_doc_no" +
                ",(select case when  trans_type in ( 1,4) or trans_flag in (260) then (select name_1 from ap_supplier where code = cust_code ) else (case when trans_type in (2,5) then (select name_1 from ar_customer where code = cust_code ) end) end) as cust_name" +
                ",cust_code as cust_code,remark as remark,total_amount, coalesce((select sum(sum_amount_exclude_vat) from ic_trans_detail where ic_trans_detail.doc_no = ic_trans.doc_no and ic_trans_detail.trans_flag = ic_trans.trans_flag and item_type = 1 ), 0) as service_amount,");
            __subQuery2.Append("case when vat_type=0 then total_value when vat_type=1 then (total_value*100/(100+vat_rate)) else total_value end as total_value,total_before_vat,total_except_vat,total_vat_value,");
            // Discount (กรณีรวมใน vat_type=1 ไม่ต้องถอด vat)
            __subQuery2.Append("case when vat_type=1 then (total_discount*100/(100+vat_rate)) when vat_type=0 then total_discount else total_discount end as total_discount," + __cashAmountQuery + " as cash_amount,");
            //__subQuery2.Append("case when vat_type=1 then (total_discount*100/(100+vat_rate)) when vat_type=0 then total_discount else total_discount end as total_discount," + __cashAmountQuery + " as cash_amount,");

            __subQuery2.Append(__taxAtPayQuery + " as tax_at_pay" +
                ",coalesce((select point_amount from cb_trans where cb_trans.trans_flag=ic_trans.trans_flag and cb_trans.doc_no=ic_trans.doc_no),0) as point_amount" +
                ",coalesce((select coupon_amount from cb_trans where cb_trans.trans_flag=ic_trans.trans_flag and cb_trans.doc_no=ic_trans.doc_no),0) as coupon_amount" +
                ",0 as card_amount, " + __chqAmountQuery + " as chq_amount ," + __transferAmountQuery + " as tranfer_amount ," + __totalIncomeAmountQuery + " as total_income_amount" +
                "," + __pettyCashAmountQuery + " as petty_cash_amount," + __discountPaymentQuery + " as discount_amount" +
                ",doc_no,doc_ref,doc_date,doc_time,doc_format_code,trans_type,inquiry_type as inquiry_type,trans_flag" +
                ",case when vat_type=0 then advance_amount when vat_type=1 then (advance_amount*100/(100+vat_rate)) else advance_amount end as advance_amount" +
                "," + __depositAmountQuery + " as deposit_amount," + __cardAmountQuery + " as credit_card_amount" +
                "," + __cardChargeAmountQuery + " as credit_card_charge, " + _g.d.ic_trans._tax_doc_date + ", " + _g.d.ic_trans._branch_code + "," + _g.d.ic_trans._department_code + "," + _g.d.ic_trans._project_code + "," +
                _g.d.ic_trans._allocate_code + "," + __vatBuyNextQuery + ", " + _g.d.ic_trans._side_code + ", " + _g.d.ic_trans._job_code +
                "," + __sumOfCostByDoc +
                "," + __totalIncomeOther + " as " + _g.d.cb_trans._total_income_other +
                "," + __totalExpenseOhter + " as " + _g.d.cb_trans._total_expense_other +
                "," + __vatBuyNoReturnQuery +
                " from " + _g.d.ic_trans._table +
                " where " + _g.d.ic_trans._doc_date + " between " + dateStart + " and " + dateStop + " " +
                __docNoWare + " and " + _g.d.ic_trans._trans_flag + " in (" + __transFlagStr.ToString() + ") " +
                " and " + _g.d.ic_trans._doc_format_code + " in (" + __transFlagSelected.ToString() + ") " +
                " and " + _g.d.ic_trans._doc_no + " not in (select " + _g.d.gl_journal._doc_no + " from " + _g.d.gl_journal._table + " where " + _g.d.gl_journal._table + "." + _g.d.gl_journal._trans_direct + "=1) " +
                " and coalesce(" + _g.d.ic_trans._is_cancel + ", 0)=0 " +
                " order by " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_time);

            // รายการยกเลิก จ่ายชำระ รับชำระ


            __return.Add(__subQuery1.ToString() + __cancelSubqyery.ToString() + __subQuery2.ToString());

            // and cb_chq_list.doc_ref = ic_trans_detail.doc_ref and coalesce(cb_chq_list.doc_line_number, 0) = coalesce(ic_trans_detail.ref_row, 0) โต๋ เพิ่มเข้าไป เพื่อ filter เช็ค และ บัตรเครดิต

            //string __chqlaststatusflag = "";
            //if (__transDetailFlagStr.Equals(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค).ToString()) || 
            //    __transDetailFlagStr.Equals(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก).ToString()))
            //{
            //    // เช็ครับ
            //    __chqlaststatusflag = "405,410,41,412";
            //}
            //else
            //{
            //    // เช็คจ่าย
            //    __chqlaststatusflag = "";
            //}

            StringBuilder __subQuery3 = new StringBuilder();
            __subQuery3.Append(" select coalesce((select item_type from ic_inventory where ic_inventory.code=item_code),0) as item_type,0 as charge ,'' as trans_number ,coalesce(item_code,'') as item_code ,coalesce(item_code,'') as pass_book_code, coalesce(doc_no,'') as doc_no ,price, sum_amount as amount, sum_of_cost, sum_amount, sum_amount_exclude_vat, trans_type, trans_flag, 0 as doc_type,coalesce((select bank_code from cb_chq_list where cb_chq_list.chq_number=ic_trans_detail.chq_number and cb_chq_list.doc_ref = ic_trans_detail.doc_ref and coalesce(cb_chq_list.doc_line_number, 0) = coalesce(ic_trans_detail.ref_row, 0) ),'') as chq_bank_ref,coalesce((select credit_card_type from cb_chq_list where cb_chq_list.chq_number=ic_trans_detail.chq_number and cb_chq_list.doc_ref = ic_trans_detail.doc_ref and coalesce(cb_chq_list.doc_line_number, 0) = coalesce(ic_trans_detail.ref_row, 0) ),'') as credit_card_ref,remark,transfer_amount,fee_amount, case when trans_flag in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค) + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก) + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค) + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก) + ") then coalesce((select " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + " trans_flag from ic_trans_detail as x where x.chq_number = ic_trans_detail.item_code and x.doc_no <> ic_trans_detail.doc_no and to_timestamp(x.doc_date  || ' ' || x.doc_time, 'yyyy-MM-DD HH24:MI') < to_timestamp(ic_trans_detail.doc_date || ' ' ||  ic_trans_detail.doc_time, 'yyyy-MM-DD HH24:MI') order by doc_date desc, doc_time desc " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + " ), 0) else 0 end as last_flag, line_number,branch_code from ic_trans_detail where " + _g.d.ic_trans_detail._doc_date + " between " + dateStart + " and " + dateStop + " " + __docNoWare + " and " + _g.d.ic_trans_detail._trans_flag + " in (" + __transDetailFlagStr.ToString() + ")");
            __subQuery3.Append(" union all ");
            __subQuery3.Append("select 9999 as item_type,charge,coalesce(trans_number,'') as trans_number,'' as item_code,case when doc_type in (0,2) then pass_book_code else trans_number end as pass_book_code,coalesce(doc_no,'') as doc_no ,case when doc_type in (1,2,4) then amount else 0 end as price,amount,0 as sum_of_cost, sum_amount, 0 as sum_amount_exclude_vat, trans_type,trans_flag, doc_type,bank_code as chq_bank_ref,credit_card_type as credit_card_ref,remark,0,0,0, line_number, '' as branch_code from cb_trans_detail where " + _g.d.ic_trans_detail._doc_date + " between " + dateStart + " and " + dateStop + " " + __docNoWare + " and " + _g.d.ic_trans_detail._trans_flag + " in (" + __transDetailFlagStr.ToString() + ") order by " + _g.d.ic_trans_detail._doc_no);
            // Table 1=รายวันย่อย
            __return.Add(__subQuery3.ToString());
            // Table 2=ic_trans_detail
            // รายละเอียดเอกสารของ ic_trans_detail
            //__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_date + " between " + __dateBegin + " and " + __dateEnd + " and " + _g.d.ic_trans_detail._trans_flag + " in (" + __transFlagStr.ToString() + ") order by " + _g.d.ic_trans_detail._doc_no));

            if (_g.g._companyProfile._use_department == 1)
            {
                _transDepartmentTableIdx = __return.Count;
                StringBuilder __query3 = new StringBuilder();
                // , (select  " + _g.d.erp_department_list._name_1 + " from " + _g.d.erp_department_list._table + " where " + _g.d.erp_department_list._table + "." + _g.d.erp_department_list._code + " = " + _g.d.ic_trans_detail_department._table + "." + _g.d.ic_trans_detail_department._department_code + ") as " + _g.d.ic_trans_detail_department._department_name + "
                __query3.Append("select * from " + _g.d.ic_trans_detail_department._table + " where doc_date between " + dateStart + " and " + dateStop + " " + __docNoWare + " and " + _g.d.ic_trans_detail_department._trans_flag + " in (" + __transFlagStr + ")");
                __return.Add(__query3.ToString());
            }

            if (_g.g._companyProfile._use_project == 1)
            {
                _transProjectTableIdx = __return.Count;
                StringBuilder __query4 = new StringBuilder();
                // , (select  " + _g.d.erp_project_list._name_1 + " from " + _g.d.erp_project_list._table + " where " + _g.d.erp_project_list._table + "." + _g.d.erp_project_list._code + " = " + _g.d.ic_trans_detail_project._table + "." + _g.d.ic_trans_detail_project._project_code + ") as " + _g.d.ic_trans_detail_project._project_name + "
                __query4.Append("select * from " + _g.d.ic_trans_detail_project._table + " where doc_date between " + dateStart + " and " + dateStop + " " + __docNoWare + " and " + _g.d.ic_trans_detail_project._trans_flag + " in (" + __transFlagStr + ")");
                __return.Add(__query4.ToString());

            }

            if (_g.g._companyProfile._use_allocate == 1)
            {
                _transAllocateTableIdx = __return.Count;
                StringBuilder __query5 = new StringBuilder();
                // , (select  " + _g.d.erp_allocate_list._name_1 + " from " + _g.d.erp_allocate_list._table + " where " + _g.d.erp_allocate_list._table + "." + _g.d.erp_allocate_list._code + " = " + _g.d.ic_trans_detail_allocate._table + "." + _g.d.ic_trans_detail_allocate._allocate_code + ") as " + _g.d.ic_trans_detail_allocate._allocate_name + "
                __query5.Append("select * from " + _g.d.ic_trans_detail_allocate._table + " where doc_date between " + dateStart + " and " + dateStop + " " + __docNoWare + " and " + _g.d.ic_trans_detail_allocate._trans_flag + " in (" + __transFlagStr + ")");
                __return.Add(__query5.ToString());

            }

            if (_g.g._companyProfile._use_unit == 1)
            {
                _transSiteTableIdx = __return.Count;
                StringBuilder __query6 = new StringBuilder();
                // , (select  " + _g.d.erp_allocate_list._name_1 + " from " + _g.d.erp_allocate_list._table + " where " + _g.d.erp_allocate_list._table + "." + _g.d.erp_allocate_list._code + " = " + _g.d.ic_trans_detail_allocate._table + "." + _g.d.ic_trans_detail_allocate._allocate_code + ") as " + _g.d.ic_trans_detail_allocate._allocate_name + "
                __query6.Append("select * from " + _g.d.ic_trans_detail_site._table + " where doc_date between " + dateStart + " and " + dateStop + " " + __docNoWare + " and " + _g.d.ic_trans_detail_site._trans_flag + " in (" + __transFlagStr + ")");
                __return.Add(__query6.ToString());

            }

            if (_g.g._companyProfile._use_job == 1)
            {
                _transJobsTableIdx = __return.Count;
                StringBuilder __query7 = new StringBuilder();
                // , (select  " + _g.d.erp_allocate_list._name_1 + " from " + _g.d.erp_allocate_list._table + " where " + _g.d.erp_allocate_list._table + "." + _g.d.erp_allocate_list._code + " = " + _g.d.ic_trans_detail_allocate._table + "." + _g.d.ic_trans_detail_allocate._allocate_code + ") as " + _g.d.ic_trans_detail_allocate._allocate_name + "
                __query7.Append("select * from " + _g.d.ic_trans_detail_jobs._table + " where doc_date between " + dateStart + " and " + dateStop + " " + __docNoWare + " and " + _g.d.ic_trans_detail_jobs._trans_flag + " in (" + __transFlagStr + ")");
                __return.Add(__query7.ToString());

            }


            return __return.ToArray();
        }

        int _transDepartmentTableIdx = -1;
        int _transProjectTableIdx = -1;
        int _transAllocateTableIdx = -1;
        int _transSiteTableIdx = -1;
        int _transJobsTableIdx = -1;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountType"></param>
        /// <param name="icTransDetail"></param>
        /// <param name="docNo"></param>
        /// <param name="accountCode"></param>
        /// <param name="transFlagCompare"></param>
        /// <param name="glTemp"></param>
        /// <param name="accountDescription"></param>
        /// <param name="isDebit"></param>
        /// <param name="custCode"></param>
        /// <param name="custName"></param>
        /// <param name="extraWhere"></param>
        /// <param name="varRate"></param>
        /// <param name="vatType">0=แยกนอก,1=รวมใน,2=อัตราศูนย์</param>
        /// <returns></returns>
        private Boolean _accountByDetail(_accountDetailType accountType, DataTable icTransDetail, string docNo, string accountCode, _g.g._transControlTypeEnum transFlagCompare, _glStruct glTemp, string accountDescription, Boolean isDebit, string custCode, string custName, string extraWhere)
        {
            string __branchCodeDetail = "";

            accountCode = accountCode.Replace(" ", "");
            if (accountCode.IndexOf("&") == -1 || icTransDetail.Rows.Count == 0)
            {
                // กรณีไม่ระบุตัวแปร ให้ return false
                return false;
            }
            String __accountCodeFixed = "";
            if (accountCode.IndexOf(',') != -1)
            {
                String[] __split = accountCode.Split(',');
                accountCode = __split[0];
                __accountCodeFixed = __split[1];
            }
            Boolean __foundByDetail = false;

            // try
            {
                string __query = _g.d.ic_trans_detail._trans_flag + " = \'" + _g.g._transFlagGlobal._transFlag(transFlagCompare).ToString() + "\' and " + _g.d.ic_trans_detail._doc_no + " = \'" + docNo + "\' " + extraWhere;
                DataRow[] __rowDetail = icTransDetail.Rows.Count == 0 ? null : icTransDetail.Select(__query);
                if (__rowDetail != null && __rowDetail.Length > 0)
                {
                    for (int __detail = 0; __detail < __rowDetail.Length; __detail++)
                    {
                        string __accountCodeDetail = "";
                        string __accountDescriptionTemp = accountDescription;
                        decimal __amount = 0.0M;
                        string __lastItemCode = "";
                        string __lastItemName = "";

                        int __line_number = MyLib._myGlobal._intPhase(__rowDetail[__detail][_g.d.ic_trans_detail._line_number].ToString());
                        string __item_code = __rowDetail[__detail][_g.d.ic_trans_detail._item_code].ToString();


                        switch (accountType)
                        {
                            #region ธนาคาร_โอนเงินระหว่างธนาคาร_ออก
                            case _accountDetailType.ธนาคาร_โอนเงินระหว่างธนาคาร_ออก:
                                {
                                    string __passBookCode = __rowDetail[__detail]["pass_book_code"].ToString().Trim();
                                    DataRow[] __selectAccountData = _passBook.Select(_g.d.erp_pass_book._code + " = \'" + __passBookCode + "\'");
                                    if (__selectAccountData.Length > 0)
                                    {
                                        decimal __amountCalc = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._transfer_amount].ToString()) + MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._fee_amount].ToString());
                                        switch (accountCode.ToUpper())
                                        {
                                            case "&ACCBANK1&":
                                                __accountCodeDetail = __selectAccountData[0][_g.d.erp_pass_book._account_code_1].ToString().Trim().ToUpper();
                                                __amount = __amountCalc;
                                                break;
                                            case "&ACCBANK2&":
                                                __accountCodeDetail = __selectAccountData[0][_g.d.erp_pass_book._account_code_2].ToString().Trim().ToUpper();
                                                __amount = __amountCalc;
                                                break;
                                        }
                                    }
                                }
                                break;
                            #endregion
                            #region ธนาคาร_โอนเงินระหว่างธนาคาร_เข้า
                            case _accountDetailType.ธนาคาร_โอนเงินระหว่างธนาคาร_เข้า:
                                {
                                    string __passBookCode = __rowDetail[__detail]["pass_book_code"].ToString().Trim();
                                    DataRow[] __selectAccountData = _passBook.Select(_g.d.erp_pass_book._code + " = \'" + __passBookCode + "\'");
                                    if (__selectAccountData.Length > 0)
                                    {
                                        switch (accountCode.ToUpper())
                                        {
                                            case "&ACCBANK1&":
                                                __accountCodeDetail = __selectAccountData[0][_g.d.erp_pass_book._account_code_1].ToString().Trim().ToUpper();
                                                __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._transfer_amount].ToString());
                                                break;
                                            case "&ACCBANK2&":
                                                __accountCodeDetail = __selectAccountData[0][_g.d.erp_pass_book._account_code_2].ToString().Trim().ToUpper();
                                                __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._transfer_amount].ToString());
                                                break;
                                        }
                                    }
                                }
                                break;
                            #endregion
                            #region ธนาคาร_บัตรเครดิต
                            case _accountDetailType.ธนาคาร_บัตรเครดิต:
                                {
                                    string __accountCode = __rowDetail[__detail]["credit_card_ref"].ToString().Trim();
                                    if (this._creditCardAccount.Rows.Count > 0)
                                    {
                                        DataRow[] __selectData = this._creditCardAccount.Select(_g.d.erp_credit_type._code + " = \'" + __accountCode + "\'");
                                        switch (accountCode.ToUpper())
                                        {
                                            case "&ACCBANK&":
                                                if (__selectData.Length > 0 && __selectData.Length > 0)
                                                {
                                                    __accountCodeDetail = __selectData[0][_g.d.erp_credit_type._account_code].ToString().Trim();
                                                    __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._sum_amount].ToString()) + ((transFlagCompare == _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน) ? MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._sum_of_cost].ToString()) : 0M);
                                                }
                                                break;
                                        }
                                    }
                                }
                                break;
                            #endregion
                            #region ธนาคาร_เช็ครับล่วงหน้า
                            case _accountDetailType.ธนาคาร_เช็ครับล่วงหน้า:
                                {
                                    string __accountCode = __rowDetail[__detail]["chq_bank_ref"].ToString().Trim();
                                    if (this._bankIncomeAccount.Rows.Count > 0)
                                    {
                                        DataRow[] __selectData = this._bankIncomeAccount.Select(_g.d.erp_bank._code + " = \'" + __accountCode + "\'");
                                        switch (accountCode.ToUpper())
                                        {
                                            case "&ACCBANK&":
                                                if (__selectData.Length > 0 && __selectData.Length > 0)
                                                {
                                                    __accountCodeDetail = __selectData[0][_g.d.erp_bank._chq_income_account_code].ToString().Trim();
                                                    __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._price].ToString());
                                                }
                                                break;
                                        }
                                    }
                                }
                                break;
                            #endregion
                            #region ธนาคาร_เช็คจ่ายล่วงหน้า
                            case _accountDetailType.ธนาคาร_เช็คจ่ายล่วงหน้า:
                                {
                                    // ถอนเงิน,ฝากเงิน
                                    string __passBookCode = __rowDetail[__detail][_g.d.cb_trans_detail._pass_book_code].ToString().Trim();
                                    if (_passBook.Rows.Count > 0)
                                    {
                                        DataRow[] __selectData = _passBook.Select(_g.d.erp_pass_book._code + " = \'" + __passBookCode + "\'");
                                        switch (accountCode.ToUpper())
                                        {
                                            case "&ACCBANK1&":
                                                if (__selectData.Length > 0 && __selectData.Length > 0)
                                                {
                                                    __accountCodeDetail = __selectData[0][_g.d.erp_pass_book._account_code_1].ToString().Trim();
                                                    __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.cb_trans_detail._amount].ToString());
                                                }
                                                break;
                                            case "&ACCBANK2&":
                                                if (__selectData.Length > 0)
                                                {
                                                    __accountCodeDetail = __selectData[0][_g.d.erp_pass_book._account_code_2].ToString().Trim();
                                                    __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.cb_trans_detail._amount].ToString());
                                                }
                                                break;
                                        }
                                    }
                                }
                                break;
                            #endregion
                            #region ค่าใช้จ่ายอื่น
                            case _accountDetailType.ค่าใช้จ่ายอื่น:
                                {
                                    string __accountCode = __rowDetail[__detail][_g.d.ic_trans_detail._item_code].ToString().Trim();
                                    __branchCodeDetail = __rowDetail[__detail][_g.d.ic_trans_detail._branch_code].ToString().Trim();

                                    if (extraWhere == "and doc_type=\'11\'" || extraWhere.Trim() == "and doc_type=\'11\'")
                                    {
                                        __accountCode = __rowDetail[__detail][_g.d.cb_trans_detail._pass_book_code].ToString().Trim();
                                    }

                                    if (this._otherExpense.Rows.Count > 0)
                                    {
                                        DataRow[] __selectData = this._otherExpense.Select(_g.d.erp_expenses_list._code + " = \'" + __accountCode + "\'");
                                        switch (accountCode.ToUpper())
                                        {
                                            case "&ACC&":
                                                if (__selectData.Length > 0 && __selectData.Length > 0)
                                                {
                                                    __accountCodeDetail = __selectData[0][_g.d.erp_expenses_list._gl_account_code].ToString().Trim();

                                                    if (extraWhere == "and doc_type=\'11\'" || extraWhere.Trim() == "and doc_type=\'11\'")
                                                    {
                                                        __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.cb_trans_detail._amount].ToString());
                                                    }
                                                    else
                                                        __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail]["sum_amount_exclude_vat"].ToString());
                                                    //__amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.cb_trans_detail._amount].ToString());
                                                }
                                                break;
                                        }
                                    }
                                }
                                break;
                            #endregion
                            #region รายได้อื่น
                            case _accountDetailType.รายได้อื่น:
                                {
                                    string __accountCode = __rowDetail[__detail][_g.d.ic_trans_detail._item_code].ToString().Trim();

                                    if (extraWhere == "and doc_type=\'12\'" || extraWhere.Trim() == "and doc_type=\'12\'")
                                    {
                                        __accountCode = __rowDetail[__detail][_g.d.cb_trans_detail._pass_book_code].ToString().Trim();
                                    }
                                    if (this._otherIncome.Rows.Count > 0)
                                    {
                                        DataRow[] __selectData = this._otherIncome.Select(_g.d.erp_income_list._code + " = \'" + __accountCode + "\'");
                                        switch (accountCode.ToUpper())
                                        {
                                            case "&ACC&":
                                                if (__selectData.Length > 0 && __selectData.Length > 0)
                                                {
                                                    __accountCodeDetail = __selectData[0][_g.d.erp_income_list._gl_account_code].ToString().Trim();
                                                    if (extraWhere == "and doc_type=\'12\'" || extraWhere.Trim() == "and doc_type=\'12\'")
                                                    {
                                                        __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.cb_trans_detail._amount].ToString());
                                                    }
                                                    else
                                                        __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail]["sum_amount_exclude_vat"].ToString());
                                                    //__amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.cb_trans_detail._amount].ToString());
                                                }
                                                break;
                                        }
                                    }
                                }
                                break;
                            #endregion
                            #region สินค้า_ราคา
                            case _accountDetailType.สินค้า_ราคา:
                                {
                                    string __itemCode = __rowDetail[__detail][_g.d.ic_trans_detail._item_code].ToString().Trim();
                                    if (_icInventory.Rows.Count > 0)
                                    {
                                        DataRow[] __selectData = _icInventory.Select(_g.d.ic_inventory._code + " = \'" + __itemCode + "\'");
                                        if (__selectData.Length > 0)
                                        {
                                            __lastItemCode = __selectData[0][_g.d.ic_inventory._code].ToString().Trim();
                                            __lastItemName = __selectData[0][_g.d.ic_inventory._name_1].ToString().Trim();
                                            switch (accountCode.ToUpper())
                                            {
                                                case "&ACC1&":
                                                    {
                                                        __accountCodeDetail = __selectData[0][_g.d.ic_inventory._account_code_1].ToString().Trim();
                                                        if (__accountCodeDetail.Length == 0)
                                                        {
                                                            __accountCodeDetail = __accountCodeFixed;
                                                        }
                                                        __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._sum_amount_exclude_vat].ToString());
                                                    }
                                                    break;
                                                case "&ACC2&":
                                                    {
                                                        __accountCodeDetail = __selectData[0][_g.d.ic_inventory._account_code_2].ToString().Trim();
                                                        if (__accountCodeDetail.Length == 0)
                                                        {
                                                            __accountCodeDetail = __accountCodeFixed;
                                                        }
                                                        __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._sum_amount_exclude_vat].ToString());
                                                    }
                                                    break;
                                                case "&ACC3&":
                                                    {
                                                        __accountCodeDetail = __selectData[0][_g.d.ic_inventory._account_code_3].ToString().Trim();
                                                        if (__accountCodeDetail.Length == 0)
                                                        {
                                                            __accountCodeDetail = __accountCodeFixed;
                                                        }
                                                        __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._sum_amount_exclude_vat].ToString());
                                                    }
                                                    break;
                                                case "&ACC4&":
                                                    {
                                                        __accountCodeDetail = __selectData[0][_g.d.ic_inventory._account_code_4].ToString().Trim();
                                                        if (__accountCodeDetail.Length == 0)
                                                        {
                                                            __accountCodeDetail = __accountCodeFixed;
                                                        }
                                                        __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._sum_amount_exclude_vat].ToString());
                                                    }
                                                    break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                    }
                                    if (__accountCodeDetail.Length == 0)
                                    {
                                        __accountCodeDetail = "blank";
                                        __accountDescriptionTemp = __itemCode;
                                    }
                                }
                                break;
                            #endregion
                            #region สินค้า_ต้นทุน
                            case _accountDetailType.สินค้า_ต้นทุน:
                                {
                                    string __itemCode = __rowDetail[__detail][_g.d.ic_trans_detail._item_code].ToString().Trim();
                                    if (_icInventory.Rows.Count > 0)
                                    {
                                        DataRow[] __selectData = _icInventory.Select(_g.d.ic_inventory._code + " = \'" + __itemCode + "\'");
                                        if (__selectData.Length > 0)
                                        {
                                            __lastItemCode = __selectData[0][_g.d.ic_inventory._code].ToString().Trim();
                                            __lastItemName = __selectData[0][_g.d.ic_inventory._name_1].ToString().Trim();
                                            switch (accountCode.ToUpper())
                                            {
                                                case "&ACC1&":
                                                    {
                                                        __accountCodeDetail = __selectData[0][_g.d.ic_inventory._account_code_1].ToString().Trim();
                                                        if (__accountCodeDetail.Length == 0)
                                                        {
                                                            __accountCodeDetail = __accountCodeFixed;
                                                        }
                                                        __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._sum_of_cost].ToString());
                                                    }
                                                    break;
                                                case "&ACC2&":
                                                    {
                                                        __accountCodeDetail = __selectData[0][_g.d.ic_inventory._account_code_2].ToString().Trim();
                                                        if (__accountCodeDetail.Length == 0)
                                                        {
                                                            __accountCodeDetail = __accountCodeFixed;
                                                        }
                                                        __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._sum_of_cost].ToString());
                                                    }
                                                    break;
                                                case "&ACC3&":
                                                    {
                                                        __accountCodeDetail = __selectData[0][_g.d.ic_inventory._account_code_3].ToString().Trim();
                                                        if (__accountCodeDetail.Length == 0)
                                                        {
                                                            __accountCodeDetail = __accountCodeFixed;
                                                        }
                                                        __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._sum_amount_exclude_vat].ToString());
                                                    }
                                                    break;
                                                case "&ACC4&":
                                                    {
                                                        __accountCodeDetail = __selectData[0][_g.d.ic_inventory._account_code_4].ToString().Trim();
                                                        if (__accountCodeDetail.Length == 0)
                                                        {
                                                            __accountCodeDetail = __accountCodeFixed;
                                                        }
                                                        __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._sum_amount_exclude_vat].ToString());
                                                    }
                                                    break;
                                            }
                                        }
                                    }
                                    if (__accountCodeDetail.Length == 0)
                                    {
                                        __accountCodeDetail = "blank";
                                        __accountDescriptionTemp = __itemCode;
                                    }
                                }
                                break;
                            #endregion
                            #region เงินสดย่อย
                            case _accountDetailType.เงินสดย่อย:
                                {
                                    string __pettyCashCode = __rowDetail[__detail][_g.d.cb_trans_detail._trans_number].ToString().Trim();
                                    if (this._pettyCash.Rows.Count > 0)
                                    {
                                        DataRow[] __selectData = this._pettyCash.Select(_g.d.cb_petty_cash._code + " = \'" + __pettyCashCode + "\'");
                                        switch (accountCode.ToUpper())
                                        {
                                            case "&ACC&":
                                                if (__selectData.Length > 0 && __selectData.Length > 0)
                                                {
                                                    __accountCodeDetail = __selectData[0][_g.d.cb_petty_cash._chart_of_account].ToString().Trim();
                                                    __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.cb_trans_detail._amount].ToString());
                                                }
                                                break;
                                        }
                                    }
                                }
                                break;
                            #endregion
                            #region รายวันเงินสดย่อย
                            case _accountDetailType.รายวันเงินสดย่อย:
                                {
                                    string __pettyCashCode = __rowDetail[__detail][_g.d.cb_trans_detail._pass_book_code].ToString().Trim();
                                    if (this._pettyCash.Rows.Count > 0)
                                    {
                                        DataRow[] __selectData = this._pettyCash.Select(_g.d.cb_petty_cash._code + " = \'" + __pettyCashCode + "\'");
                                        switch (accountCode.ToUpper())
                                        {
                                            case "&ACC&":
                                                if (__selectData.Length > 0 && __selectData.Length > 0)
                                                {
                                                    __accountCodeDetail = __selectData[0][_g.d.cb_petty_cash._chart_of_account].ToString().Trim();
                                                    __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.cb_trans_detail._amount].ToString());
                                                }
                                                break;
                                        }
                                    }
                                }
                                break;
                            #endregion
                            #region ธนาคาร
                            case _accountDetailType.ธนาคาร:
                                {
                                    // ถอนเงิน,ฝากเงิน
                                    string __passBookCode = __rowDetail[__detail][_g.d.cb_trans_detail._pass_book_code].ToString().Trim();
                                    if (_passBook.Rows.Count > 0)
                                    {
                                        DataRow[] __selectData = _passBook.Select(_g.d.erp_pass_book._code + " = \'" + __passBookCode + "\'");
                                        switch (accountCode.ToUpper())
                                        {
                                            case "&ACCBANK1&":
                                                if (__selectData.Length > 0 && __selectData.Length > 0)
                                                {
                                                    __accountCodeDetail = __selectData[0][_g.d.erp_pass_book._account_code_1].ToString().Trim();
                                                    __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.cb_trans_detail._amount].ToString());
                                                }
                                                break;
                                            case "&ACCBANK2&":
                                                if (__selectData.Length > 0)
                                                {
                                                    __accountCodeDetail = __selectData[0][_g.d.erp_pass_book._account_code_2].ToString().Trim();
                                                    __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.cb_trans_detail._amount].ToString());
                                                }
                                                break;
                                        }
                                    }
                                }
                                break;
                            #endregion
                            #region ธนาคาร_เช็ครับคืน
                            case _accountDetailType.ธนาคาร_เช็ครับคืน:
                                {
                                    decimal __amountCalc = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._sum_amount].ToString()) + MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._sum_of_cost].ToString());
                                    string __passBookCode = __rowDetail[__detail][_g.d.cb_trans_detail._pass_book_code].ToString().Trim();
                                    if (_passBook.Rows.Count > 0)
                                    {
                                        DataRow[] __selectData = _passBook.Select(_g.d.erp_pass_book._code + " = \'" + __passBookCode + "\'");
                                        switch (accountCode.ToUpper())
                                        {
                                            case "&ACCBANK1&":
                                                if (__selectData.Length > 0 && __selectData.Length > 0)
                                                {
                                                    __accountCodeDetail = __selectData[0][_g.d.erp_pass_book._account_code_1].ToString().Trim();
                                                    __amount = __amountCalc;
                                                }
                                                break;
                                            case "&ACCBANK2&":
                                                if (__selectData.Length > 0)
                                                {
                                                    __accountCodeDetail = __selectData[0][_g.d.erp_pass_book._account_code_2].ToString().Trim();
                                                    __amount = __amountCalc;
                                                }
                                                break;
                                        }
                                    }
                                }
                                break;
                            #endregion
                            #region เช็ครับ
                            case _accountDetailType.เช็ครับ:
                                {
                                    string __itemCode = __rowDetail[__detail][_g.d.cb_trans_detail._pass_book_code].ToString().Trim();
                                    if (_passBook.Rows.Count > 0)
                                    {
                                        DataRow[] __selectData1 = _passBook.Select(_g.d.erp_pass_book._code + " = \'" + __itemCode + "\'");
                                        DataRow[] __selectData2 = _passBook.Select(_g.d.erp_pass_book._code + " = \'" + __rowDetail[__detail][_g.d.cb_trans_detail._trans_number].ToString().Trim() + "\'");
                                        switch (accountCode.ToUpper())
                                        {
                                            case "&ACCBANK1&":
                                                if (__selectData2.Length > 0 && __selectData2.Length > 0)
                                                {
                                                    __accountCodeDetail = __selectData2[0][_g.d.erp_pass_book._account_code_1].ToString().Trim();
                                                    __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.cb_trans_detail._amount].ToString());
                                                }
                                                break;
                                            case "&ACCBANK2&":
                                                if (__selectData1.Length > 0)
                                                {
                                                    __accountCodeDetail = __selectData1[0][_g.d.erp_pass_book._account_code_2].ToString().Trim();
                                                    __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.cb_trans_detail._amount].ToString());
                                                }
                                                break;
                                        }
                                    }
                                }
                                break;
                            #endregion
                            #region เช็คจ่าย
                            case _accountDetailType.เช็คจ่าย:
                                {
                                    string __itemCode = __rowDetail[__detail][_g.d.cb_trans_detail._pass_book_code].ToString().Trim();
                                    if (_passBook.Rows.Count > 0)
                                    {
                                        DataRow[] __selectData1 = _passBook.Select(_g.d.erp_pass_book._code + " = \'" + __itemCode + "\'");
                                        DataRow[] __selectData2 = _passBook.Select(_g.d.erp_pass_book._code + " = \'" + __rowDetail[__detail][_g.d.cb_trans_detail._trans_number].ToString().Trim() + "\'");
                                        switch (accountCode.ToUpper())
                                        {
                                            case "&ACCBANK1&":
                                                if (__selectData2.Length > 0 && __selectData2.Length > 0)
                                                {
                                                    __accountCodeDetail = __selectData2[0][_g.d.erp_pass_book._account_code_1].ToString().Trim();
                                                    __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.cb_trans_detail._amount].ToString());
                                                }
                                                break;
                                            case "&ACCBANK2&":
                                                if (__selectData1.Length > 0)
                                                {
                                                    __accountCodeDetail = __selectData1[0][_g.d.erp_pass_book._account_code_2].ToString().Trim();
                                                    __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.cb_trans_detail._amount].ToString());
                                                }
                                                break;
                                        }
                                    }
                                }
                                break;
                            #endregion

                            case _accountDetailType.เงินล่่วงหน้ารับ:
                                {
                                    string __transNumber = __rowDetail[__detail][_g.d.cb_trans_detail._trans_number].ToString().Trim();
                                    // ดึงบัญชี ต้นทาง
                                    if (accountCode.ToUpper().Equals("&ACC&"))
                                    {
                                        string __baseAccountCodeDeposit = "select " + _g.d.erp_doc_format_gl._account_code_credit + " from erp_doc_format_gl where condition_number= 1 and erp_doc_format_gl.doc_code = (select doc_format_code from ic_trans where doc_no = \'" + __transNumber + "\')";
                                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                                        DataSet __depotsitResult = __myFrameWork._queryShort(__baseAccountCodeDeposit);
                                        if (__depotsitResult.Tables.Count > 0 && __depotsitResult.Tables[0].Rows.Count > 0)
                                        {
                                            __accountCodeDetail = __depotsitResult.Tables[0].Rows[0][_g.d.erp_doc_format_gl._account_code_credit].ToString().Trim();
                                            if (__accountCodeDetail.Length == 0)
                                            {
                                                __accountCodeDetail = __accountCodeFixed;
                                            }
                                            __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.cb_trans_detail._amount].ToString());
                                        }

                                    }
                                }
                                break;
                        }


                        if (__accountCodeDetail.Length > 0 || __rowDetail[__detail][_g.d.cb_trans_detail._remark].ToString().Trim().Length > 0)
                        {
                            __foundByDetail = true;
                            decimal __debit = 0.0M;
                            decimal __credit = 0.0M;
                            _glDetailStruct __glDetailTemp = new _glDetailStruct();
                            string __accountName = "";
                            DataRow[] __findChartOfAccount = this._chatOfAccount.Select("code=\'" + __accountCodeDetail.Trim() + "\'");
                            if (__findChartOfAccount.Length > 0)
                            {
                                __accountName = __findChartOfAccount[0][_g.d.gl_chart_of_account._name_1].ToString();
                            }
                            __glDetailTemp._accountCode = __accountCodeDetail;
                            string __getAccountDescription = __accountDescriptionTemp;
                            try
                            {
                                __getAccountDescription = __getAccountDescription.Replace("&ACC_NAME&", ((_g.g._companyProfile._gl_trans_type == 0) ? __accountName : ""));
                            }
                            catch
                            {
                            }
                            try
                            {
                                __getAccountDescription = __getAccountDescription.Replace("&ITEM_CODE&", ((_g.g._companyProfile._gl_trans_type == 0) ? __lastItemCode : ""));
                            }
                            catch
                            {
                            }
                            try
                            {
                                __getAccountDescription = __getAccountDescription.Replace("&ITEM_NAME&", ((_g.g._companyProfile._gl_trans_type == 0) ? __lastItemName : ""));
                            }
                            catch
                            {
                            }
                            // remark ได้ทั้ง 2 case
                            StringBuilder __remark = new StringBuilder(this._transRemark.Trim() + " " + __rowDetail[__detail][_g.d.cb_trans_detail._remark].ToString());
                            while (__remark.Length > 0 && __remark[0] == ' ')
                            {
                                __remark.Remove(0, 1);
                            }
                            __getAccountDescription = __getAccountDescription.Replace("&REMARK&", __remark.ToString());
                            __getAccountDescription = __getAccountDescription.Replace("&CUST_CODE&", custCode);
                            __getAccountDescription = __getAccountDescription.Replace("&CUST_NAME&", custName);
                            __getAccountDescription = __getAccountDescription.Replace("&AP_CODE&", custCode);
                            __getAccountDescription = __getAccountDescription.Replace("&AP_NAME&", custName);
                            __getAccountDescription = __getAccountDescription.Replace("&AR_CODE&", custCode);
                            __getAccountDescription = __getAccountDescription.Replace("&AR_NAME&", custName);

                            if (isDebit)
                                __debit = __amount;
                            else
                                __credit = __amount;
                            if (__glDetailTemp._accountCode.Trim().Length > 0 && (__debit != 0 || __credit != 0))
                            {
                                __glDetailTemp._accountName = __accountName;
                                __glDetailTemp._accountDescription = __getAccountDescription;
                                __glDetailTemp._debitOrCredit = (isDebit) ? 0 : 1;
                                __glDetailTemp._lineNumber = _lineNumber++;

                                // toe ใส่ปัดเศษ
                                __glDetailTemp._debit = Math.Round(__debit, 2);
                                __glDetailTemp._credit = Math.Round(__credit, 2);
                                __glDetailTemp._sortStr = ((isDebit) ? "0" : "1") + __accountCodeDetail;
                                __glDetailTemp._branchCodeDetail = __branchCodeDetail;

                                glTemp._glDetail.Add(__glDetailTemp);
                                _sumDebit += Math.Round(__debit, 2);
                                _sumCredit += Math.Round(__credit, 2);



                                // แทรกแผนก โครงการ การจัดสรร

                                // แยกแผนก
                                if (_g.g._companyProfile._use_department == 1 && ((this._icTransDetailDepartment != null && this._icTransDetailDepartment.Rows.Count > 0) || this._departmentCode.Length > 0))
                                {
                                    this._processByDist(_accountDistType.แผนก, this._icTransDetailDepartment, docNo, accountCode, transFlagCompare, __item_code, __line_number, glTemp, __glDetailTemp, __amount, isDebit, "");
                                }

                                // แยกโครงการ
                                if (_g.g._companyProfile._use_project == 1 && ((this._icTransDetailProject != null && this._icTransDetailProject.Rows.Count > 0) || this._projectCode.Length > 0))
                                {
                                    _processByDist(_accountDistType.โครงการ, this._icTransDetailProject, docNo, accountCode, transFlagCompare, __item_code, __line_number, glTemp, __glDetailTemp, __amount, isDebit, "");
                                }

                                // แยกการจัดสรร
                                if (_g.g._companyProfile._use_allocate == 1 && ((this._icTransDetailAllocate != null && this._icTransDetailAllocate.Rows.Count > 0) || this._allocateCode.Length > 0))
                                {
                                    _processByDist(_accountDistType.การจัดสรร, this._icTransDetailAllocate, docNo, accountCode, transFlagCompare, __item_code, __line_number, glTemp, __glDetailTemp, __amount, isDebit, "");
                                }

                                // แยกการจัดสรร
                                if (_g.g._companyProfile._use_unit == 1 && ((this._icTransDetailSite != null && this._icTransDetailSite.Rows.Count > 0) || this._siteCode.Length > 0))
                                {
                                    _processByDist(_accountDistType.หน่วยงาน, this._icTransDetailSite, docNo, accountCode, transFlagCompare, __item_code, __line_number, glTemp, __glDetailTemp, __amount, isDebit, "");
                                }

                                // แยกการจัดสรร
                                if (_g.g._companyProfile._use_job == 1 && ((this._icTransDetailJobs != null && this._icTransDetailJobs.Rows.Count > 0) || this._jobCode.Length > 0))
                                {
                                    _processByDist(_accountDistType.งาน, this._icTransDetailJobs, docNo, accountCode, transFlagCompare, __item_code, __line_number, glTemp, __glDetailTemp, __amount, isDebit, "");
                                }



                            }


                        }

                    }
                }
                if (_g.g._companyProfile._gl_trans_type == 1 && transFlagCompare.ToString().IndexOf("เงินสดธนาคาร") == -1)
                {
                    // คอมปาว
                    int __addr = 0;
                    while (__addr < glTemp._glDetail.Count)
                    {
                        for (int __loopDetail = __addr + 1; __loopDetail < glTemp._glDetail.Count; __loopDetail++)
                        {
                            if (glTemp._glDetail[__addr]._accountCode.Equals(glTemp._glDetail[__loopDetail]._accountCode) && glTemp._glDetail[__addr]._branchCodeDetail.Equals(glTemp._glDetail[__loopDetail]._branchCodeDetail))
                            {
                                // เอาไปรวม
                                glTemp._glDetail[__addr]._credit += glTemp._glDetail[__loopDetail]._credit;
                                glTemp._glDetail[__addr]._debit += glTemp._glDetail[__loopDetail]._debit;
                                glTemp._glDetail.RemoveAt(__loopDetail);
                                break;
                            }
                        }
                        __addr++;
                    }
                }
            }
            //catch
            //{

            //}
            // ถ้ามีรายการย่อยแล้ว ก็ reutrn true เพราะไม่งั้นมันจะซ้ำกันอีก
            return (__foundByDetail == true) ? true : false;
        }

        private decimal _getSum(DataTable icTransDetail, _g.g._transControlTypeEnum transFlagCompare, string docNo, string fieldName, string extraWhere)
        {
            decimal __result = 0.0M;
            if (icTransDetail.Rows.Count > 0)
            {
                DataRow[] __rowDetail = icTransDetail.Select(_g.d.ic_trans_detail._trans_flag + " = " + _g.g._transFlagGlobal._transFlag(transFlagCompare).ToString() + " and " + _g.d.ic_trans_detail._doc_no + " = \'" + docNo + "\' " + extraWhere);
                if (__rowDetail.Length > 0)
                {
                    for (int __detail = 0; __detail < __rowDetail.Length; __detail++)
                    {

                        __result += MyLib._myGlobal._decimalPhase(__rowDetail[__detail][fieldName].ToString());
                    }
                }
            }
            return __result;
        }

        private decimal _getSum(DataTable icTransDetail, _g.g._transControlTypeEnum transFlagCompare, string docNo, string fieldName)
        {
            return _getSum(icTransDetail, transFlagCompare, docNo, fieldName, "");
        }

        private decimal _getSumMain(DataTable icTransDetail, _g.g._transControlTypeEnum transFlagCompare, string docNo, string fieldName)
        {
            return _getSum(icTransDetail, transFlagCompare, docNo, fieldName);
            /*decimal __result = 0.0M;
            DataRow[] __rowDetail = icTransDetail.Select(_g.d.ic_trans_detail._trans_flag + " = " + _g.g._transFlagGlobal._transFlag(transFlagCompare).ToString() + " and " + _g.d.ic_trans_detail._doc_no + " = \'" + docNo + "\'");
            if (__rowDetail.Length > 0)
            {

                for (int __detail = 0; __detail < __rowDetail.Length; __detail++)
                {

                    __result += MyLib._myGlobal._decimalPhase(__rowDetail[__detail][fieldName].ToString());
                }

            }
            return __result;*/
        }

        ArrayList _getData = null;


        DataTable _icTransDetailDepartment;
        DataTable _icTransDetailProject;
        DataTable _icTransDetailAllocate;
        DataTable _icTransDetailSite;
        DataTable _icTransDetailJobs;

        string _departmentCode = "";
        string _projectCode = "";
        string _allocateCode = "";
        string _siteCode = "";
        string _jobCode = "";

        private void _processByDate(string dateBegin, string dateEnd)
        {
            _processByDate(dateBegin, dateEnd, true);
        }

        private List<_glStruct> _glTransTemp = null;
        public List<_glStruct> getGLTrans
        {
            get
            {
                return this._glTransTemp;
            }

            set
            {
                this._glTransTemp = value;
            }
        }

        private void _processByDoc(string docDate, string docNo, string doc_format_code)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myquery = new StringBuilder();
            string[] __queryStr = _processQuery("\'" + docDate + "\'", "\'" + docDate + "\'", doc_format_code, docNo);
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryStr[0]));
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryStr[1]));

            if (_g.g._companyProfile._use_department == 1)
            {
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryStr[this._transDepartmentTableIdx]));

            }

            if (_g.g._companyProfile._use_project == 1)
            {
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryStr[this._transProjectTableIdx]));
            }

            if (_g.g._companyProfile._use_allocate == 1)
            {
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryStr[this._transAllocateTableIdx]));
            }

            if (_g.g._companyProfile._use_unit == 1)
            {
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryStr[this._transSiteTableIdx]));
            }

            if (_g.g._companyProfile._use_job == 1)
            {
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryStr[this._transJobsTableIdx]));
            }


            __myquery.Append("</node>");
            string __query = __myquery.ToString();
            this._getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query);
            //
            this._icTrans = ((DataSet)this._getData[0]).Tables[0];
            this._icTransDetail = ((DataSet)this._getData[1]).Tables[0];

            if (_g.g._companyProfile._use_department == 1)
            {
                this._icTransDetailDepartment = ((DataSet)this._getData[this._transDepartmentTableIdx]).Tables[0];
            }

            if (_g.g._companyProfile._use_project == 1)
            {
                this._icTransDetailProject = ((DataSet)this._getData[this._transProjectTableIdx]).Tables[0];
            }

            // แยกการจัดสรร
            if (_g.g._companyProfile._use_allocate == 1)
            {
                this._icTransDetailAllocate = ((DataSet)this._getData[this._transAllocateTableIdx]).Tables[0];
            }

            // หน่วยงาน
            if (_g.g._companyProfile._use_unit == 1)
            {
                this._icTransDetailSite = ((DataSet)this._getData[this._transSiteTableIdx]).Tables[0];
            }

            // งาน
            if (_g.g._companyProfile._use_job == 1)
            {
                this._icTransDetailJobs = ((DataSet)this._getData[this._transJobsTableIdx]).Tables[0];
            }

            processWithData(true);
        }

        private void _processByDate(string dateBegin, string dateEnd, bool isSave)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myquery = new StringBuilder();
            string[] __queryStr = _processQuery("\'" + dateBegin + "\'", "\'" + dateEnd + "\'");
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryStr[0]));
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryStr[1]));

            if (_g.g._companyProfile._use_department == 1)
            {
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryStr[this._transDepartmentTableIdx]));

            }

            if (_g.g._companyProfile._use_project == 1)
            {
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryStr[this._transProjectTableIdx]));
            }

            if (_g.g._companyProfile._use_allocate == 1)
            {
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryStr[this._transAllocateTableIdx]));
            }

            if (_g.g._companyProfile._use_unit == 1)
            {
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryStr[this._transSiteTableIdx]));
            }

            if (_g.g._companyProfile._use_job == 1)
            {
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryStr[this._transJobsTableIdx]));
            }


            __myquery.Append("</node>");
            string __query = __myquery.ToString();
            this._getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query);
            //
            this._icTrans = ((DataSet)this._getData[0]).Tables[0];
            this._icTransDetail = ((DataSet)this._getData[1]).Tables[0];

            if (_g.g._companyProfile._use_department == 1)
            {
                this._icTransDetailDepartment = ((DataSet)this._getData[this._transDepartmentTableIdx]).Tables[0];
            }

            if (_g.g._companyProfile._use_project == 1)
            {
                this._icTransDetailProject = ((DataSet)this._getData[this._transProjectTableIdx]).Tables[0];
            }

            // แยกการจัดสรร
            if (_g.g._companyProfile._use_allocate == 1)
            {
                this._icTransDetailAllocate = ((DataSet)this._getData[this._transAllocateTableIdx]).Tables[0];
            }

            // หน่วยงาน
            if (_g.g._companyProfile._use_unit == 1)
            {
                this._icTransDetailSite = ((DataSet)this._getData[this._transSiteTableIdx]).Tables[0];
            }

            // งาน
            if (_g.g._companyProfile._use_job == 1)
            {
                this._icTransDetailJobs = ((DataSet)this._getData[this._transJobsTableIdx]).Tables[0];
            }

            processWithData(isSave);
        }

        public void processWithData(bool isSave)
        {
            #region process with data
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            //
            int __count = 0;
            int __countAll = 0;
            List<_glStruct> __glTrans = new List<_glStruct>();
            for (int __row = 0; __row < this._icTrans.Rows.Count; __row++)
            {
                //
                __count++;
                __countAll++;

                _glStruct __glTemp = new _glStruct();
                //
                DataRow __icTransRow = this._icTrans.Rows[__row];

                DateTime __docDateProcess = MyLib._myGlobal._convertDateFromQuery(__icTransRow[_g.d.ic_trans._doc_date].ToString());

                // check period 
                _g.g._accountPeriodClass __accountPeriodClass = _g.g._accountPeriodClassFind(__docDateProcess);
                if (__accountPeriodClass != null && __accountPeriodClass._status == 0)
                {
                    int __transFlag = (int)MyLib._myGlobal._decimalPhase(__icTransRow[_g.d.ic_trans._trans_flag].ToString());
                    int __transType = (int)MyLib._myGlobal._decimalPhase(__icTransRow[_g.d.ic_trans._inquiry_type].ToString());
                    string __docFormatCode = __icTransRow[_g.d.ic_trans._doc_format_code].ToString().ToUpper();
                    // select รูปแบบเอกสาร
                    string __docNo = __icTransRow[_g.d.ic_trans._doc_no].ToString();
                    string __vatDocNo = __icTransRow[_g.d.ic_trans._tax_doc_no].ToString();

                    // ผลต่างจากการ post รายตัว
                    string __diffDebitCode = "";
                    string __diffCreditCode = "";

                    System.Console.WriteLine(__docNo);

                    this._departmentCode = __icTransRow[_g.d.ic_trans._department_code].ToString();
                    this._projectCode = __icTransRow[_g.d.ic_trans._project_code].ToString();
                    this._allocateCode = __icTransRow[_g.d.ic_trans._allocate_code].ToString();
                    this._siteCode = __icTransRow[_g.d.ic_trans._side_code].ToString();
                    this._jobCode = __icTransRow[_g.d.ic_trans._job_code].ToString();

                    //
                    DataRow[] __docFormatSelect = this._docFormat.Select(_g.d.erp_doc_format._code + "=\'" + __docFormatCode + "\'");


                    if (__docFormatSelect.Length > 0 && this._docFormatGL.Rows.Count > 0)
                    {
                        // try
                        {
                            _lineNumber = 1;
                            string __screenCode = __docFormatSelect[0][_g.d.erp_doc_format._screen_code].ToString();
                            _g.g._transControlTypeEnum __transFlagEnum = _g.g._transFlagGlobal._transFlagByScreenCode(__screenCode);
                            // select รูปแบบ GL
                            DataRow[] __glFormat = this._docFormatGL.Select(_g.d.erp_doc_format_gl._doc_code + "=\'" + __docFormatCode + "\'");


                            // สร้างหัว GL
                            __glTemp._docDate = MyLib._myGlobal._convertDateFromQuery(__icTransRow[_g.d.ic_trans._doc_date].ToString());
                            __glTemp._docNo = __docNo;
                            __glTemp._bookCode = __docFormatSelect[0][_g.d.erp_doc_format._gl_book].ToString();
                            __glTemp._branchCode = __icTransRow[_g.d.ic_trans._branch_code].ToString();

                            // find account period
                            _g.g._accountPeriodClass __accountPeriod = _g.g._accountPeriodClassFind(__glTemp._docDate);
                            if (__accountPeriod != null)
                            {
                                __glTemp._account_year = __accountPeriod._year;
                                __glTemp._account_period = __accountPeriod._number;
                            }


                            string __description = __docFormatSelect[0][_g.d.erp_doc_format._gl_description].ToString();
                            string __custCode = __icTransRow[_g.d.ic_trans._cust_code].ToString();
                            string __custName = __icTransRow[_g.d.ic_trans._cust_name].ToString();
                            this._transRemark = __icTransRow["remark"].ToString();
                            string __system = __icTransRow["trans"].ToString();
                            //Boolean __isIcTrans = (__system.ToUpper().Equals("IC")) ? true : false;
                            //Boolean __isCbTrans = !__isIcTrans;
                            int __last_status = (int)MyLib._myGlobal._decimalPhase(__icTransRow["credit_card_charge"].ToString());
                            __description = __description.Replace("&REMARK&", this._transRemark);
                            __description = __description.Replace("&CUST_CODE&", __custCode);
                            __description = __description.Replace("&CUST_NAME&", __custName);
                            __description = __description.Replace("&AP_CODE&", __custCode);
                            __description = __description.Replace("&AP_NAME&", __custName);
                            __description = __description.Replace("&AR_CODE&", __custCode);
                            __description = __description.Replace("&AR_NAME&", __custName);
                            __description = __description.Replace("&DOC_NO&", __docNo);
                            __description = __description.Replace("&DOC_DATE&", MyLib._myGlobal._convertDateToString(__glTemp._docDate, false).ToString());
                            __description = __description.Replace("&TAX_NO&", __vatDocNo);
                            __description = __description.Replace("&TAX_DATE&", (__icTransRow[_g.d.ic_trans._tax_doc_date].ToString().Length == 0) ? "" : MyLib._myGlobal._convertDateToString(MyLib._myGlobal._convertDateFromQuery(__icTransRow[_g.d.ic_trans._tax_doc_date].ToString()), false).ToString());

                            __glTemp._description = __description;
                            __glTemp._transFlag = __transFlag;

                            if (
                                // สินค้า
                                __transFlagEnum == _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด_ยกเลิก ||

                                // ซื้อ
                                __transFlagEnum == _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก ||

                                __transFlagEnum == _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก ||

                                // ขาย
                                __transFlagEnum == _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก ||

                                __transFlagEnum == _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก ||

                                __transFlagEnum == _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก ||

                                // ธนาคาร
                                __transFlagEnum == _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก ||

                                __transFlagEnum == _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก ||

                                __transFlagEnum == _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย_ยกเลิก ||

                                __transFlagEnum == _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินเข้าธนาคาร_ยกเลิก ||

                                __transFlagEnum == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก_ยกเลิก ||

                                __transFlagEnum == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค_ยกเลิก ||
                                __transFlagEnum == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก_ยกเลิก
                                )
                            {
                                // กลับด้าน
                                __glTemp._docNo = __glTemp._docNo + "*";
                                string __docRef = __icTransRow[_g.d.ic_trans._doc_ref].ToString();

                                // เรียกเอกสารเก่ามาประมวลผลก่อน

                                // เช็ครายการต้นทาง ว่ามีการประมวลผลไปหรือยัง หากยัง ให้ประมวลผลต้นทางก่อน
                                string __checkGLQuery = "select count(" + _g.d.gl_journal._doc_no + ") from " + _g.d.gl_journal._table + " where " + _g.d.gl_journal._doc_no + " = \'" + __docRef + "\' ";
                                DataTable __getRowCheckDataTable = __myFrameWork._queryShort(__checkGLQuery).Tables[0];
                                if (__getRowCheckDataTable.Rows.Count > 0 && MyLib._myGlobal._intPhase(__getRowCheckDataTable.Rows[0][0].ToString()) == 0)
                                {
                                    string __getDocProcessQuery = " select doc_no, doc_date, doc_format_code from ic_trans where doc_no = \'" + __docRef + "\' union all select doc_no, doc_date, doc_format_code from ap_ar_trans where doc_no = \'" + __docRef + "\'  ";
                                    DataTable __getDocDataTable = __myFrameWork._queryShort(__getDocProcessQuery).Tables[0];

                                    if (__getDocDataTable.Rows.Count > 0)
                                    {
                                        string __docNoRefProcess = __getDocDataTable.Rows[0]["doc_no"].ToString();
                                        string __docDateRefProcess = __getDocDataTable.Rows[0]["doc_date"].ToString();
                                        string __docFormatCodeRefProcess = __getDocDataTable.Rows[0]["doc_format_code"].ToString();
                                        //สั่ง ประมวลผลเอกสารต้นทาง
                                        _processByDoc(__docDateRefProcess, __docNoRefProcess, __docFormatCodeRefProcess);
                                    }
                                }


                                //
                                {
                                    string __queryRef = "select * from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._doc_no + "=\'" + __docRef + "\'";
                                    DataTable __icTransDetail2 = __myFrameWork._queryShort(__queryRef).Tables[0];
                                    for (int __row2 = 0; __row2 < __icTransDetail2.Rows.Count; __row2++)
                                    {
                                        _glDetailStruct __glDetailTemp = new _glDetailStruct();
                                        DataRow __dataRow = __icTransDetail2.Rows[__row2];
                                        __glDetailTemp._accountCode = __dataRow[_g.d.gl_journal_detail._account_code].ToString();
                                        __glDetailTemp._accountName = "*" + __dataRow[_g.d.gl_journal_detail._account_name].ToString();
                                        __glDetailTemp._accountDescription = "";
                                        decimal __debit = MyLib._myGlobal._decimalPhase(__dataRow[_g.d.gl_journal_detail._debit].ToString());
                                        decimal __credit = MyLib._myGlobal._decimalPhase(__dataRow[_g.d.gl_journal_detail._credit].ToString());
                                        int __debitOrCredit = int.Parse(__dataRow[_g.d.gl_journal_detail._debit_or_credit].ToString());
                                        __glDetailTemp._debitOrCredit = (__debitOrCredit == 1) ? 0 : 1;
                                        __glDetailTemp._lineNumber = _lineNumber++;
                                        __glDetailTemp._debit = __credit;
                                        __glDetailTemp._credit = __debit;
                                        __glDetailTemp._sortStr = ((__glDetailTemp._debitOrCredit == 1) ? "0" : "1") + __glDetailTemp._accountCode;
                                        if (__glDetailTemp._accountCode.Trim().Length > 0)
                                        {
                                            __glTemp._glDetail.Add(__glDetailTemp);
                                            _sumDebit += __debit;
                                            _sumCredit += __credit;
                                        }
                                    }
                                    __glTrans.Add(__glTemp);
                                    if (__count >= 2000)
                                    {
                                        // Save to Database                        
                                        this._saveToDatabase(__glTrans, __countAll, this._icTrans.Rows.Count);
                                        __count = 0;
                                        __glTrans = new List<_glStruct>();
                                    }
                                }
                            }
                            else
                            {
                                // ดึงตัวแปรจากรายวัน
                                int __vat_type = (int)(MyLib._myGlobal._decimalPhase(__icTransRow[_g.d.ic_trans._vat_type].ToString()));
                                decimal __total_service_amount = Math.Round(MyLib._myGlobal._decimalPhase(__icTransRow["service_amount"].ToString()), 2);
                                decimal __total_amount = Math.Round(MyLib._myGlobal._decimalPhase(__icTransRow[_g.d.ic_trans._total_amount].ToString()), 2);
                                decimal __cash_amount = Math.Round(MyLib._myGlobal._decimalPhase(__icTransRow[_g.d.cb_trans._cash_amount].ToString()), 2);
                                decimal __card_amount = Math.Round(MyLib._myGlobal._decimalPhase(__icTransRow[_g.d.cb_trans._card_amount].ToString()), 2);
                                decimal __chq_amount = Math.Round(MyLib._myGlobal._decimalPhase(__icTransRow[_g.d.cb_trans._chq_amount].ToString()), 2);
                                decimal __tranfer_amount = Math.Round(MyLib._myGlobal._decimalPhase(__icTransRow[_g.d.cb_trans._tranfer_amount].ToString()), 2);
                                decimal __total_income_amount = Math.Round(MyLib._myGlobal._decimalPhase(__icTransRow[_g.d.cb_trans._total_income_amount].ToString()), 2);
                                decimal __petty_cash_amount = Math.Round(MyLib._myGlobal._decimalPhase(__icTransRow[_g.d.cb_trans._petty_cash_amount].ToString()), 2);
                                decimal __total_before_vat = Math.Round(MyLib._myGlobal._decimalPhase(__icTransRow[_g.d.ic_trans._total_before_vat].ToString()), 2);
                                decimal __total_except_vat = Math.Round(MyLib._myGlobal._decimalPhase(__icTransRow[_g.d.ic_trans._total_except_vat].ToString()), 2);
                                decimal __total_vat_value = Math.Round(MyLib._myGlobal._decimalPhase(__icTransRow[_g.d.ic_trans._total_vat_value].ToString()), 2);
                                decimal __total_value = Math.Round(MyLib._myGlobal._decimalPhase(__icTransRow[_g.d.ic_trans._total_value].ToString()), 2);
                                decimal __total_discount = Math.Round(MyLib._myGlobal._decimalPhase(__icTransRow[_g.d.ic_trans._total_discount].ToString()), 2);
                                decimal __tax_at_pay = Math.Round(MyLib._myGlobal._decimalPhase(__icTransRow["tax_at_pay"].ToString()), 2);
                                decimal __advance_amount = Math.Round(MyLib._myGlobal._decimalPhase(__icTransRow["advance_amount"].ToString()), 2);
                                decimal __total_coupon = Math.Round(MyLib._myGlobal._decimalPhase(__icTransRow["coupon_amount"].ToString()), 2);
                                decimal __total_point = Math.Round(MyLib._myGlobal._decimalPhase(__icTransRow["point_amount"].ToString()), 2);
                                decimal __deposit_amount = Math.Round(MyLib._myGlobal._decimalPhase(__icTransRow["deposit_amount"].ToString()), 2);
                                decimal __credit_card_amount = Math.Round(MyLib._myGlobal._decimalPhase(__icTransRow["credit_card_amount"].ToString()), 2);
                                decimal __credit_card_charge = Math.Round(MyLib._myGlobal._decimalPhase(__icTransRow["credit_card_charge"].ToString()), 2);
                                decimal __vat_next_amount = Math.Round(MyLib._myGlobal._decimalPhase(__icTransRow["vat_next"].ToString()), 2);
                                decimal __discount_payment = Math.Round(MyLib._myGlobal._decimalPhase(__icTransRow["discount_amount"].ToString()), 2);

                                decimal __total_income_ohter = Math.Round(MyLib._myGlobal._decimalPhase(__icTransRow[_g.d.cb_trans._total_income_other].ToString()), 2);
                                decimal __total_expense_other = Math.Round(MyLib._myGlobal._decimalPhase(__icTransRow[_g.d.cb_trans._total_expense_other].ToString()), 2);

                                // ภาษีซื้อขอคืนไม่ได้
                                decimal __vatBuyNoReturn = Math.Round(MyLib._myGlobal._decimalPhase(__icTransRow["vat_no_return"].ToString()), 2);

                                // toe
                                decimal __total_cost = Math.Round(MyLib._myGlobal._decimalPhase(__icTransRow[_g.d.ic_trans._total_cost].ToString()), 2);
                                if (__vat_next_amount.CompareTo(0M) != 0 && __system.ToUpper() != "CB")
                                {
                                    __total_vat_value -= __vat_next_amount;
                                }

                                if (__vatBuyNoReturn.CompareTo(0M) != 0 && __system.ToUpper() != "CB")
                                {
                                    __total_vat_value -= __vatBuyNoReturn;
                                }

                                //
                                _g.g._transControlTypeEnum __transFlagCompare = _g.g._transFlagGlobal._transFlagByScreenCode(__screenCode);
                                if (__transFlagCompare == _g.g._transControlTypeEnum.สินค้า_โอนออก)
                                {
                                    // get transflag from doc
                                    __transFlagCompare = _g.g._transFlagGlobal._transFlagByNumber(MyLib._myGlobal._intPhase(__icTransRow["trans_flag"].ToString()));
                                }

                                // รวมยอด
                                _sumDebit = 0.0M;
                                _sumCredit = 0.0M;
                                // สร้างรายละเอียด GL
                                for (int __loop = 0; __loop < __glFormat.Length; __loop++)
                                {
                                    Boolean __addDetail = false;
                                    decimal __debit = 0.0M;
                                    decimal __credit = 0.0M;
                                    string __accountDebitCode = __glFormat[__loop][_g.d.erp_doc_format_gl._account_code_debit].ToString().Trim();
                                    string __accountCreditCode = __glFormat[__loop][_g.d.erp_doc_format_gl._account_code_credit].ToString().Trim();
                                    string __conditionCase = __glFormat[__loop][_g.d.erp_doc_format_gl._condition_case].ToString().Trim();
                                    string __accountCode = (__accountDebitCode.Length > 0) ? __accountDebitCode : __accountCreditCode;
                                    DataRow[] __findChartOfAccount = this._chatOfAccount.Select("code=\'" + __accountCode + "\'");
                                    string __accountName = "";
                                    string __getAccountDescription = __glFormat[__loop][_g.d.erp_doc_format_gl._account_name].ToString().Trim();
                                    string __code = __icTransRow["cust_code"].ToString();
                                    string __name = __icTransRow["cust_name"].ToString();
                                    __getAccountDescription = __getAccountDescription.Replace("&AP_CODE&", __code);
                                    __getAccountDescription = __getAccountDescription.Replace("&AP_NAME&", __name);
                                    __getAccountDescription = __getAccountDescription.Replace("&AR_CODE&", __code);
                                    __getAccountDescription = __getAccountDescription.Replace("&AR_NAME&", __name);
                                    if (__findChartOfAccount.Length > 0)
                                    {
                                        __accountName = __findChartOfAccount[0][_g.d.gl_chart_of_account._name_1].ToString();
                                    }
                                    else
                                    {
                                        //__accountName = "not found : " + __accountCode;
                                    }
                                    Boolean __isDebit = (__accountDebitCode.Length > 0) ? true : false;
                                    int __actionCode = (int)MyLib._myGlobal._decimalPhase(__glFormat[__loop][_g.d.erp_doc_format_gl._condition_number].ToString());
                                    //
                                    _glDetailStruct __glDetailTemp = new _glDetailStruct();



                                    switch (__transFlagCompare)
                                    {

                                        #region ระบบธนาคาร

                                        #region เงินสดธนาคาร_รายจ่ายอื่น:EPO

                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    DataRow[] __ap = (this._apSupplier.Rows.Count == 0) ? null : this._apSupplier.Select(_g.d.ap_supplier._code + "=\'" + __custCode + "\'");
                                                    int __apType = (__ap == null || __ap.Length == 0) ? 0 : (int)MyLib._myGlobal._decimalPhase(__ap[0][_g.d.ap_supplier._ap_status].ToString());
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // ค่าใช้จ่าย (ไม่รวมภาษีมูลค่าเพิ่ม)
                                                            {
                                                                if (__vat_type == 2)
                                                                {
                                                                    // ภาษีอัตราศูนย์
                                                                    if (__isDebit)
                                                                        __debit = __total_value;
                                                                    else
                                                                        __credit = __total_value;
                                                                }
                                                                else
                                                                {
                                                                    if (__isDebit)
                                                                        __debit = __total_before_vat;
                                                                    else
                                                                        __credit = __total_before_vat;
                                                                }
                                                                //__addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and item_type=0");
                                                                __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            }
                                                            break;
                                                        case 30: // ยอดซื้อบริการ (ไม่รวมภาษีมูลค่าเพิ่ม)
                                                            {
                                                                if (__vat_type == 2)
                                                                {
                                                                    // ภาษีอัตราศูนย์
                                                                    if (__isDebit)
                                                                        __debit = __total_value;
                                                                    else
                                                                        __credit = __total_value;
                                                                }
                                                                else
                                                                {
                                                                    if (__isDebit)
                                                                        __debit = __total_before_vat;
                                                                    else
                                                                        __credit = __total_before_vat;
                                                                }
                                                                __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and item_type=\'1\'");
                                                            }
                                                            break;
                                                        case 2: // ภาษีมูลค่าเพิ่ม
                                                            if (__isDebit)
                                                                __debit = __total_vat_value;
                                                            else
                                                                __credit = __total_vat_value;
                                                            break;
                                                        case 23:  // ยอดภาษีมูลค่าเพิ่ม ยังไม่ถึงกำหนดชำระ (กรณีซื้อบริการเชื่อ)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __vat_next_amount;
                                                                else
                                                                    __credit = __vat_next_amount;
                                                            }
                                                            break;
                                                        case 22:
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __vatBuyNoReturn;
                                                                else
                                                                    __credit = __vatBuyNoReturn;
                                                            }
                                                            break;
                                                        case 3: // ภาษีหัก ณ. ที่จ่าย ภงด.3
                                                            if (__apType == 0)
                                                            {
                                                                // บุคคล
                                                                if (__isDebit)
                                                                    __debit = __tax_at_pay;
                                                                else
                                                                    __credit = __tax_at_pay;
                                                            }
                                                            break;
                                                        case 4: // ภาษีหัก ณ. ที่จ่าย ภงด.53
                                                            if (__apType == 1)
                                                            {
                                                                // นิติบุคคล
                                                                if (__isDebit)
                                                                    __debit = __tax_at_pay;
                                                                else
                                                                    __credit = __tax_at_pay;
                                                            }
                                                            break;
                                                        case 5: // ยอดเงินสดจ่าย
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __cash_amount;
                                                                else
                                                                    __credit = __cash_amount;
                                                            }
                                                            break;
                                                        case 6: // ยอดเช็คจ่าย
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __chq_amount;
                                                                else
                                                                    __credit = __chq_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.เช็คจ่าย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'2\'");
                                                            }
                                                            break;
                                                        case 7: // ยอดเงินโอน
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __tranfer_amount;
                                                                else
                                                                    __credit = __tranfer_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'1\'");
                                                            }
                                                            break;
                                                        case 8: // ยอดเงินสดย่อยจ่าย (บัตรเครดิต)
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __petty_cash_amount;
                                                                else
                                                                    __credit = __petty_cash_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.เงินสดย่อย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'4\'");
                                                            }
                                                            break;
                                                        case 11: // ค่าใช้จ่ายอื่นๆ
                                                            if (__total_expense_other > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_expense_other;
                                                                else
                                                                    __credit = __total_expense_other;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'11\'");

                                                            }
                                                            break;
                                                        case 12: // รายได้อื่นๆ
                                                            if (__total_income_ohter > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_income_ohter;
                                                                else
                                                                    __credit = __total_income_ohter;
                                                                __addDetail = this._accountByDetail(_accountDetailType.รายได้อื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'12\'");

                                                            }
                                                            break;
                                                        case 16: // ส่วนลดรับเงินสด 
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_discount;
                                                                else
                                                                    __credit = __total_discount;
                                                            }
                                                            break;
                                                        case 17: // ส่วนลดรับเงินเชื่อ
                                                            if (__transType == 0 || __transType == 2)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_discount;
                                                                else
                                                                    __credit = __total_discount;
                                                            }
                                                            break;
                                                        case 31:// ส่วนลดชำระเงิน
                                                            if (__isDebit)
                                                                __debit = __discount_payment;
                                                            else
                                                                __credit = __discount_payment;
                                                            break;

                                                        case 14:  // ยอดหักเงินมัดจำจ่าย
                                                            if (__isDebit)
                                                                __debit = __advance_amount;
                                                            else
                                                                __credit = __advance_amount;
                                                            break;
                                                        case 15: // ตัดเงินล่วงหน้า
                                                            if (__isDebit)
                                                                __debit = __deposit_amount;
                                                            else
                                                                __credit = __deposit_amount;
                                                            break;
                                                        case 18: // ยอดเจ้าหนี้
                                                            if (__transType == 0 || __transType == 2)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_amount;
                                                                else
                                                                    __credit = __total_amount;
                                                            }
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        case 92:  // ปัดเศษ +
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__total_income_amount < 0)
                                                                {
                                                                    if (__isDebit)
                                                                        __debit = __total_income_amount * -1;
                                                                    else
                                                                        __credit = __total_income_amount * -1;
                                                                }
                                                            }
                                                            break;
                                                        case 93:  // ปัดเศษ -
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__total_income_amount > 0)
                                                                {
                                                                    //ค่าเป็นลบ (ลงบัญชี เป็นบวก)
                                                                    if (__isDebit)
                                                                        __debit = __total_income_amount;
                                                                    else
                                                                        __credit = __total_income_amount;
                                                                }
                                                            }
                                                            break;
                                                        case 94:  // กำไรจากอัตราแลกเปลี่ยน
                                                            break;
                                                        case 95:  // ขาดทุนจากอัตราแลกเปลี่ยน
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare.ToString() + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;
                                        #endregion

                                        #region เงินสดธนาคาร_รายจ่ายอื่น ลดหนี้ :EPO

                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    DataRow[] __ap = (this._apSupplier.Rows.Count == 0) ? null : this._apSupplier.Select(_g.d.ap_supplier._code + "=\'" + __custCode + "\'");
                                                    int __apType = (__ap == null || __ap.Length == 0) ? 0 : (int)MyLib._myGlobal._decimalPhase(__ap[0][_g.d.ap_supplier._ap_status].ToString());
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // ค่าใช้จ่าย (ไม่รวมภาษีมูลค่าเพิ่ม)
                                                            {
                                                                if (__vat_type == 2)
                                                                {
                                                                    // ภาษีอัตราศูนย์
                                                                    if (__isDebit)
                                                                        __debit = __total_value;
                                                                    else
                                                                        __credit = __total_value;
                                                                }
                                                                else
                                                                {
                                                                    if (__isDebit)
                                                                        __debit = __total_before_vat;
                                                                    else
                                                                        __credit = __total_before_vat;
                                                                }
                                                                //__addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and item_type=0");
                                                                __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            }
                                                            break;
                                                        case 30: // ยอดซื้อบริการ (ไม่รวมภาษีมูลค่าเพิ่ม)
                                                            {
                                                                if (__vat_type == 2)
                                                                {
                                                                    // ภาษีอัตราศูนย์
                                                                    if (__isDebit)
                                                                        __debit = __total_value;
                                                                    else
                                                                        __credit = __total_value;
                                                                }
                                                                else
                                                                {
                                                                    if (__isDebit)
                                                                        __debit = __total_before_vat;
                                                                    else
                                                                        __credit = __total_before_vat;
                                                                }
                                                                __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and item_type=\'1\'");
                                                            }
                                                            break;
                                                        case 2: // ภาษีมูลค่าเพิ่ม
                                                            if (__isDebit)
                                                                __debit = __total_vat_value;
                                                            else
                                                                __credit = __total_vat_value;
                                                            break;
                                                        case 3: // ภาษีหัก ณ. ที่จ่าย ภงด.3
                                                            if (__apType == 0)
                                                            {
                                                                // บุคคล
                                                                if (__isDebit)
                                                                    __debit = __tax_at_pay;
                                                                else
                                                                    __credit = __tax_at_pay;
                                                            }
                                                            break;
                                                        case 4: // ภาษีหัก ณ. ที่จ่าย ภงด.53
                                                            if (__apType == 1)
                                                            {
                                                                // นิติบุคคล
                                                                if (__isDebit)
                                                                    __debit = __tax_at_pay;
                                                                else
                                                                    __credit = __tax_at_pay;
                                                            }
                                                            break;
                                                        case 5: // ยอดเงินสดจ่าย
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __cash_amount;
                                                                else
                                                                    __credit = __cash_amount;
                                                            }
                                                            break;
                                                        case 6: // ยอดเช็คจ่าย
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __chq_amount;
                                                                else
                                                                    __credit = __chq_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.เช็คจ่าย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'2\'");
                                                            }
                                                            break;
                                                        case 7: // ยอดเงินโอน
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __tranfer_amount;
                                                                else
                                                                    __credit = __tranfer_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'1\'");
                                                            }
                                                            break;
                                                        case 8: // ยอดเงินสดย่อยจ่าย (บัตรเครดิต)
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __petty_cash_amount;
                                                                else
                                                                    __credit = __petty_cash_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.เงินสดย่อย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'4\'");
                                                            }
                                                            break;
                                                        case 11: // ค่าใช้จ่ายอื่นๆ
                                                            if (__total_expense_other > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_expense_other;
                                                                else
                                                                    __credit = __total_expense_other;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'11\'");

                                                            }
                                                            break;
                                                        case 12: // รายได้อื่นๆ
                                                            if (__total_income_ohter > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_income_ohter;
                                                                else
                                                                    __credit = __total_income_ohter;
                                                                __addDetail = this._accountByDetail(_accountDetailType.รายได้อื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'12\'");

                                                            }
                                                            break;
                                                        case 16: // ส่วนลดรับเงินสด 
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_discount;
                                                                else
                                                                    __credit = __total_discount;
                                                            }
                                                            break;
                                                        case 17: // ส่วนลดรับเงินเชื่อ
                                                            if (__transType == 0 || __transType == 2)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_discount;
                                                                else
                                                                    __credit = __total_discount;
                                                            }
                                                            break;
                                                        case 31:// ส่วนลดชำระเงิน
                                                            if (__isDebit)
                                                                __debit = __discount_payment;
                                                            else
                                                                __credit = __discount_payment;
                                                            break;

                                                        case 14:  // ยอดหักเงินมัดจำจ่าย
                                                            if (__isDebit)
                                                                __debit = __advance_amount;
                                                            else
                                                                __credit = __advance_amount;
                                                            break;
                                                        case 15: // ตัดเงินล่วงหน้า
                                                            if (__isDebit)
                                                                __debit = __deposit_amount;
                                                            else
                                                                __credit = __deposit_amount;
                                                            break;
                                                        case 18: // ยอดเจ้าหนี้
                                                            if (__transType == 0 || __transType == 2)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_amount;
                                                                else
                                                                    __credit = __total_amount;
                                                            }
                                                            break;
                                                        case 23:  // ยอดภาษีมูลค่าเพิ่ม ยังไม่ถึงกำหนดชำระ (กรณีซื้อบริการเชื่อ)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __vat_next_amount;
                                                                else
                                                                    __credit = __vat_next_amount;
                                                            }
                                                            break;
                                                        case 22:
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __vatBuyNoReturn;
                                                                else
                                                                    __credit = __vatBuyNoReturn;
                                                            }
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        case 92:  // ปัดเศษ +
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__total_income_amount < 0)
                                                                {
                                                                    if (__isDebit)
                                                                        __debit = __total_income_amount * -1;
                                                                    else
                                                                        __credit = __total_income_amount * -1;
                                                                }
                                                            }
                                                            break;
                                                        case 93:  // ปัดเศษ -
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__total_income_amount > 0)
                                                                {
                                                                    //ค่าเป็นลบ (ลงบัญชี เป็นบวก)
                                                                    if (__isDebit)
                                                                        __debit = __total_income_amount;
                                                                    else
                                                                        __credit = __total_income_amount;
                                                                }
                                                            }
                                                            break;
                                                        case 94:  // กำไรจากอัตราแลกเปลี่ยน
                                                            break;
                                                        case 95:  // ขาดทุนจากอัตราแลกเปลี่ยน
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare.ToString() + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;
                                        #endregion

                                        #region เงินสดธนาคาร_รายได้อื่น : OI

                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // ยอดรายได้อื่นๆ (ยอดก่อนภาษีมูลค่าเพิ่ม)
                                                            if (__isDebit)
                                                                __debit = __total_before_vat;
                                                            else
                                                                __credit = __total_before_vat;
                                                            __addDetail = this._accountByDetail(_accountDetailType.รายได้อื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 2: // ภาษีมูลค่าเพิ่ม
                                                            if (__isDebit)
                                                                __debit = __total_vat_value;
                                                            else
                                                                __credit = __total_vat_value;
                                                            break;
                                                        case 3: // ภาษีหัก ณ. ที่จ่าย
                                                            if (__isDebit)
                                                                __debit = __tax_at_pay;
                                                            else
                                                                __credit = __tax_at_pay;
                                                            break;
                                                        case 5: // ยอดรับเงินสด
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __cash_amount;
                                                                else
                                                                    __credit = __cash_amount;
                                                            }
                                                            break;
                                                        case 6: // ยอดเช็ครับ
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __chq_amount;
                                                                else
                                                                    __credit = __chq_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร_เช็ครับล่วงหน้า, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'2\'");
                                                            }
                                                            break;
                                                        case 7: // ยอดเงินโอน
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __tranfer_amount;
                                                                else
                                                                    __credit = __tranfer_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'1\'");
                                                            }
                                                            break;
                                                        case 8: // ยอดเงินสดย่อย
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __petty_cash_amount;
                                                                else
                                                                    __credit = __petty_cash_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.เงินสดย่อย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'4\'");
                                                            }
                                                            break;
                                                        case 9: // บัตรเครดิต+CHARGE
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __credit_card_amount + __credit_card_charge;
                                                                else
                                                                    __credit = __credit_card_amount + __credit_card_charge;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร_บัตรเครดิต, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'3\'");
                                                            }
                                                            break;
                                                        case 10: // ค่าธรรมเนียมบัตร (รายได้)
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __credit_card_charge;
                                                                else
                                                                    __credit = __credit_card_charge;
                                                            }
                                                            break;
                                                        case 11:  // ค่าใช้จ่ายอื่นๆ
                                                            if (__total_income_ohter > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_income_ohter;
                                                                else
                                                                    __credit = __total_income_ohter;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'11\'");

                                                            }
                                                            break;
                                                        case 12: // รายได้อื่นๆ
                                                            if (__total_expense_other > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_expense_other;
                                                                else
                                                                    __credit = __total_expense_other;
                                                                __addDetail = this._accountByDetail(_accountDetailType.รายได้อื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'12\'");

                                                            }
                                                            break;
                                                        case 13: // จ่ายด้วยแต้ม
                                                            if (__isDebit)
                                                                __debit = __total_point;
                                                            else
                                                                __credit = __total_point;
                                                            break;
                                                        case 14:  // ยอดหักเงินมัดจำ
                                                            if (__isDebit)
                                                                __debit = __advance_amount;
                                                            else
                                                                __credit = __advance_amount;
                                                            break;
                                                        case 15: // ยอดหักเงินล่วงหน้า
                                                            if (__isDebit)
                                                                __debit = __deposit_amount;
                                                            else
                                                                __credit = __deposit_amount;
                                                            break;
                                                        case 17: // จ่ายด้วยคูปอง
                                                            if (__isDebit)
                                                                __debit = __total_coupon;
                                                            else
                                                                __credit = __total_coupon;
                                                            break;
                                                        case 18: // ยอดลูกหนี้ (กรณีขายเชื่อ)
                                                            if (__transType == 0 || __transType == 2)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_amount;
                                                                else
                                                                    __credit = __total_amount;
                                                            }
                                                            break;
                                                        case 19: // มูลค่าส่วนลด
                                                            if (__isDebit)
                                                                __debit = __total_discount;
                                                            else
                                                                __credit = __total_discount;
                                                            break;
                                                        case 31:// ส่วนลดชำระเงิน
                                                            if (__isDebit)
                                                                __debit = __discount_payment;
                                                            else
                                                                __credit = __discount_payment;
                                                            break;

                                                        case 23:  // ยอดภาษีมูลค่าเพิ่ม ยังไม่ถึงกำหนดชำระ
                                                            {
                                                                /*if (__isDebit)
                                                                    __debit = __total_vat_value;
                                                                else
                                                                    __credit = __total_vat_value
                                                                */
                                                                if (__isDebit)
                                                                    __debit = __vat_next_amount;
                                                                else
                                                                    __credit = __vat_next_amount;
                                                            }
                                                            break;
                                                        case 22:
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __vatBuyNoReturn;
                                                                else
                                                                    __credit = __vatBuyNoReturn;
                                                            }
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        case 92:  // ปัดเศษ +
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__total_income_amount > 0)
                                                                {
                                                                    if (__isDebit)
                                                                        __debit = __total_income_amount;
                                                                    else
                                                                        __credit = __total_income_amount;
                                                                }
                                                            }
                                                            break;
                                                        case 93:  // ปัดเศษ -
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__total_income_amount < 0)
                                                                {
                                                                    if (__isDebit)
                                                                        __debit = __total_income_amount * -1;
                                                                    else
                                                                        __credit = __total_income_amount * -1;
                                                                }
                                                            }
                                                            break;
                                                        case 94:  // กำไรจากอัตราแลกเปลี่ยน
                                                            break;
                                                        case 95:  // ขาดทุนจากอัตราแลกเปลี่ยน
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare.ToString() + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #endregion

                                        #region ระบบเงินสดย่อย

                                        #region เบิกเงินสดย่อย : PCD
                                        case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // เงินสดย่อยรับ
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.รายวันเงินสดย่อย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=0");
                                                            break;
                                                        case 5: // ยอดจ่ายเงินสด
                                                            if (__isDebit)
                                                                __debit = __cash_amount;
                                                            else
                                                                __credit = __cash_amount;
                                                            break;
                                                        case 6: // เช็คจ่าย ธนาคารที่ถอน (เลขที่บัญชีรายตัว)
                                                            if (__chq_amount != 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __chq_amount;
                                                                else
                                                                    __credit = __chq_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.เช็คจ่าย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'2\'");
                                                            }
                                                            break;
                                                        case 7: // เงินโอน
                                                            if (__isDebit)
                                                                __debit = __tranfer_amount;
                                                            else
                                                                __credit = __tranfer_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'1\'");
                                                            break;
                                                        case 9: // จ่ายเงินสดย่อย
                                                            if (__isDebit)
                                                                __debit = __petty_cash_amount;
                                                            else
                                                                __credit = __petty_cash_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.เงินสดย่อย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'4\'");
                                                            break;
                                                        case 11: // ค่าใช้จ่ายอื่นๆ
                                                            if (__total_expense_other > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_expense_other;
                                                                else
                                                                    __credit = __total_expense_other;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'11\'");

                                                            }
                                                            break;
                                                        case 12: // รายได้อื่นๆ
                                                            if (__total_income_ohter > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_income_ohter;
                                                                else
                                                                    __credit = __total_income_ohter;
                                                                __addDetail = this._accountByDetail(_accountDetailType.รายได้อื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'12\'");

                                                            }
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        case 92:
                                                            if (__total_income_amount < 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_income_amount * -1;
                                                                else
                                                                    __credit = __total_income_amount * 1;
                                                            }
                                                            break;
                                                        case 93:
                                                            if (__total_income_amount > 0)
                                                            {
                                                                if (__isDebit)
                                                                    //ค่าเป็นลบ (ลงบํญชี เป็นบวก)
                                                                    __debit = __total_income_amount;
                                                                else
                                                                    __credit = __total_income_amount;
                                                            }
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare.ToString() + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;
                                        #endregion

                                        #region เงินสดย่อย รับคืน
                                        case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // เงินสดย่อยรับ
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.รายวันเงินสดย่อย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=0");
                                                            break;
                                                        case 5: // ยอดจ่ายเงินสด
                                                            if (__isDebit)
                                                                __debit = __cash_amount;
                                                            else
                                                                __credit = __cash_amount;
                                                            break;
                                                        case 6: // เช็คจ่าย ธนาคารที่ถอน (เลขที่บัญชีรายตัว)
                                                            if (__chq_amount != 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __chq_amount;
                                                                else
                                                                    __credit = __chq_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'2\'");
                                                            }
                                                            break;
                                                        case 7: // เงินโอน
                                                            if (__isDebit)
                                                                __debit = __tranfer_amount;
                                                            else
                                                                __credit = __tranfer_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'1\'");
                                                            break;
                                                        case 9: // จ่ายเงินสดย่อย
                                                            if (__isDebit)
                                                                __debit = __petty_cash_amount;
                                                            else
                                                                __credit = __petty_cash_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.เงินสดย่อย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'4\'");
                                                            break;
                                                        case 11:  // ค่าใช้จ่ายอื่นๆ
                                                            if (__total_income_ohter > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_income_ohter;
                                                                else
                                                                    __credit = __total_income_ohter;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'11\'");

                                                            }
                                                            break;
                                                        case 12: // รายได้อื่นๆ
                                                            if (__total_expense_other > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_expense_other;
                                                                else
                                                                    __credit = __total_expense_other;
                                                                __addDetail = this._accountByDetail(_accountDetailType.รายได้อื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'12\'");

                                                            }
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        case 92:
                                                            if (__total_income_amount < 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_income_amount * -1;
                                                                else
                                                                    __credit = __total_income_amount * 1;
                                                            }
                                                            break;
                                                        case 93:
                                                            if (__total_income_amount > 0)
                                                            {
                                                                if (__isDebit)
                                                                    //ค่าเป็นลบ (ลงบํญชี เป็นบวก)
                                                                    __debit = __total_income_amount;
                                                                else
                                                                    __credit = __total_income_amount;
                                                            }
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare.ToString() + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #endregion

                                        #region ธนาคาร

                                        #region เงินสดธนาคาร_ฝากเงิน : DM

                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // ยอดเงินสด
                                                            if (__isDebit)
                                                                __debit = __cash_amount;
                                                            else
                                                                __credit = __cash_amount;
                                                            break;
                                                        case 2: // เงินสดย่อย
                                                            if (__isDebit)
                                                                __debit = __petty_cash_amount;
                                                            else
                                                                __credit = __petty_cash_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.เงินสดย่อย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'4\'");
                                                            break;
                                                        case 3: // เงินโอน
                                                            if (__isDebit)
                                                                __debit = __tranfer_amount;
                                                            else
                                                                __credit = __tranfer_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'1\'");
                                                            break;
                                                        case 6: // เช็คธนาคารที่ถอน (เลขที่บัญชีรายตัว)
                                                            if (__chq_amount != 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __chq_amount;
                                                                else
                                                                    __credit = __chq_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'2\'");
                                                            }
                                                            break;
                                                        case 10: // ธนาคารที่ถอน (เลขที่บัญชีรายตัว)
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=0");
                                                            break;
                                                        case 11: // ค่าใช้จ่ายอื่นๆ
                                                            if (__total_expense_other > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_expense_other;
                                                                else
                                                                    __credit = __total_expense_other;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'11\'");

                                                            }
                                                            break;
                                                        case 12: // รายได้อื่นๆ
                                                            if (__total_income_ohter > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_income_ohter;
                                                                else
                                                                    __credit = __total_income_ohter;
                                                                __addDetail = this._accountByDetail(_accountDetailType.รายได้อื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'12\'");

                                                            }
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        case 92:
                                                            if (__total_income_amount < 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_income_amount * -1;
                                                                else
                                                                    __credit = __total_income_amount * 1;
                                                            }
                                                            break;
                                                        case 93:
                                                            if (__total_income_amount > 0)
                                                            {
                                                                if (__isDebit)
                                                                    //ค่าเป็นลบ (ลงบํญชี เป็นบวก)
                                                                    __debit = __total_income_amount;
                                                                else
                                                                    __credit = __total_income_amount;
                                                            }
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare.ToString() + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #region เงินสดธนาคาร_ถอนเงิน : WM

                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // ยอดเงินสด
                                                            if (__isDebit)
                                                                __debit = __cash_amount;
                                                            else
                                                                __credit = __cash_amount;
                                                            break;
                                                        case 2: // เงินสดย่อย
                                                            if (__isDebit)
                                                                __debit = __petty_cash_amount;
                                                            else
                                                                __credit = __petty_cash_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.เงินสดย่อย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'4\'");
                                                            break;
                                                        case 3: // เงินโอน
                                                            if (__isDebit)
                                                                __debit = __tranfer_amount;
                                                            else
                                                                __credit = __tranfer_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'1\'");
                                                            break;
                                                        case 10: // ธนาคารที่ถอน (เลขที่บัญชีรายตัว)
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=0");
                                                            break;
                                                        case 11:  // ค่าใช้จ่ายอื่นๆ
                                                            if (__total_income_ohter > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_income_ohter;
                                                                else
                                                                    __credit = __total_income_ohter;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'11\'");

                                                            }
                                                            break;
                                                        case 12: // รายได้อื่นๆ
                                                            if (__total_expense_other > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_expense_other;
                                                                else
                                                                    __credit = __total_expense_other;
                                                                __addDetail = this._accountByDetail(_accountDetailType.รายได้อื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'12\'");

                                                            }
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        case 92:
                                                            if (__total_income_amount > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_income_amount;
                                                                else
                                                                    __credit = __total_income_amount;
                                                            }
                                                            break;

                                                        case 93:
                                                            if (__total_income_amount < 0)
                                                            {
                                                                if (__isDebit)
                                                                    //ค่าเป็นลบ (ลงบํญชี เป็นบวก)
                                                                    __debit = -1 * __total_income_amount;
                                                                else
                                                                    __credit = -1 * __total_income_amount;
                                                            }
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare.ToString() + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #region เงินสดธนาคาร_โอนเงินระหว่างธนาคาร : TM

                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // โอนเข้า
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร_โอนเงินระหว่างธนาคาร_เข้า, this._icTransDetail, __docNo, __accountCode, _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินเข้าธนาคาร, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 2: // โอนออก
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร_โอนเงินระหว่างธนาคาร_ออก, this._icTransDetail, __docNo, __accountCode, _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 3: // ค่าธรรมเนียม (รวมทุกบรรทัด)
                                                            decimal __amount = 0.0M;
                                                            string __query9 = _g.d.ic_trans_detail._trans_flag + " = " + _g.g._transFlagGlobal._transFlag(__transFlagCompare).ToString() + " and " + _g.d.ic_trans_detail._doc_no + " = \'" + __docNo + "\'";
                                                            DataRow[] __rowDetail = this._icTransDetail.Rows.Count == 0 ? null : this._icTransDetail.Select(__query9);
                                                            if (__rowDetail != null && __rowDetail.Length > 0)
                                                            {
                                                                for (int __detail = 0; __detail < __rowDetail.Length; __detail++)
                                                                {
                                                                    __amount = __amount + MyLib._myGlobal._decimalPhase(__rowDetail[__detail]["fee_amount"].ToString());
                                                                }
                                                            }

                                                            if (__isDebit)
                                                                __debit = __amount;
                                                            else
                                                                __credit = __amount;
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare.ToString() + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #endregion

                                        #region เช็ครับ

                                        #region เงินสดธนาคาร_เช็ครับ_ผ่าน : CHP

                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // ธนาคาร (สมุดเงินฝาก)
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 2: // ค่าธรรมเนียมธนาคาร
                                                            if (__isDebit)
                                                                __debit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_of_cost);
                                                            else
                                                                __credit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_of_cost);
                                                            break;
                                                        case 3: // เช็ครับล่วงหน้า
                                                            if (__isDebit)
                                                                __debit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._price);
                                                            else
                                                                __credit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._price);
                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร_เช็ครับล่วงหน้า, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 11: // ค่าใช้จ่ายอื่นๆ
                                                            break;
                                                        case 12: // รายได้อื่นๆ
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare.ToString() + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #region เงินสดธนาคาร_เช็ครับ_คืน

                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {

                                                    switch (__actionCode)
                                                    {
                                                        case 1: // เช็คคืน
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.เช็ครับ, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 2: // ค่าธรรมเนียมธนาคาร
                                                            if (__isDebit)
                                                                __debit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_of_cost);
                                                            else
                                                                __credit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_of_cost);
                                                            break;
                                                        case 3: // ธนาคาร
                                                            if (__isDebit)
                                                                __debit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._price);
                                                            else
                                                                __credit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._price);
                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร_เช็ครับคืน, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #region เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค

                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {

                                                    switch (__actionCode)
                                                    {
                                                        case 1: // รับเงิน (เงินสด)
                                                            if (__isDebit)
                                                                __debit = __cash_amount;
                                                            else
                                                                __credit = __cash_amount;
                                                            //__addDetail = this._accountByDetail(_accountDetailType.เช็ครับ, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 2: // รับเงิน (เช็ค)
                                                            if (__isDebit)
                                                                __debit = __chq_amount;
                                                            else
                                                                __credit = __chq_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร_เช็ครับล่วงหน้า, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'2\'");
                                                            break;
                                                        case 3: // รับเงิน (โอน)
                                                            if (__isDebit)
                                                                __debit = __tranfer_amount;
                                                            else
                                                                __credit = __tranfer_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'1\'");
                                                            break;
                                                        case 4: // รับเงิน (เงินสดย่อย)
                                                            if (__isDebit)
                                                                __debit = __petty_cash_amount;
                                                            else
                                                                __credit = __petty_cash_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.เงินสดย่อย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'4\'");
                                                            break;
                                                        case 5:
                                                            // กรณีเปลี่ยนเช็คจากเช็คในมือ
                                                            if (__isDebit)
                                                                __debit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_amount, "and item_type = 0 and last_flag <> " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน));
                                                            else
                                                                __credit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_amount, "and item_type = 0  and last_flag <> " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน));
                                                            //__addDetail = this._accountByDetail(_accountDetailType.เช็ครับ, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and last_flag <>" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน));

                                                            break;
                                                        case 6:
                                                            // เปลี่ยนจากเช็คคืน
                                                            if (__isDebit)
                                                                __debit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_amount, "and item_type = 0 and last_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน));
                                                            else
                                                                __credit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_amount, "and item_type = 0 and last_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน));
                                                            //__addDetail = this._accountByDetail(_accountDetailType.เช็ครับ, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and last_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน));

                                                            break;
                                                        case 11:  // ค่าใช้จ่ายอื่นๆ
                                                            if (__total_income_ohter > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_income_ohter;
                                                                else
                                                                    __credit = __total_income_ohter;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'11\'");

                                                            }
                                                            break;
                                                        case 12: // รายได้อื่นๆ
                                                            if (__total_expense_other > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_expense_other;
                                                                else
                                                                    __credit = __total_expense_other;
                                                                __addDetail = this._accountByDetail(_accountDetailType.รายได้อื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'12\'");

                                                            }
                                                            break;
                                                        case 15: // ตัดเงินล่วงหน้า
                                                            if (__isDebit)
                                                                __debit = __deposit_amount;
                                                            else
                                                                __credit = __deposit_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.เงินล่่วงหน้ารับ, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type='5'");
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;

                                                            if (__total_income_amount > 0)
                                                            {
                                                                __debit = __total_income_amount;
                                                            }
                                                            else
                                                            {
                                                                __credit = __total_income_amount * -1;
                                                            }
                                                            /*if (__isDebit)
                                                                __debit = __total_income_amount;
                                                            else
                                                                __credit = __total_income_amount;*/
                                                            break;
                                                        case 92:  // ปัดเศษ +
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__total_income_amount < 0)
                                                                {
                                                                    if (__isDebit)
                                                                        __debit = __total_income_amount * -1;
                                                                    else
                                                                        __credit = __total_income_amount * -1;
                                                                }
                                                            }
                                                            break;
                                                        case 93:  // ปัดเศษ -
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__total_income_amount > 0)
                                                                {
                                                                    //ค่าเป็นลบ (ลงบัญชี เป็นบวก)
                                                                    if (__isDebit)
                                                                        __debit = __total_income_amount;
                                                                    else
                                                                        __credit = __total_income_amount;
                                                                }
                                                            }
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #region เงินสดธนาคาร_เช็ครับ_ยกเลิก

                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // รับเงิน (เงินสด)
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount;
                                                            break;
                                                        case 2:
                                                            // กรณีเปลี่ยนเช็คจากเช็คในมือ
                                                            if (__isDebit)
                                                                __debit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_amount, "and item_type = 0 and last_flag <> " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน));
                                                            else
                                                                __credit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_amount, "and item_type = 0  and last_flag <> " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน));

                                                            break;
                                                        case 3:
                                                            // เปลี่ยนจากเช็คคืน
                                                            if (__isDebit)
                                                                __debit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_amount, "and item_type = 0 and last_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน));
                                                            else
                                                                __credit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_amount, "and item_type = 0 and last_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน));

                                                            break;

                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #region เงินสดธนาคาร_เช็ครับ_ฝาก : CHD

                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // เช็คนำฝาก
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร_เช็ครับล่วงหน้า, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 2: // เช็ครับล่วงหน้า
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร_เช็ครับล่วงหน้า, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #endregion

                                        #region เช็คจ่าย

                                        #region เงินสดธนาคาร_เช็คจ่าย_ผ่าน : CPP

                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // ธนาคาร (สมุดเงินฝาก)
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 2: // ค่าธรรมเนียมธนาคาร
                                                            if (__isDebit)
                                                                __debit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_of_cost);
                                                            else
                                                                __credit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_of_cost);
                                                            break;
                                                        case 3: // เช็คจ่ายล่วงหน้า
                                                            if (__isDebit)
                                                                __debit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._price);
                                                            else
                                                                __credit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._price);
                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร_เช็คจ่ายล่วงหน้า, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 11: // ค่าใช้จ่ายอื่นๆ
                                                            break;
                                                        case 12: // รายได้อื่นๆ
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare.ToString() + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #region เงินสดธนาคาร_เช็คจ่าย_คืน : CPR

                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // ธนาคาร (สมุดเงินฝาก)
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 2: // ค่าธรรมเนียมธนาคาร
                                                            if (__isDebit)
                                                                __debit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_of_cost);
                                                            else
                                                                __credit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_of_cost);
                                                            break;
                                                        case 3: // เช็คจ่ายล่วงหน้า
                                                            if (__isDebit)
                                                                __debit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._price);
                                                            else
                                                                __credit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._price);
                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร_เช็คจ่ายล่วงหน้า, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare.ToString() + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #region เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค

                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {

                                                    switch (__actionCode)
                                                    {
                                                        case 1:
                                                            // กรณีเปลี่ยนเช็คจากเช็คในมือ
                                                            if (__isDebit)
                                                                __debit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_amount, "and item_type = 0 and last_flag <> " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน));
                                                            else
                                                                __credit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_amount, "and item_type = 0  and last_flag <> " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน));

                                                            break;
                                                        case 2:
                                                            // เปลี่ยนจากเช็คคืน
                                                            if (__isDebit)
                                                                __debit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_amount, "and item_type = 0 and last_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน));
                                                            else
                                                                __credit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_amount, "and item_type = 0 and last_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน));

                                                            break;
                                                        case 3: // รับเงิน (เงินสด)
                                                            if (__isDebit)
                                                                __debit = __cash_amount;
                                                            else
                                                                __credit = __cash_amount;
                                                            break;
                                                        case 4: // รับเงิน (เช็ค)
                                                            if (__isDebit)
                                                                __debit = __chq_amount;
                                                            else
                                                                __credit = __chq_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.เช็คจ่าย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'2\'");
                                                            break;
                                                        case 5: // รับเงิน (โอน)
                                                            if (__isDebit)
                                                                __debit = __tranfer_amount;
                                                            else
                                                                __credit = __tranfer_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'1\'");
                                                            break;
                                                        case 6: // รับเงิน (เงินสดย่อย)
                                                            if (__isDebit)
                                                                __debit = __petty_cash_amount;
                                                            else
                                                                __credit = __petty_cash_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.เงินสดย่อย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'4\'");
                                                            break;
                                                        case 11: // ค่าใช้จ่ายอื่นๆ
                                                            if (__total_expense_other > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_expense_other;
                                                                else
                                                                    __credit = __total_expense_other;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'11\'");

                                                            }
                                                            break;
                                                        case 12: // รายได้อื่นๆ
                                                            if (__total_income_ohter > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_income_ohter;
                                                                else
                                                                    __credit = __total_income_ohter;
                                                                __addDetail = this._accountByDetail(_accountDetailType.รายได้อื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'12\'");

                                                            }
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #region เงินสดธนาคาร_เช็คจ่าย_ยกเลิก

                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    switch (__actionCode)
                                                    {
                                                        case 1:
                                                            // กรณีเปลี่ยนเช็คจากเช็คในมือ
                                                            if (__isDebit)
                                                                __debit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_amount, "and item_type = 0 and last_flag <> " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน));
                                                            else
                                                                __credit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_amount, "and item_type = 0  and last_flag <> " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน));

                                                            break;
                                                        case 2:
                                                            // เปลี่ยนจากเช็คคืน
                                                            if (__isDebit)
                                                                __debit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_amount, "and item_type = 0 and last_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน));
                                                            else
                                                                __credit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_amount, "and item_type = 0 and last_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน));

                                                            break;
                                                        case 3: // รับเงิน (เงินสด)
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount;
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #endregion

                                        #region บัตรเครดิต

                                        #region เงินสดธนาคาร_บัตรเครดิต_ผ่าน : CRD

                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // ธนาคาร (สมุดเงินฝาก)
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 2: // ค่าธรรมเนียมธนาคาร
                                                            if (__isDebit)
                                                                __debit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_of_cost);
                                                            else
                                                                __credit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_of_cost);
                                                            break;
                                                        case 4: // บัตรเครดิต
                                                            if (__isDebit)
                                                                __debit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._price);
                                                            else
                                                                __credit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._price);
                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร_บัตรเครดิต, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 11: // ค่าใช้จ่ายอื่นๆ
                                                            break;
                                                        case 12: // รายได้อื่นๆ
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare.ToString() + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #endregion

                                        #region ระบบลูกหนี้

                                        #region ลูกหนี้_รับชำระหนี้:EE

                                        case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                                            {
                                                if (__conditionCase.Length == 0)
                                                {
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // ลูกหนี้การค้า
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount;
                                                            break;
                                                        case 2: // ภาษีมูลค่าเพิ่ม
                                                            if (__isDebit)
                                                                __debit = __total_vat_value;
                                                            else
                                                                __credit = __total_vat_value;
                                                            break;
                                                        case 3: // ภาษีหัก ณ. ที่จ่าย 
                                                            if (__isDebit)
                                                                __debit = __tax_at_pay;
                                                            else
                                                                __credit = __tax_at_pay;
                                                            break;
                                                        case 5: // ยอดเงินสดจ่าย
                                                            if (__isDebit)
                                                                __debit = __cash_amount;
                                                            else
                                                                __credit = __cash_amount;
                                                            break;
                                                        case 6: // ยอดเช็ครับ
                                                            if (__isDebit)
                                                                __debit = __chq_amount;
                                                            else
                                                                __credit = __chq_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร_เช็ครับล่วงหน้า, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'2\'");
                                                            break;
                                                        case 7: // ยอดเงินโอน
                                                            if (__isDebit)
                                                                __debit = __tranfer_amount;
                                                            else
                                                                __credit = __tranfer_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'1\'");
                                                            break;
                                                        case 8: // ยอดเงินสดย่อยจ่าย (บัตรเครดิต)
                                                            if (__isDebit)
                                                                __debit = __petty_cash_amount;
                                                            else
                                                                __credit = __petty_cash_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.เงินสดย่อย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type='4'");
                                                            break;
                                                        case 9: // บัตรเครดิต+CHARGE
                                                            if (__isDebit)
                                                                __debit = __credit_card_amount + __credit_card_charge;
                                                            else
                                                                __credit = __credit_card_amount + __credit_card_charge;

                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร_บัตรเครดิต, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'3\'");

                                                            break;
                                                        case 10: // ค่าธรรมเนียมบัตร (รายได้)
                                                            if (__isDebit)
                                                                __debit = __credit_card_charge;
                                                            else
                                                                __credit = __credit_card_charge;
                                                            break;
                                                        case 14:  // ยอดหักเงินมัดจำจ่าย
                                                            if (__isDebit)
                                                                __debit = __advance_amount;
                                                            else
                                                                __credit = __advance_amount;
                                                            break;
                                                        case 15: // ตัดเงินล่วงหน้า
                                                            if (__isDebit)
                                                                __debit = __deposit_amount;
                                                            else
                                                                __credit = __deposit_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.เงินล่่วงหน้ารับ, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type='5'");
                                                            break;
                                                        case 19: // มูลค่าส่วนลด
                                                            if (__isDebit)
                                                                __debit = __total_discount;
                                                            else
                                                                __credit = __total_discount;
                                                            break;
                                                        case 31:// ส่วนลดชำระเงิน
                                                            if (__isDebit)
                                                                __debit = __discount_payment;
                                                            else
                                                                __credit = __discount_payment;
                                                            break;

                                                        case 23:  // ยอดภาษีมูลค่าเพิ่ม ยังไม่ถึงกำหนดชำระ
                                                            if (__isDebit)
                                                                __debit = __total_vat_value;
                                                            else
                                                                __credit = __total_vat_value;
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        case 92:  // ปัดเศษ +
                                                            if (__total_income_amount > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_income_amount;
                                                                else
                                                                    __credit = __total_income_amount;
                                                            }
                                                            break;
                                                        case 93:  // ปัดเศษ -
                                                            if (__total_income_amount < 0)
                                                            {
                                                                //ค่าเป็นลบ (ลงบัญชี เป็นบวก)
                                                                if (__isDebit)
                                                                    __debit = __total_income_amount * -1;
                                                                else
                                                                    __credit = __total_income_amount * -1;
                                                            }
                                                            break;
                                                        case 94:  // กำไรจากอัตราแลกเปลี่ยน
                                                            break;
                                                        case 95:  // ขาดทุนจากอัตราแลกเปลี่ยน
                                                            break;
                                                        case 11:  // ค่าใช้จ่ายอื่นๆ
                                                            if (__total_income_ohter > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_income_ohter;
                                                                else
                                                                    __credit = __total_income_ohter;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'11\' ");

                                                            }
                                                            break;
                                                        case 12: // รายได้อื่นๆ
                                                            if (__total_expense_other > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_expense_other;
                                                                else
                                                                    __credit = __total_expense_other;
                                                                __addDetail = this._accountByDetail(_accountDetailType.รายได้อื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'12\' ");

                                                            }
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare.ToString() + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #region ลูกหนี้_ตั้งหนี้อื่น : AOB

                                        case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    // ดึงรายละเอียดลูกหนี้
                                                    DataRow[] __ar = (this._arCustomer.Rows.Count == 0) ? null : this._arCustomer.Select(_g.d.ar_customer._code + "=\'" + __custCode + "\'");
                                                    // ประเภทเจ้าหนี้ (0=บุคคลธรรมดา,1=นิติบุคคล)
                                                    int __arType = (__ar == null || __ar.Length == 0) ? 0 : (int)MyLib._myGlobal._decimalPhase(__ar[0][_g.d.ar_customer._ar_status].ToString());
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // ยอดตั้งหนี้ไม่รวมภาษีมูลค่าเพิ่ม
                                                            if (__vat_type == 2)
                                                            {
                                                                // ภาษีอัตราศูนย์
                                                                if (__isDebit)
                                                                    __debit = __total_value;
                                                                else
                                                                    __credit = __total_value;
                                                            }
                                                            else
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_before_vat;
                                                                else
                                                                    __credit = __total_before_vat;
                                                            }
                                                            __addDetail = this._accountByDetail(_accountDetailType.รายได้อื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 2: // ภาษีมูลค่าเพิ่ม
                                                            if (__isDebit)
                                                                __debit = __total_vat_value;
                                                            else
                                                                __credit = __total_vat_value;
                                                            break;
                                                        case 13: // ยอดส่วนลด
                                                            if (__isDebit)
                                                                __debit = __total_discount;
                                                            else
                                                                __credit = __total_discount;
                                                            break;
                                                        case 14:  // ยอดหักเงินมัดจำ
                                                            if (__isDebit)
                                                                __debit = __advance_amount;
                                                            else
                                                                __credit = __advance_amount;
                                                            break;
                                                        case 18: // ยอดลูกหนี้
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount;
                                                            break;
                                                        case 19: // มูลค่าส่วนลด
                                                            if (__isDebit)
                                                                __debit = __total_discount;
                                                            else
                                                                __credit = __total_discount;
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        case 94:  // กำไรจากอัตราแลกเปลี่ยน
                                                            break;
                                                        case 95:  // ขาดทุนจากอัตราแลกเปลี่ยน
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #region ลูกหนี้_เพิ่มหนี้อื่น : ADO

                                        case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    // ดึงรายละเอียดลูกหนี้
                                                    DataRow[] __ar = (this._arCustomer.Rows.Count == 0) ? null : this._arCustomer.Select(_g.d.ar_customer._code + "=\'" + __custCode + "\'");
                                                    // ประเภทเจ้าหนี้ (0=บุคคลธรรมดา,1=นิติบุคคล)
                                                    int __arType = (__ar == null || __ar.Length == 0) ? 0 : (int)MyLib._myGlobal._decimalPhase(__ar[0][_g.d.ar_customer._ar_status].ToString());
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // ยอดตั้งหนี้ไม่รวมภาษีมูลค่าเพิ่ม
                                                            if (__vat_type == 2)
                                                            {
                                                                // ภาษีอัตราศูนย์
                                                                if (__isDebit)
                                                                    __debit = __total_value;
                                                                else
                                                                    __credit = __total_value;
                                                            }
                                                            else
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_before_vat;
                                                                else
                                                                    __credit = __total_before_vat;
                                                            }
                                                            __addDetail = this._accountByDetail(_accountDetailType.รายได้อื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 2: // ภาษีมูลค่าเพิ่ม
                                                            if (__isDebit)
                                                                __debit = __total_vat_value;
                                                            else
                                                                __credit = __total_vat_value;
                                                            break;
                                                        case 13: // ยอดส่วนลด
                                                            if (__isDebit)
                                                                __debit = __total_discount;
                                                            else
                                                                __credit = __total_discount;
                                                            break;
                                                        case 14:  // ยอดหักเงินมัดจำ
                                                            if (__isDebit)
                                                                __debit = __advance_amount;
                                                            else
                                                                __credit = __advance_amount;
                                                            break;
                                                        case 18: // ยอดลูกหนี้
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount;
                                                            break;
                                                        case 19: // มูลค่าส่วนลด
                                                            if (__isDebit)
                                                                __debit = __total_discount;
                                                            else
                                                                __credit = __total_discount;
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        case 94:  // กำไรจากอัตราแลกเปลี่ยน
                                                            break;
                                                        case 95:  // ขาดทุนจากอัตราแลกเปลี่ยน
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #region ลูกหนี้_ลดหนี้อื่น : ACO
                                        case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    // ดึงรายละเอียดลูกหนี้
                                                    DataRow[] __ar = (this._arCustomer.Rows.Count == 0) ? null : this._arCustomer.Select(_g.d.ar_customer._code + "=\'" + __custCode + "\'");
                                                    // ประเภทเจ้าหนี้ (0=บุคคลธรรมดา,1=นิติบุคคล)
                                                    int __arType = (__ar == null || __ar.Length == 0) ? 0 : (int)MyLib._myGlobal._decimalPhase(__ar[0][_g.d.ar_customer._ar_status].ToString());
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // ยอดตั้งหนี้ไม่รวมภาษีมูลค่าเพิ่ม
                                                            if (__vat_type == 2)
                                                            {
                                                                // ภาษีอัตราศูนย์
                                                                if (__isDebit)
                                                                    __debit = __total_value;
                                                                else
                                                                    __credit = __total_value;
                                                            }
                                                            else
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_before_vat;
                                                                else
                                                                    __credit = __total_before_vat;
                                                            }
                                                            __addDetail = this._accountByDetail(_accountDetailType.รายได้อื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 2: // ภาษีมูลค่าเพิ่ม
                                                            if (__isDebit)
                                                                __debit = __total_vat_value;
                                                            else
                                                                __credit = __total_vat_value;
                                                            break;
                                                        case 13: // ยอดส่วนลด
                                                            if (__isDebit)
                                                                __debit = __total_discount;
                                                            else
                                                                __credit = __total_discount;
                                                            break;
                                                        case 14:  // ยอดหักเงินมัดจำ
                                                            if (__isDebit)
                                                                __debit = __advance_amount;
                                                            else
                                                                __credit = __advance_amount;
                                                            break;
                                                        case 18: // ยอดลูกหนี้
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount;
                                                            break;
                                                        case 19: // มูลค่าส่วนลด
                                                            if (__isDebit)
                                                                __debit = __total_discount;
                                                            else
                                                                __credit = __total_discount;
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        case 94:  // กำไรจากอัตราแลกเปลี่ยน
                                                            break;
                                                        case 95:  // ขาดทุนจากอัตราแลกเปลี่ยน
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;
                                        #endregion

                                        #endregion

                                        #region ระบบเจ้าหนี้

                                        #region เจ้าหนี้_จ่ายชำระหนี้:DE

                                        case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                                            {
                                                if (__conditionCase.Length == 0)
                                                {
                                                    DataRow[] __ap = (this._apSupplier.Rows.Count == 0) ? null : this._apSupplier.Select(_g.d.ap_supplier._code + "=\'" + __custCode + "\'");
                                                    int __apType = (__ap == null || __ap.Length == 0) ? 0 : (int)MyLib._myGlobal._decimalPhase(__ap[0][_g.d.ap_supplier._ap_status].ToString());
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // ยอดเจ้าหนี้การค้า
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount;
                                                            break;
                                                        case 2: // ภาษีมูลค่าเพิ่ม
                                                            if (__isDebit)
                                                                __debit = __total_vat_value;
                                                            else
                                                                __credit = __total_vat_value;
                                                            break;
                                                        case 3: // ภาษีหัก ณ. ที่จ่าย ภงด.3
                                                            if (__apType == 0)
                                                            {
                                                                // บุคคล
                                                                if (__isDebit)
                                                                    __debit = __tax_at_pay;
                                                                else
                                                                    __credit = __tax_at_pay;
                                                            }
                                                            break;
                                                        case 4: // ภาษีหัก ณ. ที่จ่าย ภงด.53
                                                            if (__apType == 1)
                                                            {
                                                                // นิติบุคคล
                                                                if (__isDebit)
                                                                    __debit = __tax_at_pay;
                                                                else
                                                                    __credit = __tax_at_pay;
                                                            }
                                                            break;
                                                        case 5: // ยอดเงินสดจ่าย
                                                            if (__isDebit)
                                                                __debit = __cash_amount;
                                                            else
                                                                __credit = __cash_amount;
                                                            break;
                                                        case 6: // ยอดเช็คจ่าย
                                                            if (__isDebit)
                                                                __debit = __chq_amount;
                                                            else
                                                                __credit = __chq_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.เช็คจ่าย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'2\'");
                                                            break;
                                                        case 7: // ยอดเงินโอน
                                                            if (__isDebit)
                                                                __debit = __tranfer_amount;
                                                            else
                                                                __credit = __tranfer_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'1\'");
                                                            break;
                                                        case 8: // ยอดเงินสดย่อยจ่าย (บัตรเครดิต)
                                                            if (__isDebit)
                                                                __debit = __petty_cash_amount;
                                                            else
                                                                __credit = __petty_cash_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.เงินสดย่อย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'4\'");
                                                            break;
                                                        case 11: // ค่าใช้จ่ายอื่นๆ
                                                            if (__total_expense_other > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_expense_other;
                                                                else
                                                                    __credit = __total_expense_other;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'11\'");

                                                            }
                                                            break;
                                                        case 12: // รายได้อื่นๆ
                                                            if (__total_income_ohter > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_income_ohter;
                                                                else
                                                                    __credit = __total_income_ohter;
                                                                __addDetail = this._accountByDetail(_accountDetailType.รายได้อื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'12\'");

                                                            }
                                                            break;
                                                        case 14:  // ยอดหักเงินมัดจำจ่าย
                                                            if (__isDebit)
                                                                __debit = __advance_amount;
                                                            else
                                                                __credit = __advance_amount;
                                                            break;
                                                        case 15: // ตัดเงินล่วงหน้า
                                                            if (__isDebit)
                                                                __debit = __deposit_amount;
                                                            else
                                                                __credit = __deposit_amount;
                                                            break;
                                                        case 16: // ส่วนลดรับเงินสด 
                                                            if (__isDebit)
                                                                __debit = __total_discount;
                                                            else
                                                                __credit = __total_discount;
                                                            break;
                                                        case 17: // ส่วนลดรับเงินเชื่อ
                                                            if (__isDebit)
                                                                __debit = __total_discount;
                                                            else
                                                                __credit = __total_discount;
                                                            break;
                                                        case 23:  // ยอดภาษีมูลค่าเพิ่ม ยังไม่ถึงกำหนดชำระ
                                                            if (__isDebit)
                                                                __debit = __vat_next_amount;
                                                            else
                                                                __credit = __vat_next_amount;
                                                            break;
                                                        case 22:
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __vatBuyNoReturn;
                                                                else
                                                                    __credit = __vatBuyNoReturn;
                                                            }
                                                            break;
                                                        case 31:// ส่วนลดชำระเงิน
                                                            if (__isDebit)
                                                                __debit = __discount_payment;
                                                            else
                                                                __credit = __discount_payment;
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        case 92:  // ปัดเศษ +
                                                            if (__total_income_amount < 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_income_amount * -1;
                                                                else
                                                                    __credit = __total_income_amount * -1;
                                                            }
                                                            break;
                                                        case 93:  // ปัดเศษ -
                                                            if (__total_income_amount > 0)
                                                            {
                                                                //ค่าเป็นลบ (ลงบัญชี เป็นบวก)
                                                                if (__isDebit)
                                                                    __debit = __total_income_amount;
                                                                else
                                                                    __credit = __total_income_amount;
                                                            }
                                                            break;
                                                        case 94:  // กำไรจากอัตราแลกเปลี่ยน
                                                            break;
                                                        case 95:  // ขาดทุนจากอัตราแลกเปลี่ยน
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare.ToString() + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #region เจ้าหนี้_ตั้งหนี้อื่น : COB

                                        case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    // ดึงรายละเอียดเจ้าหนี้
                                                    DataRow[] __ap = (this._apSupplier.Rows.Count == 0) ? null : this._apSupplier.Select(_g.d.ap_supplier._code + "=\'" + __custCode + "\'");
                                                    // ประเภทเจ้าหนี้ (0=บุคคลธรรมดา,1=นิติบุคคล)
                                                    int __apType = (__ap == null || __ap.Length == 0) ? 0 : (int)MyLib._myGlobal._decimalPhase(__ap[0][_g.d.ap_supplier._ap_status].ToString());
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // ยอดตั้งหนี้ไม่รวมภาษีมูลค่าเพิ่ม
                                                            if (__vat_type == 2)
                                                            {
                                                                // ภาษีอัตราศูนย์
                                                                if (__isDebit)
                                                                    __debit = __total_value;
                                                                else
                                                                    __credit = __total_value;
                                                            }
                                                            else
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_before_vat;
                                                                else
                                                                    __credit = __total_before_vat;
                                                            }
                                                            __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 2: // ภาษีมูลค่าเพิ่ม
                                                            if (__isDebit)
                                                                __debit = __total_vat_value;
                                                            else
                                                                __credit = __total_vat_value;
                                                            break;
                                                        case 13: // ยอดส่วนลด
                                                            if (__isDebit)
                                                                __debit = __total_discount;
                                                            else
                                                                __credit = __total_discount;
                                                            break;
                                                        case 14:  // ยอดหักเงินมัดจำ
                                                            if (__isDebit)
                                                                __debit = __advance_amount;
                                                            else
                                                                __credit = __advance_amount;
                                                            break;
                                                        case 18: // ยอดเจ้าหนี้
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount;
                                                            break;
                                                        case 23:  // ยอดภาษีมูลค่าเพิ่ม ยังไม่ถึงกำหนดชำระ
                                                                  //if (__vatDocNo.Length == 0) toe ปล่อย
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __vat_next_amount;
                                                                else
                                                                    __credit = __vat_next_amount;
                                                            }
                                                            break;
                                                        case 22:
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __vatBuyNoReturn;
                                                                else
                                                                    __credit = __vatBuyNoReturn;
                                                            }
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        case 94:  // กำไรจากอัตราแลกเปลี่ยน
                                                            break;
                                                        case 95:  // ขาดทุนจากอัตราแลกเปลี่ยน
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #region เจ้าหนี้_ลดหนี้อื่น : CCO

                                        case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    // ดึงรายละเอียดเจ้าหนี้
                                                    DataRow[] __ap = (this._apSupplier.Rows.Count == 0) ? null : this._apSupplier.Select(_g.d.ap_supplier._code + "=\'" + __custCode + "\'");
                                                    // ประเภทเจ้าหนี้ (0=บุคคลธรรมดา,1=นิติบุคคล)
                                                    int __apType = (__ap == null || __ap.Length == 0) ? 0 : (int)MyLib._myGlobal._decimalPhase(__ap[0][_g.d.ap_supplier._ap_status].ToString());
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // ยอดตั้งหนี้ไม่รวมภาษีมูลค่าเพิ่ม
                                                            if (__isDebit)
                                                                __debit = __total_before_vat;
                                                            else
                                                                __credit = __total_before_vat;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 2: // ภาษีมูลค่าเพิ่ม
                                                            if (__isDebit)
                                                                __debit = __total_vat_value;
                                                            else
                                                                __credit = __total_vat_value;
                                                            break;
                                                        case 13: // ยอดส่วนลด
                                                            if (__isDebit)
                                                                __debit = __total_discount;
                                                            else
                                                                __credit = __total_discount;
                                                            break;
                                                        case 14:  // ยอดหักเงินมัดจำ
                                                            if (__isDebit)
                                                                __debit = __advance_amount;
                                                            else
                                                                __credit = __advance_amount;
                                                            break;
                                                        case 18: // ยอดเจ้าหนี้
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount;
                                                            break;
                                                        case 23:  // ยอดภาษีมูลค่าเพิ่ม ยังไม่ถึงกำหนดชำระ
                                                                  //if (__vatDocNo.Length == 0) โต๋ปล่อย
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __vat_next_amount;
                                                                else
                                                                    __credit = __vat_next_amount;
                                                            }
                                                            break;
                                                        case 22:
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __vatBuyNoReturn;
                                                                else
                                                                    __credit = __vatBuyNoReturn;
                                                            }
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        case 94:  // กำไรจากอัตราแลกเปลี่ยน
                                                            break;
                                                        case 95:  // ขาดทุนจากอัตราแลกเปลี่ยน
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #region เจ้าหนี้_เพิ่มหนี้อื่น : CDO

                                        case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    // ดึงรายละเอียดเจ้าหนี้
                                                    DataRow[] __ap = (this._apSupplier.Rows.Count == 0) ? null : this._apSupplier.Select(_g.d.ap_supplier._code + "=\'" + __custCode + "\'");
                                                    // ประเภทเจ้าหนี้ (0=บุคคลธรรมดา,1=นิติบุคคล)
                                                    int __apType = (__ap == null || __ap.Length == 0) ? 0 : (int)MyLib._myGlobal._decimalPhase(__ap[0][_g.d.ap_supplier._ap_status].ToString());
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // ยอดตั้งหนี้ไม่รวมภาษีมูลค่าเพิ่ม
                                                            if (__isDebit)
                                                                __debit = __total_before_vat;
                                                            else
                                                                __credit = __total_before_vat;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 2: // ภาษีมูลค่าเพิ่ม
                                                            if (__isDebit)
                                                                __debit = __total_vat_value;
                                                            else
                                                                __credit = __total_vat_value;
                                                            break;
                                                        case 13: // ยอดส่วนลด
                                                            if (__isDebit)
                                                                __debit = __total_discount;
                                                            else
                                                                __credit = __total_discount;
                                                            break;
                                                        case 14:  // ยอดหักเงินมัดจำ
                                                            if (__isDebit)
                                                                __debit = __advance_amount;
                                                            else
                                                                __credit = __advance_amount;
                                                            break;
                                                        case 18: // ยอดเจ้าหนี้
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount;
                                                            break;
                                                        case 23:  // ยอดภาษีมูลค่าเพิ่ม ยังไม่ถึงกำหนดชำระ
                                                                  //if (__vatDocNo.Length == 0) โต๋ปล่อย
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_vat_value;
                                                                else
                                                                    __credit = __total_vat_value;
                                                            }
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        case 94:  // กำไรจากอัตราแลกเปลี่ยน
                                                            break;
                                                        case 95:  // ขาดทุนจากอัตราแลกเปลี่ยน
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #endregion

                                        #region ระบบสินค้า

                                        #region สินค้า_เบิกสินค้าวัตถุดิบ IO
                                        case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                                            {
                                                switch (__actionCode)
                                                {
                                                    case 1:
                                                        if (__isDebit)
                                                            __debit = __total_amount;
                                                        else
                                                            __credit = __total_amount;
                                                        __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ต้นทุน, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                        break;
                                                    case 2:
                                                        if (__isDebit)
                                                            __debit = __total_amount;
                                                        else
                                                            __credit = __total_amount;
                                                        __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ต้นทุน, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                        break;
                                                }
                                            }
                                            break;
                                        #endregion

                                        #region สินค้า_รับคืนสินค้าจากการเบิก IR
                                        case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                                            {
                                                switch (__actionCode)
                                                {
                                                    case 1:
                                                        if (__isDebit)
                                                            __debit = __total_amount;
                                                        else
                                                            __credit = __total_amount;
                                                        __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ต้นทุน, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                        break;
                                                    case 2:
                                                        if (__isDebit)
                                                            __debit = __total_amount;
                                                        else
                                                            __credit = __total_amount;
                                                        __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ต้นทุน, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                        break;
                                                }
                                            }
                                            break;
                                        #endregion

                                        #region สินค้า_ปรับปรุงสต็อกเกิน: IA
                                        case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก:
                                            {
                                                switch (__actionCode)
                                                {
                                                    case 1:
                                                        if (__isDebit)
                                                            __debit = __total_amount;
                                                        else
                                                            __credit = __total_amount;
                                                        break;
                                                    case 2:
                                                        if (__isDebit)
                                                            __debit = __total_amount;
                                                        else
                                                            __credit = __total_amount;
                                                        break;
                                                }
                                            }
                                            break;
                                        #endregion

                                        #region สินค้า_ปรับปรุงสต็อกขาด: IS
                                        case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด:
                                            {
                                                switch (__actionCode)
                                                {
                                                    case 1:
                                                        if (__isDebit)
                                                            __debit = __total_amount;
                                                        else
                                                            __credit = __total_amount;
                                                        break;
                                                    case 2:
                                                        if (__isDebit)
                                                            __debit = __total_amount;
                                                        else
                                                            __credit = __total_amount;
                                                        break;
                                                }
                                            }
                                            break;
                                        #endregion

                                        #region สินค้า_รับสินค้าสำเร็จรูป: IF
                                        case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                                            {
                                                switch (__actionCode)
                                                {
                                                    case 1:
                                                        if (__isDebit)
                                                            __debit = __total_amount;
                                                        else
                                                            __credit = __total_amount;
                                                        __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ต้นทุน, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                        break;
                                                    case 2:
                                                        if (__isDebit)
                                                            __debit = __total_amount;
                                                        else
                                                            __credit = __total_amount;
                                                        __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ต้นทุน, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                        break;
                                                }
                                            }
                                            break;
                                        #endregion

                                        case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                                            switch (__actionCode)
                                            {
                                                case 1:
                                                    if (__isDebit)
                                                        __debit = __total_amount;
                                                    else
                                                        __credit = __total_amount;
                                                    __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ต้นทุน, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                    break;
                                                case 2:
                                                    if (__isDebit)
                                                        __debit = __total_amount;
                                                    else
                                                        __credit = __total_amount;
                                                    __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ต้นทุน, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                    break;
                                            }
                                            break;
                                        case _g.g._transControlTypeEnum.สินค้า_โอนเข้า:
                                            // กลับข้าง
                                            switch (__actionCode)
                                            {
                                                case 1:
                                                    if (!__isDebit)
                                                        __debit = __total_amount;
                                                    else
                                                        __credit = __total_amount;
                                                    __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ต้นทุน, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, !__isDebit, __custCode, __custName, "");
                                                    break;
                                                case 2:
                                                    if (!__isDebit)
                                                        __debit = __total_amount;
                                                    else
                                                        __credit = __total_amount;
                                                    __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ต้นทุน, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, !__isDebit, __custCode, __custName, "");
                                                    break;
                                            }
                                            break;

                                        #endregion

                                        #region ระบบซื้อ

                                        #region ซื้อ_จ่ายเงินล่วงหน้า:PD case 11 ค่าใช้จ่ายอื่น ๆ 12 รายได้อื่น ๆ และ 90 ,91 ยังไม่ได้ใส่

                                        case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า:
                                            {
                                                switch (__actionCode)
                                                {
                                                    case 1: // ยอดเงินล่วงหน้า
                                                        if (__isDebit)
                                                            __debit = __total_amount;
                                                        else
                                                            __credit = __total_amount;
                                                        break;
                                                    case 5: // ยอดจ่ายเงิน (เงินสด)
                                                        if (__isDebit)
                                                            __debit = __cash_amount;
                                                        else
                                                            __credit = __cash_amount;
                                                        break;
                                                    case 6: // ยอดจ่ายเงิน (เช็คจ่าย)
                                                        if (__isDebit)
                                                            __debit = __chq_amount;
                                                        else
                                                            __credit = __chq_amount;
                                                        __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'2\'");
                                                        break;
                                                    case 7: // ยอดจ่ายเงิน (เงินโอนจากบัญชีเงินฝาก)
                                                        if (__isDebit)
                                                            __debit = __tranfer_amount;
                                                        else
                                                            __credit = __tranfer_amount;
                                                        __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'1\'");
                                                        break;
                                                    case 8: // ยอดจ่ายเงิน (เงินสดย่อย,บัตรเครดิต)
                                                        if (__isDebit)
                                                            __debit = __petty_cash_amount;
                                                        else
                                                            __credit = __petty_cash_amount;
                                                        __addDetail = this._accountByDetail(_accountDetailType.เงินสดย่อย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'4\'");
                                                        break;
                                                    case 11: // ค่าใช้จ่ายอื่นๆ
                                                        if (__total_expense_other > 0)
                                                        {
                                                            if (__isDebit)
                                                                __debit = __total_expense_other;
                                                            else
                                                                __credit = __total_expense_other;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'11\'");

                                                        }
                                                        break;
                                                    case 12: // รายได้อื่นๆ
                                                        if (__total_income_ohter > 0)
                                                        {
                                                            if (__isDebit)
                                                                __debit = __total_income_ohter;
                                                            else
                                                                __credit = __total_income_ohter;
                                                            __addDetail = this._accountByDetail(_accountDetailType.รายได้อื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'12\'");

                                                        }
                                                        break;
                                                    case 31:// ส่วนลดชำระเงิน
                                                        if (__isDebit)
                                                            __debit = __discount_payment;
                                                        else
                                                            __credit = __discount_payment;
                                                        break;
                                                    case 92: // ยอดปัดเศษเพิ่ม (จากการทอนเงิน+จ่ายเกิน)
                                                        if (__total_income_amount < 0)
                                                        {
                                                            if (__isDebit)
                                                                __debit = __total_income_amount * -1;
                                                            else
                                                                __credit = __total_income_amount * -1;
                                                        }
                                                        break;
                                                    case 93: // ยอดปัดเศษลด (จากการทอนเงิน+จ่ายขาด)
                                                        if (__total_income_amount > 0)
                                                        {
                                                            if (__isDebit)
                                                                //ค่าเป็นลบ (ลงบํญชี เป็นบวก)
                                                                __debit = __total_income_amount;
                                                            else
                                                                __credit = __total_income_amount;
                                                        }
                                                        break;
                                                }
                                            }
                                            break;

                                        #endregion

                                        #region ซื้อ_จ่ายเงินล่วงหน้า_รับคืน:PDR case 11 ค่าใช้จ่ายอื่น ๆ 12 รายได้อื่น ๆ และ 90 ,91 ยังไม่ได้ใส่

                                        case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน://PDR
                                            {
                                                switch (__actionCode)
                                                {
                                                    case 1:
                                                        if (__isDebit)
                                                            __debit = __total_amount;
                                                        else
                                                            __credit = __total_amount;
                                                        //__addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    case 5:
                                                        if (__isDebit)
                                                            __debit = __cash_amount;
                                                        else
                                                            __credit = __cash_amount;
                                                        //__addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    case 6:
                                                        if (__isDebit)
                                                            __debit = __chq_amount;
                                                        else
                                                            __credit = __chq_amount;
                                                        __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                        //__addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    case 7:
                                                        if (__isDebit)
                                                            __debit = __tranfer_amount;
                                                        else
                                                            __credit = __tranfer_amount;
                                                        __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                        //__addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    case 8:
                                                        if (__isDebit)
                                                            __debit = __petty_cash_amount;
                                                        else
                                                            __credit = __petty_cash_amount;
                                                        //__addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    case 9:
                                                        if (__isDebit)
                                                            __debit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.cb_trans_detail._sum_amount);
                                                        else
                                                            __credit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.cb_trans_detail._sum_amount);
                                                        //__addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;

                                                    case 10:
                                                        if (__isDebit)
                                                            __debit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.cb_trans_detail._charge);
                                                        else
                                                            __credit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.cb_trans_detail._charge);
                                                        //__addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    case 11:  // ค่าใช้จ่ายอื่นๆ
                                                        if (__total_income_ohter > 0)
                                                        {
                                                            if (__isDebit)
                                                                __debit = __total_income_ohter;
                                                            else
                                                                __credit = __total_income_ohter;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'11\'");

                                                        }
                                                        break;
                                                    case 12: // รายได้อื่นๆ
                                                        if (__total_expense_other > 0)
                                                        {
                                                            if (__isDebit)
                                                                __debit = __total_expense_other;
                                                            else
                                                                __credit = __total_expense_other;
                                                            __addDetail = this._accountByDetail(_accountDetailType.รายได้อื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'12\'");

                                                        }
                                                        break;
                                                    case 31:// ส่วนลดชำระเงิน
                                                        if (__isDebit)
                                                            __debit = __discount_payment;
                                                        else
                                                            __credit = __discount_payment;
                                                        break;

                                                    case 92:
                                                        if (__total_income_amount > 0)
                                                        {
                                                            if (__isDebit)
                                                                __debit = __total_income_amount;
                                                            else
                                                                __credit = __total_income_amount;
                                                            //__addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        }
                                                        break;

                                                    case 93:
                                                        if (__total_income_amount < 0)
                                                        {
                                                            if (__isDebit)
                                                                //ค่าเป็นลบ (ลงบํญชี เป็นบวก)
                                                                __debit = -1 * __total_income_amount;
                                                            else
                                                                __credit = -1 * __total_income_amount;
                                                            //__addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        }
                                                        break;
                                                        //case 83: //.บัญชีเดินสะพัด
                                                        //    if (__isDebit)
                                                        //        __debit = total_amount;
                                                        //    else
                                                        //        __credit = total_amount;
                                                        //    break;

                                                        //case 90:
                                                        //    if (__isDebit)
                                                        //        __debit = total_amount;
                                                        //    else
                                                        //        __credit = total_amount;
                                                        //    break;
                                                        //case 91:
                                                        //    if (__isDebit)
                                                        //        __debit = total_amount;
                                                        //    else
                                                        //        __credit = total_amount;
                                                        //    break;
                                                }
                                                break;
                                            }

                                        #endregion

                                        #region ซื้อ_จ่ายเงินมัดจำ:PC 11.ค่าใช้จ่ายอื่นๆ 12.รายได้อื่นๆ 90.ผลต่างจากการปัดเศษ ด้าน Dr. 91.ผลต่างจากการปัดเศษ ด้าน Cr. ยังไม่ได้ใส่

                                        case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ://PC
                                            {
                                                DataRow[] __ap = (this._apSupplier.Rows.Count == 0) ? null : this._apSupplier.Select(_g.d.ap_supplier._code + "=\'" + __custCode + "\'");
                                                int __apType = (__ap == null || __ap.Length == 0) ? 0 : (int)MyLib._myGlobal._decimalPhase(__ap[0][_g.d.ap_supplier._ap_status].ToString());
                                                switch (__actionCode)
                                                {
                                                    case 1: // ยอดเงินมัดจำ (ไม่รวมภาษีมูลค่าเพิ่ม)
                                                        if (__isDebit)
                                                            __debit = __total_before_vat;
                                                        else
                                                            __credit = __total_before_vat;
                                                        break;
                                                    case 2: // ภาษีมูลค่าเพิ่ม (ซื้อ)
                                                        if (__isDebit)
                                                            __debit = __total_vat_value;
                                                        else
                                                            __credit = __total_vat_value;
                                                        break;
                                                    case 23:  // ยอดภาษีมูลค่าเพิ่ม ยังไม่ถึงกำหนดชำระ

                                                        if (__isDebit)
                                                            __debit = __vat_next_amount;
                                                        else
                                                            __credit = __vat_next_amount;

                                                        break;
                                                    case 22:
                                                        {
                                                            if (__isDebit)
                                                                __debit = __vatBuyNoReturn;
                                                            else
                                                                __credit = __vatBuyNoReturn;
                                                        }
                                                        break;
                                                    case 3: // ภาษีหัก ณ. ที่จ่าย ภงด.3
                                                        if (__apType == 0)
                                                        {
                                                            // บุคคล
                                                            if (__isDebit)
                                                                __debit = __tax_at_pay;
                                                            else
                                                                __credit = __tax_at_pay;
                                                        }
                                                        break;
                                                    case 4: // ภาษีหัก ณ. ที่จ่าย ภงด.53
                                                        if (__apType == 1)
                                                        {
                                                            // นิติบุคคล
                                                            if (__isDebit)
                                                                __debit = __tax_at_pay;
                                                            else
                                                                __credit = __tax_at_pay;
                                                        }
                                                        break;
                                                    case 5: // ยอดจ่ายเงิน (เงินสด)
                                                        if (__isDebit)
                                                            __debit = __cash_amount;
                                                        else
                                                            __credit = __cash_amount;
                                                        break;
                                                    case 6: // ยอดจ่ายเงิน (เช็คจ่าย)
                                                        if (__isDebit)
                                                            __debit = __chq_amount;
                                                        else
                                                            __credit = __chq_amount;
                                                        __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'2\'");
                                                        break;
                                                    case 7: // ยอดจ่ายเงิน (เงินโอนจากบัญชีเงินฝาก)
                                                        if (__isDebit)
                                                            __debit = __tranfer_amount;
                                                        else
                                                            __credit = __tranfer_amount;
                                                        __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'1\'");
                                                        break;
                                                    case 8: // ยอดจ่ายเงิน (เงินสดย่อย,บัตรเครดิต)
                                                        if (__isDebit)
                                                            __debit = __petty_cash_amount;
                                                        else
                                                            __credit = __petty_cash_amount;
                                                        __addDetail = this._accountByDetail(_accountDetailType.เงินสดย่อย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'4\'");
                                                        break;
                                                    case 11: // ค่าใช้จ่ายอื่นๆ
                                                        if (__total_expense_other > 0)
                                                        {
                                                            if (__isDebit)
                                                                __debit = __total_expense_other;
                                                            else
                                                                __credit = __total_expense_other;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'11\'");

                                                        }
                                                        break;
                                                    case 12: // รายได้อื่นๆ
                                                        if (__total_income_ohter > 0)
                                                        {
                                                            if (__isDebit)
                                                                __debit = __total_income_ohter;
                                                            else
                                                                __credit = __total_income_ohter;
                                                            __addDetail = this._accountByDetail(_accountDetailType.รายได้อื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'12\'");

                                                        }
                                                        break;
                                                    case 31:// ส่วนลดชำระเงิน
                                                        if (__isDebit)
                                                            __debit = __discount_payment;
                                                        else
                                                            __credit = __discount_payment;
                                                        break;
                                                    case 90:  // ผลต่างจากการปัดเศษ 
                                                        __diffDebitCode = __accountCode;
                                                        __diffCreditCode = __accountCode;
                                                        break;
                                                    case 92: // ยอดปัดเศษเพิ่ม (จากการทอนเงิน+จ่ายเกิน)
                                                        if (__total_income_amount < 0)
                                                        {
                                                            if (__isDebit)
                                                                __debit = __total_income_amount * -1;
                                                            else
                                                                __credit = __total_income_amount * -1;
                                                        }
                                                        break;
                                                    case 93: // ยอดปัดเศษลด (จากการทอนเงิน+จ่ายขาด)
                                                        if (__total_income_amount > 0)
                                                        {
                                                            if (__isDebit)
                                                                //ค่าเป็นลบ (ลงบํญชี เป็นบวก)
                                                                __debit = __total_income_amount;
                                                            else
                                                                __credit = __total_income_amount;
                                                        }
                                                        break;
                                                }
                                            }
                                            break;

                                        #endregion

                                        #region ซื้อ_จ่ายเงินมัดจำ_รับคืน:PCR

                                        case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน:
                                            {
                                                switch (__actionCode)
                                                {
                                                    case 1:
                                                        if (__isDebit)
                                                            __debit = _getSumMain(_icTrans, __transFlagCompare, __docNo, _g.d.ic_trans._total_before_vat);
                                                        else
                                                            __credit = _getSumMain(_icTrans, __transFlagCompare, __docNo, _g.d.ic_trans._total_before_vat); ;
                                                        // __addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    case 2: //ภาษีมูลค่าเพิ่ม
                                                        if (__isDebit)
                                                            __debit = _getSumMain(_icTrans, __transFlagCompare, __docNo, _g.d.ic_trans._total_vat_value);
                                                        else
                                                            __credit = _getSumMain(_icTrans, __transFlagCompare, __docNo, _g.d.ic_trans._total_vat_value);
                                                        // __addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    // case 3: //ยอดภาษีหัก ณ ที่จ่าย
                                                    case 5:
                                                        if (__isDebit)
                                                            __debit = __cash_amount;
                                                        else
                                                            __credit = __cash_amount;
                                                        // __addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    case 6:
                                                        if (__isDebit)
                                                            __debit = __chq_amount;
                                                        else
                                                            __credit = __chq_amount;
                                                        __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                        // __addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    case 7:
                                                        if (__isDebit)
                                                            __debit = __tranfer_amount;
                                                        else
                                                            __credit = __tranfer_amount;
                                                        __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                        // __addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    case 8:
                                                        if (__isDebit)
                                                            __debit = __petty_cash_amount;
                                                        else
                                                            __credit = __petty_cash_amount;
                                                        // __addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    case 9:
                                                        if (__isDebit)
                                                            __debit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.cb_trans_detail._sum_amount);
                                                        else
                                                            __credit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.cb_trans_detail._sum_amount);
                                                        // __addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    case 10:
                                                        if (__isDebit)
                                                            __debit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.cb_trans_detail._charge);
                                                        else
                                                            __credit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.cb_trans_detail._charge);
                                                        // __addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    case 11:  // ค่าใช้จ่ายอื่นๆ
                                                        if (__total_income_ohter > 0)
                                                        {
                                                            if (__isDebit)
                                                                __debit = __total_income_ohter;
                                                            else
                                                                __credit = __total_income_ohter;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'11\'");

                                                        }
                                                        break;
                                                    case 12: // รายได้อื่นๆ
                                                        if (__total_expense_other > 0)
                                                        {
                                                            if (__isDebit)
                                                                __debit = __total_expense_other;
                                                            else
                                                                __credit = __total_expense_other;
                                                            __addDetail = this._accountByDetail(_accountDetailType.รายได้อื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'12\'");

                                                        }
                                                        break;
                                                    case 31:// ส่วนลดชำระเงิน
                                                        if (__isDebit)
                                                            __debit = __discount_payment;
                                                        else
                                                            __credit = __discount_payment;
                                                        break;
                                                    case 92:
                                                        if (__total_income_amount > 0)
                                                        {
                                                            if (__isDebit)
                                                                __debit = __total_income_amount;
                                                            else
                                                                __credit = __total_income_amount;
                                                            // __addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        }
                                                        break;
                                                    case 93:
                                                        if (__total_income_amount < 0)
                                                        {
                                                            if (__isDebit)
                                                                //ค่าเป็นลบ (ลงบํญชี เป็นบวก)
                                                                __debit = -1 * __total_income_amount;
                                                            else
                                                                __credit = -1 * __total_income_amount;
                                                            // __addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        }
                                                        break;
                                                }
                                            }
                                            break;

                                        #endregion

                                        #region ซื้อ_ซื้อสินค้าและค่าบริการ:PU

                                        case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    DataRow[] __ap = (this._apSupplier.Rows.Count == 0) ? null : this._apSupplier.Select(_g.d.ap_supplier._code + "=\'" + __custCode + "\'");
                                                    int __apType = (__ap == null || __ap.Length == 0) ? 0 : (int)MyLib._myGlobal._decimalPhase(__ap[0][_g.d.ap_supplier._ap_status].ToString());
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // ยอดซื้อสินค้า (ไม่รวมภาษีมูลค่าเพิ่ม)
                                                            {
                                                                //decimal __sumAmount = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_amount, "and item_type=0");
                                                                //decimal __sumAmount = _getSum(this._icTransDetail, __transFlagCompare, __docNo, "sum_amount_exclude_vat", "and item_type=0");
                                                                if (__vat_type == 2)
                                                                {
                                                                    // ภาษีอัตราศูนย์
                                                                    if (__isDebit)
                                                                        __debit = __total_value - __total_service_amount;
                                                                    else
                                                                        __credit = __total_value - __total_service_amount;
                                                                }
                                                                else
                                                                {
                                                                    __total_before_vat = __total_before_vat - _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_amount, "and item_type=\'1\'");
                                                                    if (__isDebit)
                                                                        __debit = __total_before_vat - __total_discount;//__total_before_vat; __sumAmount
                                                                    else
                                                                        __credit = __total_before_vat - __total_discount;// __total_before_vat;

                                                                    if (__isDebit)
                                                                        __debit = __total_before_vat + __total_discount;//__total_before_vat; __sumAmount
                                                                    else
                                                                        __credit = __total_before_vat + __total_discount;// __total_before_vat;
                                                                                                                         /*if (__isDebit)
                                                                                                                             __debit = __sumAmount;//__total_before_vat; __sumAmount
                                                                                                                         else
                                                                                                                             __credit = __sumAmount;// __total_before_vat;*/
                                                                }
                                                                __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ราคา, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and item_type=0");
                                                            }
                                                            break;
                                                        case 30: // ยอดซื้อบริการ (ไม่รวมภาษีมูลค่าเพิ่ม)
                                                            {
                                                                decimal __sumAmount = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_amount, "and item_type=\'1\'");
                                                                if (__vat_type == 2)
                                                                {
                                                                    // ภาษีอัตราศูนย์
                                                                    if (__isDebit)
                                                                        __debit = __sumAmount; //__total_value;
                                                                    else
                                                                        __credit = __sumAmount; // __total_value;
                                                                }
                                                                else
                                                                {
                                                                    /*if (__isDebit)
                                                                        __debit = __sumAmount - __total_discount; //__total_before_vat;
                                                                    else
                                                                        __credit = __sumAmount - __total_discount; // __total_before_vat;*/
                                                                    if (__isDebit)
                                                                        __debit = __sumAmount; //__total_before_vat;
                                                                    else
                                                                        __credit = __sumAmount; // __total_before_vat;
                                                                }
                                                                __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ราคา, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and item_type=\'1\'");
                                                            }
                                                            break;
                                                        case 2: // ภาษีมูลค่าเพิ่ม
                                                            if (__isDebit)
                                                                __debit = __total_vat_value;
                                                            else
                                                                __credit = __total_vat_value;
                                                            break;
                                                        case 3: // ภาษีหัก ณ. ที่จ่าย ภงด.3
                                                            if (__apType == 0)
                                                            {
                                                                // บุคคล
                                                                if (__isDebit)
                                                                    __debit = __tax_at_pay;
                                                                else
                                                                    __credit = __tax_at_pay;
                                                            }
                                                            break;
                                                        case 4: // ภาษีหัก ณ. ที่จ่าย ภงด.53
                                                            if (__apType == 1)
                                                            {
                                                                // นิติบุคคล
                                                                if (__isDebit)
                                                                    __debit = __tax_at_pay;
                                                                else
                                                                    __credit = __tax_at_pay;
                                                            }
                                                            break;
                                                        case 5: // ยอดเงินสดจ่าย
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __cash_amount;
                                                                else
                                                                    __credit = __cash_amount;
                                                            }
                                                            break;
                                                        case 6: // ยอดเช็คจ่าย
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __chq_amount;
                                                                else
                                                                    __credit = __chq_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.เช็คจ่าย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'2\'");
                                                            }
                                                            break;
                                                        case 7: // ยอดเงินโอน
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __tranfer_amount;
                                                                else
                                                                    __credit = __tranfer_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'1\'");
                                                            }
                                                            break;
                                                        case 8: // ยอดเงินสดย่อยจ่าย (บัตรเครดิต)
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __petty_cash_amount;
                                                                else
                                                                    __credit = __petty_cash_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.เงินสดย่อย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'4\'");
                                                            }
                                                            break;
                                                        case 11: // ค่าใช้จ่ายอื่นๆ
                                                            if (__total_expense_other > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_expense_other;
                                                                else
                                                                    __credit = __total_expense_other;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'11\'");

                                                            }
                                                            break;
                                                        case 12: // รายได้อื่นๆ
                                                            if (__total_income_ohter > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_income_ohter;
                                                                else
                                                                    __credit = __total_income_ohter;
                                                                __addDetail = this._accountByDetail(_accountDetailType.รายได้อื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'12\'");

                                                            }
                                                            break;
                                                        case 16: // ส่วนลดรับเงินสด 
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_discount;
                                                                else
                                                                    __credit = __total_discount;
                                                            }
                                                            break;
                                                        case 17: // ส่วนลดรับเงินเชื่อ
                                                            if (__transType == 0 || __transType == 2)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_discount;
                                                                else
                                                                    __credit = __total_discount;
                                                            }
                                                            break;
                                                        case 31:// ส่วนลดชำระเงิน
                                                            if (__isDebit)
                                                                __debit = __discount_payment;
                                                            else
                                                                __credit = __discount_payment;
                                                            break;
                                                        case 14:  // ยอดหักเงินมัดจำจ่าย
                                                            if (__isDebit)
                                                                __debit = __advance_amount;
                                                            else
                                                                __credit = __advance_amount;
                                                            break;
                                                        case 15: // ตัดเงินล่วงหน้า
                                                            if (__isDebit)
                                                                __debit = __deposit_amount;
                                                            else
                                                                __credit = __deposit_amount;
                                                            break;
                                                        case 18: // ยอดเจ้าหนี้
                                                            if (__transType == 0 || __transType == 2)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_amount;
                                                                else
                                                                    __credit = __total_amount;
                                                            }
                                                            break;
                                                        case 23:  // ยอดภาษีมูลค่าเพิ่ม ยังไม่ถึงกำหนดชำระ (กรณีซื้อบริการเชื่อ)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __vat_next_amount;
                                                                else
                                                                    __credit = __vat_next_amount;
                                                            }
                                                            break;
                                                        case 22:
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __vatBuyNoReturn;
                                                                else
                                                                    __credit = __vatBuyNoReturn;
                                                            }
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        case 92:  // ปัดเศษ +
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__total_income_amount < 0)
                                                                {
                                                                    if (__isDebit)
                                                                        __debit = __total_income_amount * -1;
                                                                    else
                                                                        __credit = __total_income_amount * -1;
                                                                }
                                                            }
                                                            break;
                                                        case 93:  // ปัดเศษ -
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__total_income_amount > 0)
                                                                {
                                                                    //ค่าเป็นลบ (ลงบัญชี เป็นบวก)
                                                                    if (__isDebit)
                                                                        __debit = __total_income_amount;
                                                                    else
                                                                        __credit = __total_income_amount;
                                                                }
                                                            }
                                                            break;
                                                        case 94:  // กำไรจากอัตราแลกเปลี่ยน
                                                            break;
                                                        case 95:  // ขาดทุนจากอัตราแลกเปลี่ยน
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare.ToString() + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #region ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:PT

                                        case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด://PT
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    DataRow[] __ap = (this._apSupplier.Rows.Count == 0) ? null : this._apSupplier.Select(_g.d.ap_supplier._code + "=\'" + __custCode + "\'");
                                                    int __apType = (__ap == null || __ap.Length == 0) ? 0 : (int)MyLib._myGlobal._decimalPhase(__ap[0][_g.d.ap_supplier._ap_status].ToString());
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // เจ้าหนี้การค้า
                                                            if (__transType == 0 || __transType == 1)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_amount;
                                                                else
                                                                    __credit = __total_amount;

                                                                //__addDetail = this._accountByDetail(_accountDetailType.สินค้า_ราคา, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and item_type=0");
                                                            }
                                                            break;
                                                        case 2: // ภาษีมูลค่าเพิ่ม
                                                            if (__isDebit)
                                                                __debit = __total_vat_value;
                                                            else
                                                                __credit = __total_vat_value;
                                                            break;
                                                        case 23:  // ยอดภาษีมูลค่าเพิ่ม ยังไม่ถึงกำหนดชำระ (กรณีซื้อบริการเชื่อ)
                                                                  //if (__vatDocNo.Length > 0) โต๋ปล่อย
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __vat_next_amount;
                                                                else
                                                                    __credit = __vat_next_amount;
                                                            }
                                                            break;
                                                        case 22:
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __vatBuyNoReturn;
                                                                else
                                                                    __credit = __vatBuyNoReturn;
                                                            }
                                                            break;
                                                        case 3: // ภาษีหัก ณ. ที่จ่าย ภงด.53
                                                            if (__isDebit)
                                                                __debit = __tax_at_pay;
                                                            else
                                                                __credit = __tax_at_pay;
                                                            break;
                                                        case 5: // ยอดเงินสดจ่าย
                                                            if (__transType == 2 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __cash_amount;
                                                                else
                                                                    __credit = __cash_amount;
                                                            }
                                                            break;
                                                        case 6: // ยอดเช็คจ่าย
                                                            if (__transType == 2 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __chq_amount;
                                                                else
                                                                    __credit = __chq_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร_เช็ครับล่วงหน้า, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'2\'");
                                                            }
                                                            break;
                                                        case 7: // ยอดเงินโอน
                                                            if (__transType == 2 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __tranfer_amount;
                                                                else
                                                                    __credit = __tranfer_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'1\'");
                                                            }
                                                            break;
                                                        case 8: // ยอดเงินสดย่อยจ่าย
                                                            if (__transType == 2 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __petty_cash_amount;
                                                                else
                                                                    __credit = __petty_cash_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.เงินสดย่อย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'4\'");
                                                            }
                                                            break;
                                                        case 9: // บัตรเครดิต
                                                            if (__transType == 2 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __credit_card_amount + __credit_card_charge;
                                                                else
                                                                    __credit = __credit_card_amount + __credit_card_charge;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร_บัตรเครดิต, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'3\'");
                                                            }
                                                            break;
                                                        case 10: // ยอด CHARGE บัตรเครดิต
                                                            if (__transType == 2 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __credit_card_charge;
                                                                else
                                                                    __credit = __credit_card_charge;
                                                            }
                                                            break;
                                                        case 17: // ส่วนลดจากการส่งคืน
                                                            if (__isDebit)
                                                                __debit = __total_discount;
                                                            else
                                                                __credit = __total_discount;
                                                            break;
                                                        case 18: // ยอดส่งคืน ไม่รวมภาษีมูลค่าเพิ่ม สินค้าทัวไป
                                                            {
                                                                /*if (__isDebit)
                                                                    __debit = __total_value;
                                                                else
                                                                    __credit = __total_value; //__total_before_vat;

                                                                //__addDetail = this._accountByDetail(_accountDetailType.สินค้า_ราคา, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and item_type=0");
                                                                __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ราคา, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and item_type=0");
                                                                */
                                                                if (__vat_type == 2)
                                                                {
                                                                    // ภาษีอัตราศูนย์
                                                                    if (__isDebit)
                                                                        __debit = __total_value - __total_service_amount;
                                                                    else
                                                                        __credit = __total_value - __total_service_amount;
                                                                }
                                                                else
                                                                {
                                                                    __total_before_vat = __total_before_vat - _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_amount, "and item_type=\'1\'");
                                                                    if (__isDebit)
                                                                        __debit = __total_before_vat - __total_discount;//__total_before_vat; __sumAmount
                                                                    else
                                                                        __credit = __total_before_vat - __total_discount;// __total_before_vat;

                                                                    if (__isDebit)
                                                                        __debit = __total_before_vat + __total_discount;//__total_before_vat; __sumAmount
                                                                    else
                                                                        __credit = __total_before_vat + __total_discount;// __total_before_vat;
                                                                                                                         /*if (__isDebit)
                                                                                                                             __debit = __sumAmount;//__total_before_vat; __sumAmount
                                                                                                                         else
                                                                                                                             __credit = __sumAmount;// __total_before_vat;*/
                                                                }
                                                                __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ราคา, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and item_type=0");
                                                            }
                                                            break;
                                                        case 30: // ยอดส่งคืน สินค้าบริการ
                                                            {
                                                                decimal __sumAmount = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.ic_trans_detail._sum_amount, "and item_type=\'1\'");
                                                                if (__vat_type == 2)
                                                                {
                                                                    // ภาษีอัตราศูนย์
                                                                    if (__isDebit)
                                                                        __debit = __sumAmount; //__total_value;
                                                                    else
                                                                        __credit = __sumAmount; // __total_value;
                                                                }
                                                                else
                                                                {
                                                                    /*if (__isDebit)
                                                                        __debit = __sumAmount - __total_discount; //__total_before_vat;
                                                                    else
                                                                        __credit = __sumAmount - __total_discount; // __total_before_vat;*/
                                                                    if (__isDebit)
                                                                        __debit = __sumAmount; //__total_before_vat;
                                                                    else
                                                                        __credit = __sumAmount; // __total_before_vat;
                                                                }
                                                                __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ราคา, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and item_type=\'1\'");
                                                            }
                                                            break;
                                                        case 31:// ส่วนลดชำระเงิน
                                                            if (__isDebit)
                                                                __debit = __discount_payment;
                                                            else
                                                                __credit = __discount_payment;
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        case 92:  // ปัดเศษ +
                                                            if (__transType == 2 || __transType == 3)
                                                            {
                                                                if (__total_income_amount > 0)
                                                                {
                                                                    if (__isDebit)
                                                                        __debit = __total_income_amount;
                                                                    else
                                                                        __credit = __total_income_amount;
                                                                }
                                                            }
                                                            break;
                                                        case 93:  // ปัดเศษ -
                                                            if (__transType == 2 || __transType == 3)
                                                            {
                                                                if (__total_income_amount < 0)
                                                                {
                                                                    //ค่าเป็นลบ (ลงบัญชี เป็นบวก)
                                                                    if (__isDebit)
                                                                        __debit = __total_income_amount * -1;
                                                                    else
                                                                        __credit = __total_income_amount * -1;
                                                                }
                                                            }
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare.ToString() + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #region ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:PA

                                        case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    DataRow[] __ap = (this._apSupplier.Rows.Count == 0) ? null : this._apSupplier.Select(_g.d.ap_supplier._code + "=\'" + __custCode + "\'");
                                                    int __apType = (__ap == null || __ap.Length == 0) ? 0 : (int)MyLib._myGlobal._decimalPhase(__ap[0][_g.d.ap_supplier._ap_status].ToString());
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // ยอดซื้อสินค้า (ไม่รวมภาษีมูลค่าเพิ่ม)
                                                            if (__vat_type == 2)
                                                            {
                                                                // ภาษีอัตราศูนย์
                                                                if (__isDebit)
                                                                    __debit = __total_value;
                                                                else
                                                                    __credit = __total_value;
                                                            }
                                                            else
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_before_vat;
                                                                else
                                                                    __credit = __total_before_vat;
                                                            }
                                                            __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ราคา, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 2: // ภาษีมูลค่าเพิ่ม
                                                            if (__isDebit)
                                                                __debit = __total_vat_value;
                                                            else
                                                                __credit = __total_vat_value;
                                                            break;
                                                        case 18: // ยอดเจ้าหนี้
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount;
                                                            break;
                                                        case 31:// ส่วนลดชำระเงิน
                                                            if (__isDebit)
                                                                __debit = __discount_payment;
                                                            else
                                                                __credit = __discount_payment;
                                                            break;
                                                        case 23:  // ยอดภาษีมูลค่าเพิ่ม ยังไม่ถึงกำหนดชำระ
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __vat_next_amount;
                                                                else
                                                                    __credit = __vat_next_amount;
                                                            }
                                                            break;
                                                        case 22:
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __vatBuyNoReturn;
                                                                else
                                                                    __credit = __vatBuyNoReturn;
                                                            }
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        case 94:  // กำไรจากอัตราแลกเปลี่ยน
                                                            break;
                                                        case 95:  // ขาดทุนจากอัตราแลกเปลี่ยน
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare.ToString() + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #region ซื้อ_พาเชียล_รับสินค้า : PI

                                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    DataRow[] __ap = (this._apSupplier.Rows.Count == 0) ? null : this._apSupplier.Select(_g.d.ap_supplier._code + "=\'" + __custCode + "\'");
                                                    int __apType = (__ap == null || __ap.Length == 0) ? 0 : (int)MyLib._myGlobal._decimalPhase(__ap[0][_g.d.ap_supplier._ap_status].ToString());
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // ยอดซื้อสินค้า 
                                                            if (__isDebit)
                                                                __debit = __total_value;
                                                            else
                                                                __credit = __total_value;
                                                            __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ราคา, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 1001:
                                                            // เอา ยอดก่อนภาษี
                                                            if (__isDebit)
                                                                __debit = __total_before_vat;
                                                            else
                                                                __credit = __total_before_vat;

                                                            break;
                                                        case 2: // ภาษีมูลค่าเพิ่ม (ซื้อ)
                                                            if (__isDebit)
                                                                __debit = __total_vat_value;
                                                            else
                                                                __credit = __total_vat_value;
                                                            break;
                                                        case 16: // ส่วนลดรับเงินสด หักบัญชีพัก
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_discount;
                                                                else
                                                                    __credit = __total_discount;
                                                            }
                                                            break;
                                                        case 17: // ส่วนลดรับเงินเชื่อ หักบัญชีพัก
                                                            if (__transType == 0 || __transType == 2)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_discount;
                                                                else
                                                                    __credit = __total_discount;
                                                            }
                                                            break;
                                                        case 18: // ยอดเจ้าหนี้
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount;
                                                            break;
                                                        case 1018: // ยอดเจ้าหนี้ ไม่รวมภาษี SINGHA
                                                            if (__isDebit)
                                                                __debit = __total_value;
                                                            else
                                                                __credit = __total_value;
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare.ToString() + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #region ซื้อ_พาเชียล_ตั้งหนี้:PIU

                                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    DataRow[] __ap = (this._apSupplier.Rows.Count == 0) ? null : this._apSupplier.Select(_g.d.ap_supplier._code + "=\'" + __custCode + "\'");
                                                    int __apType = (__ap == null || __ap.Length == 0) ? 0 : (int)MyLib._myGlobal._decimalPhase(__ap[0][_g.d.ap_supplier._ap_status].ToString());
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // ยอดซื้อสินค้า (รวมภาษีมูลค่าเพิ่ม),ยอดตั้งหนี้เจ้าหนี้ไม่ถึงกำหนดตั้งหนี้ (รวมภาษีมูลค่าเพิ่ม)
                                                                // โต๋แก้ไข ดึงยอดสินค้า หักมัดจำด้วย 
                                                            if (__isDebit)
                                                                __debit = __total_amount + __advance_amount;
                                                            else
                                                                __credit = __total_amount + __advance_amount;

                                                            __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ราคา, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 1001:
                                                            if (__isDebit)
                                                                __debit = __total_value;
                                                            else
                                                                __credit = __total_value;
                                                            break;
                                                        case 1002: // ยอดก่อน vat
                                                            if (__isDebit)
                                                                __debit = __total_before_vat;
                                                            else
                                                                __credit = __total_before_vat;
                                                            break;
                                                        case 2: // ภาษีมูลค่าเพิ่ม
                                                            if (__isDebit)
                                                                __debit = __total_vat_value;
                                                            else
                                                                __credit = __total_vat_value;
                                                            break;
                                                        case 3: // ภาษีหัก ณ. ที่จ่าย ภงด.3
                                                            if (__apType == 0)
                                                            {
                                                                // บุคคล
                                                                if (__isDebit)
                                                                    __debit = __tax_at_pay;
                                                                else
                                                                    __credit = __tax_at_pay;
                                                            }
                                                            break;
                                                        case 4: // ภาษีหัก ณ. ที่จ่าย ภงด.53
                                                            if (__apType == 1)
                                                            {
                                                                // นิติบุคคล
                                                                if (__isDebit)
                                                                    __debit = __tax_at_pay;
                                                                else
                                                                    __credit = __tax_at_pay;
                                                            }
                                                            break;
                                                        case 5: // ยอดเงินสดจ่าย
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __cash_amount;
                                                                else
                                                                    __credit = __cash_amount;
                                                            }
                                                            break;
                                                        case 6: // ยอดเช็คจ่าย
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __chq_amount;
                                                                else
                                                                    __credit = __chq_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.เช็คจ่าย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            }
                                                            break;
                                                        case 7: // ยอดเงินโอน
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __tranfer_amount;
                                                                else
                                                                    __credit = __tranfer_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            }
                                                            break;
                                                        case 8: // ยอดเงินสดย่อยจ่าย
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __petty_cash_amount;
                                                                else
                                                                    __credit = __petty_cash_amount;
                                                            }
                                                            break;
                                                        case 9: // ภาษีซื้อยังไม่ถึงกำหนด (ภาษีมูลค่าเพิ่ม)
                                                            if (__isDebit)
                                                                __debit = __total_vat_value;
                                                            else
                                                                __credit = __total_vat_value;
                                                            break;
                                                        case 11: // ค่าใช้จ่ายอื่นๆ
                                                            if (__total_expense_other > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_expense_other;
                                                                else
                                                                    __credit = __total_expense_other;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'11\'");

                                                            }
                                                            break;
                                                        case 12: // รายได้อื่นๆ
                                                            if (__total_income_ohter > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_income_ohter;
                                                                else
                                                                    __credit = __total_income_ohter;
                                                                __addDetail = this._accountByDetail(_accountDetailType.รายได้อื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'12\'");

                                                            }
                                                            break;
                                                        case 14:  // ยอดหักเงินมัดจำจ่าย
                                                            if (__isDebit)
                                                                __debit = __advance_amount;
                                                            else
                                                                __credit = __advance_amount;
                                                            break;
                                                        case 15: // ตัดเงินล่วงหน้า
                                                            if (__isDebit)
                                                                __debit = __deposit_amount;
                                                            else
                                                                __credit = __deposit_amount;
                                                            break;
                                                        case 16: // ส่วนลดรับเงินสด 
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_discount;
                                                                else
                                                                    __credit = __total_discount;
                                                            }
                                                            break;
                                                        case 17: // ส่วนลดรับเงินเชื่อ
                                                            if (__transType == 0 || __transType == 2)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_discount;
                                                                else
                                                                    __credit = __total_discount;
                                                            }
                                                            break;
                                                        case 18: // ยอดเจ้าหนี้
                                                            if (__transType == 0 || __transType == 2)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_amount;
                                                                else
                                                                    __credit = __total_amount;
                                                            }
                                                            break;
                                                        case 19: // ส่วนลดรับเงินสด หักบัญชีพัก
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_discount;
                                                                else
                                                                    __credit = __total_discount;
                                                            }
                                                            break;
                                                        case 20: // ส่วนลดรับเงินเชื่อ หักบัญชีพัก
                                                            if (__transType == 0 || __transType == 2)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_discount;
                                                                else
                                                                    __credit = __total_discount;
                                                            }
                                                            break;
                                                        case 23:  // ยอดภาษีมูลค่าเพิ่ม ยังไม่ถึงกำหนดชำระ
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __vat_next_amount;
                                                                else
                                                                    __credit = __vat_next_amount;
                                                            }
                                                            break;
                                                        case 22:
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __vatBuyNoReturn;
                                                                else
                                                                    __credit = __vatBuyNoReturn;
                                                            }
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        case 92:  // ปัดเศษ +
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__total_income_amount < 0)
                                                                {
                                                                    if (__isDebit)
                                                                        __debit = __total_income_amount * -1;
                                                                    else
                                                                        __credit = __total_income_amount * -1;
                                                                }
                                                            }
                                                            break;
                                                        case 93:  // ปัดเศษ -
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__total_income_amount > 0)
                                                                {
                                                                    //ค่าเป็นลบ (ลงบัญชี เป็นบวก)
                                                                    if (__isDebit)
                                                                        __debit = __total_income_amount;
                                                                    else
                                                                        __credit = __total_income_amount;
                                                                }
                                                            }
                                                            break;
                                                        case 94:  // กำไรจากอัตราแลกเปลี่ยน
                                                            break;
                                                        case 95:  // ขาดทุนจากอัตราแลกเปลี่ยน
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare.ToString() + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #region ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด PIA
                                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    DataRow[] __ap = (this._apSupplier.Rows.Count == 0) ? null : this._apSupplier.Select(_g.d.ap_supplier._code + "=\'" + __custCode + "\'");
                                                    int __apType = (__ap == null || __ap.Length == 0) ? 0 : (int)MyLib._myGlobal._decimalPhase(__ap[0][_g.d.ap_supplier._ap_status].ToString());
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // ยอดซื้อสินค้า 
                                                            if (__isDebit)
                                                                __debit = __total_value;
                                                            else
                                                                __credit = __total_value;
                                                            __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ราคา, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 2: // ภาษีมูลค่าเพิ่ม (ซื้อ)
                                                            if (__isDebit)
                                                                __debit = __total_vat_value;
                                                            else
                                                                __credit = __total_vat_value;
                                                            break;
                                                        case 16: // ส่วนลดรับเงินสด หักบัญชีพัก
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_discount;
                                                                else
                                                                    __credit = __total_discount;
                                                            }
                                                            break;
                                                        case 17: // ส่วนลดรับเงินเชื่อ หักบัญชีพัก
                                                            if (__transType == 0 || __transType == 2)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_discount;
                                                                else
                                                                    __credit = __total_discount;
                                                            }
                                                            break;
                                                        case 18: // ยอดเจ้าหนี้
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount;
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare.ToString() + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #region ซื้อ_พาเชียล_ลดหนี้ PIC
                                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    DataRow[] __ap = (this._apSupplier.Rows.Count == 0) ? null : this._apSupplier.Select(_g.d.ap_supplier._code + "=\'" + __custCode + "\'");
                                                    int __apType = (__ap == null || __ap.Length == 0) ? 0 : (int)MyLib._myGlobal._decimalPhase(__ap[0][_g.d.ap_supplier._ap_status].ToString());
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // เจ้าหนี้การค้า
                                                            if (__transType == 0 || __transType == 1)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_amount;
                                                                else
                                                                    __credit = __total_amount;
                                                            }
                                                            break;
                                                        case 2: // ภาษีมูลค่าเพิ่ม
                                                            if (__isDebit)
                                                                __debit = __total_vat_value;
                                                            else
                                                                __credit = __total_vat_value;
                                                            break;
                                                        case 23:  // ยอดภาษีมูลค่าเพิ่ม ยังไม่ถึงกำหนดชำระ (กรณีซื้อบริการเชื่อ)
                                                                  //if (__vatDocNo.Length > 0) โต๋ปล่อย
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __vat_next_amount;
                                                                else
                                                                    __credit = __vat_next_amount;
                                                            }
                                                            break;
                                                        case 22:
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __vatBuyNoReturn;
                                                                else
                                                                    __credit = __vatBuyNoReturn;
                                                            }
                                                            break;
                                                        case 3: // ภาษีหัก ณ. ที่จ่าย ภงด.53
                                                            if (__isDebit)
                                                                __debit = __tax_at_pay;
                                                            else
                                                                __credit = __tax_at_pay;
                                                            break;
                                                        case 5: // ยอดเงินสดจ่าย
                                                            if (__transType == 2 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __cash_amount;
                                                                else
                                                                    __credit = __cash_amount;
                                                            }
                                                            break;
                                                        case 6: // ยอดเช็คจ่าย
                                                            if (__transType == 2 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __chq_amount;
                                                                else
                                                                    __credit = __chq_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร_เช็ครับล่วงหน้า, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'2\'");
                                                            }
                                                            break;
                                                        case 7: // ยอดเงินโอน
                                                            if (__transType == 2 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __tranfer_amount;
                                                                else
                                                                    __credit = __tranfer_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'1\'");
                                                            }
                                                            break;
                                                        case 8: // ยอดเงินสดย่อยจ่าย
                                                            if (__transType == 2 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __petty_cash_amount;
                                                                else
                                                                    __credit = __petty_cash_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.เงินสดย่อย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'4\'");
                                                            }
                                                            break;
                                                        case 9: // บัตรเครดิต
                                                            if (__transType == 2 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __credit_card_amount + __credit_card_charge;
                                                                else
                                                                    __credit = __credit_card_amount + __credit_card_charge;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร_บัตรเครดิต, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'3\'");
                                                            }
                                                            break;
                                                        case 10: // ยอด CHARGE บัตรเครดิต
                                                            if (__transType == 2 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __credit_card_charge;
                                                                else
                                                                    __credit = __credit_card_charge;
                                                            }
                                                            break;
                                                        case 14: // ยอดหักเงินมัดจำจ่าย
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __deposit_amount;
                                                                else
                                                                    __credit = __deposit_amount;
                                                            }
                                                            break;
                                                        case 15: // ยอดหักเงินล่วงหน้า
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __advance_amount;
                                                                else
                                                                    __credit = __advance_amount;

                                                            }
                                                            break;
                                                        case 17: // ส่วนลดจากการส่งคืน
                                                            if (__isDebit)
                                                                __debit = __total_discount;
                                                            else
                                                                __credit = __total_discount;
                                                            break;
                                                        case 18: // ยอดส่งคืน ไม่รวมภาษีมูลค่าเพิ่ม
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount; //__total_before_vat;
                                                            break;
                                                        case 31:// ส่วนลดชำระเงิน
                                                            if (__isDebit)
                                                                __debit = __discount_payment;
                                                            else
                                                                __credit = __discount_payment;
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        case 92:  // ปัดเศษ +
                                                            if (__transType == 2 || __transType == 3)
                                                            {
                                                                if (__total_income_amount > 0)
                                                                {
                                                                    if (__isDebit)
                                                                        __debit = __total_income_amount;
                                                                    else
                                                                        __credit = __total_income_amount;
                                                                }
                                                            }
                                                            break;
                                                        case 93:  // ปัดเศษ -
                                                            if (__transType == 2 || __transType == 3)
                                                            {
                                                                if (__total_income_amount < 0)
                                                                {
                                                                    //ค่าเป็นลบ (ลงบัญชี เป็นบวก)
                                                                    if (__isDebit)
                                                                        __debit = __total_income_amount * -1;
                                                                    else
                                                                        __credit = __total_income_amount * -1;
                                                                }
                                                            }
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare.ToString() + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;
                                        #endregion

                                        #region  ซื้อ_พาเชียล_เพิ่มหนี้ PID
                                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    DataRow[] __ap = (this._apSupplier.Rows.Count == 0) ? null : this._apSupplier.Select(_g.d.ap_supplier._code + "=\'" + __custCode + "\'");
                                                    int __apType = (__ap == null || __ap.Length == 0) ? 0 : (int)MyLib._myGlobal._decimalPhase(__ap[0][_g.d.ap_supplier._ap_status].ToString());
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // ยอดซื้อสินค้า (ไม่รวมภาษีมูลค่าเพิ่ม)
                                                            if (__vat_type == 2)
                                                            {
                                                                // ภาษีอัตราศูนย์
                                                                if (__isDebit)
                                                                    __debit = __total_value;
                                                                else
                                                                    __credit = __total_value;
                                                            }
                                                            else
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_before_vat;
                                                                else
                                                                    __credit = __total_before_vat;
                                                            }
                                                            __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ราคา, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 2: // ภาษีมูลค่าเพิ่ม
                                                            if (__isDebit)
                                                                __debit = __total_vat_value;
                                                            else
                                                                __credit = __total_vat_value;
                                                            break;
                                                        case 18: // ยอดเจ้าหนี้
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount;
                                                            break;
                                                        case 31:// ส่วนลดชำระเงิน
                                                            if (__isDebit)
                                                                __debit = __discount_payment;
                                                            else
                                                                __credit = __discount_payment;
                                                            break;
                                                        case 23:  // ยอดภาษีมูลค่าเพิ่ม ยังไม่ถึงกำหนดชำระ
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __vat_next_amount;
                                                                else
                                                                    __credit = __vat_next_amount;
                                                            }
                                                            break;
                                                        case 22:
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __vatBuyNoReturn;
                                                                else
                                                                    __credit = __vatBuyNoReturn;
                                                            }
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        case 94:  // กำไรจากอัตราแลกเปลี่ยน
                                                            break;
                                                        case 95:  // ขาดทุนจากอัตราแลกเปลี่ยน
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare.ToString() + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;
                                        #endregion

                                        #endregion

                                        #region ระบบขาย

                                        #region ขาย_รับเงินล่วงหน้า :SD รอดึงค่าใหม่จาก พี่จืด case 9,10

                                        case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า:
                                            {
                                                if (__conditionCase.Length == 0)
                                                {
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // ยอดรับเงินล่วงหน้า
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount;
                                                            break;
                                                        case 5: // ยอดรับเงินสด
                                                            if (__isDebit)
                                                                __debit = __cash_amount;
                                                            else
                                                                __credit = __cash_amount;
                                                            break;
                                                        case 6: // ยอดเช็ครับ
                                                            if (__isDebit)
                                                                __debit = __chq_amount;
                                                            else
                                                                __credit = __chq_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร_เช็ครับล่วงหน้า, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'2\'");
                                                            break;
                                                        case 7: // ยอดเงินโอน
                                                            if (__isDebit)
                                                                __debit = __tranfer_amount;
                                                            else
                                                                __credit = __tranfer_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'1\'");
                                                            break;
                                                        case 8: // ยอดเงินสดย่อย
                                                            if (__isDebit)
                                                                __debit = __petty_cash_amount;
                                                            else
                                                                __credit = __petty_cash_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.เงินสดย่อย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'4\'");
                                                            break;
                                                        case 9: // บัตรเครดิต+CHARGE
                                                            if (__isDebit)
                                                                __debit = __credit_card_amount + __credit_card_charge;
                                                            else
                                                                __credit = __credit_card_amount + __credit_card_charge;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร_บัตรเครดิต, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'3\'");
                                                            break;
                                                        case 10: // ค่าธรรมเนียมบัตร (รายได้)
                                                            if (__isDebit)
                                                                __debit = __credit_card_charge;
                                                            else
                                                                __credit = __credit_card_charge;
                                                            break;
                                                        case 11:  // ค่าใช้จ่ายอื่นๆ
                                                            if (__total_income_ohter > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_income_ohter;
                                                                else
                                                                    __credit = __total_income_ohter;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'11\'");

                                                            }
                                                            break;
                                                        case 12: // รายได้อื่นๆ
                                                            if (__total_expense_other > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_expense_other;
                                                                else
                                                                    __credit = __total_expense_other;
                                                                __addDetail = this._accountByDetail(_accountDetailType.รายได้อื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'12\'");

                                                            }
                                                            break;
                                                        case 31:// ส่วนลดชำระเงิน
                                                            if (__isDebit)
                                                                __debit = __discount_payment;
                                                            else
                                                                __credit = __discount_payment;
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare.ToString() + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #region ขาย_เงินล่วงหน้า_คืน:SDR

                                        case _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน://SDR
                                            {
                                                switch (__actionCode)
                                                {
                                                    case 1:
                                                        if (__isDebit)
                                                            __debit = __total_amount;
                                                        else
                                                            __credit = __total_amount;
                                                        //__addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    case 5:
                                                        if (__isDebit)
                                                            __debit = __cash_amount;
                                                        else
                                                            __credit = __cash_amount;
                                                        //__addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    case 6:
                                                        if (__isDebit)
                                                            __debit = __chq_amount;
                                                        else
                                                            __credit = __chq_amount;
                                                        __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                        //__addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    case 7:
                                                        if (__isDebit)
                                                            __debit = __tranfer_amount;
                                                        else
                                                            __credit = __tranfer_amount;
                                                        __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                        //__addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    case 8:
                                                        if (__isDebit)
                                                            __debit = __petty_cash_amount;
                                                        else
                                                            __credit = __petty_cash_amount;
                                                        __addDetail = this._accountByDetail(_accountDetailType.เงินสดย่อย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'4\'");
                                                        break;
                                                    case 11: // ค่าใช้จ่ายอื่นๆ
                                                        if (__total_expense_other > 0)
                                                        {
                                                            if (__isDebit)
                                                                __debit = __total_expense_other;
                                                            else
                                                                __credit = __total_expense_other;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'11\'");

                                                        }
                                                        break;
                                                    case 12: // รายได้อื่นๆ
                                                        if (__total_income_ohter > 0)
                                                        {
                                                            if (__isDebit)
                                                                __debit = __total_income_ohter;
                                                            else
                                                                __credit = __total_income_ohter;
                                                            __addDetail = this._accountByDetail(_accountDetailType.รายได้อื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'12\'");

                                                        }
                                                        break;
                                                    case 31:// ส่วนลดชำระเงิน
                                                        if (__isDebit)
                                                            __debit = __discount_payment;
                                                        else
                                                            __credit = __discount_payment;
                                                        break;

                                                    case 92:
                                                        if (__total_income_amount > 0)
                                                        {
                                                            if (__isDebit)
                                                                __debit = __total_income_amount;
                                                            else
                                                                __credit = __total_income_amount;
                                                            //__addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        }
                                                        break;

                                                    case 93:
                                                        if (__total_income_amount < 0)
                                                        {
                                                            if (__isDebit)
                                                                //ค่าเป็นลบ (ลงบํญชี เป็นบวก)
                                                                __debit = -1 * __total_income_amount;
                                                            else
                                                                __credit = -1 * __total_income_amount;
                                                            //__addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        }
                                                        break;
                                                }
                                                break;
                                            }

                                        #endregion

                                        #region ขาย_รับเงินมัดจำ:SRV case 11 ค่าใช้จ่ายอื่น ๆ 12 รายได้อื่น ๆ และ 90 ,91 ยังไม่ได้ใส่ ยอดภาษีหัก ณ ที่จ่าย

                                        case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ:
                                            {
                                                switch (__actionCode)
                                                {
                                                    case 1:
                                                        if (__isDebit)
                                                            __debit = _getSumMain(_icTrans, __transFlagCompare, __docNo, _g.d.ic_trans._total_before_vat);
                                                        else
                                                            __credit = _getSumMain(_icTrans, __transFlagCompare, __docNo, _g.d.ic_trans._total_before_vat); ;
                                                        //__addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    case 2: //ภาษีมูลค่าเพิ่ม
                                                        if (__isDebit)
                                                            __debit = _getSumMain(_icTrans, __transFlagCompare, __docNo, _g.d.ic_trans._total_vat_value);
                                                        else
                                                            __credit = _getSumMain(_icTrans, __transFlagCompare, __docNo, _g.d.ic_trans._total_vat_value);
                                                        //__addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    case 3: //ยอดภาษีหัก ณ ที่จ่าย
                                                        if (__isDebit)
                                                            __debit = __tax_at_pay;
                                                        else
                                                            __credit = __tax_at_pay;
                                                        break;
                                                    case 5:
                                                        if (__isDebit)
                                                            __debit = __cash_amount;
                                                        else
                                                            __credit = __cash_amount;
                                                        //__addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    case 6:
                                                        if (__isDebit)
                                                            __debit = __chq_amount;
                                                        else
                                                            __credit = __chq_amount;
                                                        __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร_เช็ครับล่วงหน้า, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'2\'");
                                                        //__addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                        //__addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    case 7:
                                                        if (__isDebit)
                                                            __debit = __tranfer_amount;
                                                        else
                                                            __credit = __tranfer_amount;
                                                        __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                        //__addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    case 8:
                                                        if (__isDebit)
                                                            __debit = __petty_cash_amount;
                                                        else
                                                            __credit = __petty_cash_amount;
                                                        //__addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    case 9:
                                                        /*
                                                        if (__isDebit)
                                                            __debit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.cb_trans_detail._sum_amount);
                                                        else
                                                            __credit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.cb_trans_detail._sum_amount);
                                                        //__addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        */
                                                        //if (__transType == 1 || __transType == 3)
                                                        {
                                                            if (__isDebit)
                                                                __debit = __credit_card_amount + __credit_card_charge;
                                                            else
                                                                __credit = __credit_card_amount + __credit_card_charge;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร_บัตรเครดิต, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'3\'");

                                                        }
                                                        break;

                                                        break;

                                                    case 10:
                                                        if (__isDebit)
                                                            __debit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.cb_trans_detail._charge);
                                                        else
                                                            __credit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.cb_trans_detail._charge);
                                                        //__addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    case 11:  // ค่าใช้จ่ายอื่นๆ
                                                        if (__total_income_ohter > 0)
                                                        {
                                                            if (__isDebit)
                                                                __debit = __total_income_ohter;
                                                            else
                                                                __credit = __total_income_ohter;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'11\'");

                                                        }
                                                        break;
                                                    case 12: // รายได้อื่นๆ
                                                        if (__total_expense_other > 0)
                                                        {
                                                            if (__isDebit)
                                                                __debit = __total_expense_other;
                                                            else
                                                                __credit = __total_expense_other;
                                                            __addDetail = this._accountByDetail(_accountDetailType.รายได้อื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'12\'");

                                                        }
                                                        break;
                                                    case 31:// ส่วนลดชำระเงิน
                                                        if (__isDebit)
                                                            __debit = __discount_payment;
                                                        else
                                                            __credit = __discount_payment;
                                                        break;
                                                    case 92:
                                                        if (__total_income_amount > 0)
                                                        {
                                                            if (__isDebit)
                                                                __debit = __total_income_amount;
                                                            else
                                                                __credit = __total_income_amount;
                                                            // __addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        }
                                                        break;

                                                    case 93:
                                                        if (__total_income_amount < 0)
                                                        {
                                                            if (__isDebit)
                                                                //ค่าเป็นลบ (ลงบัญชี เป็นบวก)
                                                                __debit = -1 * __total_income_amount;
                                                            else
                                                                __credit = -1 * __total_income_amount;
                                                            //__addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        }
                                                        break;
                                                }
                                                break;
                                            }

                                        #endregion

                                        #region ขาย_เงินมัดจำ_คืน:SRT 3.ยอดภาษีหัก ณ ที่จ่าย สำหรับ ภงด.3 4.ยอดภาษีหัก ณ ที่จ่าย สำหรับ ภงด.53 90.ผลต่างจากการปัดเศษ ด้าน Dr.91.ผลต่างจากการปัดเศษ ด้าน Cr.

                                        case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน:
                                            {
                                                switch (__actionCode)
                                                {
                                                    case 1:
                                                        if (__isDebit)
                                                            __debit = _getSumMain(_icTrans, __transFlagCompare, __docNo, _g.d.ic_trans._total_before_vat);
                                                        else
                                                            __credit = _getSumMain(_icTrans, __transFlagCompare, __docNo, _g.d.ic_trans._total_before_vat); ;
                                                        // __addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    case 2: //ภาษีมูลค่าเพิ่ม
                                                        if (__isDebit)
                                                            __debit = _getSumMain(_icTrans, __transFlagCompare, __docNo, _g.d.ic_trans._total_vat_value);
                                                        else
                                                            __credit = _getSumMain(_icTrans, __transFlagCompare, __docNo, _g.d.ic_trans._total_vat_value);
                                                        // __addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    // case 3: //ยอดภาษีหัก ณ ที่จ่าย
                                                    case 5:
                                                        if (__isDebit)
                                                            __debit = __cash_amount;
                                                        else
                                                            __credit = __cash_amount;
                                                        // __addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    case 6:
                                                        if (__isDebit)
                                                            __debit = __chq_amount;
                                                        else
                                                            __credit = __chq_amount;
                                                        __addDetail = this._accountByDetail(_accountDetailType.เช็ครับ, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, " and doc_type=2 ");
                                                        // __addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    case 7:
                                                        if (__isDebit)
                                                            __debit = __tranfer_amount;
                                                        else
                                                            __credit = __tranfer_amount;
                                                        __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, " and doc_type=1 ");
                                                        // __addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        break;
                                                    case 8:
                                                        if (__isDebit)
                                                            __debit = __petty_cash_amount;
                                                        else
                                                            __credit = __petty_cash_amount;
                                                        __addDetail = this._accountByDetail(_accountDetailType.เงินสดย่อย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'4\'");
                                                        break;
                                                    case 11: // ค่าใช้จ่ายอื่นๆ
                                                        if (__total_expense_other > 0)
                                                        {
                                                            if (__isDebit)
                                                                __debit = __total_expense_other;
                                                            else
                                                                __credit = __total_expense_other;
                                                            __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'11\'");

                                                        }
                                                        break;
                                                    case 12: // รายได้อื่นๆ
                                                        if (__total_income_ohter > 0)
                                                        {
                                                            if (__isDebit)
                                                                __debit = __total_income_ohter;
                                                            else
                                                                __credit = __total_income_ohter;
                                                            __addDetail = this._accountByDetail(_accountDetailType.รายได้อื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'12\'");

                                                        }
                                                        break;
                                                    case 31:// ส่วนลดชำระเงิน
                                                        if (__isDebit)
                                                            __debit = __discount_payment;
                                                        else
                                                            __credit = __discount_payment;
                                                        break;

                                                    //case 9:
                                                    //    if (__isDebit)
                                                    //        __debit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.cb_trans_detail._sum_amount);
                                                    //    else
                                                    //        __credit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.cb_trans_detail._sum_amount);
                                                    //    __addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                    //    break;

                                                    //case 10:
                                                    //    if (__isDebit)
                                                    //        __debit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.cb_trans_detail._charge);
                                                    //    else
                                                    //        __credit = _getSum(this._icTransDetail, __transFlagCompare, __docNo, _g.d.cb_trans_detail._charge);
                                                    //    __addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                    //    break;                                                                                 
                                                    case 92:
                                                        if (__total_income_amount > 0)
                                                        {
                                                            if (__isDebit)
                                                                __debit = __total_income_amount;
                                                            else
                                                                __credit = __total_income_amount;
                                                            // __addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        }
                                                        break;

                                                    case 93:
                                                        if (__total_income_amount < 0)
                                                        {
                                                            if (__isDebit)
                                                                //ค่าเป็นลบ (ลงบํญชี เป็นบวก)
                                                                __debit = -1 * __total_income_amount;
                                                            else
                                                                __credit = -1 * __total_income_amount;
                                                            // __addDetail = this._accountByDetail(this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit);
                                                        }
                                                        break;
                                                }
                                                break;
                                            }

                                        #endregion

                                        #region ขาย_ขายสินค้าและบริการ : SI

                                        case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ://SI
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // ยอดขายสินค้าเงินสด (ยอดก่อนภาษีมูลค่าเพิ่ม)
                                                            if (__vat_type == 2)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_amount;
                                                                else
                                                                    __credit = __total_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ราคา, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            }
                                                            else
                                                            {
                                                                // toe เพิ่ม ยอดยกเว้นภาษีเข้าไป + __total_except_vat
                                                                if (__isDebit)
                                                                    __debit = __total_before_vat + __advance_amount + __total_discount + __total_except_vat;
                                                                else
                                                                    __credit = __total_before_vat + __advance_amount + __total_discount + __total_except_vat;
                                                                __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ราคา, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            }
                                                            break;
                                                        case 1001:
                                                            // เอา ยอดก่อนภาษี
                                                            if (__isDebit)
                                                                __debit = __total_before_vat;
                                                            else
                                                                __credit = __total_before_vat;

                                                            break;
                                                        case 2: // ภาษีมูลค่าเพิ่ม
                                                            if (__isDebit)
                                                                __debit = __total_vat_value;
                                                            else
                                                                __credit = __total_vat_value;
                                                            break;
                                                        case 3: // ภาษีหัก ณ. ที่จ่าย
                                                            if (__isDebit)
                                                                __debit = __tax_at_pay;
                                                            else
                                                                __credit = __tax_at_pay;
                                                            break;
                                                        case 5: // ยอดรับเงินสด
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __cash_amount;
                                                                else
                                                                    __credit = __cash_amount;
                                                            }
                                                            break;
                                                        case 6: // ยอดเช็ครับ
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __chq_amount;
                                                                else
                                                                    __credit = __chq_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร_เช็ครับล่วงหน้า, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'2\'");
                                                            }
                                                            break;
                                                        case 7: // ยอดเงินโอน
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __tranfer_amount;
                                                                else
                                                                    __credit = __tranfer_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'1\'");
                                                            }
                                                            break;
                                                        case 8: // ยอดเงินสดย่อย
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __petty_cash_amount;
                                                                else
                                                                    __credit = __petty_cash_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.เงินสดย่อย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'4\'");
                                                            }
                                                            break;
                                                        case 9: // บัตรเครดิต+CHARGE
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __credit_card_amount + __credit_card_charge;
                                                                else
                                                                    __credit = __credit_card_amount + __credit_card_charge;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร_บัตรเครดิต, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'3\'");

                                                            }
                                                            break;
                                                        case 10: // ค่าธรรมเนียมบัตร (รายได้)
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __credit_card_charge;
                                                                else
                                                                    __credit = __credit_card_charge;
                                                            }
                                                            break;
                                                        case 11:  // ค่าใช้จ่ายอื่นๆ
                                                            if (__total_income_ohter > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_income_ohter;
                                                                else
                                                                    __credit = __total_income_ohter;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'11\'");

                                                            }
                                                            break;
                                                        case 12: // รายได้อื่นๆ
                                                            if (__total_expense_other > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_expense_other;
                                                                else
                                                                    __credit = __total_expense_other;
                                                                __addDetail = this._accountByDetail(_accountDetailType.รายได้อื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'12\'");

                                                            }
                                                            break;
                                                        case 13: // จ่ายด้วยแต้ม
                                                            if (__isDebit)
                                                                __debit = __total_point;
                                                            else
                                                                __credit = __total_point;
                                                            break;
                                                        case 14:  // ยอดหักเงินมัดจำ
                                                            if (__isDebit)
                                                                __debit = __advance_amount;
                                                            else
                                                                __credit = __advance_amount;
                                                            break;
                                                        case 15: // ยอดหักเงินล่วงหน้า
                                                            if (__isDebit)
                                                                __debit = __deposit_amount;
                                                            else
                                                                __credit = __deposit_amount;
                                                            __addDetail = this._accountByDetail(_accountDetailType.เงินล่่วงหน้ารับ, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'5\'");
                                                            break;
                                                        case 17: // รายได้ก่อนหักส่วนลด
                                                            if (__transType == 0 || __transType == 2)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_amount - __total_discount;
                                                                else
                                                                    __credit = __total_amount - __total_discount;
                                                            }
                                                            break;
                                                        case 18: // ยอดลูกหนี้ (กรณีขายเชื่อ)
                                                            if (__transType == 0 || __transType == 2)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_amount;
                                                                else
                                                                    __credit = __total_amount;
                                                            }
                                                            break;
                                                        case 19: // มูลค่าส่วนลด
                                                            if (__isDebit)
                                                                __debit = __total_discount;
                                                            else
                                                                __credit = __total_discount;
                                                            break;
                                                        case 31:// ส่วนลดชำระเงิน
                                                            if (__isDebit)
                                                                __debit = __discount_payment;
                                                            else
                                                                __credit = __discount_payment;
                                                            break;

                                                        case 23:  // ยอดภาษีมูลค่าเพิ่ม ยังไม่ถึงกำหนดชำระ
                                                            if (__transTypeStr.Equals("3") && __vatDocNo.Length == 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_vat_value;
                                                                else
                                                                    __credit = __total_vat_value;
                                                            }
                                                            break;
                                                        case 30: // จ่ายด้วยคูปอง
                                                            if (__isDebit)
                                                                __debit = __total_coupon;
                                                            else
                                                                __credit = __total_coupon;
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        case 92:  // ปัดเศษ +
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__total_income_amount > 0)
                                                                {
                                                                    if (__isDebit)
                                                                        __debit = __total_income_amount;
                                                                    else
                                                                        __credit = __total_income_amount;
                                                                }
                                                            }
                                                            break;
                                                        case 93:  // ปัดเศษ -
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__total_income_amount < 0)
                                                                {
                                                                    if (__isDebit)
                                                                        __debit = __total_income_amount * -1;
                                                                    else
                                                                        __credit = __total_income_amount * -1;
                                                                }
                                                            }
                                                            break;
                                                        case 94:  // กำไรจากอัตราแลกเปลี่ยน
                                                            break;
                                                        case 95:  // ขาดทุนจากอัตราแลกเปลี่ยน
                                                            break;
                                                        case 101: // ต้นทุนขาย Perpetual

                                                            if (__isDebit)
                                                                __debit = __total_cost;
                                                            else
                                                                __credit = __total_cost;

                                                            __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ต้นทุน, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 103:// สินค้าคงเหลือ Perpetual                                                    
                                                            if (__isDebit)
                                                                __debit = __total_cost;
                                                            else
                                                                __credit = __total_cost;

                                                            __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ต้นทุน, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare.ToString() + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #region ขาย_รับคืนสินค้าจากการขายและลดหนี้:ST

                                        case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // ยอดรับคืนสินค้า (ไม่รวมภาษีมูลค่าเพิ่ม)
                                                            if (__vat_type == 2)
                                                            {
                                                                // ภาษีอัตราศูนย์
                                                                if (__isDebit)
                                                                    __debit = __total_value;
                                                                else
                                                                    __credit = __total_value;
                                                            }
                                                            else
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_before_vat;
                                                                else
                                                                    __credit = __total_before_vat;
                                                            }
                                                            __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ราคา, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 2: // ภาษีมูลค่าเพิ่ม
                                                            if (__isDebit)
                                                                __debit = __total_vat_value;
                                                            else
                                                                __credit = __total_vat_value;
                                                            break;
                                                        case 3: // ภาษีหัก ณ. ที่จ่าย
                                                            if (__isDebit)
                                                                __debit = __tax_at_pay;
                                                            else
                                                                __credit = __tax_at_pay;
                                                            break;
                                                        case 5: // ยอดจ่ายเงินสด
                                                            if (__transType == 1 || __transType == 3 || __transType == 5)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __cash_amount;
                                                                else
                                                                    __credit = __cash_amount;
                                                            }
                                                            break;
                                                        case 6: // ยอดเช็คจ่าย
                                                            if (__transType == 1 || __transType == 3 || __transType == 5)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __chq_amount;
                                                                else
                                                                    __credit = __chq_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.เช็คจ่าย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'2\'");
                                                            }
                                                            break;
                                                        case 7: // ยอดเงินโอน
                                                            if (__transType == 1 || __transType == 3 || __transType == 5)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __tranfer_amount;
                                                                else
                                                                    __credit = __tranfer_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ธนาคาร, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'1\'");
                                                            }
                                                            break;
                                                        case 8: // ยอดเงินสดย่อย
                                                            if (__transType == 1 || __transType == 3 || __transType == 5)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __petty_cash_amount;
                                                                else
                                                                    __credit = __petty_cash_amount;
                                                                __addDetail = this._accountByDetail(_accountDetailType.เงินสดย่อย, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'4\'");
                                                            }
                                                            break;
                                                        case 11: // ค่าใช้จ่ายอื่นๆ
                                                            if (__total_expense_other > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_expense_other;
                                                                else
                                                                    __credit = __total_expense_other;
                                                                __addDetail = this._accountByDetail(_accountDetailType.ค่าใช้จ่ายอื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'11\'");

                                                            }
                                                            break;
                                                        case 12: // รายได้อื่นๆ
                                                            if (__total_income_ohter > 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_income_ohter;
                                                                else
                                                                    __credit = __total_income_ohter;
                                                                __addDetail = this._accountByDetail(_accountDetailType.รายได้อื่น, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "and doc_type=\'12\'");

                                                            }
                                                            break;
                                                        case 13: // ยอดส่วนลด
                                                                 // เฉพาะส่วนลดหลังคำนวณภาษี
                                                            if (_g.g._companyProfile._discount_type == 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_discount;
                                                                else
                                                                    __credit = __total_discount;
                                                            }
                                                            else
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_discount;
                                                                else
                                                                    __credit = __total_discount;
                                                            }
                                                            break;
                                                        case 31:// ส่วนลดชำระเงิน
                                                            if (__isDebit)
                                                                __debit = __discount_payment;
                                                            else
                                                                __credit = __discount_payment;
                                                            break;
                                                        case 14:  // ยอดหักเงินมัดจำ
                                                            if (__isDebit)
                                                                __debit = __advance_amount;
                                                            else
                                                                __credit = __advance_amount;
                                                            break;
                                                        case 15: // ตัดเงินล่วงหน้า
                                                            if (__isDebit)
                                                                __debit = __deposit_amount;
                                                            else
                                                                __credit = __deposit_amount;
                                                            break;
                                                        case 17: // ยอดรับคืนสินค้าก่อนส่วนลด
                                                            if (__isDebit)
                                                                __debit = __total_before_vat;
                                                            else
                                                                __credit = __total_before_vat;
                                                            __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ราคา, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 18: // ยอดลูกหนี้ (กรณีรับคืนเงินเชื่อ)
                                                            if (__transType == 0 || __transType == 2 || __transType == 4)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_amount;
                                                                else
                                                                    __credit = __total_amount;
                                                            }
                                                            break;
                                                        case 23:  // ยอดภาษีมูลค่าเพิ่ม ยังไม่ถึงกำหนดชำระ
                                                            if (__transTypeStr.Equals("3") && __vatDocNo.Length == 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_vat_value;
                                                                else
                                                                    __credit = __total_vat_value;
                                                            }
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        case 92:  // ปัดเศษ +
                                                            if (__transType == 1 || __transType == 3 || __transType == 5)
                                                            {
                                                                if (__total_income_amount < 0)
                                                                {
                                                                    if (__isDebit)
                                                                        __debit = __total_income_amount * -1;
                                                                    else
                                                                        __credit = __total_income_amount * -1;
                                                                }
                                                            }
                                                            break;
                                                        case 93:  // ปัดเศษ -
                                                            if (__transType == 1 || __transType == 3 || __transType == 5)
                                                            {
                                                                if (__total_income_amount > 0)
                                                                {
                                                                    if (__isDebit)
                                                                        __debit = __total_income_amount;
                                                                    else
                                                                        __credit = __total_income_amount;
                                                                }
                                                            }
                                                            break;
                                                        case 94:  // กำไรจากอัตราแลกเปลี่ยน
                                                            break;
                                                        case 95:  // ขาดทุนจากอัตราแลกเปลี่ยน
                                                            break;
                                                        case 101: // ต้นทุนขาย Perpetual

                                                            if (__isDebit)
                                                                __debit = __total_cost;
                                                            else
                                                                __credit = __total_cost;
                                                            __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ต้นทุน, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 103:// สินค้าคงเหลือ Perpetual                                                    
                                                            if (__isDebit)
                                                                __debit = __total_cost;
                                                            else
                                                                __credit = __total_cost;
                                                            __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ต้นทุน, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare.ToString() + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                        #endregion

                                        #region ขาย_เพิ่มหนี้:SA

                                        case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้://SA
                                            {
                                                string __transTypeStr = ((int)__transType + 1).ToString();
                                                if (__conditionCase.Length == 0 || __conditionCase.IndexOf(__transTypeStr) != -1)
                                                {
                                                    switch (__actionCode)
                                                    {
                                                        case 1: // ยอดขายสินค้าเงินสด (ยอดก่อนภาษีมูลค่าเพิ่ม)
                                                            if (__isDebit)
                                                                __debit = __total_before_vat;
                                                            else
                                                                __credit = __total_before_vat;
                                                            __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ราคา, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 2: // ภาษีมูลค่าเพิ่ม
                                                            if (__isDebit)
                                                                __debit = __total_vat_value;
                                                            else
                                                                __credit = __total_vat_value;
                                                            break;
                                                        case 17: // รายได้ก่อนหักส่วนลด
                                                            if (__transType == 0 || __transType == 2)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_amount - __total_discount;
                                                                else
                                                                    __credit = __total_amount - __total_discount;
                                                            }
                                                            break;
                                                        case 18: // ยอดลูกหนี้ (กรณีขายเชื่อ)
                                                            if (__isDebit)
                                                                __debit = __total_amount;
                                                            else
                                                                __credit = __total_amount;
                                                            break;
                                                        case 19: // มูลค่าส่วนลด
                                                            if (__isDebit)
                                                                __debit = __total_discount;
                                                            else
                                                                __credit = __total_discount;
                                                            break;
                                                        case 23:  // ยอดภาษีมูลค่าเพิ่ม ยังไม่ถึงกำหนดชำระ
                                                            if (__transTypeStr.Equals("3") && __vatDocNo.Length == 0)
                                                            {
                                                                if (__isDebit)
                                                                    __debit = __total_vat_value;
                                                                else
                                                                    __credit = __total_vat_value;
                                                            }
                                                            break;
                                                        case 31:// ส่วนลดชำระเงิน
                                                            if (__isDebit)
                                                                __debit = __discount_payment;
                                                            else
                                                                __credit = __discount_payment;
                                                            break;
                                                        case 90:  // ผลต่างจากการปัดเศษ 
                                                            __diffDebitCode = __accountCode;
                                                            __diffCreditCode = __accountCode;
                                                            break;
                                                        case 92:  // ปัดเศษ +
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__total_income_amount > 0)
                                                                {
                                                                    if (__isDebit)
                                                                        __debit = __total_income_amount;
                                                                    else
                                                                        __credit = __total_income_amount;
                                                                }
                                                            }
                                                            break;
                                                        case 93:  // ปัดเศษ -
                                                            if (__transType == 1 || __transType == 3)
                                                            {
                                                                if (__total_income_amount < 0)
                                                                {
                                                                    if (__isDebit)
                                                                        __debit = __total_income_amount * -1;
                                                                    else
                                                                        __credit = __total_income_amount * -1;
                                                                }
                                                            }
                                                            break;
                                                        case 94:  // กำไรจากอัตราแลกเปลี่ยน
                                                            break;
                                                        case 95:  // ขาดทุนจากอัตราแลกเปลี่ยน
                                                            break;
                                                        case 101: // ต้นทุนขาย Perpetual
                                                            if (__isDebit)
                                                                __debit = __total_cost;
                                                            else
                                                                __credit = __total_cost;
                                                            __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ต้นทุน, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        case 103:// สินค้าคงเหลือ Perpetual                                                    
                                                            if (__isDebit)
                                                                __debit = __total_cost;
                                                            else
                                                                __credit = __total_cost;
                                                            __addDetail = this._accountByDetail(_accountDetailType.สินค้า_ต้นทุน, this._icTransDetail, __docNo, __accountCode, __transFlagCompare, __glTemp, __getAccountDescription, __isDebit, __custCode, __custName, "");
                                                            break;
                                                        default:
                                                            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __transFlagCompare.ToString() + " " + __actionCode.ToString() + " case not found code : " + __docFormatCode + " : " + __docNo + " : " + __transFlagCompare.ToString(), false);
                                                            break;
                                                    }
                                                }
                                            }
                                            break;

                                            #endregion

                                            #endregion
                                    }
                                    //
                                    if (__addDetail == false && (__debit != 0 || __credit != 0))
                                    {
                                        // ตัวแปรย่อย
                                        __accountCode = __accountCode.Replace(" ", "");
                                        String __accountCodeFixed = "";
                                        if (__accountCode.IndexOf(',') != -1)
                                        {
                                            String[] __split = __accountCode.Split(',');
                                            __accountCode = __split[0];
                                            __accountCodeFixed = __split[1];
                                        }
                                        switch (__accountCode.ToUpper())
                                        {
                                            case "&ACCAP&": // เจ้าหนี้
                                                {
                                                    __accountCode = __accountCodeFixed;
                                                    if (this._apSupplier.Rows.Count > 0)
                                                    {
                                                        DataRow[] __find = this._apSupplier.Select(_g.d.ap_supplier._code + "=\'" + __custCode + "\'");
                                                        if (__find.Length > 0)
                                                        {
                                                            __accountCode = __find[0]["account_code"].ToString().Trim();
                                                            if (__accountCode.Length == 0)
                                                            {
                                                                __accountCode = __accountCodeFixed;
                                                            }
                                                        }
                                                    }
                                                }
                                                break;
                                            case "&ACCAR&": // ลูกหนี้
                                                {
                                                    __accountCode = __accountCodeFixed;
                                                    if (this._arCustomer.Rows.Count > 0)
                                                    {
                                                        DataRow[] __find = this._arCustomer.Select(_g.d.ar_customer._code + "=\'" + __custCode + "\'");
                                                        if (__find.Length > 0)
                                                        {
                                                            __accountCode = __find[0]["account_code"].ToString().Trim();
                                                        }
                                                        if (__accountCode.Length == 0)
                                                        {
                                                            __accountCode = __accountCodeFixed;
                                                        }
                                                    }
                                                }
                                                break;
                                        }
                                        //
                                        DataRow[] __getName = this._chatOfAccount.Select(_g.d.gl_chart_of_account._code + "=\'" + __accountCode + "\'");
                                        if (__getName.Length > 0)
                                        {
                                            __accountName = __getName[0][_g.d.gl_chart_of_account._name_1].ToString();
                                        }
                                        //
                                        __glDetailTemp._accountCode = __accountCode;
                                        __glDetailTemp._accountName = __accountName;
                                        __getAccountDescription = __getAccountDescription.Replace("&REMARK&", this._transRemark);
                                        __getAccountDescription = __getAccountDescription.Replace("&CUST_CODE&", __custCode);
                                        __getAccountDescription = __getAccountDescription.Replace("&CUST_NAME&", __custName);
                                        __getAccountDescription = __getAccountDescription.Replace("&AP_CODE&", __custCode);
                                        __getAccountDescription = __getAccountDescription.Replace("&AP_NAME&", __custName);
                                        __getAccountDescription = __getAccountDescription.Replace("&AR_CODE&", __custCode);
                                        __getAccountDescription = __getAccountDescription.Replace("&AR_NAME&", __custName);
                                        __glDetailTemp._accountDescription = __getAccountDescription;
                                        __glDetailTemp._debitOrCredit = (__isDebit) ? 0 : 1;
                                        __glDetailTemp._lineNumber = _lineNumber++;
                                        __glDetailTemp._debit = __debit;
                                        __glDetailTemp._credit = __credit;
                                        __glDetailTemp._sortStr = ((__isDebit) ? "0" : "1") + __accountCode;
                                        if (__glDetailTemp._accountCode.Trim().Length > 0)
                                        {
                                            __glTemp._glDetail.Add(__glDetailTemp);
                                            _sumDebit += __debit;
                                            _sumCredit += __credit;
                                        }
                                    }
                                }

                                string __screenName = _g.g._transFlagGlobal._transFlagByScreenCode(__screenCode).ToString();
                                if (_g.g._companyProfile._gl_trans_type == 1 && (__screenName.IndexOf("เงินสดธนาคาร") == -1 && __screenName.IndexOf("สินค้า_โอน") == -1))
                                {
                                    // คอมปาว
                                    int __addr = 0;
                                    while (__addr < __glTemp._glDetail.Count)
                                    {
                                        for (int __loopDetail = __addr + 1; __loopDetail < __glTemp._glDetail.Count; __loopDetail++)
                                        {
                                            if (__glTemp._glDetail[__addr]._accountCode.Equals(__glTemp._glDetail[__loopDetail]._accountCode) && __glTemp._glDetail[__addr]._branchCodeDetail.Equals(__glTemp._glDetail[__loopDetail]._branchCodeDetail))
                                            {
                                                // เอาไปรวม
                                                __glTemp._glDetail[__addr]._credit += __glTemp._glDetail[__loopDetail]._credit;
                                                __glTemp._glDetail[__addr]._debit += __glTemp._glDetail[__loopDetail]._debit;
                                                __glTemp._glDetail.RemoveAt(__loopDetail);
                                                __addr--;
                                                break;
                                            }
                                        }
                                        __addr++;
                                    }
                                }

                                // ตรวจผลต่าง (ไม่เกิน 0.1)
                                decimal __diff = Math.Round(_sumDebit - _sumCredit, 2);
                                if (__diff != 0 && (__diff >= -0.1M && __diff <= 0.1M))
                                {
                                    _glDetailStruct __glDetailTemp = new _glDetailStruct();
                                    __glDetailTemp._accountCode = (__diff <= 0M) ? __diffDebitCode : __diffCreditCode;
                                    DataRow[] __findChartOfAccount = this._chatOfAccount.Select("code=\'" + __glDetailTemp._accountCode + "\'");
                                    string __accountName = "";
                                    if (__findChartOfAccount.Length > 0)
                                    {
                                        __accountName = __findChartOfAccount[0][_g.d.gl_chart_of_account._name_1].ToString();
                                    }
                                    __glDetailTemp._accountName = __accountName;
                                    __glDetailTemp._accountDescription = "";
                                    __glDetailTemp._debitOrCredit = (__diff <= 0M) ? 0 : 1;
                                    __glDetailTemp._lineNumber = _lineNumber++;
                                    __glDetailTemp._debit = (__diff <= 0M) ? (__diff * -1) : 0;
                                    __glDetailTemp._credit = (__diff <= 0M) ? 0 : __diff;
                                    __glDetailTemp._sortStr = ((__diff <= 0M) ? "0" : "1") + __glDetailTemp._accountCode;
                                    if (__glDetailTemp._accountCode.Trim().Length > 0)
                                    {
                                        __glTemp._glDetail.Add(__glDetailTemp);
                                        _sumDebit += __glDetailTemp._debit;
                                        _sumCredit += __glDetailTemp._credit;
                                    }
                                }
                                //
                                __glTemp._debit = _sumDebit;
                                __glTemp._credit = _sumCredit;
                                __glTrans.Add(__glTemp);
                                if (__count >= 2000)
                                {
                                    // Save to Database                        
                                    this._saveToDatabase(__glTrans, __countAll, this._icTrans.Rows.Count);
                                    __count = 0;
                                    __glTrans = new List<_glStruct>();
                                }
                            }

                        }
                        //catch (Exception ex)
                        //{
                        //    // MessageBox.Show("Error Process : " + __docFormatCode + "\nMessage : " + ex.Message);
                        //}
                    }
                    else
                    {
                        this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, "* Not found code : " + __docFormatCode + " : " + __docNo, false);
                    }
                }
            }

            this._glTransTemp = __glTrans;
            if (__glTrans.Count > 0 && isSave)
            {
                // Save to Database
                this._saveToDatabase(__glTrans, __countAll, this._icTrans.Rows.Count);
            }

            #endregion

        }

        /*private void _processByDepartment(_accountDetailType accountType, DataTable icTransDetail, DataTable depaermentTransDetailTable, string docNo, string accountCode, string departmentcode, _g.g._transControlTypeEnum transFlagCompare, _glStruct glTemp, string accountDescription, Boolean isDebit, string custCode, string custName, string extraWhere)
        {
            _processByDist(_accountDistType.แผนก, accountType, icTransDetail, depaermentTransDetailTable, docNo, accountCode, departmentcode, transFlagCompare, glTemp, accountDescription, isDebit, custCode, custName, extraWhere);
        }

        private void _processByProject(_accountDetailType accountType, DataTable icTransDetail, DataTable depaermentTransDetailTable, string docNo, string accountCode, string departmentcode, _g.g._transControlTypeEnum transFlagCompare, _glStruct glTemp, string accountDescription, Boolean isDebit, string custCode, string custName, string extraWhere)
        {
            _processByDist(_accountDistType.โครงการ, accountType, icTransDetail, depaermentTransDetailTable, docNo, accountCode, departmentcode, transFlagCompare, glTemp, accountDescription, isDebit, custCode, custName, extraWhere);
        }

        private void _processByAllocate(_accountDetailType accountType, DataTable icTransDetail, DataTable depaermentTransDetailTable, string docNo, string accountCode, string departmentcode, _g.g._transControlTypeEnum transFlagCompare, _glStruct glTemp, string accountDescription, Boolean isDebit, string custCode, string custName, string extraWhere)
        {
            _processByDist(_accountDistType.การจัดสรร, accountType, icTransDetail, depaermentTransDetailTable, docNo, accountCode, departmentcode, transFlagCompare, glTemp, accountDescription, isDebit, custCode, custName, extraWhere);
        }*/


        string _getDistName(_accountDistType distType, string code)
        {
            string __result = "";
            switch (distType)
            {
                case _accountDistType.แผนก:
                    {
                        string __getDepartmentNameData = _g.d.erp_department_list._code + "=\'" + code + "\' ";
                        DataRow[] __getDepartmentRow = this._departmentTable.Select(__getDepartmentNameData);
                        if (__getDepartmentRow.Length > 0)
                        {
                            __result = __getDepartmentRow[0][_g.d.erp_department_list._name_1].ToString();
                        }

                    }
                    break;
                case _accountDistType.โครงการ:
                    {

                        string __getDepartmentNameData = _g.d.erp_project_list._code + "=\'" + code + "\'";
                        DataRow[] __getDepartmentRow = this._projectTable.Select(__getDepartmentNameData);
                        if (__getDepartmentRow.Length > 0)
                        {
                            __result = __getDepartmentRow[0][_g.d.erp_project_list._name_1].ToString();
                        }

                    }
                    break;
                case _accountDistType.การจัดสรร:
                    {

                        string __getDepartmentNameData = _g.d.erp_allocate_list._code + "=\'" + code + "\'";
                        DataRow[] __getAllocateRow = this._allocateTable.Select(__getDepartmentNameData);
                        if (__getAllocateRow.Length > 0)
                        {
                            __result = __getAllocateRow[0][_g.d.erp_allocate_list._name_1].ToString();
                        }

                    }
                    break;
                case _accountDistType.หน่วยงาน:
                    {

                        string __getDepartmentNameData = _g.d.erp_side_list._code + "=\'" + code + "\'";
                        DataRow[] __getAllocateRow = this._sideTable.Select(__getDepartmentNameData);
                        if (__getAllocateRow.Length > 0)
                        {
                            __result = __getAllocateRow[0][_g.d.erp_side_list._name_1].ToString();
                        }

                    }
                    break;
                case _accountDistType.งาน:
                    {

                        string __getDepartmentNameData = _g.d.erp_job_list._code + "=\'" + code + "\'";
                        DataRow[] __getAllocateRow = this._jobsTable.Select(__getDepartmentNameData);
                        if (__getAllocateRow.Length > 0)
                        {
                            __result = __getAllocateRow[0][_g.d.erp_job_list._name_1].ToString();
                        }

                    }
                    break;
            }

            return __result;
        }

        /// <summary>
        /// add detail เข้ากลุ่ม
        /// </summary>
        /// <param name="distType"></param>
        /// <param name="glTemp"></param>
        /// <param name="glDetailTemp"></param>
        void _addDistGLDetail(_accountDistType distType, _glStruct glTemp, _glDetailOtherStruct glDetailTemp)
        {
            switch (distType)
            {
                case _accountDistType.แผนก:
                    {
                        glTemp._glDepartmentDetail.Add(glDetailTemp);

                    }
                    break;
                case _accountDistType.โครงการ:
                    {
                        glTemp._glProjectDetail.Add(glDetailTemp);

                    }
                    break;
                case _accountDistType.การจัดสรร:
                    {
                        glTemp._glAllocateDetail.Add(glDetailTemp);
                    }
                    break;
                case _accountDistType.หน่วยงาน:
                    {
                        glTemp._glSideDetail.Add(glDetailTemp);
                    }
                    break;
                case _accountDistType.งาน:
                    {
                        glTemp._glJobDetail.Add(glDetailTemp);
                    }
                    break;
            }

        }

        /// <summary>
        /// ประมวลผลแบบกระจาย แผนก,โครงการ, การจัดสรร 
        /// </summary>
        /// <param name="distType">ประเภทการประมวลผล แผนก,โครงการ, การจัดสรร </param>
        /// <param name="depaermentTransDetailTable">ตารางที่ใช้ประมวลผล</param>
        /// <param name="detailStrut">รายการหลัก</param>
        /// <param name="extraWhere"></param>
        private void _processByDist(_accountDistType distType, DataTable distTransDetailTable, string docNo, string accountCode, _g.g._transControlTypeEnum transFlagCompare, string itemCode, int refRow, _glStruct glTemp, _glDetailStruct detailStrut, decimal amount, Boolean isDebit, string extraWhere)
        {
            string __masterDistCode = "";
            string __query = "";
            string __fieldGetCodeName = "";
            switch (distType)
            {
                case _accountDistType.แผนก:
                    {
                        __query = _g.d.ic_trans_detail_department._trans_flag + " = \'" + _g.g._transFlagGlobal._transFlag(transFlagCompare).ToString() + "\' and " + _g.d.ic_trans_detail_department._doc_no + " = \'" + docNo + "\' and " + _g.d.ic_trans_detail_department._doc_line_number + " = \'" + refRow.ToString() + "\' " + extraWhere;
                        __masterDistCode = this._departmentCode;
                        __fieldGetCodeName = _g.d.ic_trans_detail_department._department_code;
                    }
                    break;
                case _accountDistType.โครงการ:
                    {
                        __query = _g.d.ic_trans_detail_project._trans_flag + " = \'" + _g.g._transFlagGlobal._transFlag(transFlagCompare).ToString() + "\' and " + _g.d.ic_trans_detail_project._doc_no + " = \'" + docNo + "\' and " + _g.d.ic_trans_detail_project._doc_line_number + " = \'" + refRow.ToString() + "\' " + extraWhere;
                        __masterDistCode = this._projectCode;
                        __fieldGetCodeName = _g.d.ic_trans_detail_project._project_code;

                    }
                    break;
                case _accountDistType.การจัดสรร:
                    {
                        __query = _g.d.ic_trans_detail_allocate._trans_flag + " = \'" + _g.g._transFlagGlobal._transFlag(transFlagCompare).ToString() + "\' and " + _g.d.ic_trans_detail_allocate._doc_no + " = \'" + docNo + "\' and " + _g.d.ic_trans_detail_allocate._doc_line_number + " = \'" + refRow.ToString() + "\' " + extraWhere;
                        __masterDistCode = this._allocateCode;
                        __fieldGetCodeName = _g.d.ic_trans_detail_allocate._allocate_code;

                    }
                    break;
                case _accountDistType.หน่วยงาน:
                    {
                        __query = _g.d.ic_trans_detail_site._trans_flag + " = \'" + _g.g._transFlagGlobal._transFlag(transFlagCompare).ToString() + "\' and " + _g.d.ic_trans_detail_site._doc_no + " = \'" + docNo + "\' and " + _g.d.ic_trans_detail_site._doc_line_number + " = \'" + refRow.ToString() + "\' " + extraWhere;
                        __masterDistCode = this._siteCode;
                        __fieldGetCodeName = _g.d.ic_trans_detail_site._site_code;

                    }
                    break;
                case _accountDistType.งาน:
                    {
                        __query = _g.d.ic_trans_detail_jobs._trans_flag + " = \'" + _g.g._transFlagGlobal._transFlag(transFlagCompare).ToString() + "\' and " + _g.d.ic_trans_detail_jobs._doc_no + " = \'" + docNo + "\' and " + _g.d.ic_trans_detail_jobs._doc_line_number + " = \'" + refRow.ToString() + "\' " + extraWhere;
                        __masterDistCode = this._jobCode;
                        __fieldGetCodeName = _g.d.ic_trans_detail_jobs._job_code;

                    }
                    break;
            }

            DataRow[] __getDistTransDetail = (distTransDetailTable.Rows.Count == 0) ? null : distTransDetailTable.Select(__query);

            if ((__getDistTransDetail != null && __getDistTransDetail.Length > 0) || __masterDistCode.Length > 0)
            {
                if (__getDistTransDetail != null && __getDistTransDetail.Length > 0)
                {
                    for (int __rowDist = 0; __rowDist < __getDistTransDetail.Length; __rowDist++)
                    {
                        decimal __getPercent = MyLib._myGlobal._decimalPhase(__getDistTransDetail[__rowDist][_g.d.ic_trans_detail_department._ratio].ToString());
                        decimal __getAmount = MyLib._myGlobal._decimalPhase(__getDistTransDetail[__rowDist][_g.d.ic_trans_detail_department._amount].ToString());
                        string __getPartCode = __getDistTransDetail[__rowDist][__fieldGetCodeName].ToString();

                        string __getPartName = _getDistName(distType, __getPartCode);

                        _glDetailOtherStruct __glDetailTemp = new _glDetailOtherStruct();
                        __glDetailTemp._accountCode = detailStrut._accountCode;
                        __glDetailTemp._accountName = detailStrut._accountName;
                        __glDetailTemp._code = __getPartCode;
                        __glDetailTemp._name_1 = __getPartName;
                        __glDetailTemp._accountDescription = detailStrut._accountDescription;
                        __glDetailTemp._debitOrCredit = (isDebit) ? 0 : 1;
                        __glDetailTemp._lineNumber = detailStrut._lineNumber;
                        //__glDetailTemp._debit = __debit;
                        //__glDetailTemp._credit = __credit;
                        __glDetailTemp._percent = __getPercent;
                        __glDetailTemp._amount = __getAmount;
                        __glDetailTemp._sortStr = detailStrut._sortStr;
                        __glDetailTemp._lineNumberDetail = __rowDist; //__detail;

                        _addDistGLDetail(distType, glTemp, __glDetailTemp);
                    }

                }
                else if (__masterDistCode.Length > 0)
                {
                    string __getPartName = _getDistName(distType, __masterDistCode);

                    _glDetailOtherStruct __glDetailTemp = new _glDetailOtherStruct();
                    __glDetailTemp._accountCode = detailStrut._accountCode;
                    __glDetailTemp._accountName = detailStrut._accountName;
                    __glDetailTemp._code = __masterDistCode;
                    __glDetailTemp._name_1 = __getPartName;
                    __glDetailTemp._accountDescription = detailStrut._accountDescription;
                    __glDetailTemp._debitOrCredit = (isDebit) ? 0 : 1;
                    __glDetailTemp._lineNumber = detailStrut._lineNumber;
                    //__glDetailTemp._debit = __debit;
                    //__glDetailTemp._credit = __credit;
                    __glDetailTemp._percent = 100;
                    __glDetailTemp._amount = amount;
                    __glDetailTemp._sortStr = detailStrut._sortStr;
                    __glDetailTemp._lineNumberDetail = _lineNumberDepartment++; //__detail;
                    _addDistGLDetail(distType, glTemp, __glDetailTemp);
                }
            }
        }

        /// <summary>
        /// ผิด ๆ ประมวลผลแบบกระจาย แผนก,โครงการ, การจัดสรร 
        /// </summary>
        /// <param name="accountType"></param>
        /// <param name="icTransDetail"></param>
        /// <param name="depaermentTransDetailTable"></param>
        /// <param name="docNo"></param>
        /// <param name="accountCode"></param>
        /// <param name="departmentcode"></param>
        /// <param name="transFlagCompare"></param>
        /// <param name="glTemp"></param>
        /// <param name="accountDescription"></param>
        /// <param name="isDebit"></param>
        /// <param name="custCode"></param>
        /// <param name="custName"></param>
        /// <param name="extraWhere"></param>
        /// <returns></returns>
        private bool _processByDist(_accountDistType distType, _accountDetailType accountType, DataTable icTransDetail, DataTable depaermentTransDetailTable, string docNo, string accountCode, string departmentcode, _g.g._transControlTypeEnum transFlagCompare, _glStruct glTemp, string accountDescription, Boolean isDebit, string custCode, string custName, string extraWhere)
        {
            _lineNumberDepartment = 1;
            accountCode = accountCode.Replace(" ", "");
            if (accountCode.IndexOf("&") == -1 || icTransDetail.Rows.Count == 0)
            {
                // กรณีไม่ระบุตัวแปร ให้ return false
                return false;
            }

            String __accountCodeFixed = "";
            if (accountCode.IndexOf(',') != -1)
            {
                String[] __split = accountCode.Split(',');
                accountCode = __split[0];
                __accountCodeFixed = __split[1];
            }

            Boolean __foundByDetail = false;

            string __query = _g.d.ic_trans_detail._trans_flag + " = " + _g.g._transFlagGlobal._transFlag(transFlagCompare).ToString() + " and " + _g.d.ic_trans_detail._doc_no + " = \'" + docNo + "\' " + extraWhere;
            DataRow[] __rowDetail = icTransDetail.Rows.Count == 0 ? null : icTransDetail.Select(__query);
            if (__rowDetail != null && __rowDetail.Length > 0)
            {
                for (int __detail = 0; __detail < __rowDetail.Length; __detail++)
                {
                    string __accountCodeDetail = "";
                    string __accountDescriptionTemp = accountDescription;
                    decimal __amount = 0.0M;

                    int __line_number = MyLib._myGlobal._intPhase(__rowDetail[__detail][_g.d.ic_trans_detail._line_number].ToString());
                    string __item_code = __rowDetail[__detail][_g.d.ic_trans_detail._item_code].ToString();

                    /* 
                     * step
                     * 
                     * 1 หาผังบัญชีรายการย่อยก่อน
                     * 2 เมื่อได้ผังแล้วไปเฉลี่ยรายการ เข้าไปที่แผนก แล้ว add เข้า GL Detail
                     * 
                    */

                    //  step  1 หาผังบัญชีรายการย่อยก่อน

                    #region Step  1 หาผังบัญชีรายการย่อยก่อน
                    switch (accountType)
                    {
                        #region ธนาคาร_โอนเงินระหว่างธนาคาร_ออก
                        case _accountDetailType.ธนาคาร_โอนเงินระหว่างธนาคาร_ออก:
                            {
                                string __passBookCode = __rowDetail[__detail]["pass_book_code"].ToString().Trim();
                                DataRow[] __selectAccountData = _passBook.Select(_g.d.erp_pass_book._code + " = \'" + __passBookCode + "\'");
                                if (__selectAccountData.Length > 0)
                                {
                                    decimal __amountCalc = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._transfer_amount].ToString()) + MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._fee_amount].ToString());
                                    switch (accountCode.ToUpper())
                                    {
                                        case "&ACCBANK1&":
                                            __accountCodeDetail = __selectAccountData[0][_g.d.erp_pass_book._account_code_1].ToString().Trim().ToUpper();
                                            __amount = __amountCalc;
                                            break;
                                        case "&ACCBANK2&":
                                            __accountCodeDetail = __selectAccountData[0][_g.d.erp_pass_book._account_code_2].ToString().Trim().ToUpper();
                                            __amount = __amountCalc;
                                            break;
                                    }
                                }
                            }
                            break;
                        #endregion
                        #region ธนาคาร_โอนเงินระหว่างธนาคาร_เข้า
                        case _accountDetailType.ธนาคาร_โอนเงินระหว่างธนาคาร_เข้า:
                            {
                                string __passBookCode = __rowDetail[__detail]["pass_book_code"].ToString().Trim();
                                DataRow[] __selectAccountData = _passBook.Select(_g.d.erp_pass_book._code + " = \'" + __passBookCode + "\'");
                                if (__selectAccountData.Length > 0)
                                {
                                    switch (accountCode.ToUpper())
                                    {
                                        case "&ACCBANK1&":
                                            __accountCodeDetail = __selectAccountData[0][_g.d.erp_pass_book._account_code_1].ToString().Trim().ToUpper();
                                            __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._transfer_amount].ToString());
                                            break;
                                        case "&ACCBANK2&":
                                            __accountCodeDetail = __selectAccountData[0][_g.d.erp_pass_book._account_code_2].ToString().Trim().ToUpper();
                                            __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._transfer_amount].ToString());
                                            break;
                                    }
                                }
                            }
                            break;
                        #endregion
                        #region ธนาคาร_บัตรเครดิต
                        case _accountDetailType.ธนาคาร_บัตรเครดิต:
                            {
                                string __accountCode = __rowDetail[__detail]["credit_card_ref"].ToString().Trim();
                                if (this._creditCardAccount.Rows.Count > 0)
                                {
                                    DataRow[] __selectData = this._creditCardAccount.Select(_g.d.erp_credit_type._code + " = \'" + __accountCode + "\'");
                                    switch (accountCode.ToUpper())
                                    {
                                        case "&ACCBANK&":
                                            if (__selectData.Length > 0 && __selectData.Length > 0)
                                            {
                                                __accountCodeDetail = __selectData[0][_g.d.erp_credit_type._account_code].ToString().Trim();
                                                __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._sum_amount].ToString());
                                            }
                                            break;
                                    }
                                }
                            }
                            break;
                        #endregion
                        #region ธนาคาร_เช็ครับล่วงหน้า
                        case _accountDetailType.ธนาคาร_เช็ครับล่วงหน้า:
                            {
                                string __accountCode = __rowDetail[__detail]["chq_bank_ref"].ToString().Trim();
                                if (this._bankIncomeAccount.Rows.Count > 0)
                                {
                                    DataRow[] __selectData = this._bankIncomeAccount.Select(_g.d.erp_bank._code + " = \'" + __accountCode + "\'");
                                    switch (accountCode.ToUpper())
                                    {
                                        case "&ACCBANK&":
                                            if (__selectData.Length > 0 && __selectData.Length > 0)
                                            {
                                                __accountCodeDetail = __selectData[0][_g.d.erp_bank._chq_income_account_code].ToString().Trim();
                                                __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._price].ToString());
                                            }
                                            break;
                                    }
                                }
                            }
                            break;
                        #endregion
                        #region ธนาคาร_เช็คจ่ายล่วงหน้า
                        case _accountDetailType.ธนาคาร_เช็คจ่ายล่วงหน้า:
                            {
                                // ถอนเงิน,ฝากเงิน
                                string __passBookCode = __rowDetail[__detail][_g.d.cb_trans_detail._pass_book_code].ToString().Trim();
                                if (_passBook.Rows.Count > 0)
                                {
                                    DataRow[] __selectData = _passBook.Select(_g.d.erp_pass_book._code + " = \'" + __passBookCode + "\'");
                                    switch (accountCode.ToUpper())
                                    {
                                        case "&ACCBANK1&":
                                            if (__selectData.Length > 0 && __selectData.Length > 0)
                                            {
                                                __accountCodeDetail = __selectData[0][_g.d.erp_pass_book._account_code_1].ToString().Trim();
                                                __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.cb_trans_detail._amount].ToString());
                                            }
                                            break;
                                        case "&ACCBANK2&":
                                            if (__selectData.Length > 0)
                                            {
                                                __accountCodeDetail = __selectData[0][_g.d.erp_pass_book._account_code_2].ToString().Trim();
                                                __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.cb_trans_detail._amount].ToString());
                                            }
                                            break;
                                    }
                                }
                            }
                            break;
                        #endregion
                        #region ค่าใช้จ่ายอื่น
                        case _accountDetailType.ค่าใช้จ่ายอื่น:
                            {
                                string __accountCode = __rowDetail[__detail][_g.d.cb_trans_detail._pass_book_code].ToString().Trim();
                                if (this._otherExpense.Rows.Count > 0)
                                {
                                    DataRow[] __selectData = this._otherExpense.Select(_g.d.erp_expenses_list._code + " = \'" + __accountCode + "\'");
                                    switch (accountCode.ToUpper())
                                    {
                                        case "&ACC&":
                                            if (__selectData.Length > 0 && __selectData.Length > 0)
                                            {
                                                __accountCodeDetail = __selectData[0][_g.d.erp_expenses_list._gl_account_code].ToString().Trim();
                                                __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail]["sum_amount_exclude_vat"].ToString());
                                                //__amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.cb_trans_detail._amount].ToString());
                                            }
                                            break;
                                    }
                                }
                            }
                            break;
                        #endregion
                        #region รายได้อื่น
                        case _accountDetailType.รายได้อื่น:
                            {
                                string __accountCode = __rowDetail[__detail][_g.d.cb_trans_detail._pass_book_code].ToString().Trim();
                                if (this._otherIncome.Rows.Count > 0)
                                {
                                    DataRow[] __selectData = this._otherIncome.Select(_g.d.erp_income_list._code + " = \'" + __accountCode + "\'");
                                    switch (accountCode.ToUpper())
                                    {
                                        case "&ACC&":
                                            if (__selectData.Length > 0 && __selectData.Length > 0)
                                            {
                                                __accountCodeDetail = __selectData[0][_g.d.erp_income_list._gl_account_code].ToString().Trim();
                                                __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail]["sum_amount_exclude_vat"].ToString());
                                                //__amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.cb_trans_detail._amount].ToString());
                                            }
                                            break;
                                    }
                                }
                            }
                            break;
                        #endregion
                        #region สินค้า_ราคา
                        case _accountDetailType.สินค้า_ราคา:
                            {
                                string __itemCode = __rowDetail[__detail][_g.d.ic_trans_detail._item_code].ToString().Trim();
                                if (_icInventory.Rows.Count > 0)
                                {
                                    DataRow[] __selectData = _icInventory.Select(_g.d.ic_inventory._code + " = \'" + __itemCode + "\'");
                                    if (__selectData.Length > 0)
                                    {
                                        //__lastItemCode = __selectData[0][_g.d.ic_inventory._code].ToString().Trim();
                                        //__lastItemName = __selectData[0][_g.d.ic_inventory._name_1].ToString().Trim();
                                        switch (accountCode.ToUpper())
                                        {
                                            case "&ACC1&":
                                                {
                                                    __accountCodeDetail = __selectData[0][_g.d.ic_inventory._account_code_1].ToString().Trim();
                                                    if (__accountCodeDetail.Length == 0)
                                                    {
                                                        __accountCodeDetail = __accountCodeFixed;
                                                    }
                                                    __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._sum_amount_exclude_vat].ToString());
                                                }
                                                break;
                                            case "&ACC2&":
                                                {
                                                    __accountCodeDetail = __selectData[0][_g.d.ic_inventory._account_code_2].ToString().Trim();
                                                    if (__accountCodeDetail.Length == 0)
                                                    {
                                                        __accountCodeDetail = __accountCodeFixed;
                                                    }
                                                    __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._sum_amount_exclude_vat].ToString());
                                                }
                                                break;
                                            case "&ACC3&":
                                                {
                                                    __accountCodeDetail = __selectData[0][_g.d.ic_inventory._account_code_3].ToString().Trim();
                                                    if (__accountCodeDetail.Length == 0)
                                                    {
                                                        __accountCodeDetail = __accountCodeFixed;
                                                    }
                                                    __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._sum_amount_exclude_vat].ToString());
                                                }
                                                break;
                                            case "&ACC4&":
                                                {
                                                    __accountCodeDetail = __selectData[0][_g.d.ic_inventory._account_code_4].ToString().Trim();
                                                    if (__accountCodeDetail.Length == 0)
                                                    {
                                                        __accountCodeDetail = __accountCodeFixed;
                                                    }
                                                    __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._sum_amount_exclude_vat].ToString());
                                                }
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                }
                                if (__accountCodeDetail.Length == 0)
                                {
                                    __accountCodeDetail = "blank";
                                    __accountDescriptionTemp = __itemCode;
                                }
                            }
                            break;
                        #endregion
                        #region สินค้า_ต้นทุน
                        case _accountDetailType.สินค้า_ต้นทุน:
                            {
                                string __itemCode = __rowDetail[__detail][_g.d.ic_trans_detail._item_code].ToString().Trim();
                                if (_icInventory.Rows.Count > 0)
                                {
                                    DataRow[] __selectData = _icInventory.Select(_g.d.ic_inventory._code + " = \'" + __itemCode + "\'");
                                    if (__selectData.Length > 0)
                                    {
                                        //__lastItemCode = __selectData[0][_g.d.ic_inventory._code].ToString().Trim();
                                        //__lastItemName = __selectData[0][_g.d.ic_inventory._name_1].ToString().Trim();
                                        switch (accountCode.ToUpper())
                                        {
                                            case "&ACC1&":
                                                {
                                                    __accountCodeDetail = __selectData[0][_g.d.ic_inventory._account_code_1].ToString().Trim();
                                                    if (__accountCodeDetail.Length == 0)
                                                    {
                                                        __accountCodeDetail = __accountCodeFixed;
                                                    }
                                                    __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._sum_of_cost].ToString());
                                                }
                                                break;
                                            case "&ACC2&":
                                                {
                                                    __accountCodeDetail = __selectData[0][_g.d.ic_inventory._account_code_2].ToString().Trim();
                                                    if (__accountCodeDetail.Length == 0)
                                                    {
                                                        __accountCodeDetail = __accountCodeFixed;
                                                    }
                                                    __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._sum_of_cost].ToString());
                                                }
                                                break;
                                            case "&ACC3&":
                                                {
                                                    __accountCodeDetail = __selectData[0][_g.d.ic_inventory._account_code_3].ToString().Trim();
                                                    if (__accountCodeDetail.Length == 0)
                                                    {
                                                        __accountCodeDetail = __accountCodeFixed;
                                                    }
                                                    __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._sum_amount_exclude_vat].ToString());
                                                }
                                                break;
                                            case "&ACC4&":
                                                {
                                                    __accountCodeDetail = __selectData[0][_g.d.ic_inventory._account_code_4].ToString().Trim();
                                                    if (__accountCodeDetail.Length == 0)
                                                    {
                                                        __accountCodeDetail = __accountCodeFixed;
                                                    }
                                                    __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._sum_amount_exclude_vat].ToString());
                                                }
                                                break;
                                        }
                                    }
                                }
                                if (__accountCodeDetail.Length == 0)
                                {
                                    __accountCodeDetail = "blank";
                                    __accountDescriptionTemp = __itemCode;
                                }
                            }
                            break;
                        #endregion
                        #region เงินสดย่อย
                        case _accountDetailType.เงินสดย่อย:
                            {
                                string __pettyCashCode = __rowDetail[__detail][_g.d.cb_trans_detail._trans_number].ToString().Trim();
                                if (this._pettyCash.Rows.Count > 0)
                                {
                                    DataRow[] __selectData = this._pettyCash.Select(_g.d.cb_petty_cash._code + " = \'" + __pettyCashCode + "\'");
                                    switch (accountCode.ToUpper())
                                    {
                                        case "&ACC&":
                                            if (__selectData.Length > 0 && __selectData.Length > 0)
                                            {
                                                __accountCodeDetail = __selectData[0][_g.d.cb_petty_cash._chart_of_account].ToString().Trim();
                                                __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.cb_trans_detail._amount].ToString());
                                            }
                                            break;
                                    }
                                }
                            }
                            break;
                        #endregion
                        #region รายวันเงินสดย่อย
                        case _accountDetailType.รายวันเงินสดย่อย:
                            {
                                string __pettyCashCode = __rowDetail[__detail][_g.d.cb_trans_detail._pass_book_code].ToString().Trim();
                                if (this._pettyCash.Rows.Count > 0)
                                {
                                    DataRow[] __selectData = this._pettyCash.Select(_g.d.cb_petty_cash._code + " = \'" + __pettyCashCode + "\'");
                                    switch (accountCode.ToUpper())
                                    {
                                        case "&ACC&":
                                            if (__selectData.Length > 0 && __selectData.Length > 0)
                                            {
                                                __accountCodeDetail = __selectData[0][_g.d.cb_petty_cash._chart_of_account].ToString().Trim();
                                                __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.cb_trans_detail._amount].ToString());
                                            }
                                            break;
                                    }
                                }
                            }
                            break;
                        #endregion
                        #region ธนาคาร
                        case _accountDetailType.ธนาคาร:
                            {
                                // ถอนเงิน,ฝากเงิน
                                string __passBookCode = __rowDetail[__detail][_g.d.cb_trans_detail._pass_book_code].ToString().Trim();
                                if (_passBook.Rows.Count > 0)
                                {
                                    DataRow[] __selectData = _passBook.Select(_g.d.erp_pass_book._code + " = \'" + __passBookCode + "\'");
                                    switch (accountCode.ToUpper())
                                    {
                                        case "&ACCBANK1&":
                                            if (__selectData.Length > 0 && __selectData.Length > 0)
                                            {
                                                __accountCodeDetail = __selectData[0][_g.d.erp_pass_book._account_code_1].ToString().Trim();
                                                __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.cb_trans_detail._amount].ToString());
                                            }
                                            break;
                                        case "&ACCBANK2&":
                                            if (__selectData.Length > 0)
                                            {
                                                __accountCodeDetail = __selectData[0][_g.d.erp_pass_book._account_code_2].ToString().Trim();
                                                __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.cb_trans_detail._amount].ToString());
                                            }
                                            break;
                                    }
                                }
                            }
                            break;
                        #endregion
                        #region ธนาคาร_เช็ครับคืน
                        case _accountDetailType.ธนาคาร_เช็ครับคืน:
                            {
                                decimal __amountCalc = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._sum_amount].ToString()) + MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.ic_trans_detail._sum_of_cost].ToString());
                                string __passBookCode = __rowDetail[__detail][_g.d.cb_trans_detail._pass_book_code].ToString().Trim();
                                if (_passBook.Rows.Count > 0)
                                {
                                    DataRow[] __selectData = _passBook.Select(_g.d.erp_pass_book._code + " = \'" + __passBookCode + "\'");
                                    switch (accountCode.ToUpper())
                                    {
                                        case "&ACCBANK1&":
                                            if (__selectData.Length > 0 && __selectData.Length > 0)
                                            {
                                                __accountCodeDetail = __selectData[0][_g.d.erp_pass_book._account_code_1].ToString().Trim();
                                                __amount = __amountCalc;
                                            }
                                            break;
                                        case "&ACCBANK2&":
                                            if (__selectData.Length > 0)
                                            {
                                                __accountCodeDetail = __selectData[0][_g.d.erp_pass_book._account_code_2].ToString().Trim();
                                                __amount = __amountCalc;
                                            }
                                            break;
                                    }
                                }
                            }
                            break;
                        #endregion
                        #region เช็ครับ
                        case _accountDetailType.เช็ครับ:
                            {
                                string __itemCode = __rowDetail[__detail][_g.d.cb_trans_detail._pass_book_code].ToString().Trim();
                                if (_passBook.Rows.Count > 0)
                                {
                                    DataRow[] __selectData1 = _passBook.Select(_g.d.erp_pass_book._code + " = \'" + __itemCode + "\'");
                                    DataRow[] __selectData2 = _passBook.Select(_g.d.erp_pass_book._code + " = \'" + __rowDetail[__detail][_g.d.cb_trans_detail._trans_number].ToString().Trim() + "\'");
                                    switch (accountCode.ToUpper())
                                    {
                                        case "&ACCBANK1&":
                                            if (__selectData2.Length > 0 && __selectData2.Length > 0)
                                            {
                                                __accountCodeDetail = __selectData2[0][_g.d.erp_pass_book._account_code_1].ToString().Trim();
                                                __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.cb_trans_detail._amount].ToString());
                                            }
                                            break;
                                        case "&ACCBANK2&":
                                            if (__selectData1.Length > 0)
                                            {
                                                __accountCodeDetail = __selectData1[0][_g.d.erp_pass_book._account_code_2].ToString().Trim();
                                                __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.cb_trans_detail._amount].ToString());
                                            }
                                            break;
                                    }
                                }
                            }
                            break;
                        #endregion
                        #region เช็คจ่าย
                        case _accountDetailType.เช็คจ่าย:
                            {
                                string __itemCode = __rowDetail[__detail][_g.d.cb_trans_detail._pass_book_code].ToString().Trim();
                                if (_passBook.Rows.Count > 0)
                                {
                                    DataRow[] __selectData1 = _passBook.Select(_g.d.erp_pass_book._code + " = \'" + __itemCode + "\'");
                                    DataRow[] __selectData2 = _passBook.Select(_g.d.erp_pass_book._code + " = \'" + __rowDetail[__detail][_g.d.cb_trans_detail._trans_number].ToString().Trim() + "\'");
                                    switch (accountCode.ToUpper())
                                    {
                                        case "&ACCBANK1&":
                                            if (__selectData2.Length > 0 && __selectData2.Length > 0)
                                            {
                                                __accountCodeDetail = __selectData2[0][_g.d.erp_pass_book._account_code_1].ToString().Trim();
                                                __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.cb_trans_detail._amount].ToString());
                                            }
                                            break;
                                        case "&ACCBANK2&":
                                            if (__selectData1.Length > 0)
                                            {
                                                __accountCodeDetail = __selectData1[0][_g.d.erp_pass_book._account_code_2].ToString().Trim();
                                                __amount = MyLib._myGlobal._decimalPhase(__rowDetail[__detail][_g.d.cb_trans_detail._amount].ToString());
                                            }
                                            break;
                                    }
                                }
                            }
                            break;
                            #endregion
                    }

                    #endregion

                    #region Setp 2 เมื่อได้ผังแล้วไปเฉลี่ยรายการ เข้าไปที่แผนก
                    if (__accountCodeDetail.Length > 0 || __rowDetail[__detail][_g.d.cb_trans_detail._remark].ToString().Trim().Length > 0)
                    {
                        __foundByDetail = true;
                        decimal __debit = 0.0M;
                        decimal __credit = 0.0M;

                        string __accountName = "";

                        DataRow[] __findChartOfAccount = this._chatOfAccount.Select("code=\'" + __accountCodeDetail.Trim() + "\'");
                        if (__findChartOfAccount.Length > 0)
                        {
                            __accountName = __findChartOfAccount[0][_g.d.gl_chart_of_account._name_1].ToString();
                        }

                        //__glDetailTemp._accountCode = __accountCodeDetail;

                        string __getAccountDescription = __accountDescriptionTemp;
                        try
                        {
                            __getAccountDescription = __getAccountDescription.Replace("&ACC_NAME&", ((_g.g._companyProfile._gl_trans_type == 0) ? __accountName : ""));
                        }
                        catch
                        {
                        }

                        // remark ได้ทั้ง 2 case
                        StringBuilder __remark = new StringBuilder(this._transRemark.Trim() + " " + __rowDetail[__detail][_g.d.cb_trans_detail._remark].ToString());
                        while (__remark.Length > 0 && __remark[0] == ' ')
                        {
                            __remark.Remove(0, 1);
                        }
                        __getAccountDescription = __getAccountDescription.Replace("&REMARK&", __remark.ToString());
                        __getAccountDescription = __getAccountDescription.Replace("&CUST_CODE&", custCode);
                        __getAccountDescription = __getAccountDescription.Replace("&CUST_NAME&", custName);
                        __getAccountDescription = __getAccountDescription.Replace("&AP_CODE&", custCode);
                        __getAccountDescription = __getAccountDescription.Replace("&AP_NAME&", custName);
                        __getAccountDescription = __getAccountDescription.Replace("&AR_CODE&", custCode);
                        __getAccountDescription = __getAccountDescription.Replace("&AR_NAME&", custName);

                        if (isDebit)
                            __debit = __amount;
                        else
                            __credit = __amount;

                        if (__accountCodeDetail.Trim().Length > 0 && (__debit != 0 || __credit != 0))
                        {
                            switch (distType)
                            {
                                case _accountDistType.แผนก:
                                    {
                                        // ดึงรายละเอียดการเฉลี่ย ตาม ไอเท็ม,แผนก  filter department by doc_no, item_code 
                                        __query = _g.d.ic_trans_detail_department._trans_flag + " = " + _g.g._transFlagGlobal._transFlag(transFlagCompare).ToString() + " and " + _g.d.ic_trans_detail_department._doc_no + " = \'" + docNo + "\' " + extraWhere;
                                        DataRow[] __getDepartmentDetail = (depaermentTransDetailTable.Rows.Count == 0) ? null : depaermentTransDetailTable.Select(__query);

                                        if ((__getDepartmentDetail != null && __getDepartmentDetail.Length > 0) || departmentcode.Length > 0)
                                        {
                                            // step 2 หารายการแผนก 
                                            if (__getDepartmentDetail != null && __getDepartmentDetail.Length > 0)
                                            {
                                                // เฉลี่ยรายการ ไปแต่ละแผนก
                                                for (int __rowDepartment = 0; __rowDepartment < __getDepartmentDetail.Length; __rowDepartment++)
                                                {
                                                    decimal __getPercent = MyLib._myGlobal._decimalPhase(__getDepartmentDetail[__rowDepartment][_g.d.ic_trans_detail_department._ratio].ToString());
                                                    decimal __getAmount = MyLib._myGlobal._decimalPhase(__getDepartmentDetail[__rowDepartment][_g.d.ic_trans_detail_department._amount].ToString());
                                                    string __getDepartmentCode = __getDepartmentDetail[__rowDepartment][_g.d.ic_trans_detail_department._department_code].ToString();

                                                    string __departmentName = "";

                                                    string __getDepartmentNameData = _g.d.erp_department_list._code + "=\'" + __getDepartmentCode + "\'";
                                                    DataRow[] __getDepartmentRow = this._departmentTable.Select(__getDepartmentNameData);
                                                    if (__getDepartmentRow.Length > 0)
                                                    {
                                                        __departmentName = __getDepartmentRow[0][_g.d.erp_department_list._name_1].ToString();
                                                    }
                                                    //if (isDebit)
                                                    //    __debit = __getAmount;
                                                    //else
                                                    //    __credit = __getAmount;

                                                    _glDetailOtherStruct __glDetailTemp = new _glDetailOtherStruct();
                                                    __glDetailTemp._accountCode = __accountCodeDetail;
                                                    __glDetailTemp._accountName = __accountName;
                                                    __glDetailTemp._code = __getDepartmentCode;
                                                    __glDetailTemp._name_1 = __departmentName;
                                                    __glDetailTemp._accountDescription = __getAccountDescription;
                                                    __glDetailTemp._debitOrCredit = (isDebit) ? 0 : 1;
                                                    __glDetailTemp._lineNumber = __detail + 1; // _lineNumberDepartment++;
                                                    //__glDetailTemp._debit = __debit;
                                                    //__glDetailTemp._credit = __credit;
                                                    __glDetailTemp._percent = __getPercent;
                                                    __glDetailTemp._amount = __getAmount;
                                                    __glDetailTemp._sortStr = ((isDebit) ? "0" : "1") + __accountCodeDetail;
                                                    __glDetailTemp._lineNumberDetail = _lineNumberDepartment++; //__detail;
                                                    glTemp._glDepartmentDetail.Add(__glDetailTemp);
                                                }
                                            }
                                            else if (departmentcode.Length > 0)
                                            {
                                                string __departmentName = "";

                                                string __getDepartmentNameData = _g.d.erp_department_list._code + "=\'" + departmentcode + "\' ";
                                                DataRow[] __getDepartmentRow = this._departmentTable.Select(__getDepartmentNameData);
                                                if (__getDepartmentRow.Length > 0)
                                                {
                                                    __departmentName = __getDepartmentRow[0][_g.d.erp_department_list._name_1].ToString();
                                                }

                                                // ไม่เฉลี่ย เอาไปลงแผนกเต็มจำนวน
                                                _glDetailOtherStruct __glDetailTemp = new _glDetailOtherStruct();
                                                __glDetailTemp._accountCode = __accountCodeDetail;
                                                __glDetailTemp._accountName = __accountName;
                                                __glDetailTemp._code = departmentcode;
                                                __glDetailTemp._name_1 = __departmentName;
                                                __glDetailTemp._accountDescription = __getAccountDescription;
                                                __glDetailTemp._debitOrCredit = (isDebit) ? 0 : 1;
                                                __glDetailTemp._lineNumber = __detail + 1; // _lineNumberDepartment++;
                                                //__glDetailTemp._debit = __debit;
                                                //__glDetailTemp._credit = __credit;
                                                __glDetailTemp._percent = 100;
                                                __glDetailTemp._amount = __amount;
                                                __glDetailTemp._sortStr = ((isDebit) ? "0" : "1") + __accountCodeDetail;
                                                __glDetailTemp._lineNumberDetail = _lineNumberDepartment++; //__detail;
                                                glTemp._glDepartmentDetail.Add(__glDetailTemp);
                                            }
                                        }
                                    }
                                    break;
                                case _accountDistType.โครงการ:
                                    {
                                        // ดึงรายละเอียดการเฉลี่ย ตาม ไอเท็ม,แผนก  filter department by doc_no, item_code 
                                        __query = _g.d.ic_trans_detail_project._trans_flag + " = " + _g.g._transFlagGlobal._transFlag(transFlagCompare).ToString() + " and " + _g.d.ic_trans_detail_project._doc_no + " = \'" + docNo + "\' " + extraWhere;
                                        DataRow[] __getProjectDetail = (depaermentTransDetailTable.Rows.Count == 0) ? null : depaermentTransDetailTable.Select(__query);

                                        if ((__getProjectDetail != null && __getProjectDetail.Length > 0) || departmentcode.Length > 0)
                                        {
                                            // step 2 หารายการแผนก 
                                            if (__getProjectDetail != null && __getProjectDetail.Length > 0)
                                            {
                                                // เฉลี่ยรายการ ไปแต่ละแผนก
                                                for (int __rowDepartment = 0; __rowDepartment < __getProjectDetail.Length; __rowDepartment++)
                                                {
                                                    decimal __getPercent = MyLib._myGlobal._decimalPhase(__getProjectDetail[__rowDepartment][_g.d.ic_trans_detail_project._ratio].ToString());
                                                    decimal __getAmount = MyLib._myGlobal._decimalPhase(__getProjectDetail[__rowDepartment][_g.d.ic_trans_detail_project._amount].ToString());
                                                    string __getDepartmentCode = __getProjectDetail[__rowDepartment][_g.d.ic_trans_detail_project._project_code].ToString();

                                                    string __departmentName = "";

                                                    string __getDepartmentNameData = _g.d.erp_project_list._code + "=\'" + __getDepartmentCode + "\'";
                                                    DataRow[] __getDepartmentRow = this._projectTable.Select(__getDepartmentNameData);
                                                    if (__getDepartmentRow.Length > 0)
                                                    {
                                                        __departmentName = __getDepartmentRow[0][_g.d.erp_project_list._name_1].ToString();
                                                    }
                                                    //if (isDebit)
                                                    //    __debit = __getAmount;
                                                    //else
                                                    //    __credit = __getAmount;

                                                    _glDetailOtherStruct __glDetailTemp = new _glDetailOtherStruct();
                                                    __glDetailTemp._accountCode = __accountCodeDetail;
                                                    __glDetailTemp._accountName = __accountName;
                                                    __glDetailTemp._code = __getDepartmentCode;
                                                    __glDetailTemp._name_1 = __departmentName;
                                                    __glDetailTemp._accountDescription = __getAccountDescription;
                                                    __glDetailTemp._debitOrCredit = (isDebit) ? 0 : 1;
                                                    __glDetailTemp._lineNumber = (_lineNumber - 1);  // _lineNumberDepartment++;
                                                    //__glDetailTemp._debit = __debit;
                                                    //__glDetailTemp._credit = __credit;
                                                    __glDetailTemp._percent = __getPercent;
                                                    __glDetailTemp._amount = __getAmount;
                                                    __glDetailTemp._sortStr = ((isDebit) ? "0" : "1") + __accountCodeDetail;
                                                    __glDetailTemp._lineNumberDetail = _lineNumberDepartment++; //__detail;
                                                    glTemp._glProjectDetail.Add(__glDetailTemp);
                                                }
                                            }
                                            else if (departmentcode.Length > 0)
                                            {
                                                string __departmentName = "";

                                                string __getDepartmentNameData = _g.d.erp_project_list._code + "=\'" + departmentcode + "\' ";
                                                DataRow[] __getDepartmentRow = this._projectTable.Select(__getDepartmentNameData);
                                                if (__getDepartmentRow.Length > 0)
                                                {
                                                    __departmentName = __getDepartmentRow[0][_g.d.erp_project_list._name_1].ToString();
                                                }

                                                // ไม่เฉลี่ย เอาไปลงแผนกเต็มจำนวน
                                                _glDetailOtherStruct __glDetailTemp = new _glDetailOtherStruct();
                                                __glDetailTemp._accountCode = __accountCodeDetail;
                                                __glDetailTemp._accountName = __accountName;
                                                __glDetailTemp._code = departmentcode;
                                                __glDetailTemp._name_1 = __departmentName;
                                                __glDetailTemp._accountDescription = __getAccountDescription;
                                                __glDetailTemp._debitOrCredit = (isDebit) ? 0 : 1;
                                                __glDetailTemp._lineNumber = (_lineNumber - 1);  // _lineNumberDepartment++;
                                                //__glDetailTemp._debit = __debit;
                                                //__glDetailTemp._credit = __credit;
                                                __glDetailTemp._percent = 100;
                                                __glDetailTemp._amount = __amount;
                                                __glDetailTemp._sortStr = ((isDebit) ? "0" : "1") + __accountCodeDetail;
                                                __glDetailTemp._lineNumberDetail = _lineNumberDepartment++; //__detail;
                                                glTemp._glProjectDetail.Add(__glDetailTemp);
                                            }
                                        }
                                    }
                                    break;
                                case _accountDistType.การจัดสรร:
                                    {
                                        // ดึงรายละเอียดการเฉลี่ย ตาม ไอเท็ม,แผนก  filter department by doc_no, item_code 
                                        __query = _g.d.ic_trans_detail_allocate._trans_flag + " = " + _g.g._transFlagGlobal._transFlag(transFlagCompare).ToString() + " and " + _g.d.ic_trans_detail_allocate._doc_no + " = \'" + docNo + "\' " + extraWhere;

                                        DataRow[] __getAllocateDetail = (depaermentTransDetailTable.Rows.Count == 0) ? null : depaermentTransDetailTable.Select(__query);

                                        if ((__getAllocateDetail != null && __getAllocateDetail.Length > 0) || departmentcode.Length > 0)
                                        {
                                            // step 2 หารายการแผนก 
                                            if (__getAllocateDetail != null && __getAllocateDetail.Length > 0)
                                            {
                                                // เฉลี่ยรายการ ไปแต่ละแผนก
                                                for (int __rowDepartment = 0; __rowDepartment < __getAllocateDetail.Length; __rowDepartment++)
                                                {
                                                    decimal __getPercent = MyLib._myGlobal._decimalPhase(__getAllocateDetail[__rowDepartment][_g.d.ic_trans_detail_allocate._ratio].ToString());
                                                    decimal __getAmount = MyLib._myGlobal._decimalPhase(__getAllocateDetail[__rowDepartment][_g.d.ic_trans_detail_allocate._amount].ToString());
                                                    string __getDepartmentCode = __getAllocateDetail[__rowDepartment][_g.d.ic_trans_detail_allocate._allocate_code].ToString();

                                                    string __departmentName = "";

                                                    string __getDepartmentNameData = _g.d.erp_allocate_list._code + "=\'" + __getDepartmentCode + "\'";
                                                    DataRow[] __getAllocateRow = this._allocateTable.Select(__getDepartmentNameData);
                                                    if (__getAllocateRow.Length > 0)
                                                    {
                                                        __departmentName = __getAllocateRow[0][_g.d.erp_allocate_list._name_1].ToString();
                                                    }
                                                    //if (isDebit)
                                                    //    __debit = __getAmount;
                                                    //else
                                                    //    __credit = __getAmount;

                                                    _glDetailOtherStruct __glDetailTemp = new _glDetailOtherStruct();
                                                    __glDetailTemp._accountCode = __accountCodeDetail;
                                                    __glDetailTemp._accountName = __accountName;
                                                    __glDetailTemp._code = __getDepartmentCode;
                                                    __glDetailTemp._name_1 = __departmentName;
                                                    __glDetailTemp._accountDescription = __getAccountDescription;
                                                    __glDetailTemp._debitOrCredit = (isDebit) ? 0 : 1;
                                                    __glDetailTemp._lineNumber = (_lineNumber - 1); ; // _lineNumberDepartment++;
                                                    //__glDetailTemp._debit = __debit;
                                                    //__glDetailTemp._credit = __credit;
                                                    __glDetailTemp._percent = __getPercent;
                                                    __glDetailTemp._amount = __getAmount;
                                                    __glDetailTemp._sortStr = ((isDebit) ? "0" : "1") + __accountCodeDetail;
                                                    __glDetailTemp._lineNumberDetail = _lineNumberDepartment++; //__detail;
                                                    glTemp._glAllocateDetail.Add(__glDetailTemp);
                                                }
                                            }
                                            else if (departmentcode.Length > 0)
                                            {
                                                string __departmentName = "";

                                                string __getDepartmentNameData = _g.d.erp_allocate_list._code + "=\'" + departmentcode + "\' ";
                                                DataRow[] __getAllocateRow = this._allocateTable.Select(__getDepartmentNameData);
                                                if (__getAllocateRow.Length > 0)
                                                {
                                                    __departmentName = __getAllocateRow[0][_g.d.erp_allocate_list._name_1].ToString();
                                                }

                                                // ไม่เฉลี่ย เอาไปลงแผนกเต็มจำนวน
                                                _glDetailOtherStruct __glDetailTemp = new _glDetailOtherStruct();
                                                __glDetailTemp._accountCode = __accountCodeDetail;
                                                __glDetailTemp._accountName = __accountName;
                                                __glDetailTemp._code = departmentcode;
                                                __glDetailTemp._name_1 = __departmentName;
                                                __glDetailTemp._accountDescription = __getAccountDescription;
                                                __glDetailTemp._debitOrCredit = (isDebit) ? 0 : 1;
                                                __glDetailTemp._lineNumber = (_lineNumber - 1);  // _lineNumberDepartment++;
                                                //__glDetailTemp._debit = __debit;
                                                //__glDetailTemp._credit = __credit;
                                                __glDetailTemp._percent = 100;
                                                __glDetailTemp._amount = __amount;
                                                __glDetailTemp._sortStr = ((isDebit) ? "0" : "1") + __accountCodeDetail;
                                                __glDetailTemp._lineNumberDetail = _lineNumberDepartment++; //__detail;
                                                glTemp._glAllocateDetail.Add(__glDetailTemp);
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                    #endregion

                }
            }


            return (__foundByDetail == true) ? true : false;

        }


        private void _loadBeforeForProcess()
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            int __processDepartmentIndex = -1;
            int __processProjectIndex = -1;
            int __processAllocateIndex = -1;
            int __processSideIndex = -1;
            int __processJobsIndex = -1;

            int __row = 0;

            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            // Table 0=ประเภทเอกสาร 
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(this._queryDocFormat));
            __row++;
            // Table 1=ประเภทเอกสาร (ส่วน GL)
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.erp_doc_format_gl._table + " where " + _g.d.erp_doc_format_gl._condition_number + ">0 " + ((this._docFormatTempProcess.Length > 0) ? " and " + _g.d.erp_doc_format_gl._doc_code + "=\'" + this._docFormatTempProcess + "\'" : "") + " order by " + _g.d.erp_doc_format_gl._doc_code + "," + _g.d.erp_doc_format_gl._line_number));
            __row++;
            // Table 2=ดึงสินค้าพร้อมรหัสผังบัญชี
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_inventory._code, _g.d.ic_inventory._name_1, _g.d.ic_inventory._account_code_1, _g.d.ic_inventory._account_code_2, _g.d.ic_inventory._account_code_3, _g.d.ic_inventory._tax_type, _g.d.ic_inventory._account_code_4) + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._item_type + " in (0,1,2,4)order by " + _g.d.ic_inventory._code));
            __row++;
            // Table 3=ดึงผังบัญชี
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.gl_chart_of_account._code, _g.d.gl_chart_of_account._name_1) + " from " + _g.d.gl_chart_of_account._table + " order by " + _g.d.gl_chart_of_account._code));
            __row++;
            // Table 4=pass book 
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.erp_pass_book._code, _g.d.erp_pass_book._name_1, _g.d.erp_pass_book._account_code_1, _g.d.erp_pass_book._account_code_2) + " from " + _g.d.erp_pass_book._table + " order by " + _g.d.erp_pass_book._code));
            __row++;
            // Table 5=เจ้าหนี้
            string __querySupplier = "select * from (select " + MyLib._myGlobal._fieldAndComma(_g.d.ap_supplier._code, _g.d.ap_supplier._ap_status, "coalesce((select " + _g.d.ap_supplier_detail._account_code + " from " + _g.d.ap_supplier_detail._table + " where " + _g.d.ap_supplier_detail._table + "." + _g.d.ap_supplier_detail._ap_code + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + "),\'\') as account_code") + " from " + _g.d.ap_supplier._table + ") as xxx1 order by " + _g.d.ap_supplier._code;
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySupplier));
            __row++;
            // Table 6=ลูกหนี้
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from (select " + MyLib._myGlobal._fieldAndComma(_g.d.ar_customer._code, _g.d.ar_customer._ar_status, "coalesce((select " + _g.d.ar_customer_detail._account_code + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "),\'\') as account_code") + " from " + _g.d.ar_customer._table + ") as xxx2 order by " + _g.d.ar_customer._code));
            __row++;
            // Table 7=petty cash
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.cb_petty_cash._code, _g.d.cb_petty_cash._name_1, _g.d.cb_petty_cash._chart_of_account) + " from " + _g.d.cb_petty_cash._table + " order by " + _g.d.cb_petty_cash._code));
            __row++;
            // Table 8=ค่าใช้จ่ายอื่น
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.erp_expenses_list._code, _g.d.erp_expenses_list._name_1, _g.d.erp_expenses_list._gl_account_code) + " from " + _g.d.erp_expenses_list._table + " order by " + _g.d.erp_expenses_list._code));
            __row++;
            // Table 9=เช็ครับล่วงหน้า
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.erp_bank._code, _g.d.erp_bank._name_1, _g.d.erp_bank._chq_income_account_code) + " from " + _g.d.erp_bank._table + " order by " + _g.d.erp_bank._code));
            __row++;
            // Table 10=ประเภทบัตรเครดิต
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.erp_credit_type._code, _g.d.erp_credit_type._name_1, _g.d.erp_credit_type._account_code) + " from " + _g.d.erp_credit_type._table + " order by " + _g.d.erp_credit_type._code));
            __row++;
            // Table 11=รายได้อื่น
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.erp_income_list._code, _g.d.erp_income_list._name_1, _g.d.erp_income_list._gl_account_code) + " from " + _g.d.erp_income_list._table + " order by " + _g.d.erp_income_list._code));
            __row++;

            if (_g.g._companyProfile._use_department == 1)
            {
                // Table 12=รายได้อื่น
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.erp_department_list._code, _g.d.erp_department_list._name_1) + " from " + _g.d.erp_department_list._table + " order by " + _g.d.erp_department_list._code));
                __processDepartmentIndex = __row;
                __row++;

            }

            if (_g.g._companyProfile._use_project == 1)
            {
                // Table 13=รายได้อื่น
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.erp_project_list._code, _g.d.erp_project_list._name_1) + " from " + _g.d.erp_project_list._table + " order by " + _g.d.erp_project_list._code));
                __processProjectIndex = __row;
                __row++;
            }

            if (_g.g._companyProfile._use_allocate == 1)
            {
                // Table 14=รายได้อื่น
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.erp_allocate_list._code, _g.d.erp_allocate_list._name_1) + " from " + _g.d.erp_allocate_list._table + " order by " + _g.d.erp_allocate_list._code));
                __processAllocateIndex = __row;
                __row++;
            }

            if (_g.g._companyProfile._use_unit == 1)
            {
                // Table 14=รายได้อื่น
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.erp_side_list._code, _g.d.erp_side_list._name_1) + " from " + _g.d.erp_side_list._table + " order by " + _g.d.erp_side_list._code));
                __processSideIndex = __row;
                __row++;
            }

            if (_g.g._companyProfile._use_job == 1)
            {
                // Table 14=รายได้อื่น
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.erp_job_list._code, _g.d.erp_job_list._name_1) + " from " + _g.d.erp_job_list._table + " order by " + _g.d.erp_job_list._code));
                __processJobsIndex = __row;
                __row++;
            }


            __myquery.Append("</node>");
            string __query = __myquery.ToString();
            ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query);

            this._docFormat = ((DataSet)__getData[0]).Tables[0];
            this._docFormatGL = ((DataSet)__getData[1]).Tables[0];
            this._icInventory = ((DataSet)__getData[2]).Tables[0];
            this._chatOfAccount = ((DataSet)__getData[3]).Tables[0];
            this._passBook = ((DataSet)__getData[4]).Tables[0];
            this._apSupplier = ((DataSet)__getData[5]).Tables[0];
            this._arCustomer = ((DataSet)__getData[6]).Tables[0];
            this._pettyCash = ((DataSet)__getData[7]).Tables[0];
            this._otherExpense = ((DataSet)__getData[8]).Tables[0];
            this._bankIncomeAccount = ((DataSet)__getData[9]).Tables[0];
            this._creditCardAccount = ((DataSet)__getData[10]).Tables[0];
            this._otherIncome = ((DataSet)__getData[11]).Tables[0];

            if (_g.g._companyProfile._use_department == 1)
            {
                // Table 12=แผนก
                this._departmentTable = ((DataSet)__getData[__processDepartmentIndex]).Tables[0];
            }

            if (_g.g._companyProfile._use_project == 1)
            {
                // Table 13=โครงการ
                this._projectTable = ((DataSet)__getData[__processProjectIndex]).Tables[0];
            }

            if (_g.g._companyProfile._use_allocate == 1)
            {
                // Table 14=การจัดสรร
                this._allocateTable = ((DataSet)__getData[__processAllocateIndex]).Tables[0];
            }

            if (_g.g._companyProfile._use_unit == 1)
            {
                // Table 14=การจัดสรร
                this._sideTable = ((DataSet)__getData[__processSideIndex]).Tables[0];
            }

            if (_g.g._companyProfile._use_job == 1)
            {
                // Table 14=การจัดสรร
                this._jobsTable = ((DataSet)__getData[__processJobsIndex]).Tables[0];
            }
        }

        private void _process()
        {
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                _g.g._companyProfileLoad();
                this._resultGrid._clear();
                this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, "ดึงข้อมูล", false);
                //

                this._loadBeforeForProcess();

                this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, "เริ่มประมวลผล", false);
                //
                string __dateBegin = this._conditionScreen._getDataStrQuery(_g.d.gl_resource._date_begin);
                string __dateEnd = this._conditionScreen._getDataStrQuery(_g.d.gl_resource._date_end);
                string[] __queryArrayStr = this._processQuery(__dateBegin, __dateEnd);
                string __queryStr1 = "select doc_date,count(*) as xcount from (" + __queryArrayStr[0].ToString() + ") as xxx group by doc_date order by doc_date";

                DataSet __queryGetDataSet = __myFrameWork._queryShort(__queryStr1);
                if (__queryGetDataSet.Tables.Count == 0)
                    MessageBox.Show("Error Query " + __myFrameWork._lastError + "\n" + __queryStr1);

                DataTable __queryGet = __queryGetDataSet.Tables[0];

                List<__selectDateProcess> __selectDate = new List<__selectDateProcess>();
                int __recordCount = 0;
                for (int __date = 0; __date < __queryGet.Rows.Count; __date++)
                {
                    string __processDate = __queryGet.Rows[__date][0].ToString();
                    __recordCount += (int)MyLib._myGlobal._decimalPhase(__queryGet.Rows[__date][1].ToString());
                    if (__selectDate.Count == 0 || __recordCount > 5000)
                    {
                        __selectDateProcess __data = new __selectDateProcess();
                        __data._dateBegin = __processDate;
                        __data._dateEnd = __processDate;
                        __selectDate.Add(__data);
                        __recordCount = 0;
                    }
                    else
                    {
                        __selectDate[__selectDate.Count - 1]._dateEnd = __processDate;
                    }
                }
                for (int __date = 0; __date < __selectDate.Count; __date++)
                {

                    this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, "กำลังประมวลผลวันที่" + " : " + __selectDate[__date]._dateBegin + " ถึงวันที่ : " + __selectDate[__date]._dateEnd, false);
                    this._processByDate(__selectDate[__date]._dateBegin, __selectDate[__date]._dateEnd, this._autoSaveData);
                }
                //
                StringBuilder __queryUpdate = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

                //__queryUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal._table + " set " + _g.d.gl_journal._is_pass + "=0 where " + _g.d.gl_journal._is_pass + " is null"));
                //string __isPassQuery = "(select " + _g.d.gl_journal._is_pass + " from " + _g.d.gl_journal._table + " where " + _g.d.gl_journal._table + "." + _g.d.gl_journal._doc_no + "=" + _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._doc_no + ")";
                //__queryUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal_detail._table + " set " + _g.d.gl_journal_detail._is_pass + "=" + __isPassQuery + " where " + _g.d.gl_journal_detail._is_pass + " is null or " + _g.d.gl_journal_detail._is_pass + "<>" + __isPassQuery));

                string __isPassQuery = "select is_pass from gl_journal where gl_journal.doc_no=gl_journal_detail.doc_no and is_pass = {0}";
                __queryUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal_detail._table + " set " + _g.d.gl_journal_detail._is_pass + "=1 where " + _g.d.gl_journal_detail._is_pass + " <> 1 and exists(" + string.Format(__isPassQuery, "1") + ")"));
                __queryUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal_detail._table + " set " + _g.d.gl_journal_detail._is_pass + "=0 where " + _g.d.gl_journal_detail._is_pass + " <> 0 and exists(" + string.Format(__isPassQuery, "0") + ") "));


                __queryUpdate.Append("</node>");
                string __queryStr = __queryUpdate.ToString();
                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __queryStr);
                //

                if (this._autoSaveData)
                {
                    MessageBox.Show("Success");
                }
                this._success = true;
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        public void _saveToDatabase(List<_glStruct> source, int countRecord, int maxRecord)
        {

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            for (int __loop = 0; __loop < source.Count; __loop++)
            {
                _glStruct __source = source[__loop];
                //ถ้าไม่ใส่ ผังบัญชี ไว้ ไม่ต้องลงบัญชี    
                if (__source._glDetail.Count > 0 || 1 == 1)
                {
                    // ลบของเก่าออกก่อน (หัว)
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal._table + " where " + _g.d.gl_journal._doc_no + "=\'" + source[__loop]._docNo.ToUpper() + "\' and " + _g.d.gl_journal._trans_flag + "=" + __source._transFlag.ToString()));
                    // ลบของเก่าออกก่อน (หาง)
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._doc_no + "=\'" + source[__loop]._docNo.ToUpper() + "\' and " + _g.d.gl_journal_detail._trans_flag + "=" + __source._transFlag.ToString()));


                    // แยกแผนก
                    if (_g.g._companyProfile._use_department == 1)
                    {
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_depart_list._table + " where " + _g.d.gl_journal_depart_list._doc_no + "=\'" + source[__loop]._docNo.ToUpper() + "\' and " + _g.d.gl_journal_depart_list._trans_flag + "=" + __source._transFlag.ToString()));
                    }

                    // แยกโครงการ
                    if (_g.g._companyProfile._use_project == 1)
                    {
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_project_list._table + " where " + _g.d.gl_journal_project_list._doc_no + "=\'" + source[__loop]._docNo.ToUpper() + "\' and " + _g.d.gl_journal_project_list._trans_flag + "=" + __source._transFlag.ToString()));
                    }

                    // แยกการจัดสรร
                    if (_g.g._companyProfile._use_allocate == 1)
                    {
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_allocate_list._table + " where " + _g.d.gl_journal_allocate_list._doc_no + "=\'" + source[__loop]._docNo.ToUpper() + "\' and " + _g.d.gl_journal_allocate_list._trans_flag + "=" + __source._transFlag.ToString()));
                    }

                    // แยกการจัดสรร
                    if (_g.g._companyProfile._use_unit == 1)
                    {
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_side_list._table + " where " + _g.d.gl_journal_side_list._doc_no + "=\'" + source[__loop]._docNo.ToUpper() + "\' and " + _g.d.gl_journal_side_list._trans_flag + "=" + __source._transFlag.ToString()));
                    }
                    // แยกการจัดสรร
                    if (_g.g._companyProfile._use_allocate == 1)
                    {
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_job_list._table + " where " + _g.d.gl_journal_job_list._doc_no + "=\'" + source[__loop]._docNo.ToUpper() + "\' and " + _g.d.gl_journal_job_list._trans_flag + "=" + __source._transFlag.ToString()));
                    }


                    // เพิ่มของใหม่ (หัว)
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.gl_journal._table + " (" + MyLib._myGlobal._fieldAndComma(_g.d.gl_journal._book_code, _g.d.gl_journal._doc_no, _g.d.gl_journal._doc_date, _g.d.gl_journal._description, _g.d.gl_journal._debit, _g.d.gl_journal._credit, _g.d.gl_journal._doc_code, _g.d.gl_journal._trans_flag, _g.d.gl_journal._branch_code, _g.d.gl_journal._account_year, _g.d.gl_journal._period_number) + ") values (" + MyLib._myGlobal._fieldAndComma("\'" + __source._bookCode + "\'", "\'" + __source._docNo + "\'", "\'" + MyLib._myGlobal._convertDateToQuery(__source._docDate) + "\'", "\'" + MyLib._myGlobal._convertStrToQuery(__source._description) + "\'", __source._debit.ToString(), __source._credit.ToString(), "\'" + __source._docCode + "\'", __source._transFlag.ToString(), "\'" + __source._branchCode + "\'", __source._account_year.ToString(), __source._account_period.ToString()) + ")"));
                    // จัดเรียงใหม่
                    __source._glDetail.Sort(delegate (_glDetailStruct a, _glDetailStruct b)
                    {
                        return (a._sortStr.CompareTo(b._sortStr));
                    });
                    // เพิ่มของใหม่ (หาง)
                    for (int __detail = 0; __detail < __source._glDetail.Count; __detail++)
                    {
                        _glDetailStruct __sourceDetail = __source._glDetail[__detail];
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.gl_journal_detail._table + " (" + MyLib._myGlobal._fieldAndComma(_g.d.gl_journal_detail._book_code, _g.d.gl_journal_detail._doc_no, _g.d.gl_journal_detail._doc_date, _g.d.gl_journal_detail._line_number, _g.d.gl_journal_detail._account_code, _g.d.gl_journal_detail._account_name, _g.d.gl_journal_detail._description, _g.d.gl_journal_detail._debit_or_credit, _g.d.gl_journal_detail._debit, _g.d.gl_journal_detail._credit, _g.d.gl_journal_detail._trans_flag, _g.d.gl_journal_detail._journal_type, _g.d.gl_journal_detail._branch_code, _g.d.gl_journal_detail._account_year, _g.d.gl_journal_detail._period_number) + ") values (" + MyLib._myGlobal._fieldAndComma("\'" + __source._bookCode + "\'", "\'" + __source._docNo + "\'", "\'" + MyLib._myGlobal._convertDateToQuery(__source._docDate) + "\'", __sourceDetail._lineNumber.ToString(), "\'" + __sourceDetail._accountCode + "\'", "\'" + MyLib._myGlobal._convertStrToQuery(__sourceDetail._accountName) + "\'", "\'" + MyLib._myGlobal._convertStrToQuery(__sourceDetail._accountDescription) + "\'", __sourceDetail._debitOrCredit.ToString(), __sourceDetail._debit.ToString(), __sourceDetail._credit.ToString(), __source._transFlag.ToString(), "0", "\'" + ((__sourceDetail._branchCodeDetail.Length > 0) ? __sourceDetail._branchCodeDetail : __source._branchCode) + "\'", __source._account_year.ToString(), __source._account_period.ToString()) + ")"));
                    }


                    // แยกแผนก
                    if (_g.g._companyProfile._use_department == 1)
                    {
                        for (int __detail = 0; __detail < __source._glDepartmentDetail.Count; __detail++)
                        {
                            _glDetailOtherStruct __sourceDetail = __source._glDepartmentDetail[__detail];
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.gl_journal_depart_list._table + " (" + MyLib._myGlobal._fieldAndComma(
                                _g.d.gl_journal_depart_list._book_code,
                                _g.d.gl_journal_depart_list._doc_no,
                                _g.d.gl_journal_depart_list._doc_date,
                                _g.d.gl_journal_depart_list._line_number,
                                _g.d.gl_journal_depart_list._line_number_detail,
                                _g.d.gl_journal_depart_list._account_code,
                                _g.d.gl_journal_depart_list._account_name,
                                _g.d.gl_journal_depart_list._department_code,
                                _g.d.gl_journal_depart_list._department_name,
                                _g.d.gl_journal_depart_list._allocate_persent,
                                _g.d.gl_journal_depart_list._allocate_amount,
                                _g.d.gl_journal_depart_list._trans_flag,
                                _g.d.gl_journal_depart_list._journal_type,
                                _g.d.gl_journal_depart_list._account_year,
                                _g.d.gl_journal_depart_list._period_number) + ") values (" + MyLib._myGlobal._fieldAndComma(
                                "\'" + __source._bookCode + "\'",
                                "\'" + __source._docNo + "\'",
                                "\'" + MyLib._myGlobal._convertDateToQuery(__source._docDate) + "\'",
                                __sourceDetail._lineNumber.ToString(),
                                 __sourceDetail._lineNumberDetail.ToString(),
                               "\'" + __sourceDetail._accountCode + "\'",
                                "\'" + MyLib._myGlobal._convertStrToQuery(__sourceDetail._accountName) + "\'",
                               "\'" + __sourceDetail._code + "\'",
                                "\'" + MyLib._myGlobal._convertStrToQuery(__sourceDetail._name_1) + "\'",
                                __sourceDetail._percent.ToString(),
                                __sourceDetail._amount.ToString(),
                                __source._transFlag.ToString(),
                                "0",
                                __source._account_year.ToString(),
                                __source._account_period.ToString()) + ")"));
                        }
                    }

                    // แยกโครงการ
                    if (_g.g._companyProfile._use_project == 1)
                    {
                        for (int __detail = 0; __detail < __source._glProjectDetail.Count; __detail++)
                        {
                            _glDetailOtherStruct __sourceDetail = __source._glProjectDetail[__detail];
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.gl_journal_project_list._table + " (" + MyLib._myGlobal._fieldAndComma(
                                _g.d.gl_journal_project_list._book_code,
                                _g.d.gl_journal_project_list._doc_no,
                                _g.d.gl_journal_project_list._doc_date,
                                _g.d.gl_journal_project_list._line_number,
                                _g.d.gl_journal_project_list._line_number_detail,
                                _g.d.gl_journal_project_list._account_code,
                                _g.d.gl_journal_project_list._account_name,
                                _g.d.gl_journal_project_list._project_code,
                                _g.d.gl_journal_project_list._project_name,
                                _g.d.gl_journal_project_list._allocate_persent,
                                _g.d.gl_journal_project_list._allocate_amount,
                                _g.d.gl_journal_project_list._trans_flag,
                                _g.d.gl_journal_project_list._journal_type,
                                _g.d.gl_journal_project_list._account_year,
                                _g.d.gl_journal_project_list._period_number) + ") values (" + MyLib._myGlobal._fieldAndComma(
                                "\'" + __source._bookCode + "\'",
                                "\'" + __source._docNo + "\'",
                                "\'" + MyLib._myGlobal._convertDateToQuery(__source._docDate) + "\'",
                                __sourceDetail._lineNumber.ToString(),
                                 __sourceDetail._lineNumberDetail.ToString(),
                                "\'" + __sourceDetail._accountCode + "\'",
                                "\'" + MyLib._myGlobal._convertStrToQuery(__sourceDetail._accountName) + "\'",
                               "\'" + __sourceDetail._code + "\'",
                                "\'" + MyLib._myGlobal._convertStrToQuery(__sourceDetail._name_1) + "\'",
                                __sourceDetail._percent.ToString(),
                                __sourceDetail._amount.ToString(),
                                __source._transFlag.ToString(),
                                "0",
                                __source._account_year.ToString(),
                                __source._account_period.ToString()) + ")"));
                        }
                    }


                    // แยกการจัดสรร
                    if (_g.g._companyProfile._use_allocate == 1)
                    {
                        for (int __detail = 0; __detail < __source._glAllocateDetail.Count; __detail++)
                        {
                            _glDetailOtherStruct __sourceDetail = __source._glAllocateDetail[__detail];
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.gl_journal_allocate_list._table + " (" + MyLib._myGlobal._fieldAndComma(
                                _g.d.gl_journal_allocate_list._book_code,
                                _g.d.gl_journal_allocate_list._doc_no,
                                _g.d.gl_journal_allocate_list._doc_date,
                                _g.d.gl_journal_allocate_list._line_number,
                                _g.d.gl_journal_allocate_list._line_number_detail,
                                _g.d.gl_journal_allocate_list._account_code,
                                _g.d.gl_journal_allocate_list._account_name,
                                _g.d.gl_journal_allocate_list._allocate_code,
                                _g.d.gl_journal_allocate_list._allocate_name,
                                _g.d.gl_journal_allocate_list._allocate_persent,
                                _g.d.gl_journal_allocate_list._allocate_amount,
                                _g.d.gl_journal_allocate_list._trans_flag,
                                _g.d.gl_journal_allocate_list._journal_type,
                                _g.d.gl_journal_allocate_list._account_year,
                                _g.d.gl_journal_allocate_list._period_number) + ") values (" + MyLib._myGlobal._fieldAndComma(
                                "\'" + __source._bookCode + "\'",
                                "\'" + __source._docNo + "\'",
                                "\'" + MyLib._myGlobal._convertDateToQuery(__source._docDate) + "\'",
                                __sourceDetail._lineNumber.ToString(),
                                 __sourceDetail._lineNumberDetail.ToString(),
                                "\'" + __sourceDetail._accountCode + "\'",
                                "\'" + MyLib._myGlobal._convertStrToQuery(__sourceDetail._accountName) + "\'",
                               "\'" + __sourceDetail._code + "\'",
                                "\'" + MyLib._myGlobal._convertStrToQuery(__sourceDetail._name_1) + "\'",
                                __sourceDetail._percent.ToString(),
                                __sourceDetail._amount.ToString(),
                                __source._transFlag.ToString(),
                                "0",
                                __source._account_year.ToString(),
                                __source._account_period.ToString()) + ")"));
                        }

                        // หน่วยงาน
                        if (_g.g._companyProfile._use_unit == 1)
                        {
                            for (int __detail = 0; __detail < __source._glSideDetail.Count; __detail++)
                            {
                                _glDetailOtherStruct __sourceDetail = __source._glSideDetail[__detail];
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.gl_journal_side_list._table + " (" + MyLib._myGlobal._fieldAndComma(
                                    _g.d.gl_journal_side_list._book_code,
                                    _g.d.gl_journal_side_list._doc_no,
                                    _g.d.gl_journal_side_list._doc_date,
                                    _g.d.gl_journal_side_list._line_number,
                                    _g.d.gl_journal_side_list._line_number_detail,
                                    _g.d.gl_journal_side_list._account_code,
                                    _g.d.gl_journal_side_list._account_name,
                                    _g.d.gl_journal_side_list._side_code,
                                    _g.d.gl_journal_side_list._side_name,
                                    _g.d.gl_journal_side_list._allocate_persent,
                                    _g.d.gl_journal_side_list._allocate_amount,
                                    _g.d.gl_journal_side_list._trans_flag,
                                    _g.d.gl_journal_side_list._journal_type,
                                    _g.d.gl_journal_side_list._account_year,
                                    _g.d.gl_journal_side_list._period_number) + ") values (" + MyLib._myGlobal._fieldAndComma(
                                    "\'" + __source._bookCode + "\'",
                                    "\'" + __source._docNo + "\'",
                                    "\'" + MyLib._myGlobal._convertDateToQuery(__source._docDate) + "\'",
                                    __sourceDetail._lineNumber.ToString(),
                                     __sourceDetail._lineNumberDetail.ToString(),
                                    "\'" + __sourceDetail._accountCode + "\'",
                                    "\'" + MyLib._myGlobal._convertStrToQuery(__sourceDetail._accountName) + "\'",
                                   "\'" + __sourceDetail._code + "\'",
                                    "\'" + MyLib._myGlobal._convertStrToQuery(__sourceDetail._name_1) + "\'",
                                    __sourceDetail._percent.ToString(),
                                    __sourceDetail._amount.ToString(),
                                    __source._transFlag.ToString(),
                                    "0",
                                    __source._account_year.ToString(),
                                    __source._account_period.ToString()) + ")"));
                            }
                        }

                        // แยกการจัดสรร
                        if (_g.g._companyProfile._use_job == 1)
                        {
                            for (int __detail = 0; __detail < __source._glJobDetail.Count; __detail++)
                            {
                                _glDetailOtherStruct __sourceDetail = __source._glJobDetail[__detail];
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.gl_journal_job_list._table + " (" + MyLib._myGlobal._fieldAndComma(
                                    _g.d.gl_journal_job_list._book_code,
                                    _g.d.gl_journal_job_list._doc_no,
                                    _g.d.gl_journal_job_list._doc_date,
                                    _g.d.gl_journal_job_list._line_number,
                                    _g.d.gl_journal_job_list._line_number_detail,
                                    _g.d.gl_journal_job_list._account_code,
                                    _g.d.gl_journal_job_list._account_name,
                                    _g.d.gl_journal_job_list._job_code,
                                    _g.d.gl_journal_job_list._job_name,
                                    _g.d.gl_journal_job_list._allocate_persent,
                                    _g.d.gl_journal_job_list._allocate_amount,
                                    _g.d.gl_journal_job_list._trans_flag,
                                    _g.d.gl_journal_job_list._journal_type,
                                    _g.d.gl_journal_job_list._account_year,
                                    _g.d.gl_journal_job_list._period_number) + ") values (" + MyLib._myGlobal._fieldAndComma(
                                    "\'" + __source._bookCode + "\'",
                                    "\'" + __source._docNo + "\'",
                                    "\'" + MyLib._myGlobal._convertDateToQuery(__source._docDate) + "\'",
                                    __sourceDetail._lineNumber.ToString(),
                                     __sourceDetail._lineNumberDetail.ToString(),
                                    "\'" + __sourceDetail._accountCode + "\'",
                                    "\'" + MyLib._myGlobal._convertStrToQuery(__sourceDetail._accountName) + "\'",
                                   "\'" + __sourceDetail._code + "\'",
                                    "\'" + MyLib._myGlobal._convertStrToQuery(__sourceDetail._name_1) + "\'",
                                    __sourceDetail._percent.ToString(),
                                    __sourceDetail._amount.ToString(),
                                    __source._transFlag.ToString(),
                                    "0",
                                    __source._account_year.ToString(),
                                    __source._account_period.ToString()) + ")"));
                            }
                        }

                    }
                }
                else
                {
                    countRecord--;
                    maxRecord--;
                }
            }
            __query.Append("</node>");
            string __queryStr = __query.ToString();
            string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __queryStr);
            if (__result.Length > 0)
            {
                this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, __result, false);
            }
            this._resultGrid._cellUpdate(this._resultGrid._addRow(), this._resultFieldName, "Save (" + countRecord.ToString() + "/" + maxRecord.ToString() + ") Records", false);
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            // toe check period close
            //DateTime __dateBegin = this._conditionScreen._getDataDate(_g.d.gl_resource._date_begin);
            //DateTime __dateEnd = this._conditionScreen._getDataDate(_g.d.gl_resource._date_end);

            //

            this._success = false;
            //
            this._timer.Enabled = true;
            this._timer.Start();
            //
            this._stopButton.Enabled = true;
            this._processButton.Enabled = false;
            //
            this._threadWorking = new Thread(this._process);
            this._threadWorking.Start();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this._threadStop();
            this.Dispose();
        }

        private void _threadStop()
        {
            if (this._threadWorking != null)
            {
                this._threadWorking.Abort();
                this._threadWorking = null;
            }
        }

        private void _stopButton_Click(object sender, EventArgs e)
        {
            this._threadStop();
            this._timer.Stop();
            this._timer.Enabled = false;
            this._stopButton.Enabled = false;
            this._processButton.Enabled = true;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            this._resultGrid.Invalidate();
            if (this._success)
            {
                this._success = false;
                this._threadStop();
                this._timer.Stop();
                this._timer.Enabled = false;
                this._stopButton.Enabled = false;
                this._processButton.Enabled = true;
            }
        }

        public class _glStruct
        {
            public DateTime _docDate = new DateTime();
            public string _bookCode = "";
            public string _docNo = "";
            public string _description = "";
            public decimal _debit = 0.0M;
            public decimal _credit = 0.0M;
            public int _transFlag = 0;
            public string _docCode = "";
            public List<_glDetailStruct> _glDetail = new List<_glDetailStruct>();
            /// <summary>
            /// สาขา
            /// </summary>
            public string _branchCode = "";
            /// <summary>
            /// ปีบัญชี
            /// </summary>
            public int _account_year = 0;
            /// <summary>
            /// งวดบัญชี
            /// </summary>
            public int _account_period = 0;

            public List<_glDetailOtherStruct> _glDepartmentDetail = new List<_glDetailOtherStruct>();
            public List<_glDetailOtherStruct> _glProjectDetail = new List<_glDetailOtherStruct>();
            public List<_glDetailOtherStruct> _glAllocateDetail = new List<_glDetailOtherStruct>();
            public List<_glDetailOtherStruct> _glSideDetail = new List<_glDetailOtherStruct>();
            public List<_glDetailOtherStruct> _glJobDetail = new List<_glDetailOtherStruct>();

        }

        public class _glDetailStruct
        {
            public int _lineNumber = 0;
            public string _accountCode = "";
            public string _accountName = "";
            public string _accountDescription = "";
            public decimal _debit = 0.0M;
            public decimal _credit = 0.0M;
            /// <summary>
            /// 0=Debit,1=Credit
            /// </summary>
            public int _debitOrCredit = 0;
            public string _sortStr = "";

            public string _branchCodeDetail = "";
        }

        public class _glDetailOtherStruct
        {
            public int _lineNumber = 0;
            public int _lineNumberDetail = 0;
            public string _accountCode = "";
            public string _accountName = "";
            public string _accountDescription = "";
            public decimal _debit = 0.0M;
            public decimal _credit = 0.0M;
            /// <summary>
            /// 0=Debit,1=Credit
            /// </summary>
            public int _debitOrCredit = 0;
            public string _sortStr = "";
            public decimal _percent = 0M;
            public decimal _amount = 0M;
            /// <summary>
            /// Department Code, Project Code, Allocate Code
            /// </summary>
            public string _code = "";
            /// <summary>
            /// Department Name, Project Name, Allocate Name
            /// </summary>
            public string _name_1 = "";
        }

        public class __selectDateProcess
        {
            public string _dateBegin = "";
            public string _dateEnd = "";
        }

        public class _transDataObject
        {

            public _transDataObject(string trans, string doc_no, int trans_flag, int trans_type, string doc_format_code)
            {
                this.trans = trans;
                this.doc_no = doc_no;
                this.trans_flag = trans_flag;
                this.trans_type = trans_type;
                this.doc_format_code = doc_format_code;
            }



            /// <summary>
            /// cb = 239,19
            /// </summary>
            public string trans = "";
            public int vat_type = 0;
            public string tax_doc_no = "";
            public string cust_name = "";
            public string cust_code = "";
            public string remark = "";
            public decimal total_amount = 0M;
            /// <summary>
            /// มูลค่าสินค้าบริการ
            /// </summary>
            public decimal service_amount = 0M;
            public decimal total_value = 0M;
            public decimal total_before_vat = 0M;
            public decimal total_except_vat = 0M;
            public decimal total_vat_value = 0M;
            public decimal total_discount = 0M;
            public decimal cash_amount = 0M;
            public decimal tax_at_pay = 0M;
            public decimal point_amount = 0M;
            public decimal coupon_amount = 0M;
            public decimal card_amount = 0M;
            public decimal chq_amount = 0M;
            public decimal tranfer_amount = 0M;
            public decimal total_income_amount = 0M;
            public decimal petty_cash_amount = 0M;
            public decimal discount_amount = 0M;

            public string doc_no = "";
            public string doc_ref = "";
            public string doc_date = "";
            public string doc_time = "";
            public string doc_format_code = "";
            public int trans_type = 0;
            public int inquiry_type = 0;
            public int trans_flag = 0;
            public decimal advance_amount = 0M;
            public decimal deposit_amount = 0M;
            public decimal credit_card_amount = 0M;
            public decimal credit_card_charge = 0M;
            public string tax_doc_date = "";
            public string branch_code = "";
            public string department_code = "";
            public string project_code = "";
            public string allocate_code = "";
            public decimal vat_next = 0M;
            public string side_code = "";
            public string job_code = "";
            public decimal total_cost = 0M;
            public decimal total_income_other = 0M;
            public decimal total_expense_other = 0M;
            public decimal vatBuyNoReturn = 0M;

        }

        public class _transDetailDataObject
        {
            public _transDetailDataObject(string doc_no, int trans_flag, int trans_type)
            {
                this.doc_no = doc_no;
                this.trans_flag = trans_flag;
                this.trans_type = trans_type;
            }

            public int item_type = 0;
            public decimal charge = 0M;
            public string trans_number;
            public string item_code = "";
            public string pass_book_code = "";
            public string doc_no = "";
            public decimal price = 0M;
            public decimal amount = 0M;
            public decimal sum_of_cost = 0M;
            public decimal sum_amount = 0M;
            public decimal sum_amount_exclude_vat = 0M;
            public int trans_type = 0;
            public int trans_flag = 0;
            public int doc_type = 0;
            public string chq_bank_ref = "";
            public string credit_card_ref = "";

            public string remark = "";
            public decimal transfer_amount = 0M;
            public decimal fee_amount = 0M;
            public int last_flag = 0;
            public int line_number = 0;
            public string branch_code = "";
        }
    }
}
