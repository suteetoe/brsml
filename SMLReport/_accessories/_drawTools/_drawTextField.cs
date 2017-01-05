using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Globalization;
using System.Collections;
using System.Text.RegularExpressions;

namespace SMLReport._design
{
    [Serializable]
    public class _drawTextField : _drawLabel
    {
        #region TextBox Field

        private const string _entryTextBox = "TextBox";
        private const string _entryField = "Field";
        private const string _entryAllowLineSpace = "AllowLineSpace";
        private const string _entryFieldType = "FieldType";
        private const string _entryFormatText = "FieldFormat";
        private const string _entryReplaceText = "ReplaceText";
        private const string _entryDataQuery = "Query";
        private const string _entrySpecialField = "SpecialField";
        private const string _entryAsField = "AsField";
        
        private string _fieldProperty = "[EmptyField]";
        private string _fieldCurrencyProperty = "[EmptyField]";

        private SMLReport._formReport._queryRule __queryRuleProperty;
        private string _fieldFormat = "";
        private _FieldType _fieldType;
        private string _specialFieldResult = "";
        private string _asFieldResult = "";

        private bool _autoLineSpaceResult = true;
        private float _lineSpaceResult;
        private bool _showIsNumberZeroResult = true;

        private Color _textFieldHighlightColor = Color.LightGray;
        private string _replaceTextResult = "";

        private string _textFieldReplace = "";
        private bool _multiLineResult = true;

        private _fieldOperation _operationResult = _fieldOperation.None;

        #endregion

        #region TextBox Property

        /// <summary>ชื่อตัวแปร</summary>
        [Category("Data")]
        [DisplayName("Variable : ชื่อตัวแปร")]
        public string _asField
        {
            get
            {
                return _asFieldResult;
            }
            set
            {
                _asFieldResult = value;
            }
        }

        [Category("Data")]
        [DisplayName("Operation")]
        [DefaultValue(_fieldOperation.None)]
        public _fieldOperation _operation
        {
            get
            {
                return _operationResult;
            }
            set
            {
                _operationResult = value;
            }
        }

        [Category("_SML")]
        [Description("ขึ้นบรรทัดใหม่ กรณี ที่ข้อความยาวกว่า ช่องที่วาดไว้ หรือต้องการให้ข้อมูลแสดงที่ 1 บรรทัด")]
        [DisplayName("MultiLine : ")]
        public bool _multiLine
        {
            get
            {
                return _multiLineResult;
            }
            set
            {
                _multiLineResult = value;
            }
        }

        [Category("Data")]
        [DisplayName("Spacial Calculate: ")]
        [Description("ใช้สำหรับ คำนวณ Field \n Sample \n ([a.field1]+[a.field2])/100")]
        public string _specialField
        {
            get
            {
                return _specialFieldResult;
            }
            set
            {
                _specialFieldResult = value;
            }
        }

        [Category("Data")]
        [Description("ปรับเปลี่ยนการแสดงผมข้อมูล ใช้ @ แทน ค่าที่ได้จาก Field ตัวอย่าง (@) ผลลัพท์ (2,000.00)")]
        [DisplayName("ReplaceText")]
        public string _replaceText
        {
            get
            {
                return _replaceTextResult;
            }
            set
            {
                _replaceTextResult = value;
                _setTextSize();
            }

        }

        [Category("_SML")]
        [Description("การคำณวนระยะห่างระหว่าบรรทัด")]
        [DisplayName("AutoLineSpace")]
        public bool _autoLineSpace
        {
            get
            {
                return _autoLineSpaceResult;
            }

            set
            {
                _autoLineSpaceResult = value;
            }
        }

        [Category("_SML")]
        [Description("ระห่างระหว่างบรรทัด (กรณีมีข้อมูลหลายบรรทัด)")]
        [DisplayName("LineSpace")]
        public float _lineSpace
        {
            get
            {
                return _lineSpaceResult;
            }

            set
            {
                _lineSpaceResult = value;
            }
        }

        [Category("_SML")]
        [Description("ไม่แสดงกรณีมีค่าเป็นศูนย์")]
        [DisplayName("Show Zero Value :")]
        public bool _showIsNumberZero
        {
            get
            {
                return _showIsNumberZeroResult;
            }

            set
            {
                _showIsNumberZeroResult = value;
            }
        }

        [Browsable(false)]
        public new string _text { get { return base._text; } set { base._text = value; } }

