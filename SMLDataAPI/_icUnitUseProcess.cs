using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLDataAPI
{
    public class _icUnitUseProcess : _processBase
    {

        public _icUnitUseProcess()
        {
            this.tableName = "ic_unit_use";
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
        "line_number",
        "stand_value",
        "divide_value",
        "ratio",
        "row_order",
        "width_length_height",
        "ic_code",
        "remark",
        "weight",
        "status",
        "create_date_time_now",
        "branch_sync"
                ));

            __query.Append(" from {0} ");
            __query.Append(" where not exists(select code from {1} where {0}.code = {1}.code and {0}.branch_sync={1}.branch_sync ) limit 100 ");

            return string.Format(__query.ToString(), this.tableName, this._importedTableName);
        }

        protected override string _processData(DataRow row)
        {
            ic_unit_use _ic_unit_use = new ic_unit_use(row);
            return _ic_unit_use._getJson();
        }

        protected override string afterSendDataSuccess(DataRow row)
        {
            string __query = string.Format("INSERT INTO {0} (code, branch_sync) VALUES(\'{1}\',\'{2}\')", this._importedTableName, row["code"].ToString(), row["branch_sync"].ToString());
            return __query;
        }
    }
}

