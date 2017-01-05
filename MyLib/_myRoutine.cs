using System;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Security.Cryptography;
using System.Diagnostics;

namespace MyLib
{
    public class _addLabelReturn
    {
        public string _label_name;
        public int _length;
        public _myLabel _label;
    }

    public class _myUtil
    {
        public static string _genBarCodeEan13(string sTemp)
        {
            int iSum = 0;
            for (int i = sTemp.Length; i >= 1; i--)
            {
                int iDigit = Convert.ToInt32(sTemp.Substring(i - 1, 1));
                // This appears to be backwards but the 

                // EAN-13 checksum must be calculated

                // this way to be compatible with UPC-A.

                if (i % 2 == 0)
                { // odd  

                    iSum += iDigit * 3;
                }
                else
                { // even

                    iSum += iDigit * 1;
                }
            }
            int iCheckSum = (10 - (iSum % 10)) % 10;
            return sTemp + iCheckSum.ToString();
        }

        public static string _encrypt(string text)
        {
            return text;
            /*RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 128;
            rijndaelCipher.BlockSize = 128;
            byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes("jaturapornchai");
            byte[] keyBytes = new byte[16];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length) len = keyBytes.Length;
            System.Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = keyBytes;
            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
            byte[] plainText = Encoding.UTF8.GetBytes(text);
            byte[] cipherBytes = transform.TransformFinalBlock(plainText, 0, plainText.Length);
            return Convert.ToBase64String(cipherBytes);*/
        }

        public static string _decrypt(string text)
        {
            return text;
            /*RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 128;
            rijndaelCipher.BlockSize = 128;
            byte[] encryptedData = Convert.FromBase64String(text);
            byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes("jaturapornchai");
            byte[] keyBytes = new byte[16];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length) len = keyBytes.Length;
            System.Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = keyBytes;
            ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
            byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return Encoding.UTF8.GetString(plainText);*/
        }

        public static string _moneyFormat(decimal value, string format)
        {
            string __result = "";
            if (value != 0)
            {
                if (value > 0)
                {
                    __result = string.Format(format, value);
                }
                else
                {
                    __result = string.Concat("(", string.Format(format, value * -1), ")");
                }
            }
            return __result;
        }

        public static string _moneyReFormat(string value, string format)
        {
            string __result = "";

            if (value.Length > 0)
            {
                if (value.Substring(0, 1).Equals("(") == true && value.Substring(value.Length - 1, 1).Equals(")") == true)
                {
                    __result = "-" + value.Substring(1, value.Length - 2);
                }
            }

            return __result;
        }

        public static ArrayList _getStrLine(string __data)
        {
            ArrayList __strList = new ArrayList();
            __data = __data.Replace("\\n", "\n");
            string[] __tmpData = __data.Split('\n');

            foreach (string __str in __tmpData)
            {
                __strList.Add(__str);
            }

            return __strList;
        }

        public static ArrayList _cutString(Graphics g, string text, Font font, float width)
        {
            return _cutString(g, text, font, width, false);
        }

        public static ArrayList _cutString(Graphics g, string textStr, Font font, float width, bool splitNewLineAscii)
        {
            ArrayList __result = new ArrayList();

            ArrayList __strList = new ArrayList();
            if (splitNewLineAscii)
                __strList = _getStrLine(textStr);
            else
                __strList.Add(textStr);

            try
            {
                width -= 8;

                foreach (string text in __strList)
                {
                    SizeF __stringSize = g.MeasureString(text, font);
                    if (__stringSize.Width > width)
                    {
                        int __tailFirst = 0;
                        int __tail = 0;
                        int __lastCutPoint = -1;
                        int __lastThaiPoint = -1;
                        char __lastChar = ' ';

                        while (__tail < text.Length)
                        {
                            char __getChar = text[__tail];
                            if (__getChar <= ' ' || (__getChar >= ';' && __getChar <= '@') ||
                                    (__getChar >= 'ก' && __getChar <= 'ฮ' && __tail - __lastThaiPoint > 2 && __lastChar != 'า' && !(__lastChar >= 'เ' && __lastChar <= 'โ')) ||
                                    (__getChar >= 'เ' && __getChar <= 'โ') || (__getChar >= '0' && __getChar <= 'z' && __lastChar >= 'ก' && __lastChar <= 'ฮ'))
                            {
                                __lastCutPoint = __tail;
                                __lastThaiPoint = __lastCutPoint;
                            }
                            __lastChar = text[__tail];
                            __stringSize = g.MeasureString(text.Substring(__tailFirst, __tail - __tailFirst), font);
                            if (__stringSize.Width > width)
                            {
                                if (__lastCutPoint == -1)
                                {
                                    __lastCutPoint = __tail;
                                    __lastThaiPoint = __lastCutPoint;
                                }
                                __result.Add(text.Substring(__tailFirst, (__lastCutPoint - __tailFirst)));
                                __tailFirst = __lastCutPoint;
                                __lastCutPoint = -1;
                                __lastThaiPoint = -1;
                            }
                            __tail++;
                        }// while
                        if (__tailFirst != __lastCutPoint)
                        {
                            __result.Add(text.Substring(__tailFirst, (text.Length - __tailFirst)));
                        }
                    }
                    else
                    {
                        __result.Add(text);
                    }
                }
            }
            catch
            {
                __result.Add(textStr);
            }
            return __result;
        }

        /// <summary>
        /// แสดง Diaglog จากการเลือก Menu แบบ Popup Windows
        /// </summary>
        /// <param name="target">this หรืออื่นๆ</param>
        /// <param name="screenText">Title ของ Windows</param>
        /// <param name="program">โปรแกรม ต้องเป็น Form หรือ _myForm</param>
        public static void _startDialog(IWin32Window target, string screenText, Form program, Boolean fullScreen)
        {
            try
            {
                program.Text = screenText;
                program.ShowInTaskbar = true;
                program.ShowIcon = true;
                program.MinimizeBox = false;
                program.MinimumSize = new Size(program.Width, program.Height);
                program.StartPosition = FormStartPosition.CenterScreen;
                if (fullScreen)
                {
                    program.WindowState = FormWindowState.Maximized;
                }
                program.ShowDialog(target);
            }
            catch
            {
            }
        }

        public static void _startDialog(IWin32Window target, string screenText, Form program)
        {
            _startDialog(target, screenText, program, false);
        }

        public static decimal _dateDiff(string howtocompare, System.DateTime startDate, System.DateTime endDate)
        {
            decimal __diff = 0;
            System.TimeSpan __ts = new System.TimeSpan(endDate.Ticks - startDate.Ticks);

            switch (howtocompare.ToLower())
            {
                case "year":
                    __diff = Convert.ToDecimal(__ts.TotalDays / 365);
                    break;
                case "month":
                    __diff = Convert.ToDecimal((__ts.TotalDays / 365) * 12);
                    break;
                case "day":
                    __diff = Convert.ToDecimal(__ts.TotalDays);
                    break;
                case "hour":
                    __diff = Convert.ToDecimal(__ts.TotalHours);
                    break;
                case "minute":
                    __diff = Convert.ToDecimal(__ts.TotalMinutes);
                    break;
                case "second":
                    __diff = Convert.ToDecimal(__ts.TotalSeconds);
                    break;
                case "millisecond":
                    __diff = Convert.ToDecimal(__ts.TotalMilliseconds);
                    break;
            }
            return __diff;
        }

