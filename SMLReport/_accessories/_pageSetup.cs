using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Web.UI.WebControls;

namespace SMLReport._design
{
    public enum _orientationType
    {
        Portrait,
        Landscape
    }

    public enum _pageSizeType
    {
        A4,
        B5,
        Letter,
        Customer
    }

    public enum _measurementUnitType
    {
        Inches,
        Centimeters,
        Millimeters,
        Points,
        Picas
    }

    public enum _marginType
    {
        All,
        Width,
        Height,
        MarginTop,
        MarginBottom,
        MarginLeft,
        MarginRight
    }

    public partial class _pageSetup : Form
    {
        private _measurementUnitType _measurementUnit;
        private _pageSizeType _valuePaperSize = _pageSizeType.A4;
        private _orientationType _valueOrientation = _orientationType.Portrait;
        private float _valuePaperWidth;
        private float _valuePaperHeight;
        private float _valueMarginTop;
        private float _valueMarginBottom;
        private float _valueMarginLeft;
        private float _valueMarginRight;
        public static float _ratioPointPixel = 96f / 72f;

        public _pageSetup()
        {
            InitializeComponent();
            _paperSizeComboBox.Items.Add(_pageSizeType.A4.ToString());
            _paperSizeComboBox.Items.Add(_pageSizeType.B5.ToString());
            _paperSizeComboBox.Items.Add(_pageSizeType.Customer.ToString());
            _paperSizeComboBox.Items.Add(_pageSizeType.Letter.ToString());
            _paperSizeComboBox.SelectedIndexChanged += new EventHandler(_paperSizeComboBox_SelectedIndexChanged);
            _paperSizeComboBox.SelectedIndex = 0; // A4
            //
            _measurementUnitComboBox.Items.Add(_measurementUnitType.Inches.ToString());
            _measurementUnitComboBox.Items.Add(_measurementUnitType.Centimeters.ToString());
            _measurementUnitComboBox.Items.Add(_measurementUnitType.Millimeters.ToString());
            _measurementUnitComboBox.Items.Add(_measurementUnitType.Points.ToString());
            _measurementUnitComboBox.Items.Add(_measurementUnitType.Picas.ToString());
            _measurementUnitComboBox.SelectedIndexChanged += new EventHandler(_measurementUnitComboBox_SelectedIndexChanged);
            _measurementUnitComboBox.SelectedIndex = 1; // Inches
            //
            _getDefaultPaperSize(_valuePaperSize, _valueOrientation);
            this.Invalidated += new InvalidateEventHandler(_pageSetup_Invalidated);
            _orientationSelected();
        }

        void _pageSetup_Invalidated(object sender, InvalidateEventArgs e)
        {
            _orientationSelected();
        }

        /// <summary>
        /// ดึงหน่วยที่เหลือใน Page Setup
        /// </summary>
        public _measurementUnitType Unit
        {
            get
            {
                _measurementUnitType __result = _measurementUnitType.Centimeters;
                switch (_measurementUnitComboBox.SelectedIndex)
                {
                    case 0: __result = _measurementUnitType.Inches; break;
                    case 1: __result = _measurementUnitType.Centimeters; break;
                    case 2: __result = _measurementUnitType.Millimeters; break;
                    case 3: __result = _measurementUnitType.Points; break;
                    case 4: __result = _measurementUnitType.Picas; break;
                }
                return (__result);
            }
        }

        /// <summary>
        /// คำนวณขนาดกระดาษเป็น Pixel
        /// </summary>
        public Size PagePixel
        {
            get
            {
                Size __result = new Size((int)(_valuePaperWidth * _ratioPointPixel), (int)(_valuePaperHeight * _ratioPointPixel));
                return __result;
            }
        }

