using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLPosClient.control
{
    public partial class _functionButton : UserControl
    {
        public delegate void _functionClickHandler(object __sender, string __buttonText);
        public event _functionClickHandler _functionClick;

        public MyLib.VistaButton _button01
        {
            set { this.vistaButton1 = value; }
            get { return this.vistaButton1; }
        }

        public MyLib.VistaButton _button02
        {
            set { this.vistaButton2 = value; }
            get { return this.vistaButton2; }
        }

        public MyLib.VistaButton _button03
        {
            set { this.vistaButton3 = value; }
            get { return this.vistaButton3; }
        }

        public MyLib.VistaButton _button04
        {
            set { this.vistaButton4 = value; }
            get { return this.vistaButton4; }
        }

        public MyLib.VistaButton _button05
        {
            set { this.vistaButton5 = value; }
            get { return this.vistaButton5; }
        }

        public MyLib.VistaButton _button06
        {
            set { this.vistaButton6 = value; }
            get { return this.vistaButton6; }
        }

        public MyLib.VistaButton _button07
        {
            set { this.vistaButton7 = value; }
            get { return this.vistaButton7; }
        }

        public MyLib.VistaButton _button08
        {
            set { this.vistaButton8 = value; }
            get { return this.vistaButton8; }
        }

        public _functionButton()
        {
            InitializeComponent();

            this._addEvent(this);
        }

        private void _addEvent(Control __controls)
        {
            foreach (Control __getControl in __controls.Controls)
            {
                if (__getControl.GetType() == typeof(MyLib.VistaButton))
                {
                    MyLib.VistaButton __getButton = (MyLib.VistaButton)__getControl;
                    __getButton.Click += new EventHandler(__getButton_Click);
                }
                this._addEvent(__getControl);
            }
        }

        void __getButton_Click(object sender, EventArgs e)
        {
            if (this._functionClick != null)
            {
                MyLib.VistaButton __getButton = (MyLib.VistaButton)sender;
                this._functionClick(sender, __getButton.ButtonText);
            }
        }
    }
}
