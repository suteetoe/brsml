using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace SMLERPIC
{
    public class _processPromotion
    {
        private string[] _dayName = { "อาทิตย์", "จันทร์", "อังคาร", "พุธ", "พฤหัสบดี", "ศุกร์", "เสาร์" };
        public ArrayList _dateBegin = new ArrayList();
        public ArrayList _dateEnd = new ArrayList();
        public ArrayList _timeBegin = new ArrayList();
        public ArrayList _timeEnd = new ArrayList();
        public ArrayList _day = new ArrayList();
        public List<_processPromotionDetail> _detail = new List<_processPromotionDetail>();
        public string _result = "";
        public string _resultHtml;

        public void _process(string source, Boolean genHtml)
        {
            string __pattern1 = @"{|}";
            string __pattern2 = @"\[|\]";
            string[] __listStr = Regex.Split(source, "\r\n");
            for (int __line = 0; __line < __listStr.Length; __line++)
            {
                if (__listStr[__line].Length > 0)
                {
                    if (__listStr[__line][0] == '*')
                    {
                        // เงื่อนไขเวลา
                        string __str = __listStr[__line].Remove(0, 1);
                        int __commandMode = 0; // 0=Date,1=Time,2=Day
                        foreach (string __result in Regex.Split(__str, __pattern1))
                        {
                            if (__result.Length > 0 && __result[0] != ',')
                            {
                                string __resultCase = __result.Trim().ToLower();
                                switch (__resultCase.ToLower())
                                {
                                    case "date": __commandMode = 0; break;
                                    case "time": __commandMode = 1; break;
                                    case "day": __commandMode = 2; break;
                                    default:
                                        string[] __split = __result.Split('@');
                                        if (__split.Length > 0)
                                        {
                                            CultureInfo __info = new CultureInfo("th-TH");
                                            string __begin = __split[0].ToString();
                                            string __end = (__split.Length == 2) ? __split[1].ToString() : "";
                                            DateTime __dateValue;
                                            switch (__commandMode)
                                            {
                                                case 0: // Date
                                                    if (DateTime.TryParse(__begin, __info, DateTimeStyles.None, out __dateValue))
                                                    {
                                                        this._dateBegin.Add(__dateValue);
                                                    }
                                                    else
                                                    {
                                                        this._dateBegin.Add(null);
                                                    }
                                                    if (DateTime.TryParse(__end, __info, DateTimeStyles.None, out __dateValue))
                                                    {
                                                        this._dateEnd.Add(__dateValue);
                                                    }
                                                    else
                                                    {
                                                        this._dateEnd.Add(null);
                                                    }
                                                    break;
                                                case 1: // Time
                                                    this._timeBegin.Add(__begin);
                                                    this._timeEnd.Add(__end);
                                                    break;
                                                case 2: // Day
                                                    int __tryInt;
                                                    if (Int32.TryParse(__begin, out __tryInt))
                                                    {
                                                        this._day.Add(__tryInt);
                                                    }
                                                    break;
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        _processPromotionDetail __detail = new _processPromotionDetail();
                        foreach (string __result in Regex.Split(__listStr[__line], __pattern1))
                        {
                            if (__result.Length > 0 && __result[0] != ',')
                            {
                                _processPromotionDetailRang __object = null;
                                string __lastCommand = "";
                                foreach (string __result2 in Regex.Split(__result, __pattern2))
                                {
                                    if (__result2.Length > 0 && __result2[0] != ',')
                                    {
                                        switch (__result2.Trim().ToLower())
                                        {
                                            case "rangqty":
                                            case "rangamount":
                                            case "unit":
                                                __lastCommand = __result2.Trim().ToLower();
                                                break;
                                            case "discount":
                                            case "guid":
                                                {
                                                    __lastCommand = __result2.Trim().ToLower();
                                                    __object = null;
                                                }
                                                break;
                                            case "custgroupcode":
                                                {
                                                    __lastCommand = "";
                                                    _processPromotionDetailRang __newObject = new _processPromotionDetailRang();
                                                    __detail._custGroupCode.Add(__newObject);
                                                    __object = __newObject;
                                                }
                                                break;
                                            case "itemcode":
                                                {
                                                    __lastCommand = "";
                                                    _processPromotionDetailRang __newObject = new _processPromotionDetailRang();
                                                    __newObject._mode = 1;
                                                    __detail._itemCode.Add(__newObject);
                                                    __object = __newObject;
                                                }
                                                break;
                                            case "barcode":
                                                {
                                                    __lastCommand = "";
                                                    _processPromotionDetailRang __newObject = new _processPromotionDetailRang();
                                                    __newObject._mode = 2;
                                                    __detail._barcode.Add(__newObject);
                                                    __object = __newObject;
                                                }
                                                break;
                                            case "additemcode":
                                                {
                                                    __lastCommand = __result2.Trim().ToLower();
                                                    _processPromotionDetailRang __newObject = new _processPromotionDetailRang();
                                                    __newObject._rangMode = 1;
                                                    __detail._addItemCode.Add(__newObject);
                                                    __object = __newObject;
                                                }
                                                break;
                                            case "addbarcode":
                                                {
                                                    __lastCommand = __result2.Trim().ToLower();
                                                    _processPromotionDetailRang __newObject = new _processPromotionDetailRang();
                                                    __newObject._rangMode = 1;
                                                    __detail._addBarcode.Add(__newObject);
                                                    __object = __newObject;
                                                }
                                                break;
                                            case "upitemcode":
                                                {
                                                    __lastCommand = __result2.Trim().ToLower();
                                                    _processPromotionDetailRang __newObject = new _processPromotionDetailRang();
                                                    __newObject._rangMode = 2;
                                                    __detail._addItemCode.Add(__newObject);
                                                    __object = __newObject;
                                                }
                                                break;
                                            case "upbarcode":
                                                {
                                                    __lastCommand = __result2.Trim().ToLower();
                                                    _processPromotionDetailRang __newObject = new _processPromotionDetailRang();
                                                    __newObject._rangMode = 2;
                                                    __detail._addBarcode.Add(__newObject);
                                                    __object = __newObject;
                                                }
                                                break;
                                            case "ignore":
                                                if (__object != null)
                                                {
                                                    __object._ignore = true;
                                                }
                                                break;
                                            default:
                                                if (__object == null)
                                                {
                                                    switch (__lastCommand)
                                                    {
                                                        case "guid": __detail._guid = __result2; break;
                                                        case "discount": __detail._discount = __result2; break;
                                                    }
                                                }
                                                else
                                                {
                                                    switch (__lastCommand)
                                                    {
                                                        case "unit":
                                                            {
                                                                // หน่วย (เงื่อนไข)
                                                                __object._unitCode = __result2;
                                                            }
                                                            break;
                                                        case "rangqty":
                                                            {
                                                                // จำนวน (เงื่อนไข)
                                                                string[] __split = __result2.Split('@');
                                                                if (__split.Length > 0)
                                                                {
                                                                    __object._rangMode = 1;
                                                                    __object._rangQtyBegin = MyLib._myGlobal._decimalPhase(__split[0]);
                                                                    __object._rangQtyEnd = (__split.Length >= 2) ? MyLib._myGlobal._decimalPhase(__split[1]) : 0M;
                                                                }
                                                            }
                                                            break;
                                                        case "rangamount":
                                                            {
                                                                // จำนวนเงิน (เงื่อนไข)
                                                                string[] __split = __result2.Split('@');
                                                                if (__split.Length > 0)
                                                                {
                                                                    __object._rangMode = 2;
                                                                    __object._rangAmountBegin = MyLib._myGlobal._decimalPhase(__split[0]);
                                                                    __object._rangAmountEnd = (__split.Length >= 2) ? MyLib._myGlobal._decimalPhase(__split[1]) : 0M;
                                                                }
                                                            }
                                                            break;
                                                        case "additemcode":
                                                            {
                                                                // แถมตามสินค้า
                                                                string[] __split = __result2.Split('@');
                                                                if (__split.Length > 0)
                                                                {
                                                                    __object._addItemCode = __split[0];
                                                                    __object._addQty = (__split.Length >= 2) ? MyLib._myGlobal._decimalPhase(__split[1]) : 0M;
                                                                    __object._addUnitCode = (__split.Length == 3) ? __split[2] : "";
                                                                }
                                                            }
                                                            break;
                                                        case "addbarcode":
                                                            {
                                                                // แถุมตาม barcode
                                                                string[] __split = __result2.Split('@');
                                                                if (__split.Length > 0)
                                                                {
                                                                    __object._addBarCode = __split[0];
                                                                    __object._addQty = (__split.Length >= 2) ? MyLib._myGlobal._decimalPhase(__split[1]) : 0M;
                                                                    __object._addUnitCode = (__split.Length == 3) ? __split[2] : "";
                                                                }
                                                            }
                                                            break;
                                                        case "upitemcode":
                                                            {
                                                                // แลกซื้อตามสินค้า
                                                                string[] __split = __result2.Split('@');
                                                                if (__split.Length > 0)
                                                                {
                                                                    __object._addItemCode = __split[0];
                                                                    __object._addQty = (__split.Length >= 2) ? MyLib._myGlobal._decimalPhase(__split[1]) : 0M;
                                                                    __object._addUnitCode = (__split.Length == 3) ? __split[2] : "";
                                                                }
                                                            }
                                                            break;
                                                        case "upbarcode":
                                                            {
                                                                // แลกซื้อตาม barcode
                                                                string[] __split = __result2.Split('@');
                                                                if (__split.Length > 0)
                                                                {
                                                                    __object._addBarCode = __split[0];
                                                                    __object._addQty = (__split.Length >= 2) ? MyLib._myGlobal._decimalPhase(__split[1]) : 0M;
                                                                    __object._addUnitCode = (__split.Length == 3) ? __split[2] : "";
                                                                }
                                                            }
                                                            break;
                                                        default:
                                                            {
                                                                string[] __split = __result2.Split('@');
                                                                if (__split.Length > 0)
                                                                {
                                                                    __object._begin = __split[0];
                                                                    __object._end = (__split.Length == 2) ? __split[1] : null;
                                                                }
                                                            }
                                                            break;
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                        this._detail.Add(__detail);
                    }
                }
            }
            //
            if (genHtml)
            {
                StringBuilder __gen = new StringBuilder(@"
    <html><head><style>body {
	font-family: tahoma, verdana, geneva, arial, helvetica, sans-serif;
	font-size: 10pt;
    color='#585858';
	margin: 2pt;
	margin-bottom: 0pt;
	margin-left: 2pt;
	margin-right: 0pt;
	margin-top: 2pt;	
    }</style></head><body bgcolor='#E0F8F7'>");
                // วันที่
                for (int __loop = 0; __loop < this._dateBegin.Count; __loop++)
                {
                    if (__loop != 0)
                    {
                        __gen.Append("และ");
                    }
                    __gen.Append("เฉพาะช่วง");
                    if (this._dateBegin[__loop] != null && this._dateEnd[__loop] == null)
                    {
                        __gen.Append("วันที่" + " : " + "<b>" + MyLib._myGlobal._convertDateToString((DateTime)this._dateBegin[__loop], true) + "</b>");
                    }
                    else
                        if (this._dateBegin[__loop] != null && this._dateEnd[__loop] != null)
                        {
                            __gen.Append("จากวันที่" + " : " + "<b>" + MyLib._myGlobal._convertDateToString((DateTime)this._dateBegin[__loop], true) + "</b>");
                            __gen.Append(" ");
                            __gen.Append("ถึงวันที่" + " : " + "<b>" + MyLib._myGlobal._convertDateToString((DateTime)this._dateEnd[__loop], true) + "</b>");
                        }
                    __gen.Append("<br/>");
                }
                // เวลา
                for (int __loop = 0; __loop < this._timeBegin.Count; __loop++)
                {
                    if (__loop != 0)
                    {
                        __gen.Append("และ");
                    }
                    __gen.Append("เฉพาะช่วง");
                    if (this._timeBegin[__loop] != null && this._timeEnd[__loop] == null)
                    {
                        __gen.Append("เวลา" + " : " + "<b>" + this._timeEnd[__loop] + "</b>");
                    }
                    else
                        if (this._timeBegin[__loop] != null && this._dateEnd[__loop] != null)
                        {
                            __gen.Append("จากเวลา" + " : " + "<b>" + this._timeBegin[__loop] + "</b>");
                            __gen.Append(" ");
                            __gen.Append("ถึงเวลา" + " : " + "<b>" + this._timeEnd[__loop] + "</b>");
                        }
                    __gen.Append("<br/>");
                }
                // วัน
                for (int __loop = 0; __loop < this._day.Count; __loop++)
                {
                    if (__loop == 0)
                    {
                        __gen.Append("เฉพาะวัน" + " : ");
                    }
                    else
                    {
                        __gen.Append(" " + "และ" + " ");
                    }
                    __gen.Append("<b>" + this._dayName[(int)this._day[__loop]] + "</b>");
                }
                __gen.Append("<br/>");
                //
                __gen.Append("<ul>");
                for (int __loop = 0; __loop < this._detail.Count; __loop++)
                {
                    __gen.Append("<li>" + this._detail[__loop]._genHtml() + "</li>");
                }
                __gen.Append("</ul>");
                //
                __gen.Append("</body>");
                __gen.Append("</html>");
                //
                this._resultHtml = __gen.ToString();
            }
        }
    }

    public class _processPromotionDetail
    {
        public string _guid = "";
        public List<_processPromotionDetailRang> _custGroupCode = new List<_processPromotionDetailRang>();
        public List<_processPromotionDetailRang> _itemCode = new List<_processPromotionDetailRang>();
        public List<_processPromotionDetailRang> _barcode = new List<_processPromotionDetailRang>();
        public List<_processPromotionDetailRang> _addItemCode = new List<_processPromotionDetailRang>();
        public List<_processPromotionDetailRang> _addBarcode = new List<_processPromotionDetailRang>();
        public string _discount = "";
        public ArrayList _result = new ArrayList();

        public string _genRang(string word, List<_processPromotionDetailRang> source)
        {
            StringBuilder __gen = new StringBuilder();
            for (int __loop = 0; __loop < source.Count; __loop++)
            {
                if (__gen.Length > 0)
                {
                    __gen.Append(" ");
                }
                if (source[__loop]._ignore)
                {
                    __gen.Append("ยกเว้น");
                }
                if (source[__loop]._addItemCode.Length > 0)
                {
                    string __word = (source[__loop]._rangMode == 1) ? "แถม" : "แลกซื้อ";
                    __gen.Append(__word + "รหัสสินค้า" + " : <b>" + source[__loop]._addItemCode + "</b> " + "จำนวน" + " : <b>" + source[__loop]._addQty + " " + source[__loop]._addUnitCode + "</b>");
                }
                else
                    if (source[__loop]._addBarCode.Length > 0)
                    {
                        string __word = (source[__loop]._rangMode == 1) ? "แถม" : "แลกซื้อ";
                        __gen.Append(__word + "Barcode" + " : <b>" + source[__loop]._addBarCode + "</b> " + "จำนวน" + " : <b>" + source[__loop]._addQty + " " + source[__loop]._addUnitCode + "</b>");
                    }
                    else
                    {
                        if (source[__loop]._end == null)
                        {
                            __gen.Append(word + " <b>" + source[__loop]._begin + "</b>");
                        }
                        else
                        {
                            __gen.Append("จาก" + word + " <b>" + source[__loop]._begin + "</b>" + " ถึง" + word + " <b>" + source[__loop]._end + "</b>");
                        }
                        if (source[__loop]._mode == 1 || source[__loop]._mode == 2)
                        {
                            // กรณีเป็นสินค้า หรือ Barcode
                            if (source[__loop]._rangMode == 1 || source[__loop]._rangMode == 2)
                            {
                                __gen.Append(" " + "จากจำนวน" + " : <b>" + source[__loop]._rangQtyBegin.ToString() + "</b>" + " ถึงจำนวน" + " : <b>" + source[__loop]._rangQtyEnd.ToString() + "</b>");
                            }
                            if (source[__loop]._unitCode.Length > 0)
                            {
                                __gen.Append(" " + "หน่วย" + " : <b>" + source[__loop]._unitCode + "</b>");
                            }
                        }
                    }
            }
            return __gen.ToString();
        }

        public string _genHtml()
        {
            StringBuilder __gen = new StringBuilder();
            __gen.Append(this._genRang("กลุ่มลูกค้า", this._custGroupCode));
            __gen.Append(this._genRang("รหัสสินค้า", this._itemCode));
            __gen.Append(this._genRang("Barcode", this._barcode));
            __gen.Append(this._genRang("รหัสสินค้า", this._addItemCode));
            __gen.Append(this._genRang("Barcode", this._addBarcode));
            if (this._discount.Length > 0)
            {
                __gen.Append(" ");
                __gen.Append("ส่วนลด" + " : " + "<b>" + this._discount + "</b>");
            }
            if (this._guid.Length > 0)
            {
                __gen.Append(" ");
                __gen.Append("GUID" + " : " + "<b>" + this._guid + "</b>");
            }
            return __gen.ToString();
        }
    }

    public class _processPromotionDetailRang
    {
        // เงื่อนไข
        public int _mode = 0; // 0=Other,1=ItemCode,2=Barcode
        public Boolean _ignore = false;
        public object _begin = null;
        public object _end = null;
        public int _rangMode = 0; // 1=Qty,2=Amount
        public decimal _rangQtyBegin = 0M;
        public decimal _rangQtyEnd = 0M;
        public decimal _rangAmountBegin = 0M;
        public decimal _rangAmountEnd = 0M;
        public string _unitCode = "";
        // ของแถม/แลกซื้อ
        public int _addMode = 0; // 1=แถม,2=แลกซื้อ
        public string _addBarCode = "";
        public string _addItemCode = "";
        public string _addUnitCode = "";
        public decimal _addQty = 0M;
    }
}
