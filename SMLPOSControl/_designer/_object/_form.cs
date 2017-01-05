using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SMLReport._design;

namespace SMLPOSControl._designer._object
{
    public partial class _form : Form
    {
        #region hidden property
        
        // hidden window style
        [Browsable(false)]
        public new bool ControlBox { get { return base.ControlBox; } set { base.ControlBox = value; } }

        [Browsable(false)]
        public new bool HelpButton { get { return base.HelpButton; } set { base.HelpButton = value; } }

        [Browsable(false)]
        public new bool IsMdiContainer { get { return base.IsMdiContainer; } set { base.IsMdiContainer = value; } }

        [Browsable(false)]
        public new MenuStrip MainMenuStrip { get { return base.MainMenuStrip; } set { base.MainMenuStrip = value; } }

        [Browsable(false)]
        public new bool MinimizeBox { get { return base.MinimizeBox; } set { base.MinimizeBox = value; } }

        [Browsable(false)]
        public new bool MaximizeBox { get { return base.MaximizeBox; } set { base.MaximizeBox = value; } }

        [Browsable(false)]
        public new double Opacity { get { return base.Opacity; } set { base.Opacity = value; } }

        [Browsable(false)]
        public new bool ShowIcon { get { return base.ShowIcon; } set { base.ShowIcon = value; } }

        [Browsable(false)]
        public new bool ShowInTaskbar { get { return base.ShowInTaskbar; } set { base.ShowInTaskbar = value; } }

        [Browsable(false)]
        public new SizeGripStyle SizeGripStyle { get { return base.SizeGripStyle; } set { base.SizeGripStyle = value; } }

        [Browsable(false)]
        public new bool TopMost { get { return base.TopMost; } set { base.TopMost = value; } }

        [Browsable(false)]
        public new Color TransparencyKey { get { return base.TransparencyKey; } set { base.TransparencyKey = value; } }

        // end hidden window style

        // hidden misc

        [Browsable(false)]
        public new IButtonControl CancelButton { get { return base.CancelButton; } set { base.CancelButton = value; } }

        [Browsable(false)]
        public new bool KeyPreview { get { return base.KeyPreview; } set { base.KeyPreview = value; } }

        // end hidden

        // hidden Data Property

        [Browsable(false)]
        public new ControlBindingsCollection DataBindings { get { return base.DataBindings; } }

        [Browsable(false)]
        public new object Tag { get { return base.Tag; } set { base.Tag = value; } }

        // end hidden Data Property

        // hidden Behavior Property

        [Browsable(false)]
        public new bool AllowDrop { get { return base.AllowDrop; } set { base.AllowDrop = value; } }

        [Browsable(false)]
        public new AutoValidate AutoValidate { get { return base.AutoValidate; } set { base.AutoValidate = value; } }

        [Browsable(false)]
        public new ContextMenuStrip ContextMenuStrip { get { return base.ContextMenuStrip; } set { base.ContextMenuStrip = value; } }

        [Browsable(false)]
        public new bool Enabled { get { return base.Enabled; } set { base.Enabled = value; } }

        [Browsable(false)]
        public new ImeMode ImeMode { get { return base.ImeMode; } set { base.ImeMode = value; } }

        [Browsable(false)]
        public new bool Visible { get { return base.Visible; } set { base.Visible = value; } }

        // end hidden Behavior Property

        // hidden Layout Property

        [Browsable(false)]
        public new AnchorStyles Anchor { get { return base.Anchor; } set { base.Anchor = value; } }

        [Browsable(false)]
        public new bool AutoScroll { get { return base.AutoScroll; } set { base.AutoScroll = value; } }

        [Browsable(false)]
        public new Size AutoScrollMargin { get { return base.AutoScrollMargin; } set { base.AutoScrollMargin = value; } }

        [Browsable(false)]
        public new Size AutoScrollMinSize { get { return base.AutoScrollMinSize; } set { base.AutoScrollMinSize = value; } }

        [Browsable(false)]
        public new bool AutoSize { get { return base.AutoSize; } set { base.AutoSize = value; } }

        [Browsable(false)]
        public new AutoSizeMode AutoSizeMode { get { return base.AutoSizeMode; } set { base.AutoSizeMode = value; } }

        [Browsable(false)]
        public new DockStyle Dock { get { return base.Dock; } set { base.Dock = value; } }

        [Browsable(false)]
        public new Point Location { get { return base.Location; } set { base.Location = value; } }

        [Browsable(false)]
        public new Size MaximumSize { get { return base.MaximumSize; } set { base.MaximumSize = value; } }

        [Browsable(false)]
        public new Size MinimumSize { get { return base.MinimumSize; } set { base.MinimumSize = value; } }

        [Browsable(false)]
        public new Padding Padding { get { return base.Padding; } set { base.Padding = value; } }

        [Browsable(false)]
        public new Size Size { get { return base.Size; } set { base.Size = value; } }

        [Browsable(false)]
        public new FormStartPosition StartPosition { get { return base.StartPosition; } set { base.StartPosition = value; } }

        [Browsable(false)]
        public new FormWindowState WindowState { get { return base.WindowState; } set { base.WindowState = value; } }

        // end hidden Layout Property

        // hidden design property

