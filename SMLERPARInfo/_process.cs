using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace SMLERPARAPInfo
{
    public class _process
    {
        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

        public DataTable _movement(_apArConditionEnum mode, string code, MyLib._myGrid custGrid, DateTime dateBegin, DateTime dateEnd, Boolean movementOnly)
        {
            string __dateBegin = MyLib._myGlobal._convertDateToQuery(dateBegin);
            string __dateEnd = MyLib._myGlobal._convertDateToQuery(dateEnd);
            string __custWhere1 = "";
            string __custWhere2 = "";

            if (code != null)
            {
                __custWhere1 = " and ic_trans.cust_code=\'" + code + "\'";
                __custWhere2 = " and ap_ar_trans.cust_code=\'" + code + "\'";
            }
            else
            {
                __custWhere1 = custGrid._createWhere("ic_trans.cust_code");
                if (__custWhere1.Length > 0)
                {
                    __custWhere1 = " and (" + __custWhere1 + ")";
                    __custWhere2 = " and (" + custGrid._createWhere("ap_ar_trans.cust_code") + ")";
                }
            }
            //
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __query = new StringBuilder();
            string __custNameQuery = "";
            string __flag1 = ""; // ขาย,ซื้อ
            string __flag2 = ""; // รับคืน,ส่งคืน
            string __flag3 = ""; // เพิ่มหนี้
            string __flag4 = ""; // ตั้งหนี้ยกมา+เพิ่มหนี้ยกมา+เพิ่มหนี้อื่นๆ
            string __flag5 = ""; // ลดหนี้ยกมา,ลดหนี้อื่นๆ
            string __flag6 = ""; // รับชำระหนี้

            if (mode == _apArConditionEnum.ลูกหนี้_เคลื่อนไหว)
            {
                __custNameQuery = "(select name_1 from ar_customer where ar_customer.code=cust_code) as cust_name";
                // toe เพิ่มรายได้อื่น ๆ
                __flag1 = "(" + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + " or " +
                    _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น).ToString() +
                    " ) and " + _g.d.ic_trans._inquiry_type + " in (0,2) ";
                __flag2 = _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้).ToString() + " and " + _g.d.ic_trans._inquiry_type + " in (0,2,4) ";
                __flag3 = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้).ToString();
                __flag4 = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้ยกมา).ToString() + "," +
                          _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น).ToString() + "," + // โต๋ เพิ่มตี้งหนี อื่น ๆ 2012-05-23
                          _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้ยกมา).ToString() + "," +
                          _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น).ToString() + "," +
                          _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้).ToString();

                __flag5 = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้ยกมา).ToString() + "," +
                          _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น).ToString() + "," +
                          _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้).ToString();
                __flag6 = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้).ToString();
            }
            if (mode == _apArConditionEnum.เจ้าหนี้_เคลื่อนไหว)
            {
                __custNameQuery = "(select name_1 from ap_supplier where ap_supplier.code=cust_code) as cust_name";
                // toe เพิ่มค่าใช้จ่ายอื่น ๆ
                __flag1 = "(" + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ).ToString() + " or " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้).ToString() + " or " +
                    _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น).ToString() +
                    " ) and " + _g.d.ic_trans._inquiry_type + " in (0, 2) ";

                __flag2 = _g.d.ic_trans._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้).ToString() + ") and " + _g.d.ic_trans._inquiry_type + " in (0, 1) ";
                __flag3 = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้).ToString();
                __flag4 = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้ยกมา).ToString() + "," +
                          _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น).ToString() + "," + // โต๋ เพิ่มตี้งหนี อื่น ๆ 2012-05-23
                          _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้ยกมา).ToString() + "," +
                          _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น).ToString() + "," +
                          _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้).ToString();
                // toe ค่าใช้่จ่ายอื่น ๆ 


                __flag5 = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้ยกมา).ToString() + "," +
                          _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น).ToString() + "," +
                          _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้).ToString();

                __flag6 = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้).ToString();
            }
            // ขายสินค้าและบริการ
            __query.Append("select roworder,1 as doc_sort,cust_code," + __custNameQuery + ",trans_flag as doc_type,doc_date,doc_no,tax_doc_no,doc_ref,credit_day,total_amount as amount from ic_trans where last_status=0 and doc_date <= " + MyLib._myGlobal._sqlDateFunction(__dateEnd) +
                " and ( " + __flag1 + " ) " + __custWhere1);
            __query.Append(" union all ");
            // รับคืนจากการขายเชื่อ (ลดหนี้)
            __query.Append("select roworder,2 as doc_sort,cust_code," + __custNameQuery + ",trans_flag as doc_type,doc_date,doc_no,tax_doc_no,doc_ref,credit_day,total_amount as amount from ic_trans where last_status=0 and doc_date <= " + MyLib._myGlobal._sqlDateFunction(__dateEnd) + " and ("
                + __flag2 + " ) " + __custWhere1);
            __query.Append(" union all ");
            // เพิ่มหนี้
            __query.Append("select roworder,1 as doc_sort,cust_code," + __custNameQuery + ",trans_flag as doc_type,doc_date,doc_no,tax_doc_no,doc_ref,credit_day,total_amount as amount from ic_trans where last_status=0 and doc_date <= " + MyLib._myGlobal._sqlDateFunction(__dateEnd) + " and ("
                + _g.d.ic_trans._trans_flag + " in (" + __flag3 + "))" + __custWhere1);
            __query.Append(" union all ");
            // เพิ่มหนี้อื่นๆ,เพิ่มหนี้ยกมา,ตั้งหนี้ยกมา
            __query.Append("select roworder,1 as doc_sort,cust_code," + __custNameQuery + ",trans_flag as doc_type,doc_date,doc_no,tax_doc_no,doc_ref,credit_day,total_amount as amount from ic_trans where last_status=0 and doc_date <= " + MyLib._myGlobal._sqlDateFunction(__dateEnd) + " and ("
                + _g.d.ic_trans._trans_flag + " in (" + __flag4 + "))" + __custWhere1);
            __query.Append(" union all ");
            // ลดหนี้ยกมา,ลดหนี้อื่นๆ
            __query.Append("select roworder,2 as doc_sort,cust_code," + __custNameQuery + ",trans_flag as doc_type,doc_date,doc_no,tax_doc_no,doc_ref,credit_day,total_amount as amount from ic_trans where last_status=0 and doc_date <= " + MyLib._myGlobal._sqlDateFunction(__dateEnd) + " and ("
                + _g.d.ic_trans._trans_flag + " in (" + __flag5 + "))" + __custWhere1);
            __query.Append(" union all ");
            // รับชำระหนี้
            __query.Append("select roworder,3 as doc_sort,cust_code," + __custNameQuery + ",trans_flag as doc_type,doc_date,doc_no,tax_doc_no,doc_ref,0 as credit_day,total_net_value as amount from ap_ar_trans where last_status = 0 and doc_date <= " + MyLib._myGlobal._sqlDateFunction(__dateEnd) + " and trans_flag ="
                + __flag6 + __custWhere2);
            //
            __query.Append(" order by cust_code,doc_date,doc_sort,doc_no");
            DataTable __getData = __myFrameWork._queryShort(__query.ToString()).Tables[0];
            //
            int __custCodeColumnNumber = __getData.Columns.IndexOf("cust_code");
            int __custNameColumnNumber = __getData.Columns.IndexOf("cust_name");
            int __docDateColumnNumber = __getData.Columns.IndexOf("doc_date");
            int __docNoColumnNumber = __getData.Columns.IndexOf("doc_no");
            int __docTypeColumnNumber = __getData.Columns.IndexOf("doc_type");
            int __taxNoColumnNumber = __getData.Columns.IndexOf("tax_doc_no");
            int __refNoColumnNumber = __getData.Columns.IndexOf("doc_ref");
            int __creditDayColumnNumber = __getData.Columns.IndexOf("credit_day");
            int __amountColumnNumber = __getData.Columns.IndexOf("amount");
            //
            ArrayList __custList = new ArrayList();
            if (code != null && code.Length != 0)
            {
                __custList.Add(code);
            }
            else
            {
                if (__getData.Rows.Count > 0)
                {
                    DataTable __custTable = MyLib._dataTableExtension._selectDistinct(__getData, "cust_code");
                    for (int __row = 0; __row < __custTable.Rows.Count; __row++)
                    {
                        __custList.Add(__custTable.Rows[__row][0].ToString());
                    }
                }
            }
            //
            DataTable __result = new DataTable("Result");
            __result.Columns.Add(_g.d.ap_ar_resource._ar_code, typeof(String));
            __result.Columns.Add(_g.d.ap_ar_resource._ar_name, typeof(String));
            __result.Columns.Add(_g.d.ap_ar_resource._doc_date, typeof(String));
            __result.Columns.Add(_g.d.ap_ar_resource._doc_no, typeof(String));
            __result.Columns.Add(_g.d.ap_ar_resource._vat_no, typeof(String));
            __result.Columns.Add(_g.d.ap_ar_resource._ref_no, typeof(String));
            __result.Columns.Add(_g.d.ap_ar_resource._credit_day, typeof(Decimal));
            __result.Columns.Add(_g.d.ap_ar_resource._doc_type, typeof(String));
            __result.Columns.Add(_g.d.ap_ar_resource._debit_amount, typeof(Decimal));
            __result.Columns.Add(_g.d.ap_ar_resource._credit_amount, typeof(Decimal));
            __result.Columns.Add(_g.d.ap_ar_resource._ar_balance, typeof(Decimal));
            //
            for (int __custLoop = 0; __custLoop < __custList.Count; __custLoop++)
            {
                DataRow[] __trans = __getData.Select("cust_code=\'" + __custList[__custLoop].ToString() + "\'");
                decimal __balance = 0M;
                string __custCode = "";
                string __custName = "";
                Boolean __haveTrans = false; // กรณีมีรายวันไม่ต้องพิมพ์ยอดยกไป
                if (__trans.Length > 0)
                {
                    __custCode = (string)__trans[0][__custCodeColumnNumber];
                    __custName = (string)__trans[0][__custNameColumnNumber];
                }
                for (int __row = 0; __row < __trans.Length; __row++)
                {
                    DataRow __dataRow = __trans[__row];
                    DateTime __docDate = MyLib._myGlobal._convertDateFromQuery(__dataRow[__docDateColumnNumber].ToString());
                    string __docNo = (string)__dataRow[__docNoColumnNumber];
                    string __taxNo = (string)__dataRow[__taxNoColumnNumber];
                    string __refNo = (string)__dataRow[__refNoColumnNumber];
                    int __docType = MyLib._myGlobal._intPhase(__dataRow[__docTypeColumnNumber].ToString());
                    decimal __amount = MyLib._myGlobal._decimalPhase(__dataRow[__amountColumnNumber].ToString());
                    decimal __creditDay = MyLib._myGlobal._decimalPhase(__dataRow[__creditDayColumnNumber].ToString());
                    decimal __debit = 0M;
                    decimal __credit = 0M;
                    string __docTypeName = "";
                    if (__docType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) ||
                        __docType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ) ||
                        __docType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้) ||
                        __docType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น) ||
                        __docType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น))
                    {
                        __debit = __amount;
                        __docTypeName = _g.g._transFlagGlobal._transName(__docType);
                    }
                    else
                        if (__docType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้) ||
                            __docType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด) ||
                            __docType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้))
                    {
                        __credit = __amount;
                        __docTypeName = _g.g._transFlagGlobal._transName(__docType);
                    }
                    else
                            if (__docType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้) ||
                                __docType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด) ||
                                __docType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้))
                    {
                        __debit = __amount;
                        __docTypeName = _g.g._transFlagGlobal._transName(__docType);
                    }
                    else
                                if (__docType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น) ||
                                    __docType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้ยกมา) ||
                                    __docType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น) ||
                                    __docType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้ยกมา) ||
                                    __docType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น) ||
                                    __docType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้ยกมา) ||
                                    __docType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น) ||
                                    __docType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้ยกมา) ||
                                    __docType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้) ||
                                    __docType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้))
                    {
                        __debit = __amount;
                        __docTypeName = _g.g._transFlagGlobal._transName(__docType);
                    }
                    else
                                    if (__docType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น) ||
                                        __docType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้ยกมา) ||
                                        __docType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น) ||
                                        __docType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้ยกมา) ||
                                        __docType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้) ||
                                        __docType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้))
                    {
                        __credit = __amount;
                        __docTypeName = _g.g._transFlagGlobal._transName(__docType);
                    }
                    else
                                        if (__docType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้) ||
                                            __docType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้))
                    {
                        __credit = __amount;
                        __docTypeName = _g.g._transFlagGlobal._transName(__docType);
                    }
                    __balance += (__debit - __credit);
                    if (__docDate.CompareTo(dateBegin) >= 0)
                    {
                        if (__haveTrans == false)
                        {
                            __result.Rows.Add(__custCode, __custName, "", "", "", "", 0M, "ยอดยกมา", 0M, 0M, __balance - (__debit - __credit));
                            __haveTrans = true;
                        }
                        __result.Rows.Add(__custCode, __custName, MyLib._myGlobal._convertDateToString(__docDate, false), __docNo, __taxNo, __refNo, __creditDay, __docTypeName, __debit, __credit, __balance);
                    }
                }
                if (movementOnly == false && __balance != 0M && __trans.Length > 0 && __haveTrans == false)
                {
                    __result.Rows.Add(__custCode, __custName, "", "", "", "", 0M, "ยอดยกไป", 0M, 0M, __balance);
                }
            }
            return __result;
        }

        public string _custStatQuery(_apArConditionEnum mode, string dateWhere)
        {
            string __custWhere = "";
            string __flag1 = "";
            string __flag2 = "";
            string __flag3 = "";
            string __flag4 = "";
            string __flag5 = "";
            string __flagPay = "";
            string __flagExpense = "";

            switch (mode)
            {
                case _apArConditionEnum.ลูกหนี้_สถานะลูกหนี้:
                    __custWhere = " and ar_customer.code=ic_trans.cust_code";
                    // toe เพิ่ม รายได้อื่น ๆ
                    __flag1 = "(" + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + " or " +
                        _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น).ToString() +
                        " ) and " + _g.d.ic_trans._inquiry_type + " in (0, 2) "; // ขายเชื่อ,ขายเชื่อสินค้าบริการ

                    __flag2 = _g.d.ic_trans._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้ยกมา).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น).ToString() + ")"; // โต๋ เพิ่ม ตั้งหนี้อื่น ๆ 2012-05-23

                    // toe เพิ่มหนี้ รายได้อื่น ๆ
                    __flag3 = _g.d.ic_trans._trans_flag + " in ("
                        + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้).ToString() + ","
                        + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้ยกมา).ToString() + ","
                        + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น).ToString() + ","
                        + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้).ToString() + ")";

                    __flag4 = _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้).ToString() + " and " + _g.d.ic_trans._inquiry_type + " in (0,2,4) ";
                    // โต๋ เพิ่ม ลดหนี้รายได้อื่น ๆ
                    __flag5 = _g.d.ic_trans._trans_flag + " in ("
                        + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้ยกมา).ToString() + ","
                        + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น).ToString() + ","
                        + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้).ToString() + ")";
                    __flagPay = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้).ToString() + " and ar_customer.code=ap_ar_trans.cust_code";

                    break;
                default:
                    __custWhere = " and ap_supplier.code=ic_trans.cust_code";
                    // โต๋ เพิ่มค่าใช้จ่ายอื่น ๆ
                    __flag1 = "( " + _g.d.ic_trans._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้).ToString() + ") or " +
                        _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น).ToString() + " )" +
                        " and (" + _g.d.ic_trans._inquiry_type + " in (0,2) )";
                    __flag2 = _g.d.ic_trans._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้ยกมา).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น).ToString() + ")";   // โต๋ เพิ่ม ตั้งหนี้อื่น ๆ 2012-05-23
                    __flag3 = _g.d.ic_trans._trans_flag + " in ("
                        + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้).ToString() + ","
                        + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด).ToString() + ","
                        + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้ยกมา).ToString() + ","
                        + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น).ToString() + ","
                        + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้).ToString() + ")";

                    __flag4 = _g.d.ic_trans._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้).ToString() + ") and (" + _g.d.ic_trans._inquiry_type + " in (0,1) )";
                    __flag5 = _g.d.ic_trans._trans_flag + " in ("
                        + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้ยกมา).ToString() + ","
                        + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น).ToString() + ","
                        + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้).ToString() + ")";
                    __flagPay = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้).ToString() + " and ap_supplier.code=ap_ar_trans.cust_code";
                    break;
            }
            StringBuilder __queryDetail = new StringBuilder();
            // ยอดตั้งหนี้ 
            __queryDetail.Append("select roworder,1 as calc_type,doc_no,cust_code,total_amount as amount from ic_trans where last_status=0 " + ((dateWhere.Length > 0) ? " and " + dateWhere : "") + " and (" + __flag1 + ") " + __custWhere);
            __queryDetail.Append(" union all ");
            // ยอดตั้งหนี้ (ตั้งหนี้ยกมา)
            __queryDetail.Append("select roworder,1 as calc_type,doc_no,cust_code,total_amount as amount from ic_trans where last_status=0 " + ((dateWhere.Length > 0) ? " and " + dateWhere : "") + " and (" + __flag2 + ") " + __custWhere);
            __queryDetail.Append(" union all ");
            // ขายแบบเพิ่มหนี้,เพิ่มหนี้อื่นๆ,เพิ่มหนี้ยกมา
            __queryDetail.Append("select roworder,2 as calc_type,doc_no,cust_code,total_amount as amount from ic_trans where last_status=0" + ((dateWhere.Length > 0) ? " and " + dateWhere : "") + " and (" + __flag3 + ") " + __custWhere);
            __queryDetail.Append(" union all ");
            // รับคืนจากการขายเชื่อ (ลดหนี้)
            __queryDetail.Append("select roworder,3 as calc_type,doc_no,cust_code,-1*total_amount as amount from ic_trans where last_status=0 " + ((dateWhere.Length > 0) ? " and " + dateWhere : "") + " and (" + __flag4 + ") " + __custWhere);
            __queryDetail.Append(" union all ");
            // ลดหนี้ยกมา,ลดหนี้อื่นๆ
            __queryDetail.Append("select roworder,3 as calc_type,doc_no,cust_code,-1*total_amount as amount from ic_trans where last_status=0 " + ((dateWhere.Length > 0) ? " and " + dateWhere : "") + " and (" + __flag5 + ") " + __custWhere);
            __queryDetail.Append(" union all ");
            // รับชำระหนี้
            __queryDetail.Append("select roworder,4 as calc_type,doc_no,cust_code,-1*total_net_value as amount from ap_ar_trans where last_status = 0 and trans_flag =" + __flagPay + " " + ((dateWhere.Length > 0) ? " and " + dateWhere : ""));
            //
            return __queryDetail.ToString();
        }

        public DataTable _arStat(_apArConditionEnum mode, MyLib._myGrid custGrid, string custCodeBegin, string custCodeEnd, DateTime dateBegin, DateTime dateEnd)
        {
            string __dateBegin = MyLib._myGlobal._convertDateToQuery(dateBegin);
            string __dateEnd = MyLib._myGlobal._convertDateToQuery(dateEnd);
            string __custWhere = "";

            if (custGrid != null)
            {
                __custWhere = custGrid._createWhere("code");
                if (__custWhere.Length > 0)
                {
                    __custWhere = " where " + __custWhere;
                }
            }
            else
            {
                __custWhere = (custCodeBegin.Length == 0 && custCodeEnd.Length == 0) ? "" : " where " + __createWhere(custCodeBegin, custCodeEnd, "code");
            }

            string __tableName = "";
            switch (SMLERPARAPInfo._apAr._apArCheck(mode))
            {
                case _apArEnum.ลูกหนี้:
                    __tableName = "ar_customer";
                    break;
                default:
                    __tableName = "ap_supplier";
                    break;
            }

            string __querySum = "select code,name_1," +
                "(select coalesce(sum(amount),0) from (" + this._custStatQuery(mode, "doc_date < " + MyLib._myGlobal._sqlDateFunction(__dateBegin)) + ") as temp1) as " + _g.d.ap_ar_resource._balance_first + "," +
                "(select coalesce(sum(amount),0) from (" + this._custStatQuery(mode, "doc_date between " + MyLib._myGlobal._sqlDateFunction(__dateBegin) + " and " + MyLib._myGlobal._sqlDateFunction(__dateEnd)) + ") as temp2 where calc_type=1) as " + _g.d.ap_ar_resource._debit_1 + "," +
                "(select coalesce(sum(amount),0) from (" + this._custStatQuery(mode, "doc_date between " + MyLib._myGlobal._sqlDateFunction(__dateBegin) + " and " + MyLib._myGlobal._sqlDateFunction(__dateEnd)) + ") as temp3 where calc_type=2) as " + _g.d.ap_ar_resource._debit_2 + "," +
                "(select coalesce(sum(amount),0) from (" + this._custStatQuery(mode, "doc_date between " + MyLib._myGlobal._sqlDateFunction(__dateBegin) + " and " + MyLib._myGlobal._sqlDateFunction(__dateEnd)) + ") as temp4 where calc_type=3) as " + _g.d.ap_ar_resource._credit_1 + "," +
                "(select coalesce(sum(amount),0) from (" + this._custStatQuery(mode, "doc_date between " + MyLib._myGlobal._sqlDateFunction(__dateBegin) + " and " + MyLib._myGlobal._sqlDateFunction(__dateEnd)) + ") as temp5 where calc_type=4) as " + _g.d.ap_ar_resource._credit_2 + "," +
                "(select coalesce(sum(amount),0) from (" + this._custStatQuery(mode, "doc_date <= " + MyLib._myGlobal._sqlDateFunction(__dateEnd)) + ") as temp6) as " + _g.d.ap_ar_resource._balance_end +
                " from " + __tableName + " " + __custWhere;

            string __query = "select code as " + _g.d.ap_ar_resource._ar_code + "," +
                "name_1  as " + _g.d.ap_ar_resource._ar_name + "," +
                _g.d.ap_ar_resource._balance_first + "," +
                _g.d.ap_ar_resource._debit_1 + "," +
                _g.d.ap_ar_resource._debit_2 + "," +
                _g.d.ap_ar_resource._credit_1 + "," +
                _g.d.ap_ar_resource._credit_2 + "," +
                _g.d.ap_ar_resource._balance_end + " from (" + __querySum + ") as temp9 " +
                " where " +
                _g.d.ap_ar_resource._balance_first + "<>0 or " +
                _g.d.ap_ar_resource._debit_1 + "<>0 or " +
                _g.d.ap_ar_resource._debit_2 + "<>0 or " +
                _g.d.ap_ar_resource._credit_1 + "<>0 or " +
                _g.d.ap_ar_resource._credit_2 + "<>0 or " +
                _g.d.ap_ar_resource._balance_end + "<>0 order by " + _g.d.ap_ar_resource._ar_code;
            return this.__myFrameWork._queryShort(__query.ToString()).Tables[0];
        }

        public DataTable _arCreditMoneyBalance(_apArConditionEnum mode, string custCode)
        {
            return _arCreditMoneyBalance(mode, custCode, "");
        }

        /// <summary>
        /// ตรวจสอบหนี้คงค้าง
        /// </summary>
        /// <param name="mode">Mode ลูกหนี้_สถานะลูกหนี้ เท่านั้น</param>
        /// <param name="custCode"></param>
        /// <returns></returns>
        public DataTable _arCreditMoneyBalance(_apArConditionEnum mode, string custCode, string exceptDocNo)
        {
            /*
            string __tableName = "";
            switch (SMLERPARAPInfo._apAr._apArCheck(mode))
            {
                case _apArEnum.ลูกหนี้:
                    __tableName = "ar_customer";
                    break;
                default:
                    __tableName = "ap_supplier";
                    break;
            }

            string __custWhere = (custCode.Length == 0 && custCode.Length == 0) ? "" : " where " + __createWhere(custCode, "", "code");

            string __query = "select code, name_1, " +
                "coalesce((select " + _g.d.ar_customer_detail._credit_money + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "), 0) as " + _g.d.ap_ar_resource._credit_money + ", " + // วงเงิน
                "coalesce((select " + _g.d.ar_customer_detail._credit_money_max + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "), 0) as " + _g.d.ap_ar_resource._credit_money_max + ", " + // วงเงินสูงสุด
                "coalesce((select " + _g.d.ar_customer_detail._credit_status + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "), 0) as " + _g.d.ar_customer_detail._credit_status + ", " + // สถานะเครดิต
                "(select coalesce(sum(amount),0) from (" + this._custStatQuery(mode, "") + ") as temp6) as " + _g.d.ap_ar_resource._balance_end + // หนี้ค้าง
                " from " + __tableName + " " + __custWhere;
            */

            string __query = _arCreditMoneyBalanceQuery(mode, custCode, exceptDocNo);

            return this.__myFrameWork._queryShort(__query.ToString()).Tables[0];
        }

        public string _arCreditMoneyBalanceQuery(_apArConditionEnum mode, string custCode)
        {
            return _arCreditMoneyBalanceQuery(mode, custCode, "");
        }

        public string _arCreditMoneyBalanceQuery(_apArConditionEnum mode, string custCode, string exceptDocNo)
        {

            string __tableName = "";
            switch (SMLERPARAPInfo._apAr._apArCheck(mode))
            {
                case _apArEnum.ลูกหนี้:
                    __tableName = "ar_customer";
                    break;
                default:
                    __tableName = "ap_supplier";
                    break;
            }

            string __whereDocNo = "";
            if (exceptDocNo.Length > 0)
            {
                __whereDocNo = " and doc_no != \'" + exceptDocNo + "\' ";
            }

            string __chqOutstandingQuery = "0";
            if (_g.g._companyProfile._ar_credit_chq_outstanding)
            {
                string __queryChqOnHand = "(select sum(case when status != 2 then amount else 0 end) as onhand_amount from cb_chq_list where chq_type = 1 and ap_ar_code = " + __tableName + ".code)";
                __chqOutstandingQuery = "coalesce(" + __queryChqOnHand + ", 0 )";
            }

            string __sr_remain = "0";
            string __ss_remain = "0";
            string __advanceAmount = "0";
            if (_g.g._companyProfile._sr_ss_credit_check)
            {
                // reduce deposit amount from ss_remain  -(select total_amount from ic_trans as deposit  where deposit.doc_no=ic_trans.doc_ref_trans and deposit.trans_flag=40 and deposit.last_status=0 ) 
                __sr_remain = "coalesce((select sum(total_amount" +
                    "-coalesce((select sum(sum_amount) from ic_trans_detail as x where x.trans_flag in (44, 39, 36) and x.last_status = 0 and x.ref_doc_no = ic_trans.doc_no )"
                    + ",0)) from ic_trans where trans_flag = 34 and last_status = 0 and inquiry_type in (0,2) and doc_success = 0 and approve_status in (0,1) and ic_trans.cust_code = " + __tableName + ".code " + __whereDocNo + "), 0)";
                __ss_remain = "coalesce((select sum(total_amount" +
                    "-coalesce((select sum(sum_amount) from ic_trans_detail as x where x.trans_flag in (44, 37) and x.last_status = 0 and x.ref_doc_no = ic_trans.doc_no )"
                    + ",0)) from ic_trans where trans_flag = 36 and last_status = 0 and inquiry_type in (0,2) and doc_success = 0 and approve_status in (0,1) and ic_trans.cust_code = " + __tableName + ".code " + __whereDocNo + "), 0)";
                __advanceAmount = "coalesce((select sum(case when _def_last_status = 1 then 0 else total_amount-(deposit_buy2+sum_used) end) as balance_amount " +
                    " from(select cust_code, coalesce((select sum(total_amount) from ic_trans as x1 where x1.last_status = 0 and x1.doc_ref = deposit.doc_no), 0) as deposit_buy2 " +
                    " ,coalesce((select sum(amount) from cb_trans_detail as x2 where x2.last_status = 0 and x2.trans_number = deposit.doc_no),0) as sum_used " +
                    " ,total_amount,last_status as _def_last_status from ic_trans as deposit where deposit.trans_flag in (40)  and deposit.cust_code = " + __tableName + ".code " +
                    " ) as temp1 ), 0) ";
            }

            string __custWhere = (custCode.Length == 0 && custCode.Length == 0) ? "" : " where " + __createWhere(custCode, "", "code");

            string __query = "select code, name_1, " +
                "coalesce((select " + _g.d.ar_customer_detail._credit_money + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "), 0) as " + _g.d.ap_ar_resource._credit_money + ", " + // วงเงิน
                "coalesce((select " + _g.d.ar_customer_detail._credit_money_max + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "), 0) as " + _g.d.ap_ar_resource._credit_money_max + ", " + // วงเงินสูงสุด
                "coalesce((select " + _g.d.ar_customer_detail._credit_status + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "), 0) as " + _g.d.ar_customer_detail._credit_status + ", " + // สถานะเครดิต
                "(select coalesce(sum(amount),0) from (" + this._custStatQuery(mode, "") + ") as temp6  " + ((exceptDocNo.Length > 0) ? " where doc_no != \'" + exceptDocNo + "\' " : "" ) + ") as " + _g.d.ap_ar_resource._balance_end + // หนี้ค้าง
                ", " + __chqOutstandingQuery + " as chq_outstanding " +
                ", " + __sr_remain + " as sr_remain " +
                ", " + __ss_remain + " as ss_remain " +
                ", " + __advanceAmount + " as advance_amount " +

                ",coalesce((select " + _g.d.ar_customer_detail._close_reason + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "), '') as " + _g.d.ar_customer_detail._close_reason + // สถานะเครดิต
                ",(select " + _g.d.ar_customer_detail._close_reason_1 + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + ") as " + _g.d.ar_customer_detail._close_reason_1 + // สถานะเครดิต
                ",(select " + _g.d.ar_customer_detail._close_reason_2 + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + ") as " + _g.d.ar_customer_detail._close_reason_2 + // สถานะเครดิต
                ",(select " + _g.d.ar_customer_detail._close_reason_3 + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + ") as " + _g.d.ar_customer_detail._close_reason_3 + // สถานะเครดิต
                ",(select " + _g.d.ar_customer_detail._close_reason_4 + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + ") as " + _g.d.ar_customer_detail._close_reason_4 + // สถานะเครดิต
                ",(select " + _g.d.ar_customer_detail._close_credit_date + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + ") as " + _g.d.ar_customer_detail._close_credit_date + // สถานะเครดิต

                " from " + __tableName + " " + __custWhere;


            return __query;
        }

        public string __createWhere(string begin, string end, string fildName)
        {
            string __itemWhere = "";
            if (begin.Trim().Length != 0 && end.Trim().Length == 0)
            {
                __itemWhere = fildName + "=\'" + begin + "\'";
            }
            else
                if (begin.Trim().Length == 0 && end.Trim().Length != 0)
            {
                __itemWhere = fildName + "=\'" + end + "\'";
            }
            else
                    if (begin.Trim().Length != 0 && end.Trim().Length != 0)
            {
                __itemWhere = fildName + " between \'" + begin + "\' and \'" + end + "\'";
            }
            return __itemWhere;
        }

        public string _createQuery(_apArConditionEnum mode, int dateMode, string custWhere, string docNoWhere, string dateWhereEnd)
        {
            return this._createQuery(mode, dateMode, custWhere, docNoWhere, dateWhereEnd, true);
        }

        public string _createQuery(_apArConditionEnum mode, int dateMode, string custWhere, string docNoWhere, string dateWhereEnd, bool checkPayDate)
        {

            string __fieldList = "";
            string __custWhereQuery = "";
            string __docNoWhereQuery = "";
            //
            switch (mode)
            {
                case _apArConditionEnum.ลูกหนี้_ยอดคงเหลือตามเอกสาร:
                    // toe เพิ่ม credit_date as due_date
                    __fieldList = "cust_code,doc_date,credit_date as due_date,doc_no,trans_flag as doc_type,used_status,tax_doc_no,tax_doc_date,";
                    __custWhereQuery = (custWhere.Length == 0) ? "" : " and " + custWhere;
                    __docNoWhereQuery = (docNoWhere.Length == 0) ? "" : " and " + docNoWhere;
                    break;
                // toe เพิ่ม credit_date as due_date
                case _apArConditionEnum.เจ้าหนี้_ยอดคงเหลือตามเอกสาร:
                    __fieldList = "cust_code,doc_date,credit_date as due_date,doc_no,trans_flag as doc_type,used_status,tax_doc_no,tax_doc_date,";
                    __custWhereQuery = (custWhere.Length == 0) ? "" : " and " + custWhere;
                    __docNoWhereQuery = (docNoWhere.Length == 0) ? "" : " and " + docNoWhere;
                    break;
                case _apArConditionEnum.เจ้าหนี้_อายุเจ้าหนี้:
                    __fieldList = "doc_date, case when (trans_flag = 81) then due_date else credit_date end as due_date,doc_no,";
                    __custWhereQuery = " and ap_supplier.code=ic_trans.cust_code";
                    break;
                case _apArConditionEnum.ลูกหนี้_อายุลูกหนี้:
                    __fieldList = "doc_date,credit_date as due_date,doc_no,";
                    __custWhereQuery = " and ar_customer.code=ic_trans.cust_code";
                    break;
                case _apArConditionEnum.ลูกหนี้_อายุลูกหนี้_แยก_เอกสาร:
                case _apArConditionEnum.ลูกหนี้_อายุลูกหนี้_แยกลูกหนี้_แยกเอกสาร:
                    __fieldList = "cust_code,doc_date, credit_date as due_date,doc_no,trans_flag as doc_type,used_status,";
                    __custWhereQuery = (custWhere.Length == 0) ? "" : " and " + custWhere;
                    break;
                case _apArConditionEnum.เจ้าหนี้_อายุเจ้าหนี้_แยก_เอกสาร:
                case _apArConditionEnum.เจ้าหนี้_อายุเจ้าหนี้_แยกลูกหนี้_แยกเอกสาร:
                    __fieldList = "cust_code,doc_date, case when (trans_flag = 81) then due_date else credit_date end as due_date,doc_no,trans_flag as doc_type,used_status,";
                    __custWhereQuery = (custWhere.Length == 0) ? "" : " and " + custWhere;
                    break;
            }
            //
            // รายวันตั้งหนี้จากการขาย
            StringBuilder __queryDoc = new StringBuilder();
            __queryDoc.Append("select " + __fieldList);
            {
                // toe doc_ref
                __queryDoc.Append("doc_ref as " + _g.d.ap_ar_resource._ref_doc_no + ",");
                __queryDoc.Append("doc_ref_date as " + _g.d.ap_ar_resource._ref_doc_date + ",");


                // 1-ยอดหนี้
                __queryDoc.Append("coalesce(total_amount,0) as amount,coalesce(total_amount,0)-");
                // 1-หักจ่ายชำระหนี้
                string __transFlag1 = "";
                string __transFlag2 = "";
                switch (SMLERPARAPInfo._apAr._apArCheck(mode))
                {
                    case _apArEnum.ลูกหนี้:
                        __transFlag1 = _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + " and (" + _g.d.ic_trans._inquiry_type + "=0  or " + _g.d.ic_trans._inquiry_type + "=2) ";
                        __transFlag2 = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้).ToString();
                        break;
                    case _apArEnum.เจ้าหนี้:
                        // toe เอา //  ," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้).ToString() + " ออก มันซ้ำกับ ตั้งหนี้ ทำให้เอกสารเบิ้ลออกมา
                        __transFlag1 = _g.d.ic_trans._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ).ToString() + ") and (" + _g.d.ic_trans._inquiry_type + "=0 or " + _g.d.ic_trans._inquiry_type + "=2) ";
                        __transFlag2 = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้).ToString();
                        break;
                }

                // toe เอา billing_date ออก
                // and ic_trans.doc_date=ap_ar_trans_detail.billing_date
                __queryDoc.Append("(select coalesce(sum(coalesce(sum_pay_money,0)),0) from ap_ar_trans_detail where coalesce(last_status, 0)=0 and trans_flag in (" + __transFlag2 + ") and ic_trans.doc_no=ap_ar_trans_detail.billing_no and ic_trans.trans_flag=ap_ar_trans_detail.bill_type " + ((checkPayDate) ? " and doc_date <= " + MyLib._myGlobal._sqlDateFunction(dateWhereEnd) : "") + ")");
                __queryDoc.Append(" as balance_amount");
                // toe branch_code
                __queryDoc.Append(",branch_code,remark");

                __queryDoc.Append(" from ic_trans where coalesce(last_status, 0)=0 and " + __transFlag1);
                __queryDoc.Append(" and doc_date <= " + MyLib._myGlobal._sqlDateFunction(dateWhereEnd) + __custWhereQuery + __docNoWhereQuery);
                // union 
                __queryDoc.Append(" union all ");
            }
            //
            StringBuilder __debitTransFlag4 = new StringBuilder();
            {
                string __payFlag = "";
                switch (SMLERPARAPInfo._apAr._apArCheck(mode))
                {
                    case _apArEnum.ลูกหนี้:
                        __debitTransFlag4.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้).ToString() + " or ");
                        __debitTransFlag4.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้ยกมา).ToString() + " or ");
                        __debitTransFlag4.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น).ToString() + " or ");
                        __debitTransFlag4.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้ยกมา).ToString() + " or ");
                        __debitTransFlag4.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น).ToString() + " or ");

                        __debitTransFlag4.Append(" ((" + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น).ToString() + " or ");
                        __debitTransFlag4.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้).ToString() + ") and (" + _g.d.ic_trans._inquiry_type + " in (0,2)) )");

                        __payFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้).ToString();
                        break;
                    case _apArEnum.เจ้าหนี้:
                        __debitTransFlag4.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด).ToString() + " or ");
                        __debitTransFlag4.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้).ToString() + " or ");
                        __debitTransFlag4.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้).ToString() + " or ");
                        __debitTransFlag4.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้ยกมา).ToString() + " or ");
                        __debitTransFlag4.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น).ToString() + " or ");
                        __debitTransFlag4.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้ยกมา).ToString() + " or ");
                        __debitTransFlag4.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น).ToString() + " or ");

                        __debitTransFlag4.Append(" (( " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น).ToString() + " or ");
                        __debitTransFlag4.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้).ToString() + ") and ( " + _g.d.ic_trans._inquiry_type + " in (0,2)) ) ");

                        //  imex เงินจ่ายมัดจำ
                        if (MyLib._myGlobal._OEMVersion.Equals("imex"))
                        {
                            __debitTransFlag4.Append(" or (" + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ).ToString() + " and " + _g.d.ic_trans._inquiry_type + "=1 ) ");
                        }

                        __payFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้).ToString();
                        break;
                }
                //
                __queryDoc.Append("select " + __fieldList);

                // toe doc_ref
                __queryDoc.Append("'' as " + _g.d.ap_ar_resource._ref_doc_no + ",");
                __queryDoc.Append("null as " + _g.d.ap_ar_resource._ref_doc_date + ",");

                // 2-ยอดลดหนี้
                __queryDoc.Append("coalesce(total_amount,0) as amount,coalesce(total_amount,0)-");
                // 2-หักจ่ายชำระหนี้ (ส่วนลดหนี้)
                //  and ic_trans.doc_date=ap_ar_trans_detail.billing_date เช็คเฉพาะเลขที่ก็พอ
                __queryDoc.Append("(select coalesce(sum(coalesce(sum_pay_money,0)),0) from ap_ar_trans_detail where coalesce(last_status, 0)=0 and trans_flag in (" + __payFlag + ") and ic_trans.doc_no=ap_ar_trans_detail.billing_no and ic_trans.trans_flag=ap_ar_trans_detail.bill_type " + ((checkPayDate) ? " and doc_date <= " + MyLib._myGlobal._sqlDateFunction(dateWhereEnd) : "") + ")");
                __queryDoc.Append(" as balance_amount");
                __queryDoc.Append(",branch_code,remark");
                __queryDoc.Append(" from ic_trans where coalesce(last_status, 0)=0 and (" + __debitTransFlag4.ToString() + ") and doc_date <= " + MyLib._myGlobal._sqlDateFunction(dateWhereEnd) + __custWhereQuery + __docNoWhereQuery);
                // union 
                __queryDoc.Append(" union all ");
            }
            // รายวันลดหนี้,รับคืนเงินเชื่อ
            StringBuilder __debitTransFlag2 = new StringBuilder();
            {
                string __payFlag = "";
                switch (SMLERPARAPInfo._apAr._apArCheck(mode))
                {
                    case _apArEnum.ลูกหนี้:
                        __debitTransFlag2.Append("(" + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้).ToString() + " and " + _g.d.ic_trans._inquiry_type + " in (0,2,4) ) or ");
                        __debitTransFlag2.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้ยกมา).ToString() + " or ");
                        __debitTransFlag2.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้).ToString() + " or ");
                        __debitTransFlag2.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น).ToString());
                        __payFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้).ToString();
                        break;
                    case _apArEnum.เจ้าหนี้:
                        // toe เอา inquerity type ออก
                        //__debitTransFlag2.Append("(" + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด).ToString() + " and " + _g.d.ic_trans._inquiry_type + "=0) or ");
                        __debitTransFlag2.Append("(" + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด).ToString() + " and " + _g.d.ic_trans._inquiry_type + " in (0,1)) or ");
                        __debitTransFlag2.Append("(" + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้).ToString() + " and " + _g.d.ic_trans._inquiry_type + " in (0,1)) or ");
                        __debitTransFlag2.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้ยกมา).ToString() + " or ");
                        __debitTransFlag2.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้).ToString() + " or ");
                        __debitTransFlag2.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น).ToString());
                        __payFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้).ToString();
                        break;
                }
                //
                __queryDoc.Append("select " + __fieldList);
                // 2-ยอดลดหนี้ 
                // toe doc_ref
                __queryDoc.Append("'' as " + _g.d.ap_ar_resource._ref_doc_no + ",");
                __queryDoc.Append("null as " + _g.d.ap_ar_resource._ref_doc_date + ",");

                __queryDoc.Append("-1*coalesce(total_amount,0) as amount,-1*(coalesce(total_amount,0)+");
                // 2-หักจ่ายชำระหนี้ (ส่วนลดหนี้ และต้อง  + เพราะ sum_pay_money มีค่าเป็นลบ)
                // and ic_trans.doc_date=ap_ar_trans_detail.billing_date เช็คเฉพาะเลขที่พอ
                __queryDoc.Append("(select coalesce(sum(coalesce(sum_pay_money,0)),0) from ap_ar_trans_detail where coalesce(last_status, 0)=0 and trans_flag in (" + __payFlag + ") and ic_trans.doc_no=ap_ar_trans_detail.billing_no and ic_trans.trans_flag=ap_ar_trans_detail.bill_type " + ((checkPayDate) ? " and doc_date <= " + MyLib._myGlobal._sqlDateFunction(dateWhereEnd) : "") + ")");
                __queryDoc.Append(") as balance_amount");
                __queryDoc.Append(",branch_code,remark");
                __queryDoc.Append(" from ic_trans where coalesce(last_status, 0)=0 and (" + __debitTransFlag2.ToString() + ") and doc_date <= " + MyLib._myGlobal._sqlDateFunction(dateWhereEnd) + __custWhereQuery + __docNoWhereQuery);
            }
            return __queryDoc.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateMode">0=ตามวันครบกำหนด,1=ตามวันที่,</param>
        /// <param name="custCodeBegin"></param>
        /// <param name="custCodeEnd"></param>
        /// <param name="term_1_begin"></param>
        /// <param name="term_1_end"></param>
        /// <param name="term_2_begin"></param>
        /// <param name="term_2_end"></param>
        /// <param name="term_3_begin"></param>
        /// <param name="term_3_end"></param>
        /// <param name="term_4_begin"></param>
        /// <param name="term_4_end"></param>
        /// <param name="dateEnd"></param>
        /// <param name="whereByGrid"></param>
        /// <returns></returns>
        public DataTable _arAgeing(_apArConditionEnum mode, int dateMode, string custCodeBegin, string custCodeEnd, int term_1_begin, int term_1_end, int term_2_begin, int term_2_end, int term_3_begin, int term_3_end, int term_4_begin, int term_4_end, DateTime dateEnd, string whereByGrid)
        {
            string __custWhere = __createWhere(custCodeBegin, custCodeEnd, "code");
            string __dateWhereEnd = MyLib._myGlobal._convertDateToQuery(dateEnd);
            string __dueDate = (dateMode == 0) ? "due_date" : "doc_date";
            //
            StringBuilder __queryDoc = new StringBuilder(this._createQuery(mode, dateMode, __custWhere, "", __dateWhereEnd));
            /*
            // เงินรับล่วงหน้า
            StringBuilder __debitTransFlag3 = new StringBuilder();
            if (mode == _apArConditionControlEnum.ลูกหนี้_อายุลูกหนี้) __debitTransFlag3.Append(_g.g._icTransFlagGlobal._ictransFlag(_g.g._ictransControlTypeEnum.ขาย_รับเงินล่วงหน้า).ToString());
            if (mode == _apArConditionControlEnum.เจ้าหนี้_อายุเจ้าหนี้) __debitTransFlag3.Append(_g.g._icTransFlagGlobal._ictransFlag(_g.g._ictransControlTypeEnum.ซื้อ_จ่ายเงินมัดจำหรือเงินล่วงหน้า).ToString());
            // คืนเงินล่วงหน้า
            StringBuilder __debitTransFlag4 = new StringBuilder();
            if (mode == _apArConditionControlEnum.ลูกหนี้_อายุลูกหนี้) __debitTransFlag4.Append(_g.g._icTransFlagGlobal._ictransFlag(_g.g._ictransControlTypeEnum.ขาย_คืนเงินล่วงหน้า).ToString());
            if (mode == _apArConditionControlEnum.เจ้าหนี้_อายุเจ้าหนี้) __debitTransFlag4.Append(_g.g._icTransFlagGlobal._ictransFlag(_g.g._ictransControlTypeEnum.).ToString());
            //
            StringBuilder __queryDoc3 = new StringBuilder();
            __queryDoc3.Append("select ");
            // 2-ยอดเงินล่วงหน้า
            __queryDoc3.Append("coalesce(sum(coalesce(balance_amount,0)-");
            // 2-หักส่วนเงินล่วงหน้า
            __queryDoc3.Append("(select coalesce(sum(coalesce(dept.balance_amount,0)),0) from ic_trans as dept where dept.trans_flag in (" + _g.g._icTransFlagGlobal._ictransFlag(_g.g._ictransControlTypeEnum.ขาย_คืนเงินล่วงหน้า).ToString() + ")");
            __queryDoc3.Append(" and dept.doc_ref=ic_trans.doc_no and dept.doc_date <= " + MyLib._myGlobal._sqlDateFunction(__dateWhereEnd) + ")");
            __queryDoc3.Append("))");
            __queryDoc3.Append(" from ic_trans where last_status=0 and trans_flag in (" + __debitTransFlag3.ToString() + ") and doc_date <= " + MyLib._myGlobal._sqlDateFunction(__dateWhereEnd) + " and ar_customer.code=ic_trans.cust_code");
             * */
            //
            string __tableName = "";
            string __tableDetailName = "";
            string __creditDay = "";
            if (mode == _apArConditionEnum.ลูกหนี้_อายุลูกหนี้)
            {
                __tableName = "ar_customer";
                __tableDetailName = "ar_customer_detail";
                __creditDay = "ar_customer.code=ar_customer_detail.ar_code";
            }
            if (mode == _apArConditionEnum.เจ้าหนี้_อายุเจ้าหนี้)
            {
                __tableName = "ap_supplier";
                __tableDetailName = "ap_supplier_detail";
                __creditDay = "ap_supplier.code=ap_supplier_detail.ap_code";
            }
            StringBuilder __query3 = new StringBuilder();
            __query3.Append("select ");
            __query3.Append("code ,name_1 ,(select credit_day from " + __tableDetailName + " where (" + __creditDay + ")) as sum_credit_day,");
            __query3.Append("coalesce((select sum(balance_amount) from (" + __queryDoc + ") as x1 where " + MyLib._myGlobal._sqlDateFunction(__dateWhereEnd) + "-" + __dueDate + " < 0),0) as out_due,");
            __query3.Append("coalesce((select sum(balance_amount) from (" + __queryDoc + ") as x2 where " + MyLib._myGlobal._sqlDateFunction(__dateWhereEnd) + "-" + __dueDate + " = 0),0) as sum_period_0,");
            __query3.Append("coalesce((select sum(balance_amount) from (" + __queryDoc + ") as x3 where " + MyLib._myGlobal._sqlDateFunction(__dateWhereEnd) + "-" + __dueDate + " between " + term_1_begin + " and " + term_1_end + "),0) as sum_period_1,");
            __query3.Append("coalesce((select sum(balance_amount) from (" + __queryDoc + ") as x4 where " + MyLib._myGlobal._sqlDateFunction(__dateWhereEnd) + "-" + __dueDate + " between " + term_2_begin + " and " + term_2_end + "),0) as sum_period_2,");
            __query3.Append("coalesce((select sum(balance_amount) from (" + __queryDoc + ") as x5 where " + MyLib._myGlobal._sqlDateFunction(__dateWhereEnd) + "-" + __dueDate + " between " + term_3_begin + " and " + term_3_end + "),0) as sum_period_3,");
            __query3.Append("coalesce((select sum(balance_amount) from (" + __queryDoc + ") as x6 where " + MyLib._myGlobal._sqlDateFunction(__dateWhereEnd) + "-" + __dueDate + " between " + term_4_begin + " and " + term_4_end + "),0) as sum_period_4,");
            __query3.Append("coalesce((select sum(balance_amount) from (" + __queryDoc + ") as x7 where " + MyLib._myGlobal._sqlDateFunction(__dateWhereEnd) + "-" + __dueDate + " > " + term_4_end + "),0) as sum_period_5,");
            //__query3.Append("coalesce((" + __queryDoc3 + "),0) as sum_deposit_receive,");
            __query3.Append("coalesce((select sum(balance_amount) from (" + __queryDoc + ") as x8),0) as sum_total_amount");
            __query3.Append(" from " + __tableName + " " + (((__custWhere.Length == 0) ? "" : " where " + __custWhere)));
            //
            StringBuilder __query4 = new StringBuilder();
            __query4.Append("select code ,name_1 ,out_due,sum_credit_day,sum_period_0,sum_period_1,sum_period_2,sum_period_3,sum_period_4,sum_period_5,sum_total_amount");
            __query4.Append(" from ");
            __query4.Append("(" + __query3.ToString() + ")  as temp1");
            //
            string __query = "select code as " + _g.d.ap_ar_resource._ar_code + ",name_1 as " + _g.d.ap_ar_resource._ar_name + "," +
                "out_due as " + _g.d.ap_ar_resource._out_due + "," +
                "sum_period_0 as " + _g.d.ap_ar_resource._term_0 + "," +
                "sum_period_1 as " + _g.d.ap_ar_resource._term_1 + "," +
                "sum_period_2 as " + _g.d.ap_ar_resource._term_2 + "," +
                "sum_period_3 as " + _g.d.ap_ar_resource._term_3 + "," +
                "sum_period_4 as " + _g.d.ap_ar_resource._term_4 + "," +
                "sum_period_5 as " + _g.d.ap_ar_resource._term_5 + "," +
                //"sum_deposit_receive as " + _g.d.ap_ar_resource._total_deposit + "," +
                "sum_total_amount as " + _g.d.ap_ar_resource._ar_balance + " from " +
                "(" + __query4.ToString() + ") as temp2 " +
                " where out_due<>0 or sum_period_0<>0 or sum_period_1<>0 or sum_period_2<>0 or sum_period_3<>0 or sum_period_4<>0 or sum_period_5<>0 or sum_total_amount<>0";
            return this.__myFrameWork._queryShort(__query.ToString()).Tables[0];
        }

        public DataTable _arAgeingDoc(_apArConditionEnum mode, int dateMode, string arCodeBegin, string arCodeEnd, int term_1_begin, int term_1_end, int term_2_begin, int term_2_end, int term_3_begin, int term_3_end, int term_4_begin, int term_4_end, DateTime dateEnd, string whereByGrid)
        {
            string __custWhere = __createWhere(arCodeBegin, arCodeEnd, "ic_trans.cust_code");
            string __dateWhereEnd = MyLib._myGlobal._convertDateToQuery(dateEnd);
            string __dueDate = (dateMode == 0) ? "due_date" : "doc_date";
            string __findName = "";
            string __orderBy = "";
            switch (_apAr._apArCheck(mode))
            {
                case _apArEnum.ลูกหนี้:
                    __findName = "select name_1 from ar_customer where ar_customer.code";
                    break;
                default:
                    __findName = "select name_1 from ap_supplier where ap_supplier.code";
                    break;
            }
            switch (mode)
            {
                case _apArConditionEnum.เจ้าหนี้_อายุเจ้าหนี้_แยกลูกหนี้_แยกเอกสาร:
                case _apArConditionEnum.ลูกหนี้_อายุลูกหนี้_แยกลูกหนี้_แยกเอกสาร:
                    __orderBy = _g.d.ap_ar_resource._ar_code + "," + _g.d.ap_ar_resource._doc_date + "," + _g.d.ap_ar_resource._doc_no;
                    break;
                default:
                    __orderBy = _g.d.ap_ar_resource._doc_date;
                    break;
            }
            //
            StringBuilder __queryDoc = new StringBuilder(this._createQuery(mode, dateMode, __custWhere, "", __dateWhereEnd));
            //
            string __queryDetail = "select cust_code as " + _g.d.ap_ar_resource._ar_code + "," +
                "doc_no as " + _g.d.ap_ar_resource._doc_no + "," +
                "doc_date as " + _g.d.ap_ar_resource._doc_date + "," +
                "due_date as " + _g.d.ap_ar_resource._due_date + "," +
                "amount as " + _g.d.ap_ar_resource._amount + "," +
                "doc_type as " + _g.d.ap_ar_resource._doc_type + "," +
                "case when " + MyLib._myGlobal._sqlDateFunction(__dateWhereEnd) + "-" + __dueDate + " < 0 then balance_amount else 0 end as " + _g.d.ap_ar_resource._out_due + "," +
                "case when " + MyLib._myGlobal._sqlDateFunction(__dateWhereEnd) + "-" + __dueDate + " = 0 then balance_amount else 0 end as " + _g.d.ap_ar_resource._term_0 + "," +
                "case when " + MyLib._myGlobal._sqlDateFunction(__dateWhereEnd) + "-" + __dueDate + " between " + term_1_begin + " and " + term_1_end + " then balance_amount else 0 end as " + _g.d.ap_ar_resource._term_1 + "," +
                "case when " + MyLib._myGlobal._sqlDateFunction(__dateWhereEnd) + "-" + __dueDate + " between " + term_2_begin + " and " + term_2_end + " then balance_amount else 0 end as " + _g.d.ap_ar_resource._term_2 + "," +
                "case when " + MyLib._myGlobal._sqlDateFunction(__dateWhereEnd) + "-" + __dueDate + " between " + term_3_begin + " and " + term_3_end + " then balance_amount else 0 end as " + _g.d.ap_ar_resource._term_3 + "," +
                "case when " + MyLib._myGlobal._sqlDateFunction(__dateWhereEnd) + "-" + __dueDate + " between " + term_4_begin + " and " + term_4_end + " then balance_amount else 0 end as " + _g.d.ap_ar_resource._term_4 + "," +
                "case when " + MyLib._myGlobal._sqlDateFunction(__dateWhereEnd) + "-" + __dueDate + " > " + term_4_end + " then balance_amount else 0 end as " + _g.d.ap_ar_resource._term_5 + "," +
                "case when " + MyLib._myGlobal._sqlDateFunction(__dateWhereEnd) + "-" + __dueDate + " > 0  then " + MyLib._myGlobal._sqlDateFunction(__dateWhereEnd) + "-" + __dueDate + " else 0 end as " + _g.d.ap_ar_resource._due_day + "," +
                "balance_amount as " + _g.d.ap_ar_resource._ar_balance + " from " +
                "(" + __queryDoc.ToString() + ") as temp2 ";
            //
            string __query = "select " + _g.d.ap_ar_resource._ar_code + "," +
                "(" + __findName + "=" + _g.d.ap_ar_resource._ar_code + ") as " + _g.d.ap_ar_resource._ar_name + "," +
                _g.d.ap_ar_resource._doc_no + "," +
                _g.d.ap_ar_resource._doc_date + "," +
                _g.d.ap_ar_resource._due_date + "," +
                _g.d.ap_ar_resource._doc_type + "," +
                _g.d.ap_ar_resource._due_day + "," +
                _g.d.ap_ar_resource._out_due + "," +
                _g.d.ap_ar_resource._amount + "," +
                _g.d.ap_ar_resource._term_0 + "," +
                _g.d.ap_ar_resource._term_1 + "," +
                _g.d.ap_ar_resource._term_2 + "," +
                _g.d.ap_ar_resource._term_3 + "," +
                _g.d.ap_ar_resource._term_4 + "," +
                _g.d.ap_ar_resource._term_5 + "," +
                _g.d.ap_ar_resource._ar_balance + " from (" + __queryDetail + ") as temp3 " +
                " where " +
                _g.d.ap_ar_resource._out_due + "<>0 or " +
                _g.d.ap_ar_resource._term_0 + "<>0 or " +
                _g.d.ap_ar_resource._term_1 + "<>0 or " +
                _g.d.ap_ar_resource._term_2 + "<>0 or " +
                _g.d.ap_ar_resource._term_3 + "<>0 or " +
                _g.d.ap_ar_resource._term_4 + "<>0 or " +
                _g.d.ap_ar_resource._term_5 + "<>0 or " +
                _g.d.ap_ar_resource._ar_balance + "<>0 order by " + __orderBy;
            //
            DataTable __getData = this.__myFrameWork._queryShort(__query.ToString()).Tables[0];
            DataTable __result = new DataTable("Result");
            __result.Columns.Add(_g.d.ap_ar_resource._ar_code, typeof(String));
            __result.Columns.Add(_g.d.ap_ar_resource._ar_name, typeof(String));
            __result.Columns.Add(_g.d.ap_ar_resource._doc_date, typeof(String));
            __result.Columns.Add(_g.d.ap_ar_resource._doc_no, typeof(String));
            __result.Columns.Add(_g.d.ap_ar_resource._due_date, typeof(String));
            __result.Columns.Add(_g.d.ap_ar_resource._due_day, typeof(Decimal));
            __result.Columns.Add(_g.d.ap_ar_resource._doc_type, typeof(String));
            __result.Columns.Add(_g.d.ap_ar_resource._out_due, typeof(Decimal));
            __result.Columns.Add(_g.d.ap_ar_resource._amount, typeof(Decimal));
            __result.Columns.Add(_g.d.ap_ar_resource._term_0, typeof(Decimal));
            __result.Columns.Add(_g.d.ap_ar_resource._term_1, typeof(Decimal));
            __result.Columns.Add(_g.d.ap_ar_resource._term_2, typeof(Decimal));
            __result.Columns.Add(_g.d.ap_ar_resource._term_3, typeof(Decimal));
            __result.Columns.Add(_g.d.ap_ar_resource._term_4, typeof(Decimal));
            __result.Columns.Add(_g.d.ap_ar_resource._term_5, typeof(Decimal));
            __result.Columns.Add(_g.d.ap_ar_resource._ar_balance, typeof(Decimal));
            //
            int __arCodeColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._ar_code);
            int __arNameColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._ar_name);
            int __docDateColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._doc_date);
            int __docNoColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._doc_no);
            int __dueDateColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._due_date);
            int __dueDayColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._due_day);
            int __docTypeColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._doc_type);
            int __outDueColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._out_due);
            int __amountColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._amount);
            int __term0ColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._term_0);
            int __term1ColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._term_1);
            int __term2ColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._term_2);
            int __term3ColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._term_3);
            int __term4ColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._term_4);
            int __term5ColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._term_5);
            int __arBalanceColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._ar_balance);
            for (int __row = 0; __row < __getData.Rows.Count; __row++)
            {
                DataRow __dataRow = __getData.Rows[__row];
                DateTime __docDate = MyLib._myGlobal._convertDateFromQuery(__dataRow[__docDateColumnNumber].ToString());
                DateTime __getDueDate = MyLib._myGlobal._convertDateFromQuery(__dataRow[__dueDateColumnNumber].ToString());
                int __docType = MyLib._myGlobal._intPhase(__dataRow[__docTypeColumnNumber].ToString());
                string __docTypeName = _g.g._transFlagGlobal._transName(__docType);
                if (__docTypeName.Equals("*"))
                {
                    __docTypeName = _g.g._transFlagGlobal._transName(__docType);
                }
                //
                __result.Rows.Add(__dataRow[__arCodeColumnNumber].ToString(),
                __dataRow[__arNameColumnNumber].ToString(),
                MyLib._myGlobal._convertDateToString(__docDate, false),
                __dataRow[__docNoColumnNumber].ToString(),
                MyLib._myGlobal._convertDateToString(__getDueDate, false),
                __dataRow[__dueDayColumnNumber],
                __docTypeName,
                __dataRow[__outDueColumnNumber],
                __dataRow[__amountColumnNumber],
                __dataRow[__term0ColumnNumber],
                __dataRow[__term1ColumnNumber],
                __dataRow[__term2ColumnNumber],
                __dataRow[__term3ColumnNumber],
                __dataRow[__term4ColumnNumber],
                __dataRow[__term5ColumnNumber],
                __dataRow[__arBalanceColumnNumber]);
            }
            return __result;
        }

        /// <summary>
        /// คำนวณมูลค่าคงเหลือตามเอกสาร
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="dateMode"></param>
        /// <param name="arCodeBegin"></param>
        /// <param name="arCodeEnd"></param>
        /// <param name="docNoBegin"></param>
        /// <param name="docNoEnd"></param>
        /// <param name="dateEnd"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public DataTable _arBalanceDoc(_g.g._transControlTypeEnum mode, int dateMode, string arCodeBegin, string arCodeEnd, string docNoBegin, string docNoEnd, DateTime dateEnd, string orderBy)
        {
            return _arBalanceDoc(mode, dateMode, arCodeBegin, arCodeEnd, docNoBegin, docNoEnd, dateEnd, orderBy, "");
        }

        public DataTable _arBalanceDoc(_g.g._transControlTypeEnum mode, int dateMode, string arCodeBegin, string arCodeEnd, string docNoBegin, string docNoEnd, DateTime dateEnd, string orderBy, string extraWhere)
        {
            return _arBalanceDoc(mode, dateMode, arCodeBegin, arCodeEnd, docNoBegin, docNoEnd, dateEnd, orderBy, extraWhere, true);

        }

        public DataTable _arBalanceDoc(_g.g._transControlTypeEnum mode, int dateMode, string arCodeBegin, string arCodeEnd, string docNoBegin, string docNoEnd, DateTime dateEnd, string orderBy, string extraWhere, bool checkPayDate)
        {
            try
            {
                SMLERPARAPInfo._apArConditionEnum __processMode = _apArConditionEnum.ว่าง;
                switch (mode)
                {
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                        __processMode = SMLERPARAPInfo._apArConditionEnum.เจ้าหนี้_ยอดคงเหลือตามเอกสาร;
                        break;
                    case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล:
                    case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                        __processMode = SMLERPARAPInfo._apArConditionEnum.ลูกหนี้_ยอดคงเหลือตามเอกสาร;
                        break;
                }
                string __custWhere = __createWhere(arCodeBegin, arCodeEnd, "ic_trans.cust_code");
                string __docNoWhere = __createWhere(docNoBegin, docNoEnd, "ic_trans.doc_no");
                string __dateWhereEnd = MyLib._myGlobal._convertDateToQuery(dateEnd);
                string __dueDate = (dateMode == 0) ? "due_date" : "doc_date";
                string __findName = "";
                string __orderBy = "";
                if (orderBy.Length > 0)
                {
                    orderBy = orderBy + ",";
                }
                switch (_apAr._apArCheck(__processMode))
                {
                    case _apArEnum.ลูกหนี้:
                        __findName = "select name_1 from ar_customer where ar_customer.code";
                        break;
                    default:
                        __findName = "select name_1 from ap_supplier where ap_supplier.code";
                        break;
                }
                switch (mode)
                {
                    default:
                        __orderBy = orderBy + _g.d.ap_ar_resource._doc_date + "," + _g.d.ap_ar_resource._doc_no;
                        break;
                }
                //
                StringBuilder __queryDoc = new StringBuilder(this._createQuery(__processMode, dateMode, __custWhere, __docNoWhere, __dateWhereEnd, checkPayDate));
                //
                string __dueDay = "";
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                if (__myFrameWork._databaseSelectType == MyLib._myGlobal._databaseType.MicrosoftSQL2005 || __myFrameWork._databaseSelectType == MyLib._myGlobal._databaseType.MicrosoftSQL2000)
                {
                    __dueDay = "cast(" + MyLib._myGlobal._sqlDateFunction(__dateWhereEnd) + "-" + __dueDate + " as decimal)";
                }
                if (__myFrameWork._databaseSelectType == MyLib._myGlobal._databaseType.PostgreSql)
                {
                    __dueDay = MyLib._myGlobal._sqlDateFunction(__dateWhereEnd) + "-" + __dueDate;
                }
                string __queryDetail = "select cust_code as " + _g.d.ap_ar_resource._ar_code + "," +
                    "doc_no as " + _g.d.ap_ar_resource._doc_no + "," +
                    "doc_date as " + _g.d.ap_ar_resource._doc_date + "," +
                    "due_date as " + _g.d.ap_ar_resource._due_date + "," +
                    "amount as " + _g.d.ap_ar_resource._amount + "," +
                    "doc_type as " + _g.d.ap_ar_resource._doc_type + "," +
                    "used_status as " + _g.d.ap_ar_resource._status + "," +
                    "case when " + __dueDay + " > 0  then " + __dueDay + " else 0 end as " + _g.d.ap_ar_resource._due_day + "," +
                    "balance_amount as " + _g.d.ap_ar_resource._ar_balance + "," +
                    _g.d.ap_ar_resource._ref_doc_no + " as " + _g.d.ap_ar_resource._ref_doc_no + "," +
                    _g.d.ap_ar_resource._ref_doc_date + " as " + _g.d.ap_ar_resource._ref_doc_date + "," +
                    "branch_code as " + _g.d.ap_ar_resource._branch_code + "," +
                    "tax_doc_no as " + _g.d.ap_ar_resource._tax_doc_no + "," +
                    "tax_doc_date as " + _g.d.ap_ar_resource._tax_doc_date + "," +
                    "remark as " + _g.d.ap_ar_resource._remark +

                    " from " +
                    "(" + __queryDoc.ToString() + ") as temp2 ";
                //
                // toe ดึง doc_ref,doc_no จาก ic_trans

                string __query = "select " + _g.d.ap_ar_resource._ar_code + "," +
                    "(" + __findName + "=" + _g.d.ap_ar_resource._ar_code + ") as " + _g.d.ap_ar_resource._ar_name + "," +
                    //"(" + __findName + "=" + _g.d.ap_ar_resource._ar_code + ") as " + _g.d.ap_ar_resource._ref_doc_no + "," + // ดึง ref_doc_no
                    //"(" + __findName + "=" + _g.d.ap_ar_resource._ar_code + ") as " + _g.d.ap_ar_resource._ref_doc_date + "," + // ดึง ref_doc_date
                    _g.d.ap_ar_resource._doc_no + "," +
                    _g.d.ap_ar_resource._doc_date + "," +
                    _g.d.ap_ar_resource._due_date + "," +
                    _g.d.ap_ar_resource._due_day + "," +
                    _g.d.ap_ar_resource._doc_type + "," +
                    _g.d.ap_ar_resource._status + "," +
                    _g.d.ap_ar_resource._amount + "," +
                    _g.d.ap_ar_resource._ar_balance + "," +
                    _g.d.ap_ar_resource._ref_doc_no + "," +
                    _g.d.ap_ar_resource._ref_doc_date + "," +
                    _g.d.ap_ar_resource._branch_code + "," +
                    "tax_doc_no as " + _g.d.ap_ar_resource._tax_doc_no + "," +
                    "tax_doc_date as " + _g.d.ap_ar_resource._tax_doc_date + "," +
                    "remark as " + _g.d.ap_ar_resource._remark +
                    " from (" + __queryDetail + ") as temp3 " +
                    " where " +
                    _g.d.ap_ar_resource._ar_balance + "<>0 " + ((extraWhere.Length > 0) ? " and " + extraWhere : "") + "order by " + __orderBy;
                //

                DataTable __getData = this.__myFrameWork._queryShort(__query.ToString()).Tables[0];
                DataTable __result = new DataTable("Result");
                __result.Columns.Add(_g.d.ap_ar_resource._ar_code, typeof(String));
                __result.Columns.Add(_g.d.ap_ar_resource._ar_name, typeof(String));
                __result.Columns.Add(_g.d.ap_ar_resource._doc_date, typeof(String));
                __result.Columns.Add(_g.d.ap_ar_resource._doc_no, typeof(String));
                __result.Columns.Add(_g.d.ap_ar_resource._due_date, typeof(String));
                __result.Columns.Add(_g.d.ap_ar_resource._due_day, typeof(Decimal));
                __result.Columns.Add(_g.d.ap_ar_resource._doc_type_number, typeof(String));
                __result.Columns.Add(_g.d.ap_ar_resource._doc_type, typeof(String));
                __result.Columns.Add(_g.d.ap_ar_resource._status, typeof(String));
                __result.Columns.Add(_g.d.ap_ar_resource._amount, typeof(Decimal));
                __result.Columns.Add(_g.d.ap_ar_resource._ar_balance, typeof(Decimal));
                // toe
                __result.Columns.Add(_g.d.ap_ar_resource._ref_doc_no, typeof(String));
                __result.Columns.Add(_g.d.ap_ar_resource._ref_doc_date, typeof(String));
                __result.Columns.Add(_g.d.ap_ar_resource._tax_doc_no, typeof(String));
                __result.Columns.Add(_g.d.ap_ar_resource._tax_doc_date, typeof(String));
                __result.Columns.Add(_g.d.ap_ar_resource._remark, typeof(String));
                //
                int __arCodeColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._ar_code);
                int __arNameColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._ar_name);
                int __docDateColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._doc_date);
                int __docNoColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._doc_no);
                int __dueDateColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._due_date);
                int __dueDayColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._due_day);
                int __docTypeColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._doc_type);
                int __statusColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._status);
                int __amountColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._amount);
                int __arBalanceColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._ar_balance);

                int __refDocNoColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._ref_doc_no);
                int __refDocDateColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._ref_doc_date);
                int __taxDocNoColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._tax_doc_no);
                int __taxDocDateColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._tax_doc_date);

                int __columnRemark = __getData.Columns.IndexOf(_g.d.ap_ar_resource._remark);

                for (int __row = 0; __row < __getData.Rows.Count; __row++)
                {
                    DataRow __dataRow = __getData.Rows[__row];
                    DateTime __docDate = MyLib._myGlobal._convertDateFromQuery(__dataRow[__docDateColumnNumber].ToString());
                    DateTime __getDueDate = MyLib._myGlobal._convertDateFromQuery(__dataRow[__dueDateColumnNumber].ToString());

                    DateTime __getRefDate = MyLib._myGlobal._convertDateFromQuery(__dataRow[__refDocDateColumnNumber].ToString());
                    DateTime __gettaxDate = MyLib._myGlobal._convertDateFromQuery(__dataRow[__taxDocDateColumnNumber].ToString());

                    int __docType = MyLib._myGlobal._intPhase(__dataRow[__docTypeColumnNumber].ToString());
                    int __status = MyLib._myGlobal._intPhase(__dataRow[__statusColumnNumber].ToString());
                    string __docTypeName = _g.g._transFlagGlobal._transName(__docType);
                    if (__docTypeName.Equals("*"))
                    {
                        __docTypeName = _g.g._transFlagGlobal._transName(__docType);
                    }
                    //
                    __result.Rows.Add(__dataRow[__arCodeColumnNumber].ToString(),
                    __dataRow[__arNameColumnNumber].ToString(),
                    MyLib._myGlobal._convertDateToQuery(__docDate),
                    __dataRow[__docNoColumnNumber].ToString(),
                    MyLib._myGlobal._convertDateToQuery(__getDueDate),
                    __dataRow[__dueDayColumnNumber],
                    __docType, __docTypeName,
                    __status,
                    __dataRow[__amountColumnNumber],
                    __dataRow[__arBalanceColumnNumber],
                    __dataRow[__refDocNoColumnNumber],
                    MyLib._myGlobal._convertDateToQuery(__getRefDate),
                    __dataRow[__taxDocNoColumnNumber],
                    MyLib._myGlobal._convertDateToQuery(__gettaxDate),

                    __dataRow[__columnRemark]);
                }
                return __result;
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
                return null;
            }
        }

        public DataTable _depositBalanceDoc(_g.g._depositAdvanceEnum mode, string docNo, int dateMode, string arCodeBegin, string arCodeEnd, string docNoBegin, string docNoEnd, DateTime dateEnd, string orderBy)
        {
            string __custWhere = __createWhere(arCodeBegin, arCodeEnd, "ic_trans.cust_code");
            string __docNoWhere = __createWhere(docNoBegin, docNoEnd, "ic_trans.doc_no");
            string __dateWhereEnd = MyLib._myGlobal._convertDateToQuery(dateEnd);
            string __findName = "";
            string __orderBy = "";
            string __transFlag = "";
            string __docInquiryFilter = "";
            //
            if (__custWhere.Length != 0)
            {
                __custWhere = " and " + __custWhere;
            }
            if (__docNoWhere.Length != 0)
            {
                __docNoWhere = " and " + __docNoWhere;
            }
            //
            if (orderBy.Length > 0)
            {
                orderBy = orderBy + ",";
            }
            switch (mode)
            {
                case _g.g._depositAdvanceEnum.รับเงินล่วงหน้า:
                    __findName = "ic_trans.cust_code||\'~\'||coalesce((select name_1 from ar_customer where ar_customer.code=ic_trans.cust_code),'') as cust_name";
                    __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า).ToString();
                    break;
                case _g.g._depositAdvanceEnum.รับเงินมัดจำ:
                    __findName = "ic_trans.cust_code||\'~\'||coalesce((select name_1 from ar_customer where ar_customer.code=ic_trans.cust_code),'') as cust_name";
                    __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ).ToString();
                    break;
                case _g.g._depositAdvanceEnum.จ่ายเงินล่วงหน้า:
                    __findName = "ic_trans.cust_code||\'~\'||coalesce((select name_1 from ap_supplier where ap_supplier.code=ic_trans.cust_code),'') as cust_name";
                    __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า).ToString();

                    break;
                case _g.g._depositAdvanceEnum.จ่ายเงินมัดจำ:
                    __findName = "ic_trans.cust_code||\'~\'||coalesce((select name_1 from ap_supplier where ap_supplier.code=ic_trans.cust_code),'') as cust_name";
                    __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ).ToString();
                    if (MyLib._myGlobal._OEMVersion.Equals("imex"))
                    {
                        __docInquiryFilter = " and (" + _g.d.ic_trans._inquiry_type + "=0 or (inquiry_type=1 and total_amount-coalesce((select sum(sum_pay_money) from ap_ar_trans_detail where ap_ar_trans_detail.trans_flag=19 and coalesce(ap_ar_trans_detail.last_status,0)=0 and ap_ar_trans_detail.billing_no=ic_trans.doc_no and ap_ar_trans_detail.bill_type=ic_trans.trans_flag ), 0) = 0 ) )"; // or (" + _g.d.ic_trans._inquiry_type + "=1 and balance_amoumt = 0)
                    }
                    break;
            }
            switch (mode)
            {
                default:
                    __orderBy = orderBy + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_no;
                    break;
            }
            //
            StringBuilder __queryDepositUseAmount = new StringBuilder();
            __queryDepositUseAmount.Append("coalesce(( select sum(coalesce(total_amount, 0)) from ( ");

            string __returnFlag = "";
            string __useDocType = "";

            // คืนเงิน
            switch (mode)
            {
                case _g.g._depositAdvanceEnum.รับเงินมัดจำ:
                    __returnFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน).ToString();
                    __useDocType = "6";
                    break;
                case _g.g._depositAdvanceEnum.จ่ายเงินมัดจำ:
                    __returnFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน).ToString();
                    __useDocType = "6";
                    break;
                case _g.g._depositAdvanceEnum.จ่ายเงินล่วงหน้า:
                    __returnFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน).ToString();
                    __useDocType = "5";
                    break;
                case _g.g._depositAdvanceEnum.รับเงินล่วงหน้า:
                    __returnFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน).ToString();
                    __useDocType = "5";
                    break;
            }

            __queryDepositUseAmount.Append(" select total_amount from ic_trans as a where a.doc_no<>\'" + docNo + "\' and a.doc_ref = ic_trans.doc_no and trans_flag = " + __returnFlag + " and coalesce(last_status,0) = 0 ");
            __queryDepositUseAmount.Append(" union all ");
            __queryDepositUseAmount.Append("select amount as total_amount from cb_trans_detail where cb_trans_detail.doc_no<>\'" + docNo + "\' and cb_trans_detail.doc_type = " + __useDocType + " and cb_trans_detail.trans_number = ic_trans.doc_no and coalesce(cb_trans_detail.last_status,0)=0  "); // last status and (select last_status from ic_trans as temp2 where temp2.doc_no = cb_trans_detail.doc_no and temp2.trans_flag = cb_trans_detail.trans_flag)= 0
            __queryDepositUseAmount.Append(") as temp1 ), 0) ");

            // toe ใส่ process ตรงนี้

            StringBuilder __queryDoc = new StringBuilder();
            __queryDoc.Append("select doc_date,doc_no,total_amount,cust_code," + __findName + ",");
            // __queryDoc.Append("coalesce((select sum(amount) from cb_trans_detail where cb_trans_detail.trans_number=ic_trans.doc_no),0) as use_amount"); toe
            __queryDoc.Append(__queryDepositUseAmount.ToString() + " as use_amount");


            __queryDoc.Append(" from ic_trans where coalesce(last_status,0)=0 and trans_flag=" + __transFlag + __docNoWhere + __custWhere + __docInquiryFilter);
            //
            string __query = "select cust_code as " + _g.d.ap_ar_resource._ar_code + "," +
                "cust_name as " + _g.d.ap_ar_resource._ar_name + "," +
                "doc_no as " + _g.d.ap_ar_resource._doc_no + "," +
                "doc_date as " + _g.d.ap_ar_resource._doc_date + "," +
                "total_amount as " + _g.d.ap_ar_resource._amount + "," +
                "use_amount as " + _g.d.ap_ar_resource._use_amount + "," +
                "total_amount-use_amount as " + _g.d.ap_ar_resource._balance_amount + " from (" + __queryDoc + ") as temp3 " +
                " where total_amount-use_amount<>0 order by " + __orderBy;
            //
            DataTable __getData = this.__myFrameWork._queryShort(__query.ToString()).Tables[0];
            DataTable __result = new DataTable("Result");
            __result.Columns.Add(_g.d.ap_ar_resource._ar_code, typeof(String));
            __result.Columns.Add(_g.d.ap_ar_resource._ar_name, typeof(String));
            __result.Columns.Add(_g.d.ap_ar_resource._doc_date, typeof(String));
            __result.Columns.Add(_g.d.ap_ar_resource._doc_no, typeof(String));
            __result.Columns.Add(_g.d.ap_ar_resource._amount, typeof(Decimal));
            __result.Columns.Add(_g.d.ap_ar_resource._use_amount, typeof(Decimal));
            __result.Columns.Add(_g.d.ap_ar_resource._balance_amount, typeof(Decimal));
            //
            int __arCodeColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._ar_code);
            int __arNameColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._ar_name);
            int __docDateColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._doc_date);
            int __docNoColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._doc_no);
            int __amountColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._amount);
            int __useAmountColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._use_amount);
            int __arBalanceColumnNumber = __getData.Columns.IndexOf(_g.d.ap_ar_resource._balance_amount);
            for (int __row = 0; __row < __getData.Rows.Count; __row++)
            {
                DataRow __dataRow = __getData.Rows[__row];
                DateTime __docDate = MyLib._myGlobal._convertDateFromQuery(__dataRow[__docDateColumnNumber].ToString());
                //
                __result.Rows.Add(__dataRow[__arCodeColumnNumber].ToString(),
                __dataRow[__arNameColumnNumber].ToString(),
                MyLib._myGlobal._convertDateToQuery(__docDate),
                __dataRow[__docNoColumnNumber].ToString(),
                __dataRow[__amountColumnNumber],
                __dataRow[__useAmountColumnNumber],
                __dataRow[__arBalanceColumnNumber]);
            }
            return __result;
        }

        public DataTable _chqBalance(_g.g._transControlTypeEnum mode, string custCodeBegin, string custCodeEnd, string extraWhere)
        {

            DataTable __result = new DataTable("Result");

            string __custWhere = __createWhere(custCodeBegin, custCodeEnd, "ap_ar_code");

            int __ap_ar_type = 0;
            switch (_g.g._transType(mode))
            {
                case _g.g._transTypeEnum.ขาย_ลูกหนี้:
                case _g.g._transTypeEnum.ลูกหนี้:
                    __ap_ar_type = 1;
                    break;
                default:
                    __ap_ar_type = 2;
                    break;
            }

            string __fieldList = _g.d.cb_trans_detail._doc_no + "," + _g.d.cb_trans_detail._ap_ar_code + "," + _g.d.cb_trans_detail._trans_number + ", " + _g.d.cb_trans_detail._pass_book_code + "," + _g.d.cb_trans_detail._chq_due_date + ", " + _g.d.cb_trans_detail._sum_amount;

            string __queryBankName = "bank_code||\'~\'||coalesce((select name_1 from erp_bank where erp_bank.code=temp2.bank_code),'') as " + _g.d.cb_trans_detail._bank_code;
            string __queryBankBranch = "bank_branch||\'~\'||coalesce((select name_1 from erp_bank_branch where erp_bank_branch.code=temp2.bank_branch and erp_bank_branch.bank_code=temp2.bank_code),'') as " + _g.d.cb_trans_detail._bank_branch;

            string __fieldUsedAmount = " (select sum(amount) from cb_trans_detail where temp2.trans_number = cb_trans_detail.trans_number and doc_type = 2 and last_status = 0 and temp2.chq_due_date =cb_trans_detail.chq_due_date and temp2.bank_code=cb_trans_detail.bank_code and coalesce(temp2.bank_branch, '')=coalesce(cb_trans_detail.bank_branch, '') ) ";
            string __fieldBalanceAmount = " (sum_amount - " + __fieldUsedAmount + ") as " + _g.d.cb_trans_detail._balance_amount;


            StringBuilder __query = new StringBuilder();
            __query.Append("select " + __fieldList + "," + __fieldUsedAmount + " as used_amount ," + __fieldBalanceAmount);
            __query.Append("," + __queryBankName + " ");
            __query.Append("," + __queryBankBranch + " ");
            __query.Append(" from cb_trans_detail as temp2 where doc_type = 2 and last_status = 0 and sum_amount > 0 and chq_on_hand = 0 ");

            __query.Append(" and ( sum_amount -  (select sum(amount) from cb_trans_detail where temp2.trans_number = cb_trans_detail.trans_number and doc_type = 2 and last_status = 0 and temp2.chq_due_date =cb_trans_detail.chq_due_date and temp2.bank_code=cb_trans_detail.bank_code and coalesce(temp2.bank_branch, '')=coalesce(cb_trans_detail.bank_branch, '') )) > 0 ");
            __query.Append(" and " + _g.d.cb_trans_detail._ap_ar_type + "=" + __ap_ar_type);

            if (extraWhere.Length > 0)
            {
                __query.Append(" and " + extraWhere);
            }

            if (MyLib._myGlobal._OEMVersion != "imex" && MyLib._myGlobal._OEMVersion != "ims")
            {
                if (__custWhere.Length > 0)
                    __query.Append(" and " + __custWhere);

            }

            try
            {
                DataTable __getData = this.__myFrameWork._queryShort(__query.ToString()).Tables[0];

                __result.Columns.Add(_g.d.cb_trans_detail._chq_number, typeof(String));
                __result.Columns.Add(_g.d.cb_trans_detail._chq_due_date, typeof(String));
                __result.Columns.Add(_g.d.cb_trans_detail._chq_amount, typeof(Decimal));
                __result.Columns.Add(_g.d.cb_trans_detail._balance_amount, typeof(Decimal));
                __result.Columns.Add(_g.d.cb_trans_detail._bank_code, typeof(String));
                __result.Columns.Add(_g.d.cb_trans_detail._bank_branch, typeof(String));
                __result.Columns.Add(_g.d.cb_trans_detail._pass_book_code, typeof(String));
                __result.Columns.Add(_g.d.cb_trans_detail._doc_no, typeof(String));

                int __chqNumberColumnNumber = __getData.Columns.IndexOf(_g.d.cb_trans_detail._trans_number);
                int __chqDueDateColumnNumber = __getData.Columns.IndexOf(_g.d.cb_trans_detail._chq_due_date);
                int __chqAmountColumnNumber = __getData.Columns.IndexOf(_g.d.cb_trans_detail._sum_amount);
                int __balanceAmountColumnNumber = __getData.Columns.IndexOf(_g.d.cb_trans_detail._balance_amount);
                int __bankCodeColumnName = __getData.Columns.IndexOf(_g.d.cb_trans_detail._bank_code);
                int __bankBranchColumnName = __getData.Columns.IndexOf(_g.d.cb_trans_detail._bank_branch);
                int __passBookColumnName = __getData.Columns.IndexOf(_g.d.cb_trans_detail._pass_book_code);

                int __columnDocNo = __getData.Columns.IndexOf(_g.d.cb_trans_detail._doc_no); // for doc ref

                for (int __row = 0; __row < __getData.Rows.Count; __row++)
                {
                    DataRow __dataRow = __getData.Rows[__row];

                    DateTime __chq_due_date = MyLib._myGlobal._convertDateFromQuery(__dataRow[__chqDueDateColumnNumber].ToString()); //__dataRow[__chqDueDateColumnNumber].ToString();

                    __result.Rows.Add(__dataRow[__chqNumberColumnNumber].ToString(),
                    MyLib._myGlobal._convertDateToQuery(__chq_due_date),
                    __dataRow[__chqAmountColumnNumber],
                    __dataRow[__balanceAmountColumnNumber],
                    __dataRow[__bankCodeColumnName],
                    __dataRow[__bankBranchColumnName],
                    __dataRow[__passBookColumnName],
                    __dataRow[__columnDocNo]);
                }

                return __result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            return __result;
        }
    }
}
