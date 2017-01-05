using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Collections;
using System.IO.Ports;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using MyLib;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Xml.Serialization;
using Crom.Controls.Docking;
using System.Xml;
using System.Globalization;
using SMLReport;
using System.Media;
using System.Text.RegularExpressions;

namespace SMLPosClient
{
    public class _processItem
    {
        public event sendMessageEvent _sendMessage;
        public event ProcessPromotionErrorErgs _promotionErrorMessage;
        public decimal _promotionDiscountAmount = 0;
        /// <summary>รายการสินค้า</summary>
        private List<_posClientItemClass> _itemList;
        /// <summary>รายการสินค้าที่ Process แล้ว</summary>
        private List<_posClientItemClass> _itemProcess;
        /// <summary>Option สำหรับเลือกประมวลผล Promotion หรือไม่</summary>
        private Boolean _isProcessPromotion = true;
        private DataTable _promotionFormula;
        /// <summary>รหัสลูกหนี้</summary>
        private string _custCode = "";
        private string _memberCode = "";
        /// <summary>รหัสลูกค้าเริ่มต้น</summary>
        private string _posDefaultCust = "";
        private DataTable _promotionFormulaCondition;
        private DataTable _promotionFormulaAction;
        private DataTable _promotionFormulaGroup;
        /// <summary>Format ราคา ทดนิยม 2 ตำแนห่ง</summary>
        private string _formatNumber1;
        private bool _showMessage;
        private int _row;
        private string _price_type_column;
        List<_promotionResultClass> _promotionDiscountResult;
        /// <summary>
        /// ยอดแต้มที่คำนวณได้
        /// </summary>
        public decimal _pointSum = 0M;
        public decimal _pointDivAmount = 0M;
        /// <summary>มูลค่าส่วนลดอาหาร</summary>
        public string _foodDiscountWord = "";
        /// <summary>ส่วนลดอาหาร</summary>
        public decimal _foodDiscountAmount = 0M;
        /// <summary>ส่วนลดท้ายบิล</summary>
        public string _discountWord = "";
        /// <summary>POS Config</summary>
        public SMLPOSControl._posScreenConfig _posConfig = new SMLPOSControl._posScreenConfig();
        /// <summary>
        /// ยอดแต้มยกมา
        /// </summary>
        public decimal _pointBalance = 0M;
        /// <summary>มูลค่าส่วนลดท้ายบิล</summary>
        public decimal _discountAmount = 0M;
        /// <summary>ยอดรวมทั้งสิ้น</summary>
        public decimal _totalAmount = 0M;
        /// <summary>ยอดรวมสุทธิ</summary>
        public decimal _finalBalance = 0M;
        /// <summary>ยอดปัดเศษ</summary>
        public decimal _diffAmount = 0M;
        /// <summary>ยอดรวมสินค้ามีภาษี</summary>
        public decimal _totalAmountVat = 0M;
        /// <summary>ยอดรวมสินค้ายกเว้นภาษี</summary>
        public decimal _totalAmountExceptVat = 0M;
        /// <summary>รวมส่วนลดพิเศษ</summary>
        public decimal _totalExtraDiscountAmount = 0M;
        /// <summary>ยอดรวมอาหาร</summary>
        public decimal _totalFoodAmount = 0M;
        /// <summary>ยอดรวมเครื่องดื่ม</summary>
        public decimal _totalDrinkAmount = 0M;
        /// <summary>ยอดรายการรที่ลดได้</summary>
        public decimal _totalBeforeDiscount = 0M;

        /// <summary>มูลค่า Service Charge (%)</summary>
        public string _serviceChargeWord = "";

        /// <summary>มูลค่า Service Charge</summary>
        public decimal _serviceChargeAmount = 0M;

        public Boolean _usePointBirthDay = false;
        public decimal _pointMultiplyBirthDay = 0M;

        public Boolean _usePointMonthBirth = false;
        public decimal _pointMultiplyMonthBirth = 0M;

        public decimal _pointSpecialDayMultiply = 0M;

        public Boolean _isBirthDayMember = false;
        public Boolean _isBirthMonthMember = false;

        public decimal _vatExcludeAmount = 0M;
        public Boolean _point_member_only = false;

        public decimal _promotionPassAmount = 0M;

        public _processItem(List<_posClientItemClass> itemProcess, Boolean isProcessPromotion, List<_posClientItemClass> itemList, String custCode, String memberCode, String posDefaultCust, DataTable promotionFormula, DataTable promotionFormulaCondition, DataTable promotionFormulaAction, DataTable promotionFormulaGroup, List<_promotionResultClass> promotionDiscountResult, string formatNumber1, bool showMessage, int row, string price_type_column, decimal point_balance, decimal pointDivAmount)
        {
            this._itemProcess = itemProcess;
            this._isProcessPromotion = isProcessPromotion;
            this._itemList = itemList;
            this._custCode = custCode;
            this._memberCode = memberCode;
            this._posDefaultCust = posDefaultCust;
            this._promotionDiscountResult = promotionDiscountResult;
            this._promotionFormula = promotionFormula;
            this._promotionFormulaCondition = promotionFormulaCondition;
            this._promotionFormulaAction = promotionFormulaAction;
            this._promotionFormulaGroup = promotionFormulaGroup;
            this._formatNumber1 = formatNumber1;
            this._showMessage = showMessage;
            this._row = row;
            this._price_type_column = price_type_column;
            this._pointBalance = point_balance;
            this._pointDivAmount = pointDivAmount;
        }

