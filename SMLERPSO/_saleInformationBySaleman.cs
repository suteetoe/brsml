using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPSO
{
    public partial class _saleInformationBySaleman : UserControl
    {
        DataTable _arDataTable = null;
        DataTable _icDataTable = null;

        public _saleInformationBySaleman()
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

            this._salemanGrid._table_name = _g.d.erp_user._table;
            this._salemanGrid._isEdit = false;
            this._salemanGrid._addColumn(_g.d.erp_user._code, 1, 30, 30);
            this._salemanGrid._addColumn(_g.d.erp_user._name_1, 1, 70, 70);
            this._salemanGrid._calcPersentWidthToScatter();
            this._salemanGrid._mouseClick += _salemanGrid__mouseClick;

            this._icGrid._table_name = _g.d.ic_inventory._table;
            this._icGrid._isEdit = false;
            this._icGrid._addColumn(_g.d.ic_inventory._code, 1, 30, 30);
            this._icGrid._addColumn(_g.d.ic_inventory._name_1, 1, 70, 70);
            this._icGrid._calcPersentWidthToScatter();
            this._icGrid._mouseClick += _icGrid__mouseClick;

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
            this._loadSaleDoc();
        }

        private void _icGrid__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            this._loadSaleDoc();
        }

        void _loadSaleDoc()
        {
            string __DocQuery = this._saleDocQuery();

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            DataSet __resul = __myFrameWork._query(MyLib._myGlobal._databaseName, __DocQuery);
            if (__resul.Tables.Count > 0)
            {
                this._docGrid._loadFromDataTable(__resul.Tables[0]);
            }
            this._loadSaleByYear();
        }

        string _saleDocQuery()
        {
            string __getSaleCode = this._salemanGrid._cellGet(this._salemanGrid._selectRow, _g.d.erp_user._code).ToString();
            string __getItemCode = (this._icGrid._selectRow != -1) ? this._icGrid._cellGet(this._icGrid._selectRow, _g.d.ic_inventory._code).ToString() : "";
            string __getArCode = (this._arGrid._selectRow != -1) ? this._arGrid._cellGet(this._arGrid._selectRow, _g.d.ar_customer._code).ToString() : "";


            StringBuilder __getWhere = new StringBuilder();
            __getWhere.Append(" sale_code = \'" + __getSaleCode + "\' ");


            if (__getItemCode.Length > 0)
            {
                if (__getWhere.Length > 0)
                    __getWhere.Append(" and ");
                __getWhere.Append(" item_code = \'" + this._icGrid._cellGet(this._icGrid._selectRow, _g.d.ic_inventory._code).ToString() + "\' ");
            }

            if (__getArCode.Length > 0)
            {
                if (__getWhere.Length > 0)
                    __getWhere.Append(" and ");
                __getWhere.Append(" cust_code = \'" + this._icGrid._cellGet(this._arGrid._selectRow, _g.d.ar_customer._code).ToString() + "\' ");
            }


            string __query = "select doc_no, doc_date, sum(qty) as qty, unit_code, price, discount, sum(sum_amount) as sum_amount, (sale_code || '~' || (select name_1 from erp_user where erp_user.code = temp1.sale_code ) ) as sale_detail, (select discount_word from ic_trans where ic_trans.doc_no= temp1.doc_no and ic_trans.trans_flag = temp1.trans_flag) as trans_discount, trans_flag " +

               " from ( " +
               " select cust_code, doc_no, doc_date, doc_date_calc, doc_time_calc, sale_code, trans_flag, item_code, unit_code, coalesce(discount, '0') as discount, price, case when trans_flag = 48 then -1*qty else qty end as qty, case when trans_flag = 48 then -1*sum_amount else sum_amount end as sum_amount  from ic_trans_detail " +
               " where trans_flag in (44, 46, 48)  and last_status = 0  and  " + __getWhere +
               ") as temp1 " +
               " group by doc_no, doc_date, doc_date_calc, doc_time_calc, sale_code, trans_flag, item_code, unit_code, discount, price order by doc_date_calc desc, doc_time_calc desc ";



            return __query;
        }

        private void _salemanGrid__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {

            this._icGrid._selectRow = -1;
            this._arGrid._selectRow = -1;
            this._docGrid._clear();
            this._docGrid.Invalidate();

            string __getSaleCode = this._salemanGrid._cellGet(e._row, _g.d.erp_user._code).ToString();

            // show item show customer
            StringBuilder __query = new StringBuilder();
            __query.Append(MyLib._myGlobal._xmlHeader + "<node>");

            //string __whereICCode = "";

            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select code, name_1 from ic_inventory where code in (select distinct item_code from ic_trans_detail where sale_code = \'" + __getSaleCode + "\' and trans_flag in (44,46, 48) and last_status=0 ) order by code "));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select code, name_1 from ar_customer where code in (select distinct cust_code from ic_trans where sale_code = \'" + __getSaleCode + "\'  and trans_flag in (44,46, 48) and last_status=0 ) order by code  "));

            // sale query
            string __queryGetSaleAmount = " select sale_code, doc_no, trans_flag, ( case when trans_flag in (48,103) then -1*sum_amount else sum_amount end ) as amount , extract(month from doc_date ) as month_sum, extract(year from doc_date ) as year_sum from ic_trans_detail where trans_flag in (44,46,48,99,101,103) {0} ";
            string __queryGetSaleExtractMonth = "select sale_code,(case when month_sum =1 then amount else 0 end) as month_1,(case when month_sum =2 then amount else 0 end) as month_2,(case when month_sum =3 then amount else 0 end) as month_3,(case when month_sum =4 then amount else 0 end) as month_4,(case when month_sum =5 then amount else 0 end) as month_5,(case when month_sum =6 then amount else 0 end) as month_6,(case when month_sum =7 then amount else 0 end) as month_7,(case when month_sum =8 then amount else 0 end) as month_8,(case when month_sum =9 then amount else 0 end) as month_9,(case when month_sum =10 then amount else 0 end) as month_10,(case when month_sum =11 then amount else 0 end) as month_11,(case when month_sum =12 then amount else 0 end) as month_12 , year_sum from ("
                + __queryGetSaleAmount + ") as temp1 ";

            string __queryGetSumMonth = " select sum(month_1) as month_1, sum(month_2) as month_2, sum(month_3) as month_3, sum(month_4) as month_4, sum(month_5) as month_5, sum(month_6) as month_6, sum(month_7) as month_7, sum(month_8) as month_8, sum(month_9) as month_9, sum(month_10) as month_10, sum(month_11) as month_11, sum(month_12) as month_12, year_sum from ( "
                + __queryGetSaleExtractMonth + " ) as temp2 {1} group by year_sum ";

            string __getSaleYer = " select" +
                " \'All\' as " + _g.d.resource_report._category_sale +
                ", month_1 as " + _g.d.resource_report._month_jan
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
                + ", year_sum as " + _g.d.resource_report._year + " from ( " + string.Format(__queryGetSumMonth, " and sale_code = \'" + __getSaleCode + "\' ", " where sale_code = \'" + __getSaleCode + "\' ") + " ) as temp3  ";

            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__getSaleYer));

            // doc 
            // string __DocQuery = this._saleDocQuery();
            //__query.Append(MyLib._myUtil._convertTextToXmlForQuery(__DocQuery));


            __query.Append("</node>");
            //
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

            if (__result.Count > 0)
            {
                this._icDataTable = ((DataSet)__result[0]).Tables[0];
                this._arDataTable = ((DataSet)__result[1]).Tables[0];

                this._icGrid._loadFromDataTable(this._icDataTable);
                this._arGrid._loadFromDataTable(this._arDataTable);

                this._saleGrid._loadFromDataTable(((DataSet)__result[2]).Tables[0]);

                //this._docGrid._loadFromDataTable(((DataSet)__result[3]).Tables[0]);

            }
        }

        void _loadData()
        {
            string __searchTextTrim = this._searchSaleManTextbox.Text.Trim();
            string[] __searchTextSplit = __searchTextTrim.Split(' ');

            // ประกอบ where
            StringBuilder __where = new StringBuilder();
            if (__searchTextSplit.Length > 1)
            {
                // กรณี ป้อนการค้นหาหลายคำ (เว้น spacebar)
                for (int __loop = 0; __loop < this._salemanGrid._columnList.Count; __loop++)
                {
                    bool __whereFirst = false;
                    MyLib._myGrid._columnType __getColumnType = (MyLib._myGrid._columnType)this._salemanGrid._columnList[__loop];
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
                                            if (this._searchSaleManTextbox.Text[0] == '+')
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
                for (int __loop = 0; __loop < this._salemanGrid._columnList.Count; __loop++)
                {
                    MyLib._myGrid._columnType __getColumnType = (MyLib._myGrid._columnType)this._salemanGrid._columnList[__loop];
                    if (__getColumnType._originalName.Length > 0 && __getColumnType._isQuery == true && __getColumnType._isColumnFilter == true)
                    {
                        if (__getColumnType._isHide == false)
                        {
                            // กรณีการค้นหาตัวเดียว
                            if (this._searchSaleManTextbox.Text.Length > 0)
                            {
                                try
                                {
                                    string __getValue = this._searchSaleManTextbox.Text;
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
                                                if (this._searchSaleManTextbox.Text[0] == '+')
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
            string __query = "select code, name_1 from " + _g.d.erp_user._table + ((__where.Length > 0) ? " where " + __where : "") + " order by code";
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
            this._salemanGrid._loadFromDataTable(__result.Tables[0]);
        }

        string _extraWhere2 = "";

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                if (_searchAuto.Checked)
                {
                    if (_oldText.CompareTo(this._searchSaleManTextbox.Text) != 0)
                    {
                        _oldText = this._searchSaleManTextbox.Text;
                        this._loadData();
                    }
                }
            }
        }

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

        private void _searchICTextbox_TextChanged(object sender, EventArgs e)
        {
            if (this._icDataTable != null && this._icDataTable.Rows.Count > 0)
            {
                StringBuilder __where = new StringBuilder();
                // split value
                string __searchTextTrim = this._searchICTextbox.Text.Trim();
                string[] __searchTextSplit = __searchTextTrim.Split(' ');
                if (__searchTextSplit.Length > 1)
                {
                    // กรณี ป้อนการค้นหาหลายคำ (เว้น spacebar)
                    for (int __loop = 0; __loop < this._icGrid._columnList.Count; __loop++)
                    {
                        bool __whereFirst = false;
                        MyLib._myGrid._columnType __getColumnType = (MyLib._myGrid._columnType)this._icGrid._columnList[__loop];
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
                                                if (this._searchICTextbox.Text[0] == '+')
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
                    for (int __loop = 0; __loop < this._icGrid._columnList.Count; __loop++)
                    {
                        MyLib._myGrid._columnType __getColumnType = (MyLib._myGrid._columnType)this._icGrid._columnList[__loop];
                        if (__getColumnType._originalName.Length > 0 && __getColumnType._isQuery == true && __getColumnType._isColumnFilter == true)
                        {
                            if (__getColumnType._isHide == false)
                            {
                                // กรณีการค้นหาตัวเดียว
                                if (this._searchICTextbox.Text.Length > 0)
                                {
                                    try
                                    {
                                        string __getValue = this._searchICTextbox.Text;
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
                                                    if (this._searchICTextbox.Text[0] == '+')
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

                DataRow[] __selectData = _icDataTable.Select(__where.ToString());
                //DataTable __getTmp = _arDataTable.Clone();
                //foreach (DataRow row in __selectData)
                //{
                //    __getTmp.Rows.Add(row.ItemArray);
                //}
                this._icGrid._loadFromDataTable(_icDataTable, __selectData);

                //this._loadSaleByYear();
            }
        }

        private void _searchArTextbox_TextChanged(object sender, EventArgs e)
        {
            if (this._arDataTable != null && this._arDataTable.Rows.Count > 0)
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
                //this._loadSaleByYear();
            }
        }

        private void _loadSaleByYear()
        {
            string __getSaleCode = this._salemanGrid._cellGet(this._salemanGrid.SelectRow, _g.d.erp_user._code).ToString();

            string __getItemCode = (this._icGrid._selectRow != -1) ? this._icGrid._cellGet(this._icGrid._selectRow, _g.d.ic_inventory._code).ToString() : "";
            string __getArCode = (this._arGrid._selectRow != -1) ? this._arGrid._cellGet(this._arGrid._selectRow, _g.d.ar_customer._code).ToString() : "";

            // show item show customer
            StringBuilder __query = new StringBuilder();
            __query.Append(MyLib._myGlobal._xmlHeader + "<node>");

            //string __whereICCode = "";

            //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("select code, name_1 from ic_inventory where code in (select distinct item_code from ic_trans_detail where sale_code = \'" + __getSaleCode + "\' and trans_flag in (44,46, 48) and last_status=0 ) order by code "));
            //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("select code, name_1 from ar_customer where code in (select distinct cust_code from ic_trans where sale_code = \'" + __getSaleCode + "\'  and trans_flag in (44,46, 48) and last_status=0 ) order by code  "));

            // sale query
            string __queryGetSaleAmount = " select sale_code, doc_no, trans_flag, ( case when trans_flag in (48,103) then -1*sum_amount else sum_amount end ) as amount , extract(month from doc_date ) as month_sum, extract(year from doc_date ) as year_sum from ic_trans_detail where trans_flag in (44,46,48,99,101,103) {0} ";
            string __queryGetSaleExtractMonth = "select sale_code,(case when month_sum =1 then amount else 0 end) as month_1,(case when month_sum =2 then amount else 0 end) as month_2,(case when month_sum =3 then amount else 0 end) as month_3,(case when month_sum =4 then amount else 0 end) as month_4,(case when month_sum =5 then amount else 0 end) as month_5,(case when month_sum =6 then amount else 0 end) as month_6,(case when month_sum =7 then amount else 0 end) as month_7,(case when month_sum =8 then amount else 0 end) as month_8,(case when month_sum =9 then amount else 0 end) as month_9,(case when month_sum =10 then amount else 0 end) as month_10,(case when month_sum =11 then amount else 0 end) as month_11,(case when month_sum =12 then amount else 0 end) as month_12 , year_sum from ("
                + __queryGetSaleAmount + ") as temp1 ";

            string __queryGetSumMonth = " select sum(month_1) as month_1, sum(month_2) as month_2, sum(month_3) as month_3, sum(month_4) as month_4, sum(month_5) as month_5, sum(month_6) as month_6, sum(month_7) as month_7, sum(month_8) as month_8, sum(month_9) as month_9, sum(month_10) as month_10, sum(month_11) as month_11, sum(month_12) as month_12, year_sum from ( "
                + __queryGetSaleExtractMonth + " ) as temp2 {1} group by year_sum ";

            string __getSaleYer = " select" +
                " \'All\' as " + _g.d.resource_report._category_sale +
                ", month_1 as " + _g.d.resource_report._month_jan
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
                + ", year_sum as " + _g.d.resource_report._year + " from ( " + string.Format(__queryGetSumMonth, " and sale_code = \'" + __getSaleCode + "\' ", " where sale_code = \'" + __getSaleCode + "\' ") + " ) as temp3  ";




            // sale by filter
            if (__getItemCode != "" || __getArCode != "")
            {
                string __rowDesc = "";
                if (__getItemCode != "" && __getArCode != "")
                    __rowDesc = "IC-AR";
                else if (__getItemCode != "")
                {
                    __rowDesc = "IC";
                }
                else if (__getArCode != "")
                {
                    __rowDesc = "AR";
                }

                __getSaleYer +=
                     " union all select" +
                    " \'" + __rowDesc + "\' as " + _g.d.resource_report._category_sale +
                    ", month_1 as " + _g.d.resource_report._month_jan
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
                    + ", year_sum as " + _g.d.resource_report._year + " from ( " + string.Format(__queryGetSumMonth, " and sale_code = \'" + __getSaleCode + "\' " + ((__getItemCode != "") ? " and item_code=\'" + __getItemCode + "\' " : "") + ((__getArCode != "") ? " and cust_code = \'" + __getArCode + "\'" : ""), " where sale_code = \'" + __getSaleCode + "\' ") + " ) as temp  "

                    ;
            }



            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__getSaleYer));

            // doc 
            // string __DocQuery = this._saleDocQuery();
            //__query.Append(MyLib._myUtil._convertTextToXmlForQuery(__DocQuery));


            __query.Append("</node>");
            //
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

            if (__result.Count > 0)
            {
                this._saleGrid._loadFromDataTable(((DataSet)__result[0]).Tables[0]);

            }
        }
    }
}