        [Browsable(false)]
        public new string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }

        // end hidden design property

        // hidden Appearance Property

        [Browsable(false)]
        public new Cursor Cursor { get { return base.Cursor; } set { base.Cursor = value; } }

        [Browsable(false)]
        public new Font Font { get { return base.Font; } set { base.Font = value; } }

        [Browsable(false)]
        public new Color ForeColor { get { return base.ForeColor; } set { base.ForeColor = value; } }

        [Browsable(false)]
        public new FormBorderStyle FormBorderStyle { get { return base.FormBorderStyle; } set { base.FormBorderStyle = value; } }

        [Browsable(false)]
        public new RightToLeft RightToLeft { get { return base.RightToLeft; } set { base.RightToLeft = value; } }

        [Browsable(false)]
        public new bool UseWaitCursor { get { return base.UseWaitCursor; } set { base.UseWaitCursor = value; } }

        [Browsable(false)]
        public new bool RightToLeftLayout { get { return base.RightToLeftLayout; } set { base.RightToLeftLayout = value; } }

        // end hidden Appearance Property

        // hidden Accessibility Property

        [Browsable(false)]
        public new string AccessibleDescription { get { return base.AccessibleDescription; } set { base.AccessibleDescription = value; } }

        [Browsable(false)]
        public new string AccessibleName { get { return base.AccessibleName; } set { base.AccessibleName = value; } }

        [Browsable(false)]
        public new AccessibleRole AccessibleRole { get { return base.AccessibleRole; } set { base.AccessibleRole = value; } }

        // end hidden Accessibility Property

        [Browsable(false)]
        public new IButtonControl AcceptButton { get { return base.AcceptButton; } set { base.AcceptButton = value; } }

        [Browsable(false)]
        public new AutoScaleMode AutoScaleMode { get { return base.AutoScaleMode; } set { base.AutoScaleMode = value; } }

        [Browsable(false)]
        public new bool CausesValidation { get { return base.CausesValidation; } set { base.CausesValidation = value; } }
        

        public new Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                base.BackColor = value;                
                //_formlayout.BackColor = value;
            }
        }

        public new Image BackgroundImage
        {
            get { return base.BackgroundImage; }
            set
            {
                base.BackgroundImage = value;
                //_formlayout.BackgroundImage = value;
            }
        }

        #endregion

        public _form()
        {
            InitializeComponent();
            _drawPanel.Width = this.Width;
            _drawPanel.Height = this.Height;

            _drawPanel._afterPaint += new AfterPaintFocusEventHandler(_drawPanel__afterPaint);
        }

        void _drawPanel__afterPaint(Graphics sender)
        {
            _checkOverDrawPanel();
            _checkTrimDrawPanel();
        }

        public void _checkTrimDrawPanel()
        {
            bool __overWidth = false;
            bool __overHeight = false;

            for (int __loop = 0; __loop < _drawPanel._graphicsList._count; __loop++)
            {
                _drawObject __getObject = (_drawObject)_drawPanel._graphicsList[__loop];

                int _leftX = __getObject._getHandle(1).X;
                int _rightX = __getObject._getHandle(3).X;
                int _topY = __getObject._getHandle(1).Y;
                int _bottomY = __getObject._getHandle(7).Y;


                if (__getObject._getHandle(1).X <= -3)
                {
                    // over Left
                }
                if (__getObject._getHandle(3).X > this.Width + 3)
                {
                    // over right
                    __overWidth = true;
                }
                if (__getObject._getHandle(1).Y <= -3)
                {
                    // over Top
                }
                if (__getObject._getHandle(7).Y > this.Height + 3)
                {
                    // over bottom
                    __overHeight = true;
                }
                //this.Invalidate();
            }

            if (__overHeight == false)
                _drawPanel.Height = this.Height;
            if (__overWidth == false)
                _drawPanel.Width = this.Width;

        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (_drawPanel != null)
            {
                // renew size
                _drawPanel.Dock = DockStyle.None;
                _drawPanel.Width = this.Width;
                _drawPanel.Height = this.Height;

                // calc over object

                _checkOverDrawPanel();
            }
            base.OnSizeChanged(e);

        }

        public void _checkOverDrawPanel()
        {
            for (int __loop = 0; __loop < _drawPanel._graphicsList._count; __loop++)
            {
                _drawObject __getObject = (_drawObject)_drawPanel._graphicsList[__loop];

                int _leftX = __getObject._getHandle(1).X;
                int _rightX = __getObject._getHandle(3).X;
                int _topY = __getObject._getHandle(1).Y;
                int _bottomY = __getObject._getHandle(7).Y;


                if (__getObject._getHandle(1).X <= -3)
                {
                    // over Left
                }
                if (__getObject._getHandle(3).X > this.Width + 3)
                {
                    // over right
                    int __width = __getObject._getHandle(3).X;
                    _drawPanel.Width = __width;
                }
                if (__getObject._getHandle(1).Y <= -3)
                {
                    // over Top
                }
                if (__getObject._getHandle(7).Y > this.Height + 3)
                {
                    // over bottom
                    int __height = __getObject._getHandle(7).Y;
                    _drawPanel.Height = __height;
                }
                //this.Invalidate();
            }

        }
    }
}
