using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLReport._standardReport
{
    public enum _sectionType
    {
        Header,
        PageHeader,
        Group,
        Detail,
        PageFooter,
        Footer
    }

    public partial class _sectionObject : UserControl
    {
        //private _sectionType _thisType = _sectionType.Detail;
        private bool _resizeMode = false;
        private _design._pageSetup _myPageSetup;
        private string _myName;

        public _sectionObject(string  sectionName)
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            MyName = _label.Text = sectionName;
            this.MouseMove += new MouseEventHandler(_sectionObject_MouseMove);
            this.MouseDown += new MouseEventHandler(_sectionObject_MouseDown);
            this.MouseUp += new MouseEventHandler(_sectionObject_MouseUp);
            this._drawPanel.Location = new Point(this._rulerControl.Width, this._toolBar.Height + 1);
            this._draw.Location = new Point(0, 0);
            this.SizeChanged += new EventHandler(_sectionObject_SizeChanged);
            this.MouseLeave += new EventHandler(_sectionObject_MouseLeave);
            //
            /*_myGraphicsList = new _design._graphicsList();
            // create array of drawing tools
            tools = new _tool[(int)_design._drawToolType.NumberOfDrawTools];
            tools[(int)_design._drawToolType.Pointer] = new _design._toolPointer();
            tools[(int)_design._drawToolType.Rectangle] = new _design._toolRectangle();
            tools[(int)_design._drawToolType.Ellipse] = new _design._toolEllipse();
            tools[(int)_design._drawToolType.Line] = new _design._toolLine();
            tools[(int)_design._drawToolType.Label] = new _design._toolLabel();
            tools[(int)_design._drawToolType.Picture] = new _design._toolImage();*/
        }

        /// <summary>
        /// เปลี่ยนหน่วยของไม้บรรทัด
        /// </summary>
        public _design._measurementUnitType RulerUnit
        {
            get
            {
                return this._rulerControl._unit;
            }
            set
            {
                this._rulerControl._unit = value;
                this._rulerControl.Invalidate();
            }
        }

        public string MyName
        {
            get
            {
                return _myName;
            }
            set
            {
                _myName = value;
            }
        }

        public _design._pageSetup PageSetup
        {
            get
            {
                return _myPageSetup;
            }
            set
            {
                _myPageSetup = value;
            }
        }

        void _sectionObject_MouseLeave(object sender, EventArgs e)
        {
            if (this.Cursor == Cursors.SizeNS)
            {
                this.Cursor = Cursors.Default;
                _resizeMode = false;
            }
        }

        void _sectionObject_SizeChanged(object sender, EventArgs e)
        {
            Size __getPaperSize = _myPageSetup.PagePixel;
            this._drawPanel.Size = new Size(__getPaperSize.Width, _rulerControl.Height - 3);
            this._draw.Size = new Size(__getPaperSize.Width, _rulerControl.Height - 3);
        }

        void _sectionObject_MouseUp(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Default;
            _resizeMode = false;
        }

        void _sectionObject_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y >= this.Height - 2)
            {
                this.Cursor = Cursors.SizeNS;
                _resizeMode = true;
            }
        }

        void _sectionObject_MouseMove(object sender, MouseEventArgs e)
        {
            if (_resizeMode)
            {
                if (e.Y > this._toolBar.Height + 3)
                {
                    this.Size = new Size(this.Width, e.Y);
                }
            }
            else
            {
                if (e.Y >= this.Height - 2)
                {
                    this.Cursor = Cursors.SizeNS;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }
    }
}
