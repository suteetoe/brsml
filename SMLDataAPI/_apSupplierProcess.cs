using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLDataAPI
{
    public class _apSupplierProcess : _processBase
    {
        public _apSupplierProcess() {
            this.tableName = "ap_supplier";
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
            "ignore_sync ",
            "is_lock_record",
            "roworder",
            "code",
            "code_old",
            "name_1",
            "name_2",
            "name_eng_1",
            "name_eng_2",
            "prefixname",
            "firstname",
            "lastname",
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
            "ap_type",
            "remark",
            "status",
            "guid_code",
            "debt_balance",
            "chq_balance",
            "bill_balance",
            "ap_status",
            "doc_format_code",
            "country",
            "create_date_time_now",
            "interco"
                ));

            __query.Append(" from {0} ");
            __query.Append(" where not exists(select code from {1} where {0}.code = {1}.code and {0}.branch_sync={1}.branch_sync ) limit 100 ");

            return string.Format(__query.ToString(), this.tableName, this._importedTableName);
        }

        protected override string _processData(DataRow row)
        {
            ap_supplier _ap_supplier = new ap_supplier(row);
            return _ap_supplier._getJson();
        }

        protected override string afterSendDataSuccess(DataRow row)
        {
            string __query = string.Format("INSERT INTO {0} (code, branch_sync) VALUES(\'{1}\',\'{2}\')", this._importedTableName, row["code"].ToString(), row["branch_sync"].ToString());
            return __query;
        }
    }
}

