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
            //this.doc_date = MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDateToString(MyLib._myGlobal._workingDate, true));
            //this.doc_time = DateTime.Now.Hour.ToString("D2") + ":" + DateTime.Now.Minute.ToString("D2");

            this.doc_date = this.tax_doc_date;
            this.doc_time = "06.00";
            this.doc_no = this.Agentcode + "-" + this.BILLINGDOCNO;
            this.cust_code = this.ap_code;
            this.total_discount = ((this.total_discount != null) ? this.total_discount : "0");
            this.credit_day = ((this.credit_day != null) ? "\'" + this.credit_day + "\'" : "null");
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
                " VALUES (\'{0}\', {1}, {2}" +
                ", \'{3}\', \'{4}\', \'{5}\', \'{6}\'" +
                ", {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}" +
                ", {15}, \'{16}\', \'{17}\', \'{18}\'" +
                ", {19}, {20}, \'{21}\' " +
                ", \'{22}\', \'{23}\', \'{24}\', \'{25}\', {26}" +
                ", \'{27}\', {28}, \'{29}\', {30}, {31}"
                    + ", {32}, \'{33}\') "
                    );




            //   values('', 1, 1, 12, '2018-08-03', 'PU18080002', 'AP002', null, null, null, null, '2018-8-3', 'PU18080002', null, 8, 2561, 
            //null, null, 0, 7.00, 0, 0, 0, 0, 0, 0, 'บริษัท ศรีมโหสถ พลาสติก โปรดักส์ จำกัด', null, 1, '002', 0, 0) </ query >


            StringBuilder sqlDetail = new StringBuilder();
            sqlDetail.Append("INSERT INTO ic_trans_detail (" +
                "trans_flag, trans_type" +
                ", doc_no, doc_date, item_code, line_number, is_permium, unit_code, wh_code, shelf_code,"
                    + "qty, price, price_exclude_vat, sum_amount, discount_amount, total_vat_value, tax_type, vat_type"
                    + ", doc_time, calc_flag, sum_amount_exclude_vat"
                    + ", wh_code_2, shelf_code_2, branch_code"
                    + ", inquiry_type, last_status, discount, cust_code, bat_date, bat_number,date_expire)");
            sqlDetail.Append(" VALUES (" +
                "{0}, {1}" +
                ", \'{2}\', \'{3}\', \'{4}\', {5}, {6}, \'{7}\', \'{8}\', \'{9}\'" +
                ", {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}" +
                ", \'{18}\', {19}, {20}" +
                ", \'{21}\', \'{22}\', \'{23}\'" +
                ", {24}, {25}, \'{26}\', \'{27}\', {28}, \'{29}\', {30}) ");


            StringBuilder sqlgj = new StringBuilder();
            sqlgj.Append(
                "insert into gl_journal_vat_buy(" +
                "book_code, vat_calc,trans_type, trans_flag, doc_date, doc_no, ap_code, ref_doc_date, ref_doc_no, ref_vat_date, ref_vat_no, vat_date, vat_doc_no," +
                "vat_new_number, vat_effective_period, vat_effective_year, vat_description, vat_group, vat_base_amount, vat_rate, vat_amount," +
                "vat_total_amount, vat_except_amount_1, vat_average, vat_type, is_add,  manual_add, line_number)"
                );
            sqlgj.Append("values(" +
                " '', 1, 2, 12, '{0}', '{1}', '{2}', null, null, null, null, '{3}', '{4}'," +
                "null, {5}, {6}, null, null, {7}, {8}, {9},{10},{11}, {12}, {13}, {14}, {15}, {16}" +
                " ) "
                );



            StringBuilder __queryInsert = new StringBuilder();
            string vat_effective_period = tax_doc_date.ToString().Substring(5, 2);

            if (vat_effective_period != "10") {
                vat_effective_period.Replace("0", string.Empty);
            }






            // ic_trans
            __queryInsert.Append(MyLib._myUtil._convertTextToXmlForQuery(String.Format(sqlTrans.ToString()
                 , MyLib._myGlobal._userCode, 12, 1
                 , this.doc_no, this.doc_date, this.cust_code, ""
                 , this.total_value, this.total_after_vat, this.total_amount, this.total_before_vat, total_discount, total_except_vat, total_vat_value, vat_rate,
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
                    , doc_no, doc_date, detail.item_code, detail.line_number, detail.is_permium, detail.SALESUNIT, wh_from, location_from
                    , detail.qty, detail.price, detail.price_exclude_vat, detail.sum_amount, detail.discount_amount, detail.total_vat_value, detail.tax_type, detail.vat_type
                    , doc_time, 1, detail.sum_amount_exclude_vat
                    , "", "", MyLib._myGlobal._branchCode
                    , inquiry_type, 0, detail.discount_amount.ToString(), ap_code, ((detail.BAT_DATE != null) ? "\'" + detail.BAT_DATE + "\'" : "null"), detail.BAT_NUMBER, ((detail.date_expire != null) ? "\'" + detail.date_expire + "\'" : "null")
                    )));
                __queryInsert.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans_detail set "
                   + " item_name = (select name_1 from ic_inventory where ic_inventory.code = ic_trans_detail.item_code ) "
                   + ", stand_value = (select stand_value from ic_unit_use where ic_unit_use.code = ic_trans_detail.unit_code and ic_unit_use.ic_code = ic_trans_detail.item_code ) "
                   + ", divide_value = (select divide_value from ic_unit_use where ic_unit_use.code = ic_trans_detail.unit_code and ic_unit_use.ic_code = ic_trans_detail.item_code ) "
                   + ", doc_date_calc = doc_date, doc_time_calc = doc_time"
                   + ", is_serial_number = (select ic_serial_no from ic_inventory where ic_inventory.code = ic_trans_detail.item_code )"
                   + " where doc_no = \'" + doc_no + "\' "));
            }

            //gl
            __queryInsert.Append(MyLib._myUtil._convertTextToXmlForQuery(String.Format(sqlgj.ToString()
            , this.doc_date, this.doc_no, this.cust_code, this.tax_doc_date, this.tax_doc_no
            , vat_effective_period, this.tax_doc_date.ToString().Substring(0, 4), this.total_before_vat, this.vat_rate, this.total_vat_value
            , this.total_after_vat, this.total_except_vat, "0", this.vat_type, "0", "0", "0"
            )));


            __queryInsert.Append(MyLib._myUtil._convertTextToXmlForQuery("update gl_journal_vat_buy set "
              + " ap_name = (select name_1 from ap_supplier where ap_supplier.code = '" + ap_code + "' ) "
              + ", tax_no = (select tax_id from ap_supplier_detail where ap_supplier_detail.ap_code = '" + ap_code + "') "
              + ", branch_type = (select branch_type from ap_supplier_detail where ap_supplier_detail.ap_code = '" + ap_code + "') "
              + ", branch_code = (select branch_code from ap_supplier_detail where ap_supplier_detail.ap_code = '" + ap_code + "') "
              + " where doc_no = \'" + doc_no + "\' "));

            return __queryInsert.ToString();
        }
    }
}
