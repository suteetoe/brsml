using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Drawing;
using System.IO;
using System.Data;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Collections;
using System.Globalization;

namespace _g
{
    public static class g
    {
        public static MyLib._myTabControl _tabControl;
        public static String _checkerXmlFileName = "checkConfig.xml";
        /// <summary>
        /// ประเภทการซื้อสินค้า
        /// </summary>
        public static string[] _purchaseType = { _g.d.ic_trans._credit_purchase, _g.d.ic_trans._cash_purchase, _g.d.ic_trans._cash_purchase_service, _g.d.ic_trans._credit_purchase_service };
        /// <summary>
        /// ประเภทการขายสินค้า
        /// </summary>
        public static string[] _saleType = { _g.d.ic_trans._credit_sale, _g.d.ic_trans._cash_sale, _g.d.ic_trans._cash_sale_service, _g.d.ic_trans._credit_sale_service };
        /// <summary>
        /// ประเภทรายได้อื่นๆ
        /// </summary>
        public static string[] _saleOtherType = { _g.d.ic_trans._credit_sale_other, _g.d.ic_trans._cash_sale_other, _g.d.ic_trans._cash_sale_service_other, _g.d.ic_trans._credit_sale_service_other };
        /// <summary>
        /// ประเภทภาษี
        /// </summary>
        public static string[] _vatType = { _g.d.ic_trans._exclude_vat, _g.d.ic_trans._include_vat, _g.d.ic_trans._zero_vat };
        /// <summary>
        /// ประเภทเงินมัดจำจ่าย
        /// </summary>
        public static string[] _depositPayType = { _g.d.ic_trans._deposit_payment_cash, _g.d.ic_trans._deposit_payment_credit };

        /// <summary>
        /// สำหรับค้นหากลุ่มภาษี
        /// </summary>
        public class _glGetTaxGroupType
        {
            public string _name;
            public decimal _tax_rate;
        }
        // public static form matching
        public static ArrayList _menuFormList = new ArrayList();
        //
        public class _menuFormListClass
        {
            public string _screenCode = "";
            public string _screenName = "";
        }

        public enum _productCostType
        {
            ปรกติ,
            รวมต้นทุนแฝง,
            เคลื่อนไหวสินค้า
        }

        public enum _androidDisplayEnum
        {
            Clear,
            ItemName,
            Unit,
            Qty,
            Price,
            Total,
            Amount,
            AmountWord
        };

        public enum _priceGridType
        {
            /// <summary>
            /// ราคาขายทั่วไป
            /// </summary>
            ราคาปรกติ,
            /// <summary>
            /// ตามกลุ่มลูกค้า
            /// </summary>
            ราคาตามกลุ่มลูกค้า,
            /// <summary>
            /// ราคาขายตามลูกค้า
            /// </summary>
            ราคาตามลูกค้า,
            /// <summary>
            /// ราคาขายตามเจ้าหนี้
            /// </summary>
            ราคาตามเจ้าหนี้,
            ลดทั่วไป,
            ลดตามกลุ่มลูกค้า,
            ลดตามลูกค้า


        }

        public enum _priceListType
        {
            ว่าง,
            ซื้อ_ราคาซื้อทั่วไป,
            ซื้อ_ราคาซื้อตามเจ้าหนี้,
            ขาย_ราคาขายทั่วไป,
            ขาย_ราคาขายตามกลุ่มลูกค้า,
            ขาย_ราคาขายตามลูกค้า,
            ขาย_ราคาขายมาตรฐาน
        }

        public enum _vatTypeEnum
        {
            ภาษีแยกนอก,
            ภาษีรวมใน,
            ยกเว้นภาษี,
            ว่าง
        }

        public enum _autoRunType
        {
            ใบกำกับภาษี,
            บัญชี_ข้อมูลรายวัน,
            ข้อมูลรายวัน,
            สินค้า,
            ลูกหนี้,
            เจ้าหนี้,
            เจ้าหนี้_ลูกหนี้รายวัน,
            เงินเดือน_พนักงาน,
            เงินเดือน_ขาด,
            เงินเดือน_ลา,
            เงินเดือน_มาสาย,
            เงินเดือน_ค่าล่วงเวลา,
            เงินเดือน_อัตราภาษีเงินได้,
            เงินเดือน_รายละเอียดการทำงาน,
            เงินเดือน_คำนวนเงินเดือน,
            เงินเดือน_จ่ายภาษีรายเดือน,
            เงินเดือน_คำนวนภาษี,
            POS_รับเงินทอน,
            POS_ส่งเงิน,
            ใบสั่งอาหาร,
            ว่าง
        }

        public enum _screenPayrollEnum
        {
            //หน้าจอ
            คำนวนเงินเดือน,
            คำนวนภาษีรายเดือน,
            คำนวนภาษีรายปี,
            //รายงาน
            ภงด_1,
            ภงด_1_ใบแนบ,
            สปส_1_10_ส่วนที่1,
            สปส_1_10_ส่วนที่2,
            สลิปเงินเดือน,
            ภงด_1_ก,
            ภงด_1_ก_ใบแนบ,
            ภงด_91,
            รายละเอียดพนักงาน_ลูกจ้าง_ตามประเภท,
            รายละเอียดพนักงาน_ลูกจ้าง_ตามเงินเดือน,
            รายละเอียดการขาดงาน,
            รายละเอียดการลางาน,
            รายละเอียดการมาสาย,
            รายละเอียดการทำงานล่วงเวลา,
            รายละเอียดเงินเดือน_ค่าจ้าง_ตามประเภทลูกจ้าง,
            รายละเอียดเงินได้_เงินหัก_ประจำเดือน,
            รายงานภาษีเงินได้_รายเดือน,
            รายงานภาษีเงินได้_รายปี,
            รายงานสรุป_การทำงาน_พนักงาน_ลูกจ้าง,
            รายงานสรุป_การจ่ายเงินเดือน_ค้าจ้าง_ทั้งหมด,
            รายงานเปรียบเทียบ_การจ่ายเงินเดือน_ค้าจ้าง,
            รายละเอียดเงินเดือน_ค่าจ้างทั้งปี

        }

        public enum _depositAdvanceEnum
        {
            รับเงินล่วงหน้า,
            รับเงินมัดจำ,
            จ่ายเงินล่วงหน้า,
            จ่ายเงินมัดจำ
        }

        public enum ProjectAllowcateEnum
        {
            แผนก,
            โครงการ,
            การจัดสรร,
            หน่วยงาน,
            งาน

        }

        public enum _chqTypeEnum
        {
            ว่าง,
            /// <summary>
            /// 1.in_receive : บันทึกเช็ครับ
            /// </summary>
            เช็ครับ,
            /// <summary>
            /// 2.out_payment : บันทึกเช็คจ่าย
            /// </summary>
            เช็คจ่าย,
            บัตรเครดิต
        }

        public static int _chqType(_chqTypeEnum chqListControlType)
        {
            switch (chqListControlType)
            {
                case _chqTypeEnum.เช็ครับ: return 1;
                case _chqTypeEnum.เช็คจ่าย: return 2;
                case _chqTypeEnum.บัตรเครดิต: return 3;
            }
            return 0;
        }

        public static MyLib._calcDiscountResultStruct _calcFormulaPrice(decimal qty, decimal price, string formula)
        {
            MyLib._calcDiscountResultStruct __result = new MyLib._calcDiscountResultStruct();
            __result._newPrice = price;
            if (formula.Trim().Length > 0)
            {
                // = = กำหนดราคาขาย
                // + = เพิ่ม
                // - = ลด
                if (formula[0] == '=' || formula[0] == '-' || formula[0] == '+')
                {
                    if (formula[0] == '=')
                    {
                        string[] __split = formula.Split(',');
                        if (__split.Length > 0)
                        {
                            // เปลี่ยนราคาใหม่
                            string __priceStr = __split[0].Replace("=", "");
                            price = MyLib._myGlobal._decimalPhase(__priceStr);
                            StringBuilder __newFormat = new StringBuilder();
                            for (int __loop = 1; __loop < __split.Length; __loop++)
                            {
                                if (__newFormat.Length > 0)
                                {
                                    __newFormat.Append(",");
                                }
                                __newFormat.Append(__split[__loop]);
                            }
                            formula = __newFormat.ToString();
                        }
                    }


                    MyLib._calcDiscountResultStruct __data = MyLib._myGlobal._calcDiscountOnly(price, formula, price, _g.g._companyProfile._item_price_decimal);
                    __result._newPrice = __data._newPrice;
                    __result._realPrice = (formula[0] == '+') ? __data._newPrice + __data._discountAmount : __data._newPrice - __data._discountAmount; // toe
                    __result._discountWord = formula.Replace("-", "");
                    __result._discountAmount = __data._discountAmount;
                    if (qty != 1.0M && MyLib._myGlobal._decimalPhase(__result._discountWord) != 0.0M)
                    {
                        __result._discountWord = (MyLib._myGlobal._decimalPhase(__result._discountWord) * qty).ToString();
                        __result._discountAmount = MyLib._myGlobal._decimalPhase(__result._discountWord);
                    }
                    if (_g.g._companyProfile._sale_real_price)
                    {
                        __result._discountWord = "";
                        __result._discountAmount = 0.0M;
                        __result._newPrice = __result._realPrice;
                    }
                }
                else
                {
                    __result._newPrice = MyLib._myGlobal._decimalPhase(formula);
                    __result._realPrice = __result._newPrice;
                }
            }
            else
            {
                // toe
                __result._realPrice = price;
            }
            return __result;
        }

        public static _transTypeEnum _transType(_transControlTypeEnum mode)
        {
            string __modeCompare = "**" + mode.ToString();
            if (__modeCompare.IndexOf("**ขาย_") != -1) return _transTypeEnum.ขาย_ลูกหนี้;
            else
                if (__modeCompare.IndexOf("**ซื้อ_") != -1) return _transTypeEnum.ซื้อ_เจ้าหนี้;
            else
                    if (__modeCompare.IndexOf("**สินค้า_") != -1) return _transTypeEnum.สินค้า;
            else
                        if (__modeCompare.IndexOf("**เจ้าหนี้_") != -1) return _transTypeEnum.เจ้าหนี้;
            else
                            if (__modeCompare.IndexOf("**ลูกหนี้_") != -1) return _transTypeEnum.ลูกหนี้;
            return _transTypeEnum.ว่าง;
        }

        public enum _transTypeEnum
        {
            ว่าง,
            สินค้า,
            ขาย_ลูกหนี้,
            ซื้อ_เจ้าหนี้,
            เจ้าหนี้,
            ลูกหนี้
        }

        public enum _transSystemEnum
        {
            ว่าง,
            สินค้า,
            ซื้อ,
            ขาย,
            เจ้าหนี้,
            ลูกหนี้,
            เงินสดธนาคาร,
            บัญชี


        }

        public class _departmentListStruct
        {
            public string _code;
            public string _name;
        }
        public class _branchListStruct
        {
            public string _code;
            public string _name;
            public List<_departmentListStruct> _department = new List<_departmentListStruct>();
        }

        public class _convertPackingWordClass
        {
            public string _unitCode = "";
            public string _unitName = "";
            public decimal _standValue = 0.0M;
            public decimal _divideValue = 0.0M;
        }

        public class _accountPeriodClass
        {
            /// <summary>
            /// งวดบัญชี
            /// </summary>
            public int _number;
            public DateTime _beginDate;
            public DateTime _endDate;
            /// <summary>
            /// ปีบัญชี
            /// </summary>
            public int _year;
            public int _status = 0;
        }

        public static string _convertPackingWord(List<_convertPackingWordClass> source, decimal qty, Boolean addHtmlCode)
        {
            StringBuilder __result = new StringBuilder();
            qty = Math.Round(qty, 2);
            // หาหน่วย 1:1 ก่อน
            int __findAddr = -1;
            for (int __loop = 0; __loop < source.Count; __loop++)
            {
                if (source[__loop]._standValue == 1.0M && source[__loop]._divideValue == 1.0M)
                {
                    __findAddr = __loop;
                    break;
                }
            }
            // Start
            if (qty < 0.0M)
            {
                __result.Append("-");
                qty = qty * -1;
            }
            for (int __loop = 0; __loop < source.Count; __loop++)
            {
                decimal __resultQty = 0.0M;
                if (__loop == __findAddr)
                    __resultQty = Math.Round(qty / (source[__loop]._standValue / source[__loop]._divideValue), 0);
                else
                    __resultQty = Math.Floor(qty / (source[__loop]._standValue / source[__loop]._divideValue));
                if (__resultQty > 0)
                {
                    if (__result.Length != 0)
                    {
                        if (addHtmlCode)
                        {
                            __result.Append("</font>x<font color='black'>");
                        }
                        else
                        {
                            __result.Append("x");
                        }
                    }
                    __result.Append(MyLib._myGlobal._formatNumberForReport(0, __resultQty));
                    __result.Append(source[__loop]._unitName.ToString());
                }
                decimal __qtySub = __resultQty * (source[__loop]._standValue / source[__loop]._divideValue);
                qty -= __qtySub;
            }
            return __result.ToString();
        }

        /// <summary>
        /// ประเภทหน่วยนับ
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string _unitTypeName(int type)
        {
            switch (type)
            {
                case 0: return MyLib._myResource._findResource(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._single_unit)._str;
                case 1: return MyLib._myResource._findResource(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._many_unit)._str;
                case 2: return MyLib._myResource._findResource(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._double_unit)._str;
                case 3: return MyLib._myResource._findResource(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._many_unit_and_double_unit)._str;
            }
            return "";
        }

        /// <summary>
        /// ประเภทต้นทุน
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string _costTypeName(int type)
        {
            switch (type)
            {
                case 0: return MyLib._myResource._findResource(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._average_cost)._str;
                case 1: return MyLib._myResource._findResource(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._fifo_cost)._str;
                case 2: return MyLib._myResource._findResource(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._assign_cost)._str;
                case 3: return MyLib._myResource._findResource(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._expire_cost)._str;
                case 4: return MyLib._myResource._findResource(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._standard_cost)._str;
                case 5: return MyLib._myResource._findResource(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._other_cost_1)._str;
            }
            return "";
        }

        /// <summary>
        /// ประเภทสินค้า
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string _itemTypeName(int type)
        {
            switch (type)
            {
                case 0: return MyLib._myResource._findResource(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._ic_normal)._str; //  "สินค้าทั่วไป";
                case 1: return MyLib._myResource._findResource(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._ic_service)._str; //  "ค่าบริการ (ไม่ทำสต๊อก)";
                case 2: return MyLib._myResource._findResource(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._ic_rent)._str; //  "สินค้าให้เช่า";
                case 3: return MyLib._myResource._findResource(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._ic_set)._str; //  "สินค้าชุด (ไม่ทำสต๊อก)";
                case 4: return MyLib._myResource._findResource(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._ic_consignment)._str; //  "สินค้าฝากขาย";
                case 5: return MyLib._myResource._findResource(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._ic_color)._str; //  "สูตรการผลิต";
                case 6: return MyLib._myResource._findResource(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._ic_color_mixed)._str; //  "สีผสม";
            }
            return "";
        }

