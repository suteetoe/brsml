using System;
using System.Data;
using System.Text;
using System.Xml;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;
using System.Threading;

namespace SMLProcess
{
    public class _docFlowThread
    {
        private _g.g._transControlTypeEnum _transFlagEnum = _g.g._transControlTypeEnum.ว่าง;
        private string _itemCodePack = "";
        private string _docNoPack = "";

        public string _clearFlowStatusQuery(string itemCodePack, string docNoPack)
        {
            String __docNoPackIcTransWhere = "";
            if (docNoPack.Length > 0)
            {
                __docNoPackIcTransWhere = _g.d.ic_trans._doc_no + " in (" + docNoPack + ") or ";
            }
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __query = new StringBuilder();
            // ภาษีมูลค่าเพิ่ม
            switch (__myFrameWork._databaseSelectType)
            {
                case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                    //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal_vat_buy._table + " set " + _g.d.gl_journal_vat_buy._vat_effective_period + "=month(" + _g.d.gl_journal_vat_buy._vat_date + ") where " + _g.d.gl_journal_vat_buy._vat_effective_period + " < 1"));
                    //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal_vat_buy._table + " set " + _g.d.gl_journal_vat_buy._vat_effective_year + "=year(" + _g.d.gl_journal_vat_buy._vat_date + ")+" + MyLib._myGlobal._year_add.ToString() + " where " + _g.d.gl_journal_vat_buy._vat_effective_year + "=0"));
                    //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal_vat_sale._table + " set " + _g.d.gl_journal_vat_sale._vat_effective_period + "=month(" + _g.d.gl_journal_vat_sale._vat_date + ") where " + _g.d.gl_journal_vat_sale._vat_effective_period + "< 1"));
                    //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal_vat_sale._table + " set " + _g.d.gl_journal_vat_sale._vat_effective_year + "=year(" + _g.d.gl_journal_vat_sale._vat_date + ")+" + MyLib._myGlobal._year_add.ToString() + " where " + _g.d.gl_journal_vat_sale._vat_effective_year + "=0"));
                    break;
                default:
                    //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal_vat_buy._table + " set " + _g.d.gl_journal_vat_buy._vat_effective_period + "=EXTRACT(month from " + _g.d.gl_journal_vat_buy._vat_date + ") where " + _g.d.gl_journal_vat_buy._vat_effective_period + " < 1"));
                    //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal_vat_buy._table + " set " + _g.d.gl_journal_vat_buy._vat_effective_year + "=EXTRACT(year from " + _g.d.gl_journal_vat_buy._vat_date + ")+" + MyLib._myGlobal._year_add.ToString() + " where " + _g.d.gl_journal_vat_buy._vat_effective_year + "=0"));
                    //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal_vat_sale._table + " set " + _g.d.gl_journal_vat_sale._vat_effective_period + "=EXTRACT(month from " + _g.d.gl_journal_vat_sale._vat_date + ") where " + _g.d.gl_journal_vat_sale._vat_effective_period + "< 1"));
                    //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal_vat_sale._table + " set " + _g.d.gl_journal_vat_sale._vat_effective_year + "=EXTRACT(year from " + _g.d.gl_journal_vat_sale._vat_date + ")+" + MyLib._myGlobal._year_add.ToString() + " where " + _g.d.gl_journal_vat_sale._vat_effective_year + "=0"));
                    break;
            }
            //
            if (MyLib._myGlobal._subVersion == 1)
            {
                // ไม่ได้ใช้ เพราเป็นเป็น default แล้ว
                /*__query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._used_status + "=0 where " + _g.d.ic_trans._used_status + " is null"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._used_status_2 + "=0 where " + _g.d.ic_trans._used_status_2 + " is null"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._last_status + "=0 where " + _g.d.ic_trans._last_status + " is null"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._doc_success + "=0 where " + _g.d.ic_trans._doc_success + " is null"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._not_approve_1 + "=0 where " + _g.d.ic_trans._not_approve_1 + " is null"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._on_hold + "=0 where " + _g.d.ic_trans._on_hold + " is null"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._approve_status + "=0 where " + _g.d.ic_trans._approve_status + " is null"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._expire_status + "=0 where " + _g.d.ic_trans._expire_status + " is null"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._is_pos + "=0 where " + _g.d.ic_trans._is_pos + " is null"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._pos_bill_type + "=0 where " + _g.d.ic_trans._pos_bill_type + " is null"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._pos_bill_change + "=0 where " + _g.d.ic_trans._pos_bill_change + " is null"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._auto_create + "=0 where " + _g.d.ic_trans._auto_create + " is null"));
                //
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans_detail._table + " set " + _g.d.ic_trans_detail._is_pos + "=0 where " + _g.d.ic_trans_detail._is_pos + " is null"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans_detail._table + " set " + _g.d.ic_trans_detail._auto_create + "=0 where " + _g.d.ic_trans_detail._auto_create + " is null"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans_detail._table + " set " + _g.d.ic_trans_detail._last_status + "=0 where " + _g.d.ic_trans_detail._last_status + " is null"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ap_ar_trans_detail._table + " set " + _g.d.ap_ar_trans_detail._last_status + "=0 where " + _g.d.ap_ar_trans_detail._last_status + " is null"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans_serial_number._table + " set " + _g.d.ic_trans_serial_number._last_status + "=0 where " + _g.d.ic_trans_serial_number._last_status + " is null"));
                //
                string __itemCode = (itemCodePack.Length == 0) ? "" : _g.d.ic_trans_detail._item_code + " in (" + itemCodePack + ") and ";
                String __unitStand = "select stand_value from ic_unit_use where ic_unit_use.ic_code=ic_trans_detail.item_code and ic_unit_use.code=ic_trans_detail.unit_code";
                String __unitDivide = "select divide_value from ic_unit_use where ic_unit_use.ic_code=ic_trans_detail.item_code and ic_unit_use.code=ic_trans_detail.unit_code";
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery(("update ic_trans_detail set stand_value=coalesce((" + __unitStand + "),1.0) where " + __itemCode + "(stand_value<>coalesce((" + __unitStand + "),1.0))")));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery(("update ic_trans_detail set divide_value=coalesce((" + __unitDivide + "),1.0) where " + __itemCode + "(divide_value<>coalesce((" + __unitDivide + "),1.0))")));
                //
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_inventory._table + " set " + _g.d.ic_inventory._ic_serial_no + "=0 where " + _g.d.ic_inventory._ic_serial_no + " is null"));
                string __subQeury = "(select item_type from ic_inventory where ic_inventory.code=ic_trans_detail.item_code)";
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans_detail._table + " set " + _g.d.ic_trans_detail._item_type + "=" + __subQeury + " where " + __itemCode + "(" + _g.d.ic_trans_detail._item_type + "<>" + __subQeury + ")"));
                __subQeury = "(select ic_serial_no from ic_inventory where ic_inventory.code=ic_trans_detail.item_code)";
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans_detail._table + " set " + _g.d.ic_trans_detail._is_serial_number + "=" + __subQeury + " where " + __itemCode + "(" + _g.d.ic_trans_detail._is_serial_number + "<>" + __subQeury + ")"));
                __subQeury = "(select tax_type from ic_inventory where ic_inventory.code=ic_trans_detail.item_code)";
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans_detail._table + " set " + _g.d.ic_trans_detail._tax_type + "=" + __subQeury + " where " + __itemCode + "(" + _g.d.ic_trans_detail._tax_type + "<>" + __subQeury + ")"));
                __subQeury = "(select is_pos from ic_trans where ic_trans.doc_no=ic_trans_detail.doc_no and trans_flag=44)";
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans_detail._table + " set " + _g.d.ic_trans_detail._is_pos + "=" + __subQeury + " where " + __itemCode + "(" + _g.d.ic_trans_detail._is_pos + "<>" + __subQeury + ")"));
                // กรณีขาย POS กำหนดให้เป็นภาษีรวมใน
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans_detail._table + " set " + _g.d.ic_trans_detail._vat_type + "=1 where " + _g.d.ic_trans_detail._is_pos + "=1 and " + _g.d.ic_trans_detail._vat_type + "<>1"));
                //
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ap_ar_trans set used_status=0 where used_status is null"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ap_ar_trans set last_status=0 where last_status is null"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ap_ar_trans set doc_success=0 where doc_success is null"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update cb_trans_detail set last_status=0 where last_status is null"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans_detail set shelf_code='' where shelf_code is null"));
                //
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal._table + " set " + _g.d.gl_journal._is_pass + "=0 where " + _g.d.gl_journal._is_pass + " is null"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal_detail._table + " set " + _g.d.gl_journal._is_pass + "=0 where " + _g.d.gl_journal_detail._is_pass + " is null"));
                //
                // ชั่วคราว
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set advance_amount=0 where advance_amount is null"));*/
                //
            }

            switch (this._transFlagEnum)
            {
                case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล:
                case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล_ยกเลิก:
                case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก:

                case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล:
                case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล_ยกเลิก:
                case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก:

                    string __subQuery = "(coalesce((select last_status from ap_ar_trans where ap_ar_trans.doc_no=ap_ar_trans_detail.doc_no and ap_ar_trans.trans_flag=ap_ar_trans_detail.trans_flag and ap_ar_trans.trans_flag = " + _g.g._transFlagGlobal._transFlag(this._transFlagEnum) + " ),0))";
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ap_ar_trans_detail set last_status=" + __subQuery + " where " + ((docNoPack.Length > 0) ? _g.d.ap_ar_trans_detail._doc_no + " in (" + docNoPack + ")" + " and " : "") + " trans_flag = " + _g.g._transFlagGlobal._transFlag(this._transFlagEnum) + " and last_status<>" + __subQuery));
                    break;
            }
            //
            return __query.ToString();
        }

        /*public string _apQueryList()
        {
            // ประมวลผลยอดคงเหลือ
            String __subQquery1 = "(((select coalesce(sum(total_amount),0) from ic_trans where (trans_type=1) and (trans_flag in (12,14,17) ) and (ap_supplier.code=ic_trans.cust_code))"///,
                    + "-(select coalesce(sum(total_amount),0) from ic_trans where (trans_type=1) and (trans_flag in (13,15,16)) and (ap_supplier.code=ic_trans.cust_code) ))"///
                    + "+((select coalesce(sum(total_debt_amount),0) from ap_ar_trans where (trans_type=1) and (trans_flag in (1,3,6,7,9,12)) and (ap_supplier.code=ap_ar_trans.cust_code) )"///
                    + "-(select coalesce(sum(total_debt_amount),0) from ap_ar_trans where (trans_type=1) and (trans_flag in (2,4,5,8,10,11) ) and (ap_supplier.code=ap_ar_trans.cust_code) )))"; ///
            String __query1 = "update ap_supplier set debt_balance=(" + __subQquery1 + ") where debt_balance<>(" + __subQquery1 + ")";
            return MyLib._myUtil._convertTextToXmlForQuery(__query1);
        }*/