        [Category("Data")]
        [Description("ฟิลด์ข้อมูล")]
        [DisplayName("Field : ข้อมูล")]
        [TypeConverter(typeof(_fieldConverter))]
        public string Field
        {
            get
            {
                return _fieldProperty;
            }
            set
            {
                _fieldProperty = value;
                _setTextSize();
            }
        }

        [Category("Data")]
        [Description("ฟิลด์สกุลเงิน")]
        [DisplayName("Field Currency : สกุลเงิน")]
        [TypeConverter(typeof(_fieldConverter))]
        public string FieldCurrency
        {
            get
            {
                return _fieldCurrencyProperty;
            }
            set
            {
                _fieldCurrencyProperty = value;
            }
        }

        [Category("_SML")]
        [Description("รูปแบบตัวอักษร")]
        [DisplayName("Font : รูปแบบตัวอักษร")]
        public new Font _font
        {
            get
            {
                return base._font;
            }
            set
            {
                base._font = value;
                _setTextSize();
            }
        }


        [Category("Data")]
        [Description("ฟิลด์ข้อมูล")]
        [DisplayName("Format : รูปแบบ")]
        [TypeConverter(typeof(string))]
        public string FieldFormat
        {
            get
            {
                return _fieldFormat;
            }
            set
            {
                _fieldFormat = value;
            }
        }

        [Category("Data")]
        [Description("ฟิลด์ข้อมูล")]
        [DisplayName("Type : ประเภท")]
        [TypeConverter(typeof(_FieldType))]
        public _FieldType FieldType
        {
            get
            {
                return _fieldType;
            }
            set
            {
                _fieldType = value;
            }
        }

        [Category("Data")]
        [Description("เลือกชุด query")]
        [DisplayName("DataQuery")]
        public SMLReport._formReport._queryRule query
        {
            get
            {
                return __queryRuleProperty;
            }

            set
            {
                __queryRuleProperty = value;
            }
        }

        #endregion

        #region TextBox Constructor

        public _drawTextField()
        {
            this._autoSize = false;
            _setTextSize();
        }

        public _drawTextField(int __x, int __y, int __width, int __height, float __scale)
        {
            this._autoSize = false;
            _drawScale = __scale;
            //_txext1Result = _xtext2Result = _entryLabel;

            //__x = (int)(__x / _drawScale);
            //__y = (int)(__y / _drawScale);
            //__width = (int)(__width / _drawScale);
            //__height = (int)(__height / _drawScale);
            //_actualSize = new Rectangle(__x, __y, __width, __height);

            _setRectangle(__x, __y, __width, __height, __scale);
            _initialize();
            this._foreColor = Color.Black;
            this._lineColor = Color.Transparent;
            _setTextSize();
        }

        #endregion

        #region TextBox Methods

        public override void _setTextSize()
        {
            _textFieldReplace = _getReplaceText(_getResource(this._fieldProperty));
            _textSizeResult = this._getTextSize(_textFieldReplace, base._font);
        }

        public override void _draw(Graphics g)
        {
            this._draw(g, new PointF(0, 0), false);
        }

        public void _draw(Graphics g, bool _highlightTextField)
        {
            this._draw(g, new PointF(0, 0), _highlightTextField);
        }

