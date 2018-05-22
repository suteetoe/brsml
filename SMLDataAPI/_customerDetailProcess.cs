using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLDataAPI
{
    public class _customerDetailProcess : _processBase
    {
        public _customerDetailProcess()
        {
            this.tableName = "ar_customer_detail";
        }
        protected override string _createTableImportCompleteScipt()
        {
            StringBuilder __query = new StringBuilder();
            __query.Append("CREATE TABLE " + this._importedTableName + "");
            __query.Append("(");
            __query.Append("ar_code character varying(25),");
            __query.Append("branch_sync character varying(100),");
            __query.Append("CONSTRAINT " + this._importedTableName + "_pk_code PRIMARY KEY (ar_code, branch_sync) ");
            __query.Append(") ");
            return __query.ToString();
        }

        protected override string afterSendDataSuccess(DataRow row)
        {
            string __query = string.Format("INSERT INTO {0} (ar_code, branch_sync) VALUES(\'{1}\',\'{2}\')", this._importedTableName, row["ar_code"].ToString(), row["branch_sync"].ToString());
            return __query;
        }

        protected override string getDataQuery()
        {
            StringBuilder __query = new StringBuilder();
            __query.Append(" select ");

            __query.Append(MyLib._myGlobal._fieldAndComma("ignore_sync",
"is_lock_record",
"roworder",
"ar_code",
"area_code",
"sale_code",
"logistic_area",
"pay_bill_code",
"keep_chq_code",
"pay_bill_date",
"keep_chq_date",
"sale_price_level",
"credit_sale",
"credit_code",
"form_name",
"pay_bill_condition",
"keep_money_condition",
"payment_person",
"credit_person",
"keep_money_person",
"discount_item",
"discount_bill",
"credit_group",
"credit_group_code",
"credit_money",
"credit_money_max",
"credit_day",
"credit_reason",
"credit_status",
"credit_date",
"credit_status_reason",
"trade_license",
"vat_license",
"tax_id",
"tax_type",
"tax_rate",
"account_code",
"shipping_type",
"close_reason",
"close_date",
"group_main",
"group_sub_1",
"group_sub_2",
"group_sub_3",
"group_sub_4",
"picture_3",
"picture_4",
"ref_doc_1",
"ref_doc_2",
"ref_doc_3",
"dimension_1",
"dimension_2",
"dimension_3",
"dimension_4",
"dimension_5",
"currency_code",
"latitude",
"longitude",
"card_id",
"passbook_code",
"set_tax_type",
"branch_type",
"branch_code",
"area_paybill",
"reason_disable_credit",
"close_credit_date",
"close_reason_1",
"close_reason_2",
"close_reason_3",
"close_reason_4",
"dimension_6",
"dimension_7",
"dimension_8",
"dimension_9",
"dimension_10",
"disable_auto_close_credit",
"create_date_time_now",
"ap_code_ref",
"sale_type",
"guid",
"line_id",
"facebook",
"customer_type_code",
"ar_channel_code",
"ar_location_type_code",
"br_cust_code",
"ar_sub_type_1_code",
"ar_vehicle_code",
"ar_equipment_code",
"ar_sub_equipment",
"branch_sync"
                ));

            __query.Append(" from {0} ");
            __query.Append(" where not exists(select ar_code from {1} where {0}.ar_code = {1}.ar_code and {0}.branch_sync={1}.branch_sync ) limit 100 ");

            return string.Format(__query.ToString(), this.tableName, this._importedTableName);
        }

        protected override string _processData(DataRow row)
        {

            /*
            "{
 ""code"" : """",
 ""name_1"" : """",
 ""tambon"" : """",
 ""amper"" : """",
 ""province "": """",
 ""ar_type"" : """",
 ""status"" : """",
 ""ar_status"" : """",
 ""price_level"" : """",
 ""point_balance"" : """",
 ""create_date_time_now"" : """",
  < optional fields > : """",
  < optional fields > : ""
}"

             */
            // pack json query

            return "";
        }
    }
}