        /// <summary>
        /// Update Field หลุด ให้เข้าที่เข้าทาง
        /// </summary>
        /// <returns></returns>
        public static string _queryUpdateTrans()
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __result = new StringBuilder();
            __result.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ap_ar_trans_detail._table + " where " + _g.d.ap_ar_trans_detail._sum_debt_amount + "=0 and coalesce(" + _g.d.ap_ar_trans_detail._remark + ", '') = ''  and (" + _g.d.ap_ar_trans_detail._billing_no + "=\'\' or " + _g.d.ap_ar_trans_detail._billing_no + " is null)"));
            //__result.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._last_status + "=0" + " where " + _g.d.ic_trans._last_status + " is null"));
            //__result.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._credit_day + "=0 where " + _g.d.ic_trans._credit_day + " is null"));
            switch (__myFrameWork._databaseSelectType)
            {
                case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                    __result.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._due_date + "=dateadd(day," + _g.d.ic_trans._credit_day + "," + _g.d.ic_trans._doc_date + ") where " + _g.d.ic_trans._due_date + "<>dateadd(day," + _g.d.ic_trans._credit_day + "," + _g.d.ic_trans._doc_date + ")"));
                    break;
                default:
                    __result.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._due_date + "=date(" + _g.d.ic_trans._doc_date + "+" + _g.d.ic_trans._credit_day + ") where " + _g.d.ic_trans._due_date + "<>date(" + _g.d.ic_trans._doc_date + "+" + _g.d.ic_trans._credit_day + ")"));
                    break;
            }
            return __result.ToString();
        }

        public static void _updateDateTimeForCalc(_g.g._transControlTypeEnum transFlag, string itemCodePack)
        {
            string __itemCode = (itemCodePack.Length == 0) ? "" : _g.d.ic_trans_detail._item_code + " in (" + itemCodePack + ") and ";

            StringBuilder __queryUpdate = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            string __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด).ToString();
            MyLib._myFrameWork __myFramework = new MyLib._myFrameWork();
            if (MyLib._myGlobal._subVersion == 1)
            {
                // ไม่ได้ใช้เพราะกำหนดเป็น default
                // update ค่าว่าง วันที่/เวลา (แก้โดยไปใส่ตอน insert ข้อมูลแล้ว ใน _icTransControl)
                /*__queryUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans_detail._table + " set " + _g.d.ic_trans_detail._doc_date_calc + "=" + _g.d.ic_trans_detail._doc_date + "," + _g.d.ic_trans_detail._doc_time_calc + "=" + _g.d.ic_trans_detail._doc_time + " where " + __itemCode + "(" + _g.d.ic_trans_detail._doc_date_calc + "<\'1900-1-1\' or " + _g.d.ic_trans_detail._doc_date_calc + " is null) and (" + _g.d.ic_trans_detail._doc_date_calc + "<>" + _g.d.ic_trans_detail._doc_date + " or " + _g.d.ic_trans_detail._doc_time_calc + "<>" + _g.d.ic_trans_detail._doc_time + ")"));
                __queryUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans_detail._table + " set " + _g.d.ic_trans_detail._doc_time_calc + "=" + _g.d.ic_trans_detail._doc_time + " where " + __itemCode + "(" + _g.d.ic_trans_detail._doc_time_calc + " is null or " + _g.d.ic_trans_detail._doc_time_calc + "=\'\' or " + _g.d.ic_trans_detail._doc_date_calc + "<\'1900-1-1\') and (" + _g.d.ic_trans_detail._doc_time_calc + "<>" + _g.d.ic_trans_detail._doc_time + ")"));
                // กรณีที่ไม่ใช่ซื้อสินค้าราคาผิด วันที่ต้องตรงกันเท่านั้น ดักไว้เลย
                __queryUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans_detail._table + " set " + _g.d.ic_trans_detail._doc_date_calc + "=" + _g.d.ic_trans_detail._doc_date + "," + _g.d.ic_trans_detail._doc_time_calc + "=" + _g.d.ic_trans_detail._doc_time + " where " + __itemCode + "((" + _g.d.ic_trans_detail._doc_date_calc + "<>" + _g.d.ic_trans_detail._doc_date + " or " + _g.d.ic_trans_detail._doc_time_calc + "<>" + _g.d.ic_trans_detail._doc_time + ") and " + _g.d.ic_trans_detail._trans_flag + " not in (" + __transFlag + "))"));
                __queryUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans_detail._table + " set " + _g.d.ic_trans_detail._doc_time_calc + "=" + _g.d.ic_trans_detail._doc_time + " where " + __itemCode + "(" + _g.d.ic_trans_detail._doc_time_calc + "<>" + _g.d.ic_trans_detail._doc_time_calc + " and " + _g.d.ic_trans_detail._trans_flag + " not in (" + __transFlag + "))"));*/
            }
            // update ภาษีหัก ณ. ที่จ่าย
            string __selectWhtDueDate = "(select " + _g.d.gl_wht_list._due_date + " from " + _g.d.gl_wht_list._table + " where " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._doc_no + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._doc_no + " and " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._tax_doc_no + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._tax_doc_no + ")";
            __queryUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_wht_list_detail._table + " set " + _g.d.gl_wht_list_detail._due_date + "=" + __selectWhtDueDate + " where " + _g.d.gl_wht_list_detail._due_date + " is null or " + _g.d.gl_wht_list_detail._due_date + "<>" + __selectWhtDueDate));
            //
            string __selectWhtCustCode = "(select " + _g.d.gl_wht_list._cust_code + " from " + _g.d.gl_wht_list._table + " where " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._doc_no + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._doc_no + " and " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._tax_doc_no + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._tax_doc_no + ")";
            __queryUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_wht_list_detail._table + " set " + _g.d.gl_wht_list_detail._cust_code + "=" + __selectWhtCustCode + " where " + _g.d.gl_wht_list_detail._cust_code + " is null or " + _g.d.gl_wht_list_detail._cust_code + "<>" + __selectWhtCustCode));
            //
            if (transFlag == _transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด || transFlag == _transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด)
            {
                // กรณีซื้อสินค้าราคาผิด ปรับวันที่และเวลา เพื่อให้สามารถคำนวณย้อนหลังได้
                string __rowOrderFieldName = "roworder";
                string __query =
                    "select " + __rowOrderFieldName + "," + _g.d.ic_trans_detail._doc_date_calc + "," + _g.d.ic_trans_detail._doc_time_calc + "," + _g.d.ic_trans_detail._doc_date + "," + _g.d.ic_trans_detail._doc_time + " from " +
                    "(select " + __rowOrderFieldName + "," + _g.d.ic_trans_detail._doc_date_calc + "," + _g.d.ic_trans_detail._doc_time_calc + "," +
                    "(select " + _g.d.ic_trans._doc_date + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._ref_doc_no + " and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ).ToString() + ") as " + _g.d.ic_trans_detail._doc_date + "," +
                    "(select " + _g.d.ic_trans._doc_time + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._ref_doc_no + " and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ).ToString() + ") as " + _g.d.ic_trans_detail._doc_time +
                    " from " + _g.d.ic_trans_detail._table +
                    " where " + __itemCode + _g.d.ic_trans_detail._trans_flag + " in (" + __transFlag + ")" +
                    " and " + _g.d.ic_trans_detail._inquiry_type + "=1) as temp1 where " + "(" + _g.d.ic_trans_detail._doc_date_calc + "<>" + _g.d.ic_trans_detail._doc_date + " or " + _g.d.ic_trans_detail._doc_time_calc + "<>" + _g.d.ic_trans_detail._doc_time + ")";
                DataTable __getData = __myFramework._queryShort(__query).Tables[0];
                if (__getData.Rows.Count > 0)
                {
                    for (int __row = 0; __row < __getData.Rows.Count; __row++)
                    {
                        string __getRoworder = __getData.Rows[__row][__rowOrderFieldName].ToString();
                        DateTime __docDate = MyLib._myGlobal._convertDateFromQuery(__getData.Rows[__row][_g.d.ic_trans_detail._doc_date].ToString());
                        string __docTime = __getData.Rows[__row][_g.d.ic_trans_detail._doc_time].ToString();
                        __queryUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans_detail._table + " set " + _g.d.ic_trans_detail._doc_date_calc + "=\'" + MyLib._myGlobal._convertDateToQuery(__docDate) + "\'," + _g.d.ic_trans_detail._doc_time_calc + "=\'" + __docTime + "\' where " + __rowOrderFieldName + "=" + __getRoworder));
                    }
                }
            }
            __queryUpdate.Append("</node>");
            string __result = __myFramework._queryList(MyLib._myGlobal._databaseName, __queryUpdate.ToString());
            if (__result.Length > 0)
            {
                Console.WriteLine("_updateDateTimeForCalc : " + __result.ToString());
            }
        }

        /// <summary>
        /// query where สินค้า
        /// </summary>
        /// <param name="itemBegin"></param>
        /// <param name="itemEnd"></param>
        /// <returns></returns>
        public static string _getItemCodeCreate(string fieldName, string itemBegin, string itemEnd)
        {
            string __itemWhere = "";
            if (itemBegin.Trim().Length != 0 && itemEnd.Trim().Length == 0)
            {
                __itemWhere = fieldName + "=\'" + itemBegin + "\'";
            }
            else
                if (itemBegin.Trim().Length == 0 && itemEnd.Trim().Length != 0)
            {
                __itemWhere = fieldName + "=\'" + itemEnd + "\'";
            }
            else
                    if (itemBegin.Trim().Length != 0 && itemEnd.Trim().Length != 0)
            {
                __itemWhere = fieldName + " between \'" + itemBegin + "\' and \'" + itemEnd + "\'";
            }
            return __itemWhere;
        }

        /// <summary>
        /// ประกอบ Quey รหัสสินค้า จาก Grid เอาไป where
        /// </summary>
        /// <param name="itemGrid"></param>
        /// <returns></returns>
        public static string _getItemCode(string fieldName, MyLib._myGrid itemGrid)
        {
            return _getItemCode(fieldName, itemGrid, null, null);
        }

        /// <summary>
        /// ประกอบ Quey รหัสสินค้า จาก Grid เอาไป where
        /// </summary>
        /// <param name="itemGrid"></param>
        /// <param name="itemBegin"></param>
        /// <param name="itemEnd"></param>
        /// <returns></returns>
        public static string _getItemCode(string fieldName, MyLib._myGrid itemGrid, string itemBegin, string itemEnd)
        {
            string __itemWhere = "";
            if (itemGrid == null)
            {
                __itemWhere = _getItemCodeCreate(fieldName, itemBegin, itemEnd);
            }
            else
            {
                StringBuilder __itemList = new StringBuilder();
                for (int __row = 0; __row < itemGrid._rowData.Count; __row++)
                {
                    string __itemBegin = itemGrid._cellGet(__row, 0).ToString();
                    string __itemEnd = itemGrid._cellGet(__row, 1).ToString();
                    string __getWhere = _getItemCodeCreate(fieldName, __itemBegin, __itemEnd);
                    if (__getWhere.Length > 0)
                    {
                        if (__itemList.Length > 0)
                        {
                            __itemList.Append(" or ");
                        }
                        __itemList.Append(" (" + __getWhere + ") ");
                    }
                }
                if (__itemList.Length > 0)
                {
                    __itemWhere = "(" + __itemList.ToString() + ")";
                }
            }
            return __itemWhere;
        }

        public static string _conditionGrid(MyLib._myGrid grid, string text)
        {
            StringBuilder __result = new StringBuilder(text);
            if (grid != null)
            {
                try
                {
                    for (int __row = 0; __row < grid._rowData.Count; __row++)
                    {
                        string __getFrom = grid._cellGet(__row, 0).ToString().Trim();
                        string __getTo = grid._cellGet(__row, 1).ToString().Trim();
                        if (__getFrom.Length > 0 && __getTo.Length > 0)
                        {
                            __result.Append("(" + __getFrom + "," + __getTo + ") ");
                        }
                        else
                        {
                            __result.Append(__getFrom + __getTo + " ");
                        }
                    }
                }
                catch
                {
                }
            }
            return __result.ToString();
        }

        public static string _getAutoRun(_autoRunType autoRunType, string docNo, string docDate, string format, _transControlTypeEnum icTransControlType, _g.g._transControlTypeEnum _controlApArTransType)
        {
            return _getAutoRun(autoRunType, docNo, docDate, format, icTransControlType, _controlApArTransType, "");
        }

        public static string _getAutoRun(_autoRunType autoRunType, string docNo, string docDate, string format, _transControlTypeEnum icTransControlType, _g.g._transControlTypeEnum _controlApArTransType, string tableName)
        {
            return _getAutoRun(autoRunType, docNo, docDate, format, icTransControlType, _controlApArTransType, tableName, "");
        }

        public static string _getAutoRun(_autoRunType autoRunType, string docNo, string docDate, string format, _transControlTypeEnum icTransControlType, _g.g._transControlTypeEnum _controlApArTransType, string tableName, string startRunningDoc)
        {
            return _getAutoRun(autoRunType, docNo, docDate, format, icTransControlType, _controlApArTransType, tableName, startRunningDoc, "", "");
        }

        public static string _getAutoRun(_autoRunType autoRunType, string docNo, string docDate, string format, _transControlTypeEnum icTransControlType, _g.g._transControlTypeEnum _controlApArTransType, string tableName, string startRunningDoc, string fieldDocNo, string queryWhere)
        {
            string __getFormat = format;
            string __result = __getFormat;
            if (__getFormat.Length > 0)
            {
                DateTime __date = MyLib._myGlobal._convertDate(docDate);
                CultureInfo __dateUSZone = new CultureInfo("en-US");
                CultureInfo __dateTHZone = new CultureInfo("th-TH");
                __getFormat = __getFormat.Replace("DD", __date.ToString("dd", __dateUSZone));
                __getFormat = __getFormat.Replace("MM", __date.ToString("MM", __dateUSZone));
                __getFormat = __getFormat.Replace("YYYY", __date.ToString("yyyy", __dateUSZone));
                __getFormat = __getFormat.Replace("YY", __date.ToString("yy", __dateUSZone));
                __getFormat = __getFormat.Replace("วว", __date.ToString("dd", __dateTHZone));
                __getFormat = __getFormat.Replace("ดด", __date.ToString("MM", __dateTHZone));
                __getFormat = __getFormat.Replace("ปปปป", __date.ToString("yyyy", __dateTHZone));
                __getFormat = __getFormat.Replace("ปป", __date.ToString("yy", __dateTHZone));
                __getFormat = __getFormat.Replace("@", docNo);
                int __numberBegin = __getFormat.IndexOf("#");
                if (__numberBegin != -1)
                {
                    StringBuilder __getFormatNumber = new StringBuilder();
                    int __numberEnd = __numberBegin;
                    while (__numberEnd < __getFormat.Length && __getFormat[__numberEnd] == '#')
                    {
                        __getFormatNumber.Append("#");
                        __numberEnd++;
                    }
                    __getFormat = __getFormat.Remove(__numberBegin, __numberEnd - __numberBegin);
                    string __getDocQuery = __getFormat + "z";
                    string __qw = "";
                    switch (autoRunType)
                    {
                        case _autoRunType.ใบกำกับภาษี:
                            __qw = " " + _g.d.gl_journal_vat_sale._vat_number + " <= '" + __getDocQuery + "' and " + _g.d.gl_journal_vat_sale._trans_flag + " = " + _g.g._transFlagGlobal._transFlag(icTransControlType).ToString();
                            __result = MyLib._myGlobal._getAutoRun(_g.d.gl_journal_vat_sale._table, _g.d.gl_journal_vat_sale._vat_number, __qw, __getFormat, true, __getFormatNumber.ToString()).ToString();
                            break;
                        // toe autorun รายวัน
                        case _autoRunType.บัญชี_ข้อมูลรายวัน:
                            __qw = " " + _g.d.gl_journal._doc_no + " <= '" + __getDocQuery + "'";
                            __result = MyLib._myGlobal._getAutoRun(_g.d.gl_journal._table, _g.d.gl_journal._doc_no, __qw, __getFormat, true, __getFormatNumber.ToString()).ToString();
                            break;
                        case _autoRunType.ใบสั่งอาหาร:
                            __qw = " " + _g.d.table_order_doc._doc_no + " <= '" + __getDocQuery + "'";
                            __result = MyLib._myGlobal._getAutoRun(_g.d.table_order_doc._table, _g.d.table_order_doc._doc_no, __qw, __getFormat, true, __getFormatNumber.ToString(), startRunningDoc).ToString();
                            break;
                        case _autoRunType.POS_รับเงินทอน:
                            __qw = " " + _g.d.POSCashierSettle._trans_type + " =1  and " + _g.d.POSCashierSettle._DocNo + " <= '" + __getDocQuery + "'";
                            __result = MyLib._myGlobal._getAutoRun(_g.d.POSCashierSettle._table, _g.d.POSCashierSettle._DocNo, __qw, __getFormat, true, __getFormatNumber.ToString(), startRunningDoc).ToString();
                            break;
                        case _autoRunType.POS_ส่งเงิน:
                            __qw = " " + _g.d.POSCashierSettle._trans_type + " =2  and " + _g.d.POSCashierSettle._DocNo + " <= '" + __getDocQuery + "'";
                            __result = MyLib._myGlobal._getAutoRun(_g.d.POSCashierSettle._table, _g.d.POSCashierSettle._DocNo, __qw, __getFormat, true, __getFormatNumber.ToString(), startRunningDoc).ToString();
                            break;
                        case _autoRunType.ข้อมูลรายวัน:
                            if (tableName.Trim().Length == 0)
                            {
                                tableName = _g.d.ic_trans._table;
                            }
                            if (_controlApArTransType != _transControlTypeEnum.ว่าง)
                            {
                                __qw = " " + _g.d.ic_trans._trans_flag + " = " + _g.g._transFlagGlobal._transFlag(_controlApArTransType).ToString() + " and " + _g.d.ic_trans._doc_no + " <= '" + __getDocQuery + "'";
                                __result = MyLib._myGlobal._getAutoRun(tableName, _g.d.ic_trans._doc_no, __qw, __getFormat, true, __getFormatNumber.ToString(), startRunningDoc).ToString();
                            }
                            else
                            {
                                __qw = " " + _g.d.ic_trans._trans_flag + " = " + _g.g._transFlagGlobal._transFlag(icTransControlType).ToString() + " and " + _g.d.ic_trans._trans_type + " =  " + _transTypeGlobal._transType(icTransControlType).ToString() + " and " + _g.d.ic_trans._doc_no + " <= '" + __getDocQuery + "'";
                                __result = MyLib._myGlobal._getAutoRun(tableName, _g.d.ic_trans._doc_no, __qw, __getFormat, true, __getFormatNumber.ToString(), startRunningDoc).ToString();
                            }
                            break;
                        case _autoRunType.เจ้าหนี้_ลูกหนี้รายวัน:
                            __qw = " " + _g.d.ic_trans._trans_flag + " = " + _g.g._transFlagGlobal._transFlag(_controlApArTransType).ToString() + " and " + _g.d.ic_trans._doc_no + " <= '" + __getDocQuery + "'";
                            __result = MyLib._myGlobal._getAutoRun(_g.d.ap_ar_trans._table, _g.d.ap_ar_trans._doc_no, __qw, __getFormat, true, __getFormatNumber.ToString(), startRunningDoc).ToString();
                            break;
                        case _autoRunType.สินค้า:
                            __qw = " " + _g.d.ic_inventory._code + "<=\'" + __getDocQuery + "\'";
                            __result = MyLib._myGlobal._getAutoRun(_g.d.ic_inventory._table, _g.d.ic_inventory._code, __qw, __getFormat, true, __getFormatNumber.ToString()).ToString();
                            break;
                        case _autoRunType.ลูกหนี้:
                            __qw = " " + _g.d.ar_customer._code + "<=\'" + __getDocQuery + "\'";
                            __result = MyLib._myGlobal._getAutoRun(_g.d.ar_customer._table, _g.d.ar_customer._code, __qw, __getFormat, true, __getFormatNumber.ToString()).ToString();
                            break;
                        case _autoRunType.เจ้าหนี้:
                            __qw = " " + _g.d.ap_supplier._code + "<=\'" + __getDocQuery + "\'";
                            __result = MyLib._myGlobal._getAutoRun(_g.d.ap_supplier._table, _g.d.ap_supplier._code, __qw, __getFormat, true, __getFormatNumber.ToString()).ToString();
                            break;
                        case _autoRunType.เงินเดือน_พนักงาน:
                            __qw = " " + _g._dataPayroll.payroll_employee._code + "<=\'" + __getDocQuery + "\'";
                            __result = MyLib._myGlobal._getAutoRun(_g._dataPayroll.payroll_employee._table, _g._dataPayroll.payroll_employee._code, __qw, __getFormat, true, __getFormatNumber.ToString()).ToString();
                            break;
                        case _autoRunType.เงินเดือน_ขาด:
                            __qw = " " + _g._dataPayroll.payroll_short_of_work._code + "<=\'" + __getDocQuery + "\'";
                            __result = MyLib._myGlobal._getAutoRun(_g._dataPayroll.payroll_short_of_work._table, _g._dataPayroll.payroll_short_of_work._code, __qw, __getFormat, true, __getFormatNumber.ToString()).ToString();
                            break;
                        case _autoRunType.เงินเดือน_ลา:
                            __qw = " " + _g._dataPayroll.payroll_leave._code + "<=\'" + __getDocQuery + "\'";
                            __result = MyLib._myGlobal._getAutoRun(_g._dataPayroll.payroll_leave._table, _g._dataPayroll.payroll_leave._code, __qw, __getFormat, true, __getFormatNumber.ToString()).ToString();
                            break;
                        case _autoRunType.เงินเดือน_มาสาย:
                            __qw = " " + _g._dataPayroll.payroll_arrive_late._code + "<=\'" + __getDocQuery + "\'";
                            __result = MyLib._myGlobal._getAutoRun(_g._dataPayroll.payroll_arrive_late._table, _g._dataPayroll.payroll_arrive_late._code, __qw, __getFormat, true, __getFormatNumber.ToString()).ToString();
                            break;
                        case _autoRunType.เงินเดือน_ค่าล่วงเวลา:
                            __qw = " " + _g._dataPayroll.payroll_over_time._code + "<=\'" + __getDocQuery + "\'";
                            __result = MyLib._myGlobal._getAutoRun(_g._dataPayroll.payroll_over_time._table, _g._dataPayroll.payroll_over_time._code, __qw, __getFormat, true, __getFormatNumber.ToString()).ToString();
                            break;
                        case _autoRunType.เงินเดือน_อัตราภาษีเงินได้:
                            __qw = " " + _g._dataPayroll.payroll_income_tax._code + "<=\'" + __getDocQuery + "\'";
                            __result = MyLib._myGlobal._getAutoRun(_g._dataPayroll.payroll_income_tax._table, _g._dataPayroll.payroll_income_tax._code, __qw, __getFormat, true, __getFormatNumber.ToString()).ToString();
                            break;
                        case _autoRunType.เงินเดือน_รายละเอียดการทำงาน:
                            __qw = " " + _g._dataPayroll.payroll_trans._doc_no + "<=\'" + __getDocQuery + "\' and " + _g._dataPayroll.payroll_trans._trans_flag + "=1";
                            __result = MyLib._myGlobal._getAutoRun(_g._dataPayroll.payroll_trans._table, _g._dataPayroll.payroll_trans._doc_no, __qw, __getFormat, true, __getFormatNumber.ToString()).ToString();
                            break;
                        case _autoRunType.เงินเดือน_คำนวนเงินเดือน:
                            //__qw = " " + _g._dataPayroll.payroll_calculate_wages._doc_no + "<=\'" + __getDocQuery + "\'";
                            //__result = MyLib._myGlobal._getAutoRun(_g._dataPayroll.payroll_calculate_wages._table, _g._dataPayroll.payroll_calculate_wages._doc_no, __qw, __getFormat, true, __getFormatNumber.ToString()).ToString();
                            break;
                        case _autoRunType.เงินเดือน_จ่ายภาษีรายเดือน:
                            __qw = " " + _g._dataPayroll.payroll_trans._doc_no + "<=\'" + __getDocQuery + "\' and " + _g._dataPayroll.payroll_trans._trans_flag + "=2";
                            __result = MyLib._myGlobal._getAutoRun(_g._dataPayroll.payroll_trans._table, _g._dataPayroll.payroll_trans._doc_no, __qw, __getFormat, true, __getFormatNumber.ToString()).ToString();
                            break;
                        case _autoRunType.เงินเดือน_คำนวนภาษี:
                            //__qw = " " + _g._dataPayroll.payroll_calculate_tax._doc_no + "<=\'" + __getDocQuery + "\'";
                            //__result = MyLib._myGlobal._getAutoRun(_g._dataPayroll.payroll_calculate_tax._table, _g._dataPayroll.payroll_calculate_tax._doc_no, __qw, __getFormat, true, __getFormatNumber.ToString()).ToString();
                            break;
                        case _autoRunType.ว่าง:
                            if (tableName.Trim().Length > 0)
                            {
                                // toe custom run
                                __qw = " " + fieldDocNo + " <= '" + __getDocQuery + "'" + ((queryWhere.Length > 0) ? " and " + queryWhere : "");
                                __result = MyLib._myGlobal._getAutoRun(tableName, fieldDocNo, __qw, __getFormat, true, __getFormatNumber.ToString(), startRunningDoc).ToString();
                            }
                            break;
                    }
                }
            }
            return __result;
        }

        public static string[] _monthName()
        {
            return new string[] { "month_january", "month_february", "month_march", "month_april", "month_may", "month_june", "month_july", "month_august", "month_september", "month_october", "month_november", "month_december" };
        }

        public static class _arapTransTypeGlobal
        {
            public static int _transType(_g.g._transControlTypeEnum TransControlType)
            {
                if (TransControlType.Equals(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้ยกมา) ||
                      TransControlType.Equals(_g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้ยกมา) ||
                      TransControlType.Equals(_g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้ยกมา))
                {
                    return 4;
                }
                else if (TransControlType.Equals(_g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล) ||
                      TransControlType.Equals(_g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล_ยกเลิก) ||
                      TransControlType.Equals(_g.g._transControlTypeEnum.เจ้าหนี้_เตรียมจ่าย) ||
                      TransControlType.Equals(_g.g._transControlTypeEnum.เจ้าหนี้_เตรียมจ่าย_ยกเลิก) ||
                      TransControlType.Equals(_g.g._transControlTypeEnum.เจ้าหนี้_เตรียมจ่าย_อนุมัติ) ||
                      TransControlType.Equals(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้) ||
                      TransControlType.Equals(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก) ||
                      TransControlType.Equals(_g.g._transControlTypeEnum.เจ้าหนี้_ตัดหนี้สูญ) ||
                      TransControlType.Equals(_g.g._transControlTypeEnum.เจ้าหนี้_ตัดหนี้สูญ_ยกเลิก))
                {
                    return 1;
                }
                if (TransControlType.Equals(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้ยกมา) ||
                      TransControlType.Equals(_g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้ยกมา) ||
                      TransControlType.Equals(_g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้ยกมา))
                {
                    return 5;
                }
                return 2;
            }
        }

        public static class _arapLoadViewGlobal
        {
            public static string _loadViewName(_g.g._transControlTypeEnum TransControlType, int typeNumber)
            {
                //(เจ้าหนี้)   
                switch (TransControlType)
                {
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้ยกมา:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้ยกมา:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้ยกมา:
                        switch (typeNumber)
                        {
                            case 1: return _g.g._screen_ap_ar_trans_ref;
                        }
                        return _g.g._screen_ap_ic_trans;
                    case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้ยกมา:
                    case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้ยกมา:
                    case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้ยกมา:
                        switch (typeNumber)
                        {
                            case 1: return _g.g._screen_ap_ar_trans_ref;
                        }
                        return _g.g._screen_ar_ic_trans;
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล:
                        return _g.g._screen_ap_before_pay;
                    case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล:
                        return _g.g._screen_ar_before_pay;
                    case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                        switch (typeNumber)
                        {
                            case 1: return _g.g._screen_ap_ar_trans_ref;
                        }
                        return _g.g._screen_ar_trans;
                    case _g.g._transControlTypeEnum.เจ้าหนี้_เตรียมจ่าย:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_เตรียมจ่าย_ยกเลิก:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_เตรียมจ่าย_อนุมัติ:
                    //case _g.g._ictransControlTypeEnum.APCancelApprovePayBill:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ตัดหนี้สูญ:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ตัดหนี้สูญ_ยกเลิก:
                        switch (typeNumber)
                        {
                            case 1: return _g.g._screen_ap_ar_trans_ref;
                        }
                        return _g.g._screen_ap_trans;
                    case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล_ยกเลิก:
                        return _g.g._screen_ap_trans_cancel;
                    case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล_ยกเลิก:
                    case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก:
                        return _g.g._screen_ar_trans_cancel;
                    case _transControlTypeEnum.IMEX_Bill_Collector: return "screen_bill_collector";
                }
                return _g.g._screen_ap_trans;
            }
        }

        public static class _transFlagGlobal
        {
            public static string _transName(int trans_type)
            {
                String __result = "*";
                switch (trans_type)
                {
                    case 0: __result = ""; break;
                    case 1: __result = "PoInvestigate"; break;
                    case 2: __result = "ใบเสนอซื้อ"; break;
                    case 3: __result = "ยกเลิกใบเสนอซื้อ"; break;
                    case 4: __result = "อนุมัติใบเสนอซื้อ"; break;
                    case 6: __result = "ใบสั่งซื้อ"; break;
                    case 7: __result = "ยกเลิกใบสั่งซื้อ"; break;
                    case 8: __result = "อนุมัติใบสั่งซื้อ"; break;
                    case 10: __result = "จ่ายเงินล่วงหน้า(เจ้าหนี้)"; break;
                    case 11: __result = "จ่ายเงินมัดจำ(เจ้าหนี้)"; break;
                    case 12: __result = "ซื้อ"; break;
                    case 13: __result = "ยกเลิกซื้อ/บริการ"; break;
                    case 14: __result = "เพิ่มหนี้(ซื้อ)"; break;
                    case 15: __result = "ยกเลิกเพิ่มหนี้(ซื้อ)"; break;
                    case 16: __result = "ส่งคืน/ลดหนี้"; break;
                    case 17: __result = "ยกเลิกส่งคืน/ลดหนี้(ซื้อ)"; break;
                    case 20: __result = "รับคืนเงินล่วงหน้า(เจ้าหนี้)"; break;
                    case 25: __result = "รับคืนเงินมัดจำ(เจ้าหนี้)"; break;
                    case 30: __result = "ใบเสนอราคา"; break;
                    case 31: __result = "ยกเลิกใบเสนอราคา"; break;
                    case 34: __result = "สั่งจอง/สั่งซื้อ"; break;
                    case 35: __result = "ยกเลิกสั่งจอง/สั่งซื้อ"; break;
                    case 36: __result = "ใบสั่งขาย"; break;
                    case 37: __result = "ยกเลิกใบสั่งขาย"; break;
                    case 40: __result = "รับเงินล่วงหน้า(ลูกหนี้)"; break;
                    case 41: __result = "ยกเลิกรับเงินล่วงหน้า"; break;
                    case 42: __result = "คืนเงินล่วงหน้า(ลูกหนี้)"; break;
                    case 43: __result = "ยกเลิกคืนเงินล่วงหน้า"; break;
                    case 44: __result = "ขาย"; break;
                    case 45: __result = "ยกเลิกขาย"; break;
                    case 46: __result = "เพิ่มหนี้(ขาย)"; break;
                    case 47: __result = "ยกเลิกเพิ่มหนี้(ขาย)"; break;
                    case 48: __result = "รับคืน"; break;
                    case 49: __result = "ยกเลิกรับคืน"; break;
                    case 50: __result = "SoDeliveryOrder"; break;
                    case 51: __result = "SoDeliveryOrderCancel"; break;
                    case 54: __result = "ยกมา"; break;
                    case 56: __result = "เบิก"; break;
                    case 58: __result = "รับคืนจากเบิก"; break;
                    case 60: __result = "รับสำเร็จรูป"; break;
                    case 66: __result = "ปรับปรุง"; break;
                    case 68: __result = "ปรับปรุง(ขาด)"; break; // color store only
                    case 70: __result = "โอนเข้า"; break;
                    case 72: __result = "โอนออก"; break;
                    case 81: __result = "ตั้งหนี้ยกมา(เจ้าหนี้)"; break;
                    case 82: __result = "ยกเลิกตั้งหนี้ยกมา(เจ้าหนี้)"; break;
                    case 83: __result = "เพิ่มหนี้ยกมา(เจ้าหนี้)"; break;
                    case 84: __result = "ยกเลิกเพิ่มหนี้ยกมา(เจ้าหนี้)"; break;
                    case 85: __result = "ลดหนี้ยกมา(เจ้าหนี้)"; break;
                    case 86: __result = "ยกเลิกลดหนี้ยกมา(เจ้าหนี้)"; break;
                    //
                    case 87: __result = "ตั้งหนี้อื่นๆ(เจ้าหนี้)"; break;
                    case 88: __result = "ยกเลิกตั้งหนี้อื่นๆ(เจ้าหนี้)"; break;
                    case 89: __result = "เพิ่มหนี้อื่นๆ(เจ้าหนี้)"; break;
                    case 90: __result = "ยกเลิกเพิ่มหนี้อื่นๆ(เจ้าหนี้)"; break;
                    case 91: __result = "ลดหนี้อื่นๆ(เจ้าหนี้)"; break;
                    case 92: __result = "ยกเลิกลดหนี้อื่นๆ(เจ้าหนี้)"; break;
                    //
                    case 213: __result = "ใบรับวางบิล"; break;
                    case 214: __result = "ยกเลิกใบรับวางบิล"; break;
                    case 215: __result = "เตรียมจ่าย"; break;
                    case 216: __result = "ยกเลิกเตรียมจ่าย"; break;
                    case 217: __result = "อนุมัติเตรียมจ่าย"; break;
                    case 18: __result = "ยกเลิกอนุมัติเตรียมจ่าย"; break;
                    case 19: __result = "จ่ายชำระหนี้"; break;
                    case 21: __result = "ตัดหนี้สูญ(เจ้าหนี้)"; break;
                    case 22: __result = "ยกเลิกตัดหนี้สูญ(เจ้าหนี้)"; break;
                    case 23: __result = "ยกเลิกจ่ายชำระหนี้"; break;
                    // Ar
                    case 93: __result = "ตั้งหนี้ยกมา(ลูกหนี้)"; break;
                    case 94: __result = "ยกเลิกตั้งหนี้ยกมา(ลูกหนี้)"; break;
                    case 95: __result = "เพิ่มหนี้ยกมา(ลูกหนี้)"; break;
                    case 96: __result = "ยกเลิกเพิ่มหนี้ยกมา(ลูกหนี้)"; break;
                    case 97: __result = "ลดหนี้ยกมา(ลูกหนี้)"; break;
                    case 98: __result = "ยกเลิกลดหนี้ยกมา(ลูกหนี้)"; break;
                    case 801: __result = "แต้มสะสมยกมา"; break;
                    //
                    case 99: __result = "ตั้งหนี้อื่นๆ(ลูกหนี้)"; break;
                    case 100: __result = "ยกเลิกตั้งหนี้อื่นๆ(ลูกหนี้)"; break;
                    case 101: __result = "เพิ่มหนี้อื่นๆ(ลูกหนี้)"; break;
                    case 102: __result = "ยกเลิกเพิ่มหนี้อื่นๆ(ลูกหนี้)"; break;
                    case 103: __result = "ลดหนี้อื่นๆ(ลูกหนี้)"; break;
                    case 104: __result = "ยกเลิกลดหนี้อื่นๆ(ลูกหนี้)"; break;
                    case 110: __result = "รับเงินมัดจำ(ขาย)"; break;
                    case 112: __result = "รับคืนเงืนมัดจำ(ขาย)"; break;
                    case 113: __result = "ยกเลิกคืนเงินมัดจำ"; break;
                    //
                    case 235: __result = "ใบวางบิล"; break;
                    case 236: __result = "ยกเลิกใบวางบิล"; break;
                    case 237: __result = "ใบเสร็จชั่วคราว"; break;
                    case 238: __result = "ยกเลิกใบเสร็จชั่วคราว"; break;
                    case 239: __result = "รับชำระหนี้"; break;
                    case 240: __result = "ยกเลิกรับชำระหนี้"; break;
                    case 241: __result = "ตัดหนี้สูญ(ลูกหนี้)"; break;
                    case 242: __result = "ยกเลิกตัดหนี้สูญ(ลูกหนี้)"; break;
                    case 264: __result = "เพิ่มหนี้(รายจ่ายอื่นๆ)"; break;
                    case 300: __result = "เบิกเงินสดย่อย"; break;
                    case 301: __result = "รับคืนเงินสดย่อย"; break;
                    case 310: __result = "พาเชียล_รับสินค้า"; break;
                    case 311: __result = "พาเชียล_ส่งคืนสินค้าหรือราคาผิด"; break;
                    case 315: __result = "พาเชียล_ตั้งหนี้"; break;
                    case 316: __result = "พาเชียล_เพิ่มหนี้"; break;
                    case 317: __result = "พาเชียล_ลดหนี้"; break;

                    case 250: __result = "รายได้อื่น"; break;
                    case 252: __result = "รายได้อื่นลดหนี้"; break;

                    case 260: __result = "รายจ่ายอื่น"; break;
                    case 262: __result = "รายจ่ายอื่นลดหนี้"; break;

                    case 401: __result = "ฝากเงิน"; break;
                    case 402: __result = "ถอนเงิน"; break;
                    case 405: __result = "เช็ครับยกมา"; break;
                    case 406: __result = "เช็คจ่ายยกมา"; break;
                    case 407: __result = "ยกเลิกเช็คจ่ายยกมา"; break;
                    case 410: __result = "เช็ครับฝาก"; break;
                    case 411: __result = "เช็ครับผ่าน"; break;
                    case 412: __result = "เช็ครับคืน"; break;
                    case 413: __result = "เช็ครับยกเลิก"; break;
                    case 414: __result = "เช็ครับเข้าใหม่"; break;
                    case 451: __result = "เช็คจ่ายผ่าน"; break;
                    case 453: __result = "เช็คจ่ายคืน"; break;
                    case 461: __result = "ขึ้นเงินบัตรเครดิต"; break;
                    case 462: __result = "ยกเลิกบัตรเครดิต"; break;

                    case 420: __result = "โอนเข้า"; break;
                    case 422: __result = "โอนออก"; break;
                    case 416: __result = "เปลี่ยนเช็ค"; break;
                    case 456: __result = "เปลี่ยนเช็ค"; break;
                    case 452: __result = "เช็คขาดสิทธิ์"; break;
                    case 76: __result = "ตรวจนับสินค้า"; break;
                    case 471: __result = "ยกเลิกเช็คจ่ายผ่าน"; break;
                    case 472: __result = "ยกเลิกเช็คจ่ายยกเลิก"; break;
                    case 473: __result = "ยกเลิกเช็คจ่ายคืน"; break;
                    case 476: __result = "ยกเลิกเปลี่ยนเช็คจ่าย"; break;
                    case 408: __result = "ยกเลิกเช็ครับยกมา"; break;
                    case 430: __result = "ยกเลิกฝากเช็ครับ"; break;
                    case 431: __result = "ยกเลิกผ่านเช็ครับ"; break;
                    case 432: __result = "ยกเลิกคืนเช็ครับ"; break;
                    case 433: __result = "ยกเลิกเช็ครับยกเลิก"; break;
                    case 434: __result = "ยกเลิกเช็ครับเช้าใหม่"; break;
                    case 436: __result = "ยกเลิกเปลี่ยนเช็ครับ"; break;
                    case 403: __result = "ยกเลิกฝากเงิน"; break;
                    case 404: __result = "ยกเลิกถอนเงิน"; break;
                    case 421: __result = "ยกเลิกโอนเงินเข้าธนาคาร"; break;
                    case 423: __result = "ยกเลิกโอนเงินออกธนาคาร"; break;
                    case 302: __result = "ยกเลิกเบิกเงินสดย่อย"; break;
                    case 303: __result = "ยกเลิกรับคืนเงินสดย่อย"; break;
                    case 251: __result = "ยกเลิกรายได้อื่น ๆ"; break;
                    case 253: __result = "ยกเลิกลดหนี้รายได้อื่น ๆ"; break;
                    case 254: __result = "เพิ่มหนี้รายได้อื่น ๆ"; break;
                    case 255: __result = "ยกเลิกเพิ่มหนี้ รายได้อื่น ๆ"; break;
                    case 261: __result = "ยกเลิกค่าใช้จ่ายอื่น ๆ"; break;
                    case 263: __result = "ยกเลิกลดหนี้ค่าใช้จ่ายอื่น ๆ"; break;
                    case 265: __result = "ยกเลิกเพิ่มหนี้ค่าใช้จ่ายอื่น ๆ"; break;
                    case 802: __result = "ลดแต้มสะสมลูกหนี้"; break;
                    case 150: __result = "ยกเลิกจ่ายเงินล่วงหน้า"; break;
                    case 151: __result = "ยกเลิกจ่ายเงินมัดจำ"; break;
                    case 152: __result = "ยกเลิกจ่ายเงินมัดจำรับคืน"; break;
                    case 161: __result = "ยกเลิกรับคืนเงินจ่ายล่วงหน้า"; break;
                    case 26: __result = "SoInquiry"; break;
                    case 27: __result = "SoInquiryCancel"; break;
                    case 28: __result = "SoEstimate"; break;
                    case 29: __result = "SoEstimateCancel"; break;
                    case 32: __result = "อนุมัติใบเสนอราคา"; break;
                    case 38: __result = "อนุมัติสั่งซื้อ/สั่งจองสินค้า"; break;
                    case 39: __result = "ยกเลิกสั่งซื้อ/สั่งจองสินค้า"; break;
                    case 52: __result = "อนุมัติใบสั่งขาย"; break;
                    case 111: __result = "อนุมัติใบสั่งขาย"; break;
                    case 144: __result = "ใบกำกับภาษีอย่างเต็มออกแทน"; break;

                    case 122: __result = "ขอเบิกสินค้าวัตถุดิบ"; break;
                    case 123: __result = "ยกเลิกขอเบิกสินค้าวัตถุดิบ"; break;

                    case 521: __result = "ฝาก"; break;
                    case 522: __result = "เบิกฝาก"; break;
                    case 523: __result = "รับคืนจากเบิกฝาก"; break;

                    case -1: __result = "****"; break;
                    default:
                        // || SystemInformation.ComputerName.ToLower().IndexOf("toe-pc") != -1
                        if (SystemInformation.ComputerName.ToLower().IndexOf("jead8") != -1)
                        {
                            MessageBox.Show("_transName case not found " + trans_type.ToString());
                        }
                        break;
                }
                return MyLib._myGlobal._resource(__result);
            }

            public static _transControlTypeEnum _transFlagByScreenCode(string screenCode)
            {
                switch (screenCode.ToUpper())
                {
                    // สินค้า
                    case "IF": return _transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป;
                    case "IFC": return _transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป_ยกเลิก;
                    case "IO": return _transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ;
                    case "IOC": return _transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ_ยกเลิก;
                    case "IR": return _transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก;
                    case "IRC": return _transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก_ยกเลิก;
                    case "IM": return _transControlTypeEnum.สินค้า_โอนออก;
                    case "IA": return _transControlTypeEnum.สินค้า_ปรับปรุงสต็อก;
                    case "IS": return _transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด;
                    case "IAC": return _transControlTypeEnum.สินค้า_ปรับปรุงสต็อก_ยกเลิก;
                    case "ISC": return _transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด_ยกเลิก;

                    case "RIO": return _transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ;
                    case "CRO": return _transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ_ยกเลิก;

                    case "RIM": return _transControlTypeEnum.สินค้า_ขอโอน;
                    case "CRM": return _transControlTypeEnum.สินค้า_ขอโอน_ยกเลิก;


                    // ระบบซื้อ
                    case "PD": return _transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า;
                    case "PDC": return _transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก;
                    case "PDR": return _transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน;
                    case "PDRC": return _transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก;
                    case "PC": return _transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ;
                    case "PCC": return _transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก;
                    case "PCR": return _transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน;
                    case "PRT": return _transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก;

                    case "PU": return _transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ;
                    case "PUC": return _transControlTypeEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก;
                    case "PT": return _transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด;
                    case "PTC": return _transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก;
                    case "PA": return _transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด;
                    case "PAC": return _transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก;

                    case "PI": return _transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า;
                    case "PIU": return _transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้;
                    case "PIA": return _transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด;
                    case "PIC": return _transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้;
                    case "PID": return _transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้;

                    // ระบบขาย
                    case "SD": return _transControlTypeEnum.ขาย_รับเงินล่วงหน้า;
                    case "SDC": return _transControlTypeEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก;
                    case "SDR": return _transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน;
                    case "SDRC": return _transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน_ยกเลิก;
                    case "SRV": return _transControlTypeEnum.ขาย_รับเงินมัดจำ;
                    case "SCR": return _transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก;
                    case "SRT": return _transControlTypeEnum.ขาย_เงินมัดจำ_คืน;
                    case "SCT": return _transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก;

                    case "SI":
                    case "SIP": // โต๋ เพิ่ม ขาย POS
                        return _transControlTypeEnum.ขาย_ขายสินค้าและบริการ;
                    case "SIC": return _transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก;
                    case "ST": return _transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้;
                    case "STC": return _transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก;
                    case "SA": return _transControlTypeEnum.ขาย_เพิ่มหนี้;
                    case "SAC": return _transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก;

                    // เจ้าหนี้
                    case "COB": return _transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น;
                    case "COC": return _transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก;
                    case "CCO": return _transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น;
                    case "CNO": return _transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น_ยกเลิก;
                    case "CDO": return _transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น;
                    case "CIC": return _transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก;
                    case "DE": return _transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้;
                    case "DEC": return _transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก;

                    // ลูกหนี้
                    case "AOB": return _transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น;
                    case "AOC": return _transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น_ยกเลิก;
                    case "ADO": return _transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น;
                    case "AIC": return _transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น_ยกเลิก;
                    case "ACO": return _transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น;
                    case "ADC": return _transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น_ยกเลิก;
                    case "EE": return _transControlTypeEnum.ลูกหนี้_รับชำระหนี้;
                    case "EEC": return _transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก;

                    // ธนาคาร
                    case "OI": return _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น;
                    case "OIC": return _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ยกเลิก;
                    case "OCN": return _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้;
                    case "OCC": return _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก;
                    case "ODN": return _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้;
                    case "ODC": return _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก;

                    case "EPO": return _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น;
                    case "EOC": return _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก;
                    case "EPC": return _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้;
                    case "ECC": return _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก;
                    case "EPD": return _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้;
                    case "EDC": return _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก;

                    case "CRD": return _transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน;
                    case "DM": return _transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน;
                    case "DMC": return _transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน_ยกเลิก;
                    case "WM": return _transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน;
                    case "WMC": return _transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน_ยกเลิก;
                    case "TM": return _transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร;
                    case "TOC": return _transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร_ยกเลิก;

                    case "CHP": return _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน;
                    case "CCP": return _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน_ยกเลิก;
                    case "CHD": return _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก;
                    case "CDC": return _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก_ยกเลิก;
                    case "CRT": return _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน;
                    case "CTC": return _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน_ยกเลิก;
                    case "CDE": return _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค;
                    case "CED": return _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค_ยกเลิก;
                    case "CRC": return _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก;
                    case "DCC": return _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก_ยกเลิก;

                    case "CPP": return _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน;
                    case "CPC": return _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน_ยกเลิก;
                    case "CPR": return _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน;
                    case "CEC": return _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน_ยกเลิก;
                    case "CPE": return _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค;
                    case "CEP": return _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค_ยกเลิก;
                    case "CHC": return _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก;
                    case "DPC": return _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก_ยกเลิก;

                    case "CHB": return _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา;
                    case "CPB": return _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา;

                    // เงินสดย่อย
                    case "PCD": return _transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย;
                    case "PEC": return _transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย_ยกเลิก;
                    case "PRM": return _transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย;
                    case "PMC": return _transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย_ยกเลิก;

                    case "AST": return _transControlTypeEnum.สินทรัพย์_โอนค่าเสื่อม;
                    //
                    default:
                        if (SystemInformation.ComputerName.ToLower().IndexOf("jead8") != -1 || SystemInformation.ComputerName.ToLower().IndexOf("toe-pc") != -1)
                        {
                            MessageBox.Show("_transFlagByScreenCode case not found " + screenCode.ToString());
                        }
                        break;
                }
                return _transControlTypeEnum.ว่าง;
            }

            public static _transControlTypeEnum _transFlagByNumber(int number)
            {
                switch (number)
                {
                    //(เจ้าหนี้)   
                    //----------------------------------------------------------------------------------
                    // ยกมา
                    //----------------------------------------------------------------------------------
                    case 81: return _transControlTypeEnum.เจ้าหนี้_ตั้งหนี้ยกมา;
                    case 83: return _transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้ยกมา;
                    case 85: return _transControlTypeEnum.เจ้าหนี้_ลดหนี้ยกมา;
                    //----------------------------------------------------------------------------------
                    // อื่นๆๆๆ
                    //----------------------------------------------------------------------------------
                    case 87: return _transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น;
                    case 88: return _transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก;
                    case 89: return _transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น;
                    case 90: return _transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก;
                    case 91: return _transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น;
                    case 92: return _transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น_ยกเลิก;
                    //----------------------------------------------------------------------------------
                    case 213: return _transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล;
                    case 214: return _transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล_ยกเลิก;
                    case 215: return _transControlTypeEnum.เจ้าหนี้_เตรียมจ่าย;
                    case 216: return _transControlTypeEnum.เจ้าหนี้_เตรียมจ่าย_ยกเลิก;
                    case 217: return _transControlTypeEnum.เจ้าหนี้_เตรียมจ่าย_อนุมัติ;
                    case 19: return _transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้;
                    case 21: return _transControlTypeEnum.เจ้าหนี้_ตัดหนี้สูญ;
                    case 22: return _transControlTypeEnum.เจ้าหนี้_ตัดหนี้สูญ_ยกเลิก;
                    case 23: return _transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก;
                    //----------------------------------------------------------------------------------
                    // ลูกหนี้
                    //----------------------------------------------------------------------------------
                    // ยกมา
                    //----------------------------------------------------------------------------------
                    case 93: return _transControlTypeEnum.ลูกหนี้_ตั้งหนี้ยกมา;
                    case 95: return _transControlTypeEnum.ลูกหนี้_เพิ่มหนี้ยกมา;
                    case 97: return _transControlTypeEnum.ลูกหนี้_ลดหนี้ยกมา;
                    //----------------------------------------------------------------------------------
                    // อื่นๆๆๆ
                    //----------------------------------------------------------------------------------
                    case 99: return _transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น;
                    case 100: return _transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น_ยกเลิก;
                    case 101: return _transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น;
                    case 102: return _transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น_ยกเลิก;
                    case 103: return _transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น;
                    case 104: return _transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น_ยกเลิก;
                    //-----------------------------------------------------------------------------------
                    case 235: return _transControlTypeEnum.ลูกหนี้_ใบวางบิล;
                    case 236: return _transControlTypeEnum.ลูกหนี้_ใบวางบิล_ยกเลิก;
                    case 237: return _transControlTypeEnum.ลูกหนี้_ใบเสร็จชั่วคราว;
                    case 238: return _transControlTypeEnum.ลูกหนี้_ใบเสร็จชั่วคราว_ยกเลิก;
                    case 239: return _transControlTypeEnum.ลูกหนี้_รับชำระหนี้;
                    case 240: return _transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก;
                    case 241: return _transControlTypeEnum.ลูกหนี้_ตัดหนี้สูญ;
                    case 242: return _transControlTypeEnum.ลูกหนี้_ตัดหนี้สูญ_ยกเลิก;
                    //
                    case 250: return _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น;
                    case 251: return _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ยกเลิก;
                    case 252: return _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้;
                    case 253: return _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก;
                    case 254: return _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้;
                    case 255: return _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก;
                    //
                    case 260: return _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น;
                    case 261: return _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก;
                    case 262: return _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้;
                    case 263: return _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก;
                    case 264: return _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้;
                    case 265: return _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก;
                    // PO Trans
                    case 1: return _transControlTypeEnum.PoInvestigate;
                    case 2: return _transControlTypeEnum.ซื้อ_เสนอซื้อ;
                    case 3: return _transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก;
                    case 4: return _transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ;
                    //case _transControlTypeEnum.PoRequestApprovalCancel: return 5;
                    case 6: return _transControlTypeEnum.ซื้อ_ใบสั่งซื้อ;
                    case 7: return _transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก;
                    case 8: return _transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ;
                    //case _transControlTypeEnum.PoInquiryApprovalCancel: return 9;
                    case 10: return _transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า;
                    case 11: return _transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ;
                    case 151: return _transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก;
                    case 25: return _transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน;
                    case 152: return _transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก;
                    case 150: return _transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก;
                    case 20: return _transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน;
                    case 161: return _transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก;
                    case 12: return _transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ;
                    case 13: return _transControlTypeEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก;
                    case 14: return _transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด;
                    case 15: return _transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก;
                    case 16: return _transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด;
                    case 17: return _transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก;
                    // SO Trans
                    case 26: return _transControlTypeEnum.SoInquiry;
                    case 27: return _transControlTypeEnum.SoInquiryCancel;
                    case 28: return _transControlTypeEnum.SoEstimate;
                    case 29: return _transControlTypeEnum.SoEstimateCancel;
                    case 30: return _transControlTypeEnum.ขาย_เสนอราคา;
                    case 31: return _transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก;
                    case 32: return _transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ;
                    case 34: return _transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า;
                    case 38: return _transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ;
                    case 39: return _transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก;
                    case 36: return _transControlTypeEnum.ขาย_สั่งขาย;
                    case 52: return _transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ;
                    case 37: return _transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก;
                    case 40: return _transControlTypeEnum.ขาย_รับเงินล่วงหน้า;
                    case 41: return _transControlTypeEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก;
                    case 110: return _transControlTypeEnum.ขาย_รับเงินมัดจำ;
                    case 111: return _transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก;
                    case 112: return _transControlTypeEnum.ขาย_เงินมัดจำ_คืน;
                    case 113: return _transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก;
                    case 42: return _transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน;
                    case 43: return _transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน_ยกเลิก;
                    case 44: return _transControlTypeEnum.ขาย_ขายสินค้าและบริการ;
                    // toe
                    case 144: return _transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน;
                    case 45: return _transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก;
                    case 46: return _transControlTypeEnum.ขาย_เพิ่มหนี้;
                    case 47: return _transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก;
                    case 48: return _transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้;
                    case 49: return _transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก;
                    case 50: return _transControlTypeEnum.SoDeliveryOrder;
                    case 51: return _transControlTypeEnum.SoDeliveryOrderCancel;

                    // IC Trans
                    case 501: return _transControlTypeEnum.คลัง_ยอดคงเหลือสินค้ายกมา;
                    case 502: return _transControlTypeEnum.คลัง_ตรวจนับสินค้า;
                    case 503: return _transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ;
                    case 505: return _transControlTypeEnum.คลัง_จ่ายสินค้าออก_จากการขาย;
                    case 509: return _transControlTypeEnum.คลัง_ปรับปรุงเพิ่มลดสินค้า;

                    case 54: return _transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา;
                    case 76: return _transControlTypeEnum.สินค้า_ตรวจนับสินค้า;
                    case 55: return _transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา_ยกเลิก;
                    case 56: return _transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ;
                    case 57: return _transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ_ยกเลิก;
                    case 58: return _transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก;
                    case 59: return _transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก_ยกเลิก;
                    case 60: return _transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป;
                    case 61: return _transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป_ยกเลิก;
                    case 62: return _transControlTypeEnum.StockInspect;
                    case 63: return _transControlTypeEnum.StockInspectCancel;
                    case 64: return _transControlTypeEnum.StockCheck;
                    case 65: return _transControlTypeEnum.StockCheckCancel;
                    case 66: return _transControlTypeEnum.สินค้า_ปรับปรุงสต็อก;
                    case 67: return _transControlTypeEnum.สินค้า_ปรับปรุงสต็อก_ยกเลิก;
                    case 68: return _transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด; // toe
                    case 69: return _transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด_ยกเลิก;
                    case 70: return _transControlTypeEnum.สินค้า_โอนเข้า;
                    case 71: return _transControlTypeEnum.สินค้า_โอนเข้า_ยกเลิก;
                    case 72: return _transControlTypeEnum.สินค้า_โอนออก;
                    case 73: return _transControlTypeEnum.สินค้า_โอนออก_ยกเลิก;
                    // Jead ใส่ไว้ก่อน (เพิ่มประเภทเอกสาร)
                    case 270: return _transControlTypeEnum.StockCheckResult;
                    // เงินสดย่อย
                    case 300: return _transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย;
                    case 301: return _transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย;
                    //
                    case 405: return _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา;
                    case 406: return _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา;
                    //

                    case 122: return _transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ;
                    case 123: return _transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ_ยกเลิก;

                    case 124: return _transControlTypeEnum.สินค้า_ขอโอน;
                    case 125: return _transControlTypeEnum.สินค้า_ขอโอน_ยกเลิก;

                    case 410: return _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก;
                    case 411: return _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน;
                    case 412: return _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน;

                    case 413: return _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก;
                    case 414: return _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่;
                    case 416: return _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค;

                    case 407: return _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา_ยกเลิก;
                    case 451: return _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน;
                    case 452: return _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก;
                    case 453: return _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน;
                    case 456: return _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค;

                    case 310: return _transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า;
                    case 311: return _transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด;
                    case 315: return _transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้;
                    case 316: return _transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้;
                    case 317: return _transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้;

                    case 401: return _transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน;

                    case 521: return _transControlTypeEnum.คลัง_รับฝาก_ฝาก;
                    case 522: return _transControlTypeEnum.คลัง_รับฝาก_เบิก;
                    case 523: return _transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก;

                    case 1901: return _transControlTypeEnum.Shipment;
                    case 1801: return _transControlTypeEnum.สินทรัพย์_โอนค่าเสื่อม;

                    default:
                        if (SystemInformation.ComputerName.ToLower().IndexOf("jead8") != -1 || SystemInformation.ComputerName.ToLower().IndexOf("toe-pc") != -1)
                        {
                            MessageBox.Show("_icTransFlagGlobal by number case not found " + number.ToString());
                        }
                        break;
                }
                return _transControlTypeEnum.ว่าง;
            }

            public static string _transFlagStr(_transControlTypeEnum icTransControlType)
            {
                return _transFlag(icTransControlType).ToString();
            }

            public static int _transFlag(_transControlTypeEnum icTransControlType)
            {
                switch (icTransControlType)
                {
                    //(เจ้าหนี้)   
                    //----------------------------------------------------------------------------------
                    // ยกมา
                    //----------------------------------------------------------------------------------
                    case _transControlTypeEnum.เจ้าหนี้_ตั้งหนี้ยกมา: return 81;
                    case _transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้ยกมา: return 83;
                    case _transControlTypeEnum.เจ้าหนี้_ลดหนี้ยกมา: return 85;
                    //----------------------------------------------------------------------------------
                    // อื่นๆๆๆ
                    //----------------------------------------------------------------------------------
                    case _transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น: return 87;
                    case _transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก: return 88;
                    case _transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น: return 89;
                    case _transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก: return 90;
                    case _transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น: return 91;
                    case _transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น_ยกเลิก: return 92;
                    //----------------------------------------------------------------------------------
                    case _transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล: return 213;
                    case _transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล_ยกเลิก: return 214;
                    case _transControlTypeEnum.เจ้าหนี้_เตรียมจ่าย: return 215;
                    case _transControlTypeEnum.เจ้าหนี้_เตรียมจ่าย_ยกเลิก: return 216;
                    case _transControlTypeEnum.เจ้าหนี้_เตรียมจ่าย_อนุมัติ: return 217;
                    case _transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้: return 19;
                    case _transControlTypeEnum.เจ้าหนี้_ตัดหนี้สูญ: return 21;
                    case _transControlTypeEnum.เจ้าหนี้_ตัดหนี้สูญ_ยกเลิก: return 22;
                    case _transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก: return 23;
                    //----------------------------------------------------------------------------------
                    // ลูกหนี้
                    //----------------------------------------------------------------------------------
                    // ยกมา
                    //----------------------------------------------------------------------------------
                    case _transControlTypeEnum.ลูกหนี้_ตั้งหนี้ยกมา: return 93;
                    case _transControlTypeEnum.ลูกหนี้_เพิ่มหนี้ยกมา: return 95;
                    case _transControlTypeEnum.ลูกหนี้_ลดหนี้ยกมา: return 97;
                    case _transControlTypeEnum.ลูกหนี้_แต้มยกมา: return 801; // toe
                    case _transControlTypeEnum.ลูกหนี้_ลดแต้ม: return 802; // toe
                    //----------------------------------------------------------------------------------
                    // อื่นๆๆๆ
                    //----------------------------------------------------------------------------------
                    case _transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น: return 99;
                    case _transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น_ยกเลิก: return 100;
                    case _transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น: return 101;
                    case _transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น_ยกเลิก: return 102;
                    case _transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น: return 103;
                    case _transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น_ยกเลิก: return 104;
                    //-----------------------------------------------------------------------------------
                    case _transControlTypeEnum.ลูกหนี้_ใบวางบิล: return 235;
                    case _transControlTypeEnum.ลูกหนี้_ใบวางบิล_ยกเลิก: return 236;
                    case _transControlTypeEnum.ลูกหนี้_ใบเสร็จชั่วคราว: return 237;
                    case _transControlTypeEnum.ลูกหนี้_ใบเสร็จชั่วคราว_ยกเลิก: return 238;
                    case _transControlTypeEnum.ลูกหนี้_รับชำระหนี้: return 239;
                    case _transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก: return 240;
                    case _transControlTypeEnum.ลูกหนี้_ตัดหนี้สูญ: return 241;
                    case _transControlTypeEnum.ลูกหนี้_ตัดหนี้สูญ_ยกเลิก: return 242;

                    //
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น: return 250;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ยกเลิก: return 251;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้: return 252;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก: return 253;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้: return 254;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก: return 255;
                    //
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น: return 260;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก: return 261;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้: return 262;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก: return 263;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้: return 264;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก: return 265;
                    // PO Trans
                    case _transControlTypeEnum.PoInvestigate: return 1;
                    case _transControlTypeEnum.ซื้อ_เสนอซื้อ: return 2;
                    case _transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก: return 3;
                    case _transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ: return 4;
                    //case _transControlTypeEnum.PoRequestApprovalCancel: return 5;
                    case _transControlTypeEnum.ซื้อ_ใบสั่งซื้อ: return 6;
                    case _transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก: return 7;
                    case _transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ: return 8;
                    //case _transControlTypeEnum.PoInquiryApprovalCancel: return 9;
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า: return 10;
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ: return 11;
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก: return 151;
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน: return 25;
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก: return 152;
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก: return 150;
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน: return 20;
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก: return 161;
                    case _transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ: return 12;
                    case _transControlTypeEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก: return 13;
                    case _transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด: return 14;
                    case _transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก: return 15;
                    case _transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด: return 16;
                    case _transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก: return 17;
                    case _transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า: return 310;
                    case _transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด: return 311;
                    case _transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้: return 315;
                    case _transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้: return 316;
                    case _transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้: return 317;
                    // SO Trans
                    case _transControlTypeEnum.SoInquiry: return 26;
                    case _transControlTypeEnum.SoInquiryCancel: return 27;
                    case _transControlTypeEnum.SoEstimate: return 28;
                    case _transControlTypeEnum.SoEstimateCancel: return 29;
                    case _transControlTypeEnum.ขาย_เสนอราคา: return 30;
                    case _transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก: return 31;
                    case _transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ: return 32;
                    case _transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า: return 34;
                    case _transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ: return 38;
                    case _transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก: return 39;
                    case _transControlTypeEnum.ขาย_สั่งขาย: return 36;
                    case _transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ: return 52;
                    case _transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก: return 37;
                    case _transControlTypeEnum.ขาย_รับเงินล่วงหน้า: return 40;
                    case _transControlTypeEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก: return 41;
                    case _transControlTypeEnum.ขาย_รับเงินมัดจำ: return 110;
                    case _transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก: return 111;
                    case _transControlTypeEnum.ขาย_เงินมัดจำ_คืน: return 112;
                    case _transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก: return 113;
                    case _transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน: return 42;
                    case _transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน_ยกเลิก: return 43;
                    case _transControlTypeEnum.ขาย_ขายสินค้าและบริการ: return 44;

                    // toe
                    case _transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน: return 144;
                    case _transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก: return 45;
                    case _transControlTypeEnum.ขาย_เพิ่มหนี้: return 46;
                    case _transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก: return 47;
                    case _transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้: return 48;
                    case _transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก: return 49;
                    case _transControlTypeEnum.SoDeliveryOrder: return 50;
                    case _transControlTypeEnum.SoDeliveryOrderCancel: return 51;

                    // IC Trans
                    case _transControlTypeEnum.คลัง_ยอดคงเหลือสินค้ายกมา: return 501;
                    case _transControlTypeEnum.คลัง_ตรวจนับสินค้า: return 502;
                    case _transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ: return 503;
                    case _transControlTypeEnum.คลัง_จ่ายสินค้าออก_จากการขาย: return 505;
                    case _transControlTypeEnum.คลัง_ปรับปรุงเพิ่มลดสินค้า: return 509;

                    case _transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา: return 54;
                    case _transControlTypeEnum.สินค้า_ตรวจนับสินค้า: return 76;
                    case _transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา_ยกเลิก: return 55;
                    case _transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ: return 56;
                    case _transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ_ยกเลิก: return 57;
                    case _transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก: return 58;
                    case _transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก_ยกเลิก: return 59;
                    case _transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป: return 60;
                    case _transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป_ยกเลิก: return 61;
                    case _transControlTypeEnum.StockInspect: return 62;
                    case _transControlTypeEnum.StockInspectCancel: return 63;
                    case _transControlTypeEnum.StockCheck: return 64;
                    case _transControlTypeEnum.StockCheckCancel: return 65;
                    case _transControlTypeEnum.สินค้า_ปรับปรุงสต็อก: return 66;
                    case _transControlTypeEnum.สินค้า_ปรับปรุงสต็อก_ยกเลิก: return 67;
                    case _transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด: return 68;
                    case _transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด_ยกเลิก: return 69;
                    case _transControlTypeEnum.สินค้า_โอนเข้า: return 70;
                    case _transControlTypeEnum.สินค้า_โอนเข้า_ยกเลิก: return 71;
                    case _transControlTypeEnum.สินค้า_โอนออก: return 72;
                    case _transControlTypeEnum.สินค้า_โอนออก_ยกเลิก: return 73;
                    // Jead ใส่ไว้ก่อน (เพิ่มประเภทเอกสาร)
                    case _transControlTypeEnum.StockCheckResult: return 270;
                    //
                    case _transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย: return 300;
                    case _transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย: return 301;
                    case _transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย_ยกเลิก: return 302;
                    case _transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย_ยกเลิก: return 303;

                    //
                    case _transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน: return 401;
                    case _transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน_ยกเลิก: return 403;
                    case _transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน: return 402;
                    case _transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน_ยกเลิก: return 404;
                    //
                    case _transControlTypeEnum.เงินสดธนาคาร_โอนเงินเข้าธนาคาร: return 420;
                    case _transControlTypeEnum.เงินสดธนาคาร_โอนเงินเข้าธนาคาร_ยกเลิก: return 421;
                    case _transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร: return 422;
                    case _transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร_ยกเลิก: return 423;

                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา: return 405;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก: return 410;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน: return 411;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน: return 412;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก: return 413;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่: return 414;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค: return 416;

                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก_ยกเลิก: return 430;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน_ยกเลิก: return 431;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน_ยกเลิก: return 432;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก_ยกเลิก: return 433;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่_ยกเลิก: return 434;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค_ยกเลิก: return 436;
                    //
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา: return 406;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา_ยกเลิก: return 407;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน: return 451;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก: return 452;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน: return 453;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค: return 456;

                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน_ยกเลิก: return 471;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก_ยกเลิก: return 472;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน_ยกเลิก: return 473;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค_ยกเลิก: return 476;

                    //
                    case _transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน: return 461;
                    case _transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก: return 462;
                    //
                    case _transControlTypeEnum.สินค้า_บันทึกการจัดส่ง: return 701;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา_ยกเลิก: return 408;
                    case _transControlTypeEnum.แสดงราคาตามสูตรสี: return -1;
                    case _transControlTypeEnum.สินค้า_สูตรสี: return -1;
                    case _transControlTypeEnum.สินค้า_สินค้าจัดชุด: return -1;
                    case _transControlTypeEnum.สินค้่า_ตรวจสอบสินค้า_serial: return -1;
                    case _transControlTypeEnum.StockTransferRequest: return -1;
                    case _transControlTypeEnum.สินค้า_เงื่อนไขแถมตอนซื้อ: return -1;
                    case _transControlTypeEnum.สินค้า_สินค้าแถมตอนซื้อ: return -1;
                    case _transControlTypeEnum.POS_รับเงินทอน: return -1;

                    case _transControlTypeEnum.IMEX_Bill_Collector: return 810;

                    case _transControlTypeEnum.บัญชี_ข้อมูลรายวัน: return 1999;
                    case _transControlTypeEnum.GL_ภาษีถูกหักณที่จ่าย: return 1501;
                    case _transControlTypeEnum.GL_ภาษีหักณที่จ่าย: return 1502;

                    case _transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ: return 122;
                    case _transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ_ยกเลิก: return 123;

                    case _transControlTypeEnum.สินค้า_ขอโอน: return 124;
                    case _transControlTypeEnum.สินค้า_ขอโอน_ยกเลิก: return 125;

                    case _transControlTypeEnum.คลัง_รับฝาก_ฝาก: return 521;
                    case _transControlTypeEnum.คลัง_รับฝาก_เบิก: return 522;
                    case _transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก: return 523;

                    case _transControlTypeEnum.Shipment: return 1901;
                    case _transControlTypeEnum.สินทรัพย์_โอนค่าเสื่อม: return 1801;

                    // บัญชี
                    case _transControlTypeEnum.บัญชี_ประมวลผลสิ้นงวด: return 1998;
                    case _transControlTypeEnum.บัญชี_ประมวลผลสิ้นปี: return 1999;

                    default:
                        if (SystemInformation.ComputerName.ToLower().IndexOf("jead8") != -1 || SystemInformation.ComputerName.ToLower().IndexOf("toe-pc") != -1)
                        {
                            if (icTransControlType != _transControlTypeEnum.ว่าง)
                                MessageBox.Show("_icTransFlagGlobal case not found " + icTransControlType.ToString());
                        }
                        break;
                }
                return 0;
            }
        }

        public static class _transGlobalTemplate
        {
            public static string _transTemplate(_transControlTypeEnum icTransControlType)
            {
                switch (icTransControlType)
                {
                    // PO Trans
                    case _transControlTypeEnum.PoInvestigate: return _g.g._search_screen_ic_stk_transfer;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา: return _g.g._search_screen_เงินสดธนาคาร_เช็คจ่าย_ยกมา;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา: return _g.g._search_screen_เงินสดธนาคาร_เช็ครับ_ยกมา;
                    case _transControlTypeEnum.ซื้อ_เสนอซื้อ: return _g.g._search_screen_ซื้อ_เสนอซื้อ;
                    case _transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ: return _g.g._search_screen_ซื้อ_เสนอซื้อ_อนุมัติ;
                    case _transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก: return _g.g._search_screen_ซื้อ_เสนอซื้อ_ยกเลิก;
                    case _transControlTypeEnum.ซื้อ_ใบสั่งซื้อ: return _g.g._search_screen_po_inquiry;
                    case _transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก: return _g.g._search_screen_po_inquiry_cancel;
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า: return _g.g._screen_po_deposit;
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ: return _g.g._screen_po_deposit;
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก: return _g.g._screen_po_deposit;
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก: return _g.g._search_screen_po_advance_cancel;
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน: return _g.g._screen_po_deposit;
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน: return _g.g._screen_po_deposit;
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก: return _g.g._screen_po_deposit;
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก: return _g.g._search_screen_po_advance_cancel;
                    case _transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ: return _g.g._search_screen_po_purchase_approve;
                    case _transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้: return _g.g._search_screen_purchase;
                    case _transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า: return _g.g._search_screen_purchase;
                    case _transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ: return _g.g._search_screen_purchase;
                    case _transControlTypeEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก: return _g.g._search_screen_purchase_cancel;
                    case _transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด: return _g.g._search_screen_po_so_inquiry;
                    case _transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้: return _g.g._search_screen_po_so_inquiry;
                    case _transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด: return _g.g._search_screen_po_so_inquiry;
                    case _transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก: return _g.g._search_screen_purchase_debit_cancel;
                    case _transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด: return _g.g._search_screen_po_so_inquiry;
                    case _transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้: return _g.g._search_screen_po_so_inquiry;
                    case _transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก: return _g.g._search_screen_purchase_credit_cancel;
                    case _transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น: return _g.g._search_screen_purchase;
                    case _transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น: return _g.g._search_screen_purchase;
                    case _transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น: return _g.g._search_screen_purchase;
                    case _transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก: return _g.g._search_screen_purchase;
                    case _transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น_ยกเลิก: return _g.g._search_screen_purchase;
                    case _transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก: return _g.g._search_screen_purchase;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น: return _g.g._search_screen_purchase;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้: return _g.g._search_screen_purchase;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้: return _g.g._search_screen_purchase;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก: return _g.g._search_screen_purchase;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก: return _g.g._search_screen_purchase;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก: return _g.g._search_screen_purchase;
                    // SO Trans
                    case _transControlTypeEnum.SoEstimate: return _g.g._search_screen_po_so_inquiry;
                    case _transControlTypeEnum.SoInquiry: return _g.g._search_screen_po_so_inquiry;
                    case _transControlTypeEnum.ขาย_เสนอราคา: return _g.g._search_screen_ขาย_ใบเสนอราคา;
                    case _transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ: return _g.g._search_screen_ขาย_ใบเสนอราคา_อนุมัติ;
                    case _transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก: return _g.g._search_screen_ขาย_ใบเสนอราคา_ยกเลิก;
                    case _transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า: return _g.g._search_screen_ขาย_ใบสั่งจอง_สั่งซื้อ;
                    case _transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ: return _g.g._search_screen_ขาย_ใบสั่งจอง_สั่งซื้อ_อนุมัติ;
                    case _transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก: return _g.g._search_screen_ขาย_ใบสั่งจอง_สั่งซื้อ_ยกเลิก;
                    case _transControlTypeEnum.ขาย_สั่งขาย: return _g.g._search_screen_ขาย_ใบสั่งจอง_สั่งซื้อ;
                    case _transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ: return _g.g._search_screen_ขาย_ใบสั่งจอง_สั่งซื้อ_อนุมัติ;
                    case _transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก: return _g.g._search_screen_ขาย_ใบสั่งจอง_สั่งซื้อ_ยกเลิก;
                    case _transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้: return _g.g._search_screen_ขาย_ใบสั่งจอง_สั่งซื้อ;
                    case _transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก: return _g.g._search_screen_ขาย_ใบสั่งจอง_สั่งซื้อ_ยกเลิก;
                    case _transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น: return _g.g._search_screen_ขาย_ใบสั่งจอง_สั่งซื้อ;
                    case _transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น: return _g.g._search_screen_ขาย_ใบสั่งจอง_สั่งซื้อ;
                    case _transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น: return _g.g._search_screen_ขาย_ใบสั่งจอง_สั่งซื้อ;
                    case _transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น_ยกเลิก: return _g.g._search_screen_purchase;
                    case _transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น_ยกเลิก: return _g.g._search_screen_purchase;
                    case _transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น_ยกเลิก: return _g.g._search_screen_purchase;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น: return _g.g._search_screen_ขาย_ใบสั่งจอง_สั่งซื้อ;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้: return _g.g._search_screen_ขาย_ใบสั่งจอง_สั่งซื้อ;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้: return _g.g._search_screen_ขาย_ใบสั่งจอง_สั่งซื้อ;

                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ยกเลิก: return _g.g._search_screen_ขาย_ใบสั่งจอง_สั่งซื้อ;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก: return _g.g._search_screen_ขาย_ใบสั่งจอง_สั่งซื้อ;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก: return _g.g._search_screen_ขาย_ใบสั่งจอง_สั่งซื้อ;

                    case _transControlTypeEnum.ขาย_รับเงินล่วงหน้า: return _g.g._screen_so_deposit;
                    case _transControlTypeEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก: return _g.g._screen_so_deposit;
                    case _transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน: return _g.g._screen_so_deposit;
                    case _transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน_ยกเลิก: return _g.g._screen_so_deposit;

                    case _transControlTypeEnum.ขาย_รับเงินมัดจำ: return _g.g._screen_so_deposit;
                    case _transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก: return _g.g._screen_so_deposit;
                    case _transControlTypeEnum.ขาย_เงินมัดจำ_คืน: return _g.g._screen_so_deposit;
                    case _transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก: return _g.g._screen_so_deposit;

                    case _transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                        {
                            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLBIllFree)
                                return "screen_mini_sale";
                            return _g.g._search_screen_sale;
                        }

                    case _transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน: return _g.g._search_screen_sale; // toe
                    case _transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก: return _g.g._search_screen_ขาย_ใบสั่งจอง_สั่งซื้อ_ยกเลิก;
                    case _transControlTypeEnum.ขาย_เพิ่มหนี้: return _g.g._search_screen_ขาย_ใบสั่งจอง_สั่งซื้อ;
                    case _transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก: return _g.g._search_screen_ขาย_ใบสั่งจอง_สั่งซื้อ_ยกเลิก;
                    //IC Trans
                    case _transControlTypeEnum.คลัง_ยอดคงเหลือสินค้ายกมา: return _g.g._search_screen_ic_stk_wh_balance;
                    case _transControlTypeEnum.คลัง_ตรวจนับสินค้า: return _g.g._search_screen_ic_stk_balance;
                    case _transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ: return _g.g._search_screen_ic_stk_wh_balance;
                    case _transControlTypeEnum.คลัง_จ่ายสินค้าออก_จากการขาย: return _g.g._search_screen_ic_stk_wh_balance;
                    case _transControlTypeEnum.คลัง_ปรับปรุงเพิ่มลดสินค้า: return _g.g._search_screen_ic_stk_wh_balance;
                    //
                    case _transControlTypeEnum.สินค้า_ตรวจนับสินค้า: return _g.g._search_screen_ic_stk_balance;
                    case _transControlTypeEnum.สินค้า_ปรับปรุงสต็อก: return _g.g._search_screen_ic_stk_adjust;
                    case _transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด: return _g.g._search_screen_ic_stk_adjust;

                    case _transControlTypeEnum.สินค้า_ปรับปรุงสต็อก_ยกเลิก: return _g.g._search_screen_ic_stk_adjust;
                    case _transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด_ยกเลิก: return _g.g._search_screen_ic_stk_adjust;
                    case _transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา: return _g.g._search_screen_ic_stk_balance;
                    case _transControlTypeEnum.StockCheck: return _g.g._search_screen_ic_stk_check;
                    case _transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป: return _g.g._search_screen_ic_stk_finish_goods;
                    case _transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป_ยกเลิก: return _g.g._search_screen_ic_stk_finish_goods;
                    case _transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ: return _g.g._search_screen_ic_stk_request;
                    case _transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ_ยกเลิก: return _g.g._search_screen_ic_stk_request;
                    case _transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก: return _g.g._search_screen_ic_stk_return;
                    case _transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก_ยกเลิก: return _g.g._search_screen_ic_stk_return;
                    case _transControlTypeEnum.สินค้า_โอนเข้า: return _g.g._search_screen_ic_stk_transfer;
                    case _transControlTypeEnum.สินค้า_โอนเข้า_ยกเลิก: return _g.g._search_screen_ic_stk_transfer;
                    case _transControlTypeEnum.StockCheckResult: return _g.g._search_screen_ic_stk_check_result;
                    case _transControlTypeEnum.สินค้า_โอนออก: return _g.g._search_screen_ic_stk_transfer;
                    case _transControlTypeEnum.สินค้า_โอนออก_ยกเลิก: return _g.g._search_screen_ic_stk_transfer;

                    //
                    case _transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย: return _g.g._search_screen_เงินสดย่อย_รายวัน;
                    case _transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย: return _g.g._search_screen_เงินสดย่อย_รายวัน;
                    case _transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย_ยกเลิก: return _g.g._search_screen_เงินสดย่อย_รายวัน;
                    case _transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย_ยกเลิก: return _g.g._search_screen_เงินสดย่อย_รายวัน;
                    //
                    case _transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน: return _g.g._search_screen_ธนาคาร_ฝากเงิน;
                    case _transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน: return _g.g._search_screen_ธนาคาร_ฝากเงิน;
                    case _transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร: return _g.g._search_screen_ธนาคาร_ฝากเงิน;
                    //
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก: return _g.g._search_screen_cb_cheque_trans;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน: return _g.g._search_screen_cb_cheque_trans;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน: return _g.g._search_screen_cb_cheque_trans;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก: return _g.g._search_screen_cb_cheque_trans;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่: return _g.g._search_screen_cb_cheque_trans;
                    //
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก: return _g.g._search_screen_cb_cheque_trans;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน: return _g.g._search_screen_cb_cheque_trans;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน: return _g.g._search_screen_cb_cheque_trans;
                    //
                    case _transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน: return _g.g._search_screen_cb_cheque_trans;
                    case _transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก: return _g.g._search_screen_cb_cheque_trans;
                    //
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค: return _g.g._search_screen_cb_cheque_trans;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค: return _g.g._search_screen_cb_cheque_trans;
                    //
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค_ยกเลิก:

                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค_ยกเลิก:

                    case _transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร_ยกเลิก:
                        return _g.g._search_screen_chq_cancel;

                    case _transControlTypeEnum.สินค้า_บันทึกการจัดส่ง: return _g.g._search_screen_cb_cheque_trans;
                    case _transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ:
                    case _transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ_ยกเลิก:

                        return _g.g._search_screen_ic_stk_pre_request;
                    case _transControlTypeEnum.สินค้า_ขอโอน: return _g.g._search_screen_ic_stk_transfer;
                    case _transControlTypeEnum.สินค้า_ขอโอน_ยกเลิก: return _g.g._search_screen_ic_stk_transfer;

                    case _transControlTypeEnum.คลัง_รับฝาก_ฝาก:
                    case _transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:
                    case _transControlTypeEnum.คลัง_รับฝาก_เบิก:
                        return "screen_wms_product_deposit";

                    default:
                        if (SystemInformation.ComputerName.ToLower().IndexOf("jead8") != -1 || SystemInformation.ComputerName.ToLower().IndexOf("toe-pc") != -1)
                        {
                            MessageBox.Show("_ictransGlobalTemplate case not found " + icTransControlType.ToString());
                        }
                        break;
                }
                return null;
            }
        }

        public static class _transTypeGlobal
        {
            public static int _vatBuyType(_transControlTypeEnum transControlType)
            {
                // ภาษีซื้อ
                switch (transControlType)
                {
                    // ด้านบวก
                    case _transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                    case _transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                    case _transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                    case _transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                    case _transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ:
                    case _transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                    case _transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                        return 1;
                    // ด้านลบ
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน:
                    case _transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                    case _transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                    case _transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                        return -1;
                    //
                    default:
                        if (SystemInformation.ComputerName.ToLower().IndexOf("jead8") != -1 || SystemInformation.ComputerName.ToLower().IndexOf("toe-pc") != -1)
                        {
                            MessageBox.Show("_vatBuyType case not found " + transControlType.ToString());
                        }
                        break;
                }
                return 1;
            }

            public static int _vatSaleType(_transControlTypeEnum transControlType)
            {
                // ภาษีขาย
                switch (transControlType)
                {
                    // ด้านบวก
                    case _transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                    case _transControlTypeEnum.ขาย_เพิ่มหนี้:
                    case _transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                    case _transControlTypeEnum.ขาย_รับเงินมัดจำ:
                    case _transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                    case _transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                        return 1;
                    // ด้านลบ
                    case _transControlTypeEnum.ขาย_เงินมัดจำ_คืน:
                    case _transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                    case _transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                        return -1;
                    //
                    default:
                        if (SystemInformation.ComputerName.ToLower().IndexOf("jead8") != -1 || SystemInformation.ComputerName.ToLower().IndexOf("toe-pc") != -1)
                        {
                            MessageBox.Show("_vatSaleType case not found " + transControlType.ToString());
                        }
                        break;
                }
                return 1;
            }

            public static int _payType(_transControlTypeEnum transControlType)
            {
                switch (transControlType)
                {
                    // ด้านรับเงิน
                    case _transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                    case _transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                    case _transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                    case _transControlTypeEnum.ขาย_เพิ่มหนี้:
                    case _transControlTypeEnum.ขาย_รับเงินล่วงหน้า:
                    case _transControlTypeEnum.ขาย_รับเงินมัดจำ:
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน:
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน:
                    case _transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                    case _transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                    case _transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                    case _transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย:
                    case _transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน:
                    case _transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                        return 1;
                    // ด้านจ่ายเงิน
                    case _transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                    case _transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                    case _transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน:
                    case _transControlTypeEnum.ขาย_เงินมัดจำ_คืน:
                    case _transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                    case _transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                    case _transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า:
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ:
                    case _transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                    case _transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                    case _transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย:
                    case _transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                    case _transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:
                        return 2;
                    //
                    default:
                        if (SystemInformation.ComputerName.ToLower().IndexOf("jead8") != -1 || SystemInformation.ComputerName.ToLower().IndexOf("toe-pc") != -1)
                        {
                            MessageBox.Show("_payType case not found " + transControlType.ToString());
                        }
                        break;
                }
                return 0;
            }

            public static int _transType(_transControlTypeEnum transControlType)
            {
                switch (transControlType)
                {
                    case _transControlTypeEnum.เงินสดธนาคาร_โอนเงินเข้าธนาคาร:
                    case _transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร:
                        return 0;
                    // PO Trans
                    case _transControlTypeEnum.PoInvestigate:
                    case _transControlTypeEnum.ซื้อ_เสนอซื้อ:
                    case _transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก:
                    case _transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ:
                    case _transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                    case _transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                    case _transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ:
                    case _transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                    case _transControlTypeEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก:
                    case _transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                    case _transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก:
                    case _transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                    case _transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                    case _transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก:
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า:
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก:
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน:
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก:
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ:
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก:
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน:
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก:
                    case _transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                    case _transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                    case _transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                    case _transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                    case _transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก:
                    case _transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น_ยกเลิก:
                    case _transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก:
                    case _transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                    case _transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                    case _transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                    case _transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:

                        return 1;
                    // SO Trans
                    case _transControlTypeEnum.SoEstimate:
                    case _transControlTypeEnum.SoInquiry:
                    case _transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                    case _transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                    case _transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก:
                    case _transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                    case _transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก:
                    case _transControlTypeEnum.ขาย_เพิ่มหนี้:
                    case _transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก:
                    case _transControlTypeEnum.ขาย_เสนอราคา:
                    case _transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก:
                    case _transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ:
                    case _transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                    case _transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ:
                    case _transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                    case _transControlTypeEnum.ขาย_สั่งขาย:
                    case _transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ:
                    case _transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                    case _transControlTypeEnum.ขาย_รับเงินล่วงหน้า:
                    case _transControlTypeEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก:
                    case _transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน:
                    case _transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน_ยกเลิก:
                    case _transControlTypeEnum.ขาย_รับเงินมัดจำ:
                    case _transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก:
                    case _transControlTypeEnum.ขาย_เงินมัดจำ_คืน:
                    case _transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก:
                    case _transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                    case _transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                    case _transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                    case _transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                    case _transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น_ยกเลิก:
                    case _transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น_ยกเลิก:
                    case _transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:

                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก:

                    case _transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย:
                    case _transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย:
                    case _transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย_ยกเลิก:
                    case _transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย_ยกเลิก:

                    case _transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                    case _transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่:
                    case _transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน:
                    case _transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                    // toe
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค_ยกเลิก:


                    case _transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน_ยกเลิก:
                    case _transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร_ยกเลิก:

                        return 2;
                    // IC Trans
                    case _transControlTypeEnum.คลัง_ยอดคงเหลือสินค้ายกมา:
                    case _transControlTypeEnum.คลัง_ตรวจนับสินค้า:
                    case _transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ:
                    case _transControlTypeEnum.คลัง_จ่ายสินค้าออก_จากการขาย:
                    case _transControlTypeEnum.คลัง_ปรับปรุงเพิ่มลดสินค้า:

                    case _transControlTypeEnum.คลัง_รับฝาก_เบิก:
                    case _transControlTypeEnum.คลัง_รับฝาก_ฝาก:
                    case _transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:

                    case _transControlTypeEnum.สินค้า_ตรวจนับสินค้า:
                    case _transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา:
                    case _transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                    case _transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ_ยกเลิก:
                    case _transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                    case _transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก_ยกเลิก:
                    case _transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                    case _transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป_ยกเลิก:
                    case _transControlTypeEnum.StockCheck:
                    case _transControlTypeEnum.สินค้า_ปรับปรุงสต็อก:
                    case _transControlTypeEnum.สินค้า_ปรับปรุงสต็อก_ยกเลิก:
                    case _transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด:
                    case _transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด_ยกเลิก:
                    case _transControlTypeEnum.สินค้า_โอนเข้า:
                    case _transControlTypeEnum.สินค้า_โอนออก:
                    case _transControlTypeEnum.สินค้า_โอนออก_ยกเลิก:
                    case _transControlTypeEnum.StockCheckResult:
                    case _transControlTypeEnum.สินค้า_บันทึกการจัดส่ง:
                    case _transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ:
                    case _transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ_ยกเลิก:
                    case _transControlTypeEnum.สินค้า_ขอโอน:
                    case _transControlTypeEnum.สินค้า_ขอโอน_ยกเลิก:
                        return 3;
                    default:
                        if (SystemInformation.ComputerName.ToLower().IndexOf("jead8") != -1 || SystemInformation.ComputerName.ToLower().IndexOf("toe-pc") != -1)
                        {
                            MessageBox.Show("_transType case not found " + transControlType.ToString());
                        }
                        break;
                }
                return 0;
            }
        }

        public static class _transCalcTypeGlobal
        {
            public static int _transStockCalcType(_transControlTypeEnum icTransControlType)
            {
                // 1=เพิ่ม,-1=ลด
                switch (icTransControlType)
                {
                    // PO Trans
                    case _transControlTypeEnum.PoInvestigate: return 1;
                    case _transControlTypeEnum.ซื้อ_เสนอซื้อ: return 1;
                    case _transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก: return -1;
                    case _transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ: return 1;
                    case _transControlTypeEnum.ซื้อ_ใบสั่งซื้อ: return 1;
                    case _transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก: return -1;
                    case _transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ: return 1;
                    case _transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ: return 1;
                    case _transControlTypeEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก: return -1;
                    case _transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด: return 1;
                    case _transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก: return -1;
                    case _transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด: return -1;
                    case _transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก: return 1;
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก: return -1;
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก: return -1;
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก: return -1;
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก: return 1;
                    case _transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น: return 1;
                    case _transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น: return -1;
                    case _transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น: return 1;
                    case _transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก: return -1;
                    case _transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น_ยกเลิก: return 1;
                    case _transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก: return -1;
                    case _transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า: return 1;
                    case _transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด: return -1;
                    case _transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้: return 1;
                    case _transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้: return 1;
                    case _transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้: return -1;
                    // SO Trans
                    case _transControlTypeEnum.SoEstimate: return 1;
                    case _transControlTypeEnum.SoInquiry: return 1;
                    case _transControlTypeEnum.ขาย_ขายสินค้าและบริการ: return -1;
                    case _transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน: return -1;
                    case _transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก: return -1;
                    case _transControlTypeEnum.ขาย_เสนอราคา: return 1;
                    case _transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก: return -1;
                    case _transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ: return 1;
                    case _transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า: return 1;
                    case _transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ: return 1;
                    case _transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก: return -1;
                    case _transControlTypeEnum.ขาย_สั่งขาย: return 1;
                    case _transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ: return 1;
                    case _transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก: return -1;
                    case _transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้: return 1;
                    case _transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก: return -1;
                    case _transControlTypeEnum.ขาย_เพิ่มหนี้: return -1;
                    case _transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก: return 1;
                    case _transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น: return 1;
                    case _transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น: return -1;
                    case _transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น: return 1;
                    case _transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น_ยกเลิก: return -1;
                    case _transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น_ยกเลิก: return 1;
                    case _transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น_ยกเลิก: return -1;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น: return 1;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้: return 1;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้: return -1;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น: return 1;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้: return 1;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้: return -1;

                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก: return -1;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก: return -1;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก: return 1;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ยกเลิก: return -1;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก: return -1;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก: return 1;

                    case _transControlTypeEnum.ขาย_รับเงินล่วงหน้า: return 1;
                    case _transControlTypeEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก: return -1;
                    case _transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน: return 1;
                    case _transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน_ยกเลิก: return -1;
                    case _transControlTypeEnum.ขาย_รับเงินมัดจำ: return 1;
                    case _transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก: return -1;
                    case _transControlTypeEnum.ขาย_เงินมัดจำ_คืน: return 1;
                    case _transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก: return -1;

                    // IC Trans
                    case _transControlTypeEnum.คลัง_ยอดคงเหลือสินค้ายกมา: return 1;
                    case _transControlTypeEnum.คลัง_ตรวจนับสินค้า: return 1;
                    case _transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ: return 1;
                    case _transControlTypeEnum.คลัง_จ่ายสินค้าออก_จากการขาย: return -1;
                    case _transControlTypeEnum.คลัง_ปรับปรุงเพิ่มลดสินค้า: return 1;

                    case _transControlTypeEnum.สินค้า_ตรวจนับสินค้า: return 1;
                    case _transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา: return 1;
                    case _transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ: return -1;
                    case _transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ_ยกเลิก: return 1;
                    case _transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก: return 1;
                    case _transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก_ยกเลิก: return -1;
                    case _transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป: return 1;
                    case _transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป_ยกเลิก: return -1;
                    case _transControlTypeEnum.StockCheck: return 1;
                    case _transControlTypeEnum.สินค้า_ปรับปรุงสต็อก: return 1;
                    case _transControlTypeEnum.สินค้า_ปรับปรุงสต็อก_ยกเลิก: return -1;
                    // toe
                    case _transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด: return -1;
                    case _transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด_ยกเลิก: return 1;
                    case _transControlTypeEnum.สินค้า_โอนเข้า: return 1;
                    case _transControlTypeEnum.สินค้า_โอนเข้า_ยกเลิก: return -1;
                    case _transControlTypeEnum.สินค้า_โอนออก: return -1;
                    case _transControlTypeEnum.สินค้า_โอนออก_ยกเลิก: return 1;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน: return 1;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก: return 1;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่: return 1;
                    //
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค: return -1;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค: return 1;
                    // Jead
                    case _transControlTypeEnum.StockCheckResult: return 1;
                    //
                    case _transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย: return -1;
                    case _transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย: return 1;

                    case _transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย_ยกเลิก: return 1;
                    case _transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย_ยกเลิก: return -1;

                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน: return 1;
                    //
                    case _transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน: return -1;
                    case _transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน: return 1;


                    case _transControlTypeEnum.เงินสดธนาคาร_โอนเงินเข้าธนาคาร: return -1;
                    case _transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร: return 1;
                    //
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก: return 1;
                    //
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน: return 1;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน: return -1;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก: return -1;
                    //
                    case _transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน: return 1;
                    case _transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก: return -1;
                    //
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา: return -1;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา: return 1;

                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก_ยกเลิก: return 1;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน_ยกเลิก: return -1;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน_ยกเลิก: return 1;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก_ยกเลิก: return 1;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่_ยกเลิก: return 1;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค_ยกเลิก: return 1;

                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก_ยกเลิก: return -1;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน_ยกเลิก: return -1;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน_ยกเลิก: return 1;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค_ยกเลิก: return -1;
                    case _transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน_ยกเลิก: return -1;
                    case _transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน_ยกเลิก: return 1;

                    case _transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร_ยกเลิก: return 1;
                    case _transControlTypeEnum.เงินสดธนาคาร_โอนเงินเข้าธนาคาร_ยกเลิก: return -1;
                    //
                    case _transControlTypeEnum.สินค้า_บันทึกการจัดส่ง: return -1;

                    case _transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ: return 0;
                    case _transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ_ยกเลิก: return 0;

                    case _transControlTypeEnum.สินค้า_ขอโอน: return 0;
                    case _transControlTypeEnum.สินค้า_ขอโอน_ยกเลิก: return 0;

                    case _transControlTypeEnum.คลัง_รับฝาก_ฝาก: return 1;
                    case _transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก: return 1;
                    case _transControlTypeEnum.คลัง_รับฝาก_เบิก: return -1;

                    default:
                        if (SystemInformation.ComputerName.ToLower().IndexOf("jead8") != -1 || SystemInformation.ComputerName.ToLower().IndexOf("toe-pc") != -1)
                        {
                            MessageBox.Show("_transStockCalcType case not found : " + icTransControlType.ToString());
                        }
                        break;
                }
                return 0;
            }

            public static int _apArTransCalcType(_transControlTypeEnum icTransControlType)
            {
                switch (icTransControlType)
                {
                    // 1=เพิ่ม,-1=ลด
                    // PO Trans
                    case _transControlTypeEnum.PoInvestigate: return 1;
                    case _transControlTypeEnum.ซื้อ_เสนอซื้อ: return 1;
                    case _transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก: return -1;
                    case _transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ: return 1;
                    case _transControlTypeEnum.ซื้อ_ใบสั่งซื้อ: return 1;
                    case _transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก: return -1;
                    case _transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ: return 1;
                    case _transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ: return 1;
                    case _transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้: return 1;
                    case _transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า: return 1;
                    case _transControlTypeEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก: return -1;
                    case _transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด: return 1;
                    case _transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้: return 1;
                    case _transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด: return -1;
                    case _transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก: return -1;
                    case _transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด: return -1;
                    case _transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้: return -1;
                    case _transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก: return 1;
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก: return -1;
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก: return -1;
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก: return -1;
                    case _transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก: return 1;
                    case _transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น: return 1;
                    case _transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น: return -1;
                    case _transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น: return 1;
                    case _transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก: return -1;
                    case _transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น_ยกเลิก: return 1;
                    case _transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก: return -1;
                    // SO Trans
                    case _transControlTypeEnum.SoEstimate: return 1;
                    case _transControlTypeEnum.SoInquiry: return 1;
                    case _transControlTypeEnum.ขาย_ขายสินค้าและบริการ: return -1;
                    case _transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก: return -1;
                    case _transControlTypeEnum.ขาย_เสนอราคา: return 1;
                    case _transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก: return -1;
                    case _transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ: return 1;
                    case _transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า: return 1;
                    case _transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ: return 1;
                    case _transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก: return -1;
                    case _transControlTypeEnum.ขาย_สั่งขาย: return 1;
                    case _transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ: return 1;
                    case _transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก: return -1;
                    case _transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้: return -1;
                    case _transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก: return -1;
                    case _transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น: return 1;
                    case _transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น: return -1;
                    case _transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น: return 1;
                    case _transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น_ยกเลิก: return -1;
                    case _transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น_ยกเลิก: return 1;
                    case _transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น_ยกเลิก: return -1;
                    //
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น: return 1;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้: return 1;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้: return -1;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น: return 1;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้: return 1;
                    case _transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้: return -1;

                    case _transControlTypeEnum.ขาย_รับเงินล่วงหน้า: return 1;
                    case _transControlTypeEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก: return -1;
                    case _transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน: return 1;
                    case _transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน_ยกเลิก: return -1;

                    case _transControlTypeEnum.ขาย_รับเงินมัดจำ: return 1;
                    case _transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก: return -1;
                    case _transControlTypeEnum.ขาย_เงินมัดจำ_คืน: return 1;
                    case _transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก: return -1;

                    case _transControlTypeEnum.ขาย_เพิ่มหนี้: return -1;
                    case _transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก: return 1;
                    // IC Trans
                    case _transControlTypeEnum.คลัง_ยอดคงเหลือสินค้ายกมา: return 1;
                    case _transControlTypeEnum.คลัง_ตรวจนับสินค้า: return 1;
                    case _transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ: return 1;
                    case _transControlTypeEnum.คลัง_จ่ายสินค้าออก_จากการขาย: return -1;
                    case _transControlTypeEnum.คลัง_ปรับปรุงเพิ่มลดสินค้า: return 1;
                    case _transControlTypeEnum.คลัง_รับฝาก_เบิก: return -1;
                    case _transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก: return 1;

                    case _transControlTypeEnum.สินค้า_ตรวจนับสินค้า: return 1;
                    case _transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา: return 1;
                    case _transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ: return -1;
                    case _transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ_ยกเลิก: return 1;
                    case _transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก: return 1;
                    case _transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก_ยกเลิก: return -1;
                    case _transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป: return 1;
                    case _transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป_ยกเลิก: return -1;
                    case _transControlTypeEnum.StockCheck: return 1;
                    case _transControlTypeEnum.สินค้า_ปรับปรุงสต็อก: return 1;
                    case _transControlTypeEnum.สินค้า_ปรับปรุงสต็อก_ยกเลิก: return -1;

                    case _transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด: return -1; // toe
                    case _transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด_ยกเลิก: return 1;

                    case _transControlTypeEnum.สินค้า_โอนเข้า: return 1;
                    case _transControlTypeEnum.สินค้า_โอนเข้า_ยกเลิก: return -1;
                    case _transControlTypeEnum.สินค้า_โอนออก: return -1;
                    case _transControlTypeEnum.สินค้า_โอนออก_ยกเลิก: return 1;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน: return 1;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก: return 1;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่: return 1;
                    // Jead
                    case _transControlTypeEnum.StockCheckResult: return 1;
                    //
                    case _transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย: return -1;
                    case _transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย: return 1;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน: return 1;
                    //
                    case _transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน: return -1;
                    case _transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน: return 1;
                    //
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก: return 1;
                    //
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน: return 1;
                    case _transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก: return -1;
                    //
                    case _transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน: return 1;
                    case _transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก: return -1;
                    //
                    default:
                        if (SystemInformation.ComputerName.ToLower().IndexOf("jead8") != -1 || SystemInformation.ComputerName.ToLower().IndexOf("toe-pc") != -1)
                        {
                            MessageBox.Show("_apArTransCalcType case not found : " + icTransControlType.ToString());
                        }
                        break;
                }
                return 0;
            }
        }

        public enum _transControlTypeEnum
        {
            ว่าง,
            เงินสดธนาคาร_บัตรเครดิต_ผ่าน,
            เงินสดธนาคาร_บัตรเครดิต_ยกเลิก,
            เงินสดธนาคาร_เช็คจ่าย_ยกมา,
            เงินสดธนาคาร_เช็คจ่าย_ยกมา_ยกเลิก,
            เงินสดธนาคาร_เช็คจ่าย_ผ่าน,
            เงินสดธนาคาร_เช็คจ่าย_ผ่าน_ยกเลิก,
            เงินสดธนาคาร_เช็คจ่าย_ยกเลิก,
            เงินสดธนาคาร_เช็คจ่าย_ยกเลิก_ยกเลิก,
            เงินสดธนาคาร_เช็คจ่าย_คืน,
            เงินสดธนาคาร_เช็คจ่าย_คืน_ยกเลิก,
            เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค,
            เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค_ยกเลิก,
            เงินสดธนาคาร_เช็ครับ_ยกมา,
            เงินสดธนาคาร_เช็ครับ_ยกมา_ยกเลิก,
            เงินสดธนาคาร_เช็ครับ_ฝาก,
            เงินสดธนาคาร_เช็ครับ_ฝาก_ยกเลิก,
            เงินสดธนาคาร_เช็ครับ_ผ่าน,
            เงินสดธนาคาร_เช็ครับ_ผ่าน_ยกเลิก,
            เงินสดธนาคาร_เช็ครับ_คืน,
            เงินสดธนาคาร_เช็ครับ_คืน_ยกเลิก,
            เงินสดธนาคาร_เช็ครับ_เข้าใหม่,
            เงินสดธนาคาร_เช็ครับ_เข้าใหม่_ยกเลิก,
            เงินสดธนาคาร_เช็ครับ_ยกเลิก,
            เงินสดธนาคาร_เช็ครับ_ยกเลิก_ยกเลิก,
            เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค,
            เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค_ยกเลิก,
            /// <summary>Trans Flag = 401</summary>
            เงินสดธนาคาร_ฝากเงิน,
            เงินสดธนาคาร_ฝากเงิน_ยกเลิก,
            เงินสดธนาคาร_ถอนเงิน,
            เงินสดธนาคาร_ถอนเงิน_ยกเลิก,
            เงินสดธนาคาร_โอนเงินเข้าธนาคาร,
            เงินสดธนาคาร_โอนเงินออกธนาคาร,
            เงินสดธนาคาร_โอนเงินเข้าธนาคาร_ยกเลิก,
            เงินสดธนาคาร_โอนเงินออกธนาคาร_ยกเลิก,
            เงินสดย่อย_เบิกเงินสดย่อย,
            เงินสดย่อย_เบิกเงินสดย่อย_ยกเลิก,
            /// <summary>Trans Flag = 301</summary>
            เงินสดย่อย_รับคืนเงินสดย่อย,
            เงินสดย่อย_รับคืนเงินสดย่อย_ยกเลิก,
            เงินสดธนาคาร_รายได้อื่น,
            เงินสดธนาคาร_รายได้อื่น_ยกเลิก,
            เงินสดธนาคาร_รายได้อื่น_ลดหนี้,
            เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก,
            เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้,
            เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก,

            เงินสดธนาคาร_ขอรายจ่ายอื่น,
            เงินสดธนาคาร_ขอรายจ่ายอื่น_อนุมัติ,
            /// <summary>Trans Flag = 260</summary>
            เงินสดธนาคาร_รายจ่ายอื่น,
            เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก,
            เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้,
            เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก,
            เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้,
            เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก,
            เจ้าหนี้_ตั้งหนี้ยกมา,
            เจ้าหนี้_เพิ่มหนี้ยกมา,
            เจ้าหนี้_ลดหนี้ยกมา,
            /// <summary>Trans Flag = 87</summary>
            เจ้าหนี้_ตั้งหนี้อื่น,
            เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก,
            เจ้าหนี้_เพิ่มหนี้อื่น,
            เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก,
            เจ้าหนี้_ลดหนี้อื่น,
            เจ้าหนี้_ลดหนี้อื่น_ยกเลิก,
            เจ้าหนี้_ใบรับวางบิล,
            เจ้าหนี้_ใบรับวางบิล_ยกเลิก,
            เจ้าหนี้_เตรียมจ่าย,
            เจ้าหนี้_เตรียมจ่าย_ยกเลิก,
            เจ้าหนี้_เตรียมจ่าย_อนุมัติ,
            เจ้าหนี้_จ่ายชำระหนี้,
            เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก,
            เจ้าหนี้_ตัดหนี้สูญ,
            เจ้าหนี้_ตัดหนี้สูญ_ยกเลิก,
            /// <summary>Trans Flag = 93</summary>
            ลูกหนี้_ตั้งหนี้ยกมา,
            ลูกหนี้_เพิ่มหนี้ยกมา,
            ลูกหนี้_ลดหนี้ยกมา,
            ลูกหนี้_ตั้งหนี้อื่น,
            ลูกหนี้_ตั้งหนี้อื่น_ยกเลิก,
            ลูกหนี้_เพิ่มหนี้อื่น,
            ลูกหนี้_เพิ่มหนี้อื่น_ยกเลิก,
            ลูกหนี้_ลดหนี้อื่น,
            ลูกหนี้_ลดหนี้อื่น_ยกเลิก,
            ลูกหนี้_ใบวางบิล,
            ลูกหนี้_ใบวางบิล_ยกเลิก,
            ลูกหนี้_ใบเสร็จชั่วคราว,
            ลูกหนี้_ใบเสร็จชั่วคราว_ยกเลิก,
            ลูกหนี้_รับชำระหนี้,
            ลูกหนี้_รับชำระหนี้_ยกเลิก,
            ลูกหนี้_ตัดหนี้สูญ,
            ลูกหนี้_ตัดหนี้สูญ_ยกเลิก,
            ลูกหนี้_แต้มยกมา,
            ลูกหนี้_ลดแต้ม,
            แสดงราคาตามสูตรสี,
            สินค้า_สูตรสี,
            สินค้า_สินค้าจัดชุด,
            /// <summary>Investigate : สืบราคา </summary> 
            PoInvestigate,
            /// <summary>Trans Flag = 2</summary>
            ซื้อ_เสนอซื้อ,
            ซื้อ_เสนอซื้อ_ยกเลิก,
            ซื้อ_เสนอซื้อ_อนุมัติ,
            /// <summary>Trans Flag = 6</summary>
            ซื้อ_ใบสั่งซื้อ,
            ซื้อ_ใบสั่งซื้อ_ยกเลิก,
            /// <summary>Trans Flag = 8</summary>
            ซื้อ_ใบสั่งซื้อ_อนุมัติ,
            //<summary>po_report_requisition_purchase : สถานะใบเสนอซื้อ</summary>ซื้อ_ใบเสนอซื้อ_สถานะ, //MOO 18/07/2553
            /// <summary>Trans Flag = 10</summary>
            ซื้อ_จ่ายเงินล่วงหน้า,
            ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก,
            ซื้อ_จ่ายเงินล่วงหน้า_รับคืน,
            ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก,
            /// <summary>Trans Flag = 11</summary>
            ซื้อ_จ่ายเงินมัดจำ,
            ซื้อ_จ่ายเงินมัดจำ_รับคืน,
            ซื้อ_จ่ายเงินมัดจำ_ยกเลิก,
            ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก,
            ซื้อ_ซื้อสินค้าและค่าบริการ,
            ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก,
            ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด,
            ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก,
            /// <summary>Trans Flag = 16</summary>
            ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด,
            ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก,
            /// <summary>Trans Flag=  310</summary>
            ซื้อ_พาเชียล_รับสินค้า,
            ซื้อ_พาเชียล_รับสินค้า_ยกเลิก,
            ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด,
            ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด_ยกเลิก,
            /// <summary>
            /// Trans Flag = 315
            /// </summary>
            ซื้อ_พาเชียล_ตั้งหนี้,
            ซื้อ_พาเชียล_ตั้งหนี้_ยกเลิก,
            ซื้อ_พาเชียล_ลดหนี้,
            ซื้อ_พาเชียล_ลดหนี้_ยกเลิก,
            ซื้อ_พาเชียล_เพิ่มหนี้,
            ซื้อ_พาเชียล_เพิ่มหนี้_ยกเลิก,
            /// <summary>SoInquiry : ความต้องการสินค้าของลูกค้า</summary>
            SoInquiry,
            /// <summary>SoInquiryCancel : ยกเลิกความต้องการสินค้าของลูกค้า</summary>
            SoInquiryCancel,
            ///<summary>SoEstimate : กำหนดราคาขายสินค้า</summary>
            SoEstimate,
            ///<summary>SoEstimateCancel : ยกเลิกราคาขายสินค้า</summary>
            SoEstimateCancel,
            ขาย_เสนอราคา,
            ขาย_เสนอราคา_อนุมัติ,
            ขาย_เสนอราคา_ยกเลิก,
            ขาย_สั่งจองและสั่งซื้อสินค้า,
            ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ,
            /// <summary>Trans Flag = 39</summary>
            ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก,
            /// <summary>Trans Flag = 36</summary>
            ขาย_สั่งขาย,
            ขาย_สั่งขาย_อนุมัติ,
            ขาย_สั่งขาย_ยกเลิก,
            /// <summary>Trans Flag = 40</summary>
            ขาย_รับเงินล่วงหน้า,
            ขาย_รับเงินล่วงหน้า_ยกเลิก,
            ขาย_เงินล่วงหน้า_คืน,
            ขาย_เงินล่วงหน้า_คืน_ยกเลิก,
            ขาย_รับเงินมัดจำ,
            ขาย_รับเงินมัดจำ_ยกเลิก,
            ขาย_เงินมัดจำ_คืน,
            ขาย_เงินมัดจำ_คืน_ยกเลิก,
            ///<summary>SoInvoice : ขายสินค้า/บริการ</summary>
            ขาย_ขายสินค้าและบริการ,
            /// <summary>รายการใบกำกับภาษีอย่างเต็มออกแทน POS</summary>
            ขาย_ใบกำกับภาษีอย่างเต็มออกแทน,
            ///<summary>SoInvoiceCancel : ยกเลิกขายสินค้า/บริการ</summary>
            ขาย_ขายสินค้าและบริการ_ยกเลิก,
            ///<summary>SoInvoiceAdd : ขายเพิ่ม/เพิ่มหนี้</summary>
            ขาย_เพิ่มหนี้,
            ///<summary>SoInvoiceAddCancel : ยกเลิกขายเพิ่ม/เพิ่มหนี้</summary>
            ขาย_เพิ่มหนี้_ยกเลิก,
            ///<summary>SoInvoiceReturn : รับคืน/ลดหนี้</summary>
            ขาย_รับคืนสินค้าจากการขายและลดหนี้,
            ///<summary>SoInvoiceReturnCancel : ยกเลิกรับคืน/ลดหนี้</summary>
            ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก,
            ///<summary>SoDeliveryOrder : สั่งจ่าย/ส่งสินค้า</summary>
            SoDeliveryOrder,
            ///<summary>SoDeliveryOrderCancel : ยกเลิกสั่งจ่าย/ส่งสินค้า</summary>
            SoDeliveryOrderCancel,
            คลัง_ยอดคงเหลือสินค้ายกมา,
            คลัง_รับสินค้าเข้า_จากการซื้อ,
            คลัง_จ่ายสินค้าออก_จากการขาย,
            คลัง_ปรับปรุงเพิ่มลดสินค้า,
            คลัง_ตรวจนับสินค้า,
            สินค้า_ตรวจนับสินค้า,
            สินค้่า_ตรวจสอบสินค้า_serial,
            /// <summary>สินค้า_ยอดคงเหลือสินค้ายกมา Flag = 54</summary>
            สินค้า_ยอดคงเหลือสินค้ายกมา,
            สินค้า_ยอดคงเหลือสินค้ายกมา_ยกเลิก,
            ///<summary>StockRequest : เบิกสินค้า</summary>
            สินค้า_ขอเบิกสินค้าวัตถุดิบ,
            สินค้า_ขอเบิกสินค้าวัตถุดิบ_ยกเลิก,
            /// <summary>Trans Flag = 56</summary>
            สินค้า_เบิกสินค้าวัตถุดิบ,
            สินค้า_เบิกสินค้าวัตถุดิบ_ยกเลิก,
            /// <summary>Trans Flag = 58</summary>
            สินค้า_รับคืนสินค้าจากการเบิก,
            สินค้า_รับคืนสินค้าจากการเบิก_ยกเลิก,
            สินค้า_รับสินค้าสำเร็จรูป,
            สินค้า_รับสินค้าสำเร็จรูป_ยกเลิก,
            ///<summary>StockInspect : เพื่อการตรวจนับ</summary>
            StockInspect,
            ///<summary>StockInspectCancel : ยกเลิกเพื่อการตรวจนับ</summary>
            StockInspectCancel,
            /// <summary>StockCheck : ตรวจนับสินค้า</summary>
            StockCheck,
            /// <summary>StockCheckResult : ผลการตรวจนับสินค้า</summary>
            StockCheckResult,
            /// <summary>StockCheckCancel : ยกเลิกตรวจนับสินค้า</summary>
            StockCheckCancel,
            /// <summary>Trans Flag = 66</summary>
            สินค้า_ปรับปรุงสต็อก,
            สินค้า_ปรับปรุงสต็อก_ยกเลิก,
            /// <summary>Color Store And Account</summary>
            สินค้า_ปรับปรุงสต๊อก_ขาด, // toe เพิ่มเอง
            สินค้า_ปรับปรุงสต๊อก_ขาด_ยกเลิก,
            ///<summary>StockTransferIn : โอนเข้า Flag 70</summary>
            สินค้า_โอนเข้า,
            ///<summary>StockTransferInCancel : ยกเลิกโอนเข้า</summary>
            สินค้า_โอนเข้า_ยกเลิก,
            สินค้า_โอนออก,
            สินค้า_โอนออก_ยกเลิก,
            สินค้า_ขอโอน,
            สินค้า_ขอโอน_ยกเลิก,
            ///<summary>StockTransferRequest : ขอโอนสินค้า</summary>
            StockTransferRequest,
            สินค้า_เงื่อนไขแถมตอนซื้อ,
            สินค้า_สินค้าแถมตอนซื้อ,
            POS_รับเงินทอน,
            สินค้า_บันทึกการจัดส่ง,
            IMEX_Bill_Collector,
            บัญชี_ข้อมูลรายวัน,
            GL_ภาษีหักณที่จ่าย,
            GL_ภาษีถูกหักณที่จ่าย,

            // singha
            คลัง_รับฝาก_ฝาก,
            คลัง_รับฝาก_เบิก,
            คลัง_รับฝาก_รับคืนจากเบิก,

            Shipment,
            สินทรัพย์_โอนค่าเสื่อม,
            บัญชี_ประมวลผลสิ้นงวด,
            บัญชี_ประมวลผลสิ้นปี

        }

        public class _icDetailExtraClass
        {
            public string _itemcode;
            public string _docno;
            public string _pattern;
            public string _color;
            public string _size;
        }
        /// <summary>
        /// ข้อมูลบริษัท
        /// </summary>
        public class _companyProfileType
        {
            public int _printerMargin = 0;
            /// <summary>
            /// 0 = ไม่มีสาขา,1=มีสาขา
            /// </summary>
            public int _branchStatus = 1;
            /// <summary>
            /// 0=พุทธ,1=คริส
            /// </summary>
            public int _year_type = 0;
            /// <summary>
            /// ขาย 0=แยกนอก,1=รวมใน
            /// </summary>
            public int _vat_type = 0;
            /// <summary>
            /// ซื้อ 0=แยกนอก,1=รวมใน
            /// </summary>
            public int _vat_type_1 = 0;
            /// <summary>
            /// อัตราภาษี
            /// </summary>
            public decimal _vat_rate = 0;
            /// <summary>
            /// ทศนิยมของ GL
            /// </summary>
            public int _gl_no_decimal = 2;
            /// <summary>
            /// 0=ไม่ใช้,1=ใช้ (กลุ่มเอกสาร)
            /// </summary>
            public int _use_doc_group = 0;
            /// <summary>
            /// 0=ไม่ใช้,1=ใช้ (Log)
            /// </summary>
            public int _use_log_book = 0;
            /// <summary>
            /// 0=ไม่ใช้,1=ใช้ (พิมพ์เอกสารหลังจาก Save)
            /// </summary>
            public int _use_print_slip = 0;
            /// <summary>
            /// 0=ไม่ใช้,1=ใช้ (แผนก)
            /// </summary>
            public int _use_department = 0;
            /// <summary>
            /// 0=ไม่ใช้,1=ใช้ (JOB)
            /// </summary>
            public int _use_job = 0;
            /// <summary>
            /// 0=ไม่ใช้,1=ใช้ (จัดสรรค์)
            /// </summary>
            public int _use_allocate = 0;
            /// <summary>
            /// 0=ไม่ใช้,1=ใช้ (หน่วยงาน)
            /// </summary>
            public int _use_unit = 0;
            /// <summary>
            /// 0=ไม่ใช้,1=ใช้ (โครงการ)
            /// </summary>
            public int _use_project = 0;
            /// <summary>
            /// 0=ไม่ใช้,1=ใช้ (งานระหว่างทำ)
            /// </summary>
            public int _use_work_in_process = 0;
            /// <summary>
            /// 0=ไม่ใช้,1=ใช้ (วันหมดอายุ)
            /// </summary>
            public Boolean _use_expire = false;
            /// <summary>
            /// ทศนิยม จำนวนสินค้า
            /// </summary>
            public int _item_qty_decimal = 2;
            /// <summary>
            /// 0=ไม่ใช้,1=ใช้ (ทศนิยมราคาสินค้า)
            /// </summary>
            public int _item_price_decimal = 2;
            /// <summary>
            /// 0=ไม่ใช้,1=ใช้ (ทศนิยมมูลค่าสินค้า)
            /// </summary>
            public int _item_amount_decimal = 2;
            /// <summary>
            /// อัตราภาษีหัก ณ. ที่จ่าย
            /// </summary>
            public decimal _wht_rate = 0;
            /// <summary>
            /// เตือนเมื่อไม่พบราคาขาย
            /// </summary>
            public Boolean _warning_price_1 = false;
            /// <summary>
            /// เตือนราคาขายกับประเภทภาษี
            /// </summary>
            public Boolean _warning_price_2 = false;
            /// <summary>
            /// ใช้ระบบรหัสผ่านแก้ไขราคา
            /// </summary>
            public Boolean _warning_price_3 = false;
            /// <summary>
            /// กำหนดให้ส่งคืนมี,ลดหนี้,เพิ่มหนี้,อ้างเอกสารใบเดียว (ด้านซื้อหรือเจ้าหนี้)
            /// </summary>
            public Boolean _purchase_credit_note_type = false;
            /// <summary>
            /// 0=อน GL แบบแยกรายตัว,1=โอน GL แบบแยกรายตัว+คอมปาว
            /// </summary>
            public int _gl_trans_type = 0;
            /// <summary>
            /// เปิดช่อง Price (หน้ารายวัน)
            /// </summary>
            public Boolean _column_price_enable = true;
            /// <summary>
            /// ป้อนยอดเอง
            /// </summary>
            public Boolean _manual_total_enable = true;
            /// <summary>
            /// ใชระบบ Barcode ในรายวัน
            /// </summary>
            public Boolean _use_barcode = false;
            /// <summary>
            /// Url สำหรับ Sync ข้อมูล
            /// </summary>
            public string _sync_wbservice_url = "";
            /// <summary>
            /// Sync สินค้าหรือไม่
            /// </summary>
            public Boolean _sync_product = false;
            /// <summary>
            /// เตือนเมื่อสินค้าติดลบ
            /// </summary>
            public Boolean _balance_control = false;
            /// <summary>
            /// ห้ามขายเกินค้างส่ง
            /// </summary>
            public Boolean _accrued_control = false;
            /// <summary>
            /// เปลี่ยน Pack อัตโนมัติ (หน้าขาย)
            /// </summary>
            public Boolean _sale_auto_packing = false;
            /// <summary>
            /// ใช้ระบบต้นทุนแฝง
            /// </summary>
            public Boolean _hidden_cost_1 = false;
            /// <summary>
            /// ใช้ระบบรายได้แฝง
            /// </summary>
            public Boolean _hidden_income_1 = false;
            /// <summary>
            /// เตือนเมื่อมีการขายสินค้าต่ำกว่าทุน
            /// </summary>
            public Boolean _warning_low_cost = false;
            /// <summary>
            /// ใบอนุมัติซื้ออ้างอิงได้ครั้งเดียว
            /// </summary>
            public Boolean _pr_approve_lock = false;
            /// <summary>
            /// ห้ามวางบิลขายซ้ำ
            /// </summary>
            public Boolean _ar_bill_inform = false;
            /// <summary>
            /// ตรวจนับสินค้าแบบรวมจำนวน
            /// </summary>
            public Boolean _count_stock_sum = false;
            /// <summary>
            /// ต้นทุนแยกคลัง
            /// </summary>
            public Boolean _cost_by_warehouse = false;
            /// <summary>
            /// สามารถแก้ไขใบเสนอราคาได้ตลอด
            /// </summary>
            public Boolean _sale_order_edit = false;
            /// <summary>
            /// 0=ห้ามติดลบทั้งหมด,1=ห้ามติดลบตามคลัง,2=ห้ามติดลบตามที่เก็บ
            /// </summary>
            public int _balance_control_type = 0;
            /// <summary>
            /// ห้ามขายสินค้าติดลบ
            /// </summary>
            public Boolean _ic_stock_control = false;
            /// <summary>
            /// เปลี่ยนเวลาเมื่อเพิ่มข้อมูลรายวัน
            /// </summary>
            public Boolean _auto_insert_time = false;
            /// <summary>
            /// ใช้คลังที่เก็บตามสิทธิพนักงาน
            /// </summary>
            public Boolean _perm_wh_shelf = false;
            /// <summary>
            /// แสดงราคาขายในระบบตรวจนับ
            /// </summary>
            public Boolean _count_stock_display_saleprice = false;
            /// <summary>
            /// เตือนเกินวงเงินเครดิต
            /// </summary>
            public Boolean _warning_credit_money = false;
            /// <summary>
            /// ดึงราคาขายล่าสุด (0=ไม่ทำงาน,1=ราคาขายล่าสุด,2=ราคาขายเฉลี่ย)
            /// </summary>
            public int _get_last_price_type = 0;
            public Boolean _activeSync = false;
            public string _activeSyncProvider = "";
            public string _activeSyncServer = "";
            public string _activeSyncDatabase = "";
            public string _activeSyncAccessCode = "";
            public string _activeSyncBranchCode = "";

            /// <summary>
            /// ห้ามโอนสินค้าติดลบ
            /// </summary>
            public Boolean _transfer_stock_control = false;
            /// <summary>
            /// ใช้รูปภาพจากบาร์โค้ดสินค้า
            /// </summary>
            public Boolean _barcode_picture = false;
            /// <summary>
            /// การคำณวนส่วนลด (0= vat ก่อน, 1 =ลดก่อน)
            /// </summary>
            public int _discount_type = 0;
            /// <summary>
            /// ใช้ระบบสะสมแต้มจากส่วนกลาง
            /// </summary>
            public Boolean _use_point_center = false;
            /// <summary>
            /// เอกสารขาย run อัตโนมัติ กรณีเลขที่เอกสารซ้ำ
            /// </summary>
            public Boolean _auto_run_docno_sale = false;
            /// <summary>
            /// กำหนดให้ใบกำกับภาษี ตรงกับเลขที่เอกสาร
            /// </summary>
            public Boolean _doc_sale_tax_number_fixed = false;
            /// <summary>
            /// promotion_fixed_unitcode
            /// </summary>
            public Boolean _promotion_fixed_unitcode = false;
            /// <summary>
            /// ห้ามเบิกสินค้าติดลบ
            /// </summary>
            public Boolean _issue_stock_control = false;
            /// <summary>
            /// ห้ามขายสินค้าต่ำกว่าราคากลาง
            /// </summary>
            public Boolean _ic_price_formula_control = false;
            /// <summary>
            /// ปิดระบบคำนวณต้นทุนสินค้า
            /// </summary>
            public Boolean _disable_ic_cost_process = false;
            /// <summary>
            /// ใช้ระบบ Running เอกสารเงินมัดจำจาก POS
            /// </summary>
            public Boolean _deposit_format_from_pos = false;
            /// <summary>
            /// ระบบราคาคำนวณส่วนลดให้เรียบร้อย
            /// </summary>
            public Boolean _sale_real_price = false;
            /// <summary>
            /// รหัสสาขา
            /// </summary>
            public string _branch_code = "";
            /// <summary>
            /// ชื่อสาขา
            /// </summary>
            public string _branch_name = "";
            /// <summary>
            /// อนุญาติให้บันทึก Serial No ซ้ำ
            /// </summary>
            public Boolean _use_serial_no_duplicate = false;
            /// <summary>
            /// ใช้ระบบรอบการขาย
            /// </summary>
            public Boolean _use_sale_shift = false;
            /// <summary>
            /// รหัสรอบการขาย
            /// </summary>
            public string _sale_shift_id = "";
            /// <summary>
            /// ใช้ระบบเก็บประวัติการใชังาน
            /// </summary>
            public Boolean _save_logs = false;
            /// <summary>
            /// เปิดระบบอ้างอิงจากใบเสนอซื้อ
            /// </summary>
            public Boolean _use_reference_pr = false;
            /// <summary>
            /// ประเภทการขาย(เริ่มต้น)
            /// </summary>
            public int _default_sale_type = 0;
            /// <summary>
            /// ใช้ระบบดึงราคาซื้อ
            /// </summary>
            public Boolean _get_purchase_price = false;
            /// <summary>
            /// อนุญาติให้เปลี่ยนสาขาได้
            /// </summary>
            public Boolean _change_branch_code = false;
            /// <summary>
            /// ใช้ระบบ Checker อาหาร
            /// </summary>
            public Boolean _use_order_checker = false;
            /// <summary>
            /// หมายเลขเครื่องประมวลผล
            /// </summary>
            public string _process_serial_device = "";
            /// <summary>
            /// ห้ามรับวางบิลซ้ำ
            /// </summary>
            public Boolean _ap_bill_inform = false;
            /// <summary>
            /// ใช้ระบบกำหนดราคาสินค้าชุด
            /// </summary>
            public Boolean _fix_item_set_price = false;
            /// <summary>
            /// ห้ามขายต่ำกว่าทุน
            /// </summary>
            public Boolean _lock_low_cost = false;
            /// <summary>
            /// เพิ่มสินค้าสูตรสี
            /// </summary>
            public Boolean _add_item_color = false;
            /// <summary>
            /// ชื่อฐานข้อมูลสำหรับแลกเปลี่ยนช้อมูล
            /// </summary>
            public string _sync_database_name = "";
            /// <summary>
            /// รหัสผุ้ให้บริการสำหรับการแลกเปลี่ยนข้อมูล
            /// </summary>
            public String _sync_provider_code = "";
            /// <summary>
            /// ห้ามขายเกินวงเงินเครติต
            /// </summary>
            public Boolean _lock_credit_money = false;
            /// <summary>
            /// ใช้ระบบรหัสผ่านอนุมัติวงเงินลูกค้า
            /// </summary>
            public Boolean _password_ar_credit = false;
            /// <summary>
            /// ใช้ระบบตั้งรหัสลูกค้า
            /// </summary>
            public Boolean _manual_customer_code = false;
            /// <summary>
            /// แสดงเอกสารเฉพาะสาขา
            /// </summary>
            public Boolean _show_branch_doc_only = false;
            /// <summary>
            /// รหัสแบบฟอร์ม Voucher
            /// </summary>
            public String _voucher_form_code = "";
            /// <summary>
            /// จัดเก็บจำนวนลูกค้า
            /// </summary>
            public Boolean _count_customer_table = false;
            /// <summary>
            /// ค้นหาหมายเลข LOT อัตโนมัติ
            /// </summary>
            public Boolean _find_lot_auto = false;
            /// <summary>
            /// ใช้ระบบพิมพ์ใบสั่งจัด
            /// </summary>
            public Boolean _print_packing_pos_order = false;
            /// <summary>
            /// ห้ามแก้ไขวันที่และเลขที่เอกสาร
            /// </summary>
            public Boolean _disabled_edit_doc_no_doc_date = false;
            /// <summary>
            /// ไม่แสดงเอกสารที่วางบิลไปแล้ว
            /// </summary>
            public Boolean _filter_pay_bill = false;
            /// <summary>
            /// ห้ามแก้ไขรายการอาหาร
            /// </summary>
            public Boolean _disable_edit_order = false;
            /// <summary>
            /// พิมพ์รายละเอียดการส่งเงิน
            /// </summary>
            public Boolean _print_pos_settle_bill_detail = false;
            /// <summary>
            /// ปิดระบบคำณวนอัตโนมัติ
            /// </summary>
            public Boolean _disable_auto_stock_process = false;
            /// <summary>
            /// เปิดโต๊ะอัตโนมัติหลังคิดเงิน
            /// </summary>
            public Boolean _auto_open_table = false;
            /// <summary>
            /// ป้อนเลขที่ใบกำกับภาษีทุกครั้ง
            /// </summary>
            public Boolean _check_input_vat = false;
            /// <summary>
            /// ไม่ตรวจสอบยอดคงเหลือในระบบสั่งขาย
            /// </summary>
            public Boolean _sale_order_banalce_control = false;
            /// <summary>
            /// ไม่แสดงต้นทุนสินค้า
            /// </summary>
            public Boolean _disable_item_cost = false;
            /// <summary>
            /// เป็น User ที่ สามารถดูต้นทุนสินค้า
            /// </summary>
            public Boolean _is_User_show_item_cost = false;
            /// <summary>
            /// เข้าระบบสะสมแต้มทุกรายการ
            /// </summary>
            public Boolean _sum_point_all = false;
            /// <summary>
            /// สั่งสินค้าผ่าน Internet
            /// </summary>
            public Boolean _internetSync = false;
            /// <summary>
            /// ห้ามแก้ไขวันที่และเลขที่เอกสาร
            /// </summary>
            public Boolean _disable_edit_doc_no_doc_date_user = false;
            /// <summary>
            /// สั่งอาหารหลังเปิดโต๊ะทันที
            /// </summary>
            public Boolean _order_after_open_table = false;

            public Boolean _join_money_credit = false;
            public string _join_money_credit_list = "";

            public Boolean _check_lot_auto = false;

            /// <summary>
            /// ใช้ระบบรหัสผ่านแก้ไขส่วนลด
            /// </summary>
            public Boolean _warning_discount_1 = false;
            /// <summary>
            /// ห้ามขายสินค้าค้างจองทั้งระบบ
            /// </summary>
            public Boolean _stock_reserved_control = false;

            /// <summary>
            /// Report Font Name
            /// </summary>
            public string _reportFontName = "";
            /// <summary>
            /// Report Font Size
            /// </summary>
            public float _reportFontSize = 0f;

            /// <summary>
            /// Report Header Font Name 1
            /// </summary>
            public string _reportHeaderFontName_1 = "";
            /// <summary>
            /// Report Header Font Size 1
            /// </summary>
            public float _reportHeaderFontSize_1 = 0f;
            /// <summary>
            /// Report Header Font Name 2
            /// </summary>
            public string _reportHeaderFontName_2 = "";
            /// <summary>
            /// Report Header Font Size 2
            /// </summary>
            public float _reportHeaderFontSize_2 = 0f;
            /// <summary>
            /// ห้ามขายสินค้าค้างจองทั้งระบบ
            /// </summary>
            public Boolean _stock_reserved_control_location = false;
            /// <summary>
            /// ใช้บาร์โค๊ดจากเครื่องชั่ง
            /// </summary>
            public Boolean _digital_barcode_scale = false;
            /// <summary>
            /// คำนวณแต้มจากยอดชำระ
            /// </summary>
            public Boolean _calc_point_from_pay = false;
            /// <summary>
            /// พูดรายการอาหาร
            /// </summary>
            public Boolean _orders_speech = false;
            /// <summary>
            /// รูปแบบการพูด
            /// </summary>
            public string _orders_speech_format = "";
            /// <summary>
            /// ห้ามเปลี่ยนรหัสลูกค้า
            /// </summary>
            public Boolean _lock_change_customer = false;
            /// <summary>
            /// กำหนดเวลาเอกสารอัตโนมัติ
            /// </summary>
            public Boolean _real_doc_date_doc_time = false;
            /// <summary>
            /// ใช้ระบบจำพนักงานสั่งอาหาร
            /// </summary>
            public Boolean _save_user_order = false;
            /// <summary>
            /// การ Post บัญชีสินค้า
            /// </summary>
            public int _inventory_gl_post = 0;
            /// <summary>
            /// ใช้ระบบหลายสกุลเงิน (Multi-Currency)
            /// </summary>
            public Boolean _multi_currency = false;
            /// <summary>
            /// เตือนสินค้าถึงจุดสั่งซื้อ
            /// </summary>
            public Boolean _warning_reorder_point = false;
            /// <summary>
            /// เตือนลูกหนี้เกินชำระ
            /// </summary>
            public Boolean _warning_bill_overdue = false;
            /// <summary>
            /// เตือนสินค้าหมดอายุ
            /// </summary>
            public Boolean _warning_product_expire = false;
            /// <summary>
            /// สกุลเงินหลัก
            /// </summary>
            public string _home_currency = "";
            /// <summary>
            /// ปิดระบบตรวจสอบเวลาเครื่อง
            /// </summary>
            public Boolean _disable_sync_time = false;
            /// <summary>
            /// ประมวลผล GL ทันทีหลังบันทึกข้อมูล
            /// </summary>
            public Boolean _gl_process_realtime = false;
            /// <summary>
            /// ค้นหาเช็คฉบับเก่าไม่ตรวจสอบลูกหนี้/เจ้าหนี้
            /// </summary>
            public Boolean _search_cheque_other_cust_code = false;
            /// <summary>
            /// ตรวจสอบสถานะเครดิตลูกหนี้
            /// </summary>
            public Boolean _check_ar_status_credit = false;
            /// <summary>
            /// พิมพ์ใบเสร็จครั้งเดียว
            /// </summary>
            public Boolean _print_invoice_one_time = false;
            /// <summary>
            /// ตรวจสอบชื่อลูกหนี้ซ้ำ
            /// </summary>
            public Boolean _check_ar_duplicate_name = false;
            /// <summary>
            /// ใบกำกับภาษีขายตรงกับเอกสารขาย
            /// </summary>
            public Boolean _tax_from_invoice = false;
            /// <summary>
            /// เลขที่เอกสารให้เลือกจากประเภทเอกสารเท่านั้น
            /// </summary>
            public Boolean _running_doc_no_only = false;
            /// <summary>
            /// เตือนเมื่อไม่กำหนดสาขา
            /// </summary>
            public Boolean _warning_branch_input = false;
            /// <summary>
            /// ใช้ระบบขออนุมัติวงเงินเครดิต
            /// </summary>
            public Boolean _request_ar_credit = false;
            /// <summary>
            /// วิธีอนุมัติวงเงิน (0=ผ่านโปรแกรมอนุมัติ,1=ผ่านระบบ SMS,2=ผ่านระบบ SaleHub,3=ผ่านระบบ SMS และ SaleHub)
            /// </summary>
            public int _request_credit_type = 0;
            /// <summary>
            /// วงเงินเครดิตรวมเช็คคงค้าง
            /// </summary>
            public Boolean _ar_credit_chq_outstanding = false;
            /// <summary>
            /// หมายเลขโทรศัทพ์ขออนุมัติวงเงิน
            /// </summary>
            public string _phone_number_approve = "";
            /// <summary>
            /// ผู้อนุมัติ Sale Hub
            /// </summary>
            public string _sale_hub_approve = "";
            /// <summary>
            /// ใช้ระบบแสดงลูกค้าตามสาขา
            /// </summary>
            public Boolean _customer_by_branch = false;
            /// <summary>
            /// ใช้ระบบแสดงเมนูตามสิทธิ์การใช้งาน
            /// </summary>
            public Boolean _show_menu_by_permission = false;
            /// <summary>
            /// ใช้ระบบคำนวณราคาสินค้ากับส่วนลดก่อน
            /// </summary>
            public Boolean _calc_item_price_discount = false;
            /// <summary>
            /// เตือนไม่บันทึกแผนก
            /// </summary>
            public Boolean _warning_department = false;
            /// <summary>
            /// ไม่แสดงตารางราคาตามสูตร
            /// </summary>
            public Boolean _hidden_price_formula = false;

            /// <summary>
            /// ใช้ระบบหมดอายุใบเสนอราคา
            /// </summary>
            public Boolean _quotation_expire = false;
            /// <summary>
            /// อ้างอิงเอกสารสั่งซื้อที่อนุมัติแล้วเท่านั้น
            /// </summary>
            public Boolean _ref_po_approve = false;
            /// <summary>
            /// ใช้ระบบตรวจสอบวงเงินสั่งจอง/สั่งขาย
            /// </summary>
            public Boolean _sr_ss_credit_check = false;
            /// <summary>
            /// ใช้ระบบราคาจากส่วนกลาง
            /// </summary>
            public Boolean _use_price_center = false;
            /// <summary>
            /// URL Server
            /// </summary>
            public String _price_list_server = "";
            /// <summary>
            /// หมายเลขเครื่อง Process
            /// </summary>
            public String _price_list_serial_process = "";
            /// <summary>
            /// รหัสสาขา
            /// </summary>
            public String _price_list_branch = "";
            /// <summary>
            /// รหัสเข้าสู่ระบบ
            /// </summary>
            public String _price_list_key = "";
            /// <summary>
            /// Mobile Sale Server URL
            /// </summary>
            public String _mobile_sale_url = "";
            /// <summary>
            /// Mobile Sale Key
            /// </summary>
            public String _mobile_bypasskey = "";
            /// <summary>
            /// ปิดเตือนสำรองข้อมูล
            /// </summary>
            public Boolean _close_warning_backup = false;
            /// <summary>
            /// ใบสั่งขายอ้างอิงเสนอราคาเท่านั้น
            /// </summary>
            public Boolean _ss_ref_po_only = false;
            /// <summary>
            /// เตือนเลือกลูกค้าก่อนทำรายการ
            /// </summary>
            public Boolean _warning_input_customer = false;
            /// <summary>
            /// แจ้งเตือนยกเลิกเอกสาร
            /// </summary>
            public Boolean _arm_send_cancel_doc = false;
            /// <summary>
            /// ผู้รับ
            /// </summary>
            public String _arm_send_cancel_doc_to = "";
            /// <summary>
            /// แจ้งเตือนลดหนี้
            /// </summary>
            public Boolean _arm_send_cn = false;
            /// <summary>
            /// ผู้รับ
            /// </summary>
            public String _arm_send_cn_to = "";
            /// <summary>
            /// แจ้งเตือนแก้ไขลูกหนี้
            /// </summary>
            public Boolean _arm_send_ar_change = false;
            /// <summary>
            /// ผู้รับ
            /// </summary>
            public String _arm_send_ar_change_to = "";
            /// <summary>
            /// sync master url 
            /// </summary>
            public String _sync_master_url = "";
            /// <summary>
            /// warehouse on the way
            /// </summary>
            public String _warehouse_on_the_way = "";
            /// <summary>
            /// shelf on the way
            /// </summary>
            public String _shelf_on_the_way = "";
        }

        //
        //------------------------------START-----PAYRILL--------------------------------------------------------------
        //private Screen (ประกาศตัวแปลเพื่อเรียกใช้ตามหน้าจอ)
        public static string[] _money_adjust = { "no_adjust", "up_adjust", "down_adjust", "mix_adjust" };// การปัดเศษสตางค์  (หน้าจอ sml_payroll_company)
        public static string[] _salary_method = { "average", "be_true" };// วิธีคิดเงินเดือน (หน้าจอ sml_payroll_company)
        public static string[] _reserve_fund_type = { "upright", "change" };// ประเภทกองทุนเงินสำรอง (หน้าจอ sml_payroll_company)
        public static string[] _holiday = { "mon", "tue", "wed", "thu", "fri", "sat", "sun" };// วันหยุดบริษัท (หน้าจอ sml_payroll_company)
        public static string[] _employee_type = { "monthly", "weekly", "daily" };// ประเภทลูกจ้าง (หน้าจอ sml_payroll_employee)
        public static string[] _employee_status = { "active", "hold", "inactive" };// สถานะลูกจ้าง (หน้าจอ sml_payroll_employee)
        public static string[] _sex = { "male", "female" };// เพศ (หน้าจอ sml_payroll_employee)
        public static string[] _situation = { "single", "married", "widow" };// สถานภาพ (หน้าจอ sml_payroll_employee)
        public static string[] _spouse_work_status = { "spouse_work", "spouse_not_work" };// สถานะของคู่สมรส (หน้าจอ sml_payroll_employee)
        public static string[] _selected_month = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };// เลือกเดือน(หน้าจอ sml_payroll_calculate_wages)
        public static string[] _selected_year = { "2550", "2551" };// เลือกปี (หน้าจอ sml_payroll_calculate_wages)
        public static string[] _selected_money_type = { "เบี้ยขยัน", "โบนัส", "เงินได้อื่นๆ", "เงินเบิกล่วงหน้า", "เงินคํ้าประกัน", "เงินหักอื่นๆ" };// ประเภทเงิน (หน้าจอ sml_payroll_trans_list)
        public static string[] _selected_calculate_tax_type = { "six_month", "one_year" };// เลือกคำนวนภาษี (หน้าจอ sml_payroll_calculate_tax)
        public static string[] _situation_soldier = { "no_conscript", "conscript", "not_conscript" };// สถานภาพทางทหาร (หน้าจอ sml_payroll_employee)
        public static string[] _spouse_tax = { "totle_up", "separate" };// คู่สมรสยื่นภาษี (หน้าจอ sml_payroll_employee)
        public static string[] _employee_type_tax = { "employee_all", "monthly", "weekly", "daily" };// ประเภทลูกจ้าง (หน้าจอ sml_payroll_tax)        
        ////------------------------------------------------------------------------------------------------------        
        //// Master (หน้าจอหลัก)
        ////------------------------------------------------------------------------------------------------------
        public static string _screen_payroll_company_profile = "screen_payroll_company_profile"; //ค่าเริ่มต้นบริษัท
        public static string _screen_master_payroll_side_list = "screen_master_payroll_side_list"; //ฝ่าย
        public static string _screen_master_payroll_section_list = "screen_master_payroll_section_list"; //แผนก
        public static string _screen_master_payroll_work_title = "screen_master_payroll_work_title"; //ตำแหน่งงาน
        public static string _screen_master_payroll_bank_list = "screen_master_payroll_bank_list"; //ธนาคาร        
        public static string _screen_master_payroll_branch_bank = "screen_master_payroll_branch_bank"; //สาขาธนาคาร        
        ////------------------------------------------------------------------------------------------------------
        ////Screen (ค้าหาของหน้าจอแต่ละหน้าจอ)
        ////------------------------------------------------------------------------------------------------------
        public static string _search_screen_payroll_employee = "screen_payroll_employee"; // พนักงาน - ลูกจ้าง (หน้าจอ sml_payroll_employee)        
        public static string _search_screen_payroll_company_config = "screen_payroll_company_config"; // รายละเอียดบริษัท (หน้าจอ sml_payroll_company_profile)
        public static string _search_screen_payroll_short_of_work = "screen_payroll_short_of_work"; // การขาดงาน (หน้าจอ sml_payroll_short_of_work)
        public static string _search_screen_payroll_leave = "screen_payroll_leave"; // ลางาน (หน้าจอ sml_payroll_leave)
        public static string _search_screen_payroll_arrive_late = "screen_payroll_arrive_late"; // มาสาย (หน้าจอ sml_payroll_arrive_late)
        public static string _search_screen_payroll_over_time = "screen_payroll_over_time"; // ค่าล่วงเวลา (หน้าจอ sml_payroll_over_time)
        public static string _search_screen_payroll_income_tax = "screen_payroll_income_tax"; // ภาษีเงินได้ (หน้าจอ sml_payroll_income_tax)    
        public static string _search_screen_payroll_trans_list = "screen_payroll_trans_list"; // บันทึกรายละเอียดการทำงาน (หน้าจอ sml_payroll_trans_list)
        public static string _search_screen_payroll_calculate_wages = "screen_payroll_calculate_wages"; // คำนวณค่าจ้าง (หน้าจอ sml_payroll_calculate_wages)   
        public static string _search_screen_payroll_calculate_tax = "screen_payroll_calculate_tax"; // คำนวณภาษี (หน้าจอ sml_payroll_calculate_tax)
        public static string _search_screen_payroll_tax = "screen_payroll_tax"; // คำนวณภาษี (หน้าจอ sml_payroll_tax)
        ////------------------------------------------------------------------------------------------------------
        ////Popup (popup ค้นหา ชื่อรหัส)
        ////------------------------------------------------------------------------------------------------------
        public static string _search_popup_payroll_employee = "popup_payroll_employee"; // พนักงาน - ลูกจ้าง  
        public static string _search_popup_payroll_short_of_work = "popup_payroll_short_of_work"; // การขาดงาน 
        public static string _search_popup_payroll_over_time = "popup_payroll_over_time"; // ค่าล่วงเวลา         
        //
        //-------------------------------END----PAYROLL--------------------------------------------------------------
        public static _companyProfileType _companyProfile = new _companyProfileType();
        public static string[] _accountType = { "account_type_asset", "account_type_debt", "account_type_capital", "account_type_income", "account_type_expense" };
        public static string[] _accountLevel = { "account_level_0", "account_level_1", "account_level_2", "account_level_3", "account_level_4", "account_level_5", "account_level_6", "account_level_7" };
        public static string[] _accountBalanceMode = { "balance_mode_debit", "balance_mode_credit" };
        public static string[] _accountBalanceSheetEffect = { "balance_sheet_effect_normal", "balance_sheet_effect_sale_cost", "balance_sheet_effect_produce", "balance_sheet_effect_expense" };
        public static string[] _vatBuyType = { "vat_normal", "vat_no_return", "vat_out_due" };
        public static string[] _accountTaxType = { "none", "vat_sale", "vat_buy", "wht_in", "wht_out" };
        public static string[] _debt_type = { "return product", "money transfer" };
        public static string[] _po_purchase_sale_type = { "purchase_cash", "purchase_credit", "purchase_service_cash", "purchase_service_credit" };
        public static string[] _hp_customer_card_type = { "id_card_1", "id_card_2", "id_card_3", "id_card_4", "id_card_5" };
        public static string[] _ar_customer_status = { "customer", "ar", "target_customer" };// สถานะ ลูกหนี้ หน้าจอ รายละเอียดลูกหนี้ (_ar_detail)
        public static string[] _ar_customer_credit_status = { "close_credit", "open_credit", "stop_credit" };// สถานะเครดิต ลูกหนี้ หน้าจอ รายละเอียดลูกหนี้ (_ar_detail)
        /// <summary>สถานะเช็ค</summary>
        public static string[] _chq_status = { "chq_on_hand", "chq_commit", "chq_pass", "chq_return", "chq_cancel", "chq_sale", "chq_commit_again" };
        /// <summary>ประเภทเช็ค</summary>
        public static string[] _chq_type = { "chq_cash", "chq_acc_pay" };
        /// <summary>ประเภทบัตรเครดิต</summary>
        public static string[] _credit_card_type = { "credit_type_visa", "credit_type_master" };
        /// <summary>ประเภทเงินรับจ่าย</summary>
        public static string[] _moneyType = { "cash", "cheque", "money transfer" };
        // ส่งคืนสินค้า/ราคาผิด
        public static object[] _ap_bill_type_1 = new object[] { "ไม่เลือก", "ใบซื้อสินค้า", "ใบตั้งหนี้", "เจ้าหนี้อื่นๆ", "ยอดยกมา" };
        // เพิ่มหนี้/ราคาผิด
        public static object[] _ap_bill_type_2 = new object[] { "ไม่เลือก", "ใบซื้อสินค้า", "ใบตั้งหนี้" };
        // เพิ่มหนี้/ราคาผิด (พาเชียล)
        public static object[] _ap_bill_type_3 = new object[] { "ไม่เลือก", "ใบรับสินค้า" };
        // ลดหนี้จากการตั้งหนี้ (พาเชียล)
        public static object[] _ap_bill_type_4 = new object[] { "ไม่เลือก", "ใบตั้งหนี้" };
        // สั่งขาย
        public static object[] _so_sale_order_type_1 = new object[] { "ไม่เลือก", "ใบเสนอราคา", "ใบสั่งซื้อ/ใบสั่งจอง" };
        // ขาย
        public static object[] _so_sale_order_type_2 = new object[] { "ไม่เลือก", "ใบเสนอราคา", "ใบสั่งซื้อ/ใบสั่งจอง", "ใบสั่งขาย" };
        // รับคืนจากการขาย
        public static object[] _so_sale_order_type_3 = new object[] { "ไม่เลือก", "ขายเงินเชื่อ", "ตั้งหนี้ยกมา" }; // "ขายเงินสด", "ค่าบริการเงินเชื่อ", "ค่าบริการเงินสด"
        // เพิ่มหนี้ (ขาย)
        public static object[] _so_sale_order_type_4 = new object[] { "ไม่เลือก", "ขายเงินเชื่อ", "ค่าบริการเงินเชื่อ" };

        // สินค้า_โอนออก
        public static object[] _transfer_out_bill_type = new object[] { "อ้างอิงใบขอโอนสินค้า", "ซื้อสินค้า" };

        public static object[] _po_bill_type_1 = new object[] { "ใบอนุมัติเสนอซื้อ", "ใบเสนอซื้อ" };
        //
        public static string _search_screen_gl_chart_of_account = "screen_gl_chart_of_account";
        public static string _search_screen_gl_account_group = "screen_gl_account_group";
        public static string _search_screen_gl_journal_book = "screen_gl_journal_book";
        public static string _search_screen_gl_tax_group = "screen_gl_tax_group";

        // Master Modi:  21-08-2549 21:39 
        public static string _search_master_ic_warehouse = "screen_ms_ic_warehouse";// เอ้ คลังสินค้า
        public static string _search_master_ic_shelf = "screen_ms_ic_shelf";        // เอ้ ที่เก็บสินค้า
        public static string _search_master_ic_shelf_warehouse = "screen_ms_ic_shelf_by_warehouse";        // เอ้ ที่เก็บสินค้าตามคลังสินค้า
        public static string _search_master_ic_type = "screen_ms_ic_type";          // เอ้ ประเภทสินค้า
        public static string _search_master_ic_group = "screen_ms_ic_group";        // เอ้ กลุ่มสินค้า
        public static string _search_master_ic_group_sub = "screen_ms_ic_group_sub";// เอ้ กลุ่มสินค้าย่อย
        public static string _search_master_ic_category = "screen_ms_ic_category";  // เอ้ หมวดสินค้า
        public static string _search_master_ic_brand = "screen_ms_ic_brand";        // เอ้ ยี่ห้อสินค้า
        public static string _search_master_ic_pattern = "screen_ms_ic_pattern";    // เอ้ รูปแบบสินค้า
        public static string _search_master_ic_design = "screen_ms_ic_design";      // เอ้ รูปทรงสินค้า
        public static string _search_master_ic_grade = "screen_ms_ic_grade";        // เอ้ เกรดสินค้า
        public static string _search_master_ic_class = "screen_ms_ic_class";        // เอ้ ระดับสินค้า
        public static string _search_master_ic_color = "screen_ms_ic_color";        // เอ้ สีสินค้า
        public static string _search_master_ic_formular = "screen_ms_ic_formular";     // เอ้ สูตรการผลิต
        public static string _search_master_ic_size = "screen_ms_ic_size";          // เอ้ ขนาดสินค้า
        public static string _search_master_ic_issue_type = "screen_ms_ic_issue_type"; // เอ้ ประเภทการเบิก
        public static string _search_master_ic_character = "screen_ms_ic_character";// เอ้ ลักษณะสินค้า
        public static string _search_master_ic_unit_type = "screen_ms_ic_unit_type";// เอ้ ประเภทหน่วยนับ
        public static string _search_master_ic_unit = "screen_ms_ic_unit";          // เอ้ หน่วยนับสินค้า
        public static string _search_master_ic_unit_use = "screen_ms_ic_unit_use";  // เอ้ หน่วยนับสินค้า
        public static string _search_master_ic_dimension = "screen_ms_ic_dimension";// เอ้ มิติสินค้า
        public static string _search_master_ic_model = "screen_ms_ic_model";// รุ่นสินค้า
        public static string _search_master_ic_import_duty = "screen_ms_ic_import_duty";// เอ้ พิกัดศุลกากร
        public static string _search_master_ic_adjust_reason = "screen_ms_ic_adjust_reason"; //เหตุผลการปรับปรุง
        public static string _search_master_ar_type = "screen_ms_ar_type";          // ประเภทลูกค้า
        public static string _search_master_ar_group = "screen_ms_ar_group";        // กลุ่มลูกค้า  
        public static string _search_master_ar_dimension = "screen_ms_ar_dimension";// มิติลูกค้า
        public static string _search_master_ar_credit_group = "screen_ms_ar_credit_group";// กลุ่มเครดิตลูกค้า
        public static string _search_master_ar_open_reason = "screen_ms_ar_open_reason";    // เหตุผลการเปิดเครดิต
        public static string _search_master_ar_close_reason = "screen_ms_ar_close_reason";  // เหตุผลการปิดเครดิต
        public static string _search_master_ar_group_sub = "screen_ms_ar_group_sub";// กลุ่มลูกค้าย่อย
        public static string _search_master_ar_area_code = "screen_ms_ar_sale_area";// เขตการขาย
        public static string _search_master_ar_paybill_area_code = "screen_ms_ar_paybill_area";// เขตการขาย

        public static string _search_master_customer_type = "screen_ms_customer_type";// เนส ประเภทลูกค้า
        public static string _search_master_ar_channel = "screen_ms_ar_channel";// เนส ช่องทาง
        public static string _search_master_ar_location_type = "screen_ms_ar_location_type";// เนส ปรถเถทสถานที่ตั้ง
        public static string _search_master_ar_sub_type_1 = "screen_ms_ar_sub_type_1";// เนส 
        public static string _search_master_ar_vehicle = "screen_ms_ar_vehicle";// เนส 
        public static string _search_master_ar_equipment = "screen_ms_ar_equipment";// เนส 
        public static string _search_master_ar_sub_equipment = "screen_ms_ar_sub_equipment";// เนส 

        public static string _search_master_ar_project = "screen_ms_ar_project";// เนส 
        public static string _search_master_ar_shoptype1 = "screen_ms_ar_shoptype1";// เนส 
        public static string _search_master_ar_shoptype2 = "screen_ms_ar_shoptype2";// เนส 
        public static string _search_master_ar_shoptype3 = "screen_ms_ar_shoptype3";// เนส 
        public static string _search_master_ar_shoptype4 = "screen_ms_ar_shoptype4";// เนส 
        public static string _search_master_ar_shoptype5 = "screen_ms_ar_shoptype5";// เนส 
        public static string _search_master_sub_ar_shoptype5 = "screen_ms_sub_ar_shoptype5";// เนส 


        public static string _search_master_ar_logistic_area = "screen_ms_ar_logistic_area";   //เอ้  เขตการขนส่ง
        public static string _search_master_ar_credit_approve = "screen_ms_ar_credit_approve";  //เอ้  เหตุผลอนุมัติวงเงิน
        public static string _search_master_ar_pay_bill_condition = "screen_ms_ar_pay_bill_condition"; // เงื่อนไขการวางบิล
        public static string _search_master_ar_keep_chq_condition = "screen_ms_ar_keep_chq_condition"; // เงื่อนไขการรับเช็ค
        public static string _search_master_ar_work_title = "screen_ms_ar_work_title";  // ตำแหน่งงาน
        public static string _search_master_ar_keep_money = "screen_ms_ar_keep_money";  // ช่องทางการเก็บเงิน
        public static string _search_master_ap_type = "screen_ms_ap_type";          // ประเภทผู้จำหน่าย
        public static string _search_master_ap_group = "screen_ms_ap_group";        // กลุ่มผู้จำหน่าย
        public static string _search_master_ap_dimension = "screen_ms_ap_dimension";// มิติเจ้าหนี้
        public static string _search_master_ap_group_sub = "screen_ms_ap_group_sub";// กลุ่มเจ้าหนี้ย่อย
        public static string _search_master_as_asset_type = "screen_ms_as_asset_type";  // ประเภทสินทรัพย์
        public static string _search_master_as_asset_location = "screen_ms_as_asset_location"; // ตำแหน่งสินทรัพย์
        public static string _search_master_as_asset_maintain = "screen_ms_as_asset_maintain"; // บำรุงรักษาสินทรัพย์
        public static string _search_master_as_asset_maintain_unit = "screen_ms_as_asset_maintain_unit"; // หน่วยบำรุงรักษาสินทรัพย์
        public static string _search_master_as_asset_retire = "screen_ms_as_asset_retire"; // ตัดจำหน่ายสินทรัพย์
        public static string _search_master_erp_job_list = "screen_erp_job_list"; // รายละเอียดงาน
        public static string _search_master_erp_project_list = "screen_erp_project_list"; // รายละเอียดโครงการ
        public static string _search_master_erp_allocate_list = "screen_erp_allocate_list"; // รายละเอียดการจัดสรร
        public static string _search_master_ar_customer_group = "screen_ar_customer_group"; // กลุ่มพนักงานขาย
        public static string _search_ic_purchase_permium = "screen_ic_purchase_permium"; // ของแถมตอนซื้อ
        public static string _search_ic_promotion_formula = "screen_ic_promotion_formula"; // สูตร Promotion
        public static string _screen_erp_doc_format = "screen_erp_doc_format";
        //
        // CASHBANK [ เช็ค-ธนาคาร ] 21-08-2549 23:39   
        public static string _search_screen_gl_chq_receive = "screen_gl_chq_receive";   // พี่จืด( รายละเอียดเช็ครับ )
        public static string _search_screen_gl_petty_cash = "screen_gl_petty_cash";     // พี่จืด( รายละเอียดเงินสดย่อย )        
        public static string _search_screen_gl_cash_flow_group = "screen_gl_cash_flow_group";// พี่จืด( กำหนดรายละเอียดกลุ่มบัญชีกระแสเงินสด )                   
        public static string _search_screen_gl_account_type = "screen_gl_account_type";     // อรุณ( ประเภทบัญชี )            
        // CASHBANK [ เช็ค เงินสด ธนาคาร] 22-06-2552 15:49
        /// <summary>รายได้จากธนาคาร</summary>
        public static string _search_screen_bank_income = "screen_cb_bank_income";
        /// <summary>ค่าใช้จ่ายจากธนาคาร</summary>
        public static string _search_screen_bank_expanse = "screen_cb_bank_expense";
        /// <summary>ดอกเบี้ยรับ</summary>
        public static string _search_screen_interest_received = "screen_interest_received";
        /// <summary>ดอกเบี้ยจ่าย</summary>
        public static string _search_screen_interest_payment = "screen_interest_payment";
        /// <summary>รายละเอียดฝากเงิน</summary>
        public static string _search_screen_ธนาคาร_ฝากเงิน = "screen_cb_cash_payin";
        /// <summary>รายละเอียดถอนเงิน</summary>
        public static string _search_screen_cb_cash_withdraw = "screen_cb_cash_withdraw";
        /// <summary>รายละเอียดโอนเงิน</summary>
        public static string _search_screen_cb_cash_transfer = "screen_cb_cash_transfer";
        /// <summary>ข้อมูลเช็ค</summary>
        public static string _search_screen_cb_เช็ครับ = "screen_cb_chq_list_in";
        /// <summary>ข้อมูลเช็ค</summary>
        public static string _search_screen_cb_เช็คจ่าย = "screen_cb_chq_list_out";
        /// <summary>อ้างอิงเลขที่เช็ค</summary>
        public static string _search_screen_cb_chq_ref = "screen_cb_chq_ref";
        /// <summary>อ้างอิงเลขที่เช็คจากมาสเตอร์</summary>
        public static string _search_screen_cb_chq_ref_in_list = "screen_cb_chq_ref_in_list";
        /// <summary>รายการเดินเช็ค</summary>
        public static string _search_screen_cb_chq_trans = "screen_cb_chq_trans";
        /// <summary>ค้นหาเลขที่เอกสาร CB Trans</summary>
        public static string _search_screen_cb_trans = "screen_cb_trans";
        /// <summary>กำหนดวงเงินสดย่อย</summary>
        public static string _search_screen_cb_petty_cash = "screen_cb_petty_cash";
        /// <summary>รายการเดินเงินสดย่อย</summary>
        public static string _search_screen_เงินสดย่อย_รายวัน = "screen_cb_petty_cash_trans";
        /// <summary>บันทึกรายละเอียดบัตรเครดิต</summary>
        public static string _search_screen_บัตรเครดิต = "screen_cb_credit_card";
        /// <summary>ประเภทบัตรเครดิต</summary>
        public static string _search_screen_erp_credit_type = "screen_erp_credit_type";
        /// <summary>บันทึกรายการเดินบัตรเครดิต</summary>
        public static string _search_screen_cb_credit_card_trans = "screen_cb_credit_card_trans";
        /// <summary>ธนาคาร</summary>
        public static string _search_screen_bank = "screen_erp_bank_search";// พี่จืด( รายละเอียดธนาคาร )
        /// <summary>สาขาธนาคาร</summary>
        public static string _search_screen_bank_branch = "screen_erp_bank_branch_search";
        /// <summary>รายละเอียดสมุดบัญชีธนาคาร</summary>
        public static string _search_screen_สมุดเงินฝาก = "screen_pass_book";             // พี่จืด( รายละเอียดสมุดบัญชีธนาคาร )  
        /// <summary>รายละเอียดประเภทสมุดบัญชีธนาคาร</summary>
        public static string _search_screen_pass_book_type = "screen_pass_book_type";   // พี่จืด( รายละเอียดประเภทสมุดบัญชีธนาคาร )
        /// <summary>การยกเลิกเอกสารเช็ค</summary>
        public static string _search_screen_chq_cancel = "screen_chq_doc_cancel"; // 
        //
        // AP [ เจ้าหนี้ ] 20-08-2549 09:30 
        public static string _search_screen_ap = "screen_ap_supplier";          // เอนก ( รายละเอียดเจ้าหนี้ )
        public static string _screen_ap_supplier_search = "screen_ap_supplier_search";
        // AR [ ลูกหนี้ ] 20-08-2549 09:30 
        public static string _search_screen_ar_group_1 = "screen_ar_group_1";
        public static string _search_screen_ar_group_2 = "screen_ar_group_2";
        public static string _search_screen_ar = "screen_ar_customer";          // โอ๋(อรุณ)( รายละเอียดลูกหนี้ )  
        public static string _search_screen_ar_dealer = "screen_ar_dealer";          // รายละเอียดสมาชิก
        public static string _search_screen_ap_debt_balance = "screen_ap_debt_balance";          // สอาด รายละเอียดยอดหนี้ยกมา
        public static string _search_screen_ar_debt_balance = "screen_ar_debt_balance";          // สอาด รายละเอียดยอดหนี้ยกมา
        public static string _screen_erp_province = "screen_erp_province";          // โอ๋(อรุณ)( จังหวัด ) 
        public static string _screen_erp_amper = "screen_erp_amper";          // โอ๋(อรุณ)( อำเภอ ) 
        public static string _screen_erp_tambon = "screen_erp_tambon";          // โอ๋(อรุณ)( ตำบล ) 

        // AR_AP  [ลูกหนี้ , เจ้าหนี้ ]
        public static string _screen_ap_ar_trans_ref = "screen_ap_ar_trans_ref";          // jead
        public static string _screen_ap_ar_trans = "screen_ap_ar_trans";          // เอ้
        public static string _screen_ap_before_pay = "screen_ap_before_pay";          // ใบรับวางบิล (เจ้าหนี้)
        public static string _screen_ap_trans = "screen_ap_trans";          // เอ้
        public static string _screen_ar_trans = "screen_ar_trans";          // เอ้
        public static string _screen_ap_trans_cancel = "screen_ap_trans_cancel";          // เอ้
        public static string _screen_ar_trans_cancel = "screen_ar_trans_cancel";          // เอ้
        public static string _screen_ap_supplier_trans = "screen_ap_supplier_trans";          // เอ้
        public static string _screen_ar_customer_trans = "screen_ar_customer_trans";          // เอ้
        /// <summary>รายละเอียดเงินมัดจำล่วงหน้า</summary>
        public static string _screen_po_deposit = "screen_po_deposit";
        public static string _screen_so_deposit = "screen_so_deposit";
        public static string _screen_ap_ic_trans = "screen_ap_ic_trans";
        public static string _screen_ar_ic_trans = "screen_ar_ic_trans";
        public static string _screen_ar_before_pay = "screen_ar_before_pay";          // ใบรับวางบิล (ลูกหนี้)

        public static string _search_screen_ic_inventory_thai7 = "screen_ic_inventory_thai7";
        public static string _search_screen_ic_category_thai7 = "screen_ms_ic_category_thai7";
        public static string _search_screen_ic_category_thai7_sub = "screen_ms_ic_category_thai7_sub";

        /// <summary>รายการเดินเช็ค</summary>
        public static string _search_screen_cb_cheque_trans = "screen_cb_cheque_trans";


        //
        // IC [ สินค้า ] 20-08-2549 09:30         
        public static string _search_screen_ic_permission = "screen_ic_permission";     // พิเชษฐ์( บันทึกขอเบิกสินค้า )
        public static string _search_screen_ic_approve = "screen_ic_approve";           // พิเชษฐ์( บันทึกเบิกสินค้า )
        public static string _search_screen_ic_inventory = "screen_ic_inventory";       // เอ้ ( รายละเอียดสินค้า )                        
        public static string _search_screen_ic_inventory_barcode = "screen_ic_inventory_barcode";       // เอ้ ( รายละเอียดสินค้า )                        
        public static string _search_screen_ic_barcode = "screen_ic_barcode";       // barcode
        public static string _search_screen_table = "screen_screen_table";       // โต๊ะอาหาร
        public static string _screen_kitchen_master = "screen_kitchen_master";
        public static string[] _item_status = { "item_status_new", "item_status_old" }; //สถานะสินค้า
        public static Object[] _statusBarCodeComboBox = new Object[] { "use", "no_use" };

        // New XML IC Trans : 18-06-2552
        public static string _search_screen_ic_stk_wh_balance = "screen_ic_stk_wh_balance"; // Stock Balance : ยอดคงเหลือสินค้ายกมา (คลัง)

        public static string _search_screen_ic_stk_adjust = "screen_ic_stk_adjust"; // Stock Adjust : ปรับปรุงสต็อก (Anek)
        public static string _search_screen_ic_stk_balance = "screen_ic_stk_balance"; // Stock Balance : ยอดคงเหลือสินค้ายกมา (Anek)
        public static string _search_screen_ic_stk_build = "screen_ic_stk_build"; // Stock Build : จัดชุดสินค้า (Anek)
        public static string _search_screen_ic_stk_check = "screen_ic_stk_check"; // Stock Check : ตรวจนับสินค้า (Anek)
        public static string _search_screen_ic_stk_check_result = "screen_ic_stk_check_result"; // Stock Check Result : ผลการตรวจนับสินค้า (Anek)
        public static string _search_screen_ic_stk_finish_goods = "screen_ic_stk_finish_goods"; // Stock Finish Goods : รับสินค้าสำเร็จรูป
        public static string _search_screen_ic_stk_request = "screen_ic_stk_request"; // Stock Request : เบิกสินค้า (Anek)
        public static string _search_screen_ic_stk_return = "screen_ic_stk_return"; // Stock Return : รับคืนสินค้าเบิก (Anek)
        public static string _search_screen_ic_stk_transfer = "screen_ic_stk_transfer";// Stock Transfer : โอนสินค้า (Anek)
        public static string _search_screen_ic_trans = "screen_ic_trans";// screen ic trans : รายวันทั้งหมด (Anek)  
        public static string _search_screen_ic_serial = "screen_ic_serial";// screen ic serial : serial number  (Nutt)

        public static string _search_screen_ic_stk_pre_request = "screen_ic_stk_pre_request"; // ขอเบิก

        // PO [ ซื้อสินค้า ] 21-08-2549 23:39 
        public static string[] _so_quotation_type = { "item_cash", "item_credit", "service_cash", "service_credit" };   //ประเภทใบเสนอราคา
        public static string[] _po_so_tax_type = { "tax_out", "tax_in", "tax_zero" };   //ประเภทภาษี
        public static string[] _ic_tax_type = { "exclude_vat", "include_vat", "zero_vat" };   //ประเภทภาษี
        public static string[] _po_so_sending_type = { "by_yourself", "give_sending" };  //ประเภทการส่ง
        public static string[] _po_so_answer_type = { "waiting", "answer_yes", "answes_no" };   //ประเภทตอบกลับ
        public static string[] _po_request_type = { "po_request_buy", "po_request_ particular" };   //ประเภทเสนอซื้อ
        public static string[] _po_so_deposit_type = { "prepaid_money", "deposit_money" };  //ประเภทเงินรับหรือจ่าย
        public static string[] _po_so_credit_note = { "ic_defect", "ic_wrong", "price_wrong" };   //เหตุผลการคืนสินค้า
        public static string[] _po_so_deposit_money_status = { "cancle", "buy_sell", "cancle_not_all", "cancle_all" };
        public static string[] _po_so_debt_type = { "ic_defect", "ic_wrong", "price_wrong" }; //ลดหนี้เพิ่มหนี้
        public static string[] _ap_ar_branch_type = { "headquarters", "branch" };
        public static string _search_screen_เงินสดธนาคาร_เช็คจ่าย_ยกมา = "screen_chq_out_balance";
        public static string _search_screen_เงินสดธนาคาร_เช็ครับ_ยกมา = "screen_chq_in_balance";
        /// <summary>รายละเอียดสืบราคาี้</summary>
        public static string _search_screen_po_investigate = "screen_po_investigate";
        public static string _search_screen_ซื้อ_เสนอซื้อ = "screen_po_purchase_request";
        public static string _search_screen_ซื้อ_เสนอซื้อ_อนุมัติ = "screen_po_purchase_request_approve";
        public static string _search_screen_ซื้อ_เสนอซื้อ_ยกเลิก = "screen_po_purchase_request_cancel";
        /// <summary>รายละเอียดเสนอซื้อสินค้า</summary>
        public static string _search_screen_po_purchase_request_detail = "screen_po_purchase_request_detail";
        /// <summary>รายละเอียดใบสืบราคา</summary>
        public static string _search_screen_po_investigate_detail = "screen_po_investigate_detail";
        /// <summary>ใบเสนอราคา</summary>
        public static string _search_screen_po_so_quotation = "screen_po_so_quotation";
        public static string _search_screen_ขาย_ใบเสนอราคา = "screen_so_quotation";
        public static string _search_screen_ขาย_ใบเสนอราคา_อนุมัติ = "screen_so_quotation_approve";
        public static string _search_screen_ขาย_ใบเสนอราคา_ยกเลิก = "screen_so_quotation_cancel";
        /// <summary>ใบสั่งซื้อ/ขาย/จอง</summary>
        public static string _search_screen_po_inquiry = "screen_po_inquiry";
        /// <summary>อนุมัติใบสั่งซื้อสินค้า</summary>
        public static string _search_screen_po_purchase_approve = "screen_po_purchase_approve";
        public static string _search_screen_po_inquiry_cancel = "screen_po_inquiry_cancel";
        public static string _search_screen_po_advance_cancel = "screen_po_advance_cancel";
        public static string _search_screen_ขาย_ใบสั่งจอง_สั่งซื้อ = "screen_so_sale_order";
        public static string _search_screen_ขาย_ใบสั่งจอง_สั่งซื้อ_อนุมัติ = "screen_so_sale_order_approve";
        public static string _search_screen_ขาย_ใบสั่งจอง_สั่งซื้อ_ยกเลิก = "screen_so_sale_order_cancel";
        public static string _search_screen_po_so_inquiry = "screen_po_so_inquiry";
        public static string _search_screen_purchase = "screen_purchase";
        public static string _search_screen_purchase_cancel = "screen_purchase_cancel";
        public static string _search_screen_purchase_debit_cancel = "screen_purchase_debit_cancel";
        public static string _search_screen_purchase_credit_cancel = "screen_purchase_credit_cancel";
        public static string _search_screen_po_so_saleorder = "screen_po_so_saleorder";
        /// <summary>Inquiry</summary>
        public static string _search_screen_inquiry = "screen_inquiry";
        /// <summary>จ่ายเงินมัดจำ/ล่วงหน้า</summary>
        public static string _search_screen_po_so_deposit_money = "screen_po_so_deposit_money";
        /// <summary>ซื้อสินค้า</summary>
        public static string _search_screen_po_so_purchase_billing = "screen_po_so_purchase_billing";
        /// <summary>ผู้ติดต่อ(เจ้าหนี้)</summary>
        public static string _search_screen_ap_contactor = "screen_ap_contactor";
        /// <summary>ผู้ติดต่อลูกหนี้</summary>
        public static string _search_screen_ar_contactor = "screen_ar_contactor";
        /// <summary>กำหนดราคาซื้อ</summary>
        public static string _search_screen_po_purchase_price = "screen_po_purchase_price";
        /// <summary>บันทึกรับสินค้า/ตั้งหนี้</summary>
        public static string _search_screen_po_goods_receive = "screen_po_goods_receive";
        /// <summary>ซื้อ/ขายสินค้า</summary>
        public static string _search_screen_po_so_invoice = "screen_po_so_invoice";
        public static string _search_screen_po_so_invoice_bill_no = "screen_po_so_invoice_bill_no";//โอ๋ ค้นหาเลขที่ใบกำกับภาษี
        /// <summary>ขายสินค้า</summary>
        public static string _search_screen_sale = "screen_sale";

        /// 
        // AS [ สินทรัพย์ ] 21-08-2549 23:39
        public static string _search_screen_as = "screen_as_asset";
        //

        /// <summary>สาขา</summary>
        public static string _search_master_erp_branch_list = "screen_erp_branch_list";//ตลอดเวลา
        public static string _search_screen_erp_branch_list = "master_erp_branch_list";//โอ๋(ค้นหาตัวเล็ก)
        /// <summary>รายละเอียดชื่อฝ่าย</summary>
        public static string _search_screen_erp_side_list = "screen_erp_side_list";//ตลอดเวลา
        /// <summary>รายละเอียดพนักงาน</summary>
        public static string _search_screen_erp_user = "screen_erp_user";//ตลอดเวลา
        /// <summary>
        /// รายละเอียดการขนส่ง
        /// </summary>
        public static string _search_screen_erp_transport = "screen_erp_transport";//ตลอดเวลา
        /// <summary>รายละเอียดการกลุ่มพนักงาน</summary>
        public static string _search_screen_erp_user_group = "screen_erp_user_group";//ตลอดเวลา
        /// <summary>รายละเอียดแผนก</summary>
        public static string _search_screen_erp_department_list = "screen_erp_department_list";//ตลอดเวลา
        /// <summary>รายละเอียดเครดิส</summary>
        public static string _search_screen_erp_credit = "screen_erp_credit";//ตลอดเวลา
        /// <summary>รายละเอียดโครงการ</summary>
        public static string _search_screen_erp_project = "screen_erp_project";//ตลอดเวลา
        /// <summary>รายละเอียดการจัดสรร</summary>
        public static string _search_screen_erp_allocate = "screen_erp_allocate";//ตลอดเวลา
        /// <summary>รายละเอียดกลุ่มเอกสาร</summary>
        public static string _search_screen_erp_doc_group = "screen_erp_doc_group";//ตลอดเวลา
        /// <summary>รายละเอียดรูปแบบเอกสาร</summary>
        public static string _search_screen_erp_doc_format = "screen_erp_doc_format";//ตลอดเวลา
        /// <summary>รายละเอียดประเภทภาษี</summary>
        public static string _search_screen_gl_tax_type = "screen_gl_tax_type";//ตลอดเวลา
        /// <summary>รายละเอียดเงินสกุล</summary>
        public static string _search_screen_erp_currency = "screen_erp_currency";//ตลอดเวลา
        /// <summary>รายละเอียดประเภทรายได้</summary>
        public static string _search_screen_income_list = "screen_erp_income_list";//ตลอดเวลา       
        /// <summary>รายละเอียดประเภทค่าใช้จ่าย</summary>
        public static string _search_screen_expenses_list = "screen_erp_expenses_list";//ตลอดเวลา       

        /// <summary>รายละเอียดรับ/จ่ายชำระหนี้</summary>
        public static string _search_screen_ap_ar_debt_billing = "screen_ap_ar_debt_billing";//ตลอดเวลา
        /// <summary>รายละเอียดตัดหนี้สูญ</summary>
        public static string _search_screen_ap_ar_debt_billing_cut = "screen_ap_ar_debt_billing_cut";//ตลอดเวลา
        /// <summary>รายละเอียดเตรียมชำระหนี้/ใบเสร็จชั่วคราว</summary>
        public static string _search_screen_ap_ar_debt_billing_temp = "screen_ap_ar_debt_billing_temp";//ตลอดเวลา



        /// <summary>รายละเอียดใบวางบิล/รับบิล</summary>
        public static string _search_screen_ap_ar_pay_bill = "screen_ap_ar_pay_bill";
        /// <summary>รายละเอียดแผนก</summary>
        public static string _search_master_screen_erp_department_list = "screen_erp_department_list";
        public static string _search_master_screen_agent = "screen_agent";

        public static string _open_formdesign_screen = "screen_formdesign_loadform";
        /// <summary>
        /// จอค้นหางวดบัญชี
        /// </summary>
        public static string _search_screen_account_period = "screen_account_period";
        /// <summary>
        /// Healty
        /// </summary>
        public static string _search_screen_healthy_nationality = "screen_healthy_nationality";//สัญชาติ      
        public static string _search_screen_healthy_times = "screen_healthy_times";//ช่วงเวลาการใช้ยา      
        public static string _search_screen_healthy_properties = "screen_healthy_properties";//สรรพคุณยา      
        public static string _search_screen_healthy_doze = "screen_healthy_doze";//ขนาดและวิธีใช้ยา      
        public static string _search_screen_healthy_warning = "screen_healthy_warning";//คำเตือนการใช้ยา      
        public static string _search_screen_healthy_m_mim_group = "screen_healthy_m_mim_group";//ชื่อสามัญทางยา      
        public static string _search_screen_healthy_frequency = "screen_healthy_frequency";//ข้อกำหนดเวลาใช้ยา      
        public static string _search_screen_healthy_disease = "screen_healthy_disease";//ชนิดโรค   

        public static string _search_screen_healthy_patientprofile = "screen_healthy_patientprofile";//รายละเอียดคนไข้   
        public static string _search_screen_healthy_yourhealth = "screen_healthy_yourhealth";//ข้อมูลสุขภาพ   
        public static string _search_screen_healthy_consultation = "screen_healthy_consultation";//ประวัติคำปรึกษาสุขภาพ   
        public static string _search_screen_healthy_congenital_disease = "screen_healthy_congenital_disease";//โรคประจำตัว   
        public static string _search_screen_healthy_drugsconsultants = "screen_healthy_drugsconsultants";//ประวัติการให้คำปรึกษาการใช้ยา   
        public static string _search_screen_healthy_screen_healthy_allergic = "screen_healthy_allergic";//ประวัติแพ้ยา   
        public static string _search_screen_healthy_information = "screen_healthy_information";//รายละเอียดยาเพิ่มเติม   
        public static string _search_screen_healthy_member_profile = "screen_healthy_member_profile"; //รายละเอียดสมาชิก

        /// <summary>
        /// POS 
        /// </summary>
        public static string _search_screen_pos_save_send_money = "screen_pos_save_send_money"; // รายละเอียดการส่งเงิน
        public static string _search_screen_ar_point_balance = "screen_point_balance";

        /// <summary>
        /// บันทึกใบรับวางบิล
        /// </summary>

        private static int _ap_ar_pay_bill;
        public static int ap_ar_pay_bill
        {
            get { return g._ap_ar_pay_bill; }
            set { g._ap_ar_pay_bill = value; }
        }

        private static string _ap_ar_pay_bill_name;
        public static string ap_ar_pay_bill_name
        {
            get { return g._ap_ar_pay_bill_name; }
            set { g._ap_ar_pay_bill_name = value; }
        }

        /// 

        public static ArrayList _accountPeriodDateBegin = new ArrayList();
        public static ArrayList _accountPeriodDateEnd = new ArrayList();
        public static ArrayList _accountPeriods = new ArrayList();
        //
        /// <summary>
        /// อัตรภาษีมูลค่าเพิ่ม (ในกรณีไม่มีกลุ่มภาษี)
        /// </summary>
        public static decimal _default_vat_rate = 7.0M;

        private static int _convertStringToInt32(DataRow row, string fieldName)
        {
            try
            {
                return MyLib._myGlobal._intPhase(row[fieldName].ToString());
            }
            catch
            {
                return 0;
            }
        }

        public static Boolean _saveLog = false;
        /// <summary>
        /// Log Menu Save
        /// </summary>
        /// <param name="mode">1=In,2=Out</param>
        /// <param name="menuName"></param>
        /// <param name="screenName"></param>
        public static string _logMenu(int mode, string guidOld, string menuName)
        {
            string __guid = "";
            if (MyLib._myGlobal._save_logs)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                StringBuilder __queryLog = new StringBuilder();
                string __logTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US"));
                if (mode == 0 || mode == 1)
                {
                    // เข้า Menu
                    __guid = Guid.NewGuid().ToString("N");
                    __queryLog.Append(MyLib._myUtil._convertTextToXml("insert into " + _g.d.logs._table + " (" + _g.d.logs._function_type + "," + _g.d.logs._function_code + "," + _g.d.logs._user_code + "," +
                        _g.d.logs._date_time + "," + _g.d.logs._menu_name + "," + _g.d.logs._guid + "," + _g.d.logs._computer_name + ") values (" +
                        mode + ",0,\'" + MyLib._myGlobal._userCode + "\',\'" + __logTime + "\',\'" + menuName + "\',\'" + __guid + "\',\'" + SystemInformation.ComputerName + "\')"));
                }
                else
                {
                    __queryLog.Append(MyLib._myUtil._convertTextToXml("update " + _g.d.logs._table + " set " + _g.d.logs._date_time_out + "=\'" + __logTime + "\' where " + _g.d.logs._guid + "=\'" + guidOld + "\'"));
                }
                string __resultLog = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __queryLog.ToString().ToLower(), false);
                if (__resultLog.Length != 0)
                {
                    MessageBox.Show(__resultLog, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return __guid;
        }

        public static void _companyProfileLoad()
        {
            _companyProfileLoad(true);
        }
        /// <summary>
        /// ดึงรายละเอียดบริษัทจากฐานข้อมูล
        /// </summary>
        public static void _companyProfileLoad(Boolean warning)
        {
            string __getBranchCodeCompanyProfile = "";

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __query = "select " + _g.d.erp_company_profile._branch_status + ","
                + _g.d.erp_company_profile._company_name_1 + ","
                + _g.d.erp_company_profile._address_1 + ","
                + _g.d.erp_company_profile._business_name_1 + ","
                + _g.d.erp_company_profile._tax_number + ","
                + _g.d.erp_company_profile._telephone_number + ","
                + _g.d.erp_company_profile._fax_number + ","
                + _g.d.erp_company_profile._workplace_1 + ","
                + _g.d.erp_company_profile._branch_type + ","
                + _g.d.erp_company_profile._branch_code 

                + " from " + _g.d.erp_company_profile._table;
            try
            {
                DataTable __dataResult = __myFrameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];
                _companyProfile._branchStatus = MyLib._myGlobal._intPhase(__dataResult.Rows[0].ItemArray[0].ToString());
                MyLib._myGlobal._ltdName = __dataResult.Rows[0].ItemArray[1].ToString();
                MyLib._myGlobal._ltdAddress = __dataResult.Rows[0].ItemArray[2].ToString();
                MyLib._myGlobal._ltdBusinessName = __dataResult.Rows[0].ItemArray[3].ToString();
                MyLib._myGlobal._ltdTax = __dataResult.Rows[0].ItemArray[4].ToString();
                MyLib._myGlobal._ltdTel = __dataResult.Rows[0].ItemArray[5].ToString();
                MyLib._myGlobal._ltdFax = __dataResult.Rows[0].ItemArray[6].ToString();
                MyLib._myGlobal._ltdWorkplace = __dataResult.Rows[0].ItemArray[7].ToString();
                MyLib._myGlobal._branch_type = MyLib._myGlobal._intPhase(__dataResult.Rows[0].ItemArray[8].ToString());
                // MyLib._myGlobal._branch_code = __dataResult.Rows[0].ItemArray[9].ToString();
                __getBranchCodeCompanyProfile = __dataResult.Rows[0].ItemArray[9].ToString();

                if (MyLib._myGlobal._branchCodeFormSerialDrive.Length > 0)
                {
                    MyLib._myGlobal._branch_code = MyLib._myGlobal._branchCodeFormSerialDrive;
                }
                else
                {
                    MyLib._myGlobal._branch_code = __getBranchCodeCompanyProfile;
                }

            }
            catch
            {
                MyLib._myGlobal._ltdName = "...";
            }
            try
            {
                // Option
                __query = "select  *  from " + _g.d.erp_option._table;
                DataTable __dataResult = __myFrameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];
                _companyProfile._printerMargin = _convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._printer_top_margin);
                _companyProfile._year_type = _convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._year_type);
                _companyProfile._vat_type = _convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._vat_type);
                _companyProfile._vat_type_1 = _convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._vat_type_1);
                _companyProfile._vat_rate = MyLib._myGlobal._decimalPhase(__dataResult.Rows[0][_g.d.erp_option._vat_rate].ToString());

                _companyProfile._gl_no_decimal = _convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._gl_no_decimal);
                _companyProfile._use_doc_group = _convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._use_doc_group);
                _companyProfile._use_log_book = _convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._use_log_book);
                _companyProfile._use_print_slip = _convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._use_print_slip);
                _companyProfile._use_department = _convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._use_department);
                _companyProfile._use_job = _convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._use_job);
                _companyProfile._use_allocate = _convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._use_allocate);
                _companyProfile._use_unit = _convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._use_unit);
                _companyProfile._use_project = _convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._use_project);
                _companyProfile._use_work_in_process = _convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._use_work_in_process);
                _companyProfile._use_expire = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._use_expire) == 1) ? true : false;
                _companyProfile._item_qty_decimal = _convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._item_qty_decimal);
                _companyProfile._item_price_decimal = _convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._item_price_decimal);
                _companyProfile._item_amount_decimal = _convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._item_amount_decimal);
                _companyProfile._wht_rate = _convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._wht_rate);
                _companyProfile._warning_price_1 = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._warning_price_1) == 1) ? true : false;
                _companyProfile._warning_price_2 = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._warning_price_2) == 1) ? true : false;
                _companyProfile._warning_price_3 = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._warning_price_3) == 1) ? true : false;
                _companyProfile._purchase_credit_note_type = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._purchase_credit_note_type) == 1) ? true : false;
                _companyProfile._gl_trans_type = _convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._gl_trans_type);
                _companyProfile._column_price_enable = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._column_price_enable) == 1) ? true : false;
                _companyProfile._manual_total_enable = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._manual_total) == 1) ? true : false;
                _companyProfile._use_barcode = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._use_barcode) == 1) ? true : false;
                _companyProfile._sync_wbservice_url = __dataResult.Rows[0][_g.d.erp_option._sync_webservice_url].ToString();
                _companyProfile._sync_product = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._sync_product) == 1) ? true : false;
                _companyProfile._balance_control = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._stock_balance_control) == 1) ? true : false;
                _companyProfile._accrued_control = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._stock_accrued_control) == 1) ? true : false;
                _companyProfile._sale_auto_packing = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._sale_auto_packing) == 1) ? true : false;
                _companyProfile._warning_low_cost = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._warning_low_cost) == 1) ? true : false;
                _companyProfile._pr_approve_lock = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._pr_approve_lock) == 1) ? true : false;
                _companyProfile._ar_bill_inform = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._ar_bill_inform) == 1) ? true : false;
                _companyProfile._count_stock_sum = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._count_stock_sum) == 1) ? true : false;
                _companyProfile._cost_by_warehouse = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._cost_warehouse) == 1) ? true : false;
                _companyProfile._sale_order_edit = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._doc_sale_order_edit) == 1) ? true : false;
                _companyProfile._balance_control_type = _convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._balance_control_type);
                _companyProfile._ic_stock_control = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._ic_stock_control) == 1) ? true : false;
                _companyProfile._hidden_cost_1 = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._item_hidden_cost_1) == 1) ? true : false;
                _companyProfile._hidden_income_1 = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._item_hidden_income_1) == 1) ? true : false;
                MyLib._myGlobal._year_type = _companyProfile._year_type;
                // Thread.CurrentThread.CurrentCulture = MyLib._myGlobal._cultureInfo();
                _companyProfile._auto_insert_time = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._auto_insert_time) == 1) ? true : false;
                _companyProfile._perm_wh_shelf = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._perm_wh_shelf) == 1) ? true : false;
                _companyProfile._count_stock_display_saleprice = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._count_stock_display_saleprice) == 1) ? true : false;
                _companyProfile._warning_credit_money = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._warning_credit_money) == 1) ? true : false;
                _companyProfile._get_last_price_type = _convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._get_last_price_type);
                _companyProfile._activeSync = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._active_sync_active) == 1) ? true : false;
                _companyProfile._activeSyncProvider = __dataResult.Rows[0][_g.d.erp_option._active_sync_provider_name].ToString().Trim();
                _companyProfile._activeSyncServer = __dataResult.Rows[0][_g.d.erp_option._active_sync_server].ToString().Trim();
                _companyProfile._activeSyncDatabase = __dataResult.Rows[0][_g.d.erp_option._active_sync_database_name].ToString().Trim();
                _companyProfile._activeSyncAccessCode = __dataResult.Rows[0][_g.d.erp_option._active_sync_access_code].ToString().Trim().ToLower();
                _companyProfile._activeSyncBranchCode = __dataResult.Rows[0][_g.d.erp_option._active_sync_branch_code].ToString().Trim().ToLower();

                _companyProfile._transfer_stock_control = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._transfer_stock_control) == 1) ? true : false;
                _companyProfile._barcode_picture = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._barcode_picture) == 1) ? true : false;
                _companyProfile._discount_type = _convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._discout_type);
                _companyProfile._use_point_center = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._use_point_center) == 1) ? true : false;
                _companyProfile._auto_run_docno_sale = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._auto_run_docno_sale) == 1) ? true : false;
                _companyProfile._doc_sale_tax_number_fixed = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._doc_sale_tax_number_fixed) == 1) ? true : false;

                _companyProfile._promotion_fixed_unitcode = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._promotion_fixed_unitcode) == 1) ? true : false;

                _companyProfile._issue_stock_control = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._issue_stock_control) == 1) ? true : false;
                _companyProfile._ic_price_formula_control = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._ic_price_formula_control) == 1) ? true : false;
                _companyProfile._disable_ic_cost_process = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._disable_ic_cost_process) == 1) ? true : false;
                _companyProfile._deposit_format_from_pos = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._deposit_format_from_pos) == 1) ? true : false;
                _companyProfile._sale_real_price = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._sale_real_price) == 1) ? true : false;
                _companyProfile._use_serial_no_duplicate = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._use_serial_no_duplicate) == 1) ? true : false;
                _companyProfile._save_logs = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._save_logs) == 1) ? true : false;
                _companyProfile._use_reference_pr = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._use_reference_pr) == 1) ? true : false;
                _companyProfile._default_sale_type = _convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._default_sale_type);
                _companyProfile._get_purchase_price = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._get_purchase_price) == 1) ? true : false;
                _companyProfile._use_order_checker = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._use_order_checker) == 1) ? true : false;
                _companyProfile._process_serial_device = __dataResult.Rows[0][_g.d.erp_option._process_serial_device].ToString();
                _companyProfile._ap_bill_inform = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._ap_bill_inform) == 1) ? true : false;
                _companyProfile._fix_item_set_price = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._fix_item_set_price) == 1) ? true : false;
                _companyProfile._lock_low_cost = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._lock_low_cost) == 1) ? true : false;

                _companyProfile._add_item_color = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._add_item_color) == 1) ? true : false;
                _companyProfile._sync_database_name = __dataResult.Rows[0][_g.d.erp_option._sync_database_name].ToString().Trim();
                _companyProfile._sync_provider_code = __dataResult.Rows[0][_g.d.erp_option._sync_provider_code].ToString().Trim();

                _companyProfile._lock_credit_money = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._lock_credit_money) == 1) ? true : false;
                _companyProfile._password_ar_credit = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._password_ar_credit) == 1) ? true : false;

                _companyProfile._manual_customer_code = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._manual_customer_code) == 1) ? true : false;
                _companyProfile._show_branch_doc_only = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._show_branch_doc_only) == 1) ? true : false;
                _companyProfile._voucher_form_code = __dataResult.Rows[0][_g.d.erp_option._voucher_form_code].ToString();
                _companyProfile._count_customer_table = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._count_customer_table) == 1) ? true : false;
                _companyProfile._find_lot_auto = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._find_lot_auto) == 1) ? true : false;
                _companyProfile._print_packing_pos_order = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._print_packing_pos_order) == 1) ? true : false;
                _companyProfile._disabled_edit_doc_no_doc_date = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._disable_edit_doc_no_doc_date) == 1) ? true : false;
                _companyProfile._filter_pay_bill = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._filter_pay_bill) == 1) ? true : false;
                _companyProfile._disable_edit_order = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._disable_edit_order) == 1) ? true : false;
                _companyProfile._print_pos_settle_bill_detail = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._print_pos_settle_bill_detail) == 1) ? true : false;
                _companyProfile._disable_auto_stock_process = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._disable_auto_stock_process) == 1) ? true : false;
                _companyProfile._auto_open_table = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._auto_open_table) == 1) ? true : false;

                _companyProfile._check_input_vat = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._check_input_vat) == 1) ? true : false;
                _companyProfile._sale_order_banalce_control = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._sale_order_banalce_control) == 1) ? true : false;
                _companyProfile._disable_item_cost = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._disable_item_cost) == 1) ? true : false;

                _companyProfile._sum_point_all = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._sum_point_all) == 1) ? true : false;
                _companyProfile._internetSync = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._order_on_internet) == 1) ? true : false;
                _companyProfile._order_after_open_table = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._order_after_open_table) == 1) ? true : false;

                _companyProfile._join_money_credit = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._join_money_credit) == 1) ? true : false;
                _companyProfile._join_money_credit_list = __dataResult.Rows[0][_g.d.erp_option._join_money_credit_list].ToString().Trim();

                _companyProfile._check_lot_auto = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._check_lot_auto) == 1) ? true : false;
                _companyProfile._warning_discount_1 = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._warning_discount_1) == 1) ? true : false;
                _companyProfile._stock_reserved_control = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._stock_reserved_control) == 1) ? true : false;
                _companyProfile._stock_reserved_control_location = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._stock_reserved_control_location) == 1) ? true : false;

                _companyProfile._digital_barcode_scale = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._digital_barcode_scale) == 1) ? true : false;
                _companyProfile._calc_point_from_pay = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._calc_point_from_pay) == 1) ? true : false;

                _companyProfile._orders_speech = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._orders_speech) == 1) ? true : false;
                _companyProfile._orders_speech_format = __dataResult.Rows[0][_g.d.erp_option._orders_speech_format].ToString().Trim();

                _companyProfile._lock_change_customer = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._lock_change_customer) == 1) ? true : false;

                _companyProfile._real_doc_date_doc_time = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._real_doc_date_doc_time) == 1) ? true : false;
                _companyProfile._save_user_order = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._save_user_order) == 1) ? true : false;
                _companyProfile._inventory_gl_post = _convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._inventory_gl_post);

                _companyProfile._multi_currency = ((MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional) && _convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._multi_currency) == 1) ? true : false;
                _companyProfile._home_currency = __dataResult.Rows[0][_g.d.erp_option._home_currency].ToString().Trim();

                _companyProfile._warning_reorder_point = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._warning_reorder_point) == 1) ? true : false;

                _companyProfile._warning_bill_overdue = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._warning_bill_overdue) == 1) ? true : false;
                _companyProfile._warning_product_expire = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._warning_product_expire) == 1) ? true : false;

                _companyProfile._disable_sync_time = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._disable_sync_time) == 1) ? true : false;
                _companyProfile._gl_process_realtime = ((MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional ||
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional) && _convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._gl_process_realtime) == 1) ? true : false;

                _companyProfile._check_ar_status_credit = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._check_ar_status_credit) == 1) ? true : false;
                _companyProfile._search_cheque_other_cust_code = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._search_cheque_other_cust_code) == 1) ? true : false;
                _companyProfile._print_invoice_one_time = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._print_invoice_one_time) == 1) ? true : false;
                _companyProfile._check_ar_duplicate_name = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._check_ar_duplicate_name) == 1) ? true : false;

                _companyProfile._tax_from_invoice = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._tax_from_invoice) == 1) ? true : false;
                _companyProfile._running_doc_no_only = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._running_doc_no_only) == 1) ? true : false;
                _companyProfile._warning_branch_input = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._warning_branch_input) == 1) ? true : false;

                MyLib._myGlobal._round_Type = _convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._round_type);

                // toe report font
                string __reportFont = __dataResult.Rows[0][_g.d.erp_option._report_font].ToString();
                string __reportHeader_1 = __dataResult.Rows[0][_g.d.erp_option._report_font_header_1].ToString();
                string __reportHeader_2 = __dataResult.Rows[0][_g.d.erp_option._report_font_header_2].ToString();

                if (MyLib._myGlobal._isVersionAccount)
                {
                    _companyProfile._request_ar_credit = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._request_ar_credit) == 1) ? true : false;
                    _companyProfile._request_credit_type = _convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._request_credit_type);
                    _companyProfile._ar_credit_chq_outstanding = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._ar_credit_chq_outstanding) == 1) ? true : false;
                    _companyProfile._phone_number_approve = __dataResult.Rows[0][_g.d.erp_option._phone_number_approve].ToString();
                    _companyProfile._sale_hub_approve = __dataResult.Rows[0][_g.d.erp_option._sale_hub_approve].ToString();
                }

                _companyProfile._customer_by_branch = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._customer_by_branch) == 1) ? true : false;
                _companyProfile._show_menu_by_permission = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._show_menu_by_permission) == 1) ? true : false;
                _companyProfile._calc_item_price_discount = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._calc_item_price_discount) == 1) ? true : false;

                _companyProfile._warning_department = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._warning_department) == 1) ? true : false;
                _companyProfile._hidden_price_formula = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._hidden_price_formula) == 1) ? true : false;
                _companyProfile._quotation_expire = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._quotation_expire) == 1) ? true : false;
                _companyProfile._ref_po_approve = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._ref_po_approve) == 1) ? true : false;
                _companyProfile._sr_ss_credit_check = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._sr_ss_credit_check) == 1) ? true : false;

                _companyProfile._close_warning_backup = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._close_warning_backup) == 1) ? true : false;
                _companyProfile._ss_ref_po_only = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._ss_ref_po_only) == 1) ? true : false;
                _companyProfile._warning_input_customer = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._warning_input_customer) == 1) ? true : false;

                _companyProfile._arm_send_cancel_doc = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._arm_send_cancel_doc) == 1) ? true : false;
                _companyProfile._arm_send_cn = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._arm_send_cn) == 1) ? true : false;
                _companyProfile._arm_send_ar_change = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._arm_send_ar_change) == 1) ? true : false;
                _companyProfile._arm_send_cancel_doc_to = __dataResult.Rows[0][_g.d.erp_option._arm_send_cancel_doc_to].ToString();
                _companyProfile._arm_send_cn_to = __dataResult.Rows[0][_g.d.erp_option._arm_send_cn_to].ToString();
                _companyProfile._arm_send_ar_change_to = __dataResult.Rows[0][_g.d.erp_option._arm_send_ar_change_to].ToString();
                _companyProfile._sync_master_url = __dataResult.Rows[0][_g.d.erp_option._sync_master_url].ToString();
                _companyProfile._warehouse_on_the_way = __dataResult.Rows[0][_g.d.erp_option._warehouse_on_the_way].ToString();
                _companyProfile._shelf_on_the_way = __dataResult.Rows[0][_g.d.erp_option._shelf_on_the_way].ToString();


                if (MyLib._myGlobal._programName.Equals("SML CM"))
                {
                    _companyProfile._use_price_center = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._use_price_center) == 1) ? true : false;
                    _companyProfile._price_list_server = __dataResult.Rows[0][_g.d.erp_option._price_list_server].ToString();
                    _companyProfile._price_list_serial_process = __dataResult.Rows[0][_g.d.erp_option._price_list_serial_process].ToString();
                    _companyProfile._price_list_branch = __dataResult.Rows[0][_g.d.erp_option._price_list_branch].ToString();
                    _companyProfile._price_list_key = __dataResult.Rows[0][_g.d.erp_option._price_list_key].ToString();
                }

                if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                {
                    _companyProfile._mobile_sale_url = __dataResult.Rows[0][_g.d.erp_option._mobile_sale_url].ToString();
                    _companyProfile._mobile_bypasskey = __dataResult.Rows[0][_g.d.erp_option._mobile_bypasskey].ToString();
                }

                if (__reportFont.Length > 0)
                {
                    string[] __fontReport = __reportFont.Split(',');
                    if (__fontReport.Length > 1)
                    {
                        _companyProfile._reportFontName = __fontReport[0];
                        _companyProfile._reportFontSize = (float)MyLib._myGlobal._decimalPhase(__fontReport[1]);
                    }
                }
                if (__reportHeader_1.Length > 0)
                {
                    string[] __fontReport = __reportHeader_1.Split(',');
                    if (__fontReport.Length > 1)
                    {
                        _companyProfile._reportHeaderFontName_1 = __fontReport[0];
                        _companyProfile._reportHeaderFontSize_1 = (float)MyLib._myGlobal._decimalPhase(__fontReport[1]);
                    }
                }
                if (__reportHeader_2.Length > 0)
                {
                    string[] __fontReport = __reportHeader_2.Split(',');
                    if (__fontReport.Length > 1)
                    {
                        _companyProfile._reportHeaderFontName_2 = __fontReport[0];
                        _companyProfile._reportHeaderFontSize_2 = (float)MyLib._myGlobal._decimalPhase(__fontReport[1]);
                    }
                }

                MyLib._myGlobal._save_logs = _companyProfile._save_logs;

                if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
                {
                    _companyProfile._use_sale_shift = (_convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._use_sale_shift) == 1) ? true : false;
                    _companyProfile._sale_shift_id = __dataResult.Rows[0][_g.d.erp_option._sale_shift_id].ToString();
                }

                int __nextProcessTime = _convertStringToInt32(__dataResult.Rows[0], _g.d.erp_option._next_process_time);
                if (__nextProcessTime <= 10)
                {
                    MyLib._myGlobal._nextProcessTime = 10;
                }
                else
                {
                    MyLib._myGlobal._nextProcessTime = __nextProcessTime;
                }


                __dataResult.Dispose();
                __dataResult = null;
            }
            catch (Exception __ex)
            {
                if (warning)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("ค่าเริ่มต้นไม่สมบูรณ์ให้ไปกำหนดค่าเริ่มต้นก่อนเริ่มใช้งาน") + " : " + __ex.Message.ToString());
                }
            }

            //if (_companyProfile._activeSync)
            //{
            // โหลดสิทธิ์พน้กงาน แก้ไข master
            // get branch list

            // get branch from serial drive
            /* โต๋ ย้ายไปทำตอนเข้าโปรแกรม
             * 
             * StringBuilder __serialCheck = new StringBuilder();

            try
            {
                MyLib._getInfoStatus _getinfo = new MyLib._getInfoStatus();
                string[] _dataDive = Environment.GetLogicalDrives();


                for (int __loop = 0; __loop < _dataDive.Length; __loop++)
                {


                    string __getDeviceCode = _getinfo.GetVolumeSerial((_dataDive[__loop].Replace(":\\", ""))).Trim().ToLower();

                    if (__getDeviceCode.Length > 0)
                    {
                        if (__serialCheck.Length > 0)
                        {
                            __serialCheck.Append(" or ");
                        }
                        __serialCheck.Append(" (upper(serial_list) like \'%" + __getDeviceCode.ToUpper() + "%\') ");
                    }
                }

            }
            catch
            {
            
            }*/


            string __queryCheckMasterEdit = "select " +
                "coalesce(" + _g.d.erp_user_group._is_change_masterdata + ", 1) as " + _g.d.erp_user_group._is_change_masterdata +
                " from " + _g.d.erp_user_group._table + " where " + _g.d.erp_user_group._code + " in (select " + _g.d.erp_user_group_detail._group_code + " from " + _g.d.erp_user_group_detail._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_user_group_detail._user_code) + " = '" + MyLib._myGlobal._userCode.ToUpper() + "') order by coalesce(" + _g.d.erp_user_group._is_change_masterdata + ", 1) desc";

            StringBuilder __queryList = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryCheckMasterEdit));

            string __queryBranchSelect = "select " + _g.d.erp_user._change_branch_code + "," + _g.d.erp_user._branch_code + "," + _g.d.erp_user._lock_bill + ", (select coalesce(" + _g.d.erp_branch_list._name_1 + ", '') from " + _g.d.erp_branch_list._table + " where " + _g.d.erp_branch_list._code + "=" + _g.d.erp_user._branch_code + ") as " + _g.d.erp_user._name_2 +
                "," + _g.d.erp_user._show_item_cost +
                "," + _g.d.erp_user._disable_edit_doc_no_doc_date_user +
                "," + _g.d.erp_user._reset_print_log +

                ",(select group_code from  erp_user_group_approve where erp_user_group_approve.user_code = erp_user.code and approve_flag = 0 limit 1) as po_approve_group" +
                ",(select group_code from  erp_user_group_approve where erp_user_group_approve.user_code = erp_user.code and approve_flag = 1 limit 1) as sr_approve_group" +
                ",(select group_code from  erp_user_group_approve where erp_user_group_approve.user_code = erp_user.code and approve_flag = 2 limit 1) as ss_approve_group" +

                " from " + _g.d.erp_user._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_user._code) + " = '" + MyLib._myGlobal._userCode.ToUpper() + "' ";
            __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryBranchSelect));

            // load branch 
            /* โต๋ ย้ายไปทำตอนเข้าโปรแกรม
            string __queryCheckBranchCode = "";
            if (__serialCheck.Length > 0)
                __queryCheckBranchCode = " select code, name_1 from erp_branch_list where serial_list != '' and ( " + __serialCheck.ToString() + ") ";
            
            __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryCheckBranchCode));
            *
             */

            __queryList.Append("</node>");

            ArrayList __queryResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryList.ToString());

            if (__queryResult.Count > 0)
            {
                DataSet __ds = (DataSet)__queryResult[0];
                if (__ds.Tables.Count > 0 && __ds.Tables[0].Rows.Count > 0)
                {
                    MyLib._myGlobal._allowChangeMaster = (__ds.Tables[0].Rows[0][_g.d.erp_user_group._is_change_masterdata].ToString() == "1") ? true : false;
                }

                DataSet __branchDs = (DataSet)__queryResult[1];
                //DataSet __branchList = (DataSet)__queryResult[2];

                string __getBranchCode = "";
                string __getBranchName = "";

                /* โต๋ ย้ายไปทำตอนเข้าโปรแกรม
                 * if (__branchList.Tables.Count > 0 && __branchList.Tables[0].Rows.Count > 0)
                 {
                     __getBranchCode = __branchList.Tables[0].Rows[0][_g.d.erp_branch_list._code].ToString();
                     __getBranchName = __branchList.Tables[0].Rows[0][_g.d.erp_branch_list._name_1].ToString();

                 }*/

                if (__branchDs.Tables.Count > 0 && __branchDs.Tables[0].Rows.Count > 0)
                {
                    if (__branchDs.Tables[0].Rows[0][_g.d.erp_user._branch_code].ToString().Length > 0)
                    {
                        __getBranchCode = __branchDs.Tables[0].Rows[0][_g.d.erp_user._branch_code].ToString();
                        __getBranchName = __branchDs.Tables[0].Rows[0][_g.d.erp_user._name_2].ToString();

                    }
                    MyLib._myGlobal._branchCode = _companyProfile._branch_code;
                    _companyProfile._change_branch_code = (_convertStringToInt32(__branchDs.Tables[0].Rows[0], _g.d.erp_user._change_branch_code) == 1 && MyLib._myGlobal._fixBranchCode.Length == 0) ? true : false;
                    MyLib._myGlobal._isUserLockDocument = (_convertStringToInt32(__branchDs.Tables[0].Rows[0], _g.d.erp_user._lock_bill) == 1) ? true : false;
                    MyLib._myGlobal._isUserResetPrintLog = (_convertStringToInt32(__branchDs.Tables[0].Rows[0], _g.d.erp_user._reset_print_log) == 1) ? true : false;


                    _companyProfile._is_User_show_item_cost = (_convertStringToInt32(__branchDs.Tables[0].Rows[0], _g.d.erp_user._show_item_cost) == 1) ? true : false;
                    _companyProfile._disable_edit_doc_no_doc_date_user = (_convertStringToInt32(__branchDs.Tables[0].Rows[0], _g.d.erp_user._disable_edit_doc_no_doc_date_user) == 1) ? true : false;

                    MyLib._myGlobal._approve_po_group = __branchDs.Tables[0].Rows[0]["po_approve_group"].ToString();
                    MyLib._myGlobal._approve_sr_group = __branchDs.Tables[0].Rows[0]["sr_approve_group"].ToString();
                    MyLib._myGlobal._approve_ss_group = __branchDs.Tables[0].Rows[0]["ss_approve_group"].ToString();

                }

                if (__getBranchCode != "")
                {
                    _companyProfile._branch_code = __getBranchCode;
                    _companyProfile._branch_name = __getBranchName;
                    MyLib._myGlobal._branchCode = _g.g._companyProfile._branch_code;
                }
                //DataSet __ds2 
                if (MyLib._myGlobal._fixBranchCode.Length > 0)
                {
                    _companyProfile._branch_code = MyLib._myGlobal._fixBranchCode;
                    _companyProfile._branch_name = MyLib._myGlobal._fixBranchName;
                    MyLib._myGlobal._branchCode = _g.g._companyProfile._branch_code;
                }
            }
            //}

            __myFrameWork = null;

        }

        /// <summary>
        /// ดึง Format Number
        /// </summary>
        /// <param name="mode">1=Qty,2=Price,3=Amount,xx=กำหนดเอง</param>
        /// <returns></returns>
        public static string _getFormatNumberStr(int mode)
        {
            return _getFormatNumberStr(mode, 2);
        }

        public static string _getFormatNumberStr(int mode, int point)
        {
            int __point = point;
            switch (mode)
            {
                case 1: __point = _companyProfile._item_qty_decimal; break;
                case 2: __point = _companyProfile._item_price_decimal; break;
                case 3: __point = _companyProfile._item_amount_decimal; break;
                case 14: __point = 14; break;
            }
            return (__point < 10) ? ("m0" + __point.ToString()) : ("m" + __point.ToString());
        }

        public static _glGetTaxGroupType _searchTaxGroup(string code)
        {
            _glGetTaxGroupType _result = new _glGetTaxGroupType();
            _result._name = "";
            _result._tax_rate = 0;
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __query = "select " + _g.d.gl_tax_group._name_1 + "," + _g.d.gl_tax_group._tax_rate + " from " + _g.d.gl_tax_group._table + " where " + _g.d.gl_tax_group._code + "=\'" + code + "\'";
            try
            {
                DataSet _dataResult = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                try
                {
                    if (code.Length > 0)
                    {
                        _result._name = _dataResult.Tables[0].Rows[0][0].ToString();
                        _result._tax_rate = MyLib._myGlobal._decimalPhase(_dataResult.Tables[0].Rows[0][1].ToString());
                    }
                    else
                    {
                        _result._tax_rate = _default_vat_rate;
                    }
                }
                catch
                {
                    if (code.Length > 0)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ กลุ่มภาษี"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    _result._tax_rate = _default_vat_rate;
                }
                _dataResult.Dispose();
                _dataResult = null;
            }
            catch
            {
            }
            __myFrameWork = null;
            return (_result);
        }

        public static void _checkPeriod()
        {
            if (_g.g._accountPeriodDateBegin.Count == 0)
            {
                MessageBox.Show(MyLib._myGlobal._mainForm, MyLib._myGlobal._resource("ยังไม่ได้กำหนดงวดบัญชี\nแนะนำให้กำหนดก่อน"), MyLib._myGlobal._resource("เตือน"));
            }
        }

        public static void _accountPeriodGet()
        {
            _accountPeriodDateBegin.Clear();
            _accountPeriodDateEnd.Clear();
            _accountPeriods.Clear();
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __query = "select " + _g.d.erp_account_period._date_start + "," + _g.d.erp_account_period._date_end + "," + _g.d.erp_account_period._period_number + "," + _g.d.erp_account_period._period_year + "," + _g.d.erp_account_period._status + " from " + _g.d.erp_account_period._table + " order by " + _g.d.erp_account_period._period_number;
                DataSet __getData = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                if (__getData.Tables.Count > 0)
                {
                    IFormatProvider __cultureEng = new CultureInfo("en-US");
                    for (int __loop = 0; __loop < __getData.Tables[0].Rows.Count; __loop++)
                    {
                        DateTime __getDateBeginTmp = Convert.ToDateTime(__getData.Tables[0].Rows[__loop][0].ToString(), __cultureEng);
                        DateTime __getDateBegin = new DateTime(__getDateBeginTmp.Year, __getDateBeginTmp.Month, __getDateBeginTmp.Day, 0, 0, 0); //  Convert.ToDateTime(__getData.Tables[0].Rows[__loop][0].ToString(), __cultureEng);
                        DateTime __getDateEndTmp = Convert.ToDateTime(__getData.Tables[0].Rows[__loop][1].ToString(), __cultureEng);
                        DateTime __getDateEnd = new DateTime(__getDateEndTmp.Year, __getDateEndTmp.Month, __getDateEndTmp.Day, 23, 59, 59);  //Convert.ToDateTime(__getData.Tables[0].Rows[__loop][1].ToString(), __cultureEng);
                        _accountPeriodDateBegin.Add(__getDateBegin);
                        _accountPeriodDateEnd.Add(__getDateEnd);
                        _accountPeriodClass __accountPeriod = new _accountPeriodClass();
                        __accountPeriod._number = MyLib._myGlobal._intPhase(__getData.Tables[0].Rows[__loop][_g.d.erp_account_period._period_number].ToString());
                        __accountPeriod._beginDate = __getDateBegin;
                        __accountPeriod._endDate = __getDateEnd;
                        __accountPeriod._year = MyLib._myGlobal._intPhase(__getData.Tables[0].Rows[__loop][_g.d.erp_account_period._period_year].ToString());
                        __accountPeriod._status = MyLib._myGlobal._intPhase(__getData.Tables[0].Rows[__loop][_g.d.erp_account_period._status].ToString());

                        _accountPeriods.Add(__accountPeriod);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// เช็คเอกสารอยู่ในงวดบัญชีหรือไม่
        /// </summary>
        /// <returns></returns>
        public static Boolean _checkOpenPeriod()
        {
            return _checkOpenPeriod(MyLib._myGlobal._workingDate);
        }

        public static Boolean _checkOpenPeriod(DateTime docDate)
        {
            Boolean __result = false;
            int __accountPeriod = _accountPeriodFind(docDate);
            if (__accountPeriod != -1)
            {
                _accountPeriodClass __period = (_accountPeriodClass)_accountPeriods[__accountPeriod - 1];
                if (__period._status == 0)
                {
                    __result = true;
                }
            }

            if (__result == false)
            {
                MessageBox.Show("คำเตือน!\nวันที่ปัจจุบัน หรือที่เอกสารที่กำหนด ได้ถูกปิดงวดไปแล้ว หรือยังไม่ได้กำหนดงวดบัญชี\nกรุณาตรวจสอบ", "ตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return __result;
        }



        public static int _accountPeriodFind(DateTime date)
        {
            int __result = -1;
            try
            {
                for (int __loop = 0; __loop < _accountPeriodDateBegin.Count; __loop++)
                {
                    DateTime __getDateBegin = (DateTime)_accountPeriodDateBegin[__loop];
                    DateTime __getDateEnd = (DateTime)_accountPeriodDateEnd[__loop];
                    if (date.CompareTo(__getDateBegin) >= 0 && date.CompareTo(__getDateEnd) <= 0)
                    {
                        __result = __loop + 1;
                        break;
                    }
                }
            }
            catch
            {
            }
            return (__result);
        }

        public static _accountPeriodClass _accountPeriodClassFind(DateTime date)
        {
            _accountPeriodClass __result = null;
            try
            {
                for (int __loop = 0; __loop < _accountPeriods.Count; __loop++)
                {
                    DateTime __getDateBegin = ((_accountPeriodClass)_accountPeriods[__loop])._beginDate;
                    DateTime __getDateEnd = ((_accountPeriodClass)_accountPeriods[__loop])._endDate;
                    if (date.CompareTo(__getDateBegin) >= 0 && date.CompareTo(__getDateEnd) <= 0)
                    {
                        __result = (_accountPeriodClass)_accountPeriods[__loop];
                        break;
                    }
                }
            }
            catch
            {
            }
            return (__result);
        }

        public static MyLib.BeforeDisplayRowReturn _glListChartOfAccountBeforeDisplay(MyLib._myGrid sender, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData)
        {
            MyLib._myGrid._columnType __getColumn = new MyLib._myGrid._columnType();
            if (columnName.CompareTo(_g.d.gl_list_view._table + "." + _g.d.gl_list_view._account_name) == 0)
            {
                for (int __loop = 0; __loop < sender._columnList.Count; __loop++)
                {
                    __getColumn = (MyLib._myGrid._columnType)sender._columnList[__loop];
                    if (__getColumn._originalName.CompareTo(_g.d.gl_list_view._account_level) == 0)
                    {
                        try
                        {
                            StringBuilder addStr = new StringBuilder();
                            for (int count = 0; count < (int)rowData[__loop]; count++)
                            {
                                addStr.Append("   ");
                            }
                            ((ArrayList)senderRow.newData)[columnNumber] = (string)addStr.ToString() + ((ArrayList)senderRow.newData)[columnNumber].ToString();
                        }
                        catch { }
                        break;
                    }
                }
            }
            for (int __loop = 0; __loop < sender._columnList.Count; __loop++)
            {
                __getColumn = (MyLib._myGrid._columnType)sender._columnList[__loop];
                if (__getColumn._originalName.CompareTo(_g.d.gl_list_view._account_status) == 0)
                {
                    try
                    {
                        if ((int)(((ArrayList)senderRow.newData)[__loop]) == 1)
                        {
                            senderRow.newFont = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
                        }
                        else
                            if ((int)(((ArrayList)senderRow.newData)[__loop]) == 2)
                        {
                            senderRow.newFont = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
                            senderRow.newColor = Color.Blue;
                        }
                    }
                    catch { }
                    break;
                }
            }
            return (senderRow);
        }

        public static MyLib.BeforeDisplayRowReturn _chartOfAccountBeforeDisplay(MyLib._myGrid sender, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData)
        {
            MyLib._myGrid._columnType __getColumn = new MyLib._myGrid._columnType();
            if (columnName.CompareTo(d.gl_chart_of_account._table + "." + d.gl_chart_of_account._name_1) == 0 || columnName.CompareTo(d.gl_chart_of_account._table + "." + d.gl_chart_of_account._name_2) == 0)
            {
                for (int __loop = 0; __loop < sender._columnList.Count; __loop++)
                {
                    __getColumn = (MyLib._myGrid._columnType)sender._columnList[__loop];
                    if (__getColumn._originalName.CompareTo(d.gl_chart_of_account._account_level) == 0)
                    {
                        try
                        {
                            StringBuilder addStr = new StringBuilder();
                            for (int count = 0; count < (int)rowData[__loop]; count++)
                            {
                                addStr.Append(" ");
                            }
                            ((ArrayList)senderRow.newData)[columnNumber] = (string)addStr.ToString() + ((ArrayList)senderRow.newData)[columnNumber].ToString();
                        }
                        catch { }
                        break;
                    }
                }
            }
            for (int __loop = 0; __loop < sender._columnList.Count; __loop++)
            {
                __getColumn = (MyLib._myGrid._columnType)sender._columnList[__loop];
                if (__getColumn._originalName.CompareTo(d.gl_chart_of_account._status) == 0)
                {
                    try
                    {
                        if ((int)(((ArrayList)senderRow.newData)[__loop]) == 1)
                        {
                            senderRow.newFont = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
                        }
                    }
                    catch { }
                    break;
                }
            }
            return (senderRow);
        }

        public static String _getItemRepack(ArrayList source)
        {
            ArrayList __buildList = new ArrayList();
            StringBuilder __result = new StringBuilder();
            for (int __loop1 = 0; __loop1 < source.Count; __loop1++)
            {
                Boolean __found = false;
                for (int __loop2 = 0; __loop2 < __buildList.Count; __loop2++)
                {
                    if (__buildList[__loop2].ToString().Equals(source[__loop1].ToString()))
                    {
                        __found = true;
                        break;
                    }
                }
                if (__found == false)
                {
                    if (source[__loop1].ToString().Trim().Length > 0)
                    {
                        if (__result.Length > 0)
                        {
                            __result.Append(",");
                        }
                        __result.Append("\'" + source[__loop1].ToString() + "\'");
                        __buildList.Add(source[__loop1].ToString());
                    }
                }
            }
            return __result.ToString();
        }

        // ตรวจสอบการสำรองข้อมูล
        public static void _checkBackup()
        {
            try
            {
                if (MyLib._myGlobal._OEMVersion.Equals("tvdirect") || MyLib._myGlobal._OEMVersion.Equals("imex") || MyLib._myGlobal._OEMVersion.Equals("ims") ||
                    MyLib._myGlobal._webServiceServer.ToLower().IndexOf("www.smlsoft.com:8080") != -1 ||
                    MyLib._myGlobal._webServiceServer.ToLower().IndexOf("www.smldatacenter.com") != -1)
                    return;


                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __query = "select now(),coalesce(" + _g.d.erp_company_profile._last_backup + ",'2000-1-1') as " + _g.d.erp_company_profile._last_backup + " from " + _g.d.erp_company_profile._table;
                DataTable __getData = __myFrameWork._queryShort(__query).Tables[0];
                if (__getData.Rows.Count > 0)
                {
                    DateTime __dateNow = MyLib._myGlobal._convertDateFromQuery(__getData.Rows[0][0].ToString());
                    DateTime __dateLastBackup = MyLib._myGlobal._convertDateFromQuery(__getData.Rows[0][1].ToString());
                    if (__dateNow.Year != __dateLastBackup.Year || __dateNow.Month != __dateLastBackup.Month || __dateNow.Day != __dateLastBackup.Day)
                    {
                        if (MessageBox.Show("กรุณาสำรองข้อมูลประจำวัน ต้องการดูคู่มือการสำรองข้อมูลหรือไม่", "เตือน", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start("http://www.smlsoft.com/manual/backup-restore/sml-backup-03");
                        }
                    }
                }

            }
            catch
            {
            }
        }

        public static System.Drawing.Bitmap _resizeImage(System.Drawing.Image image, int width, int height)
        {
            //a holder for the result
            Bitmap result = new Bitmap(width, height);
            //set the resolutions the same to avoid cropping due to resolution differences
            result.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            //use a graphics object to draw the resized image into the bitmap
            using (Graphics graphics = Graphics.FromImage(result))
            {
                //set the resize quality modes to high quality
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //draw the image into the target bitmap
                graphics.DrawImage(image, 0, 0, result.Width, result.Height);
            }

            //return the resulting bitmap
            return result;
        }

        public class BMPXMLSerialization
        {
            public int Width;
            public int Height;
            private Bitmap bitmapObject;

            //XmlSerializer can only serialize classes that have default constructor
            public BMPXMLSerialization()
            {
                bitmapObject = null;
            }

            public BMPXMLSerialization(Bitmap bmpToSerialize)
            {

                bitmapObject = bmpToSerialize;
                Width = bmpToSerialize.Width;
                Height = bmpToSerialize.Height;
            }

            public byte[] BMPBytes
            {
                get
                {
                    MemoryStream memStream = new MemoryStream();
                    bitmapObject.Save(memStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    int ImageSize = Convert.ToInt32(memStream.Length);
                    byte[] ImageBytes = new byte[ImageSize];
                    memStream.Position = 0;
                    memStream.Read(ImageBytes, 0, ImageSize);
                    memStream.Close();
                    return ImageBytes;
                }

                set
                {
                    bitmapObject = new Bitmap(new MemoryStream(value));
                }
            }

            [XmlIgnore]
            public Bitmap Bitmap
            {
                get
                {
                    return bitmapObject;
                }
            }
        }

        public static MyLib.SMLJAVAWS.imageType[] _listType;
        public static MyLib.SMLJAVAWS.imageType _typeImage;
    }
}
