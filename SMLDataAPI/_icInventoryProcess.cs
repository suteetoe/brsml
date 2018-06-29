using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLDataAPI
{
    public class _icInventoryProcess : _processBase
    {
        public _icInventoryProcess()
        {
            this.tableName = "ic_inventory";
        }

        protected override string _createTableImportCompleteScipt()
        {
            StringBuilder __query = new StringBuilder();
            __query.Append("CREATE TABLE " + this._importedTableName + "");
            __query.Append("(");
            __query.Append("code character varying(25),");
            __query.Append("branch_sync character varying(100),");
            __query.Append("CONSTRAINT " + this._importedTableName + "_pk_code PRIMARY KEY (code, branch_sync) ");
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
        "code",
        "code_old",
        "name_1",
        "name_2",
        "name_eng_1",
        "name_eng_2",
        "name_market",
        "name_for_bill",
        "short_name",
        "name_for_pos",
        "name_for_search",
        "item_type",
        "item_category",
        "group_main",
        "item_brand",
        "item_pattern",
        "item_design",
        "item_grade",
        "item_class",
        "item_size",
        "item_color",
        "item_character",
        "item_status",
        "unit_type",
        "cost_type",
        "tax_type",
        "item_sale_type",
        "item_rent_type",
        "unit_standard",
        "unit_cost",
        "income_type",
        "description",
        "item_model",
        "ic_serial_no",
        "remark",
        "status",
        "guid_code",
        "last_movement_date",
        "average_cost",
        "item_in_stock",
        "balance_qty",
        "accrued_in_qty",
        "accrued_out_qty",
        "unit_standard_name",
        "update_price",
        "update_detail",
        "account_code_1",
        "account_code_2",
        "account_code_3",
        "account_code_4",
        "book_out_qty",
        "doc_format_code",
        "unit_standard_stand_value",
        "unit_standard_divide_value",
        "sign_code",
        "supplier_code",
        "fixed_cost",
        "drink_type",
        "average_cost_1",
        "group_sub",
        "use_expire",
        "barcode_checker_print",
        "print_order_per_unit",
        "production_period",
        "is_new_item",
        "commission_rate",
        "is_eordershow",
        "no_discount",
        "serial_no_format",
        "pos_no_sum",
        "item_promote",
        "sum_sale_1",
        "is_speech",
        "medicine_register_number",
        "medicine_standard_code",
        "quantity",
        "degree",
        "is_product_boonrawd",
        "create_date_time_now",
        "tpu_code",
        "gpu_code",
        "branch_sync"
                ));

            __query.Append(" from {0} ");
            __query.Append(" where not exists(select code from {1} where {0}.code = {1}.code and {0}.branch_sync={1}.branch_sync ) limit 100 ");

            return string.Format(__query.ToString(), this.tableName, this._importedTableName);
        }

        protected override string _processData(DataRow row)
        {
            ic_inventory _ic_inventory = new ic_inventory(row);
            return _ic_inventory._getJson();
        }

        protected override string afterSendDataSuccess(DataRow row)
        {
            string __query = string.Format("INSERT INTO {0} (code, branch_sync) VALUES(\'{1}\',\'{2}\')", this._importedTableName, row["code"].ToString(), row["branch_sync"].ToString());
            return __query;
        }
    }
}

