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
    public class _processImport
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        _singhaMasterTransfer _singhaMasterTransfer = new _singhaMasterTransfer();
        JsonValue _resultObject = null;
        //   protected string uri = "http://192.168.2.98:7400/getdb/";
        public String tableName = "";
        DateTime __today = DateTime.Now;



        public IEnumerable<Control> Controls { get; private set; }

        public _processImport(string tableName)
        {
            this.tableName = tableName;
        }
        public virtual void _process()
        {
            this._loaddata();
           // this._preparedata(this._resultObject);
            this._process_import(this._preparedata(this._resultObject));

        }

        public void _processfilter(JsonArray _jObj)
        {
            this._process_import(this._preparedata(_jObj[0]));
        }

        protected virtual void _loaddata()
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

        protected virtual JsonArray _preparedata(JsonValue jObj)
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
                    this._setlog("====================================================================================================================================================================================================");
                    this._setlog("Prepare " + this.tableName + " start time" + __today);
                    this._setlog("====================================================================================================================================================================================================");

                    string __value_code = "";
                    string __value_name = "";

                    StringBuilder __myquery = new StringBuilder();
                    __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select code,name_1 from " + this.tableName));
                    __myquery.Append("</node>");
                    ArrayList _getDatafromquery = _myFrameWork._queryListGetData(_myGlobal._databaseName, __myquery.ToString());
                    DataSet getData = (DataSet)_getDatafromquery[0];

                   
                    //this._setlog("===================== จำนวนข้อมูลของเดิมมี : " + getData.Tables[0].Rows.Count + "=====================");
                    //for (int __row2 = 0; __row2 < getData.Tables[0].Rows.Count; __row2++)
                    //{

                    //    this._setlog("data value :" + getData.Tables[0].Rows[__row2]["code"].ToString() + ":" + getData.Tables[0].Rows[__row2]["name_1"].ToString());
                    //}
                    this._setlog("===================== จำนวนข้อมูลของ Master มี : " + jObj.Count + "=====================");
                    int count = 0;
                    int count2 = 0;
                    for (int __row1 = 0; __row1 < jObj.Count; __row1++)
                    {
                        int checkrow = 0;
                        //JsonValue __obj = jObj[__row1];
                        __value_code = _checkjson(jObj[__row1]["code"]);
                        __value_name = _checkjson(jObj[__row1]["name_1"]);
                        //__value_code = jObj[__row1]["code"].ToString().Replace("\"", string.Empty);
                        //__value_name = jObj[__row1]["name_1"].ToString().Replace("\"", string.Empty);
                        //this._setlog("Master value :" + __value_code + ":" + __value_name);
                        if (getData.Tables.Count > 0)
                        {

                            for (int __row2 = 0; __row2 < getData.Tables[0].Rows.Count; __row2++)
                            {
                                if (__value_code.Equals(getData.Tables[0].Rows[__row2]["code"].ToString()))
                                {
                                    count2++;
                                    checkrow++;
                                }

                            }
                        }
                        if (checkrow == 0)
                        {
                            count++;
                            __result.Add(jObj[__row1]);
                        }

                    }

                    this._setlog("ข้อมูลที่ซ้ำกันทั้งหมด : " + count2 + "");
                    this._setlog("ข้อมูลที่นำเข้าทั้งหมด : " + count + "");


                }
            }
            catch (Exception __ex)
            {
                this._setlog("Prepare " + this.tableName + " error :" + __ex);
            }
            return __result;
        }


        protected virtual void _process_import(JsonArray jObj)
        {

            string __value_code = "";
            string __value_name = "";
            string __value_sub_ar_shoptype5 = "";
            string __sub_ar_shoptype5 = "";

            try
            {
                for (int __row1 = 0; __row1 < jObj.Count; __row1++)
                {
                    __value_code = _checkjson(jObj[__row1]["code"]);
                    __value_name = _checkjson(jObj[__row1]["name_1"]);
                    if (this.tableName.Equals("sub_ar_shoptype5")) {
                        __sub_ar_shoptype5 = ",ar_shoptype5_code";
                        __value_sub_ar_shoptype5 = ",'"+_checkjson(jObj[__row1]["ar_shoptype5_code"])+"'";
                    }
                    //__value_code = jObj[__row1]["code"].ToString().Replace("\"", string.Empty);
                    //__value_name = jObj[__row1]["name_1"].ToString().Replace("\"", string.Empty);
                    StringBuilder __myqueryinsert = new StringBuilder();
                    __myqueryinsert.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    __myqueryinsert.Append(MyLib._myUtil._convertTextToXmlForQuery("INSERT INTO " + this.tableName + " (code, name_1"+__sub_ar_shoptype5+") VALUES ('"+__value_code+"','"+__value_name+"'"+__value_sub_ar_shoptype5+")"));
                    __myqueryinsert.Append("</node>");
                    string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myqueryinsert.ToString());
                    if (result.Length == 0)
                    {
                        this._setlog("INSERT " + this.tableName + ":" + __value_code + ":" + __value_name + " success !!");
                    }
                    else
                    {
                        this._setlog("INSERT " + this.tableName + ":" + __value_code + ":" + __value_name + " error :" + result);
                    }
                }
                this._setlog("INSERT " + this.tableName + " success !!" + __today);
            }
            catch (Exception __ex)
            {
                this._setlog("process import " + this.tableName + " error :" + __ex);
            }

        }


        StringBuilder __mylog = new StringBuilder();
        protected virtual void _setlog(string log)
        {
            if (_output != null)
            {
                _output(log);
            }
        }

        protected virtual String _checkjson(JsonValue _getvalue)
        {
            string _value = "";
            if (_getvalue != null)
            {
                _value = _getvalue.ToString().Replace("\"", string.Empty);
            }
            return _value;
        }

        public delegate void _processOutput(string text);
        public event _processOutput _output;


    }
}
