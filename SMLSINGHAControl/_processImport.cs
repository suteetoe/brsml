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



        public IEnumerable<Control> Controls { get; private set; }

        public _processImport(string tableName)
        {
            //synctablename table = (synctablename)syncClass.table_enum;
            this.tableName = tableName;
        }
        public virtual void _process()
        {
            this._loaddata();

            this._preparedata(this._resultObject);

            this._process_import();
        }

        public void _loaddata()
        {
            //_singhaGridGetdata._priceStruct __result = new _singhaGridGetdata._priceStruct();
            Console.WriteLine("Load " + this.tableName);

            WebClient __n = new WebClient();

            // string __url = __urlServerSplit[0] + "http://dev.smlsoft.com:7400/getdb/erp_expenses_list";

            string __getCompanyRestUrl = MyLib._myGlobal._syncMasterUrl + this.tableName; //"http://192.168.2.98:7400/getdb/erp_expenses_list";
            _restClient __rest = new _restClient(__getCompanyRestUrl);
            string __response = __rest.MakeRequest();


            JsonValue __jsonObject = JsonValue.Parse(__response);

            if (__jsonObject.Count > 0)
            {
                this._resultObject = __jsonObject;
            }

        }

        protected virtual void _preparedata(JsonValue jObj)
        {
            DateTime __today = DateTime.Now;


            if (jObj.Count > 0)
            {
                this._setlog("Prepare " + this.tableName + " start time" + __today );


                string __value_code = "";
                string __value_name = "";


                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select code,name_1 from " + this.tableName));
                __myquery.Append("</node>");
                ArrayList _getDatafromquery = _myFrameWork._queryListGetData(_myGlobal._databaseName, __myquery.ToString());
                DataSet getData = (DataSet)_getDatafromquery[0];

                StringBuilder __myqueryinsert = new StringBuilder();
                __myqueryinsert.Append(MyLib._myGlobal._xmlHeader + "<node>");


                this._setlog("จำนวนข้อมูลของเดิมมี : " + getData.Tables[0].Rows.Count + "" );
                for (int __row2 = 0; __row2 < getData.Tables[0].Rows.Count; __row2++)
                {

                    this._setlog("data value :" + getData.Tables[0].Rows[__row2]["code"].ToString() + ":" + getData.Tables[0].Rows[__row2]["name_1"].ToString() );
                }

                this._setlog("จำนวนข้อมูลของ Master มี : " + jObj.Count + "" );
                int count = 0;
                int count2 = 0;
                for (int __row1 = 0; __row1 < jObj.Count; __row1++)
                {
                    __value_code = jObj[__row1]["code"].ToString().Replace("\"", string.Empty);
                    __value_name = jObj[__row1]["name_1"].ToString().Replace("\"", string.Empty);
                    int checkrow = 0;
                    this._setlog("Master value :" + __value_code + ":" + __value_name );
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
                        if (checkrow == 0)
                        {
                            count++;
                            __myqueryinsert.Append(MyLib._myUtil._convertTextToXmlForQuery("INSERT INTO " + this.tableName + " (code, name_1) VALUES ('" + __value_code + "',' " + __value_name + "')"));
                        }

                    }

                }
                __myqueryinsert.Append("</node>");
                this._setlog("ข้อมูลที่ซ้ำกันทั้งหมด : " + count2 + "" );
                this._setlog("ข้อมูลที่นำเข้าทั้งหมด : " + count + "" );
                string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myqueryinsert.ToString());
                if (result.Length == 0)
                {
                    this._setlog("Prepare " + this.tableName + " success !!" + __today );
                }
                else
                {
                    this._setlog("Prepare " + this.tableName + " error :" + result );
                }





            }


        }

        StringBuilder __mylog = new StringBuilder();
        public void _setlog(string log)
        {
            if (_output != null) {
                _output(log);
            }
        }

        protected virtual void _process_import()
        {
            Console.WriteLine("process Import");
        }

        public delegate void _processOutput(string text);
        public event _processOutput _output;


    }
}
