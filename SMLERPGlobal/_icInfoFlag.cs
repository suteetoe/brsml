using System;
using System.Collections.Generic;
using System.Text;

namespace _g
{
    public static class _icInfoFlag
    {
        public static string _outFlagAmountOnly = "(" + _g.d.ic_trans_detail._trans_flag + " in (" +
            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ) + "," +
            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด) + "," +
            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนออก) + "," +
            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + ")" +
            // toe
            " or (" + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก) + " and (" + _g.d.ic_trans_detail._qty + "<0 or " + _g.d.ic_trans_detail._sum_of_cost + "<0 ))" +
            " or (" + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้) + ") " +
            // จืด bug " or (" + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้) + " and " + _g.d.ic_trans_detail._inquiry_type + " in (0,1))" +
            " or (" + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด) + " )" +
            " or (" + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด) + " and " + _g.d.ic_trans_detail._inquiry_type + "=0))"

            + " and not (ic_trans_detail.doc_ref <> '' and ic_trans_detail.is_pos = 1)";
            // เพิ่มความเร็ว + " and not (((select coalesce(ic_trans.doc_ref, '') from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag and ic_trans.is_pos = 1 ) <> '' ) and ((select coalesce(ic_trans.is_pos, 0) from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag ) = 1))";

        public static string _inFlagAmountOnly = "(" + _g.d.ic_trans_detail._trans_flag + " in (" +
            // _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก) + "," + // toe fix 68 ปรับปรุงลด ย้ายไป where จำนวนด้วย
           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า) + "," +
           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา) + "," +
           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป) + "," +
           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก) + "," +
           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า) + "," +
           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ) + ")" +
           " or (" + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก) + " and (" + _g.d.ic_trans_detail._qty + ">0 or " + _g.d.ic_trans_detail._sum_of_cost + ">0 ))" +
           " or (" + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด) + ")" +
           " or (" + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้) + " and " + _g.d.ic_trans_detail._inquiry_type + " < 2))";

        public static string _inFlag = "(" + _g.d.ic_trans_detail._trans_flag + " in (" +
            // _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก) + "," + // toe fix 68 ปรับปรุงลด ย้ายไป where จำนวนด้วย
            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนเข้า) + "," +
            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา) + "," +
            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป) + "," +
            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก) + "," +
            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า) + "," +
            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ) + ")" +
            " or (" + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก) + " and " + _g.d.ic_trans_detail._qty + ">0)" +
            " or (" + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด) + " and " + _g.d.ic_trans_detail._inquiry_type + "=0)" +
            " or (" + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้) + " and " + _g.d.ic_trans_detail._inquiry_type + " < 2))";

        public static string _outFlag = "(" + _g.d.ic_trans_detail._trans_flag + " in (" +
            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ) + ",68," +
            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนออก) + "," +
            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + ")" +
            // toe ปรับปรุงลด
            " or (" + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก) + " and " + _g.d.ic_trans_detail._qty + "<0)" +
            " or (" + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้) + " and " + _g.d.ic_trans_detail._inquiry_type + " in (0,2)) " +
            // จืด bug " or (" + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้) + " and " + _g.d.ic_trans_detail._inquiry_type + " in (0,1))" +
            " or (" + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด) + " and " + _g.d.ic_trans_detail._inquiry_type + " in (0,2))" +
            " or (" + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด) + " and " + _g.d.ic_trans_detail._inquiry_type + "=0))"
         
            + " and not (ic_trans_detail.doc_ref <> '' and ic_trans_detail.is_pos = 1)";
            // + " and not (((select coalesce(ic_trans.doc_ref, '') from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag and ic_trans.is_pos = 1 ) <> '' ) and ((select coalesce(ic_trans.is_pos, 0) from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag ) = 1))";

        //              " and not (((select coalesce(ic_trans.doc_ref, '') from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag ) <>  '') and ((select coalesce(ic_trans.is_pos, 0) from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag ) = 1) )  )";
        public static string _saleFlag = "(" +
            " ( " + _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + ")" +

            " and not (ic_trans_detail.doc_ref <>  '' and ic_trans_detail.is_pos = 1) " +
            // " and not (((select coalesce(ic_trans.doc_ref, '') from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag ) <>  '') and ((select coalesce(ic_trans.is_pos, 0) from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag ) = 1) ) " +

            ")" +
            " or (" + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้) + " and (" + _g.d.ic_trans_detail._inquiry_type + " in (0,1))" +
            " or (" + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้) + " and " + _g.d.ic_trans_detail._inquiry_type + " > 1))" +
            ")";

        public static string _allFlagQty
        {
            get
            {
                return "(" + _inFlag + " or " + _outFlag + ")";
            }
        }

        public static string _allFlagAmount
        {
            get
            {
                return "(" + _inFlagAmountOnly + " or " + _outFlagAmountOnly + ")";
            }
        }

        public static string _icWhShelfUserPermissionWhereQuery(_g.g._transControlTypeEnum transType)
        {
            StringBuilder __where = new StringBuilder();

            string __screen_type = "";
            switch (transType)
            {
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                    __screen_type = " and " + _g.d.erp_user_group_wh_shelf._screen_code + "=\'" + "PU" + "\' ";
                    break;
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                    {
                        if (MyLib._myGlobal._programName.Equals("SML CM"))
                        {
                            __screen_type = " and " + _g.d.erp_user_group_wh_shelf._screen_code + "=\'" + "SI" + "\' ";
                        }
                    }
                    break;
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                    __screen_type = " and " + _g.d.erp_user_group_wh_shelf._screen_code + "=\'" + "SI" + "\' ";
                    break;
                case _g.g._transControlTypeEnum.สินค้า_ขอโอน:
                case _g.g._transControlTypeEnum.สินค้า_โอนเข้า:
                case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                case _g.g._transControlTypeEnum.สินค้า_โอนเข้า_ยกเลิก:
                case _g.g._transControlTypeEnum.สินค้า_โอนออก_ยกเลิก:
                    __screen_type = " and " + _g.d.erp_user_group_wh_shelf._screen_code + "=\'" + "IM" + "\' ";
                    break;
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก:
                    __screen_type = " and " + _g.d.erp_user_group_wh_shelf._screen_code + "=\'" + "ST" + "\' ";
                    break;
            }

            if (__screen_type.Length > 0)
            {
                // __queryWareHouseAndShelf = "select * from (" + __queryWareHouseAndShelf + ") as temp1 where " +
                /*__where.Append(
                _g.d.ic_wh_shelf._wh_code + " in (select " + _g.d.erp_user_group_wh_shelf._wh_code + " from " + _g.d.erp_user_group_wh_shelf._table + " where " + _g.d.erp_user_group_wh_shelf._group_code + " in (select " + _g.d.erp_user_group_detail._group_code + " from " + _g.d.erp_user_group_detail._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_user_group_detail._user_code) + " = '" + MyLib._myGlobal._userCode.ToUpper() + "') " + __screen_type + ") and " +
                _g.d.ic_wh_shelf._shelf_code + " in (select " + _g.d.erp_user_group_wh_shelf._shelf_code + " from " + _g.d.erp_user_group_wh_shelf._table + " where " + _g.d.erp_user_group_wh_shelf._group_code + " in (select " + _g.d.erp_user_group_detail._group_code + " from " + _g.d.erp_user_group_detail._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_user_group_detail._user_code) + " = '" + MyLib._myGlobal._userCode.ToUpper() + "') " + __screen_type + ") "

                );*/

                __where.Append(" exists ( " +
                    " select wh_code, shelf_code from erp_user_group_wh_shelf where group_code in (select group_code from erp_user_group_detail where   " + MyLib._myGlobal._addUpper(_g.d.erp_user_group_detail._user_code) + " = '" + MyLib._myGlobal._userCode.ToUpper() + "' )  " + __screen_type + " " +
                    " and ic_wh_shelf.wh_code = erp_user_group_wh_shelf.wh_code and ic_wh_shelf.shelf_code = erp_user_group_wh_shelf.shelf_code ) ");
            }

            return __where.ToString();
        }
    }
}