        public string _apArFlowQueryList()
        {
            StringBuilder __query = new StringBuilder();

            string __processFlag = "213,19,235,239";
            string __cancelFlag = "214,23,236,240";

            switch (this._transFlagEnum)
            {
                case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                    __processFlag = "239";
                    __cancelFlag = "240";
                    break;
            }
            //
            // ใบรับวางบิล,ใบจ้ายชำระหนี้
            //
            // last_status : 213=ใบรับวางบิล,19=จ่ายชำระหนี้,235=ใบวางบิล,239=รับชำระหนี้
            // 214=ยกเลิกใบรับวางบิล,23=ยกเลิกจ่ายชำระหนี้,236=ยกเลิกใบวางบิล,240=ยกเลิกรับชำระหนี้
            String __subQuery = "(select distinct doc_ref from ap_ar_trans where doc_ref is not null and trans_flag in (" + __cancelFlag + "))";
            //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ap_ar_trans set last_status=1 where last_status<>1 and trans_flag in (" + __processFlag + ") and doc_no in " + __subQuery));
            //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ap_ar_trans set last_status=0 where last_status<>0 and trans_flag in (" + __processFlag + ") and doc_no not in " + __subQuery));
            // toe update last_status cb_trans & cb_trans_detail
            // Used : 213=ใบรับวางบิล,19=จ่ายชำระหนี้,235=ใบวางบิล,239=รับชำระหนี้
            // 214=ยกเลิกใบรับวางบิล,23=ยกเลิกจ่ายชำระหนี้,236=ยกเลิกใบวางบิล,240=ยกเลิกรับชำระหนี้
            // อ้างอิงจากหัวเอกสาร
            __subQuery = "(select distinct doc_ref from ap_ar_trans_detail where last_status=0 and doc_ref is not null and trans_flag in (" + __processFlag + "," + __cancelFlag + "))";
            //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ap_ar_trans set used_status=1 where used_status<>1 and trans_flag in (" + __processFlag + ") and doc_no in " + __subQuery));
            //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ap_ar_trans set used_status=0 where used_status<>0 and trans_flag in (" + __processFlag + ") and doc_no not in " + __subQuery));

            // toe 
            // ใบวางบิล อ้างอิงครบแล้ว doc_success = 1
            if (this._transFlagEnum == _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้)
            {
                //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ap_ar_trans set doc_success = 1 where doc_success <> 1  and trans_flag = 235 and last_status = 0 and coalesce((select sum(coalesce(sum_pay_money, 0)) from ap_ar_trans_detail where ap_ar_trans_detail.doc_no = ap_ar_trans.doc_no and ap_ar_trans.trans_flag = 235 ), 0)-coalesce((select sum(coalesce(sum_pay_money, 0)) from ap_ar_trans_detail as x1 where x1.trans_flag = 239  and x1.last_status = 0 and exists (select billing_no from ap_ar_trans_detail as x2 where x2.doc_no = ap_ar_trans.doc_no and x2.trans_flag = ap_ar_trans.trans_flag and x1.billing_no = x2.billing_no and x1.bill_type = x2.bill_type)), 0)= 0 and coalesce((select count(doc_no) from ap_ar_trans_detail as x1 where x1.trans_flag = 239  and x1.last_status = 0 and exists (select billing_no from ap_ar_trans_detail as x2 where x2.doc_no = ap_ar_trans.doc_no and x2.trans_flag = ap_ar_trans.trans_flag and x1.billing_no = x2.billing_no and x1.bill_type = x2.bill_type)), 0) > 0"));
                //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ap_ar_trans set doc_success = 0 where doc_success <> 0  and trans_flag = 235 and last_status = 0 and ( coalesce((select sum(coalesce(sum_pay_money, 0)) from ap_ar_trans_detail where ap_ar_trans_detail.doc_no = ap_ar_trans.doc_no and ap_ar_trans.trans_flag = 235 ), 0)-coalesce((select sum(coalesce(sum_pay_money, 0)) from ap_ar_trans_detail as x1 where x1.trans_flag = 239  and x1.last_status = 0 and exists (select billing_no from ap_ar_trans_detail as x2 where x2.doc_no = ap_ar_trans.doc_no and x2.trans_flag = ap_ar_trans.trans_flag and x1.billing_no = x2.billing_no and x1.bill_type = x2.bill_type)), 0)<> 0 or coalesce((select count(doc_no) from ap_ar_trans_detail as x1 where x1.trans_flag = 239  and x1.last_status = 0 and exists (select billing_no from ap_ar_trans_detail as x2 where x2.doc_no = ap_ar_trans.doc_no and x2.trans_flag = ap_ar_trans.trans_flag and x1.billing_no = x2.billing_no and x1.bill_type = x2.bill_type)), 0) = 0  )"));
            }

            // ใบรับวางบิล อ้างอิงครบแล้ว doc_status = 1
            if (this._transFlagEnum == _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้)
            {
                //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ap_ar_trans set doc_success = 1 where doc_success <> 1  and trans_flag = 213 and last_status = 0 and coalesce((select sum(coalesce(sum_pay_money, 0)) from ap_ar_trans_detail where ap_ar_trans_detail.doc_no = ap_ar_trans.doc_no and ap_ar_trans.trans_flag = 213 ), 0)-coalesce((select sum(coalesce(sum_pay_money, 0)) from ap_ar_trans_detail as x1 where x1.trans_flag = 19  and x1.last_status = 0 and exists (select billing_no from ap_ar_trans_detail as x2 where x2.doc_no = ap_ar_trans.doc_no and x2.trans_flag = ap_ar_trans.trans_flag and x1.billing_no = x2.billing_no and x1.bill_type = x2.bill_type)), 0)= 0"));
                //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ap_ar_trans set doc_success = 0 where doc_success <> 0  and trans_flag = 213 and last_status = 0 and coalesce((select sum(coalesce(sum_pay_money, 0)) from ap_ar_trans_detail where ap_ar_trans_detail.doc_no = ap_ar_trans.doc_no and ap_ar_trans.trans_flag = 213 ), 0)-coalesce((select sum(coalesce(sum_pay_money, 0)) from ap_ar_trans_detail as x1 where x1.trans_flag = 19  and x1.last_status = 0 and exists (select billing_no from ap_ar_trans_detail as x2 where x2.doc_no = ap_ar_trans.doc_no and x2.trans_flag = ap_ar_trans.trans_flag and x1.billing_no = x2.billing_no and x1.bill_type = x2.bill_type)), 0)<> 0"));
            }

            // ic_trans
            if (this._transFlagEnum == _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้ ||
                this._transFlagEnum == _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้ ||
                this._transFlagEnum == _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล ||
                this._transFlagEnum == _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล)
            {
                StringBuilder __transFlag = new StringBuilder();
                switch (this._transFlagEnum)
                {
                    case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                    case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล:
                        __transFlag.Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + ",");
                        __transFlag.Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้).ToString() + ",");
                        __transFlag.Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้).ToString());
                        break;
                    case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล:
                        __transFlag.Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ).ToString() + ",");
                        __transFlag.Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด).ToString() + ",");
                        __transFlag.Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด).ToString());
                        break;
                }

                __subQuery = "(select distinct billing_no from ap_ar_trans_detail where last_status=0 and billing_no is not null and trans_flag in (" + __processFlag + "))";

                // __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status_2=1 where used_status_2<>1 and trans_flag in (" + __transFlag.ToString() + ") and doc_no in " + __subQuery));
                // __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status_2=0 where used_status_2<>0 and trans_flag in (" + __transFlag.ToString() + ") and doc_no not in " + __subQuery));
            }
            return __query.ToString();
        }

        private string _icFlowQueryListFormat(_g.g._transControlTypeEnum source, _g.g._transControlTypeEnum target)
        {
            StringBuilder __query = new StringBuilder();


            string __docRefCancel = " select " + _g.d.ic_trans._doc_ref + " from " + _g.d.ic_trans._table + " as temp1 where temp1." + _g.d.ic_trans._doc_ref + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + " and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(target) +
                " union all " +
                " select " + _g.d.ic_trans._doc_no + " from " + _g.d.ic_trans._table + " as temp3 where temp3." + _g.d.ic_trans._doc_no + " = " + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + " and temp3." + _g.d.ic_trans._is_cancel + "=1 and temp3." + _g.d.ic_trans._trans_flag + " =" + _g.g._transFlagGlobal._transFlag(source);

            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._last_status + "=0 where " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(source) + " and " + _g.d.ic_trans._last_status + "<>0 and not exists (" + __docRefCancel + ")"));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._last_status + "=1 where " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(source) + " and " + _g.d.ic_trans._last_status + "<>1 and     exists (" + __docRefCancel + ")"));
            return __query.ToString();
        }

        public string _icFlowQueryList()
        {
            StringBuilder __query = new StringBuilder();
            __query.Append(this._icFlowQueryListFormat(_g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป, _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป_ยกเลิก));
            __query.Append(this._icFlowQueryListFormat(_g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ, _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ_ยกเลิก));
            __query.Append(this._icFlowQueryListFormat(_g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก, _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก_ยกเลิก));
            __query.Append(this._icFlowQueryListFormat(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก, _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก_ยกเลิก));
            // โอนออก โอนเข้า ใช้เอกสารยกเลิกใบเดียวกัน
            __query.Append(this._icFlowQueryListFormat(_g.g._transControlTypeEnum.สินค้า_โอนออก, _g.g._transControlTypeEnum.สินค้า_โอนออก_ยกเลิก));
            __query.Append(this._icFlowQueryListFormat(_g.g._transControlTypeEnum.สินค้า_โอนเข้า, _g.g._transControlTypeEnum.สินค้า_โอนออก_ยกเลิก));

            // toe
            // update used status

            return __query.ToString();
        }

        /// <summary>
        /// ประมวลผลใบเสนอซื้อ,การอนุมัติ
        /// ใบเสนอซื้อจะถูกอ้างอิงจาก การอนุมัติ และยกเลิกเท่านั้น
        /// ใบอนุมัติเสนอซื้อ จะถูกอ้างอิงจากการสั่งซื้อเท่านั้น
        /// </summary>
        /// <returns></returns>
        public string _prFlowQueryList()
        {
            StringBuilder __query = new StringBuilder();
            //
            // ส่วนใบเสนอซื้อ
            // อ้างอืง (นำไปใช้แล้ว)
            // อนุมัติเสนอซื้อ 4
            // ยกเลิกอนุมัติเสนอซื้อ 3
            // ซื้อ_ใบสั่งซื้อ 6
            string __refQuery = "(select doc_ref from ic_trans_detail as temp1 where temp1.ref_doc_no=ic_trans.doc_no and trans_flag in (3,4,6))";
            // เสนอซื้อ trans_flag = 2
            // กรณี ไม่พบรายการอ้างอิง ให้ _user_status=0
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._used_status + "=0 where " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ) + " and " + _g.d.ic_trans._used_status + "<>0 and not exists " + __refQuery));
            // กรณี พบรายการอ้างอืง ให้ _user_status=1
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._used_status + "=1 where " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ) + " and " + _g.d.ic_trans._used_status + "<>1 and exists " + __refQuery));
            // 
            // อ้างอิงเอกสารครบแล้ว doc_success=1
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set " + _g.d.ic_trans._doc_success + "=0 where " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ) + " and " + _g.d.ic_trans._doc_success + "<>0 and not exists " + __refQuery));
            // อ้างอิงเอกสารไม่ครบ doc_success=0
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set " + _g.d.ic_trans._doc_success + "=1 where " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ) + " and " + _g.d.ic_trans._doc_success + "<>1 and exists " + __refQuery));
            //
            // กรณี ไม่พบรายการยกเลิก ให้ _last_status=0
            // อ้างอืง (นำไปใช้แล้ว)
            string __refCancelQuery = "(select " + _g.d.ic_trans._doc_ref + " from " + _g.d.ic_trans._table + " as temp1 where temp1." + _g.d.ic_trans._doc_ref + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + " and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก) + ")";
            //
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._last_status + "=0 where " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ) + " and " + _g.d.ic_trans._last_status + "<>0 and not exists " + __refCancelQuery));
            // กรณี พบรายการยกเลิก ให้ _last_status=1
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._last_status + "=1 where " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ) + " and " + _g.d.ic_trans._last_status + "<>1 and exists " + __refCancelQuery));
            // 
            // ตรวจสอบการอนุมัติ
            // อ้างอืง (นำไปใช้แล้ว)
            string __refApproveQuery = "(select " + _g.d.ic_trans._doc_ref + " from " + _g.d.ic_trans._table + " as temp1 where temp1." + _g.d.ic_trans._doc_ref + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + " and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ).ToString() + ")";
            // กรณี ไม่พบรายการอ้างอิง ให้ _approve_status=0
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._approve_status + "=0 where " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ) + " and " + _g.d.ic_trans._approve_status + "<>0 and not exists " + __refApproveQuery));
            // กรณี พบรายการอ้างอืง ให้ _approve_status=ค้นหาการอนุมัติว่าอนุมัติหรือไม่อนุมัติ
            string __findApproveQuery = "coalesce((select " + MyLib._myGlobal._getTopAndLimitOneRecord()[0].ToString() + " cast(not_approve_1 as int) from ic_trans as temp2 where temp2.doc_ref=ic_trans.doc_no and trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ) + " " + MyLib._myGlobal._getTopAndLimitOneRecord()[1].ToString() + "),0)";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._approve_status + "=(case when " + __findApproveQuery + "=1 then 2 else 1 end) where " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ) + " and " + _g.d.ic_trans._approve_status + "<>(case when " + __findApproveQuery + "=1 then 2 else 1 end) and exists " + __refApproveQuery));
            //
            // ตรวจสอบวันหมดอายุ
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._expire_status + "=0 where " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ) + " and " + _g.d.ic_trans._expire_status + "<>0 and " + _g.d.ic_trans._approve_status + "=0 and " + _g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans._doc_success + "=0 and " + _g.d.ic_trans._used_status + "=0 and " + _g.d.ic_trans._expire_date + " is not null and " + _g.d.ic_trans._expire_date + "<>" + _g.d.ic_trans._doc_date + " and " + _g.d.ic_trans._expire_date + " <= now()"));
            //
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._expire_status + "=1 where " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ) + " and " + _g.d.ic_trans._expire_status + "<>1 and " + _g.d.ic_trans._expire_date + " is not null and " + _g.d.ic_trans._expire_date + "<>" + _g.d.ic_trans._doc_date + " and now() > " + _g.d.ic_trans._expire_date + " and " + _g.d.ic_trans._approve_status + "=0 and " + _g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans._doc_success + "=0 and " + _g.d.ic_trans._used_status + "=0"));
            //
            // ส่วนอนุมัติใบเสนอซื้อ ที่มีการนำไปใช้โดยใบสั่งซื้อ (อ้างอิงจากใบสั่งซื้อมา)
            // กรณี ไม่พบรายการอนุมัติ ให้ _used_status=0
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._used_status + "=0 where " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ) + " and " + _g.d.ic_trans._used_status + "<>0 and not exists (select " + _g.d.ic_trans_detail._ref_doc_no + " from " + _g.d.ic_trans_detail._table + " as temp1 where temp1." + _g.d.ic_trans_detail._ref_doc_no + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + " and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ).ToString() + ")"));
            // กรณี พบรายการอ้างอืง ให้ _used_status=1 
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._used_status + "=1 where " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ) + " and " + _g.d.ic_trans._used_status + "<>1 and exists (select " + _g.d.ic_trans_detail._ref_doc_no + " from " + _g.d.ic_trans_detail._table + " as temp1 where temp1." + _g.d.ic_trans_detail._ref_doc_no + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + " and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ).ToString() + ")"));
            //
            return ""; // __query.ToString();
        }

        public string _purchaseFlowQueryList()
        {
            StringBuilder __query = new StringBuilder();
            //
            // ซื้อ
            // LastStatus : ยกเลิก PO ทั้งใบ x.cancel_type=1
            // อ้างอิงจากหัวเอกสาร
            String __subQuery = "(select distinct doc_ref from ic_trans as x where x.doc_ref is not null and x.trans_flag=7 and x.cancel_type=1 union all select doc_no from ic_trans as temp3 where temp3.trans_flag=6 and temp3.is_cancel = 1 )";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=1 where last_status<>1 and trans_flag=6 and doc_no in " + __subQuery));
            // 
            // ตรวจสอบการอนุมัติ
            // อ้างอืง (นำไปใช้แล้ว)
            string __refApproveQuery = "(select " + _g.d.ic_trans._doc_ref + " from " + _g.d.ic_trans._table + " as temp1 where temp1." + _g.d.ic_trans._doc_ref + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + " and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ).ToString() + ")";
            // กรณี ไม่พบรายการอ้างอิง ให้ _approve_status=0
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._approve_status + "=0 where " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ) + " and " + _g.d.ic_trans._approve_status + "<>0 and not exists " + __refApproveQuery));
            // กรณี พบรายการอ้างอืง ให้ _approve_status=ค้นหาการอนุมัติว่าอนุมัติหรือไม่อนุมัติ
            string __findApproveQuery = "coalesce((select " + MyLib._myGlobal._getTopAndLimitOneRecord()[0].ToString() + " cast(not_approve_1 as int) from ic_trans as temp2 where temp2.doc_ref=ic_trans.doc_no and trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ) + " " + MyLib._myGlobal._getTopAndLimitOneRecord()[1].ToString() + "),0)";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._approve_status + "=(case when " + __findApproveQuery + "=1 then 2 else 1 end) where " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ) + " and " + _g.d.ic_trans._approve_status + "<>(case when " + __findApproveQuery + "=1 then 2 else 1 end) and exists " + __refApproveQuery));
            //
            string __whereItemPO = " and ic_trans_detail.item_code in (select item_code from ic_trans_detail as x where x.doc_no = ic_trans.doc_no) ";

            // doc_success : เอกสารอ้างอิงครบแล้ว 
            // toe เพิ่ม ผลรวมจำนวนรายการต้องมากกว่า 0
            string __doc_qty_item_sum_more_then_zero = " and (select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=6 and coalesce(ic_trans_detail.item_code, '') <> '') > 0";
            string __docSuccess = "update ic_trans set doc_success=1 where doc_success<>1 and trans_flag=6 and last_status<>1 " + __doc_qty_item_sum_more_then_zero + " and (select count(*) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=6)>0 and (select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=6 and coalesce(ic_trans_detail.item_code, '') <> '')-" + " (select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.ref_doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag in (7,12,310) and coalesce(ic_trans_detail.item_code, '') <> '' " + __whereItemPO + ")<=0";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__docSuccess));
            //
            // LastStatus : ยกเลิก PO ทั้งใบ (คืนค่ากรณีลบรายการยกเลิก) 6=สั่งซื้อ
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=0 where last_status<>0 and trans_flag=6 and doc_no not in " + __subQuery));
            // doc_success : ยกเอกสารอ้างอิงครบแล้ว,310=รับสินค้า
            // toe เพิ่ม หรือผลรวมของจำนวนสินค้าในบิลเท่ากับ 0
            string __doc_qty_item_sum_equal_zero = " or  (select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=6 and coalesce(ic_trans_detail.item_code, '') <> '') = 0 ";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set doc_success=0 where doc_success<>0 and trans_flag=6 and last_status<>1  " + " and ((select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=6 and coalesce(ic_trans_detail.item_code, '') <> '')-" + " (select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.ref_doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag in (7,12,310) and coalesce(ic_trans_detail.item_code, '') <> ''  " + __whereItemPO + " )>0 or  (select count(*) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=6 and coalesce(ic_trans_detail.item_code, '') <> '' )=0 " + __doc_qty_item_sum_equal_zero + " )"));
            //
            // used_status : มีการนำ PO ไปใช้หรือยัง 0=ยัง,1=ใช้แล้ว
            // 7=ยกเลิก PO,8=อนุมัติใบสั่งซื้อ,12=ซื้อ,310=รับสินค้า
            __subQuery = "(select distinct doc_ref from ic_trans as x where x.doc_ref is not null and x.trans_flag in (7,8) union all select distinct ref_doc_no from ic_trans_detail where ref_doc_no is not null and trans_flag in (12,310))";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=1 where used_status<>1 and trans_flag=6 and doc_no in " + __subQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=0 where used_status<>0 and trans_flag=6 and doc_no not in " + __subQuery));
            // 7=ยกเลิก PO,12=ซื้อ,310=รับสินค้า
            __subQuery = "(select distinct ref_doc_no from ic_trans_detail where ref_doc_no is not null and trans_flag in (12,310))";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status_2=1 where used_status_2<>1 and trans_flag=6 and doc_no in " + __subQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status_2=0 where used_status_2<>0 and trans_flag=6 and doc_no not in " + __subQuery));
            //
            // used_status : 12=ซื้อ,14=เพิ่มหนี้,16=ส่งคืน (รวมยกเลิก),310=รับสินค้า
            __subQuery = "(select distinct ref_doc_no from ic_trans_detail where ref_doc_no is not null and trans_flag in (16,14,13,15,17))";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=1 where used_status<>1 and trans_flag in (12,310) and doc_no in " + __subQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=0 where used_status<>0 and trans_flag in (12,310) and doc_no not in " + __subQuery));
            //
            // last_status : 12=ซื้อ,14=เพิ่มหนี้,16=ส่งคืน ยกเลิกทั้งใบ,310=รับสินค้า
            __subQuery = "(select distinct doc_ref from ic_trans where doc_ref is not null and trans_flag in (13,15,17) union all select doc_no from ic_trans as temp3 where temp3.trans_flag in (12,14,16,310) and temp3.is_cancel=1  )";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=1 where last_status<>1 and trans_flag in (12,14,16,310) and doc_no in " + __subQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=0 where last_status<>0 and trans_flag in (12,14,16,310) and doc_no not in " + __subQuery));

            // used_status_2
            string __subQuery8 = "( select billing_no from ap_ar_trans_detail where ap_ar_trans_detail.trans_flag in (213, 19) and  ap_ar_trans_detail.last_status = 0 )";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status_2=1 where used_status_2<>1 and trans_flag in (12, 14, 16) and doc_no in " + __subQuery8));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status_2=0 where used_status_2<>0 and trans_flag in (12, 14, 16) and doc_no not in " + __subQuery8));

            return ""; // __query.ToString();
        }

        public String _processStockFlowQueryList()
        {
            StringBuilder __query = new StringBuilder();
            //
            // สินค้า
            //
            // Used : รับคินจากการเบิก 56=เบิก,58=รับคืนจากการเบิก
            // อ้างอิงจากหัวเอกสาร
            String __subQuery = "(select distinct ref_doc_no from ic_trans_detail where ref_doc_no is not null and trans_flag=58)";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=1 where used_status<>1 and trans_flag=56 and doc_no in " + __subQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=0 where used_status<>0 and trans_flag=56 and doc_no not in " + __subQuery));
            // doc_success : เอกสารอ้างอิงครบแล้ว
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set doc_success=1 where doc_success<>1 and trans_flag=56 and last_status<>1 " + " and (select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=56) > 0 " + " and (select count(*) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=56)>0 and (select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=56)-(select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.ref_doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag in (58))<=0"));
            // doc_success : ยกเอกสารอ้างอิงครบแล้ว
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set doc_success=0 where doc_success<>0 and trans_flag=56 and last_status<>1 " + " and ((select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=56)-(select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.ref_doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag in (58))>0 or (select count(*) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=56)=0" + " or (select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=56) = 0 )"));

            return __query.ToString();
        }

        public String _processFlowSaleQueryList()
        {
            StringBuilder __query = new StringBuilder();
            //
            // ขาย
            // update last_status : 44=ขาย,46=เพิ่มหนี้,48=รับคืน ยกเลิกทั้งใบ
            // 45=รับคืนจากการขาย,47=ยกเลิก ขายเพิ่มหนี้ ,49=เพิ่มหนี้ (ขาย)
            string __subQuery1 = "(select distinct doc_ref from ic_trans where doc_ref is not null and trans_flag in (45,47,49) union all select doc_no from ic_trans where is_cancel = 1 and trans_flag in (44,46,48))";
            // update ic_trans_detail
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans_detail set last_status=1 where last_status<>1 and trans_flag in (44,46,48) and doc_no in " + __subQuery1));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans_detail set last_status=0 where last_status<>0 and trans_flag in (44,46,48) and doc_no not in " + __subQuery1));
            // update ic_trans
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=1 where last_status<>1 and trans_flag in (44,46,48) and doc_no in " + __subQuery1));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=0 where last_status<>0 and trans_flag in (44,46,48) and doc_no not in " + __subQuery1));

            // toe update cb_trans_detail set last_status
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update cb_trans_detail set last_status=1 where last_status<>1 and trans_flag in (44,46,48) and doc_no in " + __subQuery1));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update cb_trans_detail set last_status=0 where last_status<>0 and trans_flag in (44,46,48) and doc_no not in " + __subQuery1));

            //
            // Used : มีการนำใบสั่งขายไปใช้หรือยัง 0=ยัง,1=ใช้แล้ว
            // 37=ยกเลิกใบสั่งขาย,36=ใบสั่งขาย,44=ขาย,52=อนุมัติสั่งขาย
            //string __subQuery2 = "(select distinct ref_doc_no from ic_trans_detail as x where x.ref_doc_no is not null and x.trans_flag=37 union all select distinct ref_doc_no from ic_trans_detail where ref_doc_no is not null and trans_flag=44 union all select distinct doc_ref as ref_doc_no from ic_trans where doc_ref is not null and trans_flag=52)";
            string __subQuery2 = "(select distinct ref_doc_no from ic_trans_detail where ic_trans_detail.ref_doc_no is not null and ic_trans_detail.trans_flag in (37,44) union all select distinct doc_ref as ref_doc_no from ic_trans where doc_ref is not null and trans_flag=52)";
            // toe__query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=1 where used_status<>1 and trans_flag=36 and doc_no in " + __subQuery2));
            // toe__query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=0 where used_status<>0 and trans_flag=36 and doc_no not in " + __subQuery2));
            //
            // Process กลุ่มใบสั่งขาย
            // LastStatus : ยกเลิกทั้งใบ (ใบสั่งขาย) (36=ใบสั่งขาย,37=ยกเลิก)
            string __subQuery3 = "(select distinct doc_ref from ic_trans as x where x.doc_ref is not null and x.trans_flag=37 and x.cancel_type=1)";
            // LastStatus : ยกเลิกใบสั่งจองทั้งใบ (คืนค่ากรณีลบรายการยกเลิก) 36=ใบสั่งขาย,37=ยกเลิก
            // toe__query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans_detail set last_status=0 where last_status<>0 and trans_flag=36 and doc_no not in " + __subQuery3));
            // toe__query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans_detail set last_status=1 where last_status<>1 and trans_flag=36 and doc_no in " + __subQuery3));
            //
            // toe__query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=0 where last_status<>0 and trans_flag=36 and doc_no not in " + __subQuery3));
            // toe__query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=1 where last_status<>1 and trans_flag=36 and doc_no in " + __subQuery3));
            // doc_success : เอกสารอ้างอิงครบแล้ว
            string __sumDocQty = " (select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=36 and ic_trans_detail.last_status=0 and ic_trans_detail.item_code <> '' ) ";
            //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set doc_success=1 where doc_success<>1 and trans_flag=36 and last_status<>1 " + "and (" + __sumDocQty + " > 0) " + " and (select count(*) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=36 and ic_trans_detail.last_status=0)>0 and " + __sumDocQty + " -" + " (select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.ref_doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag in (37,44) and ic_trans_detail.last_status=0)<=0"));
            // doc_success=0 : ใบสั่งขาย ได้ทำการยกเลิกเอกสาร หรืออ้างอิงไม่ครบ ให้กลับไปสภาพเดิมจะได้อ้างอิงใหม่ได้
            // 36=ใบสั่งขาย
            // 37=ใบสั่งขายยกเลิก,44=ขาย
            // toe__query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set doc_success=0 where doc_success<>0 and trans_flag=36 and last_status<>1 and (" + __sumDocQty + "-(select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.ref_doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag in (37,44) and ic_trans_detail.last_status=0)>0 or (select count(*) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=36 and ic_trans_detail.last_status=0)=0" + " or (" + __sumDocQty + "=0) ) "));
            //
            //----------------------------------------------------------------------------------------------------------
            // Used : มีการนำใบสั่งจองไปใช้หรือยัง 0=ยัง,1=ใช้แล้ว
            // 35=ยกเลิกใบสั่งจอง,34=ใบสั่งจอง,44=ขาย,38=อนุมัติ
            //string __subQuery4 = "(select distinct doc_ref from ic_trans as x where x.doc_ref is not null and x.trans_flag=35 union all select distinct ref_doc_no from ic_trans_detail where ref_doc_no is not null and trans_flag=44 union all select distinct doc_ref as ref_doc_no from ic_trans where doc_ref is not null and trans_flag=38)";
            string __subQuery4 = "(select distinct doc_ref from ic_trans as x where x.doc_ref is not null and x.trans_flag in (35,44) and x.last_status=0 union all select distinct doc_ref as ref_doc_no from ic_trans where doc_ref is not null and trans_flag=38)";
            // toe __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=1 where used_status<>1 and trans_flag=34 and doc_no in " + __subQuery4));
            // toe __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=0 where used_status<>0 and trans_flag=34 and doc_no not in " + __subQuery4));
            //
            // Process กลุ่มใบสั่งจอง
            // LastStatus : ยกเลิกทั้งใบ (ใบสั่งจอง) x.cancel_type=1
            // 39=ยกเลิกใบสั่งจอง,36=สั่งขาย
            string __subQuery5 = "(select distinct doc_ref from ic_trans as x where x.doc_ref is not null and x.trans_flag=39 and x.cancel_type=1)";
            // LastStatus : ยกเลิกใบสั่งจองทั้งใบ (คืนค่ากรณีลบรายการยกเลิก) 34=ใบสั่งจอง
            // toe __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans_detail set last_status=1 where last_status<>1 and trans_flag=34 and doc_no in " + __subQuery5));
            // toe __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans_detail set last_status=0 where last_status<>0 and trans_flag=34 and doc_no not in " + __subQuery5));
            //
            // toe __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=1 where last_status<>1 and trans_flag=34 and doc_no in " + __subQuery5));
            // toe __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=0 where last_status<>0 and trans_flag=34 and doc_no not in " + __subQuery5));
            // doc_success : เอกสารอ้างอิงครบแล้ว
            // toe __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set doc_success=1 where doc_success<>1 and trans_flag=34 and (select count(*) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=34 and ic_trans_detail.last_status=0)>0 and (select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=34 and ic_trans_detail.last_status=0 and coalesce(ic_trans_detail.item_code, '') <> '')-(select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.ref_doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag in (36,44) and ic_trans_detail.last_status=0 and coalesce(ic_trans_detail.item_code, '') <> '')<=0"));
            // doc_success : ยกเอกสารอ้างอิงครบแล้ว (34=ใบสั่งจอง)
            // toe __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set doc_success=0 where doc_success<>0 and trans_flag=34 and ((select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=34 and ic_trans_detail.last_status=0 and coalesce(ic_trans_detail.item_code, '') <> '' )-(select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.ref_doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag in (36,44) and ic_trans_detail.last_status=0 and coalesce(ic_trans_detail.item_code, '') <> '' )>0 or (select count(*) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=34 and ic_trans_detail.last_status=0)=0)"));
            //----------------------------------------------------------------------------------------------------------
            // Used : มีการนำ SO ไปใช้หรือยัง 0=ยัง,1=ใช้แล้ว
            // 30=ใบเสนอราคา
            // อ้าง 44=ยกเลิก SO,32=อนุมัติ,34=ใบสั่งจอง/สั่งซื้อ,36=ขาย/สั่งขาย
            //string __subQuery6 = "(select distinct ref_doc_no from ic_trans_detail as x where x.ref_doc_no is not null and x.trans_flag in (34,36) union all select distinct ref_doc_no from ic_trans_detail where ref_doc_no is not null and trans_flag=44 union all select distinct doc_ref as ref_doc_no from ic_trans where doc_ref is not null and trans_flag=32)";
            string __subQuery6 = "(select distinct ref_doc_no from ic_trans_detail as x where x.ref_doc_no is not null and x.trans_flag in (34,36,44) and x.last_status=0 union all select distinct doc_ref as ref_doc_no from ic_trans where doc_ref is not null and trans_flag=32 and last_status=0)";
            // toe __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=1 where used_status<>1 and trans_flag=30 and doc_no in " + __subQuery6));
            // toe __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=0 where used_status<>0 and trans_flag=30 and doc_no not in " + __subQuery6));
            //
            //
            // Process กลุ่มใบเสนอราคา
            // LastStatus : ยกเลิก SO ทั้งใบ (ใบเสนอราคา)
            String __subQuery = "(select distinct doc_ref from ic_trans as x where x.doc_ref is not null and x.trans_flag=31)";
            // toe __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans_detail set last_status=1 where last_status<>1 and trans_flag=30 and doc_no in " + __subQuery));
            // toe __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=1 where last_status<>1 and trans_flag=30 and doc_no in " + __subQuery));
            // doc_success : เอกสารอ้างอิงครบแล้ว
            // toe __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set doc_success=1 where doc_success<>1 and trans_flag=30 and last_status<>1 and (select count(*) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=30 and ic_trans_detail.last_status=0)>0 and (select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=30 and ic_trans_detail.last_status=0 and coalesce(ic_trans_detail.item_code, '') <> '')-(select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.ref_doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag in (31,44) and ic_trans_detail.last_status=0 and coalesce(ic_trans_detail.item_code, '') <> '' )<=0"));
            // toe __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set doc_success=1 where doc_success<>1 and trans_flag=30 and last_status<>1 and (select count(*) from ic_trans_detail where ic_trans_detail.ref_doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=30 and ic_trans_detail.last_status=0)>0 "));
            //
            // LastStatus : ยกเลิก SO ทั้งใบ (คืนค่ากรณีลบรายการยกเลิก) 30=ใบเสนอราคา
            // toe __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans_detail set last_status=0 where last_status<>0 and trans_flag=30 and doc_no not in " + __subQuery));
            // toe __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=0 where last_status<>0 and trans_flag=30 and doc_no not in " + __subQuery));
            //
            // doc_success : ยกเอกสารอ้างอิงครบแล้ว (30=ใบเสนอราคา)
            // toe __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set doc_success=0 where doc_success<>0 and trans_flag=30 and last_status<>1 and ((select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=30 and ic_trans_detail.last_status=0 and coalesce(ic_trans_detail.item_code, '') <> '')-(select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.ref_doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag in (31,44) and ic_trans_detail.last_status=0 and coalesce(ic_trans_detail.item_code, '') <> '' )>0 or (select count(*) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=30 and ic_trans_detail.last_status=0)=0)"));
            //

            // Used : มีการนำใบขายสินค้าและบริการไปอ้ัางอิงลดหนี้ หรือ เพิ่มหนี้ 0=ยัง 1=ใช้แล้ว

            string __subQuery7 = "(select distinct ref_doc_no from ic_trans_detail as x where x.ref_doc_no is not null and x.trans_flag in (46,48) and x.last_status=0 )";
            //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=1 where used_status<>1 and trans_flag=44 and doc_no in " + __subQuery7));
            // __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=0 where used_status<>0 and trans_flag=44 and doc_no not in " + __subQuery7));


            // used_status_2
            string __subQuery8 = "( select billing_no from ap_ar_trans_detail where ap_ar_trans_detail.trans_flag in (235, 239) and  ap_ar_trans_detail.last_status = 0 )";
            //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status_2=1 where used_status_2<>1 and trans_flag in (44, 46, 48) and doc_no in " + __subQuery8));
            //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status_2=0 where used_status_2<>0 and trans_flag in (44, 46, 48) and doc_no not in " + __subQuery8));


            return __query.ToString();
        }

        /// <summary>
        /// ประมวลผลจ่ายเงินล่วงหน้า (เจ้าหนี้)
        /// </summary>
        /// <returns></returns>
        public string _depositFlowQueryList()
        {
            StringBuilder __query = new StringBuilder();
            string __refQuery = "";

            // อ้างอืง (นำไปใช้แล้ว)
            // 161=ยกเลิกคืนเงินล่วงหน้า
            __refQuery = "(select doc_ref from ic_trans as temp1 where temp1.trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก) + " and temp1.doc_ref=ic_trans.doc_no union all select doc_no from ic_trans as temp3 where temp3.trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน) + " and temp3.is_cancel = 1 and temp3.doc_no = ic_trans.doc_no )";
            // update last_status,ยกเลิกคืนเงินล่วงหน้า (Step แรก),20=รับคืนจ่ายเงินล่วงหน้า
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=0 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน) + " and last_status<>0 and not exists " + __refQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=1 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน) + " and last_status<>1 and exists " + __refQuery));
            // update use_status,10=จ่ายเงินล่วงหน้า
            // 20=จ่ายเงินล่วงหน้ารับคืน,150=ยกเลิกจ่ายเงินล่วงหน้า
            __refQuery = "(select doc_ref from ic_trans as temp1 where temp1.trans_flag in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน) + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก) + ") and temp1.last_status=0 and temp1.doc_ref=ic_trans.doc_no " +
                " union all select trans_number as doc_ref from cb_trans_detail where cb_trans_detail.trans_number=ic_trans.doc_no)";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=0 where trans_flag=10 and used_status<>0 and not exists " + __refQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=1 where trans_flag=10 and used_status<>1 and exists " + __refQuery));
            // update last_status,150=ยกเลิกการจ่ายเงินล่วงหน้า
            __refQuery = "(select doc_ref from ic_trans as temp1 where temp1.trans_flag=150 and temp1.last_status=0 and temp1.doc_ref=ic_trans.doc_no union all select doc_no from ic_trans as temp3 where temp3.trans_flag=10 and temp3.is_cancel = 1 and temp3.doc_no = ic_trans.doc_no )";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=0 where trans_flag=10 and last_status<>0 and not exists " + __refQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=1 where trans_flag=10 and last_status<>1 and exists " + __refQuery));
            //



            // toe จ่ายล่วงหน้า ลูกหนี้
            // update used_status
            // 42=คืนเงินรับล่วงหน้า(ลูกหนี้), 41=ยกเลิกรับล่วงหน้า(ลูกหนี้) 
            // 40=รับเงินล่วงหน้า (ลูกหนี้)
            __refQuery = "(select doc_ref from ic_trans as temp1 where temp1.trans_flag in (42,41) and temp1.last_status=0 and temp1.doc_ref=ic_trans.doc_no " +
                " union all select trans_number as doc_ref from cb_trans_detail where cb_trans_detail.trans_number=ic_trans.doc_no )";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=0 where trans_flag=40 and used_status<>0 and not exists " + __refQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=1 where trans_flag=40 and used_status<>1 and exists " + __refQuery));


            // update last_Status
            // 41=ยกเลิกรับล่วงหน้า(ลูกหนี้)
            // 40=รับล่วงหน้า(ลูกหนี้)
            __refQuery = "(select doc_ref from ic_trans as temp1 where temp1.trans_flag = 41 and ic_trans.doc_no = temp1.doc_ref union all select doc_no from ic_trans as temp3 where temp3.trans_flag=40 and temp3.is_cancel = 1 and temp3.doc_no = ic_trans.doc_no )";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=0 where trans_flag=40 and last_status<>0 and not exists " + __refQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=1 where trans_flag=40 and last_status<>1 and exists " + __refQuery));

            // update last_status
            // 43=ยกเลิกคืนเงินรับล่วงหน้า(ลูกหนี้)
            // 42=คืนเงินรับล่วงหน้า(ลูกหนี้)
            __refQuery = "(select doc_ref from ic_trans as temp1 where temp1.trans_flag = 43 and ic_trans.doc_no = temp1.doc_ref union all select doc_no from ic_trans as temp3 where temp3.trans_flag=42 and temp3.is_cancel = 1 and temp3.doc_no = ic_trans.doc_no )";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=0 where trans_flag=42 and last_status<>0 and not exists " + __refQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=1 where trans_flag=42 and last_status<>1 and exists " + __refQuery));


            return __query.ToString();
        }

        /// <summary>
        /// ประมวลผลเงินมัดจำ (เจ้าหนี้+ลูกหนี้)
        /// </summary>
        /// <returns></returns>
        public string _advanceFlowQueryList()
        {
            StringBuilder __query = new StringBuilder();
            // อ้างอืง (นำไปใช้แล้ว)
            // 152=ยกเลิกคืนเงินมัดจำ (เจ้าหนี้)
            string __refQuery = "(select doc_ref from ic_trans as temp1 where temp1.trans_flag=152 and temp1.doc_ref=ic_trans.doc_no union all select doc_no from ic_trans as temp3 where temp3.trans_flag=25 and temp3.is_cancel = 1 and temp3.doc_no = ic_trans.doc_no )";

            // update last_status,ยกเลิกคืนเงินล่วงหน้า (Step แรก),25=รับคืนเงินจ่ายมัดจำ (เจ้าหนี้)
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=0 where trans_flag=25 and last_status<>0 and not exists " + __refQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=1 where trans_flag=25 and last_status<>1 and exists " + __refQuery));

            // update use_status,11=จ่ายเงินมัดจำ (เจ้าหนี้)
            // 25=รับคืนเงินจ่ายมัดจำ (เจ้าหนี้),151=ยกเลิกจ่ายเงินมัดจำ(เจ้าหนี้)
            __refQuery = "(select doc_ref from ic_trans as temp1 where temp1.trans_flag in (25,151) and temp1.last_status=0 and temp1.doc_ref=ic_trans.doc_no " +
                " union all select trans_number as doc_ref from cb_trans_detail where cb_trans_detail.trans_number=ic_trans.doc_no)";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=0 where trans_flag=11 and used_status<>0 and not exists " + __refQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=1 where trans_flag=11 and used_status<>1 and exists " + __refQuery));
            // update last_status,151=ยกเลิกการจ่ายเงินมัดจำ (เจ้าหนี้)
            __refQuery = "(select doc_ref from ic_trans as temp1 where temp1.trans_flag=151 and temp1.last_status=0 and temp1.doc_ref=ic_trans.doc_no  union all select doc_no from ic_trans as temp3 where temp3.trans_flag=11 and temp3.is_cancel = 1 and temp3.doc_no = ic_trans.doc_no  )";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=0 where trans_flag=11 and last_status<>0 and not exists " + __refQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=1 where trans_flag=11 and last_status<>1 and exists " + __refQuery));
            //

            // toe รับเงินมัดจำ

            // update use_status 
            // 111=ยกเลิกรับเงินมัดจำ (ลูกหนี้), 112=คืนเงินมัดจำ (ลูกหนี้)
            // 110=รับเงินมัดจำ
            __refQuery = "(select doc_ref from ic_trans as temp1 where temp1.trans_flag in (111,112) and temp1.last_status=0 and temp1.doc_ref=ic_trans.doc_no " +
                " union all select trans_number as doc_ref from cb_trans_detail where cb_trans_detail.trans_number=ic_trans.doc_no)";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=0 where trans_flag=110 and used_status<>0 and not exists " + __refQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=1 where trans_flag=110 and used_status<>1 and exists " + __refQuery));

            // update last_status รับเงินมัดจำ
            // 111=ยกเลิกรับเงินมัดจำ (ลูกหนี้)
            // 110=รับเงินมัดจำ
            __refQuery = "(select doc_ref from ic_trans as temp1 where temp1.trans_flag=111 and temp1.doc_ref=ic_trans.doc_no  union all select doc_no from ic_trans as temp3 where temp3.trans_flag=110 and temp3.is_cancel = 1 and temp3.doc_no = ic_trans.doc_no  )";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=0 where trans_flag=110 and last_status<>0 and not exists " + __refQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=1 where trans_flag=110 and last_status<>1 and exists " + __refQuery));

            // update last_status คืนเงินมัดจำ
            // 113=ยกเลิกคืนเงินมัดจำ(ลูกหนี้)
            // 112=คืนเงินมัดจำ (ลูกหนี้)
            __refQuery = "(select doc_ref from ic_trans as temp1 where temp1.trans_flag=113 and temp1.doc_ref=ic_trans.doc_no  union all select doc_no from ic_trans as temp3 where temp3.trans_flag=112 and temp3.is_cancel = 1 and temp3.doc_no = ic_trans.doc_no )";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=0 where trans_flag=112 and last_status<>0 and not exists " + __refQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=1 where trans_flag=112 and last_status<>1 and exists " + __refQuery));

            // update doc_success
            // 112=คืนเงินมัดจำ (ลูกหนี้)
            __refQuery = "coalesce(( select sum(coalesce(total_amount, 0)) from ( " +
                " select total_amount from ic_trans as a where a.doc_ref = ic_trans.doc_no and a.trans_flag = 112 and a.last_status = 0 " +
                " union all " +
                " select amount as total_amount from cb_trans_detail where cb_trans_detail.doc_type = 6 and cb_trans_detail.trans_number = ic_trans.doc_no and (select last_status from ic_trans as temp2 where temp2.doc_no = cb_trans_detail.doc_no and temp2.trans_flag = cb_trans_detail.trans_flag)= 0 " +
                ") as temp1 ), 0) ";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set doc_success = 1 where trans_flag = 110 and total_amount <> 0 and  (total_amount-" + __refQuery + ")=0 and doc_success <> 1 "));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set doc_success = 0 where trans_flag = 110 and  (total_amount-" + __refQuery + ")<>0 and doc_success <> 0 "));

            return __query.ToString();
        }

        public string _serialNumberQueryList()
        {
            StringBuilder __query = new StringBuilder();

            // and not ( trans_flag in (76) or (trans_flag in (14, 16)  and ic_trans_serial_number.inquiry_type<>0)) โต๋เพิ่มเข้าไปเพื่อเอารายการตรวจนับ และส่งคืนไม่กระทบสต๊อกออก

            String __subQuery1 = "(select last_status from ic_trans where ic_trans.doc_no=ic_trans_serial_number.doc_no and ic_trans.trans_flag=ic_trans_serial_number.trans_flag)";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans_serial_number set last_status=" + __subQuery1 + " where last_status<>" + __subQuery1));
            //

            // toe เพิ่ม where ic_trans_serial_number.ic_code = ic_serial.ic_code and last_status = 0
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from ic_serial where not exists (select serial_number from ic_trans_serial_number where ic_trans_serial_number.ic_code = ic_serial.ic_code and last_status = 0 and ic_trans_serial_number.serial_number=ic_serial.serial_number  and not ( trans_flag in (76) or (trans_flag in (14, 16)  and ic_trans_serial_number.inquiry_type<>0)) )"));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into ic_serial (ic_code,ic_unit,serial_number,void_date) (select distinct ic_code,unit_code,serial_number,void_date from ic_trans_serial_number where not exists (select serial_number from ic_serial where ic_serial.serial_number=ic_trans_serial_number.serial_number and ic_trans_serial_number.ic_code = ic_serial.ic_code) and not ( trans_flag in (76) or (trans_flag in (14, 16)  and ic_trans_serial_number.inquiry_type<>0))  and last_status = 0 )"));
            // update cust_code
            String __subQueryCustCode = "coalesce((select cust_code from ic_trans where ic_trans.doc_no=ic_trans_serial_number.doc_no and ic_trans.trans_flag=ic_trans_serial_number.trans_flag),null)";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans_serial_number set cust_code=" + __subQueryCustCode + " where cust_code<>" + __subQueryCustCode));
            // update status (กรณีส่งคืนราคาผิด,รับคืนราคาผิดไม่เอามาประมวล)
            string __normalCase = "ic_trans_serial_number.serial_number=ic_serial.serial_number and " +
                "(trans_flag in (" +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา) + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป) + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ) + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก) + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า) + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนออก) + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก) + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด) + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ) + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า) + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + ")" +
                " or (trans_flag in (" +
                 _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด) + "," +
                 _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด) + ")" +
                 " and ic_trans_serial_number.inquiry_type=0)" +
                " or ((trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้) + " and ic_trans_serial_number.inquiry_type in (0,1)) " +
                " or (trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้) + " and ic_trans_serial_number.inquiry_type=0))";
            string __subQueryFormay = "coalesce((select " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + " {0} from ic_trans_serial_number where ic_trans_serial_number.last_status=0 and (" + __normalCase + ")) order by doc_date desc,doc_time desc " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + "),{1})";
            // สถานะ
            string __subQuery4 = "case when " + String.Format(__subQueryFormay, "calc_flag", "0") + "=1 then 0 else 1 end";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_serial set status=" + __subQuery4 + " where status<>" + __subQuery4));
            // wh_code
            string __subQuery5 = String.Format(__subQueryFormay, "wh_code", "\'\'");
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_serial set wh_code=" + __subQuery5 + " where wh_code<>" + __subQuery5));
            // shelf_code
            string __subQuery6 = String.Format(__subQueryFormay, "shelf_code", "\'\'");
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_serial set shelf_code=" + __subQuery6 + " where shelf_code<>" + __subQuery6));

            string __debug = __query.ToString();
            return __query.ToString(); ;
        }

        public string _creditCardQueryList()
        {
            // status 
            //0 = ปรกติ
            //1 = ขึ้นเงินแล้ว
            //2 = ยกเลิก
            string __creditCardStatusChangeFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน).ToString() + "," +
                 _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก).ToString();

            string __creditStatusQuery = "coalesce(( select " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + " " +
                " (case when " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน).ToString() + " then 1 else " +
                " (case when " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก).ToString() + " then 2 else 0 end) end) " +
                " from " + _g.d.ic_trans_detail._table +
                " where " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._last_status + "=0 " +
                " and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._chq_number + "=" + _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._chq_number +
                " and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + " in (" + __creditCardStatusChangeFlag + ") " +

                // and doc_ref and doc_line_number 
                " and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_ref + " =" + _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._doc_ref +
                " and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._ref_row + " =" + _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._doc_line_number +

                " order by " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_date + " desc, " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_time + " desc " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + "), 0)";
            StringBuilder __query = new StringBuilder();

            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.cb_chq_list._table + " set " + _g.d.cb_chq_list._status + "=" + __creditStatusQuery + " where " + _g.d.cb_chq_list._status + "<>" + __creditStatusQuery + " and " + _g.d.cb_chq_list._chq_type + "=" + _g.g._chqType(_g.g._chqTypeEnum.บัตรเครดิต)));

            return __query.ToString();
        }

        public string _ChequeQueryList()
        {
            // chq_type = 1 เช็ครับ
            // chq_type = 2 เช็คจ่าย

            StringBuilder __query = new StringBuilder();
            // เช็คในมือ status = 0
            // เช็คนำฝาก status = 1 flag=410
            // เช็คผ่าน status = 2
            // เช็ครับคืน status = 3
            // เช็คยกเลิก(ขาดสิทธิ์) status = 4 
            // เช็คขายลด status = 5
            // เช็คคืนนำเข้าใหม่ status = 6
            // เช็คเปลี่ยน = 7

            //select * from ic_trans where doc_no = 'CHD12100001' 
            //select * from ic_trans_detail where doc_no = 'CHD12100001'  
            string __chqInStatusChangeFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา).ToString() + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก).ToString() + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน).ToString() + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน).ToString() + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก).ToString() + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่).ToString() + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค).ToString();

            string __chqInStatusQuery = "coalesce(( select " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + " " +
                " (case when " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก).ToString() + " then 1 else " +
                " (case when " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน).ToString() + " then 2 else " +
                " (case when " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน).ToString() + " then 3 else " +
                " (case when " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก).ToString() + " then 4 else " +
                " (case when " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่).ToString() + " then 6 else " +
                " (case when " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค).ToString() + " then 7 else 0 end) end) end) end) end) end) " +
                " from " + _g.d.ic_trans_detail._table +
                " where " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._last_status + "=0 " +
                " and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._chq_number + "=" + _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._chq_number +
                " and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + " in (" + __chqInStatusChangeFlag + ") " +
                // and doc_ref and doc_line_number
                " and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_ref + " =" + _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._doc_ref +
                " and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._ref_row + " =" + _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._doc_line_number +

                " order by " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_date + " desc, " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_time + " desc " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + "), 0)";

            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.cb_chq_list._table + " set " + _g.d.cb_chq_list._status + "=" + __chqInStatusQuery + " where " + _g.d.cb_chq_list._status + "<>" + __chqInStatusQuery + " and " + _g.d.cb_chq_list._chq_type + "=" + _g.g._chqType(_g.g._chqTypeEnum.เช็ครับ)));

            string __chqOutStatusChangeFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา).ToString() + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน).ToString() + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน).ToString() + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก).ToString() + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค).ToString();

            string __chqOutStatusQuery = "coalesce((select " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + " " +
                " (case when " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน).ToString() + " then 2 else " +
                " (case when " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก).ToString() + " then 4 else " +
                " ( case when " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน).ToString() + " then 3 else " +
                " ( case when " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค).ToString() + " then 7 else 0 end) end) " + " end) end) " +
                " from " + _g.d.ic_trans_detail._table +
                " where " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._last_status + "=0 " +
                " and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._chq_number + "=" + _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._chq_number +
                " and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + " in (" + __chqOutStatusChangeFlag + ") " +
                // and doc_ref and doc_line_number
                " and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_ref + " =" + _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._doc_ref +
                " and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._ref_row + " =" + _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._doc_line_number +

                " order by " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_date + " desc, " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_time + " desc " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + "), 0)";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.cb_chq_list._table + " set " + _g.d.cb_chq_list._status + "=" + __chqOutStatusQuery + " where " + _g.d.cb_chq_list._status + "<>" + __chqOutStatusQuery + " and " + _g.d.cb_chq_list._chq_type + "=" + _g.g._chqType(_g.g._chqTypeEnum.เช็คจ่าย)));

            // chq flow query
            // update last status
            /*
            // เช็ครับ ฝาก
            string __refQuery = "(select doc_ref from ic_trans as temp1 where temp1.trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก_ยกเลิก) + " and temp1.doc_ref=ic_trans.doc_no)";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=0 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก) + " and last_status<>0 and not exists " + __refQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=1 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก) + " and last_status<>1 and exists " + __refQuery));
            // เช็ครับ ผ่าน
            __refQuery = "(select doc_ref from ic_trans as temp1 where temp1.trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน_ยกเลิก) + " and temp1.doc_ref=ic_trans.doc_no)";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=0 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน) + " and last_status<>0 and not exists " + __refQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=1 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน) + " and last_status<>1 and exists " + __refQuery));
            // เช็ครับ คืน
            __refQuery = "(select doc_ref from ic_trans as temp1 where temp1.trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน_ยกเลิก) + " and temp1.doc_ref=ic_trans.doc_no)";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=0 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน) + " and last_status<>0 and not exists " + __refQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=1 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน) + " and last_status<>1 and exists " + __refQuery));
            // เช็ครับ ยกเลิก
            __refQuery = "(select doc_ref from ic_trans as temp1 where temp1.trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก_ยกเลิก) + " and temp1.doc_ref=ic_trans.doc_no)";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=0 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก) + " and last_status<>0 and not exists " + __refQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=1 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก) + " and last_status<>1 and exists " + __refQuery));
            // เช็ครับ เข้าใหม่
            __refQuery = "(select doc_ref from ic_trans as temp1 where temp1.trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่_ยกเลิก) + " and temp1.doc_ref=ic_trans.doc_no)";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=0 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่) + " and last_status<>0 and not exists " + __refQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=1 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่) + " and last_status<>1 and exists " + __refQuery));
            // เช็ครับ เปลี่ยนเช็ค
            __refQuery = "(select doc_ref from ic_trans as temp1 where temp1.trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค_ยกเลิก) + " and temp1.doc_ref=ic_trans.doc_no)";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=0 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค) + " and last_status<>0 and not exists " + __refQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=1 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค) + " and last_status<>1 and exists " + __refQuery));

            // เช็คจ่าย ผ่าน
            __refQuery = "(select doc_ref from ic_trans as temp1 where temp1.trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน_ยกเลิก) + " and temp1.doc_ref=ic_trans.doc_no)";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=0 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน) + " and last_status<>0 and not exists " + __refQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=1 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน) + " and last_status<>1 and exists " + __refQuery));
            // เช็คจ่าย ยกเลิก
            __refQuery = "(select doc_ref from ic_trans as temp1 where temp1.trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก_ยกเลิก) + " and temp1.doc_ref=ic_trans.doc_no)";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=0 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก) + " and last_status<>0 and not exists " + __refQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=1 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก) + " and last_status<>1 and exists " + __refQuery));
            // เช็คจ่าย คืน
            __refQuery = "(select doc_ref from ic_trans as temp1 where temp1.trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน_ยกเลิก) + " and temp1.doc_ref=ic_trans.doc_no)";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=0 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน) + " and last_status<>0 and not exists " + __refQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=1 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน) + " and last_status<>1 and exists " + __refQuery));
            // เช็คจ่าย เปลี่ยนเช็ค
            __refQuery = "(select doc_ref from ic_trans as temp1 where temp1.trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค_ยกเลิก) + " and temp1.doc_ref=ic_trans.doc_no)";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=0 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค) + " and last_status<>0 and not exists " + __refQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set last_status=1 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค) + " and last_status<>1 and exists " + __refQuery));


            // update used_status
            // เช็ครับ ฝาก
            __refQuery = "(select doc_no from ic_trans_detail where trans_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก) + " and exists( select chq_number from ic_trans_detail as t1 where t1.trans_flag  = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน) + " and t1.last_status = 0 and ic_trans_detail.chq_number = t1.chq_number and ic_trans_detail.doc_ref = t1.doc_ref and ic_trans_detail.ref_row = t1.ref_row ) and ic_trans.doc_no = ic_trans_detail.doc_no )";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=0 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก) + " and used_status<>0 and not exists " + __refQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=1 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก) + " and used_status<>1 and exists " + __refQuery));

            // เช็ครับ ผ่าน ที่เอาไปทำคืนแล้ว
            __refQuery = "(select doc_no from ic_trans_detail where trans_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน) + " and exists( select chq_number from ic_trans_detail as t1 where t1.trans_flag  in ( " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน) + ") and t1.last_status = 0 and ic_trans_detail.chq_number = t1.chq_number and ic_trans_detail.doc_ref = t1.doc_ref and ic_trans_detail.ref_row = t1.ref_row) and ic_trans.doc_no = ic_trans_detail.doc_no )";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=0 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน) + " and used_status<>0 and not exists " + __refQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=1 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน) + " and used_status<>1 and exists " + __refQuery));

            // เช็ครับ คืน ที่เอาไปทำ ยกเลิกขาดสิทธิ์แล้ว,การเปลี่ยนเช็คแล้ว
            __refQuery = "(select doc_no from ic_trans_detail where trans_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน) + " and exists( select chq_number from ic_trans_detail as t1 where t1.trans_flag  in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก) + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค) + ") and t1.last_status = 0 and ic_trans_detail.chq_number = t1.chq_number and ic_trans_detail.doc_ref = t1.doc_ref and ic_trans_detail.ref_row = t1.ref_row) and ic_trans.doc_no = ic_trans_detail.doc_no )";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=0 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน) + " and used_status<>0 and not exists " + __refQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=1 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน) + " and used_status<>1 and exists " + __refQuery));

            // เช็คจ่าย ผ่าน
            // เช็คจ่าย ผ่าน ที่เอาไปทำคืนแล้ว
            __refQuery = "(select doc_no from ic_trans_detail where trans_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน) + " and exists( select chq_number from ic_trans_detail as t1 where t1.trans_flag  in ( " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน) + ") and t1.last_status = 0 and ic_trans_detail.chq_number = t1.chq_number and ic_trans_detail.doc_ref = t1.doc_ref and ic_trans_detail.ref_row = t1.ref_row) and ic_trans.doc_no = ic_trans_detail.doc_no )";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=0 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน) + " and used_status<>0 and not exists " + __refQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=1 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน) + " and used_status<>1 and exists " + __refQuery));

            // เช็คจ่าย คืน เอาไปทำขาดสิทธิ์แล้ว , เปลี่ยนเช็ค
            __refQuery = "(select doc_no from ic_trans_detail where trans_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน) + " and exists( select chq_number from ic_trans_detail as t1 where t1.trans_flag  in ( " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก) + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค) + ") and t1.last_status = 0 and ic_trans_detail.chq_number = t1.chq_number and ic_trans_detail.doc_ref = t1.doc_ref and ic_trans_detail.ref_row = t1.ref_row) and ic_trans.doc_no = ic_trans_detail.doc_no )";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=0 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน) + " and used_status<>0 and not exists " + __refQuery));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set used_status=1 where trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน) + " and used_status<>1 and exists " + __refQuery));
            */
            return __query.ToString();
        }

        public _docFlowThread(_g.g._transControlTypeEnum transFlag, string itemCodePack, string docNoPack)
        {
            this._transFlagEnum = transFlag;
            this._itemCodePack = itemCodePack;
            this._docNoPack = docNoPack;
        }

        public String _processAPFlowQueryList()
        {
            StringBuilder __query = new StringBuilder();
            __query.Append(this._icFlowQueryListFormat(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น, _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก));
            __query.Append(this._icFlowQueryListFormat(_g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น, _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น_ยกเลิก));
            __query.Append(this._icFlowQueryListFormat(_g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น, _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก));

            return __query.ToString();
        }

        public String _processARFlowQueryList()
        {
            StringBuilder __query = new StringBuilder();
            __query.Append(this._icFlowQueryListFormat(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น, _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น_ยกเลิก));
            __query.Append(this._icFlowQueryListFormat(_g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น, _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น_ยกเลิก));
            __query.Append(this._icFlowQueryListFormat(_g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น, _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น_ยกเลิก));

            return __query.ToString();
        }

        public string _cbFlowQuerList()
        {
            StringBuilder __query = new StringBuilder();
            // เงินสดย่อย
            __query.Append(this._icFlowQueryListFormat(_g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย, _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย_ยกเลิก));
            __query.Append(this._icFlowQueryListFormat(_g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย, _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย_ยกเลิก));

            __query.Append(this._icFlowQueryListFormat(_g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน, _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน_ยกเลิก));
            __query.Append(this._icFlowQueryListFormat(_g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน, _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน_ยกเลิก));
            // โอนเงิน ใช้เอกสารยกเลิกใบเดียวกัน
            __query.Append(this._icFlowQueryListFormat(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร, _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร_ยกเลิก));
            __query.Append(this._icFlowQueryListFormat(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินเข้าธนาคาร, _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร_ยกเลิก));

            // ค่าใช้จ่ายอื่น ๆ
            __query.Append(this._icFlowQueryListFormat(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น, _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก));
            __query.Append(this._icFlowQueryListFormat(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้, _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก));
            __query.Append(this._icFlowQueryListFormat(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้, _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก));
            // รายได้อื่น ๆ
            __query.Append(this._icFlowQueryListFormat(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น, _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ยกเลิก));
            __query.Append(this._icFlowQueryListFormat(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้, _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก));
            __query.Append(this._icFlowQueryListFormat(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้, _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก));


            return __query.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string _processUsedStatusDepositQuery()
        {
            StringBuilder __query = new StringBuilder(); ;

            return __query.ToString();
        }

        /// <summary>
        /// ไม่ได้ใช้ สร้าง Query Used Status 2 ที่อ้างอิง จากการจ่ายชำระ รับชำระ
        /// </summary>
        /// <param name="controlType"></param>
        /// <param name="docNoList"></param>
        /// <returns></returns>
        public string _createICTraansLastStatusQuery(_g.g._transControlTypeEnum controlType, string docNoList)
        {
            return _createICTraansLastStatusQuery(controlType, docNoList, _g.g._transControlTypeEnum.ว่าง);
        }


        /// <summary>
        /// ไม่ได้ใช้ ประมวลผล แบบกลุ่ม ฝั่ง IC last_status, used_status, used_status_2, doc_usccess ของเอกสารที่เกี่ยวข้องใน flag นั้น  
        /// </summary>
        /// <param name="controlType"></param>
        /// <param name="docNoList"></param>
        /// <param name="baseControlType"></param>
        /// <returns></returns>
        public string _createICTraansLastStatusQuery(_g.g._transControlTypeEnum controlType, string docNoList, _g.g._transControlTypeEnum baseControlType)
        {

            StringBuilder __query = new StringBuilder();

            string __last_status_query = "";
            string __used_status_query = "";
            string __used_status_2_qurey = "";
            string __doc_success_query = "";

            string __tableName = "";
            string __updateStr = "";
            string __whereStr = "";

            switch (controlType)
            {

                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                    __last_status_query = " (case when ((select count(doc_no) from ic_trans as x1 where x1.doc_ref = ic_trans.doc_no and x1.trans_flag = (case when ic_trans.trans_flag = 30 then 31 else (case when ic_trans.trans_flag = 34 then 39 else (case when ic_trans.trans_flag = 36 then 37 else -1 end ) end ) end )) > 0) then 1 else 0 end ) ";
                    __used_status_query = "  (case when (select count(x2.doc_no) from ( (select doc_no from ic_trans_detail as x1 where x1.ref_doc_no = ic_trans.doc_no and x1.trans_flag in (44,34,36)) ) as x2 )> 0 then 1 else 0 end)  ";
                    __doc_success_query = " ( " +
                        " case when ( ((select count(*) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=ic_trans.trans_flag and ic_trans.last_status=0 )>0)  " +
                        " and ((select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=ic_trans.trans_flag and ic_trans_detail.last_status=0 and coalesce(ic_trans_detail.item_code, '') <> '')-(select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.ref_doc_no=ic_trans.doc_no and (  " +
                        " ic_trans_detail.trans_flag in (34, 36,44)  " +
                        " ) and ic_trans_detail.last_status=0 and coalesce(ic_trans_detail.item_code, '') <> '')<=0) " +
                        " ) " +
                        " then 1 " +
                        " else 0  " +
                        " end " +
                        ") ";

                    __updateStr = " last_status = " + __last_status_query + ", used_status = " + __used_status_query + ", doc_success = " + __doc_success_query;
                    __whereStr = " trans_flag in (30,34,36) and ( last_status = " + __last_status_query + " or used_status <> " + __used_status_query + " or doc_success <> " + __doc_success_query + " ) ";

                    if (docNoList.Length > 0)
                    {
                        __whereStr = __whereStr + " and exists ( select distinct ref_doc_no from ic_trans_detail where trans_flag = " + _g.g._transFlagGlobal._transFlag(controlType) + " and coalesce(ref_doc_no, '') != '' and ic_trans_detail.ref_doc_no = ic_trans.doc_no )";
                    }

                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล:
                    // update billing used_status_2 
                    __tableName = _g.d.ic_trans._table;
                    __last_status_query = " (case when ((select count(doc_no) from ic_trans as x1 where x1.doc_ref = ic_trans.doc_no and x1.trans_flag = (case when ic_trans.trans_flag = 44 then 45 else (case when ic_trans.trans_flag = 46 then 47 else (case when ic_trans.trans_flag = 48 then 49 else (case when ic_trans.trans_flag = 99 then 100 else (case when ic_trans.trans_flag = 101 then 102 else (case when ic_trans.trans_flag = 103 then 104 else (case when ic_trans.trans_flag = 250 then 251 else (case when ic_trans.trans_flag = 252 then 253 else (case when ic_trans.trans_flag = 254 then 255 else -1 end ) end ) end ) end ) end ) end ) end ) end ) end )) > 0) then 1 else 0 end )  ";
                    __used_status_query = " ( case when (ic_trans.trans_flag = 44 and  ic_trans.inquiry_type in (0,2)) then " +
                        "	case when  (select count(doc_no) from ic_trans_detail as x1 where x1.ref_doc_no = ic_trans.doc_no and x1.trans_flag in ( 46, 48 ) and x1.ref_doc_no != '' and (select last_status from ic_trans as x2 where  x2.doc_no = x1.doc_no and x2.trans_flag = x1.trans_flag)=0 )> 0 then 1 else 0 end " +
                        " else " +
                        " 	case when (ic_trans.trans_flag = 99) then  " +
                        " 		case when  (select count(doc_no) from ic_trans_detail as x1 where x1.ref_doc_no = ic_trans.doc_no and x1.trans_flag in ( 101, 104 ) and x1.ref_doc_no != '' and (select last_status from ic_trans as x2 where  x2.doc_no = x1.doc_no and x2.trans_flag = x1.trans_flag)=0 )> 0 then 1 else 0 end " +
                        "	else  " +
                        "		case when  (ic_trans.trans_flag = 250 and  ic_trans.inquiry_type in (0,2)) then " +
                        "			case when  (select count(doc_no) from ic_trans_detail as x1 where x1.ref_doc_no = ic_trans.doc_no and x1.trans_flag in ( 252, 254 ) and x1.ref_doc_no != '' and (select last_status from ic_trans as x2 where  x2.doc_no = x1.doc_no and x2.trans_flag = x1.trans_flag)=0 )> 0 then 1 else 0 end " +
                        "		else " +
                        "		0 " +
                        "		end		 " +
                        "	end " +
                        "end " +
                        " ) ";
                    __used_status_2_qurey = "(case when (select count(billing_no) from ap_ar_trans_detail where ap_ar_trans_detail.trans_flag in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล) + ", " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้) + ") and (select last_status from ap_ar_trans where ap_ar_trans.doc_no = ap_ar_trans_detail.doc_no and ap_ar_trans.trans_flag = ap_ar_trans_detail.trans_flag) = 0 and ap_ar_trans_detail.billing_no = ic_trans.doc_no and ap_ar_trans_detail.bill_type = ic_trans.trans_flag ) > 0 then 1 else 0 end )";

                    __updateStr = " last_status = " + __last_status_query + ", used_status=" + __used_status_query + ", used_status_2 = " + __used_status_2_qurey;

                    __whereStr = " trans_flag in ( " + //in (
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้) + "," +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้ยกมา) + "," +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้ยกมา) + "," +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้ยกมา) + "," +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น) + "," +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น) + "," +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น) + ", " + // ) 
                                                                                                                 // " or " +
                                                                                                                 //" (trans_flag in (" +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + ", " +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้) + ", " +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น) + ", " +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้) + ", " + // and inquiry_type in (0,2,6)) ) 
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้) + ") "; // // and inquiry_type in (0,2,4))


                    /*(if (docNoList.Length > 0)
                        __whereStr = __whereStr + " and exists ( select doc_no from ap_ar_trans_detail  where trans_flag =  " + _g.g._transFlagGlobal._transFlag(controlType) + " and doc_no in (" + docNoList + ") and ic_trans.doc_no = ap_ar_trans_detail.billing_no  and ic_trans.trans_flag = ap_ar_trans_detail.bill_type ) ";
                    */
                    if (docNoList.Length > 0)
                    {
                        switch (baseControlType)
                        {
                            case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก:
                                __whereStr += " and exists ( select billing_no from ap_ar_trans_detail where ap_ar_trans_detail.doc_no in ( select doc_ref from ap_ar_trans where ap_ar_trans.doc_no in (" + docNoList + ") and ap_ar_trans.trans_flag = 240 ) and ap_ar_trans_detail.trans_flag = 239 and ap_ar_trans_detail.billing_no = ic_trans.doc_no and ap_ar_trans_detail.bill_type = ic_trans.trans_flag) ";
                                break;
                            case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล_ยกเลิก:
                                __whereStr += " and exists ( select billing_no from ap_ar_trans_detail where ap_ar_trans_detail.doc_no in ( select doc_ref from ap_ar_trans where ap_ar_trans.doc_no in (" + docNoList + ") and ap_ar_trans.trans_flag = 236 ) and ap_ar_trans_detail.trans_flag = 235 and ap_ar_trans_detail.billing_no = ic_trans.doc_no and ap_ar_trans_detail.bill_type = ic_trans.trans_flag) ";
                                break;
                            case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                                __whereStr += " and exists ( select billing_no from ap_ar_trans_detail where ap_ar_trans_detail.doc_no in ( " + docNoList + " ) and ap_ar_trans_detail.trans_flag = 239 and ap_ar_trans_detail.billing_no = ic_trans.doc_no and ap_ar_trans_detail.bill_type = ic_trans.trans_flag) ";
                                break;
                            case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล:
                                __whereStr += " and exists ( select billing_no from ap_ar_trans_detail where ap_ar_trans_detail.doc_no in ( " + docNoList + " ) and ap_ar_trans_detail.trans_flag = 235 and ap_ar_trans_detail.billing_no = ic_trans.doc_no and ap_ar_trans_detail.bill_type = ic_trans.trans_flag) ";
                                break;
                        }
                    }

                    __whereStr += " and ( last_status <> " + __last_status_query + " or used_status <> " + __used_status_query + " or used_status_2 <> " + __used_status_2_qurey + " ) ";


                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format("update {0} set {1} where {2}", __tableName, __updateStr, __whereStr)));

                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล:
                case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                    __tableName = _g.d.ic_trans._table;
                    __used_status_2_qurey = "(case when (select count(billing_no) from ap_ar_trans_detail where ap_ar_trans_detail.trans_flag in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล) + ", " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้) + ") and (select last_status from ap_ar_trans where ap_ar_trans.doc_no = ap_ar_trans_detail.doc_no and ap_ar_trans.trans_flag = ap_ar_trans_detail.trans_flag) = 0 and ap_ar_trans_detail.billing_no = ic_trans.doc_no and ap_ar_trans_detail.bill_type = ic_trans.trans_flag ) > 0 then 1 else 0 end )";

                    __updateStr = " used_status_2 = " + __used_status_2_qurey;
                    __whereStr = " used_status_2 <> " + __used_status_2_qurey + " and  ( trans_flag in (" +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด) + "," +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้) + "," +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้ยกมา) + "," +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้ยกมา) + "," +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้ยกมา) + "," +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น) + "," +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น) + "," +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น) + ") " +
                        " or ( trans_flag in (" +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด) + "," +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้) + ") and   inquiry_type in (0,1) ) " +
                        " or " +
                        " (trans_flag in (" +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ) + ", " +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้) + ", " +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น) + ", " +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้) + ", " +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้) + ") and inquiry_type in (0,2)) ) " + ((docNoList.Length > 0) ? " and exists ( select doc_no from ap_ar_trans_detail  where trans_flag =  " + _g.g._transFlagGlobal._transFlag(controlType) + " and doc_no in (" + docNoList + ") and ic_trans.doc_no = ap_ar_trans_detail.billing_no  and ic_trans.trans_flag = ap_ar_trans_detail.bill_type ) " : "");

                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format("update {0} set {1} where {2}", __tableName, __updateStr, __whereStr)));

                    break;

            }

            return __query.ToString();
        }

        /// <summary>
        /// ไมได้ใช้
        /// </summary>
        /// <param name="controlType"></param>
        /// <param name="docNoList"></param>
        /// <returns></returns>
        public string _createAPAARTransProcessDocStatusQuery(_g.g._transControlTypeEnum controlType, string docNoList)
        {
            return _createAPAARTransProcessDocStatusQuery(controlType, docNoList, _g.g._transControlTypeEnum.ว่าง);
        }


        /// <summary>
        /// ไมได้ใช้ ประมวลผล last_status, used_status, used_status_2, doc_usccess ของเอกสารที่เกี่ยวข้องใน flag นั้น  แบบ 1 ต่อ 1
        /// </summary>
        /// <param name="controlType">ประเภทเอกสารที่ต้องการ update สถานะ </param>
        /// <returns></returns>
        public string _createAPAARTransProcessDocStatusQuery(_g.g._transControlTypeEnum controlType, string docNoList, _g.g._transControlTypeEnum baseControlType)
        {
            StringBuilder __query = new StringBuilder();

            string __last_status_query = "";
            string __used_status_query = "";
            string __used_status_2_qurey = "";
            string __doc_success_query = "";

            string __tableName = "";
            string __updateStr = "";
            string __whereStr = "";

            switch (controlType)
            {
                #region
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า:
                    {
                        __tableName = _g.d.ic_trans._table;
                        __last_status_query = "(case when ((select count(doc_no) from ic_trans as x1 where x1.trans_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก) + " and x1.doc_no = ic_trans.doc_no) > 0) then 1 else 0 end )";
                        __used_status_query = "(case when (select count(doc_no) from (select doc_no from ic_trans as temp1 where temp1.trans_flag in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน) + ") and temp1.last_status = 0 and temp1.doc_ref=ic_trans.doc_no union all select trans_number as doc_no from cb_trans_detail where cb_trans_detail.trans_number=ic_trans.doc_no) as temp2)>0 then 1 else 0 end )";

                        __updateStr = " last_status=" + __last_status_query + ",used_status=" + __used_status_query;
                        __whereStr = " trans_flag in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า) + ") " + ((docNoList.Length > 0) ? " and doc_no in (" + docNoList + ")" : "") + " and last_status<>" + __last_status_query + " and used_status<>" + __used_status_query;

                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format("update {0} set {1} where {2}", __tableName, __updateStr, __whereStr)));
                    }
                    break;
                #endregion

                #region ขาย
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                    __last_status_query = " (case when( (select count(doc_ref) from ic_trans as x1 where x1.trans_flag = 31 and x1.doc_ref=ic_trans.doc_no) > 0) then 1 else 0 end)";
                    __used_status_query = " (case when( " +
                        " select count(ref_doc_no) from ( " +
                        " select distinct ref_doc_no from ic_trans_detail as x where x.ref_doc_no is not null and x.trans_flag in (31,34,36,44) and x.last_status=0 and x.ref_doc_no = ic_trans.doc_no " +
                        " union all " +
                        " select distinct doc_ref as ref_doc_no from ic_trans as x2 where x2.doc_ref is not null and x2.trans_flag=32 and x2.last_status=0 and x2.doc_ref = ic_trans.doc_no " +
                        " ) as temp1 " +
                        "  )> 0 then 1 else 0 end )";
                    __doc_success_query = " ( " +
                        " case when " +
                        " (select count(*) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=30 and ic_trans_detail.last_status=0)>0 " +
                        " and  " +
                        "  ( " +
                        "  (select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=30 and ic_trans_detail.last_status=0 and coalesce(ic_trans_detail.item_code, '') <> '') " +
                        " - " +
                        " (select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.ref_doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag in (31,34,36,44) and ic_trans_detail.last_status=0 and coalesce(ic_trans_detail.item_code, '') <> '' ) " +
                        " )<=0  " +
                        " then 1 else 0 end " +
                        " ) ";


                    __updateStr = " last_status=" + __last_status_query + ",used_status=" + __used_status_query + ",doc_success=" + __doc_success_query;


                    __whereStr = " trans_flag = 30 ";

                    if (docNoList.Length > 0)
                    {
                        switch (baseControlType)
                        {
                            case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                                __whereStr += " and exists (select distinct ref_doc_no from ic_trans_detail where ic_trans_detail.trans_flag = " + _g.g._transFlagGlobal._transFlag(baseControlType) + " ic_trans_detail.doc_no in (" + docNoList + ") and ic_trans_detail.ref_doc_no = ic_trans.doc_no )";
                                break;
                            case _g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก:
                            case _g.g._transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ:
                                __whereStr += " and exists (select doc_ref from ic_trans as x3 where x3.trans_flag=" + _g.g._transFlagGlobal._transFlag(baseControlType) + " and x3.doc_no in (" + docNoList + ") and x3.doc_ref = ic_trans.doc_no )";
                                break;
                            case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                                __whereStr += " and exists (select distinct ref_doc_no from ic_trans_detail where  ic_trans_detail.doc_no in (select doc_ref from ic_trans where trans_flag = 39 and doc_no in (" + docNoList + ") and cancel_type = 1) and ic_trans_detail.ref_doc_no = ic_trans.doc_no )";
                                break;
                        }
                    }

                    __whereStr += " and ( last_status<>" + __last_status_query + " or used_status<>" + __used_status_query + " or doc_success <> " + __doc_success_query + ") ";

                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format("update {0} set {1} where {2}", __tableName, __updateStr, __whereStr)));

                    break;

                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:

                    break;

                case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า:
                    __tableName = _g.d.ic_trans._table;
                    __last_status_query = "(case when ((select count(doc_no) from ic_trans as x1 where x1.trans_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก) + " and x1.doc_no = ic_trans.doc_no) > 0) then 1 else 0 end )";
                    __used_status_query = "(case when (select count(doc_no) from (select doc_no from ic_trans as temp1 where temp1.trans_flag in (42) and temp1.last_status = 0 and temp1.doc_ref=ic_trans.doc_no union all select trans_number as doc_no from cb_trans_detail where cb_trans_detail.trans_number=ic_trans.doc_no) as temp2)>0 then 1 else 0 end )";

                    __updateStr = " last_status=" + __last_status_query + ",used_status=" + __used_status_query;
                    __whereStr = " trans_flag in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า) + ") " + ((docNoList.Length > 0) ? " and doc_no in (" + docNoList + ")" : "") + " and last_status<>" + __last_status_query + " and used_status<>" + __used_status_query;

                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format("update {0} set {1} where {2}", __tableName, __updateStr, __whereStr)));

                    break;
                #endregion

                #region เจ้าหนี้

                case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล:
                    {
                        __tableName = _g.d.ap_ar_trans._table;
                        __last_status_query = " (case when ((select count(doc_no) from ap_ar_trans as x1 where x1.trans_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล_ยกเลิก) + " and x1.doc_ref = ap_ar_trans.doc_no) > 0) then 1 else 0 end ) ";
                        __used_status_query = " (case when ( select count(doc_no) from ap_ar_trans_detail where ap_ar_trans_detail.doc_ref = ap_ar_trans.doc_no and (select last_status from ap_ar_trans as t2 where t2.doc_no = ap_ar_trans_detail.doc_no and t2.trans_flag = ap_ar_trans_detail.trans_flag) = 0) > 0 then 1 else 0 end) ";
                        __doc_success_query = " (case when ( coalesce((select sum(coalesce(sum_pay_money, 0)) from ap_ar_trans_detail where ap_ar_trans_detail.doc_no = ap_ar_trans.doc_no and ap_ar_trans.trans_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล) + " and ap_ar_trans.last_status = 0 ), 0)-coalesce((select sum(coalesce(sum_pay_money, 0)) from ap_ar_trans_detail as x1 where x1.trans_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้) + "  and x1.last_status = 0 and exists (select billing_no from ap_ar_trans_detail as x2 where x2.doc_no = ap_ar_trans.doc_no and x2.trans_flag = ap_ar_trans.trans_flag and x1.billing_no = x2.billing_no and x1.bill_type = x2.bill_type)), 0)) = 0 then 1 else 0 end ) ";

                        __updateStr = " last_status = " + __last_status_query + " ,used_status  = " + __used_status_query + ", doc_success =" + __doc_success_query;
                        __whereStr = " trans_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล) + " " + ((docNoList.Length > 0) ? " and doc_no in (" + docNoList + ")" : "") + " and last_status <> " + __last_status_query + " and used_status  <> " + __used_status_query + " and doc_success =" + __doc_success_query;

                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format("update {0} set {1} where {2}", __tableName, __updateStr, __whereStr)));
                    }
                    break;

                case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:

                    __tableName = _g.d.ap_ar_trans._table;
                    __last_status_query = "(case when ((select count(doc_no) from ap_ar_trans as x1 where x1.trans_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก) + " and x1.doc_ref = ap_ar_trans.doc_no) > 0) then 1 else 0 end )";

                    __updateStr = "last_status=" + __last_status_query;
                    __whereStr = " trans_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้) + " and last_status<>" + __last_status_query + ((docNoList.Length > 0) ? " and doc_no in (" + docNoList + ")" : "");

                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format("update {0} set {1} where {2}", __tableName, __updateStr, __whereStr)));

                    break;

                #endregion

                #region ลูกหนี้

                case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล:
                    {
                        __tableName = _g.d.ap_ar_trans._table;
                        __last_status_query = " (case when ((select count(doc_no) from ap_ar_trans as x1 where x1.trans_flag = 236 and x1.doc_ref = ap_ar_trans.doc_no) > 0) then 1 else 0 end ) ";
                        __used_status_query = " (case when ( select count(doc_no) from ap_ar_trans_detail where ap_ar_trans_detail.doc_ref = ap_ar_trans.doc_no and (select last_status from ap_ar_trans as t2 where t2.doc_no = ap_ar_trans_detail.doc_no and t2.trans_flag = ap_ar_trans_detail.trans_flag) = 0) > 0 then 1 else 0 end) ";
                        __doc_success_query = " (case when ( coalesce((select sum(coalesce(sum_pay_money, 0)) from ap_ar_trans_detail where ap_ar_trans_detail.doc_no = ap_ar_trans.doc_no and ap_ar_trans.trans_flag = 235 and ap_ar_trans.last_status = 0 ), 0)-coalesce((select sum(coalesce(sum_pay_money, 0)) from ap_ar_trans_detail as x1 where x1.trans_flag = 239  and x1.last_status = 0 and exists (select billing_no from ap_ar_trans_detail as x2 where x2.doc_no = ap_ar_trans.doc_no and x2.trans_flag = ap_ar_trans.trans_flag and x1.billing_no = x2.billing_no and x1.bill_type = x2.bill_type)), 0)) = 0 then 1 else 0 end ) ";

                        __updateStr = " last_status = " + __last_status_query + " ,used_status  = " + __used_status_query + ", doc_success =" + __doc_success_query;
                        __whereStr = " trans_flag = 235 ";

                        if (docNoList.Length > 0)
                        {
                            switch (baseControlType)
                            {
                                case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก:
                                    __whereStr += " and exists ( select distinct doc_ref from ap_ar_trans_detail where ap_ar_trans_detail.trans_flag = 239 and ap_ar_trans_detail.doc_no in (select c1.doc_ref from ap_ar_trans as c1 where c1.trans_flag = 240 and c1.doc_no = in (" + docNoList + ")) and ap_ar_trans_detail.doc_ref = ap_ar_trans.doc_no  ) ";
                                    break;
                                case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล_ยกเลิก:
                                    __whereStr += " and doc_no = (select c1.doc_ref from ap_ar_trans as c1 where c1.trans_flag = 236 and c1.doc_no in (" + docNoList + ") ) ";
                                    break;
                                case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                                    __whereStr += " and exists ( select distinct doc_ref from ap_ar_trans_detail where ap_ar_trans_detail.trans_flag = 239 and ap_ar_trans_detail.doc_no in (" + docNoList + ") and ap_ar_trans_detail.doc_ref = ap_ar_trans.doc_no  ) ";
                                    break;
                            }
                        }

                        __whereStr += " and last_status <> " + __last_status_query + " and used_status  <> " + __used_status_query + " and doc_success =" + __doc_success_query;

                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format("update {0} set {1} where {2}", __tableName, __updateStr, __whereStr)));
                    }
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                    __tableName = _g.d.ap_ar_trans._table;
                    __last_status_query = "(case when ((select count(doc_no) from ap_ar_trans as x1 where x1.trans_flag = 240 and x1.doc_ref = ap_ar_trans.doc_no) > 0) then 1 else 0 end )";

                    __updateStr = "last_status=" + __last_status_query;
                    __whereStr = " trans_flag = 239 and last_status<>" + __last_status_query + ((docNoList.Length > 0) ? " and doc_no in (" + docNoList + ")" : "");

                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format("update {0} set {1} where {2}", __tableName, __updateStr, __whereStr)));

                    break;

                    #endregion
            }

            return __query.ToString();
        }


        public string _createUpdateDetailStatusQuery(_g.g._transControlTypeEnum controlType, string docNoList)
        {
            StringBuilder __query = new StringBuilder();

            string __last_status_query = "";
            string __used_status_query = "";
            string __used_status_2_qurey = "";
            string __doc_success_query = "";

            string __tableName = "";
            string __updateStr = "";
            string __whereStr = "";

            switch (controlType)
            {
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                    __last_status_query = "(select last_status from ic_trans where ic_trans.trans_flag = ic_trans_detail.trans_flag ";
                    break;
            }



            return __query.ToString();
        }

        public string _processARDocQuery(_g.g._transControlTypeEnum controlType, string docNoList)
        {
            StringBuilder __query = new StringBuilder();

            string __last_status_query = "";
            string __used_status_query = "";
            string __used_status_2_qurey = "";
            string __doc_success_query = "";
            string __updateStr = "";
            string __whereStr = "";

            __last_status_query = " (case when ((select count(doc_no) from ic_trans as x1 where x1.doc_ref = ic_trans.doc_no and x1.trans_flag = (case when ic_trans.trans_flag = 44 then 45 else (case when ic_trans.trans_flag = 46 then 47 else (case when ic_trans.trans_flag = 48 then 49 else (case when ic_trans.trans_flag = 99 then 100 else (case when ic_trans.trans_flag = 101 then 102 else (case when ic_trans.trans_flag = 103 then 104 else (case when ic_trans.trans_flag = 250 then 251 else (case when ic_trans.trans_flag = 252 then 253 else (case when ic_trans.trans_flag = 254 then 255 else -1 end ) end ) end ) end ) end ) end ) end ) end ) end )) > 0) then 1 else 0 end )  ";
            __used_status_query = " ( case when (ic_trans.trans_flag = 44 and  ic_trans.inquiry_type in (0,2)) then " +
                "	case when  (select count(doc_no) from ic_trans_detail as x1 where x1.ref_doc_no = ic_trans.doc_no and x1.trans_flag in ( 46, 48 ) and x1.ref_doc_no != '' and (select last_status from ic_trans as x2 where  x2.doc_no = x1.doc_no and x2.trans_flag = x1.trans_flag)=0 )> 0 then 1 else 0 end " +
                " else " +
                " 	case when (ic_trans.trans_flag = 99) then  " +
                " 		case when  (select count(doc_no) from ic_trans_detail as x1 where x1.ref_doc_no = ic_trans.doc_no and x1.trans_flag in ( 101, 104 ) and x1.ref_doc_no != '' and (select last_status from ic_trans as x2 where  x2.doc_no = x1.doc_no and x2.trans_flag = x1.trans_flag)=0 )> 0 then 1 else 0 end " +
                "	else  " +
                "		case when  (ic_trans.trans_flag = 250 and  ic_trans.inquiry_type in (0,2)) then " +
                "			case when  (select count(doc_no) from ic_trans_detail as x1 where x1.ref_doc_no = ic_trans.doc_no and x1.trans_flag in ( 252, 254 ) and x1.ref_doc_no != '' and (select last_status from ic_trans as x2 where  x2.doc_no = x1.doc_no and x2.trans_flag = x1.trans_flag)=0 )> 0 then 1 else 0 end " +
                "		else " +
                "		0 " +
                "		end		 " +
                "	end " +
                "end " +
                " ) ";
            __used_status_2_qurey = "(case when (select count(billing_no) from ap_ar_trans_detail where ap_ar_trans_detail.trans_flag in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล) + ", " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้) + ") and (select last_status from ap_ar_trans where ap_ar_trans.doc_no = ap_ar_trans_detail.doc_no and ap_ar_trans.trans_flag = ap_ar_trans_detail.trans_flag) = 0 and ap_ar_trans_detail.billing_no = ic_trans.doc_no and ap_ar_trans_detail.bill_type = ic_trans.trans_flag ) > 0 then 1 else 0 end )";

            __updateStr = " last_status = " + __last_status_query + ", used_status=" + __used_status_query + ", used_status_2 = " + __used_status_2_qurey;

            __whereStr = " trans_flag in ( " + //in (
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้) + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้ยกมา) + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้ยกมา) + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้ยกมา) + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น) + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น) + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น) + ", " + // ) 
                                                                                                         // " or " +
                                                                                                         //" (trans_flag in (" +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + ", " +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้) + ", " +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น) + ", " +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้) + ", " + // and inquiry_type in (0,2,6)) ) 
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้) + ") "; // // and inquiry_type in (0,2,4))


            //(if (docNoList.Length > 0)
            //    __whereStr = __whereStr + " and exists ( select doc_no from ap_ar_trans_detail  where trans_flag =  " + _g.g._transFlagGlobal._transFlag(controlType) + " and doc_no in (" + docNoList + ") and ic_trans.doc_no = ap_ar_trans_detail.billing_no  and ic_trans.trans_flag = ap_ar_trans_detail.bill_type ) ";

            if (docNoList.Length > 0)
            {
                switch (controlType)
                {
                    case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก:
                        __whereStr += " and exists ( select billing_no from ap_ar_trans_detail where ap_ar_trans_detail.doc_no in ( select doc_ref from ap_ar_trans where ap_ar_trans.doc_no in (" + docNoList + ") and ap_ar_trans.trans_flag = 240 ) and ap_ar_trans_detail.trans_flag = 239 and ap_ar_trans_detail.billing_no = ic_trans.doc_no and ap_ar_trans_detail.bill_type = ic_trans.trans_flag) ";
                        break;
                    case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล_ยกเลิก:
                        __whereStr += " and exists ( select billing_no from ap_ar_trans_detail where ap_ar_trans_detail.doc_no in ( select doc_ref from ap_ar_trans where ap_ar_trans.doc_no in (" + docNoList + ") and ap_ar_trans.trans_flag = 236 ) and ap_ar_trans_detail.trans_flag = 235 and ap_ar_trans_detail.billing_no = ic_trans.doc_no and ap_ar_trans_detail.bill_type = ic_trans.trans_flag) ";
                        break;
                    case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                        __whereStr += " and exists ( select billing_no from ap_ar_trans_detail where ap_ar_trans_detail.doc_no in ( " + docNoList + " ) and ap_ar_trans_detail.trans_flag = 239 and ap_ar_trans_detail.billing_no = ic_trans.doc_no and ap_ar_trans_detail.bill_type = ic_trans.trans_flag) ";
                        break;
                    case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล:
                        __whereStr += " and exists ( select billing_no from ap_ar_trans_detail where ap_ar_trans_detail.doc_no in ( " + docNoList + " ) and ap_ar_trans_detail.trans_flag = 235 and ap_ar_trans_detail.billing_no = ic_trans.doc_no and ap_ar_trans_detail.bill_type = ic_trans.trans_flag) ";
                        break;
                }
            }

            __whereStr += " and ( last_status <> " + __last_status_query + " or used_status <> " + __used_status_query + " or used_status_2 <> " + __used_status_2_qurey + " ) ";
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format("update {0} set {1} where {2}", _g.d.ic_trans._table, __updateStr, __whereStr)));


            // detail

            //__last_status_query = " (select last_status from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag )  ";
            //__updateStr = " last_status = " + __last_status_query;

            //__whereStr = " last_status <> " + __last_status_query;
            //switch (controlType)
            //{
            //    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
            //        __whereStr += " trans_flag = 44 and doc_no in (" + docNoList + ") ";
            //        break;
            //    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก:
            //        __whereStr += " trans_flag = 44 and doc_no in (select doc_ref from ic_trans where ic_trans.doc_no in (" + docNoList + ")) ";
            //        break;
            //    default:
            //        break;

            //}
            //__query.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format("update {0} set {1} where {2}", _g.d.ic_trans_detail._table, __updateStr, __whereStr)));


            return __query.ToString();

        }


        public string _processPurchaseQuery(_g.g._transControlTypeEnum controlType, string docNoList)
        {
            StringBuilder __query = new StringBuilder();

            string __last_status_query = "";
            string __used_status_query = "";
            string __doc_success_query = "";
            string __updateStr = "";
            string __whereStr = "";

            __last_status_query = "( case when (select count(*) from ic_trans as x where x.doc_ref = ic_trans.doc_no and x.trans_flag = (case when ic_trans.trans_flag = 2 then 3 else (case when ic_trans.trans_flag = 6 then 7 else -1 end) end) )>0 then 1 else 0 end)  ";
            __used_status_query = "";
            __doc_success_query = "";


            __updateStr = " last_status = " + __last_status_query + " ";
            __whereStr = " trans_flag in (2,4,6) ";

            __whereStr += " and ( last_status <> " + __last_status_query + " ) "; // or  used_status <> " + __used_status_query + " or  doc_success <> " + __doc_success_query + " 

            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format("update {0} set {1} where {2}", _g.d.ic_trans._table, __updateStr, __whereStr)));

            // detail

            // update detail
            __last_status_query = " (select last_status from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag )  ";
            __updateStr = " last_status = " + __last_status_query;

            __whereStr = " last_status <> " + __last_status_query;
            switch (controlType)
            {
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                    __whereStr += " and trans_flag = 12 and doc_no in (" + docNoList + ") ";
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก:
                    __whereStr += " and trans_flag = 12 and doc_no in (select doc_ref from ic_trans where ic_trans.doc_no in (" + docNoList + ")) ";
                    break;
                default:
                    break;

            }
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format("update {0} set {1} where {2}", _g.d.ic_trans_detail._table, __updateStr, __whereStr)));



            return __query.ToString();

        }


        public string _processSaleDocQuery(_g.g._transControlTypeEnum controlType, string docNoList)
        {
            StringBuilder __query = new StringBuilder();

            string __last_status_query = "";
            string __used_status_query = "";
            string __doc_success_query = "";
            string __updateStr = "";
            string __whereStr = "";




            __last_status_query = " (case when ((select count(doc_no) from ic_trans as x1 where x1.doc_ref = ic_trans.doc_no and x1.trans_flag = (case when ic_trans.trans_flag = 30 then 31 else (case when ic_trans.trans_flag = 34 then 39 else (case when ic_trans.trans_flag = 36 then 37 else -1 end ) end ) end )) > 0) then 1 else 0 end ) ";
            __used_status_query = "  (case when (select count(x2.doc_no) from ( (select doc_no from ic_trans_detail as x1 where x1.ref_doc_no = ic_trans.doc_no and x1.trans_flag in (44,34,36)) union all select doc_no from ic_trans as x4 where x4.trans_flag in (32,38,52) and x4.doc_ref = ic_trans.doc_no  ) as x2 )> 0 then 1 else 0 end)  ";
            __doc_success_query = " ( case when ( ((select count(item_code) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=ic_trans.trans_flag and ic_trans.last_status=0 )>0) and ((select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=ic_trans.trans_flag and ic_trans_detail.last_status=0 and coalesce(ic_trans_detail.item_code, '') <> '')-(select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.ref_doc_no=ic_trans.doc_no and ( ic_trans_detail.trans_flag in (34, 36,44) ) and ic_trans_detail.last_status=0 and coalesce(ic_trans_detail.item_code, '') <> '')<=0) ) then 1 else 0 end ) ";

            __updateStr = " last_status = " + __last_status_query + ", used_status = " + __used_status_query + ", doc_success = " + __doc_success_query;
            __whereStr = " trans_flag in (30,34,36) ";



            if (docNoList.Length > 0)
            {
                switch (controlType)
                {
                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                        __whereStr += "  and exists ( select distinct ref_doc_no from ic_trans_detail where trans_flag = 44 and ic_trans_detail.ref_doc_no in (" + docNoList + ") and ic_trans_detail.ref_doc_no = ic_trans.doc_no ) ";
                        break;
                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก:
                        __whereStr += "  and exists ( select distinct ref_doc_no from ic_trans_detail where trans_flag = 44 and ic_trans_detail.doc_no in ( select doc_ref from ic_trans as t5 where t5.trans_flag = 45 and t5.doc_no in (" + docNoList + ")) and ic_trans_detail.ref_doc_no = ic_trans.doc_no ) ";
                        break;
                    default:
                        //__whereStr += "  and exists ( select distinct ref_doc_no from ic_trans_detail where trans_flag = " + _g.g._transFlagGlobal._transFlag(controlType) + " and ic_trans_detail.ref_doc_no in (" + docNoList + ") and ic_trans_detail.ref_doc_no = ic_trans.doc_no ) ";
                        break;
                }
                //__whereStr = __whereStr + " and exists ( select distinct ref_doc_no from ic_trans_detail where trans_flag = " + _g.g._transFlagGlobal._transFlag(controlType) + " and coalesce(ref_doc_no, '') != '' and ic_trans_detail.ref_doc_no = ic_trans.doc_no )";
            }
            __whereStr += " and ( last_status <> " + __last_status_query + " or used_status <> " + __used_status_query + " or doc_success <> " + __doc_success_query + " ) ";

            // update เสนอราคา, สั่งขาย, สั่งจอง
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format("update {0} set {1} where {2}", _g.d.ic_trans._table, __updateStr, __whereStr)));


            // update detail
            __last_status_query = " (select last_status from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag )  ";
            __updateStr = " last_status = " + __last_status_query;

            __whereStr = " last_status <> " + __last_status_query;
            switch (controlType)
            {
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                    __whereStr += " and trans_flag = 44 and doc_no in (" + docNoList + ") ";
                    break;
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก:
                    __whereStr += " and trans_flag = 44 and doc_no in (select doc_ref from ic_trans where ic_trans.doc_no in (" + docNoList + ")) ";
                    break;
                default:
                    break;

            }
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format("update {0} set {1} where {2}", _g.d.ic_trans_detail._table, __updateStr, __whereStr)));


            return __query.ToString();
        }

        public string _processAPDocQuery(_g.g._transControlTypeEnum controlType, string docNoList)
        {
            StringBuilder __query = new StringBuilder();

            string __last_status_query = "";
            string __used_status_query = "";
            string __used_status_2_qurey = "";
            string __doc_success_query = "";
            string __updateStr = "";
            string __whereStr = "";

            __last_status_query = " (case when (select count(doc_ref) from ic_trans as c where c.doc_ref = ic_trans.doc_no and c.trans_flag = " +
                " (case when ic_trans.trans_flag = 12 then 13 else (case when ic_trans.trans_flag = 14 then 15 else (case when ic_trans.trans_flag = 16 then 17 else " +
                " (case when ic_trans.trans_flag = 87 then 88 else (case when ic_trans.trans_flag = 89 then 90 else (case when ic_trans.trans_flag = 91 then 92 else " +
                " (case when ic_trans.trans_flag = 260 then 261 else (case when ic_trans.trans_flag = 262 then 263 else (case when ic_trans.trans_flag = 264 then 265 else -1 end) end) end) " +
                " end) end) end)  " +
                " end) end) end) " +
                " 	) " +
                " >0 then 1 else 0 end ) ";

            __used_status_query = "( case when ( select count( ref_doc_no ) from ic_trans_detail  where ic_trans_detail.last_status = 0 and ic_trans_detail.ref_doc_no=ic_trans.doc_no " +
                " and ( (ic_trans.trans_flag = 12 and ic_trans_detail.trans_flag in (14,16)) or " +
                " (ic_trans.trans_flag = 315 and ic_trans_detail.trans_flag in (316, 317)) or " +
                " (ic_trans.trans_flag = 87 and ic_trans_detail.trans_flag in (89,91)) or   " +
                " (ic_trans.trans_flag=260 and ic_trans_detail.trans_flag in (262,264)) " +
                " ) " +
                " ) " +
                " > 0  then 1 else 0 end )";

            __used_status_2_qurey = "(case when (select count(billing_no) from ap_ar_trans_detail where ap_ar_trans_detail.trans_flag in (213,19) and (select last_status from ap_ar_trans where ap_ar_trans.doc_no = ap_ar_trans_detail.doc_no and ap_ar_trans.trans_flag = ap_ar_trans_detail.trans_flag) = 0 and ap_ar_trans_detail.billing_no = ic_trans.doc_no and ap_ar_trans_detail.bill_type = ic_trans.trans_flag ) > 0 then 1 else 0 end )";

            __updateStr = " last_status = " + __last_status_query + ", used_status = " + __used_status_query + " , used_status_2 = " + __used_status_2_qurey;
            __whereStr = " trans_flag in (" +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ) + ", " +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด) + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด) + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้) + ", " +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้) + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้) + ", " +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้ยกมา) + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้ยกมา) + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้ยกมา) + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น) + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น) + "," +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น) + ", " +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น) + ", " +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้) + ", " +
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้) + ")  ";


            if (docNoList.Length > 0)
            {
                //__whereStr += " and exists ( select doc_no from ap_ar_trans_detail  where trans_flag =  " + _g.g._transFlagGlobal._transFlag(controlType) + " and doc_no in (" + docNoList + ") and ic_trans.doc_no = ap_ar_trans_detail.billing_no  and ic_trans.trans_flag = ap_ar_trans_detail.bill_type ) " : "");
            }

            __whereStr += " and ( last_status <> " + __last_status_query + " or used_status <> " + __used_status_query + " or  used_status_2 <> " + __used_status_2_qurey + " ) ";

            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format("update {0} set {1} where {2}", _g.d.ic_trans._table, __updateStr, __whereStr)));


            return __query.ToString();
        }

        public void _docFlowProcess()
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            // _docNoPack = "\'PI1608-001\'";
            if (this._docNoPack.Length > 0)
            {
                string __getDocQuery = "select roworder, doc_no, trans_flag from ic_trans where doc_no in (" + this._docNoPack + ") union all select roworder, doc_no, trans_flag from ap_ar_trans where doc_no in (" + this._docNoPack + ")  union all select roworder, doc_no, trans_flag from ic_wms_trans where doc_no in (" + this._docNoPack + ")  union all select roworder, doc_no, trans_flag from pp_shipment where doc_no in (" + this._docNoPack + ")  ";
                DataSet __docResult = __myFrameWork._queryShort(__getDocQuery);


                if (__docResult.Tables.Count > 0 && __docResult.Tables[0].Rows.Count > 0)
                {
                    for (int __row = 0; __row < __docResult.Tables[0].Rows.Count; __row++)
                    {
                        string __tableName = "ic_trans";

                        string __fieldDocRefCount = "ref_doc_count"; // นับจำนวนเอกสารอ้างอิง กรณีมากกว่า 0 ให้ถือว่ามีการอ้างอิงแล้ว
                        string __fieldDocRefCount2 = "ref_doc_count_2"; // นับจำนวนเอกสารอ้างอิง กรณีมากกว่า 0 ให้ถือว่ามีการอ้างอิงแล้ว

                        string __fieldCancelDocNo = "cancel_doc_no_count";
                        string __fieldDetailRowCount = "detailrowcount";
                        string __fieldCountItemQty = "item_count";
                        string __fidldReduceItemQty = "reduce_count";

                        string __fieldCheckApproveStatus = "approve_count";

                        string __fieldDocSuccess = "doc_success";
                        string __fieldUsedStatus = "used_status";
                        string __fieldLastStatus = "last_status";
                        string __fieldUsedStatus2 = "used_status_2";
                        string __fieldApproveStatus = "0 as approve_status";


                        string __queryDocRefCount = "0";
                        string __queryDocRefCount2 = "0";

                        string __queryDocCancelCount = "0";

                        string __queryDocSuccess = "doc_success";
                        string __queryUsedStatus = "used_status";
                        string __queryLastStatus = "last_status";

                        string __queryDetailRowCount = "0";
                        string __queryCountItemQty = " 0"; // จำนวนรายการต้นทาง
                        string __queryReduceItemQty = "0"; // จำนวนรายการที่นำไปใช้
                        string __queryApproveStatus = "0";

                        string __docNo = __docResult.Tables[0].Rows[__row]["doc_no"].ToString();
                        int __transFlag = MyLib._myGlobal._intPhase(__docResult.Tables[0].Rows[__row]["trans_flag"].ToString());
                        _g.g._transControlTypeEnum _transType = _g.g._transFlagGlobal._transFlagByNumber(__transFlag);

                        // for next process
                        string __queryDocRefForNextProcess = "";

                        switch (_transType)
                        {

                            #region สินค้า

                            case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 55 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 54 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_calcel_temp ) ";
                                break;
                            case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 61 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 60 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                break;
                            case _g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 123 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 122 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp  ) ";
                                break;
                            case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 57 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 56 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                break;
                            case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 59 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 58 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                break;
                            case _g.g._transControlTypeEnum.สินค้า_ขอโอน:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 125 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 124 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp  ) ";
                                __queryCountItemQty = "(select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=124 and ic_trans_detail.last_status=0 and ic_trans_detail.item_code <> '' )";
                                __queryReduceItemQty = "(select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.ref_doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag in (72) and ic_trans_detail.last_status=0)";
                                break;
                            case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 73 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag in (70,72) and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                break;
                            case _g.g._transControlTypeEnum.สินค้า_โอนเข้า:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 73 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag in (70,72) and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                break;
                            case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 67 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 66 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                break;
                            case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 69 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 68 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                break;
                            #endregion

                            #region ซื้อ
                            case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 3 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 2 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                __queryDocRefCount = "(select count(*) from (select doc_no from ic_trans as doc_ref where doc_ref.doc_ref = ic_trans.doc_no and doc_ref.trans_flag = 4 union all select ref_doc_no  from ic_trans_detail where ic_trans_detail.last_status = 0 and ic_trans_detail.ref_doc_no = ic_trans.doc_no and ic_trans_detail.trans_flag in (6) ) as doc_ref_2)";

                                __fieldApproveStatus = "approve_status";
                                __queryApproveStatus = "(case when (coalesce((select " + MyLib._myGlobal._getTopAndLimitOneRecord()[0].ToString() + " cast(not_approve_1 as int) from ic_trans as doc_approve where doc_approve.doc_ref=ic_trans.doc_no and trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ) + " " + MyLib._myGlobal._getTopAndLimitOneRecord()[1].ToString() + " ),0)) = 1 then 2 else 1 end  )";


                                break;
                            case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ:
                                __queryDocRefCount = " (select count(ref_doc_no)  from ic_trans_detail where ic_trans_detail.last_status = 0 and ic_trans_detail.ref_doc_no = ic_trans.doc_no and ic_trans_detail.trans_flag in (6)) ";
                                __queryDocCancelCount = "(select count(doc_no) from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 4 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) ";
                                break;
                            case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 7 and doc_cancel.cancel_type = 1 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 6 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                __queryDocRefCount = "(select count(*) from (select doc_no from ic_trans as doc_ref where doc_ref.doc_ref = ic_trans.doc_no and doc_ref.trans_flag = 8 and doc_ref.last_status = 0 union all select doc_no from ic_trans as doc_ref_cancel where doc_ref_cancel.doc_ref = ic_trans.doc_no and doc_ref_cancel.trans_flag = 7  and doc_ref_cancel.cancel_type = 2 union all select ref_doc_no  from ic_trans_detail where ic_trans_detail.last_status = 0 and ic_trans_detail.ref_doc_no = ic_trans.doc_no and ic_trans_detail.trans_flag in (12,310)   union all select doc_no from erp_credit_approved_logs where erp_credit_approved_logs.doc_no = ic_trans.doc_no and erp_credit_approved_logs.trans_flag = erp_credit_approved_logs.trans_flag ) as doc_ref_2)";

                                __queryDetailRowCount = " (select count(*) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=6 and ic_trans_detail.last_status=0)";
                                __queryCountItemQty = "(select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=6 and ic_trans_detail.last_status=0 and ic_trans_detail.item_code <> '' )";
                                __queryReduceItemQty = "(select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.ref_doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag in (7,12) and ic_trans_detail.last_status=0)";

                                // 
                                __fieldApproveStatus = "approve_status";
                                //__queryApproveStatus = "(case when (coalesce((select " + MyLib._myGlobal._getTopAndLimitOneRecord()[0].ToString() + " cast(not_approve_1 as int) from ic_trans as doc_approve where doc_approve.doc_ref=ic_trans.doc_no and trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ) + " " + MyLib._myGlobal._getTopAndLimitOneRecord()[1].ToString() + " ),0)) = 1 then 2 else 1 end  )";
                                //"(coalesce((select " + MyLib._myGlobal._getTopAndLimitOneRecord()[0].ToString() + " cast(not_approve_1 as int) from ic_trans as temp2 where temp2.doc_ref=ic_trans.doc_no and trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ) + " " + MyLib._myGlobal._getTopAndLimitOneRecord()[1].ToString() + "),0))";
                                __queryApproveStatus = "(case when coalesce(auto_approved, 0) = 1 then 1 when (coalesce((select  cast(not_approve_1 as int) from ic_trans as doc_approve where doc_approve.doc_ref=ic_trans.doc_no and trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ) + "  offset 0 limit 1  ),-1)) = 1 then 2 when(coalesce((select  cast(not_approve_1 as int) from ic_trans as doc_approve where doc_approve.doc_ref = ic_trans.doc_no and trans_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ) + "  offset 0 limit 1), -1)) = -1 then coalesce((select approve_status from erp_credit_approved_logs where erp_credit_approved_logs.doc_no = ic_trans.doc_no and erp_credit_approved_logs.trans_flag = ic_trans.trans_flag limit 1), 0) else 1 end )";
                                break;
                            case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ:
                                __queryDocRefForNextProcess = "select doc_ref from ic_trans as doc_ref where doc_ref.doc_no = \'" + __docNo + "\' and doc_ref.last_status=0";
                                __queryDocCancelCount = "(select count(doc_no) from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 8 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) ";

                                break;
                            case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                                __queryDocRefForNextProcess = "select doc_ref from ic_trans as doc_ref where doc_ref.doc_no = \'" + __docNo + "\' and doc_ref.last_status=0";

                                break;
                            case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 13 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 12 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                __queryDocRefCount = "(select count(ref_doc_no) from ic_trans_detail as doc_ref where doc_ref.ref_doc_no = ic_trans.doc_no and doc_ref.trans_flag in (14,16) and doc_ref.last_status=0 )";
                                __queryDocRefCount2 = "(select count(billing_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_trans.doc_no and doc_ref.bill_type = ic_trans.trans_flag and doc_ref.last_status=0 )";

                                __queryDocRefForNextProcess = "select billing_no from ap_ar_trans_detail as doc_ref where doc_ref.doc_no = \'" + __docNo + "\' and doc_ref.last_status=0";

                                break;
                            case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 15 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 14 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                __queryDocRefCount2 = "(select count(billing_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_trans.doc_no and doc_ref.bill_type = ic_trans.trans_flag and doc_ref.last_status=0 )";

                                __queryDocRefForNextProcess = "select billing_no from ap_ar_trans_detail as doc_ref where doc_ref.doc_no = \'" + __docNo + "\' and doc_ref.last_status=0";

                                break;
                            case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 17 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 16 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                __queryDocRefCount2 = "(select count(billing_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_trans.doc_no and doc_ref.bill_type = ic_trans.trans_flag and doc_ref.last_status=0 )";

                                __queryDocRefForNextProcess = "select billing_no from ap_ar_trans_detail as doc_ref where doc_ref.doc_no = \'" + __docNo + "\' and doc_ref.last_status=0";

                                break;

                            case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                                __queryDocRefCount = "(select count(ref_doc_no) from ic_trans_detail as doc_ref where doc_ref.ref_doc_no = ic_trans.doc_no and doc_ref.trans_flag in (311,315) and doc_ref.last_status=0 )"; //"(select count(doc_no) from (select doc_no from ic_trans_detail as doc_ref where doc_ref.ref_doc_no = ic_trans.doc_no and doc_ref.trans_flag in (311,315) and doc_ref.last_status=0 union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 310 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";

                                __queryDocCancelCount = "(select count(doc_no) from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 310 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no) ";

                                __queryCountItemQty = "(select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=310 and ic_trans_detail.last_status=0 and ic_trans_detail.item_code <> '' )";
                                __queryReduceItemQty = "(select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.ref_doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag = 315 and ic_trans_detail.last_status=0)";

                                __queryDocRefForNextProcess = "select billing_no from ap_ar_trans_detail as doc_ref where doc_ref.doc_no = \'" + __docNo + "\' and doc_ref.last_status=0";
                                break;

                            case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                                __queryDocRefCount = "(select count(ref_doc_no) from ic_trans_detail as doc_ref where doc_ref.ref_doc_no = ic_trans.doc_no and doc_ref.trans_flag in (316,317) and doc_ref.last_status=0 )";
                                __queryDocRefCount2 = "(select count(billing_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_trans.doc_no and doc_ref.bill_type = ic_trans.trans_flag and doc_ref.last_status=0 )";

                                __queryDocCancelCount = "(select count(doc_no) from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 315 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no) ";

                                __queryDocRefForNextProcess = "select billing_no from ap_ar_trans_detail as doc_ref where doc_ref.doc_no = \'" + __docNo + "\' and doc_ref.last_status=0";
                                break;

                            case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                                __queryDocRefCount2 = "(select count(billing_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_trans.doc_no and doc_ref.bill_type = ic_trans.trans_flag and doc_ref.last_status=0 )";
                                __queryDocCancelCount = "(select count(doc_no) from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 316 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no) ";

                                __queryDocRefForNextProcess = "select billing_no from ap_ar_trans_detail as doc_ref where doc_ref.doc_no = \'" + __docNo + "\' and doc_ref.last_status=0";
                                break;

                            case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                                __queryDocRefCount2 = "(select count(billing_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_trans.doc_no and doc_ref.bill_type = ic_trans.trans_flag and doc_ref.last_status=0 )";
                                __queryDocCancelCount = "(select count(doc_no) from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 317 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no) ";

                                __queryDocRefForNextProcess = "select billing_no from ap_ar_trans_detail as doc_ref where doc_ref.doc_no = \'" + __docNo + "\' and doc_ref.last_status=0";
                                break;
                            case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                                __queryDocRefCount2 = "(select count(billing_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_trans.doc_no and doc_ref.bill_type = ic_trans.trans_flag and doc_ref.last_status=0 )";
                                __queryDocCancelCount = "(select count(doc_no) from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 311 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no) ";

                                __queryDocRefForNextProcess = "select billing_no from ap_ar_trans_detail as doc_ref where doc_ref.doc_no = \'" + __docNo + "\' and doc_ref.last_status=0";
                                break;

                            case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 151 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 11 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";

                                __queryDetailRowCount = "1";
                                __queryDocRefCount = "(select count(doc_ref) from ic_trans as doc_ref where doc_ref.doc_ref = ic_trans.doc_no and doc_ref.trans_flag in (25) and doc_ref.last_status=0 )";
                                __queryDocRefCount2 = "(select count(trans_number) from cb_trans_detail where cb_trans_detail.trans_number = ic_trans.doc_no and cb_trans_detail.doc_type = 6 and cb_trans_detail.last_status=0 )";

                                __queryCountItemQty = "total_amount";
                                __queryReduceItemQty = "coalesce(( select sum(coalesce(total_amount, 0)) from ( " +
                                    " select total_amount from ic_trans as a where a.doc_ref = ic_trans.doc_no and a.trans_flag = 25 and a.last_status = 0 " +
                                    " union all " +
                                    " select amount as total_amount from cb_trans_detail where cb_trans_detail.doc_type = 6 and cb_trans_detail.trans_number = ic_trans.doc_no and cb_trans_detail.last_status=0 " +
                                    ") as temp1 ), 0) ";

                                break;

                            case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 150 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 10 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";

                                __queryDetailRowCount = "1";
                                __queryDocRefCount = "(select count(doc_ref) from ic_trans as doc_ref where doc_ref.doc_ref = ic_trans.doc_no and doc_ref.trans_flag in (20) and doc_ref.last_status=0 )";
                                __queryDocRefCount2 = "(select count(trans_number) from cb_trans_detail where cb_trans_detail.trans_number = ic_trans.doc_no and cb_trans_detail.doc_type = 5 and cb_trans_detail.last_status=0 )";

                                __queryCountItemQty = "total_amount";
                                __queryReduceItemQty = "coalesce(( select sum(coalesce(total_amount, 0)) from ( " +
                                    " select total_amount from ic_trans as a where a.doc_ref = ic_trans.doc_no and a.trans_flag = 20 and a.last_status = 0 " +
                                    " union all " +
                                    " select amount as total_amount from cb_trans_detail where cb_trans_detail.doc_type = 5 and cb_trans_detail.trans_number = ic_trans.doc_no and cb_trans_detail.last_status=0 " +
                                    ") as temp1 ), 0) ";

                                break;

                            #endregion

                            #region ขาย
                            case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 31 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 30 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                __queryDocRefCount = "(select count(*) from (select doc_no from ic_trans as doc_ref where doc_ref.doc_ref = ic_trans.doc_no and doc_ref.trans_flag = 32 union all select ref_doc_no  from ic_trans_detail where ic_trans_detail.last_status = 0 and ic_trans_detail.ref_doc_no = ic_trans.doc_no and ic_trans_detail.trans_flag in (34,36,44) ) as doc_ref_2)";
                                __queryDetailRowCount = " (select count(*) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=30 and ic_trans_detail.last_status=0)";
                                __queryCountItemQty = "(select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=30 and ic_trans_detail.last_status=0 and ic_trans_detail.item_code <> '' )";
                                __queryReduceItemQty = "(select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.ref_doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag in (34,36,44) and ic_trans_detail.last_status=0)";

                                break;

                            case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 39 and doc_cancel.doc_ref = ic_trans.doc_no and doc_cancel.cancel_type = 1 union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 34 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                __queryDocRefCount = "(select count(*) from (select doc_no from ic_trans as doc_ref where doc_ref.doc_ref = ic_trans.doc_no and doc_ref.trans_flag = 38 union all select ref_doc_no  from ic_trans_detail where ic_trans_detail.last_status = 0 and ic_trans_detail.ref_doc_no = ic_trans.doc_no and ic_trans_detail.trans_flag in (36,44) union all select doc_no from erp_credit_approved_logs where erp_credit_approved_logs.doc_no = ic_trans.doc_no and erp_credit_approved_logs.trans_flag = erp_credit_approved_logs.trans_flag ) as doc_ref_2)";
                                __queryDetailRowCount = " (select count(*) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=34 and ic_trans_detail.last_status=0)";
                                __queryCountItemQty = "(select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=34 and ic_trans_detail.last_status=0 and ic_trans_detail.item_code <> '' )";
                                __queryReduceItemQty = "(select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.ref_doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag in (36,44) and ic_trans_detail.last_status=0)";

                                __queryDocRefForNextProcess = "select billing_no from ap_ar_trans_detail as doc_ref where doc_ref.doc_no = \'" + __docNo + "\' and doc_ref.last_status=0";

                                __fieldApproveStatus = "approve_status";
                                //__queryApproveStatus = "(case when (coalesce((select " + MyLib._myGlobal._getTopAndLimitOneRecord()[0].ToString() + " cast(not_approve_1 as int) from ic_trans as doc_approve where doc_approve.doc_ref=ic_trans.doc_no and trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ) + " " + MyLib._myGlobal._getTopAndLimitOneRecord()[1].ToString() + " ),0)) = 1 then 2 else 1 end  )";
                                __queryApproveStatus = "(case when coalesce(auto_approved, 0) = 1 then 1 when (coalesce((select  cast(not_approve_1 as int) from ic_trans as doc_approve where doc_approve.doc_ref=ic_trans.doc_no and trans_flag=38  offset 0 limit 1  ),-1)) = 1 then 2 when(coalesce((select  cast(not_approve_1 as int) from ic_trans as doc_approve where doc_approve.doc_ref = ic_trans.doc_no and trans_flag = 38  offset 0 limit 1), -1)) = -1 then coalesce((select approve_status from erp_credit_approved_logs where erp_credit_approved_logs.doc_no = ic_trans.doc_no and erp_credit_approved_logs.trans_flag = ic_trans.trans_flag limit 1), 0) else 1 end )";

                                break;
                            case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ:
                                __queryDocRefForNextProcess = "select doc_ref from ic_trans as doc_ref where doc_ref.doc_no = \'" + __docNo + "\' and doc_ref.last_status=0";
                                break;
                            case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 37 and doc_cancel.doc_ref = ic_trans.doc_no and doc_cancel.cancel_type = 1 union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 36 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                __queryDocRefCount = "(select count(*) from (select doc_no from ic_trans as doc_ref where doc_ref.doc_ref = ic_trans.doc_no and doc_ref.trans_flag = 52 union all select ref_doc_no  from ic_trans_detail where ic_trans_detail.last_status = 0 and ic_trans_detail.ref_doc_no = ic_trans.doc_no and ic_trans_detail.trans_flag in (37,44) union all select doc_no from erp_credit_approved_logs where erp_credit_approved_logs.doc_no = ic_trans.doc_no and erp_credit_approved_logs.trans_flag = erp_credit_approved_logs.trans_flag ) as doc_ref_2)";
                                __queryDetailRowCount = " (select count(*) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=36 and ic_trans_detail.last_status=0)";
                                __queryCountItemQty = "(select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag=36 and ic_trans_detail.last_status=0 and ic_trans_detail.item_code <> '' )";
                                __queryReduceItemQty = "(select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_trans_detail where ic_trans_detail.ref_doc_no=ic_trans.doc_no and ic_trans_detail.trans_flag in (37,44) and ic_trans_detail.last_status=0)";

                                __queryDocRefForNextProcess = "select billing_no from ap_ar_trans_detail as doc_ref where doc_ref.doc_no = \'" + __docNo + "\' and doc_ref.last_status=0";

                                __fieldApproveStatus = "approve_status";
                                // __queryApproveStatus = "(case when (coalesce((select " + MyLib._myGlobal._getTopAndLimitOneRecord()[0].ToString() + " cast(not_approve_1 as int) from ic_trans as doc_approve where doc_approve.doc_ref=ic_trans.doc_no and trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ) + " " + MyLib._myGlobal._getTopAndLimitOneRecord()[1].ToString() + " ),0)) = 1 then 2 else 1 end  )";
                                __queryApproveStatus = "(case when coalesce(auto_approved, 0) = 1 then 1 when (coalesce((select  cast(not_approve_1 as int) from ic_trans as doc_approve where doc_approve.doc_ref=ic_trans.doc_no and trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ) + "  offset 0 limit 1  ),-1)) = 1 then 2 when(coalesce((select  cast(not_approve_1 as int) from ic_trans as doc_approve where doc_approve.doc_ref = ic_trans.doc_no and trans_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ) + "  offset 0 limit 1), -1)) = -1 then coalesce((select approve_status from erp_credit_approved_logs where erp_credit_approved_logs.doc_no = ic_trans.doc_no and erp_credit_approved_logs.trans_flag = ic_trans.trans_flag limit 1), 0) else 1 end )";


                                break;
                            case _g.g._transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ:
                                __queryDocRefForNextProcess = "select doc_ref from ic_trans as doc_ref where doc_ref.doc_no = \'" + __docNo + "\' and doc_ref.last_status=0";
                                break;
                            case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                                // __queryDocCancelCount = "(select count(doc_no) from ic_trans as doc_cancel where doc_cancel.trans_flag = 45 and doc_cancel.doc_ref = ic_trans.doc_no union all  ) ";

                                __queryDocCancelCount = " (select count(doc_count) from (select  doc_cancel.doc_no as doc_count from ic_trans as doc_cancel where doc_cancel.trans_flag = 45 and doc_cancel.doc_ref = ic_trans.doc_no union all  select doc_cancel2.doc_no as doc_count from ic_trans doc_cancel2 where doc_cancel2.trans_flag = 44 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_calcel_temp  ) ";

                                __queryDocRefCount = "( select count(doc_no) from ( " +
                                    "select ref_doc_no as doc_no from ic_trans_detail as doc_ref where doc_ref.ref_doc_no = ic_trans.doc_no and doc_ref.trans_flag in (46,48) and doc_ref.last_status=0 " +
                                    " union all " +
                                    " select doc_no from ic_trans_detail as r2 where r2.doc_ref = ic_trans.doc_no and r2.trans_flag in (410,411) and r2.last_status=0 " +
                                    ") as r3 )";
                                __queryDocRefCount2 = "(select count(billing_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_trans.doc_no and doc_ref.bill_type = ic_trans.trans_flag and doc_ref.last_status=0 )";

                                __queryDocRefForNextProcess = "select billing_no from ap_ar_trans_detail as doc_ref where doc_ref.doc_no = \'" + __docNo + "\' and doc_ref.last_status=0";
                                break;

                            case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 47 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 46 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                __queryDocRefCount2 = "(select count(billing_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_trans.doc_no and doc_ref.bill_type = ic_trans.trans_flag and doc_ref.last_status=0 )";

                                __queryDocRefForNextProcess = "select billing_no from ap_ar_trans_detail as doc_ref where doc_ref.doc_no = \'" + __docNo + "\' and doc_ref.last_status=0";
                                break;

                            case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 49 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 48 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                __queryDocRefCount2 = "(select count(billing_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_trans.doc_no and doc_ref.bill_type = ic_trans.trans_flag and doc_ref.last_status=0 )";

                                __queryDocRefForNextProcess = "select billing_no from ap_ar_trans_detail as doc_ref where doc_ref.doc_no = \'" + __docNo + "\' and doc_ref.last_status=0";

                                break;

                            case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 41 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 40 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";

                                __queryDetailRowCount = "1";
                                __queryDocRefCount = "(select count(doc_ref) from ic_trans as doc_ref where doc_ref.doc_ref = ic_trans.doc_no and doc_ref.trans_flag in (42) and doc_ref.last_status=0 )";
                                __queryDocRefCount2 = "(select count(trans_number) from cb_trans_detail where cb_trans_detail.trans_number = ic_trans.doc_no and cb_trans_detail.doc_type = 5 and cb_trans_detail.last_status=0 )";

                                __queryCountItemQty = "total_amount";
                                __queryReduceItemQty = "coalesce(( select sum(coalesce(total_amount, 0)) from ( " +
                                    " select total_amount from ic_trans as a where a.doc_ref = ic_trans.doc_no and a.trans_flag = 42 and a.last_status = 0 " +
                                    " union all " +
                                    " select amount as total_amount from cb_trans_detail where cb_trans_detail.doc_type = 5 and cb_trans_detail.trans_number = ic_trans.doc_no and cb_trans_detail.last_status=0 " +
                                    ") as temp1 ), 0) ";
                                break;

                            case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 111 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 110 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";

                                __queryDetailRowCount = "1";
                                __queryDocRefCount = "(select count(doc_ref) from ic_trans as doc_ref where doc_ref.doc_ref = ic_trans.doc_no and doc_ref.trans_flag in (112) and doc_ref.last_status=0 )";
                                __queryDocRefCount2 = "(select count(trans_number) from cb_trans_detail where cb_trans_detail.trans_number = ic_trans.doc_no and cb_trans_detail.doc_type = 6 and cb_trans_detail.last_status=0 )";

                                __queryCountItemQty = "total_amount";
                                __queryReduceItemQty = "coalesce(( select sum(coalesce(total_amount, 0)) from ( " +
                                    " select total_amount from ic_trans as a where a.doc_ref = ic_trans.doc_no and a.trans_flag = 112 and a.last_status = 0 " +
                                    " union all " +
                                    " select amount as total_amount from cb_trans_detail where cb_trans_detail.doc_type = 6 and cb_trans_detail.trans_number = ic_trans.doc_no and cb_trans_detail.last_status=0 " +
                                    ") as temp1 ), 0) ";
                                break;
                            #endregion

                            #region เจ้าหนี้

                            case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้ยกมา:
                                __queryDocCancelCount = "(select count(doc_no) from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 81 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no) ";
                                __queryDocRefCount2 = "(select count(billing_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_trans.doc_no and doc_ref.bill_type = ic_trans.trans_flag and doc_ref.last_status=0 )";
                                break;
                            case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้ยกมา:
                                __queryDocCancelCount = "(select count(doc_no) from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 83 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no) ";
                                __queryDocRefCount2 = "(select count(billing_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_trans.doc_no and doc_ref.bill_type = ic_trans.trans_flag and doc_ref.last_status=0 )";
                                break;
                            case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้ยกมา:
                                __queryDocCancelCount = "(select count(doc_no) from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 85 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no) ";
                                __queryDocRefCount2 = "(select count(billing_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_trans.doc_no and doc_ref.bill_type = ic_trans.trans_flag and doc_ref.last_status=0 )";
                                break;

                            case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 88 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 87 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                __queryDocRefCount = "(select count(ref_doc_no) from ic_trans_detail as doc_ref where doc_ref.ref_doc_no = ic_trans.doc_no and doc_ref.trans_flag in (89,91) and doc_ref.last_status=0 )";
                                __queryDocRefCount2 = "(select count(billing_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_trans.doc_no and doc_ref.bill_type = ic_trans.trans_flag and doc_ref.last_status=0 )";
                                break;
                            case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 90 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 89 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                __queryDocRefCount2 = "(select count(billing_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_trans.doc_no and doc_ref.bill_type = ic_trans.trans_flag and doc_ref.last_status=0 )";
                                break;
                            case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 92 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 91 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                __queryDocRefCount2 = "(select count(billing_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_trans.doc_no and doc_ref.bill_type = ic_trans.trans_flag and doc_ref.last_status=0 )";
                                break;

                            case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล:
                                __tableName = _g.d.ap_ar_trans._table;
                                __fieldUsedStatus2 = "0 as " + __fieldUsedStatus2;

                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from " + __tableName + " as doc_cancel where doc_cancel.trans_flag = 23 and doc_cancel.doc_ref = ap_ar_trans.doc_no union all select doc_no from ap_ar_trans as doc_cancel2 where doc_cancel2.trans_flag = 213 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ap_ar_trans.doc_no  ) as doc_cancel_temp ) ";
                                __queryDocRefCount = "(select count(doc_ref) from ap_ar_trans_detail as doc_ref where doc_ref.doc_ref = ap_ar_trans.doc_no and doc_ref.trans_flag in (19) and doc_ref.last_status=0 )";

                                __queryDetailRowCount = "(select count(billing_no) from ap_ar_trans_detail where  ap_ar_trans_detail.doc_no = ap_ar_trans.doc_no and ap_ar_trans_detail.trans_flag = ap_ar_trans.trans_flag)";
                                __queryCountItemQty = "coalesce((select sum(coalesce(sum_pay_money, 0)) from ap_ar_trans_detail where ap_ar_trans_detail.doc_no = ap_ar_trans.doc_no and ap_ar_trans.trans_flag = 213 ), 0)";
                                __queryReduceItemQty = "coalesce((select sum(coalesce(sum_pay_money, 0)) from ap_ar_trans_detail as x1 where x1.trans_flag = 19  and x1.last_status = 0 and exists (select billing_no from ap_ar_trans_detail as x2 where x2.doc_no = ap_ar_trans.doc_no and x2.trans_flag = ap_ar_trans.trans_flag and x1.billing_no = x2.billing_no and x1.bill_type = x2.bill_type) ), 0)";

                                __queryDocRefForNextProcess = "select billing_no from ap_ar_trans_detail as doc_ref where doc_ref.doc_no = \'" + __docNo + "\' and doc_ref.last_status=0";
                                break;
                            case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                                __tableName = _g.d.ap_ar_trans._table;
                                __fieldUsedStatus2 = "0 as " + __fieldUsedStatus2;
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from " + __tableName + " as doc_cancel where doc_cancel.trans_flag = 23 and doc_cancel.doc_ref = ap_ar_trans.doc_no union all select doc_no from ap_ar_trans as doc_cancel2 where doc_cancel2.trans_flag = 19 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ap_ar_trans.doc_no  ) as doc_cancel_temp ) ";

                                __queryDocRefForNextProcess = "select billing_no from ap_ar_trans_detail as doc_ref where doc_ref.doc_no = \'" + __docNo + "\' and doc_ref.last_status=0";
                                break;

                            case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก:
                                __tableName = _g.d.ap_ar_trans._table;
                                __fieldUsedStatus2 = "0 as " + __fieldUsedStatus2;
                                __queryDocRefForNextProcess = "select doc_ref from ap_ar_trans as doc_ref where doc_ref.doc_no = \'" + __docNo + "\' ";
                                break;
                            #endregion

                            #region ลูกหนี้

                            case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้ยกมา:
                                __queryDocCancelCount = "(select count(doc_no) from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 93 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no) ";
                                __queryDocRefCount2 = "(select count(billing_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_trans.doc_no and doc_ref.bill_type = ic_trans.trans_flag and doc_ref.last_status=0 )";
                                break;
                            case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้ยกมา:
                                __queryDocCancelCount = "(select count(doc_no) from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 95 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no) ";
                                __queryDocRefCount2 = "(select count(billing_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_trans.doc_no and doc_ref.bill_type = ic_trans.trans_flag and doc_ref.last_status=0 )";
                                break;
                            case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้ยกมา:
                                __queryDocCancelCount = "(select count(doc_no) from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 97 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no) ";
                                __queryDocRefCount2 = "(select count(billing_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_trans.doc_no and doc_ref.bill_type = ic_trans.trans_flag and doc_ref.last_status=0 )";
                                break;
                            case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 100 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 99 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                __queryDocRefCount = "(select count(ref_doc_no) from ic_trans_detail as doc_ref where doc_ref.ref_doc_no = ic_trans.doc_no and doc_ref.trans_flag in (101,103) and doc_ref.last_status=0 )";
                                __queryDocRefCount2 = "(select count(billing_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_trans.doc_no and doc_ref.bill_type = ic_trans.trans_flag and doc_ref.last_status=0 )";
                                break;
                            case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 102 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 101 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                __queryDocRefCount2 = "(select count(billing_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_trans.doc_no and doc_ref.bill_type = ic_trans.trans_flag and doc_ref.last_status=0 )";
                                break;
                            case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 104 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 103 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                __queryDocRefCount2 = "(select count(billing_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_trans.doc_no and doc_ref.bill_type = ic_trans.trans_flag and doc_ref.last_status=0 )";
                                break;

                            case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล:
                                __tableName = _g.d.ap_ar_trans._table;
                                __fieldUsedStatus2 = "0 as " + __fieldUsedStatus2;
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from " + __tableName + " as doc_cancel where doc_cancel.trans_flag = 236 and doc_cancel.doc_ref = " + __tableName + ".doc_no union all select doc_no from ap_ar_trans as doc_cancel2 where doc_cancel2.trans_flag = 235 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ap_ar_trans.doc_no  ) as doc_cancel_temp ) ";
                                __queryDocRefCount = "(select count(doc_ref) from ap_ar_trans_detail as doc_ref where doc_ref.doc_ref = ap_ar_trans.doc_no and doc_ref.trans_flag in (239) and doc_ref.last_status=0 )";

                                __queryDetailRowCount = "(select count(billing_no) from ap_ar_trans_detail where  ap_ar_trans_detail.doc_no = ap_ar_trans.doc_no and ap_ar_trans_detail.trans_flag = ap_ar_trans.trans_flag)";
                                __queryCountItemQty = "coalesce((select sum(coalesce(sum_pay_money, 0)) from ap_ar_trans_detail where ap_ar_trans_detail.doc_no = ap_ar_trans.doc_no and ap_ar_trans.trans_flag = 235 ), 0)";
                                __queryReduceItemQty = "coalesce((select sum(coalesce(sum_pay_money, 0)) from ap_ar_trans_detail as x1 where x1.trans_flag = 239  and x1.last_status = 0 and exists (select billing_no from ap_ar_trans_detail as x2 where x2.doc_no = ap_ar_trans.doc_no and x2.trans_flag = ap_ar_trans.trans_flag and x1.billing_no = x2.billing_no and x1.bill_type = x2.bill_type) ), 0)";

                                __queryDocRefForNextProcess = "select billing_no from ap_ar_trans_detail as doc_ref where doc_ref.doc_no = \'" + __docNo + "\' and doc_ref.last_status=0";
                                break;
                            case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                                __tableName = _g.d.ap_ar_trans._table;
                                __fieldUsedStatus2 = "0 as " + __fieldUsedStatus2;
                                __queryDocRefCount = "(select count(doc_no) from ic_trans_detail as r2 where r2.doc_ref = ap_ar_trans.doc_no and r2.trans_flag in ( 410, 411) and r2.last_status = 0 )"; // เช็คฝาก,ผ่าน แล้ว
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from " + __tableName + " as doc_cancel where doc_cancel.trans_flag = 240 and doc_cancel.doc_ref = ap_ar_trans.doc_no union all select doc_no from ap_ar_trans as doc_cancel2 where doc_cancel2.trans_flag = 239 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ap_ar_trans.doc_no  ) as doc_cancel_temp ) ";

                                __queryDocRefForNextProcess = "select billing_no from ap_ar_trans_detail as doc_ref where doc_ref.doc_no = \'" + __docNo + "\' and doc_ref.last_status=0";
                                break;

                            #endregion


                            #region เงินสด ธนาคาร
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 251 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 250 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                __queryDocRefCount = "(select count(ref_doc_no) from ic_trans_detail as doc_ref where doc_ref.ref_doc_no = ic_trans.doc_no and doc_ref.trans_flag in (252,254) and doc_ref.last_status=0 )";

                                __queryDocRefCount2 = "(select count(billing_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_trans.doc_no and doc_ref.bill_type = ic_trans.trans_flag and doc_ref.last_status=0 )";
                                break;

                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 255 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 254 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                __queryDocRefCount2 = "(select count(billing_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_trans.doc_no and doc_ref.bill_type = ic_trans.trans_flag and doc_ref.last_status=0 )";

                                __queryDocRefForNextProcess = "select billing_no from ap_ar_trans_detail as doc_ref where doc_ref.doc_no = \'" + __docNo + "\' and doc_ref.last_status=0";

                                break;

                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 253 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 252 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                __queryDocRefCount2 = "(select count(billing_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_trans.doc_no and doc_ref.bill_type = ic_trans.trans_flag and doc_ref.last_status=0 )";

                                __queryDocRefForNextProcess = "select billing_no from ap_ar_trans_detail as doc_ref where doc_ref.doc_no = \'" + __docNo + "\' and doc_ref.last_status=0";
                                break;

                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 261 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 260 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                __queryDocRefCount = "(select count(ref_doc_no) from ic_trans_detail as doc_ref where doc_ref.ref_doc_no = ic_trans.doc_no and doc_ref.trans_flag in (262,264) and doc_ref.last_status=0 )";

                                __queryDocRefCount2 = "(select count(billing_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_trans.doc_no and doc_ref.bill_type = ic_trans.trans_flag and doc_ref.last_status=0 )";

                                break;

                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 265 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 264 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                __queryDocRefCount2 = "(select count(billing_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_trans.doc_no and doc_ref.bill_type = ic_trans.trans_flag and doc_ref.last_status=0 )";

                                __queryDocRefForNextProcess = "select billing_no from ap_ar_trans_detail as doc_ref where doc_ref.doc_no = \'" + __docNo + "\' and doc_ref.last_status=0";
                                break;

                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 263 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 262 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                __queryDocRefCount2 = "(select count(billing_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_trans.doc_no and doc_ref.bill_type = ic_trans.trans_flag and doc_ref.last_status=0 )";

                                __queryDocRefForNextProcess = "select billing_no from ap_ar_trans_detail as doc_ref where doc_ref.doc_no = \'" + __docNo + "\' and doc_ref.last_status=0";
                                break;


                            #endregion

                            #region ฝาก/ถอน
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 403 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 401 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 404 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 402 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                break;
                            #endregion

                            #region เช็ครับ
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 408 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 405 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 430 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 410 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 431 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 411 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 432 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 412 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 433 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 413 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 434 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 414 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 436 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 416 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                break;
                            #endregion

                            #region เช็คจ่าย
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 407 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 406 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 471 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 451 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 472 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 452 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 473 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 453 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel where doc_cancel.trans_flag = 476 and doc_cancel.doc_ref = ic_trans.doc_no union all select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 456 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                break;

                            #endregion

                            #region บัตรเครดิต
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 461 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก:
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_trans as doc_cancel2 where doc_cancel2.trans_flag = 462 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_trans.doc_no ) as doc_cancel_temp ) ";
                                break;

                            #endregion

                            #region คลัง
                            case _g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก:
                                __tableName = _g.d.ic_wms_trans._table;
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_wms_trans as doc_cancel2 where doc_cancel2.trans_flag = 521 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_wms_trans.doc_no ) as doc_cancel_temp ) ";
                                __queryDocRefCount = "(select count(doc_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_wms_trans.doc_no and doc_ref.trans_flag = 522)";

                                __queryDetailRowCount = " (select count(*) from ic_wms_trans_detail where ic_wms_trans_detail.doc_no=ic_wms_trans.doc_no and ic_wms_trans_detail.trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก).ToString() + " and ic_wms_trans_detail.last_status=0)";

                                string __returnDepositQuery = "coalesce((select ref2.qty*(ref2.stand_value/ref2.divide_value) from ic_wms_trans_detail as ref2 where ref1.doc_no = ref2.ref_doc_no and ref2.trans_flag = 523 and ref2.last_status=0 ) ,0)";

                                string __depositQuery = "coalesce((select sum( qty*(ref1.stand_value/ref1.divide_value) -" + __returnDepositQuery + " ) from ic_wms_trans_detail as ref1 where trans_flag = 522 and ref1.ref_doc_no = ic_wms_trans.doc_no and ref1.last_status = 0 ), 0)";

                                /*

                                __query = "select " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._barcode + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._wh_code + ","
                                    + _g.d.ic_trans_detail._shelf_code + ",((qty*(stand_value/divide_value)) - " + __depositQuery + ")/(stand_value*divide_value) as " + _g.d.ic_trans_detail._qty
                                    + " from " + _g.d.ic_wms_trans_detail._table
                                    + " where " + _g.d.ic_trans_detail._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก).ToString()
                                    + " order by " + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._line_number;
                                
                                */
                                __queryCountItemQty = "(select coalesce(sum(qty * (stand_value/divide_value)),0) from ic_wms_trans_detail where ic_wms_trans_detail.doc_no=ic_wms_trans.doc_no and ic_wms_trans_detail.trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก).ToString() + " and ic_wms_trans_detail.last_status=0 and ic_wms_trans_detail.item_code <> '' )";
                                __queryReduceItemQty = "(" + __depositQuery + ")";

                                break;

                            case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:
                                __tableName = _g.d.ic_wms_trans._table;
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_wms_trans as doc_cancel2 where doc_cancel2.trans_flag = 522 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_wms_trans.doc_no ) as doc_cancel_temp ) ";
                                __queryDocRefCount = "(select count(doc_no) from ap_ar_trans_detail as doc_ref where doc_ref.billing_no = ic_wms_trans.doc_no and doc_ref.trans_flag = 523)";
                                break;

                            case _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:
                                __tableName = _g.d.ic_wms_trans._table;
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from ic_wms_trans as doc_cancel2 where doc_cancel2.trans_flag = 523 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  ic_wms_trans.doc_no ) as doc_cancel_temp ) ";
                                break;
                            case _g.g._transControlTypeEnum.Shipment:
                                __tableName = _g.d.pp_shipment._table;
                                __queryDocCancelCount = "(select count(doc_no) from ( select doc_no from pp_shipment as doc_cancel2 where doc_cancel2.trans_flag = 1901 and doc_cancel2.is_cancel = 1 and doc_cancel2.doc_no =  pp_shipment.doc_no ) as doc_cancel_temp ) ";
                                __fieldUsedStatus2 = " 0 as " + __fieldUsedStatus2;
                                __queryDocSuccess = "0";
                                __queryUsedStatus = " 0 ";
                                break;
                            #endregion
                            default:
                                {
                                    if (SystemInformation.ComputerName.ToLower().IndexOf("toe-pc") != -1)
                                    {
                                        string __insertDocFlowQuery = "insert into docflowcheck (flow_name) select \'" + _transType.ToString() + "\' from docflowcheck where (select count(flow_name) from docflowcheck where flow_name = \'" + _transType.ToString() + "\'  ) = 0 limit 1 ";

                                        MyLib._myFrameWork __ws = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
                                        __ws._queryInsertOrUpdate("toedebug", __insertDocFlowQuery);
                                        //MessageBox.Show("Doc Flow Not Found: " + _transType.ToString());
                                    }
                                }
                                break;
                        }

                        StringBuilder __query = new StringBuilder();

                        __query.Append("select roworder, doc_no, trans_flag," + __queryDocSuccess + " as " + __fieldDocSuccess);
                        __query.Append("," + __queryLastStatus + " as " + __fieldLastStatus);
                        __query.Append("," + __queryDocCancelCount + " as " + __fieldCancelDocNo); // ยกเลิก
                        __query.Append("," + __queryUsedStatus + " as " + __fieldUsedStatus);
                        __query.Append("," + __queryDocRefCount + " as " + __fieldDocRefCount); // used ไปหรือยัง
                        __query.Append("," + __queryDetailRowCount + " as " + __fieldDetailRowCount);
                        __query.Append("," + __queryCountItemQty + " as " + __fieldCountItemQty);
                        __query.Append("," + __queryReduceItemQty + " as " + __fidldReduceItemQty);

                        // used_status_2
                        __query.Append("," + __fieldUsedStatus2 + "," + __queryDocRefCount2 + " as " + __fieldDocRefCount2);

                        //__query.Append(__fieldDocSuccess + "," + __queryCountItemQty);

                        // approve
                        __query.Append("," + __fieldApproveStatus + "," + __queryApproveStatus + " as " + __fieldCheckApproveStatus);


                        __query.Append(" from " + __tableName + " where doc_no = \'" + __docNo + "\' and trans_flag = " + __transFlag);

                        StringBuilder __queryDocFlow = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

                        __queryDocFlow.Append(MyLib._myUtil._convertTextToXmlForQuery(__query.ToString()));
                        if (__queryDocRefForNextProcess.Length > 0)
                        {
                            __queryDocFlow.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryDocRefForNextProcess));
                        }

                        __queryDocFlow.Append("</node>");

                        //DataSet __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query.ToString());

                        ArrayList __getResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryDocFlow.ToString());

                        if (__getResult.Count > 0)
                        {
                            DataSet __result = (DataSet)__getResult[0]; // __myFrameWork._query(MyLib._myGlobal._databaseName, __query.ToString());
                            // query List Get Data
                            if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
                            {
                                // old value
                                int __roworder = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[0]["roworder"].ToString());
                                string __getDocNo = __result.Tables[0].Rows[0]["doc_no"].ToString();
                                int __getTransFlag = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[0]["trans_flag"].ToString());

                                int __detailRowCount = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[0][__fieldDetailRowCount].ToString());

                                int __old_doc_success = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[0][__fieldDocSuccess].ToString());
                                int __old_last_status = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[0][__fieldLastStatus].ToString());
                                int __old_used_status = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[0][__fieldUsedStatus].ToString());

                                // new value
                                int __last_status = (MyLib._myGlobal._intPhase(__result.Tables[0].Rows[0][__fieldCancelDocNo].ToString()) > 0) ? 1 : 0;
                                int __used_status = (MyLib._myGlobal._intPhase(__result.Tables[0].Rows[0][__fieldDocRefCount].ToString()) > 0) ? 1 : 0;

                                // int __doc_success = (__result.Tables[0].Rows[0][__fieldDocRefCount].ToString().Length > 0) ? 1 : 0;

                                // update by roworder 
                                Boolean __isUpdate = false;
                                Boolean __isLastStatusChange = false;

                                StringBuilder __updateStr = new StringBuilder();

                                // last_status
                                if (__last_status != __old_last_status)
                                {
                                    __isUpdate = true;
                                    __isLastStatusChange = true;

                                    if (__updateStr.Length > 0)
                                        __updateStr.Append(",");

                                    __updateStr.Append(__fieldLastStatus + "=" + __last_status);
                                }

                                // used_status
                                if (__used_status != __old_used_status)
                                {
                                    __isUpdate = true;
                                    if (__updateStr.Length > 0)
                                        __updateStr.Append(",");

                                    __updateStr.Append(__fieldUsedStatus + "=" + __used_status);
                                }

                                // doc_success
                                switch (_transType)
                                {
                                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                                    case _g.g._transControlTypeEnum.สินค้า_ขอโอน:
                                        {
                                            decimal __itemQty = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[0][__fieldCountItemQty].ToString());
                                            decimal __reduce = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[0][__fidldReduceItemQty].ToString());
                                            int __doc_success = (__itemQty == __reduce && __reduce != 0) ? 1 : 0;
                                            // doc_success check
                                            if (__doc_success != __old_doc_success)
                                            {
                                                __isUpdate = true;
                                                if (__updateStr.Length > 0)
                                                    __updateStr.Append(",");

                                                __updateStr.Append(__fieldDocSuccess + "=" + __doc_success);
                                            }
                                        }
                                        break;

                                    case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:

                                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ:
                                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า:

                                    // ขาย
                                    case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ:
                                    case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า:

                                    case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                                    case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                                    case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                                    // ลูกหนี้
                                    case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล:
                                    case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล:

                                    case _g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก:

                                        {
                                            decimal __itemQty = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[0][__fieldCountItemQty].ToString());
                                            decimal __reduce = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[0][__fidldReduceItemQty].ToString());
                                            int __doc_success = (__itemQty <= __reduce && __reduce != 0 && __detailRowCount > 0) ? 1 : 0;

                                            // doc_success check
                                            if (__doc_success != __old_doc_success)
                                            {
                                                __isUpdate = true;
                                                if (__updateStr.Length > 0)
                                                    __updateStr.Append(",");

                                                __updateStr.Append(__fieldDocSuccess + "=" + __doc_success);
                                            }

                                        }
                                        break;
                                    case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                                        {

                                            if (__used_status != __old_doc_success)
                                            {
                                                __isUpdate = true;
                                                if (__updateStr.Length > 0)
                                                    __updateStr.Append(",");

                                                __updateStr.Append(__fieldDocSuccess + "=" + __used_status);
                                            }
                                        }
                                        break;
                                }

                                // used_status_2
                                switch (_transType)
                                {
                                    case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                                    case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:

                                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ:
                                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า:

                                    // ขาย
                                    case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ:
                                    case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า:

                                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                                    case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                                    case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:

                                    case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้ยกมา:
                                    case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้ยกมา:
                                    case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้ยกมา:

                                    case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                                    case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                                    case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:

                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                                        {
                                            int __used_status_old_2 = (MyLib._myGlobal._intPhase(__result.Tables[0].Rows[0][__fieldUsedStatus2].ToString()) > 0) ? 1 : 0;
                                            int __used_status_2 = (MyLib._myGlobal._intPhase(__result.Tables[0].Rows[0][__fieldDocRefCount2].ToString()) > 0) ? 1 : 0;

                                            if (__used_status_old_2 != __used_status_2)
                                            {
                                                __isUpdate = true;
                                                if (__updateStr.Length > 0)
                                                    __updateStr.Append(",");

                                                __updateStr.Append(__fieldUsedStatus2 + "=" + __used_status_2);
                                            }
                                        }
                                        break;
                                }

                                // approve_status 
                                switch (_transType)
                                {
                                    case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                                    case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:

                                    case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                                    case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                                        {
                                            int __approveStatusOld = (MyLib._myGlobal._intPhase(__result.Tables[0].Rows[0][__fieldApproveStatus].ToString()) > 0) ? 1 : 0;
                                            int __approveStatus = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[0][__fieldCheckApproveStatus].ToString());

                                            if (_transType == _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ && MyLib._myGlobal._intPhase(__result.Tables[0].Rows[0][__fieldDocRefCount].ToString()) == 0)
                                            {
                                                __approveStatus = 0;
                                            }

                                            if (__approveStatusOld != __approveStatus)
                                            {
                                                __isUpdate = true;
                                                if (__updateStr.Length > 0)
                                                    __updateStr.Append(",");

                                                __updateStr.Append(__fieldApproveStatus + "=" + __approveStatus);
                                            }
                                        }
                                        break;
                                }

                                StringBuilder __queryUpdate = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

                                if (__isUpdate)
                                {
                                    // if (__updateStr.Length > 0)
                                    {
                                        __queryUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + __tableName + " set " + __updateStr.ToString() + " where roworder = " + __roworder));
                                    }
                                }

                                // toe เอา detail มา update เผื่อหลุด
                                // change last status ic_trans_detail
                                {
                                    switch (_transType)
                                    {
                                        case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                                        case _g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ:
                                        case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                                        case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                                        case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                                        case _g.g._transControlTypeEnum.สินค้า_โอนเข้า:
                                        case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก:
                                        case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด:

                                        case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                                        case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:


                                        case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                                        case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                                        case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:

                                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:


                                        case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                                        case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                                        case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                                        case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                                        case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                                        case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:

                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน:
                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก:

                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:
                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:
                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:

                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:
                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก:
                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่:
                                            {
                                                __queryUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans_detail set last_status = " + __last_status + " where doc_no = \'" + __getDocNo + "\' and trans_flag in (" + __getTransFlag + ((_transType == _g.g._transControlTypeEnum.สินค้า_โอนออก || _transType == _g.g._transControlTypeEnum.สินค้า_โอนเข้า) ? ",70,72" : "") + ") and last_status <> " + __last_status));

                                            }
                                            break;

                                        case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:
                                        case _g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก:
                                        case _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:
                                            {
                                                __queryUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_wms_trans_detail set last_status = " + __last_status + " where doc_no = \'" + __getDocNo + "\' and trans_flag =" + __getTransFlag + " and last_status <> " + __last_status));
                                            }
                                            break;
                                        case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                                        case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล:

                                        case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล:
                                        case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                                            {
                                                __queryUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("update ap_ar_trans_detail set last_status = " + __last_status + " where doc_no = \'" + __getDocNo + "\' and trans_flag =" + __getTransFlag + " and last_status <> " + __last_status));
                                            }
                                            break;
                                    }
                                }

                                // change last status cb_trans_detail
                                switch (_transType)
                                {
                                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:

                                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                                    case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                                    case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                                    case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                                    case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:

                                    case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ:
                                    case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า:
                                        {
                                            __queryUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("update cb_trans_detail set last_status = " + __last_status + " where doc_no = \'" + __getDocNo + "\' and trans_flag =" + __getTransFlag + " and last_status <> " + __last_status));
                                        }
                                        break;
                                }



                                // update อื่นๆ 
                                switch (this._transFlagEnum)
                                {
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก:
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                                        {
                                            string __getChqDocRef = "(select doc_ref from cb_chq_list where cb_chq_list.chq_number = ic_trans_detail.chq_number and chq_type = 1 and ap_ar_type = 1)";
                                            __queryUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans_detail set doc_ref = " + __getChqDocRef + " where doc_no = \'" + __getDocNo + "\' and trans_flag =" + __getTransFlag + " and doc_ref <> " + __getChqDocRef));

                                        }
                                        break;
                                }


                                __queryUpdate.Append("</node>");


                                string __queryResult = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __queryUpdate.ToString());
                                if (__queryResult.Length > 0)
                                {
                                    MessageBox.Show(__queryResult);
                                }
                                else
                                {

                                }
                            }
                            else
                            {
                                // if toe show last query
                                if (SystemInformation.ComputerName.ToLower().IndexOf("toe-pc") != -1)
                                {
                                    MessageBox.Show(__query.ToString());
                                }
                            }

                            // toe process doc_ref_next new thread ไปทำ
                            if (__getResult.Count > 1)
                            {
                                if (((DataSet)__getResult[1]).Tables.Count > 0)
                                {
                                    StringBuilder __getDocRefForNextProcess = new StringBuilder();
                                    DataTable __refTabletable = ((DataSet)__getResult[1]).Tables[0];
                                    for (int refRow = 0; refRow < __refTabletable.Rows.Count; refRow++)
                                    {
                                        if (__getDocRefForNextProcess.Length > 0)
                                            __getDocRefForNextProcess.Append(",");
                                        string __refDocNo = __refTabletable.Rows[refRow][0].ToString();

                                        __getDocRefForNextProcess.Append("\'" + __refDocNo + "\'");
                                    }

                                    _docFlowThread __proces = new _docFlowThread(this._transFlagEnum, "", __getDocRefForNextProcess.ToString());
                                    Thread __thread = new Thread(new ThreadStart(__proces._docFlowProcess));
                                    __thread.Start();

                                }
                            }
                        }
                        else
                        {
                            if (SystemInformation.ComputerName.ToLower().IndexOf("toe-pc") != -1)
                            {
                                MessageBox.Show(__queryDocFlow.ToString());
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// ประมวลผลใบเสนอซื้อ,การอนุมัติ
        /// ใบเสนอซื้อจะถูกอ้างอิงจาก การอนุมัติ และยกเลิกเท่านั้น
        /// ใบอนุมัติเสนอซื้อ จะถูกอ้างอิงจากการสั่งซื้อเท่านั้น
        /// </summary>
        public void _processAll()
        {
            // toe new doc flow
            switch (this._transFlagEnum)
            {
                #region สินค้า

                case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา:
                case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา_ยกเลิก:

                case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป_ยกเลิก:

                case _g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ:
                case _g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ_ยกเลิก:

                case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ_ยกเลิก:

                case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก_ยกเลิก:

                case _g.g._transControlTypeEnum.สินค้า_ขอโอน:
                case _g.g._transControlTypeEnum.สินค้า_โอนเข้า:
                case _g.g._transControlTypeEnum.สินค้า_โอนเข้า_ยกเลิก:
                case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                case _g.g._transControlTypeEnum.สินค้า_โอนออก_ยกเลิก:

                case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก:
                case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก_ยกเลิก:

                case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด:
                case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด_ยกเลิก:

                #endregion

                #region ซื้อ
                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก:
                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ:

                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ:
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:

                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก:
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก:
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก:

                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า:
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก:
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน:
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก:

                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ:
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก:
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน:
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก:

                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:

                #endregion

                #region ขาย

                case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก:
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ:

                case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ:
                case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก:
                case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน:
                case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก:

                case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า:
                case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก:
                case _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน:
                case _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน_ยกเลิก:

                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ:

                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ:

                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก:
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก:
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก:

                #endregion

                #region เจ้าหนี้

                case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้ยกมา:
                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้ยกมา:
                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้ยกมา:

                case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก:
                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก:
                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น_ยกเลิก:

                case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล:
                case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล_ยกเลิก:
                case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก:

                #endregion

                #region ลูกหนี้
                case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้ยกมา:
                case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้ยกมา:
                case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้ยกมา:

                case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น_ยกเลิก:
                case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น_ยกเลิก:
                case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น_ยกเลิก:

                case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล:
                case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล_ยกเลิก:
                case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก:

                #endregion

                #region เงินสดธนาคาร

                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก:

                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก:

                case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย:
                case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย:
                case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย_ยกเลิก:

                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                // เช็จจ่าย
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:

                case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก:


                #endregion

                case _g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก:
                case _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:
                case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:

                case _g.g._transControlTypeEnum.Shipment:
                    {
                        _docFlowProcess();

                        switch (this._transFlagEnum)
                        {
                            case _g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า:
                            case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                            case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก:
                            case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา:
                            case _g.g._transControlTypeEnum.สินค้า_เงื่อนไขแถมตอนซื้อ:
                            case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                            case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                            case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_สินค้าแถมตอนซื้อ:
                            case _g.g._transControlTypeEnum.สินค้า_โอนเข้า:
                            case _g.g._transControlTypeEnum.สินค้า_โอนเข้า_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                            case _g.g._transControlTypeEnum.สินค้า_โอนออก_ยกเลิก:
                            case _g.g._transControlTypeEnum.แสดงราคาตามสูตรสี:
                            case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                            case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                            case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ:
                            case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                            case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก:
                            case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ:
                            case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                            case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                            case _g.g._transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ:
                            case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                            case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                            case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ:
                            case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                            case _g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก:
                            case _g.g._transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ:
                                break;
                            default:
                                {


                                    SMLERPGlobal._smlFrameWork __process = new SMLERPGlobal._smlFrameWork();
                                    __process._processChqCreditCard(MyLib._myGlobal._databaseName);

                                }
                                break;
                        }


                        // other process
                        // serial process
                        StringBuilder __query = new StringBuilder();
                        __query.Append("<?xml version=\'1.0\' encoding=\'utf-8\' ?><node>");
                        __query.Append(this._serialNumberQueryList());

                        // cash back process
                        switch (this._transFlagEnum)
                        {
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย:
                            case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย:
                            case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย_ยกเลิก:

                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:

                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก:

                                __query.Append(this._cbFlowQuerList());
                                break;

                        }

                        switch (this._transFlagEnum)
                        {
                            // เช็ครับ
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                            // เช็จจ่าย
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:
                                __query.Append(this._ChequeQueryList());
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก:
                                __query.Append(this._creditCardQueryList());
                                break;

                        }

                        __query.Append("</node>");
                        //
                        string __queryString = __query.ToString();
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        string __queryResult = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __queryString);
                        if (__queryResult.Length > 0)
                        {
                            MessageBox.Show(__queryResult);
                        }

                    }
                    break;
                default:
                    {
                        #region Old Process
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        StringBuilder __query = new StringBuilder();
                        __query.Append("<?xml version=\'1.0\' encoding=\'utf-8\' ?><node>");
                        switch (this._transFlagEnum)
                        {
                            case _g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า:
                            case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                            case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก:
                            case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา:
                            case _g.g._transControlTypeEnum.สินค้า_เงื่อนไขแถมตอนซื้อ:
                            case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                            case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                            case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_สินค้าแถมตอนซื้อ:
                            case _g.g._transControlTypeEnum.สินค้า_โอนเข้า:
                            case _g.g._transControlTypeEnum.สินค้า_โอนเข้า_ยกเลิก:
                            case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                            case _g.g._transControlTypeEnum.สินค้า_โอนออก_ยกเลิก:
                                __query.Append(this._clearFlowStatusQuery(this._itemCodePack, this._docNoPack));
                                __query.Append(this._icFlowQueryList());
                                __query.Append(this._processStockFlowQueryList());
                                __query.Append(this._serialNumberQueryList());
                                break;
                            default:
                                //__query.Append(this._clearFlowStatusQuery(this._itemCodePack, this._docNoPack));
                                __query.Append(this._icFlowQueryList());
                                __query.Append(this._prFlowQueryList());
                                __query.Append(this._purchaseFlowQueryList());
                                __query.Append(this._processStockFlowQueryList());
                                __query.Append(this._processFlowSaleQueryList());
                                __query.Append(this._apArFlowQueryList());
                                __query.Append(this._depositFlowQueryList());
                                __query.Append(this._advanceFlowQueryList());
                                __query.Append(this._serialNumberQueryList());
                                break;
                        }

                        //toe add process cheque
                        switch (this._transFlagEnum)
                        {
                            // เช็ครับ
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                            // เช็จจ่าย
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:
                                __query.Append(this._ChequeQueryList());
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก:
                                __query.Append(this._creditCardQueryList());
                                break;

                        }

                        // cash back process
                        switch (this._transFlagEnum)
                        {
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย:
                            case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย:
                            case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย_ยกเลิก:

                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:

                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก:

                                __query.Append(this._cbFlowQuerList());
                                break;

                        }

                        // ap ar process
                        switch (this._transFlagEnum)
                        {
                            case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                            case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                            case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                            case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก:
                            case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก:
                            case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น_ยกเลิก:
                                __query.Append(this._processAPFlowQueryList());
                                break;

                            case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                            case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น_ยกเลิก:
                            case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                            case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น_ยกเลิก:
                            case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                            case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น_ยกเลิก:
                                __query.Append(this._processARFlowQueryList());
                                break;
                        }



                        //__query.Append(this._apQueryList());
                        // Finish
                        // LastStatus : update ตัวลูก
                        if (this._itemCodePack.Trim().Length > 0)
                        {
                            string __itemCode = _g.d.ic_trans_detail._item_code + " in (" + this._itemCodePack.Trim() + ") and ";
                            String __subQuery = "(select last_status from ic_trans where ic_trans.doc_no=ic_trans_detail.doc_no and ic_trans.trans_flag=ic_trans_detail.trans_flag)";
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans_detail set last_status=" + __subQuery + " where " + __itemCode + "last_status<>" + __subQuery));
                        }
                        __query.Append("</node>");
                        //
                        string __queryString = __query.ToString();
                        string __queryResult = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __queryString);
                        if (__queryResult.Length > 0)
                        {
                            MessageBox.Show(__queryResult);
                        }
                        else
                        {
                            switch (this._transFlagEnum)
                            {
                                case _g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า:
                                case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                                case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก:
                                case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา:
                                case _g.g._transControlTypeEnum.สินค้า_เงื่อนไขแถมตอนซื้อ:
                                case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ_ยกเลิก:
                                case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก_ยกเลิก:
                                case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา_ยกเลิก:
                                case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                                case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก_ยกเลิก:
                                case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                                case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป_ยกเลิก:
                                case _g.g._transControlTypeEnum.สินค้า_สินค้าแถมตอนซื้อ:
                                case _g.g._transControlTypeEnum.สินค้า_โอนเข้า:
                                case _g.g._transControlTypeEnum.สินค้า_โอนเข้า_ยกเลิก:
                                case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                                case _g.g._transControlTypeEnum.สินค้า_โอนออก_ยกเลิก:
                                case _g.g._transControlTypeEnum.แสดงราคาตามสูตรสี:
                                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ:
                                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก:
                                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ:
                                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                                case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                                case _g.g._transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ:
                                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ:
                                case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                                case _g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก:
                                case _g.g._transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ:
                                    break;
                                default:
                                    SMLERPGlobal._smlFrameWork __process = new SMLERPGlobal._smlFrameWork();
                                    __process._processChqCreditCard(MyLib._myGlobal._databaseName);
                                    break;
                            }
                        }

                        #endregion
                    }
                    break;
            }
        }
    }
}