        public string _start()
        {
            this._promotionDiscountAmount = 0;
            this._promotionPassAmount = 0m;

            // toe เพิ่ม option ประมวลผลโปรโมชั่นหรือไม่
            if (this._isProcessPromotion)
            {
                #region ประมวลผล Promotion

                List<_posClientItemClass> __itemListTemp = new List<_posClientItemClass>();
                for (int __loop1 = 0; __loop1 < _itemList.Count; __loop1++)
                {
                    Boolean __found = false;
                    decimal __calcQty = ((this._itemList[__loop1]._qty * ((_g.g._companyProfile._promotion_fixed_unitcode == true) ? 1 : (this._itemList[__loop1]._standValue / this._itemList[__loop1]._divideValue))));  // ปรับหน่วย
                    for (int __loop2 = 0; __loop2 < __itemListTemp.Count; __loop2++)
                    {
                        if (this._itemList[__loop1]._itemCode.Equals(__itemListTemp[__loop2]._itemCode))
                        {
                            if (_g.g._companyProfile._promotion_fixed_unitcode == true && this._itemList[__loop1]._unitCode.Equals(__itemListTemp[__loop2]._unitCode) ||
                                _g.g._companyProfile._promotion_fixed_unitcode == false
                                )
                            {
                                __found = true;
                                __itemListTemp[__loop2]._promotion_qty_balance += __calcQty;
                                __itemListTemp[__loop2]._promotionAmount += this._itemList[__loop1]._amount;
                                break;
                            }
                        }
                    }
                    if (__found == false)
                    {
                        __itemListTemp.Add(this._itemList[__loop1]);
                        __itemListTemp[__itemListTemp.Count - 1]._promotion_qty_balance = __calcQty;
                        __itemListTemp[__itemListTemp.Count - 1]._promotionAmount = this._itemList[__loop1]._amount;

                    }
                }

                if (this._promotionFormula != null && this._promotionFormula.Rows.Count > 0)
                {
                    // รวมสินค้า ตามรหัสสินค้า เพื่อรวมจำนวนเอาไปประมวลผล Promotion

                    // ประมวลผล Promotion
                    for (int __loopFormula = 0; __loopFormula < this._promotionFormula.Rows.Count; __loopFormula++)
                    {


                        string __promotionCode = this._promotionFormula.Rows[__loopFormula][_g.d.ic_promotion_formula._code].ToString();
                        string __promotionName = this._promotionFormula.Rows[__loopFormula][_g.d.ic_promotion_formula._name_1].ToString();
                        Boolean __promotionNoDiscount = this._promotionFormula.Rows[__loopFormula][_g.d.ic_promotion_formula._is_no_discount].ToString().Equals("1") ? true : false;
                        int __useDateFilter = MyLib._myGlobal._intPhase(this._promotionFormula.Rows[__loopFormula][_g.d.ic_promotion_formula._use_date_range].ToString());
                        DateTime __fromDate = MyLib._myGlobal._convertDateFromQuery(this._promotionFormula.Rows[__loopFormula][_g.d.ic_promotion_formula._from_date].ToString());
                        DateTime __to_date = MyLib._myGlobal._convertDateFromQuery(this._promotionFormula.Rows[__loopFormula][_g.d.ic_promotion_formula._to_date].ToString());

                        Boolean __normal_price_only = this._promotionFormula.Rows[__loopFormula][_g.d.ic_promotion_formula._item_normal_price].ToString().Equals("1") ? true : false;

                        string __lock_day = this._promotionFormula.Rows[__loopFormula][_g.d.ic_promotion_formula._lock_day].ToString();

                        try
                        {
                            // ปรับให้เป็น 23.59.59
                            __to_date = __to_date.AddDays(1).AddSeconds(-1);
                        }
                        catch
                        {
                        }
                        DateTime __today = DateTime.Now;

                        if (__useDateFilter == 0 ||
                            (__useDateFilter == 1 && (__fromDate <= __today) && (__to_date >= __today))
                            )
                        {
                            if (__lock_day == "" || __lock_day.IndexOf(((int)DateTime.Now.DayOfWeek).ToString()) != -1)
                            {
                                try
                                {
                                    //Boolean __isSumUnit = this._promotionFormula.Rows[__loopFormula][_g.d.ic_promotion_formula._auto_change_unitcode].ToString().Equals("1") ? true : false;

                                    // 0 ทั่วไป, 1 สมาชิก ,2 ไม่ใช่สมาชิก
                                    int _member_check = (int)MyLib._myGlobal._decimalPhase(this._promotionFormula.Rows[__loopFormula][_g.d.ic_promotion_formula._member_condition].ToString());
                                    int __promotionCase = (int)MyLib._myGlobal._decimalPhase(this._promotionFormula.Rows[__loopFormula][_g.d.ic_promotion_formula._case_number].ToString());

                                    // toe check เงื่อนไขสมาชิกก่อน
                                    if (_member_check == 0 || (_member_check == 1 && this._memberCode.Length > 0 && (this._memberCode.Equals(this._posDefaultCust) == false)) || (_member_check == 2 && this._memberCode.Length == 0)) // this._custCode.Equals(this._posDefaultCust)
                                    {
                                        //MessageBox.Show(" match Member");
                                        switch (__promotionCase)
                                        {
                                            #region 2. ส่วนลดตามจำนวนเต็ม
                                            case 1: // ส่วนลดตามจำนวนเต็ม
                                                {
                                                    if (_promotionFormulaCondition != null && _promotionFormulaCondition.Rows.Count > 0)
                                                    {
                                                        DataRow[] __condition = this._promotionFormulaCondition.Select(_g.d.ic_promotion_formula_condition._code + "=\'" + __promotionCode + "\'");

                                                        decimal __no_discount_amount_promotion = 0M;

                                                        //Boolean __isSumUnit = __condition[0][_g.d.ic_promotion_formula_condition.
                                                        decimal __totalQty = 0M;
                                                        for (int __row1 = 0; __row1 < __condition.Length; __row1++)
                                                        {
                                                            for (int __row2 = 0; __row2 < __itemListTemp.Count; __row2++)
                                                            {
                                                                if (__normal_price_only == false || ((__itemListTemp[__row2]._price_type == 1 || __itemListTemp[__row2]._price_type == 5) && __itemListTemp[__row2]._discountNumber == 0))
                                                                {
                                                                    if (__condition[__row1][_g.d.ic_promotion_formula_condition._condition_from].ToString().Equals(__itemListTemp[__row2]._itemCode) && __itemListTemp[__row2]._promotion_qty_balance > 0)
                                                                    {

                                                                        if ((_g.g._companyProfile._promotion_fixed_unitcode == true && __condition[__row1][_g.d.ic_promotion_formula_condition._condition_unit_code].ToString().Equals(__itemListTemp[__row2]._unitCode)) || _g.g._companyProfile._promotion_fixed_unitcode == false)
                                                                        {
                                                                            // หาจำนวน pro ไม่ลด
                                                                            if ((__promotionNoDiscount == true) &&
                                                                                    (
                                                                                        ((MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro) && __itemListTemp[__row2]._drink_type == 0) ||
                                                                                        (MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong && MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong)
                                                                                    )
                                                                                )
                                                                            {
                                                                                __no_discount_amount_promotion -= (__itemListTemp[__row2]._promotion_qty_balance * __itemListTemp[__row2]._price);
                                                                            }

                                                                            __totalQty += __itemListTemp[__row2]._promotion_qty_balance;
                                                                            __itemListTemp[__row2]._promotion_qty_balance = 0m;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        if (__totalQty > 0m)
                                                        {
                                                            // toe หา no discount

                                                            // มีรายการส่วนลด (เอาจากมากไปหาน้อย)
                                                            _promotionResultClass __newResult = new _promotionResultClass();
                                                            __newResult._promotionCode = __promotionCode;
                                                            __newResult._promotionName = __promotionName;
                                                            decimal __calcQty = __totalQty;
                                                            DataRow[] __action = this._promotionFormulaAction.Select(_g.d.ic_promotion_formula_action._code + "=\'" + __promotionCode + "\'");
                                                            bool __addPromotion = false;
                                                            for (int __row = __action.Length - 1; __row >= 0; __row--)
                                                            {
                                                                decimal __qty = MyLib._myGlobal._decimalPhase(__action[__row][_g.d.ic_promotion_formula_action._qty_from].ToString());
                                                                if (__qty > 0)
                                                                {
                                                                    decimal __calc = Math.Floor(__calcQty / __qty);
                                                                    if (__calc > 0)
                                                                    {
                                                                        __calcQty -= (__calc * __qty);
                                                                        __newResult._amount += (MyLib._myGlobal._decimalPhase(__action[__row][_g.d.ic_promotion_formula_action._action_command].ToString()) * __calc);
                                                                        __newResult._qty += __calc;
                                                                        __addPromotion = true;
                                                                    }
                                                                }
                                                            }

                                                            // กรณีมีจำนวนเหลือ ให้คือกลับไป
                                                            while (__calcQty > 0)
                                                            {
                                                                for (int __row1 = 0; __row1 < __condition.Length; __row1++)
                                                                {
                                                                    for (int __row2 = __itemListTemp.Count - 1; __row2 >= 0; __row2--)
                                                                    {
                                                                        if (__normal_price_only == false || (__itemListTemp[__row2]._price_type == 1 || __itemListTemp[__row2]._price_type == 5))
                                                                        {
                                                                            if (__condition[__row1][_g.d.ic_promotion_formula_condition._condition_from].ToString().Equals(__itemListTemp[__row2]._itemCode))
                                                                            {
                                                                                // fix หน่วย
                                                                                if (__calcQty <= __itemListTemp[__row2]._qty * ((_g.g._companyProfile._promotion_fixed_unitcode == true) ? 1 : (__itemListTemp[__row2]._standValue / __itemListTemp[__row2]._divideValue)))
                                                                                {
                                                                                    // toe คืนมูลค่า
                                                                                    if ((__promotionNoDiscount == true) &&
                                                                                        (
                                                                                            ((MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro) && __itemListTemp[__row2]._drink_type == 0) ||
                                                                                            (MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong && MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong)
                                                                                        )
                                                                                    )
                                                                                    {
                                                                                        __no_discount_amount_promotion += (__calcQty * __itemListTemp[__row2]._price);
                                                                                    }

                                                                                    __itemListTemp[__row2]._promotion_qty_balance = __calcQty;
                                                                                    __calcQty = 0;
                                                                                }
                                                                                else
                                                                                {
                                                                                    // toe คืนมูลค่า
                                                                                    if ((__promotionNoDiscount == true) &&
                                                                                        (
                                                                                            ((MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro) && __itemListTemp[__row2]._drink_type == 0) ||
                                                                                            (MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong && MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong)
                                                                                        ))
                                                                                    {
                                                                                        __no_discount_amount_promotion += (__itemListTemp[__row2]._qty * __itemListTemp[__row2]._price);
                                                                                    }

                                                                                    __itemListTemp[__row2]._promotion_qty_balance = __itemListTemp[__row2]._qty;
                                                                                    __calcQty -= __itemListTemp[__row2]._qty;
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                            if (__addPromotion)
                                                            {
                                                                __newResult._no_discount_amount = __no_discount_amount_promotion;
                                                                this._promotionDiscountAmount += __newResult._amount;
                                                                _promotionDiscountResult.Add(__newResult);
                                                            }

                                                        }
                                                    }
                                                }
                                                break;
                                            #endregion
                                            #region 3. ส่วนลดสินค้าจัดชุด
                                            case 2: // ส่วนลดสินค้าจัดชุด
                                                {
                                                    if (_promotionFormulaCondition != null && _promotionFormulaCondition.Rows.Count > 0)
                                                    {
                                                        DataRow[] __condition = this._promotionFormulaCondition.Select(_g.d.ic_promotion_formula_condition._code + "=\'" + __promotionCode + "\'");
                                                        int __itemCodeColumn = this._promotionFormulaCondition.Columns.IndexOf(_g.d.ic_promotion_formula_condition._condition_from);
                                                        int __groupNumberColumn = this._promotionFormulaCondition.Columns.IndexOf(_g.d.ic_promotion_formula_condition._condition_group);
                                                        int __qtyColumnNumber = this._promotionFormulaCondition.Columns.IndexOf(_g.d.ic_promotion_formula_condition._condition_value_to);
                                                        _promotionResultClass __newResult = new _promotionResultClass();
                                                        __newResult._promotionCode = __promotionCode;
                                                        __newResult._promotionName = __promotionName;
                                                        Boolean __havePromotion = false;
                                                        // หาว่ามีกี่กลุ่ม
                                                        List<int> __groupList = new List<int>();
                                                        for (int __row = 0; __row < __condition.Length; __row++)
                                                        {
                                                            int __groupNumber = (int)MyLib._myGlobal._decimalPhase(__condition[__row][__groupNumberColumn].ToString());
                                                            if (__condition[__row][__itemCodeColumn].ToString().Length > 0)
                                                            {
                                                                bool __found = false;
                                                                for (int __find = 0; __find < __groupList.Count; __find++)
                                                                {
                                                                    if (__groupList[__find] == __groupNumber)
                                                                    {
                                                                        __found = true;
                                                                        break;
                                                                    }
                                                                }
                                                                if (__found == false)
                                                                {
                                                                    __groupList.Add(__groupNumber);
                                                                }
                                                            }
                                                        }
                                                        bool __promotionPass = true;
                                                        while (__promotionPass && __groupList.Count > 0)
                                                        {
                                                            List<decimal> __gridLineQty = new List<decimal>();
                                                            for (int __row = 0; __row < __itemListTemp.Count; __row++)
                                                            {
                                                                __gridLineQty.Add(0m);
                                                            }
                                                            List<bool> __groupCheck = new List<bool>();
                                                            for (int __groupLoop = 0; __groupLoop < __groupList.Count; __groupLoop++)
                                                            {
                                                                __groupCheck.Add(false);
                                                                for (int __conditionLoop = 0; __conditionLoop < __condition.Length && __groupCheck[__groupLoop] == false; __conditionLoop++)
                                                                {
                                                                    if (__groupList[__groupLoop] == (int)MyLib._myGlobal._decimalPhase(__condition[__conditionLoop][__groupNumberColumn].ToString()))
                                                                    {
                                                                        decimal __conditionQty = MyLib._myGlobal._decimalPhase(__condition[__conditionLoop][__qtyColumnNumber].ToString());
                                                                        for (int __row = 0; __row < __itemListTemp.Count; __row++)
                                                                        {
                                                                            if (__normal_price_only == false || ((__itemListTemp[__row]._price_type == 1 || __itemListTemp[__row]._price_type == 5 || __itemListTemp[__row]._price_type == 7) && __itemListTemp[__row]._discountNumber == 0))
                                                                            {
                                                                                if (__itemListTemp[__row]._itemCode.Equals(__condition[__conditionLoop][__itemCodeColumn].ToString()))
                                                                                {
                                                                                    // toe fix หน่วยนับ
                                                                                    if ((_g.g._companyProfile._promotion_fixed_unitcode == true && __condition[__conditionLoop][_g.d.ic_promotion_formula_condition._condition_unit_code].ToString().Equals(__itemListTemp[__row]._unitCode)) || _g.g._companyProfile._promotion_fixed_unitcode == false)
                                                                                    {

                                                                                        if ((__itemListTemp[__row]._promotion_qty_balance - __gridLineQty[__row]) >= __conditionQty)
                                                                                        {
                                                                                            // toe check pro no discount && ver != tomyumgoong || tomyumgoong && isfood
                                                                                            if ((__promotionNoDiscount == true) &&
                                                                                                (
                                                                                                    ((MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro) && __itemListTemp[__row]._drink_type == 0) ||
                                                                                                    (MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong && MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong)
                                                                                                ))
                                                                                            {
                                                                                                __newResult._no_discount_amount -= (__itemListTemp[__row]._price * (__itemListTemp[__row]._promotion_qty_balance - __gridLineQty[__row]));
                                                                                            }

                                                                                            __gridLineQty[__row] += __conditionQty;
                                                                                            __groupCheck[__groupLoop] = true;
                                                                                            break;
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            // ดูว่าเข้าเงื่อนครบทุกกลุ่ม
                                                            for (int __groupLoop = 0; __groupLoop < __groupList.Count; __groupLoop++)
                                                            {
                                                                if (__groupCheck[__groupLoop] == false)
                                                                {
                                                                    __promotionPass = false;
                                                                    break;
                                                                }
                                                            }
                                                            if (__promotionPass)
                                                            {
                                                                // เข้าเงื่อนไข
                                                                __havePromotion = true;
                                                                DataRow[] __action = this._promotionFormulaAction.Select(_g.d.ic_promotion_formula_action._code + "=\'" + __promotionCode + "\'");
                                                                __newResult._amount += MyLib._myGlobal._decimalPhase(__action[0][_g.d.ic_promotion_formula_action._action_command].ToString());
                                                                __newResult._qty += 1;
                                                                // ลดตัวเลขคงเหลือสำหรับ Promotion ต่อไป
                                                                for (int __row = 0; __row < __itemListTemp.Count; __row++)
                                                                {
                                                                    if (__normal_price_only == false || ((__itemListTemp[__row]._price_type == 1 || __itemListTemp[__row]._price_type == 5 || __itemListTemp[__row]._price_type == 7) && __itemListTemp[__row]._discountNumber == 0))
                                                                    {
                                                                        // ตัดจำนวน ของ Pro ต่อไป
                                                                        if ((__gridLineQty[__row] > 0) &&
                                                                            ((__itemListTemp[__row]._promotion_qty_balance - __gridLineQty[__row]) > 0) &&
                                                                            (__promotionNoDiscount == true) &&
                                                                            (
                                                                                ((MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro) && __itemListTemp[__row]._drink_type == 0) ||
                                                                                (MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong && MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong)
                                                                            ))
                                                                        {
                                                                            __newResult._no_discount_amount += (__itemListTemp[__row]._price * (__itemListTemp[__row]._promotion_qty_balance - __gridLineQty[__row]));
                                                                        }

                                                                        __itemListTemp[__row]._promotion_qty_balance -= __gridLineQty[__row];
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {

                                                                // ตัดมูลค่าคงเหลือ ทีเข้ากลุ่มออกไป
                                                                __gridLineQty = new List<decimal>();
                                                                for (int __row = 0; __row < __itemListTemp.Count; __row++)
                                                                {
                                                                    __gridLineQty.Add(0m);
                                                                }
                                                                __groupCheck = new List<bool>();
                                                                for (int __groupLoop = 0; __groupLoop < __groupList.Count; __groupLoop++)
                                                                {
                                                                    __groupCheck.Add(false);
                                                                    for (int __conditionLoop = 0; __conditionLoop < __condition.Length && __groupCheck[__groupLoop] == false; __conditionLoop++)
                                                                    {
                                                                        if (__groupList[__groupLoop] == (int)MyLib._myGlobal._decimalPhase(__condition[__conditionLoop][__groupNumberColumn].ToString()))
                                                                        {
                                                                            decimal __conditionQty = MyLib._myGlobal._decimalPhase(__condition[__conditionLoop][__qtyColumnNumber].ToString());
                                                                            for (int __row = 0; __row < __itemListTemp.Count; __row++)
                                                                            {
                                                                                if (__normal_price_only == false || ((__itemListTemp[__row]._price_type == 1 || __itemListTemp[__row]._price_type == 5 || __itemListTemp[__row]._price_type == 7) && __itemListTemp[__row]._discountNumber == 0))
                                                                                {
                                                                                    if (__itemListTemp[__row]._itemCode.Equals(__condition[__conditionLoop][__itemCodeColumn].ToString()))
                                                                                    {
                                                                                        // toe fix หน่วยนับ
                                                                                        if ((_g.g._companyProfile._promotion_fixed_unitcode == true && __condition[__conditionLoop][_g.d.ic_promotion_formula_condition._condition_unit_code].ToString().Equals(__itemListTemp[__row]._unitCode)) || _g.g._companyProfile._promotion_fixed_unitcode == false)
                                                                                        {

                                                                                            if ((__itemListTemp[__row]._promotion_qty_balance - __gridLineQty[__row]) >= __conditionQty)
                                                                                            {
                                                                                                // toe check pro no discount && ver != tomyumgoong || tomyumgoong && isfood
                                                                                                if ((__promotionNoDiscount == true) &&
                                                                                                    (
                                                                                                        ((MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro) && __itemListTemp[__row]._drink_type == 0) ||
                                                                                                        (MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong && MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong)
                                                                                                    ))
                                                                                                {
                                                                                                    __newResult._no_discount_amount += (__itemListTemp[__row]._price * (__itemListTemp[__row]._promotion_qty_balance - __gridLineQty[__row]));
                                                                                                }

                                                                                                __gridLineQty[__row] += __conditionQty;
                                                                                                __groupCheck[__groupLoop] = true;
                                                                                                break;
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }


                                                        }
                                                        if (__havePromotion)
                                                        {
                                                            this._promotionDiscountAmount += __newResult._amount;
                                                            _promotionDiscountResult.Add(__newResult);
                                                        }
                                                    }
                                                }
                                                break;
                                            #endregion
                                            #region 4. ส่วนลดสินค้าจัดชุด (รวมกลุ่ม)
                                            case 3: // ส่วนลดตามจำนวนเต็ม (จัดชุด) ตามกลุ่ม
                                                {
                                                    if (_promotionFormulaCondition != null && _promotionFormulaCondition.Rows.Count > 0)
                                                    {

                                                        //  toe fix update group = -1
                                                        for (int __loop1 = 0; __loop1 < _itemList.Count; __loop1++)
                                                        {
                                                            __itemListTemp[__loop1]._groupNumber = -1;
                                                        }

                                                        DataRow[] __condition = this._promotionFormulaCondition.Select(_g.d.ic_promotion_formula_condition._code + "=\'" + __promotionCode + "\'");
                                                        int __itemCodeColumn = this._promotionFormulaCondition.Columns.IndexOf(_g.d.ic_promotion_formula_condition._condition_from);
                                                        int __groupNumberColumn = this._promotionFormulaCondition.Columns.IndexOf(_g.d.ic_promotion_formula_condition._condition_group);
                                                        DataRow[] __conditionGroup = this._promotionFormulaGroup.Select(_g.d.ic_promotion_formula_group_qty._code + "=\'" + __promotionCode + "\'");
                                                        int __groupQtyGroupNumber = this._promotionFormulaGroup.Columns.IndexOf(_g.d.ic_promotion_formula_group_qty._group_number);
                                                        int __groupQtyQtyColumnNumber = this._promotionFormulaGroup.Columns.IndexOf(_g.d.ic_promotion_formula_group_qty._qty);
                                                        _promotionResultClass __newResult = new _promotionResultClass();
                                                        __newResult._promotionCode = __promotionCode;
                                                        __newResult._promotionName = __promotionName;
                                                        Boolean __havePromotion = false;
                                                        // 
                                                        List<int> __groupList = new List<int>();
                                                        List<decimal> __groupListQty = new List<decimal>();
                                                        for (int __row = 0; __row < __conditionGroup.Length; __row++)
                                                        {
                                                            __groupList.Add((int)MyLib._myGlobal._decimalPhase(__conditionGroup[__row][__groupQtyGroupNumber].ToString()));

                                                            decimal __groupQty = (MyLib._myGlobal._decimalPhase(__conditionGroup[__row][__groupQtyQtyColumnNumber].ToString()) == 0) ? 1 : MyLib._myGlobal._decimalPhase(__conditionGroup[__row][__groupQtyQtyColumnNumber].ToString());
                                                            __groupListQty.Add(__groupQty);
                                                        }
                                                        //
                                                        bool __promotionPass = true;
                                                        while (__promotionPass && __groupList.Count > 0)
                                                        {
                                                            // รวมจำนวนตามกลุ่ม
                                                            List<decimal> __groupListQtySum = new List<decimal>();
                                                            for (int __groupLoop = 0; __groupLoop < __groupList.Count; __groupLoop++)
                                                            {
                                                                __groupListQtySum.Add(0m);
                                                                for (int __conditionLoop = 0; __conditionLoop < __condition.Length; __conditionLoop++)
                                                                {
                                                                    if (__groupList[__groupLoop] == (int)MyLib._myGlobal._decimalPhase(__condition[__conditionLoop][__groupNumberColumn].ToString()))
                                                                    {
                                                                        for (int __row = 0; __row < __itemListTemp.Count; __row++)
                                                                        {
                                                                            if (__normal_price_only == false || ((__itemListTemp[__row]._price_type == 1 || __itemListTemp[__row]._price_type == 5 || __itemListTemp[__row]._price_type == 7) && __itemListTemp[__row]._discountNumber == 0))
                                                                            {
                                                                                if (__itemListTemp[__row]._itemCode.Equals(__condition[__conditionLoop][__itemCodeColumn].ToString()))
                                                                                {
                                                                                    // toe fix หน่วยนับ
                                                                                    if ((_g.g._companyProfile._promotion_fixed_unitcode == true && __condition[__conditionLoop][_g.d.ic_promotion_formula_condition._condition_unit_code].ToString().Equals(__itemListTemp[__row]._unitCode)) || _g.g._companyProfile._promotion_fixed_unitcode == false)
                                                                                    {

                                                                                        // add no discount
                                                                                        if (__itemListTemp[__row]._promotion_qty_balance > 0 &&
                                                                                            (__promotionNoDiscount == true) &&
                                                                                            (
                                                                                                ((MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro) && __itemListTemp[__row]._drink_type == 0) ||
                                                                                                (MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong && MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong)
                                                                                            )
                                                                                        )
                                                                                        {
                                                                                            __newResult._no_discount_amount -= (__itemListTemp[__row]._price * __itemListTemp[__row]._promotion_qty_balance);
                                                                                        }

                                                                                        __groupListQtySum[__groupLoop] += __itemListTemp[__row]._promotion_qty_balance;
                                                                                        __itemListTemp[__row]._groupNumber = __groupList[__groupLoop];
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        //__itemListTemp[__row]._groupNumber = -1;
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    //__itemListTemp[__row]._groupNumber = -1;
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            // ดูว่าเข้าเงื่อนครบทุกกลุ่ม
                                                            for (int __groupLoop = 0; __groupLoop < __groupList.Count; __groupLoop++)
                                                            {
                                                                if (__groupListQtySum[__groupLoop] < __groupListQty[__groupLoop])
                                                                {
                                                                    // toe คืนจำนวน no_discount
                                                                    if (__promotionNoDiscount == true)
                                                                    {
                                                                        for (int __groupLoop2 = 0; __groupLoop2 < __groupList.Count; __groupLoop2++)
                                                                        {
                                                                            //if (__groupListQtyTemp[__groupLoop] > 0)
                                                                            //{
                                                                            for (int __row = 0; __row < __itemListTemp.Count; __row++) // && __groupListQtyTemp[__groupLoop] > 0
                                                                            {
                                                                                if (__normal_price_only == false || ((__itemListTemp[__row]._price_type == 1 || __itemListTemp[__row]._price_type == 5 || __itemListTemp[__row]._price_type == 7) && __itemListTemp[__row]._discountNumber == 0))
                                                                                {
                                                                                    if (__itemListTemp[__row]._groupNumber == __groupList[__groupLoop2] && __itemListTemp[__row]._promotion_qty_balance > 0)
                                                                                    {
                                                                                        if ((__promotionNoDiscount == true) &&
                                                                                            (
                                                                                                ((MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro) && __itemListTemp[__row]._drink_type == 0) ||
                                                                                                (MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong && MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong)
                                                                                            )
                                                                                        )
                                                                                        {
                                                                                            __newResult._no_discount_amount += (__itemListTemp[__row]._price * __itemListTemp[__row]._promotion_qty_balance);
                                                                                        }


                                                                                    }
                                                                                }
                                                                            }
                                                                            //}
                                                                        }
                                                                    }

                                                                    __promotionPass = false;

                                                                    break;
                                                                }
                                                            }
                                                            if (__promotionPass)
                                                            {
                                                                // เข้าเงื่อนไข
                                                                __havePromotion = true;
                                                                DataRow[] __action = this._promotionFormulaAction.Select(_g.d.ic_promotion_formula_action._code + "=\'" + __promotionCode + "\'");
                                                                string __command = __action[0][_g.d.ic_promotion_formula_action._action_command].ToString().Trim();
                                                                if (__command.Length > 0)
                                                                {
                                                                    // กรณีเป็นส่วนลด
                                                                    __newResult._amount += MyLib._myGlobal._decimalPhase(__command);
                                                                    __newResult._qty += 1;

                                                                    // ลดตัวเลขคงเหลือสำหรับ Promotion ต่อไป
                                                                    List<decimal> __groupListQtyTemp = new List<decimal>();
                                                                    for (int __groupLoop = 0; __groupLoop < __groupList.Count; __groupLoop++)
                                                                    {
                                                                        __groupListQtyTemp.Add(__groupListQty[__groupLoop]);
                                                                    }
                                                                    for (int __groupLoop = 0; __groupLoop < __groupList.Count; __groupLoop++)
                                                                    {
                                                                        if (__groupListQtyTemp[__groupLoop] > 0)
                                                                        {
                                                                            for (int __row = 0; __row < __itemListTemp.Count && __groupListQtyTemp[__groupLoop] > 0; __row++)
                                                                            {
                                                                                if (__normal_price_only == false || ((__itemListTemp[__row]._price_type == 1 || __itemListTemp[__row]._price_type == 5 || __itemListTemp[__row]._price_type == 7) && __itemListTemp[__row]._discountNumber == 0))
                                                                                {
                                                                                    if (__itemListTemp[__row]._groupNumber == __groupList[__groupLoop] && __itemListTemp[__row]._promotion_qty_balance >= 0)
                                                                                    {
                                                                                        decimal __calc = __groupListQtyTemp[__groupLoop];
                                                                                        __itemListTemp[__row]._promotion_qty_balance -= __groupListQtyTemp[__groupLoop];
                                                                                        if (__itemListTemp[__row]._promotion_qty_balance < 0)
                                                                                        {
                                                                                            __groupListQtyTemp[__groupLoop] += (__itemListTemp[__row]._promotion_qty_balance * -1);
                                                                                            __itemListTemp[__row]._promotion_qty_balance = 0;
                                                                                        }
                                                                                        __groupListQtyTemp[__groupLoop] -= __calc;
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }

                                                                }
                                                                // toe คืนจำนวน no_discount
                                                                if (__promotionNoDiscount == true)
                                                                {
                                                                    for (int __groupLoop = 0; __groupLoop < __groupList.Count; __groupLoop++)
                                                                    {
                                                                        //if (__groupListQtyTemp[__groupLoop] > 0)
                                                                        //{
                                                                        for (int __row = 0; __row < __itemListTemp.Count; __row++) // && __groupListQtyTemp[__groupLoop] > 0
                                                                        {
                                                                            if (__normal_price_only == false || ((__itemListTemp[__row]._price_type == 1 || __itemListTemp[__row]._price_type == 5 || __itemListTemp[__row]._price_type == 7) && __itemListTemp[__row]._discountNumber == 0))
                                                                            {
                                                                                if (__itemListTemp[__row]._groupNumber == __groupList[__groupLoop] && __itemListTemp[__row]._promotion_qty_balance > 0)
                                                                                {
                                                                                    if ((__promotionNoDiscount == true) &&
                                                                                        (
                                                                                            ((MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro) && __itemListTemp[__row]._drink_type == 0) ||
                                                                                            (MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong && MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong)
                                                                                        )
                                                                                    )
                                                                                    {
                                                                                        __newResult._no_discount_amount += (__itemListTemp[__row]._price * __itemListTemp[__row]._promotion_qty_balance);
                                                                                    }


                                                                                }
                                                                            }
                                                                        }
                                                                        //}
                                                                    }
                                                                }

                                                            }
                                                        }
                                                        if (__havePromotion)
                                                        {
                                                            this._promotionDiscountAmount += __newResult._amount;
                                                            _promotionDiscountResult.Add(__newResult);
                                                        }
                                                    }
                                                }
                                                break;
                                            #endregion
                                            #region 5. ของแถมสินค้าจัดชุด
                                            case 4: // ของแถมตามจำนวนเต็ม (จัดชุด)
                                                {
                                                    if (_promotionFormulaCondition != null && _promotionFormulaCondition.Rows.Count > 0)
                                                    {
                                                        DataRow[] __condition = this._promotionFormulaCondition.Select(_g.d.ic_promotion_formula_condition._code + "=\'" + __promotionCode + "\'");
                                                        int __itemCodeColumn = this._promotionFormulaCondition.Columns.IndexOf(_g.d.ic_promotion_formula_condition._condition_from);
                                                        int __groupNumberColumn = this._promotionFormulaCondition.Columns.IndexOf(_g.d.ic_promotion_formula_condition._condition_group);
                                                        int __qtyColumnNumber = this._promotionFormulaCondition.Columns.IndexOf(_g.d.ic_promotion_formula_condition._condition_value_to);
                                                        _promotionResultClass __newResult = new _promotionResultClass();
                                                        __newResult._promotionCode = __promotionCode;
                                                        __newResult._promotionName = __promotionName;
                                                        Boolean __havePromotion = false;
                                                        // หาว่ามีกี่กลุ่ม
                                                        List<int> __groupList = new List<int>();
                                                        for (int __row = 0; __row < __condition.Length; __row++)
                                                        {
                                                            int __groupNumber = (int)MyLib._myGlobal._decimalPhase(__condition[__row][__groupNumberColumn].ToString());
                                                            if (__condition[__row][__itemCodeColumn].ToString().Length > 0)
                                                            {
                                                                bool __found = false;
                                                                for (int __find = 0; __find < __groupList.Count; __find++)
                                                                {
                                                                    if (__groupList[__find] == __groupNumber)
                                                                    {
                                                                        __found = true;
                                                                        break;
                                                                    }
                                                                }
                                                                if (__found == false)
                                                                {
                                                                    __groupList.Add(__groupNumber);
                                                                }
                                                            }
                                                        }
                                                        // ดูว่าแต่ละกลุ่มเข้าเงื่อนไขหรือไม่
                                                        bool __promotionPass = true;
                                                        while (__promotionPass && __groupList.Count > 0)
                                                        {
                                                            List<decimal> __gridLineQty = new List<decimal>();
                                                            for (int __row = 0; __row < __itemListTemp.Count; __row++)
                                                            {
                                                                __gridLineQty.Add(0m);
                                                            }
                                                            List<bool> __groupCheck = new List<bool>();
                                                            for (int __groupLoop = 0; __groupLoop < __groupList.Count; __groupLoop++)
                                                            {
                                                                __groupCheck.Add(false);
                                                                for (int __conditionLoop = 0; __conditionLoop < __condition.Length && __groupCheck[__groupLoop] == false; __conditionLoop++)
                                                                {
                                                                    if (__groupList[__groupLoop] == (int)MyLib._myGlobal._decimalPhase(__condition[__conditionLoop][__groupNumberColumn].ToString()))
                                                                    {
                                                                        decimal __conditionQty = MyLib._myGlobal._decimalPhase(__condition[__conditionLoop][__qtyColumnNumber].ToString());
                                                                        for (int __row = 0; __row < __itemListTemp.Count; __row++)
                                                                        {
                                                                            if (__normal_price_only == false || ((__itemListTemp[__row]._price_type == 1 || __itemListTemp[__row]._price_type == 5 || __itemListTemp[__row]._price_type == 7) && __itemListTemp[__row]._discountNumber == 0))
                                                                            {
                                                                                if (__itemListTemp[__row]._itemCode.Equals(__condition[__conditionLoop][__itemCodeColumn].ToString()))
                                                                                {
                                                                                    // toe fix หน่วยนับ
                                                                                    if ((_g.g._companyProfile._promotion_fixed_unitcode == true && __condition[__conditionLoop][_g.d.ic_promotion_formula_condition._condition_unit_code].ToString().Equals(__itemListTemp[__row]._unitCode)) || _g.g._companyProfile._promotion_fixed_unitcode == false)
                                                                                    {
                                                                                        if ((__itemListTemp[__row]._promotion_qty_balance - __gridLineQty[__row]) >= __conditionQty)
                                                                                        {
                                                                                            // toe check pro no discount && ver != tomyumgoong || tomyumgoong && isfood
                                                                                            if ((__promotionNoDiscount == true) &&
                                                                                                (
                                                                                                    ((MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro) && __itemListTemp[__row]._drink_type == 0) ||
                                                                                                    (MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong && MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong)
                                                                                                )
                                                                                            )
                                                                                            {
                                                                                                __newResult._no_discount_amount -= (__itemListTemp[__row]._price * (__itemListTemp[__row]._promotion_qty_balance - __gridLineQty[__row]));
                                                                                                __itemListTemp[__row]._groupNumber = __groupLoop;
                                                                                            }

                                                                                            __gridLineQty[__row] += __conditionQty;
                                                                                            __groupCheck[__groupLoop] = true;
                                                                                            break;
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            // ดูว่าเข้าเงื่อนครบทุกกลุ่ม
                                                            for (int __groupLoop = 0; __groupLoop < __groupList.Count; __groupLoop++)
                                                            {
                                                                if (__groupCheck[__groupLoop] == false)
                                                                {
                                                                    // toe คืนจำนวน no_discount
                                                                    if (__promotionNoDiscount == true)
                                                                    {
                                                                        /* bug คืนจำนวน
                                                                        for (int __groupLoop2 = 0; __groupLoop2 < __groupList.Count; __groupLoop2++)
                                                                        {
                                                                            //if (__groupListQtyTemp[__groupLoop] > 0)
                                                                            //{
                                                                            for (int __row = 0; __row < __itemListTemp.Count; __row++) // && __groupListQtyTemp[__groupLoop] > 0
                                                                            {
                                                                                if (__itemListTemp[__row]._groupNumber == __groupList[__groupLoop2] && __itemListTemp[__row]._promotion_qty_balance > 0)
                                                                                {
                                                                                    if ((__promotionNoDiscount == true) &&
                                                                                        (
                                                                                            ((MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro) && __itemListTemp[__row]._drink_type == 0) ||
                                                                                            (MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong && MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong)
                                                                                        )
                                                                                    )
                                                                                    {
                                                                                        __newResult._no_discount_amount += (__itemListTemp[__row]._price * __itemListTemp[__row]._promotion_qty_balance);
                                                                                    }


                                                                                }
                                                                            }
                                                                            //}
                                                                        }*/
                                                                    }

                                                                    __promotionPass = false;
                                                                    break;
                                                                }
                                                            }
                                                            if (__promotionPass)
                                                            {
                                                                // เข้าเงื่อนไข
                                                                DataRow[] __action = this._promotionFormulaAction.Select(_g.d.ic_promotion_formula_action._code + "=\'" + __promotionCode + "\'");
                                                                __havePromotion = false;
                                                                for (int __actionRow = 0; __actionRow < __action.Length; __actionRow++)
                                                                {
                                                                    string __command = __action[__actionRow][_g.d.ic_promotion_formula_action._action_command].ToString().Trim();
                                                                    decimal __qty = MyLib._myGlobal._decimalPhase(__action[__actionRow][_g.d.ic_promotion_formula_action._qty_from].ToString().Trim());

                                                                    __promotionPass = false;
                                                                    string[] __split = __command.Split(',');
                                                                    string __itemCode = __split[0].ToString();
                                                                    string __unitCode = __split[1].ToString();
                                                                    // ค้นหาราคาของแถม เพื่อเป็นส่วนลด
                                                                    for (int __row3 = 0; __row3 < __itemListTemp.Count; __row3++)
                                                                    {
                                                                        if (__normal_price_only == false || (__itemListTemp[__row3]._price_type == 1 || __itemListTemp[__row3]._price_type == 5))
                                                                        {
                                                                            if (__itemListTemp[__row3]._itemCode.Equals(__itemCode) && __itemListTemp[__row3]._unitCode.Equals(__unitCode) && __itemListTemp[__row3]._promotion_qty_balance > 0)
                                                                            {
                                                                                // check pro no discount && ver != tomyumgoong || tomyumgoong && isfood
                                                                                //if ((__promotionNoDiscount == true) &&
                                                                                //    (
                                                                                //        ((MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro) && __itemListTemp[__row3]._drink_type == 0) ||
                                                                                //        (MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong && MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong)
                                                                                //    )
                                                                                //)
                                                                                //{
                                                                                //    __newResult._no_discount_amount -= (__itemListTemp[__row3]._price * __qty);
                                                                                //}

                                                                                __newResult._amount -= (__itemListTemp[__row3]._price * __qty);
                                                                                __newResult._noPointAmount += (__itemListTemp[__row3]._havePoint) ? (__itemListTemp[__row3]._price * __qty) : 0M;
                                                                                __newResult._qty += __qty;
                                                                                __havePromotion = true;
                                                                                __promotionPass = true;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                // ลดตัวเลขคงเหลือสำหรับ Promotion ต่อไป
                                                                for (int __row = 0; __row < __itemListTemp.Count; __row++)
                                                                {
                                                                    if (__normal_price_only == false || ((__itemListTemp[__row]._price_type == 1 || __itemListTemp[__row]._price_type == 5 || __itemListTemp[__row]._price_type == 7) && __itemListTemp[__row]._discountNumber == 0))
                                                                    {

                                                                        // ตัดจำนวน ของ Pro ต่อไป
                                                                        if (
                                                                             (__gridLineQty[__row] > 0) &&
                                                                            ((__itemListTemp[__row]._promotion_qty_balance - __gridLineQty[__row]) > 0) &&
                                                                            (__promotionNoDiscount == true) &&
                                                                            (
                                                                                ((MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro) && __itemListTemp[__row]._drink_type == 0) ||
                                                                                (MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong && MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong)
                                                                            )
                                                                            )
                                                                        {
                                                                            __newResult._no_discount_amount += (__itemListTemp[__row]._price * (__itemListTemp[__row]._promotion_qty_balance - __gridLineQty[__row]));
                                                                        }

                                                                        __itemListTemp[__row]._promotion_qty_balance -= __gridLineQty[__row];
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        if (__havePromotion)
                                                        {
                                                            this._promotionDiscountAmount += __newResult._amount;
                                                            _promotionDiscountResult.Add(__newResult);
                                                        }
                                                    }
                                                }
                                                break;
                                            #endregion
                                            #region 6. ของแถมสินค้าตามมูลค่า โต๋ ย้ายไปทำสุดท้าย รอบเดียว
                                            /*case 5:
                                            {
                                                if (_promotionFormulaCondition != null && _promotionFormulaCondition.Rows.Count > 0)
                                                {
                                                    // fix group = -1;
                                                    decimal __sumTotalAmount = 0M;
                                                    for (int __loop1 = 0; __loop1 < _itemList.Count; __loop1++)
                                                    {
                                                        __itemListTemp[__loop1]._groupNumber = -1;
                                                        //__sumTotalAmount += __itemListTemp[__loop1]._amount;
                                                    }


                                                    // เงื่อนไข
                                                    DataRow[] __condition = this._promotionFormulaCondition.Select(_g.d.ic_promotion_formula_condition._code + "=\'" + __promotionCode + "\'");
                                                    DataRow[] __conditionGroup = this._promotionFormulaGroup.Select(_g.d.ic_promotion_formula_group_qty._code + "=\'" + __promotionCode + "\'");

                                                    int __itemCodeColumn = this._promotionFormulaCondition.Columns.IndexOf(_g.d.ic_promotion_formula_condition._condition_from);
                                                    int __groupNumberColumn = this._promotionFormulaCondition.Columns.IndexOf(_g.d.ic_promotion_formula_condition._condition_group);

                                                    int __groupQtyGroupNumber = this._promotionFormulaGroup.Columns.IndexOf(_g.d.ic_promotion_formula_group_qty._group_number);
                                                    int __groupQtyQtyColumnNumber = this._promotionFormulaGroup.Columns.IndexOf(_g.d.ic_promotion_formula_group_qty._qty);
                                                    int __groupAmountColumnNumber = this._promotionFormulaGroup.Columns.IndexOf(_g.d.ic_promotion_formula_group_qty._group_amount);

                                                    _promotionResultClass __newResult = new _promotionResultClass();
                                                    __newResult._promotionCode = __promotionCode;
                                                    __newResult._promotionName = __promotionName;
                                                    Boolean __havePromotion = false;

                                                    List<int> __groupList = new List<int>();
                                                    List<decimal> __groupListQty = new List<decimal>();
                                                    List<decimal> __groupListAmount = new List<decimal>();

                                                    for (int __row = 0; __row < __conditionGroup.Length; __row++)
                                                    {
                                                        __groupList.Add((int)MyLib._myGlobal._decimalPhase(__conditionGroup[__row][__groupQtyGroupNumber].ToString()));
                                                        __groupListQty.Add(MyLib._myGlobal._decimalPhase(__conditionGroup[__row][__groupQtyQtyColumnNumber].ToString()));
                                                        __groupListAmount.Add(MyLib._myGlobal._decimalPhase(__conditionGroup[__row][__groupAmountColumnNumber].ToString()));
                                                    }

                                                    bool __promotionPass = true;

                                                    while (__promotionPass && __groupList.Count > 0)
                                                    {
                                                        List<decimal> __gridLineQty = new List<decimal>();

                                                        __sumTotalAmount = 0M;
                                                        // รวมมูลค่า
                                                        for (int __loop1 = 0; __loop1 < _itemList.Count; __loop1++)
                                                        {
                                                            __gridLineQty.Add(0m);
                                                            if (__itemListTemp[__loop1]._promotion_qty_balance > 0 &&
                                                                // (__promotionNoDiscount == true) &&
                                                                                    (
                                                                                        ((MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro) && __itemListTemp[__loop1]._drink_type == 0) ||
                                                                                        (MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong && MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong))
                                                                )
                                                            {
                                                                __sumTotalAmount += (__itemListTemp[__loop1]._promotionAmount);
                                                            }
                                                        }

                                                        //__sumTotalAmount += this._promotionDiscountAmount; // เอามูลค่าที่ได้ pro ไปแล้วมาหักด้วย
                                                        __sumTotalAmount += this._promotionPassAmount;


                                                        // รวมจำนวนตามกลุ่ม
                                                        List<decimal> __groupListQtySum = new List<decimal>();
                                                        for (int __groupLoop = 0; __groupLoop < __groupList.Count; __groupLoop++)
                                                        {
                                                            __groupListQtySum.Add(0m);
                                                            for (int __conditionLoop = 0; __conditionLoop < __condition.Length; __conditionLoop++)
                                                            {
                                                                if (__groupList[__groupLoop] == (int)MyLib._myGlobal._decimalPhase(__condition[__conditionLoop][__groupNumberColumn].ToString()))
                                                                {
                                                                    for (int __row = 0; __row < __itemListTemp.Count; __row++)
                                                                    {
                                                                        if (__itemListTemp[__row]._itemCode.Equals(__condition[__conditionLoop][__itemCodeColumn].ToString()))
                                                                        {
                                                                            // toe fix หน่วยนับ
                                                                            if ((_g.g._companyProfile._promotion_fixed_unitcode == true && __condition[__conditionLoop][_g.d.ic_promotion_formula_condition._condition_unit_code].ToString().Equals(__itemListTemp[__row]._unitCode)) || _g.g._companyProfile._promotion_fixed_unitcode == false)
                                                                            {

                                                                                // add no discount
                                                                                if (__itemListTemp[__row]._promotion_qty_balance > 0 &&
                                                                                    (__promotionNoDiscount == true) &&
                                                                                    (
                                                                                        ((MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro) && __itemListTemp[__row]._drink_type == 0) ||
                                                                                        (MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong && MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong)
                                                                                    )
                                                                                )
                                                                                {
                                                                                    __newResult._no_discount_amount -= (__itemListTemp[__row]._price * __itemListTemp[__row]._promotion_qty_balance);
                                                                                }

                                                                                __groupListQtySum[__groupLoop] += __itemListTemp[__row]._promotion_qty_balance;
                                                                                __itemListTemp[__row]._groupNumber = __groupList[__groupLoop];
                                                                            }
                                                                            else
                                                                            {
                                                                                //__itemListTemp[__row]._groupNumber = -1;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            //__itemListTemp[__row]._groupNumber = -1;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        // ดูว่าเข้าเงื่อนครบทุกกลุ่ม หรือไม่

                                                        for (int __groupLoop = 0; __groupLoop < __groupList.Count; __groupLoop++)
                                                        {
                                                            if ((__sumTotalAmount - __newResult._no_discount_amount) < __groupListAmount[__groupLoop] || __groupListQtySum[__groupLoop] < __groupListQty[__groupLoop]) // เอามูลค่าโปรที่สะสมไว้มาหักด้วย
                                                            {
                                                                // toe คืนจำนวน no_discount
                                                                if (__promotionNoDiscount == true)
                                                                {
                                                                    for (int __groupLoop2 = 0; __groupLoop2 < __groupList.Count; __groupLoop2++)
                                                                    {
                                                                        //if (__groupListQtyTemp[__groupLoop] > 0)
                                                                        //{
                                                                        for (int __row = 0; __row < __itemListTemp.Count; __row++) // && __groupListQtyTemp[__groupLoop] > 0
                                                                        {
                                                                            if (__itemListTemp[__row]._groupNumber == __groupList[__groupLoop2] && __itemListTemp[__row]._promotion_qty_balance > 0)
                                                                            {
                                                                                if ((__promotionNoDiscount == true) &&
                                                                                    (
                                                                                        ((MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro) && __itemListTemp[__row]._drink_type == 0) ||
                                                                                        (MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong && MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong)
                                                                                    )
                                                                                )
                                                                                {
                                                                                    __newResult._no_discount_amount += (__itemListTemp[__row]._price * __itemListTemp[__row]._promotion_qty_balance);
                                                                                }


                                                                            }
                                                                        }
                                                                        //}
                                                                    }
                                                                }

                                                                __promotionPass = false;

                                                                break;
                                                            }
                                                        }


                                                        // ถ้าเข้าโปร
                                                        if (__promotionPass)
                                                        {
                                                            // คำสั่ง
                                                            // เข้าเงื่อนไข

                                                            // ลด promotion qty
                                                            // ลด amount qty
                                                            DataRow[] __action = this._promotionFormulaAction.Select(_g.d.ic_promotion_formula_action._code + "=\'" + __promotionCode + "\'");
                                                            __havePromotion = false;
                                                            for (int __actionRow = 0; __actionRow < __action.Length; __actionRow++)
                                                            {
                                                                string __command = __action[__actionRow][_g.d.ic_promotion_formula_action._action_command].ToString().Trim();
                                                                decimal __qty = MyLib._myGlobal._decimalPhase(__action[__actionRow][_g.d.ic_promotion_formula_action._qty_from].ToString().Trim());

                                                                __promotionPass = false;
                                                                string[] __split = __command.Split(',');
                                                                string __itemCode = __split[0].ToString();
                                                                string __unitCode = __split[1].ToString();
                                                                // ค้นหาราคาของแถม เพื่อเป็นส่วนลด
                                                                for (int __row3 = 0; __row3 < __itemListTemp.Count; __row3++)
                                                                {
                                                                    if (__itemListTemp[__row3]._itemCode.Equals(__itemCode) && __itemListTemp[__row3]._unitCode.Equals(__unitCode) && __itemListTemp[__row3]._promotion_qty_balance > 0)
                                                                    {
                                                                        // check pro no discount && ver != tomyumgoong || tomyumgoong && isfood
                                                                        //if ((__promotionNoDiscount == true) &&
                                                                        //    (
                                                                        //        ((MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro) && __itemListTemp[__row3]._drink_type == 0) ||
                                                                        //        (MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong && MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong)
                                                                        //    )
                                                                        //)
                                                                        //{
                                                                        //    __newResult._no_discount_amount -= (__itemListTemp[__row3]._price * __qty);
                                                                        //}

                                                                        __newResult._amount -= (__itemListTemp[__row3]._price * __qty);
                                                                        __newResult._noPointAmount += (__itemListTemp[__row3]._havePoint) ? (__itemListTemp[__row3]._price * __qty) : 0M;
                                                                        __newResult._qty += __qty;
                                                                        __havePromotion = true;
                                                                        __promotionPass = true;
                                                                    }
                                                                }
                                                            }

                                                            // toe ปรับลดจำนวน promotion_qty_balance ลง
                                                            for (int __groupLoop = 0; __groupLoop < __groupList.Count; __groupLoop++)
                                                            {
                                                                decimal __promotionGroupQty = __groupListQty[__groupLoop];
                                                                for (int __row = 0; __row < __itemListTemp.Count; __row++)
                                                                {
                                                                    if (__itemListTemp[__row]._groupNumber == __groupList[__groupLoop])
                                                                    {
                                                                        if (__promotionGroupQty > 0 && __itemListTemp[__row]._promotion_qty_balance > 0)
                                                                        {
                                                                            if (__promotionGroupQty >= __itemListTemp[__row]._promotion_qty_balance)
                                                                            {
                                                                                __itemListTemp[__row]._promotion_qty_balance = 0;
                                                                                __promotionGroupQty -= __itemListTemp[__row]._promotion_qty_balance;
                                                                            }
                                                                            else
                                                                            {
                                                                                __itemListTemp[__row]._promotion_qty_balance -= __groupListQty[__groupLoop];
                                                                                //__itemListTemp[__row]._promotionAmount -= __groupListQty[__groupLoop];
                                                                                __promotionGroupQty = 0;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                __newResult._no_discount_amount += __groupListAmount[__groupLoop];
                                                            }

                                                            // toe คืนจำนวน no_discount
                                                            if (__promotionNoDiscount == true)
                                                            {
                                                                for (int __groupLoop = 0; __groupLoop < __groupList.Count; __groupLoop++)
                                                                {
                                                                    //if (__groupListQtyTemp[__groupLoop] > 0)
                                                                    //{
                                                                    for (int __row = 0; __row < __itemListTemp.Count; __row++) // && __groupListQtyTemp[__groupLoop] > 0
                                                                    {
                                                                        if (__itemListTemp[__row]._groupNumber == __groupList[__groupLoop] && __itemListTemp[__row]._promotion_qty_balance > 0)
                                                                        {
                                                                            if ((__promotionNoDiscount == true) &&
                                                                                (
                                                                                    ((MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro) && __itemListTemp[__row]._drink_type == 0) ||
                                                                                    (MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong && MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong)
                                                                                )
                                                                            )
                                                                            {
                                                                                __newResult._no_discount_amount += (__itemListTemp[__row]._price * __itemListTemp[__row]._promotion_qty_balance);
                                                                            }


                                                                        }
                                                                    }
                                                                    //}
                                                                }
                                                            }
                                                        }

                                                        if (__havePromotion == true)
                                                        {
                                                            this._promotionDiscountAmount += __newResult._amount;
                                                            this._promotionPassAmount += (-1 * __groupListAmount[0]);
                                                            __newResult._no_discount_amount += (__newResult._amount * -1);
                                                        }

                                                    }
                                                    if (__havePromotion)
                                                    {
                                                        //this._promotionDiscountAmount += __newResult._amount;
                                                        //this._promotionPassAmount  += (-1 * __groupListAmount[0]);
                                                        //__newResult._no_discount_amount += (__newResult._amount * -1);
                                                        _promotionDiscountResult.Add(__newResult);

                                                        // ปรับปรุง ยอดสินค้า คงเหลือ เอาไปทำ promotion ต่อไป
                                                        //for (int row = 0; row < __itemListTemp.Count; row++)
                                                        //{
                                                        //    if (__itemListTemp[row]._promotionAmount > 0 && __itemListTemp[row]._promotion_qty_balance > 0)
                                                        //    {
                                                        //        if (__itemListTemp[row]._promotionAmount - __newResult._no_discount_amount > 0)
                                                        //        {
                                                        //            __itemListTemp[row]._promotionAmount -= __newResult._no_discount_amount;

                                                        //        }
                                                        //        else
                                                        //        {
                                                        //            // ไม่พอหัก ไปตัวต่อไป
                                                        //            __itemListTemp[row]._promotionAmount = 0;
                                                        //        }
                                                        //    }
                                                        //    else
                                                        //    {

                                                        //    }
                                                        //}
                                                    }

                                                }
                                            }
                                            break;*/
                                            #endregion
                                            #region 99. ของแถมตามจำนวนเต็ม (จัดชุด) ตามกลุ่ม ยังไมีมีเงื่อนไขนี้
                                            case 99: // ของแถมตามจำนวนเต็ม (จัดชุด) ตามกลุ่ม
                                                {
                                                    if (_promotionFormulaCondition != null && _promotionFormulaCondition.Rows.Count > 0)
                                                    {
                                                        DataRow[] __condition = this._promotionFormulaCondition.Select(_g.d.ic_promotion_formula_condition._code + "=\'" + __promotionCode + "\'");
                                                        int __itemCodeColumn = this._promotionFormulaCondition.Columns.IndexOf(_g.d.ic_promotion_formula_condition._condition_from);
                                                        int __groupNumberColumn = this._promotionFormulaCondition.Columns.IndexOf(_g.d.ic_promotion_formula_condition._condition_group);
                                                        DataRow[] __conditionGroup = this._promotionFormulaGroup.Select(_g.d.ic_promotion_formula_group_qty._code + "=\'" + __promotionCode + "\'");
                                                        int __groupQtyGroupNumber = this._promotionFormulaGroup.Columns.IndexOf(_g.d.ic_promotion_formula_group_qty._group_number);
                                                        int __groupQtyQtyColumnNumber = this._promotionFormulaGroup.Columns.IndexOf(_g.d.ic_promotion_formula_group_qty._qty);
                                                        _promotionResultClass __newResult = new _promotionResultClass();
                                                        __newResult._promotionCode = __promotionCode;
                                                        __newResult._promotionName = __promotionName;
                                                        Boolean __havePromotion = false;
                                                        // 
                                                        List<int> __groupList = new List<int>();
                                                        List<decimal> __groupListQty = new List<decimal>();
                                                        for (int __row = 0; __row < __conditionGroup.Length; __row++)
                                                        {
                                                            __groupList.Add((int)MyLib._myGlobal._decimalPhase(__conditionGroup[__row][__groupQtyGroupNumber].ToString()));
                                                            __groupListQty.Add(MyLib._myGlobal._decimalPhase(__conditionGroup[__row][__groupQtyQtyColumnNumber].ToString()));
                                                        }
                                                        //
                                                        bool __promotionPass = true;
                                                        while (__promotionPass && __groupList.Count > 0)
                                                        {
                                                            // รวมจำนวนตามกลุ่ม
                                                            List<decimal> __groupListQtySum = new List<decimal>();
                                                            for (int __groupLoop = 0; __groupLoop < __groupList.Count; __groupLoop++)
                                                            {
                                                                __groupListQtySum.Add(0m);
                                                                for (int __conditionLoop = 0; __conditionLoop < __condition.Length; __conditionLoop++)
                                                                {
                                                                    if (__groupList[__groupLoop] == (int)MyLib._myGlobal._decimalPhase(__condition[__conditionLoop][__groupNumberColumn].ToString()))
                                                                    {
                                                                        for (int __row = 0; __row < __itemListTemp.Count; __row++)
                                                                        {
                                                                            if (__itemListTemp[__row]._itemCode.Equals(__condition[__conditionLoop][__itemCodeColumn].ToString()))
                                                                            {
                                                                                // toe fix หน่วยนับ
                                                                                if ((_g.g._companyProfile._promotion_fixed_unitcode == true && __condition[__conditionLoop][_g.d.ic_promotion_formula_condition._condition_unit_code].ToString().Equals(__itemListTemp[__row]._unitCode)) || _g.g._companyProfile._promotion_fixed_unitcode == false)
                                                                                {

                                                                                    __groupListQtySum[__groupLoop] += __itemListTemp[__row]._promotion_qty_balance;
                                                                                    __itemListTemp[__row]._groupNumber = __groupList[__groupLoop];
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            // ดูว่าเข้าเงื่อนครบทุกกลุ่ม
                                                            for (int __groupLoop = 0; __groupLoop < __groupList.Count; __groupLoop++)
                                                            {
                                                                if (__groupListQtySum[__groupLoop] < __groupListQty[__groupLoop])
                                                                {
                                                                    __promotionPass = false;
                                                                    break;
                                                                }
                                                            }
                                                            if (__promotionPass)
                                                            {
                                                                // เข้าเงื่อนไข
                                                                __havePromotion = true;
                                                                DataRow[] __action = this._promotionFormulaAction.Select(_g.d.ic_promotion_formula_action._code + "=\'" + __promotionCode + "\'");
                                                                string __command = __action[0][_g.d.ic_promotion_formula_action._action_command].ToString().Trim();
                                                                if (__command.Length > 0)
                                                                {
                                                                    if (__command[0] == '=')
                                                                    {
                                                                        // กรณีเป็นของแถม
                                                                        try
                                                                        {
                                                                            __havePromotion = false;
                                                                            __promotionPass = false;
                                                                            string[] __split = __command.Remove(0, 1).Split(',');
                                                                            string __itemCode = __split[0].ToString();
                                                                            string __unitCode = __split[1].ToString();
                                                                            // ค้นหาราคาของแถม เพื่อเป็นส่วนลด
                                                                            for (int __row3 = 0; __row3 < __itemListTemp.Count; __row3++)
                                                                            {
                                                                                if (__itemListTemp[__row3]._itemCode.Equals(__itemCode) && __itemListTemp[__row3]._unitCode.Equals(__unitCode) && __itemListTemp[__row3]._promotion_qty_balance > 0)
                                                                                {
                                                                                    __newResult._amount -= __itemListTemp[__row3]._price;
                                                                                    __newResult._qty += 1m;
                                                                                    /*if (_itemListTemp[__row3]._promotion_qty_balance < 0)
                                                                                    {
                                                                                        // กรณีแถมตัวเอง เอายอดไปชดเชยรหัสอื่น
                                                                                        for (int __row2 = 0; __row2 < _itemListTemp.Count ; __row2++)
                                                                                        {
                                                                                            if (_itemListTemp[__row2]._groupNumber == _itemListTemp[__row3]._groupNumber && _itemListTemp[__row2]._promotion_qty_balance < _itemListTemp[__row2]._qty)
                                                                                            {
                                                                                                _itemListTemp[__row2]._promotion_qty_balance += 1m;
                                                                                                break;
                                                                                            }
                                                                                        }
                                                                                    }*/
                                                                                    __havePromotion = true;
                                                                                    __promotionPass = true;
                                                                                    // ลดตัวเลขคงเหลือสำหรับ Promotion ต่อไป
                                                                                    List<decimal> __groupListQtyTemp = new List<decimal>();
                                                                                    for (int __groupLoop = 0; __groupLoop < __groupList.Count; __groupLoop++)
                                                                                    {
                                                                                        __groupListQtyTemp.Add(__groupListQty[__groupLoop]);
                                                                                    }
                                                                                    for (int __groupLoop = 0; __groupLoop < __groupList.Count; __groupLoop++)
                                                                                    {
                                                                                        if (__groupListQtyTemp[__groupLoop] > 0)
                                                                                        {
                                                                                            for (int __row = 0; __row < __itemListTemp.Count && __groupListQtyTemp[__groupLoop] > 0; __row++)
                                                                                            {
                                                                                                if (__itemListTemp[__row]._groupNumber == __groupList[__groupLoop] && __itemListTemp[__row]._promotion_qty_balance >= 0)
                                                                                                {
                                                                                                    decimal __calc = __groupListQtyTemp[__groupLoop];
                                                                                                    __itemListTemp[__row]._promotion_qty_balance -= __groupListQtyTemp[__groupLoop];
                                                                                                    if (__itemListTemp[__row]._promotion_qty_balance < 0)
                                                                                                    {
                                                                                                        __groupListQtyTemp[__groupLoop] += (__itemListTemp[__row]._promotion_qty_balance * -1);
                                                                                                        __itemListTemp[__row]._promotion_qty_balance = 0;
                                                                                                    }
                                                                                                    __groupListQtyTemp[__groupLoop] -= __calc;
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                    break;
                                                                                }
                                                                            }
                                                                        }
                                                                        catch (Exception __ex)
                                                                        {
                                                                            MessageBox.Show(__ex.Message.ToString());
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        // กรณีเป็นส่วนลด
                                                                        __newResult._amount += MyLib._myGlobal._decimalPhase(__command);
                                                                        __newResult._qty += 1;
                                                                        // ลดตัวเลขคงเหลือสำหรับ Promotion ต่อไป
                                                                        List<decimal> __groupListQtyTemp = new List<decimal>();
                                                                        for (int __groupLoop = 0; __groupLoop < __groupList.Count; __groupLoop++)
                                                                        {
                                                                            __groupListQtyTemp.Add(__groupListQty[__groupLoop]);
                                                                        }
                                                                        for (int __groupLoop = 0; __groupLoop < __groupList.Count; __groupLoop++)
                                                                        {
                                                                            if (__groupListQtyTemp[__groupLoop] > 0)
                                                                            {
                                                                                for (int __row = 0; __row < __itemListTemp.Count && __groupListQtyTemp[__groupLoop] > 0; __row++)
                                                                                {
                                                                                    if (__itemListTemp[__row]._groupNumber == __groupList[__groupLoop] && __itemListTemp[__row]._promotion_qty_balance >= 0)
                                                                                    {
                                                                                        decimal __calc = __groupListQtyTemp[__groupLoop];
                                                                                        __itemListTemp[__row]._promotion_qty_balance -= __groupListQtyTemp[__groupLoop];
                                                                                        if (__itemListTemp[__row]._promotion_qty_balance < 0)
                                                                                        {
                                                                                            __groupListQtyTemp[__groupLoop] += (__itemListTemp[__row]._promotion_qty_balance * -1);
                                                                                            __itemListTemp[__row]._promotion_qty_balance = 0;
                                                                                        }
                                                                                        __groupListQtyTemp[__groupLoop] -= __calc;
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        if (__havePromotion)
                                                        {
                                                            this._promotionDiscountAmount += __newResult._amount;
                                                            _promotionDiscountResult.Add(__newResult);
                                                        }
                                                    }
                                                }
                                                break;
                                                #endregion
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    if (this._promotionErrorMessage != null)
                                    {
                                        this._promotionErrorMessage(__promotionName, ex.Message.ToString());
                                    }
                                }
                            }
                        }
                    }

                    // หา total มาประมวลผลก่อน


                    // process case 6. ของแถมตามมูลค่า
                    for (int __loopFormula = 0; __loopFormula < this._promotionFormula.Rows.Count; __loopFormula++)
                    {
                        string __promotionCode = this._promotionFormula.Rows[__loopFormula][_g.d.ic_promotion_formula._code].ToString();
                        string __promotionName = this._promotionFormula.Rows[__loopFormula][_g.d.ic_promotion_formula._name_1].ToString();
                        Boolean __promotionNoDiscount = this._promotionFormula.Rows[__loopFormula][_g.d.ic_promotion_formula._is_no_discount].ToString().Equals("1") ? true : false;
                        int __useDateFilter = MyLib._myGlobal._intPhase(this._promotionFormula.Rows[__loopFormula][_g.d.ic_promotion_formula._use_date_range].ToString());
                        DateTime __fromDate = MyLib._myGlobal._convertDateFromQuery(this._promotionFormula.Rows[__loopFormula][_g.d.ic_promotion_formula._from_date].ToString());
                        DateTime __to_date = MyLib._myGlobal._convertDateFromQuery(this._promotionFormula.Rows[__loopFormula][_g.d.ic_promotion_formula._to_date].ToString());
                        int __promotionCase = (int)MyLib._myGlobal._decimalPhase(this._promotionFormula.Rows[__loopFormula][_g.d.ic_promotion_formula._case_number].ToString());

                        string __lock_day = this._promotionFormula.Rows[__loopFormula][_g.d.ic_promotion_formula._lock_day].ToString();

                        // toe lock day
                        if (__lock_day == "" || __lock_day.IndexOf(((int)DateTime.Now.DayOfWeek).ToString()) != -1)
                        {
                            if (__promotionCase == 5)
                            {
                                try
                                {
                                    // ปรับให้เป็น 23.59.59
                                    __to_date = __to_date.AddDays(1).AddSeconds(-1);
                                }
                                catch
                                {
                                }
                                DateTime __today = DateTime.Now;

                                if (__useDateFilter == 0 ||
                                    (__useDateFilter == 1 && (__fromDate <= __today) && (__to_date >= __today))
                                    )
                                {
                                    try
                                    {
                                        //Boolean __isSumUnit = this._promotionFormula.Rows[__loopFormula][_g.d.ic_promotion_formula._auto_change_unitcode].ToString().Equals("1") ? true : false;

                                        // 0 ทั่วไป, 1 สมาชิก ,2 ไม่ใช่สมาชิก
                                        int _member_check = (int)MyLib._myGlobal._decimalPhase(this._promotionFormula.Rows[__loopFormula][_g.d.ic_promotion_formula._member_condition].ToString());

                                        // toe check เงื่อนไขสมาชิกก่อน
                                        if (_member_check == 0 || (_member_check == 1 && this._memberCode.Length > 0 && (this._memberCode.Equals(this._posDefaultCust) == false)) || (_member_check == 2 && this._memberCode.Length == 0)) // this._custCode.Equals(this._posDefaultCust)
                                        {
                                            if (_promotionFormulaCondition != null && _promotionFormulaCondition.Rows.Count > 0)
                                            {
                                                // fix group = -1;
                                                decimal __sumTotalAmount = 0M;

                                                for (int __loop1 = 0; __loop1 < _itemList.Count; __loop1++)
                                                {
                                                    __itemListTemp[__loop1]._groupNumber = -1;
                                                    //__sumTotalAmount += __itemListTemp[__loop1]._amount;
                                                }


                                                // เงื่อนไข
                                                DataRow[] __condition = this._promotionFormulaCondition.Select(_g.d.ic_promotion_formula_condition._code + "=\'" + __promotionCode + "\'");
                                                DataRow[] __conditionGroup = this._promotionFormulaGroup.Select(_g.d.ic_promotion_formula_group_qty._code + "=\'" + __promotionCode + "\'");

                                                int __itemCodeColumn = this._promotionFormulaCondition.Columns.IndexOf(_g.d.ic_promotion_formula_condition._condition_from);
                                                int __groupNumberColumn = this._promotionFormulaCondition.Columns.IndexOf(_g.d.ic_promotion_formula_condition._condition_group);

                                                int __groupQtyGroupNumber = this._promotionFormulaGroup.Columns.IndexOf(_g.d.ic_promotion_formula_group_qty._group_number);
                                                int __groupQtyQtyColumnNumber = this._promotionFormulaGroup.Columns.IndexOf(_g.d.ic_promotion_formula_group_qty._qty);
                                                int __groupAmountColumnNumber = this._promotionFormulaGroup.Columns.IndexOf(_g.d.ic_promotion_formula_group_qty._group_amount);

                                                _promotionResultClass __newResult = new _promotionResultClass();
                                                __newResult._promotionCode = __promotionCode;
                                                __newResult._promotionName = __promotionName;
                                                Boolean __havePromotion = false;

                                                List<int> __groupList = new List<int>();
                                                List<decimal> __groupListQty = new List<decimal>();
                                                List<decimal> __groupListAmount = new List<decimal>();

                                                for (int __row = 0; __row < __conditionGroup.Length; __row++)
                                                {
                                                    __groupList.Add((int)MyLib._myGlobal._decimalPhase(__conditionGroup[__row][__groupQtyGroupNumber].ToString()));
                                                    __groupListQty.Add(MyLib._myGlobal._decimalPhase(__conditionGroup[__row][__groupQtyQtyColumnNumber].ToString()));
                                                    __groupListAmount.Add(MyLib._myGlobal._decimalPhase(__conditionGroup[__row][__groupAmountColumnNumber].ToString()));
                                                }

                                                bool __promotionPass = true;

                                                while (__promotionPass && __groupList.Count > 0)
                                                {
                                                    List<decimal> __gridLineQty = new List<decimal>();

                                                    __sumTotalAmount = 0M;
                                                    // รวมมูลค่า
                                                    for (int __loop1 = 0; __loop1 < _itemList.Count; __loop1++)
                                                    {
                                                        __gridLineQty.Add(0m);
                                                        if (__itemListTemp[__loop1]._promotion_qty_balance > 0 &&
                                                                                // (__promotionNoDiscount == true) &&
                                                                                (
                                                                                    ((MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro) && __itemListTemp[__loop1]._drink_type == 0) ||
                                                                                    (MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong && MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong))
                                                            )
                                                        {
                                                            __sumTotalAmount += (__itemListTemp[__loop1]._promotionAmount);
                                                        }
                                                    }

                                                    // __sumTotalAmount += this._promotionDiscountAmount; // เอามูลค่าที่ได้ pro ไปแล้วมาหักด้วย
                                                    __sumTotalAmount += this._promotionPassAmount;


                                                    // รวมจำนวนตามกลุ่ม
                                                    List<decimal> __groupListQtySum = new List<decimal>();
                                                    for (int __groupLoop = 0; __groupLoop < __groupList.Count; __groupLoop++)
                                                    {
                                                        __groupListQtySum.Add(0m);
                                                        for (int __conditionLoop = 0; __conditionLoop < __condition.Length; __conditionLoop++)
                                                        {
                                                            if (__groupList[__groupLoop] == (int)MyLib._myGlobal._decimalPhase(__condition[__conditionLoop][__groupNumberColumn].ToString()))
                                                            {
                                                                for (int __row = 0; __row < __itemListTemp.Count; __row++)
                                                                {
                                                                    if (__itemListTemp[__row]._itemCode.Equals(__condition[__conditionLoop][__itemCodeColumn].ToString()))
                                                                    {
                                                                        // toe fix หน่วยนับ
                                                                        if ((_g.g._companyProfile._promotion_fixed_unitcode == true && __condition[__conditionLoop][_g.d.ic_promotion_formula_condition._condition_unit_code].ToString().Equals(__itemListTemp[__row]._unitCode)) || _g.g._companyProfile._promotion_fixed_unitcode == false)
                                                                        {

                                                                            // add no discount
                                                                            if (__itemListTemp[__row]._promotion_qty_balance > 0 &&
                                                                                (__promotionNoDiscount == true) &&
                                                                                (
                                                                                    ((MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro) && __itemListTemp[__row]._drink_type == 0) ||
                                                                                    (MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong && MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong)
                                                                                )
                                                                            )
                                                                            {

                                                                                // โต๋หาจำนวนที่จะลดได้ เอาไปคิด no discount amount

                                                                                decimal __promotionQty = __itemListTemp[__row]._promotion_qty_balance;

                                                                                DataRow[] __actionQty = this._promotionFormulaAction.Select(_g.d.ic_promotion_formula_action._code + "=\'" + __promotionCode + "\'");
                                                                                for (int __actionRow = 0; __actionRow < __actionQty.Length; __actionRow++)
                                                                                {
                                                                                    string __command = __actionQty[__actionRow][_g.d.ic_promotion_formula_action._action_command].ToString().Trim();
                                                                                    decimal __qty = MyLib._myGlobal._decimalPhase(__actionQty[__actionRow][_g.d.ic_promotion_formula_action._qty_from].ToString().Trim());

                                                                                    // __promotionPass = false;
                                                                                    string[] __split = __command.Split(',');
                                                                                    string __itemCode = __split[0].ToString();

                                                                                    if (__itemCode.Equals(__itemListTemp[__row]._itemCode))
                                                                                    {
                                                                                        __promotionQty = __qty;
                                                                                        break;
                                                                                    }
                                                                                }


                                                                                //__newResult._no_discount_amount += (__itemListTemp[__row]._price * __itemListTemp[__row]._promotion_qty_balance);
                                                                                __newResult._no_discount_amount += (__itemListTemp[__row]._price * __promotionQty);
                                                                            }

                                                                            __groupListQtySum[__groupLoop] += __itemListTemp[__row]._promotion_qty_balance;
                                                                            __itemListTemp[__row]._groupNumber = __groupList[__groupLoop];
                                                                        }
                                                                        else
                                                                        {
                                                                            //__itemListTemp[__row]._groupNumber = -1;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        //__itemListTemp[__row]._groupNumber = -1;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }

                                                    // ดูว่าเข้าเงื่อนครบทุกกลุ่ม หรือไม่

                                                    for (int __groupLoop = 0; __groupLoop < __groupList.Count; __groupLoop++)
                                                    {
                                                        decimal __productFreeAmount = 0M;
                                                        // หาราคาของแถม

                                                        for (int __row = 0; __row < __itemListTemp.Count; __row++)
                                                        {
                                                            if (__itemListTemp[__row]._groupNumber == __groupList[__groupLoop])
                                                            {
                                                                __productFreeAmount += __itemListTemp[__row]._amount;
                                                            }
                                                        }

                                                        if ((__sumTotalAmount - __newResult._no_discount_amount) < __groupListAmount[__groupLoop] || __groupListQtySum[__groupLoop] < __groupListQty[__groupLoop]) // เอามูลค่าโปรที่สะสมไว้มาหักด้วย
                                                        {
                                                            // toe คืนจำนวน no_discount
                                                            if (__promotionNoDiscount == true)
                                                            {
                                                                for (int __groupLoop2 = 0; __groupLoop2 < __groupList.Count; __groupLoop2++)
                                                                {
                                                                    //if (__groupListQtyTemp[__groupLoop] > 0)
                                                                    //{
                                                                    for (int __row = 0; __row < __itemListTemp.Count; __row++) // && __groupListQtyTemp[__groupLoop] > 0
                                                                    {
                                                                        if (__itemListTemp[__row]._groupNumber == __groupList[__groupLoop2] && __itemListTemp[__row]._promotion_qty_balance > 0)
                                                                        {
                                                                            if ((__promotionNoDiscount == true) &&
                                                                                (
                                                                                    ((MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro) && __itemListTemp[__row]._drink_type == 0) ||
                                                                                    (MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong && MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong)
                                                                                )
                                                                            )
                                                                            {
                                                                                __newResult._no_discount_amount += (__itemListTemp[__row]._price * __itemListTemp[__row]._promotion_qty_balance);
                                                                            }


                                                                        }
                                                                    }
                                                                    //}
                                                                }
                                                            }

                                                            __promotionPass = false;

                                                            break;
                                                        }
                                                    }


                                                    // ถ้าเข้าโปร
                                                    if (__promotionPass)
                                                    {
                                                        // คำสั่ง
                                                        // เข้าเงื่อนไข

                                                        // ลด promotion qty
                                                        // ลด amount qty
                                                        DataRow[] __action = this._promotionFormulaAction.Select(_g.d.ic_promotion_formula_action._code + "=\'" + __promotionCode + "\'");
                                                        __havePromotion = false;
                                                        for (int __actionRow = 0; __actionRow < __action.Length; __actionRow++)
                                                        {
                                                            string __command = __action[__actionRow][_g.d.ic_promotion_formula_action._action_command].ToString().Trim();
                                                            decimal __qty = MyLib._myGlobal._decimalPhase(__action[__actionRow][_g.d.ic_promotion_formula_action._qty_from].ToString().Trim());

                                                            __promotionPass = false;
                                                            string[] __split = __command.Split(',');
                                                            string __itemCode = __split[0].ToString();
                                                            string __unitCode = __split[1].ToString();
                                                            // ค้นหาราคาของแถม เพื่อเป็นส่วนลด
                                                            for (int __row3 = 0; __row3 < __itemListTemp.Count; __row3++)
                                                            {
                                                                if (__itemListTemp[__row3]._itemCode.Equals(__itemCode) && __itemListTemp[__row3]._unitCode.Equals(__unitCode) && __itemListTemp[__row3]._promotion_qty_balance > 0)
                                                                {
                                                                    // check pro no discount && ver != tomyumgoong || tomyumgoong && isfood
                                                                    //if ((__promotionNoDiscount == true) &&
                                                                    //    (
                                                                    //        ((MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro) && __itemListTemp[__row3]._drink_type == 0) ||
                                                                    //        (MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong && MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong)
                                                                    //    )
                                                                    //)
                                                                    //{
                                                                    //    __newResult._no_discount_amount -= (__itemListTemp[__row3]._price * __qty);
                                                                    //}

                                                                    __newResult._amount -= (__itemListTemp[__row3]._price * __qty);
                                                                    __newResult._noPointAmount += (__itemListTemp[__row3]._havePoint) ? (__itemListTemp[__row3]._price * __qty) : 0M;
                                                                    __newResult._qty += __qty;
                                                                    __havePromotion = true;
                                                                    __promotionPass = true;
                                                                }
                                                            }
                                                        }

                                                        // toe ปรับลดจำนวน promotion_qty_balance ลง
                                                        for (int __groupLoop = 0; __groupLoop < __groupList.Count; __groupLoop++)
                                                        {
                                                            decimal __promotionGroupQty = __groupListQty[__groupLoop];
                                                            for (int __row = 0; __row < __itemListTemp.Count; __row++)
                                                            {
                                                                if (__itemListTemp[__row]._groupNumber == __groupList[__groupLoop])
                                                                {
                                                                    if (__promotionGroupQty > 0 && __itemListTemp[__row]._promotion_qty_balance > 0)
                                                                    {
                                                                        if (__promotionGroupQty >= __itemListTemp[__row]._promotion_qty_balance)
                                                                        {
                                                                            __itemListTemp[__row]._promotion_qty_balance = 0;
                                                                            __promotionGroupQty -= __itemListTemp[__row]._promotion_qty_balance;
                                                                        }
                                                                        else
                                                                        {
                                                                            __itemListTemp[__row]._promotion_qty_balance -= __groupListQty[__groupLoop];
                                                                            //__itemListTemp[__row]._promotionAmount -= __groupListQty[__groupLoop];
                                                                            __promotionGroupQty = 0;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            __newResult._no_discount_amount += __groupListAmount[__groupLoop];
                                                        }

                                                        // toe คืนจำนวน no_discount
                                                        if (__promotionNoDiscount == true)
                                                        {
                                                            for (int __groupLoop = 0; __groupLoop < __groupList.Count; __groupLoop++)
                                                            {
                                                                //if (__groupListQtyTemp[__groupLoop] > 0)
                                                                //{
                                                                for (int __row = 0; __row < __itemListTemp.Count; __row++) // && __groupListQtyTemp[__groupLoop] > 0
                                                                {
                                                                    if (__itemListTemp[__row]._groupNumber == __groupList[__groupLoop] && __itemListTemp[__row]._promotion_qty_balance > 0)
                                                                    {
                                                                        if ((__promotionNoDiscount == true) &&
                                                                            (
                                                                                ((MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro) && __itemListTemp[__row]._drink_type == 0) ||
                                                                                (MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong && MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLTomYumGoong)
                                                                            )
                                                                        )
                                                                        {
                                                                            __newResult._no_discount_amount += (__itemListTemp[__row]._price * __itemListTemp[__row]._promotion_qty_balance);
                                                                        }


                                                                    }
                                                                }
                                                                //}
                                                            }
                                                        }
                                                    }

                                                    if (__havePromotion == true)
                                                    {

                                                        this._promotionPassAmount += (-1 * __groupListAmount[0]);
                                                        __newResult._no_discount_amount += (__newResult._amount * -1);
                                                    }

                                                }
                                                if (__havePromotion)
                                                {
                                                    //this._promotionDiscountAmount += __newResult._amount;
                                                    //this._promotionPassAmount  += (-1 * __groupListAmount[0]);
                                                    //__newResult._no_discount_amount += (__newResult._amount * -1);
                                                    _promotionDiscountResult.Add(__newResult);
                                                    this._promotionDiscountAmount += __newResult._amount;
                                                    break;

                                                    // ปรับปรุง ยอดสินค้า คงเหลือ เอาไปทำ promotion ต่อไป
                                                    //for (int row = 0; row < __itemListTemp.Count; row++)
                                                    //{
                                                    //    if (__itemListTemp[row]._promotionAmount > 0 && __itemListTemp[row]._promotion_qty_balance > 0)
                                                    //    {
                                                    //        if (__itemListTemp[row]._promotionAmount - __newResult._no_discount_amount > 0)
                                                    //        {
                                                    //            __itemListTemp[row]._promotionAmount -= __newResult._no_discount_amount;

                                                    //        }
                                                    //        else
                                                    //        {
                                                    //            // ไม่พอหัก ไปตัวต่อไป
                                                    //            __itemListTemp[row]._promotionAmount = 0;
                                                    //        }
                                                    //    }
                                                    //    else
                                                    //    {

                                                    //    }
                                                    //}
                                                }

                                            }

                                        }
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }

                    }
                }
                #endregion
            }
            //
            if (this._itemList.Count > 0 && _showMessage)
            {
                if (_sendMessage != null)
                {
                    _posClientItemClass __item = this._itemList[this._itemList.Count - 1];
                    string __word = "";
                    if (_row != -1)
                    {
                        __item = this._itemList[_row];
                        __word = "เพิ่ม" + " : ";
                    }
                    __word = __word + String.Format("{0}/{1}/{2}/@ {3} x {4} = {5}", __item._barCode, __item._itemName, __item._unitCode, __item._qty.ToString(this._formatNumber1), __item._price.ToString(this._formatNumber1), (__item._qty * __item._price).ToString(this._formatNumber1));
                    _sendMessage("update:label,status", __word, "");
                }
            }
            //
            for (int __loop = 0; __loop < this._itemList.Count; __loop++)
            {
                _posClientItemClass __newData = new _posClientItemClass();
                __newData._barCode = this._itemList[__loop]._barCode;
                __newData._itemCode = this._itemList[__loop]._itemCode;
                __newData._itemName = this._itemList[__loop]._itemName;
                __newData._itemNameEng = this._itemList[__loop]._itemNameEng;
                __newData._itemType = this._itemList[__loop]._itemType;
                __newData._standValue = this._itemList[__loop]._standValue;
                __newData._divideValue = this._itemList[__loop]._divideValue;
                __newData._unitCode = this._itemList[__loop]._unitCode;
                __newData._unitCodeEng = this._itemList[__loop]._unitCodeEng;
                __newData._qtyLast = this._itemList[__loop]._qtyLast;
                __newData._qty = this._itemList[__loop]._qty;
                __newData._taxType = this._itemList[__loop]._taxType;
                __newData._vat_type = this._itemList[__loop]._vat_type;
                __newData._vatRate = this._itemList[__loop]._vatRate;
                __newData._price = this._itemList[__loop]._price;
                __newData._havePoint = this._itemList[__loop]._havePoint;
                __newData._discountWord = this._itemList[__loop]._discountWord;
                __newData._discount = this._itemList[__loop]._discount;
                __newData._priceInfo = this._itemList[__loop]._priceInfo;
                __newData._discountNumber = this._itemList[__loop]._discountNumber;
                __newData._isChangeDiscount = this._itemList[__loop]._isChangeDiscount;
                __newData._isChangePrice = this._itemList[__loop]._isChangePrice;
                __newData._priceDefault = this._itemList[__loop]._priceDefault;
                __newData._serialNumber = this._itemList[__loop]._serialNumber;
                __newData._price_type = this._itemList[__loop]._price_type;
                __newData._drink_type = this._itemList[__loop]._drink_type;

                __newData._price_base = this._itemList[__loop]._price_base;
                __newData._no_discount = this._itemList[__loop]._no_discount;

                __newData._print_per_unit = this._itemList[__loop]._print_per_unit;
                __newData._barcode_checker_print = this._itemList[__loop]._barcode_checker_print;
                __newData._confirm_guid = this._itemList[__loop]._confirm_guid;
                __newData._kitchen_code = this._itemList[__loop]._kitchen_code;
                __newData._replace_remark_qty = this._itemList[__loop]._replace_remark_qty;
                __newData._item_remark = this._itemList[__loop]._item_remark;
                __newData._item_name_remark = this._itemList[__loop]._item_name_remark;

                __newData._ref_guid = this._itemList[__loop]._ref_guid;
                __newData._set_ref_line = this._itemList[__loop]._set_ref_line;
                __newData._ref_row = this._itemList[__loop]._ref_row;
                __newData._item_code_main = this._itemList[__loop]._item_code_main;
                __newData._set_ref_qty = this._itemList[__loop]._set_ref_qty;
                __newData._set_ref_price = this._itemList[__loop]._set_ref_price;
                __newData._price_set_ratio = this._itemList[__loop]._price_set_ratio;

                __newData._pos_no_sum = this._itemList[__loop]._pos_no_sum;
                __newData._balanceControl = this._itemList[__loop]._balanceControl;

                decimal __calc = __newData._qty * __newData._price; // __newData._qty * __newData._price;

                if (__newData._discountWord.IndexOf("@") != -1)
                {
                    decimal __discountValue = MyLib._myGlobal._decimalPhase(__newData._discountWord.Replace("@", string.Empty)) * __newData._qty; // 
                    __newData._amount = MyLib._myGlobal._calcAfterDiscount(__discountValue.ToString(), MyLib._myGlobal._round(__calc, _g.g._companyProfile._item_price_decimal), _g.g._companyProfile._item_price_decimal); ;
                }
                else
                {

                    // รวม Promotion qty 
                    decimal __promotion_qty_balance = 0M;
                    for (int __row2 = 0; __row2 < this._itemList.Count; __row2++)
                    {
                        if (this._itemList[__loop]._itemCode.Equals(_itemList[__row2]._itemCode))
                        {
                            __promotion_qty_balance += this._itemList[__row2]._promotion_qty_balance;
                        }
                    }

                    if (__promotion_qty_balance == 0)
                    {
                        __newData._discountWord = "";
                    }

                    __newData._amount = MyLib._myGlobal._calcAfterDiscount(__newData._discountWord, MyLib._myGlobal._round(__calc, _g.g._companyProfile._item_price_decimal), _g.g._companyProfile._item_price_decimal); ;
                }

                __newData._discount = __calc - __newData._amount;

                if (__newData._replace_remark_qty == true)
                {
                    // replace remark
                    Regex __regex = new Regex(@"(\{.(.*).\})");
                    MatchCollection __matchs = __regex.Matches(__newData._item_remark);
                    foreach (Match __match in __matchs)
                    {
                        // get match and replace
                        string __matValue = __match.Value;
                        string __getValue = __matValue.Replace("@qty@", __newData._qty.ToString());

                        var __resultProcess = new DataTable().Compute(__getValue.Replace("{", string.Empty).Replace("}", string.Empty), null);
                        __newData._item_remark = __newData._item_remark.Replace(__matValue, __resultProcess.ToString());
                    }
                    __newData._itemName = __newData._item_name_remark + "\n" + __newData._item_remark;
                }
                this._itemProcess.Add(__newData);
            }
            // Add Promotion
            /*if (this._promotionResult != null)
            {
                for (int __loop = 0; __loop < this._promotionResult.Count; __loop++)
                {
                    _posClientItemClass __newData = new _posClientItemClass();
                    __newData._barCode = this._promotionResult[__loop]._promotionCode;
                    __newData._itemCode = this._promotionResult[__loop]._promotionCode;
                    __newData._itemName = this._promotionResult[__loop]._promotionName;
                    __newData._itemType = 0;
                    __newData._standValue = 0;
                    __newData._divideValue = 0;
                    __newData._unitCode = "";
                    __newData._qty = this._promotionResult[__loop]._count;
                    __newData._taxType = 0;
                    __newData._vatRate = 0;
                    __newData._price = 0;
                    __newData._havePoint = false;
                    __newData._discountWord = "";
                    __newData._discount = this._promotionResult[__loop]._discount;
                    __newData._priceInfo = "";
                    __newData._discountNumber = 0;
                    __newData._isChangeDiscount = 0;
                    __newData._isChangePrice = 0;
                    __newData._priceDefault = 0;
                    __newData._serialNumber = "";
                    __newData._isPromotionProduct = true;
                    __newData._amount = this._promotionResult[__loop]._discount;
                    this._itemProcess.Add(__newData);
                }
            }*/
            // ส่วนลดพิเศษ

            decimal __noPointAmount = 0M;

            for (int __loop = 0; __loop < _promotionDiscountResult.Count; __loop++)
            {
                _posClientItemClass __newData = new _posClientItemClass();
                __newData._barCode = "";
                __newData._itemCode = "";
                __newData._promotionCode = _promotionDiscountResult[__loop]._promotionCode;
                __newData._itemName = _promotionDiscountResult[__loop]._promotionName;
                __newData._itemType = 0;
                __newData._standValue = 0;
                __newData._divideValue = 0;
                __newData._unitCode = "";
                __newData._qtyLast = _promotionDiscountResult[__loop]._qty;
                __newData._qty = _promotionDiscountResult[__loop]._qty;
                __newData._taxType = 0;
                __newData._vatRate = 0;
                __newData._price = _promotionDiscountResult[__loop]._amount / _promotionDiscountResult[__loop]._qty;
                __newData._havePoint = false;
                __newData._discountWord = "";
                __newData._discount = 0;
                __newData._priceInfo = "";
                __newData._discountNumber = 0;
                __newData._isChangeDiscount = 0;
                __newData._isChangePrice = 0;
                __newData._priceDefault = 0;
                __newData._serialNumber = "";
                __newData._isPromotionProduct = true;
                __newData._amount = __newData._qty * __newData._price;
                __newData._no_discount_amount = _promotionDiscountResult[__loop]._no_discount_amount;
                if (_promotionDiscountResult[__loop]._noPointAmount > 0)
                {
                    __noPointAmount += _promotionDiscountResult[__loop]._noPointAmount;
                }

                this._itemProcess.Add(__newData);
            }
            // ประกอบ
            StringBuilder __message = new StringBuilder();
            __message.Append(MyLib._myGlobal._xmlHeader);
            __message.Append("<node>");
            for (int __loop = 0; __loop < this._itemProcess.Count; __loop++)
            {
                __message.Append("<item>");
                __message.Append("<BarCode>" + MyLib._myUtil._convertTextToXml(this._itemProcess[__loop]._barCode) + "</BarCode>");
                __message.Append("<ItemName>" + MyLib._myUtil._convertTextToXml(this._itemProcess[__loop]._itemName) + "</ItemName>");
                __message.Append("<Unit>" + MyLib._myUtil._convertTextToXml(this._itemProcess[__loop]._unitCode) + "</Unit>");
                __message.Append("<Qty>" + MyLib._myUtil._convertTextToXml(this._itemProcess[__loop]._qty.ToString()) + "</Qty>");
                __message.Append("<Price>" + MyLib._myUtil._convertTextToXml(this._itemProcess[__loop]._price.ToString()) + "</Price>");
                __message.Append("<Discount>" + MyLib._myUtil._convertTextToXml(this._itemProcess[__loop]._discount.ToString()) + "</Discount>");
                __message.Append("<DiscountWord>" + MyLib._myUtil._convertTextToXml(this._itemProcess[__loop]._discountWord.ToString()) + "</DiscountWord>");
                __message.Append("<" + _price_type_column + ">" + MyLib._myUtil._convertTextToXml(this._itemProcess[__loop]._price_type.ToString()) + "</" + _price_type_column + ">");
                __message.Append("<Total>" + MyLib._myUtil._convertTextToXml(this._itemProcess[__loop]._amount.ToString()) + "</Total>");
                string __vatType = "";
                switch (this._itemProcess[__loop]._taxType)
                {
                    case 0: __vatType = "V"; break;
                    case 1: __vatType = ""; break;
                }
                __message.Append("<VatType>" + MyLib._myUtil._convertTextToXml(__vatType) + "</VatType>");
                __message.Append("<IsDrink>" + MyLib._myUtil._convertTextToXml(this._itemProcess[__loop]._drink_type.ToString()) + "</IsDrink>");
                __message.Append("</item>");
            }
            __message.Append("</node>");
            // send กรณีมี grid หลาย อัน (สองจอ) ก็ให้ลงทั้งสอง grid โดยเช็ค control type
            // __sendMessage("add:item_grid",__message);
            // __sendMessage("add:item_grid,a1,a2",__message);
            // __sendMessage("change:label,a1,a2",__message);
            // คำนวณยอดรวม
            decimal __totalAmount = 0M;
            decimal __pointTotal = 0M;
            decimal __totalAmountVat = 0M;
            decimal __totalAmountExceptVat = 0M;
            decimal __totalFoodAmount = 0M;
            decimal __totalBeforeDiscount = 0M; // มูลค่าที่ลดได้
            decimal __totalDrinkAmount = 0M;
            decimal __totalAmountForCalcDiscount = 0M;

            for (int __loop = 0; __loop < this._itemProcess.Count; __loop++)
            {
                __totalAmount += this._itemProcess[__loop]._amount;

                __pointTotal += (this._itemProcess[__loop]._havePoint || _g.g._companyProfile._sum_point_all) ? this._itemProcess[__loop]._amount : 0M;
                if (this._itemProcess[__loop]._isPromotionProduct == false)
                {
                    if (this._itemProcess[__loop]._taxType == 0)
                    {
                        //__totalVat += Math.Round(this._itemProcess[__loop]._amount * (_g.g._companyProfile._vat_rate / 100.0M), _g.g._companyProfile._item_amount_decimal);
                        __totalAmountVat += this._itemProcess[__loop]._amount;
                    }
                    else
                    {
                        __totalAmountExceptVat += this._itemProcess[__loop]._amount;
                    }
                }

                // คำณวนยอดอาหารและเครื่องดื่ม
                if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro)
                {
                    if (this._itemProcess[__loop]._isPromotionProduct == false)
                    {
                        if (this._itemProcess[__loop]._drink_type == 0)
                        {
                            if (this._itemProcess[__loop]._no_discount == false)
                            {
                                __totalBeforeDiscount += this._itemProcess[__loop]._amount;
                            }
                            __totalFoodAmount += this._itemProcess[__loop]._amount;
                            //__totalBeforeDiscount += this._itemProcess[__loop]._amount;

                        }
                        else
                        {
                            __totalDrinkAmount += this._itemProcess[__loop]._amount;
                        }
                    }
                    else
                    {
                        // เอา Promotion มาตัดยอดรวมอาหารออก // (เป็นยอดลบ)
                        __totalFoodAmount += this._itemProcess[__loop]._no_discount_amount;
                        __totalBeforeDiscount += this._itemProcess[__loop]._no_discount_amount;
                    }
                }

                // sum price for calc discount
                if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro)
                {
                    // เอาอาหาร เท่านั้น และ ตัดรายการ promotion ออกไป
                }
                else
                {

                }
            }
            // คำนวณหาแต้มสะสม ของบิล
            this._pointSum = 0M;
            //if (this._memberCode.Trim().Length > 0 && (this._memberCode.Equals(this._posDefaultCust) == false))
            if (this._point_member_only == false || (this._point_member_only == true && this._memberCode.Trim().Length > 0))
            {
                this._pointSum = (this._pointDivAmount == 0M) ? 0M : Math.Floor((__pointTotal - __noPointAmount) / this._pointDivAmount);

                // toe x แต้ม
                if (this._pointSpecialDayMultiply > 0)
                {
                    this._pointSum *= this._pointSpecialDayMultiply;
                }

                if (this._usePointMonthBirth && this._isBirthMonthMember && this._pointMultiplyMonthBirth > 0)
                {
                    this._pointSum *= this._pointMultiplyMonthBirth;
                }

                if (this._usePointBirthDay && this._isBirthDayMember && this._pointMultiplyBirthDay > 0)
                {
                    this._pointSum *= this._pointMultiplyBirthDay;
                }

            }


            #region หาส่วนลดอาหาร จาก F7

            //if (__totalFoodAmount < 0)
            //    __totalFoodAmount = 0;
            if (__totalBeforeDiscount < 0)
                __totalBeforeDiscount = 0;
            // หาส่วนลด ต้มยำกุ้ง
            decimal __discountFoodAmount = 0M;
            if (this._foodDiscountWord.Length > 0 && (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro))
            {
                decimal __totalAfterDiscount = MyLib._myGlobal._calcAfterDiscount(this._foodDiscountWord, MyLib._myGlobal._round(__totalBeforeDiscount, _g.g._companyProfile._item_price_decimal), _g.g._companyProfile._item_price_decimal);
                // คำณวนส่วนลด
                __discountFoodAmount = __totalBeforeDiscount - __totalAfterDiscount;

                this._foodDiscountAmount = __discountFoodAmount;
                // __totalAmount -= __discountFoodAmount;

                if (_sendMessage != null)
                {
                    _sendMessage("update:label,food_discount", String.Format("{0}{1} : {2}", MyLib._myGlobal._resource("ส่วนลด "), ((this._foodDiscountWord.IndexOf("%") != -1) ? this._foodDiscountWord : ""), this._foodDiscountAmount.ToString(this._formatNumber1)), "");
                }
            }

            #endregion


            // check
            if (this._serviceChargeWord.Length > 0)
            {
                string __checkStrNumber = _serviceChargeWord.Replace("%", string.Empty);
                if (MyLib._myGlobal._decimalPhase(__checkStrNumber) < 0)
                {
                    _serviceChargeWord = "";
                }
            }

            // service charge
            decimal __serviceChargeAmount = 0M;
            if (this._serviceChargeWord.Length > 0)
            {


                __serviceChargeAmount = __totalAmount - MyLib._myGlobal._calcAfterDiscount(this._serviceChargeWord, MyLib._myGlobal._round(__totalAmount, _g.g._companyProfile._item_price_decimal), _g.g._companyProfile._item_price_decimal);
                if (_sendMessage != null)
                {
                    _sendMessage("update:label,service_charge", String.Format("{0}{1} : {2}", MyLib._myGlobal._resource("Service Charge "), ((this._serviceChargeWord.IndexOf("%") != -1) ? this._serviceChargeWord : ""), __serviceChargeAmount.ToString(this._formatNumber1)), "");
                }
            }


            // ลดขั้นสุดท้าย หาจาก total amount ( ท้ายบิล)
            decimal __afterDiscount = 0M;
            decimal __discountAmount = 0M;

            if (this._discountWord.Length > 0)
            {
                __afterDiscount = MyLib._myGlobal._calcAfterDiscount(this._discountWord, MyLib._myGlobal._round((__totalAmount + __serviceChargeAmount), _g.g._companyProfile._item_price_decimal), _g.g._companyProfile._item_price_decimal);
                __discountAmount = (__totalAmount + __serviceChargeAmount) - __afterDiscount;
                // __totalAmount -= __discountAmount;
            }

            // toe check option
            // ชาร์ท ก่อนลด
            // ส่วนลด
            __totalAmount = __totalAmount - (__discountFoodAmount + __discountAmount);


            // vat calc

            decimal __vatExcludeAmount = 0M;
            if (this._posConfig._pos_vat_type == 1)
            {
                decimal __discountNoVatAmount = 0M;
                decimal __beforeVat = 0M;
                if ((__totalAmountVat + __serviceChargeAmount) < (__discountAmount + __discountFoodAmount))
                {
                    __beforeVat = 0;
                    __discountNoVatAmount = ((__discountAmount + __discountFoodAmount) - __totalAmountVat);

                }
                else
                {
                    __beforeVat = ((__totalAmountVat + __serviceChargeAmount) - (__discountAmount + __discountFoodAmount));
                }

                decimal __vatValue = MyLib._myGlobal._round(__beforeVat * (_g.g._companyProfile._vat_rate / 100.0M), _g.g._companyProfile._item_amount_decimal);
                // decimal __afterVat = __beforeVat + __vatValue;
                //__totalAmount = __totalAmountExceptVat + __afterVat;
                __vatExcludeAmount = __vatValue;
                __totalAmount += __vatValue;
            }


            // ปัดเศษ
            decimal __totalAfterDiff = _posProcess._diffProcess(__totalAmount + __serviceChargeAmount, this._posConfig);

            decimal __finalBalance = Math.Floor(__totalAmount + __serviceChargeAmount) + __totalAfterDiff;
            decimal __diffAmount = (__finalBalance - __totalAmount); // (this._finalBalance - this._totalAfterDiscount) * -1;

            //
            if (_sendMessage != null)
            {
                _sendMessage("update:label,vat_amount", MyLib._myGlobal._resource("สินค้ามีภาษี") + " : " + __totalAmountVat.ToString(this._formatNumber1), "");
                _sendMessage("update:label,except_vat_amount", MyLib._myGlobal._resource("สินค้ายกเว้นภาษี") + " : " + __totalAmountExceptVat.ToString(this._formatNumber1), "");
                _sendMessage("update:label,total_amount", __finalBalance.ToString(this._formatNumber1), "");
                _sendMessage("update:label,point_info", String.Format("{0} : {1} - {2} : {3}", MyLib._myGlobal._resource("แต้มยกมา"), this._pointBalance.ToString(this._formatNumber1), MyLib._myGlobal._resource("แต้มใหม่"), this._pointSum.ToString(this._formatNumber1)), "");
                _sendMessage("update:label,diff_amount", MyLib._myGlobal._resource("ยอดปัดเศษ") + " : " + __diffAmount.ToString(this._formatNumber1), "");

                // food & drink
                _sendMessage("update:label,food_amount", String.Format("{0} : {1}", MyLib._myGlobal._resource("ยอดอาหาร"), __totalFoodAmount.ToString(this._formatNumber1)), "");
                _sendMessage("update:label,drink_amount", String.Format("{0} : {1}", MyLib._myGlobal._resource("ยอดเครื่องดื่ม"), __totalDrinkAmount.ToString(this._formatNumber1)), "");
                _sendMessage("update:label,exvat_amount", String.Format("{0} : {1}", MyLib._myGlobal._resource("ภาษีมูลค่าเพิ่ม "), __vatExcludeAmount.ToString(this._formatNumber1)), "");


            }
            this._discountAmount = __discountAmount;
            this._totalAmount = __totalAmount;
            this._finalBalance = __finalBalance;
            this._diffAmount = __diffAmount;
            this._totalAmountVat = __totalAmountVat;
            this._totalAmountExceptVat = __totalAmountExceptVat;
            this._totalExtraDiscountAmount = (this._promotionDiscountAmount * -1); // +(this._discountAmout * -1);
            this._totalFoodAmount = __totalFoodAmount;
            this._totalDrinkAmount = __totalDrinkAmount;
            this._serviceChargeAmount = __serviceChargeAmount;
            this._vatExcludeAmount = __vatExcludeAmount;

            return __message.ToString();
        }
    }

    public delegate void sendMessageEvent(string __object, string __message, string serialNumber);
    public delegate void ProcessPromotionErrorErgs(string __object, string __message);
}
