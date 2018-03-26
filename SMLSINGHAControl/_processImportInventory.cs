using MyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Json;
using System.Net;
using System.Text;
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

        protected override void _loaddata()
        {
            WebClient __n = new WebClient();
            string __getCompanyRestUrl = MyLib._myGlobal._syncMasterUrl + this.tableName; //http://localhost:9000/getdb/ MyLib._myGlobal._syncMasterUrl

            if (MyLib._myGlobal._syncMasterUrlOption !="") {
                __getCompanyRestUrl = MyLib._myGlobal._syncMasterUrlOption + this.tableName;
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

                    StringBuilder __setjson = new StringBuilder();
                    __setjson.Append("[");
                    base._setlog("===================== จำนวนข้อมูลของ Master มี : " + jObj.Count + "=====================");
                    int count = 0;
                    int count2 = 0;
                    for (int __row1 = 0; __row1 < jObj.Count; __row1++)
                    {
                        int checkrow = 0;
                        if (jObj[__row1]["code"] != null) {
                            __value_code = jObj[__row1]["code"].ToString().Replace("\"", string.Empty);

                           // base._setlog("Master value :" + __value_code + ":" + __value_name);
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
            if (jObj != null) {
                string __value_code = "";
                string __value_name_1 = "";
                string __value_name_2 = "";
                string __value_unit_type = "";
                string __value_cost_type = "";
                string __value_unit_standard = "";
                string __value_unit_cost = "";
                
                try
                {
                    for (int __row1 = 0; __row1 < jObj.Count; __row1++)
                    {

                        __value_code = jObj[__row1]["code"].ToString().Replace("\"", string.Empty);
                        if (jObj[__row1]["name_1"] != null) {
                            __value_name_1 = jObj[__row1]["name_1"].ToString().Replace("\"", string.Empty);
                        }
                        if (jObj[__row1]["name_2"] != null)
                        {
                            __value_name_2 = jObj[__row1]["name_2"].ToString().Replace("\"", string.Empty);
                        }
                        if (jObj[__row1]["unitType"] != null)
                        {
                            __value_unit_type = jObj[__row1]["unitType"].ToString().Replace("\"", string.Empty);
                        }
                        if (jObj[__row1]["costType"] != null)
                        {
                            __value_cost_type = jObj[__row1]["costType"].ToString().Replace("\"", string.Empty);
                        }
                        if (jObj[__row1]["unitStandard"] != null)
                        {
                            __value_unit_standard = jObj[__row1]["unitStandard"].ToString().Replace("\"", string.Empty);
                        }
                        if (jObj[__row1]["unitCost"] != null)
                        {
                            __value_unit_cost = jObj[__row1]["unitCost"].ToString().Replace("\"", string.Empty);
                        }

                        StringBuilder __myqueryinsert = new StringBuilder();
                        __myqueryinsert.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        __myqueryinsert.Append(MyLib._myUtil._convertTextToXmlForQuery("INSERT INTO ic_inventory (code, name_1, name_2,unit_type,cost_type,unit_standard,unit_cost) VALUES ('" + __value_code + "',' " + __value_name_1 + "',' " + __value_name_2 + "'," + __value_unit_type + ", " + __value_cost_type + ",' " + __value_unit_standard + "',' " + __value_unit_cost + "')")); 
                        __myqueryinsert.Append("</node>");
                        string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myqueryinsert.ToString());
                        if (result.Length == 0)
                        {
                            this._setlog("INSERT ic_inventory :" + __value_code + ":" + __value_name_1 + " success !!");
                        }
                        else
                        {
                            this._setlog("INSERT ic_inventory :" + __value_code + ":" + __value_name_1 + " error :" + result);
                        }


                        JsonValue __objunitUse = jObj[__row1]["unitUse"];
                        if (__objunitUse.Count > 0)
                        {

                            JsonValue __objunitUse2 = __objunitUse;
                          //  string __value_unitUse_roworder = "";
                            string __value_unitUse_code = "";
                            string __value_unitUse_ic_code = "";
                            string __value_unitUse_standvalue = "";
                            string __value_unitUse_dividevalue = "";
                            string __value_unitUse_roworder1 = "";

                            for (int __unitUserow1 = 0; __unitUserow1 < __objunitUse2.Count; __unitUserow1++)
                            {
                                //  __value_unitUse_roworder = __objunitUse2[__unitUserow1]["roworder"].ToString().Replace("\"", string.Empty);
                                if (__objunitUse2[__unitUserow1]["code"] != null) {
                                    __value_unitUse_code = __objunitUse2[__unitUserow1]["code"].ToString().Replace("\"", string.Empty);
                                }
                                if (__objunitUse2[__unitUserow1]["ic_code"] != null)
                                {
                                    __value_unitUse_ic_code = __objunitUse2[__unitUserow1]["ic_code"].ToString().Replace("\"", string.Empty);
                                }
                                if (__objunitUse2[__unitUserow1]["standvalue"] != null)
                                {
                                    __value_unitUse_standvalue = __objunitUse2[__unitUserow1]["standvalue"].ToString().Replace("\"", string.Empty);
                                }
                                if (__objunitUse2[__unitUserow1]["dividevalue"] != null)
                                {
                                    __value_unitUse_dividevalue = __objunitUse2[__unitUserow1]["dividevalue"].ToString().Replace("\"", string.Empty);
                                }
                                if (__objunitUse2[__unitUserow1]["roworder1"] != null)
                                {
                                    __value_unitUse_roworder1 = __objunitUse2[__unitUserow1]["roworder1"].ToString().Replace("\"", string.Empty);
                                }

                                StringBuilder __myqueryinsertunitUse = new StringBuilder();
                                __myqueryinsertunitUse.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                __myqueryinsertunitUse.Append(MyLib._myUtil._convertTextToXmlForQuery("INSERT INTO ic_unit_use (code, ic_code, stand_value,divide_value,row_order) VALUES ('" + __value_unitUse_code + "',' " + __value_unitUse_ic_code + "', " + __value_unitUse_standvalue + ", " + __value_unitUse_dividevalue + ", " + __value_unitUse_roworder1 + ")"));
                                __myqueryinsertunitUse.Append("</node>");
                                string resultunitUse = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myqueryinsertunitUse.ToString());
                                if (resultunitUse.Length == 0)
                                {
                                    this._setlog("INSERT ic_unit_use :" + __value_unitUse_code + ":" + __value_unitUse_ic_code + " success !!");
                                }
                                else
                                {
                                    this._setlog("INSERT ic_unit_use :" + __value_unitUse_code + ":" + __value_unitUse_ic_code + " error :" + result);
                                }

                            }

                        }

                        JsonValue __objbarcode = jObj[__row1]["barcode"];
                        if (__objbarcode.Count > 0)
                        {
                            JsonValue __objbarcode2 = __objbarcode;
                            string __value_barcode_barcode = "";
                            string __value_barcode_ic_code = "";
                            string __value_barcode_unitCode = "";
                            for (int __barcoderow1 = 0; __barcoderow1 < __objbarcode2.Count; __barcoderow1++)
                            {
                                if (__objbarcode2[__barcoderow1]["barcode"] != null) {
                                    __value_barcode_barcode = __objbarcode2[__barcoderow1]["barcode"].ToString().Replace("\"", string.Empty);
                                }
                                if (__objbarcode2[__barcoderow1]["ic_code"] != null)
                                {
                                    __value_barcode_ic_code = __objbarcode2[__barcoderow1]["ic_code"].ToString().Replace("\"", string.Empty);
                                }
                                if (__objbarcode2[__barcoderow1]["unitCode"] != null)
                                {
                                    __value_barcode_unitCode = __objbarcode2[__barcoderow1]["unitCode"].ToString().Replace("\"", string.Empty);
                                }


                                StringBuilder __myqueryinsertbarcode = new StringBuilder();
                                __myqueryinsertbarcode.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                __myqueryinsertbarcode.Append(MyLib._myUtil._convertTextToXmlForQuery("INSERT INTO ic_inventory_barcode (barcode, ic_code, unit_code) VALUES ('" + __value_barcode_barcode + "',' " + __value_barcode_ic_code + "',' " + __value_barcode_unitCode + "')"));
                                __myqueryinsertbarcode.Append("</node>");
                                string resultbarcode = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myqueryinsertbarcode.ToString());
                                if (resultbarcode.Length == 0)
                                {
                                    this._setlog("INSERT ic_inventory_barcode :" + __value_barcode_barcode + ":" + __value_barcode_ic_code + " success !!");
                                }
                                else
                                {
                                    this._setlog("INSERT ic_inventory_barcode :" + __value_barcode_barcode + ":" + __value_barcode_ic_code + " error :" + result);
                                }

                            }
                        }

                        JsonValue __objdetail = jObj[__row1]["detail"];
                        if (__objdetail.Count > 0)
                        {
                            JsonValue __objdetail2 = __objdetail;
                            string __value_detail_ic_code = "";
                            string __value_detail_start_purchase_unit = "";

                            if (__objdetail2["ic_code"] != null)
                            {
                                __value_detail_ic_code = __objdetail2["ic_code"].ToString().Replace("\"", string.Empty);
                            }
                            if (__objdetail2["start_purchase_unit"] != null)
                            {
                                __value_detail_start_purchase_unit = __objdetail2["start_purchase_unit"].ToString().Replace("\"", string.Empty);
                            }


                            StringBuilder __myqueryinsertdetail = new StringBuilder();
                            __myqueryinsertdetail.Append(MyLib._myGlobal._xmlHeader + "<node>");
                            __myqueryinsertdetail.Append(MyLib._myUtil._convertTextToXmlForQuery("INSERT INTO ic_inventory_detail (ic_code, start_purchase_unit) VALUES ('" + __value_detail_ic_code + "',' " + __value_detail_start_purchase_unit + "')"));
                            __myqueryinsertdetail.Append("</node>");
                            string resultdetail = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myqueryinsertdetail.ToString());
                            if (resultdetail.Length == 0)
                            {
                                this._setlog("INSERT ic_inventory_detail :" + __value_detail_ic_code + ":" + __value_detail_start_purchase_unit + " success !!");
                            }
                            else
                            {
                                this._setlog("INSERT ic_inventory_detail :" + __value_detail_ic_code + ":" + __value_detail_start_purchase_unit + " error :" + result);
                            }
                        }

                    }


                       this._setlog("INSERT " + this.tableName + " success !!" + __today);
                }
                catch (Exception __ex)
                {
                    this._setlog("process import " + this.tableName + " error :" + __ex);
                }

            }

        }








    }
}
