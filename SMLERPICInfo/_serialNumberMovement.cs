using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SMLERPICInfo
{
    public class _serialNumberMovement : MyLib._myGrid
    {
        public _serialNumberMovement()
        {
            this._isEdit = false;
            this._table_name = _g.d.ic_trans_serial_number._table;
            this._addColumn(_g.d.ic_trans_serial_number._doc_date, 4, 10, 10);
            this._addColumn(_g.d.ic_trans_serial_number._doc_time, 1, 5, 5);
            this._addColumn(_g.d.ic_trans_serial_number._doc_no, 1, 10, 10);
            this._addColumn(_g.d.ic_trans_serial_number._cust_name, 1, 20, 20);
            this._addColumn(_g.d.ic_trans_serial_number._trans_flag, 1, 10, 10);
            this._addColumn(_g.d.ic_trans_serial_number._wh_code, 1, 5, 5);
            this._addColumn(_g.d.ic_trans_serial_number._shelf_code, 1, 5, 5);
            this._addColumn(_g.d.ic_trans_serial_number._price, 3, 1, 10, true, false, true, false, _g.g._getFormatNumberStr(2));
            this._addColumn(_g.d.ic_trans_serial_number._description, 1, 20, 20);
            this._calcPersentWidthToScatter();
        }

        public void _load(string serialNumber, string itemCode)
        {
            this._clear();
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __custName = "case when trans_flag in (12,14,16) then coalesce((select name_1 from ap_supplier where ap_supplier.code=ic_trans_serial_number.cust_code),\'\') else coalesce((select name_1 from ar_customer where ar_customer.code=ic_trans_serial_number.cust_code),\'\') end";
            string __query = "select *," + __custName +" as cust_name from " + _g.d.ic_trans_serial_number._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans_serial_number._serial_number) + "=\'" + serialNumber.ToUpper() + "\' and " + _g.d.ic_trans_serial_number._ic_code + "=\'" + itemCode + "\' " +" order by doc_date,doc_time";
            DataTable __data = __myFrameWork._queryShort(__query).Tables[0];
            for (int __row = 0; __row < __data.Rows.Count; __row++)
            {
                DataRow __dataRow = __data.Rows[__row];
                int __addr = this._addRow();
                this._cellUpdate(__addr, _g.d.ic_trans_serial_number._doc_date, MyLib._myGlobal._convertDateFromQuery(__dataRow[_g.d.ic_trans_serial_number._doc_date].ToString()), false);
                this._cellUpdate(__addr, _g.d.ic_trans_serial_number._doc_time, __dataRow[_g.d.ic_trans_serial_number._doc_time].ToString(), false);
                this._cellUpdate(__addr, _g.d.ic_trans_serial_number._doc_no, __dataRow[_g.d.ic_trans_serial_number._doc_no].ToString(), false);
                this._cellUpdate(__addr, _g.d.ic_trans_serial_number._cust_name, __dataRow[_g.d.ic_trans_serial_number._cust_name].ToString(), false);
                this._cellUpdate(__addr, _g.d.ic_trans_serial_number._trans_flag, _g.g._transFlagGlobal._transName((int)MyLib._myGlobal._decimalPhase(__dataRow[_g.d.ic_trans_serial_number._trans_flag].ToString())), false);
                this._cellUpdate(__addr, _g.d.ic_trans_serial_number._wh_code, __dataRow[_g.d.ic_trans_serial_number._wh_code].ToString(), false);
                this._cellUpdate(__addr, _g.d.ic_trans_serial_number._shelf_code, __dataRow[_g.d.ic_trans_serial_number._shelf_code].ToString(), false);
                this._cellUpdate(__addr, _g.d.ic_trans_serial_number._price, MyLib._myGlobal._decimalPhase(__dataRow[_g.d.ic_trans_serial_number._price].ToString()), false);
                this._cellUpdate(__addr, _g.d.ic_trans_serial_number._description, __dataRow[_g.d.ic_trans_serial_number._description].ToString(), false);
            }
            this.Invalidate();
        }
    }
}
