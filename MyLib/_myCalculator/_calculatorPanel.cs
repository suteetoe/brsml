using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MyLib;
using System.Globalization;

namespace MyLib
{
    public partial class _CalculatorPanel : _myForm
    {

        #region State

        private Control _m_ResultControl;
        private Control _m_AnchorControl;
        private string _m_StartValue = String.Empty;
        private string _m_Result = "0";
        private bool _m_Restart = true;
        private Color _m_BorderColor = Color.White;
        private char _m_LastOp = '\0';
        private char _firstchar;
        private bool _m_IsInEquals = false;
        private Stack<double> _m_Stack = new Stack<double>();
        private EventHandler<MyLib._CalculatorParseEventArgs> _m_Parse;
        private EventHandler<_CalculatorFormatEventArgs> _m_Format;
        private bool _m_Centered = false;
        private bool _m_AutoEvaluatePercentKey = false;

        #endregion //--State


        #region Construction

        public _CalculatorPanel()
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint, true);
            __m_Btn_Num_Negate.Text = '\u00B1'.ToString();
            __m_Btn_Num_Dot.Text = _GetSeparatorChar().ToString();
        }
        #endregion //--Construction


        #region Event Handlers

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _HookAnchorEvents(true);
            _InitStartValue();
            __m_LblTitle.Text = Text;
        }

        protected override void OnClosed(EventArgs e)
        {
            _HookAnchorEvents(false);
            base.OnClosed(e);
        }

        private void AnchorLocationChanged(object sender, EventArgs e)
        {
            _Reposition();
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (e.KeyCode == Keys.Escape)
            {
                _CancelAndClose();
            }
            if (e.KeyCode == Keys.F2) {
                _OkAndClose();
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            char __c = e.KeyChar;
            EventArgs ev = EventArgs.Empty;
            if (Char.IsDigit(__c) || __c == _GetSeparatorChar())
                _AddChar(__c);
            else
            {
                switch (__c)
                {
                    case '+':
                    case '-':
                    case '/':
                    case '*':
                        __DoOpChar(__c);
                        break;
                    case '%':
                        _DoPercentChar();
                        break;
                    case '=':
                    case '\r':
                        _DoEqualsChar();
                        break;
                    default:
                        break;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics __g = e.Graphics;
            Rectangle __bounds = ClientRectangle;
            __bounds.Inflate(-4, -4);
            __g.DrawRectangle(new Pen(_m_BorderColor, 2f), __bounds);
        }

        protected virtual void OnParse(_CalculatorParseEventArgs e)
        {
            if (null != _m_Parse)
                _m_Parse(this, e);
        }

        protected virtual void OnFormat(_CalculatorFormatEventArgs e)
        {
            if (null != _m_Format)
                _m_Format(this, e);
        }
        #endregion //--Event Handlers


        #region Private Members

        private void _HookAnchorEvents(bool hook)
        {
            Control __ctl = _GetAnchorControl();
            while (null != __ctl)
            {
                __ctl.LocationChanged -= new EventHandler(
                    AnchorLocationChanged);
                __ctl = __ctl.Parent;
            }
            if (hook)
            {
                __ctl = _GetAnchorControl();
                while (null != __ctl)
                {
                    __ctl.LocationChanged += new EventHandler(
                        AnchorLocationChanged);
                    __ctl = __ctl.Parent;
                }
                __ctl = _GetAnchorControl();
                if (null != __ctl)
                    Owner = __ctl.FindForm();
            }
        }

        private Control _GetAnchorControl()
        {
            if (null != _m_ResultControl)
                return _m_ResultControl;

            return _m_AnchorControl;
        }

        private char _GetSeparatorChar()
        {
            CultureInfo __culture = CultureInfo.CurrentCulture;
            string sep = __culture.NumberFormat.NumberDecimalSeparator;
            if (String.IsNullOrEmpty(sep))
                return '.';

            return sep[0];
        }

        private void _SetDisplay(string s)
        {
            __m_TxtDisplay.Focus();
            _m_Result = s;
            __m_TxtDisplay.Text = s;


        }

        private void _AddChar(char c)
        {
            string __s = __m_TxtDisplay.Text;
            if (c == _GetSeparatorChar() &&
                __s.Contains(_GetSeparatorChar().ToString()) && !_m_Restart)
                return;

            if (__s == "0")
                __s = c.ToString();
            else
                __s += c;

            _SetDisplay(__s);
            if (_m_Restart)
            {
                _SetDisplay(c.ToString());
                _m_Restart = false;

            }

        }

        private double _DisplayValue
        {
            get
            {
                double d = 0;

                if (Double.TryParse(__m_TxtDisplay.Text, out d))
                {
                    this.__listResult.Items.Add(d.ToString() + this._firstchar.ToString());
                    return d;
                }
                return 0.0;
            }
        }

        private void _DoLastOp()
        {
            _m_Restart = true;
            if (_m_LastOp == '\0' || _m_Stack.Count == 1)
            {
                return;
            }

            double __valTwo = _m_Stack.Pop();
            double __valOne = _m_Stack.Pop();
            switch (_m_LastOp)
            {
                case '+':
                    _m_Stack.Push(__valOne + __valTwo);

                    break;
                case '-':
                    _m_Stack.Push(__valOne - __valTwo);

                    break;
                case '*':
                    _m_Stack.Push(__valOne * __valTwo);


                    break;
                case '/':
                    _m_Stack.Push(__valOne / __valTwo);

                    break;
                default:
                    break;
            }            
            _SetDisplay(_m_Stack.Peek().ToString());
            if (_m_IsInEquals)
            {
                _m_Stack.Push(__valTwo);
                __listResult.Items.Add(__m_TxtDisplay.Text + "=");
            }
            else
            {

            }
        }

        private void __DoOpChar(char op)
        {
            _firstchar = op;
            if (_m_IsInEquals)
            {
                _m_Stack.Clear();
                _m_IsInEquals = false;
            }
            _m_Stack.Push(_DisplayValue);
            _DoLastOp();
            _m_LastOp = op;
            _firstchar = _m_LastOp;
        }

        private void _DoEqualsChar()
        {
            if (_m_LastOp == '\0')
                return;

            if (!_m_IsInEquals)
            {
                _m_IsInEquals = true;
                _m_Stack.Push(_DisplayValue);
            }
            _DoLastOp();
        }

        private void _DoPercentChar()
        {
            if (_m_Stack.Count == 0)
                return;

            _SetDisplay((_m_Stack.Peek() * (_DisplayValue / 100.0)).ToString());
            if (_AutoEvaluatePercentKey)
                _DoEqualsChar();
        }

        private void _ResetAll()
        {
            _SetDisplay("0");
            _m_LastOp = '\0';
            _m_Restart = true;
            _m_Stack.Clear();
            __listResult.Items.Clear();
        }

        private void _InitStartValue()
        {
            if (null != _m_ResultControl)
            {
                if (_m_ResultControl.GetType() == typeof(MyLib._myNumberBox))
                {
                    MyLib._myNumberBox __coltrol = (_myNumberBox)_m_ResultControl;
                    _m_StartValue = __coltrol.textBox.Text;
                }
                else
                {
                    _m_StartValue = _m_ResultControl.Text;
                }
            }
            _CalculatorParseEventArgs __cpe = new _CalculatorParseEventArgs(_m_StartValue);
            OnParse(__cpe);
            _SetDisplay(__cpe._GetResult().ToString());
        }

        private void _OkAndClose()
        {
            _SetDisplay(__m_TxtDisplay.Text);
            _CalculatorFormatEventArgs cfe = new _CalculatorFormatEventArgs(_Result);
            OnFormat(cfe);
            Close();
            if (null != _m_ResultControl)
            {
                if (_m_ResultControl.GetType() == typeof(MyLib._myNumberBox))
                {
                    MyLib._myNumberBox __coltrol = (_myNumberBox)_m_ResultControl;
                    __coltrol.textBox.Text = __m_TxtDisplay.Text;
                }
                else
                {
                    _m_ResultControl.Text = __m_TxtDisplay.Text;
                    _m_ResultControl.Focus();
                }
            }
            if (null != _m_AnchorControl)
            {
                _m_AnchorControl.Focus();
            }
        }

        private void _CancelAndClose()
        {
            _m_Result = _m_StartValue;
            Close();
        }
        #endregion //--Private Members


        #region Control Events

        private void NumberBtnClick(object sender, EventArgs e)
        {
            _AddChar(((Button)sender).Text[0]);
        }

        private void OpBtnClick(object sender, EventArgs e)
        {
            __DoOpChar(((Button)sender).Text[0]);
        }

        private void EqualsBtnClick(object sender, EventArgs e)
        {
            _DoEqualsChar();
        }

        private void PercentBtnClick(object sender, EventArgs e)
        {
            _DoPercentChar();
        }

        private void NegateBtnClick(object sender, EventArgs e)
        {
            _SetDisplay((-(_DisplayValue)).ToString());
        }

        private void ClearBtnClick(object sender, EventArgs e)
        {
            _ResetAll();
        }

        private void OkBtnClick(object sender, EventArgs e)
        {
            _OkAndClose();
        }

        private void CancelBtnClick(object sender, EventArgs e)
        {
            _CancelAndClose();
        }
        #endregion //--Control Events


        #region Public Interface

        public void Accept()
        {
            _OkAndClose();
        }

        [Category("Calculator")]
        public event EventHandler<_CalculatorParseEventArgs> _Parse
        {
            add { _m_Parse += value; }
            remove { _m_Parse -= value; }
        }

        [Category("Calculator")]
        public event EventHandler<_CalculatorFormatEventArgs> _Format
        {
            add { _m_Format += value; }
            remove { _m_Format -= value; }
        }

        /// <summary>
        /// Gets or sets the control that opened the calculator. This is used
        /// for positioning if ResultControl is null.
        /// </summary>
        [Browsable(false)]
        public Control _AnchorControl
        {
            get { return _m_AnchorControl; }
            set
            {
                _m_AnchorControl = value;
                _Reposition();
                _HookAnchorEvents(true);
            }
        }

        /// <summary>
        /// Gets or sets the control where the text result will be stored.
        /// </summary>
        [Category("Calculator")]
        [DefaultValue(null)]
        public Control _ResultControl
        {
            get { return _m_ResultControl; }
            set
            {
                _m_ResultControl = value;
                _Reposition();
                _HookAnchorEvents(true);
                _ResetAll();
                _InitStartValue();
            }
        }

        [Category("_SMLCalculator")]
        [DefaultValue(typeof(Color), "White")]
        public Color _BorderColor
        {
            get { return _m_BorderColor; }
            set
            {
                _m_BorderColor = value;
                Invalidate();
            }
        }

        [Category("_SMLCalculator")]
        [DefaultValue(typeof(Color), "Black")]
        public Color _NumberColor
        {
            get { return __m_Btn_Num_0.ForeColor; }
            set
            {
                foreach (Control __c in Controls)
                {
                    if (__c.Name.Contains("_Num_"))
                        __c.ForeColor = value;
                }
                Invalidate();
            }
        }

        [Category("_SMLCalculator")]
        [DefaultValue(typeof(Color), "DimGray")]
        public Color _OperatorColor
        {
            get { return __m_Btn_Op_Add.ForeColor; }
            set
            {
                foreach (Control __c in Controls)
                {
                    if (__c.Name.Contains("_Op_"))
                        __c.ForeColor = value;
                }
                Invalidate();
            }
        }

        [Category("_SMLCalculator")]
        [DefaultValue(typeof(Color), "White")]
        public Color _TitleColor
        {
            get { return __m_LblTitle.ForeColor; }
            set
            {
                __m_LblTitle.ForeColor = value;
                Invalidate();
            }
        }

        [Category("_SMLCalculator")]
        [DefaultValue(FlatStyle.Standard)]
        public FlatStyle _ButtonFlatStyle
        {
            get { return __m_Btn_Num_0.FlatStyle; }
            set
            {
                foreach (Control __c in Controls)
                {
                    if (__c is Button)
                        ((Button)__c).FlatStyle = value;
                }
                Invalidate();
            }
        }

        [Category("_SMLCalculator")]
        [DefaultValue(false)]
        public bool _AutoEvaluatePercentKey
        {
            get { return _m_AutoEvaluatePercentKey; }
            set { _m_AutoEvaluatePercentKey = value; }
        }

        [Browsable(false)]
        public string _StartValue
        {
            get { return _m_StartValue; }
            set { _m_StartValue = null == value ? String.Empty : value; }
        }

        [Browsable(false)]
        public double _Result
        {
            get
            {
                double d = 0;
                Double.TryParse(_m_Result, out d);
                return d;
            }
        }

        [Browsable(false)]
        public string _StringResult
        {
            get { return _m_Result; }
        }

        public void _Reposition()
        {
            Control __ctl = _GetAnchorControl();
            if (null == __ctl)
                return;

            if (!_m_Centered)
            {
                CenterToScreen();
                _m_Centered = true;
            }
            Point __pt = _locationFinder._GetScreenLocation(__ctl);
            Rectangle __screen = Screen.PrimaryScreen.WorkingArea;
            if (__screen.Height < __pt.Y + Height + __ctl.Height)
                __pt = new Point(__pt.X, __pt.Y - Height);
            else
                __pt = new Point(__pt.X, __pt.Y + __ctl.Height);
            Location = __pt;
        }
        #endregion //--Public Interface

        private void __showlist_Click(object sender, EventArgs e)
        {
            int __xOldposition = 0;
            if (this.__panelList.Visible)
            {
                this.__panelList.Visible = false;
                this.Size = new Size(174, 233);                
                this.__showlist.Text = "ShowList";
                __xOldposition =this.Location.X;
                this.Location = new Point(__xOldposition += 76, this.Location.Y);
            }
            else
            {
                this.__panelList.Visible = true;
                this.Size = new Size(250, 233);
                this.__showlist.Text = "HideList";
                __xOldposition = this.Location.X;
                this.Location = new Point(__xOldposition -= 76, this.Location.Y);
            }
        }
        private void __listResult_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            Brush __textBrush = SystemBrushes.ControlText;
            Font __drawFont = e.Font;

            if (__listResult.Items[e.Index].ToString().EndsWith("="))
            {
                __textBrush = Brushes.Blue;
                __drawFont = new Font(__drawFont.FontFamily, __drawFont.Size, FontStyle.Underline);
            }
            else if (__listResult.Items[e.Index].ToString().Contains("-"))
            {
                __textBrush = Brushes.Red;
                __drawFont = new Font(__drawFont.FontFamily, __drawFont.Size, FontStyle.Underline);
            }
            else if ((e.State & DrawItemState.Selected) > 0)
            {
                __textBrush = SystemBrushes.HighlightText;
            }

            e.Graphics.DrawString(this.__listResult.Items[e.Index].ToString(), __drawFont, __textBrush, e.Bounds);
        }
    }
}
