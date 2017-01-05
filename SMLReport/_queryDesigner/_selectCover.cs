using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLReport._design
{
    public partial class _selectCover : UserControl
    {
        public Control _converObject = null;
        Size _oldSize;
        Size _oldObjectSize;
        Point _fixedLocation;
        Point _fixedObjectLocation;
        Point _oldLocation;
        Point _oldParentLocation;
        Point _oldObjectLocation;
        Point[] _coverPin = { new Point(0, 0), new Point(0, 0), new Point(0, 0), new Point(0, 0), new Point(0, 0), new Point(0, 0), new Point(0, 0), new Point(0, 0) };
        public bool _controlMoveMode = false;
        bool _controlResizeMode = false;
        int _pinSelectID = 0;
        //
        public _selectCover(Control sender)
        {
            InitializeComponent();
            _converObject = sender;
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.AllowDrop = false;
            this.MouseDown += new MouseEventHandler(_selectCover_MouseDown);
            this.MouseUp += new MouseEventHandler(_selectCover_MouseUp);
            this.Paint += new PaintEventHandler(_selectCover_Paint);
            this.MouseMove += new MouseEventHandler(_selectCover_MouseMove);
            _refresh();
        }

        int _getPin(Point mousePoint)
        {
            if (_controlResizeMode)
            {
                return (_pinSelectID);
            }
            int __result = -1;
            for (int __loop = 0; __loop < 8; __loop++)
            {
                if (mousePoint.X >= _coverPin[__loop].X - 3 && mousePoint.X <= _coverPin[__loop].X + 3 &&
                    mousePoint.Y >= _coverPin[__loop].Y - 3 && mousePoint.Y <= _coverPin[__loop].Y + 3)
                {
                    switch (__loop)
                    {
                        case 0: this.Cursor = Cursors.SizeNWSE; break;
                        case 1: this.Cursor = Cursors.SizeNS; break;
                        case 2: this.Cursor = Cursors.SizeNESW; break;
                        case 3: this.Cursor = Cursors.SizeWE; break;
                        case 4: this.Cursor = Cursors.SizeNWSE; break;
                        case 5: this.Cursor = Cursors.SizeNS; break;
                        case 6: this.Cursor = Cursors.SizeNESW; break;
                        case 7: this.Cursor = Cursors.SizeWE; break;
                    }
                    __result = __loop;
                    break;
                }
            }
            return __result;
        }

        public void _selectCover_MouseDown(object sender, MouseEventArgs e)
        {
            _fixedLocation = this.Location;
            _fixedObjectLocation = this._converObject.Location;
            _oldSize = this.Size;
            _oldObjectSize = this._converObject.Size;
            _oldLocation = this.PointToClient(Control.MousePosition);
            _oldParentLocation = this.Parent.PointToClient(Control.MousePosition);
            _oldObjectLocation = _converObject.PointToClient(Control.MousePosition);
            _pinSelectID = _getPin(new Point(e.X, e.Y));
            if (_pinSelectID == -1)
            {
                _controlMoveMode = true;
                _controlResizeMode = false;
            }
            else
            {
                _controlMoveMode = false;
                _controlResizeMode = true;
            }
        }

        public void _selectCover_MouseUp(object sender, MouseEventArgs e)
        {
            _controlMoveMode = false;
            _controlResizeMode = false;
            this._refresh();
            this._converObject.Invalidate();
            this._converObject.BringToFront();
        }

        void _selectCover_MouseMove(object sender, MouseEventArgs e)
        {
            Control __getControl = (Control)sender;
            if (_controlResizeMode)
            {
                Point __newLocation = Parent.PointToClient(Control.MousePosition);
                Point __newPoint = new Point(__newLocation.X - _oldLocation.X, __newLocation.Y - _oldLocation.Y);
                Point __newPointObject = new Point(__newLocation.X - _oldObjectLocation.X, __newLocation.Y - _oldObjectLocation.Y);
                if (_pinSelectID == 0)
                {
                    // ปุ่มบนซ้าย
                    int __newHeight = _oldSize.Height + (_oldParentLocation.Y - __newLocation.Y);
                    int __newWidth = _oldSize.Width + (_oldParentLocation.X - __newLocation.X);
                    int __newObjectHeight = _oldObjectSize.Height + (_oldParentLocation.Y - __newLocation.Y);
                    int __newObjectWidth = _oldObjectSize.Width + (_oldParentLocation.X - __newLocation.X);
                    if (__newHeight > 20)
                    {
                        this._converObject.Location = __newPointObject;
                        this._converObject.Size = new Size(__newObjectWidth, __newObjectHeight);
                        this.Location = __newPoint;
                        this.Size = new Size(__newWidth, __newHeight);
                    }
                }
                else
                    if (_pinSelectID == 1)
                    {
                        // ปุ่มบนกลาง
                        __newPoint = new Point(_fixedLocation.X, __newLocation.Y - _oldLocation.Y);
                        __newPointObject = new Point(_fixedObjectLocation.X, __newLocation.Y - _oldObjectLocation.Y);
                        int __newHeight = _oldSize.Height + (_oldParentLocation.Y - __newLocation.Y);
                        int __newObjectHeight = _oldObjectSize.Height + (_oldParentLocation.Y - __newLocation.Y);
                        if (__newHeight > 20)
                        {
                            this._converObject.Location = __newPointObject;
                            this._converObject.Size = new Size(this._converObject.Width, __newObjectHeight);
                            this.Location = __newPoint;
                            this.Size = new Size(this.Width, __newHeight);
                        }
                    }
                    else
                        if (_pinSelectID == 2)
                        {
                            // ปุ่มบนขวา
                            int __newHeight = _oldSize.Height + (_oldParentLocation.Y - __newLocation.Y);
                            int __newWidth = _oldSize.Width + (__newLocation.X - _oldParentLocation.X);
                            //
                            int __newObjectHeight = _oldObjectSize.Height + (_oldParentLocation.Y - __newLocation.Y);
                            int __newObjectWidth = _oldObjectSize.Width + (__newLocation.X - _oldParentLocation.X);
                            //
                            __newPoint = new Point(_fixedLocation.X, _fixedLocation.Y - (__newHeight - _oldSize.Height));
                            __newPointObject = new Point(_fixedObjectLocation.X, _fixedObjectLocation.Y - (__newObjectHeight - _oldObjectSize.Height));
                            //
                            if (__newHeight > 20 && __newWidth > 20)
                            {
                                this._converObject.Location = __newPointObject;
                                this._converObject.Size = new Size(__newObjectWidth, __newObjectHeight);
                                //
                                this.Location = __newPoint;
                                this.Size = new Size(__newWidth, __newHeight);
                            }
                        }
                        else
                            if (_pinSelectID == 3)
                            {
                                // ปุ่มขวากลาง
                                int __newWidth = _oldSize.Width + (__newLocation.X - _oldParentLocation.X);
                                int __newObjectWidth = _oldObjectSize.Width + (__newLocation.X - _oldParentLocation.X);
                                //
                                if (__newWidth > 20)
                                {
                                    this._converObject.Size = new Size(__newObjectWidth, this._converObject.Height);
                                    this.Size = new Size(__newWidth, this.Height);
                                }
                            }
                            else
                                if (_pinSelectID == 4)
                                {
                                    // ปุ่มล่างขวา
                                    int __newHeight = _oldSize.Height + (__newLocation.Y - _oldParentLocation.Y);
                                    int __newWidth = _oldSize.Width + (__newLocation.X - _oldParentLocation.X);
                                    //
                                    int __newObjectHeight = _oldObjectSize.Height + (__newLocation.Y - _oldParentLocation.Y);
                                    int __newObjectWidth = _oldObjectSize.Width + (__newLocation.X - _oldParentLocation.X);
                                    //
                                    if (__newHeight > 20 && __newWidth > 20)
                                    {
                                        this._converObject.Size = new Size(__newObjectWidth, __newObjectHeight);
                                        //
                                        this.Size = new Size(__newWidth, __newHeight);
                                    }
                                }
                                else
                                    if (_pinSelectID == 5)
                                    {
                                        // ปุ่มลางกลาง
                                        int __newHeight = _oldSize.Height + (__newLocation.Y - _oldParentLocation.Y);
                                        int __newObjectHeight = _oldObjectSize.Height + (__newLocation.Y - _oldParentLocation.Y);
                                        if (__newHeight > 20)
                                        {
                                            this._converObject.Size = new Size(this._converObject.Width, __newObjectHeight);
                                            this.Size = new Size(this.Width, __newHeight);
                                        }
                                    }
                                    else
                                        if (_pinSelectID == 6)
                                        {
                                            // ปุ่มล่างซ้าย
                                            int __newHeight = _oldSize.Height + (__newLocation.Y - _oldParentLocation.Y);
                                            int __newWidth = _oldSize.Width + (_oldParentLocation.X - __newLocation.X);
                                            //
                                            int __newObjectHeight = _oldObjectSize.Height + (__newLocation.Y - _oldParentLocation.Y);
                                            int __newObjectWidth = _oldObjectSize.Width + (_oldParentLocation.X - __newLocation.X);
                                            //
                                            __newPoint = new Point(_fixedLocation.X - (_oldParentLocation.X - __newLocation.X), _fixedLocation.Y);
                                            __newPointObject = new Point(_fixedObjectLocation.X - (_oldParentLocation.X - __newLocation.X), _fixedObjectLocation.Y);
                                            if (__newHeight > 20 && __newWidth > 20)
                                            {
                                                this._converObject.Location = __newPointObject;
                                                this._converObject.Size = new Size(__newObjectWidth, __newObjectHeight);
                                                this.Location = __newPoint;
                                                this.Size = new Size(__newWidth, __newHeight);
                                            }
                                        }
                                        else
                                            if (_pinSelectID == 7)
                                            {
                                                // ปุ่มขวากลาง
                                                int __newWidth = _oldSize.Width + (_oldParentLocation.X - __newLocation.X);
                                                int __newObjectWidth = _oldObjectSize.Width + (_oldParentLocation.X - __newLocation.X);
                                                if (__newWidth > 20)
                                                {
                                                    this._converObject.Location = __newPointObject;
                                                    this._converObject.Size = new Size(__newObjectWidth, this._converObject.Height);
                                                    this.Location = __newPoint;
                                                    this.Size = new Size(__newWidth, this.Height);
                                                }
                                            }
                this.Invalidate();
                this._converObject.Invalidate();
            }
            else
                if (_controlMoveMode == true)
                {
                    Point __newLocation = Parent.PointToClient(Control.MousePosition);
                    Point __newPoint = new Point(__newLocation.X - _oldLocation.X, __newLocation.Y - _oldLocation.Y);
                    Point __newPointObject = new Point(__newLocation.X - _oldObjectLocation.X, __newLocation.Y - _oldObjectLocation.Y);
                    if (__newPointObject.X < 0) __newPointObject.X = 0;
                    if (__newPointObject.Y < 0) __newPointObject.Y = 0;
                    if (__newPointObject.X + __getControl.Width > __getControl.Parent.Width) __newPointObject.X = __getControl.Parent.Width - __getControl.Width;
                    if (__newPointObject.Y + __getControl.Height > __getControl.Parent.Height) __newPointObject.Y = __getControl.Parent.Height - __getControl.Height;

                    this.Location = __newPoint;
                    this._converObject.Location = __newPointObject;
                }
                else
                {
                    Point __currentPoint = this.PointToClient(Control.MousePosition);
                    int __getPin = _getPin(__currentPoint);
                    if (__getPin == -1)
                    {
                        this.Cursor = Cursors.SizeAll;
                        _pinSelectID = -1;
                    }
                }
        }


        void _selectCover_Paint(object sender, PaintEventArgs e)
        {
            Graphics __g = e.Graphics;
            Pen __myPen = new Pen((_controlMoveMode) ? Color.LightGray : Color.Black, (_controlMoveMode) ? 2 : 0);
            __myPen.DashStyle = (_controlMoveMode) ? DashStyle.Solid : DashStyle.Dash;
            _coverPin[0] = new Point(3, 3);
            _coverPin[1] = new Point((3 + (this.Width - 4)) / 2, 3);
            _coverPin[2] = new Point(this.Width - 4, 3);
            _coverPin[3] = new Point(this.Width - 4, (3 + (this.Height - 4)) / 2);
            _coverPin[4] = new Point(this.Width - 4, this.Height - 4);
            _coverPin[5] = new Point((3 + (this.Width - 4)) / 2, this.Height - 4);
            _coverPin[6] = new Point(3, this.Height - 4);
            _coverPin[7] = new Point(3, (3 + (this.Height - 4)) / 2);
            Point[] __point = {_coverPin[0],
                _coverPin[2],
                _coverPin[4],
                _coverPin[6],
                _coverPin[0]
            };
            __g.DrawLines(__myPen, __point);
            if (_controlMoveMode == false)
            {
                Pen __myPenPin = new Pen(Color.Black, 0);
                for (int __loop = 0; __loop < 8; __loop++)
                {
                    Point[] __pinPoint = { new Point(_coverPin[__loop].X-2,_coverPin[__loop].Y-2),
                    new Point(_coverPin[__loop].X+2,_coverPin[__loop].Y-2),
                    new Point(_coverPin[__loop].X+2,_coverPin[__loop].Y+2),
                    new Point(_coverPin[__loop].X-2,_coverPin[__loop].Y+2),
                    new Point(_coverPin[__loop].X-2,_coverPin[__loop].Y-2)
                };
                    GraphicsPath __box = new GraphicsPath();
                    SolidBrush __brush = new SolidBrush(Color.White);
                    __box.AddPolygon(__pinPoint);
                    __g.FillPolygon(__brush, __pinPoint, FillMode.Winding);
                    __g.DrawPath(__myPenPin, __box);
                }
            }
        }

        public void _refresh()
        {
            if (_converObject != null)
            {
                if (this.Visible)
                {
                    int __x = (_converObject.Location.X - 6);
                    int __y = (_converObject.Location.Y - 6);
                    this.Location = new Point(__x, __y);
                    this.Size = new Size(_converObject.Width + 12, _converObject.Height + 12);
                    this.BringToFront();
                    this._converObject.BringToFront();
                }
            }
        }
    }
}