        /// <summary>
        /// เปลี่ยน Text เป้น Xml Text ไม่เช่นนั้นอาจจะ Error ในกรณีมีตัวประหลาด
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static String _convertTextToXml(String source)
        {
            return source.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;");
        }

        public static String _reConvertTextToXml(String source)
        {
            return source.Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&quot;", "\"");
        }

        /// <summary>
        /// เปลี่ยน Text เป้น Xml Text ไม่เช่นนั้นอาจจะ Error ในกรณีมีตัวประหลาด และเพิ่มปิดหัวท้าย
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static String _convertTextToXmlForQuery(String source)
        {
            return (string.Concat("<query>", _convertTextToXml(source), "</query>"));
        }

        /// <summary>
        /// ตรวจสอบ Folders มีอยู่หรือเปล่า
        /// </summary>
        /// <param name="sDirName">Path</param>
        /// <returns>true มีอยู่จริง</returns>
        public static bool _dirExists(string sDirName)
        {
            try
            {
                return (System.IO.Directory.Exists(sDirName));    //Check for file
            }
            catch (Exception)
            {
                return (false);                                 //Exception occured, return False
            }
        }

        /// <summary>
        /// ตรวจสอบ ไฟล์ ว่ามีอยู่หรือเปล่า
        /// </summary>
        /// <param name="sPathName">Path</param>
        /// <returns>true มีอยู่จริง</returns>
        public static bool _fileExists(string sPathName)
        {
            try
            {
                return (System.IO.File.Exists(sPathName));  //Exception for folder
            }
            catch (Exception)
            {
                return (false);                                   //Error occured, return False
            }
        }

