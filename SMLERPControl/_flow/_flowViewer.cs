using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPControl._flow
{
    public class _flowViewer : UserControl
    {
        Control __template;
        double __persendWidth;
        double __persendHeight;
        double __oldWidth;
        double __oldHeight;

        public _flowViewer(Control templateControl)
        {
            this.DoubleBuffered = true;
            this.__template = templateControl;
            this.Dock = DockStyle.Fill;
            this.__oldWidth = templateControl.Width;
            this.__oldHeight = templateControl.Height;
            this.Controls.Add(templateControl);
            this.SizeChanged += new EventHandler(_flowViewer_SizeChanged);
        }

        void _adjustObject(Control control)
        {
            control.SuspendLayout();
            if (control.GetType() == typeof(_arrowObject))
            {
                _arrowObject __myControl = (_arrowObject)control;
                __myControl.Location = new Point((int)((double)__myControl.OriginalLocation.X * this.__persendWidth), (int)((double)__myControl.OriginalLocation.Y * this.__persendHeight));
                __myControl.Size = new Size((int)((double)__myControl.OriginalSize.Width * this.__persendWidth), (int)((double)__myControl.OriginalSize.Height * this.__persendHeight));
            }
            else
                if (control.GetType() == typeof(MyLib._grouper))
                {
                    MyLib._grouper __myControl = (MyLib._grouper)control;
                    __myControl.Location = new Point((int)((double)__myControl.OriginalLocation.X * this.__persendWidth), (int)((double)__myControl.OriginalLocation.Y * this.__persendHeight));
                    __myControl.Size = new Size((int)((double)__myControl.OriginalSize.Width * this.__persendWidth), (int)((double)__myControl.OriginalSize.Height * this.__persendHeight));
                }
                else
                    if (control.GetType() == typeof(MyLib._myPanel))
                    {
                        MyLib._myPanel __myControl = (MyLib._myPanel)control;
                        __myControl.Location = new Point((int)((double)__myControl.OriginalLocation.X * this.__persendWidth), (int)((double)__myControl.OriginalLocation.Y * this.__persendHeight));
                        __myControl.Size = new Size((int)((double)__myControl.OriginalSize.Width * this.__persendWidth), (int)((double)__myControl.OriginalSize.Height * this.__persendHeight));
                    }
                    else
                    if (control.GetType() == typeof(_flowObject))
                    {
                        _flowObject __myControl = (_flowObject)control;
                        __myControl.Location = new Point((int)((double)__myControl.OriginalLocation.X * this.__persendWidth), (int)((double)__myControl.OriginalLocation.Y * this.__persendHeight));
                        __myControl.Size = new Size((int)((double)__myControl.OriginalSize.Width * this.__persendWidth), (int)((double)__myControl.OriginalSize.Height * this.__persendHeight));
                    }
            foreach (Control __getControl in control.Controls)
            {
                _adjustObject(__getControl);
            }
            control.ResumeLayout(false);
        }

        void _flowViewer_SizeChanged(object sender, EventArgs e)
        {
            this.SuspendLayout();
            this.__persendWidth = (double)this.Size.Width / (double)this.__oldWidth;
            this.__persendHeight = (double)this.Size.Height / (double)this.__oldHeight;
            this.__template.Size = this.Size;
            _adjustObject(this);
            this.ResumeLayout(false);
        }
    }
}
