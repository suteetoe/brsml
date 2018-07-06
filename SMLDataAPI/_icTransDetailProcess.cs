using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLDataAPI
{
    class _icTransDetailProcess : _processBase
    {
        public _icTransDetailProcess()
        {
            this.tableName = "ic_trans_detail";
        }
        protected override string _createTableImportCompleteScipt()
        {
            StringBuilder __query = new StringBuilder();
            __query.Append("CREATE TABLE " + this._importedTableName + "");
            __query.Append("(");
            __query.Append("doc_no character varying(25) COLLATE pg_catalog.\"default\" NOT NULL DEFAULT '',");
            __query.Append("trans_flag smallint NOT NULL DEFAULT 0,");
            __query.Append("doc_date date NOT NULL ,");
            __query.Append("item_code character varying(25) COLLATE pg_catalog.\"default\" NOT NULL DEFAULT '',");
            __query.Append("shelf_code character varying(10) COLLATE pg_catalog.\"default\" NOT NULL DEFAULT '',");
            __query.Append("wh_code character varying(10) COLLATE pg_catalog.\"default\" NOT NULL DEFAULT '',");
            __query.Append("line_number integer DEFAULT 0,");
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
        "cust_code",
        "inquiry_type",
        "item_code",
        "item_name",
        "unit_code",
        "qty",
        "price",
        "discount",
        "sum_of_cost",
        "sum_amount",
        "due_date",
        "remark",
        "status",
        "line_number",
        "ref_doc_no",
        "ref_doc_date",
        "ref_line_number",
        "ref_cust_code",
        "branch_code",
        "wh_code",
        "shelf_code",
        "wh_code_2",
        "shelf_code_2",
        "department_code",
        "total_vat_value",
        "cancel_qty",
        "total_qty",
        "stand_value",
        "divide_value",
        "ratio",
        "ic_pattern",
        "ic_color",
        "ic_size",
        "priority_level",
        "calc_flag",
        "last_status",
        "set_ref_line",
        "set_ref_price",
        "set_ref_qty",
        "item_type",
        "vat_type",
        "doc_ref_type",
        "item_code_main",
        "ref_row",
        "ref_guid",
        "doc_time",
        "is_permium",
        "is_get_price",
        "average_cost",
        "sum_amount_exclude_vat",
        "doc_date_calc",
        "doc_time_calc",
        "discount_amount",
        "price_exclude_vat",
        "user_approve",
        "price_type",
        "price_mode",
        "temp_float_1",
        "temp_float_2",
        "temp_string_1",
        "is_serial_number",
        "bank_name",
        "bank_branch",
        "chq_number",
        "barcode",
        "discount_number",
        "price_changed",
        "discount_changed",
        "price_default",
        "tax_type",
        "is_pos",
        "auto_create",
        "date_expire",
        "hidden_cost_1",
        "hidden_cost_1_exclude_vat",
        "hidden_cost_2",
        "hidden_cost_2_exclude_vat",
        "sum_of_cost_1",
        "average_cost_1",
        "sale_code",
        "sale_group",
        "date_due",
        "lot_number_1",
        "item_code_2",
        "bank_name_2",
        "bank_branch_2",
        "sale_shift_id",
        "price_base",
        "creator_code",
        "last_editor_code",
        "create_datetime",
        "lastedit_datetime",
        "fee_amount",
        "transfer_amount",
        "price_set_ratio",
        "mfd_date",
        "mfn_name",
        "price_2",
        "sum_amount_2",
        "price_guid",
        "discount_amount_2",
        "is_lock_cost",
        "sum_of_cost_fix",
        "create_date_time_now",
        "profit_lost_cost_amount",
        "is_doc_copy",
        "branch_sync"
                ));

            __query.Append(" from {0} ");
            __query.Append(" where not exists(select doc_no from {1} where {0}.doc_no = {1}.doc_no and {0}.branch_sync={1}.branch_sync and {0}.trans_flag={1}.trans_flag and {0}.doc_date={1}.doc_date and {0}.item_code={1}.item_code and {0}.wh_code={1}.wh_code and {0}.shelf_code={1}.shelf_code and {0}.line_number={1}.line_number ) limit 100 ");

            return string.Format(__query.ToString(), this.tableName, this._importedTableName);
        }

        protected override string _processData(DataRow row)
        {
            ic_trans_detail _ic_trans_detail = new ic_trans_detail(row);
            return _ic_trans_detail._getJson();
        }

        protected override string afterSendDataSuccess(DataRow row)
        {
            string __query = string.Format("INSERT INTO {0} (doc_no, branch_sync,trans_flag,doc_date,item_code,wh_code,shelf_code,line_number) VALUES(\'{1}\',\'{2}\',{3},{4},\'{5}\',\'{6}\',\'{7}\',{8})", this._importedTableName, row["doc_no"].ToString(), row["branch_sync"].ToString(), row["trans_flag"].ToString(), row["doc_date"].ToString(), row["item_code"].ToString(), row["wh_code"].ToString(), row["shelf_code"].ToString(), row["line_number"].ToString());
            return __query;
        }
    }
}