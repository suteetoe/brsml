using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SMLProcess
{
    public class _posProcess
    {
        public string _numberList = "";

        public decimal _pointBalance(string custCode)
        {
            return _pointBalance(custCode, false);
        }

        public decimal _pointBalance(string custCode, Boolean isGetSyncPoint)
        {
            decimal __result = 0M;
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            string __dateQuery = MyLib._myGlobal._convertDateToQuery(DateTime.Now);
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select coalesce(sum(case when trans_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ลดแต้ม).ToString() + " then -1*" + _g.d.ic_trans._sum_point + " else " + _g.d.ic_trans._sum_point + " end ),0) as " + _g.d.ic_trans._sum_point + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._cust_code + "=\'" + custCode + "\' and (" + _g.d.ic_trans._is_pos + "=1 or " + _g.d.ic_trans._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_แต้มยกมา).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ลดแต้ม).ToString() + ") ) and " + _g.d.ic_trans._last_status + "=0"));
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select coalesce(sum(" + _g.d.cb_trans._point_qty + "),0) as " + _g.d.cb_trans._point_qty + " from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._ap_ar_code + "=\'" + custCode + "\' and coalesce((select " + _g.d.ic_trans._last_status + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + "=" + _g.d.cb_trans._table + "." + _g.d.cb_trans._doc_no + " and " + _g.d.ic_trans._table + "." + _g.d.ic_trans._trans_flag + "=" + _g.d.cb_trans._table + "." + _g.d.cb_trans._trans_flag + "),0)=0"));
            if (isGetSyncPoint == true)
            {
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(" select " + _g.d.ar_customer._point_balance + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "= \'" + custCode + "\' "));
            }
            __myquery.Append("</node>");
            ArrayList __data = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            if (__data.Count > 0)
            {
                if (isGetSyncPoint == true && _g.g._companyProfile._activeSync == true && _g.g._companyProfile._use_point_center == true)
                {
                    DataTable __balancePoint = ((DataSet)__data[2]).Tables[0];
                    if (__balancePoint.Rows.Count > 0)
                    {
                        __result = MyLib._myGlobal._decimalPhase(__balancePoint.Rows[0][0].ToString());
                    }

                }
                else
                {
                    DataTable __in = ((DataSet)__data[0]).Tables[0];
                    DataTable __out = ((DataSet)__data[1]).Tables[0];
                    decimal __inValue = MyLib._myGlobal._decimalPhase(__in.Rows[0][_g.d.ic_trans._sum_point].ToString());
                    decimal __outValue = MyLib._myGlobal._decimalPhase(__out.Rows[0][_g.d.cb_trans._point_qty].ToString());
                    __result = __inValue - __outValue;
                }
            }
            return __result;
        }

        /// <summary>
        /// Process For Thread Call
        /// </summary>
        public void _processCoupon()
        {
            _processCoupon(this._numberList);
        }

        public void _processCoupon(string numberList)
        {
            if (numberList.Trim().Length > 0)
            {
                StringBuilder __numberList = new StringBuilder();
                String[] __numberSplit = numberList.Split(',');
                for (int __loop = 0; __loop < __numberSplit.Length; __loop++)
                {
                    if (__numberSplit[__loop].Trim().Length > 0)
                    {
                        if (__numberList.Length > 0)
                        {
                            __numberList.Append(",");
                        }
                        __numberList.Append("\'" + __numberSplit[__loop].Trim() + "\'");
                    }
                }

                if (__numberList.Length > 0)
                {
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    StringBuilder __myQuery = new StringBuilder();
                    __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.coupon_list._table + " set " + _g.d.coupon_list._balance_amount + "=case when " + _g.d.coupon_list._coupon_type + "=1 then 0 else (" + _g.d.coupon_list._amount + "-coalesce((select sum(" + _g.d.cb_trans_detail._amount + ") from " + _g.d.cb_trans_detail._table + " where " + _g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._trans_number + "=" + _g.d.coupon_list._table + "." + _g.d.coupon_list._number + " and " + _g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._doc_type + "=9 and " + _g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._last_status + "=0),0)) end where " + _g.d.coupon_list._number + " in (" + __numberList.ToString() + ")"));
                    __myQuery.Append("</node>");
                    string __query = __myQuery.ToString();
                    __myFrameWork._queryList(MyLib._myGlobal._databaseName, __query);
                }
            }
        }


        /// <summary>
        /// Recal Point
        /// </summary>
        /// <param name="docNo"></param>
        public void _pointReCal(string docNo)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            string __queryGetItem = "select " + _g.d.ic_trans_detail._sum_amount + ", (select have_point from ic_inventory_detail where ic_inventory_detail.ic_code = ic_trans_detail.item_code) as " + _g.d.ic_inventory_detail._have_point + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + " = \'" + docNo + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ);
            string __queryGetDivPoint = "select " + _g.d.pos_point_period._amount + " from " + _g.d.pos_point_period._table + " where ( select ic_trans.doc_date from ic_trans where doc_no =\'" + docNo + "\' and trans_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + " ) between " + _g.d.pos_point_period._from_date + " and " + _g.d.pos_point_period._to_date;
            //string __queryGetSaleDoc = "select * from ic_trans where " + _g.d.ic_trans_detail._doc_no + " = \'" + docNo + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ);
            string __queryGetPay = "select " + _g.d.cb_trans._cash_amount + "," + _g.d.cb_trans._card_amount + " from " + _g.d.cb_trans._table + " where " + _g.d.ic_trans_detail._doc_no + " = \'" + docNo + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ);

            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryGetItem));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryGetDivPoint));
            //__query.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryGetSaleDoc));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryGetPay));
            __query.Append("</node>");

            ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

            // เอา sum amount มาคิด point

            try
            {
                int __tableCount = 0;
                DataTable __itemTable = ((DataSet)__result[__tableCount++]).Tables[0];
                DataTable __pointConfigTable = ((DataSet)__result[__tableCount++]).Tables[0];
                //DataTable __saleDocTable = ((DataSet)__result[__tableCount ++]).Tables[0];
                DataTable __payTable = ((DataSet)__result[__tableCount++]).Tables[0];

                decimal __pointDivAmount = MyLib._myGlobal._decimalPhase(__pointConfigTable.Rows[0][_g.d.pos_point_period._amount].ToString());
                decimal __pointTotal = 0M;

                decimal __totalPointBalanceBill = 0M;

                for (int __row = 0; __row < __itemTable.Rows.Count; __row++)
                {
                    if (__itemTable.Rows[__row][_g.d.ic_inventory_detail._have_point].ToString().Equals("1"))
                    {
                        __pointTotal += MyLib._myGlobal._decimalPhase(__itemTable.Rows[__row][_g.d.ic_trans_detail._sum_amount].ToString());
                    }
                }

                __totalPointBalanceBill = (__pointDivAmount == 0M) ? 0M : Math.Floor(__pointTotal / __pointDivAmount);
                if (_g.g._companyProfile._calc_point_from_pay)
                {
                    decimal __payAmount = MyLib._myGlobal._decimalPhase(__payTable.Rows[0][_g.d.cb_trans._cash_amount].ToString()) + MyLib._myGlobal._decimalPhase(__payTable.Rows[0][_g.d.cb_trans._card_amount].ToString());

                    __totalPointBalanceBill = (__pointDivAmount == 0M) ? 0M : Math.Floor(__payAmount / __pointDivAmount);
                }

                // update it now
                string __updateStr = "update ic_trans set " + _g.d.ic_trans._sum_point + " = " + __totalPointBalanceBill + " where doc_no = \'" + docNo + "\' and " + _g.d.ic_trans._sum_point + " <> " + __totalPointBalanceBill + " ";
                string __queryResult = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __updateStr);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static string _processPointBalanceQuery(string custcodeList, string docDateBegin, string docDateEnd, Boolean isSync)
        {
            string __result = "";
            StringBuilder __query = new StringBuilder();

            string __pointInQuery = "( select coalesce(sum(" + _g.d.ic_trans._sum_point + "),0) from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " and " + _g.d.ic_trans._table + "." + _g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans._table + "." + _g.d.ic_trans._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_แต้มยกมา).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + ") )";
            string __pointOutQuery = "( select coalesce(sum(" + _g.d.cb_trans._point_qty + "), 0) from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._table + "." + _g.d.cb_trans._ap_ar_code + "= " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " and ( coalesce((select " + _g.d.ic_trans._last_status + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + "=" + _g.d.cb_trans._table + "." + _g.d.cb_trans._doc_no + " and " + _g.d.ic_trans._table + "." + _g.d.ic_trans._trans_flag + "=" + _g.d.cb_trans._table + "." + _g.d.cb_trans._trans_flag + ((isSync == true) ? " and " + _g.d.ic_trans._table + ".branch_sync = " + _g.d.cb_trans._table + ".branch_sync " : "") + "), 0) = 0) and (" + _g.d.cb_trans._table + "." + _g.d.cb_trans._trans_flag + " in ( " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + ") ) ) ";

            __query.Append("update " + _g.d.ar_customer._table + " set " + _g.d.ar_customer._point_balance + "=(" + __pointInQuery + "-" + __pointOutQuery + ") where coalesce(" + _g.d.ar_customer._point_balance + ", 0)  <> (" + __pointInQuery + "-" + __pointOutQuery + ") ");
            if (custcodeList.Length > 0)
            {
                __query.Append(" and " + _g.d.ar_customer._code + " in (" + custcodeList + ") ");
            }
            __result = __query.ToString();

            return __result;
        }
    }
}
