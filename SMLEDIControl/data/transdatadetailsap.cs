using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Json;
using System.Text;

namespace SMLEDIControl
{
    public class transdatadetailsap
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
        public string BAT_DATE { get; set; }
        public string BAT_NUMBER { get; set; }
        public string date_expire { get; set; }
        public string item_code { get; set; }
        public decimal total_vat_value { get; set; }
        public decimal sum_amount_exclude_vat { get; set; }

        





        public transdatadetailsap()
        {

        }

        public JsonValue _getJson()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            return json;
        }
        public static transdatadetailsap Parse(string json)
        {
            if (json != null)
            {
                transdatadetailsap _transdatadetail = JsonConvert.DeserializeObject<transdatadetailsap>(json);
                return _transdatadetail;
            }
            return null;
        }
    }
}