        void _paperSizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string __getString = _paperSizeComboBox.SelectedItem.ToString();
            if (__getString.Equals(_pageSizeType.A4.ToString()))
            {
                _valuePaperSize = _pageSizeType.A4;
            }
            if (__getString.Equals(_pageSizeType.B5.ToString()))
            {
                _valuePaperSize = _pageSizeType.B5;
            }
            if (__getString.Equals(_pageSizeType.Customer.ToString()))
            {
                _valuePaperSize = _pageSizeType.Customer;
            }
            if (__getString.Equals(_pageSizeType.Letter.ToString()))
            {
                _valuePaperSize = _pageSizeType.Letter;
            }
            _getDefaultPaperSize(_valuePaperSize, _valueOrientation);
            _calcUnitRatio(_marginType.All);
        }

        void _measurementUnitComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string __getString = _measurementUnitComboBox.SelectedItem.ToString();
            if (__getString.Equals(_measurementUnitType.Inches.ToString()))
            {
                _measurementUnit = _measurementUnitType.Inches;
            }
            if (__getString.Equals(_measurementUnitType.Inches.ToString()))
            {
                _measurementUnit = _measurementUnitType.Inches;
            }
            if (__getString.Equals(_measurementUnitType.Centimeters.ToString()))
            {
                _measurementUnit = _measurementUnitType.Centimeters;
            }
            if (__getString.Equals(_measurementUnitType.Millimeters.ToString()))
            {
                _measurementUnit = _measurementUnitType.Millimeters;
            }
            if (__getString.Equals(_measurementUnitType.Picas.ToString()))
            {
                _measurementUnit = _measurementUnitType.Picas;
            }
            if (__getString.Equals(_measurementUnitType.Points.ToString()))
            {
                _measurementUnit = _measurementUnitType.Points;
            }
            _calcUnitRatio(_marginType.All);
        }

        public void _getDefaultPaperSize(_pageSizeType _paperSize, _orientationType _orientation)
        {
            if (_orientation == _orientationType.Portrait)
            {
                switch (_paperSize)
                {
                    case _pageSizeType.A4:
                        _valuePaperWidth = 595.35f;
                        _valuePaperHeight = 841.95f;
                        _valueMarginTop = 28.35f;
                        _valueMarginBottom = 28.35f;
                        _valueMarginLeft = 28.35f;
                        _valueMarginRight = 28.35f;
                        break;

                    case _pageSizeType.B5:
                        _valuePaperWidth = 419.55f;
                        _valuePaperHeight = 595.35f;
                        _valueMarginTop = 28.35f;
                        _valueMarginBottom = 28.35f;
                        _valueMarginLeft = 28.35f;
                        _valueMarginRight = 28.35f;
                        break;

                    case _pageSizeType.Letter:
                        _valuePaperWidth = 611.95f;
                        _valuePaperHeight = 791.95f;
                        _valueMarginTop = 28.35f;
                        _valueMarginBottom = 28.35f;
                        _valueMarginLeft = 28.35f;
                        _valueMarginRight = 28.35f;
                        break;
                }
            }
            else
            {
                switch (_paperSize)
                {
                    case _pageSizeType.A4:
                        _valuePaperWidth = 841.95f;
                        _valuePaperHeight = 595.35f;
                        _valueMarginTop = 28.35f;
                        _valueMarginBottom = 28.35f;
                        _valueMarginLeft = 28.35f;
                        _valueMarginRight = 28.35f;
                        break;

                    case _pageSizeType.B5:
                        _valuePaperWidth = 595.35f;
                        _valuePaperHeight = 419.55f;
                        _valueMarginTop = 28.35f;
                        _valueMarginBottom = 28.35f;
                        _valueMarginLeft = 28.35f;
                        _valueMarginRight = 28.35f;
                        break;

                    case _pageSizeType.Letter:
                        _valuePaperWidth = 791.95f;
                        _valuePaperHeight = 611.95f;
                        _valueMarginTop = 28.35f;
                        _valueMarginBottom = 28.35f;
                        _valueMarginLeft = 28.35f;
                        _valueMarginRight = 28.35f;
                        break;
                }
            }
        }

        /// <summary>
        /// เปลี่ยนจากหน่วยต่างๆ ให้เป็นหน่วย Pixel
        /// </summary>
        /// <param name="_measurementUnit"></param>
        /// <returns></returns>
        public static float _convertUnitToPixel(_measurementUnitType _measurementUnit)
        {
            return _convertUnitToPoint(_measurementUnit) / _ratioPointPixel;
        }

        /// <summary>
        /// เปลียนจากหน่วยต่างๆ ให้เป็นหน่วย Point
        /// </summary>
        /// <param name="_measurementUnit"></param>
        /// <returns></returns>
        public static float _convertUnitToPoint(_measurementUnitType _measurementUnit)
        {
            float __valueDiv = 1;
            switch (_measurementUnit)
            {
                case _measurementUnitType.Points: __valueDiv = 1;
                    break;
                case _measurementUnitType.Inches: __valueDiv = 1.0f / 72.0f;
                    break;
                case _measurementUnitType.Centimeters: __valueDiv = 1.0f / 28.35f;
                    break;
                case _measurementUnitType.Picas: __valueDiv = 1.0f / 12.0f;
                    break;
                case _measurementUnitType.Millimeters: __valueDiv = 1.0f / 2.835f;
                    break;
            }
            return __valueDiv;
        }

        public static float _convertPixelToUnit(_measurementUnitType _measurementUnit, int pixel, float scale)
        {
            float __result = 0;
            __result = (float)-(Math.Round(((pixel / _ratioPointPixel) * _convertUnitToPoint(_measurementUnit)) / scale, 2));
            return __result;
        }

        void _calcUnitRatio(_marginType mode)
        {
            float __valueDiv = _convertUnitToPoint(_measurementUnit);
            if (mode == _marginType.All || mode == _marginType.Width) this._paperWidth.textBox.Text = Math.Round(_valuePaperWidth * __valueDiv, (_measurementUnit == _measurementUnitType.Millimeters) ? 0 : 2).ToString();
            if (mode == _marginType.All || mode == _marginType.Height) this._paperHeight.textBox.Text = Math.Round(_valuePaperHeight * __valueDiv, (_measurementUnit == _measurementUnitType.Millimeters) ? 0 : 2).ToString();
            if (mode == _marginType.All || mode == _marginType.MarginTop) this._marginTop.textBox.Text = Math.Round(_valueMarginTop * __valueDiv, (_measurementUnit == _measurementUnitType.Millimeters) ? 0 : 2).ToString();
            if (mode == _marginType.All || mode == _marginType.MarginBottom) this._marginBottom.textBox.Text = Math.Round(_valueMarginBottom * __valueDiv, (_measurementUnit == _measurementUnitType.Millimeters) ? 0 : 2).ToString();
            if (mode == _marginType.All || mode == _marginType.MarginLeft) this._marginLeft.textBox.Text = Math.Round(_valueMarginLeft * __valueDiv, (_measurementUnit == _measurementUnitType.Millimeters) ? 0 : 2).ToString();
            if (mode == _marginType.All || mode == _marginType.MarginRight) this._marginRight.textBox.Text = Math.Round(_valueMarginRight * __valueDiv, (_measurementUnit == _measurementUnitType.Millimeters) ? 0 : 2).ToString();
        }

        public _measurementUnitType MeasurementUnit
        {
            get
            {
                return _measurementUnit;
            }
            set
            {
                _measurementUnit = value;
                this._measurementUnitComboBox.SelectedItem = _measurementUnit.ToString();
                _calcUnitRatio(_marginType.All);
            }
        }

        public float PaperWidth
        {
            get
            {
                return _valuePaperWidth;
            }
            set
            {
                _valuePaperWidth = value;
                _calcUnitRatio(_marginType.Width);
            }
        }

        public float PaperHeight
        {
            get
            {
                return _valuePaperHeight;
            }
            set
            {
                _valuePaperHeight = value;
                _calcUnitRatio(_marginType.Height);
            }
        }

        public float PaperDrawWidthPixel
        {
            get
            {
                return (this.PaperWidth - (this.MarginLeft + this.MarginRight)) * _ratioPointPixel;
            }
        }

        public float PaperDrawHeightPixel
        {
            get
            {
                return (this.PaperHeight - (this.MarginTop + this.MarginBottom)) * _ratioPointPixel;
            }
        }

        public float MarginTopPixel
        {
            get
            {
                return _valueMarginTop * _ratioPointPixel;
            }
        }

        public float MarginTop
        {
            get
            {
                return _valueMarginTop;
            }
            set
            {
                _valueMarginTop = value;
                _calcUnitRatio(_marginType.MarginTop);
            }
        }

        public bool _autoPrinterPaperSize
        {
            get
            {
                return _printerPaperSizeCheckbox.Checked;
            }
            set
            {
                _printerPaperSizeCheckbox.Checked = value;
            }
        }

        public float MarginBottom
        {
            get
            {
                return _valueMarginBottom;
            }
            set
            {
                _valueMarginBottom = value;
                _calcUnitRatio(_marginType.MarginBottom);
            }
        }

        public float MarginLeftPixel
        {
            get
            {
                return _valueMarginLeft * _ratioPointPixel;
            }
        }

        public float MarginLeft
        {
            get
            {
                return _valueMarginLeft;
            }
            set
            {
                _valueMarginLeft = value;
                _calcUnitRatio(_marginType.MarginLeft);
            }
        }

        public float MarginRight
        {
            get
            {
                return this._valueMarginRight;
            }
            set
            {
                this._valueMarginRight = value;
                _calcUnitRatio(_marginType.MarginRight);
            }
        }

        public _orientationType Orientation
        {
            get
            {
                return this._valueOrientation;
            }
            set
            {
                this._valueOrientation = value;
            }
        }

        public _pageSizeType PaperSize
        {
            get
            {
                return this._valuePaperSize;
            }
            set
            {
                this._valuePaperSize = value;
                this._paperSizeComboBox.SelectedItem = value.ToString();
            }
        }

        /// <summary>
        /// ความสูงของกระดาษ Hundredth of inch
        /// </summary>
        public float _getPageHeightHOI
        {
            get
            {
                return _valuePaperHeight * (100 / 72f);
            }
        }

        /// <summary>
        /// ความกว้างของกระดาษ ความสูงของกระดาษ Hundredth of inch
        /// </summary>
        public float _getPageWidthHOI
        {
            get
            {
                return _valuePaperWidth * (100/ 72f);
            }
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void _myNumberBoxUpDown5_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            _valueOrientation = _orientationType.Portrait;
            _getDefaultPaperSize(_valuePaperSize, _valueOrientation);
            _calcUnitRatio(_marginType.All);
            _orientationSelected();
        }

        private void _buttonLandscape_Click(object sender, EventArgs e)
        {
            _valueOrientation = _orientationType.Landscape;
            _getDefaultPaperSize(_valuePaperSize, _valueOrientation);
            _calcUnitRatio(_marginType.All);
            _orientationSelected();
        }

        void _orientationSelected()
        {
            _buttonPortrait.BackColor = (_valueOrientation == _orientationType.Portrait) ? Color.LightCyan : Color.White;
            _buttonLandscape.BackColor = (_valueOrientation == _orientationType.Landscape) ? Color.LightCyan : Color.White;
        }

        private void _buttonOk_Click(object sender, EventArgs e)
        {
            float __valueDiv = _convertUnitToPoint(_measurementUnit);
            this.PaperWidth = (float)Double.Parse(this._paperWidth.textBox.Text) / __valueDiv;
            this.PaperHeight = (float)Double.Parse(this._paperHeight.textBox.Text) / __valueDiv;
            this.MarginTop = (float)Double.Parse(this._marginTop.textBox.Text) / __valueDiv;
            this.MarginBottom = (float)Double.Parse(this._marginBottom.textBox.Text) / __valueDiv;
            this.MarginLeft = (float)Double.Parse(this._marginLeft.textBox.Text) / __valueDiv;
            this.MarginRight = (float)Double.Parse(this._marginRight.textBox.Text) / __valueDiv;
            this.Close();
        }

        private void _pageSetup_Load(object sender, EventArgs e)
        {

        }

        private void _buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}