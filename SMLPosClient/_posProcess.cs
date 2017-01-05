using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace SMLPosClient
{
    public class _posProcess
    {
        public static decimal _diffProcess(decimal totalAmount, SMLPOSControl._posScreenConfig _posConfig)
        {
            //decimal __diff = 0M;
            decimal __diff = totalAmount - Math.Floor(totalAmount);

            if (_posConfig._round_type == 1 && _posConfig._round_table.Length > 0 && _posConfig._round_table_list.Count > 0)
            {
                foreach (SMLPOSControl._roundTable __round in _posConfig._round_table_list)
                {
                    if (__diff >= __round._fromValue && __diff <= __round._toValue)
                    {
                        __diff = __round._roundValue;
                        break;
                    }
                }
            }
            else
            {
                // ปัดเหมือนเดิม
                if (__diff >= 0.01M && __diff <= 0.24M) __diff = 0M;
                else
                    if (__diff >= 0.25M && __diff <= 0.49M) __diff = 0.25M;
                else
                        if (__diff >= 0.50M && __diff <= 0.74M) __diff = 0.5M;
                else
                            if (__diff >= 0.75M && __diff <= 0.99M) __diff = 0.75M;
            }
            return __diff;
        }

        public static _itemPriceClass _findPOSPrice(string _custCode, string _memberCode, string _posDefaultCust, bool _isStaff, string itemCode, string barCode, decimal qty, string unitCode, decimal priceNormal, decimal memberPrice, DataRow[] price1, DataRow[] price2, DataRow[] price3, DataRow[] price4, DataRow[] price5)
        {
            _itemPriceClass __result = new _itemPriceClass();
            string _formatNumber1 = "#,0.00";

            Boolean __foundPriceOrDiscount = false;
            int __discountNumber = 0;
            string __priceInfo = "";

            decimal __priceMember = memberPrice;
            int __price_type = 5;
            decimal __price_stand = 0M;
            decimal __price = 0M;
            string __discountWord = "";

            __price = priceNormal;
            __discountWord = "";
            string __staffDiscount = (price5 == null || price5.Length == 0) ? "" : price5[0][_g.d.ic_inventory_barcode_price._discount_word].ToString().Trim();
            string __extraDiscount = (price1 == null || price1.Length == 0) ? "" : price1[0][_g.d.ic_inventory_barcode_price._discount_word].ToString().Trim();
            string __extraDiscountMember = (price2 == null || price2.Length == 0) ? "" : price2[0][_g.d.ic_inventory_barcode_price._discount_word].ToString().Trim();
            string __discount = (price3 == null || price3.Length == 0) ? "" : price3[0][_g.d.ic_inventory_barcode_price._discount_word].ToString().Trim();
            string __discountMember = (price4 == null || price4.Length == 0) ? "" : price4[0][_g.d.ic_inventory_barcode_price._discount_word].ToString().Trim();
            //
            if (_memberCode.Length > 0 && (_memberCode.Equals(_posDefaultCust) == false) && _isStaff && __staffDiscount.Length > 0)
            {
                if (__staffDiscount.IndexOf("#") == 0)
                {
                    int __startDiscountPoint = __staffDiscount.IndexOf(",");

                    __price = MyLib._myGlobal._decimalPhase(__staffDiscount.Substring(1, (__startDiscountPoint == -1) ? __staffDiscount.Length - 1 : __startDiscountPoint));
                    __priceInfo = MyLib._myGlobal._resource("ราคาพนักงาน") + " " + __price.ToString(_formatNumber1);
                    __foundPriceOrDiscount = true;

                    // เช็คส่วนลด
                    if (__startDiscountPoint != -1)
                    {
                        __discountWord = __staffDiscount.Substring(__startDiscountPoint + 1, __staffDiscount.Length - (__startDiscountPoint + 1));
                        __priceInfo = MyLib._myGlobal._resource("ส่วนลดพนักงาน") + " " + __discountWord;
                        __discountNumber = 5;
                    }
                }
                else
                {
                    // ราคาพนักงาน
                    __foundPriceOrDiscount = true;
                    __discountWord = __staffDiscount;
                    __priceInfo = MyLib._myGlobal._resource("ส่วนลดพนักงาน") + " " + __discountWord;
                    __discountNumber = 5;
                }
            }
            if (__foundPriceOrDiscount == false)
            {
                // ส่วนลดพิเศษ (ตามฤดู)
                if (_memberCode.Length > 0 && (_memberCode.Equals(_posDefaultCust) == false))
                {
                    // สมาชิก
                    string __memberDiscount = __extraDiscountMember;
                    if (__memberDiscount.Length > 0)
                    {
                        if (__memberDiscount.IndexOf("#") == 0)
                        {
                            int __startDiscountPoint = __memberDiscount.IndexOf(",");

                            __price = MyLib._myGlobal._decimalPhase(__memberDiscount.Substring(1, (__startDiscountPoint == -1) ? __memberDiscount.Length - 1 : __startDiscountPoint));
                            __priceInfo = MyLib._myGlobal._resource("ราคาพิเศษ+สมาชิก") + " " + __price.ToString(_formatNumber1);
                            __foundPriceOrDiscount = true;

                            // เช็คส่วนลด
                            if (__startDiscountPoint != -1)
                            {
                                __discountWord = __memberDiscount.Substring(__startDiscountPoint + 1, __memberDiscount.Length - (__startDiscountPoint + 1));
                                __priceInfo = MyLib._myGlobal._resource("ส่วนลดพิเศษ+สมาชิก") + " " + __discountWord;
                                __discountNumber = 2;
                            }
                        }
                        else
                        {
                            __discountWord = __extraDiscountMember;
                            if (__priceInfo.Length > 0)
                            {
                                __priceInfo = __priceInfo + " ";
                            }
                            __priceInfo = __priceInfo + MyLib._myGlobal._resource("ส่วนลดพิเศษ+สมาชิก") + " " + __discountWord;
                            __foundPriceOrDiscount = true;
                            __discountNumber = 2;
                        }
                    }
                }
                if (__foundPriceOrDiscount == false)
                {
                    string __promotionDiscount = __extraDiscount;
                    if (__promotionDiscount.Length > 0)
                    {
                        if (__promotionDiscount.IndexOf("#") == 0)
                        {
                            int __startDiscountPoint = __promotionDiscount.IndexOf(",");

                            __price = MyLib._myGlobal._decimalPhase(__promotionDiscount.Substring(1, (__startDiscountPoint == -1) ? __promotionDiscount.Length - 1 : __startDiscountPoint));
                            __priceInfo = MyLib._myGlobal._resource("ราคาพิเศษ+โปรโมชั่น") + " " + __price.ToString(_formatNumber1);
                            __foundPriceOrDiscount = true;

                            // เช็คส่วนลด
                            if (__startDiscountPoint != -1)
                            {
                                __discountWord = __promotionDiscount.Substring(__startDiscountPoint + 1, __promotionDiscount.Length - (__startDiscountPoint + 1));
                                __priceInfo = MyLib._myGlobal._resource("ส่วนลดพิเศษ+โปรโมชั่น") + " " + __discountWord;
                                __discountNumber = 1;
                            }
                        }
                        else
                        {
                            __discountWord = __promotionDiscount;
                            __foundPriceOrDiscount = true;
                            if (__priceInfo.Length > 0)
                            {
                                __priceInfo = __priceInfo + " ";
                            }
                            __priceInfo = __priceInfo + MyLib._myGlobal._resource("ส่วนลดพิเศษ+โปรโมชั่น") + " " + __discountWord;
                            __discountNumber = 1;
                        }
                    }
                }
                // ส่วนลดทั่วไป
                if (__foundPriceOrDiscount == false && _memberCode.Length > 0 && (_memberCode.Equals(_posDefaultCust) == false))
                {
                    // สมาชิก
                    string __memberDiscount = __discountMember;
                    if (__memberDiscount.Length > 0)
                    {
                        if (__memberDiscount.IndexOf("#") == 0)
                        {
                            int __startDiscountPoint = __memberDiscount.IndexOf(",");

                            __price = MyLib._myGlobal._decimalPhase(__memberDiscount.Substring(1, (__startDiscountPoint == -1) ? __memberDiscount.Length - 1 : __startDiscountPoint));
                            __priceInfo = MyLib._myGlobal._resource("ราคาสมาชิก") + " " + __price.ToString(_formatNumber1);
                            __foundPriceOrDiscount = true;


                            // เช็คส่วนลด
                            if (__startDiscountPoint != -1)
                            {
                                __discountWord = __memberDiscount.Substring(__startDiscountPoint + 1, __memberDiscount.Length - (__startDiscountPoint + 1));
                                __priceInfo = MyLib._myGlobal._resource("ส่วนลดสมาชิก") + " " + __discountWord;
                                __discountNumber = 4;
                            }

                        }
                        else
                        {
                            __discountWord = __memberDiscount;
                            if (__priceInfo.Length > 0)
                            {
                                __priceInfo = __priceInfo + " ";
                            }
                            __priceInfo = __priceInfo + MyLib._myGlobal._resource("ส่วนลดสมาชิก") + " " + __discountWord;
                            __foundPriceOrDiscount = true;
                            __discountNumber = 4;
                        }
                    }
                }
                if (__foundPriceOrDiscount == false)
                {
                    // Promotion
                    string __promotionDiscount = __discount;
                    if (__promotionDiscount.Length > 0)
                    {
                        if (__promotionDiscount.IndexOf("#") == 0)
                        {
                            int __startDiscountPoint = __promotionDiscount.IndexOf(",");

                            __price = MyLib._myGlobal._decimalPhase(__promotionDiscount.Substring(1, (__startDiscountPoint == -1) ? __promotionDiscount.Length - 1 : __startDiscountPoint));
                            __priceInfo = MyLib._myGlobal._resource("ราคาโปรโมชั่น") + " " + __price.ToString(_formatNumber1);
                            __foundPriceOrDiscount = true;

                            // เช็คส่วนลด
                            if (__startDiscountPoint != -1)
                            {
                                __discountWord = __promotionDiscount.Substring(__startDiscountPoint, __promotionDiscount.Length - __startDiscountPoint);
                                __priceInfo = " " + MyLib._myGlobal._resource("ส่วนลดโปรโมชั่น") + " " + __discountWord;
                                __discountNumber = 3;
                            }

                        }
                        else
                        {
                            __discountWord = __promotionDiscount;
                            __foundPriceOrDiscount = true;
                            if (__priceInfo.Length > 0)
                            {
                                __priceInfo = __priceInfo + " ";
                            }
                            __priceInfo = __priceInfo + MyLib._myGlobal._resource("ส่วนลดโปรโมชั่น") + " " + __discountWord;
                            __discountNumber = 3;
                        }
                    }
                }

                // toe หยิบราคาตาม สมาชิก และทั่วไป
                //toe if (__foundPriceOrDiscount == false) 
                //toe {
                if (__foundPriceOrDiscount == false)
                {
                    // ปรกติ
                    if (_memberCode.Length > 0 && __priceMember > 0M && (_memberCode.Equals(_posDefaultCust) == false))
                    {
                        __price = __priceMember;
                        __foundPriceOrDiscount = true;
                        __priceInfo = MyLib._myGlobal._resource("ราคาสมาชิก") + " " + __price.ToString(_formatNumber1);
                        __price_type = 7;
                    }
                    else
                        if (priceNormal > 0M)
                    {
                        __price = priceNormal;
                        __foundPriceOrDiscount = true;
                        __priceInfo = MyLib._myGlobal._resource("ราคาปรกติ") + " " + __price.ToString(_formatNumber1);
                    }
                }
                //toe }
            }
            if (__foundPriceOrDiscount == false)
            {
                /*string __message = "ไม่พบราคาสินค้า" + " : " + __barCode + " : " + __getData.Rows[0][_g.d.ic_inventory_barcode._description].ToString();
                MessageBox.Show(__message);
                _sendMessage("update:label,price_info", __message);*/
                //if (__getData.Rows.Count > 0)
                //{
                string __today = MyLib._myGlobal._convertDateToQuery(DateTime.Now);
                //string __itemCode = __getData.Rows[0][_g.d.ic_inventory_barcode._ic_code].ToString();
                //string __unitCode = __getData.Rows[0][_g.d.ic_inventory_barcode._unit_code].ToString();

                if (_g.g._companyProfile._use_price_center)
                {
                    SMLInventoryControl._findPriceCenter __priceCenter = new SMLInventoryControl._findPriceCenter();
                    SMLInventoryControl._icTransItemGridControl._priceStruct __resultCenter = __priceCenter._findPrice(MyLib._myGlobal._branchCode, itemCode, barCode, unitCode, qty, _custCode, _memberCode, false, "", __today, _g.g._vatTypeEnum.ภาษีรวมใน, 0, 1, 0);
                    __price = __resultCenter._price;
                    __price_type = __resultCenter._type;
                    __price_stand = __resultCenter._price;
                    __discountWord = "";

                }
                else
                {


                    SMLInventoryControl._findPriceClass __getPrice = new SMLInventoryControl._findPriceClass();
                    SMLInventoryControl._icTransItemGridControl._priceStruct __priceResult = __getPrice._findPriceByItem(itemCode, barCode, unitCode, qty, _custCode, _memberCode, false, "", __today, _g.g._vatTypeEnum.ภาษีรวมใน, 0, 1, 0);
                    __price = __priceResult._price;
                    __price_type = __priceResult._type;
                    __price_stand = __priceResult._stand_price;
                    __discountWord = __priceResult._discountWord;
                    if (__price == 0)
                    {
                        // เอาราคารวมในมาเพิ่ม vat
                        __price = MyLib._myGlobal._round(__priceResult._price1 + (__priceResult._price1 * (_g.g._companyProfile._vat_rate / 100M)), 2);
                    }
                    //}
                }
            }

            // pack result ส่งกลับ

            __result._discountWord = __discountWord;
            __result._price = __price;
            __result._priceInfo = __priceInfo;
            __result._discountNumber = __discountNumber;
            __result._price_type = __price_type;


            return __result;
        }

        public static decimal _pointConditionProcess(string pointConditinStr, decimal pointQty)
        {
            decimal __result = 0M;

            List<SMLPOSControl._roundTable> __roundPointList = new List<SMLPOSControl._roundTable>();
            try
            {
                string[] __roundSplit = pointConditinStr.Split(',');

                foreach (string __round in __roundSplit)
                {
                    //Regex __tableRoundRegex = new Regex(@"^(0\.+[0-9]{1,2})+\-+(0\.+[0-9]{1,2})\=+([0-1]+\.+[0-9]{1,2})");
                    Regex __tableRoundRegex = new Regex(@"^([0-9]{1,10})+\-+([0-9]{0,10})\=+([0-9]{1,10})");
                    if (__tableRoundRegex.IsMatch(__round))
                    {
                        MatchCollection __matcgh = __tableRoundRegex.Matches(__round);
                        decimal __fromValue = MyLib._myGlobal._decimalPhase(__matcgh[0].Groups[1].Value.ToString());
                        decimal __toValue = MyLib._myGlobal._decimalPhase(__matcgh[0].Groups[2].Value.ToString());
                        decimal __roundValue = MyLib._myGlobal._decimalPhase(__matcgh[0].Groups[3].Value.ToString());

                        __roundPointList.Add(new SMLPOSControl._roundTable() { _fromValue = __fromValue, _toValue = __toValue, _roundValue = __roundValue });
                    }
                }
            }
            catch
            {
            }

            foreach (SMLPOSControl._roundTable __round in __roundPointList)
            {
                if (pointQty >= __round._fromValue && pointQty <= __round._toValue)
                {
                    __result = __round._roundValue;
                    break;
                }
            }

            return __result;
        }

    }

    public class _totalStruct
    {
        public decimal _totalAmount;
    }
}
