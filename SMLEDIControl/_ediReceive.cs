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
            this._docGrid._addColumn("data", 12, 0, 0, false,true);
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
                this._getData();
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
                            transdata __transdata = new transdata(__resultObject[__row1].ToString());
                            __lastResulValue = __transdata._getJson();
                            string __BILLINGDOCNO = __lastResulValue["BILLINGDOCNO"].ToString().Replace("\"", string.Empty);
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
                                string data = this._docGrid._cellGet(__row, 2).ToString();
                                MessageBox.Show(data);
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
        public string details { get; set; }
        public string wh_from { get; set; }
        public string location_from { get; set; }
        public string credit_day { get; set; }
        public string credit_date { get; set; }
        public string remark { get; set; }
        public string discount_word { get; set; }
        public string PAYMENTTERM_CAL { get; set; }

        public transdata(string json)
        {

            if (json != null)
            {
                transdata _transdata = JsonConvert.DeserializeObject<transdata>(json);
                this.Agentcode = _transdata.Agentcode;
                this.BILLINGDOCNO = _transdata.BILLINGDOCNO;
                this.doc_no = _transdata.doc_no;
                this.doc_format_code = _transdata.doc_format_code;
                this.doc_date = _transdata.doc_date;
                this.doc_time = _transdata.doc_time;
                this.ap_code = _transdata.ap_code;
                this.tax_doc_date = _transdata.tax_doc_date;
                this.tax_doc_no = _transdata.tax_doc_no;
                this.vat_rate = _transdata.vat_rate;
                this.total_value = _transdata.total_value;
                this.total_discount = _transdata.total_discount;
                this.total_before_vat = _transdata.total_before_vat;
                this.total_vat_value = _transdata.total_vat_value;
                this.total_except_vat = _transdata.total_except_vat;
                this.total_after_vat = _transdata.total_after_vat;
                this.total_amount = _transdata.total_amount;
                this.details = _transdata.details;
                this.wh_from = _transdata.wh_from;
                this.location_from = _transdata.location_from;
                this.credit_day = _transdata.credit_day;
                this.credit_date = _transdata.credit_date;
                this.remark = _transdata.remark;
                this.discount_word = _transdata.discount_word;
                this.PAYMENTTERM_CAL = _transdata.PAYMENTTERM_CAL;
            }
        }
        public JsonValue _getJson()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            JsonValue __resultObject = JsonValue.Parse(json);
            return __resultObject;
        }
    }
}
