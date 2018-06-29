using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLDataAPI
{
    public class _jsondata
    {



        public string _getJson()
        {
            string jsonIgnoreNullValues = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
                //,
                //DefaultValueHandling = DefaultValueHandling.Ignore
            });


            return jsonIgnoreNullValues;
        }

        protected string _getData(DataRow row, string fieldName, Boolean checkNull = true)
        {
            if (row[fieldName].ToString().Length > 0)
            {
                return row[fieldName].ToString();
            }
            else if (checkNull)
            {
                return null;
            }
            else
            {
                return "";
            }

        }

        public DateTime _getDate(string data)
        {
            return MyLib._myGlobal._convertDateFromQuery(data);
        }

        public int _getInt(string data)
        {
            return MyLib._myGlobal._intPhase(data);
        }

        public decimal _getDecimal(string data)
        {
            return MyLib._myGlobal._decimalPhase(data);
        }

    }



    /// <summary>
    /// ลูกหนี้
    /// </summary>
    public class ar_customer : _jsondata
    {
        public ar_customer(DataRow row)
        {

            this.code = _getData(row, "code", false);
            this.code_old = _getData(row, "code_old");
            this.name_1 = _getData(row, "name_1", false);
            this.name_2 = _getData(row, "name_2");
            this.name_eng_1 = _getData(row, "name_eng_1");
            this.name_eng_2 = _getData(row, "name_eng_2");
            this.address = _getData(row, "address");
            this.address_eng = _getData(row, "address_eng");
            this.tambon = _getData(row, "tambon", false);
            this.amper = _getData(row, "amper", false);
            this.province = _getData(row, "province", false);
            this.zip_code = _getData(row, "zip_code");
            this.telephone = _getData(row, "telephone");
            this.fax = _getData(row, "fax");
            this.email = _getData(row, "email");
            this.website = _getData(row, "website");
            this.description = _getData(row, "description");

            if (row["birth_day"].ToString().Length > 0)
            {
                this.birth_day = _getDate(row["birth_day"].ToString());
            }
            this.ar_type = _getData(row, "ar_type", false);
            this.remark = _getData(row, "remark");
            this.status = _getInt(row["status"].ToString());
            this.guid_code = _getData(row, "guid_code");
            this.ar_status = _getInt(row["ar_status"].ToString());
            this.doc_format_code = _getData(row, "doc_format_code");
            this.prefixname = _getData(row, "prefixname");
            this.first_name = _getData(row, "first_name");
            this.last_name = _getData(row, "last_name");
            this.price_level = _getInt(row["price_level"].ToString());
            this.point_balance = _getDecimal(row["point_balance"].ToString());
            this.sale_shift_id = _getData(row, "sale_shift_id");
            this.sms_phonenumber = _getData(row, "sms_phonenumber");
            this.home_address = _getData(row, "home_address");
            this.home_name = _getData(row, "home_name");
            this.moo = _getData(row, "moo");
            this.soi = _getData(row, "soi");
            this.road = _getData(row, "road");
            this.room_no = _getData(row, "room_no");
            this.floor = _getData(row, "floor");
            this.building = _getData(row, "building");
            this.sex = _getInt(row["sex"].ToString());
            this.country = _getData(row, "country");
            if (row["register_date"].ToString().Length > 0)
            {
                this.register_date = _getDate(row["register_date"].ToString());
            }
            this.arm_code = _getData(row, "arm_code");
            this.ar_code_main = _getData(row, "ar_code_main");
            this.ar_branch_code = _getData(row, "ar_branch_code");
            this.arm_approve = _getInt(row["arm_approve"].ToString());
            if (row["arm_approve_date"].ToString().Length > 0)
            {
                this.arm_approve_date = _getDate(row["arm_approve_date"].ToString());
            }
            this.nfc_id = _getData(row, "nfc_id");
            this.arm_tier = _getInt(row["arm_tier"].ToString());
            if (row["create_date_time_now"].ToString().Length > 0)
            {
                this.create_date_time_now = _getDate(row["create_date_time_now"].ToString());
            }
            if (row["last_update_time"].ToString().Length > 0)
            {
                this.last_update_time = _getDate(row["last_update_time"].ToString());
            }
            this.guid = _getData(row, "guid");

            this.interco = _getData(row, "interco");

        }

        public DateTime create_date_time_now { get; set; }
        public string guid { get; set; }


        /// <summary>
        ///รหัส
        /// </summary>
        public string code { get; set; }
        /// <summary>
        ///รหัสเก่า
        /// </summary>
        public string code_old { get; set; }
        /// <summary>
        ///ชื่อ 1
        /// </summary>
        public string name_1 { get; set; }
        /// <summary>
        ///ชื่อ 2
        /// </summary>
        public string name_2 { get; set; }
        /// <summary>
        ///ชื่อ (ภาษาอังกฤษ) 1
        /// </summary>
        public string name_eng_1 { get; set; }
        /// <summary>
        ///ชื่อ (ภาษาอังกฤษ) 2
        /// </summary>
        public string name_eng_2 { get; set; }
        /// <summary>
        ///ที่อยู่
        /// </summary>
        public string address { get; set; }
        /// <summary>
        ///ที่อยู่ (ภาษาอังกฤษ)
        /// </summary>
        public string address_eng { get; set; }
        /// <summary>
        ///ตำบล/แขวง
        /// </summary>
        public string tambon { get; set; }
        /// <summary>
        ///อำเภอ/เขต
        /// </summary>
        public string amper { get; set; }
        /// <summary>
        ///จังหวัด
        /// </summary>
        public string province { get; set; }
        /// <summary>
        ///รหัสไปรษณีย์
        /// </summary>
        public string zip_code { get; set; }
        /// <summary>
        ///หมายเลขโทรศัพท์
        /// </summary>
        public string telephone { get; set; }
        /// <summary>
        ///หมายเลขโทรสาร
        /// </summary>
        public string fax { get; set; }
        /// <summary>
        ///อีเมล์
        /// </summary>
        public string email { get; set; }
        /// <summary>
        ///เวบไซด์
        /// </summary>
        public string website { get; set; }
        /// <summary>
        ///รายละเอียด
        /// </summary>
        public string description { get; set; }
        /// <summary>
        ///วันเกิด/วันที่จดทะเบียน
        /// </summary>
        public DateTime birth_day { get; set; }
        /// <summary>
        ///ประเภทลูกหนี้
        /// </summary>
        public string ar_type { get; set; }
        /// <summary>
        ///หมายเหตุ
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        ///สถานะ
        /// </summary>
        public int status { get; set; }
        /// <summary>
        ///GUID
        /// </summary>
        public string guid_code { get; set; }
        /// <summary>
        ///ใช้งาน
        /// </summary>
        public string active { get; set; }
        /// <summary>
        ///ไม่ใช้งาน
        /// </summary>
        public string inactive { get; set; }
        /// <summary>
        ///ปิด
        /// </summary>
        public string close_credit { get; set; }
        /// <summary>
        ///เปิด
        /// </summary>
        public string open_credit { get; set; }
        /// <summary>
        ///ปิด(ชั่วคราว)
        /// </summary>
        public string stop_credit { get; set; }
        /// <summary>
        ///รายละเอียด
        /// </summary>
        public string tab_detail { get; set; }
        /// <summary>
        ///เครดิต
        /// </summary>
        public string tab_credit { get; set; }
        /// <summary>
        ///การจ่ายเงิน
        /// </summary>
        public string tab_paybill { get; set; }
        /// <summary>
        ///มิติ
        /// </summary>
        public string tab_dimension { get; set; }
        /// <summary>
        ///ติดต่ออ้างอิง
        /// </summary>
        public string tab_contact { get; set; }
        /// <summary>
        ///สินค้า
        /// </summary>
        public string tab_item { get; set; }
        /// <summary>
        ///สถานที่ส่งสินค้า
        /// </summary>
        public string tab_logistic_area { get; set; }
        /// <summary>
        ///กลุ่มเครดิต
        /// </summary>
        public string tab_credit_group { get; set; }
        /// <summary>
        ///รูปภาพ
        /// </summary>
        public string tab_image { get; set; }
        /// <summary>
        ///ชนิดลูกค้า
        /// </summary>
        public int ar_status { get; set; }
        /// <summary>
        ///นิติบุคคล
        /// </summary>
        public string juristic_person { get; set; }
        /// <summary>
        ///บุคคลธรรมดา
        /// </summary>
        public string personality { get; set; }
        /// <summary>
        ///รหัสเอกสาร
        /// </summary>
        public string doc_format_code { get; set; }
        /// <summary>
        ///กลุ่มหลัก
        /// </summary>
        public string group_main { get; set; }
        /// <summary>
        ///คำนำหน้า
        /// </summary>
        public string prefixname { get; set; }
        /// <summary>
        ///ชื่อ
        /// </summary>
        public string first_name { get; set; }
        /// <summary>
        ///นามสกุล
        /// </summary>
        public string last_name { get; set; }
        /// <summary>
        ///รายละเอียดหลัก
        /// </summary>
        public string tab_ar_main { get; set; }
        /// <summary>
        ///รายละเอียดเพิ่มเติม
        /// </summary>
        public string tab_ar_detail { get; set; }
        /// <summary>
        ///ระดับราคาขาย
        /// </summary>
        public int price_level { get; set; }
        /// <summary>
        ///ราคากลาง
        /// </summary>
        public string price_level_0 { get; set; }
        /// <summary>
        ///ราคาที่ 1
        /// </summary>
        public string price_level_1 { get; set; }
        /// <summary>
        ///ราคาที่ 2
        /// </summary>
        public string price_level_2 { get; set; }
        /// <summary>
        ///ราคาที่ 3
        /// </summary>
        public string price_level_3 { get; set; }
        /// <summary>
        ///ราคาที่ 4
        /// </summary>
        public string price_level_4 { get; set; }
        /// <summary>
        ///ราคาที่ 5
        /// </summary>
        public string price_level_5 { get; set; }
        /// <summary>
        ///ราคาที่ 6
        /// </summary>
        public string price_level_6 { get; set; }
        /// <summary>
        ///ราคาที่ 7
        /// </summary>
        public string price_level_7 { get; set; }
        /// <summary>
        ///ราคาที่ 8
        /// </summary>
        public string price_level_8 { get; set; }
        /// <summary>
        ///ราคาที่ 9
        /// </summary>
        public string price_level_9 { get; set; }
        /// <summary>
        ///แต้มสะสม
        /// </summary>
        public decimal point_balance { get; set; }
        /// <summary>
        ///รอบการขาย
        /// </summary>
        public string sale_shift_id { get; set; }
        /// <summary>
        ///หมายเลข SMS
        /// </summary>
        public string sms_phonenumber { get; set; }
        /// <summary>
        ///บ้านเลขที่
        /// </summary>
        public string home_address { get; set; }
        /// <summary>
        ///หมู่บ้าน
        /// </summary>
        public string home_name { get; set; }
        /// <summary>
        ///หมู่
        /// </summary>
        public string moo { get; set; }
        /// <summary>
        ///ซอย
        /// </summary>
        public string soi { get; set; }
        /// <summary>
        ///ถนน
        /// </summary>
        public string road { get; set; }
        /// <summary>
        ///ห้อง
        /// </summary>
        public string room_no { get; set; }
        /// <summary>
        ///ชั้น
        /// </summary>
        public string floor { get; set; }
        /// <summary>
        ///อาคาร
        /// </summary>
        public string building { get; set; }
        /// <summary>
        ///เพศ
        /// </summary>
        public int sex { get; set; }
        /// <summary>
        ///ชาย
        /// </summary>
        public string sex_male { get; set; }
        /// <summary>
        ///หญิง
        /// </summary>
        public string sex_female { get; set; }
        /// <summary>
        ///ประเทศ
        /// </summary>
        public string country { get; set; }
        /// <summary>
        ///วันลงทะเบียน
        /// </summary>
        public DateTime? register_date { get; set; }
        /// <summary>
        ///เลขที่บัตรประชาชน
        /// </summary>
        public string card_id { get; set; }
        /// <summary>
        ///เลขประจำตัวผู้เสียภาษี
        /// </summary>
        public string tax_id { get; set; }
        /// <summary>
        ///สถานประกอบการ
        /// </summary>
        public int branch_type { get; set; }
        /// <summary>
        ///สำนักงานใหญ่
        /// </summary>
        public string headquarters { get; set; }
        /// <summary>
        ///สาขา
        /// </summary>
        public string branch { get; set; }
        /// <summary>
        ///เลขที่สาขา
        /// </summary>
        public string branch_code { get; set; }
        /// <summary>
        ///ARM Code
        /// </summary>
        public string arm_code { get; set; }
        /// <summary>
        ///รหัสลูกค้าหลัก
        /// </summary>
        public string ar_code_main { get; set; }
        /// <summary>
        ///ใช้เฉพาะสาขา
        /// </summary>
        public string ar_branch_code { get; set; }
        /// <summary>
        ///อนุมัติ ARM Code
        /// </summary>
        public int arm_approve { get; set; }
        /// <summary>
        ///วันที่อนุมัติ ARM Code
        /// </summary>
        public DateTime arm_approve_date { get; set; }
        /// <summary>
        ///NFC ID
        /// </summary>
        public string nfc_id { get; set; }
        /// <summary>
        ///Tier
        /// </summary>
        public int arm_tier { get; set; }
        /// <summary>
        ///None
        /// </summary>
        public string tier_0 { get; set; }
        /// <summary>
        ///Silver
        /// </summary>
        public string tier_1 { get; set; }
        /// <summary>
        ///Gold
        /// </summary>
        public string tier_2 { get; set; }
        /// <summary>
        ///Platinum
        /// </summary>
        public string tier_3 { get; set; }
        /// <summary>
        ///Diamond
        /// </summary>
        public string tier_4 { get; set; }
        /// <summary>
        ///วันเวลาทีแก้ไขล่าสุด
        /// </summary>
        public DateTime last_update_time { get; set; }
        /// <summary>
        ///INTERCO
        /// </summary>
        public string interco { get; set; }
        /// <summary>
        ///รูปภาพ
        /// </summary>
        public string tab_ar_picture { get; set; }
        /// <summary>
        ///หนังสือรับรองบริษัท
        /// </summary>
        public string tab_certificate_book { get; set; }
        /// <summary>
        ///ภ.พ.20
        /// </summary>
        public string tab_pp20 { get; set; }
        /// <summary>
        ///บัตรประจำตัวประชาชน
        /// </summary>
        public string tab_Identity_card { get; set; }
        /// <summary>
        ///ใบอนุญาตขายสุรา 
        /// </summary>
        public string tab_liquor_license { get; set; }
        /// <summary>
        ///อื่นๆ 1
        /// </summary>
        public string tab_other_picture1 { get; set; }
        /// <summary>
        ///อื่นๆ 2
        /// </summary>
        public string tab_other_picture2 { get; set; }
        /// <summary>
        ///ลูกค้า
        /// </summary>
        public string tab_customer { get; set; }
        /// <summary>
        ///วันที่สมัคร ARM Code
        /// </summary>
        public DateTime arm_register_date { get; set; }
        /// <summary>
        ///สมัคร อาร์ม
        /// </summary>
        public int arm_register { get; set; }


    }


    /// <summary>
    /// รายละเอียดลูกหนี้
    /// </summary>
    public class ar_customer_detail : _jsondata
    {
        public ar_customer_detail(DataRow row)
        {
            this.ar_code = _getData(row, "ar_code",false);
            this.area_code = _getData(row, "area_code");
            this.sale_code = _getData(row, "sale_code");
            this.logistic_area = _getData(row, "logistic_area");
            this.pay_bill_code = _getData(row, "pay_bill_code");
            this.keep_chq_code = _getData(row, "keep_chq_code");
            this.pay_bill_date = _getInt(row["pay_bill_date"].ToString());
            this.keep_chq_date = _getInt(row["keep_chq_date"].ToString());
            this.sale_price_level = _getInt(row["sale_price_level"].ToString());
            this.credit_sale = _getInt(row["credit_sale"].ToString());
            this.credit_code = _getData(row, "credit_code");
            this.form_name = _getData(row, "form_name");
            this.pay_bill_condition = _getData(row, "pay_bill_condition");
            this.keep_money_condition = _getData(row, "keep_money_condition");
            this.payment_person = _getData(row, "payment_person");
            this.credit_person = _getData(row, "credit_person");
            this.keep_money_person = _getData(row, "keep_money_person");
            this.discount_item = _getData(row, "discount_item");
            this.discount_bill = _getData(row, "discount_bill");
            this.credit_group = _getInt(row["credit_group"].ToString());
            this.credit_group_code = _getData(row, "credit_group_code");
            this.credit_money = _getDecimal(row["credit_money"].ToString());
            this.credit_money_max = _getDecimal(row["credit_money_max"].ToString());
            this.credit_day = _getInt(row["credit_day"].ToString());
            this.credit_reason = _getData(row, "credit_reason");
            this.credit_status = _getInt(row["credit_status"].ToString());
            if (row["credit_date"].ToString().Length > 0)
            {
                this.credit_date = _getDate(row["credit_date"].ToString());
            }
            this.credit_status_reason = _getData(row, "credit_status_reason");
            this.trade_license = _getData(row, "trade_license");
            this.vat_license = _getData(row, "vat_license");
            this.tax_id = _getData(row, "tax_id");
            this.tax_type = _getInt(row["tax_type"].ToString());
            this.tax_rate = _getInt(row["tax_rate"].ToString());
            this.account_code = _getData(row, "account_code");
            this.shipping_type = _getInt(row["shipping_type"].ToString());
            this.close_reason = _getData(row, "close_reason");
            if (row["close_date"].ToString().Length > 0)
            {
                this.credit_date = _getDate(row["close_date"].ToString());
            }
            this.group_main = _getData(row, "group_main");
            this.group_sub_1 = _getData(row, "group_sub_1");
            this.group_sub_2 = _getData(row, "group_sub_2");
            this.group_sub_3 = _getData(row, "group_sub_3");
            this.group_sub_4 = _getData(row, "group_sub_4");
            this.picture_3 = _getData(row, "picture_3");
            this.picture_4 = _getData(row, "picture_4");
            this.ref_doc_1 = _getData(row, "ref_doc_1");
            this.ref_doc_2 = _getData(row, "ref_doc_2");
            this.ref_doc_3 = _getData(row, "ref_doc_3");
            this.dimension_1 = _getData(row, "dimension_1");
            this.dimension_2 = _getData(row, "dimension_2");
            this.dimension_3 = _getData(row, "dimension_3");
            this.dimension_4 = _getData(row, "dimension_4");
            this.dimension_5 = _getData(row, "dimension_5");
            this.currency_code = _getData(row, "currency_code");
            this.latitude = _getDecimal(row["latitude"].ToString());
            this.longitude = _getDecimal(row["longitude"].ToString());
            this.card_id = _getData(row, "card_id");
            this.passbook_code = _getData(row, "passbook_code");
            //this.set_tax_type = _getInt(row["set_tax_type"].ToString());
            this.branch_type = _getInt(row["branch_type"].ToString());
            this.branch_code = _getData(row, "branch_code");
            this.area_paybill = _getData(row, "area_paybill");
            this.reason_disable_credit = _getData(row, "reason_disable_credit");
            if (row["close_credit_date"].ToString().Length > 0)
            {
                this.credit_date = _getDate(row["close_credit_date"].ToString());
            }
            this.close_reason_1 = _getInt(row["close_reason_1"].ToString());
            this.close_reason_2 = _getInt(row["close_reason_2"].ToString());
            this.close_reason_3 = _getInt(row["close_reason_3"].ToString());
            this.close_reason_4 = _getInt(row["close_reason_4"].ToString());
            this.dimension_6 = _getData(row, "dimension_6");
            this.dimension_7 = _getData(row, "dimension_7");
            this.dimension_8 = _getData(row, "dimension_8");
            this.dimension_9 = _getData(row, "dimension_9");
            this.dimension_10 = _getData(row, "dimension_10");
            this.disable_auto_close_credit = _getInt(row["disable_auto_close_credit"].ToString());
            if (row["create_date_time_now"].ToString().Length > 0)
            {
                this.create_date_time_now = _getDate(row["create_date_time_now"].ToString());
            }
            this.ap_code_ref = _getData(row, "ap_code_ref");
            this.sale_type = _getInt(row["sale_type"].ToString());
            this.line_id = _getData(row, "line_id");
            this.facebook = _getData(row, "facebook");
            this.customer_type_code = _getData(row, "customer_type_code");
            this.ar_channel_code = _getData(row, "ar_channel_code");
            this.ar_location_type_code = _getData(row, "ar_location_type_code");
            this.br_cust_code = _getData(row, "br_cust_code");
            this.ar_sub_type_1_code = _getData(row, "ar_sub_type_1_code");
            this.ar_vehicle_code = _getData(row, "ar_vehicle_code");
            this.ar_equipment_code = _getData(row, "ar_equipment_code");
            this.ar_sub_equipment = _getData(row, "ar_sub_equipment");

        }

        public DateTime create_date_time_now { get; set; }
        
        /// <summary>
        ///รหัสลูกค้า
        /// </summary>
        public string ar_code { get; set; }
        /// <summary>
        ///เขตการขาย
        /// </summary>
        public string area_code { get; set; }
        /// <summary>
        ///รหัสพนักงานขาย
        /// </summary>
        public string sale_code { get; set; }
        /// <summary>
        ///เขตการขนส่ง
        /// </summary>
        public string logistic_area { get; set; }
        /// <summary>
        ///รหัสวางบิล
        /// </summary>
        public string pay_bill_code { get; set; }
        /// <summary>
        ///รหัสเก็บเช็ค
        /// </summary>
        public string keep_chq_code { get; set; }
        /// <summary>
        ///วางบิลได้ทุกวันที่
        /// </summary>
        public int pay_bill_date { get; set; }
        /// <summary>
        ///เก็บเช็คได้ทุกวันที่
        /// </summary>
        public int keep_chq_date { get; set; }
        /// <summary>
        ///ระดับราคาขาย
        /// </summary>
        public int sale_price_level { get; set; }
        /// <summary>
        ///ขายเครดิต
        /// </summary>
        public int credit_sale { get; set; }
        /// <summary>
        ///รหัสเครดิต
        /// </summary>
        public string credit_code { get; set; }
        /// <summary>
        ///ชื่อฟอร์มรายลูกค้า
        /// </summary>
        public string form_name { get; set; }
        /// <summary>
        ///เงื่อนไขการวางบิล
        /// </summary>
        public string pay_bill_condition { get; set; }
        /// <summary>
        ///เงื่อนไขการเก็บเงิน
        /// </summary>
        public string keep_money_condition { get; set; }
        /// <summary>
        ///กลุ่มพนักงานวางบิล
        /// </summary>
        public string payment_person { get; set; }
        /// <summary>
        ///กลุ่มพนักงานสินเชื่อ
        /// </summary>
        public string credit_person { get; set; }
        /// <summary>
        ///กลุ่มพนักงานเก็บเงิน
        /// </summary>
        public string keep_money_person { get; set; }
        /// <summary>
        ///ส่วนลดต่อรายการ
        /// </summary>
        public string discount_item { get; set; }
        /// <summary>
        ///ส่วนลดท้ายบิล
        /// </summary>
        public string discount_bill { get; set; }
        /// <summary>
        ///ใช้วงเงินกลุ่ม
        /// </summary>
        public int credit_group { get; set; }
        /// <summary>
        ///รหัสกลุ่มวงเงิน
        /// </summary>
        public string credit_group_code { get; set; }
        /// <summary>
        ///วงเงินเครดิต
        /// </summary>
        public decimal credit_money { get; set; }
        /// <summary>
        ///วงเงินเครดิตสูงสุด
        /// </summary>
        public decimal credit_money_max { get; set; }
        /// <summary>
        ///จำนวนวันเครดิต
        /// </summary>
        public int credit_day { get; set; }
        /// <summary>
        ///เหตุผลลงเครดิต
        /// </summary>
        public string credit_reason { get; set; }
        /// <summary>
        ///สถานะเครดิต
        /// </summary>
        public int credit_status { get; set; }
        /// <summary>
        ///วันที่ลงสถานะเครดิต
        /// </summary>
        public DateTime credit_date { get; set; }
        /// <summary>
        ///เหตุผลลงสถานะเครดิต
        /// </summary>
        public string credit_status_reason { get; set; }
        /// <summary>
        ///ทะเบียนการค้า
        /// </summary>
        public string trade_license { get; set; }
        /// <summary>
        ///ทะเบียนภาษีมูลค่าเพิ่ม
        /// </summary>
        public string vat_license { get; set; }
        /// <summary>
        ///เลขประจำตัวผู้เสียภาษี
        /// </summary>
        public string tax_id { get; set; }
        /// <summary>
        ///ประเภทภาษี
        /// </summary>
        public int tax_type { get; set; }
        /// <summary>
        ///อัตราภาษี
        /// </summary>
        public decimal tax_rate { get; set; }
        /// <summary>
        ///บัญชีลูกค้ารายตัว
        /// </summary>
        public string account_code { get; set; }
        /// <summary>
        ///ประเภทการส่งมอบ
        /// </summary>
        public int shipping_type { get; set; }
        /// <summary>
        ///เหตุผลในการปิด
        /// </summary>
        public string close_reason { get; set; }
        /// <summary>
        ///วันที่ปิด
        /// </summary>
        public DateTime close_date { get; set; }
        /// <summary>
        ///กลุ่มหลัก
        /// </summary>
        public string group_main { get; set; }
        /// <summary>
        ///กลุ่มย่อย 1
        /// </summary>
        public string group_sub_1 { get; set; }
        /// <summary>
        ///กลุ่มย่อย 2
        /// </summary>
        public string group_sub_2 { get; set; }
        /// <summary>
        ///กลุ่มย่อย 3
        /// </summary>
        public string group_sub_3 { get; set; }
        /// <summary>
        ///กลุ่มย่อย 4
        /// </summary>
        public string group_sub_4 { get; set; }
        /// <summary>
        ///รูปภาพ 3
        /// </summary>
        public string picture_3 { get; set; }
        /// <summary>
        ///รูปภาพ 4
        /// </summary>
        public string picture_4 { get; set; }
        /// <summary>
        ///เอกสารอ้างอิง 1
        /// </summary>
        public string ref_doc_1 { get; set; }
        /// <summary>
        ///เอกสารอ้างอิง 2
        /// </summary>
        public string ref_doc_2 { get; set; }
        /// <summary>
        ///เอกสารอ้างอิง 3
        /// </summary>
        public string ref_doc_3 { get; set; }
        /// <summary>
        ///มิติ 1
        /// </summary>
        public string dimension_1 { get; set; }
        /// <summary>
        ///มิติ 2
        /// </summary>
        public string dimension_2 { get; set; }
        /// <summary>
        ///มิติ 3
        /// </summary>
        public string dimension_3 { get; set; }
        /// <summary>
        ///มิติ 4
        /// </summary>
        public string dimension_4 { get; set; }
        /// <summary>
        ///มิติ 5
        /// </summary>
        public string dimension_5 { get; set; }
        /// <summary>
        ///รหัสสกุลเงิน
        /// </summary>
        public string currency_code { get; set; }
        /// <summary>
        ///ละติจูด
        /// </summary>
        public decimal latitude { get; set; }
        /// <summary>
        ///ลองจิจูด
        /// </summary>
        public decimal longitude { get; set; }
        /// <summary>
        ///ปิด
        /// </summary>
        public string close_credit { get; set; }
        /// <summary>
        ///เปิด
        /// </summary>
        public string open_credit { get; set; }
        /// <summary>
        ///ปิด(ชั่วคราว)
        /// </summary>
        public string stop_credit { get; set; }
        /// <summary>
        ///รับเอง
        /// </summary>
        public string to_be_responsible { get; set; }
        /// <summary>
        ///ส่งให้
        /// </summary>
        public string to_support { get; set; }
        /// <summary>
        ///อื่นๆ
        /// </summary>
        public string other { get; set; }
        /// <summary>
        ///ภาษีอัตราศูนย์
        /// </summary>
        public string zero_vat { get; set; }
        /// <summary>
        ///ภาษีกรมสรรพสามิต
        /// </summary>
        public string excise_vat { get; set; }
        /// <summary>
        ///ยกเว้นภาษี
        /// </summary>
        public string exc_vat { get; set; }
        /// <summary>
        ///ภาษีมูลค่าเพิ่ม
        /// </summary>
        public string normal_vat { get; set; }
        /// <summary>
        ///ภาษีแยกนอก
        /// </summary>
        public string tax_out { get; set; }
        /// <summary>
        ///ภาษีรวมใน
        /// </summary>
        public string tax_in { get; set; }
        /// <summary>
        ///ภาษีอัตราศูนย์
        /// </summary>
        public string tax_zero { get; set; }
        /// <summary>
        ///เลขที่บัตรประชาชน
        /// </summary>
        public string card_id { get; set; }
        /// <summary>
        ///สมุดเงินฝาก
        /// </summary>
        public string passbook_code { get; set; }
        /// <summary>
        ///ระบุประเภทภาษี
        /// </summary>
        //public int set_tax_type { get; set; }
        /// <summary>
        ///สถานประกอบการ
        /// </summary>
        public int branch_type { get; set; }
        /// <summary>
        ///สำนักงานใหญ่
        /// </summary>
        public string headquarters { get; set; }
        /// <summary>
        ///สาขา
        /// </summary>
        public string branch { get; set; }
        /// <summary>
        ///เลขที่สาขา
        /// </summary>
        public string branch_code { get; set; }
        /// <summary>
        ///เขตเก็บเงิน
        /// </summary>
        public string area_paybill { get; set; }
        /// <summary>
        ///เหตุผลปิดวงเงินชั่วคราว
        /// </summary>
        public string reason_disable_credit { get; set; }
        /// <summary>
        ///วันที่ปิดเครดิต
        /// </summary>
        public DateTime close_credit_date { get; set; }
        /// <summary>
        ///ชำระเช็คเกินกำหนด
        /// </summary>
        public int close_reason_1 { get; set; }
        /// <summary>
        ///ใหม่ยอดเกินวงเงินเครดิต
        /// </summary>
        public int close_reason_2 { get; set; }
        /// <summary>
        ///ผิดเงื่อนไขการชำระ
        /// </summary>
        public int close_reason_3 { get; set; }
        /// <summary>
        ///อื่น ๆ
        /// </summary>
        public int close_reason_4 { get; set; }

        /// <summary>
        ///ไม่เลือก
        /// </summary>
        public string no_select { get; set; }
        /// <summary>
        ///มิติ 6
        /// </summary>
        public string dimension_6 { get; set; }
        /// <summary>
        ///มิติ 7
        /// </summary>
        public string dimension_7 { get; set; }
        /// <summary>
        ///มิติ 8
        /// </summary>
        public string dimension_8 { get; set; }
        /// <summary>
        ///มิติ 9
        /// </summary>
        public string dimension_9 { get; set; }
        /// <summary>
        ///มิติ 10
        /// </summary>
        public string dimension_10 { get; set; }
        /// <summary>
        ///ไม่คำณวนปิดสถานะเครดิต
        /// </summary>
        public int disable_auto_close_credit { get; set; }
        /// <summary>
        ///อ้างอิงรหัสเจ้าหนี้
        /// </summary>
        public string ap_code_ref { get; set; }
        /// <summary>
        ///ประเภทการขาย
        /// </summary>
        public int sale_type { get; set; }
        /// <summary>
        ///ไม่เลือก
        /// </summary>
        public string sale_type_0 { get; set; }
        /// <summary>
        ///ขายสด
        /// </summary>
        public string sale_type_1 { get; set; }
        /// <summary>
        ///ขายเชื่อ
        /// </summary>
        public string sale_type_2 { get; set; }
        /// <summary>
        ///line id
        /// </summary>
        public string line_id { get; set; }
        /// <summary>
        ///facebook
        /// </summary>
        public string facebook { get; set; }
        /// <summary>
        ///Type
        /// </summary>
        public string customer_type_code { get; set; }
        /// <summary>
        ///Channel
        /// </summary>
        public string ar_channel_code { get; set; }
        /// <summary>
        ///ประเภทสถานที่ตั้ง
        /// </summary>
        public string ar_location_type_code { get; set; }
        /// <summary>
        ///รหัสกลาง
        /// </summary>
        public string br_cust_code { get; set; }
        /// <summary>
        ///Sub Type 1
        /// </summary>
        public string ar_sub_type_1_code { get; set; }
        /// <summary>
        ///Vehicle
        /// </summary>
        public string ar_vehicle_code { get; set; }
        /// <summary>
        ///Equipment
        /// </summary>
        public string ar_equipment_code { get; set; }
        /// <summary>
        ///Sub Equipment
        /// </summary>
        public string ar_sub_equipment { get; set; }
        /// <summary>
        ///ชื่อโครงการ
        /// </summary>
        public string ar_project_code { get; set; }
        /// <summary>
        ///ค้าส่ง
        /// </summary>
        public string ar_shoptype1_code { get; set; }
        /// <summary>
        ///ค้าปลีก
        /// </summary>
        public string ar_shoptype2_code { get; set; }
        /// <summary>
        ///ห้างท้องถิ่น
        /// </summary>
        public string ar_shoptype3_code { get; set; }
        /// <summary>
        ///On-Premise,HORECA
        /// </summary>
        public string ar_shoptype4_code { get; set; }
        /// <summary>
        ///ช่องทางพิเศษ
        /// </summary>
        public string ar_shoptype5_code { get; set; }
        /// <summary>
        ///ช่องทางพิเศษย่อย
        /// </summary>
        public string sub_ar_shoptype5_code { get; set; }
        /// <summary>
        ///Tier
        /// </summary>
        public string arm_tier { get; set; }
        /// <summary>
        ///วันที่อนุมัติ ARM Code
        /// </summary>
        public string arm_approve_date { get; set; }
        /// <summary>
        ///None
        /// </summary>
        public string tier_0 { get; set; }
        /// <summary>
        ///Silver
        /// </summary>
        public string tier_1 { get; set; }
        /// <summary>
        ///Gold
        /// </summary>
        public string tier_2 { get; set; }
        /// <summary>
        ///Platinum
        /// </summary>
        public string tier_3 { get; set; }
        /// <summary>
        ///Diamond
        /// </summary>
        public string tier_4 { get; set; }
        /// <summary>
        ///Customer Channel
        /// </summary>
        public string ar_customer_channel { get; set; }
        /// <summary>
        ///โครงการ
        /// </summary>
        public int ar_project_code_name { get; set; }
    }


    /// <summary>
    /// เจ้าหนี้
    /// </summary>
    public class ap_supplier : _jsondata
    {
        public ap_supplier(DataRow row)
        {
            this.code = _getData(row, "code",false);
            this.code_old = _getData(row, "code_old");
            this.name_1 = _getData(row, "name_1",false);
            this.name_2 = _getData(row, "name_2");
            this.name_eng_1 = _getData(row, "name_eng_1");
            this.name_eng_2 = _getData(row, "name_eng_2");
            this.prefixname = _getData(row, "prefixname");
            this.firstname = _getData(row, "firstname",false);
            this.lastname = _getData(row, "lastname");
            this.address = _getData(row, "address");
            this.address_eng = _getData(row, "address_eng");
            this.tambon = _getData(row, "tambon");
            this.amper = _getData(row, "amper");
            this.province = _getData(row, "province");
            this.zip_code = _getData(row, "zip_code");
            this.telephone = _getData(row, "telephone");
            this.fax = _getData(row, "fax");
            this.email = _getData(row, "email");
            this.website = _getData(row, "website");
            this.description = _getData(row, "description");
            this.ap_type = _getData(row, "ap_type");
            this.remark = _getData(row, "remark");
            this.status = _getInt(row["status"].ToString());
            this.guid_code = _getData(row, "guid_code");
            this.debt_balance = _getDecimal(row["debt_balance"].ToString());
            this.chq_balance = _getDecimal(row["chq_balance"].ToString());
            this.bill_balance = _getDecimal(row["chq_balance"].ToString());
            this.ap_status = _getInt(row["ap_status"].ToString());
            this.doc_format_code = _getData(row, "doc_format_code");
            this.country = _getData(row, "country");
            if (row["create_date_time_now"].ToString().Length > 0)
            {
                this.create_date_time_now = _getDate(row["create_date_time_now"].ToString());
            }
            this.interco = _getData(row, "interco");

        }
        /// <summary>
        ///รหัส
        /// </summary>
        public DateTime create_date_time_now { get; set; }
        
        public string code { get; set; }
        /// <summary>
        ///รหัสเก่า
        /// </summary>
        public string code_old { get; set; }
        /// <summary>
        ///ชื่อ 1
        /// </summary>
        public string name_1 { get; set; }
        /// <summary>
        ///ชื่อ 2
        /// </summary>
        public string name_2 { get; set; }
        /// <summary>
        ///ชื่อ (ภาษาอังกฤษ) 1
        /// </summary>
        public string name_eng_1 { get; set; }
        /// <summary>
        ///ชื่อ (ภาษาอังกฤษ) 2
        /// </summary>
        public string name_eng_2 { get; set; }
        /// <summary>
        ///คำนำหน้า
        /// </summary>
        public string prefixname { get; set; }
        /// <summary>
        ///ชื่อ
        /// </summary>
        public string firstname { get; set; }
        /// <summary>
        ///นามสกุล
        /// </summary>
        public string lastname { get; set; }
        /// <summary>
        ///ที่อยู่
        /// </summary>
        public string address { get; set; }
        /// <summary>
        ///ที่อยู่ (ภาษาอังกฤษ)
        /// </summary>
        public string address_eng { get; set; }
        /// <summary>
        ///ตำบล/แขวง
        /// </summary>
        public string tambon { get; set; }
        /// <summary>
        ///อำเภอ/เขต
        /// </summary>
        public string amper { get; set; }
        /// <summary>
        ///จังหวัด
        /// </summary>
        public string province { get; set; }
        /// <summary>
        ///รหัสไปรษณีย์
        /// </summary>
        public string zip_code { get; set; }
        /// <summary>
        ///หมายเลขโทรศัพท์
        /// </summary>
        public string telephone { get; set; }
        /// <summary>
        ///หมายเลขโทรสาร
        /// </summary>
        public string fax { get; set; }
        /// <summary>
        ///อีเมล์
        /// </summary>
        public string email { get; set; }
        /// <summary>
        ///เวบไซด์
        /// </summary>
        public string website { get; set; }
        /// <summary>
        ///รายละเอียด
        /// </summary>
        public string description { get; set; }
        /// <summary>
        ///ประเภทเจ้าหนี้
        /// </summary>
        public string ap_type { get; set; }
        /// <summary>
        ///หมายเหตุ
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        ///สถานะ
        /// </summary>
        public int status { get; set; }
        /// <summary>
        ///GUID
        /// </summary>
        public string guid_code { get; set; }
        /// <summary>
        ///รายละเอียด
        /// </summary>
        public string tab_detail { get; set; }
        /// <summary>
        ///เครดิต
        /// </summary>
        public string tab_credit { get; set; }
        /// <summary>
        ///การจ่ายเงิน
        /// </summary>
        public string tab_paybill { get; set; }
        /// <summary>
        ///มิติ
        /// </summary>
        public string tab_dimension { get; set; }
        /// <summary>
        ///ติดต่ออ้างอิง
        /// </summary>
        public string tab_contact { get; set; }
        /// <summary>
        ///สินค้า
        /// </summary>
        public string tab_item { get; set; }
        /// <summary>
        ///สถานที่ส่งสินค้า
        /// </summary>
        public string tab_logistic_area { get; set; }
        /// <summary>
        ///กลุ่มเครดิต
        /// </summary>
        public string tab_credit_group { get; set; }
        /// <summary>
        ///ยอดคงเหลือ
        /// </summary>
        public decimal debt_balance { get; set; }
        /// <summary>
        ///ยอดเช็ครอขึ้นเงิน
        /// </summary>
        public decimal chq_balance { get; set; }
        /// <summary>
        ///ยอดรับวางบิลรอจ่าย
        /// </summary>
        public decimal bill_balance { get; set; }
        /// <summary>
        ///ใช้งาน
        /// </summary>
        public string active { get; set; }
        /// <summary>
        ///ไม่ใช้งาน
        /// </summary>
        public string inactive { get; set; }
        /// <summary>
        ///รูปภาพ
        /// </summary>
        public string tab_image { get; set; }
        /// <summary>
        ///ชนิดเจ้าหนี้
        /// </summary>
        public int ap_status { get; set; }
        /// <summary>
        ///นิติบุคคล
        /// </summary>
        public string juristic_person { get; set; }
        /// <summary>
        ///บุคคลธรรมดา
        /// </summary>
        public string personality { get; set; }
        /// <summary>
        ///รหัสเอกสาร
        /// </summary>
        public string doc_format_code { get; set; }
        /// <summary>
        ///รายละเอียดหลัก
        /// </summary>
        public string tab_ap_master { get; set; }
        /// <summary>
        ///รายละเอียดเพิ่มเติม
        /// </summary>
        public string tab_ap_detail { get; set; }
        /// <summary>
        ///ประเทศ
        /// </summary>
        public string country { get; set; }
        /// <summary>
        ///เลขที่บัตรประชาชน
        /// </summary>
        public string card_id { get; set; }
        /// <summary>
        ///เลขประจำตัวผู้เสียภาษี
        /// </summary>
        public string tax_id { get; set; }
        /// <summary>
        ///สถานประกอบการ
        /// </summary>
        public string branch_type { get; set; }
        /// <summary>
        ///สำนักงานใหญ่
        /// </summary>
        public string headquarters { get; set; }
        /// <summary>
        ///สาขา
        /// </summary>
        public string branch { get; set; }
        /// <summary>
        ///เลขที่สาขา
        /// </summary>
        public string branch_code { get; set; }
        /// <summary>
        ///INTERCO
        /// </summary>
        public string interco { get; set; }
    }

    /// <summary>
    /// รายละเอียดเจ้าหนี้
    /// </summary>
    public class ap_supplier_detail : _jsondata
    {
        public ap_supplier_detail(DataRow row)
        {
            this.ap_code = _getData(row, "ap_code",false);
            this.staff_pay_code = _getData(row, "staff_pay_code");
            this.payment_way = _getData(row, "payment_way");
            this.pay_bill_way = _getData(row, "pay_bill_way");
            this.pay_condition = _getData(row, "pay_condition");
            this.credit_purchase = _getInt(row["credit_purchase"].ToString());
            this.credit_code = _getData(row, "credit_code");
            this.form_name = _getData(row, "form_name");
            this.discount_item = _getData(row, "discount_item");
            this.discount_bill = _getData(row, "discount_bill");
            this.credit_day = _getInt(row["credit_day"].ToString());
            this.trade_license = _getData(row, "trade_license");
            this.tax_id = _getData(row, "tax_id");
            this.tax_type = _getData(row, "tax_type");
            this.tax_rate = _getDecimal(row["tax_rate"].ToString());
            this.account_code = _getData(row, "account_code");
            this.shipping_type = _getData(row, "shipping_type");
            this.close_reason = _getData(row, "close_reason");
            if (row["close_date"].ToString().Length > 0)
            {
                this.close_date = _getDate(row["close_date"].ToString());
            }
            this.group_main = _getData(row, "group_main");
            this.group_sub_1 = _getData(row, "group_sub_1");
            this.group_sub_2 = _getData(row, "group_sub_2");
            this.group_sub_3 = _getData(row, "group_sub_3");
            this.group_sub_4 = _getData(row, "group_sub_4");
            this.picture_1 = _getData(row, "picture_1");
            this.picture_2 = _getData(row, "picture_2");
            this.picture_3 = _getData(row, "picture_3");
            this.picture_4 = _getData(row, "picture_4");
            this.ref_doc_1 = _getData(row, "ref_doc_1");
            this.ref_doc_2 = _getData(row, "ref_doc_2");
            this.ref_doc_3 = _getData(row, "ref_doc_3");
            this.dimension_1 = _getData(row, "dimension_1");
            this.dimension_2 = _getData(row, "dimension_2");
            this.dimension_3 = _getData(row, "dimension_3");
            this.dimension_4 = _getData(row, "dimension_4");
            this.dimension_5 = _getData(row, "dimension_5");
            this.currency_code = _getData(row, "currency_code");
            this.card_id = _getData(row, "card_id");
            this.passbook_code = _getData(row, "passbook_code");
           // this.set_tax_type = _getInt(row["set_tax_type"].ToString());
            this.branch_type = _getInt(row["branch_type"].ToString());
            this.branch_code = _getData(row, "branch_code");
            this.dimension_6 = _getData(row, "dimension_6");
            this.dimension_7 = _getData(row, "dimension_7");
            this.dimension_8 = _getData(row, "dimension_8");
            this.dimension_9 = _getData(row, "dimension_9");
            this.dimension_10 = _getData(row, "dimension_10");
            if (row["create_date_time_now"].ToString().Length > 0)
            {
                this.create_date_time_now = _getDate(row["create_date_time_now"].ToString());
            }
            this.lead_time = _getInt(row["lead_time"].ToString());

        }
        public DateTime create_date_time_now { get; set; }
         
        /// <summary>
        ///รหัสผู้จำหน่าย
        /// </summary>
        public string ap_code { get; set; }
        /// <summary>
        ///พนักงานเตรียมจ่าย
        /// </summary>
        public string staff_pay_code { get; set; }
        /// <summary>
        ///ช่องทางการจ่ายเงิน
        /// </summary>
        public string payment_way { get; set; }
        /// <summary>
        ///ช่องทางการวางบิล
        /// </summary>
        public string pay_bill_way { get; set; }
        /// <summary>
        ///เงิ่อนไขการจ่ายเงิน
        /// </summary>
        public string pay_condition { get; set; }
        /// <summary>
        ///เครดิตซื้อ
        /// </summary>
        public int credit_purchase { get; set; }
        /// <summary>
        ///รหัสเครดิต
        /// </summary>
        public string credit_code { get; set; }
        /// <summary>
        ///ชื่อฟอร์มรายเจ้าหนี้
        /// </summary>
        public string form_name { get; set; }
        /// <summary>
        ///ส่วนลดต่อรายการ
        /// </summary>
        public string discount_item { get; set; }
        /// <summary>
        ///ส่วนลดท้ายบิล
        /// </summary>
        public string discount_bill { get; set; }
        /// <summary>
        ///จำนวนวันเครดิต
        /// </summary>
        public int credit_day { get; set; }
        /// <summary>
        ///ทะเบียนการค้า
        /// </summary>
        public string trade_license { get; set; }
        /// <summary>
        ///เลขประจำตัวผู้เสียภาษี
        /// </summary>
        public string tax_id { get; set; }
        /// <summary>
        ///ประเภทภาษี
        /// </summary>
        public string tax_type { get; set; }
        /// <summary>
        ///อัตราภาษี
        /// </summary>
        public decimal tax_rate { get; set; }
        /// <summary>
        ///บัญชีเจ้าหนี้รายตัว
        /// </summary>
        public string account_code { get; set; }
        /// <summary>
        ///ประเภทการส่งมอบ
        /// </summary>
        public string shipping_type { get; set; }
        /// <summary>
        ///เหตุผลในการปิด
        /// </summary>
        public string close_reason { get; set; }
        /// <summary>
        ///วันที่ปิด
        /// </summary>
        public DateTime close_date { get; set; }
        /// <summary>
        ///กลุ่มหลัก
        /// </summary>
        public string group_main { get; set; }
        /// <summary>
        ///กลุ่มย่อย 1
        /// </summary>
        public string group_sub_1 { get; set; }
        /// <summary>
        ///กลุ่มย่อย 2
        /// </summary>
        public string group_sub_2 { get; set; }
        /// <summary>
        ///กลุ่มย่อย 3
        /// </summary>
        public string group_sub_3 { get; set; }
        /// <summary>
        ///กลุ่มย่อย 4
        /// </summary>
        public string group_sub_4 { get; set; }
        /// <summary>
        ///รูปภาพ 1
        /// </summary>
        public string picture_1 { get; set; }
        /// <summary>
        ///รูปภาพ 2
        /// </summary>
        public string picture_2 { get; set; }
        /// <summary>
        ///รูปภาพ 3
        /// </summary>
        public string picture_3 { get; set; }
        /// <summary>
        ///รูปภาพ 4
        /// </summary>
        public string picture_4 { get; set; }
        /// <summary>
        ///เอกสารอ้างอิง 1
        /// </summary>
        public string ref_doc_1 { get; set; }
        /// <summary>
        ///เอกสารอ้างอิง 2
        /// </summary>
        public string ref_doc_2 { get; set; }
        /// <summary>
        ///เอกสารอ้างอิง 3
        /// </summary>
        public string ref_doc_3 { get; set; }
        /// <summary>
        ///มิติ 1
        /// </summary>
        public string dimension_1 { get; set; }
        /// <summary>
        ///มิติ 2
        /// </summary>
        public string dimension_2 { get; set; }
        /// <summary>
        ///มิติ 3
        /// </summary>
        public string dimension_3 { get; set; }
        /// <summary>
        ///มิติ 4
        /// </summary>
        public string dimension_4 { get; set; }
        /// <summary>
        ///มิติ 5
        /// </summary>
        public string dimension_5 { get; set; }
        /// <summary>
        ///รหัสสกุลเงิน
        /// </summary>
        public string currency_code { get; set; }
        /// <summary>
        ///ภาษีแยกนอก
        /// </summary>
        public string tax_out { get; set; }
        /// <summary>
        ///ภาษีรวมใน
        /// </summary>
        public string tax_in { get; set; }
        /// <summary>
        ///ภาษีอัตราศูนย์
        /// </summary>
        public string tax_zero { get; set; }
        /// <summary>
        ///เงินล่วงหน้า
        /// </summary>
        public string prepaid_money { get; set; }
        /// <summary>
        ///มูลค่าเงินมัดจำ
        /// </summary>
        public string deposit_money { get; set; }
        /// <summary>
        ///จำนวนเงินมัดจำ
        /// </summary>
        public string deposit_value { get; set; }
        /// <summary>
        ///เลขที่บัตรประชาชน
        /// </summary>
        public string card_id { get; set; }
        /// <summary>
        ///สมุดเงินฝาก
        /// </summary>
        public string passbook_code { get; set; }
        /// <summary>
        ///ระบุประเภทภาษี
        /// </summary>
        //public int set_tax_type { get; set; }
        /// <summary>
        ///สถานประกอบการ
        /// </summary>
        public int branch_type { get; set; }
        /// <summary>
        ///สำนักงานใหญ่
        /// </summary>
        public string headquarters { get; set; }
        /// <summary>
        ///สาขา
        /// </summary>
        public string branch { get; set; }
        /// <summary>
        ///เลขที่สาขา
        /// </summary>
        public string branch_code { get; set; }
        /// <summary>
        ///มิติ 6
        /// </summary>
        public string dimension_6 { get; set; }
        /// <summary>
        ///มิติ 7
        /// </summary>
        public string dimension_7 { get; set; }
        /// <summary>
        ///มิติ 8
        /// </summary>
        public string dimension_8 { get; set; }
        /// <summary>
        ///มิติ 9
        /// </summary>
        public string dimension_9 { get; set; }
        /// <summary>
        ///มิติ 10
        /// </summary>
        public string dimension_10 { get; set; }
        /// <summary>
        ///ระยะเวลาสั่งสินค้า
        /// </summary>
        public int lead_time { get; set; }
    }


    /// <summary>
    /// รายวันทั้งหมด
    /// </summary>
    public class ic_trans : _jsondata
    {
        public ic_trans(DataRow row)
        {
        
            this.trans_type = _getInt(row["trans_type"].ToString());
            this.trans_flag = _getInt(row["trans_flag"].ToString());
            this.doc_group = _getData(row, "doc_group");
            if (row["doc_date"].ToString().Length > 0)
            {
                this.doc_date = _getDate(row["doc_date"].ToString());
            }
            this.doc_no = _getData(row, "doc_no", false);
            this.doc_ref = _getData(row, "doc_ref");
            if (row["doc_ref_date"].ToString().Length > 0)
            {
                this.doc_ref_date = _getDate(row["doc_ref_date"].ToString());
            }
            this.tax_doc_no = _getData(row, "tax_doc_no");
            if (row["tax_doc_date"].ToString().Length > 0)
            {
                this.tax_doc_date = _getDate(row["tax_doc_date"].ToString());
            }
            this.inquiry_type = _getInt(row["inquiry_type"].ToString());
            this.vat_type = _getInt(row["vat_type"].ToString());
            this.cust_code = _getData(row, "cust_code");
            this.contactor = _getData(row, "contactor");
            this.branch_code = _getData(row, "branch_code");
            this.project_code = _getData(row, "project_code");
            this.side_code = _getData(row, "side_code");
            this.department_code = _getData(row, "department_code");
            this.sale_area_code = _getData(row, "sale_area_code");
            this.sale_code = _getData(row, "sale_code");
            this.send_type = _getInt(row["send_type"].ToString());
            this.send_day = _getInt(row["send_day"].ToString());
            if (row["send_date"].ToString().Length > 0)
            {
                this.send_date = _getDate(row["send_date"].ToString());
            }
            if (row["expire_date"].ToString().Length > 0)
            {
                this.expire_date = _getDate(row["expire_date"].ToString());
            }
            this.credit_day = _getInt(row["credit_day"].ToString());
            if (row["due_date"].ToString().Length > 0)
            {
                this.due_date = _getDate(row["due_date"].ToString());
            }
            this.answer_type = _getInt(row["answer_type"].ToString());
            this.transport_value = _getDecimal(row["transport_value"].ToString());
            this.discount_word = _getData(row, "discount_word");
            this.vat_rate = _getDecimal(row["vat_rate"].ToString());
            this.currency_code = _getData(row, "currency_code");
            this.exchange_rate = _getDecimal(row["exchange_rate"].ToString());
            this.total_cost = _getDecimal(row["total_cost"].ToString());
            this.total_value = _getDecimal(row["total_value"].ToString());
            this.total_discount = _getDecimal(row["total_discount"].ToString());
            this.total_vat_value = _getDecimal(row["total_vat_value"].ToString());
            this.total_after_vat = _getDecimal(row["total_after_vat"].ToString());
            this.total_except_vat = _getDecimal(row["total_except_vat"].ToString());
            this.total_amount = _getDecimal(row["total_amount"].ToString());
            this.balance_amount = _getDecimal(row["balance_amount"].ToString());
            this.adjust_reason = _getData(row, "adjust_reason");
            this.approve_code = _getData(row, "approve_code");
            if (row["approve_date"].ToString().Length > 0)
            {
                this.approve_date = _getDate(row["approve_date"].ToString());
            }
            this.remark = _getData(row, "remark");
            this.status = _getInt(row["status"].ToString());
            this.guid_code = _getData(row, "guid_code");
            this.user_request = _getData(row, "user_request");
            this.expire_day = _getInt(row["expire_day"].ToString());
            this.send_mail = _getInt(row["send_mail"].ToString());
            this.send_sms = _getInt(row["send_sms"].ToString());
            this.doc_ref_trans = _getData(row, "doc_ref_trans");
            if (row["credit_date"].ToString().Length > 0)
            {
                this.credit_date = _getDate(row["credit_date"].ToString());
            }
            this.total_before_vat = _getDecimal(row["total_before_vat"].ToString());
            this.doc_time = _getData(row, "doc_time");
            this.allocate_code = _getData(row, "allocate_code");
            this.job_code = _getData(row, "job_code");
            if (row["delivery_date"].ToString().Length > 0)
            {
                this.delivery_date = _getDate(row["delivery_date"].ToString());
            }
            this.pay_amount = _getDecimal(row["pay_amount"].ToString());
            this.money = _getDecimal(row["money"].ToString());
            this.sum_pay_money_diff = _getDecimal(row["sum_pay_money_diff"].ToString());
            this.currency_money = _getDecimal(row["currency_money"].ToString());
            this.total_debt_amount = _getDecimal(row["total_debt_amount"].ToString());
            this.cancel_type = _getInt(row["cancel_type"].ToString());
            this.last_status = _getInt(row["last_status"].ToString());
            this.used_status = _getInt(row["used_status"].ToString());
            this.doc_success = _getInt(row["doc_success"].ToString());
            this.total_manual = _getInt(row["total_manual"].ToString());
            this.on_hold = _getInt(row["on_hold"].ToString());
            this.extra_word = _getData(row, "extra_word");
            this.transport_code = _getData(row, "transport_code");
            this.want_day = _getInt(row["want_day"].ToString());
            if (row["want_date"].ToString().Length > 0)
            {
                this.want_date = _getDate(row["want_date"].ToString());
            }
            this.deposit_day = _getInt(row["deposit_day"].ToString());
            if (row["deposit_date"].ToString().Length > 0)
            {
                this.want_date = _getDate(row["deposit_date"].ToString());
            }
            this.sale_group = _getData(row, "sale_group");
            this.not_approve_1 = _getInt(row["not_approve_1"].ToString());
            this.user_approve = _getData(row, "user_approve");
            this.approve_status = _getInt(row["approve_status"].ToString());
            this.expire_status = _getInt(row["expire_status"].ToString());
            this.advance_amount = _getDecimal(row["advance_amount"].ToString());
            this.doc_format_code = _getData(row, "doc_format_code", false);
            this.used_status_2 = _getInt(row["used_status_2"].ToString());
            this.remark_2 = _getData(row, "remark_2");
            this.remark_3 = _getData(row, "remark_3");
            this.ref_amount = _getDecimal(row["ref_amount"].ToString());
            this.ref_new_amount = _getDecimal(row["ref_new_amount"].ToString());
            this.ref_diff = _getDecimal(row["ref_diff"].ToString());
            this.sum_point = _getDecimal(row["sum_point"].ToString());
            this.cashier_code = _getData(row, "cashier_code");
            this.is_pos = _getInt(row["is_pos"].ToString());
            this.pos_id = _getData(row, "pos_id");
            this.member_code = _getData(row, "member_code");
            this.doc_no_guid = _getData(row, "doc_no_guid");
            this.pos_bill_type = _getInt(row["pos_bill_type"].ToString());
            this.pos_bill_change = _getInt(row["pos_bill_change"].ToString());
            this.doc_type = _getInt(row["doc_type"].ToString());
            this.recheck_count_day = _getDecimal(row["recheck_count_day"].ToString());
            this.recheck_count = _getInt(row["recheck_count"].ToString());
            this.wh_from = _getData(row, "wh_from");
            this.location_from = _getData(row, "location_from");
            this.wh_to = _getData(row, "wh_to");
            this.location_to = _getData(row, "location_to");
            this.auto_create = _getInt(row["auto_create"].ToString());
            this.payable_sub_type_1_1 = _getData(row, "payable_sub_type_1_1");
            this.payable_sub_type_1_2 = _getData(row, "payable_sub_type_1_2");
            this.payable_sub_type_3 = _getData(row, "payable_sub_type_3");
            this.payable_sub_type_4 = _getData(row, "payable_sub_type_4");
            this.invoice_add_cash_other = _getData(row, "invoice_add_cash_other");
            this.invoice_add_cash_service_other = _getData(row, "invoice_add_cash_service_other");
            this.send_to_pick_and_pack = _getInt(row["send_to_pick_and_pack"].ToString());
            this.service_charge_word = _getData(row, "service_charge_word");
            this.total_service_charge = _getDecimal(row["total_service_charge"].ToString());
            this.sale_shift_id = _getData(row, "sale_shift_id");
            this.creator_code = _getData(row, "creator_code");
            this.last_editor_code = _getData(row, "last_editor_code");
            if (row["create_datetime"].ToString().Length > 0)
            {
                this.create_datetime = _getDate(row["create_datetime"].ToString());
            }
            if (row["lastedit_datetime"].ToString().Length > 0)
            {
                this.lastedit_datetime = _getDate(row["lastedit_datetime"].ToString());
            }
            this.table_number = _getData(row, "table_number");
            this.period_guid = _getData(row, "period_guid");
            this.branch_code_to = _getData(row, "branch_code_to");
            this.allocate_code_to = _getData(row, "allocate_code_to");
            this.project_code_to = _getData(row, "project_code_to");
            this.job_code_to = _getData(row, "job_code_to");
            this.side_code_to = _getData(row, "side_code_to");
            this.department_code_to = _getData(row, "department_code_to");
            this.point_telephone = _getData(row, "point_telephone");
            this.sum_point_2 = _getDecimal(row["sum_point_2"].ToString());
            this.remark_4 = _getData(row, "remark_4");
            this.remark_5 = _getData(row, "remark_5");
            this.is_manual_vat = _getInt(row["is_manual_vat"].ToString());
            this.pass_book_code = _getData(row, "pass_book_code");
            this.total_amount_2 = _getDecimal(row["total_amount_2"].ToString());
            this.sender_code = _getData(row, "sender_code");
            this.is_cancel = _getInt(row["is_cancel"].ToString());
            this.cancel_code = _getData(row, "cancel_code");
            if (row["cancel_datetime"].ToString().Length > 0)
            {
                this.cancel_datetime = _getDate(row["cancel_datetime"].ToString());
            }
            this.is_hold = _getInt(row["is_hold"].ToString());
            this.auto_approved = _getInt(row["auto_approved"].ToString());
            this.pos_transfer = _getInt(row["pos_transfer"].ToString());
            this.doc_close = _getInt(row["doc_close"].ToString());
            this.ref_doc_type = _getData(row, "ref_doc_type");
            if (row["create_date_time_now"].ToString().Length > 0)
            {
                this.create_date_time_now = _getDate(row["create_date_time_now"].ToString());
            }
            this.is_doc_copy = _getInt(row["is_doc_copy"].ToString());
            this.doc_reason = _getData(row, "doc_reason");

        }
        public DateTime create_date_time_now { get; set; }
       
        /// <summary>
        ///ประเภทรายวัน
        /// </summary>
        public int trans_type { get; set; }
        /// <summary>
        ///รายวันระบบ
        /// </summary>
        public int trans_flag { get; set; }
        /// <summary>
        ///กลุ่มเอกสาร
        /// </summary>
        public string doc_group { get; set; }
        /// <summary>
        ///เอกสารวันที่
        /// </summary>
        public DateTime doc_date { get; set; }
        /// <summary>
        ///เอกสารเลขที่
        /// </summary>
        public string doc_no { get; set; }
        /// <summary>
        ///เอกสารอ้างอิง
        /// </summary>
        public string doc_ref { get; set; }
        /// <summary>
        ///เอกสารอ้างอิงวันที่
        /// </summary>
        public DateTime doc_ref_date { get; set; }
        /// <summary>
        ///เลขที่ใบกำกับ
        /// </summary>
        public string tax_doc_no { get; set; }
        /// <summary>
        ///วันที่ใบกำกับ
        /// </summary>
        public DateTime tax_doc_date { get; set; }
        /// <summary>
        ///ประเภทรายการ
        /// </summary>
        public int inquiry_type { get; set; }
        /// <summary>
        ///ประเภทภาษี
        /// </summary>
        public int vat_type { get; set; }
        /// <summary>
        ///ลูกหนี้/เจ้าหนี้
        /// </summary>
        public string cust_code { get; set; }
        /// <summary>
        ///ผู้ติดต่อ
        /// </summary>
        public string contactor { get; set; }
        /// <summary>
        ///สาขา
        /// </summary>
        public string branch_code { get; set; }
        /// <summary>
        ///โครงการ
        /// </summary>
        public string project_code { get; set; }
        /// <summary>
        ///ฝ่าย
        /// </summary>
        public string side_code { get; set; }
        /// <summary>
        ///แผนก
        /// </summary>
        public string department_code { get; set; }
        /// <summary>
        ///เขตการขาย
        /// </summary>
        public string sale_area_code { get; set; }
        /// <summary>
        ///พนักงานขาย
        /// </summary>
        public string sale_code { get; set; }
        /// <summary>
        ///ประเภทการส่ง
        /// </summary>
        public int send_type { get; set; }
        /// <summary>
        ///รับ/ส่ง ภายใน(วัน)
        /// </summary>
        public int send_day { get; set; }
        /// <summary>
        ///วันที่รับ/ส่งสินค้า
        /// </summary>
        public DateTime send_date { get; set; }
        /// <summary>
        ///วันที่หมดอายุ
        /// </summary>
        public DateTime expire_date { get; set; }
        /// <summary>
        ///วันเครดิต
        /// </summary>
        public int credit_day { get; set; }
        /// <summary>
        ///วันที่ครบกำหนด
        /// </summary>
        public DateTime due_date { get; set; }
        /// <summary>
        ///ลูกค้าตอบกลับ
        /// </summary>
        public int answer_type { get; set; }
        /// <summary>
        ///ค่าขนส่ง
        /// </summary>
        public decimal transport_value { get; set; }
        /// <summary>
        ///ส่วนลด
        /// </summary>
        public string discount_word { get; set; }
        /// <summary>
        ///อัตราภาษี
        /// </summary>
        public decimal vat_rate { get; set; }
        /// <summary>
        ///รหัสสกุลเงิน
        /// </summary>
        public string currency_code { get; set; }
        /// <summary>
        ///อัตราแลกเปลี่ยน
        /// </summary>
        public decimal exchange_rate { get; set; }
        /// <summary>
        ///ต้นทุนสินค้า
        /// </summary>
        public decimal total_cost { get; set; }
        /// <summary>
        ///มูลค่าสินค้า
        /// </summary>
        public decimal total_value { get; set; }
        /// <summary>
        ///มูลค่าส่วนลด
        /// </summary>
        public decimal total_discount { get; set; }
        /// <summary>
        ///ภาษีมูลค่าเพิ่ม
        /// </summary>
        public decimal total_vat_value { get; set; }
        /// <summary>
        ///มูลค่าหลังคิดภาษี
        /// </summary>
        public decimal total_after_vat { get; set; }
        /// <summary>
        ///มูลค่ายกเว้นภาษี
        /// </summary>
        public decimal total_except_vat { get; set; }
        /// <summary>
        ///มูลค่าสุทธิ
        /// </summary>
        public decimal total_amount { get; set; }
        /// <summary>
        ///ยอดเงินคงเหลือ
        /// </summary>
        public decimal balance_amount { get; set; }
        /// <summary>
        ///เหตุผลการปรับปรุง
        /// </summary>
        public string adjust_reason { get; set; }
        /// <summary>
        ///กลุ่มผู้อนุมัติ
        /// </summary>
        public string approve_code { get; set; }
        /// <summary>
        ///วันที่อนุมัติ
        /// </summary>
        public DateTime approve_date { get; set; }
        /// <summary>
        ///หมายเหตุ
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        ///สถานะ
        /// </summary>
        public int status { get; set; }
        /// <summary>
        ///GUID
        /// </summary>
        public string guid_code { get; set; }
        /// <summary>
        ///รหัสลูกค้า/ลูกหนี้
        /// </summary>
        public string ar_code { get; set; }
        /// <summary>
        ///รหัสเจ้าหนี้
        /// </summary>
        public string ap_code { get; set; }
        /// <summary>
        ///ผู้ขออนุมัติ
        /// </summary>
        public string user_request { get; set; }
        /// <summary>
        ///จำนวนวันหมดอายุ
        /// </summary>
        public int expire_day { get; set; }
        /// <summary>
        ///แจ้งส่งทาง อีเมล์
        /// </summary>
        public int send_mail { get; set; }
        /// <summary>
        ///แจ้งส่งทาง SMS
        /// </summary>
        public int send_sms { get; set; }
        /// <summary>
        ///อ้างอิงเอกสาร
        /// </summary>
        public string doc_ref_trans { get; set; }
        /// <summary>
        ///เลขที่ใบเบิก
        /// </summary>
        public string doc_ref_requisition { get; set; }
        /// <summary>
        ///แยกตามหน่วยนับ
        /// </summary>
        public string condition_pack_1 { get; set; }
        /// <summary>
        ///เลขที่ใบขอตรวจนับ
        /// </summary>
        public string doc_ref_check_stock { get; set; }
        /// <summary>
        ///เลขที่ใบโอนออก
        /// </summary>
        public string doc_ref_transfer_out { get; set; }
        /// <summary>
        ///ภาษีอัตราศูนย์
        /// </summary>
        public string zero_vat { get; set; }
        /// <summary>
        ///ยกเว้นภาษี
        /// </summary>
        public string exc_vat { get; set; }
        /// <summary>
        ///ภาษีมูลค่าเพิ่ม
        /// </summary>
        public string normal_vat { get; set; }
        /// <summary>
        ///ภาษีแยกนอก
        /// </summary>
        public string exclude_vat { get; set; }
        /// <summary>
        ///ภาษีรวมใน
        /// </summary>
        public string include_vat { get; set; }
        /// <summary>
        ///วันที่ครบกำหนด
        /// </summary>
        public DateTime credit_date { get; set; }
        /// <summary>
        ///รับเอง
        /// </summary>
        public string ownreceive { get; set; }
        /// <summary>
        ///ส่งให้
        /// </summary>
        public string sendto { get; set; }
        /// <summary>
        ///ยอดก่อนภาษี
        /// </summary>
        public decimal total_before_vat { get; set; }
        /// <summary>
        ///ซื้อสินค้าเงินเชื่อ
        /// </summary>
        public string credit_purchase { get; set; }
        /// <summary>
        ///ซื้อสินค้าเงินสด
        /// </summary>
        public string cash_purchase { get; set; }
        /// <summary>
        ///ซื้อสินค้าเงินสด (สินค้าบริการ)
        /// </summary>
        public string credit_purchase_service { get; set; }
        /// <summary>
        ///ซื้อสินค้าเงินเชื่อ (สินค้าบริการ)
        /// </summary>
        public string cash_purchase_service { get; set; }
        /// <summary>
        ///ค่าใช้จ่ายเงินเชื่อ
        /// </summary>
        public string credit_purchase_other { get; set; }
        /// <summary>
        ///ค่าใช้จ่ายเงินสด
        /// </summary>
        public string cash_purchase_other { get; set; }
        /// <summary>
        ///ค่าใช้จ่ายเงินสด (สินค้าบริการ)
        /// </summary>
        public string credit_purchase_service_other { get; set; }
        /// <summary>
        ///ค่าใช้จ่ายเงินเชื่อ (สินค้าบริการ)
        /// </summary>
        public string cash_purchase_service_other { get; set; }
        /// <summary>
        ///ขายเงินเชื่อ
        /// </summary>
        public string credit_sale { get; set; }
        /// <summary>
        ///ขายเงินสด
        /// </summary>
        public string cash_sale { get; set; }
        /// <summary>
        ///ขายสินค้าเงินสด (สินค้าบริการ)
        /// </summary>
        public string credit_sale_service { get; set; }
        /// <summary>
        ///ขายสินค้าเงินเชื่อ (สินค้าบริการ)
        /// </summary>
        public string cash_sale_service { get; set; }
        /// <summary>
        ///รายได้เงินเชื่อ
        /// </summary>
        public string credit_sale_other { get; set; }
        /// <summary>
        ///รายได้เงินสด
        /// </summary>
        public string cash_sale_other { get; set; }
        /// <summary>
        ///รายได้เงินสด (สินค้าบริการ)
        /// </summary>
        public string credit_sale_service_other { get; set; }
        /// <summary>
        ///รายได้เงินเชื่อ (สินค้าบริการ)
        /// </summary>
        public string cash_sale_service_other { get; set; }
        /// <summary>
        ///เวลา
        /// </summary>
        public string doc_time { get; set; }
        /// <summary>
        ///การจัดสรร
        /// </summary>
        public string allocate_code { get; set; }
        /// <summary>
        ///งาน
        /// </summary>
        public string job_code { get; set; }
        /// <summary>
        ///วันที่ส่งของ
        /// </summary>
        public DateTime delivery_date { get; set; }
        /// <summary>
        ///เลขที่ใบจ่ายเงินล่วงหน้า
        /// </summary>
        public string deposit_no { get; set; }
        /// <summary>
        ///เงินชำระ
        /// </summary>
        public decimal pay_amount { get; set; }
        /// <summary>
        ///จำนวนเงินคืน
        /// </summary>
        public decimal money { get; set; }
        /// <summary>
        ///รับ/จ่ายชำระหนี้ (เงินสด)
        /// </summary>
        public decimal sum_pay_money_cash { get; set; }
        /// <summary>
        ///รับ/จ่ายชำระหนี้ (เช็ค)
        /// </summary>
        public decimal sum_pay_money_chq { get; set; }
        /// <summary>
        ///รับ/จ่ายชำระหนี้ (บัตรเครดิต)
        /// </summary>
        public decimal sum_pay_money_credit { get; set; }
        /// <summary>
        ///รับ/จ่ายชำระหนี้ (เงินโอน)
        /// </summary>
        public decimal sum_pay_money_transfer { get; set; }
        /// <summary>
        ///รับชำระ (เงินสด)
        /// </summary>
        public decimal sum_pay_money_cash_in { get; set; }
        /// <summary>
        ///รับชำระ (เช็ค)
        /// </summary>
        public decimal sum_pay_money_chq_in { get; set; }
        /// <summary>
        ///รับชำระ (บัตรเครดิต)
        /// </summary>
        public decimal sum_pay_money_credit_in { get; set; }
        /// <summary>
        ///รับชำระ (เงินโอน)
        /// </summary>
        public decimal sum_pay_money_transfer_in { get; set; }
        /// <summary>
        ///จ่ายชำระ (เงินสด)
        /// </summary>
        public decimal sum_pay_money_cash_out { get; set; }
        /// <summary>
        ///จ่ายชำระ (เช็ค)
        /// </summary>
        public decimal sum_pay_money_chq_out { get; set; }
        /// <summary>
        ///จ่ายชำระ (บัตรเครดิต)
        /// </summary>
        public decimal sum_pay_money_credit_out { get; set; }
        /// <summary>
        ///จ่ายชำระ (เงินโอน)
        /// </summary>
        public decimal sum_pay_money_transfer_out { get; set; }
        /// <summary>
        ///ผลต่างยอดชำระ
        /// </summary>
        public decimal sum_pay_money_diff { get; set; }
        /// <summary>
        ///รับคืนจากขายเงินสด
        /// </summary>
        public string ret_cash { get; set; }
        /// <summary>
        ///รับคืนจากขายเงินเชื่อ
        /// </summary>
        public string ret_credit { get; set; }
        /// <summary>
        ///ลดหนี้จากขายเงินสด (ไม่กระทบสต๊อก)
        /// </summary>
        public string ret_cash_1 { get; set; }
        /// <summary>
        ///ลดหนี้จากขายเงินเชื่อ (ไม่กระทบสต๊อก)
        /// </summary>
        public string ret_credit_1 { get; set; }
        /// <summary>
        ///รับคืนจากขายสินค้าเงินสด (สินค้าบริการ)
        /// </summary>
        public string ret_cash_service { get; set; }
        /// <summary>
        ///รับคืนจากขายสินค้าเงินเชื่อ (สินค้าบริการ)
        /// </summary>
        public string ret_credit_service { get; set; }
        /// <summary>
        ///ลดหนี้รายได้เงินเชื่อ
        /// </summary>
        public string ret_credit_other { get; set; }
        /// <summary>
        ///ลดหนี้รายได้เงินสด
        /// </summary>
        public string ret_cash_other { get; set; }
        /// <summary>
        ///ลดหนี้รายได้เงินสด (สินค้าบริการ)
        /// </summary>
        public string ret_cash_service_other { get; set; }
        /// <summary>
        ///ลดหนี้รายได้เงินเชื่อ (สินค้าบริการ)
        /// </summary>
        public string ret_credit_service_other { get; set; }
        /// <summary>
        ///เพิ่มหนี้จากการขายเงินเชื่อ
        /// </summary>
        public string invoice_add_credit { get; set; }
        /// <summary>
        ///เพิ่มหนี้จากการขายเงินเชื่อ (ไม่กระทบสต๊อก)
        /// </summary>
        public string invoice_add_credit_1 { get; set; }
        /// <summary>
        ///เพิ่มหนี้จากการขายสินค้าเงินเชื่อ (สินค้าบริการ)
        /// </summary>
        public string invoice_add_credit_service { get; set; }
        /// <summary>
        ///เพิ่มหนี้จากรายได้อื่นเงินเชื่อ
        /// </summary>
        public string invoice_add_credit_other { get; set; }
        /// <summary>
        ///เพิ่มหนี้จากรายได้อื่นเงินเชื่อ (สินค้าบริการ)
        /// </summary>
        public string invoice_add_credit_service_other { get; set; }
        /// <summary>
        ///ลดหนี้ค่าใช้จ่ายเงินเชื่อ
        /// </summary>
        public string reduction_expense_other_credit { get; set; }
        /// <summary>
        ///ลดหนี้ค่าใช้จ่ายเงินสด
        /// </summary>
        public string reduction_expense_other_cash { get; set; }
        /// <summary>
        ///ลดหนี้ค่าใช้จ่ายเงินเชื่อ (สินค้าบริการ)
        /// </summary>
        public string reduction_expense_other_credit_service { get; set; }
        /// <summary>
        ///ลดหนี้ค่าใช้จ่ายเงินสด (สินค้าบริการ)
        /// </summary>
        public string reduction_expense_other_cash_service { get; set; }
        /// <summary>
        ///เพิ่มหนี้ค่าใช้จ่ายเงินเชื่อ
        /// </summary>
        public string invoice_add_expense_other_credit { get; set; }
        /// <summary>
        ///เพิ่มหนี้ค่าใช้จ่ายเงินสด
        /// </summary>
        public string invoice_add_expense_other_cash { get; set; }
        /// <summary>
        ///เพิ่มหนี้ค่าใช้จ่ายเงินเชื่อ (สินค้าบริการ)
        /// </summary>
        public string invoice_add_expense_other_credit_service { get; set; }
        /// <summary>
        ///เพิ่มหนี้ค่าใช้จ่ายเงินสด (สินค้าบริการ)
        /// </summary>
        public string invoice_add_expense_other_cash_service { get; set; }
        /// <summary>
        ///ยอดคงเหลือ(ตามสกุลเงิน)
        /// </summary>
        public decimal currency_money { get; set; }
        /// <summary>
        ///ยอดหนี้รวมภาษี
        /// </summary>
        public decimal total_debt_amount { get; set; }
        /// <summary>
        ///ชื่อพนักงานขาย
        /// </summary>
        public string sale_name { get; set; }
        /// <summary>
        ///เลขที่ใบจ่ายล่วงหน้า
        /// </summary>
        public string buy_deposit { get; set; }
        /// <summary>
        ///เลขที่ใบจ่ายมัดจำ
        /// </summary>
        public string buy_deposit2 { get; set; }
        /// <summary>
        ///เลขที่ใบรับล่วงหน้า
        /// </summary>
        public string sell_deposit { get; set; }
        /// <summary>
        ///เลขที่ใบรับมัดจำ
        /// </summary>
        public string sell_deposit2 { get; set; }
        /// <summary>
        ///จำนวนจ่ายเงินล่วงหน้า
        /// </summary>
        public string pay_deposit_buy { get; set; }
        /// <summary>
        ///จำนวนเงินจ่ายมัดจำ
        /// </summary>
        public string pay_deposit_buy2 { get; set; }
        /// <summary>
        ///จำนวนเงินรับล่วงหน้า
        /// </summary>
        public string pay_deposit_sell { get; set; }
        /// <summary>
        ///จำนวนเงินรับมัดจำ
        /// </summary>
        public string pay_deposit_sell2 { get; set; }
        /// <summary>
        ///จ่ายคืนเงินล่วงหน้า
        /// </summary>
        public string deposit_buy { get; set; }
        /// <summary>
        ///จ่ายคืนเงินมัดจำ
        /// </summary>
        public string deposit_buy2 { get; set; }
        /// <summary>
        ///รับคืนเงินล่วงหน้า
        /// </summary>
        public string deposit_sell { get; set; }
        /// <summary>
        ///รับคืนเงินมัดจำ
        /// </summary>
        public string deposit_sell2 { get; set; }
        /// <summary>
        ///ตั้งหนี้ยกมา
        /// </summary>
        public string po_debt_balance { get; set; }
        /// <summary>
        ///ลดหนี้ยกมา
        /// </summary>
        public string po_cn_balance { get; set; }
        /// <summary>
        ///ซื้อสินค้า
        /// </summary>
        public string po_purchase { get; set; }
        /// <summary>
        ///เพิ่มสินค้า/เพิ่มหนี้
        /// </summary>
        public string po_addition_debt { get; set; }
        /// <summary>
        ///บิลขาย
        /// </summary>
        public string so_billing { get; set; }
        /// <summary>
        ///เพิ่มสินค้า/เพิ่มหนี้
        /// </summary>
        public string so_addition_debt { get; set; }
        /// <summary>
        ///ตั้งหนี้
        /// </summary>
        public string so_debt_balance { get; set; }
        /// <summary>
        ///ลดหนี้
        /// </summary>
        public string so_cn_balance { get; set; }
        /// <summary>
        ///ประเภทการขาย
        /// </summary>
        public string sale_type { get; set; }
        /// <summary>
        ///ประเภทการส่งคืน
        /// </summary>
        public string payable_add_type { get; set; }
        /// <summary>
        ///เพิ่มหนี้เจ้าหนี้, เพิ่มสินค้า
        /// </summary>
        public string payable_add_type_1 { get; set; }
        /// <summary>
        ///เพิ่มหนี้เจ้าหนี้, ราคาผิด (ไม่มีผลกับสต๊อกสินค้า)
        /// </summary>
        public string payable_add_type_2 { get; set; }
        /// <summary>
        ///ลดหนี้เจ้าหนี้, ลดสินค้า
        /// </summary>
        public string payable_sub_type_1 { get; set; }
        /// <summary>
        ///ลดหนี้เจ้าหนี้, ราคาผิด (ไม่มีผลกับสต๊อกสินค้า)
        /// </summary>
        public string payable_sub_type_2 { get; set; }
        /// <summary>
        ///เลขที่ใบสั่งซื้อ
        /// </summary>
        public string doc_no_po { get; set; }
        /// <summary>
        ///วันที่ใบสั่งซื้อ
        /// </summary>
        public string doc_date_po { get; set; }
        /// <summary>
        ///ประเภทการยกเลิก
        /// </summary>
        public int cancel_type { get; set; }
        /// <summary>
        ///ยกเลิกทั้งหมด
        /// </summary>
        public string cancel_type_1 { get; set; }
        /// <summary>
        ///ยกเลิกรายสินค้า
        /// </summary>
        public string cancel_type_2 { get; set; }
        /// <summary>
        ///สถานะ
        /// </summary>
        public int last_status { get; set; }
        /// <summary>
        ///มีการอ้างอิงแล้ว
        /// </summary>
        public int used_status { get; set; }
        /// <summary>
        ///สิ้นสุด
        /// </summary>
        public int doc_success { get; set; }
        /// <summary>
        ///เลขที่ใบจ่ายล่วงหน้า
        /// </summary>
        public string doc_advance_in_no { get; set; }
        /// <summary>
        ///วันที่ใบจ่ายล่วงหน้า
        /// </summary>
        public string doc_advance_in_date { get; set; }
        /// <summary>
        ///เลขที่ใบซื้อ
        /// </summary>
        public string doc_purchase_no { get; set; }
        /// <summary>
        ///วันที่ใบซื้อ
        /// </summary>
        public string doc_purchase_date { get; set; }
        /// <summary>
        ///ใบเพิ่มหนี้
        /// </summary>
        public string doc_purchase_debit_no { get; set; }
        /// <summary>
        ///วันที่เพิ่มหนี้
        /// </summary>
        public string doc_purchase_debit_date { get; set; }
        /// <summary>
        ///ใบลดหนี้
        /// </summary>
        public string doc_purchase_credit_no { get; set; }
        /// <summary>
        ///วันที่ลดหนี้
        /// </summary>
        public string doc_purchase_credit_date { get; set; }
        /// <summary>
        ///ใบเสนอราคา
        /// </summary>
        public string doc_quotation_no { get; set; }
        /// <summary>
        ///วันที่ใบเสนอราคา
        /// </summary>
        public string doc_quotation_date { get; set; }
        /// <summary>
        ///เลขที่ใบสั่งจอง
        /// </summary>
        public string doc_inquiry_no { get; set; }
        /// <summary>
        ///วันที่ใบสั่งจอง
        /// </summary>
        public string doc_inquiry_date { get; set; }
        /// <summary>
        ///เลขที่ใบสั่งขาย
        /// </summary>
        public string doc_saleorder_no { get; set; }
        /// <summary>
        ///วันที่ใบสั่งขาย
        /// </summary>
        public string doc_saleorder_date { get; set; }
        /// <summary>
        ///เลขที่ใบรับเงินล่วงหน้า
        /// </summary>
        public string doc_advance_so_no { get; set; }
        /// <summary>
        ///วันที่ใบรับเงินล่วงหน้า
        /// </summary>
        public string doc_advance_so_date { get; set; }
        /// <summary>
        ///เลขที่ใบคืนเงินล่วงหน้า
        /// </summary>
        public string doc_advance_so_return_no { get; set; }
        /// <summary>
        ///วันที่ใบคืนเงินล่วงหน้า
        /// </summary>
        public string doc_advance_so_return_date { get; set; }
        /// <summary>
        ///เลขที่ใบขายสินค้า/บริการ
        /// </summary>
        public string doc_invoice_no { get; set; }
        /// <summary>
        ///วันที่ใบขายสินค้า/บริการ
        /// </summary>
        public string doc_invoice_date { get; set; }
        /// <summary>
        ///เลขที่ใบเพิ่มหนี้
        /// </summary>
        public string doc_ar_debit_note_no { get; set; }
        /// <summary>
        ///วันที่ใบเพิ่มหนี้
        /// </summary>
        public string doc_ar_debit_note_date { get; set; }
        /// <summary>
        ///เลขที่ใบลดหนี้
        /// </summary>
        public string doc_ar_credit_note_no { get; set; }
        /// <summary>
        ///วันที่ใบลดหนี้
        /// </summary>
        public string doc_ar_credit_note_date { get; set; }
        /// <summary>
        ///ประเภทการเพิ่มหนี้
        /// </summary>
        public string sale_add_type { get; set; }
        /// <summary>
        ///บันทึกยอดรวมเอง
        /// </summary>
        public int total_manual { get; set; }
        /// <summary>
        ///มูลค่าหลังหักส่วนลด
        /// </summary>
        public string total_after_discount { get; set; }
        /// <summary>
        ///ชื่อลูกหนี้/เจ้าหนี้
        /// </summary>
        public string cust_name { get; set; }
        /// <summary>
        ///ชื่อเจ้าหนี้
        /// </summary>
        public string ap_name { get; set; }
        /// <summary>
        ///ยอดรวม
        /// </summary>
        public string sum_total { get; set; }
        /// <summary>
        ///ชื่อลูกหนี้
        /// </summary>
        public string ar_name { get; set; }
        /// <summary>
        ///รายละเอียด
        /// </summary>
        public string tab_detail { get; set; }
        /// <summary>
        ///เพิ่มเติม
        /// </summary>
        public string tab_more { get; set; }
        /// <summary>
        ///ระงับชั่วคราว
        /// </summary>
        public int on_hold { get; set; }
        /// <summary>
        ///ข้อความเพิ่มเติม
        /// </summary>
        public string extra_word { get; set; }
        /// <summary>
        ///ขนส่งโดย
        /// </summary>
        public string transport_code { get; set; }
        /// <summary>
        ///ผู้สั่งซื้อ
        /// </summary>
        public string purchase_name { get; set; }
        /// <summary>
        ///รายละเอียดการจ่ายเงิน
        /// </summary>
        public string tab_pay_out { get; set; }
        /// <summary>
        ///รายละเอียดการรับเงิน
        /// </summary>
        public string tab_pay_in { get; set; }
        /// <summary>
        ///ต้องการภายใน (วัน)
        /// </summary>
        public int want_day { get; set; }
        /// <summary>
        ///ต้องการภายใน (วันที่)
        /// </summary>
        public DateTime want_date { get; set; }
        /// <summary>
        ///ระยะเวลาภายใน (วัน)
        /// </summary>
        public int deposit_day { get; set; }
        /// <summary>
        ///ระยะเวลาภายใน (วันที่)
        /// </summary>
        public DateTime deposit_date { get; set; }
        /// <summary>
        ///หมายเหตุ
        /// </summary>
        public string remark2 { get; set; }
        /// <summary>
        ///ภาษีหัก ณ. ที่จ่าย
        /// </summary>
        public string tab_wht_in { get; set; }
        /// <summary>
        ///ภาษีถูกหัก ณ. ที่จ่าย
        /// </summary>
        public string tab_wht_out { get; set; }
        /// <summary>
        ///ภาษีมูลค่าเพิ่ม
        /// </summary>
        public string tab_vat { get; set; }
        /// <summary>
        ///กลุ่มพนักงานขาย
        /// </summary>
        public string sale_group { get; set; }
        /// <summary>
        ///เลขที่ใบเสนอซื้อ
        /// </summary>
        public string doc_po_request { get; set; }
        /// <summary>
        ///ไม่อนุมัติ
        /// </summary>
        public int not_approve_1 { get; set; }
        /// <summary>
        ///ผู้อนุมัติ
        /// </summary>
        public string user_approve { get; set; }
        /// <summary>
        ///ผู้ยกเลิก
        /// </summary>
        public string user_cancel { get; set; }
        /// <summary>
        ///ประเภทการรับคืน
        /// </summary>
        public string sale_return_type { get; set; }
        /// <summary>
        ///สถานะการอนุมัติ
        /// </summary>
        public int approve_status { get; set; }
        /// <summary>
        ///สถานะหมดอายุ
        /// </summary>
        public int expire_status { get; set; }
        /// <summary>
        ///ปัดเศษ
        /// </summary>
        public string sum_pay_rounding { get; set; }
        /// <summary>
        ///เงินสดย่อย
        /// </summary>
        public string sum_petty_cash { get; set; }
        /// <summary>
        ///เงินรับล่วงหน้า
        /// </summary>
        public string sum_deposit { get; set; }
        /// <summary>
        ///เงินมัดจำ
        /// </summary>
        public decimal sum_advance { get; set; }
        /// <summary>
        ///ยอดเงินนำไปใช้
        /// </summary>
        public string sum_used { get; set; }
        /// <summary>
        ///ตัดเงินมัดจำ
        /// </summary>
        public decimal advance_amount { get; set; }
        /// <summary>
        ///เงินมัดจำ
        /// </summary>
        public string tab_advance { get; set; }
        /// <summary>
        ///เลขที่ใบรับคืนจ่ายเงินล่วงหน้า
        /// </summary>
        public string doc_ref_name_1 { get; set; }
        /// <summary>
        ///เลขที่ใบรับคืนเงินมัดจำ
        /// </summary>
        public string doc_ref_name_2 { get; set; }
        /// <summary>
        ///รหัสเอกสาร
        /// </summary>
        public string doc_format_code { get; set; }
        /// <summary>
        ///รายละเอียดการจ่าย
        /// </summary>
        public string tab_pay { get; set; }
        /// <summary>
        ///ภาษีหัก ณ. ที่จ่าย
        /// </summary>
        public string tab_wht { get; set; }
        /// <summary>
        ///สถานะ 2
        /// </summary>
        public int used_status_2 { get; set; }
        /// <summary>
        ///หมายเหตุ 2
        /// </summary>
        public string remark_2 { get; set; }
        /// <summary>
        ///หมายเหตุ 3
        /// </summary>
        public string remark_3 { get; set; }
        /// <summary>
        ///ประเภทการลดหนี้
        /// </summary>
        public string return_type { get; set; }
        /// <summary>
        ///มูลค่าเอกสารเดิม
        /// </summary>
        public decimal ref_amount { get; set; }
        /// <summary>
        ///มูลค่าที่ถูกต้อง
        /// </summary>
        public decimal ref_new_amount { get; set; }
        /// <summary>
        ///ผลต่าง
        /// </summary>
        public decimal ref_diff { get; set; }
        /// <summary>
        ///แต้ม
        /// </summary>
        public decimal sum_point { get; set; }
        /// <summary>
        ///Cashier
        /// </summary>
        public string cashier_code { get; set; }
        /// <summary>
        ///จากระบบ POS
        /// </summary>
        public int is_pos { get; set; }
        /// <summary>
        ///หมายเลขเครื่อง POS
        /// </summary>
        public string pos_id { get; set; }
        /// <summary>
        ///รหัสสมาชิก
        /// </summary>
        public string member_code { get; set; }
        /// <summary>
        ///เลขที่เอกสารเปรียบเทียบ
        /// </summary>
        public string doc_no_guid { get; set; }
        /// <summary>
        ///0=อย่างย่อ,1=อย่างเต็ม
        /// </summary>
        public int pos_bill_type { get; set; }
        /// <summary>
        ///0=ปรกติ,1=ใบแทน
        /// </summary>
        public int pos_bill_change { get; set; }
        /// <summary>
        ///ประเภทเอกสาร
        /// </summary>
        public int doc_type { get; set; }
        /// <summary>
        ///ซื้อสินค้า
        /// </summary>
        public string doc_type_purchase { get; set; }
        /// <summary>
        ///รับสินค้าสำเร็จรูป
        /// </summary>
        public string doc_type_finish_good { get; set; }
        /// <summary>
        ///รับจากใบสั่งซื้อ
        /// </summary>
        public string wh_in_1 { get; set; }
        /// <summary>
        ///รับจากการซื้อ
        /// </summary>
        public string wh_in_2 { get; set; }
        /// <summary>
        ///รับจากการเพิ่มหนี้
        /// </summary>
        public string wh_in_3 { get; set; }
        /// <summary>
        ///ประเภทเอกสารรับ
        /// </summary>
        public string wh_in { get; set; }
        /// <summary>
        ///ส่งคืนจากใบรับสินค้า (คลัง)
        /// </summary>
        public string wh_out_1 { get; set; }
        /// <summary>
        ///ส่งคืนจากใบส่งคืน (บัญชี)
        /// </summary>
        public string wh_out_2 { get; set; }
        /// <summary>
        ///ส่งคืนจากใบลดหนี้ (บัญชี)
        /// </summary>
        public string wh_out_3 { get; set; }
        /// <summary>
        ///ประเภทเอกสารออก
        /// </summary>
        public string wh_out { get; set; }
        /// <summary>
        ///รับสินค้าสำเร็จรูป
        /// </summary>
        public string wh_in_4 { get; set; }
        /// <summary>
        ///รับคืนวัตถุดิบ
        /// </summary>
        public string wh_in_5 { get; set; }
        /// <summary>
        ///เตือนย้อนหลัง (วัน)
        /// </summary>
        public decimal recheck_count_day { get; set; }
        /// <summary>
        ///เปิดระบบเตือนตรวจนับซ้ำ
        /// </summary>
        public int recheck_count { get; set; }
        /// <summary>
        ///คลัง
        /// </summary>
        public string wh_from { get; set; }
        /// <summary>
        ///พื้นที่เก็บ
        /// </summary>
        public string location_from { get; set; }
        /// <summary>
        ///คลังปลายทาง
        /// </summary>
        public string wh_to { get; set; }
        /// <summary>
        ///ที่เก็บปลายทาง
        /// </summary>
        public string location_to { get; set; }
        /// <summary>
        ///สร้างโดยโปรแกรม
        /// </summary>
        public int auto_create { get; set; }
        /// <summary>
        ///ส่งคืนสินค้า
        /// </summary>
        public string payable_sub_type_1_1 { get; set; }
        /// <summary>
        ///ราคาผิด (ไม่มีผลกับสต๊อกสินค้า)
        /// </summary>
        public string payable_sub_type_1_2 { get; set; }
        /// <summary>
        ///ส่งคืนเงินสด
        /// </summary>
        public string payable_sub_type_3 { get; set; }
        /// <summary>
        ///ราคาผิดเงินสด (ไม่มีผลกับสต๊อกสินค้า)
        /// </summary>
        public string payable_sub_type_4 { get; set; }
        /// <summary>
        ///เพิ่มหนี้รายได้อื่นๆ เงินสด
        /// </summary>
        public string invoice_add_cash_other { get; set; }
        /// <summary>
        ///เพิ่มหนี้รายได้อื่นๆเงินสด (ค่าบริการ)
        /// </summary>
        public string invoice_add_cash_service_other { get; set; }
        /// <summary>
        ///ยอดตั้งหนี้
        /// </summary>
        public string total_dept_amount { get; set; }
        /// <summary>
        ///ยอดลดหนี้
        /// </summary>
        public string debt_reduction_amount { get; set; }
        /// <summary>
        ///ยอดเพิ่มหนี้
        /// </summary>
        public string increase_the_debt_amount { get; set; }
        /// <summary>
        ///ยอดจ่ายชำระ
        /// </summary>
        public string pay_debt_amount { get; set; }
        /// <summary>
        ///เกินกำหนด
        /// </summary>
        public string due_day_over { get; set; }
        /// <summary>
        ///สั่งจัดสินค้า
        /// </summary>
        public int send_to_pick_and_pack { get; set; }
        /// <summary>
        ///GL
        /// </summary>
        public string tab_gl { get; set; }
        /// <summary>
        ///ชื่อลูกค้า (อังกฤษ)
        /// </summary>
        public string cust_name_en { get; set; }
        /// <summary>
        ///Service Charge
        /// </summary>
        public string service_charge_word { get; set; }
        /// <summary>
        ///มูลค่า Service Charge
        /// </summary>
        public decimal total_service_charge { get; set; }
        /// <summary>
        ///รอบการขาย
        /// </summary>
        public string sale_shift_id { get; set; }
        /// <summary>
        ///ผู้สร้างเอกสาร
        /// </summary>
        public string creator_code { get; set; }
        /// <summary>
        ///ผู้แก้ไขเอกสารล่าสุด
        /// </summary>
        public string last_editor_code { get; set; }
        /// <summary>
        ///วันที่สร้างเอกสาร
        /// </summary>
        public DateTime create_datetime { get; set; }
        /// <summary>
        ///วันที่แก้ไขล่าสุด
        /// </summary>
        public DateTime lastedit_datetime { get; set; }
        /// <summary>
        ///เลขที่ใบรับเงินมัดจำ
        /// </summary>
        public string deposit_doc { get; set; }
        /// <summary>
        ///วันที่ใบรับเงินมัดจำ
        /// </summary>
        public string deposit_doc_date { get; set; }
        /// <summary>
        ///เลขที่ใบคืนเงินมัดจำ
        /// </summary>
        public string deposit_return_doc { get; set; }
        /// <summary>
        ///วันที่ใบคืนเงินมัดจำ
        /// </summary>
        public string deposit_return_doc_date { get; set; }
        /// <summary>
        ///หมายเลขโต๊ะ
        /// </summary>
        public string table_number { get; set; }
        /// <summary>
        ///GUID กะ
        /// </summary>
        public string period_guid { get; set; }
        /// <summary>
        ///ถึงสาขา
        /// </summary>
        public string branch_code_to { get; set; }
        /// <summary>
        ///ถึงการจัดสรร
        /// </summary>
        public string allocate_code_to { get; set; }
        /// <summary>
        ///ถึงโครงการ
        /// </summary>
        public string project_code_to { get; set; }
        /// <summary>
        ///ถึงงาน
        /// </summary>
        public string job_code_to { get; set; }
        /// <summary>
        ///ถึงฝ่าย
        /// </summary>
        public string side_code_to { get; set; }
        /// <summary>
        ///ถึงแผนก
        /// </summary>
        public string department_code_to { get; set; }
        /// <summary>
        ///หมายเลขโทรศัพท์
        /// </summary>
        public string point_telephone { get; set; }
        /// <summary>
        ///ยอดแต้มสะสมระบบกลาง
        /// </summary>
        public decimal sum_point_2 { get; set; }
        /// <summary>
        ///การจัดส่ง
        /// </summary>
        public string tab_shipment { get; set; }
        /// <summary>
        ///หมายเหตุ 4
        /// </summary>
        public string remark_4 { get; set; }
        /// <summary>
        ///หมายเหตุ 5
        /// </summary>
        public string remark_5 { get; set; }
        /// <summary>
        ///ภาษีซื้อ
        /// </summary>
        public string tab_vat_buy { get; set; }
        /// <summary>
        ///ภาษีขาย
        /// </summary>
        public string tab_vat_sale { get; set; }
        /// <summary>
        ///บันทึกรายการภาษีเอง
        /// </summary>
        public int is_manual_vat { get; set; }
        /// <summary>
        ///สมุดเงินฝาก
        /// </summary>
        public string pass_book_code { get; set; }
        /// <summary>
        ///มูลค่าสุทธิ(สกุลเงิน)
        /// </summary>
        public decimal total_amount_2 { get; set; }
        /// <summary>
        ///พนักงานขนส่ง
        /// </summary>
        public string sender_code { get; set; }
        /// <summary>
        ///เอกสารยกเลิก
        /// </summary>
        public int is_cancel { get; set; }
        /// <summary>
        ///ผู้ยกเลิกเอกสาร
        /// </summary>
        public string cancel_code { get; set; }
        /// <summary>
        ///เวลายกเลิกเอกสาร
        /// </summary>
        public DateTime cancel_datetime { get; set; }
        /// <summary>
        ///จ่ายมัดจำเงินสด
        /// </summary>
        public string deposit_payment_cash { get; set; }
        /// <summary>
        ///จ่ายมัดจำเงินเชื่อ
        /// </summary>
        public string deposit_payment_credit { get; set; }
        /// <summary>
        ///ผู้ขอโอน
        /// </summary>
        public string user_request_transfer { get; set; }
        /// <summary>
        ///ระงับเอกสาร
        /// </summary>
        public int is_hold { get; set; }
        /// <summary>
        ///อนุมัติโดยอัตโนมัติ
        /// </summary>
        public int auto_approved { get; set; }
        /// <summary>
        ///โอนมาจากPOS
        /// </summary>
        public int pos_transfer { get; set; }
        /// <summary>
        ///ปิดเอกสาร
        /// </summary>
        public int doc_close { get; set; }
        /// <summary>
        ///ประเภทเอกสาร
        /// </summary>
        public string ref_doc_type { get; set; }
        /// <summary>
        ///ARM
        /// </summary>
        public string is_arm { get; set; }
        /// <summary>
        ///ปรับปรุงสินค้า
        /// </summary>
        public string adjust_normal { get; set; }
        /// <summary>
        ///ปรับปรุงยกมาสินค้า
        /// </summary>
        public string adjust_stock_balance { get; set; }
        /// <summary>
        ///ปรับปรุงปิดสินค้า
        /// </summary>
        public string adjust_end_product { get; set; }
        /// <summary>
        ///เป็นเอกสารที่ออกแทน
        /// </summary>
        public int is_doc_copy { get; set; }
        /// <summary>
        ///เหตุผล
        /// </summary>
        public string doc_reason { get; set; }
        /// <summary>
        ///เหตุผลการลดหนี้
        /// </summary>
        public string cn_reason { get; set; }
        /// <summary>
        ///เหตุผลการเพิ่มหนี้
        /// </summary>
        public string dn_reason { get; set; }
        /// <summary>
        ///ไม่กระทบภาษี
        /// </summary>
        public string no_tax { get; set; }
        /// <summary>
        ///รหัสเอกสารปรับปรุงเพิ่ม
        /// </summary>
        public string doc_format_code_adjust { get; set; }
        /// <summary>
        ///เงินมัดจำคงเหลือ
        /// </summary>
        public string deposit_balance { get; set; }
        /// <summary>
        ///ส่วนลด(สกุลเงิน)
        /// </summary>
        public string discount_word_2 { get; set; }
        /// <summary>
        ///มูลค่าส่วนลด(สกุลเงิน)
        /// </summary>
        public decimal total_discount_2 { get; set; }
        /// <summary>
        ///มูลค่าสินค้า(สกุลเงิน)
        /// </summary>
        public decimal total_value_2 { get; set; }
    }


    /// <summary>
    /// รายละเอียดรายวัน
    /// </summary>
    public class ic_trans_detail : _jsondata
    {
        public ic_trans_detail(DataRow row)
        {
   
            this.trans_type = _getInt(row["trans_type"].ToString());
            this.trans_flag = _getInt(row["trans_flag"].ToString());
            this.doc_group = _getData(row, "doc_group");
             if (row["doc_date"].ToString().Length > 0)
            {
                this.doc_date = _getDate(row["doc_date"].ToString());
            }
            this.doc_no = _getData(row, "doc_no",false);
            this.doc_ref = _getData(row, "doc_ref");
            this.cust_code = _getData(row, "cust_code");
            this.inquiry_type = _getInt(row["inquiry_type"].ToString());
            this.item_code = _getData(row, "item_code", false);
            this.item_name = _getData(row, "item_name", false);
            this.unit_code = _getData(row, "unit_code", false);
            this.qty = _getDecimal(row["qty"].ToString());
            this.price = _getDecimal(row["price"].ToString());
            this.discount = _getData(row, "discount");
            this.sum_of_cost = _getDecimal(row["sum_of_cost"].ToString());
            this.sum_amount = _getDecimal(row["sum_amount"].ToString());
            if (row["due_date"].ToString().Length > 0)
            {
                this.due_date = _getDate(row["due_date"].ToString());
            }
            this.remark = _getData(row, "remark");
            this.status =_getInt(row["status"].ToString());
            this.line_number = _getInt(row["line_number"].ToString());
            this.ref_doc_no = _getData(row, "ref_doc_no");
            if (row["ref_doc_date"].ToString().Length > 0)
            {
                this.ref_doc_date = _getDate(row["ref_doc_date"].ToString());
            }
            this.ref_line_number = _getInt(row["ref_line_number"].ToString());
            this.ref_cust_code = _getData(row, "ref_cust_code");
            this.branch_code = _getData(row, "branch_code", false);
            this.wh_code = _getData(row, "wh_code", false);
            this.shelf_code = _getData(row, "shelf_code", false);
            this.wh_code_2 = _getData(row, "wh_code_2");
            this.shelf_code_2 = _getData(row, "shelf_code_2");
            this.department_code = _getData(row, "department_code");
            this.total_vat_value = _getDecimal(row["total_vat_value"].ToString());
            this.cancel_qty = _getDecimal(row["cancel_qty"].ToString());
            this.total_qty = _getDecimal(row["total_qty"].ToString());
            this.stand_value = _getDecimal(row["stand_value"].ToString());
            this.divide_value = _getDecimal(row["divide_value"].ToString());
            this.ratio = _getDecimal(row["ratio"].ToString());
            this.ic_pattern = _getData(row, "ic_pattern");
            this.ic_color = _getData(row, "ic_color");
            this.ic_size = _getData(row, "ic_size");
            this.priority_level = _getInt(row["priority_level"].ToString());
            this.calc_flag = _getInt(row["calc_flag"].ToString());
            this.last_status = _getInt(row["last_status"].ToString());
            this.set_ref_line = _getData(row, "set_ref_line");
            this.set_ref_price = _getDecimal(row["set_ref_price"].ToString());
            this.set_ref_qty = _getDecimal(row["set_ref_qty"].ToString());
            this.item_type = _getInt(row["item_type"].ToString());
            this.vat_type = _getInt(row["vat_type"].ToString());
            this.doc_ref_type = _getInt(row["doc_ref_type"].ToString());
            this.item_code_main = _getData(row, "item_code_main");
            this.ref_row = _getInt(row["ref_row"].ToString());
            this.ref_guid = _getData(row, "ref_guid");
            this.doc_time = _getData(row, "doc_time");
            this.is_permium = _getInt(row["is_permium"].ToString());
            this.is_get_price = _getInt(row["is_get_price"].ToString());
            this.average_cost = _getDecimal(row["average_cost"].ToString());
            this.sum_amount_exclude_vat = _getDecimal(row["sum_amount_exclude_vat"].ToString());
            if (row["doc_date_calc"].ToString().Length > 0)
            {
                this.doc_date_calc = _getDate(row["doc_date_calc"].ToString());
            }
            this.doc_time_calc = _getData(row, "doc_time_calc");
            this.discount_amount = _getDecimal(row["discount_amount"].ToString());
            this.price_exclude_vat = _getDecimal(row["price_exclude_vat"].ToString());
            this.user_approve = _getData(row, "user_approve");
            this.price_type = _getInt(row["price_type"].ToString());
            this.price_mode = _getInt(row["price_mode"].ToString());
            this.temp_float_1 = _getDecimal(row["temp_float_1"].ToString());
            this.temp_float_2 = _getDecimal(row["temp_float_2"].ToString());
            this.temp_string_1 = _getData(row, "temp_string_1");
            this.is_serial_number = _getInt(row["is_serial_number"].ToString());
            this.bank_name = _getData(row, "bank_name");
            this.bank_branch = _getData(row, "bank_branch");
            this.chq_number = _getData(row, "chq_number");
            this.barcode = _getData(row, "barcode");
            this.discount_number = _getInt(row["discount_number"].ToString());
            this.price_changed = _getInt(row["price_changed"].ToString());
            this.discount_changed = _getInt(row["discount_changed"].ToString());
            this.price_default = _getDecimal(row["price_default"].ToString());
            this.tax_type = _getInt(row["tax_type"].ToString());
            this.is_pos = _getInt(row["is_pos"].ToString());
            this.auto_create = _getInt(row["auto_create"].ToString());
            if (row["date_expire"].ToString().Length > 0)
            {
                this.date_expire = _getDate(row["date_expire"].ToString());
            }
            this.hidden_cost_1 = _getDecimal(row["hidden_cost_1"].ToString());
            this.hidden_cost_1_exclude_vat = _getDecimal(row["hidden_cost_1_exclude_vat"].ToString());
            this.hidden_cost_2 = _getDecimal(row["hidden_cost_2"].ToString());
            this.hidden_cost_2_exclude_vat = _getDecimal(row["hidden_cost_2_exclude_vat"].ToString());
            this.sum_of_cost_1 = _getDecimal(row["sum_of_cost_1"].ToString());
            this.average_cost_1 = _getDecimal(row["average_cost_1"].ToString());
            this.sale_code = _getData(row, "sale_code");
            this.sale_group = _getData(row, "sale_group");
            if (row["date_due"].ToString().Length > 0)
            {
                this.date_due = _getDate(row["date_due"].ToString());
            }
            this.lot_number_1 = _getData(row, "lot_number_1");
            this.item_code_2 = _getData(row, "item_code_2");
            this.bank_name_2 = _getData(row, "bank_name_2");
            this.bank_branch_2 = _getData(row, "bank_branch_2");
            this.sale_shift_id = _getData(row, "sale_shift_id");
            this.price_base = _getDecimal(row["price_base"].ToString());
            this.creator_code = _getData(row, "creator_code");
            this.last_editor_code = _getData(row, "last_editor_code");
            if (row["create_datetime"].ToString().Length > 0)
            {
                this.create_datetime = _getDate(row["create_datetime"].ToString());
            }
            if (row["lastedit_datetime"].ToString().Length > 0)
            {
                this.lastedit_datetime = _getDate(row["lastedit_datetime"].ToString());
            }
            this.fee_amount = _getDecimal(row["fee_amount"].ToString());
            this.transfer_amount = _getDecimal(row["transfer_amount"].ToString());
            this.price_set_ratio = _getDecimal(row["price_set_ratio"].ToString());
            if (row["mfd_date"].ToString().Length > 0)
            {
                this.mfd_date = _getDate(row["mfd_date"].ToString());
            }
            this.mfn_name = _getData(row, "mfn_name");
            this.price_2 = _getDecimal(row["price_2"].ToString());
            this.sum_amount_2 = _getDecimal(row["sum_amount_2"].ToString());
            this.price_guid = _getData(row, "price_guid");
            this.discount_amount_2 = _getDecimal(row["discount_amount_2"].ToString());
            this.is_lock_cost = _getInt(row["is_lock_cost"].ToString());
            this.sum_of_cost_fix = _getDecimal(row["sum_of_cost_fix"].ToString());
            if (row["create_date_time_now"].ToString().Length > 0)
            {
                this.create_date_time_now = _getDate(row["create_date_time_now"].ToString());
            }
            this.profit_lost_cost_amount = _getDecimal(row["profit_lost_cost_amount"].ToString());
            this.is_doc_copy = _getInt(row["is_doc_copy"].ToString());

        }

        public DateTime create_date_time_now { get; set; }
        /// <summary>
        ///ประเภทรายวัน
        /// </summary>
        public int trans_type { get; set; }
        /// <summary>
        ///รายวันระบบ
        /// </summary>
        public int trans_flag { get; set; }
        /// <summary>
        ///กลุ่มเอกสาร
        /// </summary>
        public string doc_group { get; set; }
        /// <summary>
        ///เอกสารวันที่
        /// </summary>
        public DateTime doc_date { get; set; }
        /// <summary>
        ///เอกสารเลขที่
        /// </summary>
        public string doc_no { get; set; }
        /// <summary>
        ///เอกสารอ้างอิง
        /// </summary>
        public string doc_ref { get; set; }
        /// <summary>
        ///ลูกหนี้/เจ้าหนี้
        /// </summary>
        public string cust_code { get; set; }
        /// <summary>
        ///ประเภทการซื้อ
        /// </summary>
        public int inquiry_type { get; set; }
        /// <summary>
        ///รหัสสินค้า
        /// </summary>
        public string item_code { get; set; }
        /// <summary>
        ///ชื่อสินค้า
        /// </summary>
        public string item_name { get; set; }
        /// <summary>
        ///หน่วยนับ
        /// </summary>
        public string unit_code { get; set; }
        /// <summary>
        ///ต้นทุน
        /// </summary>
        public decimal cost { get; set; }
        /// <summary>
        ///จำนวน
        /// </summary>
        public decimal qty { get; set; }
        /// <summary>
        ///ราคา
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        ///ส่วนลด
        /// </summary>
        public string discount { get; set; }
        /// <summary>
        ///รวมต้นทุน
        /// </summary>
        public decimal sum_of_cost { get; set; }
        /// <summary>
        ///รวมมูลค่า
        /// </summary>
        public decimal sum_amount { get; set; }
        /// <summary>
        ///วันที่ต้องการ
        /// </summary>
        public DateTime due_date { get; set; }
        /// <summary>
        ///หมายเหตุ
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        ///สถานะ
        /// </summary>
        public int status { get; set; }
        /// <summary>
        ///บรรทัด
        /// </summary>
        public int line_number { get; set; }
        /// <summary>
        ///อ้างอิง
        /// </summary>
        public string ref_doc_no { get; set; }
        /// <summary>
        ///อ้างอิง วันที่
        /// </summary>
        public DateTime ref_doc_date { get; set; }
        /// <summary>
        ///อ้างอิง บรรทัด
        /// </summary>
        public int ref_line_number { get; set; }
        /// <summary>
        ///อ้างอิง เจ้าหนี้/ลูกหนี้
        /// </summary>
        public string ref_cust_code { get; set; }
        /// <summary>
        ///สาขา
        /// </summary>
        public string branch_code { get; set; }
        /// <summary>
        ///คลัง
        /// </summary>
        public string wh_code { get; set; }
        /// <summary>
        ///พื้นที่เก็บ
        /// </summary>
        public string shelf_code { get; set; }
        /// <summary>
        ///คลัง
        /// </summary>
        public string wh_code_2 { get; set; }
        /// <summary>
        ///พื้นที่เก็บ
        /// </summary>
        public string shelf_code_2 { get; set; }
        /// <summary>
        ///แผนกที่ขออนุมัติ
        /// </summary>
        public string department_code { get; set; }
        /// <summary>
        ///จำนวนอนุมัติ
        /// </summary>
        public decimal approval_qty { get; set; }
        /// <summary>
        ///ภาษีมูลค่าเพิ่ม
        /// </summary>
        public decimal total_vat_value { get; set; }
        /// <summary>
        ///จำนวนยกเลิก
        /// </summary>
        public decimal cancel_qty { get; set; }
        /// <summary>
        ///จำนวนคงเหลือ
        /// </summary>
        public decimal total_qty { get; set; }
        /// <summary>
        ///ตัวตั้ง
        /// </summary>
        public decimal stand_value { get; set; }
        /// <summary>
        ///ตัวหาร
        /// </summary>
        public decimal divide_value { get; set; }
        /// <summary>
        ///อัตราส่วน
        /// </summary>
        public decimal ratio { get; set; }
        /// <summary>
        ///มิติ
        /// </summary>
        public string dimension { get; set; }
        /// <summary>
        ///รูปแบบสินค้า
        /// </summary>
        public string ic_pattern { get; set; }
        /// <summary>
        ///สีสินค้า
        /// </summary>
        public string ic_color { get; set; }
        /// <summary>
        ///ขนาดสินค้า
        /// </summary>
        public string ic_size { get; set; }
        /// <summary>
        ///หน่วยนับที่ใช้
        /// </summary>
        public string approval_unit { get; set; }
        /// <summary>
        ///เพิ่มเติม
        /// </summary>
        public string extra { get; set; }
        /// <summary>
        ///ความสำคัญ
        /// </summary>
        public int priority_level { get; set; }
        /// <summary>
        ///ปรกติ
        /// </summary>
        public string priority_level_1 { get; set; }
        /// <summary>
        ///ด่วน
        /// </summary>
        public string priority_level_2 { get; set; }
        /// <summary>
        ///ด่วนมาก
        /// </summary>
        public string priority_level_3 { get; set; }
        /// <summary>
        ///ด่วนมากพิเศษ
        /// </summary>
        public string priority_level_4 { get; set; }
        /// <summary>
        ///ยอดที่ตรวจนับได้
        /// </summary>
        public string check_stock_1 { get; set; }
        /// <summary>
        ///ผลต่าง
        /// </summary>
        public string check_stock_2 { get; set; }
        /// <summary>
        ///ชื่อหน่วยนับ
        /// </summary>
        public string unit_name { get; set; }
        /// <summary>
        ///ชื่อคลัง
        /// </summary>
        public string wh_name { get; set; }
        /// <summary>
        ///ชื่อพื้นที่เก็บ
        /// </summary>
        public string shelf_name { get; set; }
        /// <summary>
        ///ประเภทหน่วยนับ
        /// </summary>
        public string unit_type { get; set; }
        /// <summary>
        ///บวกหรือลบ (1=บวก,-1=ลบ)
        /// </summary>
        public int calc_flag { get; set; }
        /// <summary>
        ///สถานะล่าสุด (0=Normal,1=ยกเลิก)
        /// </summary>
        public int last_status { get; set; }
        /// <summary>
        ///จากคลัง
        /// </summary>
        public string wh_name_out { get; set; }
        /// <summary>
        ///จากที่เก็บ
        /// </summary>
        public string shelf_name_out { get; set; }
        /// <summary>
        ///เข้าคลัง
        /// </summary>
        public string wh_name_in { get; set; }
        /// <summary>
        ///เข้าที่เก็บ
        /// </summary>
        public string shelf_name_in { get; set; }
        /// <summary>
        ///เลขที่ใบเบิก
        /// </summary>
        public string doc_ref_requisition { get; set; }
        /// <summary>
        ///ชื่อคลัง
        /// </summary>
        public string wh_name_2 { get; set; }
        /// <summary>
        ///ชื่อพื้นที่เก็บ
        /// </summary>
        public string shelf_name_2 { get; set; }
        /// <summary>
        ///บรรทัดอ้างอิง (สินค้าชุด) 
        /// </summary>
        public string set_ref_line { get; set; }
        /// <summary>
        ///ราคาอ้างอิง (สินค้าชุด)
        /// </summary>
        public decimal set_ref_price { get; set; }
        /// <summary>
        ///จำนวนอ้างอิง (สินค้าชุด)
        /// </summary>
        public decimal set_ref_qty { get; set; }
        /// <summary>
        ///ประเภทสินค้า
        /// </summary>
        public int item_type { get; set; }
        /// <summary>
        ///เลขที่ใบสั่งซื้อ
        /// </summary>
        public string doc_ref_purchase_order { get; set; }
        /// <summary>
        ///เลขที่ใบเสนอราคา
        /// </summary>
        public string doc_ref_sale_order { get; set; }
        /// <summary>
        ///ประเภทภาษี (0=แยกนอก,1=รวมใน,2=อัตรา 0)
        /// </summary>
        public int vat_type { get; set; }
        /// <summary>
        ///เลขที่ใบซื้อสินค้า/สินค้าบริการ
        /// </summary>
        public string doc_ref_purchase { get; set; }
        /// <summary>
        ///ประเภทเอกสาร
        /// </summary>
        public int doc_ref_type { get; set; }
        /// <summary>
        ///สินค้าหลัก
        /// </summary>
        public string item_code_main { get; set; }
        /// <summary>
        ///ref_row
        /// </summary>
        public int ref_row { get; set; }
        /// <summary>
        ///ref_guid
        /// </summary>
        public string ref_guid { get; set; }
        /// <summary>
        ///เวลา
        /// </summary>
        public string doc_time { get; set; }
        /// <summary>
        ///1=ของแถม
        /// </summary>
        public int is_permium { get; set; }
        /// <summary>
        ///ให้ดึงราคาขาย (1=ดึง)
        /// </summary>
        public int is_get_price { get; set; }
        /// <summary>
        ///ราคาทุนเฉลี่ย
        /// </summary>
        public decimal average_cost { get; set; }
        /// <summary>
        ///รวมมูลค่า
        /// </summary>
        public decimal sum_amount_exclude_vat { get; set; }
        /// <summary>
        ///วันที่สำหรับคำนวณ
        /// </summary>
        public DateTime doc_date_calc { get; set; }
        /// <summary>
        ///เวลาสำหรับคำนวณ
        /// </summary>
        public string doc_time_calc { get; set; }
        /// <summary>
        ///กำไรขั้นต้น
        /// </summary>
        public string profit { get; set; }
        /// <summary>
        ///มูลค่าส่วนลด
        /// </summary>
        public decimal discount_amount { get; set; }
        /// <summary>
        ///ราคาไม่รวมภาษี
        /// </summary>
        public decimal price_exclude_vat { get; set; }
        /// <summary>
        ///รายละเอียดลูกหนี้/ลูกค้า
        /// </summary>
        public string ar_detail { get; set; }
        /// <summary>
        ///รายละเอียดเจ้าหนี้
        /// </summary>
        public string ap_detail { get; set; }
        /// <summary>
        ///ประเภทภาษี
        /// </summary>
        public string vat_type_word { get; set; }
        /// <summary>
        ///รหัสผู้อนุมัติ
        /// </summary>
        public string user_approve { get; set; }
        /// <summary>
        ///ประเภทราคา
        /// </summary>
        public int price_type { get; set; }
        /// <summary>
        ///กลุ่มราคา
        /// </summary>
        public int price_mode { get; set; }
        /// <summary>
        ///ราคาอนุมัติ
        /// </summary>
        public decimal approval_price { get; set; }
        /// <summary>
        ///ตัวเลข 1
        /// </summary>
        public decimal temp_float_1 { get; set; }
        /// <summary>
        ///ตัวเลข 2
        /// </summary>
        public decimal temp_float_2 { get; set; }
        /// <summary>
        ///ตัวอักษร 1
        /// </summary>
        public string temp_string_1 { get; set; }
        /// <summary>
        ///จำนวนสั่งซื้อ
        /// </summary>
        public string po_qty { get; set; }
        /// <summary>
        ///จำนวนสั่งซื้อคงเหลือ
        /// </summary>
        public string po_qty_2 { get; set; }
        /// <summary>
        ///เลขที่ใบรับ
        /// </summary>
        public string doc_ref_receive_no { get; set; }
        /// <summary>
        ///มูลค่าอนุมัติ
        /// </summary>
        public string approval_sum_amount { get; set; }
        /// <summary>
        ///ส่วนลดอนุมัติ
        /// </summary>
        public string approval_discount { get; set; }
        /// <summary>
        ///รหัสค่าใช้จ่าย
        /// </summary>
        public string expense_code { get; set; }
        /// <summary>
        ///ชื่อค่าใช้จ่าย
        /// </summary>
        public string expense_name { get; set; }
        /// <summary>
        ///คำอธิบายรายการ
        /// </summary>
        public string detail { get; set; }
        /// <summary>
        ///จำนวนเงิน
        /// </summary>
        public string amount2 { get; set; }
        /// <summary>
        ///หน่วยต้นทุนตัวตั้ง
        /// </summary>
        public string average_cost_stand { get; set; }
        /// <summary>
        ///หน่วยต้นทุนตัวหาร
        /// </summary>
        public string average_cost_div { get; set; }
        /// <summary>
        ///หมายเลขเครื่อง
        /// </summary>
        public string serial_number { get; set; }
        /// <summary>
        ///รหัสรายได้
        /// </summary>
        public string income_code { get; set; }
        /// <summary>
        ///ชื่อรายได้
        /// </summary>
        public string income_name { get; set; }
        /// <summary>
        ///มีหมายเลขเครื่อง
        /// </summary>
        public int is_serial_number { get; set; }
        /// <summary>
        ///รหัสเงินสดย่อย
        /// </summary>
        public string cash_sub_code { get; set; }
        /// <summary>
        ///ชื่อเงินสดย่อย
        /// </summary>
        public string cash_sub_name { get; set; }
        /// <summary>
        ///สมุดเงินฝาก
        /// </summary>
        public string book_bank_code { get; set; }
        /// <summary>
        ///ธนาคาร
        /// </summary>
        public string bank_name { get; set; }
        /// <summary>
        ///สาขา
        /// </summary>
        public string bank_branch { get; set; }
        /// <summary>
        ///เลขที่เช็ค
        /// </summary>
        public string chq_number { get; set; }
        /// <summary>
        ///สถานะ
        /// </summary>
        public int chq_flag { get; set; }
        /// <summary>
        ///ค่าใช้จ่าย
        /// </summary>
        public string expense { get; set; }
        /// <summary>
        ///เลขที่บัตรเครดิต
        /// </summary>
        public string credit_card_no { get; set; }
        /// <summary>
        ///Barcode
        /// </summary>
        public string barcode { get; set; }
        /// <summary>
        ///หมายเลขส่วนลด
        /// </summary>
        public int discount_number { get; set; }
        /// <summary>
        ///มีการแก้ไขราคา
        /// </summary>
        public int price_changed { get; set; }
        /// <summary>
        ///มีการแก้ไขส่วนลด
        /// </summary>
        public int discount_changed { get; set; }
        /// <summary>
        ///ราคาเริ่มต้น
        /// </summary>
        public decimal price_default { get; set; }
        /// <summary>
        ///ประเภทภาษี (0=มีภาษี,1=ยกเว้น)
        /// </summary>
        public int tax_type { get; set; }
        /// <summary>
        ///จาก POS
        /// </summary>
        public int is_pos { get; set; }
        /// <summary>
        ///สร้างโดยโปรแกรม
        /// </summary>
        public int auto_create { get; set; }
        /// <summary>
        ///หมดอายุ
        /// </summary>
        public DateTime date_expire { get; set; }
        /// <summary>
        ///ต้นทุนแฝงบริหาร
        /// </summary>
        public decimal hidden_cost_1 { get; set; }
        /// <summary>
        ///ต้นทุนแฝงบริหารไม่รวมภาษี
        /// </summary>
        public decimal hidden_cost_1_exclude_vat { get; set; }
        /// <summary>
        ///ต้นทุนแฝงบัญชี
        /// </summary>
        public decimal hidden_cost_2 { get; set; }
        /// <summary>
        ///ต้นทุนแฝงบัญชีไม่รวมภาษี
        /// </summary>
        public decimal hidden_cost_2_exclude_vat { get; set; }
        /// <summary>
        ///ต้นทุนรวมต้นทุนแฝงบริหาร
        /// </summary>
        public decimal sum_of_cost_1 { get; set; }
        /// <summary>
        ///ทุนแฝง
        /// </summary>
        public string hidden_cost_1_name_1 { get; set; }
        /// <summary>
        ///ราคาทุนเฉลี่ยแฝง
        /// </summary>
        public decimal average_cost_1 { get; set; }
        /// <summary>
        ///รายได้แฝง
        /// </summary>
        public string hidden_cost_1_name_2 { get; set; }
        /// <summary>
        ///ใบรับสินค้า
        /// </summary>
        public string doc_ref_partial { get; set; }
        /// <summary>
        ///รหัสพนักงานขาย
        /// </summary>
        public string sale_code { get; set; }
        /// <summary>
        ///กลุ่มพนักงานขาย
        /// </summary>
        public string sale_group { get; set; }
        /// <summary>
        ///วันครบกำหนด
        /// </summary>
        public DateTime date_due { get; set; }
        /// <summary>
        ///เลข LOT
        /// </summary>
        public string lot_number_1 { get; set; }
        /// <summary>
        ///ชื่อสินค้า (อังกฤษ)
        /// </summary>
        public string item_name_en { get; set; }
        /// <summary>
        ///หน่วยนับ (อังกฤษ)
        /// </summary>
        public string unit_name_en { get; set; }
        /// <summary>
        ///รหัสสินค้า
        /// </summary>
        public string item_code_2 { get; set; }
        /// <summary>
        ///จากสมุดเงินฝาก
        /// </summary>
        public string book_bank_code_out { get; set; }
        /// <summary>
        ///เข้าสมุดเงินฝาก
        /// </summary>
        public string book_bank_code_in { get; set; }
        /// <summary>
        ///ธนาคาร
        /// </summary>
        public string bank_name_2 { get; set; }
        /// <summary>
        ///สาขา
        /// </summary>
        public string bank_branch_2 { get; set; }
        /// <summary>
        ///ใช้ระบบวันหมดอายุ
        /// </summary>
        public string use_expire { get; set; }
        /// <summary>
        ///รอบการขาย
        /// </summary>
        public string sale_shift_id { get; set; }
        /// <summary>
        ///ราคากลาง
        /// </summary>
        public decimal price_base { get; set; }
        /// <summary>
        ///ผู้สร้างเอกสาร
        /// </summary>
        public string creator_code { get; set; }
        /// <summary>
        ///ผู้แก้ไขเอกสารล่าสุด
        /// </summary>
        public string last_editor_code { get; set; }
        /// <summary>
        ///วันที่สร้างเอกสาร
        /// </summary>
        public DateTime create_datetime { get; set; }
        /// <summary>
        ///วันที่แก้ไขล่าสุด
        /// </summary>
        public DateTime lastedit_datetime { get; set; }
        /// <summary>
        ///ค่าธรรมเนียม
        /// </summary>
        public decimal fee_amount { get; set; }
        /// <summary>
        ///ยอดโอนเงิน
        /// </summary>
        public decimal transfer_amount { get; set; }
        /// <summary>
        ///อัตราส่วนราคาสินค้าชุด
        /// </summary>
        public decimal price_set_ratio { get; set; }
        /// <summary>
        ///จำนวนพิมพ์
        /// </summary>
        public string print_qty { get; set; }
        /// <summary>
        ///วันที่ผลิต
        /// </summary>
        public DateTime mfd_date { get; set; }
        /// <summary>
        ///ผู้ผลิต
        /// </summary>
        public string mfn_name { get; set; }
        /// <summary>
        ///จำนวนเริ่มต้น
        /// </summary>
        public string start_qty { get; set; }
        /// <summary>
        ///จำนวนสินสุด
        /// </summary>
        public string end_qty { get; set; }
        /// <summary>
        ///แผนก
        /// </summary>
        public string department { get; set; }
        /// <summary>
        ///การจัดสรร
        /// </summary>
        public string allocate { get; set; }
        /// <summary>
        ///โครงการ
        /// </summary>
        public string project { get; set; }
        /// <summary>
        ///ราคา
        /// </summary>
        public decimal price_2 { get; set; }
        /// <summary>
        ///รวมมูลค่า
        /// </summary>
        public decimal sum_amount_2 { get; set; }
        /// <summary>
        ///Price Guid
        /// </summary>
        public string price_guid { get; set; }
        /// <summary>
        ///มูลค่าส่วนลด
        /// </summary>
        public decimal discount_amount_2 { get; set; }
        /// <summary>
        ///กำหนดต้นทุนเอง
        /// </summary>
        public int is_lock_cost { get; set; }
        /// <summary>
        ///ต้นทุน
        /// </summary>
        public decimal sum_of_cost_fix { get; set; }
        /// <summary>
        ///กำไรขาดทุน
        /// </summary>
        public decimal profit_lost_cost_amount { get; set; }
        /// <summary>
        ///เป็นเอกสารที่ออกแทน
        /// </summary>
        public int is_doc_copy { get; set; }
    }
    

    /// <summary>
    /// สินค้า
    /// </summary>
    public class ic_inventory : _jsondata
    {
        public ic_inventory(DataRow row)
        {
            this.code = _getData(row, "code",false);
            this.code_old = _getData(row, "code_old");
            this.name_1 = _getData(row, "name_1", false);
            this.name_2 = _getData(row, "name_2");
            this.name_eng_1 = _getData(row, "name_eng_1");
            this.name_eng_2 = _getData(row, "name_eng_2");
            this.name_market = _getData(row, "name_market");
            this.name_for_bill = _getData(row, "name_for_bill");
            this.short_name = _getData(row, "short_name");
            this.name_for_pos = _getData(row, "name_for_pos");
            this.name_for_search = _getData(row, "name_for_search");
            this.item_type = _getInt(row["item_type"].ToString());
            this.item_category = _getData(row, "item_category");
            this.group_main = _getData(row, "group_main");
            this.item_brand = _getData(row, "item_brand");
            this.item_pattern = _getData(row, "item_pattern");
            this.item_design = _getData(row, "item_design");
            this.item_grade = _getData(row, "item_grade");
            this.item_class = _getData(row, "item_class");
            this.item_size = _getData(row, "item_size");
            this.item_color = _getData(row, "item_color");
            this.item_character = _getData(row, "item_character");
            this.item_status = _getInt(row["item_status"].ToString());
            this.unit_type = _getInt(row["unit_type"].ToString());
            this.cost_type = _getInt(row["cost_type"].ToString());
            this.tax_type = _getInt(row["tax_type"].ToString());
            this.item_sale_type = _getInt(row["item_sale_type"].ToString());
            this.item_rent_type = _getInt(row["item_rent_type"].ToString());
            this.unit_standard = _getData(row, "unit_standard", false);
            this.unit_cost = _getData(row, "unit_cost", false);
            this.income_type = _getData(row, "income_type");
            this.description = _getData(row, "description");
            this.item_model = _getData(row, "item_model");
            this.ic_serial_no = _getInt(row["ic_serial_no"].ToString());
            this.remark = _getData(row, "remark");
            this.status = _getInt(row["status"].ToString());
            this.guid_code = _getData(row, "guid_code");
            if (row["last_movement_date"].ToString().Length > 0)
            {
                this.last_movement_date = _getDate(row["last_movement_date"].ToString());
            }
            this.average_cost = _getDecimal(row["average_cost"].ToString());
            this.item_in_stock = _getDecimal(row["item_in_stock"].ToString());
            this.balance_qty = _getDecimal(row["balance_qty"].ToString());
            this.accrued_in_qty = _getDecimal(row["accrued_in_qty"].ToString());
            this.accrued_out_qty = _getDecimal(row["accrued_out_qty"].ToString());
            this.unit_standard_name = _getData(row, "unit_standard_name");
            this.update_price = _getInt(row["update_price"].ToString());
            this.update_detail = _getInt(row["update_detail"].ToString());
            this.account_code_1 = _getData(row, "account_code_1");
            this.account_code_2 = _getData(row, "account_code_2");
            this.account_code_3 = _getData(row, "account_code_3");
            this.account_code_4 = _getData(row, "account_code_4");
            this.book_out_qty = _getDecimal(row["book_out_qty"].ToString());
            this.doc_format_code = _getData(row, "doc_format_code");
            this.unit_standard_stand_value = _getDecimal(row["unit_standard_stand_value"].ToString());
            this.unit_standard_divide_value = _getDecimal(row["unit_standard_divide_value"].ToString());
            this.sign_code = _getData(row, "sign_code");
            this.supplier_code = _getData(row, "supplier_code");
            this.fixed_cost = _getDecimal(row["fixed_cost"].ToString());
            this.drink_type = _getInt(row["drink_type"].ToString());
            this.average_cost_1 = _getDecimal(row["average_cost_1"].ToString());
            this.group_sub = _getData(row, "group_sub");
            this.use_expire = _getInt(row["use_expire"].ToString());
            this.barcode_checker_print = _getInt(row["barcode_checker_print"].ToString());
            this.print_order_per_unit = _getInt(row["print_order_per_unit"].ToString());
            this.production_period = _getInt(row["production_period"].ToString());
            this.is_new_item = _getInt(row["is_new_item"].ToString());
            this.commission_rate = _getData(row, "commission_rate");
            this.is_eordershow = _getInt(row["is_eordershow"].ToString());
            this.no_discount = _getInt(row["no_discount"].ToString());
            this.serial_no_format = _getData(row, "serial_no_format");
            this.pos_no_sum = _getInt(row["pos_no_sum"].ToString());
            this.item_promote = _getInt(row["item_promote"].ToString());
            this.sum_sale_1 = _getDecimal(row["sum_sale_1"].ToString());
            this.is_speech = _getInt(row["is_speech"].ToString());
            this.medicine_register_number = _getData(row, "medicine_register_number");
            this.medicine_standard_code = _getData(row, "medicine_standard_code");
            this.quantity = _getData(row, "quantity");
            this.degree = _getData(row, "degree");
            this.is_product_boonrawd = _getInt(row["is_product_boonrawd"].ToString());
            if (row["create_date_time_now"].ToString().Length > 0)
            {
                this.create_date_time_now = _getDate(row["create_date_time_now"].ToString());
            }
            this.tpu_code = _getData(row, "tpu_code");
            this.gpu_code = _getData(row, "gpu_code");

        }
        public DateTime create_date_time_now { get; set; }
        
        /// <summary>
        ///รหัสสินค้า
        /// </summary>
        public string code { get; set; }
        /// <summary>
        ///รหัสสินค้าเก่า
        /// </summary>
        public string code_old { get; set; }
        /// <summary>
        ///ชื่อสินค้า 1
        /// </summary>
        public string name_1 { get; set; }
        /// <summary>
        ///ชื่อสินค้า 2
        /// </summary>
        public string name_2 { get; set; }
        /// <summary>
        ///ชื่อ (ภาษาอังกฤษ) 1
        /// </summary>
        public string name_eng_1 { get; set; }
        /// <summary>
        ///ชื่อ (ภาษาอังกฤษ) 2
        /// </summary>
        public string name_eng_2 { get; set; }
        /// <summary>
        ///ชื่อทางการตลาด
        /// </summary>
        public string name_market { get; set; }
        /// <summary>
        ///ชื่อสำหรับออกบิล
        /// </summary>
        public string name_for_bill { get; set; }
        /// <summary>
        ///ชื่อสินค้าแบบย่อ
        /// </summary>
        public string short_name { get; set; }
        /// <summary>
        ///ชื่อสำหรับขายหน้าร้าน
        /// </summary>
        public string name_for_pos { get; set; }
        /// <summary>
        ///ชื่อสำหรับการค้นหา
        /// </summary>
        public string name_for_search { get; set; }
        /// <summary>
        ///ประเภทสินค้า
        /// </summary>
        public int item_type { get; set; }
        /// <summary>
        ///หมวดสินค้า
        /// </summary>
        public string item_category { get; set; }
        /// <summary>
        ///กลุ่มสินค้าหลัก
        /// </summary>
        public string group_main { get; set; }
        /// <summary>
        ///ยี่ห้อสินค้า
        /// </summary>
        public string item_brand { get; set; }
        /// <summary>
        ///รูปแบบสินค้า
        /// </summary>
        public string item_pattern { get; set; }
        /// <summary>
        ///รูปทรงสินค้า
        /// </summary>
        public string item_design { get; set; }
        /// <summary>
        ///เกรดสินค้า
        /// </summary>
        public string item_grade { get; set; }
        /// <summary>
        ///ระดับสินค้า
        /// </summary>
        public string item_class { get; set; }
        /// <summary>
        ///ขนาดสินค้า
        /// </summary>
        public string item_size { get; set; }
        /// <summary>
        ///สีสินค้า
        /// </summary>
        public string item_color { get; set; }
        /// <summary>
        ///ลักษณะสินค้า
        /// </summary>
        public string item_character { get; set; }
        /// <summary>
        ///สถานะสินค้า
        /// </summary>
        public int item_status { get; set; }
        /// <summary>
        ///ประเภทหน่วยนับ
        /// </summary>
        public int unit_type { get; set; }
        /// <summary>
        ///ประเภทต้นทุน
        /// </summary>
        public int cost_type { get; set; }
        /// <summary>
        ///ประเภทภาษี
        /// </summary>
        public int tax_type { get; set; }
        /// <summary>
        ///สินค้าฝากขาย
        /// </summary>
        public int item_sale_type { get; set; }
        /// <summary>
        ///สินค้าเช่า
        /// </summary>
        public int item_rent_type { get; set; }
        /// <summary>
        ///หน่วยยอดคงเหลือ
        /// </summary>
        public string unit_standard { get; set; }
        /// <summary>
        ///หน่วยต้นทุน
        /// </summary>
        public string unit_cost { get; set; }
        /// <summary>
        ///ประเภทรายได้
        /// </summary>
        public string income_type { get; set; }
        /// <summary>
        ///รายละเอียด
        /// </summary>
        public string description { get; set; }
        /// <summary>
        ///รุ่นสินค้า
        /// </summary>
        public string item_model { get; set; }
        /// <summary>
        ///สินค้ามี Serial
        /// </summary>
        public int ic_serial_no { get; set; }
        /// <summary>
        ///หมายเหตุ
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        ///สถานะ
        /// </summary>
        public int status { get; set; }
        /// <summary>
        ///GUID
        /// </summary>
        public string guid_code { get; set; }
        /// <summary>
        ///วันที่เคลื่อนไหวล่าสุด
        /// </summary>
        public DateTime last_movement_date { get; set; }
        /// <summary>
        ///สินค้าทั่วไป
        /// </summary>
        public string ic_normal { get; set; }
        /// <summary>
        ///สินค้าบริการ (ไม่ทำสต๊อก)
        /// </summary>
        public string ic_service { get; set; }
        /// <summary>
        ///สินค้าให้เช่า
        /// </summary>
        public string ic_rent { get; set; }
        /// <summary>
        ///สินค้าฝากขาย
        /// </summary>
        public string ic_consignment { get; set; }
        /// <summary>
        ///สินค้าชุด  (ไม่ทำสต๊อก)
        /// </summary>
        public string ic_set { get; set; }
        /// <summary>
        ///ต้นทุนเฉลี่ย
        /// </summary>
        public decimal average_cost { get; set; }
        /// <summary>
        ///ต้นทุนมาตรฐาน
        /// </summary>
        public string standard_cost { get; set; }
        /// <summary>
        ///ต้นทุน LOT FIFO
        /// </summary>
        public string fifo_cost { get; set; }
        /// <summary>
        ///หน่วยนับเดี่ยว
        /// </summary>
        public string single_unit { get; set; }
        /// <summary>
        ///หลายหน่วยนับ
        /// </summary>
        public string many_unit { get; set; }
        /// <summary>
        ///หน่วยนับขนาน
        /// </summary>
        public string double_unit { get; set; }
        /// <summary>
        ///หลายหน่วยนับ+หน่วยนับขนาน
        /// </summary>
        public string many_unit_and_double_unit { get; set; }
        /// <summary>
        ///ภาษีอัตราศูนย์
        /// </summary>
        public string zero_vat { get; set; }
        /// <summary>
        ///ภาษีกรมสรรพสามิต
        /// </summary>
        public string excise_vat { get; set; }
        /// <summary>
        ///ยกเว้นภาษี
        /// </summary>
        public string exc_vat { get; set; }
        /// <summary>
        ///ภาษีมูลค่าเพิ่ม
        /// </summary>
        public string normal_vat { get; set; }
        /// <summary>
        ///กว้างxยาวxสูง
        /// </summary>
        public string width_length_height { get; set; }
        /// <summary>
        ///น้ำหนัก(กก)
        /// </summary>
        public string weight { get; set; }
        /// <summary>
        ///จำนวนสินค้า
        /// </summary>
        public decimal item_in_stock { get; set; }
        /// <summary>
        ///ยอดคงเหลือ
        /// </summary>
        public decimal balance_qty { get; set; }
        /// <summary>
        ///ยอดค้างรับ
        /// </summary>
        public decimal accrued_in_qty { get; set; }
        /// <summary>
        ///ยอดค้างส่ง
        /// </summary>
        public decimal accrued_out_qty { get; set; }
        /// <summary>
        ///สูตรการผลิต
        /// </summary>
        public string ic_color { get; set; }
        /// <summary>
        ///สีผสม
        /// </summary>
        public string ic_color_mixed { get; set; }
        /// <summary>
        ///หน่วยคงเหลือ
        /// </summary>
        public string unit_standard_name { get; set; }
        /// <summary>
        ///แก้ไขตารางราคา
        /// </summary>
        public int update_price { get; set; }
        /// <summary>
        ///แก้ไขรายละเอียดประกอบ
        /// </summary>
        public int update_detail { get; set; }
        /// <summary>
        ///รหัสผังบัญชีสินค้า
        /// </summary>
        public string account_code_1 { get; set; }
        /// <summary>
        ///รหัสผังบัญชีต้นทุน
        /// </summary>
        public string account_code_2 { get; set; }
        /// <summary>
        ///รหัสผังบัญชีรายได้
        /// </summary>
        public string account_code_3 { get; set; }
        /// <summary>
        ///รหัสผังบัญชีรับคืน
        /// </summary>
        public string account_code_4 { get; set; }
        /// <summary>
        ///ยอดค้างจอง
        /// </summary>
        public decimal book_out_qty { get; set; }
        /// <summary>
        ///รหัสเอกสาร
        /// </summary>
        public string doc_format_code { get; set; }
        /// <summary>
        ///อัตราส่วนยอดคงเหลือตัวตั้ง
        /// </summary>
        public decimal unit_standard_stand_value { get; set; }
        /// <summary>
        ///อัตราส่วนยอดคงเหลือตัวหาร
        /// </summary>
        public decimal unit_standard_divide_value { get; set; }
        /// <summary>
        ///เครื่องหมาย
        /// </summary>
        public string sign_code { get; set; }
        /// <summary>
        ///ผู้จัดจำหน่ายหลัก
        /// </summary>
        public string supplier_code { get; set; }
        /// <summary>
        ///สะสมแต้ม
        /// </summary>
        public string have_point { get; set; }
        /// <summary>
        ///ต้นทุนคงที่
        /// </summary>
        public decimal fixed_cost { get; set; }
        /// <summary>
        ///ประเภทเครื่องดื่ม
        /// </summary>
        public int drink_type { get; set; }
        /// <summary>
        ///ต้นทุนเฉลี่ยแฝงบริหาร
        /// </summary>
        public decimal average_cost_1 { get; set; }
        /// <summary>
        ///มีสินค้าทดแทน
        /// </summary>
        public string have_replacement { get; set; }
        /// <summary>
        ///กลุ่มสินค้าย่อย
        /// </summary>
        public string group_sub { get; set; }
        /// <summary>
        ///ใช้ระบบหมดอายุ
        /// </summary>
        public int use_expire { get; set; }
        /// <summary>
        ///ต้นทุน FEFO (วันหมดอายุ)
        /// </summary>
        public string expire_cost { get; set; }
        /// <summary>
        ///ต้นทุน LOT กำหนดเอง
        /// </summary>
        public string assign_cost { get; set; }
        /// <summary>
        ///พิมพ์บาร์โค๊ดตรวจสอบ
        /// </summary>
        public int barcode_checker_print { get; set; }
        /// <summary>
        ///พิมพ์ใบจัดรายการแบบแยก
        /// </summary>
        public int print_order_per_unit { get; set; }
        /// <summary>
        ///ระยะเวลาในการผลิต(นาที)
        /// </summary>
        public int production_period { get; set; }
        /// <summary>
        ///เป็นสินค้าใหม่
        /// </summary>
        public int is_new_item { get; set; }
        /// <summary>
        ///อัตราค่าคอมมิสชั่น
        /// </summary>
        public string commission_rate { get; set; }
        /// <summary>
        ///แสดงในระบบ e-Order
        /// </summary>
        public int is_eordershow { get; set; }
        /// <summary>
        ///สินค้าไม่ลดราคา
        /// </summary>
        public int no_discount { get; set; }
        /// <summary>
        ///รูปแบบ Serial
        /// </summary>
        public string serial_no_format { get; set; }
        /// <summary>
        ///ไม่รวมรายการในระบบ POS
        /// </summary>
        public int pos_no_sum { get; set; }
        /// <summary>
        ///เป็นสินค้าแนะนำ
        /// </summary>
        public int item_promote { get; set; }
        /// <summary>
        ///ยอดขายตามจำนวน
        /// </summary>
        public decimal sum_sale_1 { get; set; }
        /// <summary>
        ///พูดรายการอาหาร
        /// </summary>
        public int is_speech { get; set; }
        /// <summary>
        ///ต้นทุนแบบพิเศษ 1
        /// </summary>
        public int other_cost_1 { get; set; }
        /// <summary>
        ///เลขทะเบียนยา
        /// </summary>
        public string medicine_register_number { get; set; }
        /// <summary>
        ///เลขทะเบียนยา 24
        /// </summary>
        public string medicine_standard_code { get; set; }
        /// <summary>
        ///ปริมาณ
        /// </summary>
        public string quantity { get; set; }
        /// <summary>
        ///ดีกรี
        /// </summary>
        public string degree { get; set; }
        /// <summary>
        ///สินค้าบุญรอด
        /// </summary>
        public int is_product_boonrawd { get; set; }
        /// <summary>
        ///สินค้าพรีเมี่ยม
        /// </summary>
        public string is_premium { get; set; }
        /// <summary>
        ///TPU
        /// </summary>
        public string tpu_code { get; set; }
        /// <summary>
        ///GPU
        /// </summary>
        public string gpu_code { get; set; }
        /// <summary>
        ///กลุ่มสินค้าย่อย2
        /// </summary>
        public string group_sub2 { get; set; }
    }



    /// <summary>
    /// รายละเอียดสินค้า
    /// </summary>
    public class ic_inventory_detail : _jsondata
    {
        public ic_inventory_detail(DataRow row) {
            this.ic_code = _getData(row, "ic_code",false);
            this.formular = _getData(row, "formular");
            this.po_over = _getInt(row["po_over"].ToString());
            this.so_over = _getInt(row["so_over"].ToString());
            this.account_group = _getData(row, "account_group");
            this.serial_number = _getData(row, "serial_number");
            this.tax_import = _getData(row, "tax_import");
            this.tax_rate = _getDecimal(row["tax_rate"].ToString());
            this.purchase_manager = _getData(row, "purchase_manager");
            this.sale_manager = _getData(row, "sale_manager");
            this.start_purchase_wh = _getData(row, "start_purchase_wh");
            this.start_purchase_shelf = _getData(row, "start_purchase_shelf");
            this.start_purchase_unit = _getData(row, "start_purchase_unit");
            this.start_sale_wh = _getData(row, "start_sale_wh");
            this.start_sale_shelf = _getData(row, "start_sale_shelf");
            this.start_sale_unit = _getData(row, "start_sale_unit");
            this.cost_produce = _getDecimal(row["cost_produce"].ToString());
            this.cost_standard = _getDecimal(row["cost_standard"].ToString());
            this.unit_for_stock = _getData(row, "unit_for_stock");
            this.ic_out_wh = _getData(row, "ic_out_wh");
            this.ic_out_shelf = _getData(row, "ic_out_shelf");
            this.ic_reserve_wh = _getData(row, "ic_reserve_wh");
            this.reserve_status = _getInt(row["reserve_status"].ToString());
            this.discount = _getData(row, "discount");
            this.purchase_point = _getDecimal(row["purchase_point"].ToString());
            this.unit_2_code = _getData(row, "unit_2_code");
            this.unit_2_qty = _getInt(row["unit_2_qty"].ToString());
            this.unit_2_average = _getInt(row["unit_2_average"].ToString());
            this.unit_2_average_value = _getDecimal(row["unit_2_average_value"].ToString());
            this.user_group_for_purchase = _getData(row, "user_group_for_purchase");
            this.user_group_for_sale = _getData(row, "user_group_for_sale");
            this.user_group_for_manage = _getData(row, "user_group_for_manage");
            this.user_group_for_warehouse = _getData(row, "user_group_for_warehouse");
            this.user_status = _getInt(row["user_status"].ToString());
            this.close_reason = _getData(row, "close_reason");
            if (row["close_date"].ToString().Length > 0)
            {
                this.close_date = _getDate(row["close_date"].ToString());
            }
            this.ref_file_1 = _getData(row, "ref_file_1");
            this.ref_file_2 = _getData(row, "ref_file_2");
            this.ref_file_3 = _getData(row, "ref_file_3");
            this.ref_file_4 = _getData(row, "ref_file_4");
            this.ref_file_5 = _getData(row, "ref_file_5");
            this.dimension_1 = _getData(row, "dimension_1");
            this.dimension_2 = _getData(row, "dimension_2");
            this.dimension_3 = _getData(row, "dimension_3");
            this.dimension_4 = _getData(row, "dimension_4");
            this.dimension_5 = _getData(row, "dimension_5");
            this.dimension_6 = _getData(row, "dimension_6");
            this.dimension_7 = _getData(row, "dimension_7");
            this.dimension_8 = _getData(row, "dimension_8");
            this.dimension_9 = _getData(row, "dimension_9");
            this.dimension_10 = _getData(row, "dimension_10");
            this.sale_price_1 = _getDecimal(row["sale_price_1"].ToString());
            this.sale_price_2 = _getDecimal(row["sale_price_2"].ToString());
            this.sale_price_3 = _getDecimal(row["sale_price_3"].ToString());
            this.sale_price_4 = _getDecimal(row["sale_price_4"].ToString());
            this.maximum_qty = _getDecimal(row["maximum_qty"].ToString());
            this.minimum_qty = _getDecimal(row["minimum_qty"].ToString());
            this.dimension_11 = _getData(row, "dimension_11");
            this.dimension_12 = _getData(row, "dimension_12");
            this.dimension_13 = _getData(row, "dimension_13");
            this.dimension_14 = _getData(row, "dimension_14");
            this.dimension_15 = _getData(row, "dimension_15");
            this.dimension_16 = _getData(row, "dimension_16");
            this.dimension_17 = _getData(row, "dimension_17");
            this.dimension_18 = _getData(row, "dimension_18");
            this.dimension_19 = _getData(row, "dimension_19");
            this.dimension_20 = _getData(row, "dimension_20");
            this.accrued_control = _getInt(row["accrued_control"].ToString());
            this.lock_price = _getInt(row["lock_price"].ToString());
            this.lock_discount =_getInt(row["lock_discount"].ToString());
            this.lock_cost = _getInt(row["lock_cost"].ToString());
            this.is_end = _getInt(row["is_end"].ToString());
            this.is_hold_purchase = _getInt(row["is_hold_purchase"].ToString());
            this.is_hold_sale = _getInt(row["is_hold_sale"].ToString());
            this.is_stop = _getInt(row["is_stop"].ToString());
            this.balance_control = _getInt(row["balance_control"].ToString());
            this.have_point = _getInt(row["have_point"].ToString());
            this.start_unit_code = _getData(row, "start_unit_code",false);
            this.dimension_21 = _getData(row, "dimension_21");
            this.dimension_22 = _getData(row, "dimension_22");
            this.dimension_23 = _getData(row, "dimension_23");
            this.dimension_24 = _getData(row, "dimension_24");
            this.dimension_25 = _getData(row, "dimension_25");
            this.is_premium = _getInt(row["is_premium"].ToString());
            if (row["create_date_time_now"].ToString().Length > 0)
            {
                this.create_date_time_now = _getDate(row["create_date_time_now"].ToString());
            }
            this.dimension_26 = _getData(row, "dimension_26");
            this.dimension_27 = _getData(row, "dimension_27");
            this.dimension_28 = _getData(row, "dimension_28");
            this.dimension_29 = _getData(row, "dimension_29");
            this.dimension_30 = _getData(row, "dimension_30");
            this.dimension_31 = _getData(row, "dimension_31");
            this.dimension_32 = _getData(row, "dimension_32");
            this.dimension_33 = _getData(row, "dimension_33");
            this.dimension_34 = _getData(row, "dimension_34");
            this.dimension_35 = _getData(row, "dimension_35");
            this.dimension_36 = _getData(row, "dimension_36");
            this.dimension_37 = _getData(row, "dimension_37");
            this.dimension_38 = _getData(row, "dimension_38");
            this.dimension_39 = _getData(row, "dimension_39");
            this.dimension_40 = _getData(row, "dimension_40");
            this.dimension_41 = _getData(row, "dimension_41");
            this.dimension_42 = _getData(row, "dimension_42");
            this.dimension_43 = _getData(row, "dimension_43");
            this.dimension_44 = _getData(row, "dimension_44");
            this.dimension_45 = _getData(row, "dimension_45");

        }
        public DateTime create_date_time_now { get; set; }
        
        /// <summary>
        ///รหัสสินค้า
        /// </summary>
        public string ic_code { get; set; }
        /// <summary>
        ///รหัสสูตรการผลิต
        /// </summary>
        public string formular { get; set; }
        /// <summary>
        ///รับเกินที่สั่งซื้อได้
        /// </summary>
        public int po_over { get; set; }
        /// <summary>
        ///เบิกเกินสั่งขายได้
        /// </summary>
        public int so_over { get; set; }
        /// <summary>
        ///รหัสกลุ่มบัญชี
        /// </summary>
        public string account_group { get; set; }
        /// <summary>
        ///serial number
        /// </summary>
        public string serial_number { get; set; }
        /// <summary>
        ///% ภาษีนำเข้า
        /// </summary>
        public string tax_import { get; set; }
        /// <summary>
        ///พิกัดศุลกากร
        /// </summary>
        public decimal tax_rate { get; set; }
        /// <summary>
        ///ผู้รับผิดชอบการซื้อ
        /// </summary>
        public string purchase_manager { get; set; }
        /// <summary>
        ///ผู้รับผิดชอบการขาย
        /// </summary>
        public string sale_manager { get; set; }
        /// <summary>
        ///คลังเริ่มต้นซื้อ
        /// </summary>
        public string start_purchase_wh { get; set; }
        /// <summary>
        ///ที่เก็บเริ่มต้นซื้อ
        /// </summary>
        public string start_purchase_shelf { get; set; }
        /// <summary>
        ///หน่วยเริ่มต้นซื้อ
        /// </summary>
        public string start_purchase_unit { get; set; }
        /// <summary>
        ///คลังเริ่มต้นขาย
        /// </summary>
        public string start_sale_wh { get; set; }
        /// <summary>
        ///ที่เก็บเริ่มต้นขาย
        /// </summary>
        public string start_sale_shelf { get; set; }
        /// <summary>
        ///หน่วยเริ่มต้นขาย
        /// </summary>
        public string start_sale_unit { get; set; }
        /// <summary>
        ///ต้นทุนการผลิต
        /// </summary>
        public decimal cost_produce { get; set; }
        /// <summary>
        ///ต้นทุนมาตรฐาน
        /// </summary>
        public decimal cost_standard { get; set; }
        /// <summary>
        ///หน่วยนับสต็อก
        /// </summary>
        public string unit_for_stock { get; set; }
        /// <summary>
        ///คลังสินค้าชำรุด
        /// </summary>
        public string ic_out_wh { get; set; }
        /// <summary>
        ///ที่เก็บสินค้าชำรุด
        /// </summary>
        public string ic_out_shelf { get; set; }
        /// <summary>
        ///คลังสินค้าจอง
        /// </summary>
        public string ic_reserve_wh { get; set; }
        /// <summary>
        ///สถานะการจอง
        /// </summary>
        public int reserve_status { get; set; }
        /// <summary>
        ///ส่วนลดต่อรายการ
        /// </summary>
        public string discount { get; set; }
        /// <summary>
        ///จุดสั่งซื้อ
        /// </summary>
        public decimal purchase_point { get; set; }
        /// <summary>
        ///หน่วยนับที่ 2
        /// </summary>
        public string unit_2_code { get; set; }
        /// <summary>
        ///ปริมาณหน่วยนับ 2
        /// </summary>
        public int unit_2_qty { get; set; }
        /// <summary>
        ///เฉลี่ยค่าระหว่างหน่วยนับ
        /// </summary>
        public int unit_2_average { get; set; }
        /// <summary>
        ///ค่าเฉลี่ยระหว่างหน่วยนับ
        /// </summary>
        public decimal unit_2_average_value { get; set; }
        /// <summary>
        ///กลุ่มผู้รับผิดชอบการซื้อ
        /// </summary>
        public string user_group_for_purchase { get; set; }
        /// <summary>
        ///กลุ่มผู้รับผิดชอบการขาย
        /// </summary>
        public string user_group_for_sale { get; set; }
        /// <summary>
        ///กลุ่มผู้รับผิดชอบการจัดการ
        /// </summary>
        public string user_group_for_manage { get; set; }
        /// <summary>
        ///กลุ่มผู้รับผิดชอบคลังสินค้า
        /// </summary>
        public string user_group_for_warehouse { get; set; }
        /// <summary>
        ///สถานะผู้มีสิทธิใช้งาน
        /// </summary>
        public int user_status { get; set; }
        /// <summary>
        ///เหตุผลในการปิด
        /// </summary>
        public string close_reason { get; set; }
        /// <summary>
        ///วันที่ปิด
        /// </summary>
        public DateTime close_date { get; set; }
        /// <summary>
        ///ไฟล์อ้างอิง 1
        /// </summary>
        public string ref_file_1 { get; set; }
        /// <summary>
        ///ไฟล์อ้างอิง 2
        /// </summary>
        public string ref_file_2 { get; set; }
        /// <summary>
        ///ไฟล์อ้างอิง 3
        /// </summary>
        public string ref_file_3 { get; set; }
        /// <summary>
        ///ไฟล์อ้างอิง 4
        /// </summary>
        public string ref_file_4 { get; set; }
        /// <summary>
        ///ไฟล์อ้างอิง 5
        /// </summary>
        public string ref_file_5 { get; set; }
        /// <summary>
        ///มิติ 1
        /// </summary>
        public string dimension_1 { get; set; }
        /// <summary>
        ///มิติ 2
        /// </summary>
        public string dimension_2 { get; set; }
        /// <summary>
        ///มิติ 3
        /// </summary>
        public string dimension_3 { get; set; }
        /// <summary>
        ///มิติ 4
        /// </summary>
        public string dimension_4 { get; set; }
        /// <summary>
        ///มิติ 5
        /// </summary>
        public string dimension_5 { get; set; }
        /// <summary>
        ///มิติ 6
        /// </summary>
        public string dimension_6 { get; set; }
        /// <summary>
        ///มิติ 7
        /// </summary>
        public string dimension_7 { get; set; }
        /// <summary>
        ///มิติ 8
        /// </summary>
        public string dimension_8 { get; set; }
        /// <summary>
        ///มิติ 9
        /// </summary>
        public string dimension_9 { get; set; }
        /// <summary>
        ///มิติ 10
        /// </summary>
        public string dimension_10 { get; set; }
        /// <summary>
        ///ชื่อสินค้า
        /// </summary>
        public string ic_name { get; set; }
        /// <summary>
        ///ราคาขาย 1
        /// </summary>
        public decimal sale_price_1 { get; set; }
        /// <summary>
        ///ราคาขาย 2
        /// </summary>
        public decimal sale_price_2 { get; set; }
        /// <summary>
        ///ราคาขาย 3
        /// </summary>
        public decimal sale_price_3 { get; set; }
        /// <summary>
        ///ราคาขาย 4
        /// </summary>
        public decimal sale_price_4 { get; set; }
        /// <summary>
        ///จุดสูงสุด
        /// </summary>
        public decimal maximum_qty { get; set; }
        /// <summary>
        ///จุดต่ำสุด
        /// </summary>
        public decimal minimum_qty { get; set; }
        /// <summary>
        ///มิติ 11
        /// </summary>
        public string dimension_11 { get; set; }
        /// <summary>
        ///มิติ 12
        /// </summary>
        public string dimension_12 { get; set; }
        /// <summary>
        ///มิติ 13
        /// </summary>
        public string dimension_13 { get; set; }
        /// <summary>
        ///มิติ 14
        /// </summary>
        public string dimension_14 { get; set; }
        /// <summary>
        ///มิติ 15
        /// </summary>
        public string dimension_15 { get; set; }
        /// <summary>
        ///มิติ 16
        /// </summary>
        public string dimension_16 { get; set; }
        /// <summary>
        ///มิติ 17
        /// </summary>
        public string dimension_17 { get; set; }
        /// <summary>
        ///มิติ 18
        /// </summary>
        public string dimension_18 { get; set; }
        /// <summary>
        ///มิติ 19
        /// </summary>
        public string dimension_19 { get; set; }
        /// <summary>
        ///มิติ 20
        /// </summary>
        public string dimension_20 { get; set; }
        /// <summary>
        ///อนุญาติให้ขายสินค้าค้างส่ง
        /// </summary>
        public int accrued_control { get; set; }
        /// <summary>
        ///ห้ามแก้ราคาขาย
        /// </summary>
        public int lock_price { get; set; }
        /// <summary>
        ///ห้ามแก้ส่วนลดขาย
        /// </summary>
        public int lock_discount { get; set; }
        /// <summary>
        ///ห้ามขายต่ำกว่าต้นทุน
        /// </summary>
        public int lock_cost { get; set; }
        /// <summary>
        ///สินค้าเลิกผลิตแล้ว
        /// </summary>
        public int is_end { get; set; }
        /// <summary>
        ///สินค้าหยุดซื้อ
        /// </summary>
        public int is_hold_purchase { get; set; }
        /// <summary>
        ///สินค้าหยุดขาย
        /// </summary>
        public int is_hold_sale { get; set; }
        /// <summary>
        ///ห้ามใช้
        /// </summary>
        public int is_stop { get; set; }
        /// <summary>
        ///อนุญาติให้ยอดคงเหลือติดลบ
        /// </summary>
        public int balance_control { get; set; }
        /// <summary>
        ///เข้าสะสมแต้ม
        /// </summary>
        public int have_point { get; set; }
        /// <summary>
        ///หน่วยเริ่มต้นสินค้า
        /// </summary>
        public string start_unit_code { get; set; }
        /// <summary>
        ///มิติ 21
        /// </summary>
        public string dimension_21 { get; set; }
        /// <summary>
        ///มิติ 22
        /// </summary>
        public string dimension_22 { get; set; }
        /// <summary>
        ///มิติ 23
        /// </summary>
        public string dimension_23 { get; set; }
        /// <summary>
        ///มิติ 24
        /// </summary>
        public string dimension_24 { get; set; }
        /// <summary>
        ///มิติ 25
        /// </summary>
        public string dimension_25 { get; set; }
        /// <summary>
        ///สินค้าสมนาคุณ
        /// </summary>
        public int is_premium { get; set; }
        /// <summary>
        ///มิติ 26
        /// </summary>
        public string dimension_26 { get; set; }
        /// <summary>
        ///มิติ 27
        /// </summary>
        public string dimension_27 { get; set; }
        /// <summary>
        ///มิติ 28
        /// </summary>
        public string dimension_28 { get; set; }
        /// <summary>
        ///มิติ 29
        /// </summary>
        public string dimension_29 { get; set; }
        /// <summary>
        ///มิติ 30
        /// </summary>
        public string dimension_30 { get; set; }
        /// <summary>
        ///มิติ 31
        /// </summary>
        public string dimension_31 { get; set; }
        /// <summary>
        ///มิติ 32
        /// </summary>
        public string dimension_32 { get; set; }
        /// <summary>
        ///มิติ 33
        /// </summary>
        public string dimension_33 { get; set; }
        /// <summary>
        ///มิติ 34
        /// </summary>
        public string dimension_34 { get; set; }
        /// <summary>
        ///มิติ 35
        /// </summary>
        public string dimension_35 { get; set; }
        /// <summary>
        ///มิติ 36
        /// </summary>
        public string dimension_36 { get; set; }
        /// <summary>
        ///มิติ 37
        /// </summary>
        public string dimension_37 { get; set; }
        /// <summary>
        ///มิติ 38
        /// </summary>
        public string dimension_38 { get; set; }
        /// <summary>
        ///มิติ 39
        /// </summary>
        public string dimension_39 { get; set; }
        /// <summary>
        ///มิติ 40
        /// </summary>
        public string dimension_40 { get; set; }
        /// <summary>
        ///มิติ 41
        /// </summary>
        public string dimension_41 { get; set; }
        /// <summary>
        ///มิติ 42
        /// </summary>
        public string dimension_42 { get; set; }
        /// <summary>
        ///มิติ 43
        /// </summary>
        public string dimension_43 { get; set; }
        /// <summary>
        ///มิติ 44
        /// </summary>
        public string dimension_44 { get; set; }
        /// <summary>
        ///มิติ 45
        /// </summary>
        public string dimension_45 { get; set; }
    }



    /// <summary>
    /// หน่วยนับสินค้าที่ใช้
    /// </summary>
    public class ic_unit_use : _jsondata
    {
        public ic_unit_use(DataRow row) {
           
            this.code = _getData(row, "code",false);
            this.line_number = _getInt(row["line_number"].ToString());
            this.stand_value = _getDecimal(row["stand_value"].ToString());
            this.divide_value = _getDecimal(row["divide_value"].ToString());
            this.ratio = _getDecimal(row["ratio"].ToString());
            this.row_order = _getInt(row["row_order"].ToString());
            this.width_length_height = _getData(row, "width_length_height",false);
            this.ic_code = _getData(row, "ic_code",false);
            this.remark = _getData(row, "remark");
            this.weight = _getData(row, "weight",false);
            this.status = _getInt(row["status"].ToString());
            if (row["create_date_time_now"].ToString().Length > 0)
            {
                this.create_date_time_now = _getDate(row["create_date_time_now"].ToString());
            }

        }

        public DateTime create_date_time_now { get; set; }
        /// <summary>
        ///รหัส
        /// </summary>
        public string code { get; set; }
        /// <summary>
        ///ชื่อ
        /// </summary>
        public string name_1 { get; set; }
        /// <summary>
        ///ชื่อ (ภาษาอังกฤษ)
        /// </summary>
        public string name_2 { get; set; }
        /// <summary>
        ///ลำดับ
        /// </summary>
        public int line_number { get; set; }
        /// <summary>
        ///ตัวตั้ง
        /// </summary>
        public decimal stand_value { get; set; }
        /// <summary>
        ///ตัวหาร
        /// </summary>
        public decimal divide_value { get; set; }
        /// <summary>
        ///อัตราส่วน
        /// </summary>
        public decimal ratio { get; set; }
        /// <summary>
        ///ลำดับที่
        /// </summary>
        public int row_order { get; set; }
        /// <summary>
        ///กว้างxยาวxสูง
        /// </summary>
        public string width_length_height { get; set; }
        /// <summary>
        ///รหัสสินค้า
        /// </summary>
        public string ic_code { get; set; }
        /// <summary>
        ///หมายเหตุ
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        ///น้ำหนัก(กก)
        /// </summary>
        public string weight { get; set; }
        /// <summary>
        ///สถานะ
        /// </summary>
        public int status { get; set; }
    }





}