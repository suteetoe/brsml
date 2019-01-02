using MyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Json;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;



namespace SMLSINGHAControl
{
    public class _processImportInventory : _processImport
    {

        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        _singhaMasterTransfer _singhaMasterTransfer = new _singhaMasterTransfer();
        JsonValue _resultObject = null;
        DateTime __today = DateTime.Now;


        public _processImportInventory(string tableName) : base(tableName)
        {
            this.tableName = tableName;
        }

        public virtual void _processInventory()
        {
            this._loaddata();

            this._process_import(this._preparedata(this._resultObject));


        }

        public void _processfilterInventory(JsonArray _jObj)
        {
            this._process_import(this._preparedata(_jObj[0]));
        }



        protected override void _loaddata()
        {
            WebClient __n = new WebClient();
            string __getCompanyRestUrl = MyLib._myGlobal._syncMasterUrl + this.tableName; //http://localhost:9000/getdb/ MyLib._myGlobal._syncMasterUrl
            if (_g.g._companyProfile._sync_master_url != "")
            {
                __getCompanyRestUrl = _g.g._companyProfile._sync_master_url + this.tableName;
            }

            try
            {
                _restClient __rest = new _restClient(__getCompanyRestUrl);
                string __response = __rest.MakeRequest();

                JsonValue __jsonObject = JsonValue.Parse(__response);
                if (__jsonObject.Count > 0)
                {
                    this._resultObject = __jsonObject;
                }
            }
            catch (Exception __ex)
            {
                this._setlog("loaddata error :" + __ex);
            }

        }



