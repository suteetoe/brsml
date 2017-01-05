using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SMLInventoryControl
{
    public class _findPurchasePriceClass
    {
        public _icTransItemGridControl._priceStruct _findPurchasePrice(string itemCode, string unitCode, decimal qty, string apCode, string docDate, _g.g._vatTypeEnum vatType, decimal vatRate, int sale_type)
        {
            string __roworderFieldName = "roworder";

            _icTransItemGridControl._priceStruct __result = new _icTransItemGridControl._priceStruct();
            __result._mode = -1;
            __result._price = 0;
            __result._type = -1;
            __result._roworder = -1;
            __result._user_approve = "";
            __result._foundByCondition = false;
            __result._foundPrice = false;
            __result._price1 = 0M;
            __result._price2 = 0M;

            // start process

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __today = docDate;
            string __todayCondition = "(\'" + __today + "\' >= " + _g.d.ic_inventory_purchase_price._from_date + " and \'" + __today + "\' <= " + _g.d.ic_inventory_purchase_price._to_date + ")";
            string __qtyCondition = "(" + qty.ToString() + " >= " + _g.d.ic_inventory_purchase_price._from_qty + " and " + qty.ToString() + " <= " + _g.d.ic_inventory_purchase_price._to_qty + ")";
            string __fieldPrice = (vatType == _g.g._vatTypeEnum.ภาษีแยกนอก) ? _g.d.ic_inventory_purchase_price._sale_price1 : _g.d.ic_inventory_purchase_price._sale_price2;
            string __fieldPrice1 = _g.d.ic_inventory_purchase_price._sale_price1;
            string __fieldPrice2 = _g.d.ic_inventory_purchase_price._sale_price2;

            string __saleFilterType = (sale_type == 0 || sale_type == 2) ? "2" : "1";

            string __saleTypeCondition = " and " + _g.d.ic_inventory_purchase_price._sale_type + " in (0," + __saleFilterType + ") ";
            string __transportTypeCondition = ""; //: " and " + _g.d.ic_inventory_price._transport_type + " in (0," + __SaleFiltertype + ") ";
            // 0=ราคาตามลูกค้า
            // 1=ราคาตามกลุ่มลูกค้า
            // 2=ราคาขายทั่วไป
            // 3=ราคามาตรฐาน
            // 4,5=ราคาตามสูตร
            // 6=ราคาตาม Barcode
            // ดึงราคาขายล่าสุด (Option)
            // ดึงราคาขายเฉลียน (Option)
            StringBuilder __query = new StringBuilder();
            __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
            // เรียงตาม pricemode desc เพราะจะได้ดึงราคาขายทั่วไปก่อน ถ้าไม่เจอ ก็ค่อยไปเอาราคามาตรฐาน
            // 0=ราคาตามลูกค้า
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + __roworderFieldName + "," + _g.d.ic_inventory_purchase_price._sale_price1 + "," + _g.d.ic_inventory_purchase_price._sale_price2 + " from " + _g.d.ic_inventory_purchase_price._table + " where " + _g.d.ic_inventory_purchase_price._ic_code + "=\'" + itemCode + "\' and " + _g.d.ic_inventory_purchase_price._unit_code + "=\'" + unitCode + "\' and " + _g.d.ic_inventory_purchase_price._supplier_code + "=\'" + apCode + "\' and " + _g.d.ic_inventory_purchase_price._price_type + "=3 and " + __todayCondition + " and " + __qtyCondition + __saleTypeCondition + __transportTypeCondition + " order by " + _g.d.ic_inventory_price._sale_type + " desc ," + _g.d.ic_inventory_price._transport_type + " desc"));
            // 1=ราคาซื้อทั่วไป
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + __roworderFieldName + "," + _g.d.ic_inventory_purchase_price._sale_price1 + "," + _g.d.ic_inventory_purchase_price._sale_price2 + " from " + _g.d.ic_inventory_purchase_price._table + " where " + _g.d.ic_inventory_purchase_price._ic_code + "=\'" + itemCode + "\' and " + _g.d.ic_inventory_purchase_price._unit_code + "=\'" + unitCode + "\' and " + _g.d.ic_inventory_purchase_price._price_type + "=1 and " + __todayCondition + " and " + __qtyCondition + __saleTypeCondition + __transportTypeCondition + " order by " + _g.d.ic_inventory_price._sale_type + " desc ," + _g.d.ic_inventory_price._transport_type + " desc"));

            //
            int __lastPrice = -1;
            __lastPrice = 7;
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select (" + _g.d.ic_trans_detail._sum_amount_exclude_vat + "/" + _g.d.ic_trans_detail._qty + ") as " + _g.d.ic_trans_detail._price_exclude_vat + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._cust_code + "=\'" + apCode + "\' and " + _g.d.ic_trans_detail._item_code + "=\'" + itemCode + "\' and " + _g.d.ic_trans_detail._unit_code + "=\'" + unitCode + "\' and " + _g.d.ic_trans_detail._last_status + "=0 and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ) + " and " + _g.d.ic_trans_detail._price_exclude_vat + ">0 and " + _g.d.ic_trans_detail._qty + " <> 0  order by " + _g.d.ic_trans_detail._doc_date + " desc," + _g.d.ic_trans_detail._doc_time + " desc limit 1"));

            //
            __query.Append("</node>");
            string __queryStr = __query.ToString();
            ArrayList __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryStr);
            {
                // หาตามเจ้าหนี้
                DataTable __itemSetTable = ((DataSet)__dataResult[0]).Tables[0];
                if (__itemSetTable.Rows.Count > 0)
                {
                    __result._price = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][__fieldPrice].ToString());
                    __result._foundPrice = true;
                }
            }
            if (__result._foundPrice == false)
            {
                // ราคาซื้อทั่วไป
                DataTable __itemSetTable = ((DataSet)__dataResult[1]).Tables[0];
                if (__itemSetTable.Rows.Count > 0)
                {
                    __result._price = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][__fieldPrice].ToString());
                    __result._foundPrice = true;
                }
            }

            // ซื้อล่าสุด
            if (__result._foundPrice == false)
            {
                DataTable __itemSetTable = ((DataSet)__dataResult[2]).Tables[0];
                if (__itemSetTable.Rows.Count > 0)
                {
                    __result._price = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][_g.d.ic_trans_detail._price_exclude_vat].ToString());
                    if (vatType == _g.g._vatTypeEnum.ภาษีรวมใน)
                    {
                        __result._price = __result._price + (__result._price * (vatRate / 100M));
                    }
                    __result._foundPrice = true;
                }
            }
            return __result;
        }
    }
}
