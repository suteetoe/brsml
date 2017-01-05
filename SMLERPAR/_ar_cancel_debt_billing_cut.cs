using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPAR
{
    public partial class _ar_cancel_debt_billing_cut : UserControl
    {
        public _ar_cancel_debt_billing_cut()
        {
            InitializeComponent();
            this._ar_ap_trans1._myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
            this._ar_ap_trans1._closeButton.Click += new EventHandler(_closeButton_Click);
        }

        void _myManageData1__closeScreen()
        {
            this.Dispose();
        }

        void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