        protected override JsonArray _preparedata(JsonValue jObj)
        {
            JsonArray __result = new JsonArray();
            if (jObj == null)
            {
                this._setlog("====================================================================================================================================================================================================");
                this._setlog("Prepare " + this.tableName + " start time" + __today);
                this._setlog("====================================================================================================================================================================================================");
                this._setlog("===================== ไม่พบข้อมูล =====================");
                return __result;
            }
            try
            {


                if (jObj.Count > 0)
                {
                    base._setlog("====================================================================================================================================================================================================");
                    base._setlog("Prepare " + this.tableName + " start time" + __today);
                    base._setlog("====================================================================================================================================================================================================");

                    string __value_code = "";
                    string __value_name = "";

                    StringBuilder __myquery = new StringBuilder();
                    __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select code,name_1 from " + this.tableName));
                    __myquery.Append("</node>");
                    ArrayList _getDatafromquery = _myFrameWork._queryListGetData(_myGlobal._databaseName, __myquery.ToString());

                    //StringBuilder __myquery2 = new StringBuilder();
                    //__myquery2.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    //__myquery2.Append(MyLib._myUtil._convertTextToXmlForQuery("select code from ic_unit_use"));
                    //__myquery2.Append("</node>");
                    //ArrayList _getDatafromquery2 = _myFrameWork._queryListGetData(_myGlobal._databaseName, __myquery2.ToString());

                    //StringBuilder __myquery3 = new StringBuilder();
                    //__myquery3.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    //__myquery3.Append(MyLib._myUtil._convertTextToXmlForQuery("select barcode from ic_inventory_barcode"));
                    //__myquery3.Append("</node>");
                    //ArrayList _getDatafromquery3 = _myFrameWork._queryListGetData(_myGlobal._databaseName, __myquery3.ToString());

                    //StringBuilder __myquery4 = new StringBuilder();
                    //__myquery4.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    //__myquery4.Append(MyLib._myUtil._convertTextToXmlForQuery("select ic_code from ic_inventory_detail"));
                    //__myquery4.Append("</node>");
                    //ArrayList _getDatafromquery4 = _myFrameWork._queryListGetData(_myGlobal._databaseName, __myquery4.ToString());

                    DataSet getData0 = (DataSet)_getDatafromquery[0];
                    //DataSet getData1 = (DataSet)_getDatafromquery2[0];
                    //DataSet getData2 = (DataSet)_getDatafromquery3[0];
                    //DataSet getData3 = (DataSet)_getDatafromquery4[0];

                    base._setlog("===================== จำนวนข้อมูลของ Master มี : " + jObj.Count+1 + "=====================");
                    int count = 0;
                    int count2 = 0;
                    for (int __row1 = 0; __row1 < jObj.Count; __row1++)
                    {
                        int checkrow = 0;
                        if (jObj[__row1]["code"] != null)
                        {
                            __value_code = jObj[__row1]["code"].ToString().Replace("\"", string.Empty);
                            __value_code = base._checkjson(jObj[__row1]["code"]);
                            // base._setlog("Master value :"+__value_code + ":"+__value_name);
                            if (getData0.Tables.Count > 0)
                            {

                                for (int __row2 = 0; __row2 < getData0.Tables[0].Rows.Count; __row2++)
                                {
                                    if (__value_code.Equals(getData0.Tables[0].Rows[__row2]["code"].ToString()))
                                    {
                                        count2++;
                                        checkrow++;
                                    }

                                }
                                if (checkrow == 0)
                                {
                                    count++;
                                    __result.Add(jObj[__row1]);
                                }
                            }

                        }



                        //JsonValue __objunitUse = jObj[__row1]["unitUse"];
                        //if (__objunitUse.Count > 0)
                        //{

                        //    int checkrow1 = 0;
                        //    JsonValue __objunitUse2 = __objunitUse;
                        //    string __value_unitUse_roworder = "";
                        //    string __value_unitUse_code = "";
                        //    string __value_unitUse_ic_code = "";
                        //    string __value_unitUse_standvalue = "";
                        //    string __value_unitUse_dividevalue = "";
                        //    string __value_unitUse_roworder1 = "";

                        //    for (int __unitUserow1 = 0; __unitUserow1 < __objunitUse2.Count; __unitUserow1++)
                        //    {
                        //        __value_unitUse_roworder = __objunitUse2[__unitUserow1]["roworder"].ToString().Replace("\"", string.Empty);
                        //        __value_unitUse_code = __objunitUse2[__unitUserow1]["code"].ToString().Replace("\"", string.Empty);
                        //        __value_unitUse_ic_code = __objunitUse2[__unitUserow1]["ic_code"].ToString().Replace("\"", string.Empty);
                        //        __value_unitUse_standvalue = __objunitUse2[__unitUserow1]["standvalue"].ToString().Replace("\"", string.Empty);
                        //        __value_unitUse_dividevalue = __objunitUse2[__unitUserow1]["dividevalue"].ToString().Replace("\"", string.Empty);
                        //        __value_unitUse_roworder1 = __objunitUse2[__unitUserow1]["roworder1"].ToString().Replace("\"", string.Empty);

                        //        if (getData1.Tables.Count > 0)
                        //        {

                        //            for (int __row2 = 0; __row2 < getData1.Tables[0].Rows.Count; __row2++)
                        //            {
                        //                if (__value_unitUse_code.Equals(getData1.Tables[0].Rows[__row2]["code"].ToString()))
                        //                {
                        //                    checkrow1++;
                        //                }

                        //            }
                        //        }
                        //        if (checkrow1 == 0)
                        //        {
                        //            __result.Add(__objunitUse2[__unitUserow1]);
                        //        }
                        //    }
                        //}

                        //JsonValue __objbarcode = jObj[__row1]["barcode"];
                        //if (__objbarcode.Count > 0)
                        //{
                        //    int checkrow2 = 0;
                        //    JsonValue __objbarcode2 = __objbarcode;
                        //    string __value_barcode_barcode = "";
                        //    string __value_barcode_ic_code = "";
                        //    string __value_barcode_unitCode = "";
                        //    for (int __barcoderow1 = 0; __barcoderow1 < __objbarcode2.Count; __barcoderow1++)
                        //    {
                        //        __value_barcode_barcode = __objbarcode2[__barcoderow1]["barcode"].ToString().Replace("\"", string.Empty);
                        //        __value_barcode_ic_code = __objbarcode2[__barcoderow1]["ic_code"].ToString().Replace("\"", string.Empty);
                        //        __value_barcode_unitCode = __objbarcode2[__barcoderow1]["unit_code"].ToString().Replace("\"", string.Empty);
                        //        if (getData2.Tables.Count > 0)
                        //        {

                        //            for (int __row2 = 0; __row2 < getData2.Tables[0].Rows.Count; __row2++)
                        //            {
                        //                if (__value_barcode_barcode.Equals(getData2.Tables[0].Rows[__row2]["barcode"].ToString()))
                        //                {
                        //                    checkrow2++;
                        //                }

                        //            }
                        //        }
                        //        if (checkrow2 == 0)
                        //        {
                        //            __result.Add(__objbarcode2[__barcoderow1]);
                        //        }
                        //    }
                        //}

                        //JsonValue __objdetail = jObj[__row1]["detail"];
                        //if (__objdetail.Count > 0)
                        //{
                        //    int checkrow3 = 0;
                        //    JsonValue __objdetail2 = __objdetail;
                        //    string __value_detail_ic_code = "";
                        //    string __value_detail_start_purchase_unit = "";

                        //    __value_detail_ic_code = __objdetail2["ic_code"].ToString().Replace("\"", string.Empty);
                        //    __value_detail_start_purchase_unit = __objdetail2["start_purchase_unit"].ToString().Replace("\"", string.Empty);

                        //    if (getData3.Tables.Count > 0)
                        //    {

                        //        for (int __row2 = 0; __row2 < getData3.Tables[0].Rows.Count; __row2++)
                        //        {
                        //            if (__value_detail_ic_code.Equals(getData3.Tables[0].Rows[__row2]["ic_code"].ToString()))
                        //            {
                        //                checkrow3++;
                        //            }
                        //        }
                        //    }
                        //    if (checkrow3 == 0)
                        //    {
                        //        __result.Add(__objdetail2);
                        //    }
                        //}

                    }
                    //JsonValue __obj = jObj[__row1];
                    base._setlog("ข้อมูลที่ซ้ำกันทั้งหมด : " + count2 + "");
                    base._setlog("ข้อมูลที่นำเข้าทั้งหมด : " + count + "");

                }
            }
            catch (Exception __ex)
            {
                base._setlog("Prepare " + this.tableName + " error :" + __ex);
            }
            return __result;
        }

