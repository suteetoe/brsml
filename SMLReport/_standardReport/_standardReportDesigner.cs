using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLReport._standardReport
{
    public partial class _standardReportDesigner : UserControl
    {
        private _design._pageSetup _myPageSetup = new SMLReport._design._pageSetup();
        private ArrayList _mySection = new ArrayList();
        public _standardReportDesigner()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this.toolStrip2.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UseTextForAccessibility, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.CacheText, true);
            //
            this.splitContainer1.Panel1.SizeChanged += new EventHandler(Panel1_SizeChanged);
            this._topRuler._unit = _myPageSetup.Unit; ;
            this._topRuler.Invalidate();
            _vScrollBar.Visible = false;
            _hScrollBar.Visible = false;
            for (int __loop = 0; __loop <  5; __loop++)
            {
                _sectionType __sectionType = new _sectionType();
                switch (__loop)
                {
                    case 0: __sectionType = _sectionType.Header; break;
                    case 1: __sectionType = _sectionType.PageHeader; break;
                    case 2: __sectionType = _sectionType.Detail; break;
                    case 3: __sectionType = _sectionType.PageFooter; break;
                    case 4: __sectionType = _sectionType.Footer; break;
                }
                _sectionObject __createControl = new _sectionObject(_getNewName(__sectionType));
                __createControl.PageSetup = _myPageSetup;
                __createControl.Dock = DockStyle.Top;
                __createControl.RulerUnit = _myPageSetup.Unit;
                __createControl._draw.AllowDrop = true;
                __createControl._draw._afterClick += new SMLReport._design.AfterClickEventHandler(_draw__afterClick);
                __createControl._draw._afterClickLostFocus += new SMLReport._design.AfterClickLostFocusEventHandler(_draw__afterClickLostFocus);
                //__createControl._draw.MouseDown += new MouseEventHandler(_draw_MouseDown);
                this._drawArea.Controls.Add(__createControl);
                __createControl.BringToFront();
                _mySection.Add(__createControl);
            }
            _toolStripFontSizeComboBox.SelectedIndex = 3;
        }

        void _draw__afterClickLostFocus(object sender)
        {
            _unSelectAll(sender);
        }

        /// <summary>
        /// หลังจากสร้าง Object ให้ยกเลิกการเลือกทุก Section ต่อไป
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="sender"></param>
        void _draw__afterClick(object sender, object[] senderObject)
        {
            for (int __loop = 0; __loop < _mySection.Count; __loop++)
            {
                _sectionObject __getControl = (_sectionObject)_mySection[__loop];
                __getControl._draw._activeTool = _design._drawToolType.Pointer;
            }
            _unSelectAll(sender);
        }

        void _unSelectAll(object sender)
        {
            for (int __loop = 0; __loop < _mySection.Count; __loop++)
            {
                _sectionObject __getControl = (_sectionObject)_mySection[__loop];
                if (!((_sectionObject)sender).MyName.Equals(__getControl.MyName))
                {
                    __getControl._draw._graphicsList._unselectAll();
                    __getControl._draw.Invalidate();
                }
            }
        }

        string _getNewName(_sectionType sectionType)
        {
            string __result = sectionType.ToString();
            return __result;
        }

        void Panel1_SizeChanged(object sender, EventArgs e)
        {
            _topRuler.Location = new Point(18, 0);
            _topRuler.Width = this.splitContainer1.Panel1.Width - 18;
            //
            _topRuler.Invalidate();
            _drawArea.Location = new Point(0, 18);
            if (this._drawArea.Width < this.splitContainer1.Panel1.Width)
            {
                this._drawArea.Width = this.splitContainer1.Panel1.Width;
            }
            if (this._drawArea.Height < this.splitContainer1.Panel1.Height - 18)
            {
                this._drawArea.Height = this.splitContainer1.Panel1.Height- 18;
            }
            _drawArea.Invalidate();
        }

        private void _buttonPageSetup_Click(object sender, EventArgs e)
        {
            _myPageSetup.ShowDialog(this);
            for (int __loop = 0; __loop < _mySection.Count; __loop++)
            {
                _sectionObject __getControl = (_sectionObject)_mySection[__loop];
                __getControl.RulerUnit = _myPageSetup.Unit;
                __getControl.Invalidate();
                this._topRuler._unit = _myPageSetup.Unit; ;
                this._topRuler.Invalidate();
            }
        }

        void _activeTool(_design._drawToolType activeType)
        {
            for (int __loop = 0; __loop < _mySection.Count; __loop++)
            {
                _sectionObject __getControl = (_sectionObject)_mySection[__loop];
                __getControl._draw._activeTool = activeType;
            }
        }

        private void _buttonAddLabel_Click(object sender, EventArgs e)
        {
            _activeTool(_design._drawToolType.Label);
        }

        private void _buttonAddRectangle_Click(object sender, EventArgs e)
        {
            _activeTool(_design._drawToolType.Rectangle);
        }
    }
}
