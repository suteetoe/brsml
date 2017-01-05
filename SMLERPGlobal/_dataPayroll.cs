using System;
using System.Collections.Generic;
using System.Text;

namespace _g
{
    public class _dataPayroll
    {
        /// <summary>
        /// Form Design
        /// </summary>
        public class formdesign
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "formdesign";
            /// <summary>
            /// Code
            /// </summary>
            public static String _formcode = "formcode";
            /// <summary>
            /// formguid_code
            /// </summary>
            public static String _formguid_code = "formguid_code";
            /// <summary>
            /// guid_code
            /// </summary>
            public static String _guid_code = "guid_code";
            /// <summary>
            /// Name
            /// </summary>
            public static String _formname = "formname";
            /// <summary>
            /// Update
            /// </summary>
            public static String _timeupdate = "timeupdate";
            /// <summary>
            /// Text
            /// </summary>
            public static String _formdesigntext = "formdesigntext";
            /// <summary>
            /// formbackground
            /// </summary>
            public static String _formbackground = "formbackground";
        }

        /// <summary>
        /// รายชื่อกลุ่ม
        /// </summary>
        public class sml_group_list
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "sml_group_list";
            /// <summary>
            /// รหัสกลุ่ม
            /// </summary>
            public static String _group_code = "group_code";
            /// <summary>
            /// ชื่อกลุ่ม
            /// </summary>
            public static String _group_name = "group_name";
            /// <summary>
            /// ใช้งาน
            /// </summary>
            public static String _active_status = "active_status";
            /// <summary>
            /// เมนู
            /// </summary>
            public static String _menu = "menu";
            /// <summary>
            /// ค้นหา
            /// </summary>
            public static String _search = "search";
        }

        /// <summary>
        /// รายละเอียดผู้ใช้
        /// </summary>
        public class sml_user_list
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "sml_user_list";
            /// <summary>
            /// รหัสผู้ใช้
            /// </summary>
            public static String _user_code = "user_code";
            /// <summary>
            /// ชื่อผู้ใช้
            /// </summary>
            public static String _user_name = "user_name";
            /// <summary>
            /// รหัสผ่าน
            /// </summary>
            public static String _user_password = "user_password";
            /// <summary>
            /// ระดับความสามารถ
            /// </summary>
            public static String _user_level = "user_level";
            /// <summary>
            /// ใช้งาน
            /// </summary>
            public static String _active_status = "active_status";
            /// <summary>
            /// เมนู
            /// </summary>
            public static String _menu = "menu";
            /// <summary>
            /// ค้นหา
            /// </summary>
            public static String _search = "search";
        }

        /// <summary>
        /// กำหนดสิทธิ์กลุ่มผู้ใช้งาน
        /// </summary>
        public class sml_permissions_group
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "sml_permissions_group";
            /// <summary>
            /// รหัส
            /// </summary>
            public static String _usercode = "usercode";
            /// <summary>
            /// แฟ้มกำหนดสิทธิ์
            /// </summary>
            public static String _image_file = "image_file";
            /// <summary>
            /// ระบบ
            /// </summary>
            public static String _system_id = "system_id";
            /// <summary>
            /// guid_code
            /// </summary>
            public static String _guid_code = "guid_code";
            /// <summary>
            /// รหัสเมนู
            /// </summary>
            public static String _menucode = "menucode";
            /// <summary>
            /// ชื่อเมนู
            /// </summary>
            public static String _menuname = "menuname";
            /// <summary>
            /// ใช้งานได้
            /// </summary>
            public static String _isread = "isread";
            /// <summary>
            /// เพิ่ม
            /// </summary>
            public static String _isadd = "isadd";
            /// <summary>
            /// ลบ
            /// </summary>
            public static String _isdelete = "isdelete";
            /// <summary>
            /// แก้ไข
            /// </summary>
            public static String _isedit = "isedit";
        }

        /// <summary>
        /// กำหนดสิทธิ์ผู้ใช้งาน
        /// </summary>
        public class sml_permissions_user
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "sml_permissions_user";
            /// <summary>
            /// รหัส
            /// </summary>
            public static String _usercode = "usercode";
            /// <summary>
            /// แฟ้มกำหนดสิทธิ์
            /// </summary>
            public static String _image_file = "image_file";
            /// <summary>
            /// ระบบ
            /// </summary>
            public static String _system_id = "system_id";
            /// <summary>
            /// guid_code
            /// </summary>
            public static String _guid_code = "guid_code";
            /// <summary>
            /// รหัสเมนู
            /// </summary>
            public static String _menucode = "menucode";
            /// <summary>
            /// ชื่อเมนู
            /// </summary>
            public static String _menuname = "menuname";
            /// <summary>
            /// ใช้งานได้
            /// </summary>
            public static String _isread = "isread";
            /// <summary>
            /// เพิ่ม
            /// </summary>
            public static String _isadd = "isadd";
            /// <summary>
            /// ลบ
            /// </summary>
            public static String _isdelete = "isdelete";
            /// <summary>
            /// แก้ไข
            /// </summary>
            public static String _isedit = "isedit";
        }

        /// <summary>
        /// รายชื่อสาขา
        /// </summary>
        public class erp_branch_list
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "erp_branch_list";
            /// <summary>
            /// รหัสสาขา
            /// </summary>
            public static String _code = "code";
            /// <summary>
            /// สาขาลำดับที่
            /// </summary>
            public static String _number = "number";
            /// <summary>
            /// ชื่อสาขา
            /// </summary>
            public static String _name_1 = "name_1";
            /// <summary>
            /// ชื่อ (ภาษาอังกฤษ)
            /// </summary>
            public static String _name_2 = "name_2";
            /// <summary>
            /// ที่อยู่สาขา 
            /// </summary>
            public static String _address_1 = "address_1";
            /// <summary>
            /// ที่อยู่ (ภาษาอังกฤษ)
            /// </summary>
            public static String _address_2 = "address_2";
            /// <summary>
            /// หมายเลขโทรศัพท์
            /// </summary>
            public static String _telephone = "telephone";
            /// <summary>
            /// หมายเลขโทรสาร
            /// </summary>
            public static String _fax = "fax";
            /// <summary>
            /// หมายเหตุ
            /// </summary>
            public static String _remark = "remark";
            /// <summary>
            /// GUID
            /// </summary>
            public static String _guid_code = "guid_code";
        }

        /// <summary>
        /// ข้อเลือกในการทำงาน
        /// </summary>
        public class erp_option
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "erp_option";
            /// <summary>
            /// ประเภทปี
            /// </summary>
            public static String _year_type = "year_type";
            /// <summary>
            /// ประเภทภาษีมูลค่าเพิ่ม (ขาย)
            /// </summary>
            public static String _vat_type = "vat_type";
            /// <summary>
            /// อัตราภาษีมูลค่าเพิ่ม
            /// </summary>
            public static String _vat_rate = "vat_rate";
            /// <summary>
            /// จุดทศนิยม (บัญชี)
            /// </summary>
            public static String _gl_no_decimal = "gl_no_decimal";
            /// <summary>
            /// ใช้ระบบตรวจสอบการใช้งาน
            /// </summary>
            public static String _use_log_book = "use_log_book";
            /// <summary>
            /// พิมพ์เอกสารหลังการบันทึกข้อมูล
            /// </summary>
            public static String _use_print_slip = "use_print_slip";
            /// <summary>
            /// ใช้กลุ่มเอกสาร
            /// </summary>
            public static String _use_doc_group = "use_doc_group";
            /// <summary>
            /// ใช้ระบบแยกแผนก
            /// </summary>
            public static String _use_department = "use_department";
            /// <summary>
            /// ใช้ระบบแยกงาน
            /// </summary>
            public static String _use_job = "use_job";
            /// <summary>
            /// ใช้ระบบการจัดสรรค์
            /// </summary>
            public static String _use_allocate = "use_allocate";
            /// <summary>
            /// ใช้ระบบแยกหน่วยงาน
            /// </summary>
            public static String _use_unit = "use_unit";
            /// <summary>
            /// ใช้ระบบแยกการผลิต
            /// </summary>
            public static String _use_work_in_process = "use_work_in_process";
            /// <summary>
            /// พุทธศักราช
            /// </summary>
            public static String _buddhist = "buddhist";
            /// <summary>
            /// คริสศักราช
            /// </summary>
            public static String _christian = "christian";
            /// <summary>
            /// บัญชีแยกประเภท
            /// </summary>
            public static String _tab_gl = "tab_gl";
            /// <summary>
            /// รวมใน
            /// </summary>
            public static String _vat_include = "vat_include";
            /// <summary>
            /// แยกนอก
            /// </summary>
            public static String _vat_exclude = "vat_exclude";
            /// <summary>
            /// จุดทศนิยม (จำนวนสินค้า)
            /// </summary>
            public static String _item_qty_decimal = "item_qty_decimal";
            /// <summary>
            /// จุดทศนิยม (ราคาสินค้า)
            /// </summary>
            public static String _item_price_decimal = "item_price_decimal";
            /// <summary>
            /// จุดทศนิยม (มูลค่าสินค้า)
            /// </summary>
            public static String _item_amount_decimal = "item_amount_decimal";
            /// <summary>
            /// สินค้าคงคลัง
            /// </summary>
            public static String _tab_item = "tab_item";
            /// <summary>
            /// อัตราภาษีหัก ณ. ที่จ่าย
            /// </summary>
            public static String _wht_rate = "wht_rate";
            /// <summary>
            /// ประเภทภาษีมูลค่าเพิ่ม (ซื้อ)
            /// </summary>
            public static String _vat_type_1 = "vat_type_1";
            /// <summary>
            /// เตือนเมื่อไม่พบราคาขาย
            /// </summary>
            public static String _warning_price_1 = "warning_price_1";
            /// <summary>
            /// เตือนราคาขายกับประเภทภาษี
            /// </summary>
            public static String _warning_price_2 = "warning_price_2";
            /// <summary>
            /// ใช้ระบบรหัสผ่านแก้ไขราคา
            /// </summary>
            public static String _warning_price_3 = "warning_price_3";
            /// <summary>
            /// ส่งคืน,ลดหนี้ใช้แบบอ้างอิงเอกสารใบเดียว
            /// </summary>
            public static String _purchase_credit_note_type = "purchase_credit_note_type";
            /// <summary>
            /// Top Margin
            /// </summary>
            public static String _printer_top_margin = "printer_top_margin";
        }

        /// <summary>
        /// รูปแบบเอกสาร
        /// </summary>
        public class erp_doc_format
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "erp_doc_format";
            /// <summary>
            /// รหัส
            /// </summary>
            public static String _code = "code";
            /// <summary>
            /// ชื่อ
            /// </summary>
            public static String _name_1 = "name_1";
            /// <summary>
            /// ชื่อ (ภาษาอังกฤษ)
            /// </summary>
            public static String _name_2 = "name_2";
            /// <summary>
            /// GUID
            /// </summary>
            public static String _guid_code = "guid_code";
            /// <summary>
            /// รูปแบบเอกสาร
            /// </summary>
            public static String _format = "format";
            /// <summary>
            /// รหัสหน้าจอ
            /// </summary>
            public static String _screen_code = "screen_code";
            /// <summary>
            /// รหัสแบบฟอร์ม
            /// </summary>
            public static String _form_code = "form_code";
        }

        /// <summary>
        /// รูปภาพ
        /// </summary>
        public class images
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "images";
            /// <summary>
            /// รหัส
            /// </summary>
            public static String _image_id = "image_id";
            /// <summary>
            /// รูปภาพ
            /// </summary>
            public static String _image_file = "image_file";
            /// <summary>
            /// ระบบ
            /// </summary>
            public static String _system_id = "system_id";
            /// <summary>
            /// guid_code
            /// </summary>
            public static String _guid_code = "guid_code";
        }

        /// <summary>
        /// รายละเอียดหน้าจอค้นหา
        /// </summary>
        public class erp_view_column
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "erp_view_column";
            /// <summary>
            /// กลุ่มหน้าจอ
            /// </summary>
            public static String _screen_group = "screen_group";
            /// <summary>
            /// รหัสหน้าจอ
            /// </summary>
            public static String _screen_code = "screen_code";
            /// <summary>
            /// ลำดับที่
            /// </summary>
            public static String _column_number = "column_number";
            /// <summary>
            /// ชื่อ
            /// </summary>
            public static String _column_name = "column_name";
            /// <summary>
            /// ชื่อ (ภาษาอังกฤษ)
            /// </summary>
            public static String _column_name_2 = "column_name_2";
            /// <summary>
            /// ชื่อข้อมูล
            /// </summary>
            public static String _column_field_name = "column_field_name";
            /// <summary>
            /// ชื่อข้อมูลเพื่อเรียง
            /// </summary>
            public static String _column_field_sort = "column_field_sort";
            /// <summary>
            /// ความกว้าง
            /// </summary>
            public static String _column_width = "column_width";
            /// <summary>
            /// ประเภท
            /// </summary>
            public static String _column_type = "column_type";
            /// <summary>
            /// จัดช่อง
            /// </summary>
            public static String _column_align = "column_align";
            /// <summary>
            /// รูปแบบ
            /// </summary>
            public static String _column_format = "column_format";
            /// <summary>
            /// Resource
            /// </summary>
            public static String _column_resource = "column_resource";
        }

        /// <summary>
        /// หน้าจอสำหรับแสดงรายการข้อมูล
        /// </summary>
        public class erp_view_document
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "erp_view_document";
            /// <summary>
            /// รหัสหน้าจอ
            /// </summary>
            public static String _screen_code = "screen_code";
            /// <summary>
            /// รหัส
            /// </summary>
            public static String _code = "code";
            /// <summary>
            /// ชื่อไทย
            /// </summary>
            public static String _name_1 = "name_1";
            /// <summary>
            /// ชื่ออังกฤษ
            /// </summary>
            public static String _name_2 = "name_2";
        }

        /// <summary>
        /// รายละเอียดการแสดงผลแต่ละหน้าจอ
        /// </summary>
        public class erp_view_table
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "erp_view_table";
            /// <summary>
            /// รหัสหน้าจอ
            /// </summary>
            public static String _screen_code = "screen_code";
            /// <summary>
            /// ชื่อหน้าจอ
            /// </summary>
            public static String _name_1 = "name_1";
            /// <summary>
            /// ชื่อหน้าจอ (ภาษาอังกฤษ)
            /// </summary>
            public static String _name_2 = "name_2";
            /// <summary>
            /// ชื่อตารางข้อมูลหลัก
            /// </summary>
            public static String _table_name = "table_name";
            /// <summary>
            /// ชื่อตารางข้อมูลที่เกี่ยวข้อง
            /// </summary>
            public static String _table_list = "table_list";
            /// <summary>
            /// ชื่อข้อมูลที่จะเรียง
            /// </summary>
            public static String _sort = "sort";
            /// <summary>
            /// เงื่อนไขการแสดงผล
            /// </summary>
            public static String _filter = "filter";
            /// <summary>
            /// ความกว้างเป็นเปอร์เซ็นต์
            /// </summary>
            public static String _width_persent = "width_persent";
        }

        /// <summary>
        /// ค่าเริ่มต้นบริษัท
        /// </summary>
        public class payroll_company_config
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "payroll_company_config";
            /// <summary>
            /// ชื่อบริษัท (ไทย)
            /// </summary>
            public static String _company_name_th = "company_name_th";
            /// <summary>
            /// ชื่อบริษัท (อังกฤษ)
            /// </summary>
            public static String _company_name_en = "company_name_en";
            /// <summary>
            /// ผู้ประกอบการ (ไทย)
            /// </summary>
            public static String _business_name_th = "business_name_th";
            /// <summary>
            /// ผู้ประกอบการ (อังกฤษ)
            /// </summary>
            public static String _business_name_en = "business_name_en";
            /// <summary>
            /// ที่อยู่ (ไทย)
            /// </summary>
            public static String _address_th = "address_th";
            /// <summary>
            /// ที่อยู่ (อังกฤษ)
            /// </summary>
            public static String _address_en = "address_en";
            /// <summary>
            /// อาคาร
            /// </summary>
            public static String _house = "house";
            /// <summary>
            /// ห้องที่
            /// </summary>
            public static String _room_no = "room_no";
            /// <summary>
            /// ชั้นที่
            /// </summary>
            public static String _floor_no = "floor_no";
            /// <summary>
            /// หมู่บ้าน
            /// </summary>
            public static String _village = "village";
            /// <summary>
            /// เลขที่
            /// </summary>
            public static String _house_no = "house_no";
            /// <summary>
            /// หมู่ที่
            /// </summary>
            public static String _crowd_no = "crowd_no";
            /// <summary>
            /// ตรอก/ซอย
            /// </summary>
            public static String _lane = "lane";
            /// <summary>
            /// ถนน
            /// </summary>
            public static String _road = "road";
            /// <summary>
            /// ตำบล/แขวง
            /// </summary>
            public static String _locality = "locality";
            /// <summary>
            /// อำเภอ/เขต
            /// </summary>
            public static String _amphur = "amphur";
            /// <summary>
            /// จังหวัด
            /// </summary>
            public static String _province = "province";
            /// <summary>
            /// รหัสไปรษณีย์
            /// </summary>
            public static String _postcode = "postcode";
            /// <summary>
            /// หมายเลขโทรศัพท์
            /// </summary>
            public static String _telephone = "telephone";
            /// <summary>
            /// หมายเลขโทรสาร
            /// </summary>
            public static String _fax = "fax";
            /// <summary>
            /// เลขประจำตัวผู้เสียภาษี
            /// </summary>
            public static String _tax_no = "tax_no";
            /// <summary>
            /// ทะเบียนการค้า
            /// </summary>
            public static String _trade_license = "trade_license";
            /// <summary>
            /// เลขประกันสังคม
            /// </summary>
            public static String _social_security_no = "social_security_no";
            /// <summary>
            /// เลขที่ใบอนุญาตกองทุนสำรอง
            /// </summary>
            public static String _reserve_fund_no = "reserve_fund_no";
            /// <summary>
            /// เครื่องหมายบริษัท
            /// </summary>
            public static String _company_logo = "company_logo";
            /// <summary>
            /// สาขา
            /// </summary>
            public static String _branch = "branch";
            /// <summary>
            /// หมายเหตุ
            /// </summary>
            public static String _remark = "remark";
            /// <summary>
            /// การปัดเศษสตางค์
            /// </summary>
            public static String _money_adjust = "money_adjust";
            /// <summary>
            /// วิธีคิดเงินเดือน
            /// </summary>
            public static String _salary_method = "salary_method";
            /// <summary>
            /// อัตราสมทบกองทุนประกันสังคม
            /// </summary>
            public static String _social_security_rate = "social_security_rate";
            /// <summary>
            /// อัตราสมทบกองทุนสำรองฯ
            /// </summary>
            public static String _reserve_fund_rate = "reserve_fund_rate";
            /// <summary>
            /// ประเภทกองทุนเงินสำรอง
            /// </summary>
            public static String _reserve_fund_type = "reserve_fund_type";
            /// <summary>
            /// อัตราสมทบกองทุนสำรองฯ (หักจากลูกจ้าง)
            /// </summary>
            public static String _customer_rf_rate = "customer_rf_rate";
            /// <summary>
            /// อัตราสมทบกองทุนสำรองฯ (หักจากนายจ้าง)
            /// </summary>
            public static String _employer_rf_rate = "employer_rf_rate";
            /// <summary>
            /// อัตราสมทบประกันสังคม (หักจากลูกจ้าง)
            /// </summary>
            public static String _customer_ss_rate = "customer_ss_rate";
            /// <summary>
            /// อัตราสมทบประกันสังคม (หักจากนายจ้าง)
            /// </summary>
            public static String _employer_ss_rate = "employer_ss_rate";
            /// <summary>
            /// อัตราค่าแรงสูงสุด
            /// </summary>
            public static String _social_security_max = "social_security_max";
            /// <summary>
            /// อัตราค่าแรงต่ำสุด
            /// </summary>
            public static String _social_security_min = "social_security_min";
            /// <summary>
            /// ลดฯประกันสังคมไม่เกิน
            /// </summary>
            public static String _reduce_social_security = "reduce_social_security";
            /// <summary>
            /// ลดฯกองทุนสำรองฯไม่เกิน
            /// </summary>
            public static String _reduce_reserve_fund = "reduce_reserve_fund";
            /// <summary>
            /// จำนวนชั่วโมงทำงาน/วัน
            /// </summary>
            public static String _hourly = "hourly";
            /// <summary>
            /// จำนวนชั่วโมงทำงาน/วัน
            /// </summary>
            public static String _hourly_day = "hourly_day";
            /// <summary>
            /// จำนวนชั่วโมงทำงาน/วัน
            /// </summary>
            public static String _hourly_week = "hourly_week";
            /// <summary>
            /// จำนวนวันทำงาน/สัปดาห์
            /// </summary>
            public static String _work_day = "work_day";
            /// <summary>
            /// หักขาดงาน
            /// </summary>
            public static String _costs_sow = "costs_sow";
            /// <summary>
            /// หักลางาน
            /// </summary>
            public static String _costs_leave = "costs_leave";
            /// <summary>
            /// หักมาสาย
            /// </summary>
            public static String _costs_late = "costs_late";
            /// <summary>
            /// ค่าใช้จ่ายอื่นๆ
            /// </summary>
            public static String _costs_payout = "costs_payout";
            /// <summary>
            /// หักเงินจ่ายล่วงหน้า
            /// </summary>
            public static String _costs_advance = "costs_advance";
            /// <summary>
            /// เงินค้ำประกัน
            /// </summary>
            public static String _costs_insure = "costs_insure";
            /// <summary>
            /// ปี
            /// </summary>
            public static String _year = "year";
            /// <summary>
            /// guid_code
            /// </summary>
            public static String _guid_code = "guid_code";
            /// <summary>
            /// ไม่ปัดเศษ
            /// </summary>
            public static String _no_adjust = "no_adjust";
            /// <summary>
            /// ปัดขึ้นทั้งหมด
            /// </summary>
            public static String _up_adjust = "up_adjust";
            /// <summary>
            /// ปัดลงทั้งหมด
            /// </summary>
            public static String _down_adjust = "down_adjust";
            /// <summary>
            /// ปัดแบบสัดส่วน
            /// </summary>
            public static String _mix_adjust = "mix_adjust";
            /// <summary>
            /// แบบเฉลี่ย 30 วัน/เดือน
            /// </summary>
            public static String _average = "average";
            /// <summary>
            /// แบบจำนวนวันจริง
            /// </summary>
            public static String _be_true = "be_true";
            /// <summary>
            /// รายวัน
            /// </summary>
            public static String _daily = "daily";
            /// <summary>
            /// จันทร์
            /// </summary>
            public static String _mon = "mon";
            /// <summary>
            /// อังคาร
            /// </summary>
            public static String _tue = "tue";
            /// <summary>
            /// พุธ
            /// </summary>
            public static String _wed = "wed";
            /// <summary>
            /// พฤหัสบดี
            /// </summary>
            public static String _thu = "thu";
            /// <summary>
            /// ศุกร์
            /// </summary>
            public static String _fri = "fri";
            /// <summary>
            /// เสาร์
            /// </summary>
            public static String _sat = "sat";
            /// <summary>
            /// อาทิตย์
            /// </summary>
            public static String _sun = "sun";
            /// <summary>
            /// รายละเอียด
            /// </summary>
            public static String _tab_detail = "tab_detail";
            /// <summary>
            /// เงื่อนไขการคำนวณ(อื่นๆ)
            /// </summary>
            public static String _tab_method = "tab_method";
            /// <summary>
            /// บัญชีธนาคาร
            /// </summary>
            public static String _tab_bank = "tab_bank";
            /// <summary>
            /// วันหยุดบริษัท
            /// </summary>
            public static String _tab_holiday = "tab_holiday";
            /// <summary>
            /// แบบคงที่
            /// </summary>
            public static String _upright = "upright";
            /// <summary>
            /// แบบแปรผัน
            /// </summary>
            public static String _change = "change";
            /// <summary>
            /// เงื่อนไขการคำนวณเงินเดือน
            /// </summary>
            public static String _tab_method_payroll = "tab_method_payroll";
            /// <summary>
            /// ที่อยู่ในการออกใบกำกับภาษี
            /// </summary>
            public static String _tab_address_tax = "tab_address_tax";
            /// <summary>
            /// ค่าใช้จ่าย
            /// </summary>
            public static String _tab_costs = "tab_costs";
            /// <summary>
            /// หักจากเงินได้
            /// </summary>
            public static String _deducted = "deducted";
            /// <summary>
            /// ไม่หักจากเงินได้
            /// </summary>
            public static String _not_deducted = "not_deducted";
            /// <summary>
            /// การคำนวณภาษี
            /// </summary>
            public static String _groupbox_tax = "groupbox_tax";
            /// <summary>
            /// งวดสุดท้าย
            /// </summary>
            public static String _last_period = "last_period";
            /// <summary>
            /// ทุกงวด
            /// </summary>
            public static String _all_period = "all_period";
            /// <summary>
            /// คำนวณภาษี
            /// </summary>
            public static String _tax_period_month = "tax_period_month";
            /// <summary>
            /// คำนวณภาษี
            /// </summary>
            public static String _tax_period_week = "tax_period_week";
            /// <summary>
            /// คำนวณภาษี
            /// </summary>
            public static String _tax_period_day = "tax_period_day";
            /// <summary>
            /// จำนวนงวดต่อเดือน
            /// </summary>
            public static String _period_number_month = "period_number_month";
            /// <summary>
            /// จำนวนงวดต่อเดือน
            /// </summary>
            public static String _period_number_week = "period_number_week";
            /// <summary>
            /// จำนวนงวดต่อเดือน
            /// </summary>
            public static String _period_number_day = "period_number_day";
            /// <summary>
            /// หนี่งงวด
            /// </summary>
            public static String _one_period = "one_period";
            /// <summary>
            /// สองงวด
            /// </summary>
            public static String _two_period = "two_period";
            /// <summary>
            /// สามงวด
            /// </summary>
            public static String _three_period = "three_period";
            /// <summary>
            /// สี่งวด
            /// </summary>
            public static String _four_period = "four_period";
            /// <summary>
            /// งวดที่หนึ่งจากวันที่
            /// </summary>
            public static String _start_one_month = "start_one_month";
            /// <summary>
            /// ถึงวันที่
            /// </summary>
            public static String _end_one_month = "end_one_month";
            /// <summary>
            /// งวดที่สองจากวันที่
            /// </summary>
            public static String _start_two_month = "start_two_month";
            /// <summary>
            /// ถึงวันที่
            /// </summary>
            public static String _end_two_month = "end_two_month";
            /// <summary>
            /// งวดที่สามจากวันที่
            /// </summary>
            public static String _start_three_month = "start_three_month";
            /// <summary>
            /// ถึงวันที่
            /// </summary>
            public static String _end_three_month = "end_three_month";
            /// <summary>
            /// งวดที่สี่จากวันที่
            /// </summary>
            public static String _start_four_month = "start_four_month";
            /// <summary>
            /// ถึงวันที่
            /// </summary>
            public static String _end_four_month = "end_four_month";
            /// <summary>
            /// งวดที่หนึ่งจากวันที่
            /// </summary>
            public static String _start_one_week = "start_one_week";
            /// <summary>
            /// ถึงวันที่
            /// </summary>
            public static String _end_one_week = "end_one_week";
            /// <summary>
            /// งวดที่สองจากวันที่
            /// </summary>
            public static String _start_two_week = "start_two_week";
            /// <summary>
            /// ถึงวันที่
            /// </summary>
            public static String _end_two_week = "end_two_week";
            /// <summary>
            /// งวดที่สามจากวันที่
            /// </summary>
            public static String _start_three_week = "start_three_week";
            /// <summary>
            /// ถึงวันที่
            /// </summary>
            public static String _end_three_week = "end_three_week";
            /// <summary>
            /// งวดที่สี่จากวันที่
            /// </summary>
            public static String _start_four_week = "start_four_week";
            /// <summary>
            /// ถึงวันที่
            /// </summary>
            public static String _end_four_week = "end_four_week";
            /// <summary>
            /// งวดที่หนึ่งจากวันที่
            /// </summary>
            public static String _start_one_day = "start_one_day";
            /// <summary>
            /// ถึงวันที่
            /// </summary>
            public static String _end_one_day = "end_one_day";
            /// <summary>
            /// งวดที่สองจากวันที่
            /// </summary>
            public static String _start_two_day = "start_two_day";
            /// <summary>
            /// ถึงวันที่
            /// </summary>
            public static String _end_two_day = "end_two_day";
            /// <summary>
            /// งวดที่สามจากวันที่
            /// </summary>
            public static String _start_three_day = "start_three_day";
            /// <summary>
            /// ถึงวันที่
            /// </summary>
            public static String _end_three_day = "end_three_day";
            /// <summary>
            /// งวดที่สี่จากวันที่
            /// </summary>
            public static String _start_four_day = "start_four_day";
            /// <summary>
            /// ถึงวันที่
            /// </summary>
            public static String _end_four_day = "end_four_day";
            /// <summary>
            /// พนักงานรายเดือน
            /// </summary>
            public static String _tab_calculate_tax_month = "tab_calculate_tax_month";
            /// <summary>
            /// พนักงานรายสัปดาห์
            /// </summary>
            public static String _tab_calculate_tax_week = "tab_calculate_tax_week";
            /// <summary>
            /// พนักงานรายวัน
            /// </summary>
            public static String _tab_calculate_tax_day = "tab_calculate_tax_day";
            /// <summary>
            /// คำนวนเงินเดือน
            /// </summary>
            public static String _calculate_salary_month = "calculate_salary_month";
            /// <summary>
            /// คำนวนเงินเดือน
            /// </summary>
            public static String _calculate_salary_week = "calculate_salary_week";
            /// <summary>
            /// คำนวนเงินเดือน
            /// </summary>
            public static String _calculate_salary_day = "calculate_salary_day";
            /// <summary>
            /// บันทึกข้อมูลรายวัน
            /// </summary>
            public static String _recorded_daily = "recorded_daily";
            /// <summary>
            /// ไม่บันทึกข้อมูลรายวัน
            /// </summary>
            public static String _not_recorded_daily = "not_recorded_daily";
        }

        /// <summary>
        /// ฝ่าย
        /// </summary>
        public class payroll_side_list
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "payroll_side_list";
            /// <summary>
            /// รหัส
            /// </summary>
            public static String _code = "code";
            /// <summary>
            /// ชื่อ (ไทย)
            /// </summary>
            public static String _name = "name";
            /// <summary>
            /// ชื่อ (อังกฤษ)
            /// </summary>
            public static String _name_en = "name_en";
            /// <summary>
            /// หมายเหตุ
            /// </summary>
            public static String _remark = "remark";
            /// <summary>
            /// สถานะ
            /// </summary>
            public static String _status = "status";
            /// <summary>
            /// guid_code
            /// </summary>
            public static String _guid_code = "guid_code";
            /// <summary>
            /// ใช้
            /// </summary>
            public static String _active = "active";
            /// <summary>
            /// ไม่ใช้
            /// </summary>
            public static String _inactive = "inactive";
        }

        /// <summary>
        /// แผนก
        /// </summary>
        public class payroll_section_list
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "payroll_section_list";
            /// <summary>
            /// รหัส
            /// </summary>
            public static String _code = "code";
            /// <summary>
            /// ชื่อ (ไทย)
            /// </summary>
            public static String _name = "name";
            /// <summary>
            /// ชื่อ (อังกฤษ)
            /// </summary>
            public static String _name_en = "name_en";
            /// <summary>
            /// หมายเหตุ
            /// </summary>
            public static String _remark = "remark";
            /// <summary>
            /// สถานะ
            /// </summary>
            public static String _status = "status";
            /// <summary>
            /// guid_code
            /// </summary>
            public static String _guid_code = "guid_code";
            /// <summary>
            /// ใช้
            /// </summary>
            public static String _active = "active";
            /// <summary>
            /// ไม่ใช้
            /// </summary>
            public static String _inactive = "inactive";
        }

        /// <summary>
        /// ตำแหน่งงาน
        /// </summary>
        public class payroll_work_title
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "payroll_work_title";
            /// <summary>
            /// รหัส
            /// </summary>
            public static String _code = "code";
            /// <summary>
            /// ชื่อ (ไทย)
            /// </summary>
            public static String _name = "name";
            /// <summary>
            /// ชื่อ (อังกฤษ)
            /// </summary>
            public static String _name_en = "name_en";
            /// <summary>
            /// หมายเหตุ
            /// </summary>
            public static String _remark = "remark";
            /// <summary>
            /// สถานะ
            /// </summary>
            public static String _status = "status";
            /// <summary>
            /// guid_code
            /// </summary>
            public static String _guid_code = "guid_code";
            /// <summary>
            /// ใช้
            /// </summary>
            public static String _active = "active";
            /// <summary>
            /// ไม่ใช้
            /// </summary>
            public static String _inactive = "inactive";
        }

        /// <summary>
        /// ธนาคาร
        /// </summary>
        public class payroll_bank_list
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "payroll_bank_list";
            /// <summary>
            /// รหัส
            /// </summary>
            public static String _code = "code";
            /// <summary>
            /// ชื่อ (ไทย)
            /// </summary>
            public static String _name = "name";
            /// <summary>
            /// ชื่อ (อังกฤษ)
            /// </summary>
            public static String _name_en = "name_en";
            /// <summary>
            /// หมายเหตุ
            /// </summary>
            public static String _remark = "remark";
            /// <summary>
            /// สถานะ
            /// </summary>
            public static String _status = "status";
            /// <summary>
            /// guid_code
            /// </summary>
            public static String _guid_code = "guid_code";
            /// <summary>
            /// ใช้
            /// </summary>
            public static String _active = "active";
            /// <summary>
            /// ไม่ใช้
            /// </summary>
            public static String _inactive = "inactive";
        }

        /// <summary>
        /// พนักงาน/ลูกจ้าง
        /// </summary>
        public class payroll_employee
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "payroll_employee";
            /// <summary>
            /// รหัส
            /// </summary>
            public static String _code = "code";
            /// <summary>
            /// คำนำหน้าชื่อ
            /// </summary>
            public static String _title_name = "title_name";
            /// <summary>
            /// ชื่อ-นามสกุล
            /// </summary>
            public static String _name = "name";
            /// <summary>
            /// ชื่อเล่น
            /// </summary>
            public static String _nick_name = "nick_name";
            /// <summary>
            /// ฝ่าย
            /// </summary>
            public static String _side_code = "side_code";
            /// <summary>
            /// แผนก
            /// </summary>
            public static String _section_code = "section_code";
            /// <summary>
            /// ตำแหน่ง
            /// </summary>
            public static String _work_title = "work_title";
            /// <summary>
            /// ประเภทลูกจ้าง
            /// </summary>
            public static String _employee_type = "employee_type";
            /// <summary>
            /// วันที่เริ่มต้นทำงาน
            /// </summary>
            public static String _start_work = "start_work";
            /// <summary>
            /// ค่าแรง
            /// </summary>
            public static String _salary = "salary";
            /// <summary>
            /// หมายเหตุ
            /// </summary>
            public static String _remark = "remark";
            /// <summary>
            /// สถานะ
            /// </summary>
            public static String _status = "status";
            /// <summary>
            /// เลขประกันสังคม
            /// </summary>
            public static String _social_security_no = "social_security_no";
            /// <summary>
            /// ชื่อสถานพยาบาล
            /// </summary>
            public static String _hospital = "hospital";
            /// <summary>
            /// วันที่ออกบัตร
            /// </summary>
            public static String _produce_social_security = "produce_social_security";
            /// <summary>
            /// วันที่หมดอายุ
            /// </summary>
            public static String _expire_social_security = "expire_social_security";
            /// <summary>
            /// เลขบัญชีธนาคาร
            /// </summary>
            public static String _book_bank_no = "book_bank_no";
            /// <summary>
            /// รหัสธนาคาร
            /// </summary>
            public static String _bank_code = "bank_code";
            /// <summary>
            /// ชื่อธนาคาร
            /// </summary>
            public static String _bank_name = "bank_name";
            /// <summary>
            /// สาขา
            /// </summary>
            public static String _branch_bank = "branch_bank";
            /// <summary>
            /// เลขประจำตัวผู้เสียภาษี
            /// </summary>
            public static String _tax_no = "tax_no";
            /// <summary>
            /// guid_code
            /// </summary>
            public static String _guid_code = "guid_code";
            /// <summary>
            /// จ้างงาน
            /// </summary>
            public static String _active = "active";
            /// <summary>
            /// พักงาน
            /// </summary>
            public static String _hold = "hold";
            /// <summary>
            /// เลิกจ้าง
            /// </summary>
            public static String _inactive = "inactive";
            /// <summary>
            /// รายเดือน
            /// </summary>
            public static String _monthly = "monthly";
            /// <summary>
            /// รายสัปดาห์
            /// </summary>
            public static String _weekly = "weekly";
            /// <summary>
            /// รายวัน
            /// </summary>
            public static String _daily = "daily";
            /// <summary>
            /// ตั้งแต่
            /// </summary>
            public static String _salary_from = "salary_from";
            /// <summary>
            /// จนกระทั่งถึง
            /// </summary>
            public static String _salary_to = "salary_to";
            /// <summary>
            /// เดือน
            /// </summary>
            public static String _for_month = "for_month";
            /// <summary>
            /// พ.ศ.
            /// </summary>
            public static String _for_year = "for_year";
            /// <summary>
            /// จากพนักงาน
            /// </summary>
            public static String _employee_from = "employee_from";
            /// <summary>
            /// ถึง
            /// </summary>
            public static String _employee_to = "employee_to";
            /// <summary>
            /// รายละเอียด
            /// </summary>
            public static String _tab_detail = "tab_detail";
            /// <summary>
            /// ที่อยู่ปัจจุบัน
            /// </summary>
            public static String _tab_adderss = "tab_adderss";
            /// <summary>
            /// ประวัติการศึกษา
            /// </summary>
            public static String _tab_education = "tab_education";
            /// <summary>
            /// ประสบการณ์การทำงาน
            /// </summary>
            public static String _tab_experience = "tab_experience";
            /// <summary>
            /// รูปพนักงาน
            /// </summary>
            public static String _tab_picture = "tab_picture";
            /// <summary>
            /// ลดหย่อน
            /// </summary>
            public static String _tab_reduce = "tab_reduce";
            /// <summary>
            /// ประกันสังคม
            /// </summary>
            public static String _tab_social_security = "tab_social_security";
            /// <summary>
            /// เงินเดือน
            /// </summary>
            public static String _tab_salary = "tab_salary";
            /// <summary>
            /// ความสามารถพิเศษ
            /// </summary>
            public static String _tab_talent = "tab_talent";
            /// <summary>
            /// บุคคลค้ำประกัน
            /// </summary>
            public static String _tab_collateral = "tab_collateral";
            /// <summary>
            /// ผู้ติดต่อกรณีฉุกเฉิน
            /// </summary>
            public static String _tab_family = "tab_family";
            /// <summary>
            /// ลดหย่อนคู่สมรส
            /// </summary>
            public static String _tab_reduce_spouse = "tab_reduce_spouse";
            /// <summary>
            /// ลดย่อนอื่นๆ
            /// </summary>
            public static String _tab_reduce_detail = "tab_reduce_detail";
            /// <summary>
            /// ชื่อผู้มีหน้าที่หักภาษี
            /// </summary>
            public static String _name_employee_tex = "name_employee_tex";
            /// <summary>
            /// มกราคม
            /// </summary>
            public static String _January = "January";
            /// <summary>
            /// กุมภาพันธ์
            /// </summary>
            public static String _February = "February";
            /// <summary>
            /// มีนาคม
            /// </summary>
            public static String _March = "March";
            /// <summary>
            /// เมษายน
            /// </summary>
            public static String _April = "April";
            /// <summary>
            /// พฤษภาคม
            /// </summary>
            public static String _May = "May";
            /// <summary>
            /// มิถุนายน
            /// </summary>
            public static String _June = "June";
            /// <summary>
            /// กรกฎาคม
            /// </summary>
            public static String _July = "July";
            /// <summary>
            /// สิงหาคม
            /// </summary>
            public static String _August = "August";
            /// <summary>
            /// กันยายน
            /// </summary>
            public static String _September = "September";
            /// <summary>
            /// ตุลาคม
            /// </summary>
            public static String _October = "October";
            /// <summary>
            /// พฤศจิกายน
            /// </summary>
            public static String _November = "November";
            /// <summary>
            /// ธันวาคม
            /// </summary>
            public static String _December = "December";
            /// <summary>
            /// กองทุนสำรองเลี้ยงชีพ
            /// </summary>
            public static String _tab_reserve_fund = "tab_reserve_fund";
            /// <summary>
            /// เข้ากองทุนสำรองเลี้ยงชีพ
            /// </summary>
            public static String _access_reserve_fund = "access_reserve_fund";
            /// <summary>
            /// ไม่เข้ากองทุนสำรองเลี้ยงชีพ
            /// </summary>
            public static String _not_access_reserve_fund = "not_access_reserve_fund";
            /// <summary>
            /// รหัสสมาชิกกองทุนสำรองเลี้ยงชีพ
            /// </summary>
            public static String _reserve_fund_no = "reserve_fund_no";
            /// <summary>
            /// วันที่เริ่มเข้ากองทุนสำรองเลี้ยงชีพ
            /// </summary>
            public static String _reserve_fund_start_date = "reserve_fund_start_date";
            /// <summary>
            /// วันที่สิ้นสุดสมาชิก
            /// </summary>
            public static String _reserve_fund_end_date = "reserve_fund_end_date";
            /// <summary>
            /// สถานะ
            /// </summary>
            public static String _reserve_fund_status = "reserve_fund_status";
            /// <summary>
            /// สถานะเงินเดือน
            /// </summary>
            public static String _salary_status = "salary_status";
            /// <summary>
            /// เงินเดือนแบบแปรผัน
            /// </summary>
            public static String _salary_vary = "salary_vary";
            /// <summary>
            /// เงินเดือนแบบคงที่
            /// </summary>
            public static String _salary_constant = "salary_constant";
            /// <summary>
            /// เงินเดือนแบบแปรผัน
            /// </summary>
            public static String _tab_salary_vary = "tab_salary_vary";
        }

        /// <summary>
        /// รายละเอียด
        /// </summary>
        public class payroll_employee_detail
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "payroll_employee_detail";
            /// <summary>
            /// รหัส
            /// </summary>
            public static String _code = "code";
            /// <summary>
            /// วันเกิด
            /// </summary>
            public static String _birth_day = "birth_day";
            /// <summary>
            /// อายุ
            /// </summary>
            public static String _age = "age";
            /// <summary>
            /// เพศ
            /// </summary>
            public static String _sex = "sex";
            /// <summary>
            /// กรุ๊ปเลือด
            /// </summary>
            public static String _group_blood = "group_blood";
            /// <summary>
            /// ส่วนสูง(cm)
            /// </summary>
            public static String _high = "high";
            /// <summary>
            /// น้ำหนัก(kg)
            /// </summary>
            public static String _weigh = "weigh";
            /// <summary>
            /// เชื้อชาติ
            /// </summary>
            public static String _race = "race";
            /// <summary>
            /// สัญชาติ
            /// </summary>
            public static String _nationality = "nationality";
            /// <summary>
            /// ศาสนา
            /// </summary>
            public static String _religion = "religion";
            /// <summary>
            /// รหัสบัตรประชาชน
            /// </summary>
            public static String _id_code = "id_code";
            /// <summary>
            /// ออกให้ ณ
            /// </summary>
            public static String _produce_at = "produce_at";
            /// <summary>
            /// วันที่ออกบัตร
            /// </summary>
            public static String _produce_card = "produce_card";
            /// <summary>
            /// วันที่หมดอายุ
            /// </summary>
            public static String _expire_card = "expire_card";
            /// <summary>
            /// ที่อยู่ตามบัตรประชาชน
            /// </summary>
            public static String _address_id_card = "address_id_card";
            /// <summary>
            /// ความสามารถพิเศษ
            /// </summary>
            public static String _talent = "talent";
            /// <summary>
            /// สถานะภาพทางทหาร
            /// </summary>
            public static String _situation_soldier = "situation_soldier";
            /// <summary>
            /// สถานภาพ
            /// </summary>
            public static String _situation = "situation";
            /// <summary>
            /// ชื่อคู่สมรส
            /// </summary>
            public static String _spouse_name = "spouse_name";
            /// <summary>
            /// วันเกิด
            /// </summary>
            public static String _spouse_birth_day = "spouse_birth_day";
            /// <summary>
            /// รหัสบัตรประชาชน
            /// </summary>
            public static String _spouse_id_code = "spouse_id_code";
            /// <summary>
            /// สถานะของคู่สมรส
            /// </summary>
            public static String _spouse_work_status = "spouse_work_status";
            /// <summary>
            /// คู่สมรสยื่นภาษี
            /// </summary>
            public static String _spouse_tax = "spouse_tax";
            /// <summary>
            /// ลดหย่อนคู่สมรส
            /// </summary>
            public static String _reduce_spouse = "reduce_spouse";
            /// <summary>
            /// ลดหย่อนผู้มีเงินได้
            /// </summary>
            public static String _reduce_myself = "reduce_myself";
            /// <summary>
            /// จำนวนบุตรกำลังศึกษา
            /// </summary>
            public static String _child_study_qty = "child_study_qty";
            /// <summary>
            /// จำนวนบุตรไม่ได้ศึกษา
            /// </summary>
            public static String _child_no_study_qty = "child_no_study_qty";
            /// <summary>
            /// ลดหย่อนบุตรที่ศึกษาอยู่
            /// </summary>
            public static String _reduce_child_study = "reduce_child_study";
            /// <summary>
            /// ลดหย่อนบุตรที่ไม่ได้ศึกษา
            /// </summary>
            public static String _reduce_child_no_study = "reduce_child_no_study";
            /// <summary>
            /// เบี้ยประกันบิดามารดา
            /// </summary>
            public static String _insurance_parents = "insurance_parents";
            /// <summary>
            /// ลดหย่อนบิดามารดา
            /// </summary>
            public static String _reduce_parents = "reduce_parents";
            /// <summary>
            /// ลดหย่อนบิดา
            /// </summary>
            public static String _reduce_father = "reduce_father";
            /// <summary>
            /// ลดหย่อนมารดา
            /// </summary>
            public static String _reduce_mother = "reduce_mother";
            /// <summary>
            /// ลดหย่อนบิดาคู่สมรส
            /// </summary>
            public static String _reduce_father_law = "reduce_father_law";
            /// <summary>
            /// ลดหย่อนมารดาคู่สมรส
            /// </summary>
            public static String _reduce_mother_law = "reduce_mother_law";
            /// <summary>
            /// ชื่อบิดา
            /// </summary>
            public static String _father_name = "father_name";
            /// <summary>
            /// ชื่อมารดา
            /// </summary>
            public static String _mother_name = "mother_name";
            /// <summary>
            /// ชื่อบิดาคู่สมรส
            /// </summary>
            public static String _father_law_name = "father_law_name";
            /// <summary>
            /// ชื่อมารดาคู่สมรส
            /// </summary>
            public static String _mother_law_name = "mother_law_name";
            /// <summary>
            /// รหัสบัตรประชาชนบิดา
            /// </summary>
            public static String _father_id = "father_id";
            /// <summary>
            /// รหัสบัตรประชาชนมารดา
            /// </summary>
            public static String _mother_id = "mother_id";
            /// <summary>
            /// รหัสบัตรประชาชนบิดาคู่สมรส
            /// </summary>
            public static String _father_law_id = "father_law_id";
            /// <summary>
            /// รหัสบัตรประชาชนมารดาคู่สมรส
            /// </summary>
            public static String _mother_law_id = "mother_law_id";
            /// <summary>
            /// เบี้ยประกันชีวิต
            /// </summary>
            public static String _insurance = "insurance";
            /// <summary>
            /// เบี้ยกู้ยืมเพื่อซื้อที่อยู่อาศัย
            /// </summary>
            public static String _loan = "loan";
            /// <summary>
            /// สะสมกองทุนสำรองเลี้ยงชีพ
            /// </summary>
            public static String _funds_reserves = "funds_reserves";
            /// <summary>
            /// ซื้อหน่วยลงทุน RMF
            /// </summary>
            public static String _buy_funds_reserves = "buy_funds_reserves";
            /// <summary>
            /// ซื้อหน่วยลงทุน LTF
            /// </summary>
            public static String _buy_share = "buy_share";
            /// <summary>
            /// เงินบริจาค
            /// </summary>
            public static String _donation = "donation";
            /// <summary>
            /// บริจาคเพื่อการศึกษา
            /// </summary>
            public static String _donation_education = "donation_education";
            /// <summary>
            /// เงินสมทบกองทุนประกันสังคม
            /// </summary>
            public static String _insurance_social = "insurance_social";
            /// <summary>
            /// ยกเว้นสะสมกองทุนสำรองเลี้ยงชีพ
            /// </summary>
            public static String _except_funds_reserves = "except_funds_reserves";
            /// <summary>
            /// ยกเว้นสะสม กบข.
            /// </summary>
            public static String _except_gbk = "except_gbk";
            /// <summary>
            /// ยกเว้นสะสมสงเคราะห์ครูเอกชน
            /// </summary>
            public static String _except_teacher = "except_teacher";
            /// <summary>
            /// ยกเว้นผู้มีเงินได้อายุ 65 ปีขึ้นไป
            /// </summary>
            public static String _except_65year = "except_65year";
            /// <summary>
            /// ยกเว้นคู่สมรสอายุ 65 ปีขึ้นไป
            /// </summary>
            public static String _except_spouse_65year = "except_spouse_65year";
            /// <summary>
            /// ยกเว้นค่าชดเชยตาม กม. แรงงาน
            /// </summary>
            public static String _except_labour_legislation = "except_labour_legislation";
            /// <summary>
            /// โสด
            /// </summary>
            public static String _single = "single";
            /// <summary>
            /// สมรส
            /// </summary>
            public static String _married = "married";
            /// <summary>
            /// หม้าย
            /// </summary>
            public static String _widow = "widow";
            /// <summary>
            /// คู่สมรสมีเงินได้
            /// </summary>
            public static String _spouse_work = "spouse_work";
            /// <summary>
            /// คู่สมรสไม่มีเงินได้
            /// </summary>
            public static String _spouse_not_work = "spouse_not_work";
            /// <summary>
            /// ชาย
            /// </summary>
            public static String _male = "male";
            /// <summary>
            /// หญิง
            /// </summary>
            public static String _female = "female";
            /// <summary>
            /// เกณฑ์ทหารแล้ว
            /// </summary>
            public static String _conscript = "conscript";
            /// <summary>
            /// ยังไม่เกณฑ์ทหาร
            /// </summary>
            public static String _not_conscript = "not_conscript";
            /// <summary>
            /// ยื่นรวม
            /// </summary>
            public static String _totle_up = "totle_up";
            /// <summary>
            /// แยกยื่น
            /// </summary>
            public static String _separate = "separate";
            /// <summary>
            /// ยกเว้นการเกณฑ์ทหาร
            /// </summary>
            public static String _no_conscript = "no_conscript";
        }

        /// <summary>
        /// ที่อยู่ปัจจุบัน
        /// </summary>
        public class payroll_employee_address
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "payroll_employee_address";
            /// <summary>
            /// รหัส
            /// </summary>
            public static String _code = "code";
            /// <summary>
            /// ที่อยู่
            /// </summary>
            public static String _address = "address";
            /// <summary>
            /// อาคาร
            /// </summary>
            public static String _house = "house";
            /// <summary>
            /// ห้องที่
            /// </summary>
            public static String _room_no = "room_no";
            /// <summary>
            /// ชั้นที่
            /// </summary>
            public static String _floor_no = "floor_no";
            /// <summary>
            /// หมู่บ้าน
            /// </summary>
            public static String _village = "village";
            /// <summary>
            /// เลขที่
            /// </summary>
            public static String _house_no = "house_no";
            /// <summary>
            /// หมู่ที่
            /// </summary>
            public static String _crowd_no = "crowd_no";
            /// <summary>
            /// ตรอก/ซอย
            /// </summary>
            public static String _lane = "lane";
            /// <summary>
            /// ถนน
            /// </summary>
            public static String _road = "road";
            /// <summary>
            /// ตำบล/แขวง
            /// </summary>
            public static String _locality = "locality";
            /// <summary>
            /// อำเภอ/เขต
            /// </summary>
            public static String _amphur = "amphur";
            /// <summary>
            /// จังหวัด
            /// </summary>
            public static String _province = "province";
            /// <summary>
            /// รหัสไปรษณีย์
            /// </summary>
            public static String _postcode = "postcode";
            /// <summary>
            /// โทรศัพท์
            /// </summary>
            public static String _telephone = "telephone";
            /// <summary>
            /// โทรศัพท์มือถือ
            /// </summary>
            public static String _mobile = "mobile";
            /// <summary>
            /// ผู้ติดต่อกรณีฉุกเฉิน
            /// </summary>
            public static String _contact = "contact";
            /// <summary>
            /// ความสัมพันธ์
            /// </summary>
            public static String _relation = "relation";
            /// <summary>
            /// โทรศัพท์
            /// </summary>
            public static String _telephone_contact = "telephone_contact";
        }

        /// <summary>
        /// ประวัติการศึกษา
        /// </summary>
        public class payroll_employee_education
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "payroll_employee_education";
            /// <summary>
            /// รหัส
            /// </summary>
            public static String _code = "code";
            /// <summary>
            /// ระดับการศึกษา
            /// </summary>
            public static String _education_level = "education_level";
            /// <summary>
            /// คณะ/สาขาวิชาหลัก
            /// </summary>
            public static String _education_major = "education_major";
            /// <summary>
            /// คณะ/สาขาวิชารอง
            /// </summary>
            public static String _education_minor = "education_minor";
            /// <summary>
            /// สถาบันการศึกษา
            /// </summary>
            public static String _education_intitute = "education_intitute";
            /// <summary>
            /// ปีที่จบการศึกษา
            /// </summary>
            public static String _finished_year = "finished_year";
            /// <summary>
            /// กิจกรรมขณะศึกษาอยู่
            /// </summary>
            public static String _activity = "activity";
            /// <summary>
            /// หมายเหตุ
            /// </summary>
            public static String _remark = "remark";
            /// <summary>
            /// เกรดเฉลี่ย
            /// </summary>
            public static String _grade = "grade";
        }

        /// <summary>
        /// ประสบการณ์การทำงาน
        /// </summary>
        public class payroll_employee_experience
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "payroll_employee_experience";
            /// <summary>
            /// รหัส
            /// </summary>
            public static String _code = "code";
            /// <summary>
            /// สถานที่ทำงาน
            /// </summary>
            public static String _exp_place = "exp_place";
            /// <summary>
            /// ตำแหน่ง
            /// </summary>
            public static String _exp_title = "exp_title";
            /// <summary>
            /// ระยะเวลา
            /// </summary>
            public static String _exp_time = "exp_time";
            /// <summary>
            /// สาเหตุที่ออก
            /// </summary>
            public static String _remark = "remark";
            /// <summary>
            /// ปีที่เริ่ม
            /// </summary>
            public static String _exp_start = "exp_start";
            /// <summary>
            /// ถึงปี
            /// </summary>
            public static String _exp_to = "exp_to";
            /// <summary>
            /// เงินเดือน
            /// </summary>
            public static String _exp_salary = "exp_salary";
        }

        /// <summary>
        /// การขาดงาน
        /// </summary>
        public class payroll_short_of_work
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "payroll_short_of_work";
            /// <summary>
            /// รหัส
            /// </summary>
            public static String _code = "code";
            /// <summary>
            /// ชื่อ
            /// </summary>
            public static String _name = "name";
            /// <summary>
            /// อัตราหักค่าแรง(ต่อวัน)
            /// </summary>
            public static String _wages_cut_rate = "wages_cut_rate";
            /// <summary>
            /// จำนวนเงินที่หัก
            /// </summary>
            public static String _wages_cut_money = "wages_cut_money";
            /// <summary>
            /// วิธีการคำนวณ
            /// </summary>
            public static String _select_rate_or_money = "select_rate_or_money";
            /// <summary>
            /// หมายเหตุ
            /// </summary>
            public static String _remark = "remark";
            /// <summary>
            /// guid_code
            /// </summary>
            public static String _guid_code = "guid_code";
            /// <summary>
            /// คำนวณจากอัตราค่าแรง(ต่อวัน)
            /// </summary>
            public static String _select_rate = "select_rate";
            /// <summary>
            /// คำนวณจากเงินที่หัก
            /// </summary>
            public static String _select_money = "select_money";
        }

        /// <summary>
        /// การลางาน
        /// </summary>
        public class payroll_leave
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "payroll_leave";
            /// <summary>
            /// รหัส
            /// </summary>
            public static String _code = "code";
            /// <summary>
            /// ชื่อ
            /// </summary>
            public static String _name = "name";
            /// <summary>
            /// อัตราหักค่าแรง(ต่อชม.)
            /// </summary>
            public static String _leave_rate = "leave_rate";
            /// <summary>
            /// จำนวนเงินที่หัก
            /// </summary>
            public static String _leave_money = "leave_money";
            /// <summary>
            /// วิธีการคำนวณ
            /// </summary>
            public static String _select_rate_or_money = "select_rate_or_money";
            /// <summary>
            /// หมายเหตุ
            /// </summary>
            public static String _remark = "remark";
            /// <summary>
            /// guid_code
            /// </summary>
            public static String _guid_code = "guid_code";
            /// <summary>
            /// คำนวณจากอัตราค่าแรง(ต่อชม.)
            /// </summary>
            public static String _select_rate = "select_rate";
            /// <summary>
            /// คำนวณจากเงินที่หัก
            /// </summary>
            public static String _select_money = "select_money";
        }

        /// <summary>
        /// การมาสาย
        /// </summary>
        public class payroll_arrive_late
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "payroll_arrive_late";
            /// <summary>
            /// รหัส
            /// </summary>
            public static String _code = "code";
            /// <summary>
            /// ชื่อ
            /// </summary>
            public static String _name = "name";
            /// <summary>
            /// อัตราหักค่าแรง(ต่อชม.)
            /// </summary>
            public static String _arrive_late_rate = "arrive_late_rate";
            /// <summary>
            /// จำนวนเงินที่หัก
            /// </summary>
            public static String _arrive_late_money = "arrive_late_money";
            /// <summary>
            /// วิธีการคำนวณ
            /// </summary>
            public static String _select_rate_or_money = "select_rate_or_money";
            /// <summary>
            /// หมายเหตุ
            /// </summary>
            public static String _remark = "remark";
            /// <summary>
            /// guid_code
            /// </summary>
            public static String _guid_code = "guid_code";
            /// <summary>
            /// คำนวณจากอัตราค่าแรง(ต่อชม.)
            /// </summary>
            public static String _select_rate = "select_rate";
            /// <summary>
            /// คำนวณจากเงินที่หัก
            /// </summary>
            public static String _select_money = "select_money";
        }

        /// <summary>
        /// ค่าล่วงเวลา
        /// </summary>
        public class payroll_over_time
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "payroll_over_time";
            /// <summary>
            /// รหัส
            /// </summary>
            public static String _code = "code";
            /// <summary>
            /// ชื่อ
            /// </summary>
            public static String _name = "name";
            /// <summary>
            /// อัตราล่วงเวลา(ต่อชม.)
            /// </summary>
            public static String _ot_rate = "ot_rate";
            /// <summary>
            /// หมายเหตุ
            /// </summary>
            public static String _remark = "remark";
            /// <summary>
            /// guid_code
            /// </summary>
            public static String _guid_code = "guid_code";
        }

        /// <summary>
        /// ภาษีเงินได้
        /// </summary>
        public class payroll_income_tax
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "payroll_income_tax";
            /// <summary>
            /// รหัส
            /// </summary>
            public static String _code = "code";
            /// <summary>
            /// เงินได้ตั้งแต่
            /// </summary>
            public static String _income_from = "income_from";
            /// <summary>
            /// จนกระทั่งถึง
            /// </summary>
            public static String _income_to = "income_to";
            /// <summary>
            /// จำนวนเงิน
            /// </summary>
            public static String _income_money = "income_money";
            /// <summary>
            /// อัตราภาษี
            /// </summary>
            public static String _tax_rate = "tax_rate";
            /// <summary>
            /// มูลค่าภาษี
            /// </summary>
            public static String _tax_money = "tax_money";
            /// <summary>
            /// มูลค่าภาษีสะสม
            /// </summary>
            public static String _sum_taxt_money = "sum_taxt_money";
            /// <summary>
            /// หมายเหตุ
            /// </summary>
            public static String _remark = "remark";
            /// <summary>
            /// guid_code
            /// </summary>
            public static String _guid_code = "guid_code";
        }

        /// <summary>
        /// บันทึกการทำงาน
        /// </summary>
        public class payroll_trans
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "payroll_trans";
            /// <summary>
            /// trans flag
            /// </summary>
            public static String _trans_flag = "trans_flag";
            /// <summary>
            /// เลขที่เอกสาร
            /// </summary>
            public static String _doc_no = "doc_no";
            /// <summary>
            /// วันที่เอกสาร
            /// </summary>
            public static String _doc_date = "doc_date";
            /// <summary>
            /// ชื่อ-นามสกุล
            /// </summary>
            public static String _employee = "employee";
            /// <summary>
            /// ประเภทลูกจ้าง
            /// </summary>
            public static String _employee_type = "employee_type";
            /// <summary>
            /// วันที่เริ่มต้น
            /// </summary>
            public static String _work_from_date = "work_from_date";
            /// <summary>
            /// วันที่สิ้นสุด
            /// </summary>
            public static String _work_end_date = "work_end_date";
            /// <summary>
            /// วันที่จ่ายค่าจ้าง
            /// </summary>
            public static String _date_pay_salary = "date_pay_salary";
            /// <summary>
            /// จำนวนวันหยุด
            /// </summary>
            public static String _holiday = "holiday";
            /// <summary>
            /// จำนวนวันทำงาน
            /// </summary>
            public static String _working_day = "working_day";
            /// <summary>
            /// ผู้บันทึก
            /// </summary>
            public static String _saver_code = "saver_code";
            /// <summary>
            /// เดือน
            /// </summary>
            public static String _select_month = "select_month";
            /// <summary>
            /// เดือน
            /// </summary>
            public static String _select_month_str = "select_month_str";
            /// <summary>
            /// ปี
            /// </summary>
            public static String _select_year = "select_year";
            /// <summary>
            /// หมายเหตุ
            /// </summary>
            public static String _remark = "remark";
            /// <summary>
            /// สถานะ
            /// </summary>
            public static String _status = "status";
            /// <summary>
            /// guid_code
            /// </summary>
            public static String _guid_code = "guid_code";
            /// <summary>
            /// ปกติ
            /// </summary>
            public static String _normal = "normal";
            /// <summary>
            /// ยกเลิก
            /// </summary>
            public static String _cancel = "cancel";
            /// <summary>
            /// รายเดือน
            /// </summary>
            public static String _monthly = "monthly";
            /// <summary>
            /// รายสัปดาห์
            /// </summary>
            public static String _weekly = "weekly";
            /// <summary>
            /// รายวัน
            /// </summary>
            public static String _daily = "daily";
            /// <summary>
            /// ขาดงาน
            /// </summary>
            public static String _tab_short_of_work = "tab_short_of_work";
            /// <summary>
            /// ลางาน
            /// </summary>
            public static String _tab_leave = "tab_leave";
            /// <summary>
            /// มาสาย
            /// </summary>
            public static String _tab_arrive_late = "tab_arrive_late";
            /// <summary>
            /// ล่วงเวลา
            /// </summary>
            public static String _tab_over_time = "tab_over_time";
            /// <summary>
            /// เงินได้-เงินหักอื่นๆ
            /// </summary>
            public static String _tab_other_money = "tab_other_money";
            /// <summary>
            /// วันทำงาน
            /// </summary>
            public static String _tab_working_day = "tab_working_day";
            /// <summary>
            /// มกราคม
            /// </summary>
            public static String _January = "January";
            /// <summary>
            /// กุมภาพันธ์
            /// </summary>
            public static String _February = "February";
            /// <summary>
            /// มีนาคม
            /// </summary>
            public static String _March = "March";
            /// <summary>
            /// เมษายน
            /// </summary>
            public static String _April = "April";
            /// <summary>
            /// พฤษภาคม
            /// </summary>
            public static String _May = "May";
            /// <summary>
            /// มิถุนายน
            /// </summary>
            public static String _June = "June";
            /// <summary>
            /// กรกฎาคม
            /// </summary>
            public static String _July = "July";
            /// <summary>
            /// สิงหาคม
            /// </summary>
            public static String _August = "August";
            /// <summary>
            /// กันยายน
            /// </summary>
            public static String _September = "September";
            /// <summary>
            /// ตุลาคม
            /// </summary>
            public static String _October = "October";
            /// <summary>
            /// พฤษจิกายน
            /// </summary>
            public static String _November = "November";
            /// <summary>
            /// ธันวาคม
            /// </summary>
            public static String _December = "December";
            /// <summary>
            /// จากลูกจ้าง
            /// </summary>
            public static String _employee_from = "employee_from";
            /// <summary>
            /// ถึงลูกจ้าง
            /// </summary>
            public static String _employee_to = "employee_to";
            /// <summary>
            /// ลูกจ้างทั้งหมด
            /// </summary>
            public static String _employee_all = "employee_all";
            /// <summary>
            /// ชื่อลูกจ้าง
            /// </summary>
            public static String _employee_name = "employee_name";
            /// <summary>
            /// เงินค่าจ้าง
            /// </summary>
            public static String _wages_money = "wages_money";
            /// <summary>
            /// เงินหัก(ขาดงาน)
            /// </summary>
            public static String _sow_money = "sow_money";
            /// <summary>
            /// เงินหัก(ลางาน)
            /// </summary>
            public static String _leave_money = "leave_money";
            /// <summary>
            /// เงินหัก(มาสาย)
            /// </summary>
            public static String _late_money = "late_money";
            /// <summary>
            /// ค่าล่วงเวลา
            /// </summary>
            public static String _ot_money = "ot_money";
            /// <summary>
            /// ประกันสังคม
            /// </summary>
            public static String _society_insure_money = "society_insure_money";
            /// <summary>
            /// เงินกองทุนสำรอง
            /// </summary>
            public static String _reserve_money = "reserve_money";
            /// <summary>
            /// เบี๊ยขยัน
            /// </summary>
            public static String _diligent_money = "diligent_money";
            /// <summary>
            /// เงินโบนัส
            /// </summary>
            public static String _bonus_money = "bonus_money";
            /// <summary>
            /// เงินได้อื่นๆ
            /// </summary>
            public static String _other_income_money = "other_income_money";
            /// <summary>
            /// หักเงินจ่ายล่วงหน้า
            /// </summary>
            public static String _advance_money = "advance_money";
            /// <summary>
            /// เงินค้ำประกัน
            /// </summary>
            public static String _insure_money = "insure_money";
            /// <summary>
            /// เงินจ่ายอื่นๆ
            /// </summary>
            public static String _other_payout_money = "other_payout_money";
            /// <summary>
            /// เงินค่าจ้างสุทธิ
            /// </summary>
            public static String _net_wages = "net_wages";
            /// <summary>
            /// เงินจ่ายล่วงหน้า
            /// </summary>
            public static String _prepay_money = "prepay_money";
        }

        /// <summary>
        /// รายละเอียดการทำงาน
        /// </summary>
        public class payroll_trans_detail
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "payroll_trans_detail";
            /// <summary>
            /// trans flag
            /// </summary>
            public static String _trans_flag = "trans_flag";
            /// <summary>
            /// trans type
            /// </summary>
            public static String _trans_type = "trans_type";
            /// <summary>
            /// เลขที่เอกสาร
            /// </summary>
            public static String _doc_no = "doc_no";
            /// <summary>
            /// วันที่เอกสาร
            /// </summary>
            public static String _doc_date = "doc_date";
            /// <summary>
            /// รหัสพนักงาน
            /// </summary>
            public static String _employee = "employee";
            /// <summary>
            /// ประเภทลูกจ้าง
            /// </summary>
            public static String _employee_type = "employee_type";
            /// <summary>
            /// ประเภทการทำงาน
            /// </summary>
            public static String _working_type = "working_type";
            /// <summary>
            /// วันที่ทำงาน
            /// </summary>
            public static String _working_date = "working_date";
            /// <summary>
            /// จำนวนวัน
            /// </summary>
            public static String _working_day = "working_day";
            /// <summary>
            /// จำนวนชั่วโมง
            /// </summary>
            public static String _working_hours = "working_hours";
            /// <summary>
            /// นาที
            /// </summary>
            public static String _working_minute = "working_minute";
            /// <summary>
            /// จำนวนเงิน
            /// </summary>
            public static String _money = "money";
            /// <summary>
            /// เบี้ยขยัน
            /// </summary>
            public static String _diligent_money = "diligent_money";
            /// <summary>
            /// เงินโบนัส
            /// </summary>
            public static String _bonus_money = "bonus_money";
            /// <summary>
            /// เงินได้อื่นๆ
            /// </summary>
            public static String _other_income_money = "other_income_money";
            /// <summary>
            /// เงินจ่ายล่วงหน้า
            /// </summary>
            public static String _prepay_money = "prepay_money";
            /// <summary>
            /// หักเงินจ่ายล่วงหน้า
            /// </summary>
            public static String _advance_money = "advance_money";
            /// <summary>
            /// เงินค้ำประกัน
            /// </summary>
            public static String _insure_money = "insure_money";
            /// <summary>
            /// เงินจ่ายอื่นๆ
            /// </summary>
            public static String _other_payout_money = "other_payout_money";
            /// <summary>
            /// ภาษีที่ชำระแล้ว
            /// </summary>
            public static String _tax_already_pay = "tax_already_pay";
            /// <summary>
            /// เดือน
            /// </summary>
            public static String _select_month = "select_month";
            /// <summary>
            /// ปี
            /// </summary>
            public static String _select_year = "select_year";
            /// <summary>
            /// หมายเหตุ
            /// </summary>
            public static String _remark = "remark";
            /// <summary>
            /// บรรทัด
            /// </summary>
            public static String _line_number = "line_number";
            /// <summary>
            /// รหัสการขาดงาน
            /// </summary>
            public static String _sow_type = "sow_type";
            /// <summary>
            /// ประเภทการขาดงาน
            /// </summary>
            public static String _sow_name = "sow_name";
            /// <summary>
            /// รหัสการมาสาย
            /// </summary>
            public static String _late_type = "late_type";
            /// <summary>
            /// ประเภทการมาสาย
            /// </summary>
            public static String _late_name = "late_name";
            /// <summary>
            /// รหัสการลางาน
            /// </summary>
            public static String _leave_type = "leave_type";
            /// <summary>
            /// ประเภทการลางาน
            /// </summary>
            public static String _leave_name = "leave_name";
            /// <summary>
            /// รหัสการล่วงเวลา
            /// </summary>
            public static String _ot_type = "ot_type";
            /// <summary>
            /// ประเภทการล่วงเวลา
            /// </summary>
            public static String _ot_name = "ot_name";
            /// <summary>
            /// วันที่มาสาย
            /// </summary>
            public static String _late_date = "late_date";
            /// <summary>
            /// วันที่ลางาน
            /// </summary>
            public static String _leave_date = "leave_date";
            /// <summary>
            /// วันที่ขาดงาน
            /// </summary>
            public static String _sow_date = "sow_date";
            /// <summary>
            /// วันที่ทำโอที
            /// </summary>
            public static String _ot_date = "ot_date";
            /// <summary>
            /// ภาษีเงินได้
            /// </summary>
            public static String _tax_income = "tax_income";
            /// <summary>
            /// มูลค่าภาษีสุทธิ
            /// </summary>
            public static String _net_tax = "net_tax";
            /// <summary>
            /// ค่าจ้างสุทธิ
            /// </summary>
            public static String _net_wages = "net_wages";
            /// <summary>
            /// เงินค่าจ้าง
            /// </summary>
            public static String _wages_money = "wages_money";
            /// <summary>
            /// ชื่อพนักงาน
            /// </summary>
            public static String _employee_name = "employee_name";
            /// <summary>
            /// วันที่ทำรายการ
            /// </summary>
            public static String _other_money_date = "other_money_date";
        }

        /// <summary>
        /// สาขาธนาคาร
        /// </summary>
        public class payroll_branch_bank
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "payroll_branch_bank";
            /// <summary>
            /// รหัส
            /// </summary>
            public static String _code = "code";
            /// <summary>
            /// ชื่อ(ไทย)
            /// </summary>
            public static String _name1 = "name1";
            /// <summary>
            /// ชื่อ(อังกฤษ)
            /// </summary>
            public static String _name2 = "name2";
            /// <summary>
            /// รหัสธนาคาร
            /// </summary>
            public static String _bank_code = "bank_code";
            /// <summary>
            /// หมายเหตุ
            /// </summary>
            public static String _remark = "remark";
            /// <summary>
            /// สถานะ
            /// </summary>
            public static String _status = "status";
            /// <summary>
            /// guid_code
            /// </summary>
            public static String _guid_code = "guid_code";
            /// <summary>
            /// ใช้
            /// </summary>
            public static String _active = "active";
            /// <summary>
            /// ไม่ใช้
            /// </summary>
            public static String _inactive = "inactive";
        }

        /// <summary>
        /// บุคคลค้ำประกัน
        /// </summary>
        public class payroll_employee_collateral
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "payroll_employee_collateral";
            /// <summary>
            /// รหัส
            /// </summary>
            public static String _code = "code";
            /// <summary>
            /// ชื่อผู้ค้ำประกัน
            /// </summary>
            public static String _name = "name";
            /// <summary>
            /// อาชีพ
            /// </summary>
            public static String _occupation = "occupation";
            /// <summary>
            /// ความสัมพันธ์
            /// </summary>
            public static String _relation = "relation";
            /// <summary>
            /// ที่อยู่
            /// </summary>
            public static String _address = "address";
            /// <summary>
            /// โทรศัพท์
            /// </summary>
            public static String _telephone = "telephone";
        }

        /// <summary>
        /// ผู้ติดต่อกรณีฉุกเฉิน
        /// </summary>
        public class payroll_employee_family
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "payroll_employee_family";
            /// <summary>
            /// รหัส
            /// </summary>
            public static String _code = "code";
            /// <summary>
            /// ชื่อ - นามสกุล
            /// </summary>
            public static String _name = "name";
            /// <summary>
            /// อาชีพ
            /// </summary>
            public static String _occupation = "occupation";
            /// <summary>
            /// ความสัมพันธ์
            /// </summary>
            public static String _relation = "relation";
            /// <summary>
            /// ที่อยู่
            /// </summary>
            public static String _address = "address";
            /// <summary>
            /// โทรศัพท์
            /// </summary>
            public static String _telephone = "telephone";
        }

        /// <summary>
        /// บัญชีธนาคาร
        /// </summary>
        public class payroll_company_bank
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "payroll_company_bank";
            /// <summary>
            /// รหัส
            /// </summary>
            public static String _code = "code";
            /// <summary>
            /// ชื่อธนาคาร
            /// </summary>
            public static String _name = "name";
            /// <summary>
            /// เลขสมุดบัญชีธนาคาร
            /// </summary>
            public static String _book_bank_no = "book_bank_no";
            /// <summary>
            /// สาขา
            /// </summary>
            public static String _branch_bank = "branch_bank";
            /// <summary>
            /// guid_code
            /// </summary>
            public static String _guid_code = "guid_code";
            /// <summary>
            /// ชื่อสาขา
            /// </summary>
            public static String _branch_name = "branch_name";
        }

        /// <summary>
        /// วันหยุดบริษัท
        /// </summary>
        public class payroll_company_holiday
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "payroll_company_holiday";
            /// <summary>
            /// รหัส
            /// </summary>
            public static String _code = "code";
            /// <summary>
            /// จันทร์
            /// </summary>
            public static String _mon = "mon";
            /// <summary>
            /// อังคาร
            /// </summary>
            public static String _tue = "tue";
            /// <summary>
            /// พุธ
            /// </summary>
            public static String _wed = "wed";
            /// <summary>
            /// พฤหัสบดี
            /// </summary>
            public static String _thu = "thu";
            /// <summary>
            /// ศุกร์
            /// </summary>
            public static String _fri = "fri";
            /// <summary>
            /// เสาร์
            /// </summary>
            public static String _sat = "sat";
            /// <summary>
            /// อาทิตย์
            /// </summary>
            public static String _sun = "sun";
        }

        /// <summary>
        /// log
        /// </summary>
        public class logs
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "logs";
            /// <summary>
            /// 1=เพิ่ม,2=แก้ไข,3=ลบ
            /// </summary>
            public static String _function_code = "function_code";
            /// <summary>
            /// ข้อมูลใหม่
            /// </summary>
            public static String _data1 = "data1";
            /// <summary>
            /// ข้อมูลเดิม
            /// </summary>
            public static String _data2 = "data2";
            /// <summary>
            /// รหัสผู้ใช้
            /// </summary>
            public static String _user_code = "user_code";
            /// <summary>
            /// วันที่/เวลา
            /// </summary>
            public static String _date_time = "date_time";
            /// <summary>
            /// รหัสหน้าจอ
            /// </summary>
            public static String _screen_code = "screen_code";
            /// <summary>
            /// guid
            /// </summary>
            public static String _guid = "guid";
            /// <summary>
            /// วันที่เอกสาร
            /// </summary>
            public static String _doc_date = "doc_date";
            /// <summary>
            /// วันที่เอกสารเดิม
            /// </summary>
            public static String _doc_date_old = "doc_date_old";
            /// <summary>
            /// เลขที่เอกสาร
            /// </summary>
            public static String _doc_no = "doc_no";
            /// <summary>
            /// เลขที่เอกสารเดิม
            /// </summary>
            public static String _doc_no_old = "doc_no_old";
            /// <summary>
            /// มูลค่า
            /// </summary>
            public static String _doc_amount = "doc_amount";
            /// <summary>
            /// มูลค่าเดิม
            /// </summary>
            public static String _doc_amount_old = "doc_amount_old";
            /// <summary>
            /// 0=Login,1=Menu,2=Trans,3=Report
            /// </summary>
            public static String _function_type = "function_type";
            /// <summary>
            /// วันที่/เวลาออก
            /// </summary>
            public static String _date_time_out = "date_time_out";
            /// <summary>
            /// จากวันที่
            /// </summary>
            public static String _date_begin = "date_begin";
            /// <summary>
            /// ถึงวันที่
            /// </summary>
            public static String _date_end = "date_end";
            /// <summary>
            /// ค้นหา
            /// </summary>
            public static String _search = "search";
            /// <summary>
            /// ชื่อเครื่อง
            /// </summary>
            public static String _computer_name = "computer_name";
            /// <summary>
            /// เวลาเข้าใช้ระบบ
            /// </summary>
            public static String _login_time = "login_time";
            /// <summary>
            /// เวลาออกจากระบบ
            /// </summary>
            public static String _logout_time = "logout_time";
            /// <summary>
            /// ชื่อระบบ
            /// </summary>
            public static String _menu_name = "menu_name";
            /// <summary>
            /// ข้อมูลใหม่
            /// </summary>
            public static String _data_new = "data_new";
            /// <summary>
            /// ข้อมูลเดิม
            /// </summary>
            public static String _data_old = "data_old";
        }

        /// <summary>
        /// Resource
        /// </summary>
        public class payroll_resource
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "payroll_resource";
            /// <summary>
            /// เลขที่เอกสาร
            /// </summary>
            public static String _doc_no = "doc_no";
            /// <summary>
            /// วันที่เอกสาร
            /// </summary>
            public static String _doc_date = "doc_date";
            /// <summary>
            /// ประเภทลูกจ้าง
            /// </summary>
            public static String _employee_type = "employee_type";
            /// <summary>
            /// เดือน
            /// </summary>
            public static String _select_month = "select_month";
            /// <summary>
            /// ปี
            /// </summary>
            public static String _select_year = "select_year";
            /// <summary>
            /// จากวันที่
            /// </summary>
            public static String _date_from = "date_from";
            /// <summary>
            /// ถึงวันที่
            /// </summary>
            public static String _date_to = "date_to";
            /// <summary>
            /// เลือกลูกจ้างทั้งหมด
            /// </summary>
            public static String _employee_all = "employee_all";
            /// <summary>
            /// จากลูกจ้าง
            /// </summary>
            public static String _employee_from = "employee_from";
            /// <summary>
            /// ถึงลูกจ้าง
            /// </summary>
            public static String _employee_to = "employee_to";
            /// <summary>
            /// ปกติ
            /// </summary>
            public static String _normal = "normal";
            /// <summary>
            /// ยกเลิก
            /// </summary>
            public static String _cancel = "cancel";
            /// <summary>
            /// รายเดือน
            /// </summary>
            public static String _monthly = "monthly";
            /// <summary>
            /// รายสัปดาห์
            /// </summary>
            public static String _weekly = "weekly";
            /// <summary>
            /// รายวัน
            /// </summary>
            public static String _daily = "daily";
            /// <summary>
            /// มกราคม
            /// </summary>
            public static String _January = "January";
            /// <summary>
            /// กุมภาพันธ์
            /// </summary>
            public static String _February = "February";
            /// <summary>
            /// มีนาคม
            /// </summary>
            public static String _March = "March";
            /// <summary>
            /// เมษายน
            /// </summary>
            public static String _April = "April";
            /// <summary>
            /// พฤษภาคม
            /// </summary>
            public static String _May = "May";
            /// <summary>
            /// มิถุนายน
            /// </summary>
            public static String _June = "June";
            /// <summary>
            /// กรกฎาคม
            /// </summary>
            public static String _July = "July";
            /// <summary>
            /// สิงหาคม
            /// </summary>
            public static String _August = "August";
            /// <summary>
            /// กันยายน
            /// </summary>
            public static String _September = "September";
            /// <summary>
            /// ตุลาคม
            /// </summary>
            public static String _October = "October";
            /// <summary>
            /// พฤศจิกายน
            /// </summary>
            public static String _November = "November";
            /// <summary>
            /// ธันวาคม
            /// </summary>
            public static String _December = "December";
            /// <summary>
            /// รหัสลูกจ้าง
            /// </summary>
            public static String _employee = "employee";
            /// <summary>
            /// ชื่อลูกจ้าง
            /// </summary>
            public static String _employee_name = "employee_name";
            /// <summary>
            /// เงินค่าจ้าง
            /// </summary>
            public static String _wages_money = "wages_money";
            /// <summary>
            /// เงินหัก (ขาดงาน)
            /// </summary>
            public static String _sow_money = "sow_money";
            /// <summary>
            /// เงินหัก(ลางาน)
            /// </summary>
            public static String _leave_money = "leave_money";
            /// <summary>
            /// เงินหัก(มาสาย)
            /// </summary>
            public static String _late_money = "late_money";
            /// <summary>
            /// ค่าล่วงเวลา
            /// </summary>
            public static String _ot_money = "ot_money";
            /// <summary>
            /// เงินประกันสังคม
            /// </summary>
            public static String _society_insure_money = "society_insure_money";
            /// <summary>
            /// เงินกองทุนสำรอง
            /// </summary>
            public static String _reserve_money = "reserve_money";
            /// <summary>
            /// เบี้ยขยัน
            /// </summary>
            public static String _diligent_money = "diligent_money";
            /// <summary>
            /// เงินโบนัส
            /// </summary>
            public static String _bonus_money = "bonus_money";
            /// <summary>
            /// เงินจ่ายล่วงหน้า
            /// </summary>
            public static String _prepay_money = "prepay_money";
            /// <summary>
            /// เงินได้อื่นๆ
            /// </summary>
            public static String _other_income_money = "other_income_money";
            /// <summary>
            /// หักเงินจ่ายล่วงหน้า
            /// </summary>
            public static String _advance_money = "advance_money";
            /// <summary>
            /// เงินคํ้าประกัน
            /// </summary>
            public static String _insure_money = "insure_money";
            /// <summary>
            /// เงินจ่ายอื่นๆ
            /// </summary>
            public static String _other_payout_money = "other_payout_money";
            /// <summary>
            /// เป็นเงินค่าจ้างสุทธิ
            /// </summary>
            public static String _net_wages = "net_wages";
            /// <summary>
            /// ลดฯบุตร
            /// </summary>
            public static String _reduce_child = "reduce_child";
            /// <summary>
            /// ลดฯบิดา/มารดา
            /// </summary>
            public static String _reduce_father_mother = "reduce_father_mother";
            /// <summary>
            /// เบี้ยประกัน
            /// </summary>
            public static String _insurance = "insurance";
            /// <summary>
            /// เบี้ยกู้ยืม
            /// </summary>
            public static String _loan = "loan";
            /// <summary>
            /// หักค่าใช้จ่าย 40%
            /// </summary>
            public static String _reduce_40 = "reduce_40";
            /// <summary>
            /// ลดฯผู้มีเงินได้
            /// </summary>
            public static String _reduce_myself = "reduce_myself";
            /// <summary>
            /// ลดหย่อนอื่นๆ
            /// </summary>
            public static String _reduce_other = "reduce_other";
            /// <summary>
            /// ภาษีเงินได้
            /// </summary>
            public static String _tax_income = "tax_income";
            /// <summary>
            /// ภาษีที่ชำระแล้ว
            /// </summary>
            public static String _tax_already_pay = "tax_already_pay";
            /// <summary>
            /// มูลค่าภาษีสุทธิ
            /// </summary>
            public static String _net_tax = "net_tax";
            /// <summary>
            /// หมายเหตุ
            /// </summary>
            public static String _remark = "remark";
            /// <summary>
            /// จากค่าจ้าง
            /// </summary>
            public static String _from_salary = "from_salary";
            /// <summary>
            /// ถึงค่าจ้าง
            /// </summary>
            public static String _to_salary = "to_salary";
            /// <summary>
            /// ลดฯบิดามารดา
            /// </summary>
            public static String _reduce_parents = "reduce_parents";
            /// <summary>
            /// เบี้ยประกันบิดามารดา
            /// </summary>
            public static String _insurance_parents = "insurance_parents";
            /// <summary>
            /// งวดที่
            /// </summary>
            public static String _select_period = "select_period";
            /// <summary>
            /// งวดที่หนึ่ง
            /// </summary>
            public static String _period_one = "period_one";
            /// <summary>
            /// งวดที่สอง
            /// </summary>
            public static String _period_two = "period_two";
            /// <summary>
            /// งวดที่สาม
            /// </summary>
            public static String _period_three = "period_three";
            /// <summary>
            /// งวดที่สี่
            /// </summary>
            public static String _period_four = "period_four";
            /// <summary>
            /// ทุกงวด
            /// </summary>
            public static String _period_all = "period_all";
            /// <summary>
            /// ลดฯคู่สมรส
            /// </summary>
            public static String _reduce_spouse = "reduce_spouse";
            /// <summary>
            /// แสดงเป็นเดือน
            /// </summary>
            public static String _show_month = "show_month";
            /// <summary>
            /// แสดงเป็นงวด
            /// </summary>
            public static String _show_period = "show_period";
            /// <summary>
            /// Option
            /// </summary>
            public static String _option = "option";
        }

        /// <summary>
        /// เงินเดือน
        /// </summary>
        public class payroll_employee_salary
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "payroll_employee_salary";
            /// <summary>
            /// รหัสพนักงาน
            /// </summary>
            public static String _code = "code";
            /// <summary>
            /// ค่าจ้าง
            /// </summary>
            public static String _salary = "salary";
            /// <summary>
            /// วันที่เริ่มใช้งาน
            /// </summary>
            public static String _start_date = "start_date";
            /// <summary>
            /// วันที่สิ้นสุดการใช้งาน
            /// </summary>
            public static String _end_date = "end_date";
            /// <summary>
            /// สถานะ
            /// </summary>
            public static String _status = "status";
        }
    }
}
