using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AKZOBIReportCenter
{
    class Program
    {
        static void Main(string[] args)
        {
            // global config
            MyLib._myGlobal._webServiceName = "SMLJavaWebService";

            string _biServerCenterHost = "THMRTS001"; // "THMRTS001"; // "THMRTS001";
            string _biServerCenterUser = "sml"; // "sml"; //"sml";
            string _biSeverCenerPassword = "1"; // "1";
            string _biServerDatabaseName = "arrowicibireportcenter";

            string _biNewServerDatabaseName = "arrowicidtsdatacenter";
            string _biOldServerDatabaseName = "icibireportcenter";

            string _biCenterHost = "localhost:8080";
            string _biCenterConfig = "SMLConfigICI.xml"; // "SMLConfigICI.xml";
            MyLib._myGlobal._databaseType _biCenterType = MyLib._myGlobal._databaseType.MicrosoftSQL2005;
            string _biCenterDataBase = "icibireportcenter";


            // truncate target server
            string __connstring = "Server=" + _biServerCenterHost + ";Database=" + _biServerDatabaseName + ";uid=" + _biServerCenterUser + ";Password=" + _biSeverCenerPassword + ";Connect Timeout=3000";
            SqlConnection __connObject = new SqlConnection(__connstring);
            SqlConnection __conn = (SqlConnection)__connObject;
            __conn.Open();

            #region Copy From arrowicidtsdatacenter

            SqlCommand __command = new SqlCommand();
            __command.Connection = __conn;

            #region select Insert Ic Inventory

            __command.CommandText = "truncate table ic_inventory";
            __command.ExecuteNonQuery();

            string __inventoryFieldList = MyLib._myGlobal._fieldAndComma(
                "code",
                "code_old",
                "name_1",
                "name_2",
                "name_eng_1",
                "name_eng_2",
                "name_market",
                "name_for_bill",
                "short_name",
                "name_for_pos",
                "name_for_search",
                "item_type",
                "item_category",
                "group_main",
                "item_brand",
                "item_pattern",
                "item_design",
                "item_grade",
                "item_class",
                "item_size",
                "item_color",
                "item_character",
                "item_status",
                "unit_type",
                "cost_type",
                "tax_type",
                "item_sale_type",
                "item_rent_type",
                "unit_standard",
                "unit_cost",
                "onhand_less_zero",
                "income_type",
                "description",
                "item_model",
                "ic_serial_no",
                "remark",
                "status",
                "guid_code",
                "last_movement_date",
                "reserved_qty",
                "unreceived_qty",
                "remaining_delivered",
                "average_cost",
                "item_in_stock",
                "balance_qty",
                "accrued_in_qty",
                "accrued_out_qty",
                "unit_standard_name",
                "update_price",
                "update_detail",
                "ignore_sync",
                "account_code_1",
                "account_code_2",
                "account_code_3",
                "book_out_qty",
                "doc_format_code",
                "unit_standard_stand_value",
                "unit_standard_divide_value",
                "sign_code",
                "supplier_code",
                "fixed_cost",
                "drink_type",
                "average_cost_1",
                "group_sub",
                "use_expire"
                );

            __command.CommandText = "insert into ic_inventory (" + __inventoryFieldList + ") select " + __inventoryFieldList + " from " + _biNewServerDatabaseName + ".dbo.ic_inventory ";
            __command.ExecuteNonQuery();

            #endregion

            #region Select Insert unit_standard_lite

            __command.CommandText = "truncate table unit_standard_lite";
            __command.ExecuteNonQuery();

            __command.CommandText = "insert into unit_standard_lite (unit_cost,liter) select unit_cost,liter from " + _biNewServerDatabaseName + ".dbo.unit_standard_lite ";
            __command.ExecuteNonQuery();

            #endregion

            #region Select Insert ic_design

            __command.CommandText = "truncate table ic_design";
            __command.ExecuteNonQuery();

            __command.CommandText = "insert into ic_design (code,name_1, name_2, status) select code,name_1,name_2, status from " + _biNewServerDatabaseName + ".dbo.ic_design ";
            __command.ExecuteNonQuery();

            #endregion

            #region Select Insert ic_unit_use

            __command.CommandText = "truncate table ic_unit_use";
            __command.ExecuteNonQuery();

            __command.CommandText = "insert into ic_unit_use (code,line_number,stand_value,divide_value, ratio,ic_code, width_length_height,remark, status) select code,line_number,stand_value,divide_value, ratio,ic_code, width_length_height,remark, status from " + _biNewServerDatabaseName + ".dbo.ic_unit_use ";
            __command.ExecuteNonQuery();

            #endregion

            #region Select Insert ic_brand

            __command.CommandText = "truncate table ic_brand";
            __command.ExecuteNonQuery();

            __command.CommandText = "insert into ic_brand (code,name_1, name_2, status) select code,name_1,name_2, status from " + _biNewServerDatabaseName + ".dbo.ic_brand ";
            __command.ExecuteNonQuery();

            #endregion

            #region Select Insert ic_category

            __command.CommandText = "truncate table ic_category";
            __command.ExecuteNonQuery();

            __command.CommandText = "insert into ic_category (code, name_1, name_2, status) select code,name_1,name_2, status from " + _biNewServerDatabaseName + ".dbo.ic_category ";
            __command.ExecuteNonQuery();

            #endregion

            #region Select Insert Ar Customer

            __command.CommandText = "truncate table ar_customer";
            __command.ExecuteNonQuery();

            string __customerFieldList = MyLib._myGlobal._fieldAndComma(
                "code",
                "code_old",
                "name_1",
                "name_2",
                "name_eng_1",
                "name_eng_2",
                "address",
                "address_eng",
                "tambon",
                "amper",
                "province",
                "zip_code",
                "telephone",
                "fax",
                "email",
                "website",
                "description",
                "birth_day",
                "ar_type",
                "remark",
                "status",
                "guid_code",
                "ar_status",
                "guid",
                "agencode",
                "databasename",
                "datetimemanage",
                "is_lock_record",
                "ignore_sync",
                "doc_format_code",
                "prefixname",
                "first_name",
                "last_name",
                "price_level",
                "point_balance"
            );
            __command.CommandText = "insert into ar_customer (" + __customerFieldList + ") select " + __customerFieldList + " from " + _biNewServerDatabaseName + ".dbo.ar_customer ";
            __command.ExecuteNonQuery();

            #endregion

            #region select Insert ic trans
            //// start copy from new server

            __command.CommandText = "truncate table ic_trans";
            __command.ExecuteNonQuery();

            string __icTransFieldList = MyLib._myGlobal._fieldAndComma(
                "trans_type",
                "trans_flag",
                "doc_group",
                "doc_date",
                "doc_no",

                "doc_ref",
                "doc_ref_date",
                "tax_doc_no",
                "tax_doc_date",
                "inquiry_type",

                "vat_type",
                "cust_code",
                "contactor",
                "branch_code",
                "project_code",

                "side_code",
                "department_code",
                "sale_area_code",
                "sale_code",
                "send_type",

                "send_day",
                "send_date",
                "expire_day",
                "expire_date",
                "credit_day",

                "due_date",
                "answer_type",
                "transport_value",
                "discount_word",
                "vat_rate",

                "currency_code",
                "exchange_rate",
                "total_cost",
                "total_value",
                "total_discount",

                "total_vat_value",
                "total_after_vat",
                "total_except_vat",
                "total_amount",
                "balance_amount",

                "adjust_reason",
                "approve_code",
                "approve_date",
                "remark",
                "status",

                "guid_code",
                "user_request",
                "send_mail",
                "send_sms",
                "doc_ref_trans",

                "credit_date",
                "total_before_vat",
                "doc_time",
                "allocate_code",
                "job_code",

                "delivery_date",
                "pay_amount",
                "money",
                "sum_pay_money_diff",
                "currency_money",

                "total_debt_amount",
                "cancel_type",
                "last_status",
                "used_status",
                "doc_success",

                "total_manual",
                "on_hold",
                "extra_word",
                "transport_code",
                "want_day",

                "want_date",
                "deposit_day",
                "deposit_date",
                "sale_group",
                "not_approve_1",

                "user_approve",
                "approve_status",
                "expire_status",
                "advance_amount",
                "doc_format_code",

                "used_status_2",
                "remark_2",
                "remark_3",
                "ref_amount",
                "ref_new_amount",

                "ref_diff",
                "sum_point",
                "cashier_code",
                "is_pos",
                "pos_id",

                "member_code",
                "doc_no_guid",
                "pos_bill_type",
                "pos_bill_change",
                "doc_type",

                "auto_create",
                "agencode",
                "branch_sync"
                );

            __command.CommandText = "insert into ic_trans (" + __icTransFieldList + ") select " + __icTransFieldList + " from " + _biNewServerDatabaseName + ".dbo.ic_trans ";
            __command.ExecuteNonQuery();

            #endregion

            #region Select Insert ic trans detail

            __command.CommandText = "truncate table ic_trans_detail";
            __command.ExecuteNonQuery();

            string __transDetailFieldList = MyLib._myGlobal._fieldAndComma(
                "trans_type",
                "trans_flag",
                "doc_group",
                "doc_date",
                "doc_no",
                "doc_ref",
                "cust_code",
                "inquiry_type",
                "item_code",
                "item_name",
                "unit_code",
                "qty",
                "price",
                "discount",
                "sum_of_cost",
                "sum_amount",
                "due_date",
                "remark",
                "status",
                "line_number",
                "ref_doc_no",
                "ref_doc_date",
                "ref_line_number",
                "ref_cust_code",
                "branch_code",
                "wh_code",
                "shelf_code",
                "wh_code_2",
                "shelf_code_2",
                "department_code",
                "total_vat_value",
                "cancel_qty",
                "total_qty",
                "stand_value",
                "divide_value",
                "ratio",
                "ic_pattern",
                "ic_color",
                "ic_size",
                "priority_level",
                "calc_flag",
                "last_status",
                "set_ref_line",
                "set_ref_price",
                "set_ref_qty",
                "item_type",
                "vat_type",
                "doc_ref_type",
                "item_code_main",
                "ref_row",
                "ref_guid",
                "doc_time",
                "is_permium",
                "is_get_price",
                "average_cost",
                "sum_amount_exclude_vat",
                "doc_date_calc",
                "doc_time_calc",
                "discount_amount",
                "price_exclude_vat",
                "user_approve",
                "price_type",
                "price_mode",
                "temp_float_1",
                "temp_float_2",
                "temp_string_1",
                "is_serial_number",
                "bank_name",
                "bank_branch",
                "chq_number",
                "barcode",
                "discount_number",
                "price_changed",
                "discount_changed",
                "price_default",
                "tax_type",
                "is_pos",
                "auto_create",
                "date_expire",
                "hidden_cost_1",
                "hidden_cost_1_exclude_vat",
                "hidden_cost_2",
                "hidden_cost_2_exclude_vat",
                "sum_of_cost_1",
                "average_cost_1",
                "sale_code",
                "sale_group",
                "date_due",
                "lot_number_1",
                "agencode",
                "branch_sync"
                );

            __command.CommandText = "insert into ic_trans_detail (" + __transDetailFieldList + ") select " + __transDetailFieldList + " from " + _biNewServerDatabaseName + ".dbo.ic_trans_detail ";
            __command.ExecuteNonQuery();

            #endregion

            #endregion

            #region Copy From New icibireportcenter

            #region Import ic_trans Old Server

            {
                string __icTransOldServerFieldList = MyLib._myGlobal._fieldAndComma(
                    "trans_type",
                    "trans_flag",
                    "doc_group",
                    "doc_date",
                    "doc_no",

                    "doc_ref",
                    "doc_ref_date",
                    "tax_doc_no",
                    "tax_doc_date",
                    "inquiry_type",

                    "vat_type",
                    "cust_code",
                    "contactor",
                    "branch_code",
                    "project_code",

                    "side_code",
                    "department_code",
                    "sale_area_code",
                    "sale_code",
                    "send_type",

                    "send_day",
                    "send_date",
                    "expire_day",
                    "expire_date",
                    "credit_day",

                    "due_date",
                    "answer_type",
                    "transport_value",
                    "discount_word",
                    "vat_rate",

                    "currency_code",
                    "exchange_rate",
                    "total_cost",
                    "total_value",
                    "total_discount",

                    "total_vat_value",
                    "total_after_vat",
                    "total_except_vat",
                    "total_amount",
                    "balance_amount",

                    "adjust_reason",
                    "approve_code",
                    "approve_date",
                    "remark",
                    "status",

                    "guid_code",
                    "user_request",
                    "send_mail",
                    "send_sms",
                    "doc_ref_trans",

                    "credit_date",
                    "total_before_vat",
                    "doc_time",
                    "allocate_code",
                    "job_code",

                    "delivery_date",
                    "pay_amount",
                    "money",
                    "sum_pay_money_diff",
                    "currency_money",

                    "total_debt_amount",
                    "cancel_type",
                    "last_status",
                    "used_status",
                    "doc_success",

                    "total_manual",
                    "on_hold",
                    "extra_word",
                    "transport_code",
                    "want_day",

                    "want_date",
                    "deposit_day",
                    "deposit_date",
                    "sale_group",
                    "not_approve_1",

                    "user_approve",
                    "approve_status",
                    "expire_status",
                    "advance_amount",
                    "doc_format_code",

                    "used_status_2",
                    "remark_2",
                    "remark_3",
                    "ref_amount",
                    "ref_new_amount",

                    "ref_diff",
                    "sum_point",
                    "cashier_code",
                    "is_pos",
                    "pos_id",

                    "member_code",
                    "doc_no_guid",
                    "pos_bill_type",
                    "pos_bill_change",
                    "doc_type",

                    "auto_create",
                    "agencode",
                    "branch_sync"
                    );

                string __commandText = "insert into ic_trans (" + __icTransOldServerFieldList + ") select " + __icTransOldServerFieldList + " from " + _biOldServerDatabaseName + ".dbo.ic_trans ";
                __command.CommandText = __commandText;
                __command.ExecuteNonQuery();

                /*
                "where not exists (" +
                " select doc_no, trans_flag, branch_sync from ic_trans " + 
                " where " +
                " ic_trans.doc_no = icibireportcenter.dbo.ic_trans.doc_no " + 
                " and ic_trans.trans_flag= icibireportcenter.dbo.ic_trans.trans_flag " + 
                " and ic_trans.branch_sync = icibireportcenter.dbo.ic_trans.branch_sync " +
                ")";*/

                /*
            string __icTransOldServerFieldList = MyLib._myGlobal._fieldAndComma(
                "trans_type",
                "trans_flag",
                "doc_group",
                "doc_date",
                "doc_no",

                "doc_ref",
                "doc_ref_date",
                "tax_doc_no",
                "tax_doc_date",
                "inquiry_type",

                "vat_type",
                "cust_code",
                "contactor",
                "branch_code",
                "project_code",

                "side_code",
                "department_code",
                "sale_area_code",
                "sale_code",
                "send_type",

                "send_day",
                "send_date",
                "expire_day",
                "expire_date",
                "credit_day",

                "due_date",
                "answer_type",
                "transport_value",
                "discount_word",
                "vat_rate",

                "currency_code",
                "exchange_rate",
                "total_cost",
                "total_value",
                "total_discount",

                "total_vat_value",
                "total_after_vat",
                "total_except_vat",
                "total_amount",
                "balance_amount",

                "adjust_reason",
                "approve_code",
                "approve_date",
                "remark",
                "status",

                "guid_code",
                "user_request",
                "send_mail",
                "send_sms",
                "doc_ref_trans",

                "credit_date",
                "total_before_vat",
                "doc_time",
                "allocate_code",
                "job_code",

                "delivery_date",
                "pay_amount",
                "money",
                " 0 as sum_pay_money_diff",
                "currency_money",

                "total_debt_amount",
                "cancel_type",
                "last_status",
                "used_status",
                "doc_success",

                " 0 as total_manual",
                " 0 as on_hold",
                " \'\' as extra_word",
                " \'\' as transport_code",
                " 0 as want_day",

                " null as want_date",
                " 0 as deposit_day",
                " null as deposit_date",
                " '' as sale_group",
                " 0 as not_approve_1",

                " \'\' as user_approve",
                " 0 as approve_status",
                " 0 as expire_status",
                " 0 as advance_amount",
                " \'\' as doc_format_code",

                " 0 as used_status_2",
                " \'\' as remark_2",
                " \'\' as remark_3",
                " 0 as ref_amount",
                " 0 as ref_new_amount",

                " 0 as ref_diff",
                " 0 as sum_point",
                " \'\' as cashier_code",
                " 0 as is_pos",
                " \'\' as pos_id",

                " \'\' as member_code",
                " \'\' as doc_no_guid",
                " 0 as pos_bill_type",
                " 0 as pos_bill_change",
                " 0 as doc_type",

                " 0 as auto_create",
                "agencode"
                );
            */
            }

            #endregion

            #region Import ic_trans_detail Old Server
            {
                __transDetailFieldList = MyLib._myGlobal._fieldAndComma(
                "trans_type",
                "trans_flag",
                "doc_group",
                "doc_date",
                "doc_no",
                "doc_ref",
                "cust_code",
                "inquiry_type",
                "item_code",
                "item_name",
                "unit_code",
                "qty",
                "price",
                "discount",
                "sum_of_cost",
                "sum_amount",
                "due_date",
                "remark",
                "status",
                "line_number",
                "ref_doc_no",
                "ref_doc_date",
                "ref_line_number",
                "ref_cust_code",
                "branch_code",
                "wh_code",
                "shelf_code",
                "wh_code_2",
                "shelf_code_2",
                "department_code",
                "total_vat_value",
                "cancel_qty",
                "total_qty",
                "stand_value",
                "divide_value",
                "ratio",
                "ic_pattern",
                "ic_color",
                "ic_size",
                "priority_level",
                "calc_flag",
                "last_status",
                "set_ref_line",
                "set_ref_price",
                "set_ref_qty",
                "item_type",
                "vat_type",
                "doc_ref_type",
                "item_code_main",
                "ref_row",
                "ref_guid",
                "doc_time",
                "is_permium",
                "is_get_price",
                "average_cost",
                "sum_amount_exclude_vat",
                "doc_date_calc",
                "doc_time_calc",
                "discount_amount",
                "price_exclude_vat",
                "user_approve",
                "price_type",
                "price_mode",
                "temp_float_1",
                "temp_float_2",
                "temp_string_1",
                "is_serial_number",
                "bank_name",
                "bank_branch",
                "chq_number",
                "barcode",
                "discount_number",
                "price_changed",
                "discount_changed",
                "price_default",
                "tax_type",
                "is_pos",
                "auto_create",
                "date_expire",
                "hidden_cost_1",
                "hidden_cost_1_exclude_vat",
                "hidden_cost_2",
                "hidden_cost_2_exclude_vat",
                "sum_of_cost_1",
                "average_cost_1",
                "sale_code",
                "sale_group",
                "date_due",
                "lot_number_1",
                "agencode",
                "branch_sync"
                );

                string __commandText = "insert into ic_trans_detail (" + __transDetailFieldList + ") select " + __transDetailFieldList + " from " + _biOldServerDatabaseName + ".dbo.ic_trans_detail ";

                __command.CommandText = __commandText;
                __command.ExecuteNonQuery();

                /*
                string __transDetailOldServerFieldList = MyLib._myGlobal._fieldAndComma(
                    "trans_type",
                    "trans_flag",
                    "doc_group",
                    "doc_date",
                    "doc_no",
                    "doc_ref",
                    "cust_code",
                    "inquiry_type",
                    "coalesce((select ic_code_sap.sap from ic_code_sap where ic_code_sap.bpcs = ic_trans_detail.item_code), item_code) as item_code",
                    "item_name",
                    "unit_code",
                    "qty",
                    "price",
                    "discount",
                    "sum_of_cost",
                    "sum_amount",
                    "due_date",
                    "remark",
                    "status",
                    "line_number",
                    "ref_doc_no",
                    "ref_doc_date",
                    "ref_line_number",
                    "ref_cust_code",
                    "branch_code",
                    "wh_code",
                    "shelf_code",
                    "wh_code_2",
                    "shelf_code_2",
                    "department_code",
                    "total_vat_value",
                    "cancel_qty",
                    "total_qty",
                    "stand_value",
                    "divide_value",
                    "ratio",
                    "ic_pattern",
                    "ic_color",
                    "ic_size",
                    "priority_level",
                    "calc_flag",
                    "last_status",
                    "set_ref_line",
                    "set_ref_price",
                    "set_ref_qty",
                    "item_type",
                    "vat_type",
                    "doc_ref_type",
                    "item_code_main",
                    "ref_row",
                    "ref_guid",
                    "doc_time",
                    "is_permium",
                    "is_get_price",
                    "average_cost",
                    "sum_amount_exclude_vat",
                    "doc_date_calc",
                    "doc_time_calc",
                    "discount_amount",
                    "price_exclude_vat",
                    "user_approve",
                    "price_type",
                    "price_mode",
                    " 0 as temp_float_1",
                    " 0 as temp_float_2",
                    " \'\' as temp_string_1",
                    " 0 as is_serial_number",
                    " \'\' as bank_name",
                    " \'\' as bank_branch",
                    " \'\' as chq_number",
                    " \'\' as barcode",
                    " 0 as discount_number",
                    " 0 as price_changed",
                    " 0 as discount_changed",
                    " 0 as price_default",
                    " 0 as tax_type",
                    " 0 as is_pos",
                    " 0 as auto_create",
                    " null as date_expire",
                    " 0 as hidden_cost_1",
                    " 0 as hidden_cost_1_exclude_vat",
                    " 0 as hidden_cost_2",
                    " 0 as hidden_cost_2_exclude_vat",
                    " 0 as sum_of_cost_1",
                    " 0 as average_cost_1",
                    " \'\' as sale_code",
                    " \'\' as sale_group",
                    " null as date_due",
                    " \'\' as lot_number_1",
                    "agencode"
                    );

                // แผนสอง เอามาทีละ 5000
                DataTable __tableCount = __myFrameWork._query(_biOldServerDatabaseName, "select count(*) as xcount from ic_trans_detail").Tables[0];
                if (__tableCount.Rows.Count > 0 && decimal.Parse(__tableCount.Rows[0][0].ToString()) > 0)
                {
                    string __str = __tableCount.Rows[0][0].ToString();
                    int __maxRecord = int.Parse(__tableCount.Rows[0][0].ToString());
                    decimal __maxLoop = Math.Ceiling(decimal.Parse(__tableCount.Rows[0][0].ToString()) / 5000);

                    for (int __loop = 0; __loop < __maxLoop; __loop++)
                    {
                        int __offset = 0;
                        if (__loop > 0)
                        {
                            __offset = __loop * 5000;
                        }

                        DataSet __icTransDetailDataSet = __myFrameWork._queryLimit(_biOldServerDatabaseName, "select count(*) as xcount from ic_trans_detail", "select " + __transDetailOldServerFieldList + " from ic_trans_detail order by roworder ", __offset, 5000, __maxRecord).detail;
                        if (__icTransDetailDataSet != null && __icTransDetailDataSet.Tables.Count > 0)
                        {
                            DataTable __icTransDetailTable = __icTransDetailDataSet.Tables[0];
                            StringBuilder __queryList = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                            for (int __row = 0; __row < __icTransDetailTable.Rows.Count; __row++)
                            {
                                string __valueList = MyLib._myGlobal._fieldAndComma(
                                    "\'" + __icTransDetailTable.Rows[__row]["trans_type"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["trans_flag"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["doc_group"].ToString() + "\'",
                                    _getDateStr(__icTransDetailTable.Rows[__row]["doc_date"].ToString()),
                                    "\'" + __icTransDetailTable.Rows[__row]["doc_no"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["doc_ref"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["cust_code"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["inquiry_type"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["item_code"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["item_name"].ToString().Replace("\'", "\'\'") + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["unit_code"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["qty"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["price"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["discount"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["sum_of_cost"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["sum_amount"].ToString() + "\'",
                                    _getDateStr(__icTransDetailTable.Rows[__row]["due_date"].ToString()),
                                    "\'" + __icTransDetailTable.Rows[__row]["remark"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["status"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["line_number"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["ref_doc_no"].ToString() + "\'",
                                    _getDateStr(__icTransDetailTable.Rows[__row]["ref_doc_date"].ToString()),
                                    "\'" + __icTransDetailTable.Rows[__row]["ref_line_number"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["ref_cust_code"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["branch_code"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["wh_code"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["shelf_code"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["wh_code_2"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["shelf_code_2"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["department_code"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["total_vat_value"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["cancel_qty"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["total_qty"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["stand_value"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["divide_value"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["ratio"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["ic_pattern"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["ic_color"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["ic_size"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["priority_level"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["calc_flag"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["last_status"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["set_ref_line"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["set_ref_price"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["set_ref_qty"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["item_type"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["vat_type"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["doc_ref_type"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["item_code_main"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["ref_row"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["ref_guid"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["doc_time"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["is_permium"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["is_get_price"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["average_cost"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["sum_amount_exclude_vat"].ToString() + "\'",
                                    _getDateStr(__icTransDetailTable.Rows[__row]["doc_date_calc"].ToString()),
                                    "\'" + __icTransDetailTable.Rows[__row]["doc_time_calc"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["discount_amount"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["price_exclude_vat"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["user_approve"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["price_type"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["price_mode"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["temp_float_1"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["temp_float_2"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["temp_string_1"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["is_serial_number"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["bank_name"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["bank_branch"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["chq_number"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["barcode"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["discount_number"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["price_changed"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["discount_changed"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["price_default"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["tax_type"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["is_pos"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["auto_create"].ToString() + "\'",
                                    _getDateStr(__icTransDetailTable.Rows[__row]["date_expire"].ToString()),
                                    "\'" + __icTransDetailTable.Rows[__row]["hidden_cost_1"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["hidden_cost_1_exclude_vat"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["hidden_cost_2"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["hidden_cost_2_exclude_vat"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["sum_of_cost_1"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["average_cost_1"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["sale_code"].ToString() + "\'",
                                    "\'" + __icTransDetailTable.Rows[__row]["sale_group"].ToString() + "\'",
                                    _getDateStr(__icTransDetailTable.Rows[__row]["date_due"].ToString()),
                                    "\'" + __icTransDetailTable.Rows[__row]["lot_number_1"].ToString() + "\'",
                                    "\'" + convertAgenCode(__icTransDetailTable.Rows[__row]["agencode"].ToString()) + "\'"
                                );

                                string __query = "insert into ic_trans_detail (" + __transDetailFieldList + ") values (" + __valueList + ")";

                                __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));


                            }
                            __queryList.Append("</node>");
                            string __debug_query = __queryList.ToString();
                            string __result = __biCenterFrameWork._queryList(_biCenterDataBase, __queryList.ToString());
                            if (__result.Length > 0)
                            {
                                Console.WriteLine(__result.ToString());
                            }

                            __icTransDetailTable.Dispose();

                        }
                        __icTransDetailDataSet.Dispose();
                    }

                }*/

            }
            #endregion

            #endregion
            __conn.Close();
            __conn.Dispose();
        }

        public static string _getDateStr(string strDate)
        {
            if (strDate.Length == 0)
                return "null";

            return "\'" + strDate + "\'";
        }

        public static string convertAgenCode(string agenCode)
        {
            if (agenCode.Length > 0)
            {
                string __zapId = agenCode.Substring(0, 6);
                string __agenId = agenCode.Substring(7, 7);

                agenCode = string.Format("{0}({1})", __agenId.ToLower().Replace("dcdc00", "dcdc000"), __zapId);
            }
            return agenCode;
        }

    }
}