        protected override void _process_import(JsonArray jObj)
        {
            if (jObj != null)
            {
                try
                {
                    for (int __row1 = 0; __row1 < jObj.Count; __row1++)
                    {
                        string __value_code = base._checkjson(jObj[__row1]["code"]);
                        string __value_name_1 = base._checkjson(jObj[__row1]["name_1"]);
                        string __value_name_2 = base._checkjson(jObj[__row1]["name_1"]);
                        string __value_unit_type = base._checkjson(jObj[__row1]["unitType"]);
                        string __value_cost_type = base._checkjson(jObj[__row1]["costType"]);
                        string __value_unit_standard = base._checkjson(jObj[__row1]["unitStandard"]);
                        string __value_unit_cost = base._checkjson(jObj[__row1]["unitCost"]);
                        string __value_ic_serial_no = base._checkjson(jObj[__row1]["icSerialNo"]);
                        string __value_tax_type = base._checkjson(jObj[__row1]["taxType"]);
                        string __value_item_type = base._checkjson(jObj[__row1]["itemType"]);
                        string __value_name_eng_1 = base._checkjson(jObj[__row1]["nameEng1"]);
                        string __value_name_eng_2 = base._checkjson(jObj[__row1]["nameEng2"]);
                        string __value_income_type = base._checkjson(jObj[__row1]["incomeType"]);
                        string __value_item_pattern = base._checkjson(jObj[__row1]["itemPattern"]);
                        string __value_supplier_code = base._checkjson(jObj[__row1]["supplierCode"]);
                        string __value_sign_code = base._checkjson(jObj[__row1]["signCode"]);
                        string __value_account_code_1 = base._checkjson(jObj[__row1]["accountCode1"]);
                        string __value_account_code_2 = base._checkjson(jObj[__row1]["accountCode2"]);
                        string __value_account_code_3 = base._checkjson(jObj[__row1]["accountCode3"]);
                        string __value_account_code_4 = base._checkjson(jObj[__row1]["accountCode4"]);
                        string __value_update_detail = base._checkjson(jObj[__row1]["updateDetail"]);
                        string __value_drink_type = base._checkjson(jObj[__row1]["drinkType"]);
                        string __value_no_discount = base._checkjson(jObj[__row1]["noDiscount"]);
                        string __value_use_expire = base._checkjson(jObj[__row1]["useExpire"]);
                        string __value_update_price = base._checkjson(jObj[__row1]["updatePrice"]);
                        string __value_item_brand = base._checkjson(jObj[__row1]["itemBrand"]);
                        string __value_item_design = base._checkjson(jObj[__row1]["itemDesign"]);
                        string __value_item_grade = base._checkjson(jObj[__row1]["itemGrade"]);
                        string __value_item_model = base._checkjson(jObj[__row1]["itemModel"]);
                        string __value_item_category = base._checkjson(jObj[__row1]["itemCategory"]);
                        string __value_group_main = base._checkjson(jObj[__row1]["groupMain"]);
                        string __value_item_class = base._checkjson(jObj[__row1]["itemClass"]);
                        string __value_group_sub = base._checkjson(jObj[__row1]["groupSub"]);
                        string __value_description = base._checkjson(jObj[__row1]["description"]);
                        string __value_commission_rate = base._checkjson(jObj[__row1]["commissionRate"]);
                        string __value_serial_no_format = base._checkjson(jObj[__row1]["serialNoFormat"]);


                        StringBuilder __myqueryinsert = new StringBuilder();
                        __myqueryinsert.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        __myqueryinsert.Append(MyLib._myUtil._convertTextToXmlForQuery("INSERT INTO ic_inventory (code,name_1,name_2,unit_type,cost_type,unit_standard,unit_cost,ic_serial_no,tax_type,item_type,name_eng_1,name_eng_2,income_type,item_pattern,supplier_code,sign_code,account_code_1,account_code_2,account_code_3,account_code_4,update_detail,drink_type,no_discount,use_expire,update_price,item_brand,item_design,item_grade,item_model,item_category,group_main,item_class,group_sub,description,commission_rate,serial_no_format) VALUES ('"+__value_code+"','"+__value_name_1+"','"+__value_name_2+"',"+__value_unit_type+","+__value_cost_type+",'"+__value_unit_standard+"','"+__value_unit_cost+"',"+__value_ic_serial_no+","+__value_tax_type+","+__value_item_type+",'"+__value_name_eng_1+"','"+__value_name_eng_2+"','"+__value_income_type+"','"+__value_item_pattern+"','"+__value_supplier_code+"','"+__value_sign_code+"','"+__value_account_code_1+"','"+__value_account_code_2+"','"+__value_account_code_3+"','"+__value_account_code_4+"',"+__value_update_detail+","+__value_drink_type+","+__value_no_discount+","+__value_use_expire+","+__value_update_price+",'"+__value_item_brand+"','"+__value_item_design+"','"+__value_item_grade+"','"+__value_item_model+"','"+__value_item_category+"','"+__value_group_main+"','"+__value_item_class+"','"+__value_group_sub+"','"+__value_description+"','"+__value_commission_rate+"','"+__value_serial_no_format+"')"));
                        __myqueryinsert.Append("</node>");
                        string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myqueryinsert.ToString());
                        if (result.Length == 0)
                        {
                            this._setlog("INSERT ic_inventory :"+__value_code + ":"+__value_name_1 + " success !!");
                        }
                        else
                        {
                            this._setlog("INSERT ic_inventory :"+__value_code + ":"+__value_name_1 + " error :" + result);
                        }

                        JsonValue __objunitUse = jObj[__row1]["unitUse"];
                        if (__objunitUse.Count > 0)
                        {
                            JsonValue __objunitUse2 = __objunitUse;

                            string __value_unitUse_code = "";
                            string __value_unitUse_ic_code = "";
                            string __value_unitUse_standvalue = "";
                            string __value_unitUse_dividevalue = "";
                            string __value_unitUse_roworder1 = "";

                            string __value_unitUse_ratio = "";
                            string __value_unitUse_width_length_height = "";
                            string __value_unitUse_weight = "";

                            this._setlog("INSERT ic_unit_use ic_inventory.code ="+__value_code + "  start !!");
                                for (int __unitUserow1 = 0; __unitUserow1 < __objunitUse2.Count; __unitUserow1++)
                                {

                                    __value_unitUse_code = base._checkjson(__objunitUse2[__unitUserow1]["code"]);
                                    __value_unitUse_ic_code = base._checkjson(__objunitUse2[__unitUserow1]["ic_code"]);
                                    __value_unitUse_standvalue = base._checkjson(__objunitUse2[__unitUserow1]["standvalue"]);
                                    __value_unitUse_dividevalue = base._checkjson(__objunitUse2[__unitUserow1]["dividevalue"]);
                                    __value_unitUse_roworder1 = base._checkjson(__objunitUse2[__unitUserow1]["roworder1"]);

                                    __value_unitUse_ratio = base._checkjson(__objunitUse2[__unitUserow1]["ratio"]);
                                    __value_unitUse_width_length_height = base._checkjson(__objunitUse2[__unitUserow1]["width_length_height"]);
                                    __value_unitUse_weight = base._checkjson(__objunitUse2[__unitUserow1]["weight"]);
                                  
                                StringBuilder __myqueryinsertunitUse = new StringBuilder();
                                    __myqueryinsertunitUse.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                    __myqueryinsertunitUse.Append(MyLib._myUtil._convertTextToXmlForQuery("INSERT INTO ic_unit_use (code, ic_code, stand_value,divide_value,row_order,ratio,width_length_height,weight) VALUES ('" + __value_unitUse_code+"','"+__value_unitUse_ic_code+"',"+__value_unitUse_standvalue+","+__value_unitUse_dividevalue+","+__value_unitUse_roworder1+","+__value_unitUse_ratio+",'"+__value_unitUse_width_length_height+"','"+__value_unitUse_weight+"')"));
                                    __myqueryinsertunitUse.Append("</node>");
                                    string resultunitUse = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myqueryinsertunitUse.ToString());
                                    if (resultunitUse.Length == 0)
                                    {
                                        this._setlog("INSERT ic_unit_use :"+__value_unitUse_code + ":"+__value_unitUse_ic_code + " success !!");
                                    }
                                    else {
                                        this._setlog("INSERT ic_unit_use :"+__value_unitUse_code + ":"+__value_unitUse_ic_code + " error :" + resultunitUse);
                                    }
                                }
                                this._setlog("INSERT ic_unit_use ic_inventory.code ="+__value_code + "  success !!");

                        }

                        JsonValue __objbarcode = jObj[__row1]["barcode"];
                        if (__objbarcode.Count > 0)
                        {
                            JsonValue __objbarcode2 = __objbarcode;
                            string __value_barcode_barcode = "";
                            string __value_barcode_ic_code = "";
                            string __value_barcode_unitCode = "";
                           
                                this._setlog("INSERT ic_inventory_barcode ic_inventory.code ="+__value_code + "  start !!");
                                for (int __barcoderow1 = 0; __barcoderow1 < __objbarcode2.Count; __barcoderow1++)
                                {
                                    __value_barcode_barcode = base._checkjson(__objbarcode2[__barcoderow1]["barcode"]);
                                    __value_barcode_ic_code = base._checkjson(__objbarcode2[__barcoderow1]["ic_code"]);
                                    __value_barcode_unitCode = base._checkjson(__objbarcode2[__barcoderow1]["unitCode"]);


                                    StringBuilder __myqueryinsertbarcode = new StringBuilder();
                                    __myqueryinsertbarcode.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                    __myqueryinsertbarcode.Append(MyLib._myUtil._convertTextToXmlForQuery("INSERT INTO ic_inventory_barcode (barcode, ic_code, unit_code) VALUES ('"+__value_barcode_barcode+"','"+__value_barcode_ic_code+"','"+__value_barcode_unitCode+"')"));
                                    __myqueryinsertbarcode.Append("</node>");
                                    string resultbarcode = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myqueryinsertbarcode.ToString());
                                    if (resultbarcode.Length == 0)
                                    {
                                        this._setlog("INSERT ic_inventory_barcode :"+__value_barcode_barcode + ":"+__value_barcode_ic_code + " success !!");
                                    }
                                    else {
                                        this._setlog("INSERT ic_inventory_barcode :"+__value_barcode_barcode + ":"+__value_barcode_ic_code + " error :" + resultbarcode);
                                    }

                                }
                                this._setlog("INSERT ic_inventory_barcode ic_inventory.code ="+__value_code + "  success !!");
                        }

                        JsonValue __objdetail = jObj[__row1]["detail"];
                        if (__objdetail.Count > 0)
                        {
                            this._setlog("INSERT ic_inventory_detail ic_inventory.code ="+__value_code + "  start !!");
                            JsonValue __objdetail2 = __objdetail;
                            string __value_detail_ic_code = base._checkjson(__objdetail2["ic_code"]);
                            string __value_detail_start_purchase_unit = base._checkjson(__objdetail2["start_purchase_unit"]);
                            string __value_detail_formular = base._checkjson(__objdetail2["formular"]);
                            string __value_detail_po_over= base._checkjson(__objdetail2["po_over"]);
                            string __value_detail_so_over= base._checkjson(__objdetail2["so_over"]);
                            string __value_detail_account_group= base._checkjson(__objdetail2["account_group"]);
                            string __value_detail_serial_number= base._checkjson(__objdetail2["serial_number"]);
                            string __value_detail_tax_import= base._checkjson(__objdetail2["tax_import"]);
                            string __value_detail_tax_rate= base._checkjson(__objdetail2["tax_rate"]);
                            string __value_detail_purchase_manager= base._checkjson(__objdetail2["purchase_manager"]);
                            string __value_detail_sale_manager= base._checkjson(__objdetail2["sale_manager"]);
                            string __value_detail_start_purchase_wh= base._checkjson(__objdetail2["start_purchase_wh"]);
                            string __value_detail_start_purchase_shelf= base._checkjson(__objdetail2["start_purchase_shelf"]);
                            string __value_detail_start_sale_wh= base._checkjson(__objdetail2["start_sale_wh"]);
                            string __value_detail_start_sale_shelf= base._checkjson(__objdetail2["start_sale_shelf"]);
                            string __value_detail_start_sale_unit= base._checkjson(__objdetail2["start_sale_unit"]);
                            string __value_detail_cost_produce= base._checkjson(__objdetail2["cost_produce"]);
                            string __value_detail_cost_standard= base._checkjson(__objdetail2["cost_standard"]);
                            string __value_detail_unit_for_stock= base._checkjson(__objdetail2["unit_for_stock"]);
                            string __value_detail_ic_out_wh= base._checkjson(__objdetail2["ic_out_wh"]);
                            string __value_detail_ic_out_shelf= base._checkjson(__objdetail2["ic_out_shelf"]);
                            string __value_detail_ic_reserve_wh= base._checkjson(__objdetail2["ic_reserve_wh"]);
                            string __value_detail_reserve_status= base._checkjson(__objdetail2["reserve_status"]);
                            string __value_detail_discount= base._checkjson(__objdetail2["discount"]);
                            string __value_detail_purchase_point= base._checkjson(__objdetail2["purchase_point"]);
                            string __value_detail_unit_2_code= base._checkjson(__objdetail2["unit_2_code"]);
                            string __value_detail_unit_2_qty= base._checkjson(__objdetail2["unit_2_qty"]);
                            string __value_detail_unit_2_average= base._checkjson(__objdetail2["unit_2_average"]);
                            string __value_detail_unit_2_average_value= base._checkjson(__objdetail2["unit_2_average_value"]);
                            string __value_detail_user_group_for_purchase= base._checkjson(__objdetail2["user_group_for_purchase"]);
                            string __value_detail_user_group_for_sale= base._checkjson(__objdetail2["user_group_for_sale"]);
                            string __value_detail_user_group_for_manage= base._checkjson(__objdetail2["user_group_for_manage"]);
                            string __value_detail_user_group_for_warehouse= base._checkjson(__objdetail2["user_group_for_warehouse"]);
                            string __value_detail_user_status= base._checkjson(__objdetail2["user_status"]);
                            string __value_detail_close_reason= base._checkjson(__objdetail2["close_reason"]);
                            //string __value_detail_close_date= base._checkjson(__objdetail2["close_date"]);
                            string __value_detail_ref_file_1= base._checkjson(__objdetail2["ref_file_1"]);
                            string __value_detail_ref_file_2= base._checkjson(__objdetail2["ref_file_2"]);
                            string __value_detail_ref_file_3= base._checkjson(__objdetail2["ref_file_3"]);
                            string __value_detail_ref_file_4= base._checkjson(__objdetail2["ref_file_4"]);
                            string __value_detail_ref_file_5= base._checkjson(__objdetail2["ref_file_5"]);
                            string __value_detail_sale_price_1= base._checkjson(__objdetail2["sale_price_1"]);
                            string __value_detail_sale_price_2= base._checkjson(__objdetail2["sale_price_2"]);
                            string __value_detail_sale_price_3= base._checkjson(__objdetail2["sale_price_3"]);
                            string __value_detail_sale_price_4= base._checkjson(__objdetail2["sale_price_4"]);
                            string __value_detail_maximum_qty= base._checkjson(__objdetail2["maximum_qty"]);
                            string __value_detail_minimum_qty= base._checkjson(__objdetail2["minimum_qty"]);
                            string __value_detail_accrued_control= base._checkjson(__objdetail2["accrued_control"]);
                            string __value_detail_lock_price= base._checkjson(__objdetail2["lock_price"]);
                            string __value_detail_lock_discount= base._checkjson(__objdetail2["lock_discount"]);
                            string __value_detail_lock_cost= base._checkjson(__objdetail2["lock_cost"]);
                            string __value_detail_is_end= base._checkjson(__objdetail2["is_end"]);
                            string __value_detail_is_hold_purchase= base._checkjson(__objdetail2["is_hold_purchase"]);
                            string __value_detail_is_hold_sale= base._checkjson(__objdetail2["is_hold_sale"]);
                            string __value_detail_is_stop= base._checkjson(__objdetail2["is_stop"]);
                            string __value_detail_balance_control= base._checkjson(__objdetail2["balance_control"]);
                            string __value_detail_have_point= base._checkjson(__objdetail2["have_point"]);
                            string __value_detail_start_unit_code= base._checkjson(__objdetail2["start_unit_code"]);
                            string __value_detail_is_premium= base._checkjson(__objdetail2["is_premium"]);
                            string __value_detail_dimension_1= base._checkjson(__objdetail2["dimension_1"]);
                            string __value_detail_dimension_2= base._checkjson(__objdetail2["dimension_2"]);
                            string __value_detail_dimension_3= base._checkjson(__objdetail2["dimension_3"]);
                            string __value_detail_dimension_4= base._checkjson(__objdetail2["dimension_4"]);
                            string __value_detail_dimension_5= base._checkjson(__objdetail2["dimension_5"]);
                            string __value_detail_dimension_6= base._checkjson(__objdetail2["dimension_6"]);
                            string __value_detail_dimension_7= base._checkjson(__objdetail2["dimension_7"]);
                            string __value_detail_dimension_8= base._checkjson(__objdetail2["dimension_8"]);
                            string __value_detail_dimension_9= base._checkjson(__objdetail2["dimension_9"]);
                            string __value_detail_dimension_10= base._checkjson(__objdetail2["dimension_10"]);
                            string __value_detail_dimension_11= base._checkjson(__objdetail2["dimension_11"]);
                            string __value_detail_dimension_12= base._checkjson(__objdetail2["dimension_12"]);
                            string __value_detail_dimension_13= base._checkjson(__objdetail2["dimension_13"]);
                            string __value_detail_dimension_14= base._checkjson(__objdetail2["dimension_14"]);
                            string __value_detail_dimension_15= base._checkjson(__objdetail2["dimension_15"]);
                            string __value_detail_dimension_16= base._checkjson(__objdetail2["dimension_16"]);
                            string __value_detail_dimension_17= base._checkjson(__objdetail2["dimension_17"]);
                            string __value_detail_dimension_18= base._checkjson(__objdetail2["dimension_18"]);
                            string __value_detail_dimension_19= base._checkjson(__objdetail2["dimension_19"]);
                            string __value_detail_dimension_20= base._checkjson(__objdetail2["dimension_20"]);
                            string __value_detail_dimension_21= base._checkjson(__objdetail2["dimension_21"]);
                            string __value_detail_dimension_22= base._checkjson(__objdetail2["dimension_22"]);
                            string __value_detail_dimension_23= base._checkjson(__objdetail2["dimension_23"]);
                            string __value_detail_dimension_24= base._checkjson(__objdetail2["dimension_24"]);
                            string __value_detail_dimension_25= base._checkjson(__objdetail2["dimension_25"]);
                            string __value_detail_dimension_26= base._checkjson(__objdetail2["dimension_26"]);
                            string __value_detail_dimension_27= base._checkjson(__objdetail2["dimension_27"]);
                            string __value_detail_dimension_28= base._checkjson(__objdetail2["dimension_28"]);
                            string __value_detail_dimension_29= base._checkjson(__objdetail2["dimension_29"]);
                            string __value_detail_dimension_30= base._checkjson(__objdetail2["dimension_30"]);
                            string __value_detail_dimension_31= base._checkjson(__objdetail2["dimension_31"]);
                            string __value_detail_dimension_32= base._checkjson(__objdetail2["dimension_32"]);
                            string __value_detail_dimension_33= base._checkjson(__objdetail2["dimension_33"]);
                            string __value_detail_dimension_34= base._checkjson(__objdetail2["dimension_34"]);
                            string __value_detail_dimension_35= base._checkjson(__objdetail2["dimension_35"]);
                            string __value_detail_dimension_36= base._checkjson(__objdetail2["dimension_36"]);
                            string __value_detail_dimension_37= base._checkjson(__objdetail2["dimension_37"]);
                            string __value_detail_dimension_38= base._checkjson(__objdetail2["dimension_38"]);
                            string __value_detail_dimension_39= base._checkjson(__objdetail2["dimension_39"]);
                            string __value_detail_dimension_40= base._checkjson(__objdetail2["dimension_40"]);
                            string __value_detail_dimension_41= base._checkjson(__objdetail2["dimension_41"]);
                            string __value_detail_dimension_42= base._checkjson(__objdetail2["dimension_42"]);
                            string __value_detail_dimension_43= base._checkjson(__objdetail2["dimension_43"]);
                            string __value_detail_dimension_44= base._checkjson(__objdetail2["dimension_44"]);
                            string __value_detail_dimension_45= base._checkjson(__objdetail2["dimension_45"]);

                            string close_date = "";
                            string __value_detail_close_date = base._checkjson(__objdetail2["close_date"]);
                            if (__value_detail_close_date !="") {
                                __value_detail_close_date = ","+__value_detail_close_date;
                                close_date = ",close_date";
                            }
                            if (__value_detail_is_premium == "") {
                                __value_detail_is_premium = "0";
                            }
                      
                                StringBuilder __myqueryinsertdetail = new StringBuilder();
                                __myqueryinsertdetail.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                __myqueryinsertdetail.Append(MyLib._myUtil._convertTextToXmlForQuery("INSERT INTO ic_inventory_detail (ic_code, start_purchase_unit,formular,po_over,so_over,account_group,serial_number,tax_import,tax_rate,purchase_manager,sale_manager,start_purchase_wh,start_purchase_shelf,start_sale_wh,start_sale_shelf,start_sale_unit,cost_produce,cost_standard,unit_for_stock,ic_out_wh,ic_out_shelf,ic_reserve_wh,reserve_status,discount,purchase_point,unit_2_code,unit_2_qty,unit_2_average,unit_2_average_value,user_group_for_purchase,user_group_for_sale,user_group_for_manage,user_group_for_warehouse,user_status,close_reason"+close_date+",ref_file_1,ref_file_2,ref_file_3,ref_file_4,ref_file_5,sale_price_1,sale_price_2,sale_price_3,sale_price_4,maximum_qty,minimum_qty,accrued_control,lock_price,lock_discount,lock_cost,is_end,is_hold_purchase,is_hold_sale,is_stop,balance_control,have_point,start_unit_code,is_premium,dimension_1,dimension_2,dimension_3,dimension_4,dimension_5,dimension_6,dimension_7,dimension_8,dimension_9,dimension_10,dimension_11,dimension_12,dimension_13,dimension_14,dimension_15,dimension_16,dimension_17,dimension_18,dimension_19,dimension_20,dimension_21,dimension_22,dimension_23,dimension_24,dimension_25,dimension_26,dimension_27,dimension_28,dimension_29,dimension_30,dimension_31,dimension_32,dimension_33,dimension_34,dimension_35,dimension_36,dimension_37,dimension_38,dimension_39,dimension_40,dimension_41,dimension_42,dimension_43,dimension_44,dimension_45) VALUES ('"+__value_detail_ic_code+"','"+__value_detail_start_purchase_unit+"','"+__value_detail_formular+"',"+__value_detail_po_over+","+__value_detail_so_over+",'"+__value_detail_account_group+"','"+__value_detail_serial_number+"','"+__value_detail_tax_import+"',"+__value_detail_tax_rate+",'"+__value_detail_purchase_manager+"','"+__value_detail_sale_manager+"','"+__value_detail_start_purchase_wh+"','"+__value_detail_start_purchase_shelf+"','"+__value_detail_start_sale_wh+"','"+__value_detail_start_sale_shelf+"','"+__value_detail_start_sale_unit+"',"+__value_detail_cost_produce+","+__value_detail_cost_standard+",'"+__value_detail_unit_for_stock+"','"+__value_detail_ic_out_wh+"','"+__value_detail_ic_out_shelf+"','"+__value_detail_ic_reserve_wh+"',"+__value_detail_reserve_status+",'"+__value_detail_discount+"',"+__value_detail_purchase_point+",'"+__value_detail_unit_2_code+"',"+__value_detail_unit_2_qty+","+__value_detail_unit_2_average+","+__value_detail_unit_2_average_value+",'"+__value_detail_user_group_for_purchase+"','"+__value_detail_user_group_for_sale+"','"+__value_detail_user_group_for_manage+"','"+__value_detail_user_group_for_warehouse+"',"+__value_detail_user_status+",'"+__value_detail_close_reason + "'"+__value_detail_close_date+",'"+__value_detail_ref_file_1+"','"+__value_detail_ref_file_2+"','"+__value_detail_ref_file_3+"','"+__value_detail_ref_file_4+"','"+__value_detail_ref_file_5+"',"+__value_detail_sale_price_1+","+__value_detail_sale_price_2+","+__value_detail_sale_price_3+","+__value_detail_sale_price_4+","+__value_detail_maximum_qty+","+__value_detail_minimum_qty+","+__value_detail_accrued_control+","+__value_detail_lock_price+","+__value_detail_lock_discount+","+__value_detail_lock_cost+","+__value_detail_is_end+","+__value_detail_is_hold_purchase+","+__value_detail_is_hold_sale+","+__value_detail_is_stop+","+__value_detail_balance_control+","+__value_detail_have_point+",'"+__value_detail_start_unit_code+"',"+__value_detail_is_premium+",'"+__value_detail_dimension_1+"','"+__value_detail_dimension_2+"','"+__value_detail_dimension_3+"','"+__value_detail_dimension_4+"','"+__value_detail_dimension_5+"','"+__value_detail_dimension_6+"','"+__value_detail_dimension_7+"','"+__value_detail_dimension_8+"','"+__value_detail_dimension_9+"','"+__value_detail_dimension_10+"','"+__value_detail_dimension_11+"','"+__value_detail_dimension_12+"','"+__value_detail_dimension_13+"','"+__value_detail_dimension_14+"','"+__value_detail_dimension_15+"','"+__value_detail_dimension_16+"','"+__value_detail_dimension_17+"','"+__value_detail_dimension_18+"','"+__value_detail_dimension_19+"','"+__value_detail_dimension_20+"','"+__value_detail_dimension_21+"','"+__value_detail_dimension_22+"','"+__value_detail_dimension_23+"','"+__value_detail_dimension_24+"','"+__value_detail_dimension_25+"','"+__value_detail_dimension_26+"','"+__value_detail_dimension_27+"','"+__value_detail_dimension_28+"','"+__value_detail_dimension_29+"','"+__value_detail_dimension_30+"','"+__value_detail_dimension_31+"','"+__value_detail_dimension_32+"','"+__value_detail_dimension_33+"','"+__value_detail_dimension_34+"','"+__value_detail_dimension_35+"','"+__value_detail_dimension_36+"','"+__value_detail_dimension_37+"','"+__value_detail_dimension_38+"','"+__value_detail_dimension_39+"','"+__value_detail_dimension_40+"','"+__value_detail_dimension_41+"','"+__value_detail_dimension_42+"','"+__value_detail_dimension_43+"','"+__value_detail_dimension_44+"','"+__value_detail_dimension_45+"')"));
                                __myqueryinsertdetail.Append("</node>");
                                string resultdetail = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myqueryinsertdetail.ToString());
                                if (resultdetail.Length == 0)
                                {
                                    this._setlog("INSERT ic_inventory_detail :"+__value_detail_ic_code + ":"+__value_detail_start_purchase_unit + " success !!");
                                }
                                else {
                                    this._setlog("INSERT ic_inventory_detail :"+__value_detail_ic_code + ":"+__value_detail_start_purchase_unit + " error :" + resultdetail);
                                }
                        }
                        this._setlog("INSERT ic_inventory_detail ic_inventory.code ="+__value_code + "  success !!");
                    }
                    this._setlog("INSERT " + this.tableName + " success !!" + __today);
                }
                catch (Exception __ex)
                {
                    this._setlog("process import " + this.tableName + " error :" + __ex);
                }

                _g._utils __utils = new _g._utils();
                __utils._updateInventoryMaster("");
                Thread __thread = new Thread(new ThreadStart(__utils._updateInventoryMasterFunction));
                __thread.Start();
            }

        }


    }
}
