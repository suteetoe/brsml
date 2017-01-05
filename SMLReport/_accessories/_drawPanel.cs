using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SMLReport._design
{
    public partial class _drawPanel : UserControl
    {
        public _graphicsList _graphicsList = new _graphicsList();    // list of draw objects
        public _graphicsList _graphicsListClone = new _graphicsList();    // list of draw objects
        private _drawToolType _activeToolResult = _drawToolType.Pointer;      // active drawing tool
        private _tool[] tools;                 // array of tools
        private Rectangle _netRectangleResult;
        private bool _drawNetRectangleResult = false;
        private float _drawScaleResult = 1f;
        private Point _mouseDownPositionResult;

        private bool _showHighlightTextResult = false;

        // lock drawpanel on load form
        public bool _onPanelLoaded = false;
        public object _ownerForm;

        public string _specialText = "";

        public _drawPanel()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UseTextForAccessibility, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.CacheText, true);

            this._addEvent();
            //
            // create array of drawing tools
            tools = new _tool[(int)_drawToolType.NumberOfDrawTools];
            tools[(int)_drawToolType.Pointer] = new _toolPointer();
            tools[(int)_drawToolType.Rectangle] = new _toolRectangle();
            tools[(int)_drawToolType.Ellipse] = new _toolEllipse();
            tools[(int)_drawToolType.Line] = new _toolLine();
            tools[(int)_drawToolType.Label] = new _toolLabel();
            tools[(int)_drawToolType.TextField] = new _toolTextField();
            tools[(int)_drawToolType.ImageField] = new _toolImageField();
            tools[(int)_drawToolType.RoundedRectangle] = new _toolRoundedRectangle();
            tools[(int)_drawToolType.Picture] = new _toolImage();
            tools[(int)_drawToolType.Table] = new _toolTable();
            tools[(int)_drawToolType.SpecialLabel] = new _toolLabel(true);
            tools[(int)_drawToolType.SpecialImageField] = new _toolImageField(true);

            this.Disposed += _drawPanel_Disposed;
        }

        private void _drawPanel_Disposed(object sender, EventArgs e)
        {
            for (int __loop = this._graphicsList._count -1; __loop >= 0; __loop--)
            {
                ((_drawObject)this._graphicsList[__loop]).Dispose();               
            }
        }

        public void _addEvent()
        {
            this._drawNetRectangle = false;
            //
            this.Paint -= new PaintEventHandler(_designReportPanel_Paint);
            this.MouseMove -= new MouseEventHandler(_designReportPanel_MouseMove);
            this.MouseUp -= new MouseEventHandler(_designReportPanel_MouseUp);
            this.MouseDown -= new MouseEventHandler(_designReportPanel_MouseDown);
            this.DragDrop -= new DragEventHandler(_drawPanel_DragDrop);
            this.DragOver -= new DragEventHandler(_drawPanel_DragOver);
            //
            this.Paint += new PaintEventHandler(_designReportPanel_Paint);
            this.MouseMove += new MouseEventHandler(_designReportPanel_MouseMove);
            this.MouseUp += new MouseEventHandler(_designReportPanel_MouseUp);
            this.MouseDown += new MouseEventHandler(_designReportPanel_MouseDown);
            this.DragDrop += new DragEventHandler(_drawPanel_DragDrop);
            this.DragOver += new DragEventHandler(_drawPanel_DragOver);
        }



        void _drawPanel_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        void _drawPanel_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(_drawPanel).ToString()))
            {
                _drawPanel __draw = (_drawPanel)e.Data.GetData(typeof(_drawPanel));

                // Read source list in reverse order, add every selected item
                // to temporary list and remove it from source list
                _graphicsList __temp = new _graphicsList();
                for (int __loop = 0; __loop < __draw._graphicsList._count; __loop++)
                {
                    if (((_drawObject)__draw._graphicsList[__loop])._selected)
                    {
                        __temp._add(__draw._graphicsList[__loop]);
                    }
                }
                __draw._graphicsList._deleteSelection();
                //
                for (int __loop = 0; __loop < __temp._count; __loop++)
                {
                    Point __getOldPosition = __temp[__loop]._getHandle(1);
                    Point __mousePosition = this.PointToClient(Control.MousePosition);
                    __temp[__loop]._moveToPoint(new Point((__mousePosition.X - __draw._mouseDownPosition.X) + __getOldPosition.X, (__mousePosition.Y - __draw._mouseDownPosition.Y) + __getOldPosition.Y));
                    this._graphicsList._add(__temp[__loop]);
                }
                __draw.Invalidate();
                this.Invalidate();
            }
        }

        public Point _mouseDownPosition
        {
            get
            {
                return _mouseDownPositionResult;
            }
        }

        public float _drawScale
        {
            get
            {
                return _drawScaleResult;
            }
            set
            {
                _drawScaleResult = value;
            }
        }

        public bool _drawNetRectangle
        {
            get
            {
                return _drawNetRectangleResult;
            }
            set
            {
                _drawNetRectangleResult = value;
            }
        }

        public Rectangle _netRectangle
        {
            get
            {
                return _netRectangleResult;
            }
            set
            {
                _netRectangleResult = value;
            }
        }

        public bool _showHighlightTextField
        {
            get
            {
                return _showHighlightTextResult;
            }
            set
            {
                _showHighlightTextResult = value;
            }
        }

        public _drawToolType _activeTool
        {
            get
            {
                return _activeToolResult;
            }
            set
            {
                _activeToolResult = value;
            }
        }

        void _designReportPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (!_onPanelLoaded)
            {
                if (e.Button == MouseButtons.Left)
                {
                    _mouseDownPositionResult = this.PointToClient(Control.MousePosition);
                    //tools[(int)_activeToolResult].OnMouseDown(this, e);

                    if (_activeToolResult == _drawToolType.SpecialLabel)
                    {
                        ((_toolLabel)tools[(int)_activeToolResult])._specialText = _specialText;
                        ((_toolLabel)tools[(int)_activeToolResult]).OnMouseDown(this, e);
                    }
                    else if (_activeToolResult == _drawToolType.SpecialImageField)
                    {
                        ((_toolImageField)tools[(int)_activeToolResult])._specialText = _specialText;
                        ((_toolImageField)tools[(int)_activeToolResult]).OnMouseDown(this, e);
                    }
                    else
                    {
                        tools[(int)_activeToolResult].OnMouseDown(this, e);
                    }

                    if (_activeToolResult != _drawToolType.Pointer)
                    {
                        if (_afterAddDrawObject != null)
                        {
                            _afterAddDrawObject(sender);
                        }
                    }
                }
            }
        }

        // Toe เพิ่ม ให้ pos manager ให้ overide ได้
        protected virtual void _onAfterClickDrawPanel(Object sender, object[] __selectedObject)
        {
            if (_afterClick != null)
            {
                _afterClick(this.Parent.Parent, __selectedObject);
            }
        }

        void _designReportPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                tools[(int)_activeToolResult].OnMouseUp(this, e);
            }
            Point point = new Point(e.X, e.Y);
            int n = _graphicsList._count;
            _drawObject o = null;
            ArrayList _objects = new ArrayList();

            for (int i = 0; i < n; i++)
            {
                if (((_drawObject)_graphicsList[i])._selected)
                {
                    o = _graphicsList[i];
                    _objects.Add(o);
                }
            }

            if (o != null)
            {
                //if (_afterClick != null)
                //{
                //    _afterClick(this.Parent.Parent, (object[])_objects.ToArray());
                //}
                _onAfterClickDrawPanel(this, (object[])_objects.ToArray());
            }
            else
            {
                //if (_afterClick != null)
                //{
                //    _afterClick(this.Parent.Parent, null);
                //}
                _onAfterClickDrawPanel(this, null);


                if (_afterClickLostFocus != null)
                {
                    _afterClickLostFocus(this.Parent.Parent);
                }
            }
            // ปรับตำแหน่งไม่ให้เกิน
            for (int __loop = 0; __loop < _graphicsList._count; __loop++)
            {
                _drawObject __getObject = (_drawObject)_graphicsList[__loop];
                if (__getObject._getHandle(1).X <= -3)
                {
                    // over Left                    
                    _onDrawObjectOverLeftPanel(this, __getObject);
                }
                if (__getObject._getHandle(3).X > this.Width + 3)
                {
                    _onDrawObjectOverRightPanel(this, __getObject);
                }
                if (__getObject._getHandle(1).Y <= -3)
                {
                    // over Top                    
                    _onDrawObjectOverTopPanel(this, __getObject);
                }
                if (__getObject._getHandle(7).Y > this.Height + 3)
                {
                    _onDrawObjectOverBottomPanel(this, __getObject);
                }
                this.Invalidate();
            }
        }

        protected virtual void _onDrawObjectOverLeftPanel(Object __sender, _drawObject __objOver)
        {
            _drawObject __getObject = __objOver as _drawObject;
            __getObject._moveToPoint(new Point(10, __getObject._getHandle(1).Y));
        }

        protected virtual void _onDrawObjectOverRightPanel(Object __sender, _drawObject __objOver)
        {
            _drawObject __getObject = __objOver as _drawObject;
            __getObject._moveToPoint(new Point(this.Width - (__getObject._width + 10), __getObject._getHandle(3).Y));
        }

        protected virtual void _onDrawObjectOverTopPanel(Object __sender, _drawObject __objOver)
        {
            _drawObject __getObject = __objOver as _drawObject;
            __getObject._moveToPoint(new Point(__getObject._getHandle(1).X, 10));
        }

        protected virtual void _onDrawObjectOverBottomPanel(Object __sender, _drawObject __objOver)
        {
            _drawObject __getObject = __objOver as _drawObject;
            __getObject._moveToPoint(new Point(__getObject._getHandle(7).X, this.Height - (__getObject._height + 10)));
        }

        void _designReportPanel_MouseMove(object sender, MouseEventArgs e)
        {
            Point __getPoint = this.PointToClient(Control.MousePosition);
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.None)
            {
                tools[(int)_activeToolResult].OnMouseMove(this, e);
            }
            else
                this.Cursor = Cursors.Default;

            if (_mouseOverDrawPanel != null)
            {
                _mouseOverDrawPanel(this, e);
            }
        }

        void _designReportPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.PageUnit = GraphicsUnit.Pixel;

            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            if (_afterPaint != null)
            {
                _afterPaint(g);
            }
            if (_graphicsList != null)
            {
                _graphicsList._draw(g, _drawScaleResult);
                if (_showHighlightTextResult)
                {
                    _graphicsList._drawObjectHighlight(g, _drawScaleResult);
                }
            }
            _drawNetSelection(g);
        }

        /// <summary>
        ///  Draw group selection rectangle
        /// </summary>
        /// <param name="g"></param>
        public void _drawNetSelection(Graphics g)
        {
            if (_drawNetRectangle == false)
                return;
            ControlPaint.DrawFocusRectangle(g, _netRectangle, Color.Black, Color.Transparent);
        }
        //
        public object _clone()
        {
            this._graphicsListClone._clear();
            for (int __loop = 0; __loop < this._graphicsList._count; __loop++)
            {
                this._graphicsListClone._add((SMLReport._design._drawObject)((SMLReport._design._drawObject)this._graphicsList[__loop])._clone());
            }
            return this.MemberwiseClone();
        }
        //
        public event AfterPaintFocusEventHandler _afterPaint;
        public event AfterClickEventHandler _afterClick;
        public event AfterClickLostFocusEventHandler _afterClickLostFocus;
        public event MouseOverDrawPanelEventHandler _mouseOverDrawPanel;
        public event addDrawObject _afterAddDrawObject;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                    this._graphicsList._movePosition(_design._graphicsList._moveEnum.Up, -1);
                    this.Invalidate();
                    return true;
                case Keys.Down:
                    this._graphicsList._movePosition(_design._graphicsList._moveEnum.Down, 1);
                    this.Invalidate();
                    return true;
                case Keys.Left:
                    this._graphicsList._movePosition(_design._graphicsList._moveEnum.Left, -1);
                    this.Invalidate();
                    return true;
                case Keys.Right:
                    this._graphicsList._movePosition(_design._graphicsList._moveEnum.Right, 1);
                    this.Invalidate();
                    return true;
                case Keys.Control | Keys.Up:
                    this._graphicsList._movePosition(_design._graphicsList._moveEnum.Up, -10);
                    this.Invalidate();
                    return true;
                case Keys.Control | Keys.Down:
                    this._graphicsList._movePosition(_design._graphicsList._moveEnum.Down, 10);
                    this.Invalidate();
                    return true;
                case Keys.Control | Keys.Left:
                    this._graphicsList._movePosition(_design._graphicsList._moveEnum.Left, -10);
                    this.Invalidate();
                    return true;
                case Keys.Control | Keys.Right:
                    this._graphicsList._movePosition(_design._graphicsList._moveEnum.Right, 10);
                    this.Invalidate();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
    public delegate void AfterClickEventHandler(object sender, object[] senderObject);
    public delegate void AfterClickLostFocusEventHandler(object sender);
    public delegate void AfterPaintFocusEventHandler(Graphics sender);
    public delegate void MouseOverDrawPanelEventHandler(object sender, MouseEventArgs e);
    public delegate void addDrawObject(object sender);

}
