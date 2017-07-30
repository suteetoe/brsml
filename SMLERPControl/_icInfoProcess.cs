using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;

namespace SMLERPControl
{
    public class _icInfoProcess
    {
        private string _wareHouseField = "warehouse";
        private string _locationField = "location";
        private string _unitRatio = "unit_ratio";
        private string _unitStandardRatio = "unit_standard_ratio";

        public string _stkStockNoMovementQuery(MyLib._myGrid itemGrid, string itemBegin, string itemEnd, int dayFrom, int dayTo, DateTime dateEnd)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __itemWhere = _g.g._getItemCode(_g.d.ic_resource._ic_code, itemGrid, itemBegin, itemEnd);
            string __dateWhereEnd = MyLib._myGlobal._convertDateToQuery(dateEnd);
            string __docDateFieldName = _g.d.ic_trans_detail._doc_date_calc;
            //
            string __transDetailBalanceWhere = _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._last_status + "=0 and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " and " + __docDateFieldName + "<=\'" + __dateWhereEnd + "\'";
            string __unitStandard = "(select " + _g.d.ic_unit_use._stand_value + "/" + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table +
                " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code +
                " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._unit_standard + ")";
            string __queryGetAverageCost = "(select " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + _g.d.ic_trans_detail._average_cost + " from " + _g.d.ic_trans_detail._table +
                " where " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._last_status + "=0 and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "=" + "temp1." + _g.d.ic_resource._ic_code +
                " and " + __docDateFieldName + "<=\'" + __dateWhereEnd + "\' and " + _g._icInfoFlag._allFlagQty + " order by " + __docDateFieldName + " desc," + _g.d.ic_trans_detail._doc_time + " desc ," + _g.d.ic_trans_detail._line_number + " desc " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + ")";
            string __queryLastFormat = "(select " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + " {0} from " + _g.d.ic_trans_detail._table +
                " where " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._last_status + "=0 and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "=" + "temp1." + _g.d.ic_resource._ic_code +
                " and " + __docDateFieldName + "<=\'" + __dateWhereEnd + "\' and {1} order by " + __docDateFieldName + " desc," + _g.d.ic_trans_detail._doc_time + " desc ," + _g.d.ic_trans_detail._line_number + " desc  " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + ")";
            string __query = "select " + _g.d.ic_resource._ic_code + "," + _g.d.ic_resource._ic_name + "," + _g.d.ic_resource._ic_unit_code + "," + _g.d.ic_resource._average_cost_end + "," +
                _g.d.ic_resource._balance_qty + "," + _g.d.ic_resource._balance_amount + "," + "case when " + _g.d.ic_resource._balance_qty + "=0 then 0 else " + _g.d.ic_resource._balance_amount + "/" + _g.d.ic_resource._balance_qty + " end as " + _g.d.ic_resource._average_cost + "," +
                _g.d.ic_resource._last_in_flag + "," + _g.d.ic_resource._last_in_date + "," + _g.d.ic_resource._last_out_flag + "," + _g.d.ic_resource._last_out_date + "," + _g.d.ic_resource._last_sale_date + "," + _g.d.ic_resource._no_movement_date +
                " from " +
                "(select " + _g.d.ic_resource._ic_code + "," + _g.d.ic_resource._ic_name + "," + _g.d.ic_resource._ic_unit_code + "," + _g.d.ic_resource._average_cost_end + "," +
                _g.d.ic_resource._balance_qty + "," + _g.d.ic_resource._balance_amount + "," + "case when " + _g.d.ic_resource._balance_qty + "=0 then 0 else " + _g.d.ic_resource._balance_amount + "/" + _g.d.ic_resource._balance_qty + " end as " + _g.d.ic_resource._average_cost + "," +
                _g.d.ic_resource._last_in_flag + "," + _g.d.ic_resource._last_in_date + "," + _g.d.ic_resource._last_out_flag + "," + _g.d.ic_resource._last_out_date + "," + _g.d.ic_resource._last_sale_date + "," +
                "coalesce(\'" + __dateWhereEnd + "\'-" + _g.d.ic_resource._no_movement_date + ",0) as " + _g.d.ic_resource._no_movement_date +
                " from " +
                "(select " + _g.d.ic_resource._ic_code + "," + _g.d.ic_resource._ic_name + "," + _g.d.ic_resource._ic_unit_code + "," + __queryGetAverageCost + "*" + _unitRatio + " as " + _g.d.ic_resource._average_cost_end + "," +
                // ยอดเข้าล่าสุด
                String.Format(__queryLastFormat, _g.d.ic_trans_detail._trans_flag, _g._icInfoFlag._inFlag) + " as " + _g.d.ic_resource._last_in_flag + "," +
                String.Format(__queryLastFormat, __docDateFieldName, _g._icInfoFlag._inFlag) + " as " + _g.d.ic_resource._last_in_date + "," +
                // ออกล่าสุด
                String.Format(__queryLastFormat, _g.d.ic_trans_detail._trans_flag, _g._icInfoFlag._outFlag) + " as " + _g.d.ic_resource._last_out_flag + "," +
                String.Format(__queryLastFormat, __docDateFieldName, _g._icInfoFlag._outFlag) + " as " + _g.d.ic_resource._last_out_date + "," +
                // ขายล่าสุด
                String.Format(__queryLastFormat, __docDateFieldName, _g._icInfoFlag._saleFlag) + " as " + _g.d.ic_resource._last_sale_date + "," +
                // วันที่เคลื่อนไหวล่าสุด
                String.Format(__queryLastFormat, __docDateFieldName, _g._icInfoFlag._allFlagQty) + " as " + _g.d.ic_resource._no_movement_date + "," +
                //
                _g.d.ic_resource._balance_qty + "/" + _unitRatio + " as " + _g.d.ic_resource._balance_qty + "," + _g.d.ic_resource._balance_amount +
                " from " +
                "(select " + _g.d.ic_inventory._code + " as " + _g.d.ic_resource._ic_code + "," + _g.d.ic_inventory._name_1 + " as " + _g.d.ic_resource._ic_name + "," + _g.d.ic_inventory._average_cost + " as " + _g.d.ic_resource._average_cost_end + "," +
                // หน่วยนับ
                _g.d.ic_inventory._unit_standard + "||'~'||(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._unit_standard + ") as " + _g.d.ic_resource._ic_unit_code + "," +
                __unitStandard + " as " + _unitRatio + "," +
                // ยอดคงเหลือ
                "(select sum(" + _g.d.ic_trans_detail._calc_flag + "*((case when " + _g.d.ic_trans_detail._trans_flag + " in (14,16) and " + _g.d.ic_trans_detail._inquiry_type + "=1 then 0 else " + _g.d.ic_trans_detail._qty + " end)*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))) from " + __transDetailBalanceWhere + " and " + _g._icInfoFlag._allFlagQty + ") as " + _g.d.ic_resource._balance_qty + "," +
                "(select sum(" + _g.d.ic_trans_detail._calc_flag + "*(" + _g.d.ic_trans_detail._sum_of_cost + ")) from " + __transDetailBalanceWhere + " and " + _g._icInfoFlag._allFlagQty + ") as " + _g.d.ic_resource._balance_amount +
                " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._item_type + "<>5) as temp1 " +
                ((__itemWhere.Length == 0) ? "" : " where " + __itemWhere) + ") as temp2) as temp3 where " + _g.d.ic_resource._no_movement_date + " between " + dayFrom.ToString() + " and " + dayTo.ToString() + " order by " + _g.d.ic_resource._ic_code;
            return __query;
        }

        /// <summary>
        /// สินค้าไม่เคลื่อนไหว
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataTable _stkStockNoMovement(MyLib._myGrid itemGrid, string itemBegin, string itemEnd, int dayFrom, int dayTo, DateTime dateEnd)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __query = this._stkStockNoMovementQuery(itemGrid, itemBegin, itemEnd, dayFrom, dayTo, dateEnd);
            return __myFrameWork._queryShort(__query).Tables[0];
        }

        public string _stkStockInfoAndBalanceQuery(_g.g._productCostType costMode, MyLib._myGrid itemGrid, string itemBegin, string itemEnd, DateTime dateBegin, DateTime dateEnd, Boolean getBalanceOnly, _stockBalanceType balanceType, string wareHouseList, string locationList, bool haveStock, bool haveBarcode, string itemNameFilter)
        {
            return _stkStockInfoAndBalanceQuery(costMode, itemGrid, itemBegin, itemEnd, dateBegin, dateEnd, getBalanceOnly, balanceType, wareHouseList, locationList, haveStock, haveBarcode, itemNameFilter, false);
        }

        public string _stkStockInfoAndBalanceQuery(_g.g._productCostType costMode, MyLib._myGrid itemGrid, string itemBegin, string itemEnd, DateTime dateBegin, DateTime dateEnd, Boolean getBalanceOnly, _stockBalanceType balanceType, string wareHouseList, string locationList, bool haveStock, bool haveBarcode, string itemNameFilter, bool _serialOnly)
        {
            string __itemWhere = _g.g._getItemCode(_g.d.ic_resource._ic_code, itemGrid, itemBegin, itemEnd);
            string __dateWhereBegin = MyLib._myGlobal._convertDateToQuery(dateBegin);
            string __dateWhereEnd = MyLib._myGlobal._convertDateToQuery(dateEnd);
            string __docDateFieldName = _g.d.ic_trans_detail._doc_date_calc;
            string __query = "";
            string __groupBy = "";
            string __fieldList0 = "";
            string __fieldList1 = "";
            string __fieldList2 = "";
            string __fieldSumOfCost = (costMode == _g.g._productCostType.ปรกติ) ? "(" + _g.d.ic_trans_detail._sum_of_cost + "+profit_lost_cost_amount)" : "(" + _g.d.ic_trans_detail._sum_of_cost_1 + "+profit_lost_cost_amount)";
            string __wareHouseAndLocationQuery = ((wareHouseList.Length == 0) ? "" : " and " + (_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_code + " in (" + wareHouseList + ")")) + ((locationList.Length == 0) ? "" : " and " + (_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._shelf_code + " in (" + locationList + ")"));

            string __serialQuery = (_serialOnly) ? " and " + _g.d.ic_trans_detail._is_serial_number + "=1 " : "";
            switch (balanceType)
            {
                case _stockBalanceType.ยอดคงเหลือตามที่เก็บ:
                    {
                        __groupBy = MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._wh_code, _g.d.ic_trans_detail._shelf_code);
                        __fieldList0 = _g.d.ic_resource._ic_code + "," + _wareHouseField + "," + _locationField;
                        __fieldList1 = _g.d.ic_trans_detail._item_code + " as " + _g.d.ic_resource._ic_code + "," + _g.d.ic_trans_detail._wh_code + " as " + _wareHouseField + "," + _g.d.ic_trans_detail._shelf_code + " as " + _locationField;
                        __fieldList2 = _g.d.ic_resource._ic_code + "," + _wareHouseField + "," + _locationField;
                    }
                    break;
                case _stockBalanceType.ยอดคงเหลือตามคลัง:
                    {
                        __groupBy = MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._wh_code);
                        __fieldList0 = _g.d.ic_resource._ic_code + "," + _wareHouseField;
                        __fieldList1 = _g.d.ic_trans_detail._item_code + " as " + _g.d.ic_resource._ic_code + "," + _g.d.ic_trans_detail._wh_code + " as " + _wareHouseField;
                        __fieldList2 = _g.d.ic_resource._ic_code + "," + _wareHouseField;
                    }
                    break;
                case _stockBalanceType.ยอดคงเหลือตามสินค้า:
                    {
                        __groupBy = MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._item_code);
                        __fieldList0 = _g.d.ic_resource._ic_code;
                        __fieldList1 = _g.d.ic_trans_detail._item_code + " as " + _g.d.ic_resource._ic_code;
                        __fieldList2 = _g.d.ic_resource._ic_code;
                    }
                    break;
            }
            string __queryGetAverageCost = "(select " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + " " + _g.d.ic_trans_detail._average_cost + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._last_status + "=0 and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "=" + "temp2." + _g.d.ic_resource._ic_code + " and " + __docDateFieldName + "<=\'" + __dateWhereEnd + "\' and " + _g._icInfoFlag._allFlagAmount + " order by " + __docDateFieldName + " desc," + _g.d.ic_trans_detail._doc_time + " desc ," + _g.d.ic_trans_detail._line_number + " desc " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + ")";
            //
            string __queryIn = "";
            string __queryOut = "";
            if (getBalanceOnly == false)
            {
                // ยอดเข้า 
                __queryIn = "sum(case when " + __docDateFieldName + ">=\'" + __dateWhereBegin + "\' and " + _g._icInfoFlag._inFlag + " then " + _g.d.ic_trans_detail._calc_flag + "*(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + ")) else 0 end) as " + _g.d.ic_resource._qty_in + ",sum(case when " + __docDateFieldName + ">=\'" + __dateWhereBegin + "\' and " + _g._icInfoFlag._inFlagAmountOnly + " then " + _g.d.ic_trans_detail._calc_flag + "*" + __fieldSumOfCost + " else 0 end) as " + _g.d.ic_resource._amount_in;
                // ยอดออก
                __queryOut = "-1*sum(case when " + __docDateFieldName + ">=\'" + __dateWhereBegin + "\' and " + _g._icInfoFlag._outFlagAmountOnly + " then " + _g.d.ic_trans_detail._calc_flag + "*" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + ") else 0 end) as " + _g.d.ic_resource._qty_out + ",-1*sum(case when " + __docDateFieldName + ">=\'" + __dateWhereBegin + "\' and " + _g._icInfoFlag._outFlagAmountOnly + " then (case when " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก) + " and " + _g.d.ic_trans_detail._qty + " < 0 then -1 else " + _g.d.ic_trans_detail._calc_flag + " end) * " + __fieldSumOfCost + " else 0 end) as " + _g.d.ic_resource._amount_out;
            }
            else
            {
                __queryIn = "0 as " + _g.d.ic_resource._qty_in + ",0 as " + _g.d.ic_resource._amount_in;
                __queryOut = "0 as " + _g.d.ic_resource._qty_out + ",0 as " + _g.d.ic_resource._amount_out;
            }
            //
            string __itemNameQuery = _g.d.ic_inventory._name_1;
            if (haveBarcode)
            {
                __itemNameQuery = __itemNameQuery + MyLib._myGlobal._getSignPlusStringQuery() + "\'(\'" + MyLib._myGlobal._getSignPlusStringQuery() + "coalesce((select  " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + " " + _g.d.ic_inventory_barcode._barcode + " from " + _g.d.ic_inventory_barcode._table + " where " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " and " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._unit_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._unit_standard + " " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + " ),\'\')" + MyLib._myGlobal._getSignPlusStringQuery() + "\')\'";
            }
            //
            __query = "(select " + MyLib._myGlobal._fieldAndComma(__fieldList1, "(select " + __itemNameQuery + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_resource._ic_name, "(select " + _g.d.ic_inventory._unit_standard + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_resource._ic_unit_code, "coalesce(sum(" + _g.d.ic_trans_detail._calc_flag + "*(case when " + _g._icInfoFlag._allFlagQty + " then " + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + " / " + _g.d.ic_trans_detail._divide_value + ") else 0 end)),0) as " + _g.d.ic_resource._balance_qty
                , "coalesce(sum(" + _g.d.ic_trans_detail._calc_flag + "*(case when " + _g._icInfoFlag._allFlagAmount + " then case when " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก) + " and " + _g.d.ic_trans_detail._qty + " < 0 then -1* " + __fieldSumOfCost + " else " + __fieldSumOfCost + " end else 0 end)),0) as " + _g.d.ic_resource._balance_amount
                , __queryIn, __queryOut) + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._last_status + "=0 and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_type + "<>5 and (select " + _g.d.ic_inventory._item_type + " from ic_inventory where ic_inventory.code = ic_trans_detail.item_code) not in (1,3) and " + __docDateFieldName + "<=\'" + __dateWhereEnd + "\'" + __wareHouseAndLocationQuery + __serialQuery + " group by " + __groupBy + ") as temp1";
            //
            __query = "(select " + MyLib._myGlobal._fieldAndComma(__fieldList2, _g.d.ic_resource._ic_name, _g.d.ic_resource._balance_qty, _g.d.ic_resource._ic_unit_code, "(select " + _g.d.ic_inventory._unit_standard_stand_value + "/" + _g.d.ic_inventory._unit_standard_divide_value + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=temp1.ic_code) as " + _unitRatio, _g.d.ic_resource._balance_amount, _g.d.ic_resource._qty_in, _g.d.ic_resource._amount_in, _g.d.ic_resource._qty_out, _g.d.ic_resource._amount_out) + " from " + __query + ") as temp2 ";
            //
            __query = "select coalesce((select stand_value/divide_value from ic_unit_use where ic_unit_use.ic_code=temp2.ic_code and ic_unit_use.code=temp2.ic_unit_code),1) as " + _unitStandardRatio + "," + MyLib._myGlobal._fieldAndComma(__fieldList2, _g.d.ic_resource._ic_name, _g.d.ic_resource._balance_qty, _g.d.ic_resource._ic_unit_code, "case when " + _g.d.ic_resource._balance_qty + "=0 then 0 else " + _g.d.ic_resource._balance_amount + "/" + _g.d.ic_resource._balance_qty + " end as " + _g.d.ic_resource._average_cost, "coalesce((" + __queryGetAverageCost + "*" + _unitRatio + "),0) as " + _g.d.ic_resource._average_cost_end, "case when " + _g.d.ic_resource._balance_qty + "=0 then 0 else " + _g.d.ic_resource._balance_amount + " end as " + _g.d.ic_resource._balance_amount, _g.d.ic_resource._qty_in,
                // toe เอา case when ออก กรณีไม่มียอดคงเหลือแต่มียอดเข้า ต้องแสดงมูลค่าเข้าด้วย
                // " case when " + _g.d.ic_resource._balance_qty + " = 0 then 0 else " + _g.d.ic_resource._amount_in + " end as " +
                _g.d.ic_resource._amount_in, "case when " + _g.d.ic_resource._qty_in + "=0 then 0 else " + _g.d.ic_resource._amount_in + "/" + _g.d.ic_resource._qty_in + " end as " + _g.d.ic_resource._average_cost_in, _g.d.ic_resource._qty_out, _g.d.ic_resource._amount_out, "case when " + _g.d.ic_resource._qty_out + "=0 then 0 else " + _g.d.ic_resource._amount_out + "/" + _g.d.ic_resource._qty_out + " end as " + _g.d.ic_resource._average_cost_out) + " from " + __query;
            //
            string __extraWhere = "";
            if (itemNameFilter.Trim().Length != 0)
            {
                __extraWhere = " where (" + _g.d.ic_resource._ic_code + " like \'%" + itemNameFilter.Trim() + "%\' or " + _g.d.ic_resource._ic_name + " like \'%" + itemNameFilter.Trim() + "%\')";
            }
            if (__itemWhere.Length > 0)
            {
                __extraWhere = __extraWhere + ((__extraWhere.Length == 0) ? " where " : " and ") + __itemWhere + " ";
            }

            if (getBalanceOnly == false)
            {
                __extraWhere = __extraWhere + ((__extraWhere.Length == 0) ? " where " : " and ") + "(" + _g.d.ic_resource._qty_in + "<>0 or " + _g.d.ic_resource._amount_in + "<>0 or " + _g.d.ic_resource._qty_out + "<>0 or " + _g.d.ic_resource._amount_out + "<>0 or " + _g.d.ic_resource._balance_qty + "<>0 or " + _g.d.ic_resource._balance_amount + "<>0)";
            }
            else
            {
                // โต๋ เพิ่มกรณี ดึงรายารที่มียอด เป็น 0 มาด้วย
                if (haveStock)
                {
                    __extraWhere = __extraWhere + ((__extraWhere.Length == 0) ? " where " : " and ") + "(" + _g.d.ic_resource._balance_qty + "<>0 or " + _g.d.ic_resource._balance_amount + "<>0)";
                }
            }
            __query = "select " + MyLib._myGlobal._fieldAndComma(__fieldList0,
                _g.d.ic_resource._ic_name,
                _g.d.ic_resource._ic_unit_code,
                _g.d.ic_resource._balance_qty + "/" + _unitStandardRatio + " as " + _g.d.ic_resource._balance_qty,
                (((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 " : _g.d.ic_resource._average_cost + "*" + _unitStandardRatio) + " as " + _g.d.ic_resource._average_cost,
                (((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 " + " as " + _g.d.ic_resource._average_cost_end : _g.d.ic_resource._average_cost_end),
                (((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 as " + _g.d.ic_resource._balance_amount : _g.d.ic_resource._balance_amount),
                _g.d.ic_resource._qty_in + "/" + _unitStandardRatio + " as " + _g.d.ic_resource._qty_in,
                (((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 " + " as " + _g.d.ic_resource._amount_in : _g.d.ic_resource._amount_in),
                (((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 " : _g.d.ic_resource._average_cost_in + "*" + _unitStandardRatio) + " as " + _g.d.ic_resource._average_cost_in,
                _g.d.ic_resource._qty_out + "/" + _unitStandardRatio + " as " + _g.d.ic_resource._qty_out,
                 (((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 " + " as " + _g.d.ic_resource._amount_out : _g.d.ic_resource._amount_out),
                (((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 " : _g.d.ic_resource._average_cost_out + "*" + _unitStandardRatio) + " as " + _g.d.ic_resource._average_cost_out
                ) + " from (" + __query + __extraWhere + ") as final";
            return __query;
        }

        public string _stkStockInfoAndBalanceByLotQuery(_g.g._productCostType costMode, MyLib._myGrid itemGrid, string itemBegin, string itemEnd, DateTime dateEnd, Boolean getBalanceOnly, _stockBalanceType balanceType)
        {
            return _stkStockInfoAndBalanceByLotQuery(costMode, itemGrid, itemBegin, itemEnd, dateEnd, getBalanceOnly, balanceType, "");
        }

        public string _stkStockInfoAndBalanceByLotQuery(_g.g._productCostType costMode, MyLib._myGrid itemGrid, string itemBegin, string itemEnd, DateTime dateEnd, Boolean getBalanceOnly, _stockBalanceType balanceType, string extraWhere)
        {
            string __dateWhereEnd = (dateEnd != null) ? MyLib._myGlobal._convertDateToQuery(dateEnd) : "";
            return _stkStockInfoAndBalanceByLotQuery(costMode, itemGrid, itemBegin, itemEnd, __dateWhereEnd, getBalanceOnly, balanceType, extraWhere);
        }

        public string _stkStockInfoAndBalanceByLotQuery(_g.g._productCostType costMode, MyLib._myGrid itemGrid, string itemBegin, string itemEnd, string dateEnd, Boolean getBalanceOnly, _stockBalanceType balanceType, string extraWhere)
        {
            string __itemWhere = _g.g._getItemCode(_g.d.ic_trans_detail_lot._item_code, itemGrid, itemBegin, itemEnd);
            if (itemGrid == null && itemEnd.Length == 0)
            {
                __itemWhere = _g.d.ic_trans_detail_lot._item_code + " in (" + itemBegin + ") ";
            }

            string __dateWhereEnd = dateEnd; // (dateEnd != null) ? MyLib._myGlobal._convertDateToQuery(dateEnd) : "";
            string __docDateFieldName = _g.d.ic_trans_detail._doc_date_calc;
            StringBuilder __query = new StringBuilder();
            StringBuilder __where = new StringBuilder();
            switch (balanceType)
            {
                case _stockBalanceType.ยอดคงเหลือตามLOT:
                    {
                        __where.Append(__itemWhere);

                        if (__dateWhereEnd.Length > 0)
                        {
                            if (__where.Length > 0)
                            {
                                __where.Append(" and ");
                            }
                            __where.Append(_g.d.ic_trans_detail_lot._doc_date + "<=\'" + __dateWhereEnd + "\'");
                        }

                        string __unitStandard = "(select " + _g.d.ic_unit_use._stand_value + "/" + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table +
                            " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=temp2." + _g.d.ic_resource._ic_code +
                            " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=temp2." + _g.d.ic_resource._ic_unit_code + ")";
                        // ยอดคงเหลือ
                        __query.Append("select " + _g.d.ic_resource._ic_code + "," + _g.d.ic_resource._ic_name + ",");
                        __query.Append(_g.d.ic_resource._ic_unit_code + "||'~'||(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_resource._ic_unit_code + ") as " + _g.d.ic_resource._ic_unit_code + ",");
                        __query.Append(_g.d.ic_resource._lot_number + ",");
                        __query.Append("qty_in*" + __unitStandard + " as " + _g.d.ic_resource._qty_in +
                            "," + (((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 as " + _g.d.ic_resource._amount_in : _g.d.ic_resource._amount_in) +
                            "," + (((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 as " : "case when qty_in=0 then 0 else " + _g.d.ic_resource._amount_in + "/qty_in end as ") + _g.d.ic_resource._average_cost_in + ",");
                        __query.Append("qty_out*" + __unitStandard + " as " + _g.d.ic_resource._qty_out +
                            "," + (((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 as " + _g.d.ic_resource._amount_out : _g.d.ic_resource._amount_out) +
                            "," + (((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 as " : "case when qty_out=0 then 0 else " + _g.d.ic_resource._amount_out + "/qty_out end as ") + _g.d.ic_resource._average_cost_out + ",");
                        __query.Append(_g.d.ic_resource._balance_qty + "*" + __unitStandard + " as " + _g.d.ic_resource._balance_qty + ",");

                        __query.Append((((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 as " : "case when " + _g.d.ic_resource._balance_qty + "=0 then 0 else " + _g.d.ic_resource._balance_amount + "/" + _g.d.ic_resource._balance_qty + " end as ") + _g.d.ic_resource._average_cost + ",");
                        __query.Append((((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 as " + _g.d.ic_resource._balance_amount : _g.d.ic_resource._balance_amount));

                        string __expireQueryDetail = ",(select " + MyLib._myGlobal._getTopAndLimitOneRecord(1)[0] + " {0} from ic_trans_detail_lot where ic_trans_detail_lot.lot_number = temp2.lot_number and ic_trans_detail_lot.item_code=temp2.ic_code and calc_flag = 1 order by doc_date, doc_time " + MyLib._myGlobal._getTopAndLimitOneRecord(1)[1] + " ) as {1} ";
                        // expire_date
                        __query.Append(string.Format(__expireQueryDetail, _g.d.ic_trans_detail_lot._expire_date, _g.d.ic_resource._date_expire));
                        // mfd
                        __query.Append(string.Format(__expireQueryDetail, _g.d.ic_trans_detail_lot._mfd_date, _g.d.ic_trans_detail_lot._mfd_date));
                        //mfn
                        __query.Append(string.Format(__expireQueryDetail, _g.d.ic_trans_detail_lot._mfn_name, _g.d.ic_trans_detail_lot._mfn_name));

                        __query.Append(" from ");
                        __query.Append("(select " + _g.d.ic_resource._ic_code + "," + _g.d.ic_resource._ic_name + "," + _g.d.ic_resource._ic_unit_code + "," + _g.d.ic_resource._lot_number + ",");
                        __query.Append("coalesce(sum(" + _g.d.ic_resource._balance_qty + "),0) as " + _g.d.ic_resource._balance_qty + ",");
                        __query.Append("coalesce(sum(" + _g.d.ic_resource._balance_amount + "),0) as " + _g.d.ic_resource._balance_amount + ",");
                        __query.Append("sum(qty_in) as qty_in,sum(amount_in) as amount_in,sum(qty_out) as qty_out,sum(amount_out) as amount_out");
                        __query.Append(" from (select ");
                        __query.Append(_g.d.ic_trans_detail_lot._item_code + " as " + _g.d.ic_resource._ic_code + ",");
                        __query.Append("coalesce((select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail_lot._table + "." + _g.d.ic_trans_detail_lot._item_code + "),\'\') as " + _g.d.ic_resource._ic_name + ",");
                        __query.Append("coalesce((select " + _g.d.ic_inventory._unit_cost + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail_lot._table + "." + _g.d.ic_trans_detail_lot._item_code + "),\'\') as " + _g.d.ic_resource._ic_unit_code + ",");
                        __query.Append(_g.d.ic_trans_detail_lot._lot_number + " as " + _g.d.ic_resource._lot_number + ",");
                        // ยอดเข้า
                        __query.Append("case when " + _g.d.ic_trans_detail_lot._calc_flag + "=1 then " + _g.d.ic_trans_detail_lot._qty + " else 0 end as qty_in,");
                        __query.Append("case when " + _g.d.ic_trans_detail_lot._calc_flag + "=1 then " + _g.d.ic_trans_detail_lot._amount + " else 0 end as amount_in,");
                        __query.Append("case when " + _g.d.ic_trans_detail_lot._calc_flag + "=-1 then " + _g.d.ic_trans_detail_lot._qty + " else 0 end as qty_out,");
                        __query.Append("case when " + _g.d.ic_trans_detail_lot._calc_flag + "=-1 then " + _g.d.ic_trans_detail_lot._amount + " else 0 end as amount_out,");
                        //
                        __query.Append(_g.d.ic_trans_detail_lot._calc_flag + "*" + _g.d.ic_trans_detail_lot._qty + " as " + _g.d.ic_resource._balance_qty + ",");
                        __query.Append(_g.d.ic_trans_detail_lot._calc_flag + "*" + _g.d.ic_trans_detail_lot._amount + " as " + _g.d.ic_resource._balance_amount);
                        __query.Append(" from " + _g.d.ic_trans_detail_lot._table + " where " + __where + ") as temp1 ");
                        __query.Append(" group by " + _g.d.ic_resource._ic_code + "," + _g.d.ic_resource._ic_name + "," + _g.d.ic_resource._ic_unit_code + "," + _g.d.ic_resource._lot_number + ") as temp2");
                        if (getBalanceOnly)
                        {
                            __query.Append(" where " + _g.d.ic_resource._balance_qty + "<>0 or " + _g.d.ic_resource._balance_amount + "<>0");
                        }
                        __query.Append(" order by " + _g.d.ic_resource._ic_code + "," + _g.d.ic_resource._lot_number);
                    }
                    break;
                case _stockBalanceType.ยอดคงเหลือตามLOT_ที่เก็บ:
                    {
                        __where.Append(__itemWhere);
                        if (__dateWhereEnd.Length > 0)
                        {
                            if (__where.Length > 0)
                            {
                                __where.Append(" and ");
                            }

                            __where.Append(_g.d.ic_trans_detail_lot._doc_date + "<=\'" + __dateWhereEnd + "\'");
                        }


                        if (__where.Length > 0 && extraWhere.Length > 0)
                        {
                            __where.Append(" and " + extraWhere);
                        }

                        string __unitStandard = "(select " + _g.d.ic_unit_use._stand_value + "/" + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table +
                            " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=temp2." + _g.d.ic_resource._ic_code +
                            " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=temp2." + _g.d.ic_resource._ic_unit_code + ")";
                        // ยอดคงเหลือ
                        __query.Append("select " + _g.d.ic_resource._ic_code + "," + _g.d.ic_resource._ic_name + ",");
                        __query.Append(_g.d.ic_resource._ic_unit_code + "||'~'||(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_resource._ic_unit_code + ") as " + _g.d.ic_resource._ic_unit_code + ",");
                        __query.Append(_g.d.ic_resource._lot_number + ",");
                        __query.Append("qty_in*" + __unitStandard + " as " + _g.d.ic_resource._qty_in +
                            "," + (((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 as " + _g.d.ic_resource._amount_in : _g.d.ic_resource._amount_in) +
                            "," + (((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 as " : "case when qty_in=0 then 0 else " + _g.d.ic_resource._amount_in + "/qty_in end as ") + _g.d.ic_resource._average_cost_in + ",");
                        __query.Append("qty_out*" + __unitStandard + " as " + _g.d.ic_resource._qty_out +
                            "," + (((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 as " + _g.d.ic_resource._amount_out : _g.d.ic_resource._amount_out) +
                            ", " + (((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 as " : "case when qty_out=0 then 0 else " + _g.d.ic_resource._amount_out + "/qty_out end as ") + _g.d.ic_resource._average_cost_out + ",");
                        __query.Append(_g.d.ic_resource._balance_qty + "*" + __unitStandard + " as " + _g.d.ic_resource._balance_qty + ",");

                        __query.Append((((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 as " : "case when " + _g.d.ic_resource._balance_qty + "=0 then 0 else " + _g.d.ic_resource._balance_amount + "/" + _g.d.ic_resource._balance_qty + " end as ") + _g.d.ic_resource._average_cost + ",");
                        __query.Append((((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0  as " + _g.d.ic_resource._balance_amount : _g.d.ic_resource._balance_amount));

                        string __expireQueryDetail = ",(select " + MyLib._myGlobal._getTopAndLimitOneRecord(1)[0] + " {0} from ic_trans_detail_lot where ic_trans_detail_lot.lot_number = temp2.lot_number and ic_trans_detail_lot.item_code=temp2.ic_code and calc_flag = 1 order by doc_date, doc_time " + MyLib._myGlobal._getTopAndLimitOneRecord(1)[1] + " ) as {1} ";
                        // expire_date
                        __query.Append(string.Format(__expireQueryDetail, _g.d.ic_trans_detail_lot._expire_date, _g.d.ic_resource._date_expire));
                        // mfd
                        __query.Append(string.Format(__expireQueryDetail, _g.d.ic_trans_detail_lot._mfd_date, _g.d.ic_trans_detail_lot._mfd_date));
                        //mfn
                        __query.Append(string.Format(__expireQueryDetail, _g.d.ic_trans_detail_lot._mfn_name, _g.d.ic_trans_detail_lot._mfn_name));
                        __query.Append("," + _g.d.ic_trans_detail_lot._wh_code + "," + _g.d.ic_trans_detail_lot._shelf_code);

                        __query.Append(" from ");
                        __query.Append("(select " + _g.d.ic_resource._ic_code + "," + _g.d.ic_resource._ic_name + "," + _g.d.ic_resource._ic_unit_code + "," + _g.d.ic_resource._lot_number + ",");
                        __query.Append("coalesce(sum(" + _g.d.ic_resource._balance_qty + "),0) as " + _g.d.ic_resource._balance_qty + ",");
                        __query.Append("coalesce(sum(" + _g.d.ic_resource._balance_amount + "),0) as " + _g.d.ic_resource._balance_amount + ",");
                        __query.Append("sum(qty_in) as qty_in,sum(amount_in) as amount_in,sum(qty_out) as qty_out,sum(amount_out) as amount_out");
                        __query.Append("," + _g.d.ic_trans_detail_lot._wh_code + "," + _g.d.ic_trans_detail_lot._shelf_code);

                        __query.Append(" from (select ");
                        __query.Append(_g.d.ic_trans_detail_lot._item_code + " as " + _g.d.ic_resource._ic_code + ",");
                        __query.Append("coalesce((select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail_lot._table + "." + _g.d.ic_trans_detail_lot._item_code + "),\'\') as " + _g.d.ic_resource._ic_name + ",");
                        __query.Append("coalesce((select " + _g.d.ic_inventory._unit_cost + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail_lot._table + "." + _g.d.ic_trans_detail_lot._item_code + "),\'\') as " + _g.d.ic_resource._ic_unit_code + ",");
                        __query.Append(_g.d.ic_trans_detail_lot._lot_number + " as " + _g.d.ic_resource._lot_number + ",");
                        // ยอดเข้า
                        __query.Append("case when " + _g.d.ic_trans_detail_lot._calc_flag + "=1 then " + _g.d.ic_trans_detail_lot._qty + " else 0 end as qty_in,");
                        __query.Append("case when " + _g.d.ic_trans_detail_lot._calc_flag + "=1 then " + _g.d.ic_trans_detail_lot._amount + " else 0 end as amount_in,");
                        __query.Append("case when " + _g.d.ic_trans_detail_lot._calc_flag + "=-1 then " + _g.d.ic_trans_detail_lot._qty + " else 0 end as qty_out,");
                        __query.Append("case when " + _g.d.ic_trans_detail_lot._calc_flag + "=-1 then " + _g.d.ic_trans_detail_lot._amount + " else 0 end as amount_out,");
                        //
                        __query.Append(_g.d.ic_trans_detail_lot._calc_flag + "*" + _g.d.ic_trans_detail_lot._qty + " as " + _g.d.ic_resource._balance_qty + ",");
                        __query.Append(_g.d.ic_trans_detail_lot._calc_flag + "*" + _g.d.ic_trans_detail_lot._amount + " as " + _g.d.ic_resource._balance_amount);
                        __query.Append("," + _g.d.ic_trans_detail_lot._wh_code + "," + _g.d.ic_trans_detail_lot._shelf_code);
                        __query.Append(" from " + _g.d.ic_trans_detail_lot._table + " where " + __where + ") as temp1 ");
                        __query.Append(" group by " + _g.d.ic_resource._ic_code + "," + _g.d.ic_resource._ic_name + "," + _g.d.ic_resource._ic_unit_code + "," + _g.d.ic_resource._lot_number + "," + _g.d.ic_trans_detail_lot._wh_code + "," + _g.d.ic_trans_detail_lot._shelf_code + ") as temp2");
                        if (getBalanceOnly)
                        {
                            __query.Append(" where " + _g.d.ic_resource._balance_qty + "<>0 or " + _g.d.ic_resource._balance_amount + "<>0");
                        }



                        __query.Append(" order by " + _g.d.ic_resource._ic_code + "," + _g.d.ic_resource._lot_number);
                    }
                    break;
                case _stockBalanceType.ยอดคงเหลือตามLOT_ที่เก็บ_เรียงตามเอกสาร_IMEX:
                    {
                        __where.Append(__itemWhere);
                        //if (__where.Length > 0)
                        //{
                        //    __where.Append(" and ");
                        //}

                        //__where.Append(_g.d.ic_trans_detail_lot._doc_date + "<=\'" + __dateWhereEnd + "\'");

                        //if (__where.Length > 0 && extraWhere.Length > 0)
                        //{
                        //    __where.Append(" and " + extraWhere);
                        //}

                        string __unitStandard = "(select " + _g.d.ic_unit_use._stand_value + "/" + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table +
                            " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=temp2." + _g.d.ic_resource._ic_code +
                            " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=temp2." + _g.d.ic_resource._ic_unit_code + ")";
                        // ยอดคงเหลือ
                        __query.Append("select " + _g.d.ic_resource._ic_code + "," + _g.d.ic_resource._ic_name + ",");
                        __query.Append(_g.d.ic_resource._ic_unit_code + "||'~'||(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_resource._ic_unit_code + ") as " + _g.d.ic_resource._ic_unit_code + ",");
                        __query.Append(_g.d.ic_resource._lot_number + ",");
                        __query.Append("qty_in*" + __unitStandard + " as " + _g.d.ic_resource._qty_in +
                            "," + (((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 as " + _g.d.ic_resource._amount_in : _g.d.ic_resource._amount_in) +
                            "," + (((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 as " : "case when qty_in=0 then 0 else " + _g.d.ic_resource._amount_in + "/qty_in end as ") + _g.d.ic_resource._average_cost_in + ",");
                        __query.Append("qty_out*" + __unitStandard + " as " + _g.d.ic_resource._qty_out +
                            "," + (((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 as " + _g.d.ic_resource._amount_out : _g.d.ic_resource._amount_out) +
                            ", " + (((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 as " : "case when qty_out=0 then 0 else " + _g.d.ic_resource._amount_out + "/qty_out end as ") + _g.d.ic_resource._average_cost_out + ",");
                        __query.Append(_g.d.ic_resource._balance_qty + "*" + __unitStandard + " as " + _g.d.ic_resource._balance_qty + ",");

                        __query.Append((((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 as " : "case when " + _g.d.ic_resource._balance_qty + "=0 then 0 else " + _g.d.ic_resource._balance_amount + "/" + _g.d.ic_resource._balance_qty + " end as ") + _g.d.ic_resource._average_cost + ",");
                        __query.Append((((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0  as " + _g.d.ic_resource._balance_amount : _g.d.ic_resource._balance_amount));

                        string __expireQueryDetail = ",(select " + MyLib._myGlobal._getTopAndLimitOneRecord(1)[0] + " {0} from ic_trans_detail_lot where ic_trans_detail_lot.lot_number = temp2.lot_number and ic_trans_detail_lot.item_code=temp2.ic_code and calc_flag = 1 order by doc_date, doc_time " + MyLib._myGlobal._getTopAndLimitOneRecord(1)[1] + " ) as {1} ";

                        string __expireQueryDetail2 = ",(select " + MyLib._myGlobal._getTopAndLimitOneRecord(1)[0] + " {0} from ic_trans_detail_lot where ic_trans_detail_lot.lot_number = temp2.lot_number and ic_trans_detail_lot.item_code=temp2.ic_code and calc_flag = 1 and ic_trans_detail_lot.wh_code = temp2.wh_code and ic_trans_detail_lot.shelf_code = temp2.shelf_code order by doc_date, doc_time " + MyLib._myGlobal._getTopAndLimitOneRecord(1)[1] + " ) as {1} ";
                        // expire_date
                        __query.Append(string.Format(__expireQueryDetail, _g.d.ic_trans_detail_lot._expire_date, _g.d.ic_resource._date_expire));
                        // mfd
                        __query.Append(string.Format(__expireQueryDetail, _g.d.ic_trans_detail_lot._mfd_date, _g.d.ic_trans_detail_lot._mfd_date));
                        //mfn
                        __query.Append(string.Format(__expireQueryDetail, _g.d.ic_trans_detail_lot._mfn_name, _g.d.ic_trans_detail_lot._mfn_name));
                        __query.Append(string.Format(__expireQueryDetail2, _g.d.ic_trans_detail_lot._doc_date, _g.d.ic_trans_detail_lot._doc_date));
                        __query.Append(string.Format(__expireQueryDetail2, _g.d.ic_trans_detail_lot._doc_time, _g.d.ic_trans_detail_lot._doc_time));

                        __query.Append("," + _g.d.ic_trans_detail_lot._wh_code + "," + _g.d.ic_trans_detail_lot._shelf_code);

                        string __getLotManageQuery = ", coalesce((select {0} from " + _g.d.ic_lot_manage._table + " where " + _g.d.ic_lot_manage._table + "." + _g.d.ic_lot_manage._ic_code + " = temp2." + _g.d.ic_resource._ic_code + " and " + _g.d.ic_lot_manage._table + "." + _g.d.ic_lot_manage._lot_number + " = temp2." + _g.d.ic_resource._lot_number + " and " + _g.d.ic_lot_manage._table + "." + _g.d.ic_lot_manage._wh_code + " = temp2." + _g.d.ic_trans_detail_lot._wh_code + " and " + _g.d.ic_lot_manage._table + "." + _g.d.ic_lot_manage._shelf_code + " = temp2." + _g.d.ic_trans_detail_lot._shelf_code + "), {2} ) as {1} ";
                        __query.Append(string.Format(__getLotManageQuery, _g.d.ic_lot_manage._sort_order, _g.d.ic_resource._sort_order, "\'A\'"));
                        __query.Append(string.Format(__getLotManageQuery, _g.d.ic_lot_manage._lot_select, _g.d.ic_resource._lot_select, "0"));


                        __query.Append(" from ");
                        __query.Append("(select " + _g.d.ic_resource._ic_code + "," + _g.d.ic_resource._ic_name + "," + _g.d.ic_resource._ic_unit_code + "," + _g.d.ic_resource._lot_number + ",");
                        __query.Append("coalesce(sum(" + _g.d.ic_resource._balance_qty + "),0) as " + _g.d.ic_resource._balance_qty + ",");
                        __query.Append("coalesce(sum(" + _g.d.ic_resource._balance_amount + "),0) as " + _g.d.ic_resource._balance_amount + ",");
                        __query.Append("sum(qty_in) as qty_in,sum(amount_in) as amount_in,sum(qty_out) as qty_out,sum(amount_out) as amount_out");
                        __query.Append("," + _g.d.ic_trans_detail_lot._wh_code + "," + _g.d.ic_trans_detail_lot._shelf_code);

                        __query.Append(" from (select ");
                        __query.Append(_g.d.ic_trans_detail_lot._item_code + " as " + _g.d.ic_resource._ic_code + ",");
                        __query.Append("coalesce((select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail_lot._table + "." + _g.d.ic_trans_detail_lot._item_code + "),\'\') as " + _g.d.ic_resource._ic_name + ",");
                        __query.Append("coalesce((select " + _g.d.ic_inventory._unit_cost + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail_lot._table + "." + _g.d.ic_trans_detail_lot._item_code + "),\'\') as " + _g.d.ic_resource._ic_unit_code + ",");
                        __query.Append(_g.d.ic_trans_detail_lot._lot_number + " as " + _g.d.ic_resource._lot_number + ",");
                        // ยอดเข้า
                        __query.Append("case when " + _g.d.ic_trans_detail_lot._calc_flag + "=1 then " + _g.d.ic_trans_detail_lot._qty + " else 0 end as qty_in,");
                        __query.Append("case when " + _g.d.ic_trans_detail_lot._calc_flag + "=1 then " + _g.d.ic_trans_detail_lot._amount + " else 0 end as amount_in,");
                        __query.Append("case when " + _g.d.ic_trans_detail_lot._calc_flag + "=-1 then " + _g.d.ic_trans_detail_lot._qty + " else 0 end as qty_out,");
                        __query.Append("case when " + _g.d.ic_trans_detail_lot._calc_flag + "=-1 then " + _g.d.ic_trans_detail_lot._amount + " else 0 end as amount_out,");
                        //
                        __query.Append(_g.d.ic_trans_detail_lot._calc_flag + "*" + _g.d.ic_trans_detail_lot._qty + " as " + _g.d.ic_resource._balance_qty + ",");
                        __query.Append(_g.d.ic_trans_detail_lot._calc_flag + "*" + _g.d.ic_trans_detail_lot._amount + " as " + _g.d.ic_resource._balance_amount);
                        __query.Append("," + _g.d.ic_trans_detail_lot._wh_code + "," + _g.d.ic_trans_detail_lot._shelf_code);
                        __query.Append(" from " + _g.d.ic_trans_detail_lot._table + " where " + __where + ") as temp1 ");
                        __query.Append(" group by " + _g.d.ic_resource._ic_code + "," + _g.d.ic_resource._ic_name + "," + _g.d.ic_resource._ic_unit_code + "," + _g.d.ic_resource._lot_number + "," + _g.d.ic_trans_detail_lot._wh_code + "," + _g.d.ic_trans_detail_lot._shelf_code + ") as temp2");

                        string __where2 = "";
                        if (getBalanceOnly)
                        {
                            __where2 = " where ( " + _g.d.ic_resource._balance_qty + "<>0 ) ";
                        }

                        if (extraWhere.Length > 0)
                        {

                            __where2 = __where2 + ((__where2.Length == 0) ? " where " : " and ");

                            __where2 = __where2 + extraWhere;
                        }

                        if (__where2.Length > 0)
                            __query.Append(__where2);

                        __query.Append(" order by " + _g.d.ic_lot_manage._sort_order + "," + _g.d.ic_resource._doc_date + "," + _g.d.ic_resource._doc_time + "," + _g.d.ic_resource._lot_number);
                    }
                    break;

            }
            //string __temp = __query.ToString();
            return __query.ToString();
        }

        public DataTable _stkLotInfoAndBalance(MyLib._myGrid itemGrid, string itemBegin, string itemEnd, string wareHouseList, string locationList, bool haveStock)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __query = this._stkLotInfoAndBalanceQuery(itemGrid, itemBegin, itemEnd, wareHouseList, locationList, haveStock);
            return __myFrameWork._queryShort(__query).Tables[0];
        }

        public string _stkLotInfoAndBalanceQuery(MyLib._myGrid itemGrid, string itemBegin, string itemEnd, string wareHouseList, string locationList, bool haveStock)
        {
            return this._stkLotInfoAndBalanceQuery(itemGrid, itemBegin, itemEnd, wareHouseList, locationList, haveStock, true);
        }

        public string _stkLotInfoAndBalanceQuery(MyLib._myGrid itemGrid, string itemBegin, string itemEnd, string wareHouseList, string locationList, bool haveStock, Boolean getBalanceOnly)
        {
            string __itemWhere = _g.g._getItemCode(_g.d.ic_resource._ic_code, itemGrid, itemBegin, itemEnd);
            //string __dateWhereBegin = MyLib._myGlobal._convertDateToQuery(dateBegin);
            //string __dateWhereEnd = MyLib._myGlobal._convertDateToQuery(dateEnd);
            string __docDateFieldName = _g.d.ic_trans_detail._doc_date_calc;
            string __query = "";
            string __groupBy = "";
            string __fieldList0 = "";
            string __fieldList1 = "";
            string __fieldList2 = "";
            string __fieldSumOfCost = _g.d.ic_trans_detail._sum_of_cost;// + "+profit_lost_cost_amount"; // (costMode == _g.g._productCostType.ปรกติ) ? _g.d.ic_trans_detail._sum_of_cost : _g.d.ic_trans_detail._sum_of_cost_1;
            string __wareHouseAndLocationQuery = ((wareHouseList.Length == 0) ? "" : " and " + (_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_code + " in (" + wareHouseList + ")")) + ((locationList.Length == 0) ? "" : " and " + (_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._shelf_code + " in (" + locationList + ")"));


            // เอายอดคงเหลือตามที่เก็บ เท่านั้น
            __groupBy = MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._wh_code, _g.d.ic_trans_detail._shelf_code, " coalesce(" + _g.d.ic_trans_detail._lot_number_1 + ", \'error\') ");
            __fieldList0 = _g.d.ic_resource._ic_code + "," + _wareHouseField + " as wh_code ," + _locationField + " as shelf_code," + _g.d.ic_resource._lot_number + ",sort_order,lot_select, date_expire,mfd_date, mfn_name, doc_date, doc_time ";
            __fieldList1 = _g.d.ic_trans_detail._item_code + " as " + _g.d.ic_resource._ic_code + "," + _g.d.ic_trans_detail._wh_code + " as " + _wareHouseField + "," + _g.d.ic_trans_detail._shelf_code + " as " + _locationField + "," + " coalesce(" + _g.d.ic_trans_detail._lot_number_1 + ", \'error\') " + " as " + _g.d.ic_resource._lot_number;
            __fieldList2 = _g.d.ic_resource._ic_code + "," + _wareHouseField + "," + _locationField + "," + _g.d.ic_resource._lot_number;


            string __queryGetAverageCost = "(select " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + " " + _g.d.ic_trans_detail._average_cost + " from " + _g.d.ic_trans_detail._table +
" where " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._last_status + "=0 and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "=" + "temp2." + _g.d.ic_resource._ic_code +
// and " + __docDateFieldName + "<=\'" + __dateWhereEnd + "\'
" and " + _g._icInfoFlag._allFlagAmount + " order by " + __docDateFieldName + " desc," + _g.d.ic_trans_detail._doc_time + " desc ," + _g.d.ic_trans_detail._line_number + " desc " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + ")";
            //
            string __queryIn = "";
            string __queryOut = "";

            if (getBalanceOnly == false)
            {
                // ยอดเข้า 
                __queryIn = "sum(case when " + _g._icInfoFlag._inFlag + " then " + _g.d.ic_trans_detail._calc_flag + "*(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + ")) else 0 end) as " + _g.d.ic_resource._qty_in + ",sum(case when " + _g._icInfoFlag._inFlagAmountOnly + " then " + _g.d.ic_trans_detail._calc_flag + "*" + __fieldSumOfCost + " else 0 end) as " + _g.d.ic_resource._amount_in;
                // ยอดออก
                __queryOut = "-1*sum(case when " + _g._icInfoFlag._outFlagAmountOnly + " then " + _g.d.ic_trans_detail._calc_flag + "*" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + ") else 0 end) as " + _g.d.ic_resource._qty_out + ",-1*sum(case when " + _g._icInfoFlag._outFlagAmountOnly + " then (case when " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก) + " and " + _g.d.ic_trans_detail._qty + " < 0 then -1 else " + _g.d.ic_trans_detail._calc_flag + " end) * " + __fieldSumOfCost + " else 0 end) as " + _g.d.ic_resource._amount_out;
            }
            else
            {
                __queryIn = "0 as " + _g.d.ic_resource._qty_in + ",0 as " + _g.d.ic_resource._amount_in;
                __queryOut = "0 as " + _g.d.ic_resource._qty_out + ",0 as " + _g.d.ic_resource._amount_out;
            }
            //
            string __itemNameQuery = _g.d.ic_inventory._name_1;
            //if (haveBarcode)
            //{
            //    __itemNameQuery = __itemNameQuery + MyLib._myGlobal._getSignPlusStringQuery() + "\'(\'" + MyLib._myGlobal._getSignPlusStringQuery() + "coalesce((select  " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + " " + _g.d.ic_inventory_barcode._barcode + " from " + _g.d.ic_inventory_barcode._table + " where " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " and " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._unit_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._unit_standard + " " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + " ),\'\')" + MyLib._myGlobal._getSignPlusStringQuery() + "\')\'";
            //}
            //
            __query = "(select " + MyLib._myGlobal._fieldAndComma(__fieldList1, "(select " + __itemNameQuery + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_resource._ic_name, "(select " + _g.d.ic_inventory._unit_standard + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_resource._ic_unit_code, "coalesce(sum(" + _g.d.ic_trans_detail._calc_flag + "*(case when " + _g._icInfoFlag._allFlagQty + " then " + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + " / " + _g.d.ic_trans_detail._divide_value + ") else 0 end)),0) as " + _g.d.ic_resource._balance_qty
                , "coalesce(sum(" + _g.d.ic_trans_detail._calc_flag + "*(case when " + _g._icInfoFlag._allFlagAmount + " then case when " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก) + " and " + _g.d.ic_trans_detail._qty + " < 0 then -1* " + __fieldSumOfCost + " else " + __fieldSumOfCost + " end else 0 end)),0) as " + _g.d.ic_resource._balance_amount
                , __queryIn, __queryOut) + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._last_status + "=0 and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_type + "<>5  "
                // and " + __docDateFieldName + "<=\'" + __dateWhereEnd + "\'
                + __wareHouseAndLocationQuery + " group by " + __groupBy + ") as temp1";
            //
            __query = "(select " + MyLib._myGlobal._fieldAndComma(__fieldList2, _g.d.ic_resource._ic_name, _g.d.ic_resource._balance_qty, _g.d.ic_resource._ic_unit_code, "(select " + _g.d.ic_inventory._unit_standard_stand_value + "/" + _g.d.ic_inventory._unit_standard_divide_value + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=temp1.ic_code) as " + _unitRatio, _g.d.ic_resource._balance_amount, _g.d.ic_resource._qty_in, _g.d.ic_resource._amount_in, _g.d.ic_resource._qty_out, _g.d.ic_resource._amount_out) + " from " + __query + ") as temp2 ";
            //

            // temp2
            StringBuilder __queryExtra = new StringBuilder();

            string __expireQueryDetail = ",(select " + MyLib._myGlobal._getTopAndLimitOneRecord(1)[0] + " {0} from ic_trans_detail where ic_trans_detail.item_code=temp2.ic_code and ic_trans_detail.lot_number_1 = temp2.lot_number and calc_flag = 1 order by doc_date, doc_time " + MyLib._myGlobal._getTopAndLimitOneRecord(1)[1] + " ) as {1} ";

            string __expireQueryDetail2 = ",(select " + MyLib._myGlobal._getTopAndLimitOneRecord(1)[0] + " {0} from ic_trans_detail where ic_trans_detail.item_code=temp2.ic_code and ic_trans_detail.lot_number_1 = temp2.lot_number and  calc_flag = 1 and ic_trans_detail.wh_code = temp2.warehouse and ic_trans_detail.shelf_code = temp2.location order by doc_date, doc_time " + MyLib._myGlobal._getTopAndLimitOneRecord(1)[1] + " ) as {1} ";
            // expire_date
            __queryExtra.Append(string.Format(__expireQueryDetail, _g.d.ic_trans_detail._date_expire, _g.d.ic_resource._date_expire));
            // mfd
            __queryExtra.Append(string.Format(__expireQueryDetail, _g.d.ic_trans_detail._mfd_date, _g.d.ic_trans_detail._mfd_date));
            //mfn
            __queryExtra.Append(string.Format(__expireQueryDetail, _g.d.ic_trans_detail._mfn_name, _g.d.ic_trans_detail._mfn_name));
            __queryExtra.Append(string.Format(__expireQueryDetail2, _g.d.ic_trans_detail._doc_date, _g.d.ic_trans_detail._doc_date));
            __queryExtra.Append(string.Format(__expireQueryDetail2, _g.d.ic_trans_detail._doc_time, _g.d.ic_trans_detail._doc_time));

            string __getLotManageQuery = ", coalesce((select {0} from " + _g.d.ic_lot_manage._table + " where " + _g.d.ic_lot_manage._table + "." + _g.d.ic_lot_manage._ic_code + " = temp2." + _g.d.ic_resource._ic_code + " and " + _g.d.ic_lot_manage._table + "." + _g.d.ic_lot_manage._lot_number + " = temp2." + _g.d.ic_resource._lot_number + " and " + _g.d.ic_lot_manage._table + "." + _g.d.ic_lot_manage._wh_code + " = temp2.warehouse and " + _g.d.ic_lot_manage._table + "." + _g.d.ic_lot_manage._shelf_code + " = temp2.location), {2} ) as {1} ";
            __queryExtra.Append(string.Format(__getLotManageQuery, _g.d.ic_lot_manage._sort_order, _g.d.ic_resource._sort_order, "\'A\'"));
            __queryExtra.Append(string.Format(__getLotManageQuery, _g.d.ic_lot_manage._lot_select, _g.d.ic_resource._lot_select, "0"));


            __query = "select coalesce((select stand_value/divide_value from ic_unit_use where ic_unit_use.ic_code=temp2.ic_code and ic_unit_use.code=temp2.ic_unit_code),1) as " + _unitStandardRatio + "," + MyLib._myGlobal._fieldAndComma(__fieldList2, _g.d.ic_resource._ic_name, _g.d.ic_resource._balance_qty, _g.d.ic_resource._ic_unit_code, "case when " + _g.d.ic_resource._balance_qty + "=0 then 0 else " + _g.d.ic_resource._balance_amount + "/" + _g.d.ic_resource._balance_qty + " end as " + _g.d.ic_resource._average_cost, "coalesce((" + __queryGetAverageCost + "*" + _unitRatio + "),0) as " + _g.d.ic_resource._average_cost_end, "case when " + _g.d.ic_resource._balance_qty + "=0 then 0 else " + _g.d.ic_resource._balance_amount + " end as " + _g.d.ic_resource._balance_amount, _g.d.ic_resource._qty_in,
                // toe เอา case when ออก กรณีไม่มียอดคงเหลือแต่มียอดเข้า ต้องแสดงมูลค่าเข้าด้วย
                // " case when " + _g.d.ic_resource._balance_qty + " = 0 then 0 else " + _g.d.ic_resource._amount_in + " end as " +
                _g.d.ic_resource._amount_in, "case when " + _g.d.ic_resource._qty_in + "=0 then 0 else " + _g.d.ic_resource._amount_in + "/" + _g.d.ic_resource._qty_in + " end as " + _g.d.ic_resource._average_cost_in, _g.d.ic_resource._qty_out, _g.d.ic_resource._amount_out, "case when " + _g.d.ic_resource._qty_out + "=0 then 0 else " + _g.d.ic_resource._amount_out + "/" + _g.d.ic_resource._qty_out + " end as " + _g.d.ic_resource._average_cost_out) + __queryExtra.ToString() + " from " + __query;
            //
            string __extraWhere = "";
            //if (itemNameFilter.Trim().Length != 0)
            //{
            //    __extraWhere = " where (" + _g.d.ic_resource._ic_code + " like \'%" + itemNameFilter.Trim() + "%\' or " + _g.d.ic_resource._ic_name + " like \'%" + itemNameFilter.Trim() + "%\')";
            //}
            if (__itemWhere.Length > 0)
            {
                __extraWhere = __extraWhere + ((__extraWhere.Length == 0) ? " where " : " and ") + __itemWhere + " ";
            }

            //if (getBalanceOnly == false)
            //{
            //    __extraWhere = __extraWhere + ((__extraWhere.Length == 0) ? " where " : " and ") + "(" + _g.d.ic_resource._qty_in + "<>0 or " + _g.d.ic_resource._amount_in + "<>0 or " + _g.d.ic_resource._qty_out + "<>0 or " + _g.d.ic_resource._amount_out + "<>0 or " + _g.d.ic_resource._balance_qty + "<>0 or " + _g.d.ic_resource._balance_amount + "<>0)";
            //}
            //else
            {
                // โต๋ เพิ่มกรณี ดึงรายารที่มียอด เป็น 0 มาด้วย
                if (haveStock)
                {
                    __extraWhere = __extraWhere + ((__extraWhere.Length == 0) ? " where " : " and ") + "(" + _g.d.ic_resource._balance_qty + "<>0 )";
                }
            }

            // final
            __query = "select " + MyLib._myGlobal._fieldAndComma(__fieldList0,
                _g.d.ic_resource._ic_name,
                _g.d.ic_resource._ic_unit_code,
                _g.d.ic_resource._balance_qty + "/" + _unitStandardRatio + " as " + _g.d.ic_resource._balance_qty,
                (((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 " : _g.d.ic_resource._average_cost + "*" + _unitStandardRatio) + " as " + _g.d.ic_resource._average_cost,
                (((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 " + " as " + _g.d.ic_resource._average_cost_end : _g.d.ic_resource._average_cost_end),
                (((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 as " + _g.d.ic_resource._balance_amount : _g.d.ic_resource._balance_amount),
                _g.d.ic_resource._qty_in + "/" + _unitStandardRatio + " as " + _g.d.ic_resource._qty_in,
                (((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 " + " as " + _g.d.ic_resource._amount_in : _g.d.ic_resource._amount_in),
                (((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 " : _g.d.ic_resource._average_cost_in + "*" + _unitStandardRatio) + " as " + _g.d.ic_resource._average_cost_in,
                _g.d.ic_resource._qty_out + "/" + _unitStandardRatio + " as " + _g.d.ic_resource._qty_out,
                 (((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 " + " as " + _g.d.ic_resource._amount_out : _g.d.ic_resource._amount_out),
                (((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false)) ? " 0 " : _g.d.ic_resource._average_cost_out + "*" + _unitStandardRatio) + " as " + _g.d.ic_resource._average_cost_out
                ) + " from (" + __query + __extraWhere + ") as final order by sort_order,doc_date,doc_time,lot_number ";
            return __query;
        }


        /// <summary>
        /// คำนวณยอดคงเหลือ สำหรับหน้าจอ และรายงาน
        /// </summary>
        /// <param name="itemBegin"></param>
        /// <param name="itemEnd"></param>
        /// <param name="dateBegin">วันที่เริ่มต้น</param>
        /// <param name="dateEnd">วันที่สิ้นสุด</param>
        /// <param name="getBalanceOnly">True=คำนวณยอดคงเหลือเท่านั้น,False=คำนวณทุกอย่าง</param>
        /// <returns></returns>
        public DataTable _stkStockInfoAndBalance(_g.g._productCostType costMode, MyLib._myGrid itemGrid, string itemBegin, string itemEnd, DateTime dateBegin, DateTime dateEnd, Boolean getBalanceOnly, _stockBalanceType balanceType, string wareHouseList, string locationList, Boolean haveStock, Boolean haveBarcode, string itemNameFilter)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __query = this._stkStockInfoAndBalanceQuery(costMode, itemGrid, itemBegin, itemEnd, dateBegin, dateEnd, getBalanceOnly, balanceType, wareHouseList, locationList, haveStock, haveBarcode, itemNameFilter) + " order by " + _g.d.ic_resource._ic_code;
            return __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __query).Tables[0];
        }

        public DataTable _stkStockInfoAndBalanceByWareHouse(_g.g._productCostType costMode, MyLib._myGrid itemGrid, string itemBegin, string itemEnd, DateTime dateBegin, DateTime dateEnd, Boolean getBalanceOnly, _stockBalanceType balanceType, string wareHouseList, string locationList, Boolean haveStock, Boolean haveBarcode, string itemNameFilter)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __query = this._stkStockInfoAndBalanceQuery(costMode, itemGrid, itemBegin, itemEnd, dateBegin, dateEnd, getBalanceOnly, balanceType, wareHouseList, locationList, haveStock, haveBarcode, itemNameFilter) + " order by " + _g.d.ic_resource._ic_code + "," + _g.d.ic_resource._warehouse;
            return __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __query).Tables[0];
        }

        public DataTable _stkStockInfoAndBalanceByLocation(_g.g._productCostType costMode, MyLib._myGrid itemGrid, string itemBegin, string itemEnd, DateTime dateBegin, DateTime dateEnd, Boolean getBalanceOnly, _stockBalanceType balanceType, string wareHouseList, string locationList, Boolean haveStock, Boolean haveBarcode, string itemNameFilter)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __query = this._stkStockInfoAndBalanceQuery(costMode, itemGrid, itemBegin, itemEnd, dateBegin, dateEnd, getBalanceOnly, balanceType, wareHouseList, locationList, haveStock, haveBarcode, itemNameFilter) + " order by " + _g.d.ic_resource._ic_code + "," + _g.d.ic_resource._warehouse + "," + _g.d.ic_resource._location;
            return __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __query).Tables[0];
        }

        public DataTable _stkStockInfoAndBalanceByLot(_g.g._productCostType costMode, MyLib._myGrid itemGrid, string itemBegin, string itemEnd, DateTime dateEnd, Boolean getBalanceOnly, _stockBalanceType balanceType)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __query = this._stkStockInfoAndBalanceByLotQuery(costMode, itemGrid, itemBegin, itemEnd, dateEnd, getBalanceOnly, balanceType);
            return __myFrameWork._queryShort(__query).Tables[0];
        }

        public DataTable _stkStockInfoAndBalanceByLot(_g.g._productCostType costMode, MyLib._myGrid itemGrid, string itemBegin, string itemEnd, string dateEnd, Boolean getBalanceOnly, _stockBalanceType balanceType)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __query = this._stkStockInfoAndBalanceByLotQuery(costMode, itemGrid, itemBegin, itemEnd, dateEnd, getBalanceOnly, balanceType, "");
            return __myFrameWork._queryShort(__query).Tables[0];
        }

        public DataTable _stkStockInfoAndBalanceByLotLocation(_g.g._productCostType costMode, MyLib._myGrid itemGrid, string itemBegin, string itemEnd, DateTime dateEnd, Boolean getBalanceOnly, _stockBalanceType balanceType, string extraWhere)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __query = this._stkStockInfoAndBalanceByLotQuery(costMode, itemGrid, itemBegin, itemEnd, dateEnd, getBalanceOnly, balanceType, extraWhere);
            return __myFrameWork._queryShort(__query).Tables[0];
        }

        public DataTable _stkStockInfoAndBalanceByLotLocation(_g.g._productCostType costMode, MyLib._myGrid itemGrid, string itemBegin, string itemEnd, string dateEnd, Boolean getBalanceOnly, _stockBalanceType balanceType, string extraWhere)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __query = this._stkStockInfoAndBalanceByLotQuery(costMode, itemGrid, itemBegin, itemEnd, dateEnd, getBalanceOnly, balanceType, extraWhere);
            return __myFrameWork._queryShort(__query).Tables[0];
        }


        public string _stkStockMovementSumQuery(_g.g._productCostType costMode, int mode, MyLib._myGrid itemGrid, string itemBegin, string itemEnd, DateTime dateBegin, DateTime dateEnd, Boolean movementOnly)
        {
            return _stkStockMovementSumQuery(costMode, mode, itemGrid, itemBegin, itemEnd, dateBegin, dateEnd, movementOnly, "");
        }

        public string _stkStockMovementSumQuery(_g.g._productCostType costMode, int mode, MyLib._myGrid itemGrid, string itemBegin, string itemEnd, DateTime dateBegin, DateTime dateEnd, Boolean movementOnly, string extraWhere)
        {
            string __fieldSumOfCost = (costMode == _g.g._productCostType.ปรกติ) ? _g.d.ic_trans_detail._sum_of_cost : _g.d.ic_trans_detail._sum_of_cost_1;
            string __itemWhere = _g.g._getItemCode(_g.d.ic_resource._ic_code, itemGrid, itemBegin, itemEnd);
            string __dateWhereBegin = MyLib._myGlobal._convertDateToQuery(dateBegin);
            string __dateWhereEnd = MyLib._myGlobal._convertDateToQuery(dateEnd);
            string __docDateFieldName = _g.d.ic_trans_detail._doc_date_calc;
            string __transDetailWhere = _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._last_status + "=0 and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " and " + __docDateFieldName + " between \'" + __dateWhereBegin + "\' and \'" + __dateWhereEnd + "\'";
            string __transDetailBalanceFirstWhere = _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._last_status + "=0 and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " and " + __docDateFieldName + "<\'" + __dateWhereBegin + "\'";
            string __transDetailBalanceWhere = _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._last_status + "=0 and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " and " + __docDateFieldName + "<=\'" + __dateWhereEnd + "\'";
            string __unitStandard = "(select " + _g.d.ic_unit_use._stand_value + "/" + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table +
                " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code +
                " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._unit_standard + ")";
            string __query = "";
            if (mode == 0)
            {
                // ตามจำนวน
                __query = "select " + _g.d.ic_resource._ic_code + "," + _g.d.ic_resource._ic_name + "," + _g.d.ic_resource._ic_unit_code + "," +
                    _g.d.ic_resource._balance_qty_first + "," + _g.d.ic_resource._trans_flag_12 + "," + _g.d.ic_resource._trans_flag_48 + "," +
                    _g.d.ic_resource._trans_flag_58 + "," + _g.d.ic_resource._trans_flag_60 + "," + _g.d.ic_resource._trans_flag_66 + "," +
                    _g.d.ic_resource._trans_flag_54 + "," + _g.d.ic_resource._trans_flag_44 + "," + _g.d.ic_resource._trans_flag_16 + "," +
                    _g.d.ic_resource._trans_flag_56 + "," + _g.d.ic_resource._trans_flag_68 + "," + _g.d.ic_resource._balance_qty +
                    " from " +
                    "(select " + _g.d.ic_resource._ic_code + "," + _g.d.ic_resource._ic_name + "," + _g.d.ic_resource._ic_unit_code + "," +
                    _g.d.ic_resource._balance_qty_first + "/" + _unitRatio + " as " + _g.d.ic_resource._balance_qty_first + "," +
                    _g.d.ic_resource._balance_qty + "/" + _unitRatio + " as " + _g.d.ic_resource._balance_qty + "," +
                    _g.d.ic_resource._trans_flag_12 + "/" + _unitRatio + " as " + _g.d.ic_resource._trans_flag_12 + "," +
                    _g.d.ic_resource._trans_flag_48 + "/" + _unitRatio + " as " + _g.d.ic_resource._trans_flag_48 + "," +
                    _g.d.ic_resource._trans_flag_58 + "/" + _unitRatio + " as " + _g.d.ic_resource._trans_flag_58 + "," +
                    _g.d.ic_resource._trans_flag_60 + "/" + _unitRatio + " as " + _g.d.ic_resource._trans_flag_60 + "," +
                    _g.d.ic_resource._trans_flag_66 + "/" + _unitRatio + " as " + _g.d.ic_resource._trans_flag_66 + "," +
                    _g.d.ic_resource._trans_flag_54 + "/" + _unitRatio + " as " + _g.d.ic_resource._trans_flag_54 + "," +
                    _g.d.ic_resource._trans_flag_44 + "/" + _unitRatio + " as " + _g.d.ic_resource._trans_flag_44 + "," +
                    _g.d.ic_resource._trans_flag_16 + "/" + _unitRatio + " as " + _g.d.ic_resource._trans_flag_16 + "," +
                    _g.d.ic_resource._trans_flag_56 + "/" + _unitRatio + " as " + _g.d.ic_resource._trans_flag_56 + "," +
                    _g.d.ic_resource._trans_flag_68 + "/" + _unitRatio + " as " + _g.d.ic_resource._trans_flag_68 +
                    " from " +
                    "(select " + _g.d.ic_inventory._code + " as " + _g.d.ic_resource._ic_code + "," + _g.d.ic_inventory._name_1 + " as " + _g.d.ic_resource._ic_name + "," + _g.d.ic_inventory._average_cost + " as " + _g.d.ic_resource._average_cost_end + "," +
                    // หน่วยนับ
                    _g.d.ic_inventory._unit_standard + "||'~'||(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._unit_standard + ") as " + _g.d.ic_resource._ic_unit_code + "," +
                    __unitStandard + " as " + _unitRatio + "," +
                    // ยอดคงเหลือยกมา
                    "(select sum(" + _g.d.ic_trans_detail._calc_flag + "*((case when " + _g.d.ic_trans_detail._trans_flag + " in (14,16) and " + _g.d.ic_trans_detail._inquiry_type + "=1 then 0 else " + _g.d.ic_trans_detail._qty + " end)*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))) from " + __transDetailBalanceFirstWhere + " and " + _g._icInfoFlag._allFlagQty + ") as " + _g.d.ic_resource._balance_qty_first + "," +
                    // ซื้อ
                    "(select sum(" + _g.d.ic_trans_detail._calc_flag + "*((case when " + _g.d.ic_trans_detail._trans_flag + " in (14,16) and " + _g.d.ic_trans_detail._inquiry_type + "=1 then 0 else " + _g.d.ic_trans_detail._qty + " end)*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))) from " + __transDetailWhere + " and " + _g.d.ic_trans_detail._trans_flag + " in (12,14,310)) as " + _g.d.ic_resource._trans_flag_12 + "," +
                    // รับคืน
                    "(select sum(" + _g.d.ic_trans_detail._calc_flag + "*(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))) from " + __transDetailWhere + " and " + _g.d.ic_trans_detail._trans_flag + "=48 and " + _g.d.ic_trans_detail._inquiry_type + "<2) as " + _g.d.ic_resource._trans_flag_48 + "," +
                    // รับคืนจากการเบิก
                    "(select sum(" + _g.d.ic_trans_detail._calc_flag + "*(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))) from " + __transDetailWhere + " and " + _g.d.ic_trans_detail._trans_flag + "=58) as " + _g.d.ic_resource._trans_flag_58 + "," +
                    // รับสำเร็จรูป
                    "(select sum(" + _g.d.ic_trans_detail._calc_flag + "*(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))) from " + __transDetailWhere + " and " + _g.d.ic_trans_detail._trans_flag + "=60) as " + _g.d.ic_resource._trans_flag_60 + "," +
                    // ปรับปรุงเพิ่ม
                    "(select sum(" + _g.d.ic_trans_detail._calc_flag + "*(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))) from " + __transDetailWhere + " and " + _g.d.ic_trans_detail._qty + ">=0 and " + _g.d.ic_trans_detail._trans_flag + "=66) as " + _g.d.ic_resource._trans_flag_66 + "," +
                    // ยอดยกมาต้นปี
                    "(select sum(" + _g.d.ic_trans_detail._calc_flag + "*(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))) from " + __transDetailWhere + " and " + _g.d.ic_trans_detail._trans_flag + "=54) as " + _g.d.ic_resource._trans_flag_54 + "," +
                    // ขาย
                    "(select -1*sum(" + _g.d.ic_trans_detail._calc_flag + "*(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))) from " + __transDetailWhere + " and " + _g.d.ic_trans_detail._trans_flag + " in (44,46) and not(" + _g.d.ic_trans_detail._trans_flag + "=46 and (" + _g.d.ic_trans_detail._inquiry_type + " in (1,2)) or (" + _g.d.ic_trans_detail._trans_flag + "=48 and " + _g.d.ic_trans_detail._inquiry_type + ">1))) as " + _g.d.ic_resource._trans_flag_44 + "," +
                    // ส่งคืน
                    "(select -1*sum(" + _g.d.ic_trans_detail._calc_flag + "*((case when " + _g.d.ic_trans_detail._trans_flag + " in (14,16,311) and " + _g.d.ic_trans_detail._inquiry_type + "=1 then 0 else " + _g.d.ic_trans_detail._qty + " end)*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))) from " + __transDetailWhere + " and " + _g.d.ic_trans_detail._trans_flag + " in (16,311) ) as " + _g.d.ic_resource._trans_flag_16 + "," +
                    // เบิก
                    "(select -1*sum(" + _g.d.ic_trans_detail._calc_flag + "*(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))) from " + __transDetailWhere + " and " + _g.d.ic_trans_detail._trans_flag + "=56) as " + _g.d.ic_resource._trans_flag_56 + "," +
                    // ปรับปรุงลด
                    "(select -1*sum(" + _g.d.ic_trans_detail._calc_flag + "*(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))) from " + __transDetailWhere + " and " + _g.d.ic_trans_detail._qty + "<=0 and " + _g.d.ic_trans_detail._trans_flag + "=66) as " + _g.d.ic_resource._trans_flag_68 + "," +
                    // ยอดคงเหลือยกไป
                    "(select sum(" + _g.d.ic_trans_detail._calc_flag + "*((case when " + _g.d.ic_trans_detail._trans_flag + " in (14,16) and " + _g.d.ic_trans_detail._inquiry_type + "=1 then 0 else " + _g.d.ic_trans_detail._qty + " end)*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))) from " + __transDetailBalanceWhere + " and " + _g._icInfoFlag._allFlagQty + ") as " + _g.d.ic_resource._balance_qty +
                    " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._item_type + "<>5) as temp1 ";
            }
            else
            {
                // ตามมูลค่า
                __query = "select " + _g.d.ic_resource._ic_code + "," + _g.d.ic_resource._ic_name + "," + _g.d.ic_resource._ic_unit_code + "," +
                    _g.d.ic_resource._balance_qty_first + "," + _g.d.ic_resource._trans_flag_12 + "," + _g.d.ic_resource._trans_flag_48 + "," +
                    _g.d.ic_resource._trans_flag_58 + "," + _g.d.ic_resource._trans_flag_60 + "," + _g.d.ic_resource._trans_flag_66 + "," +
                    _g.d.ic_resource._trans_flag_54 + "," + _g.d.ic_resource._trans_flag_44 + "," + _g.d.ic_resource._trans_flag_16 + "," +
                    _g.d.ic_resource._trans_flag_56 + "," + _g.d.ic_resource._trans_flag_68 + "," + _g.d.ic_resource._balance_qty +
                    " from " +
                    "(select " + _g.d.ic_resource._ic_code + "," + _g.d.ic_resource._ic_name + "," + _g.d.ic_resource._ic_unit_code + "," +
                    _g.d.ic_resource._balance_qty_first + "," +
                    _g.d.ic_resource._balance_qty + "," +
                    _g.d.ic_resource._trans_flag_12 + "," +
                    _g.d.ic_resource._trans_flag_48 + "," +
                    _g.d.ic_resource._trans_flag_58 + "," +
                    _g.d.ic_resource._trans_flag_60 + "," +
                    _g.d.ic_resource._trans_flag_66 + "," +
                    _g.d.ic_resource._trans_flag_54 + "," +
                    _g.d.ic_resource._trans_flag_44 + "," +
                    _g.d.ic_resource._trans_flag_16 + "," +
                    _g.d.ic_resource._trans_flag_56 + "," +
                    _g.d.ic_resource._trans_flag_68 +
                    " from " +
                    "(select " + _g.d.ic_inventory._code + " as " + _g.d.ic_resource._ic_code + "," + _g.d.ic_inventory._name_1 + " as " + _g.d.ic_resource._ic_name + "," + _g.d.ic_inventory._average_cost + " as " + _g.d.ic_resource._average_cost_end + "," +
                    // หน่วยนับ
                    _g.d.ic_inventory._unit_standard + "||'~'||(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._unit_standard + ") as " + _g.d.ic_resource._ic_unit_code + "," +
                    __unitStandard + " as " + _unitRatio + "," +
                    // ยอดคงเหลือยกมา
                    "(select sum(" + _g.d.ic_trans_detail._calc_flag + "*" + __fieldSumOfCost + ") from " + __transDetailBalanceFirstWhere + " and " + _g._icInfoFlag._allFlagAmount + ") as " + _g.d.ic_resource._balance_qty_first + "," +
                    // ซื้อ
                    "(select sum(" + _g.d.ic_trans_detail._calc_flag + "*((case when " + _g.d.ic_trans_detail._trans_flag + " in (14,16) and " + _g.d.ic_trans_detail._inquiry_type + "=1 then 0 else " + __fieldSumOfCost + " end))) from " + __transDetailWhere + " and " + _g.d.ic_trans_detail._trans_flag + " in (12,14)) as " + _g.d.ic_resource._trans_flag_12 + "," +
                    // รับคืน
                    "(select sum(" + _g.d.ic_trans_detail._calc_flag + "*(" + __fieldSumOfCost + ")) from " + __transDetailWhere + " and " + _g.d.ic_trans_detail._trans_flag + "=48) as " + _g.d.ic_resource._trans_flag_48 + "," +
                    // รับคืนจากการเบิก
                    "(select sum(" + _g.d.ic_trans_detail._calc_flag + "*(" + __fieldSumOfCost + ")) from " + __transDetailWhere + " and " + _g.d.ic_trans_detail._trans_flag + "=58) as " + _g.d.ic_resource._trans_flag_58 + "," +
                    // รับสำเร็จรูป
                    "(select sum(" + _g.d.ic_trans_detail._calc_flag + "*(" + __fieldSumOfCost + ")) from " + __transDetailWhere + " and " + _g.d.ic_trans_detail._trans_flag + "=60) as " + _g.d.ic_resource._trans_flag_60 + "," +
                    // ปรับปรุงเพิ่ม
                    "(select sum(" + _g.d.ic_trans_detail._calc_flag + "*(" + __fieldSumOfCost + ")) from " + __transDetailWhere + " and " + _g.d.ic_trans_detail._qty + ">=0 and " + _g.d.ic_trans_detail._trans_flag + "=66) as " + _g.d.ic_resource._trans_flag_66 + "," +
                    // ยอดยกมาต้นปี
                    "(select sum(" + _g.d.ic_trans_detail._calc_flag + "*(" + __fieldSumOfCost + ")) from " + __transDetailWhere + " and " + _g.d.ic_trans_detail._trans_flag + "=54) as " + _g.d.ic_resource._trans_flag_54 + "," +
                    // ขาย
                    "(select -1*sum(" + _g.d.ic_trans_detail._calc_flag + "*(" + __fieldSumOfCost + ")) from " + __transDetailWhere + " and " + _g.d.ic_trans_detail._trans_flag + " in (44,46) and not(" + _g.d.ic_trans_detail._trans_flag + "=46 and (" + _g.d.ic_trans_detail._inquiry_type + " in (1,2)) or (" + _g.d.ic_trans_detail._trans_flag + "=48 and " + _g.d.ic_trans_detail._inquiry_type + ">1))) as " + _g.d.ic_resource._trans_flag_44 + "," +
                    // ส่งคืน
                    "(select -1*sum(" + _g.d.ic_trans_detail._calc_flag + "*" + __fieldSumOfCost + ") from " + __transDetailWhere + " and " + _g.d.ic_trans_detail._trans_flag + "=16) as " + _g.d.ic_resource._trans_flag_16 + "," +
                    // เบิก
                    "(select -1*sum(" + _g.d.ic_trans_detail._calc_flag + "*(" + __fieldSumOfCost + ")) from " + __transDetailWhere + " and " + _g.d.ic_trans_detail._trans_flag + "=56) as " + _g.d.ic_resource._trans_flag_56 + "," +
                    // ปรับปรุงลด
                    "(select -1*sum(" + _g.d.ic_trans_detail._calc_flag + "*(" + __fieldSumOfCost + ")) from " + __transDetailWhere + " and " + _g.d.ic_trans_detail._qty + "<=0 and " + _g.d.ic_trans_detail._trans_flag + "=66) as " + _g.d.ic_resource._trans_flag_68 + "," +
                    // ยอดคงเหลือยกไป
                    "(select sum(" + _g.d.ic_trans_detail._calc_flag + "*" + __fieldSumOfCost + ") from " + __transDetailBalanceWhere + " and " + _g._icInfoFlag._allFlagAmount + ") as " + _g.d.ic_resource._balance_qty +
                    " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._item_type + "<>5) as temp1 ";
            }
            __query = __query + " where " + __itemWhere + ((__itemWhere.Length == 0) ? "" : " and ") + " (";
            if (movementOnly == false)
            {
                __query = __query + _g.d.ic_resource._balance_qty_first + "<>0 or " + _g.d.ic_resource._balance_qty + "<>0 or ";
            }
            __query = __query + _g.d.ic_resource._trans_flag_12 + "<>0 or " + _g.d.ic_resource._trans_flag_48 + "<>0 or " +
             _g.d.ic_resource._trans_flag_58 + "<>0 or " + _g.d.ic_resource._trans_flag_60 + "<>0 or " + _g.d.ic_resource._trans_flag_66 + "<>0 or " +
             _g.d.ic_resource._trans_flag_54 + "<>0 or " + _g.d.ic_resource._trans_flag_44 + "<>0 or " + _g.d.ic_resource._trans_flag_16 + "<>0 or " +
             _g.d.ic_resource._trans_flag_56 + "<>0 or " + _g.d.ic_resource._trans_flag_68 + "<>0)) as temp2" +

             ((extraWhere.Length > 0) ? " where " + extraWhere : "")
              +
             " order by " + _g.d.ic_resource._ic_code;
            return __query;
        }

        /// <summary>
        /// สรุปการเคลื่อนไหวสินค้า
        /// </summary>
        /// <param name="mode">0=ตามปริมาณ,1=ตามมูลค่า</param>
        /// <param name="itemBegin"></param>
        /// <param name="itemEnd"></param>
        /// <param name="dateBegin">วันที่เริ่มต้น</param>
        /// <param name="dateEnd">วันที่สิ้นสุด</param>
        /// <param name="getBalanceOnly">True=คำนวณยอดคงเหลือเท่านั้น,False=คำนวณทุกอย่าง</param>
        /// <returns></returns>
        public DataTable _stkStockMovementSum(_g.g._productCostType costMode, int mode, MyLib._myGrid itemGrid, string itemBegin, string itemEnd, DateTime dateBegin, DateTime dateEnd, Boolean movementOnly, string extraWhere)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __query = _stkStockMovementSumQuery(costMode, mode, itemGrid, itemBegin, itemEnd, dateBegin, dateEnd, movementOnly, extraWhere);

            return __myFrameWork._queryShort(__query).Tables[0];
        }

        /// <summary>
        /// รายการเคลื่อนไหว Serial
        /// </summary>
        /// <returns></returns>
        public DataTable _serialMovement(DateTime dateBegin, DateTime dateEnd, string serialBegin, string serialEnd, string itemBegin, string itemEnd)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __query = _serialMovementQuery(); //this._stkStockNoMovementQuery(itemGrid, itemBegin, itemEnd, dayFrom, dayTo, dateEnd);
            return __myFrameWork._queryShort(__query).Tables[0];
        }

        public string _serialMovementQuery()
        {
            // จาก สินค้า ถึงสินค้า 

            return "";
        }

        public enum _stockBalanceType
        {
            ยอดคงเหลือตามสินค้า,
            ยอดคงเหลือตามคลัง,
            ยอดคงเหลือตามที่เก็บ,
            ยอดคงเหลือตามLOT,
            ยอดคงเหลือตามLOT_ที่เก็บ,
            ยอดคงเหลือตามLOT_ที่เก็บ_เรียงตามเอกสาร_IMEX
        }


        public void _checkICPurchasePointAlert()
        {
            // check ถึงจุดสั่งซื้อก่อน หากมี ก็ popup รายการสินค้าขึ้นมาเลย

            string __endDate = DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            try
            {
                StringBuilder __query = new StringBuilder();
                __query.Append("select ic_code, ic_name, ic_unit_code, (balance_qty/unit_standard_stand_value/unit_standard_divide_value) as balance_qty, purchase_point, minimum_qty, maximum_qty from (");

                __query.Append(" select ic_code, ic_name, ic_unit_code, balance_qty ");// (balance_qty * (select stand_value/divide_value from ic_unit_use where ic_unit_use.ic_code=temp1.ic_code and ic_unit_use.code = (select unit_standard from ic_inventory where ic_inventory.code = temp1.ic_code))) as balance_qty ");

                __query.Append(" ,(select unit_standard_stand_value from ic_inventory where ic_inventory.code = temp1.ic_code) as unit_standard_stand_value ");
                __query.Append(" , (select unit_standard_divide_value from ic_inventory where ic_inventory.code = temp1.ic_code) as unit_standard_divide_value ");
                __query.Append(", (select purchase_point from ic_inventory_detail where ic_inventory_detail.ic_code = temp1.ic_code) as purchase_point ");
                __query.Append(", (select minimum_qty from ic_inventory_detail where ic_inventory_detail.ic_code = temp1.ic_code) as minimum_qty ");
                __query.Append(" , (select maximum_qty from ic_inventory_detail where ic_inventory_detail.ic_code = temp1.ic_code) as maximum_qty ");
                __query.Append(" , (select accrued_in_qty from ic_inventory where ic_inventory.code = temp1.ic_code) as purchase_balance_qty ");
                __query.Append(" , (select accrued_out_qty from ic_inventory where ic_inventory.code = temp1.ic_code) as accrued_out_qty ");
                __query.Append(" , (select book_out_qty from ic_inventory where ic_inventory.code = temp1.ic_code) as book_out_qty ");
                __query.Append(" from sml_ic_function_stock_balance('" + __endDate + "', '')  as temp1 ");
                __query.Append(") as temp2 ");
                __query.Append(" where purchase_point != 0 and balance_qty < purchase_point order by ic_code ");

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataSet __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query.ToString());
                if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
                {
                    // load to grid
                    SMLERPControl._ic._icPurchasePointForm __alert = new _ic._icPurchasePointForm();
                    __alert._gridPurchasePoint._loadFromDataTable(__result.Tables[0]);
                    __alert.ShowDialog(MyLib._myGlobal._mainForm);
                }
            }
            catch
            {

            }
        }

        public void _checkICExpireAlert()
        {
            string __endDate = DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            try
            {
                StringBuilder __query = new StringBuilder();
                __query.Append("select doc_date,doc_no, ic_code, ic_name,unit_code,cust_code,cust_name,expire_qty,expire_date,balance_qty,balance_unit_code,(select name_1 from ic_unit where xx.unit_code=ic_unit.code) as unit_name  from ");

                __query.Append(" stock_expire (\'\', '" + __endDate + "', 0) as xx ");

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataSet __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query.ToString());
                if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
                {
                    // load to grid
                    SMLERPControl._alertForm __alert = new _alertForm();

                    // build
                    string __formatNumberQty = _g.g._getFormatNumberStr(1);

                    __alert.Text = "เตือนสินค้าหมดอายุ";
                    __alert._grid._isEdit = false;
                    __alert._grid._table_name = _g.d.ic_resource._table;
                    __alert._grid._addColumn(_g.d.ic_resource._doc_date, 4, 20, 20);
                    __alert._grid._addColumn(_g.d.ic_resource._doc_no, 1, 20, 20);

                    __alert._grid._addColumn(_g.d.ic_resource._ic_code, 1, 20, 20);
                    __alert._grid._addColumn(_g.d.ic_resource._ic_name, 1, 30, 30);
                    __alert._grid._addColumn(_g.d.ic_resource._unit_code, 1, 10, 10);

                    __alert._grid._addColumn(_g.d.ic_resource._expire_qty, 3, 20, 20, true, false, true, false, __formatNumberQty);
                    __alert._grid._addColumn(_g.d.ic_resource._expire_date, 4, 20, 20);

                    __alert._grid._addColumn(_g.d.ic_resource._balance_qty, 3, 20, 20, true, false, true, false, __formatNumberQty);
                    __alert._grid._addColumn("balance_unit_code", 1, 20, 20, false, false, true, false, "", "", "", _g.d.ic_resource._unit_code);

                    __alert._grid._loadFromDataTable(__result.Tables[0]);
                    __alert.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                    __alert.ShowDialog(MyLib._myGlobal._mainForm);
                }
            }
            catch
            {

            }
        }
    }
}
