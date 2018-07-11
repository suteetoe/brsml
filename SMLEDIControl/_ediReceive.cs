using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Globalization;
using System.Collections;
using System.Json;
using MyLib;
using Newtonsoft.Json;

namespace SMLEDIControl
{
    public partial class _ediReceive : UserControl
    {
        string _agentCode = "";
        string __url = "http://bs.brteasy.com:8080/SinghaEDI";
        int _transFlag = 36;
        int _transType = 2;
        JsonValue _dataRowObject = null;


        public _ediReceive()
        {
            InitializeComponent();
            this._docGrid._table_name = _g.d.ic_trans._table;
            this._docGrid._addColumn("select", 11, 10, 10);
            this._docGrid._isEdit = false;
            this._docGrid._addColumn(_g.d.ic_trans._doc_no, 1, 90, 90);
            this._docGrid._addColumn("data", 12, 0, 0, false, true);
            this._docGrid.WidthByPersent = true;
            this._docGrid._calcPersentWidthToScatter();
            this._docGrid._mouseClick += _docGrid__mouseClick;


            splitContainer1.Panel2Collapsed = true;
            splitContainer1.Panel2.Hide();

            this.Load += _singhaOnlineOrderImport_Load;
        }

        private void _singhaOnlineOrderImport_Load(object sender, EventArgs e)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __result = __myFrameWork._queryShort("select * from " + _g.d.erp_company_profile._table);

