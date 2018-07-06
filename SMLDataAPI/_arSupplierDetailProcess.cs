using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLDataAPI
{
    public class _arSupplierDetailProcess : _processBase
    {
        public _arSupplierDetailProcess()
        {
            this.tableName = "ap_supplier_detail";
        }
        protected override string _createTableImportCompleteScipt()
        {
            StringBuilder __query = new StringBuilder();
            __query.Append("CREATE TABLE " + this._importedTableName + "");
            __query.Append("(");
            __query.Append("ap_code character varying(25),");
            __query.Append("branch_sync character varying(100),");
            __query.Append("CONSTRAINT " + this._importedTableName + "_pk_code PRIMARY KEY (ap_code, branch_sync) ");
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
            "ap_code",
            "staff_pay_code",
            "payment_way",
            "pay_bill_way",
            "pay_condition",
            "credit_purchase",
            "credit_code",
            "form_name",
            "discount_item",
            "discount_bill",
            "credit_day",
            "trade_license",
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
            "picture_1",
            "picture_2",
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
            "card_id",
            "passbook_code",
            "set_tax_type",
            "branch_type",
            "branch_code",
            "dimension_6",
            "dimension_7",
            "dimension_8",
            "dimension_9",
            "dimension_10",
            "create_date_time_now",
            "lead_time",
            "branch_sync"
                ));

            __query.Append(" from {0} ");
            __query.Append(" where not exists(select ap_code from {1} where {0}.ap_code = {1}.ap_code and {0}.branch_sync={1}.branch_sync ) limit 100 ");

            return string.Format(__query.ToString(), this.tableName, this._importedTableName);
        }

        protected override string _processData(DataRow row)
        {
            ap_supplier_detail _ap_supplier_detail = new ap_supplier_detail(row);
            return _ap_supplier_detail._getJson();
        }

        protected override string afterSendDataSuccess(DataRow row)
        {
            string __query = string.Format("INSERT INTO {0} (ap_code, branch_sync) VALUES(\'{1}\',\'{2}\')", this._importedTableName, row["ap_code"].ToString(), row["branch_sync"].ToString());
            return __query;
        }
    }
}