using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLDataAPI
{
    public class _icInventoryDetailProcess : _processBase
    {
        public _icInventoryDetailProcess()
        {
            this.tableName = "ic_inventory_detail";
        }

        protected override string _createTableImportCompleteScipt()
        {
            StringBuilder __query = new StringBuilder();
            __query.Append("CREATE TABLE " + this._importedTableName + "");
            __query.Append("(");
            __query.Append("ic_code character varying(25),");
            __query.Append("branch_sync character varying(100),");
            __query.Append("CONSTRAINT " + this._importedTableName + "_pk_code PRIMARY KEY (ic_code, branch_sync) ");
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
        "ic_code",
        "formular",
        "po_over",
        "so_over",
        "account_group",
        "serial_number",
        "tax_import",
        "tax_rate",
        "purchase_manager",
        "sale_manager",
        "start_purchase_wh",
        "start_purchase_shelf",
        "start_purchase_unit",
        "start_sale_wh",
        "start_sale_shelf",
        "start_sale_unit",
        "cost_produce",
        "cost_standard",
        "unit_for_stock",
        "ic_out_wh",
        "ic_out_shelf",
        "ic_reserve_wh",
        "reserve_status",
        "discount",
        "purchase_point",
        "unit_2_code",
        "unit_2_qty",
        "unit_2_average",
        "unit_2_average_value",
        "user_group_for_purchase",
        "user_group_for_sale",
        "user_group_for_manage",
        "user_group_for_warehouse",
        "user_status",
        "close_reason",
        "close_date",
        "ref_file_1",
        "ref_file_2",
        "ref_file_3",
        "ref_file_4",
        "ref_file_5",
        "dimension_1",
        "dimension_2",
        "dimension_3",
        "dimension_4",
        "dimension_5",
        "dimension_6",
        "dimension_7",
        "dimension_8",
        "dimension_9",
        "dimension_10",
        "sale_price_1",
        "sale_price_2",
        "sale_price_3",
        "sale_price_4",
        "maximum_qty",
        "minimum_qty",
        "dimension_11",
        "dimension_12",
        "dimension_13",
        "dimension_14",
        "dimension_15",
        "dimension_16",
        "dimension_17",
        "dimension_18",
        "dimension_19",
        "dimension_20",
        "accrued_control",
        "lock_price",
        "lock_discount",
        "lock_cost",
        "is_end",
        "is_hold_purchase",
        "is_hold_sale",
        "is_stop",
        "balance_control",
        "have_point",
        "start_unit_code",
        "dimension_21",
        "dimension_22",
        "dimension_23",
        "dimension_24",
        "dimension_25",
        "is_premium",
        "create_date_time_now",
        "dimension_26",
        "dimension_27",
        "dimension_28",
        "dimension_29",
        "dimension_30",
        "dimension_31",
        "dimension_32",
        "dimension_33",
        "dimension_34",
        "dimension_35",
        "dimension_36",
        "dimension_37",
        "dimension_38",
        "dimension_39",
        "dimension_40",
        "dimension_41",
        "dimension_42",
        "dimension_43",
        "dimension_44",
        "dimension_45",
        "branch_sync"
                ));

            __query.Append(" from {0} ");
            __query.Append(" where not exists(select code from {1} where {0}.ic_code = {1}.ic_code and {0}.branch_sync={1}.branch_sync ) limit 100 ");

            return string.Format(__query.ToString(), this.tableName, this._importedTableName);
        }

        protected override string _processData(DataRow row)
        {
            ic_inventory_detail _ic_inventory_detail = new ic_inventory_detail(row);
            return _ic_inventory_detail._getJson();
        }

        protected override string afterSendDataSuccess(DataRow row)
        {
            string __query = string.Format("INSERT INTO {0} (ic_code, branch_sync) VALUES(\'{1}\',\'{2}\')", this._importedTableName, row["ic_code"].ToString(), row["branch_sync"].ToString());
            return __query;
        }
    }
}
