using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace SMLERPDashBoard
{
    public partial class _borderControl : UserControl
    {
        private Boolean _isMove = false;
        private Boolean _isResize = false;
        private Boolean _isMouseDown = false;
        int _lastControlLocationX = 0;
        int _lastControlLocationY = 0;
        int _lastMouseLocationX = 0;
        int _lastMouseLocationY = 0;

        public _borderControl()
        {
            InitializeComponent();
            this.Invalidated += new InvalidateEventHandler(_borderControl_Invalidated);
            this.MouseMove += new MouseEventHandler(_borderControl_MouseMove);
            this.MouseLeave += new EventHandler(_borderControl_MouseLeave);
            this.MouseDown += new MouseEventHandler(_borderControl_MouseDown);
            this.MouseUp += new MouseEventHandler(_borderControl_MouseUp);
            this._closePictureBox.Click += new EventHandler(_closePictureBox_Click);
        }

        void _borderControl_MouseUp(object sender, MouseEventArgs e)
        {
            this._isMouseDown = false;
            if (this._isMove)
            {
                Cursor.Current = Cursors.SizeAll;
            }
        }

        void _borderControl_MouseDown(object sender, MouseEventArgs e)
        {
            this._isMouseDown = true;
            this._lastControlLocationX = this.Location.X;
            this._lastControlLocationY = this.Location.Y;
            this._lastMouseLocationX = e.X;
            this._lastMouseLocationY = e.Y;
        }

        void _closePictureBox_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _borderControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (this._isMouseDown)
            {
                if (this._isResize)
                {
                    Cursor.Current = Cursors.SizeNWSE;
                    this.Size = new Size(e.X, e.Y);
                    this.Invalidate();
                }
                if (this._isMove)
                {
                    Cursor.Current = Cursors.SizeAll;
                    int xNew = this._lastControlLocationX + (e.X - this._lastMouseLocationX);
                    int yNew = this._lastControlLocationY + (e.Y - this._lastMouseLocationY);
                    ((UserControl)sender).Location = new Point(xNew, yNew);
                    this._lastControlLocationX = ((UserControl)sender).Location.X;
                    this._lastControlLocationY = ((UserControl)sender).Location.Y;
                    this.Invalidate();
                }
            }
            else
            {
                this._isMove = false;
                this._isResize = false;
                if (e.Y < 10 || e.Y > this.Height - 10 && e.X < this.Width - 10)
                {
                    Cursor.Current = Cursors.SizeAll;
                    this._isMove = true;
                }
                else
                    if (e.Y > this.Height - 10 && e.X > this.Width - 10)
                    {
                        Cursor.Current = Cursors.SizeNWSE;
                        this._isResize = true;
                    }
                    else
                    {
                        Cursor.Current = Cursors.Default;
                    }
            }
        }

        void _borderControl_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Default;
            this._isMouseDown = false;
        }

        void _borderControl_Invalidated(object sender, InvalidateEventArgs e)
        {
            this._closePictureBox.Location = new Point(this.Width - 16, 0);
        }
    }
}
