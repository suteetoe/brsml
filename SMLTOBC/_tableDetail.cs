using System;
using System.Collections.Generic;
using System.Text;

namespace SMLTOBC
{
    public class _tableDetail
    {
        public int _tableCode;
        public string _tableName;
        public List<string> _fieldCompare;
        public List<string> _fieldChampGuid;

        public _tableDetail(int tableCode,string tableName,string fieldCompare,string fieldChampGuid)
        {
            this._tableCode = tableCode;
            this._tableName = tableName;
            this._fieldCompare = new List<string>(fieldCompare.Split(','));
            this._fieldChampGuid = new List<string>(fieldChampGuid.Split(','));
        }
    }
}
