using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;

namespace SMLProcess
{
    public class _syncClass
    {
        private string __foundItemcodeResult = "";
        public string _foundItemCode
        {
            get
            {
                return this.__foundItemcodeResult;
            }
        }


        private string _intToStr(string source)
        {
            return ((int)MyLib._myGlobal._decimalPhase(source)).ToString();
        }

        private string _decimalToStr(string source)
        {
            return MyLib._myGlobal._decimalPhase(source).ToString();
        }

        /// <summary>
        /// SML Color Store Only
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public Boolean _findSetDetail(string barcode)
        {
            Boolean __result = false;
            string __masterDatabaseName = (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore && _g.g._companyProfile._sync_database_name.Length > 0) ? _g.g._companyProfile._sync_database_name : "SMLMASTERCENTER";
            string __masterProviderName = (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore) ? "SMLConfig" + _g.g._companyProfile._sync_provider_code.ToUpper() + ".xml" : "SMLConfigSMLMASTER.xml";

            try
            {
                string __webServiceURL = (MyLib._myGlobal._programName.ToUpper().Equals("POS-RETAIL")) ? MyLib._myGlobal._masterWebservice : _g.g._companyProfile._sync_wbservice_url;

                MyLib._myFrameWork __myFrameWorkLocal = new MyLib._myFrameWork();
                MyLib._myFrameWork __myFrameWorkMaster = new MyLib._myFrameWork(__webServiceURL, __masterProviderName, MyLib._myGlobal._databaseType.PostgreSql);
                __myFrameWorkMaster._guid = "SMLX";

                string __itemCodeField = (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore) ? _g.d.ic_inventory._code : _g.d.ic_inventory_barcode._ic_code;
                string __queryCheck = "select "+ __itemCodeField + " from " + _g.d.ic_inventory._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory._code) + "=\'" + barcode.Trim().ToUpper() + "\'";

                DataTable __find = __myFrameWorkMaster._query(__masterDatabaseName, __queryCheck).Tables[0];


                if (__find.Rows.Count > 0)
                {
                    string __itemCode = __find.Rows[0][__itemCodeField].ToString().Trim().ToUpper();
                    this.__foundItemcodeResult = __itemCode;

                    StringBuilder __query = new StringBuilder();
                    __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    // ic_inventory_set_detail
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory_set_detail._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_set_detail._ic_set_code) + "=\'" + __itemCode + "\' order by " + _g.d.ic_inventory_set_detail._line_number));

                    __query.Append("</node>");

                    String __queryStr = __query.ToString();
                    ArrayList __dataResult = __myFrameWorkMaster._queryListGetData(__masterDatabaseName, __queryStr);
                    DataTable __itemSetDetail = ((DataSet)__dataResult[0]).Tables[0];