        /// <summary>
        /// Gen MultiSelect Condition
        /// </summary>
        /// <param name="fieldName">ชื่อตัวแปร</param>
        /// <param name="value">เงื่อนไข</param>
        /// <returns></returns>
        public static string _genCodeList(string fieldName, string value)
        {
            try
            {
                string[] __split = value.Split(',');
                StringBuilder __between = new StringBuilder();
                StringBuilder __tempText = new StringBuilder();
                Boolean __haveIn = false;
                string __orText = "";
                for (int __loop = 0; __loop < __split.Length; __loop++)
                {
                    string __str = __split[__loop];
                    if (__str.IndexOf(':') != -1)
                    {
                        if (__between.Length > 0)
                        {
                            __between.Append(" or ");
                        }
                        string[] __split2 = __str.Split(':');
                        __between.Append(fieldName + " between \'" + __split2[0].ToString() + "\' and \'" + __split2[1].ToString() + "\'");
                    }
                    else
                    {
                        if (__str.Length > 0)
                        {
                            if (__tempText.Length > 0)
                            {
                                __tempText.Append(",");
                            }
                            __tempText.Append("\'" + __str + "\'");
                            __haveIn = true;
                        }
                    }
                }
                if (__haveIn)
                {
                    if (__between.Length > 0)
                    {
                        __orText = " or ";
                    }
                    __tempText = new StringBuilder(__orText + fieldName + " in (" + __tempText.ToString() + ") ");
                }
                return __between.ToString() + __tempText.ToString();
            }
            catch
            {
                return "";
            }
        }
    }

    public partial class _myVScrollBar : VScrollBar
    {
        protected override void OnKeyDown(KeyEventArgs e)
        {
            e.Handled = true;
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            e.Handled = true;
        }
    }

    public partial class _myHScrollBar : HScrollBar
    {
        protected override void OnKeyDown(KeyEventArgs e)
        {
            e.Handled = true;
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            e.Handled = true;
        }
    }

    public partial class _myToolStrip : ToolStrip
    {
    }

    public partial class _myToolStripMenuItem : ToolStripMenuItem
    {
        public _myToolStripMenuItem _parent;
        public ToolStripItem _next;
        public string _menuName;
    }

    public partial class _myTextBoxUpDown : _myTextBox
    {
        public _myTextBoxUpDown()
        {
            this._iconNumber = 2;
            this.Invalidate();
        }
    }

    public partial class _myNumberBox : _myTextBox
    {
        /// <summary>ค่าที่น้อยที่สุด</summary>
        private decimal __minValueResult;
        /// <summary>ค่าที่มากที่สุด</summary>
        private decimal __maxValueResult;
        /// <summary>ทศนิยม</summary>
        private decimal __pointResult;
        /// <summary>เก็บค่าไว้</summary>
        private decimal __doubleResult = 0;
        /// <summary>รูปแบบ</summary>
        private string __formatResult = "";
        /// <summary>สีปรกติ</summary>
        private Color __default_colorResult = Color.Black;
        public decimal __oldResult = 0;
        public event AfterSelectCalculatorHandler _afterSelectCalculator;

        private Boolean __hiddenNumberValueResult = false;

        public event _onCustomSearchClick _customSearchClick;
        public delegate void _onCustomSearchClick(object sender, EventArgs e);
        public _myNumberBox(int iconType)
        {
            this._init();
            this._iconType = iconType;
        }

        public _myNumberBox()
        {
            this._init();
        }

        void _init()
        {
            this._iconSearch.Image = Properties.Resources.IconCalc;
            this._icon = true;
            this._iconSearch.Click += new EventHandler(_iconSearch_Click);
            this.textBox.TextAlign = HorizontalAlignment.Right;
            this.textBox.Enter += new EventHandler(textBox_Enter);
            this.textBox.Leave += new EventHandler(textBox_Leave);
            this.textBox.KeyUp += new KeyEventHandler(textBox_KeyUp);
            this.BackColor = Color.Transparent;
        }

        public Boolean _hiddenNumberValue
        {
            get
            {
                return __hiddenNumberValueResult;
            }

            set
            {
                __hiddenNumberValueResult = value;

                if (__hiddenNumberValueResult == true)
                {
                    this.textBox.PasswordChar = '*';
                }
                else
                {
                    this.textBox.PasswordChar = '\0';
                }
            }
        }

        void _iconSearch_Click(object sender, EventArgs e)
        {
            if (_customSearchClick != null)
            {
                _customSearchClick(sender, e);
            }
            else
            {
                try
                {
                    this.Focus();
                    this.textBox.Focus();
                    this.textBox.SelectAll();
                    _CalculatorPanel __calcultor = new _CalculatorPanel();
                    __calcultor.Text = "Calculator";
                    __calcultor._Format += new EventHandler<_CalculatorFormatEventArgs>(__calcultor__Format);
                    __calcultor._ResultControl = this;
                    int newLocationX = this._iconSearch.Width - __calcultor.Width;
                    int newLocationY = this._iconSearch.Height;
                    Point x = this._iconSearch.PointToScreen(new Point(newLocationX, newLocationY));
                    __calcultor.DesktopLocation = x;
                    __calcultor.StartPosition = FormStartPosition.Manual;
                    //__calcultor.ShowDialog(this.Parent); // toe
                    __calcultor.ShowDialog();
                }
                catch (Exception ex)
                {
                    string __ex = ex.ToString();
                }
            }
        }

        void __calcultor__Format(object sender, _CalculatorFormatEventArgs e)
        {
            this.textBox.Text = e._FormattedResult;
            _checkNumber();
            _refresh();
            if (_afterSelectCalculator != null)
            {
                _afterSelectCalculator((decimal)e._Result);
            }
        }
        void textBox_Enter(object sender, EventArgs e)
        {
            this.textBox.Text = (__doubleResult == 0) ? "" : __doubleResult.ToString();
        }

        public decimal _minValue { get { return __minValueResult; } set { __minValueResult = value; } }
        public decimal _maxValue { get { return __maxValueResult; } set { __maxValueResult = value; } }
        public decimal _point { get { return __pointResult; } set { __pointResult = value; } }
        public decimal _double { get { return __doubleResult; } set { __doubleResult = value; } }
        public string _format { get { return __formatResult; } set { __formatResult = value; } }
        public Color _default_color { get { return __default_colorResult; } set { __default_colorResult = value; } }

        public decimal _setDataNumber
        {
            set
            {
                this._double = value;
                this.__oldResult = value;
                this._textFirst = value.ToString();
                this._refresh();

            }
        }
        void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            _checkNumber();
            try
            {
                this.textBox.ForeColor = (this.textBox.Text.Length == 0) ? _default_color : (Convert.ToDecimal(this.textBox.Text) < 0) ? Color.Red : _default_color;
            }
            catch
            {
                this.textBox.ForeColor = _default_color;
            }
        }

        public void _refresh()
        {
            string __format = this._format;
            decimal __point = this._point;
            if (__format.Length > 0 && __format[0] == 'm')
            {
                __point = MyLib._myGlobal._decimalPhase(__format.Remove(0, 1));
                __format = "";
            }
            if (__format.Length == 0)
            {
                __format = "{0:n" + __point.ToString() + "}";
            }
            this.textBox.Text = (_double == 0) ? "" : string.Format(__format, _double);
            this.textBox.ForeColor = (_double < 0) ? Color.Red : _default_color;
            if (this.textBox.Text.Length == 0)
            {
                _double = 0;
            }
            else
            {
                try
                {
                    _double = Convert.ToDecimal(this.textBox.Text);
                }
                catch
                {
                    _double = 0;
                    // Debugger.Break();
                }
            }
            // this.Invalidate();
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            try
            {
                _checkNumber();
                _refresh();
            }
            catch
            {
            }
        }

        public event beforeNumberBoxCheckValue _beforeCheckNumberValue;

        public bool _checkNumber()
        {
            if (_beforeCheckNumberValue != null)
            {
                this._beforeCheckNumberValue(this, this.textBox.Text);
            }

            try
            {
                string __text = this.textBox.Text;
                /*if (__text.IndexOf('=') == 0)
                {
                    __text = __text.Substring(1);
                }*/

                _mathParser __formula = new _mathParser();
                _double = (decimal)__formula.Calculate(__text);
            }
            catch
            {
                _double = 0;
            }
            this.Invalidate();
            return (true);
        }
    }

    public delegate void beforeNumberBoxCheckValue(object sender, string valueStr);

    public partial class _myDateBox : _myTextBox
    {
        /// <summary>
        /// ตัวที่ใช้เก็บข้อมูล
        /// </summary>
        private DateTime __dateTimeResult = new DateTime(1000, 1, 1);
        private DateTime __dateTimeOldResult = new DateTime(1000, 1, 1);
        private Boolean __warningResult = true;
        private Boolean __lostFocustResult = true;

        public event AfterSelectCalendarHandler _afterSelectCalendar;

        public _myDateBox()
        {
            this._iconSearch.Image = Properties.Resources.IconCalendar;
            this._icon = true;
            this._iconSearch.Click += new EventHandler(IconSearch_Click);
            this.textBox.Enter += new EventHandler(textBox_Enter);
            this.textBox.Leave += new EventHandler(textBox_Leave);
            this.BackColor = Color.Transparent;
        }

        public DateTime _dateTime { get { return __dateTimeResult; } set { __dateTimeResult = value; } }
        public DateTime _dateTimeOld { get { return __dateTimeOldResult; } set { __dateTimeOldResult = value; } }
        public Boolean _warning { get { return __warningResult; } set { __warningResult = value; } }
        public Boolean _lostFocust { get { return __lostFocustResult; } set { __lostFocustResult = value; } }

        void textBox_Leave(object sender, EventArgs e)
        {
            try
            {
                _myTextBox __result = (_myTextBox)((TextBox)sender).Parent;
                if (__result._displayLabel && __result._label != null)
                {
                    __result._label.Font = new Font(__result._label.Font, FontStyle.Regular);
                }
                if (_lostFocust)
                {
                    _checkDate(true, _warning);
                }
            }
            catch
            {
            }
        }

        public string _textQuery()
        {
            return _textQuery("");
        }

        public string _textQuery(string extraWord)
        {
            string __result = "null";
            try
            {
                IFormatProvider __culture = new CultureInfo("en-US");
                __result = _dateTime.ToString("yyyy-MM-dd", __culture);
                __result = (_dateTime.Year < 1800) ? "null" : string.Concat("\'", __result, extraWord, "\'");
            }
            catch
            {
                // Debugger.Break();
            }
            return (__result);
        }

        /// <summary>
        /// ก่อนจะบันทึกให้เปลี่ยนเป็นตัวเลขให้หมด
        /// </summary>
        public void _beforeInput()
        {
            IFormatProvider __culture = MyLib._myGlobal._cultureInfo();
            if (_dateTime.Year > 1000)
            {
                this.textBox.Text = _dateTime.ToString("d/M/yyyy", __culture);
            }
            else
            {
                this.textBox.Text = "";
            }
        }

        void textBox_Enter(object sender, EventArgs e)
        {
            _myTextBox __result = (_myTextBox)((TextBox)sender).Parent;
            if (__result._displayLabel && __result._label != null)
            {
                __result._label.Font = new Font(__result._label.Font, FontStyle.Underline);
            }
            _beforeInput();
        }

        public void _refresh()
        {
            this.textBox.Text = MyLib._myGlobal._convertDateToString(_dateTime, true);
            this.textBox.Invalidate();
        }

        /// <summary>
        /// ตรวจสอบว่าป้อนวันที่ถูกหรือไม่
        /// </summary>
        public bool _checkDate(Boolean changeText, Boolean warning)
        {
            if (this.textBox.Text.Length > 0)
            {
                try
                {
                    int __day = _myGlobal._workingDate.Day;
                    int __month = _myGlobal._workingDate.Month;
                    int __year = _myGlobal._workingDate.Year + _myGlobal._year_add;
                    /*Debug.Print(this.textBox.Text);
                    Debug.Print(Environment.StackTrace.ToString());*/
                    string __dateBuffer = this.textBox.Text;
                    __dateBuffer = __dateBuffer.Replace(' ', '/');
                    __dateBuffer = __dateBuffer.Replace('-', '/');
                    __dateBuffer = __dateBuffer.Replace('.', '/');
                    __dateBuffer = __dateBuffer.Replace('*', '/');
                    if (__dateBuffer.Length > 0)
                    {
                        if (__dateBuffer[__dateBuffer.Length - 1] == '/')
                        {
                            __dateBuffer = __dateBuffer.Remove(__dateBuffer.Length - 1, 1);
                        }
                    }
                    string[] __dateSplit = __dateBuffer.Split('/');
                    if (__dateBuffer.Length == 4 && _myGlobal._decimalPhase(__dateBuffer) != 0)
                    {
                        // ddmm
                        __day = Convert.ToInt32(__dateBuffer.Substring(0, 2));
                        __month = Convert.ToInt32(__dateBuffer.Substring(2, 2));
                    }
                    else
                        if (__dateBuffer.Length == 6 && _myGlobal._decimalPhase(__dateBuffer) != 0)
                    {
                        // ddmmyy
                        __day = Convert.ToInt32(__dateBuffer.Substring(0, 2));
                        __month = Convert.ToInt32(__dateBuffer.Substring(2, 2));
                        __year = Convert.ToInt32(__dateBuffer.Substring(4, 2));
                    }
                    else
                            if (__dateSplit.Length == 1)
                    {
                        // ป้อนแต่วันที่
                        __day = Convert.ToInt32(__dateSplit[0]);
                    }
                    else
                                if (__dateSplit.Length == 2)
                    {
                        // ป้อน วันที่+เดือน
                        __day = Convert.ToInt32(__dateSplit[0]);
                        __month = Convert.ToInt32(__dateSplit[1]);
                    }
                    else if (__dateSplit.Length >= 3)
                    {
                        // ป้อน วันที่+เดือน+ปี
                        __day = Convert.ToInt32(__dateSplit[0]);
                        string __monthNumber = __dateSplit[1];
                        /*string[] __monthNameArray = { "มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม" };
                        for (int __loop = 0; __loop < 12; __loop++)
                        {
                            if (__monthNumber.Equals(__monthNameArray[__loop].ToString()))
                            {
                                __monthNumber = ((int)__loop + 1).ToString();
                                break; ;
                            }
                        }*/
                        __month = Convert.ToInt32(__monthNumber);
                        __year = Convert.ToInt32(__dateSplit[2]);
                    }
                    // ดึงวันเดือนปีปรกติ ถ้าในกรณีป้อนไม่ครบ ก็ประกอบร่างใหม่ เช่น ป้อนเฉพาะวันที่
                    if (MyLib._myGlobal._year_type == 1)
                    {
                        if (__year < 2500)
                        {
                            if (__year < 100) // toe fix 24xx for birth day
                            {
                                __year = __year + 2500;
                            }
                        }
                    }
                    else
                    {
                        if (__year < 2000)
                        {
                            if (__year < 100)
                            {
                                __year = __year + 2000;
                            }
                        }
                    }
                    __year = __year - _myGlobal._year_add;
                    DateTime __myDate = new DateTime(__year, __month, __day);
                    if (changeText)
                    {
                        if (__myDate.Year <= 1000)
                        {
                            this.textBox.Text = "";
                        }
                    }
                    _dateTime = new DateTime(__myDate.Year, __myDate.Month, __myDate.Day);
                    if (changeText)
                    {
                        this._refresh();
                    }
                }
                catch
                {
                    // Debugger.Break();
                    if (warning)
                    {
                        // ป้อนวันที่ผิดพลาด
                        MessageBox.Show(MyLib._myGlobal._resource("format_date_error"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (changeText)
                    {
                        this.textBox.Text = "";
                    }
                    _dateTime = new DateTime(1000, 1, 1);
                    return (false);
                }
            }
            else
            {
                _dateTime = new DateTime(1000, 1, 1);
            }
            return (true);
        }

        void IconSearch_Click(object sender, EventArgs e)
        {
            this.Focus();
            this.textBox.Focus();
            this.textBox.SelectAll();
            _selectDate __myDate = new _selectDate();
            //			myDate.Location = new Point((this.IconSearch.Location.X + this.IconSearch.Width)-myDate.Width, this.IconSearch.Location.Y + this.IconSearch.Height);
            int newLocationX = this._iconSearch.Width - __myDate.Width;
            int newLocationY = this._iconSearch.Height;
            Point __x = this._iconSearch.PointToScreen(new Point(newLocationX, newLocationY));
            __myDate.DesktopLocation = __x;
            __myDate.StartPosition = FormStartPosition.Manual;
            __myDate._selectedDate += new _selectDate.SelectDateEventHandler(myDate__selectedDate);
            __myDate.ShowDialog(this.Parent);
        }

        void myDate__selectedDate(DateTime e)
        {
            this.textBox.Text = string.Concat(e.Day, "/", e.Month, "/", (e.Year + MyLib._myGlobal._year_add));
            _checkDate(true, _warning);
            _beforeInput();
            if (_afterSelectCalendar != null)
            {
                _afterSelectCalendar(e);
            }
        }
    }

    public partial class _myForm : System.Windows.Forms.Form
    {
        private Color __colorBeginResult = Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(161)))), ((int)(((byte)(230)))));
        private Color __colorEndResult = Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(117)))), ((int)(((byte)(214)))));
        private Boolean __colorBackgroundResult = true;
        private string _resultFixed = "";
        private _languageEnum _lastLanguage = _languageEnum.Null;

        public _myForm()
        {
            this.DoubleBuffered = true;
            /*this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UseTextForAccessibility, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.CacheText, true);*/
        }

        [Category("_SML")]
        [Description("Name ใช้ในการหา Resource")]
        [DisplayName("Name ใช้ในการหา Resource")]
        public string ResourceName
        {
            get
            {
                return _resultFixed;
            }
            set
            {
                _resultFixed = value;
                this.Invalidate();
            }
        }

        [Category("_SML")]
        [Description("แสดงสีพื้นแบบพิเศษ")]
        public Boolean _colorBackground
        {
            get
            {
                return __colorBackgroundResult;
            }
            set
            {
                __colorBackgroundResult = value;
                this.Invalidate();
            }
        }

        [Category("_SML")]
        [Description("สีพื้น (เริ่มจาก)")]
        public Color _colorBegin
        {
            get
            {
                return __colorBeginResult;
            }
            set
            {
                __colorBeginResult = value;
                this.Invalidate();
            }
        }

        [Category("_SML")]
        [Description("สีพื้น (ถึง)")]
        public Color _colorEnd
        {
            get
            {
                return __colorEndResult;
            }
            set
            {
                __colorEndResult = value;
                this.Invalidate();
            }
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            if (_myGlobal._language != this._lastLanguage)
            {
                _lastLanguage = _myGlobal._language;
                if (this._resultFixed != null && this._resultFixed.Length > 0)
                {
                    this.Text = _myGlobal._resource(this._resultFixed);
                }
            }
            if (this._colorBackground)
            {
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                // declare linear gradient brush for fill background of label
                LinearGradientBrush __GBrush = new LinearGradientBrush(new Point(0, 0), new Point(0, this.Height), _colorBegin, _colorEnd);
                Rectangle __rect = new Rectangle(0, 0, this.Width, this.Height);
                // Fill with gradient 
                e.Graphics.FillRectangle(__GBrush, __rect);
                __GBrush.Dispose();
            }
        }
    }

    public partial class _myTreeView : System.Windows.Forms.TreeView
    {
        public string __name;

        public _myTreeView()
        {
        }
    }

    public partial class _myUserControl : System.Windows.Forms.UserControl
    {
        public string _menuName = "";
    }

    public partial class _myTabPage : System.Windows.Forms.TabPage
    {
        public string __screenGuid = "";
        public string __menuCode = "";
    }

    public partial class _myTabControl : System.Windows.Forms.TabControl
    {
        private bool __showTabNumber = false;
        private bool __fixedName = false;
        private string __tableNameResult = "";

        public _myTabControl()
        {
            this.Multiline = true;
            this.Invalidated += new InvalidateEventHandler(_myTabControl_Invalidated);
            this.Font = new Font(_myGlobal._myFont.FontFamily, _myGlobal._myFont.Size);
        }

        [Category("_SML")]
        [Description("Table name เพื่อดึง Resource")]
        [DefaultValue(false)]
        public string TableName
        {
            get
            {
                return __tableNameResult;
            }
            set
            {
                __tableNameResult = value;
            }
        }

        void _myTabControl_Invalidated(object sender, InvalidateEventArgs e)
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                _getResource();
            }
        }

        /// <summary>
        /// ดึง Resource
        /// </summary>
        public void _getResource()
        {
            if (this.__fixedName == false)
            {
                int __number = 1;
                foreach (TabPage __getTab in this.TabPages)
                {
                    try
                    {
                        string __numberStr = __number.ToString();
                        if (__number > 9)
                        {
                            __numberStr = char.ConvertFromUtf32(65 + (__number - 10));
                        }
                        string __newTabName = (__getTab.Tag != null && __getTab.Tag.ToString().Length != 0) ? __getTab.Tag.ToString() : __getTab.Name;
                        string __getTabID = (TableName == null || TableName.Length == 0) ? __newTabName : string.Concat(TableName, ".", __newTabName);
                        if (MyLib._myGlobal._isDesignMode == false)
                        {
                            string __getResource = MyLib._myResource._findResource(__getTabID, __getTabID)._str;
                            __getTab.Text = ((ShowTabNumber) ? (__numberStr + ".") : "") + ((__getResource.Length == 0) ? __getTab.Name : __getResource);
                        }
                        else
                        {
                            __getTab.Text = ((ShowTabNumber) ? (__numberStr + ".") : "") + __getTabID;
                        }
                        __number++;
                    }
                    catch
                    {
                    }
                }
            }
        }

        [Category("_SML")]
        [Description("แสดงหมายเลข Runnung Tab")]
        [DefaultValue(false)]
        public bool ShowTabNumber
        {
            get
            {
                return __showTabNumber;
            }
            set
            {
                __showTabNumber = value;
                _getResource();
            }
        }

        [Category("_SML")]
        [Description("กำหนดให้ใช้ชื่อที่กำหนด (ไม่ไปดึง Resource)")]
        [DefaultValue(false)]
        public bool FixedName
        {
            get
            {
                return __fixedName;
            }
            set
            {
                __fixedName = value;
                _getResource();
            }
        }
    }

    public partial class _myRadioButton : System.Windows.Forms.RadioButton
    {
        public string __name = "";
        public string __resource_name = "";
        public int __row;
        public int __column;
        public object __value;
        private string _resultFixed = "";
        public _languageEnum _lastLanguage = _languageEnum.Null;
        //
        public _myRadioButton()
        {
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            if (_myGlobal._language != this._lastLanguage)
            {
                _lastLanguage = _myGlobal._language;
                if (this._resultFixed.Length > 0)
                {
                    this.Text = _myGlobal._resource(this._resultFixed);
                }
            }
            base.OnPaint(pevent);
        }


        [Category("_SML")]
        [Description("Name ใช้ในการหา Resource")]
        [DisplayName("Name ใช้ในการหา Resource")]
        public string ResourceName
        {
            get
            {
                return _resultFixed;
            }
            set
            {
                _resultFixed = value;
                this.Invalidate();
            }
        }
    }

    public partial class _myGroupBox : System.Windows.Forms.GroupBox
    {
        public string __tableName = "";
        public string __name = "";
        public string __resource_name = "";
        public bool __query = false;
        /// <summary>
        /// จำนวน Column ใน GroupBox
        /// </summary>
        public int __maxColumn = 0;
        /// <summary>
        /// จำนวน Column ใน Screen
        /// </summary>
        public int __maxColumnPanel = 0;
        public int __row;
        public int __column;
        public int __rowCount;
        public _languageEnum _lastLanguage = _languageEnum.Null;
        private string _resultFixed = "";
        //
        public _myGroupBox()
        {
            this.DoubleBuffered = true;
            /*this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UseTextForAccessibility, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.CacheText, true);*/
            //
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_myGlobal._language != this._lastLanguage)
            {
                _lastLanguage = _myGlobal._language;
                if (this._resultFixed.Length > 0)
                {
                    this.Text = _myGlobal._resource(this._resultFixed);
                    this.Invalidate();
                }
            }
            base.OnPaint(e);
        }

        [Category("_SML")]
        [Description("ใช้ Query (กรณี Radio Butoon)")]
        [DefaultValue(false)]
        public bool Query
        {
            get
            {
                return __query;
            }
            set
            {
                __query = value;
            }
        }

        [Category("_SML")]
        [Description("Name ใช้ในการหา Resource")]
        [DisplayName("Name ใช้ในการหา Resource")]
        public string ResourceName
        {
            get
            {
                return _resultFixed;
            }
            set
            {
                _resultFixed = value;
            }
        }
    }

    public partial class _myButton : VistaButton
    {
        public string _name = "";
        public string _resource_name = "";
        public int _row = 0;
        public int _column = 0;
        public int _maxColumn = 0;
        //
        private System.Windows.Forms.TextImageRelation _textImageRelationResult = TextImageRelation.ImageBeforeText;
        private Boolean _useVisualStyleBackColorResult = false;
        public _languageEnum _lastLanguage = _languageEnum.Null;
        private string _resultFixed = "";

        public _myButton()
        {
            this.AutoSize = true;
            this.Padding = new Padding(0, 0, 0, 0);
            this.Margin = new Padding(1, 0, 1, 0);
            this.myTextImageRelation = TextImageRelation.ImageBeforeText;
            this.Invalidated += new InvalidateEventHandler(_myButton_Invalidated);
        }

        void _myButton_Invalidated(object sender, InvalidateEventArgs e)
        {
            if (_myGlobal._language != this._lastLanguage)
            {
                _lastLanguage = _myGlobal._language;
                if (this._resultFixed.Length > 0)
                {
                    this.ButtonText = _myGlobal._resource(this._resultFixed);
                    this.Invalidate();
                }
            }
        }

        public Boolean myUseVisualStyleBackColor
        {
            set
            {
                this._useVisualStyleBackColorResult = value;
            }
            get
            {
                return this._useVisualStyleBackColorResult;
            }
        }

        public System.Windows.Forms.TextImageRelation myTextImageRelation
        {
            set
            {
                this._textImageRelationResult = value;
            }
            get
            {
                return this._textImageRelationResult;
            }
        }

        [Category("_SML")]
        [Description("Name ใช้ในการหา Resource")]
        [DisplayName("Name ใช้ในการหา Resource")]
        public string ResourceName
        {
            get
            {
                return _resultFixed;
            }
            set
            {
                _resultFixed = value;
                this.Invalidate();
            }
        }
    }

    public partial class _myComboBox : System.Windows.Forms.ComboBox
    {
        public Boolean _isQuery { get { return _isQueryResult; } set { _isQueryResult = value; } }
        private Boolean _isQueryResult = true;

        public int _row;
        public int _column;
        public string _name;
        public string[] _listName;
        public Boolean _addCounter;
        public string _tableName;
        public _myLabel _label;
        public int _oldValue = 0;
        /// <summary>
        /// ให้จำข้อมูลล่าสุด
        /// </summary>
        public Boolean _useRecentValue = false;

        /// <summary>จำนวน Column เอาไว้ให้ routine screen ใช้</summary>
        public int _maxColumn { get { return _maxColumnResult; } set { _maxColumnResult = value; } }
        private int _maxColumnResult = 1;

        public _myComboBox()
        {
            this.Font = _myGlobal._myFont;
            this.BackColor = Color.White;
            this.DropDown += new EventHandler(_myComboBox_DropDown);
            this.Enter += new EventHandler(_myComboBox_Enter);
            this.Leave += new EventHandler(_myComboBox_Leave);
            this.HandleCreated += new EventHandler(_myComboBox_HandleCreated);
        }

        void _myComboBox_Leave(object sender, EventArgs e)
        {
            this.Parent.Invalidate();
        }

        void _myComboBox_Enter(object sender, EventArgs e)
        {
            this.Parent.Invalidate();
        }

        void _myComboBox_HandleCreated(object sender, EventArgs e)
        {
            this.Parent.Paint += new PaintEventHandler(Parent_Paint);
        }

        void Parent_Paint(object sender, PaintEventArgs e)
        {
            /*if (this.Focused)
            {
                Graphics __g = e.Graphics;
                Point __getClient = this.Location;
                Pen __myPen = new Pen(Color.Orange, 1);
                Point[] __point = {
                    new Point(__getClient.X-1,__getClient.Y-1),
                    new Point(__getClient.X-1,__getClient.Y+(this.Height)),
                    new Point(__getClient.X+(this.Width),__getClient.Y+(this.Height)),
                    new Point(__getClient.X+(this.Width),__getClient.Y-1),
                    new Point(__getClient.X-1,__getClient.Y-1)
                };
                __g.DrawLines(__myPen, __point);
                __myPen.Dispose();
            }*/
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;

            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                if (keyData == Keys.Home || keyData == Keys.End || keyData == Keys.PageDown || keyData == Keys.PageUp)
                {
                    return true;
                }
                else
                    if (keyData == Keys.F2 || keyData == Keys.Space)
                {
                    this.DroppedDown = (this.DroppedDown == true) ? false : true;
                    return true;
                }
                else
                        if (keyData == Keys.Up && this.DroppedDown == false)
                {
                    _cellMoveUpWork();
                    return true;
                }
                else
                            if (keyData == Keys.Down && this.DroppedDown == false)
                {
                    _cellMoveDownWork();
                    return true;
                }
                else
                                if (keyData == Keys.Left)
                {
                    _cellMoveLeftWork();
                    return true;
                }
                else
                                    if (keyData == Keys.Enter || keyData == Keys.Right)
                {
                    _cellMoveRightWork();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _myComboBox_DropDown(object sender, EventArgs e)
        {
            this.DropDownWidth = this.Width;
            for (int __loop = 0; __loop < this.Items.Count; __loop++)
            {
                string __getName = this.Items[__loop].ToString();
                Graphics myGraphics = this.CreateGraphics();
                myGraphics.SmoothingMode = SmoothingMode.HighQuality;
                SizeF stringSize = myGraphics.MeasureString(__getName, _myGlobal._myFont);
                if (stringSize.Width > this.DropDownWidth - 5)
                {
                    this.DropDownWidth = (int)stringSize.Width + 5;
                }
            }
        }

        /// <summary>
        /// จะบวกหนึ่งให้เอง
        /// </summary>
        /// <returns></returns>
        public int _selectedIndex
        {
            get
            {
                return this.SelectedIndex + 1;
            }
        }

        public event CellComboBoxMoveRightHandler _cellMoveRight;
        public event CellComboBoxMoveLeftHandler _cellMoveLeft;
        public event CellComboBoxMoveDownHandler _cellMoveDown;
        public event CellComboBoxMoveUpHandler _cellMoveUp;

        protected virtual void _cellMoveDownWork()
        {
            if (_cellMoveDown != null) _cellMoveDown(this);
        }

        protected virtual void _cellMoveUpWork()
        {
            if (_cellMoveUp != null) _cellMoveUp(this);
        }

        protected virtual void _cellMoveRightWork()
        {
            if (_cellMoveRight != null) _cellMoveRight(this);
        }

        protected virtual void _cellMoveLeftWork()
        {
            if (_cellMoveLeft != null) _cellMoveLeft(this);
        }
    }

    public delegate void CellComboBoxMoveRightHandler(object sender);
    public delegate void CellComboBoxMoveLeftHandler(object sender);
    public delegate void CellComboBoxMoveUpHandler(object sender);
    public delegate void CellComboBoxMoveDownHandler(object sender);

    public partial class _myCheckBox : System.Windows.Forms.CheckBox
    {
        public int _row;
        public int _column;
        public string _name = "";
        public string _resource_name;
        public _myLabel _label;
        public _languageEnum _lastLanguage = _languageEnum.Null;
        private string _resultFixed = "";
        /// <summary>เป็นของ Database (เอาไปใช้ตอน Query)</summary>
        public Boolean _isQuery { get { return _isQueryResult; } set { _isQueryResult = value; } }
        private Boolean _isQueryResult = true;
        public Boolean _defaultValue = false;

        public _myCheckBox()
        {
            this.AutoSize = true;
            this.KeyDown += new KeyEventHandler(_myCheckBox_KeyDown);
            this.Leave += new EventHandler(_myCheckBox_Leave);
            this.Enter += new EventHandler(_myCheckBox_Enter);
            this.HandleCreated += new EventHandler(_myCheckBox_HandleCreated);
        }

        void _myCheckBox_HandleCreated(object sender, EventArgs e)
        {
            this.Parent.Paint += new PaintEventHandler(Parent_Paint);
        }

        void _myCheckBox_Enter(object sender, EventArgs e)
        {
            this.Parent.Invalidate();
        }

        void _myCheckBox_Leave(object sender, EventArgs e)
        {
            this.Parent.Invalidate();
        }

        void Parent_Paint(object sender, PaintEventArgs e)
        {
            /*if (this.Focused)
            {
                Graphics __g = e.Graphics;
                Point __getClient = this.Location;
                Pen __myPen = new Pen(Color.Orange, 1);
                if (this.Text.Length == 0)
                {
                    Point[] __point = {
                    new Point(__getClient.X-1,__getClient.Y-1),
                    new Point(__getClient.X-1,__getClient.Y+(this.Height-1)),
                    new Point(__getClient.X+(this.Width-2),__getClient.Y+(this.Height-1)),
                    new Point(__getClient.X+(this.Width-2),__getClient.Y-1),
                    new Point(__getClient.X-1,__getClient.Y-1)
                };

                    __g.DrawLines(__myPen, __point);
                }
                else
                {
                    Point[] __point = {
                    new Point(__getClient.X-1,__getClient.Y+1),
                    new Point(__getClient.X-1,__getClient.Y+(15)),
                    new Point(__getClient.X+(13),__getClient.Y+(15)),
                    new Point(__getClient.X+(13),__getClient.Y+1),
                    new Point(__getClient.X-1,__getClient.Y+1)
                };

                    __g.DrawLines(__myPen, __point);
                }
                __myPen.Dispose();
            }*/
        }


        protected override void OnPaint(PaintEventArgs pevent)
        {
            if (_myGlobal._language != this._lastLanguage)
            {
                _lastLanguage = _myGlobal._language;
                if (_resultFixed != null && this._resultFixed.Length > 0)
                {
                    this.Text = _myGlobal._resource(this._resultFixed);
                    this.Invalidate();
                }
            }
            base.OnPaint(pevent);
        }

        [Category("_SML")]
        [Description("Name ใช้ในการหา Resource")]
        [DisplayName("Name ใช้ในการหา Resource")]
        public string ResourceName
        {
            get
            {
                return _resultFixed;
            }
            set
            {
                _resultFixed = value;
                this.Invalidate();
            }
        }

        public void _getResource(string tableName)
        {
            /*if (ResourceName.Length != 0)
            {
                if (MyLib._myGlobal._isDesignMode )
                {
                    this.Text = tableName + "." + ResourceName;
                }
                else
                {
                    this.Text = MyLib._myResource._findResource(string.Concat(tableName, ResourceName), this.Text)._str;
                }
            }
            this.Invalidate();*/
        }

        void _myCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                _cellMoveRightWork();
            }
            if (e.KeyCode == Keys.Left)
            {
                _cellMoveLeftWork();
            }
        }

        public event CellCheckBoxMoveRightHandler _cellMoveRight;
        public event CellCheckBoxMoveLeftHandler _cellMoveLeft;

        protected virtual void _cellMoveRightWork()
        {
            if (_cellMoveRight != null) _cellMoveRight(this);
        }

        protected virtual void _cellMoveLeftWork()
        {
            if (_cellMoveLeft != null) _cellMoveLeft(this);
        }
    }

    public delegate void CellCheckBoxMoveRightHandler(object sender);
    public delegate void CellCheckBoxMoveLeftHandler(object sender);

    public partial class _myLabel : System.Windows.Forms.Label
    {
        public int _row;
        public int _column = -1;
        public string _field_name;
        public string _help_name = "";
        //public string _resource_name = "";
        public string _buttomStr;
        public _languageEnum _lastLanguage = _languageEnum.Null;
        public Boolean _getResource = false;
        private string _resultFixed = "";

        public _myLabel()
        {
            this.Padding = new Padding(0, 0, 0, 0);
        }

        [Category("_SML")]
        [Description("Name ใช้ในการหา Resource")]
        [DisplayName("Name ใช้ในการหา Resource")]
        public string ResourceName
        {
            get
            {
                return _resultFixed;
            }
            set
            {
                _resultFixed = value;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_myGlobal._language != this._lastLanguage)
            {
                this._lastLanguage = _myGlobal._language;
                if (this._resultFixed.Length > 0 && this._column == -1)
                {
                    this.Text = _myGlobal._resource(this._resultFixed);
                    this.Invalidate();
                }
            }
            base.OnPaint(e);
        }
    }

    public partial class _myPanel : System.Windows.Forms.Panel
    {
        /// <summary>
        /// สีเริ่มต้น
        /// </summary>
        public Color _colorBegin = Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
        /// <summary>
        /// สีสุดท้าย
        /// </summary>
        public Color _colorEnd = Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
        /// <summary>
        /// แสดง Background (เส้น)
        /// </summary>
        public Boolean _showBackground = true;
        public Boolean _showLine = false;
        private int _showLineCount = 10;
        Image _cornerPicture;
        private Size _originalSizeResult;
        private Point _originalLocationResult;
        private Boolean _switchTabAutoTemp = false;
        private _myTabControl _tabPointer = null;
        private Boolean _findTabFirst = false;

        public _myPanel()
        {
            /*this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UseTextForAccessibility, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.CacheText, true);*/
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.LocationChanged += new EventHandler(_myPanel_LocationChanged);
            this.SizeChanged += new EventHandler(_myPanel_SizeChanged);
        }

        /// <summary>
        /// กรณีต้องการให้กด CTRL+1 เท่ากับ Tab 1
        /// </summary>
        public Boolean _switchTabAuto
        {
            set
            {
                this._switchTabAutoTemp = value;
                this._tabPointer = null;
            }
            get
            {
                return this._switchTabAutoTemp;
            }
        }

        private Control _findTabPointer(Control source)
        {
            if (this._tabPointer == null)
            {
                foreach (Control __getControl in source.Controls)
                {
                    if (__getControl.GetType() == typeof(_myTabControl))
                    {
                        this._tabPointer = (_myTabControl)__getControl;
                        break;
                    }
                    _findTabPointer(__getControl);
                }
            }
            return source;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (this._switchTabAuto == true)
            {
                if (this._findTabFirst == false && this._tabPointer == null)
                {
                    _findTabPointer(this);
                    this._findTabFirst = true;
                }
                if (this._tabPointer != null)
                {
                    Keys __keyCode = (Keys)(int)keyData & Keys.KeyCode;
                    if ((keyData & Keys.Alt) == Keys.Alt)
                    {
                        switch (__keyCode)
                        {
                            case Keys.D1: this._tabPointer.SelectedIndex = 0; return true;
                            case Keys.D2: this._tabPointer.SelectedIndex = 1; return true;
                            case Keys.D3: this._tabPointer.SelectedIndex = 2; return true;
                            case Keys.D4: this._tabPointer.SelectedIndex = 3; return true;
                            case Keys.D5: this._tabPointer.SelectedIndex = 4; return true;
                            case Keys.D6: this._tabPointer.SelectedIndex = 5; return true;
                            case Keys.D7: this._tabPointer.SelectedIndex = 6; return true;
                            case Keys.D8: this._tabPointer.SelectedIndex = 7; return true;
                            case Keys.D9: this._tabPointer.SelectedIndex = 8; return true;
                            case Keys.A: this._tabPointer.SelectedIndex = 9; return true;
                            case Keys.B: this._tabPointer.SelectedIndex = 10; return true;
                            case Keys.C: this._tabPointer.SelectedIndex = 11; return true;
                            case Keys.D: this._tabPointer.SelectedIndex = 12; return true;
                            case Keys.E: this._tabPointer.SelectedIndex = 13; return true;
                            case Keys.F: this._tabPointer.SelectedIndex = 14; return true;
                            case Keys.G: this._tabPointer.SelectedIndex = 15; return true;
                            case Keys.H: this._tabPointer.SelectedIndex = 16; return true;
                            case Keys.I: this._tabPointer.SelectedIndex = 17; return true;
                            case Keys.J: this._tabPointer.SelectedIndex = 18; return true;
                            case Keys.K: this._tabPointer.SelectedIndex = 19; return true;
                            case Keys.L: this._tabPointer.SelectedIndex = 20; return true;
                            case Keys.M: this._tabPointer.SelectedIndex = 21; return true;
                        }
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _myPanel_SizeChanged(object sender, EventArgs e)
        {
            if (this.Name.Length > 0)
            {
                this.SizeChanged -= new EventHandler(_myPanel_SizeChanged);
                this._originalSizeResult = this.Size;
            }
        }

        void _myPanel_LocationChanged(object sender, EventArgs e)
        {
            this.LocationChanged -= new EventHandler(_myPanel_LocationChanged);
            this._originalLocationResult = this.Location;
        }

        [Category("_SML")]
        [Description("ขนาดเดิม")]
        public Size OriginalSize
        {
            get
            {
                return this._originalSizeResult;
            }
        }

        [Category("_SML")]
        [Description("ตำแหน่งเดิม")]
        public Point OriginalLocation
        {
            get
            {
                return this._originalLocationResult;
            }
        }

        /// <summary>
        /// รูปภาพ
        /// </summary>
        [Category("_SML")]
        [Description("รูปภาพ")]
        public Image CornerPicture
        {
            get
            {
                return _cornerPicture;
            }
            set
            {
                _cornerPicture = value;
            }
        }

        /// <summary>
        /// แสดง Background (เส้น)
        /// </summary>
        [Category("_SML")]
        [Description("จำนวนเส้น (Background)")]
        [DefaultValue(10)]
        public int ShowBackgroundLineCount
        {
            get
            {
                return _showLineCount;
            }
            set
            {
                _showLineCount = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// แสดง Background (เส้น)
        /// </summary>
        [Category("_SML")]
        [Description("แสดง Background (พื้น)")]
        [DefaultValue(true)]
        public bool ShowBackground
        {
            get
            {
                return _showBackground;
            }
            set
            {
                _showBackground = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// แสดง Background (เส้น)
        /// </summary>
        [Category("_SML")]
        [Description("แสดง Background (เส้น)")]
        [DefaultValue(false)]
        public bool ShowLineBackground
        {
            get
            {
                return _showLine;
            }
            set
            {
                _showLine = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// สีเริ่มต้น
        /// </summary>
        [Category("_SML")]
        [Description("สีเริ่มต้น")]
        public Color BeginColor
        {
            get
            {
                return (_colorBegin);
            }
            set
            {
                _colorBegin = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// สีสุดท้าย
        /// </summary>
        [Category("_SML")]
        [Description("สีสุดท้าย")]
        public Color EndColor
        {
            get
            {
                return (_colorEnd);
            }
            set
            {
                _colorEnd = value;
                this.Invalidate();
            }
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            if (ShowBackground)
            {
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                using (System.Drawing.Drawing2D.LinearGradientBrush __GBrush = new System.Drawing.Drawing2D.LinearGradientBrush(new Point(0, 0), new Point(0, this.Height), _colorBegin, _colorEnd))
                {
                    Rectangle __rect = new Rectangle(0, 0, this.Width, this.Height);
                    e.Graphics.FillRectangle(__GBrush, __rect);
                    __GBrush.Dispose();
                }

            }
            if (ShowLineBackground)
            {
                // Create pens.
                Pen __greenPen = new Pen(Color.White, 0.1f);

                // Create points that define curve.
                float __div1 = (this.Width / 2) / ShowBackgroundLineCount;
                float __top = this.Width - (this.Width / ShowBackgroundLineCount);
                // Create offset, number of segments, and tension.
                int __offset = 0;
                int __numSegments = 2;
                float __tension = 1F;

                for (int __loop = 0; __loop < ShowBackgroundLineCount; __loop++)
                {
                    PointF __point1 = new PointF(__top -= 5, 0);
                    PointF __point2 = new PointF(this.Width - (this.Width / 4), this.Height - (this.Height / 2));
                    PointF __point3 = new PointF(__loop * __div1, this.Height);
                    PointF[] __curvePoints = { __point1, __point2, __point3 };
                    // Draw curve to screen.
                    e.Graphics.DrawCurve(__greenPen, __curvePoints, __offset, __numSegments, __tension);
                }
                __greenPen.Dispose();
            }
            base.OnPaint(e);
            if (CornerPicture != null)
            {
                e.Graphics.DrawImage(CornerPicture, this.Size.Width - (CornerPicture.Size.Width + 10), this.Size.Height - (CornerPicture.Size.Height + 10), CornerPicture.Size.Width, CornerPicture.Size.Height);
            }
        }
    }

    public partial class _myFlowLayoutPanel : System.Windows.Forms.FlowLayoutPanel
    {
        public int _level = -1;

        public _myFlowLayoutPanel()
        {
            this.DoubleBuffered = true;
            /*this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UseTextForAccessibility, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.CacheText, true);*/
            this.BackColor = Color.Transparent;
        }
    }


    public class AutoClosingMessageBox
    {
        System.Threading.Timer _timeoutTimer;
        string _caption;
        AutoClosingMessageBox(string text, string caption, int timeout)
        {
            _caption = caption;
            _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                null, timeout, System.Threading.Timeout.Infinite);
            MessageBox.Show(text, caption);
        }
        public static void Show(string text, string caption, int timeout)
        {
            new AutoClosingMessageBox(text, caption, timeout);
        }

        void OnTimerElapsed(object state)
        {
            IntPtr mbWnd = FindWindow(null, _caption);
            if (mbWnd != IntPtr.Zero)
                SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            _timeoutTimer.Dispose();
        }
        const int WM_CLOSE = 0x0010;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
    }

}
