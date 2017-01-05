using System;
using System.Collections.Generic;
using System.Text;

namespace SMLTOBC
{
    public class _champGuid
    {
        public int _rowOrder;
        public _tableCodeEnum _tableCode;
        public string _refNo;
        public int _actionCode;
        public _champGuid(string refNo, int actionCode, int roworder, _tableCodeEnum tableCode)
        {
            this._refNo = refNo;
            this._actionCode = actionCode;
            this._rowOrder = roworder;
            this._tableCode = tableCode;
        }
    }
    public enum _tableCodeEnum
    {
        Ap,
        Ar,
        Item,
        ArInvoice
    }
}
