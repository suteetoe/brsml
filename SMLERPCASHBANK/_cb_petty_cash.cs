using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPCASHBANK
{
    public partial class _cb_petty_cash : UserControl
    {
        public _cb_petty_cash()
        {
            InitializeComponent();
            this._pettyCashMasterControl1._closeButton.Click += new EventHandler(_closeButton_Click);
            this._pettyCashMasterControl1._myManagePettyCash._closeScreen += new MyLib.CloseScreenEvent(_myManagePettyCash__closeScreen);
        }

        void _myManagePettyCash__closeScreen()
        {
            this.Dispose();
        }

        void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