                    string __extraField = "";
                    string __extraValue = "";
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        __extraField = ",is_lock_record";
                        __extraValue = ",1";
                    }

                    //if (__item.Rows.Count > 0)
                    //{
                    StringBuilder __queryItem = new StringBuilder();
                    __queryItem.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    string __field = "";
                    string __value = "";

                    // item_set
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                    {

                        if (__itemSetDetail.Rows.Count > 0)
                        {
                            for (int __row = 0; __row < __itemSetDetail.Rows.Count; __row++)
                            {
                                DataRow __itemSetRow = __itemSetDetail.Rows[__row];
                                __field = MyLib._myGlobal._fieldAndComma(_g.d.ic_inventory_set_detail._ic_set_code, _g.d.ic_inventory_set_detail._ic_code, _g.d.ic_inventory_set_detail._unit_code, _g.d.ic_inventory_set_detail._qty, _g.d.ic_inventory_set_detail._status, _g.d.ic_inventory_set_detail._line_number, _g.d.ic_inventory_set_detail._price, _g.d.ic_inventory_set_detail._sum_amount, _g.d.ic_inventory_set_detail._barcode, _g.d.ic_inventory_set_detail._price_ratio);
                                __value = MyLib._myGlobal._fieldAndComma("\'" + __itemSetRow[_g.d.ic_inventory_set_detail._ic_set_code].ToString() + "\'", "\'" + __itemSetRow[_g.d.ic_inventory_set_detail._ic_code].ToString() + "\'", "\'" + __itemSetRow[_g.d.ic_inventory_set_detail._unit_code].ToString() + "\'", _decimalToStr(__itemSetRow[_g.d.ic_inventory_set_detail._qty].ToString()), _intToStr(__itemSetRow[_g.d.ic_inventory_set_detail._status].ToString()), _intToStr(__itemSetRow[_g.d.ic_inventory_set_detail._line_number].ToString())
                                    , _decimalToStr(__itemSetRow[_g.d.ic_inventory_set_detail._price].ToString()), _decimalToStr(__itemSetRow[_g.d.ic_inventory_set_detail._sum_amount].ToString()), "\'" + __itemSetRow[_g.d.ic_inventory_set_detail._barcode].ToString() + "\'", _decimalToStr(__itemSetRow[_g.d.ic_inventory_set_detail._price_ratio].ToString()));
                                __queryItem.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_inventory_set_detail._table + " (" + __field + __extraField + ") values (" + __value + __extraValue + ")"));
                            }
                        }
                    }

                    __queryItem.Append("</node>");
                    __queryStr = __queryItem.ToString();
                    string __resultQuery = __myFrameWorkLocal._queryList(MyLib._myGlobal._databaseName, __queryStr);
                    if (__resultQuery.Length == 0)
                    {
                        __result = true;

                        /*SMLProcess._docFlow __process = new SMLProcess._docFlow();
                        __process._processAll(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก, "", "");
                        __result = true;

                        DataTable __unitResult = __myFrameWorkLocal._queryShort("select code from ic_unit_use where not exists ( select code from ic_Unit where ic_unit.code = ic_unit_use.code) and ic_code = \'" + __itemCode.ToUpper() + "\'").Tables[0];
                        if (__unitResult.Rows.Count > 0)
                        {
                            StringBuilder __queryList = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");


                            for (int __row = 0; __row < __unitResult.Rows.Count; __row++)
                            {
                                string __unknowUnitCode = __unitResult.Rows[__row][_g.d.ic_unit_use._code].ToString();

                                for (int __rowUnit = 0; __rowUnit < __itemUnitUse.Rows.Count; __rowUnit++)
                                {
                                    string __unitICCode = __itemUnitUse.Rows[__rowUnit][_g.d.ic_unit_use._code].ToString();
                                    string __unitICName = __itemUnitUse.Rows[__rowUnit][_g.d.ic_unit_use._name_1].ToString();

                                    if (__unknowUnitCode.Equals(__unitICCode) == true)
                                    {
                                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into ic_unit (code, name_1) values (\'" + __unitICCode.ToUpper() + "\', \'" + __unitICName + "\')"));
                                    }
                                }
                            }

                            __queryList.Append("</node>");
                            __resultQuery = __myFrameWorkLocal._queryList(MyLib._myGlobal._databaseName, __queryList.ToString());
                            if (__resultQuery.Length > 0)
                            {
                                Console.WriteLine(__resultQuery.ToString());
                            }
                        }*/
                    }
                    
                }
            }
            catch
            {
            }

            return __result;
        }

        public Boolean _findProduct(string barcode)
        {
            Boolean __result = false;
            string __masterDatabaseName = (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore && _g.g._companyProfile._sync_database_name.Length > 0) ? _g.g._companyProfile._sync_database_name : "SMLMASTERCENTER";
            string __masterProviderName = (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore) ? "SMLConfig" + _g.g._companyProfile._sync_provider_code.ToUpper() + ".xml" : "SMLConfigSMLMASTER.xml";
            try
            {
                string __webServiceURL = (MyLib._myGlobal._programName.ToUpper().Equals("POS-RETAIL")) ? MyLib._myGlobal._masterWebservice : _g.g._companyProfile._sync_wbservice_url;

                MyLib._myFrameWork __myFrameWorkLocal = new MyLib._myFrameWork();
                MyLib._myFrameWork __myFrameWorkMaster = new MyLib._myFrameWork(__webServiceURL, __masterProviderName, MyLib._myGlobal._databaseType.PostgreSql);
                __myFrameWorkMaster._guid = "SMLX";


                string __queryCheck = "";
                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                {
                    __queryCheck = "select * from " + _g.d.ic_inventory._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory._code) + "=\'" + barcode.Trim().ToUpper() + "\'";
                }
                else
                {
                    __queryCheck = "select * from " + _g.d.ic_inventory_barcode._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_barcode._barcode) + "=\'" + barcode.Trim().ToUpper() + "\'";
                }

                DataTable __find = __myFrameWorkMaster._query(__masterDatabaseName, __queryCheck).Tables[0];


                if (__find.Rows.Count > 0)
                {
                    string __itemCodeField = (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore) ? _g.d.ic_inventory._code : _g.d.ic_inventory_barcode._ic_code;
                    string __itemCode = __find.Rows[0][__itemCodeField].ToString().Trim().ToUpper();
                    this.__foundItemcodeResult = __itemCode;

                    StringBuilder __query = new StringBuilder();
                    __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    // รายละเอียดสินค้า
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory._code) + "=\'" + __itemCode + "\'"));
                    // รายละเอียดสินค้า (detail)
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory_detail._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + "=\'" + __itemCode + "\'"));
                    // หน่วยนับ
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select *, (select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + ") as " + _g.d.ic_unit_use._name_1 + " from " + _g.d.ic_unit_use._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_unit_use._ic_code) + "=\'" + __itemCode + "\'"));
                    // Barcode
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory_barcode._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_barcode._ic_code) + "=\'" + __itemCode + "\'"));
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        // ic_inventory_set_detail
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory_set_detail._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_set_detail._ic_set_code) + "=\'" + __itemCode + "\' order by " + _g.d.ic_inventory_set_detail._line_number));
                        // ราคา
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory_price._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_price._ic_code) + "=\'" + __itemCode + "\' and coalesce(" + _g.d.ic_inventory_price._price_mode + ", 0)=0"));
                    }

                    __query.Append("</node>");
                    String __queryStr = __query.ToString();
                    ArrayList __dataResult = __myFrameWorkMaster._queryListGetData(__masterDatabaseName, __queryStr);
                    DataTable __item = ((DataSet)__dataResult[0]).Tables[0];
                    DataTable __itemDetail = ((DataSet)__dataResult[1]).Tables[0];
                    DataTable __itemUnitUse = ((DataSet)__dataResult[2]).Tables[0];
                    DataTable __itemBarCode = ((DataSet)__dataResult[3]).Tables[0];

                    string __extraField = "";
                    string __extraValue = "";
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        __extraField = ",is_lock_record";
                        __extraValue = ",1";
                    }

                    if (__item.Rows.Count > 0)
                    {
                        DataRow __itemRow = __item.Rows[0];
                        StringBuilder __queryItem = new StringBuilder();
                        __queryItem.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        // สินค้า (หัว)
                        string __field = MyLib._myGlobal._fieldAndComma(_g.d.ic_inventory._code, _g.d.ic_inventory._name_1, _g.d.ic_inventory._name_2, _g.d.ic_inventory._name_eng_1, _g.d.ic_inventory._name_eng_2, _g.d.ic_inventory._item_type, _g.d.ic_inventory._unit_type, _g.d.ic_inventory._unit_standard, _g.d.ic_inventory._unit_cost, _g.d.ic_inventory._item_brand, _g.d.ic_inventory._item_category, _g.d.ic_inventory._ic_serial_no);
                        string __value = MyLib._myGlobal._fieldAndComma("\'" + __itemRow[_g.d.ic_inventory._code].ToString() + "\'", "\'" + __itemRow[_g.d.ic_inventory._name_1].ToString() + "\'", "\'" + __itemRow[_g.d.ic_inventory._name_2].ToString() + "\'", "\'" + __itemRow[_g.d.ic_inventory._name_eng_1].ToString() + "\'", "\'" + __itemRow[_g.d.ic_inventory._name_eng_2].ToString() + "\'", _intToStr(__itemRow[_g.d.ic_inventory._item_type].ToString()), _intToStr(__itemRow[_g.d.ic_inventory._unit_type].ToString()), "\'" + __itemRow[_g.d.ic_inventory._unit_standard].ToString() + "\'", "\'" + __itemRow[_g.d.ic_inventory._unit_cost].ToString() + "\'", "\'" + __itemRow[_g.d.ic_inventory._item_brand].ToString() + "\'", "\'" + __itemRow[_g.d.ic_inventory._item_category].ToString() + "\'", _intToStr(__itemRow[_g.d.ic_inventory._ic_serial_no].ToString()));
                        __queryItem.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_inventory._table + " (" + __field + ", " + _g.d.ic_inventory._update_detail + ", " + _g.d.ic_inventory._update_price + __extraField + ") values (" + __value + ", 1, 1" + __extraValue + ")"));
                        // รายละเอียดเพิ่มเติม
                        if (__itemDetail.Rows.Count > 0)
                        {
                            DataRow __itemRowDetail = __itemDetail.Rows[0];
                            __field = MyLib._myGlobal._fieldAndComma(_g.d.ic_inventory_detail._ic_code, _g.d.ic_inventory_detail._dimension_1, _g.d.ic_inventory_detail._dimension_2, _g.d.ic_inventory_detail._dimension_3, _g.d.ic_inventory_detail._dimension_4, _g.d.ic_inventory_detail._dimension_5, _g.d.ic_inventory_detail._dimension_6, _g.d.ic_inventory_detail._dimension_7, _g.d.ic_inventory_detail._dimension_8, _g.d.ic_inventory_detail._dimension_9, _g.d.ic_inventory_detail._dimension_10, _g.d.ic_inventory_detail._dimension_11, _g.d.ic_inventory_detail._dimension_12, _g.d.ic_inventory_detail._dimension_13, _g.d.ic_inventory_detail._dimension_14, _g.d.ic_inventory_detail._dimension_15, _g.d.ic_inventory_detail._dimension_16, _g.d.ic_inventory_detail._dimension_17, _g.d.ic_inventory_detail._dimension_18, _g.d.ic_inventory_detail._dimension_19, _g.d.ic_inventory_detail._dimension_20);
                            __value = MyLib._myGlobal._fieldAndComma("\'" + __itemRowDetail[_g.d.ic_inventory_detail._ic_code].ToString() + "\'", "\'" + __itemRowDetail[_g.d.ic_inventory_detail._dimension_1].ToString() + "\'", "\'" + __itemRowDetail[_g.d.ic_inventory_detail._dimension_2].ToString() + "\'", "\'" + __itemRowDetail[_g.d.ic_inventory_detail._dimension_3].ToString() + "\'", "\'" + __itemRowDetail[_g.d.ic_inventory_detail._dimension_4].ToString() + "\'", "\'" + __itemRowDetail[_g.d.ic_inventory_detail._dimension_5].ToString() + "\'", "\'" + __itemRowDetail[_g.d.ic_inventory_detail._dimension_6].ToString() + "\'", "\'" + __itemRowDetail[_g.d.ic_inventory_detail._dimension_7].ToString() + "\'", "\'" + __itemRowDetail[_g.d.ic_inventory_detail._dimension_8].ToString() + "\'", "\'" + __itemRowDetail[_g.d.ic_inventory_detail._dimension_9].ToString() + "\'", "\'" + __itemRowDetail[_g.d.ic_inventory_detail._dimension_10].ToString() + "\'", "\'" + __itemRowDetail[_g.d.ic_inventory_detail._dimension_11].ToString() + "\'", "\'" + __itemRowDetail[_g.d.ic_inventory_detail._dimension_12].ToString() + "\'", "\'" + __itemRowDetail[_g.d.ic_inventory_detail._dimension_13].ToString() + "\'", "\'" + __itemRowDetail[_g.d.ic_inventory_detail._dimension_14].ToString() + "\'", "\'" + __itemRowDetail[_g.d.ic_inventory_detail._dimension_15].ToString() + "\'", "\'" + __itemRowDetail[_g.d.ic_inventory_detail._dimension_16].ToString() + "\'", "\'" + __itemRowDetail[_g.d.ic_inventory_detail._dimension_17].ToString() + "\'", "\'" + __itemRowDetail[_g.d.ic_inventory_detail._dimension_18].ToString() + "\'", "\'" + __itemRowDetail[_g.d.ic_inventory_detail._dimension_19].ToString() + "\'", "\'" + __itemRowDetail[_g.d.ic_inventory_detail._dimension_20].ToString() + "\'");
                            __queryItem.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_inventory_detail._table + " (" + __field + __extraField + ") values (" + __value + __extraValue + ")"));
                        }
                        // หน่วยนับสินค้า
                        if (__itemUnitUse.Rows.Count > 0)
                        {
                            for (int __row = 0; __row < __itemUnitUse.Rows.Count; __row++)
                            {
                                DataRow __itemUnitUseRow = __itemUnitUse.Rows[__row];
                                __field = MyLib._myGlobal._fieldAndComma(_g.d.ic_unit_use._ic_code, _g.d.ic_unit_use._code, _g.d.ic_unit_use._stand_value, _g.d.ic_unit_use._divide_value, _g.d.ic_unit_use._line_number, _g.d.ic_unit_use._ratio, _g.d.ic_unit_use._width_length_height, _g.d.ic_unit_use._remark, _g.d.ic_unit_use._weight, _g.d.ic_unit_use._status, _g.d.ic_unit_use._row_order);
                                __value = MyLib._myGlobal._fieldAndComma("\'" + __itemUnitUseRow[_g.d.ic_unit_use._ic_code].ToString() + "\'", "\'" + __itemUnitUseRow[_g.d.ic_unit_use._code].ToString() + "\'", _decimalToStr(__itemUnitUseRow[_g.d.ic_unit_use._stand_value].ToString()), _decimalToStr(__itemUnitUseRow[_g.d.ic_unit_use._divide_value].ToString()), _intToStr(__itemUnitUseRow[_g.d.ic_unit_use._line_number].ToString()), _decimalToStr(__itemUnitUseRow[_g.d.ic_unit_use._ratio].ToString()), "\'" + __itemUnitUseRow[_g.d.ic_unit_use._width_length_height].ToString() + "\'", "\'" + __itemUnitUseRow[_g.d.ic_unit_use._remark].ToString() + "\'", "\'" + __itemUnitUseRow[_g.d.ic_unit_use._weight].ToString() + "\'", _intToStr(__itemUnitUseRow[_g.d.ic_unit_use._status].ToString()), _intToStr(__itemUnitUseRow[_g.d.ic_unit_use._row_order].ToString()));
                                __queryItem.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_unit_use._table + " (" + __field + ") values (" + __value + ")"));
                            }
                        }
                        // Barcode
                        if (__itemBarCode.Rows.Count > 0)
                        {
                            for (int __row = 0; __row < __itemBarCode.Rows.Count; __row++)
                            {
                                DataRow __itemBarcodeRow = __itemBarCode.Rows[__row];
                                __field = MyLib._myGlobal._fieldAndComma(_g.d.ic_inventory_barcode._barcode, _g.d.ic_inventory_barcode._ic_code, _g.d.ic_inventory_barcode._description, _g.d.ic_inventory_barcode._status, _g.d.ic_inventory_barcode._line_number, _g.d.ic_inventory_barcode._unit_code);
                                __value = MyLib._myGlobal._fieldAndComma("\'" + __itemBarcodeRow[_g.d.ic_inventory_barcode._barcode].ToString() + "\'", "\'" + __itemBarcodeRow[_g.d.ic_inventory_barcode._ic_code].ToString() + "\'", "\'" + __itemBarcodeRow[_g.d.ic_inventory_barcode._description].ToString() + "\'", _intToStr(__itemBarcodeRow[_g.d.ic_inventory_barcode._status].ToString()), _intToStr(__itemBarcodeRow[_g.d.ic_inventory_barcode._line_number].ToString()), "\'" + __itemBarcodeRow[_g.d.ic_inventory_barcode._unit_code].ToString() + "\'");
                                __queryItem.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_inventory_barcode._table + " (" + __field + __extraField + ") values (" + __value + __extraValue + ")"));
                            }
                        }

                        // item_set
                        if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                        {
                            DataTable __itemSetDetail = ((DataSet)__dataResult[4]).Tables[0];
                            DataTable __itemPrice = ((DataSet)__dataResult[5]).Tables[0];

                            if (__itemSetDetail.Rows.Count > 0)
                            {
                                for (int __row = 0; __row < __itemSetDetail.Rows.Count; __row++)
                                {
                                    DataRow __itemSetRow = __itemSetDetail.Rows[__row];
                                    __field = MyLib._myGlobal._fieldAndComma(_g.d.ic_inventory_set_detail._ic_set_code, _g.d.ic_inventory_set_detail._ic_code, _g.d.ic_inventory_set_detail._unit_code, _g.d.ic_inventory_set_detail._qty, _g.d.ic_inventory_set_detail._status, _g.d.ic_inventory_set_detail._line_number, _g.d.ic_inventory_set_detail._price, _g.d.ic_inventory_set_detail._sum_amount, _g.d.ic_inventory_set_detail._barcode, _g.d.ic_inventory_set_detail._price_ratio);
                                    __value = MyLib._myGlobal._fieldAndComma("\'" + __itemSetRow[_g.d.ic_inventory_set_detail._ic_set_code].ToString() + "\'", "\'" + __itemSetRow[_g.d.ic_inventory_set_detail._ic_code].ToString() + "\'", "\'" + __itemSetRow[_g.d.ic_inventory_set_detail._unit_code].ToString() + "\'", _decimalToStr(__itemSetRow[_g.d.ic_inventory_set_detail._qty].ToString()), _intToStr(__itemSetRow[_g.d.ic_inventory_set_detail._status].ToString()), _intToStr(__itemSetRow[_g.d.ic_inventory_set_detail._line_number].ToString())
                                        , _decimalToStr(__itemSetRow[_g.d.ic_inventory_set_detail._price].ToString()), _decimalToStr(__itemSetRow[_g.d.ic_inventory_set_detail._sum_amount].ToString()), "\'" + __itemSetRow[_g.d.ic_inventory_set_detail._barcode].ToString() + "\'", _decimalToStr(__itemSetRow[_g.d.ic_inventory_set_detail._price_ratio].ToString()));
                                    __queryItem.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_inventory_set_detail._table + " (" + __field + __extraField + ") values (" + __value + __extraValue + ")"));
                                }
                            }

                            if (__itemPrice.Rows.Count > 0)
                            {
                                for (int __row = 0; __row < __itemPrice.Rows.Count; __row++)
                                {
                                    DataRow __itemPriceRow = __itemPrice.Rows[__row];
                                    __field = MyLib._myGlobal._fieldAndComma(_g.d.ic_inventory_price._ic_code, _g.d.ic_inventory_price._price_type, _g.d.ic_inventory_price._price_mode,
                                        _g.d.ic_inventory_price._sale_price1, _g.d.ic_inventory_price._sale_price2, _g.d.ic_inventory_price._unit_code, _g.d.ic_inventory_price._from_qty, _g.d.ic_inventory_price._to_qty,
                                        _g.d.ic_inventory_price._from_date, _g.d.ic_inventory_price._to_date, _g.d.ic_inventory_price._sale_type, _g.d.ic_inventory_price._transport_type, _g.d.ic_inventory_price._status);

                                    __value = MyLib._myGlobal._fieldAndComma("\'" + __itemPriceRow[_g.d.ic_inventory_price._ic_code].ToString() + "\'", _intToStr(__itemPriceRow[_g.d.ic_inventory_price._price_type].ToString()), _intToStr(__itemPriceRow[_g.d.ic_inventory_price._price_mode].ToString()),
                                        _decimalToStr(__itemPriceRow[_g.d.ic_inventory_price._sale_price1].ToString()), _decimalToStr(__itemPriceRow[_g.d.ic_inventory_price._sale_price2].ToString()), "\'" + __itemPriceRow[_g.d.ic_inventory_price._unit_code].ToString() + "\'", _decimalToStr(__itemPriceRow[_g.d.ic_inventory_price._from_qty].ToString()), _decimalToStr(__itemPriceRow[_g.d.ic_inventory_price._to_qty].ToString()),
                                        "\'" + __itemPriceRow[_g.d.ic_inventory_price._from_date].ToString() + "\'", "\'" + __itemPriceRow[_g.d.ic_inventory_price._to_date].ToString() + "\'", _intToStr(__itemPriceRow[_g.d.ic_inventory_price._sale_type].ToString()), _intToStr(__itemPriceRow[_g.d.ic_inventory_price._transport_type].ToString()), _intToStr(__itemPriceRow[_g.d.ic_inventory_price._status].ToString()));
                                    __queryItem.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_inventory_price._table + " (" + __field + __extraField + ") values (" + __value + __extraValue + ")"));
                                }
                            }
                        }

                        // all warehouse shelf
                        __queryItem.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_wh_shelf._table + "(" + _g.d.ic_wh_shelf._ic_code + "," + _g.d.ic_wh_shelf._shelf_code + "," + _g.d.ic_wh_shelf._wh_code + ") select \'" + __itemCode.ToUpper() + "\', " + _g.d.ic_shelf._code + ", " + _g.d.ic_shelf._whcode + " from " + _g.d.ic_shelf._table + " "));

                        __queryItem.Append("</node>");
                        __queryStr = __queryItem.ToString();
                        string __resultQuery = __myFrameWorkLocal._queryList(MyLib._myGlobal._databaseName, __queryStr);
                        if (__resultQuery.Length == 0)
                        {
                            SMLProcess._docFlow __process = new SMLProcess._docFlow();
                            __process._processAll(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก, "", "");
                            __result = true;

                            DataTable __unitResult = __myFrameWorkLocal._queryShort("select code from ic_unit_use where not exists ( select code from ic_Unit where ic_unit.code = ic_unit_use.code) and ic_code = \'" + __itemCode.ToUpper() + "\'").Tables[0];
                            if (__unitResult.Rows.Count > 0)
                            {
                                StringBuilder __queryList = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");


                                for (int __row = 0; __row < __unitResult.Rows.Count; __row++)
                                {
                                    string __unknowUnitCode = __unitResult.Rows[__row][_g.d.ic_unit_use._code].ToString();

                                    for (int __rowUnit = 0; __rowUnit < __itemUnitUse.Rows.Count; __rowUnit++)
                                    {
                                        string __unitICCode = __itemUnitUse.Rows[__rowUnit][_g.d.ic_unit_use._code].ToString();
                                        string __unitICName = __itemUnitUse.Rows[__rowUnit][_g.d.ic_unit_use._name_1].ToString();

                                        if (__unknowUnitCode.Equals(__unitICCode) == true)
                                        {
                                            __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into ic_unit (code, name_1) values (\'" + __unitICCode.ToUpper() + "\', \'" + __unitICName + "\')"));
                                        }
                                    }
                                }

                                __queryList.Append("</node>");
                                __resultQuery = __myFrameWorkLocal._queryList(MyLib._myGlobal._databaseName, __queryList.ToString());
                                if (__resultQuery.Length > 0)
                                {
                                    Console.WriteLine(__resultQuery.ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return __result;
        }
    }
}
