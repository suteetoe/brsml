using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Json;
using System.Text;

namespace SMLEDIControl
{
    public class transdatasap
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

        public List<transdatadetailsap> details { get; set; }


        public string wh_from { get; set; }
        public string location_from { get; set; }
        public string credit_day { get; set; }
        public string credit_date { get; set; }
        public string remark { get; set; }
        public string discount_word { get; set; }
        public string PAYMENTTERM_CAL { get; set; }
        public string cust_code { get; set; }

        public transdatasap()
        {

        }

        public void check()
        {
            this.doc_date = MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDateToString(MyLib._myGlobal._workingDate, true));
            this.doc_time = DateTime.Now.Hour.ToString("D2") + ":" + DateTime.Now.Minute.ToString("D2");
            this.doc_no = this.Agentcode.Substring(3) + "-" + this.BILLINGDOCNO;
            this.cust_code = this.ap_code;
            this.vat_rate = this.vat_rate.Trim();
        }

        public JsonValue _getJson()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            return json;
        }
        public static transdatasap Parse(string json)
        {
            if (json != null)
            {
                transdatasap _transdatasap = JsonConvert.DeserializeObject<transdatasap>(json);
                return _transdatasap;
            }
            return null;
        }

        public string _queryInsert()
        {
            return "";
        }
    }
}
