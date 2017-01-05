using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLPosClient.control
{
    public partial class _cashButton : UserControl
    {
        public delegate void _moneyClickHandler(object __sender, string __buttonText);
        public event _moneyClickHandler _moneyClick;

        public MyLib.VistaButton _button01
        {
            set { this._bank1000Button = value; }
            get { return this._bank1000Button; }
        }

        public MyLib.VistaButton _button02
        {
            set { this._bank500Button = value; }
            get { return this._bank500Button; }
        }

        public MyLib.VistaButton _button03
        {
            set { this._bank100Button = value; }
            get { return this._bank100Button; }
        }

        public MyLib.VistaButton _button04
        {
            set { this._bank50Button = value; }
            get { return this._bank50Button; }
        }

        public MyLib.VistaButton _button05
        {
            set { this._bank20Button = value; }
            get { return this._bank20Button; }
        }

        public MyLib.VistaButton _button06
        {
            set { this._bank10Button = value; }
            get { return this._bank10Button; }
        }

        public MyLib.VistaButton _button07
        {
            set { this._bank5Button = value; }
            get { return this._bank5Button; }
        }

        public MyLib.VistaButton _button08
        {
            set { this._bank1Button = value; }
            get { return this._bank1Button; }
        }

        public _cashButton()
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
            if (this._moneyClick != null)
            {
                MyLib.VistaButton __getButton = (MyLib.VistaButton)sender;
                this._moneyClick(sender, __getButton.ButtonText);
            }
        }
    }
}
