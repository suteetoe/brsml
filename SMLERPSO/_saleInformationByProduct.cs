﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPSO
{
    public partial class _saleInformationByProduct : UserControl
    {
        DataTable _arDataTable = null;

        public _saleInformationByProduct()
        {
            InitializeComponent();

            this._build();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._loadData();
            }
        }

        void _build()
        {
            string __amountFormatStr = _g.g._getFormatNumberStr(3);
            string __qtyFormatStr = _g.g._getFormatNumberStr(1);

            this._icmainScreenTopControl1.Enabled = false;

            this._icGrid._table_name = _g.d.ic_inventory._table;
            this._icGrid._isEdit = false;
            this._icGrid._addColumn(_g.d.ic_inventory._code, 1, 30, 30);
            this._icGrid._addColumn(_g.d.ic_inventory._name_1, 1, 70, 70);
            this._icGrid._calcPersentWidthToScatter();
            this._icGrid._mouseClick += _icGrid_MouseClick;

            this._arGrid._table_name = _g.d.ar_customer._table;
            this._arGrid._isEdit = false;
            this._arGrid._addColumn(_g.d.ar_customer._code, 1, 30, 30);
            this._arGrid._addColumn(_g.d.ar_customer._name_1, 1, 70, 70);
            this._arGrid._calcPersentWidthToScatter();
            this._arGrid._mouseClick += _arGrid__mouseClick;

            this._docGrid._table_name = _g.d.ic_resource._table;
            this._docGrid._isEdit = false;
            this._docGrid._width_by_persent = true;
            this._docGrid._addColumn(_g.d.ic_resource._doc_date, 4, 20, 20);
            this._docGrid._addColumn(_g.d.ic_resource._doc_no, 1, 20, 20);
            this._docGrid._addColumn(_g.d.ic_resource._qty, 3, 15, 15, false, false, true, false, __qtyFormatStr);
            this._docGrid._addColumn(_g.d.ic_resource._price, 3, 15, 15, false, false, true, false, __amountFormatStr);
            this._docGrid._addColumn(_g.d.ic_resource._unit_code, 1, 15, 15);
            this._docGrid._addColumn(_g.d.ic_resource._discount, 1, 15, 15);
            this._docGrid._addColumn(_g.d.ic_resource._sale_detail, 1, 20, 20);
            this._docGrid._addColumn(_g.d.ic_resource._trans_discount, 1, 15, 15);
            this._docGrid._addColumn(_g.d.ic_trans._trans_flag, 2, 15, 15, false, true);
            this._docGrid._calcPersentWidthToScatter();

            this._saleGrid._table_name = _g.d.resource_report._table;
            this._saleGrid._isEdit = false;
            this._saleGrid._width_by_persent = true;
            this._saleGrid._addColumn(_g.d.resource_report._year, 1, 10, 10);
            this._saleGrid._addColumn(_g.d.resource_report._category_sale, 1, 10, 10);
            this._saleGrid._addColumn(_g.d.resource_report._month_jan, 3, 10, 10, false, false, true, false, __amountFormatStr);
            this._saleGrid._addColumn(_g.d.resource_report._month_feb, 3, 10, 10, false, false, true, false, __amountFormatStr);
            this._saleGrid._addColumn(_g.d.resource_report._month_mar, 3, 10, 10, false, false, true, false, __amountFormatStr);
            this._saleGrid._addColumn(_g.d.resource_report._month_apr, 3, 10, 10, false, false, true, false, __amountFormatStr);
            this._saleGrid._addColumn(_g.d.resource_report._month_may, 3, 10, 10, false, false, true, false, __amountFormatStr);
            this._saleGrid._addColumn(_g.d.resource_report._month_jun, 3, 10, 10, false, false, true, false, __amountFormatStr);
            this._saleGrid._addColumn(_g.d.resource_report._month_jul, 3, 10, 10, false, false, true, false, __amountFormatStr);
            this._saleGrid._addColumn(_g.d.resource_report._month_aug, 3, 10, 10, false, false, true, false, __amountFormatStr);
            this._saleGrid._addColumn(_g.d.resource_report._month_sep, 3, 10, 10, false, false, true, false, __amountFormatStr);
            this._saleGrid._addColumn(_g.d.resource_report._month_oct, 3, 10, 10, false, false, true, false, __amountFormatStr);
            this._saleGrid._addColumn(_g.d.resource_report._month_nov, 3, 10, 10, false, false, true, false, __amountFormatStr);
            this._saleGrid._addColumn(_g.d.resource_report._month_dec, 3, 10, 10, false, false, true, false, __amountFormatStr);
            this._saleGrid._addColumn(_g.d.resource_report._total, 3, 10, 10, false, false, true, false, __amountFormatStr);
            this._saleGrid._calcPersentWidthToScatter();
        }

        private void _arGrid__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            // select ar
            string __getARCode = this._arGrid._cellGet(e._row, _g.d.ar_customer._code).ToString();  //this._arGrid._cellGet(this._arGrid._selectRow, _g.d.ar_customer._code).ToString();
            string __getICCode = this._icmainScreenTopControl1._getDataStr(_g.d.ic_inventory._code);

            /*
            // load sale information
            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(
                "select doc_no, doc_date, sum(qty) as qty, unit_code, price, discount, sum(sum_amount) as sum_amount, (sale_code || '~' || (select name_1 from erp_user where erp_user.code = temp1.sale_code ) ) as sale_detail, (select discount_word from ic_trans where ic_trans.doc_no= temp1.doc_no and ic_trans.trans_flag = temp1.trans_flag) as trans_discount, trans_flag " +

                " from ( " +
                " select doc_no, doc_date, doc_date_calc, doc_time_calc, sale_code, trans_flag, item_code, unit_code, coalesce(discount, '0') as discount, price, case when trans_flag = 48 then -1*qty else qty end as qty, case when trans_flag = 48 then -1*sum_amount else sum_amount end as sum_amount  from ic_trans_detail where cust_code = '" + __getARCode + "' and trans_flag in (44, 46, 48) and item_code = \'" + __getICCode + "\' and last_status = 0  " +
                ") as temp1 " +
                " group by doc_no, doc_date, doc_date_calc, doc_time_calc, sale_code, trans_flag, item_code, unit_code, discount, price order by doc_date_calc desc, doc_time_calc desc "
                ));

            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from ar_customer where code = \'" + __getARCode + "\'"));

            __query.Append("</node>");
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

            DataTable __getDataICTrans = ((DataSet)__result[0]).Tables[0];
            DataTable __arDetail = ((DataSet)__result[1]).Tables[0];

            this._docGrid._loadFromDataTable(__getDataICTrans);
            this._addressTextbox.Text = "ที่อยู่ :" + __arDetail.Rows[0][_g.d.ar_customer._address].ToString() + "\r\nโทรศัทพ์ : " + __arDetail.Rows[0][_g.d.ar_customer._telephone].ToString();
            */
            try
            {
                // string __getARCode = this._arGrid._cellGet(e._row, _g.d.ar_customer._code).ToString();

                // load sale information
                StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");


                // sale year
                string __queryGetSaleAmount = " select cust_code, doc_no, trans_flag, ( case when trans_flag in (48,103) then -1*sum_amount else sum_amount end ) as amount , extract(month from doc_date ) as month_sum, extract(year from doc_date ) as year_sum from ic_trans_detail where trans_flag in (44,46,48,99,101,103) and item_code = \'" + __getICCode + "\' {0} ";
                string __queryGetSaleExtractMonth = "select cust_code,(case when month_sum =1 then amount else 0 end) as month_1,(case when month_sum =2 then amount else 0 end) as month_2,(case when month_sum =3 then amount else 0 end) as month_3,(case when month_sum =4 then amount else 0 end) as month_4,(case when month_sum =5 then amount else 0 end) as month_5,(case when month_sum =6 then amount else 0 end) as month_6,(case when month_sum =7 then amount else 0 end) as month_7,(case when month_sum =8 then amount else 0 end) as month_8,(case when month_sum =9 then amount else 0 end) as month_9,(case when month_sum =10 then amount else 0 end) as month_10,(case when month_sum =11 then amount else 0 end) as month_11,(case when month_sum =12 then amount else 0 end) as month_12 , year_sum from ("
                    + __queryGetSaleAmount + ") as temp1 ";

                string __queryGetSumMonth = " select sum(month_1) as month_1, sum(month_2) as month_2, sum(month_3) as month_3, sum(month_4) as month_4, sum(month_5) as month_5, sum(month_6) as month_6, sum(month_7) as month_7, sum(month_8) as month_8, sum(month_9) as month_9, sum(month_10) as month_10, sum(month_11) as month_11, sum(month_12) as month_12, year_sum from ( "
                    + __queryGetSaleExtractMonth + " ) as temp2 {1} group by year_sum ";

                string __getSaleYer = " select 'Cust' as  " + _g.d.resource_report._category_sale
                    + ", month_1 as " + _g.d.resource_report._month_jan
                    + ", month_2 as " + _g.d.resource_report._month_feb
                    + ", month_3 as " + _g.d.resource_report._month_mar
                    + ", month_4 as " + _g.d.resource_report._month_apr
                    + ", month_5 as " + _g.d.resource_report._month_may
                    + ", month_6 as " + _g.d.resource_report._month_jun
                    + ", month_7 as " + _g.d.resource_report._month_jul
                    + ", month_8 as " + _g.d.resource_report._month_aug
                    + ", month_9 as " + _g.d.resource_report._month_sep
                    + ", month_10 as " + _g.d.resource_report._month_oct
                    + ", month_11 as " + _g.d.resource_report._month_nov
                    + ", month_12 as " + _g.d.resource_report._month_dec
                    + ", (month_1 + month_2 + month_3 + month_4 + month_5 + month_6 + month_7 + month_8 + month_9 + month_10 + month_11 + month_12 ) " + _g.d.resource_report._total
                    + ", year_sum as " + _g.d.resource_report._year + " from ( " + string.Format(__queryGetSumMonth, " and cust_code = \'" + __getARCode + "\' ", " where cust_code = \'" + __getARCode + "\' ") + " ) as temp3  ";

                string __getSaleAll = " select 'All' as  " + _g.d.resource_report._category_sale
                    + ", month_1 as " + _g.d.resource_report._month_jan
                    + ", month_2 as " + _g.d.resource_report._month_feb
                    + ", month_3 as " + _g.d.resource_report._month_mar
                    + ", month_4 as " + _g.d.resource_report._month_apr
                    + ", month_5 as " + _g.d.resource_report._month_may
                    + ", month_6 as " + _g.d.resource_report._month_jun
                    + ", month_7 as " + _g.d.resource_report._month_jul
                    + ", month_8 as " + _g.d.resource_report._month_aug
                    + ", month_9 as " + _g.d.resource_report._month_sep
                    + ", month_10 as " + _g.d.resource_report._month_oct
                    + ", month_11 as " + _g.d.resource_report._month_nov
                    + ", month_12 as " + _g.d.resource_report._month_dec
                    + ", (month_1 + month_2 + month_3 + month_4 + month_5 + month_6 + month_7 + month_8 + month_9 + month_10 + month_11 + month_12 ) " + _g.d.resource_report._total
                    + ", year_sum as " + _g.d.resource_report._year + " from ( " + string.Format(__queryGetSumMonth, "", "") + " ) as temp3 ";

                string __allSaleQuery = "select * from ( " + __getSaleYer + " union all " + __getSaleAll + " ) as temp4 order by year, category_sale desc ";

                __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__allSaleQuery));

                __query.Append(MyLib._myUtil._convertTextToXmlForQuery(
    "select doc_no, doc_date, sum(qty) as qty, unit_code, price, discount, sum(sum_amount) as sum_amount, (sale_code || '~' || (select name_1 from erp_user where erp_user.code = temp1.sale_code ) ) as sale_detail, (select discount_word from ic_trans where ic_trans.doc_no= temp1.doc_no and ic_trans.trans_flag = temp1.trans_flag) as trans_discount, trans_flag " +

    " from ( " +
    " select doc_no, doc_date, doc_date_calc, doc_time_calc, sale_code, trans_flag, item_code, unit_code, coalesce(discount, '0') as discount, price, case when trans_flag = 48 then -1*qty else qty end as qty, case when trans_flag = 48 then -1*sum_amount else sum_amount end as sum_amount  from ic_trans_detail where cust_code = '" + __getARCode + "' and trans_flag in (44, 46, 48) and item_code = \'" + __getICCode + "\' and last_status = 0  " +
    ") as temp1 " +
    " group by doc_no, doc_date, doc_date_calc, doc_time_calc, sale_code, trans_flag, item_code, unit_code, discount, price order by doc_date_calc desc, doc_time_calc desc "
    ));

                // ar detail
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.ar_customer._code, _g.d.ar_customer._name_1, _g.d.ar_customer._address, _g.d.ar_customer._telephone, _g.d.ar_customer._remark) + " from ar_customer where ar_customer.code = \'" + __getARCode + "\'"));

                // ar status
                /*
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select ar_code" +
                    ", sum(case when(due_date < now()) then balance_amount else 0 end ) as over_due " +
                    ", sum(case when(due_date < now()) then 1 else 0 end ) as overdue_count" +
                    ",sum(case when(due_date >= now()) then balance_amount else 0 end ) as indue " +
                    ",sum(case when(due_date >= now()) then 1 else 0 end ) as indue_count " +
                    ",sum(balance_amount) as ar_balance from sml_ar_balance_doc(null, null, \'" + __getARCode + "\') as temp1 group by ar_code "));

                // chq
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select ap_ar_code" +
                    ", sum(case when status = 0 then amount else 0 end) as onhand_amount" +
                    ", sum(case when status = 0 then 1 else 0 end) as onhand_count" +
                    ", sum(case when status = 3 then amount else 0 end) as return_amount" +
                    ", sum(case when status = 3 then 1 else 0 end) as return_count from cb_chq_list where chq_type = 1 and ap_ar_code = \'" + __getARCode + "\' group by ap_ar_code "));
                    */

                __query.Append("</node>");
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());


                DataTable __getDataSale = ((DataSet)__result[0]).Tables[0];
                DataTable __docGrid = ((DataSet)__result[1]).Tables[0];
                DataTable __getARTable = ((DataSet)__result[2]).Tables[0];

                //this._gridIC._loadFromDataTable(_tableIC);
                this._docGrid._loadFromDataTable(__docGrid);
                this._saleGrid._loadFromDataTable(__getDataSale);

                StringBuilder __arDetaiLStr = new StringBuilder();

                __arDetaiLStr.Append("ที่อยู่" + " : " + __getARTable.Rows[0][_g.d.ar_customer._address].ToString() + "\r\n");
                __arDetaiLStr.Append("โทรศัพท์" + " : " + __getARTable.Rows[0][_g.d.ar_customer._telephone].ToString() + "\r\n");
                if (__getARTable.Rows[0][_g.d.ar_customer._remark].ToString().Length > 0)
                {
                    __arDetaiLStr.Append("หมายเหตุ" + " : " + __getARTable.Rows[0][_g.d.ar_customer._remark].ToString() + "\r\n");

                }

                this._addressTextbox.Text = __arDetaiLStr.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void _icGrid_MouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            // click ic grid load detail
            string __getItemCode = this._icGrid._cellGet(e._row, _g.d.ic_inventory._code).ToString();

            StringBuilder __queryList = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

            // ic_inventory
            __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("select  * from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + __getItemCode + "\'"));

            // ar list 
            __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("select code, name_1 from ar_customer where code in (select distinct cust_code from ic_trans_detail where item_code = '" + __getItemCode + "' and trans_flag in (44, 46, 48))"));

            __queryList.Append("</node>");

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            this._arDataTable = null;
            ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryList.ToString());
            if (__result.Count > 0)
            {
                this._icmainScreenTopControl1._loadData(((DataSet)__result[0]).Tables[0]);
                this._arDataTable = ((DataSet)__result[1]).Tables[0];
                this._arGrid._loadFromDataTable(_arDataTable);
            }
        }

        void _loadData()
        {
            string __searchTextTrim = this._searchTextbox.Text.Trim();
            string[] __searchTextSplit = __searchTextTrim.Split(' ');

            // ประกอบ where
            StringBuilder __where = new StringBuilder();
            if (__searchTextSplit.Length > 1)
            {
                // กรณี ป้อนการค้นหาหลายคำ (เว้น spacebar)
                for (int __loop = 0; __loop < this._arGrid._columnList.Count; __loop++)
                {
                    bool __whereFirst = false;
                    MyLib._myGrid._columnType __getColumnType = (MyLib._myGrid._columnType)this._arGrid._columnList[__loop];
                    if (__getColumnType._originalName.Length > 0 && __getColumnType._isQuery == true && __getColumnType._isColumnFilter == true)
                    {
                        if (__getColumnType._isHide == false)
                        {
                            // กรณี ป้อนการค้นหาหลายคำ (เว้น spacebar)
                            bool __first2 = false;
                            for (int __searchIndex = 0; __searchIndex < __searchTextSplit.Length; __searchIndex++)
                            {
                                if (__searchTextSplit[__searchIndex].Length > 0)
                                {
                                    string __getValue = __searchTextSplit[__searchIndex].ToUpper();
                                    string __newDateValue = __getValue;
                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                    {
                                        __newDateValue = __getValue.ToString().Remove(0, 1);
                                    }
                                    switch (__getColumnType._type)
                                    {
                                        case 2: // Number
                                        case 3:
                                        case 5:
                                            //
                                            decimal __newValue = 0M;
                                            try
                                            {
                                                if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                {
                                                    __newValue = MyLib._myGlobal._decimalPhase(__newDateValue);
                                                    //
                                                    if (__newValue != 0)
                                                    {
                                                        if (__whereFirst == false)
                                                        {
                                                            if (__where.Length > 0)
                                                            {
                                                                __where.Append(" or ");
                                                            }
                                                            __where.Append("(");
                                                            __whereFirst = true;
                                                        }
                                                        if (__first2)
                                                        {
                                                            __where.Append(" and ");
                                                        }
                                                        __first2 = true;
                                                        //
                                                        //if (this._addQuotWhere)
                                                        //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                        //else
                                                        __where.Append(string.Concat(__getColumnType._query));
                                                        //
                                                        if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                            __getValue = __getValue[0].ToString() + __newValue.ToString();
                                                        else
                                                            __getValue = String.Concat("=", __newValue.ToString());
                                                        __where.Append(__getValue);
                                                    }
                                                }
                                            }
                                            catch
                                            {
                                            }
                                            break;
                                        case 4: // Date
                                            try
                                            {
                                                if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                {
                                                    DateTime __test = DateTime.Parse(__newDateValue, MyLib._myGlobal._cultureInfo());
                                                    //
                                                    if (__whereFirst == false)
                                                    {
                                                        if (__where.Length > 0)
                                                        {
                                                            __where.Append(" or ");
                                                        }
                                                        __where.Append("(");
                                                        __whereFirst = true;
                                                    }
                                                    if (__first2)
                                                    {
                                                        __where.Append(" and ");
                                                    }
                                                    __first2 = true;
                                                    //
                                                    //if (this._addQuotWhere)
                                                    //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                    //else
                                                    __where.Append(string.Concat(__getColumnType._query));
                                                    DateTime __newDate = MyLib._myGlobal._convertDate(__getValue);
                                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                    {
                                                        __newDate = MyLib._myGlobal._convertDate(__newDateValue);
                                                        __where.Append(string.Concat(__getValue[0].ToString(), "\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                    }
                                                    else
                                                    {
                                                        __where.Append(string.Concat("=\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                    }
                                                }
                                            }
                                            catch
                                            {
                                            }
                                            break;
                                        case 11:
                                            {

                                            }
                                            break;
                                        default:// String
                                            if (__whereFirst == false)
                                            {
                                                if (__where.Length > 0)
                                                {
                                                    __where.Append(" or ");
                                                }
                                                __where.Append("(");
                                                __whereFirst = true;
                                            }
                                            if (__first2)
                                            {
                                                __where.Append(" and ");
                                            }
                                            __first2 = true;
                                            //
                                            //if (this._addQuotWhere)
                                            //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                            //else
                                            __where.Append(string.Concat("upper(" + __getColumnType._query + ")"));
                                            if (this._searchTextbox.Text[0] == '+')
                                            {
                                                __where.Append(string.Concat(" like \'", __newDateValue.Remove(0, 1).ToUpper(), "%\'"));
                                            }
                                            else
                                            {
                                                __where.Append(string.Concat(" like \'%", __newDateValue.ToUpper(), "%\'"));
                                            }
                                            break;
                                    }
                                }
                            }
                            if (__whereFirst)
                            {
                                __where.Append(")");
                            }
                        }
                    }
                } // for
            }
            else
            {
                bool __whereFirst = false;
                for (int __loop = 0; __loop < this._arGrid._columnList.Count; __loop++)
                {
                    MyLib._myGrid._columnType __getColumnType = (MyLib._myGrid._columnType)this._arGrid._columnList[__loop];
                    if (__getColumnType._originalName.Length > 0 && __getColumnType._isQuery == true && __getColumnType._isColumnFilter == true)
                    {
                        if (__getColumnType._isHide == false)
                        {
                            // กรณีการค้นหาตัวเดียว
                            if (this._searchTextbox.Text.Length > 0)
                            {
                                try
                                {
                                    string __getValue = this._searchTextbox.Text;
                                    string __newDateValue = __getValue;
                                    Boolean __valueExtra = false;
                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                    {
                                        __newDateValue = __getValue.ToString().Remove(0, 1);
                                        __valueExtra = true;
                                    }
                                    switch (__getColumnType._type)
                                    {
                                        case 2: // Number
                                        case 3:
                                        case 5:
                                            double __newValue = 0;
                                            try
                                            {
                                                if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                {
                                                    __newValue = Double.Parse(__newDateValue);
                                                    //
                                                    if (__newValue != 0)
                                                    {
                                                        if (__whereFirst == true)
                                                        {
                                                            __where.Append(" or ");
                                                        }
                                                        __whereFirst = true;
                                                        //
                                                        //if (this._addQuotWhere)
                                                        //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                        //else
                                                        __where.Append(string.Concat(__getColumnType._query));
                                                        //
                                                        if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                            __getValue = __getValue[0].ToString() + __newValue.ToString();
                                                        else
                                                            __getValue = String.Concat("=", __newValue.ToString());
                                                        __where.Append(__getValue);
                                                    }
                                                }
                                            }
                                            catch
                                            {
                                            }
                                            break;
                                        case 4: // Date
                                            try
                                            {
                                                if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                {
                                                    DateTime __test = DateTime.Parse(__newDateValue, MyLib._myGlobal._cultureInfo());
                                                    //
                                                    if (__whereFirst == true)
                                                    {
                                                        __where.Append(" or ");
                                                    }
                                                    __whereFirst = true;
                                                    //
                                                    //if (this._addQuotWhere)
                                                    //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                    //else
                                                    __where.Append(string.Concat(__getColumnType._query));
                                                    DateTime __newDate = MyLib._myGlobal._convertDate(__getValue);
                                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                    {
                                                        __newDate = MyLib._myGlobal._convertDate(__newDateValue);
                                                        __where.Append(string.Concat(__getValue[0].ToString(), "\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                    }
                                                    else
                                                    {
                                                        __where.Append(string.Concat("=\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                    }
                                                }
                                            }
                                            catch
                                            {
                                            }
                                            break;
                                        case 11:
                                            {

                                            }
                                            break;
                                        default:// String
                                            //
                                            if (__valueExtra == false)
                                            {
                                                if (__whereFirst == true)
                                                {
                                                    __where.Append(" or ");
                                                }
                                                __whereFirst = true;
                                                //
                                                //if (this._addQuotWhere)
                                                //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                //else
                                                __where.Append(string.Concat("upper(" + __getColumnType._query + ")"));
                                                if (this._searchTextbox.Text[0] == '+')
                                                {
                                                    __where.Append(string.Concat(" like \'", __newDateValue.Remove(0, 1).ToUpper(), "%\'"));
                                                }
                                                else
                                                {
                                                    __where.Append(string.Concat(" like \'%", __newDateValue.ToUpper(), "%\'"));
                                                }
                                            }
                                            break;
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                } // for
            }
            if (__where.Length > 0)
            {
                __where = new StringBuilder("(" + __where.ToString() + ")");
            }

            if (__where.Length > 0)
            {
                if (this._extraWhere2.Length > 0)
                {
                    __where.Append(" and (" + this._extraWhere2 + ")");
                }
            }
            else
            {
                if (this._extraWhere2.Length > 0)
                {
                    __where.Append("(" + this._extraWhere2 + ")");
                }
            }

            // load item
            string __query = "select code, name_1 from " + _g.d.ic_inventory._table + ((__where.Length > 0) ? " where " + __where : "") + " order by code";
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
            this._icGrid._loadFromDataTable(__result.Tables[0]);
        }
        string _extraWhere2 = "";


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;

            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                this._timer.Stop();
                this._timer.Start();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        string _oldText = "";
        private void _timer_Tick(object sender, EventArgs e)
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                if (_searchAuto.Checked)
                {
                    if (_oldText.CompareTo(this._searchTextbox.Text) != 0)
                    {
                        _oldText = this._searchTextbox.Text;
                        this._loadData();
                    }
                }
            }
        }

        private void _searchArTextbox_TextChanged(object sender, EventArgs e)
        {

            if (this._arDataTable != null)
            {
                StringBuilder __where = new StringBuilder();
                // split value
                string __searchTextTrim = this._searchArTextbox.Text.Trim();
                string[] __searchTextSplit = __searchTextTrim.Split(' ');
                if (__searchTextSplit.Length > 1)
                {
                    // กรณี ป้อนการค้นหาหลายคำ (เว้น spacebar)
                    for (int __loop = 0; __loop < this._arGrid._columnList.Count; __loop++)
                    {
                        bool __whereFirst = false;
                        MyLib._myGrid._columnType __getColumnType = (MyLib._myGrid._columnType)this._arGrid._columnList[__loop];
                        if (__getColumnType._originalName.Length > 0 && __getColumnType._isQuery == true && __getColumnType._isColumnFilter == true)
                        {
                            if (__getColumnType._isHide == false)
                            {
                                // กรณี ป้อนการค้นหาหลายคำ (เว้น spacebar)
                                bool __first2 = false;
                                for (int __searchIndex = 0; __searchIndex < __searchTextSplit.Length; __searchIndex++)
                                {
                                    if (__searchTextSplit[__searchIndex].Length > 0)
                                    {
                                        string __getValue = __searchTextSplit[__searchIndex].ToUpper();
                                        string __newDateValue = __getValue;
                                        if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                        {
                                            __newDateValue = __getValue.ToString().Remove(0, 1);
                                        }
                                        switch (__getColumnType._type)
                                        {
                                            case 2: // Number
                                            case 3:
                                            case 5:
                                                //
                                                decimal __newValue = 0M;
                                                try
                                                {
                                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                    {
                                                        __newValue = MyLib._myGlobal._decimalPhase(__newDateValue);
                                                        //
                                                        if (__newValue != 0)
                                                        {
                                                            if (__whereFirst == false)
                                                            {
                                                                if (__where.Length > 0)
                                                                {
                                                                    __where.Append(" or ");
                                                                }
                                                                __where.Append("(");
                                                                __whereFirst = true;
                                                            }
                                                            if (__first2)
                                                            {
                                                                __where.Append(" and ");
                                                            }
                                                            __first2 = true;
                                                            //
                                                            //if (this._addQuotWhere)
                                                            //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                            //else
                                                            __where.Append(string.Concat(__getColumnType._query));
                                                            //
                                                            if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                                __getValue = __getValue[0].ToString() + __newValue.ToString();
                                                            else
                                                                __getValue = String.Concat("=", __newValue.ToString());
                                                            __where.Append(__getValue);
                                                        }
                                                    }
                                                }
                                                catch
                                                {
                                                }
                                                break;
                                            case 4: // Date
                                                try
                                                {
                                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                    {
                                                        DateTime __test = DateTime.Parse(__newDateValue, MyLib._myGlobal._cultureInfo());
                                                        //
                                                        if (__whereFirst == false)
                                                        {
                                                            if (__where.Length > 0)
                                                            {
                                                                __where.Append(" or ");
                                                            }
                                                            __where.Append("(");
                                                            __whereFirst = true;
                                                        }
                                                        if (__first2)
                                                        {
                                                            __where.Append(" and ");
                                                        }
                                                        __first2 = true;
                                                        //
                                                        //if (this._addQuotWhere)
                                                        //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                        //else
                                                        __where.Append(string.Concat(__getColumnType._query));
                                                        DateTime __newDate = MyLib._myGlobal._convertDate(__getValue);
                                                        if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                        {
                                                            __newDate = MyLib._myGlobal._convertDate(__newDateValue);
                                                            __where.Append(string.Concat(__getValue[0].ToString(), "\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                        }
                                                        else
                                                        {
                                                            __where.Append(string.Concat("=\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                        }
                                                    }
                                                }
                                                catch
                                                {
                                                }
                                                break;
                                            case 11:
                                                {

                                                }
                                                break;
                                            default:// String
                                                if (__whereFirst == false)
                                                {
                                                    if (__where.Length > 0)
                                                    {
                                                        __where.Append(" or ");
                                                    }
                                                    __where.Append("(");
                                                    __whereFirst = true;
                                                }
                                                if (__first2)
                                                {
                                                    __where.Append(" and ");
                                                }
                                                __first2 = true;
                                                //
                                                //if (this._addQuotWhere)
                                                //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                //else
                                                __where.Append(string.Concat("" + __getColumnType._query + ""));
                                                if (this._searchArTextbox.Text[0] == '+')
                                                {
                                                    __where.Append(string.Concat(" like \'", __newDateValue.Remove(0, 1).ToUpper(), "%\'"));
                                                }
                                                else
                                                {
                                                    __where.Append(string.Concat(" like \'%", __newDateValue.ToUpper(), "%\'"));
                                                }
                                                break;
                                        }
                                    }
                                }
                                if (__whereFirst)
                                {
                                    __where.Append(")");
                                }
                            }
                        }
                    } // for
                }
                else
                {
                    bool __whereFirst = false;
                    for (int __loop = 0; __loop < this._arGrid._columnList.Count; __loop++)
                    {
                        MyLib._myGrid._columnType __getColumnType = (MyLib._myGrid._columnType)this._arGrid._columnList[__loop];
                        if (__getColumnType._originalName.Length > 0 && __getColumnType._isQuery == true && __getColumnType._isColumnFilter == true)
                        {
                            if (__getColumnType._isHide == false)
                            {
                                // กรณีการค้นหาตัวเดียว
                                if (this._searchArTextbox.Text.Length > 0)
                                {
                                    try
                                    {
                                        string __getValue = this._searchArTextbox.Text;
                                        string __newDateValue = __getValue;
                                        Boolean __valueExtra = false;
                                        if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                        {
                                            __newDateValue = __getValue.ToString().Remove(0, 1);
                                            __valueExtra = true;
                                        }
                                        switch (__getColumnType._type)
                                        {
                                            case 2: // Number
                                            case 3:
                                            case 5:
                                                double __newValue = 0;
                                                try
                                                {
                                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                    {
                                                        __newValue = Double.Parse(__newDateValue);
                                                        //
                                                        if (__newValue != 0)
                                                        {
                                                            if (__whereFirst == true)
                                                            {
                                                                __where.Append(" or ");
                                                            }
                                                            __whereFirst = true;
                                                            //
                                                            //if (this._addQuotWhere)
                                                            //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                            //else
                                                            __where.Append(string.Concat(__getColumnType._query));
                                                            //
                                                            if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                                __getValue = __getValue[0].ToString() + __newValue.ToString();
                                                            else
                                                                __getValue = String.Concat("=", __newValue.ToString());
                                                            __where.Append(__getValue);
                                                        }
                                                    }
                                                }
                                                catch
                                                {
                                                }
                                                break;
                                            case 4: // Date
                                                try
                                                {
                                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                    {
                                                        DateTime __test = DateTime.Parse(__newDateValue, MyLib._myGlobal._cultureInfo());
                                                        //
                                                        if (__whereFirst == true)
                                                        {
                                                            __where.Append(" or ");
                                                        }
                                                        __whereFirst = true;
                                                        //
                                                        //if (this._addQuotWhere)
                                                        //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                        //else
                                                        __where.Append(string.Concat(__getColumnType._query));
                                                        DateTime __newDate = MyLib._myGlobal._convertDate(__getValue);
                                                        if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                        {
                                                            __newDate = MyLib._myGlobal._convertDate(__newDateValue);
                                                            __where.Append(string.Concat(__getValue[0].ToString(), "\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                        }
                                                        else
                                                        {
                                                            __where.Append(string.Concat("=\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                        }
                                                    }
                                                }
                                                catch
                                                {
                                                }
                                                break;
                                            case 11:
                                                {

                                                }
                                                break;
                                            default:// String
                                                    //
                                                if (__valueExtra == false)
                                                {
                                                    if (__whereFirst == true)
                                                    {
                                                        __where.Append(" or ");
                                                    }
                                                    __whereFirst = true;
                                                    //
                                                    //if (this._addQuotWhere)
                                                    //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                    //else
                                                    __where.Append(string.Concat("" + __getColumnType._query + ""));
                                                    if (this._searchArTextbox.Text[0] == '+')
                                                    {
                                                        __where.Append(string.Concat(" like \'", __newDateValue.Remove(0, 1).ToUpper(), "%\'"));
                                                    }
                                                    else
                                                    {
                                                        __where.Append(string.Concat(" like \'%", __newDateValue.ToUpper(), "%\'"));
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    } // for
                }

                DataRow[] __selectData = _arDataTable.Select(__where.ToString());
                //DataTable __getTmp = _arDataTable.Clone();
                //foreach (DataRow row in __selectData)
                //{
                //    __getTmp.Rows.Add(row.ItemArray);
                //}
                this._arGrid._loadFromDataTable(_arDataTable, __selectData);

            }
        }
    }
}
