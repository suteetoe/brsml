using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Json;
using System.Text;
using System.Windows.Forms;

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
        public string branch_code { get; set; }

        public int branch_type { get; set; }
        public string BRANCH_CODE { get; set; }




        //edi
        //tran
        public string doc_ref { get; set; }
        public string sale_code { get; set; }
        public string send_date { get; set; }
        public string ref_doc_type { get; set; }

        //shipment
        public string transport_name { get; set; }
        public string transport_address { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }

        //additional
        public string deliveryday { get; set; }
        public string ssfquantity { get; set; }
        public string cust_id { get; set; }
        public string web_order_id { get; set; }
        public string district1_name { get; set; }
        public string district2_name { get; set; }
        public string province_name { get; set; }
        public string custlat { get; set; }
        public string custlng { get; set; }
        public string custbusinesstel { get; set; }
        public string orderdetail { get; set; }
        public string ordertype { get; set; }
        public string firstorder { get; set; }
        public string orderpaymenttype { get; set; }
        public string deliveryaddressid { get; set; }
        public string orderdeliverydate { get; set; }
        public string custrefcode { get; set; }
        public string paymentdate { get; set; }
        public string orderdate { get; set; }











        public transdatasap()
        {

        }

        public void check()
        {
            //this.doc_date = MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDateToString(MyLib._myGlobal._workingDate, true));
            //this.doc_time = DateTime.Now.Hour.ToString("D2") + ":" + DateTime.Now.Minute.ToString("D2");

            this.doc_date = this.tax_doc_date;
            this.doc_time = "06:00";
            this.doc_no = this.Agentcode + "-" + this.BILLINGDOCNO;
            this.cust_code = this.ap_code;
            this.total_discount = ((this.total_discount != null) ? this.total_discount : "0");
            this.credit_day = ((this.PAYMENTTERM_CAL != null) ? "\'" + this.PAYMENTTERM_CAL + "\'" : "null");
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
        public static transdatasap ParseEDIText(string __text_in_line)
        {
            string Header = __cutstring(__text_in_line, "Header", 1, 6);
            string PO_Type = __cutstring(__text_in_line, "PO_Type", 7, 1);
            string Order_Number = __cutstring(__text_in_line, "Order_Number", 8, 15);
            string Order_Date = __cutstring(__text_in_line, "Order_Date", 23, 8);
            string Delivery_Date = __cutstring(__text_in_line, "Delivery_Date", 31, 12);
            string Customer_Number = __cutstring(__text_in_line, "Customer_Number", 43, 14);
            string Customer_Name = __cutstring(__text_in_line, "Customer_Name", 57, 50);
            string Customer_Address = __cutstring(__text_in_line, "Customer_Address", 107, 100);
            string Ship_to_Code = __cutstring(__text_in_line, "Ship_to_Code", 207, 14);
            string Ship_to_Name = __cutstring(__text_in_line, "Ship_to_Name", 221, 50);
            string Supplier_Code = __cutstring(__text_in_line, "Supplier_Code", 271, 14);
            string Supplier_Name = __cutstring(__text_in_line, "Supplier_Name", 285, 50);
            string Supplier_Address = __cutstring(__text_in_line, "Supplier_Address", 335, 100);
            string Bill_To_Customer_Number = __cutstring(__text_in_line, "Bill_To_Customer_Number", 435, 14);
            string MAIL_PROMOTION_NUMBER = __cutstring(__text_in_line, "MAIL_PROMOTION_NUMBER", 449, 10);
            string Payment_eriod = __cutstring(__text_in_line, "Payment_eriod", 459, 3);
            string Discount_Percent1 = __cutstring(__text_in_line, "Discount_Percent1", 462, 5);
            string Discount_Amount1 = __cutstring(__text_in_line, "Discount_Amount1", 467, 15);
            string Discount_Percent2 = __cutstring(__text_in_line, "Discount_Percent2", 482, 5);
            string Discount_Amount2 = __cutstring(__text_in_line, "Discount_Amount2", 487, 15);
            string Discount_Percent3 = __cutstring(__text_in_line, "Discount_Percent3", 502, 5);
            string Discount_Amount3 = __cutstring(__text_in_line, "Discount_Amount3", 507, 15);
            string Gross_Amount = __cutstring(__text_in_line, "Gross_Amount", 522, 17);
            string Net_Amount = __cutstring(__text_in_line, "Net_Amount", 539, 17);
            string Total_Amount = __cutstring(__text_in_line, "Total_Amount", 556, 17);
            string Discount_Amount = __cutstring(__text_in_line, "Discount_Amount", 573, 17);
            string Free_Text = __cutstring(__text_in_line, "Free_Text", 590, 100);

            Console.WriteLine("--------หัว-----------");
            Console.WriteLine(Header);
            Console.WriteLine(PO_Type);
            Console.WriteLine(Order_Number);
            Console.WriteLine(Order_Date);
            Console.WriteLine(Delivery_Date);
            Console.WriteLine(Customer_Number);
            Console.WriteLine(Customer_Name);
            Console.WriteLine(Customer_Address);
            Console.WriteLine(Ship_to_Code);
            Console.WriteLine(Ship_to_Name);
            Console.WriteLine(Supplier_Code);
            Console.WriteLine(Supplier_Name);
            Console.WriteLine(Supplier_Address);
            Console.WriteLine(Bill_To_Customer_Number);
            Console.WriteLine(MAIL_PROMOTION_NUMBER);
            Console.WriteLine(Payment_eriod);
            Console.WriteLine(Discount_Percent1);
            Console.WriteLine(Discount_Amount1);
            Console.WriteLine(Discount_Percent2);
            Console.WriteLine(Discount_Amount2);
            Console.WriteLine(Discount_Percent3);
            Console.WriteLine(Discount_Amount3);
            Console.WriteLine(Gross_Amount);
            Console.WriteLine(Net_Amount);
            Console.WriteLine(Total_Amount);
            Console.WriteLine(Discount_Amount);
            Console.WriteLine(Free_Text);
            Console.WriteLine("---------------------");
            transdatasap _transdatasap = new transdatasap();

            _transdatasap.doc_no = "EDI"+Order_Number;
            _transdatasap.doc_date = Order_Date;
            _transdatasap.cust_code = Customer_Number;
            _transdatasap.doc_time = "";
            _transdatasap.doc_ref = Order_Number;
            _transdatasap.tax_doc_no = "";
            _transdatasap.tax_doc_date = "";
            _transdatasap.sale_code = "EDI";
            _transdatasap.cust_code = "";
            _transdatasap.vat_rate = _g.g._companyProfile._vat_rate.ToString();
            _transdatasap.total_value = Total_Amount;
            _transdatasap.send_date = Delivery_Date;
            _transdatasap.discount_word = ((Discount_Amount1.Length > 0) ? Discount_Amount1 : "")+((Discount_Amount2.Length > 0) ? ","+Discount_Amount2 : "") + ((Discount_Amount3.Length > 0) ? "," + Discount_Amount3 : "");
            _transdatasap.total_discount = Discount_Amount;
            _transdatasap.total_before_vat = "";
            _transdatasap.total_vat_value = "";
            _transdatasap.total_after_vat = "";
            _transdatasap.total_except_vat = "";
            _transdatasap.total_amount = Total_Amount;
            _transdatasap.ref_doc_type = "EDI";

            //shipment
            _transdatasap.transport_name = Ship_to_Code;
            _transdatasap.transport_address = Ship_to_Name;
            _transdatasap.latitude = "";
            _transdatasap.longitude = "";

            //additional
            _transdatasap.deliveryday = "";
            _transdatasap.ssfquantity = "";
            _transdatasap.cust_id = "";
            _transdatasap.cust_code = "";
            _transdatasap.web_order_id = "";
            _transdatasap.district1_name = "";
            _transdatasap.district2_name = "";
            _transdatasap.province_name = "";
            _transdatasap.custlat = "";
            _transdatasap.custlng = "";
            _transdatasap.custbusinesstel = "";
            _transdatasap.orderdetail = "";
            _transdatasap.ordertype = "";
            _transdatasap.firstorder = "";
            _transdatasap.orderpaymenttype = "";
            _transdatasap.deliveryaddressid = "";
            _transdatasap.orderdeliverydate = "";
            _transdatasap.custrefcode = "";
            _transdatasap.paymentdate = "";
            _transdatasap.orderdate = "";

            return _transdatasap;

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
                    + ", credit_date, remark,is_lock_record) ");
            sqlTrans.Append(
                " VALUES (\'{0}\', {1}, {2}" +
                ", \'{3}\', \'{4}\', \'{5}\', \'{6}\'" +
                ", {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}" +
                ", {15}, \'{16}\', \'{17}\', \'{18}\'" +
                ", {19}, {20}, \'{21}\' " +
                ", \'{22}\', \'{23}\', \'{24}\', \'{25}\', {26}" +
                ", \'{27}\', {28}, \'{29}\', {30}, {31}"
                    + ", {32}, \'{33}\',1) "
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
                "vat_total_amount, vat_except_amount_1, vat_average, vat_type, is_add,  manual_add, line_number,branch_type,branch_code)"
                );
            sqlgj.Append("values(" +
                " '', 1, 2, 12, '{0}', '{1}', '{2}', null, null, null, null, '{3}', '{4}'," +
                "null, {5}, {6}, null, null, {7}, {8}, {9},{10},{11}, {12}, {13}, {14}, {15}, {16}, {17},'{18}'" +
                " ) "
                );



            StringBuilder __queryInsert = new StringBuilder();
            string vat_effective_period = tax_doc_date.ToString().Substring(5, 2);

            if (vat_effective_period != "10")
            {
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
                 , BILLINGDOCNO, "null", branch_code, 0, credit_day
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
                    , "", "", branch_code
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
            if (this.BRANCH_CODE.Equals("00000"))
            {
                this.branch_type = 0;
            }
            else {
                this.branch_type = 1;
            }
            __queryInsert.Append(MyLib._myUtil._convertTextToXmlForQuery(String.Format(sqlgj.ToString()
            , this.doc_date, this.doc_no, this.cust_code, this.tax_doc_date, this.tax_doc_no
            , vat_effective_period, MyLib._myGlobal._intPhase(this.tax_doc_date.ToString().Substring(0, 4))+543, this.total_before_vat, this.vat_rate, this.total_vat_value
            , this.total_after_vat, this.total_except_vat, "0", this.vat_type, "0", "0", "0",this.branch_type, this.BRANCH_CODE
            )));


            __queryInsert.Append(MyLib._myUtil._convertTextToXmlForQuery("update gl_journal_vat_buy set "
              + " ap_name = (select name_1 from ap_supplier where ap_supplier.code = '" + ap_code + "' ) "
              + ", tax_no = (select tax_id from ap_supplier_detail where ap_supplier_detail.ap_code = '" + ap_code + "') "
              //+ ", branch_type = (select branch_type from ap_supplier_detail where ap_supplier_detail.ap_code = '" + ap_code + "') "
              //+ ", branch_code = (select branch_code from ap_supplier_detail where ap_supplier_detail.ap_code = '" + ap_code + "') "
              + " where doc_no = \'" + doc_no + "\' "));

            return __queryInsert.ToString();
        }
        public static string __cutstring(string data, string name, int start, int Length)
        {
            string value = "";
            start = start - 1;
            try
            {
                if (Length < data.Length)
                {
                    value = data.Substring(start, Length);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ผิดพลาดการนำเข้าห้อมูล " + name + "ที่เริ่มจาก" + start.ToString() + "ทั้งหมด " + Length.ToString() + "ตัวอักษร", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return value.Trim();
        }

        public string _queryInsertEdi()
        {
            StringBuilder __queryInsert = new StringBuilder();
            __queryInsert.Append(MyLib._myUtil._convertTextToXmlForQuery(String.Format("  delete from ic_trans where doc_no = '" + this.doc_no + "' and trans_flag = 36")));
            __queryInsert.Append(MyLib._myUtil._convertTextToXmlForQuery(String.Format("  delete from ic_trans_detail where doc_no = '" + this.doc_no + "' and trans_flag = 36")));
            __queryInsert.Append(MyLib._myUtil._convertTextToXmlForQuery(String.Format("  delete from gl_journal_vat_sale where doc_no = '" + this.doc_no + "' and trans_flag = 36")));
            __queryInsert.Append(MyLib._myUtil._convertTextToXmlForQuery(String.Format("  delete from ic_trans_shipment where doc_no = '" + this.doc_no + "' and trans_flag = 36")));
            __queryInsert.Append(MyLib._myUtil._convertTextToXmlForQuery(String.Format("  delete from ic_trans_additional where doc_no = '" + this.doc_no + "' and trans_flag = 36")));

            // ic_trans
            StringBuilder sqlTrans = new StringBuilder();
            sqlTrans.Append(
                "INSERT INTO ic_trans(" +
                "trans_flag, trans_type, doc_no, doc_date, doc_time,doc_ref," +
                " tax_doc_no, tax_doc_date, sale_code, cust_code,inquiry_type," +
                "  vat_type, vat_rate,total_value, send_date,discount_word," +
                "  total_discount, total_before_vat, total_vat_value, total_after_vat,total_except_vat," +
                "  total_amount, ref_doc_type) ");
            sqlTrans.Append(
                " VALUES (\'{0}\', {1}, {2},\'{3}\', \'{4}\',\'{5}\'" +
                ",\'{6}\', {7}, {8},\'{9}\', \'{10}\'" +
                ",\'{11}\', {12}, {13},\'{14}\', \'{15}\'" +
                ",\'{16}\', {17}, {18},\'{19}\', \'{20}\'" +
                ",\'{21}\', {22}" +
                ")"
                );

            __queryInsert.Append(MyLib._myUtil._convertTextToXmlForQuery(String.Format(sqlTrans.ToString()
                 , 36, 2, this.doc_no, this.doc_date, this.doc_time, this.doc_ref
                 , this.tax_doc_no, this.tax_doc_date, this.sale_code , this.cust_code, 0
                 , 0, this.vat_rate, this.total_value, this.send_date, this.discount_word
                 , this.total_discount, this.total_before_vat, this.total_vat_value, this.total_after_vat, this.total_except_vat
                 , this.total_amount, this.ref_doc_type
                 )));



            //shipment
            StringBuilder sqlshipment = new StringBuilder();
            sqlshipment.Append(
                "INSERT INTO ic_trans_shipment(" +
                "doc_no, doc_date, trans_flag, transport_name, transport_address, latitude, longitude");
            sqlshipment.Append(
                " VALUES (\'{0}\', {1}, {2},\'{3}\', \'{4}\',\'{5}\',{6})"
                );

            __queryInsert.Append(MyLib._myUtil._convertTextToXmlForQuery(String.Format(sqlshipment.ToString()
            , this.doc_no, this.doc_date, 36, this.transport_name, this.transport_address, this.latitude, this.longitude
             )));


            //additional
            StringBuilder sqladditional = new StringBuilder();
            sqladditional.Append(
                "INSERT INTO ic_trans_additional(" +
                "doc_no, trans_flag, deliveryday, ssfquantity, cust_id, custcode," +
                "web_order_id, district1_name, district2_name, province_name, custlat," +
                "custlng, custbusinesstel, orderdetail, ordertype, firstorder," +
                " orderpaymenttype, deliveryaddressid, orderdeliverydate, custrefcode, paymentdate," +
                " orderdate");
            sqladditional.Append(
                " VALUES (\'{0}\', {1}, {2},\'{3}\', \'{4}\',\'{5}\'" +
                ",{6},{7},{8},{9},{10}" +
                ",{11},{12},{13},{14},{15}" +
                ",{16},{17},{18},{19},{20}" +
                ",{21})"
                );

            __queryInsert.Append(MyLib._myUtil._convertTextToXmlForQuery(String.Format(sqladditional.ToString()
            , this.doc_no, 36, this.deliveryday, this.ssfquantity, this.cust_id, this.cust_code
            , this.web_order_id, this.district1_name, this.district2_name, this.province_name, this.custlat
            , this.custlng, this.custbusinesstel, this.orderdetail, this.ordertype, this.firstorder
            , this.orderpaymenttype, this.deliveryaddressid, this.orderdeliverydate, this.custrefcode, this.paymentdate
            , this.orderdate

             )));



            //detail
            StringBuilder sqlDetail = new StringBuilder();
            sqlDetail.Append("INSERT INTO ic_trans_detail (" +
                "trans_flag, trans_type, doc_no, doc_date, doc_time, item_code," +
                "line_number,unit_code, qty, wh_code, shelf_code, price," +
                "price_exclude_vat, sum_amount, discount_amount, discount, total_vat_value," +
                "tax_type, vat_type, item_name" +
                ")");
            sqlDetail.Append(
                   " VALUES (\'{0}\', {1}, {2},\'{3}\', \'{4}\',\'{5}\'" +
                   ",{6},{7},{8},{9},{10}" +
                   ",{11},{12},{13},{14},{15}" +
                   ",{16},{17},{18})"
            );

            for (int __row = 0; __row < this.details.Count; __row++)
            {
                transdatadetailsap detail = details[__row];

                __queryInsert.Append(MyLib._myUtil._convertTextToXmlForQuery(String.Format(sqlDetail.ToString()
                    , 36, 2, this.doc_no, this.doc_date, this.doc_time, detail.item_code
                    , detail.line_number, detail.unit_code, detail.qty, detail.wh_code, detail.shelf_code, detail.price
                    , detail.price_exclude_vat, detail.sum_amount, detail.discount_amount, detail.discount, detail.total_vat_value
                    , detail.qty, detail.price, detail.price_exclude_vat, detail.sum_amount, detail.discount_amount, detail.total_vat_value, detail.tax_type, detail.vat_type
                    , detail.tax_type, detail.vat_type, detail.item_name

                    )));


                __queryInsert.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans_detail set "
                   + " item_name = (select name_1 from ic_inventory where ic_inventory.code = ic_trans_detail.item_code ) "
                   + ", stand_value = (select stand_value from ic_unit_use where ic_unit_use.code = ic_trans_detail.unit_code and ic_unit_use.ic_code = ic_trans_detail.item_code ) "
                   + ", divide_value = (select divide_value from ic_unit_use where ic_unit_use.code = ic_trans_detail.unit_code and ic_unit_use.ic_code = ic_trans_detail.item_code ) "
                   + ", doc_date_calc = doc_date, doc_time_calc = doc_time"
                   + ", is_serial_number = (select ic_serial_no from ic_inventory where ic_inventory.code = ic_trans_detail.item_code )"
                   + " where doc_no = \'" + doc_no + "\' "));
            }



            return __queryInsert.ToString();
        }




    }
}