            if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
            {
                this._agentCode = __result.Tables[0].Rows[0][_g.d.erp_company_profile._agent_code].ToString();

            }
            if (_agentCode != "")
            {
                //this._getData();
            }
            else
            {
                MessageBox.Show("ไม่พบ Agent Code ที่ใช้ในการดึงข้อมูล กรุณากำหนด Agent Code ในเมนู 1.1.1 ", "Error  Agent Code is missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void _docGrid__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            // load data to detail
            if (e._row > -1)
            {
                string __docNo = this._docGrid._cellGet(e._row, _g.d.ic_trans._doc_no).ToString();
                //DataRow[] __getRow = this._dataDetail.Select(_g.d.ic_trans_detail._doc_no + "=\'" + __docNo + "\'");
                //this._detailGrid._loadFromDataTable(this._dataDetail, __getRow);
            }
        }

        void _getData()
        {
            try
            {
                this._docGrid._clear();
                // get data from restful server
                WebClient __n = new WebClient();

                string _data = "{\"P_agentcode\":\"" + _agentCode + "\"}";
                //string _data = "{\"P_agentcode\":\"0001014678\"}";
                MyLib._restClient __rest = new _restClient("http://ws-dev.boonrawd.co.th/MasterPaymentAgent/api/SMLHeaderBill", HttpVerb.POST, _data);
                __rest._setContentType("application/json");
                __rest._addHeaderRequest(string.Format("APIKey: {0}", "api_sml"));
                __rest._addHeaderRequest(string.Format("APISecret: {0}", "@p1_$m1@p1_$m1"));
                string __result = __rest.MakeRequest("");
                if (__result.Length > 0)
                {
                    JsonValue __jsonObject = JsonValue.Parse(__result);
                    if (__jsonObject.Count > 0)
                    {
                        JsonValue __resultObject = JsonValue.Parse(__jsonObject["Result"].ToString());
                        JsonValue __lastResulValue = null;
                        for (int __row1 = 0; __row1 < __resultObject.Count; __row1++)
                        {
                            transdata __transdata = transdata.Parse(__resultObject[__row1].ToString());

                            string __BILLINGDOCNO = __resultObject[__row1]["BILLINGDOCNO"].ToString().Replace("\"", string.Empty);


                            //ดึง detail 6002351316
                            // string _datadetail = "{\"P_billingdocno\":\"" + __BILLINGDOCNO + "\",\"P_agentcode\":\"" + _agentCode + "\"}";
                            string _datadetail = "{\"P_billingdocno\":\"6002351316\",\"P_agentcode\":\"" + _agentCode + "\"}";
                            MyLib._restClient __restdetail = new _restClient("http://ws-dev.boonrawd.co.th/MasterPaymentAgent/api/SMLDetailMat", HttpVerb.POST, _datadetail);
                            __restdetail._setContentType("application/json");
                            __restdetail._addHeaderRequest(string.Format("APIKey: {0}", "api_sml"));
                            __restdetail._addHeaderRequest(string.Format("APISecret: {0}", "@p1_$m1@p1_$m1"));
                            string __resultdetail = __restdetail.MakeRequest("");
                            JsonValue __jsonDetailObject = JsonValue.Parse(__resultdetail);
                            JsonArray __resultarray = new JsonArray();
                            JsonObject detail = new JsonObject();
                            detail.Add("detail", __jsonDetailObject["Result"]);
                            string jsondetail = detail.ToString();


                            __transdata.details = new List<transdatadetail>();
                            if (__jsonDetailObject["Result"].Count > 0)
                            {

                                for (int __row = 0; __row1 < __jsonDetailObject["Result"].Count; __row1++)
                                {
                                    transdatadetail __transdatadetail = transdatadetail.Parse(__jsonDetailObject["Result"][__row1].ToString());
                                    __transdata.details.Add(__transdatadetail);
                                }
                            }
                            __transdata.check();

                            //  JsonArray __resultdetailObject = JsonArray.Parse(__jsonDetailObject["Result"].ToString());

                            //transdatadetail __transdatadetail = transdatadetail.Parse(__resultdetailObject[0].ToString());

                            __lastResulValue = __transdata._getJson();
                            // __lastResulValue = __transdata._getJson(__resultObject[__row1].ToString(), __transdatadetail.ToString());
                            // string __BILLINGDOCNO = __lastResulValue["BILLINGDOCNO"].ToString().Replace("\"", string.Empty);


                            int __rowAdd = this._docGrid._addRow();
                            this._docGrid._cellUpdate(__rowAdd, _g.d.ic_trans._doc_no, __BILLINGDOCNO, true);
                            this._docGrid._cellUpdate(__rowAdd, 0, 1, true);
                            this._docGrid._cellUpdate(__rowAdd, "data", __lastResulValue, true);
                        }
                    }

                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error Agent Code :" + _agentCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void _reloadButton_Click(object sender, EventArgs e)
        {
            this._getData();
        }

        private void _importButton_Click(object sender, EventArgs e)
        {
            this._process();
        }

        void _process()
        {
            try
            {
                if (MessageBox.Show("ต้องการนำเข้าข้อมูลที่ได้เลือกไว้หรือไม่", "ยืนยัน", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    for (int __row = 0; __row < this._docGrid._rowData.Count; __row++)
                    {
                        if (this._docGrid._cellGet(__row, 0).ToString().Equals("1"))
                        {
                            try
                            {
                                string _data = this._docGrid._cellGet(__row, 2).ToString();
                                MessageBox.Show(_data);
                                //WebClient __n = new WebClient();
                                //MyLib._restClient __rest = new _restClient("http://ws-dev.boonrawd.co.th/MasterPaymentAgent/api/SMLHeaderBill", HttpVerb.POST, _data);
                                //__rest._setContentType("application/json");
                                //__rest._addHeaderRequest(string.Format("APIKey: {0}", "api_sml"));
                                //__rest._addHeaderRequest(string.Format("APISecret: {0}", "@p1_$m1@p1_$m1"));
                                //string __result = __rest.MakeRequest("");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }
                    } // end loop

                    this._getData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void _selectAllButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._docGrid._rowData.Count; __row++)
            {
                this._docGrid._cellUpdate(__row, 0, 1, true);
            }
            this._docGrid.Invalidate();
        }

        private void _selectNoneButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._docGrid._rowData.Count; __row++)
            {
                this._docGrid._cellUpdate(__row, 0, 0, true);
            }
            this._docGrid.Invalidate();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }

    class transdata
    {
        public string Agentcode { get; set; }
        public string BILLINGDOCNO { get; set; }
        public string doc_no { get; set; }
        public string doc_format_code { get; set; }
        public string doc_date { get; set; }
        public string doc_time { get; set; }
        public string ap_code { get; set; }
        public string tax_doc_date { get; set; }
        public string tax_doc_no { get; set; }
        public string vat_rate { get; set; }
        public string total_value { get; set; }
        public string total_discount { get; set; }
        public string total_before_vat { get; set; }
        public string total_vat_value { get; set; }
        public string total_except_vat { get; set; }
        public string total_after_vat { get; set; }
        public string total_amount { get; set; }
        public List<transdatadetail> details { get; set; }
        public string wh_from { get; set; }
        public string location_from { get; set; }
        public string credit_day { get; set; }
        public string credit_date { get; set; }
        public string remark { get; set; }
        public string discount_word { get; set; }
        public string PAYMENTTERM_CAL { get; set; }
        public string Bat_number { get; set; }
        public string Bat_date { get; set; }
        public string cust_code { get; set; }





        public transdata()
        {

        }
        public string _getJson()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            //JsonValue __resultObject = JsonValue.Parse(json);
            return json;

        }
        public void check()
        {
            this.doc_date = MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDateToString(MyLib._myGlobal._workingDate, true));
            this.doc_time = DateTime.Now.Hour.ToString("D2") + ":" + DateTime.Now.Minute.ToString("D2");
            this.doc_no = this.Agentcode.Substring(3) + "-" + this.BILLINGDOCNO;
            this.cust_code = this.ap_code;
            //this.vat_rate = MyLib._myGlobal._decimalPhase(this.vat_rate.ToString().Replace("%", string.Empty));
            this.vat_rate =this.vat_rate.Replace("%", string.Empty);
        }

        public static transdata Parse(string json)
        {

            if (json != null)
            {
                transdata _transdata = JsonConvert.DeserializeObject<transdata>(json);
                return _transdata;
            }
            return null;
        }
    }

    class transdatadetail
    {
        public string BILLINGDOCNO { get; set; }
        public string Agentcode { get; set; }
        public string MATERIALCODE { get; set; }
        public string line_number { get; set; }
        public string is_permium { get; set; }
        public string SALESUNIT { get; set; }
        public string wh_code { get; set; }
        public string shelf_code { get; set; }
        public string qty { get; set; }
        public string price { get; set; }
        public string price_exclude_vat { get; set; }
        public string discount_amount { get; set; }
        public string sum_amount { get; set; }
        public string vat_amount { get; set; }
        public string tax_type { get; set; }
        public string vat_type { get; set; }

        public transdatadetail()
        {

        }

        public JsonValue _getJson()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            JsonValue __resultObject = JsonValue.Parse(json);
            return __resultObject;
        }
        public static transdatadetail Parse(string json)
        {
            if (json != null)
            {
                transdatadetail _transdatadetail = JsonConvert.DeserializeObject<transdatadetail>(json);
                return _transdatadetail;
            }
            return null;
        }
    }
}