        public void _draw(Graphics g, PointF __point, bool _highlightTextField)
        {
            Pen pen = new Pen((this._lineColor == Color.Transparent) ? Color.LightGray : this._lineColor, this._penWidth * _drawScale);
            pen.DashStyle = (this._lineColor == Color.Transparent) ? System.Drawing.Drawing2D.DashStyle.DashDot : this._lineStyle;
            SolidBrush brush = new SolidBrush(this._foreColor);
            SolidBrush __BgBrush = new SolidBrush(this._backColor);

            Font __newFont = new Font(_font.FontFamily, (float)(_font.Size * _drawScale), _font.Style, _font.Unit, _font.GdiCharSet, _font.GdiVerticalFont);

            RectangleF __drawStrRect = _drawRectangle._getNormalizedRectangle(Size);
            __drawStrRect.X += __point.X;
            __drawStrRect.Y += __point.Y;

            if (_highlightTextField)
            {
                g.FillRectangle(new SolidBrush(_textFieldHighlightColor), Rectangle.Round(__drawStrRect));
            }
            else
            {
                g.FillRectangle(__BgBrush, Rectangle.Round(__drawStrRect));
            }

            g.DrawRectangle(pen, Rectangle.Round(__drawStrRect));

            if (this._fieldProperty != "")
            {
                string __strShowText = _textFieldReplace;

                SizeF __stringSize = this._textSizeResult;

                if (this._autoSize)
                {
                    this._actualSize = Rectangle.Round(new RectangleF(_rectangleResult.X, _rectangleResult.Y, (float)Math.Ceiling(__stringSize.Width / _drawScale), (float)Math.Ceiling(__stringSize.Height / _drawScale)));
                }

                PointF __drawStrPoint = __drawStrRect.Location;

                PointF __getDrawPoint = _getPointTextAlingDraw(__drawStrRect.Width, __drawStrRect.Height, __stringSize.Width, __stringSize.Height, this._textAlign, _padding);
                g.DrawString(__strShowText, __newFont, brush, (__drawStrPoint.X + __getDrawPoint.X), (__drawStrPoint.Y + __getDrawPoint.Y), StringFormat.GenericTypographic);

                //g.DrawString(__strShowText, __newFont, brush, _getRectangleFPadding(__drawStrRect, _padding), _getStringFormat(_textAlign));
            }

            __newFont.Dispose();
            __BgBrush.Dispose();
            pen.Dispose();
            brush.Dispose();

        }

        private string _getReplaceText(string __data)
        {
            if (__data == null)
                return "";

            if (_replaceText.IndexOf("@") != -1)
            {
                __data = _replaceText.Replace("@", __data);
            }

            return __data;
        }

        public override void _drawFromPoint(Graphics g, PointF __point, float __drawScale)
        {
            this._drawScale = __drawScale;
            _draw(g, __point, false);
        }

        private string _getResource(string __strFieldAndResource)
        {
            if (__strFieldAndResource == null)
                return "";

            string[] __split = __strFieldAndResource.Split(',');
            if (__strFieldAndResource.Trim().Length == 0 || __strFieldAndResource.Length == 0)
            {
                return __strFieldAndResource;
            }

            if (__split.Length == 1)
                return __split[0].Trim().ToString();

            string __result = __split[1].Trim();
            if (__result.Length == 0)
            {
                return __split[0].Trim();
            }

            return __result;
        }

        #endregion

        public override void Dispose()
        {
            base.Dispose();
        }

        #region Save Load From Stream

