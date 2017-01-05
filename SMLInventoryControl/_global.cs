using System;
using System.Collections.Generic;
using System.Text;

namespace SMLInventoryControl
{
    public class _gForm
    {
        public static string[] _number = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        public static string[] _numstr = { "สตางค์", "บาท", "เอ็ด", "ยี่", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า" };
        public static string[] _numunt = { "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };
        public static string[] _strNumber = { "ถ้วน" };

        static string[] ones = new string[] { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
        static string[] teens = new string[] { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        static string[] tens = new string[] { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
        static string[] thousandsGroups = { "", " Thousand", " Million", " Billion" };

        //Convert Number to Baht String
        public static string _convertNumToBaht(string result)
        {
            if (result.Equals(0))
                return "ศูนย์บาท";

            if (result == "")
                return "";

            string format2 = MyLib._myGlobal._getFormatNumber("m02");
            string format0 = MyLib._myGlobal._getFormatNumber("m00");
            string __result = "";
            string[] _strsplit = Convert.ToDouble(result).ToString("0.00").Split('.');
            string _firstNum = "";
            string _seconNum = "";
            string __strNum1 = "";
            string __strNum2 = "";
            if (_strsplit.Length == 2)
            {

                _firstNum = _strsplit[0].ToString();
                //double __setNumber = Double.Parse(_strsplit[1].ToString());
                if (_strsplit[1].ToString().Length == 2)
                {
                    //_seconNum = _convertStang(string.Format(format0, __setNumber));
                    _seconNum = _convertStang(_strsplit[1].ToString());

                }
                else
                {
                    _seconNum = _convertStang("00");
                }
            }
            else
            {
                _firstNum = _strsplit[0].ToString();
                _seconNum = _convertStang("00");
            }
            if (_firstNum.Length <= 7)
            {
                if (_firstNum.Length > 1)
                {
                    _firstNum = _convertBaht(_strsplit[0].ToString());
                }
                else
                {
                    for (int loops = 0; loops < _number.Length; loops++)
                    {
                        if (_firstNum.Equals(_number[loops].ToString()) && result.CompareTo("0") != 0)
                        {
                            _firstNum = _numstr[loops + 3].ToString();
                        }
                    }
                }
            }
            else if (_firstNum.Length > 7 && _firstNum.Length < 13)
            {
                for (int loop = 0; loop < _firstNum.Length; loop++)
                {
                    if (_firstNum.Substring(loop).Length < 7)
                    {
                        if (__strNum2.Length <= 0)
                        {
                            __strNum2 = "0" + _firstNum.Substring(loop).ToString();
                        }
                    }
                    else
                    {
                        __strNum1 = __strNum1 + _firstNum.Substring(loop, 1).ToString();
                    }
                }
                if (__strNum1.Length > 0)
                {
                    _firstNum = _convertBaht(__strNum1.ToString()) + _numunt[5].ToString() + _convertBaht(__strNum2.ToString());// +_numstr[1].ToString();
                }
                else
                {
                    _firstNum = _convertBaht(__strNum2.ToString());
                }
            }
            else if (_firstNum.Length >= 13)
            {
            }
            if (_seconNum.Length > 0)
            {
                __result = _firstNum + _numstr[1].ToString() + _seconNum + _numstr[0].ToString();
            }
            else
            {
                __result = _firstNum + _numstr[1].ToString() + _strNumber[0].ToString();
            }
            return __result.ToString();
        }

        public static string _convertBaht(string result)
        {
            string __result = "";
            string __numStr1 = result;
            string __strNumber = "";
            string __strResult = "";
            string __strResult2 = "";
            bool __chekNumber = false;
            for (int loop = 0; loop < result.Length; loop++)
            {
                __strNumber = result.Substring(loop, 1);
                for (int loops = 0; loops < _number.Length; loops++)
                {
                    if (__strNumber.Equals(_number[loops].ToString()) && __strNumber.CompareTo("0") != 0)
                    {
                        __strResult = _numstr[loops + 3].ToString();
                    }
                }
                if (result.Substring(loop).Length > 2 && result.Substring(loop).Length <= 7)
                {
                    if (__strNumber.CompareTo("0") != 0)
                    {
                        if (result.Substring(loop).Length == 7)
                        {
                            __strResult = __strResult + _numunt[5].ToString();
                        }
                        if (result.Substring(loop).Length == 6)
                        {
                            __strResult = __strResult + _numunt[4].ToString();
                        }
                        if (result.Substring(loop).Length == 5)
                        {
                            __strResult = __strResult + _numunt[3].ToString();
                        }
                        if (result.Substring(loop).Length == 4)
                        {
                            __strResult = __strResult + _numunt[2].ToString();
                        }
                        if (result.Substring(loop).Length == 3)
                        {
                            __strResult = __strResult + _numunt[1].ToString();
                        }
                    }
                }
                else
                {
                    if (__chekNumber == false)
                    {
                        __chekNumber = true;
                        __strResult2 = result.Substring(loop);
                    }
                    __strResult = "";
                }
                __result = __result + __strResult.ToString();
                __strResult = "";
            }
            if (__chekNumber == true)
            {
                __strResult2 = _convertStang(__strResult2);// +_numstr[1].ToString();
            }
            __result = __result + __strResult2;
            return __result.ToString();
        }

        public static string _convertStang(string result)
        {
            string __result = "";
            string __numStr1 = result.Substring(0, 1);
            string __numStr2 = result.Substring(1, 1);
            if (__numStr1.Equals(_number[0].ToString()))
            {
                for (int loop = 0; loop < _number.Length; loop++)
                {
                    if (__numStr2.Equals(_number[loop].ToString()) && __numStr2.CompareTo("0") != 0)
                    {
                        __result = _numstr[loop + 3].ToString();
                    }
                }
            }
            else
            {
                if (__numStr2.Equals(_number[0].ToString()))
                {
                    if (__numStr1.Equals(_number[1].ToString()))
                    {
                        __result = _numunt[0].ToString();
                    }
                    else if (__numStr1.Equals(_number[2].ToString()))
                    {
                        __result = _numstr[3].ToString() + _numunt[0].ToString();
                    }
                    else
                    {
                        for (int loop = 0; loop < _number.Length; loop++)
                        {
                            if (__numStr1.Equals(_number[loop].ToString()))
                            {
                                __result = _numstr[loop + 3].ToString() + _numunt[0].ToString();
                            }
                        }
                    }
                }
                else
                {
                    if (__numStr1.Equals(_number[1].ToString()) && __numStr2.Equals(_number[1].ToString()))
                    {
                        __result = _numunt[0].ToString() + _numstr[2].ToString();
                    }
                    else if (__numStr1.Equals(_number[2].ToString()) && __numStr2.Equals(_number[1].ToString()))
                    {
                        __result = _numstr[3].ToString() + _numunt[0].ToString() + _numstr[2].ToString();
                    }
                    else
                    {
                        if (__numStr2.Equals(_number[1].ToString()))
                        {
                            for (int loop = 0; loop < _number.Length; loop++)
                            {
                                if (__numStr1.Equals(_number[loop].ToString()))
                                {
                                    __result = _numstr[loop + 3].ToString() + _numunt[0].ToString() + _numstr[2].ToString();
                                }
                            }
                        }
                        else
                        {
                            if (__numStr1.Equals(_number[1].ToString()))
                            {
                                for (int loopj = 0; loopj < _number.Length; loopj++)
                                {
                                    if (__numStr2.Equals(_number[loopj].ToString()))
                                    {
                                        __result = _numunt[0].ToString() + _numstr[loopj + 3].ToString();
                                    }
                                }
                            }
                            else if (__numStr1.Equals(_number[2].ToString()))
                            {
                                for (int loopj = 0; loopj < _number.Length; loopj++)
                                {
                                    if (__numStr2.Equals(_number[loopj].ToString()))
                                    {
                                        __result = _numstr[3].ToString() + _numunt[0].ToString() + _numstr[loopj + 3].ToString();
                                    }
                                }
                            }
                            else
                            {
                                for (int loop = 0; loop < _number.Length; loop++)
                                {
                                    if (__numStr1.Equals(_number[loop].ToString()))
                                    {
                                        for (int loopj = 0; loopj < _number.Length; loopj++)
                                        {
                                            if (__numStr2.Equals(_number[loopj].ToString()))
                                            {
                                                __result = _numstr[loop + 3].ToString() + _numunt[0].ToString() + _numstr[loopj + 3].ToString();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return __result.ToString();

        }

        public static string _convertNumToDolla(string result)
        {
            string __result = "";
            try
            {
                string[] _strsplit = Convert.ToDouble(result).ToString("0.00").Split('.');
                string _firstNum = "";
                string _seconNum = "";
                if (_strsplit.Length == 1)
                {
                    _firstNum = IntegerToWritten(MyLib._myGlobal._intPhase(_strsplit[0]));
                }
                else if (_strsplit.Length == 2)
                {
                    _firstNum = IntegerToWritten(MyLib._myGlobal._intPhase(_strsplit[0]));
                    _seconNum = IntegerToWrittenII(MyLib._myGlobal._intPhase(_strsplit[1]));
                }
                if ((_firstNum.Length > 0) && (_seconNum.Length <= 0))
                {
                    __result = _firstNum;
                }
                else if ((_firstNum.Length > 0) && (_seconNum.Length > 0))
                {
                    __result = _firstNum + " and " + _seconNum;
                }
            }
            catch (Exception ex)
            {
            }
            return __result;
        }

        public static string IntegerToWrittenII(int n)
        {
            if (n == 0)
            {
                return "";
            }
            else if (n < 0)
            {
                return "";
            }

            return FriendlyInteger(n, "", 0);
        }


        private static string FriendlyInteger(int n, string leftDigits, int thousands)
        {
            if (n == 0)
            {
                return leftDigits;
            }
            string friendlyInt = leftDigits;
            if (friendlyInt.Length > 0)
            {
                friendlyInt += " ";
            }

            if (n < 10)
            {
                friendlyInt += ones[n];
            }
            else if (n < 20)
            {
                friendlyInt += teens[n - 10];
            }
            else if (n < 100)
            {
                friendlyInt += FriendlyInteger(n % 10, tens[n / 10 - 2], 0);
            }
            else if (n < 1000)
            {
                friendlyInt += FriendlyInteger(n % 100, (ones[n / 100] + " Hundred"), 0);
            }
            else
            {
                friendlyInt += FriendlyInteger(n % 1000, FriendlyInteger(n / 1000, "", thousands + 1), 0);
            }

            return friendlyInt + thousandsGroups[thousands];
        }

        public static string IntegerToWritten(int n)
        {
            if (n == 0)
            {
                return "Zero";
            }
            else if (n < 0)
            {
                return "Negative " + IntegerToWritten(-n);
            }

            return FriendlyInteger(n, "", 0);
        }

        public class _printoption
        {
            public string _paperSource;
            public int _printQuality;
            public int _box1;
            public int _box2;
            public int _box3;
            public int _box4;
            public int _box5;
            public bool _printerSelect;
        }

        public enum _formEnum
        {
            ใบเสนอราคา,
            ขาย_ขายสินค้าและบริการ,
            ขาย_ขายสินค้าและบริการ_ici,
            ขาย_ใบสั่งขาย,
            ขาย_สั่งจองสินค้าและสั่งซื้อสินค้า,
            ขาย_เพิ่มหนี้,
            ขาย_รับเงินล่วงหน้า,
            ขาย_คืนเงินล่วงหน้า,
            ขาย_ลดหนี้,
            ใบกำกับภาษี,
            ใบแจ้งหนี้,
            ใบซื้อสินค้าและบริการ,
            ซื้อ_ซื้อสินค้าและค่าบริการ,
            ซื้อ_ใบสั่งซื้อ,
            ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด,
            ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด,
            ซื้อ_จ่ายเงินมัดจำหรือเงินล่วงหน้า,
            ซื้อ_คืนเงินมัดจำหรือเงินล่วงหน้า,
            เจ้าหนี้_ใบรับวางบิล,
            เจ้าหนี้_ใบจ่ายชำระหนี้,
            ลูกหนี้_ใบวางบิล,
            ลูกหนี้_รับชำระหนี้,
            สินค้า_รับสินค้าสำเร็จรูป,
            สินค้า_เบิกสินค้าวัตถุดิบ,
            สินค้า_รับคืนสินค้าจากการเบิก,
            สินค้า_โอนเข้า,
            สินค้า_โอนออก,
            ซื้อ_พาเชียล_ตั้งหนี้,
            ซื้อ_พาเชียล_รับสินค้า,
            ซื้อ_พาเชียล_ราคาผิด,
            ซื้อ_พาเชียล_ลดหนี้,
            ซื้อ_พาเชียล_เพิ่มหนี้
        }
    }
}
