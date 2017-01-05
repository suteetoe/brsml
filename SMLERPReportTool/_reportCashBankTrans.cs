using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPReportTool
{
    public class _reportCashBankTrans : UserControl
    {
        private SMLReport._generate _report;
        private String _screenName = "";
        private DataTable _dataTable;
        private DataTable _dataTableDetail;
        private SMLERPReportTool._conditionScreen _form_condition;
        string _levelNameRoot = "root";
        string _levelNameDetail = "detail";
        SMLReport._generateLevelClass _level2;
        Boolean _displayDetail = false;
        Boolean _showBarcode = false;
        _reportEnum _mode;
        _reportVatNumberType _vatNumberType = _reportVatNumberType.ปรกติ;
        _g.g._transControlTypeEnum _transFlag;
        string _extraWhere = "";

        public _reportCashBankTrans(_reportEnum mode, _g.g._transControlTypeEnum transFlag, string screenName)
        {
            bool __landscape = true;

            this._screenName = screenName;
            this._mode = mode;
            this._transFlag = transFlag;
            this._report = new SMLReport._generate(screenName, __landscape);
            this._report._query += new SMLReport._generate.QueryEventHandler(_report__query);
            this._report._init += new SMLReport._generate.InitEventHandler(_report__init);
            this._report._showCondition += new SMLReport._generate.ShowConditionEventHandler(_report__showCondition);
            this._report._dataRowSelect += new SMLReport._generate.DataRowSelectEventHandler(_report__dataRowSelect);
            this._report._renderValue += new SMLReport._generate.RenderValueEventHandler(_report__renderValue);
            this._report._renderFont += new SMLReport._generate.RenderFontEventHandler(_report__renderFont);
            this._report.Disposed += new EventHandler(_report_Disposed);
            //
            this._report.Dock = DockStyle.Fill;
            this.Controls.Add(this._report);
            this._report__showCondition(screenName);
        }

        void _report__query()
        {
            if (this._dataTable == null)
            {
                try
                {
                    //
                    string __beginDate = this._form_condition._screen._getDataStrQuery(_g.d.resource_report._from_date);
                    string __endDate = this._form_condition._screen._getDataStrQuery(_g.d.resource_report._to_date);

                    string __beginTime = this._form_condition._screen._getDataStr(_g.d.resource_report._from_time);
                    string __endTime = this._form_condition._screen._getDataStr(_g.d.resource_report._to_time);


                    Boolean __useLastStatus = this._form_condition._screen._getDataStr(_g.d.resource_report._show_cancel_document).ToString().Equals("1") ? true : false;
                    string __getWhereMain = "";
                    string __getWhere = "";
                    string __where = this._form_condition._extra._getWhere("").Replace(" where ", " and ");
                    //
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    string __lastStatusWhere = (__useLastStatus) ? "" : " and last_status=0";
                    //
                    StringBuilder __extraField = new StringBuilder();
                    StringBuilder __extraAs = new StringBuilder();
                    StringBuilder __where_doc_sucess = new StringBuilder();

                    #region Extra As, Extra Field
                    switch (this._mode)
                    {
                        case _reportEnum.เงินสดธนาคาร_รายได้อื่น:
                        case _reportEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                        case _reportEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                        case _reportEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                        case _reportEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                        case _reportEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                            {
                                __extraAs.Append("doc_time as \"ic_trans.doc_time\",");
                                __extraAs.Append("cust_code as \"ic_trans.cust_code\",");
                                __extraAs.Append("cashier_code as \"ic_trans.cashier_code\",");
                                if (_global._reportType(this._mode) == _reportTypeEnum.เจ้าหนี้)
                                {
                                    __extraAs.Append("(select name_1 from ap_supplier where ap_supplier.code=cust_code) as \"ic_trans.cust_name\",");
                                }
                                else
                                {
                                    __extraAs.Append("(select name_1 from ar_customer where ar_customer.code=cust_code) as \"ic_trans.cust_name\",");
                                }
                                __extraAs.Append("total_value as \"ic_trans.total_value\",");
                                __extraAs.Append("total_discount as \"ic_trans.total_discount\",");
                                __extraAs.Append("total_after_discount as \"ic_trans.total_after_discount\",");
                                __extraAs.Append("total_vat_value as \"ic_trans.total_vat_value\",");
                                __extraAs.Append("total_except_vat as \"ic_trans.total_except_vat\",");

                                __extraField.Append("cust_code,total_value,total_discount,total_value - total_discount as total_after_discount, total_vat_value,total_except_vat,");

                                __getWhereMain = this._form_condition._grid._createWhere(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code);
                                __getWhere = this._form_condition._grid._createWhere(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._cust_code);
                                break;
                            }
                        case _reportEnum.เช็ครับ_ยกมา:
                            {
                                __extraAs.Append("cust_code as \"ic_trans.cust_code\",");
                                __extraAs.Append("(select name_1 from ar_customer where ar_customer.code=cust_code) as \"ic_trans.cust_name\",");
                                __extraField.Append("cust_code,");

                                __getWhereMain = this._form_condition._grid._createWhere(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code);
                                __getWhere = this._form_condition._grid._createWhere(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._cust_code);
                            }
                            break;
                        case _reportEnum.เช็คจ่าย_ยกมา:
                            {
                                __extraAs.Append("cust_code as \"ic_trans.cust_code\",");
                                __extraAs.Append("(select name_1 from ap_supplier where ap_supplier.code=cust_code) as \"ic_trans.cust_name\",");
                                __extraField.Append("cust_code,");

                                __getWhereMain = this._form_condition._grid._createWhere(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code);
                                __getWhere = this._form_condition._grid._createWhere(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._cust_code);

                            }
                            break;
                        case _reportEnum.เงินสดย่อย_รับคืน:
                        case _reportEnum.เงินสดย่อย_เบิก:
                        case _reportEnum.ธนาคาร_โอนเงิน:
                        case _reportEnum.บัตรเครดิต_ขึ้นเงิน:
                        case _reportEnum.บัตรเครดิต_ยกเลิก:
                            {
                                __extraAs.Append("doc_time as \"ic_trans.doc_time\",");
                                __extraAs.Append("remark as \"ic_trans.remark\",");

                                __extraField.Append("remark,");

                                // where petty cash ด้วยนะ

                            }
                            break;
                        default:
                            __getWhere = this._form_condition._grid._createWhere(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code);
                            break;
                    }

                    #endregion

                    if (__getWhereMain.Length > 0) __getWhereMain = " and (" + __getWhereMain + ")";
                    if (__getWhere.Length > 0) __getWhere = " and (" + __getWhere + ")";

                    //
                    StringBuilder __query = new StringBuilder();
                    __query.Append("select " + __extraAs.ToString());
                    __query.Append("doc_date as \"ic_trans.doc_date\",");
                    __query.Append("doc_no as \"ic_trans.doc_no\",");
                    __query.Append("doc_ref_date as \"ic_trans.doc_ref_date\",");

                    if ((this._mode == _reportEnum.ขาย_ขายสินค้าและบริการ ||
                        this._mode == _reportEnum.ขาย_รับเงินมัดจำ_รายงานยอดคงเหลือ) && __useLastStatus == true ||
                        this._mode == _reportEnum.ขาย_รับเงินมัดจำ_รายงานตัดเงิน && __useLastStatus == true
                        )
                    {
                        __query.Append("case when _def_last_status = 0 then doc_ref else 'ยกเลิก' end as\"ic_trans.doc_ref\",");
                        __query.Append("case when _def_last_status = 0 then total_amount else 0 end as \"ic_trans.total_amount\",");
                    }
                    // toe หากเป็นลดหนี้ ทำเป็นค่า -
                    else if (this._mode == _reportEnum.ขาย_ขายสินค้าและบริการ_ลดหนี้_เพิ่มหนี้)
                    {
                        __query.Append("doc_ref as \"ic_trans.doc_ref\",");
                        __query.Append("(case when (trans_flag = 48) then -1 else 1 end ) * total_amount as \"ic_trans.total_amount\",");
                    }
                    else
                    {
                        __query.Append("doc_ref as \"ic_trans.doc_ref\",");
                        __query.Append("total_amount as \"ic_trans.total_amount\",");
                    }
                    __query.Append("vat_type as \"ic_trans.vat_type\",");

                    // for vat
                    if ((this._mode == _reportEnum.ขาย_ขายสินค้าและบริการ ||
                        this._mode == _reportEnum.ขาย_รับเงินมัดจำ_รายงานยอดคงเหลือ) && __useLastStatus == true)
                    {
                        __query.Append("case when _def_last_status = 0 then vat_rate else 0 end as \"ic_trans.vat_rate\",");
                    }
                    else if (this._mode == _reportEnum.ขาย_ขายสินค้าและบริการ_ลดหนี้_เพิ่มหนี้)
                    {
                        __query.Append("(case when (trans_flag = 48) then -1 else 1 end ) * vat_rate as \"ic_trans.vat_rate\",");
                    }
                    else
                    {
                        __query.Append("vat_rate as \"ic_trans.vat_rate\",");
                    }

                    __query.Append("last_status as \"ic_trans.last_status\"");
                    __query.Append(" from (select ");
                    __query.Append(__extraField.ToString() + "doc_date,doc_no,doc_time,cashier_code,doc_ref_date,doc_ref,total_amount,total_before_vat,vat_type,vat_rate,last_status as _def_last_status,");
                    __query.Append("cast(last_status as varchar)||','||cast(used_status as varchar)||','||cast(doc_success as varchar)||','||cast(not_approve_1 as varchar)||','||cast(on_hold as varchar)||','||cast(approve_status as varchar)||','||cast(expire_status  as varchar) as last_status");

                    __query.Append(" from ic_trans ");

                    // toe ดัก flag report รวม
                    if (this._mode == _reportEnum.ขาย_ขายสินค้าและบริการ_ลดหนี้_เพิ่มหนี้)
                    {
                        __query.Append(" where trans_flag in (" + _g.g._transFlagGlobal._transFlag(this._transFlag).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้).ToString() + ") ");
                    }
                    else
                    {
                        __query.Append(" where trans_flag in (" + _g.g._transFlagGlobal._transFlag(this._transFlag).ToString() + ") ");
                    }

                    __query.Append(this._extraWhere);


                    if (__beginTime.Length > 0 && __endTime.Length > 0 && (_mode == _reportEnum.ขาย_รับเงินล่วงหน้า || _mode == _reportEnum.ขาย_รับเงินล่วงหน้า_คืน || _mode == _reportEnum.ขาย_รับเงินมัดจำ || _mode == _reportEnum.ขาย_รับเงินมัดจำ_คืน))
                    {
                        //and to_timestamp(date('@to_date@') || ' ' || case when '@to_time@' = '' then '23:59' else '@to_time@' end , 'YYYY/MM/DD HH24:MI')::timestamp
                        string __beginTimeStr = "  to_timestamp(date(" + __beginDate + ") || ' ' || '" + __beginTime + "' , 'YYYY/MM/DD HH24:MI')::timestamp  ";
                        string __endTimeStr = "  to_timestamp(date(" + __endDate + ") || ' ' || '" + __endTime + "' , 'YYYY/MM/DD HH24:MI')::timestamp  ";
                        __query.Append(" and to_timestamp(date(ic_trans.doc_date) || ' ' || ic_trans.doc_time, 'YYYY/MM/DD HH24:MI')::timestamp between " + __beginTimeStr + " and " + __endTimeStr);
                    }
                    else
                    {
                        __query.Append(" and ic_trans.doc_date between " + __beginDate + " and " + __endDate);
                    }

                    __query.Append(__lastStatusWhere + __where_doc_sucess + __getWhereMain + __where + ") as temp1");
                    __query.Append(this._form_condition._extra._getOrderBy() + ",doc_no");
                    //
                    StringBuilder __extraDetailField = new StringBuilder();
                    StringBuilder __extraDetailAs = new StringBuilder();
                    StringBuilder __whereHouse = new StringBuilder();
                    string __priceField = "";
                    string __sumAmountField = "";
                    string __sumAmountTransField = "";

                    #region detail Extra As, Extra Field
                    switch (this._mode)
                    {
                        case _reportEnum.เช็คจ่าย_ยกมา:
                        case _reportEnum.เช็คจ่าย_ผ่าน:
                        case _reportEnum.เช็คจ่าย_คืน:
                        case _reportEnum.เช็คจ่าย_ขาดสิทธิ์:
                        case _reportEnum.เช็คจ่าย_เปลี่ยนเช็ค:
                            {
                                __extraDetailAs.Append("(bank_name || '~' || (select name_1 from erp_bank where erp_bank.code = bank_name)) as \"ic_trans_detail.bank_name\",");
                                __extraDetailAs.Append("(bank_branch || '~' || (select name_1 from erp_bank_branch where erp_bank_branch.bank_code = bank_name and erp_bank_branch.code = bank_branch )) as \"ic_trans_detail.bank_branch\",");
                                __extraDetailAs.Append("date_due as \"ic_trans_detail.date_due\",");
                                __extraDetailAs.Append("remark as \"ic_trans_detail.remark\",");
                                __extraDetailAs.Append("chq_number as \"ic_trans_detail.chq_number\",");
                                __extraDetailAs.Append("sum_of_cost as \"ic_trans_detail.sum_of_cost\",");

                                __extraDetailField.Append("bank_name, bank_branch, date_due, remark,chq_number,sum_of_cost,");

                                __priceField = "price";
                                __sumAmountField = "sum_amount";
                                __sumAmountTransField = "sum_amount";

                            }
                            break;
                        case _reportEnum.เช็ครับ_ยกมา:
                            {
                                __extraDetailAs.Append("(bank_name || '~' || (select name_1 from erp_bank where erp_bank.code = bank_name)) as \"ic_trans_detail.bank_name\",");
                                __extraDetailAs.Append("(bank_branch || '~' || (select name_1 from erp_bank_branch where erp_bank_branch.bank_code = bank_name and erp_bank_branch.code = bank_branch )) as \"ic_trans_detail.bank_branch\",");
                                __extraDetailAs.Append("date_due as \"ic_trans_detail.date_due\",");
                                __extraDetailAs.Append("remark as \"ic_trans_detail.remark\",");
                                __extraDetailField.Append("bank_name, bank_branch, date_due, remark,");

                                __priceField = "price";
                                __sumAmountField = "sum_amount";
                                __sumAmountTransField = "sum_amount";

                            }
                            break;
                        case _reportEnum.เช็ครับ_ฝาก:
                            {
                                __extraDetailAs.Append("(bank_name || '~' || (select name_1 from erp_bank where erp_bank.code = bank_name)) as \"ic_trans_detail.bank_name\",");
                                __extraDetailAs.Append("(bank_branch || '~' || (select name_1 from erp_bank_branch where erp_bank_branch.bank_code = bank_name and erp_bank_branch.code = bank_branch )) as \"ic_trans_detail.bank_branch\",");
                                __extraDetailAs.Append("remark as \"ic_trans_detail.remark\",");
                                __extraDetailAs.Append("chq_number as \"ic_trans_detail.chq_number\",");

                                __extraDetailField.Append("bank_name, bank_branch, chq_number, remark,");

                                __priceField = "price";
                                __sumAmountField = "sum_amount";
                                __sumAmountTransField = "sum_amount";

                            }
                            break;
                        case _reportEnum.เช็ครับ_เปลี่ยนเช็ค:
                            {
                                __extraDetailAs.Append("(bank_name || '~' || (select name_1 from erp_bank where erp_bank.code = bank_name)) as \"ic_trans_detail.bank_name\",");
                                __extraDetailAs.Append("(bank_branch || '~' || (select name_1 from erp_bank_branch where erp_bank_branch.bank_code = bank_name and erp_bank_branch.code = bank_branch )) as \"ic_trans_detail.bank_branch\",");
                                __extraDetailAs.Append("remark as \"ic_trans_detail.remark\",");
                                __extraDetailAs.Append("chq_number as \"ic_trans_detail.chq_number\",");
                                __extraDetailAs.Append("date_due as \"ic_trans_detail.date_due\",");

                                __extraDetailField.Append("bank_name, bank_branch, chq_number, remark,date_due,");

                                __priceField = "price";
                                __sumAmountField = "sum_amount";
                                __sumAmountTransField = "sum_amount";

                            }
                            break;
                        case _reportEnum.เช็ครับ_ผ่าน:
                        case _reportEnum.เช็ครับ_รับคืน:
                        case _reportEnum.เช็ครับ_ขาดสิทธิ์:
                            {
                                __extraDetailAs.Append("(bank_name || '~' || (select name_1 from erp_bank where erp_bank.code = bank_name)) as \"ic_trans_detail.bank_name\",");
                                __extraDetailAs.Append("(bank_branch || '~' || (select name_1 from erp_bank_branch where erp_bank_branch.bank_code = bank_name and erp_bank_branch.code = bank_branch )) as \"ic_trans_detail.bank_branch\",");
                                __extraDetailAs.Append("remark as \"ic_trans_detail.remark\",");
                                __extraDetailAs.Append("chq_number as \"ic_trans_detail.chq_number\",");
                                __extraDetailAs.Append("sum_of_cost as \"ic_trans_detail.sum_of_cost\",");

                                __extraDetailField.Append("sum_of_cost,bank_name, bank_branch, chq_number, remark,");

                                __priceField = "price";
                                __sumAmountField = "sum_amount";
                                __sumAmountTransField = "sum_amount";

                            }
                            break;
                        case _reportEnum.เงินสดธนาคาร_รายได้อื่น:
                        case _reportEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                        case _reportEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:

                        case _reportEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                        case _reportEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                        case _reportEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                            {
                                if (_global._reportType(this._mode) == _reportTypeEnum.เจ้าหนี้)
                                {
                                    __extraDetailAs.Append("(select name_1 from erp_expenses_list where erp_expenses_list.code = item_code) as \"ic_trans_detail.item_name\",");
                                }
                                else
                                {
                                    __extraDetailAs.Append("(select name_1 from erp_income_list where erp_income_list.code = item_code) as \"ic_trans_detail.item_name\",");
                                }
                                __extraDetailAs.Append("remark as \"ic_trans_detail.remark\",");
                                __extraDetailField.Append("remark,");

                                __priceField = "price";
                                __sumAmountField = "sum_amount";
                                __sumAmountTransField = "sum_amount";
                            }
                            break;
                        case _reportEnum.เงินสดย่อย_เบิก:
                        case _reportEnum.เงินสดย่อย_รับคืน:
                            {
                                __extraDetailAs.Append("(select name_1 from " + _g.d.cb_petty_cash._table + " where " + _g.d.cb_petty_cash._table + ".code = item_code) as \"ic_trans_detail.item_name\",");

                                __extraDetailAs.Append("remark as \"ic_trans_detail.remark\",");
                                __extraDetailField.Append("remark,");

                                __priceField = "price";
                                __sumAmountField = "sum_amount";
                                __sumAmountTransField = "sum_amount";
                            }
                            break;
                        case _reportEnum.ธนาคาร_โอนเงิน:
                            {
                                __extraDetailAs.Append("(select " + _g.d.erp_pass_book._name_1 + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._table + ".code = " + _g.d.ic_trans_detail._item_code + ") as \"ic_trans_detail." + _g.d.ic_trans_detail._bank_name + "\",");
                                __extraDetailAs.Append("(select " + _g.d.erp_pass_book._name_1 + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._table + ".code = " + _g.d.ic_trans_detail._item_code_2 + ") as \"ic_trans_detail." + _g.d.ic_trans_detail._bank_name_2 + "\",");
                                __extraDetailAs.Append("(select " + _g.d.erp_bank._name_1 + " from " + _g.d.erp_bank._table + " where " + _g.d.erp_bank._table + "." + _g.d.erp_bank._code + "=(select " + _g.d.erp_pass_book._bank_code + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._table + ".code = " + _g.d.ic_trans_detail._item_code + ")) || '(' || (select " + _g.d.erp_bank_branch._name_1 + " from " + _g.d.erp_bank_branch._table + " where " + _g.d.erp_bank_branch._table + "." + _g.d.erp_bank_branch._bank_code + "=(select " + _g.d.erp_pass_book._bank_code + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._table + ".code = " + _g.d.ic_trans_detail._item_code + ") and " + _g.d.erp_bank_branch._table + "." + _g.d.erp_bank_branch._code + "=(select " + _g.d.erp_pass_book._bank_branch + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._table + ".code = " + _g.d.ic_trans_detail._item_code + "))  ||')' as \"ic_trans_detail." + _g.d.ic_trans_detail._bank_branch + "\",");
                                __extraDetailAs.Append("(select " + _g.d.erp_bank._name_1 + " from " + _g.d.erp_bank._table + " where " + _g.d.erp_bank._table + "." + _g.d.erp_bank._code + "=(select " + _g.d.erp_pass_book._bank_code + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._table + ".code = " + _g.d.ic_trans_detail._item_code_2 + ")) || '(' || (select " + _g.d.erp_bank_branch._name_1 + " from " + _g.d.erp_bank_branch._table + " where " + _g.d.erp_bank_branch._table + "." + _g.d.erp_bank_branch._bank_code + "=(select " + _g.d.erp_pass_book._bank_code + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._table + ".code = " + _g.d.ic_trans_detail._item_code_2 + ") and " + _g.d.erp_bank_branch._table + "." + _g.d.erp_bank_branch._code + "=(select " + _g.d.erp_pass_book._bank_branch + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._table + ".code = " + _g.d.ic_trans_detail._item_code_2 + "))  ||')' as \"ic_trans_detail." + _g.d.ic_trans_detail._bank_branch_2 + "\",");
                                __extraDetailAs.Append(_g.d.ic_trans_detail._transfer_amount + " as \"ic_trans_detail." + _g.d.ic_trans_detail._transfer_amount + "\",");
                                __extraDetailAs.Append(_g.d.ic_trans_detail._fee_amount + " as \"ic_trans_detail." + _g.d.ic_trans_detail._fee_amount + "\",");
                                __extraDetailAs.Append(_g.d.ic_trans_detail._item_code_2 + " as \"ic_trans_detail." + _g.d.ic_trans_detail._item_code_2 + "\",");
                                __extraDetailAs.Append("remark as \"ic_trans_detail.remark\",");

                                __extraDetailField.Append("remark,item_code_2," + _g.d.ic_trans_detail._transfer_amount + "," + _g.d.ic_trans_detail._fee_amount + ",");

                                __priceField = "price";
                                __sumAmountField = "sum_amount";
                                __sumAmountTransField = "sum_amount";

                            }
                            break;
                        case _reportEnum.บัตรเครดิต_ขึ้นเงิน:
                        case _reportEnum.บัตรเครดิต_ยกเลิก:
                            {
                                __extraDetailAs.Append("(select " + _g.d.erp_pass_book._name_1 + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._table + ".code = " + _g.d.ic_trans_detail._item_code + ") as \"ic_trans_detail." + _g.d.ic_trans_detail._bank_name + "\",");
                                __extraDetailAs.Append("(select " + _g.d.erp_bank._name_1 + " from " + _g.d.erp_bank._table + " where " + _g.d.erp_bank._table + "." + _g.d.erp_bank._code + "=(select " + _g.d.erp_pass_book._bank_code + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._table + ".code = " + _g.d.ic_trans_detail._item_code + ")) || '(' || (select " + _g.d.erp_bank_branch._name_1 + " from " + _g.d.erp_bank_branch._table + " where " + _g.d.erp_bank_branch._table + "." + _g.d.erp_bank_branch._bank_code + "=(select " + _g.d.erp_pass_book._bank_code + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._table + ".code = " + _g.d.ic_trans_detail._item_code + ") and " + _g.d.erp_bank_branch._table + "." + _g.d.erp_bank_branch._code + "=(select " + _g.d.erp_pass_book._bank_branch + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._table + ".code = " + _g.d.ic_trans_detail._item_code + "))  ||')' as \"ic_trans_detail." + _g.d.ic_trans_detail._bank_branch + "\",");
                                __extraDetailAs.Append(_g.d.ic_trans_detail._chq_number + " as \"ic_trans_detail." + _g.d.ic_trans_detail._chq_number + "\",");
                                //__extraDetailAs.Append("price as \"ic_trans_detail." + _g.d.ic_trans_detail._price + "\",");
                                __extraDetailAs.Append(_g.d.ic_trans_detail._fee_amount + " as \"ic_trans_detail." + _g.d.ic_trans_detail._fee_amount + "\",");
                                __extraDetailAs.Append("remark as \"ic_trans_detail.remark\",");

                                __extraDetailField.Append("remark," + _g.d.ic_trans_detail._chq_number + "," + _g.d.ic_trans_detail._fee_amount + ",");

                                __priceField = "price";
                                __sumAmountField = "sum_amount";
                                __sumAmountTransField = "sum_amount";

                            }
                            break;
                        default:
                            __whereHouse.Append("wh_code as \"ic_trans_detail.wh_code\",");
                            __whereHouse.Append("shelf_code as \"ic_trans_detail.shelf_code\",");
                            __priceField = "price";
                            __sumAmountField = "sum_amount";
                            __sumAmountTransField = "sum_amount";
                            break;
                    }

                    #endregion

                    // toe 
                    //string __detailWhere = "";
                    //if (this._mode == _reportEnum.ขาย_ขายสินค้าและบริการ)
                    //{
                    //    __detailWhere = " where doc_ref_trans is null";
                    //}

                    StringBuilder __queryDetail = new StringBuilder();
                    switch (this._mode)
                    {
                        case _reportEnum.ซื้อ_รายงานตัดเงินจ่ายล่วงหน้า:
                        case _reportEnum.ซื้อ_รายงานตัดเงินมัดจำจ่าย:
                            {
                                string __transFlag = "";
                                switch (this._mode)
                                {
                                    case _reportEnum.ซื้อ_รายงานตัดเงินจ่ายล่วงหน้า:
                                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน).ToString();
                                        break;
                                    case _reportEnum.ซื้อ_รายงานตัดเงินมัดจำจ่าย:
                                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน).ToString();
                                        break;
                                }
                                __queryDetail.Append("select ");
                                __queryDetail.Append("trans_flag as \"cb_trans_detail.trans_flag\",");
                                __queryDetail.Append("doc_date as \"cb_trans_detail.doc_date\",");
                                __queryDetail.Append("doc_no as \"cb_trans_detail.doc_no\",");
                                __queryDetail.Append("trans_number as \"cb_trans_detail.trans_number\",");
                                __queryDetail.Append("amount as \"cb_trans_detail.amount\"");
                                __queryDetail.Append(" from ");
                                __queryDetail.Append("(select trans_flag,doc_date,doc_no,trans_number,amount from cb_trans_detail where cb_trans_detail.last_status=0");
                                __queryDetail.Append(" union all ");
                                __queryDetail.Append("select trans_flag,doc_date,doc_no,doc_ref as trans_number,total_amount as amount from ic_trans where ic_trans.trans_flag=" + __transFlag + " and ic_trans.last_status=0) as temp2");
                                __queryDetail.Append(" order by doc_date,doc_no");
                            }
                            break;
                        case _reportEnum.ขาย_รับเงินมัดจำ_รายงานตัดเงิน:
                        case _reportEnum.ขาย_รับเงินล่วงหน้า_รายงานตัดเงิน:
                            {
                                string __transFlag = "";
                                switch (this._mode)
                                {
                                    case _reportEnum.ขาย_รับเงินล่วงหน้า_รายงานตัดเงิน:
                                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน).ToString();
                                        break;
                                    case _reportEnum.ขาย_รับเงินมัดจำ_รายงานตัดเงิน:
                                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน).ToString();
                                        break;
                                }
                                __queryDetail.Append("select ");
                                __queryDetail.Append("trans_flag as \"cb_trans_detail.trans_flag\",");
                                __queryDetail.Append("doc_date as \"cb_trans_detail.doc_date\",");
                                __queryDetail.Append("doc_no as \"cb_trans_detail.doc_no\",");
                                __queryDetail.Append("trans_number as \"cb_trans_detail.trans_number\",");
                                __queryDetail.Append("amount as \"cb_trans_detail.amount\"");
                                __queryDetail.Append(" from ");
                                __queryDetail.Append("(select trans_flag,doc_date,doc_no,trans_number,amount from cb_trans_detail where cb_trans_detail.last_status=0");
                                __queryDetail.Append(" union all ");
                                __queryDetail.Append("select trans_flag,doc_date,doc_no,doc_ref as trans_number,total_amount as amount from ic_trans where ic_trans.trans_flag=" + __transFlag + " and ic_trans.last_status=0) as temp2");
                                __queryDetail.Append(" order by doc_date,doc_no");
                            }
                            break;
                        default:
                            {
                                __queryDetail.Append("select " + __extraDetailAs.ToString());
                                __queryDetail.Append("doc_date as \"ic_trans.doc_date\","); // กำหนดเป็น ic_trans เพื่อใช้สำหรับ datatable select
                                __queryDetail.Append("doc_no as \"ic_trans.doc_no\","); // กำหนดเป็น ic_trans เพื่อใช้สำหรับ datatable select
                                __queryDetail.Append("item_code as \"ic_trans_detail.item_code\",");
                                __queryDetail.Append("barcode as \"ic_trans_detail.barcode\",");
                                __queryDetail.Append(__whereHouse.ToString());
                                __queryDetail.Append("unit_code||'~'||coalesce((select name_1 from ic_unit where ic_unit.code=unit_code),'') as \"ic_trans_detail.unit_code\",");
                                __queryDetail.Append("qty as \"ic_trans_detail.qty\",");
                                __queryDetail.Append("temp_float_1 as \"ic_trans_detail.temp_float_1\",");
                                __queryDetail.Append("temp_float_2 as \"ic_trans_detail.temp_float_2\",");
                                __queryDetail.Append("vat_type as \"ic_trans_detail.vat_type\",");
                                __queryDetail.Append("ref_row as \"ic_trans_detail.ref_row\",");
                                __queryDetail.Append(__priceField + " as \"ic_trans_detail.price\",");
                                __queryDetail.Append(__sumAmountField + " as \"ic_trans_detail.sum_amount\"");
                                __queryDetail.Append(" from (select " + __extraDetailField.ToString());
                                __queryDetail.Append("doc_date,doc_no,item_code,barcode,wh_code,shelf_code,wh_code_2,shelf_code_2,vat_type,ref_row,unit_code,temp_float_1,temp_float_2,qty,line_number,(select ic_trans.doc_ref from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no) as doc_ref_trans," + __priceField + "," + __sumAmountTransField);
                                __queryDetail.Append(" from ic_trans_detail where trans_flag in(" + _g.g._transFlagGlobal._transFlag(this._transFlag).ToString() + ") "); // and ic_trans_detail.doc_date between " + __beginDate + " and " + __endDate);
                                if (__beginTime.Length > 0 && __endTime.Length > 0 && (_mode == _reportEnum.ขาย_รับเงินล่วงหน้า || _mode == _reportEnum.ขาย_รับเงินล่วงหน้า_คืน || _mode == _reportEnum.ขาย_รับเงินมัดจำ || _mode == _reportEnum.ขาย_รับเงินมัดจำ_คืน))
                                {
                                    //and to_timestamp(date('@to_date@') || ' ' || case when '@to_time@' = '' then '23:59' else '@to_time@' end , 'YYYY/MM/DD HH24:MI')::timestamp
                                    string __beginTimeStr = "  to_timestamp(date(" + __beginDate + ") || ' ' || '" + __beginTime + "' , 'YYYY/MM/DD HH24:MI')::timestamp  ";
                                    string __endTimeStr = "  to_timestamp(date(" + __endDate + ") || ' ' || '" + __endTime + "' , 'YYYY/MM/DD HH24:MI')::timestamp  ";
                                    __queryDetail.Append(" and to_timestamp(date(ic_trans_detail.doc_date) || ' ' || ic_trans_detail.doc_time, 'YYYY/MM/DD HH24:MI')::timestamp between " + __beginTimeStr + " and " + __endTimeStr);
                                }
                                else
                                {
                                    __queryDetail.Append(" and ic_trans_detail.doc_date between " + __beginDate + " and " + __endDate);
                                }

                                __queryDetail.Append(__lastStatusWhere + __getWhere + ") as temp1 order by doc_date,doc_no,line_number");
                            }
                            break;
                    }
                    //
                    string __queryHeadStr = __query.ToString();
                    string __queryDetailStr = __queryDetail.ToString();
                    this._dataTable = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __queryHeadStr).Tables[0];
                    this._dataTableDetail = (this._displayDetail) ? __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __queryDetailStr).Tables[0] : null;
                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.Message.ToString());
                }
            }
        }

        DataRow[] _report__dataRowSelect(SMLReport._generateLevelClass levelParent, SMLReport._generateLevelClass level, DataRow source)
        {
            if (this._dataTable == null)
            {
                return null;
            }
            if (level._levelName.Equals(this._levelNameRoot))
            {
                return this._dataTable.Select();
            }
            else
            {
                try
                {
                    if (level._levelName.Equals(this._levelNameDetail))
                    {
                        StringBuilder __where = new StringBuilder();
                        switch (this._mode)
                        {
                            case _reportEnum.ขาย_รับเงินมัดจำ_รายงานตัดเงิน:
                            case _reportEnum.ขาย_รับเงินล่วงหน้า_รายงานตัดเงิน:
                            case _reportEnum.ซื้อ_รายงานตัดเงินจ่ายล่วงหน้า:
                            case _reportEnum.ซื้อ_รายงานตัดเงินมัดจำจ่าย:
                                {
                                    int __refColumn = levelParent._findColumnName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                                    __where.Append(_g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._trans_number + "=\'" + source[levelParent._columnList[__refColumn]._fieldName].ToString() + "\'");
                                }
                                break;
                            default:
                                {
                                    Boolean __foundIsWhere = false;
                                    for (int __loop = 0; __loop < levelParent._columnList.Count; __loop++)
                                    {
                                        if (levelParent._columnList[__loop]._isWhere)
                                        {
                                            __foundIsWhere = true;
                                            break;
                                        }
                                    }
                                    if (__foundIsWhere == false)
                                    {
                                        // Report ที่ไม่ได้กำหนดการค้นหา Detail ให้เอา 2 Field แรก
                                        for (int __loop = 0; __loop < 2; __loop++)
                                        {
                                            if (levelParent._columnList[__loop]._fieldName.Length > 0)
                                            {
                                                if (__where.Length > 0)
                                                {
                                                    __where.Append(" and ");
                                                }
                                                __where.Append(levelParent._columnList[__loop]._fieldName + "=\'" + source[levelParent._columnList[__loop]._fieldName].ToString().Replace("\'", "\'\'") + "\'");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        for (int __loop = 0; __loop < levelParent._columnList.Count; __loop++)
                                        {
                                            if (levelParent._columnList[__loop]._isWhere)
                                            {
                                                if (levelParent._columnList[__loop]._fieldName.Length > 0)
                                                {
                                                    if (__where.Length > 0)
                                                    {
                                                        __where.Append(" and ");
                                                    }
                                                    __where.Append(levelParent._columnList[__loop]._fieldName + "=\'" + source[levelParent._columnList[__loop]._fieldName].ToString().Replace("\'", "\'\'") + "\'");
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
                        }
                        return (this._dataTableDetail == null || this._dataTableDetail.Rows.Count == 0) ? null : this._dataTableDetail.Select(__where.ToString());
                    }
                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.Message.ToString());
                }
            }
            return null;
        }


        Font _report__renderFont(DataRow data, SMLReport._generateColumnListClass source, SMLReport._generateLevelClass sender)
        {
            if (sender._levelName.Equals(_levelNameDetail))
            {
                int __columnNumber = this._dataTableDetail.Columns.IndexOf(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._ref_row);
                if (__columnNumber != -1)
                {
                    if ((int)MyLib._myGlobal._decimalPhase(data[__columnNumber].ToString()) == -1)
                    {
                        return new Font(source._font.FontFamily, source._font.Size, FontStyle.Italic);
                    }
                }
            }
            return source._font;
        }

        /// <summary>
        /// ไม่ได้ใช้
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="columnName"></param>
        /// <param name="mode">0=ปรับให้เป็นแยกนอก,1=ปรับให้เป็นรวมใน</param>
        void _changeValueVatType(SMLReport._generateLevelClass sender, string columnName, int columnNumber, int __vatTypeColumnNumber)
        {
            int __columnNumber = sender._findColumnName(columnName);
            if (__columnNumber != -1 && __columnNumber == columnNumber && __vatTypeColumnNumber != -1)
            {
                // ดูประเภทภาษี
                int __vatType = (int)MyLib._myGlobal._decimalPhase(sender._columnList[__vatTypeColumnNumber]._dataStr);
                //
                switch (this._vatNumberType)
                {
                    case _reportVatNumberType.แสดงตัวเลขแบบแยกนอกทั้งหมด:
                        switch (__vatType)
                        {
                            case 1:
                                {
                                    // กรณีข้อมูลเป็นแบบรวมใน ให้เอามาแยกนอก
                                    decimal __value = sender._columnList[__columnNumber]._dataNumber * 100M / 107M;
                                    sender._columnList[__columnNumber]._dataNumber = __value;
                                }
                                break;
                        }
                        break;
                    case _reportVatNumberType.แสดงตัวเลขแบบรวมในทั้งหมด:
                        switch (__vatType)
                        {
                            case 0:
                                {
                                    // กรณีข้อมูลเป็นแบบแยกนอก ให้เอามารวมใน
                                    decimal __value = sender._columnList[__columnNumber]._dataNumber + (sender._columnList[__columnNumber]._dataNumber * (7M / 100M));
                                    sender._columnList[__columnNumber]._dataNumber = __value;
                                }
                                break;
                        }
                        break;
                }
            }
        }

        void _report__renderValue(SMLReport._generateLevelClass sender, int columnNumber, SMLReport._generateColumnStyle isTotal)
        {
            if (isTotal == SMLReport._generateColumnStyle.Data)
            {
                switch (this._mode)
                {
                    case _reportEnum.ซื้อ_รายงานตัดเงินจ่ายล่วงหน้า:
                    case _reportEnum.ซื้อ_รายงานตัดเงินมัดจำจ่าย:
                    case _reportEnum.ขาย_รับเงินมัดจำ_รายงานตัดเงิน:
                    case _reportEnum.ขาย_รับเงินล่วงหน้า_รายงานตัดเงิน:
                        {
                            int __transFlagColumnNumber = sender._findColumnName(_g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._trans_flag);
                            if (__transFlagColumnNumber != -1 && __transFlagColumnNumber == columnNumber)
                            {
                                int __value = (int)MyLib._myGlobal._decimalPhase(sender._columnList[columnNumber]._dataStr);
                                sender._columnList[columnNumber]._dataStr = _g.g._transFlagGlobal._transName(__value);
                            }
                        }
                        break;
                }
                int __vatTypeColumnNumber = sender._findColumnName(_g.d.ic_trans._table + "." + _g.d.ic_trans._vat_type);
                if (__vatTypeColumnNumber != -1 && __vatTypeColumnNumber == columnNumber)
                {
                    // 0=แยกนอก
                    // 1=รวมใน
                    // 2=ยกเว้น
                    switch ((int)MyLib._myGlobal._decimalPhase(sender._columnList[columnNumber]._dataStr))
                    {
                        case 0: sender._columnList[columnNumber]._dataStr = "E"; break;
                        case 1: sender._columnList[columnNumber]._dataStr = "I"; break;
                        case 2: sender._columnList[columnNumber]._dataStr = "C"; break;
                    }
                }
                // ปรับภาษี ตามเงื่อนไข (ส่วนหัว)
                if (__vatTypeColumnNumber != -1)
                {
                    this._changeValueVatType(sender, _g.d.ic_trans._table + "." + _g.d.ic_trans._total_value, columnNumber, __vatTypeColumnNumber);
                    this._changeValueVatType(sender, _g.d.ic_trans._table + "." + _g.d.ic_trans._total_discount, columnNumber, __vatTypeColumnNumber);
                    this._changeValueVatType(sender, _g.d.ic_trans._table + "." + _g.d.ic_trans._total_after_discount, columnNumber, __vatTypeColumnNumber);
                }
                int __vatTypeDetailColumnNumber = sender._findColumnName(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._tax_type);
                if (__vatTypeDetailColumnNumber != -1 && __vatTypeDetailColumnNumber == columnNumber)
                {
                    // 0=แยกนอก
                    // 1=รวมใน
                    // 2=ยกเว้น
                    switch ((int)MyLib._myGlobal._decimalPhase(sender._columnList[columnNumber]._dataStr))
                    {
                        case 0: sender._columnList[columnNumber]._dataStr = "V"; break;
                        case 1: sender._columnList[columnNumber]._dataStr = ""; break;
                    }
                }
                // ปรับภาษี ตามเงื่อนไข (ส่วนรายละเอียด)
                if (__vatTypeDetailColumnNumber != -1)
                {
                    this._changeValueVatType(sender, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, columnNumber, __vatTypeDetailColumnNumber);
                    this._changeValueVatType(sender, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._discount_amount, columnNumber, __vatTypeDetailColumnNumber);
                    this._changeValueVatType(sender, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, columnNumber, __vatTypeDetailColumnNumber);
                }
                //
                int __lastStatusColumnNumber = sender._findColumnName(_g.d.ic_trans._table + "." + _g.d.ic_trans._last_status);
                if (__lastStatusColumnNumber != -1 && __lastStatusColumnNumber == columnNumber)
                {
                    // last_status,used_status,doc_success,not_approve,on_hold,approve_status
                    string[] __str = sender._columnList[columnNumber]._dataStr.Split(',');
                    string __message = "";
                    if (__str.Length > 1)
                    {
                        int __lastStatus = Int32.Parse(__str[0]);
                        int __usedStatus = Int32.Parse(__str[1]);
                        int __docSuccess = Int32.Parse(__str[2]);
                        int __notApprove = Int32.Parse(__str[3]);
                        int __onHold = Int32.Parse(__str[4]);
                        int __approveStatus = Int32.Parse(__str[5]);
                        int __expireDateStatus = Int32.Parse(__str[6]);
                        //
                        switch (this._mode)
                        {
                            case _reportEnum.ซื้อ_รายงานค้างรับตามใบสั่งซื้อ:
                            case _reportEnum.ซื้อ_รายงานใบสั่งซื้อ:
                            case _reportEnum.ซื้อ_รายงานสถานะใบสั่งซื้อ:
                                switch (__approveStatus)
                                {
                                    case 0: __message = MyLib._myGlobal._resource("รออนุมัติ"); break;
                                    case 1: __message = MyLib._myGlobal._resource("อนุมัติเรียบร้อย"); break;
                                    case 2: __message = MyLib._myGlobal._resource("ไม่อนุมัติ"); break;
                                }
                                if (__onHold == 1) __message = MyLib._myGlobal._resource("ระงับชั่วคราว");
                                if (__lastStatus == 1) __message = MyLib._myGlobal._resource("ยกเลิก");
                                if (__expireDateStatus == 1) __message = MyLib._myGlobal._resource("เอกสารหมดอายุ");
                                if (__docSuccess == 1) __message = MyLib._myGlobal._resource("มีการรับสินค้า");
                                break;
                            case _reportEnum.ซื้อ_ใบเสนอซื้อ:
                            case _reportEnum.ซื้อ_ใบเสนอซื้อ_สถานะ:
                                switch (__approveStatus)
                                {
                                    case 0: __message = MyLib._myGlobal._resource("รออนุมัติ"); break;
                                    case 1: __message = MyLib._myGlobal._resource("อนุมัติเรียบร้อย"); break;
                                    case 2: __message = MyLib._myGlobal._resource("ไม่อนุมัติ"); break;
                                }
                                if (__onHold == 1) __message = MyLib._myGlobal._resource("ระงับชั่วคราว");
                                if (__lastStatus == 1) __message = MyLib._myGlobal._resource("ยกเลิก");
                                if (__expireDateStatus == 1) __message = MyLib._myGlobal._resource("เอกสารหมดอายุ");
                                break;
                            case _reportEnum.ซื้อ_ใบเสนอซื้อ_อนุมัติ:
                                if (__docSuccess == 0) __message = MyLib._myGlobal._resource("รอออกใบสั่งซื้อ");
                                if (__docSuccess == 1) __message = MyLib._myGlobal._resource("ออกใบสั่งซื้อเรียบร้อย");
                                break;
                            case _reportEnum.ขาย_เสนอราคา:
                            case _reportEnum.ขาย_เสนอราคา_สถานะ:
                                switch (__approveStatus)
                                {
                                    case 0: __message = MyLib._myGlobal._resource("รออนุมัติ"); break;
                                    case 1: __message = MyLib._myGlobal._resource("อนุมัติเรียบร้อย"); break;
                                    case 2: __message = MyLib._myGlobal._resource("ไม่อนุมัติ"); break;
                                }
                                if (__onHold == 1) __message = MyLib._myGlobal._resource("ระงับชั่วคราว");
                                if (__lastStatus == 1) __message = MyLib._myGlobal._resource("ยกเลิก");
                                if (__expireDateStatus == 1) __message = MyLib._myGlobal._resource("เอกสารหมดอายุ");
                                break;
                            case _reportEnum.ขาย_เสนอราคา_อนุมัติ:
                                if (__docSuccess == 0) __message = MyLib._myGlobal._resource("รอออกใบสั่งซื้อ");
                                if (__docSuccess == 1) __message = MyLib._myGlobal._resource("ออกใบสั่งซื้อเรียบร้อย");
                                break;
                            case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                            case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_สถานะ:
                                switch (__approveStatus)
                                {
                                    case 0: __message = MyLib._myGlobal._resource("รออนุมัติ"); break;
                                    case 1: __message = MyLib._myGlobal._resource("อนุมัติเรียบร้อย"); break;
                                    case 2: __message = MyLib._myGlobal._resource("ไม่อนุมัติ"); break;
                                }
                                if (__onHold == 1) __message = MyLib._myGlobal._resource("ระงับชั่วคราว");
                                if (__lastStatus == 1) __message = MyLib._myGlobal._resource("ยกเลิก");
                                if (__expireDateStatus == 1) __message = MyLib._myGlobal._resource("เอกสารหมดอายุ");
                                break;
                            case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ:
                                if (__docSuccess == 0) __message = MyLib._myGlobal._resource("รอออกใบสั่งซื้อ");
                                if (__docSuccess == 1) __message = MyLib._myGlobal._resource("ออกใบสั่งซื้อเรียบร้อย");
                                break;
                            case _reportEnum.ขาย_สั่งขาย:
                            case _reportEnum.ขาย_สั่งขาย_สถานะ:
                                switch (__approveStatus)
                                {
                                    case 0: __message = MyLib._myGlobal._resource("รออนุมัติ"); break;
                                    case 1: __message = MyLib._myGlobal._resource("อนุมัติเรียบร้อย"); break;
                                    case 2: __message = MyLib._myGlobal._resource("ไม่อนุมัติ"); break;
                                }
                                if (__onHold == 1) __message = MyLib._myGlobal._resource("ระงับชั่วคราว");
                                if (__lastStatus == 1) __message = MyLib._myGlobal._resource("ยกเลิก");
                                if (__expireDateStatus == 1) __message = MyLib._myGlobal._resource("เอกสารหมดอายุ");
                                break;
                            case _reportEnum.ขาย_สั่งขาย_อนุมัติ:
                                if (__docSuccess == 0) __message = MyLib._myGlobal._resource("รอออกใบสั่งซื้อ");
                                if (__docSuccess == 1) __message = MyLib._myGlobal._resource("ออกใบสั่งซื้อเรียบร้อย");
                                break;
                            default:
                                if (__onHold == 1) __message = MyLib._myGlobal._resource("ระงับชั่วคราว");
                                if (__usedStatus == 1) __message = MyLib._myGlobal._resource("อ้างอิงบางส่วน");
                                if (__docSuccess == 1) __message = MyLib._myGlobal._resource("อ้างอิงครบ");
                                if (__notApprove == 1) __message = MyLib._myGlobal._resource("ไม่อนุมัติ");
                                if (__lastStatus == 1) __message = MyLib._myGlobal._resource("ยกเลิก");
                                break;
                        }
                    }
                    sender._columnList[columnNumber]._dataStr = __message;
                }
            }
            //
            switch (this._mode)
            {
                case _reportEnum.ซื้อ_ใบเสนอซื้อ:
                case _reportEnum.ซื้อ_ใบเสนอซื้อ_อนุมัติ:
                case _reportEnum.ซื้อ_ใบเสนอซื้อ_ยกเลิก:
                case _reportEnum.ซื้อ_ใบเสนอซื้อ_สถานะ:
                    {
                        if (isTotal == SMLReport._generateColumnStyle.Data)
                        {
                            int __inquiryTypeColumnNumber = sender._findColumnName(_g.d.ic_trans._table + "." + _g.d.ic_trans._inquiry_type);
                            if (columnNumber == __inquiryTypeColumnNumber)
                            {
                                int __type = (int)MyLib._myGlobal._decimalPhase(sender._columnList[columnNumber]._dataStr);
                                string __fieldName = _g.d.ic_trans._table + "." + _g.g._purchaseType[__type];
                                sender._columnList[columnNumber]._dataStr = MyLib._myResource._findResource(__fieldName, __fieldName)._str;
                            }
                        }
                    }
                    break;
                case _reportEnum.ขาย_เสนอราคา:
                case _reportEnum.ขาย_เสนอราคา_ยกเลิก:
                case _reportEnum.ขาย_เสนอราคา_สถานะ:
                case _reportEnum.ขาย_เสนอราคา_อนุมัติ:
                    {
                        if (isTotal == SMLReport._generateColumnStyle.Data)
                        {
                            int __inquiryTypeColumnNumber = sender._findColumnName(_g.d.ic_trans._table + "." + _g.d.ic_trans._inquiry_type);
                            if (columnNumber == __inquiryTypeColumnNumber)
                            {
                                int __type = (int)MyLib._myGlobal._decimalPhase(sender._columnList[columnNumber]._dataStr);
                                string __fieldName = _g.d.ic_trans._table + "." + _g.g._saleType[__type];
                                sender._columnList[columnNumber]._dataStr = MyLib._myResource._findResource(__fieldName, __fieldName)._str;
                            }
                        }
                    }
                    break;
                case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_สถานะ:
                case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ:
                    {
                        if (isTotal == SMLReport._generateColumnStyle.Data)
                        {
                            int __inquiryTypeColumnNumber = sender._findColumnName(_g.d.ic_trans._table + "." + _g.d.ic_trans._inquiry_type);
                            if (columnNumber == __inquiryTypeColumnNumber)
                            {
                                int __type = (int)MyLib._myGlobal._decimalPhase(sender._columnList[columnNumber]._dataStr);
                                string __fieldName = _g.d.ic_trans._table + "." + _g.g._saleType[__type];
                                sender._columnList[columnNumber]._dataStr = MyLib._myResource._findResource(__fieldName, __fieldName)._str;
                            }
                        }
                    }
                    break;
                case _reportEnum.ขาย_สั่งขาย:
                case _reportEnum.ขาย_สั่งขาย_ยกเลิก:
                case _reportEnum.ขาย_สั่งขาย_สถานะ:
                case _reportEnum.ขาย_สั่งขาย_อนุมัติ:
                    {
                        if (isTotal == SMLReport._generateColumnStyle.Data)
                        {
                            int __inquiryTypeColumnNumber = sender._findColumnName(_g.d.ic_trans._table + "." + _g.d.ic_trans._inquiry_type);
                            if (columnNumber == __inquiryTypeColumnNumber)
                            {
                                int __type = (int)MyLib._myGlobal._decimalPhase(sender._columnList[columnNumber]._dataStr);
                                string __fieldName = _g.d.ic_trans._table + "." + _g.g._saleType[__type];
                                sender._columnList[columnNumber]._dataStr = MyLib._myResource._findResource(__fieldName, __fieldName)._str;
                            }
                        }
                    }
                    break;
            }
        }

        private void _reportInitRootColumn(List<SMLReport._generateColumnListClass> columnList)
        {
            FontStyle __fontStyle = (this._displayDetail) ? FontStyle.Bold : FontStyle.Regular;
            switch (this._mode)
            {
                case _reportEnum.เช็คจ่าย_ยกมา:
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 10, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code, _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_code, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_name, _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_name, 40, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_amount, null, 20, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));

                    }
                    break;
                case _reportEnum.เช็คจ่าย_ผ่าน:
                case _reportEnum.เช็คจ่าย_คืน:
                case _reportEnum.เช็คจ่าย_เปลี่ยนเช็ค:
                case _reportEnum.เช็คจ่าย_ขาดสิทธิ์:
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 10, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_amount, null, 20, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));

                    }
                    break;
                case _reportEnum.เช็ครับ_ยกมา:
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 10, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code, _g.d.ic_trans._table + "." + _g.d.ic_trans._ar_code, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_name, _g.d.ic_trans._table + "." + _g.d.ic_trans._ar_name, 40, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_amount, null, 20, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                    }
                    break;
                case _reportEnum.เช็ครับ_ฝาก:
                case _reportEnum.เช็ครับ_ผ่าน:
                case _reportEnum.เช็ครับ_รับคืน:
                case _reportEnum.เช็ครับ_ขาดสิทธิ์:
                case _reportEnum.เช็ครับ_เปลี่ยนเช็ค:
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 10, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 70, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_amount, null, 20, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                    }
                    break;
                case _reportEnum.เงินสดธนาคาร_รายได้อื่น_ยกเลิก:
                case _reportEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก:
                case _reportEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก:

                case _reportEnum.เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก:
                case _reportEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก:
                case _reportEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก:

                case _reportEnum.เงินสดย่อย_เบิก_ยกเลิก:
                case _reportEnum.เงินสดย่อย_รับคืน_ยกเลิก:

                case _reportEnum.ธนาคาร_ฝากเงิน_ยกเลิก:
                case _reportEnum.ธนาคาร_ถอนเงิน_ยกเลิก:
                case _reportEnum.ธนาคาร_โอนเงิน_ยกเลิก:

                case _reportEnum.เช็ครับ_ฝาก_ยกเลิก:
                case _reportEnum.เช็ครับ_ผ่าน_ยกเลิก:
                case _reportEnum.เช็ครับ_รับคืน_ยกเลิก:
                case _reportEnum.เช็ครับ_ขาดสิทธิ์_ยกเลิก:
                case _reportEnum.เช็ครับ_เปลี่ยน_ยกเลิก:

                case _reportEnum.เช็คจ่าย_ผ่าน_ยกเลิก:
                case _reportEnum.เช็คจ่าย_คืน_ยกเลิก:
                case _reportEnum.เช็คจ่าย_ขาดสิทธิ์_ยกเลิก:
                case _reportEnum.เช็คจ่าย_เปลี่ยน_ยกเลิก:


                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 10, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref_date, null, 10, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                    break;

                case _reportEnum.เงินสดธนาคาร_รายได้อื่น:
                case _reportEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                case _reportEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                case _reportEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                case _reportEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                case _reportEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                    {
                        string __custCodeField = "";
                        string __custNameField = "";
                        if (_global._reportType(this._mode) == _reportTypeEnum.เจ้าหนี้)
                        {
                            __custCodeField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_code;
                            __custNameField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_name;
                        }
                        else
                        {
                            __custCodeField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ar_code;
                            __custNameField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ar_name;
                        }

                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 10, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 12, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_time, null, 8, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref, null, 12, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref_date, null, 12, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code, __custCodeField, 12, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_name, __custNameField, 30, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_value, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_discount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_after_discount, null, 12, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_except_vat, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._vat_rate, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_vat_value, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._vat_type, null, 5, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cashier_code, null, 5, SMLReport._report._cellType.String, 0, __fontStyle));
                    }
                    break;
                case SMLERPReportTool._reportEnum.เงินสดย่อย_เบิก:
                case SMLERPReportTool._reportEnum.เงินสดย่อย_รับคืน:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 10, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_time, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._remark, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));

                    break;
                case _reportEnum.ธนาคาร_โอนเงิน:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 10, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_time, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._remark, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    break;

                case _reportEnum.บัตรเครดิต_ขึ้นเงิน:
                case _reportEnum.บัตรเครดิต_ยกเลิก:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 10, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_time, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._remark, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    break;
            }
        }

        private void _reportInitColumnValue(List<SMLReport._generateColumnListClass> columnList)
        {
            switch (this._mode)
            {
                case _reportEnum.เช็คจ่าย_ยกมา:
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._book_bank_code, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._bank_name, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._bank_branch, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._chq_number, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._date_due, null, 15, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, null, 15, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._remark, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    }
                    break;
                case _reportEnum.เช็คจ่าย_ผ่าน:
                case _reportEnum.เช็คจ่าย_คืน:
                case _reportEnum.เช็คจ่าย_ขาดสิทธิ์:
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._book_bank_code, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._bank_name, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._bank_branch, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._chq_number, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._date_due, null, 15, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_of_cost, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._expense, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._amount2, 15, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._remark, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));

                    }
                    break;
                case _reportEnum.เช็คจ่าย_เปลี่ยนเช็ค:
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._chq_number, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._bank_name, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._bank_branch, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._date_due, null, 15, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, 15, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));

                    }
                    break;
                case _reportEnum.เช็ครับ_ยกมา:
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._chq_number, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._bank_name, null, 25, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._bank_branch, null, 25, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._date_due, null, 10, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, null, 15, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._remark, null, 25, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    }
                    break;
                case _reportEnum.เช็ครับ_ฝาก:
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._book_bank_code, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._bank_name, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._bank_branch, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._chq_number, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, null, 15, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._remark, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    }
                    break;
                case _reportEnum.เช็ครับ_เปลี่ยนเช็ค:
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._chq_number, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._bank_name, null, 25, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._bank_branch, null, 25, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._date_due, null, 15, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, null, 20, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        //columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._remark, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    }
                    break;
                case _reportEnum.เช็ครับ_ผ่าน:
                case _reportEnum.เช็ครับ_รับคืน:
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._book_bank_code, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._bank_name, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._bank_branch, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._chq_number, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_of_cost, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._expense, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._amount2, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._remark, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    }
                    break;
                case _reportEnum.เช็ครับ_ขาดสิทธิ์:
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._chq_number, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, 20, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_of_cost, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._expense, 20, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._amount2, 20, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._remark, null, 30, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    }
                    break;

                case _reportEnum.เงินสดธนาคาร_รายได้อื่น:
                case _reportEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                case _reportEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                case _reportEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                case _reportEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                case _reportEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                    {
                        string __itemCodeField = "";
                        string __itemNameField = "";
                        if (_global._reportType(this._mode) == _reportTypeEnum.เจ้าหนี้)
                        {
                            __itemCodeField = _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._expense_code;
                            __itemNameField = _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._expense_name;
                        }
                        else
                        {
                            __itemCodeField = _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._income_code;
                            __itemNameField = _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._income_name;
                        }
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, __itemCodeField, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, __itemNameField, 30, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._remark, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._detail, 45, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._amount2, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    } break;
                case SMLERPReportTool._reportEnum.เงินสดย่อย_เบิก:
                case SMLERPReportTool._reportEnum.เงินสดย่อย_รับคืน:
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._cash_sub_code, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._cash_sub_name, 30, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._remark, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._detail, 45, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._amount2, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    }
                    break;
                case _reportEnum.ธนาคาร_โอนเงิน:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._book_bank_code_out, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._bank_name, _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._name_1, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._bank_branch, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._bank_name, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code_2, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._book_bank_code_in, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._bank_name_2, _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._name_1, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._bank_branch_2, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._bank_name_2, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._remark, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._detail, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._transfer_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._fee_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._amount2, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    break;
                case _reportEnum.บัตรเครดิต_ขึ้นเงิน:
                case _reportEnum.บัตรเครดิต_ยกเลิก:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._book_bank_code, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._bank_name, _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._name_1, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._bank_branch, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._bank_name, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._chq_number, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._credit_card_no, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._remark, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._detail, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._fee_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._amount2, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));

                    break;
            }
        }

        SMLReport._generateLevelClass _reportInitRoot(SMLReport._generateLevelClass levelParent, Boolean sumTotal, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            this._reportInitRootColumn(__columnList);
            return this._report._addLevel(this._levelNameRoot, levelParent, __columnList, sumTotal, autoWidth);
        }

        SMLReport._generateLevelClass _reportInitDetail(SMLReport._generateLevelClass levelParent, Boolean sumTotal, float spaceFirstColumnWidth, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            this._reportInitDetailColumn(__columnList, spaceFirstColumnWidth);
            this._reportInitColumnValue(__columnList);
            return this._report._addLevel(this._levelNameDetail, levelParent, __columnList, sumTotal, autoWidth);
        }

        private void _reportInitDetailColumn(List<SMLReport._generateColumnListClass> columnList, float spaceFirstColumnWidth)
        {
            columnList.Add(new SMLReport._generateColumnListClass("", "", spaceFirstColumnWidth, SMLReport._report._cellType.String, 0));
        }

        void _report_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _report__showCondition(string screenName)
        {
            this._showCondition();
        }

        void _report__init()
        {
            //this._extraWhere = extraWhere;
            this._displayDetail = this._form_condition._screen._getDataStr(_g.d.resource_report._display_detail).ToString().Equals("1") ? true : false;
            this._showBarcode = this._form_condition._screen._getDataStr(_g.d.resource_report._show_barcode).ToString().Equals("1") ? true : false;
            this._report._level = this._reportInitRoot(null, true, true);
            if (this._displayDetail == true)
            {
                this._level2 = this._reportInitDetail(this._report._level, false, 2, true);
            }
            this._report._resourceTable = ""; // แบบกำหนดเอง
            // ยอดรวมแบบ Grand Total
            this._report._level.__grandTotal = new List<SMLReport._generateColumnListClass>();
            this._reportInitRootColumn(this._report._level.__grandTotal);
            this._report._level._levelGrandTotal = this._report._level;
        }

        private void _showCondition()
        {
            if (this._form_condition == null)
            {
                this._form_condition = new SMLERPReportTool._conditionScreen(this._mode, this._screenName);
                this._report__init();
                this._form_condition._extra._tableName = _g.d.ic_trans._table;
                string[] __fieldList = this._report._level.__fieldList(false).Split(',');
                this._form_condition._extra._addFieldComboBox(__fieldList);
                //this._form_condition._title = this._screenName;
            }
            this._form_condition.ShowDialog();
            if (this._form_condition._processClick)
            {
                this._printNow();
            }
        }

        private void _printNow()
        {
            this._dataTable = null; // จะได้ load data ใหม่
            // แสดงเงื่อนไข
            this._report._conditionTextDetail = "";
            StringBuilder __conditionDetail = new StringBuilder();
            string __beginDate = this._form_condition._screen._getDataStr(_g.d.resource_report._from_date);
            string __endDate = this._form_condition._screen._getDataStr(_g.d.resource_report._to_date);
            int __getVatType = (int)MyLib._myGlobal._decimalPhase(this._form_condition._screen._getDataStr(_g.d.resource_report._vat_display_type));
            switch (__getVatType)
            {
                case 0: this._vatNumberType = _reportVatNumberType.ปรกติ; break;
                case 1: this._vatNumberType = _reportVatNumberType.แสดงตัวเลขแบบแยกนอกทั้งหมด; break;
                case 2: this._vatNumberType = _reportVatNumberType.แสดงตัวเลขแบบรวมในทั้งหมด; break;
            }
            //
            this._report._conditionText = MyLib._myGlobal._resource("จากวันที่") + " : " + __beginDate + "  " + MyLib._myGlobal._resource("ถึงวันที่") + " : " + __endDate + " ";
            // ลูกหนี้
            this._report._conditionText = _g.g._conditionGrid(this._form_condition._grid, this._report._conditionText);
            //
            if (this._vatNumberType != _reportVatNumberType.ปรกติ)
            {
                this._report._conditionTextDetail = this._vatNumberType.ToString() + " ";
            }
            switch (this._mode)
            {
                case _reportEnum.สินค้า_รายงานโอนสินค้าและวัตถุดิบ:
                case _reportEnum.สินค้า_รายงานปรับปรุงสินค้าและวัตถุดิบ_ขาด:
                case _reportEnum.สินค้า_รายงานปรับปรุงสินค้าและวัตถุดิบ_เกิน:
                case _reportEnum.สินค้า_รายงานรับคืนเบิกสินค้าวัตถุดิบ:
                case _reportEnum.สินค้า_รายงานเบิกสินค้าวัตถุดิบ:
                case _reportEnum.สินค้า_รายงานรับสินค้าสำเร็จรูป:
                case _reportEnum.สินค้า_รายงานสินค้าและวัตถุดิบคงเหลือยกมา:
                case _reportEnum.สินค้า_รายงานสินค้าตรวจนับ:
                case _reportEnum.ขาย_ขายสินค้าและบริการ:
                case _reportEnum.ขาย_ขายสินค้าและบริการ_รายงานการออกใบกำกับภาษีอย่างเต็ม:
                case _reportEnum.ขาย_เพิ่มหนี้สินค้าราคาผิด:
                case _reportEnum.ขาย_รับคืนสินค้า:
                case _reportEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก:
                case _reportEnum.ขาย_เพิ่มหนี้สินค้าราคาผิด_ยกเลิก:
                case _reportEnum.ขาย_รับคืนสินค้า_ยกเลิก:
                case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_สถานะ:
                case _reportEnum.ขาย_สั่งขาย:
                case _reportEnum.ขาย_สั่งขาย_สถานะ:
                case _reportEnum.ขาย_เสนอราคา:
                case _reportEnum.ขาย_เสนอราคา_สถานะ:
                case _reportEnum.ซื้อ_เพิ่มหนี้ราคาผิด:
                case _reportEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                case _reportEnum.ซื้อ_ซื้อสินค้าและบริการ:
                case _reportEnum.ซื้อ_พาเชียล_รับสินค้า:
                case _reportEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                case _reportEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                case _reportEnum.ซื้อ_พาเชียล_ลดหนี้:
                case _reportEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                case _reportEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก:
                case _reportEnum.ซื้อ_เพิ่มหนี้ราคาผิด_ยกเลิก:
                case _reportEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก:
                case _reportEnum.ซื้อ_รายงานใบสั่งซื้อ:
                case _reportEnum.ซื้อ_รายงานค้างรับตามใบสั่งซื้อ:
                case _reportEnum.ซื้อ_รายงานยกเลิกใบสั่งซื้อ:
                case _reportEnum.ซื้อ_รายงานอนุมัติใบสั่งซื้อ:
                case _reportEnum.ซื้อ_รายงานสถานะใบสั่งซื้อ:
                case _reportEnum.ซื้อ_ใบเสนอซื้อ_สถานะ:
                case _reportEnum.ซื้อ_ใบเสนอซื้อ:
                    this._report._conditionTextDetail = this._report._conditionTextDetail + MyLib._myGlobal._resource(__conditionDetail.Append((this._form_condition._screen._getDataStr(_g.d.resource_report._show_cancel_document).ToString().Equals("1")) ? MyLib._myGlobal._resource("แสดงรายการที่ถูกยกเลิก") : " ").ToString());
                    break;
            }
            //
            this._report._build();
        }

    }
}