        public override void _saveToStream(System.Runtime.Serialization.SerializationInfo info, int orderNumber)
        {
            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTextBox + _entryField, orderNumber),
                _fieldProperty);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTextBox + _entryAllowLineSpace, orderNumber),
                _autoLineSpace);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTextBox + _entryFieldType, orderNumber),
                FieldType);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTextBox + _entryFormatText, orderNumber),
                FieldFormat);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTextBox + _entryReplaceText, orderNumber),
                _replaceText);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTextBox + _entrySpecialField, orderNumber),
                _specialField);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTextBox + _entryAsField, orderNumber),
                _asFieldResult);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTextBox + _entryDataQuery, orderNumber),
                query);

            base._saveToStream(info, orderNumber);
        }

        public override void _loadFromStream(System.Runtime.Serialization.SerializationInfo info, int orderNumber)
        {
            _fieldProperty = (string)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTextBox + _entryField, orderNumber),
                typeof(string));

            _autoLineSpace = (bool)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTextBox + _entryAllowLineSpace, orderNumber),
                typeof(bool));

            FieldType = (_FieldType)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTextBox + _entryFieldType, orderNumber),
                typeof(_FieldType));

            FieldFormat = (string)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTextBox + _entryFormatText, orderNumber),
                typeof(string));

            _replaceText = (string)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTextBox + _entryReplaceText, orderNumber),
                typeof(string));

            _specialField = (string)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTextBox + _entrySpecialField, orderNumber),
                typeof(string));

            _asFieldResult = (string)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTextBox + _entryAsField, orderNumber),
                typeof(string));

            query = (_formReport._queryRule)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTextBox + _entryDataQuery, orderNumber),
                typeof(_formReport._queryRule));

            base._loadFromStream(info, orderNumber);
            _setTextSize();
        }

        #endregion

        #region comment

        // for format string # ##### ##### ## #
        /*
        if (FieldType == _FieldType.String && FieldFormat != "")
        {
            char[] __showTextChar = __strShowText.ToCharArray();
            Regex __stringRegex = new Regex("#");
            int __strMatch = __stringRegex.Matches(FieldFormat).Count;


            char[] __charFormat = FieldFormat.ToCharArray();

            //if (__showTextChar.Length >= __charFormat.Length) 
            //{
            StringBuilder __tmpFormat = new StringBuilder();
            if (__strMatch > 0)
            {
                int __index = 0;

                int __minLength = (__charFormat.Length > __showTextChar.Length) ? __showTextChar.Length : __charFormat.Length; // จำนวนตัวอักษรในข้อความไม่เท่ากัน

                for (int __i = 0; __i < __charFormat.Length; __i++)
                {
                    string __tmpCharFormat = "";
                    // ตรวจสอบว่า index อยู่ในข้อมูลหรือไม่
                    if (__index < __showTextChar.Length)
                    {
                        if (__charFormat[__i].Equals('#'))
                        {
                            __tmpCharFormat = __showTextChar[__index].ToString();
                            __index++;
                        }
                        else
                        {
                            __tmpCharFormat = __charFormat[__i].ToString();
                        }
                        __tmpFormat.Append(__tmpCharFormat);
                    }
                }
            }

            string __newFormatString = __tmpFormat.ToString();
            __strShowText = __tmpFormat.ToString(); //string.Format(__newFormatString, __showTextChar);
            //}
        }
        */

        // draw multiline

        /*
__strShowText = SMLReport._design._drawLabel._replaceLineBreak(__strShowText, _allowLineBreak);

ArrayList __dataStrList = SMLReport._design._drawLabel._cutString(__strShowText, __newFont, this.Size.Width, _charSpace, _charWidth, _padding);

SizeF __stringSize = this._getTextSize(__dataStrList, __newFont);
*/

        /*
for (int __line = 0; __line < __dataStrList.Count; __line++)
{
    SizeF __strLineSize = _getTextSize((string)__dataStrList[__line], __newFont);

    if (_charSpace != 0)
    {
        char[] __char = ((string)__dataStrList[__line]).ToCharArray();
        SizeF __getDrawSize = _getTextSize((string)__dataStrList[__line], __newFont);
        __getDrawSize.Width += (_charSpace * (__char.Length - 1));

        PointF __getDrawPoint = _getPointTextAlingDraw(__drawStrRect.Width, __drawStrRect.Height, __getDrawSize.Width, __getDrawSize.Height, this._textAlign);

        PointF __currentDrawCharPoint = new PointF();
        for (int __i = 0; __i < __char.Length; __i++)
        {
            g.DrawString(__char[__i].ToString(), __newFont, brush, (__getDrawPoint.X + __strDrawPoint.X + __currentDrawCharPoint.X), __strDrawPoint.Y, StringFormat.GenericTypographic);

            SizeF __currentCharSize = _getTextSize(__char[__i].ToString(), __newFont);
            __currentDrawCharPoint.X += (__currentCharSize.Width + _charSpace);
        }

        //PointF __currentDrawCharPoint = new PointF();
        //for (int __i = 0; __i < __char.Length; __i++)
        //{
        //    g.DrawString(__char[__i].ToString(), __newFont, brush, __drawStrPoint.X + __currentDrawCharPoint.X, __drawStrPoint.Y, StringFormat.GenericTypographic);
        //    SizeF __currentCharSize = _getTextSize(__char[__i].ToString(), __newFont);
        //    __currentDrawCharPoint.X += (__currentCharSize.Width + _charSpace);
        //}

    }
    else
    {
        SizeF __getDrawSize = _getTextSize((string)__dataStrList[__line], __newFont);
        PointF __getDrawPoint = _getPointTextAlingDraw(__drawStrRect.Width, __drawStrRect.Height, __getDrawSize.Width, __getDrawSize.Height, this._textAlign);

        g.DrawString((string)__dataStrList[__line], __newFont, brush, (__strDrawPoint.X + __getDrawPoint.X), __strDrawPoint.Y, StringFormat.GenericTypographic);

        //g.DrawString((string)__dataStrList[__line], __newFont, brush, __drawStrPoint.X, __drawStrPoint.Y, StringFormat.GenericTypographic);
    }
    __strDrawPoint.Y += __strLineSize.Height;

}
                
//g.DrawString(__strShowText, __newFont, brush, __drawStrPoint.X, __drawStrPoint.Y, StringFormat.GenericTypographic);
 */



        #endregion
    }
}
