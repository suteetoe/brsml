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

        public int vat_type { get; set; }
        public int inquiry_type { get; set; }
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
            StringBuilder sqlTrans = new StringBuilder();
            sqlTrans.Append(
                "INSERT INTO ic_trans(creator_code, trans_flag, trans_type" +
                ", doc_no, doc_date, cust_code, sale_code" +
                ", total_value, total_after_vat, total_amount, total_before_vat, total_discount, total_except_vat, total_vat_value, vat_rate" +
                ", is_cancel, doc_time, tax_doc_date, tax_doc_no" +
                ", inquiry_type, vat_type, doc_format_code" +
                ", wh_from, location_from, wh_to, location_to, send_type" +
                ", doc_ref, doc_ref_date , branch_code, last_status, credit_day"
                    + ", credit_date, remark) ");
            sqlTrans.Append(
                " VALUES (\'?\', ?, ?" +
                ", \'?\', \'?\', \'?\', \'?\'" +
                ", ?, ?, ?, ?, ?, ?, ?, ?" +
                ", ?, \'?\', \'?\', \'?\'" +
                ", ?, ?, \'?\' " +
                ", \'?\', \'?\', \'?\', \'?\', ?" +
                ", \'?\', ?, \'?\', ?, ?"
                    + ", ?, \'?\') "
                    );





            StringBuilder sqlDetail = new StringBuilder();
            sqlDetail.Append("INSERT INTO ic_trans_detail (" +
                "trans_flag, trans_type" +
                ", doc_no, doc_date, item_code, line_number, is_permium, unit_code, wh_code, shelf_code,"
                    + "qty, price, price_exclude_vat, sum_amount, discount_amount, total_vat_value, tax_type, vat_type"
                    + ", doc_time, calc_flag, sum_amount_exclude_vat"
                    + ", wh_code_2, shelf_code_2, branch_code"
                    + ", inquiry_type, last_status, discount, cust_code)");
            sqlDetail.Append(" VALUES (" +
                "?, ?" +
                ", \'?\', \'?\', \'?\', ?, ?, \'?\', \'?\', \'?\'" +
                ", ?, ?, ?, ?, ?, ?, ?, ?" +
                ", \'?\', ?, ?" +
                ", \'?\', \'?\', \'?\'" +
                ", ?, ?, \'?\', \'?\') ");
            

            StringBuilder __queryInsert = new StringBuilder();


            // ic_trans
            __queryInsert.Append(MyLib._myUtil._convertTextToXmlForQuery(String.Format(sqlTrans.ToString()
                , MyLib._myGlobal._userCode, 12, 1
                , this.doc_no, this.doc_date, this.cust_code, ""
                , this.total_value, this.total_after_vat, this.total_amount, this.total_before_vat, total_discount, total_except_vat, total_vat_value, vat_rate
                0, doc_time, tax_doc_date, tax_doc_no
                , inquiry_type, vat_type, doc_format_code
                , wh_from, location_from, "", "", 0
                , BILLINGDOCNO, "null", MyLib._myGlobal._branchCode, 0, credit_day
                , ((credit_date.Length > 0) ? "\'" + credit_date + "\'" : "null"), remark
                )));

            // __queryInsert.Append(MyLib._myUtil._convertTextToXmlForQuery());

            // detail
            for (int __row = 0; __row < this.details.Count; __row++)
            {
                transdatadetailsap detail = details[__row];

                __queryInsert.Append(MyLib._myUtil._convertTextToXmlForQuery(String.Format(sqlDetail.ToString()
                    , 12, 1
                    , doc_no, doc_date, detail.item_code, __row, detail.is_permium, detail.SALESUNIT, wh_from, location_from
                    , detail.qty, detail.price, detail.price_exclude_vat, detail.sum_amount, detail.discount_amount, detail.total_vat_value, detail.tax_type, detail.vat_type
                    , doc_time, 1, detail.sum_amount_exclude_vat
                    , "", "", MyLib._myGlobal._branchCode
                    , inquiry_type, 0, detail.discount_amount.ToString(), ap_code

                    )));
            }
            return __queryInsert.ToString();
        }
    }
}
