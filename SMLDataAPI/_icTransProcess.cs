using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLDataAPI
{
    public class _icTransProcess : _processBase
    {
        public _icTransProcess()
        {
            this.tableName = "ic_trans";
        }
        protected override string _createTableImportCompleteScipt()
        {
            StringBuilder __query = new StringBuilder();
            __query.Append("CREATE TABLE " + this._importedTableName + "");
            __query.Append("(");
            __query.Append("doc_no character varying(25),");
            __query.Append("trans_flag smallint NOT NULL DEFAULT 0,");
            __query.Append("branch_sync character varying(100),");
            __query.Append("CONSTRAINT " + this._importedTableName + "_pk_code PRIMARY KEY (doc_no, branch_sync) ");
            __query.Append(") ");
            return __query.ToString();
        }

        protected override string getDataQuery()
        {
            StringBuilder __query = new StringBuilder();
            __query.Append(" select ");

            __query.Append(MyLib._myGlobal._fieldAndComma(
        "ignore_sync",
        "is_lock_record",
        "roworder",
        "trans_type",
        "trans_flag",
        "doc_group",
        "doc_date",
        "doc_no",
        "doc_ref",
        "doc_ref_date",
        "tax_doc_no",
        "tax_doc_date",
        "inquiry_type",
        "vat_type",
        "cust_code",
        "contactor",
        "branch_code",
        "project_code",
        "side_code",
        "department_code",
        "sale_area_code",
        "sale_code",
        "send_type",
        "send_day",
        "send_date",
        "expire_date",
        "credit_day",
        "due_date",
        "answer_type",
        "transport_value",
        "discount_word",
        "vat_rate",
        "currency_code",
        "exchange_rate",
        "total_cost",
        "total_value",
        "total_discount",
        "total_vat_value",
        "total_after_vat",
        "total_except_vat",
        "total_amount",
        "balance_amount",
        "adjust_reason",
        "approve_code",
        "approve_date",
        "remark",
        "status",
        "guid_code",
        "user_request",
        "expire_day",
        "send_mail",
        "send_sms",
        "doc_ref_trans",
        "credit_date",
        "total_before_vat",
        "doc_time",
        "allocate_code",
        "job_code",
        "delivery_date",
        "pay_amount",
        "money",
        "sum_pay_money_diff",
        "currency_money",
        "total_debt_amount",
        "cancel_type",
        "last_status",
        "used_status",
        "doc_success",
        "total_manual",
        "on_hold",
        "extra_word",
        "transport_code",
        "want_day",
        "want_date",
        "deposit_day",
        "deposit_date",
        "sale_group",
        "not_approve_1",
        "user_approve",
        "approve_status",
        "expire_status",
        "advance_amount",
        "doc_format_code",
        "used_status_2",
        "remark_2",
        "remark_3",
        "ref_amount",
        "ref_new_amount",
        "ref_diff",
        "sum_point",
        "cashier_code",
        "is_pos",
        "pos_id",
        "member_code",
        "doc_no_guid",
        "pos_bill_type",
        "pos_bill_change",
        "doc_type",
        "recheck_count_day",
        "recheck_count",
        "wh_from",
        "location_from",
        "wh_to",
        "location_to",
        "auto_create",
        "payable_sub_type_1_1",
        "payable_sub_type_1_2",
        "payable_sub_type_3",
        "payable_sub_type_4",
        "invoice_add_cash_other",
        "invoice_add_cash_service_other",
        "send_to_pick_and_pack",
        "service_charge_word",
        "total_service_charge",
        "sale_shift_id",
        "creator_code",
        "last_editor_code",
        "create_datetime",
        "lastedit_datetime",
        "table_number",
        "period_guid",
        "branch_code_to",
        "allocate_code_to",
        "project_code_to",
        "job_code_to",
        "side_code_to",
        "department_code_to",
        "point_telephone",
        "sum_point_2",
        "remark_4",
        "remark_5",
        "is_manual_vat",
        "pass_book_code",
        "total_amount_2",
        "sender_code",
        "is_cancel",
        "cancel_code",
        "cancel_datetime",
        "is_hold",
        "auto_approved",
        "pos_transfer",
        "doc_close",
        "ref_doc_type",
        "create_date_time_now",
        "is_doc_copy",
        "doc_reason",
        "branch_sync"
                ));

            __query.Append(" from {0} ");
            __query.Append(" where not exists(select doc_no from {1} where {0}.doc_no = {1}.doc_no and {0}.branch_sync={1}.branch_sync and {0}.trans_flag={1}.trans_flag  ) limit 100 ");

            return string.Format(__query.ToString(), this.tableName, this._importedTableName);
        }

        protected override string _processData(DataRow row)
        {
            ic_trans _ic_trans = new ic_trans(row);
            return _ic_trans._getJson();
        }

        protected override string afterSendDataSuccess(DataRow row)
        {
            string __query = string.Format("INSERT INTO {0} (doc_no, branch_sync,trans_flag) VALUES(\'{1}\',\'{2}\',{3})", this._importedTableName, row["code"].ToString(), row["branch_sync"].ToString(), row["trans_flag"].ToString());
            return __query; 
        }
    }
}