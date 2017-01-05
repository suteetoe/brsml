using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SMLInventoryControl
{
    public class _findPriceClass
    {
        public _icTransItemGridControl._priceStruct _findPriceByItem(string itemCode, string barcode, string unitCode, decimal qty, string custCode, string memberCode, Boolean approve, string getUserApprove, string docDate, _g.g._vatTypeEnum vatType)
        {
            return this._findPriceByItem(itemCode, barcode, unitCode, qty, custCode, memberCode, approve, getUserApprove, docDate, vatType, 0, 0, 0);
        }

        public _icTransItemGridControl._priceStruct _findPriceByItem(string itemCode, string barcode, string unitCode, decimal qty, string custCode, string memberCode, Boolean approve, string getUserApprove, string docDate, _g.g._vatTypeEnum vatType, decimal vatRate)
        {
            return this._findPriceByItem(itemCode, barcode, unitCode, qty, custCode, memberCode, approve, getUserApprove, docDate, vatType, vatRate, 0, 0);
        }

        public _icTransItemGridControl._priceStruct _findPriceByItem(string itemCode, string barcode, string unitCode, decimal qty, string custCode, string memberCode, Boolean approve, string getUserApprove, string docDate, _g.g._vatTypeEnum vatType, decimal vatRate, int sale_type, int transport_type)
        {
            int __queryIndex = 0;

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
            //
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __today = docDate;
            string __todayCondition = "(\'" + __today + "\' >= " + _g.d.ic_inventory_price._from_date + " and \'" + __today + "\' <= " + _g.d.ic_inventory_price._to_date + ")";
            string __qtyCondition = "(" + qty.ToString() + " >= " + _g.d.ic_inventory_price._from_qty + " and " + qty.ToString() + " <= " + _g.d.ic_inventory_price._to_qty + ")";
            string __fieldPrice = (vatType == _g.g._vatTypeEnum.ภาษีแยกนอก) ? _g.d.ic_inventory_price._sale_price1 : _g.d.ic_inventory_price._sale_price2;
            string __fieldPrice1 = _g.d.ic_inventory_price._sale_price1;
            string __fieldPrice2 = _g.d.ic_inventory_price._sale_price2;

            string __saleFilterType = (sale_type == 0 || sale_type == 2) ? "2" : "1";

            string __saleTypeCondition = " and " + _g.d.ic_inventory_price._sale_type + " in (0," + __saleFilterType + ") ";
            string __transportTypeCondition = ""; //: " and " + _g.d.ic_inventory_price._transport_type + " in (0," + __SaleFiltertype + ") ";
            // 0=ราคาตามลูกค้า
            // 1=ราคาตามกลุ่มลูกค้า
            // 2=ราคาขายทั่วไป
            // 3=ราคามาตรฐาน
            // 4,5=ราคาตามสูตร
            // 6=ราคาตาม Barcode
            // ดึงราคาขายล่าสุด (Option)
            // ดึงราคาขายเฉลียน (Option)
            string __groupSub = " ( " + _g.d.ic_inventory_price._cust_group_2 + " = (select {0} from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._ar_code + " = '" + custCode + "')) ";
            string __groupSubWhere = " and ("
                + string.Format(__groupSub, _g.d.ar_customer_detail._group_sub_1)
                + " or " + string.Format(__groupSub, _g.d.ar_customer_detail._group_sub_1)
                + " or " + string.Format(__groupSub, _g.d.ar_customer_detail._group_sub_3)
                + " or " + string.Format(__groupSub, _g.d.ar_customer_detail._group_sub_4)
                + " or ( coalesce(" + _g.d.ic_inventory_price._cust_group_2 + ", '') = '') " +
                ")";

            StringBuilder __query = new StringBuilder();
            __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
            // เรียงตาม pricemode desc เพราะจะได้ดึงราคาขายทั่วไปก่อน ถ้าไม่เจอ ก็ค่อยไปเอาราคามาตรฐาน
            // 0=ราคาตามลูกค้า
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + __roworderFieldName + "," + _g.d.ic_inventory_price._sale_price1 + "," + _g.d.ic_inventory_price._sale_price2 + "," + _g.d.ic_inventory_price._price_mode + " from " + _g.d.ic_inventory_price._table + " where " + _g.d.ic_inventory_price._ic_code + "=\'" + itemCode + "\' and " + _g.d.ic_inventory_price._unit_code + "=\'" + unitCode + "\' and " + _g.d.ic_inventory_price._cust_code + "=\'" + custCode + "\' and " + _g.d.ic_inventory_price._price_type + "=3 and " + __todayCondition + " and " + __qtyCondition + __saleTypeCondition + __transportTypeCondition + " order by " + _g.d.ic_inventory_price._price_mode + " desc ," + _g.d.ic_inventory_price._sale_type + " desc ," + _g.d.ic_inventory_price._transport_type + " desc"));
            __queryIndex++;

            // 1=ราคาตามกลุ่มลูกค้า
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + __roworderFieldName + "," + _g.d.ic_inventory_price._sale_price1 + "," + _g.d.ic_inventory_price._sale_price2 + "," + _g.d.ic_inventory_price._price_mode + " from " + _g.d.ic_inventory_price._table + " where " + _g.d.ic_inventory_price._ic_code + "=\'" + itemCode + "\' and " + _g.d.ic_inventory_price._unit_code + "=\'" + unitCode + "\' and " + _g.d.ic_inventory_price._cust_group_1 + "=(select " + _g.d.ar_customer_detail._group_main + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._ar_code + "=\'" + custCode + "\') " + __groupSubWhere + " and " + _g.d.ic_inventory_price._price_type + "=2 and " + __todayCondition + " and " + __qtyCondition + __saleTypeCondition + __transportTypeCondition + " order by " + _g.d.ic_inventory_price._price_mode + " desc," + _g.d.ic_inventory_price._sale_type + " desc ," + _g.d.ic_inventory_price._transport_type + " desc, " + _g.d.ic_inventory_price._cust_group_2));
            __queryIndex++;

            // 2=ราคาขายทั่วไป
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + __roworderFieldName + "," + _g.d.ic_inventory_price._sale_price1 + "," + _g.d.ic_inventory_price._sale_price2 + "," + _g.d.ic_inventory_price._price_mode + " from " + _g.d.ic_inventory_price._table + " where " + _g.d.ic_inventory_price._ic_code + "=\'" + itemCode + "\' and " + _g.d.ic_inventory_price._unit_code + "=\'" + unitCode + "\' and " + _g.d.ic_inventory_price._price_type + "=1 and " + __todayCondition + " and " + __qtyCondition + __saleTypeCondition + __transportTypeCondition + "  and " + _g.d.ic_inventory_price._price_mode + "=1" + " order by " + _g.d.ic_inventory_price._price_mode + " desc ," + _g.d.ic_inventory_price._sale_type + " desc ," + _g.d.ic_inventory_price._transport_type + " desc"));
            __queryIndex++;

            // 3=ราคามาตรฐาน
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + __roworderFieldName + "," + _g.d.ic_inventory_price._sale_price1 + "," + _g.d.ic_inventory_price._sale_price2 + "," + _g.d.ic_inventory_price._price_mode + " from " + _g.d.ic_inventory_price._table + " where " + _g.d.ic_inventory_price._ic_code + "=\'" + itemCode + "\' and " + _g.d.ic_inventory_price._unit_code + "=\'" + unitCode + "\' and " + _g.d.ic_inventory_price._price_type + "=1 and " + __todayCondition + " and " + __qtyCondition + __saleTypeCondition + __transportTypeCondition + " and " + _g.d.ic_inventory_price._price_mode + "=0" + " order by " + _g.d.ic_inventory_price._price_mode + " desc ," + _g.d.ic_inventory_price._sale_type + " desc ," + _g.d.ic_inventory_price._transport_type + " desc"));
            __queryIndex++;

            // 4=ราคาตามสูตร (ค้นหาระดับราคาลูกค้า)
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_customer._price_level + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + custCode + "\'"));
            __queryIndex++;

            // 5=ราคาตามสูตร
            string __saleTypeStr = "0";
            switch (sale_type)
            {
                case 0: // ขายเชื่อ
                    __saleTypeStr = __saleTypeStr + ",2";
                    break;
                case 1:  // ขายสด
                    __saleTypeStr = __saleTypeStr + ",1";
                    break;
            }

            string __taxTypestr = "0";
            switch(vatType)
            {
                case _g.g._vatTypeEnum.ภาษีแยกนอก:
                    __taxTypestr = __taxTypestr + ",1";
                    break;
                case _g.g._vatTypeEnum.ภาษีรวมใน:
                    __taxTypestr = __taxTypestr + ",2";
                    break;
                case _g.g._vatTypeEnum.ยกเว้นภาษี:
                    __taxTypestr = __taxTypestr + ",3";
                    break;

            }
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory_price_formula._table + " where " + _g.d.ic_inventory_price_formula._ic_code + "=\'" + itemCode + "\' and " + _g.d.ic_inventory_price_formula._unit_code + "=\'" + unitCode + "\' and " + _g.d.ic_inventory_price_formula._sale_type + " in (" + __saleTypeStr + ")  and coalesce(" + _g.d.ic_inventory_price_formula._tax_type + ", 0) in (" + __taxTypestr + ") order by " + _g.d.ic_inventory_price_formula._sale_type + " desc "));
            __queryIndex++;

            // 6=ราคาตาม Barcode
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory_barcode._price + " from " + _g.d.ic_inventory_barcode._table + " where " + _g.d.ic_inventory_barcode._barcode + "=\'" + barcode + "\'"));
            __queryIndex++;
            //
            int __lastPrice = -1;
            switch (_g.g._companyProfile._get_last_price_type)
            {
                case 1: // ราคาขายล่าสุด
                    {
                        __lastPrice = 7;
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans_detail._price_exclude_vat + "," + _g.d.ic_trans_detail._price + ", (select vat_type from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag=ic_trans_detail.trans_flag) as " + _g.d.ic_trans._vat_type + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._cust_code + "=\'" + custCode + "\' and " + _g.d.ic_trans_detail._item_code + "=\'" + itemCode + "\' and " + _g.d.ic_trans_detail._unit_code + "=\'" + unitCode + "\' and " + _g.d.ic_trans_detail._last_status + "=0 and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + " and " + _g.d.ic_trans_detail._price_exclude_vat + ">0 order by " + _g.d.ic_trans_detail._doc_date + " desc," + _g.d.ic_trans_detail._doc_time + " desc limit 1"));
                        __queryIndex++;
                    }
                    break;
                case 2: // ราคาขายเฉลี่ย
                    {
                        // เอาราคามาหารจำนวนครั้ง
                        __lastPrice = 7;
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select sum(" + _g.d.ic_trans_detail._price_exclude_vat + ")/count(*) as " + _g.d.ic_trans_detail._price_exclude_vat + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._cust_code + "=\'" + custCode + "\' and " + _g.d.ic_trans_detail._item_code + "=\'" + itemCode + "\' and " + _g.d.ic_trans_detail._unit_code + "=\'" + unitCode + "\' and " + _g.d.ic_trans_detail._last_status + "=0 and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + " and " + _g.d.ic_trans_detail._price_exclude_vat + ">0 group by " + _g.d.ic_trans_detail._item_code + "=\'" + itemCode + "\' and " + _g.d.ic_trans_detail._unit_code + "=\'" + unitCode + "\'"));
                        __queryIndex++;
                    }
                    break;
            }
            //

            int __discountNormalTableNumber = -1;
            int __discountGroupTableNumber = -1;
            int __discountCustomerTableNumber = -1;

            // ลดตามลูกค้า
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + __roworderFieldName + "," + _g.d.ic_inventory_discount._discount + " from " + _g.d.ic_inventory_discount._table + " where " + _g.d.ic_inventory_discount._ic_code + "=\'" + itemCode + "\' and " + _g.d.ic_inventory_discount._unit_code + "=\'" + unitCode + "\' and " + _g.d.ic_inventory_discount._cust_code + "=\'" + custCode + "\' and " + _g.d.ic_inventory_discount._discount_type + "=2 and " + __todayCondition + " and " + __qtyCondition + __saleTypeCondition + __transportTypeCondition + " order by line_number"));
            __discountCustomerTableNumber = __queryIndex;
            __queryIndex++;

            // ลดตามกลุ่ม
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + __roworderFieldName + "," + _g.d.ic_inventory_discount._discount + " from " + _g.d.ic_inventory_discount._table + " where " + _g.d.ic_inventory_discount._ic_code + "=\'" + itemCode + "\' and " + _g.d.ic_inventory_discount._unit_code + "=\'" + unitCode + "\' and " + _g.d.ic_inventory_discount._cust_group_1 + "=(select " + _g.d.ar_customer_detail._group_main + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._ar_code + "=\'" + custCode + "\') " + __groupSubWhere + " and " + _g.d.ic_inventory_discount._discount_type + "=1 and " + __todayCondition + " and " + __qtyCondition + __saleTypeCondition + __transportTypeCondition + " order by roworder"));
            __discountGroupTableNumber = __queryIndex;
            __queryIndex++;

            // ลดทั่วไป
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + __roworderFieldName + "," + _g.d.ic_inventory_discount._discount + " from " + _g.d.ic_inventory_discount._table + " where " + _g.d.ic_inventory_discount._ic_code + "=\'" + itemCode + "\' and " + _g.d.ic_inventory_discount._unit_code + "=\'" + unitCode + "\' and " + _g.d.ic_inventory_discount._discount_type + "=0 and " + __todayCondition + " and " + __qtyCondition + __saleTypeCondition + __transportTypeCondition + " order by " + _g.d.ic_inventory_discount._line_number));
            __discountNormalTableNumber = __queryIndex;
            __queryIndex++;



            __query.Append("</node>");
            string __queryStr = __query.ToString();
            ArrayList __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryStr);
            //
            if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
            {
                // หาตามลูกค้าก่อน
                DataTable __itemDataTable = ((DataSet)__dataResult[0]).Tables[0];
                if (__itemDataTable.Rows.Count > 0)
                {
                    __result._foundPrice = true;
                    __result._price1 = MyLib._myGlobal._decimalPhase(__itemDataTable.Rows[0][__fieldPrice1].ToString());
                    __result._price2 = MyLib._myGlobal._decimalPhase(__itemDataTable.Rows[0][__fieldPrice2].ToString());
                    //
                    __result._price = MyLib._myGlobal._decimalPhase(__itemDataTable.Rows[0][__fieldPrice].ToString());
                    __result._type = 1;
                    __result._mode = MyLib._myGlobal._intPhase(__itemDataTable.Rows[0][_g.d.ic_inventory_price._price_mode].ToString());
                    __result._roworder = MyLib._myGlobal._intPhase(__itemDataTable.Rows[0][__roworderFieldName].ToString());
                    __result._foundByCondition = true;
                }
            }
            if (__result._foundByCondition == false)
            {
                // หาตามกลุ่มลูกค้า
                DataTable __itemSetTable = ((DataSet)__dataResult[1]).Tables[0];
                if (__itemSetTable.Rows.Count > 0)
                {
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        if (approve)
                        {
                            // กรณีมีการอนุมัติบรรทัดนี้แล้ว ไม่ต้องถามอีก ยกเว้นแก้ไขรหัสสินค้า,จำนวน,หน่วยนับ
                            string __getUserApprove = getUserApprove;
                            Boolean __getPriceNow = true;
                            if (_g.g._companyProfile._warning_price_3)
                            {
                                if (__getUserApprove.Length == 0)
                                {
                                    if (_g.g._companyProfile._warning_price_3)
                                    {
                                        _pricePasswordForm __pricePassword = new _pricePasswordForm(2);
                                        __pricePassword.ShowDialog();
                                        if (__pricePassword._passwordPass)
                                        {
                                            //this._cellUpdate(row, _g.d.ic_trans_detail._user_approve, __pricePassword._userCode, false);
                                            __result._user_approve = __pricePassword._userCode;
                                        }
                                        else
                                        {
                                            __getPriceNow = false;
                                        }
                                    }
                                }
                            }
                            if (__getPriceNow)
                            {
                                __result._foundPrice = true;
                                __result._price1 = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][__fieldPrice1].ToString());
                                __result._price2 = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][__fieldPrice2].ToString());
                                //
                                __result._price = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][__fieldPrice].ToString());
                                __result._mode = MyLib._myGlobal._intPhase(__itemSetTable.Rows[0][_g.d.ic_inventory_price._price_mode].ToString());
                                __result._type = 2;
                                __result._roworder = MyLib._myGlobal._intPhase(__itemSetTable.Rows[0][__roworderFieldName].ToString());
                                __result._foundByCondition = true;
                            }
                        }
                    }
                    else
                    {
                        __result._foundPrice = true;
                        __result._price1 = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][__fieldPrice1].ToString());
                        __result._price2 = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][__fieldPrice2].ToString());
                        //
                        __result._price = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][__fieldPrice].ToString());
                        __result._mode = MyLib._myGlobal._intPhase(__itemSetTable.Rows[0][_g.d.ic_inventory_price._price_mode].ToString());
                        __result._roworder = MyLib._myGlobal._intPhase(__itemSetTable.Rows[0][__roworderFieldName].ToString());
                        __result._type = 2;
                        __result._foundByCondition = true;
                    }
                }
            }
            if (__result._foundByCondition == false)
            {
                // ราคาขายทั่วไป
                {
                    // เรียงตาม pricemode เพราะจะได้ดึงราคาที่กำหนดเองก่อน ถ้าไม่เจอ ก็ค่อยไปเอาราคายืน
                    DataTable __itemSetTable = ((DataSet)__dataResult[2]).Tables[0];
                    if (__itemSetTable.Rows.Count > 0)
                    {
                        __result._foundPrice = true;
                        __result._price1 = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][__fieldPrice1].ToString());
                        __result._price2 = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][__fieldPrice2].ToString());
                        //
                        __result._price = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][__fieldPrice].ToString());
                        __result._mode = MyLib._myGlobal._intPhase(__itemSetTable.Rows[0][_g.d.ic_inventory_price._price_mode].ToString());
                        __result._roworder = MyLib._myGlobal._intPhase(__itemSetTable.Rows[0][__roworderFieldName].ToString());
                        __result._type = 3;
                        __result._foundByCondition = true;
                    }
                }
                if (__result._foundByCondition == false)
                {
                    // ราคามาตรฐาน (ยืน)
                    {
                        DataTable __itemSetTable = ((DataSet)__dataResult[3]).Tables[0];
                        if (__itemSetTable.Rows.Count > 0)
                        {
                            __result._foundPrice = true;
                            __result._price1 = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][__fieldPrice1].ToString());
                            __result._price2 = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][__fieldPrice2].ToString());
                            //
                            __result._price = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][__fieldPrice].ToString());
                            __result._mode = MyLib._myGlobal._intPhase(__itemSetTable.Rows[0][_g.d.ic_inventory_price._price_mode].ToString());
                            __result._roworder = MyLib._myGlobal._intPhase(__itemSetTable.Rows[0][__roworderFieldName].ToString());
                            __result._type = 3;
                            __result._foundByCondition = true;
                        }
                    }
                }
                if (__result._foundPrice == false)
                {
                    // ค้นหาตามสูตร
                    int __priceLevel = 0;
                    DataTable __getCustGroup = ((DataSet)__dataResult[4]).Tables[0];
                    if (__getCustGroup.Rows.Count > 0)
                    {
                        __priceLevel = (int)MyLib._myGlobal._decimalPhase(__getCustGroup.Rows[0][_g.d.ar_customer._price_level].ToString());
                    }
                    //
                    DataTable __itemSetTable = ((DataSet)__dataResult[5]).Tables[0];
                    if (__itemSetTable.Rows.Count > 0)
                    {
                        string __priceStandard = __itemSetTable.Rows[0][_g.d.ic_inventory_price_formula._price_0].ToString();
                        string __formula = __priceStandard;
                        switch (__priceLevel)
                        {
                            case 1: __formula = __itemSetTable.Rows[0][_g.d.ic_inventory_price_formula._price_1].ToString(); break;
                            case 2: __formula = __itemSetTable.Rows[0][_g.d.ic_inventory_price_formula._price_2].ToString(); break;
                            case 3: __formula = __itemSetTable.Rows[0][_g.d.ic_inventory_price_formula._price_3].ToString(); break;
                            case 4: __formula = __itemSetTable.Rows[0][_g.d.ic_inventory_price_formula._price_4].ToString(); break;
                            case 5: __formula = __itemSetTable.Rows[0][_g.d.ic_inventory_price_formula._price_5].ToString(); break;
                            case 6: __formula = __itemSetTable.Rows[0][_g.d.ic_inventory_price_formula._price_6].ToString(); break;
                            case 7: __formula = __itemSetTable.Rows[0][_g.d.ic_inventory_price_formula._price_7].ToString(); break;
                            case 8: __formula = __itemSetTable.Rows[0][_g.d.ic_inventory_price_formula._price_8].ToString(); break;
                            case 9: __formula = __itemSetTable.Rows[0][_g.d.ic_inventory_price_formula._price_9].ToString(); break;
                        }
                        MyLib._calcDiscountResultStruct __calcPrice = _g.g._calcFormulaPrice(qty, MyLib._myGlobal._decimalPhase(__priceStandard), __formula);

                        if (MyLib._myGlobal._OEMVersion == "SINGHA")
                        {
                            if (__calcPrice._newPrice != 0 || MyLib._myGlobal._decimalPhase(__priceStandard) != 0M)
                            {
                                __result._price = __calcPrice._newPrice;
                                __result._price2 = __result._price;
                                __result._discountWordFormula = __calcPrice._discountWord;
                                __result._discountWord = __calcPrice._discountWord;
                                __result._mode = 6;
                                __result._roworder = 0;
                                __result._type = 6;
                                __result._foundPrice = true;
                            }
                        }
                        else
                        {
                            __result._price = __calcPrice._newPrice;
                            __result._price2 = __result._price;
                            __result._discountWordFormula = __calcPrice._discountWord;
                            __result._discountWord = __calcPrice._discountWord;
                            __result._mode = 6;
                            __result._roworder = 0;
                            __result._type = 6;
                            __result._foundPrice = true;
                        }
                    }
                }
                if (__result._foundPrice == false && barcode.Trim().Length > 0)
                {
                    // ค้นหาตาม Barcode
                    DataTable __itemSetTable = ((DataSet)__dataResult[6]).Tables[0];
                    if (__itemSetTable.Rows.Count > 0)
                    {
                        if ((MyLib._myGlobal._OEMVersion == "SINGHA"))
                        {
                            if (MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][_g.d.ic_inventory_barcode._price].ToString()) != 0M)
                            {
                                __result._price = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][_g.d.ic_inventory_barcode._price].ToString());
                                __result._price2 = __result._price;
                                __result._mode = 5;
                                __result._roworder = 0;
                                __result._type = 5;
                                __result._foundPrice = true;
                            }
                        }
                        else
                        {
                            __result._price = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][_g.d.ic_inventory_barcode._price].ToString());
                            __result._price2 = __result._price;
                            __result._mode = 5;
                            __result._roworder = 0;
                            __result._type = 5;
                            __result._foundPrice = true;

                        }
                    }
                }
                if (__result._foundPrice == false && __lastPrice != -1)
                {
                    // ราคาขายล่าสุด/ราคาขายเฉลี่ย
                    DataTable __itemSetTable = ((DataSet)__dataResult[__lastPrice]).Tables[0];
                    if (__itemSetTable.Rows.Count > 0)
                    {
                        __result._price = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][_g.d.ic_trans_detail._price_exclude_vat].ToString());
                        if (vatType == _g.g._vatTypeEnum.ภาษีรวมใน)
                        {
                            __result._price = __result._price + (__result._price * (vatRate / 100M));
                        }

                        if ((MyLib._myGlobal._OEMVersion == "SINGHA") && vatType == _g.g._vatTypeEnum.ภาษีรวมใน && __itemSetTable.Rows[0][_g.d.ic_trans._vat_type].ToString().Equals("1"))
                        {
                            // เป็นรวมในเหมือนกัน ดึงราคาได้เลย
                            __result._price = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][_g.d.ic_trans_detail._price].ToString());

                        }
                        __result._price2 = __result._price;
                        __result._mode = 5;
                        __result._roworder = 0;
                        __result._type = 7;
                        __result._foundPrice = true;
                    }
                }
            }

            // toe ราคากลาง
            if (__result._foundPrice == true && _g.g._companyProfile._ic_price_formula_control)
            {
                DataTable __itemSetTable = ((DataSet)__dataResult[5]).Tables[0];
                if (__itemSetTable.Rows.Count > 0)
                {
                    __result._stand_price = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][_g.d.ic_inventory_price_formula._price_0].ToString());
                }
            }

            // find discount
            Boolean __foundDiscount = false;

            DataTable __discountCustomerDatatable = ((DataSet)__dataResult[__discountCustomerTableNumber]).Tables[0];
            DataTable __discountCustomerGroupDatatable = ((DataSet)__dataResult[__discountGroupTableNumber]).Tables[0];
            DataTable __discountDatatable = ((DataSet)__dataResult[__discountNormalTableNumber]).Tables[0];

            if (__discountCustomerDatatable.Rows.Count > 0)
            {
                __result._defaultDiscount = __discountCustomerDatatable.Rows[0][_g.d.ic_inventory_discount._discount].ToString();
                __foundDiscount = true;
            }

            if (__foundDiscount == false && __discountCustomerGroupDatatable.Rows.Count > 0)
            {
                __result._defaultDiscount = __discountCustomerGroupDatatable.Rows[0][_g.d.ic_inventory_discount._discount].ToString();
                __foundDiscount = true;
            }

            if (__foundDiscount == false && __discountDatatable.Rows.Count > 0)
            {
                __result._defaultDiscount = __discountDatatable.Rows[0][_g.d.ic_inventory_discount._discount].ToString();
                __foundDiscount = true;
            }


            return __result;
        }

        /// <summary>
        /// สำหรับค้นหา ราคาสินค้าของ POS ดูจาก barcode และส่วนลดตาม barcode เป็นหลัก
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="unitCode"></param>
        /// <param name="qty"></param>
        /// <param name="custCode"></param>
        /// <param name="docDate"></param>
        /// <returns></returns>
        public _icTransItemGridControl._priceStruct _findPricePOS(string itemCode, string barcode, string unitCode, decimal qty, string custCode, string memberCode, int priceNumber, bool isStaff, string posId, string defaultcustCode)
        {
            // step
            // 1. หาจากราคา barcode ก่อน
            // 1.1 ดูส่วนลดตาม barcode ด้วย
            // 2. ไม่ได้ไปหาราคาจากรหัสสินค้า (หลังร้าน)

            string __today = MyLib._myGlobal._convertDateToQuery(DateTime.Now);
            string _formatNumber1 = "#,0.00";

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

            StringBuilder __query = new StringBuilder();
            __query.Append(MyLib._myGlobal._xmlHeader + "<node>");

            string __barcodeQuery = "select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_inventory_barcode._barcode,
                _g.d.ic_inventory_barcode._ic_code, "(select " + _g.d.ic_inventory._name_1 + " from " +
                _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=" + _g.d.ic_inventory_barcode._ic_code +
                ") as " + _g.d.ic_inventory_barcode._description, "(select " + _g.d.ic_inventory_detail._have_point +
                " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." +
                _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_inventory_barcode._table + "." +
                _g.d.ic_inventory_barcode._ic_code + ") as " + _g.d.ic_inventory._have_point, "(select " + _g.d.ic_inventory._item_type +
                " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=" +
                _g.d.ic_inventory_barcode._ic_code + ") as item_type", "(select " + _g.d.ic_inventory._tax_type + " from " +
                _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=" + _g.d.ic_inventory_barcode._ic_code +
                ") as tax_type", _g.d.ic_inventory_barcode._unit_code, "(select " + _g.d.ic_unit_use._stand_value +
                " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code +
                "=" + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._ic_code + " and " +
                _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=" + _g.d.ic_inventory_barcode._table + "." +
                _g.d.ic_inventory_barcode._unit_code + ") as stand_value", "(select " + _g.d.ic_unit_use._divide_value +
                " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code +
                "=" + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._ic_code + " and " +
                _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=" + _g.d.ic_inventory_barcode._table + "." +
                _g.d.ic_inventory_barcode._unit_code + ") as divide_value", _g.d.ic_inventory_barcode._price, _g.d.ic_inventory_barcode._price_2, _g.d.ic_inventory_barcode._price_3, _g.d.ic_inventory_barcode._price_4, _g.d.ic_inventory_barcode._price_member, _g.d.ic_inventory_barcode._price_member_2, _g.d.ic_inventory_barcode._price_member_3, _g.d.ic_inventory_barcode._price_member_4) + ", " + _g.d.ic_inventory_barcode._hidden_text + " from " +
                _g.d.ic_inventory_barcode._table + " where " + _g.d.ic_inventory_barcode._barcode +
                "=\'" + barcode + "\'";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__barcodeQuery));

            /*
            // ดึงราคาตาม barcode
            for (int __loop = 1; __loop <= 5; __loop++)
            {
                string __dateCompare = "\'" + __today + "\'";
                string __queryBarcodePrice = "select " + _g.d.ic_inventory_barcode_price._discount_word + " from " +
                    _g.d.ic_inventory_barcode_price._table + " where " + _g.d.ic_inventory_barcode_price._barcode + "=\'" +
                    barcode + "\' and " + _g.d.ic_inventory_barcode_price._discount_type + "=" + __loop.ToString() +
                    " and " + __dateCompare + ">=" + _g.d.ic_inventory_barcode_price._date_begin + " and " + __dateCompare +
                    "<=" + _g.d.ic_inventory_barcode_price._date_end + " and " + _g.d.ic_inventory_barcode_price._status + "=1 and " +
                    qty + " >= " + _g.d.ic_inventory_barcode_price._qty_begin + "  and " + qty + " <= " + _g.d.ic_inventory_barcode_price._qty_end +
                    "  order by " + _g.d.ic_inventory_barcode_price._priority;
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryBarcodePrice));
            }
             * */
            string __dateCompare = "\'" + __today + "\'";

            string __queryBarcodePrice = "select " + _g.d.ic_inventory_barcode_price._discount_word + "," + _g.d.ic_inventory_barcode_price._discount_type + " from " +
                _g.d.ic_inventory_barcode_price._table + " where " + _g.d.ic_inventory_barcode_price._barcode + "=\'" +
                barcode + "\' and " + __dateCompare + " between " + _g.d.ic_inventory_barcode_price._date_begin + " and " + _g.d.ic_inventory_barcode_price._date_end + " and " + _g.d.ic_inventory_barcode_price._status + "=1 and " +
                qty + " between " + _g.d.ic_inventory_barcode_price._qty_begin + "  and " + _g.d.ic_inventory_barcode_price._qty_end +
                // toe ส่วนลดตามสาขา
                " and ((coalesce(" + _g.d.ic_inventory_barcode_price._lock_discount + ", 0)=0) or (coalesce(" + _g.d.ic_inventory_barcode_price._lock_discount + ", 0)=1 and " + _g.d.ic_inventory_barcode_price._lock_code_list + " like '%" + posId + "%' ))" +

                // toe filter ตามวัน
                " and ((coalesce(lock_day, '') = '') or ( lock_day like '%" + ((int)DateTime.Now.DayOfWeek).ToString() + "%')) " +

                "  order by " + _g.d.ic_inventory_barcode_price._discount_type + "," + _g.d.ic_inventory_barcode_price._priority;

            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryBarcodePrice));



            __query.Append("</node>");
            string __queryStr = __query.ToString();
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            ArrayList __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryStr);

            if (__result._foundByCondition == false)
            {
                // หาราคาตาม barcode 
                DataTable __itemSetTable = ((DataSet)__dataResult[0]).Tables[0];
                if (__itemSetTable.Rows.Count > 0)
                {
                    //steppos
                    //DataTable __price1 = ((DataSet)__dataResult[1]).Tables[0];
                    //DataTable __price2 = ((DataSet)__dataResult[2]).Tables[0];
                    //DataTable __price3 = ((DataSet)__dataResult[3]).Tables[0];
                    //DataTable __price4 = ((DataSet)__dataResult[4]).Tables[0];
                    //DataTable __price5 = ((DataSet)__dataResult[5]).Tables[0];

                    DataTable __priceTable = ((DataSet)__dataResult[1]).Tables[0];
                    DataRow[] __price1 = (__priceTable.Rows.Count == 0) ? null : __priceTable.Select(_g.d.ic_inventory_barcode_price._discount_type + "=1");
                    DataRow[] __price2 = (__priceTable.Rows.Count == 0) ? null : __priceTable.Select(_g.d.ic_inventory_barcode_price._discount_type + "=2");
                    DataRow[] __price3 = (__priceTable.Rows.Count == 0) ? null : __priceTable.Select(_g.d.ic_inventory_barcode_price._discount_type + "=3");
                    DataRow[] __price4 = (__priceTable.Rows.Count == 0) ? null : __priceTable.Select(_g.d.ic_inventory_barcode_price._discount_type + "=4");
                    DataRow[] __price5 = (__priceTable.Rows.Count == 0) ? null : __priceTable.Select(_g.d.ic_inventory_barcode_price._discount_type + "=5");


                    // ส่ง discount word กับ discount number และราคา


                    int __discountNumber = 0;
                    string __priceInfo = "";
                    decimal __priceNormal = 0M;
                    decimal __priceMember = 0M;
                    int __price_type = 5;

                    switch (priceNumber)
                    {
                        case 0:
                        case 1:
                            __priceNormal = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][_g.d.ic_inventory_barcode._price].ToString());
                            __priceMember = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][_g.d.ic_inventory_barcode._price_member].ToString());
                            break;
                        case 2:
                            __priceNormal = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][_g.d.ic_inventory_barcode._price_2].ToString());
                            __priceMember = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][_g.d.ic_inventory_barcode._price_member_2].ToString());
                            break;
                        case 3:
                            __priceNormal = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][_g.d.ic_inventory_barcode._price_3].ToString());
                            __priceMember = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][_g.d.ic_inventory_barcode._price_member_3].ToString());
                            break;
                        case 4:
                            __priceNormal = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][_g.d.ic_inventory_barcode._price_4].ToString());
                            __priceMember = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][_g.d.ic_inventory_barcode._price_member_4].ToString());
                            break;
                    }
                    Boolean __foundPriceOrDiscount = false;

                    //this._lastBarcode = __barCode;
                    decimal __price = __priceNormal;
                    string __discountWord = "";
                    string __staffDiscount = (__price5 == null || __price5.Length == 0) ? "" : __price5[0][_g.d.ic_inventory_barcode_price._discount_word].ToString().Trim();
                    string __extraDiscount = (__price1 == null || __price1.Length == 0) ? "" : __price1[0][_g.d.ic_inventory_barcode_price._discount_word].ToString().Trim();
                    string __extraDiscountMember = (__price2 == null || __price2.Length == 0) ? "" : __price2[0][_g.d.ic_inventory_barcode_price._discount_word].ToString().Trim();
                    string __discount = (__price3 == null || __price3.Length == 0) ? "" : __price3[0][_g.d.ic_inventory_barcode_price._discount_word].ToString().Trim();
                    string __discountMember = (__price4 == null || __price4.Length == 0) ? "" : __price4[0][_g.d.ic_inventory_barcode_price._discount_word].ToString().Trim();
                    //
                    if (memberCode.Length > 0 && (memberCode.Equals(defaultcustCode) == false) && isStaff && __staffDiscount.Length > 0)
                    {
                        if (__staffDiscount.IndexOf("#") == 0)
                        {
                            int __startDiscountPoint = __staffDiscount.IndexOf(",");

                            __price = MyLib._myGlobal._decimalPhase(__staffDiscount.Substring(1, (__startDiscountPoint == -1) ? __staffDiscount.Length - 1 : __startDiscountPoint));
                            __priceInfo = "ราคาพนักงาน " + __price.ToString(_formatNumber1);
                            __foundPriceOrDiscount = true;

                            // เช็คส่วนลด
                            if (__startDiscountPoint != -1)
                            {
                                __discountWord = __staffDiscount.Substring(__startDiscountPoint + 1, __staffDiscount.Length - (__startDiscountPoint + 1));
                                __priceInfo = "ส่วนลดพนักงาน" + " " + __discountWord;
                                __discountNumber = 5;
                            }
                        }
                        else
                        {
                            // ราคาพนักงาน
                            __foundPriceOrDiscount = true;
                            __discountWord = __staffDiscount;
                            __priceInfo = "ส่วนลดพนักงาน" + " " + __discountWord;
                            __discountNumber = 5;

                        }
                    }
                    if (__foundPriceOrDiscount == false)
                    {
                        // ส่วนลดพิเศษ (ตามฤดู)
                        if (memberCode.Length > 0 && (memberCode.Equals(defaultcustCode) == false))
                        {
                            // สมาชิก
                            string __memberDiscount = __extraDiscountMember;
                            if (__memberDiscount.Length > 0)
                            {
                                if (__memberDiscount.IndexOf("#") == 0)
                                {
                                    int __startDiscountPoint = __memberDiscount.IndexOf(",");

                                    __price = MyLib._myGlobal._decimalPhase(__memberDiscount.Substring(1, (__startDiscountPoint == -1) ? __memberDiscount.Length - 1 : __startDiscountPoint));
                                    __priceInfo = "ราคาพิเศษ+สมาชิก " + __price.ToString(_formatNumber1);
                                    __foundPriceOrDiscount = true;

                                    // เช็คส่วนลด
                                    if (__startDiscountPoint != -1)
                                    {
                                        __discountWord = __memberDiscount.Substring(__startDiscountPoint + 1, __memberDiscount.Length - (__startDiscountPoint + 1));
                                        __priceInfo = "ส่วนลดพิเศษ+สมาชิก" + " " + __discountWord;
                                        __discountNumber = 2;
                                    }


                                }
                                else
                                {
                                    __discountWord = __extraDiscountMember;
                                    if (__priceInfo.Length > 0)
                                    {
                                        __priceInfo = __priceInfo + " ";
                                    }
                                    __priceInfo = __priceInfo + "ส่วนลดพิเศษ+สมาชิก" + " " + __discountWord;
                                    __foundPriceOrDiscount = true;
                                    __discountNumber = 2;
                                }
                            }
                        }
                        if (__foundPriceOrDiscount == false)
                        {
                            string __promotionDiscount = __extraDiscount;
                            if (__promotionDiscount.Length > 0)
                            {
                                if (__promotionDiscount.IndexOf("#") == 0)
                                {
                                    int __startDiscountPoint = __promotionDiscount.IndexOf(",");

                                    __price = MyLib._myGlobal._decimalPhase(__promotionDiscount.Substring(1, (__startDiscountPoint == -1) ? __promotionDiscount.Length - 1 : __startDiscountPoint));
                                    __priceInfo = "ราคาพิเศษ+โปรโมชั่น " + __price.ToString(_formatNumber1);
                                    __foundPriceOrDiscount = true;

                                    // เช็คส่วนลด
                                    if (__startDiscountPoint != -1)
                                    {
                                        __discountWord = __promotionDiscount.Substring(__startDiscountPoint + 1, __promotionDiscount.Length - (__startDiscountPoint + 1));
                                        __priceInfo = "ส่วนลดพิเศษ+โปรโมชั่น" + " " + __discountWord;
                                        __discountNumber = 1;
                                    }
                                }
                                else
                                {
                                    __discountWord = __promotionDiscount;
                                    __foundPriceOrDiscount = true;
                                    if (__priceInfo.Length > 0)
                                    {
                                        __priceInfo = __priceInfo + " ";
                                    }
                                    __priceInfo = __priceInfo + "ส่วนลดพิเศษ+โปรโมชั่น" + " " + __discountWord;
                                    __discountNumber = 1;
                                }
                            }
                        }
                        // ส่วนลดทั่วไป
                        if (__foundPriceOrDiscount == false && memberCode.Length > 0 && (memberCode.Equals(defaultcustCode) == false))
                        {
                            // สมาชิก
                            string __memberDiscount = __discountMember;
                            if (__memberDiscount.Length > 0)
                            {
                                if (__memberDiscount.IndexOf("#") == 0)
                                {
                                    int __startDiscountPoint = __memberDiscount.IndexOf(",");

                                    __price = MyLib._myGlobal._decimalPhase(__memberDiscount.Substring(1, (__startDiscountPoint == -1) ? __memberDiscount.Length - 1 : __startDiscountPoint));
                                    __priceInfo = "ราคาสมาชิก " + __price.ToString(_formatNumber1);
                                    __foundPriceOrDiscount = true;


                                    // เช็คส่วนลด
                                    if (__startDiscountPoint != -1)
                                    {
                                        __discountWord = __memberDiscount.Substring(__startDiscountPoint + 1, __memberDiscount.Length - (__startDiscountPoint + 1));
                                        __priceInfo = "ส่วนลดสมาชิก" + " " + __discountWord;
                                        __discountNumber = 4;
                                    }

                                }
                                else
                                {
                                    __discountWord = __memberDiscount;
                                    if (__priceInfo.Length > 0)
                                    {
                                        __priceInfo = __priceInfo + " ";
                                    }
                                    __priceInfo = __priceInfo + "ส่วนลดสมาชิก" + " " + __discountWord;
                                    __foundPriceOrDiscount = true;
                                    __discountNumber = 4;
                                }
                            }
                        }
                        if (__foundPriceOrDiscount == false)
                        {
                            // Promotion
                            string __promotionDiscount = __discount;
                            if (__promotionDiscount.Length > 0)
                            {
                                if (__promotionDiscount.IndexOf("#") == 0)
                                {
                                    int __startDiscountPoint = __promotionDiscount.IndexOf(",");

                                    __price = MyLib._myGlobal._decimalPhase(__promotionDiscount.Substring(1, (__startDiscountPoint == -1) ? __promotionDiscount.Length - 1 : __startDiscountPoint));
                                    __priceInfo = "ราคาโปรโมชั่น " + __price.ToString(_formatNumber1);
                                    __foundPriceOrDiscount = true;

                                    // เช็คส่วนลด
                                    if (__startDiscountPoint != -1)
                                    {
                                        __discountWord = __promotionDiscount.Substring(__startDiscountPoint, __promotionDiscount.Length - __startDiscountPoint);
                                        __priceInfo = " ส่วนลดโปรโมชั่น" + " " + __discountWord;
                                        __discountNumber = 3;
                                    }

                                }
                                else
                                {
                                    __discountWord = __promotionDiscount;
                                    __foundPriceOrDiscount = true;
                                    if (__priceInfo.Length > 0)
                                    {
                                        __priceInfo = __priceInfo + " ";
                                    }
                                    __priceInfo = __priceInfo + "ส่วนลดโปรโมชั่น" + " " + __discountWord;
                                    __discountNumber = 3;
                                }
                            }
                        }
                        if (__foundPriceOrDiscount == false)
                        {
                            // ปรกติ
                            if (memberCode.Length > 0 && (memberCode.Equals(defaultcustCode) == false) && __priceMember > 0M)
                            {
                                __price = __priceMember;
                                __foundPriceOrDiscount = true;
                                __priceInfo = "ราคาสมาชิก" + " " + __price.ToString(_formatNumber1);
                                __price_type = 7;
                            }
                            else
                                if (__priceNormal > 0M)
                            {
                                __price = __priceNormal;
                                __priceInfo = "ราคาปรกติ" + " " + __price.ToString(_formatNumber1);
                                __foundPriceOrDiscount = true;
                            }
                        }

                    }
                    __result._price = __price;
                    __result._type = __price_type;
                    __result._discountNumber = __discountNumber;
                    __result._discountWord = __discountWord;
                    __result._price_info = __priceInfo;
                    __result._foundPrice = __foundPriceOrDiscount;

                }

                if (__result._foundPrice == false)
                {
                    // ไปหาจาก รหัสสินค้าได้เลย
                    __result = _findPriceByItem(itemCode, barcode, unitCode, qty, custCode, memberCode, false, "", __today, _g.g._vatTypeEnum.ภาษีรวมใน, 0, 1, 0);
                }
            }

            return __result;
        }
    }
}
