using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLDataAPI
{
    public class _arCustomerProcess : _processBase
    {
        public _arCustomerProcess()
        {
            this.tableName = "ar_customer";
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
"address",
"address_eng",
"tambon",
"amper",
"province",
"zip_code",
"telephone",
"fax",
"email",
"website",
"description",
"birth_day",
"ar_type",
"remark",
"status",
"guid_code",
"ar_status",
"doc_format_code",
"prefixname",
"first_name",
"last_name",
"price_level",
"point_balance",
"sale_shift_id",
"sms_phonenumber",
"home_address",
"home_name",
"moo",
"soi",
"road",
"room_no",
"floor",
"building",
"sex",
"country",
"register_date",
"arm_code",
"ar_code_main",
"ar_branch_code",
"arm_approve",
"arm_approve_date",
"nfc_id",
"arm_tier",
"create_date_time_now",
"last_update_time",
"guid",
"interco",
"branch_sync"
                ));

            __query.Append(" from {0} ");
            __query.Append(" where not exists(select code from {1} where {0}.code = {1}.code and {0}.branch_sync={1}.branch_sync ) limit 100 ");

            return string.Format(__query.ToString(), this.tableName, this._importedTableName);
        }

        protected override string _processData(DataRow row)
        {
            
            /* pack json */
            return "";
        }

        protected override string afterSendDataSuccess(DataRow row)
        {
            string __query = string.Format("INSERT INTO {0} (code, branch_sync) VALUES(\'{1}\',\'{2}\')", this._importedTableName, row["code"].ToString(), row["branch_sync"].ToString());
            return __query;
        }
    }
}
